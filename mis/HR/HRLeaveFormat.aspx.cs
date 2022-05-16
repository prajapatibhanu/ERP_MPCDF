using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Linq;




public partial class mis_HR_HRLeaveFormat : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["LeaveId"] != null)
            {
                //ViewState["LeaveId"] = objdb.Decrypt(Request.QueryString["LeaveId"].ToString());


                //ViewState["LeaveId"] = objdb.Decrypt("c0OxRJUqJUA1Y+XQks1ihQ==");
                //ViewState["LeaveId"] = objdb.Decrypt("2MS8lKjDncX85kKxFzbJZA==");
                //ViewState["LeaveId"] = objdb.Decrypt("PqFZ7OARMIoqG8HEGDj6Ow==");
                ViewState["LeaveId"] = objdb.Decrypt(Request.QueryString["LeaveId"].ToString());


                lblBalance.Text = "";
                lblDate.Text = "";
                lblDateLeave.Text = "";
                lblDays.Text = "";
                lblDesignation.Text = "";
                lblDivision.Text = "";
                lblEmpName.Text = "";
                lblEmpNo.Text = "";
                lblFrom.Text = "";
                lblOffice.Text = "";
                lblReason.Text = "";
                lblTo.Text = "";
                lblLeaveType.Text = "";

                ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveID" }, new string[] { "41", ViewState["LeaveId"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count != 0)
                {
                    mainbody.Visible = true;
                    mainbody2.Visible = false;
                    lblBalance.Text = ds.Tables[0].Rows[0]["BalanceLeave"].ToString();
                    lblDate.Text = ds.Tables[0].Rows[0]["LeaveAppliedOn"].ToString();
                    lblDateLeave.Text = ds.Tables[0].Rows[0]["LeaveAppliedOn"].ToString();
                    lblDays.Text = ds.Tables[0].Rows[0]["TotalLeaves"].ToString() + "  Day(s)";
                    lblDesignation.Text = ds.Tables[0].Rows[0]["Designation_Name"].ToString();
                    lblDivision.Text = ds.Tables[0].Rows[0]["Department_Name"].ToString();
                    lblEmpName.Text = ds.Tables[0].Rows[0]["Emp_Name"].ToString();
                    lblEmpNo.Text = ds.Tables[0].Rows[0]["UserName"].ToString();
                    lblFrom.Text = ds.Tables[0].Rows[0]["LeaveFromDate"].ToString();
                    lblOffice.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                    lblReason.Text = ds.Tables[0].Rows[0]["LeaveRemark"].ToString();
                    lblTo.Text = ds.Tables[0].Rows[0]["LeaveToDate"].ToString();
                    if (ds.Tables[0].Rows[0]["LeaveType"].ToString() == "2")
                    {
                        lblLeaveType.Text = "Casual Leave (CL)";
                    }
                    else if (ds.Tables[0].Rows[0]["LeaveType"].ToString() == "5")
                    {
                        lblLeaveType.Text = "Resticted Holiday (RH)";
                    }
                    else
                    {
                        lblLeaveType.Text = " ";
                    }

                }
                else
                {
                    mainbody.Visible = false;
                    mainbody2.Visible = true;
                }

            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {

        }

    }
}