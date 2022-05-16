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

public partial class mis_HRSetLeaveApprovalAuthAll : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
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
                    FillDetail();
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
    protected void FillDetail()
    {
        try
        {
            gvDetails.DataSource = null;
            gvDetails.DataBind();
            ds = objdb.ByProcedure("SpHRBalanceLeaveDetail", new string[] { "flag", "Office_ID" },
                    new string[] { "27", ViewState["Office_ID"].ToString() }, "datatset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
                btnSend.Enabled = true;
                myInput.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {

            int count = gvDetails.Rows.Count;
            foreach (GridViewRow gvrow in gvDetails.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                HiddenField HF_Emp_ID = (HiddenField)gvrow.FindControl("HF_Emp_ID");
                if (chk.Checked == true)
                {
                    string Approval_Emp_Id = HF_Emp_ID.Value.ToString();
                    string Approval_Updated_By = ViewState["Emp_ID"].ToString();
                    ds = objdb.ByProcedure("SpHRBalanceLeaveDetail",
                        new string[] { "flag","Approval_Auth_Id", "Emp_ID" },
                        new string[] { "24", Approval_Emp_Id, Approval_Updated_By }, "dataset");
                }
                else
                {
                    string Approval_Emp_Id = HF_Emp_ID.Value.ToString();
                    string Approval_Updated_By = ViewState["Emp_ID"].ToString();
                    ds = objdb.ByProcedure("SpHRBalanceLeaveDetail",
                        new string[] { "flag", "Approval_Auth_Id","Emp_ID" },
                        new string[] { "25", Approval_Emp_Id, Approval_Updated_By }, "dataset");
                }
            }
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");


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
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

}