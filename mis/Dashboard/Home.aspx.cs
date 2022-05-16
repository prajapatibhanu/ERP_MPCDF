using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;


public partial class mis_Dashboard_Home : System.Web.UI.Page
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
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillDetail();
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }

        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillDetail()
    {
        try
        {
            string imagePath = "";
            gvCirculerDetails.DataSource = null;
            gvCirculerDetails.DataBind();
            h4.Visible = false;
            ds = objdb.ByProcedure("SpEmailBuilder", new string[] { "flag", "EmailSentBy" },
                    new string[] { "2", ViewState["Emp_ID"].ToString() }, "datatset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                h4.Visible = true;
                gvCirculerDetails.DataSource = ds;
                gvCirculerDetails.DataBind();
                foreach (GridViewRow row in gvCirculerDetails.Rows)
                {
                    HyperLink hyp = (HyperLink)row.FindControl("hypUploadedDoc");
                    imagePath = hyp.NavigateUrl;
                    if (imagePath != "")
                    {
                        hyp.NavigateUrl = imagePath;
                        hyp.Enabled = true;
                    }
                    else
                    {
                        hyp.Style.Add("color", "red");
                        hyp.Enabled = false;
                    }

                }
            }
            //ds = null;
            //ds = objdb.ByProcedure("Sp_DashboardAllEmployee", new string[] { "flag", "emp_ID" }, new string[] { "0", ViewState["Emp_ID"].ToString() }, "dataset");

            //if (ds.Tables.Count > 0)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        // SELECT @FileOnDesk as FileOnDesk, @TotalGrievance as TotalGrievance,@OtherPendingLeave as OtherPendingLeave, @MyPendingLeave as MyPendingLeave
            //        //lblFileOnDesk.Text = ds.Tables[0].Rows[0]["FileOnDesk"].ToString();
            //        //lblTotalGrievance.Text = ds.Tables[0].Rows[0]["TotalGrievance"].ToString();
            //        //lblOtherPendingLeave.Text = ds.Tables[0].Rows[0]["OtherPendingLeave"].ToString();
            //        //lblMyPendingLeave.Text = ds.Tables[0].Rows[0]["MyPendingLeave"].ToString();
            //    }
            //    if (ds.Tables[1].Rows.Count > 0)
            //    {
            //        //RepeaterTodayMeeting.DataSource = ds.Tables[1];
            //        //RepeaterTodayMeeting.DataBind();
            //    }
            //    if (ds.Tables[2].Rows.Count > 0)
            //    {
            //        //RepeaterTomorrowMeeting.DataSource = ds.Tables[2];
            //        //RepeaterTomorrowMeeting.DataBind();
            //    }
            //    //if (ds.Tables[3].Rows.Count > 0)
            //    //{

            //    //}
            //    if (ds.Tables[4].Rows.Count > 0)
            //    {
            //        //lblSalaryMonth.Text = ds.Tables[4].Rows[0]["Salary_Month"].ToString();
            //        //lblSalaryYear.Text = ds.Tables[4].Rows[0]["Salary_Year"].ToString();
            //        //lblTotalEarnings.Text = ds.Tables[4].Rows[0]["Salary_EarningTotal"].ToString();
            //        //lblTotalDeductions.Text = ds.Tables[4].Rows[0]["Salary_DeductionTotal"].ToString();
            //        //lblNetSalary.Text = ds.Tables[4].Rows[0]["Salary_NetSalary"].ToString();
            //    }

            //    if (ds.Tables[3].Rows.Count > 0)
            //    {
            //        //GridViewBirth.DataSource = ds.Tables[3];
            //        //GridViewBirth.DataBind();
            //    }

            //}
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}