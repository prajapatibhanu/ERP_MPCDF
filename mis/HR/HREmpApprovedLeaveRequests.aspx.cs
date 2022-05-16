using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_HR_HREmpApprovedLeaveRequests : System.Web.UI.Page
{
    DataSet ds, ds2;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();

                    // txtReason.Attributes.Add("readonly", "readonly");
                    FillDropdown();
                    FillGrid();
                }
                else
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillDropdown()
    {
        try
        {
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlFinancialYear.DataSource = ds;
                ddlFinancialYear.DataTextField = "Year";
                ddlFinancialYear.DataValueField = "Year";
                ddlFinancialYear.DataBind();
                ddlFinancialYear.Items.Insert(0, new ListItem("Select Year", "0"));
            }
            ddlFinancialYear.SelectedValue = DateTime.Now.Year.ToString(); ;

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
            lblMsg2.Text = "";
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = objdb.ByProcedure("SpHRBalanceLeaveDetail", new string[] { "flag", "Office_ID", "LeaveApproveAuthority", "Year", "Financial_Year" },
                  new string[] { "28", ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString(), ddlFinancialYear.SelectedValue.ToString(), ddlFinancialYear.SelectedValue.ToString() }, "datatset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
            else
            {
                // lblMsg2.Text = "No Record Found...";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }












    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
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

            if (e.CommandName == "RowCancelingEdit")
            {
                Control ctrl = e.CommandSource as Control;
                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                Label lbLeaveStatus = (Label)row.FindControl("lbLeaveStatusreal");
                Label lblleavetype = (Label)row.FindControl("lblleavetype");
                Label lblemloyeeID = (Label)row.FindControl("lblemloyeeID");
                string LeaveId = e.CommandArgument.ToString();
                DataSet ds1;
                string DocPath = "";
                string OrderDate = "";
                lblMsg.Text = "";
                string msg = "";
                string TakenLeave = "";
                int DayCount = 0;
                decimal TotalTakenLeave = 0;
                ds = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveType_ID", "Financial_Year", "Emp_ID" },
              new string[] { "38", lblleavetype.Text, ddlFinancialYear.SelectedValue.ToString(), ViewState["Emp_ID"].ToString() }, "datatset");

                string financialYear = ds.Tables[0].Rows[0]["Financial_Year"].ToString();
                string LeaveDay1 = ds.Tables[0].Rows[0]["Leave_Days"].ToString();

                if (lbLeaveStatus.Text == "Approved")
                {
                    ds1 = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId" },
                new string[] { "8", LeaveId.ToString() }, "datatset");
                    if (ds1 != null && ds1.Tables.Count != 0)
                    {
                        if (ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "First Half" || ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "Second Half" || ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "Half Day")
                        {
                            TakenLeave = "0.5";
                            TotalTakenLeave = Convert.ToDecimal(TakenLeave.ToString());
                        }
                        else
                        {
                            int count = ds1.Tables[1].Rows.Count;
                            for (int i = 0; i < count; i++)
                            {
                                string DayName = ds1.Tables[1].Rows[i]["DayName"].ToString();
                                string Leave_Date = ds1.Tables[1].Rows[i]["LeaveDate"].ToString();
                                if (DayName == "Sunday")
                                {
                                    DayCount = DayCount;
                                }
                                else
                                {
                                    int status = 1;
                                    DataSet ds2 = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId" },
                 new string[] { "21", LeaveId.ToString() }, "datatset");
                                    if (ds1 != null)
                                    {
                                        int leaveCount = ds2.Tables[0].Rows.Count;
                                        if (ds2 != null && ds2.Tables[0].Rows.Count != 0)
                                        {
                                            for (int j = 0; j < leaveCount; j++)
                                            {
                                                string Holiday_Date = ds2.Tables[0].Rows[j]["Holiday_Date"].ToString();
                                                if (Leave_Date == Holiday_Date)
                                                {
                                                    status = 0;
                                                    break;
                                                }
                                                else
                                                {
                                                    status = 1;
                                                }
                                            }
                                        }
                                        if (status == 1)
                                        {
                                            DayCount++;
                                        }
                                        else
                                        {
                                            DayCount = DayCount;
                                        }
                                    }
                                }
                            }
                            TotalTakenLeave = DayCount;


                            ds1 = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "Emp_ID", "LeaveType", "TotalAllowedLeave", "TotalTakenLeave", "FinancialYear" },
               new string[] { "43", lblemloyeeID.Text, lblleavetype.Text, LeaveDay1, TotalTakenLeave.ToString(), financialYear }, "datatset");


                            ds1 = objdb.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId", "LeaveCancelBy" },
                              new string[] { "42", LeaveId, ViewState["Emp_ID"].ToString() }, "datatset");
                            if (ds1.Tables.Count > 0)
                            {
                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    FillGrid();
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "success!", ds1.Tables[0].Rows[0]["msg"].ToString());
                                }
                            }
                            //TotalTakenLeave = Convert.ToDecimal(ds1.Tables[0].Rows[0]["TakenLeave"].ToString());
                        }
                    }

                }
                else
                {
                    TotalTakenLeave = 0;
                }






            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
}