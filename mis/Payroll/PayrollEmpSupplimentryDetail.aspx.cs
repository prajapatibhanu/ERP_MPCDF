using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollEmpSupplimentryDetail : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Attributes.Add("readonly", "readonly");
                DivDetail.Visible = false;
                TextReadonly();
                FillDropdown();
                //FillEDDetails();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
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
            ddlOfficeName.SelectedValue = Session["Office_ID"].ToString();
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
            ds = null;
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // ==== Month ====
                ddlYear.DataSource = ds;
                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
                // ==== Search Year ====

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillEDDetails()
    {
        try
        {
            lblMsg.Text = "";
            RepeaterEarning.DataSource = null;
            RepeaterEarning.DataBind();
            RepeaterDeduction.DataSource = null;
            RepeaterDeduction.DataBind();
            ds = objdb.ByProcedure("USP_tblPayrollEmpSupplimentryDetail", new string[] { "flag", "Office_ID" },
                new string[] { "2", Session["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                DivDetail.Visible = true;

                RepeaterEarning.DataSource = ds.Tables[0];
                RepeaterEarning.DataBind();

                RepeaterDeduction.DataSource = ds.Tables[1];
                RepeaterDeduction.DataBind();
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
            txtTotalEarning.Text = "0";
            txtTotalDeduction.Text = "0";
            txtNetPayment.Text = "0";
            txtEarnRemainingSalary_Basic.Text = "0";
            txtETotalRemaining.Text = "0";
            //txtDeductionPaidSalary.Text = "0";
            //txtDeductionRemainingSalary.Text = "0";
            txtPolicyRemaining.Text = "0";
            // txtDTotalRemaining.Text = "";
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
        txtTotalEarning.Attributes.Add("readonly", "readonly");
        txtTotalDeduction.Attributes.Add("readonly", "readonly");
        txtNetPayment.Attributes.Add("readonly", "readonly");
        txtETotalRemaining.Attributes.Add("readonly", "readonly");
        txtDTotalRemaining.Attributes.Add("readonly", "readonly");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMsg.Text = "";
            GetCompareDate();
        }
    }
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ClearText();
            GetSupplimentryDetailsByEmpId();
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

            if (Page.IsValid)
            {
                lblMsg.Text = "";
                DivDetail.Visible = false;
                ClearText();
                FillEDDetails();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetSupplimentryDetailsByEmpId()
    {
        try
        {

            GridView1.DataSource = null;
            GridView1.DataBind();
            if (ddlEmployee.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("USP_tblPayrollEmpSupplimentryDetail", new string[] { "flag", "Emp_ID", }, new string[] { "3", ddlEmployee.SelectedValue.ToString(), }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = new string[] { };
                    GridView1.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetCompareDate()
    {
        try
        {
            string myStringfromdat = txtFromDate.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string fdat = fdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            string myStringtodate = txtToDate.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string tdat = tdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                InsertSupplimentryDetails();
            }
            else
            {

                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
        }
    }
    private void InsertSupplimentryDetails()
    {
        DataSet ds1 = new DataSet();
        try
        {
            string fromyear = "", frommonth = "", frommonthname = "", toyear = "", tomonth = "", tomonthname = "";


            // ===Earning===
            if (txtEarnRemainingSalary_Basic.Text == "")
                txtEarnRemainingSalary_Basic.Text = "0";
            if (txtETotalRemaining.Text == "")
                txtETotalRemaining.Text = "0";
            if (txtTotalEarning.Text == "")
                txtTotalEarning.Text = "0";


            // ===Deduction ===
            if (txtPolicyRemaining.Text == "")
                txtPolicyRemaining.Text = "0";
            if (txtDTotalRemaining.Text == "")
                txtDTotalRemaining.Text = "0";
            if (txtTotalDeduction.Text == "")
                txtTotalDeduction.Text = "0";
            if (txtNetPayment.Text == "")
                txtNetPayment.Text = "0";



            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime Todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            DateTime minusday = fromdate.AddDays(-1);
            string fdate = fromdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string tdate = Todate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            fromyear = fromdate.Year.ToString();
            frommonth = fromdate.Month.ToString();
            frommonthname = fromdate.ToString("MMMM");

            toyear = Todate.Year.ToString();
            tomonth = Todate.Month.ToString();
            tomonthname = Todate.ToString("MMMM");

            DataTable dtEarning = new DataTable();
            DataRow drE;

            dtEarning.Columns.Add("Emp_ID", typeof(int));
            dtEarning.Columns.Add("EarnDedution_ID", typeof(int));
            dtEarning.Columns.Add("EarnDeductionName", typeof(string));
            dtEarning.Columns.Add("RemainingAmount", typeof(decimal));
            dtEarning.Columns.Add("EarnDeductionType", typeof(string));
            drE = dtEarning.NewRow();
            foreach (RepeaterItem item in RepeaterEarning.Items)
            {
                Label lblEarnDeduction_ID = (Label)item.FindControl("lblEarnDeduction_ID");
                Label lblEarnDeduction_Name = (Label)item.FindControl("lblEarnDeduction_Name");
                TextBox txtRemainingAmount = (TextBox)item.FindControl("txtRemainingEarning");

                if (txtRemainingAmount.Text == "")
                    txtRemainingAmount.Text = "0";

                drE[0] = ddlEmployee.SelectedValue;
                drE[1] = lblEarnDeduction_ID.Text;
                drE[2] = lblEarnDeduction_Name.Text;
                drE[3] = txtRemainingAmount.Text;
                drE[4] = "Earning";

                dtEarning.Rows.Add(drE.ItemArray);
            }

            DataTable dtDeduction = new DataTable();
            DataRow drD;

            dtDeduction.Columns.Add("Emp_ID", typeof(int));
            dtDeduction.Columns.Add("EarnDedution_ID", typeof(int));
            dtDeduction.Columns.Add("EarnDeductionName", typeof(string));
            dtDeduction.Columns.Add("RemainingAmount", typeof(decimal));
            dtDeduction.Columns.Add("EarnDeductionType", typeof(string));
            drD = dtDeduction.NewRow();
            foreach (RepeaterItem item in RepeaterDeduction.Items)
            {
                Label lblEarnDeduction_ID = (Label)item.FindControl("lblEarnDeduction_ID");
                Label lblEarnDeduction_Name = (Label)item.FindControl("lblEarnDeduction_Name");
                TextBox txtRemainingAmount = (TextBox)item.FindControl("txtRemainingDeduction");
                if (txtRemainingAmount.Text == "")
                    txtRemainingAmount.Text = "0";


                drD[0] = ddlEmployee.SelectedValue;
                drD[1] = lblEarnDeduction_ID.Text;
                drD[2] = lblEarnDeduction_Name.Text;
                drD[3] = txtRemainingAmount.Text;
                drD[4] = "Deduction";
                dtDeduction.Rows.Add(drD.ItemArray);

            }



            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                if (dtEarning.Rows.Count > 0 && dtDeduction.Rows.Count > 0)
                {
                    ds1 = objdb.ByProcedure("USP_tblPayrollEmpSupplimentryDetail",
                                            new string[] { "flag", "Office_ID", "Emp_ID", "FromDate", "ToDate"
                                      , "CurrentYear", "CurrentMonth", "FromYear", "FromMonth", "FromMonthName"
                                      , "ToYear", "ToMonth", "ToMonthName", "BasicSalary"
                                      , "TotalEarning", "Policy", "TotalDeduction", "NetPaymentAmount", "Supplimentry_UpdatedBy" },
                                            new string[] { "1", ddlOfficeName.SelectedValue, ddlEmployee.SelectedValue,fdate.ToString(),
                                       tdate.ToString() , ddlYear.SelectedValue, ddlMonth.SelectedValue, fromyear,frommonth, 
                                       frommonthname,toyear,tomonth, tomonthname,txtEarnRemainingSalary_Basic.Text, txtETotalRemaining.Text,
                                       txtPolicyRemaining.Text, txtDTotalRemaining.Text, txtNetPayment.Text, Convert.ToString(Session["Emp_ID"]) },
                            new string[] { "type_E_tblPayrollEmpSupplimentryEarnDeductionDetail", "type_D_tblPayrollEmpSupplimentryEarnDeductionDetail" },
                            new DataTable[] { dtEarning, dtDeduction }, "dataset");
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            dtEarning.Dispose();
                            dtDeduction.Dispose();
                            ddlYear.ClearSelection();
                            ddlMonth.ClearSelection();
                            ClearText();
                            GetSupplimentryDetailsByEmpId();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }

                    }

                }
            }
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }

    protected void txtEarnRemainingSalary_Basic_TextChanged(object sender, EventArgs e)
    {
      

        if(txtEarnRemainingSalary_Basic.Text!="" && txtEarnRemainingSalary_Basic.Text!="0" && txtEarnRemainingSalary_Basic.Text!="0.00")
        {

            decimal basic = 0,da = 2000,epf=0,te=0,td=0,tn=0;
           
         

            foreach (RepeaterItem item in RepeaterEarning.Items)
            {
                Label lblEarnDeduction_ID = (Label)item.FindControl("lblEarnDeduction_ID");
                TextBox txtRemainingEarning = (TextBox)item.FindControl("txtRemainingEarning");
                if (lblEarnDeduction_ID.Text == "1")
                {

                    basic = Convert.ToDecimal(txtEarnRemainingSalary_Basic.Text);
                    da = 0;
                    da = ((basic * 20) / 100);
                    txtRemainingEarning.Text = da.ToString("0");
                    te = (Convert.ToDecimal(txtTotalEarning.Text) + da);
                    txtETotalRemaining.Text = te.ToString("0");
                    txtTotalEarning.Text = te.ToString("0");
                    break;

                }
            }
            foreach (RepeaterItem item1 in RepeaterDeduction.Items)
            {
                Label lblEarnDeduction_ID = (Label)item1.FindControl("lblEarnDeduction_ID");
                TextBox txtRemainingDeduction = (TextBox)item1.FindControl("txtRemainingDeduction");
                if (lblEarnDeduction_ID.Text == "8")
                {
                    epf = (((basic + da) * 12) / 100);
                    txtRemainingDeduction.Text = epf.ToString("0");
                    td = (Convert.ToDecimal(txtTotalDeduction.Text) + epf);
                    txtTotalDeduction.Text = td.ToString("0");
                    txtDTotalRemaining.Text = td.ToString("0");
                    tn = Convert.ToDecimal(txtTotalEarning.Text) + Convert.ToDecimal(txtTotalDeduction.Text);
                    txtNetPayment.Text = Convert.ToString(tn);
                   break;
                }
            }
        }
    }
}