using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
public partial class mis_Finance_TDS_TCS_Master : System.Web.UI.Page
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
                lblMsg.Text = "";
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtTCS_ApplicableOn.Attributes.Add("readonly", "readonly");
                txtTDS_ApplicableOn.Attributes.Add("readonly", "readonly");
                FillDetails();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillDetails()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpFinTCS_TDS_Master", new string[] { "flag", "Office_ID" }, new string[] { "1", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtTCS_ApplicableOn.Text = ds.Tables[0].Rows[0]["TCS_ApplicableOn"].ToString();
                txtTCS_Pan_Exist_TurnoverAmt.Text = ds.Tables[0].Rows[0]["TCS_Pan_Exist_TurnoverAmt"].ToString();
                txtTCS_Pan_Exist_TurnoverBeforeRate.Text = ds.Tables[0].Rows[0]["TCS_Pan_Exist_TurnoverBeforeRate"].ToString();
                txtTCS_Pan_Exist_TurnoverAfterRate.Text = ds.Tables[0].Rows[0]["TCS_Pan_Exist_TurnoverAfterRate"].ToString();
                txtTCS_Pan_NotExist_TurnoverAmt.Text = ds.Tables[0].Rows[0]["TCS_Pan_NotExist_TurnoverAmt"].ToString();
                txtTCS_Pan_NotExist_TurnoverBeforeRate.Text = ds.Tables[0].Rows[0]["TCS_Pan_NotExist_TurnoverBeforeRate"].ToString();
                txtTCS_Pan_NotExist_TurnoverAfterRate.Text = ds.Tables[0].Rows[0]["TCS_Pan_NotExist_TurnoverAfterRate"].ToString();

                txtTDS_ApplicableOn.Text = ds.Tables[0].Rows[0]["TDS_ApplicableOn"].ToString();
                txtTDS_Pan_Exist_Rate.Text = ds.Tables[0].Rows[0]["TDS_Pan_Exist_Rate"].ToString();
                txtTDS_Pan_NotExist_Rate.Text = ds.Tables[0].Rows[0]["TDS_Pan_NotExist_Rate"].ToString();
            }
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
            if (txtTCS_ApplicableOn.Text == "")
            {
                msg += "Select TCS Applicable On Date. \\n";
            }
            if (txtTCS_Pan_Exist_TurnoverAmt.Text == "")
            {
                msg += "Enter TCS if Pan Exist Turnover Amt. \\n";
            }
            if (txtTCS_Pan_Exist_TurnoverBeforeRate.Text == "")
            {
                msg += "Enter TCS if Pan Exist Turnover Before Rate. \\n";
            }
            if (txtTCS_Pan_Exist_TurnoverAfterRate.Text == "")
            {
                msg += "Enter TCS if Pan Exist Turnover After Rate. \\n";
            }
            if (txtTCS_Pan_NotExist_TurnoverAmt.Text == "")
            {
                msg += "Enter TCS Pan Not Exist Turnover Amt. \\n";
            }
            if (txtTCS_Pan_NotExist_TurnoverBeforeRate.Text == "")
            {
                msg += "Enter TCS Pan Not Exist Turnover Before Rate. \\n";
            }
            if (txtTCS_Pan_NotExist_TurnoverAfterRate.Text == "")
            {
                msg += "Enter TCS Pan Not Exist Turnover After Rate. \\n";
            }
            if (txtTDS_ApplicableOn.Text == "")
            {
                msg += "Select TDS Applicable On Date. \\n";
            }
            if (txtTDS_Pan_Exist_Rate.Text == "")
            {
                msg += "Enter TDS Pan Exist Rate. \\n";
            }
            if (txtTDS_Pan_NotExist_Rate.Text == "")
            {
                msg += "Enter TDS Pan Not Exist Rate. \\n";
            }
            if (msg.Trim() == "")
            {

                objdb.ByProcedure("SpFinTCS_TDS_Master",
                      new string[] { "flag","TCS_ApplicableOn","TCS_Pan_Exist_TurnoverAmt","TCS_Pan_Exist_TurnoverBeforeRate","TCS_Pan_Exist_TurnoverAfterRate","TCS_Pan_NotExist_TurnoverAmt","TCS_Pan_NotExist_TurnoverBeforeRate","TCS_Pan_NotExist_TurnoverAfterRate","TDS_ApplicableOn","TDS_Pan_Exist_Rate","TDS_Pan_NotExist_Rate","Office_ID","UpdatedBy" },
                      new string[] { "0", Convert.ToDateTime(txtTCS_ApplicableOn.Text, cult).ToString("yyyy/MM/dd"), txtTCS_Pan_Exist_TurnoverAmt.Text, txtTCS_Pan_Exist_TurnoverBeforeRate.Text, txtTCS_Pan_Exist_TurnoverAfterRate.Text, txtTCS_Pan_NotExist_TurnoverAmt.Text, txtTCS_Pan_NotExist_TurnoverBeforeRate.Text, txtTCS_Pan_NotExist_TurnoverAfterRate.Text, Convert.ToDateTime(txtTDS_ApplicableOn.Text, cult).ToString("yyyy/MM/dd"), txtTDS_Pan_Exist_Rate.Text, txtTDS_Pan_NotExist_Rate.Text, ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");



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
}