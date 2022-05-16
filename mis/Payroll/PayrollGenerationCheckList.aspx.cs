using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollGenerationCheckList : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Emp_ID"] != null)
            {

                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillDropdown();

            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
    }
    protected void FillDropdown()
    {
        try
        {
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
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
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["Selected_Year"] = ddlFinancialYear.SelectedValue.ToString();
        ViewState["Selected_Month"] = ddlMonth.SelectedValue.ToString();
        ViewState["Selected_Office_ID"] = ddlOfficeName.SelectedValue.ToString();

        FillForm();
    }

    public void FillForm()
    {

        try
        {


            lblMsg.Text = "";
            string msg = "";

            if (ddlOfficeName.SelectedIndex == 0)
            {
                msg += "Select Office. \\n";
            }
            if (ddlFinancialYear.SelectedIndex == 0)
            {
                msg += "Select Year.  \\n";
            }
            if (ddlMonth.SelectedIndex == 0)
            {
                msg += "Select Month.  \\n";
            }


            if (msg.Trim() == "")
            {

                

                txtCurrentMonthEmpCount.Text = "";
                txtCurrentMonthEmpRemark.Text = "";
                txtLastMonthEmpCount.Text = "";
                txtLastMonthEmpRemark.Text = "";

                txtEmpTransferIn.Text = "";
                txtEmpTransferInRemark.Text = "";
                txtEmpTransferOut.Text = "";
                txtEmpTransferOutRemark.Text = "";
                txtEmpRetire.Text = "";
                txtEmpRetireRemark.Text = "";

                txtEmpChangedBasic.Text = "";
                txtEmpChangedBasicRemark.Text = "";
                txtEmpDeduction.Text = "";
                txtEmpDeductionRemark.Text = "";
                txtEmpEarning.Text = "";
                txtEmpEarningRemark.Text = "";
                lblEmpDifference.Text = "";


                ds = objdb.ByProcedure("SpPayrollCheckList", new string[] { "flag", "Office_ID", "Year", "Month" }, new string[] { "0", ViewState["Selected_Office_ID"].ToString(), ViewState["Selected_Year"].ToString(), ViewState["Selected_Month"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    txtCurrentMonthEmpCount.Text = ds.Tables[0].Rows[0]["CurrentMonthEmployee"].ToString();
                    txtCurrentMonthEmpRemark.Text = ds.Tables[0].Rows[0]["CurrentMonthEmployeeRemark"].ToString();
                    txtLastMonthEmpCount.Text = ds.Tables[0].Rows[0]["LastMonthEmployee"].ToString();
                    txtLastMonthEmpRemark.Text = ds.Tables[0].Rows[0]["LastMonthEmployeeRemark"].ToString();

                    txtEmpTransferIn.Text = ds.Tables[0].Rows[0]["TransferIn"].ToString();
                    txtEmpTransferInRemark.Text = ds.Tables[0].Rows[0]["TransferInRemark"].ToString();
                    txtEmpTransferOut.Text = ds.Tables[0].Rows[0]["TransferOut"].ToString();
                    txtEmpTransferOutRemark.Text = ds.Tables[0].Rows[0]["TransferOutRemark"].ToString();
                    txtEmpRetire.Text = ds.Tables[0].Rows[0]["Retire"].ToString();
                    txtEmpRetireRemark.Text = ds.Tables[0].Rows[0]["RetireRemark"].ToString();

                    txtEmpChangedBasic.Text = ds.Tables[0].Rows[0]["BasicChange"].ToString();
                    txtEmpChangedBasicRemark.Text = ds.Tables[0].Rows[0]["BasicChangeRemark"].ToString();
                    txtEmpDeduction.Text = ds.Tables[0].Rows[0]["Deductions"].ToString();
                    txtEmpDeductionRemark.Text = ds.Tables[0].Rows[0]["DeductionsRemark"].ToString();
                    txtEmpEarning.Text = ds.Tables[0].Rows[0]["Earnings"].ToString();
                    txtEmpEarningRemark.Text = ds.Tables[0].Rows[0]["EarningsRemark"].ToString();

                    lblEmpDifference.Text = Math.Abs((Int32.Parse(ds.Tables[0].Rows[0]["CurrentMonthEmployee"].ToString()) - Int32.Parse(ds.Tables[0].Rows[0]["LastMonthEmployee"].ToString()))).ToString();
                }

                dvAssesmentForm.Visible = true;
            }
            else
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Something went wrong, Please refresh the page.');", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }

        }
        catch (Exception ex)
        {

        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            lblMsg.Text = "";
            string msg = "";


            if (txtCurrentMonthEmpCount.Text == "")
            {
                msg += "कृपया भरें -  1. पिछले माह कुल कितने अधिकारीयों / कर्मचारियों का वेतन पत्रक जनरेट किया गया ?   \\n";
            }
            else if (!objdb.isNumber(txtCurrentMonthEmpCount.Text))
            {
                msg += "केवल संख्या भरें - 1. पिछले माह कुल कितने अधिकारीयों / कर्मचारियों का वेतन पत्रक जनरेट किया गया | \\n";
            }

            if (txtLastMonthEmpCount.Text == "")
            {
                msg += "कृपया भरें -  2. इस माह कुल कितने अधिकारीयों / कर्मचारियों का वेतन पत्रक जनरेट किया जा रहा है ? \\n";
            }
            else if (!objdb.isNumber(txtLastMonthEmpCount.Text))
            {
                msg += " केवल संख्या भरें -  2. इस माह कुल कितने अधिकारीयों / कर्मचारियों का वेतन पत्रक जनरेट किया जा रहा है | \\n";
            }

            /******/
            if (txtEmpTransferIn.Text == "")
            {
                msg += "कृपया भरें -  3. इस माह अन्य स्थान से ट्रान्सफर होकर आए अधिकारी / कर्मचारी ? \\n";
            }
            else if (!objdb.isNumber(txtEmpTransferIn.Text))
            {
                msg += " केवल संख्या भरें -  3. इस माह अन्य स्थान से ट्रान्सफर होकर आए अधिकारी / कर्मचारी | \\n";
            }

            if (txtEmpTransferOut.Text == "")
            {
                msg += "कृपया भरें -  4. इस माह इस कार्यालय से अन्य स्थान पर ट्रान्सफर होकर गए अधिकारी / कर्मचारी ? \\n";
            }
            else if (!objdb.isNumber(txtEmpTransferOut.Text))
            {
                msg += " केवल संख्या भरें -  4. इस माह इस कार्यालय से अन्य स्थान पर ट्रान्सफर होकर गए अधिकारी / कर्मचारी | \\n";
            }

            if (txtEmpRetire.Text == "")
            {
                msg += "कृपया भरें -  5. इस माह रिटायर हुए अधिकारी / कर्मचारी ? \\n";
            }
            else if (!objdb.isNumber(txtEmpRetire.Text))
            {
                msg += " केवल संख्या भरें -  5. इस माह रिटायर हुए अधिकारी / कर्मचारी | \\n";
            }

            /********/

            if (txtEmpChangedBasic.Text == "")
            {
                msg += "कृपया भरें -  6. किसी अधिकारी / कर्मचारी का बेसिक वेतन में बदलाव  ? \\n";
            }
            else if (!objdb.isNumber(txtEmpChangedBasic.Text))
            {
                msg += " केवल संख्या भरें -  6. किसी अधिकारी / कर्मचारी का बेसिक वेतन में बदलाव  | \\n";
            }
            if (txtEmpDeduction.Text == "")
            {
                msg += "कृपया भरें -  7. किसी अधिकारी / कर्मचारी का अन्य कोई कतौत्रा ? ( हेड वार जानकारी देवें ) ? \\n";
            }
            else if (!objdb.isNumber(txtEmpDeduction.Text))
            {
                msg += " केवल संख्या भरें -  7. किसी अधिकारी / कर्मचारी का अन्य कोई कतौत्रा ? ( हेड वार जानकारी देवें ) | \\n";
            }
            if (txtEmpEarning.Text == "")
            {
                msg += "कृपया भरें -  8. किसी अधिकारी / कर्मचारी का अन्य कोई भुगतान ? ( हेड वार जानकारी देवें ) ? \\n";
            }
            else if (!objdb.isNumber(txtEmpEarning.Text))
            {
                msg += " केवल संख्या भरें -  8. किसी अधिकारी / कर्मचारी का अन्य कोई भुगतान ? ( हेड वार जानकारी देवें ) | \\n";
            }
            /**************/
            if (msg.Trim() == "")
            {

                objdb.ByProcedure("SpPayrollCheckList",
                    new string[] { "flag", "Office_ID", "Year", "Month", "IsActive", "CurrentMonthEmployee", "CurrentMonthEmployeeRemark", "LastMonthEmployee", "LastMonthEmployeeRemark", "TransferIn", "TransferInRemark", "TransferOut", "TransferOutRemark", "Retire", "RetireRemark", "BasicChange", "BasicChangeRemark", "Deductions", "DeductionsRemark", "Earnings", "EarningsRemark", "UpdatedBy" },
                    new string[] { "1", ViewState["Selected_Office_ID"].ToString(), ViewState["Selected_Year"].ToString(), ViewState["Selected_Month"].ToString(), "1", txtCurrentMonthEmpCount.Text, txtCurrentMonthEmpRemark.Text, txtLastMonthEmpCount.Text, txtLastMonthEmpRemark.Text, txtEmpTransferIn.Text, txtEmpTransferInRemark.Text, txtEmpTransferOut.Text, txtEmpTransferOutRemark.Text, txtEmpRetire.Text, txtEmpRetireRemark.Text, txtEmpChangedBasic.Text, txtEmpChangedBasicRemark.Text, txtEmpDeduction.Text, txtEmpDeductionRemark.Text, txtEmpEarning.Text, txtEmpEarningRemark.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");

                FillForm();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }



        }
        catch (Exception ex)
        {

        }
    }
}