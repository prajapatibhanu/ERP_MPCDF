using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class mis_Payroll_SMSBirthday : System.Web.UI.Page
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
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    //btnSendSMS.Visible = false;
                    FillGrid();
                    FillGridReport();
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

    private void FillGridReport()
    {
        try
        {
            ds = objdb.ByProcedure("SpSMSReminder", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = new string[] { };
                GridView2.DataBind();
            }
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
            ds = objdb.ByProcedure("SpSMSReminder", new string[] { "flag" }, new string[] { "0" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnSendSMS_Click(object sender, EventArgs e)
    {
        try
        {
            int count = GridView1.Rows.Count;
            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                if (chk.Checked == true)
                {
                    string MobileNo = gvrow.Cells[2].Text;
                    string Emp_Detail = gvrow.Cells[1].Text + "(" + gvrow.Cells[3].Text + ")";
                    if (MobileNo != "")
                    {
                        string massege = "Wishing you a great birthday and a memorable year. From all of us (MPAGRO Department).";
                        //MobileNo = "8109604170";
                        string link = "http://mysms.mssinfotech.com/app/smsapi/index.php?key=3597C1493C124F&type=unicode&routeid=2&contacts=" + MobileNo + "&senderid=MPAGRO&msg=" + Server.UrlEncode(massege);
                        HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
                        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                        Stream stream = response.GetResponseStream();

                        ds = objdb.ByProcedure("SpSMSReminder",
                            new string[] { "flag", "EmpName", "SmsType", "SmsContent", "SmsMobile", "SmsSendBy" },
                            new string[] { "1", Emp_Detail, "Birthday", "Wishing you a great birthday and a memorable year. From all of us (MPAGRO Department).", MobileNo, ViewState["Emp_ID"].ToString() }, "dataset");


                    }
                }
            }
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}