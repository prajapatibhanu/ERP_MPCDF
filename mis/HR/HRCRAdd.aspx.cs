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

public partial class mis_HRCRAdd : System.Web.UI.Page
{
    DataSet ds,ds1;
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
                    FillDropDown();
                    btnSave.Visible = false;

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
            ds = objdb.ByProcedure("SpHrEmpACR", new string[] { "flag", "OfficeID","Year" },
                    new string[] { "5", ddlOffice.SelectedValue.ToString(), ddlFromYear.SelectedValue.ToString(), }, "datatset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();

                gvDetails.UseAccessibleHeader = true;
                gvDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                btnSave.Visible = true;

                ViewState["Office_IDD"] = ddlOffice.SelectedValue.ToString();
            }
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
            ddlFromYear.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
           
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ds1 = ds;

                ddlFromYear.DataSource = ds;
                ddlFromYear.DataTextField = "Year";
                ddlFromYear.DataValueField = "Year";
                ddlFromYear.DataBind();
                ddlFromYear.Items.Insert(0, new ListItem("2011", "2011"));
                ddlFromYear.Items.Insert(1, new ListItem("2012", "2012"));
                ddlFromYear.Items.Insert(2, new ListItem("2013", "2013"));
                ddlFromYear.Items.Insert(3, new ListItem("2014", "2014"));
                ddlFromYear.Items.Insert(4, new ListItem("2015", "2015"));
                ddlFromYear.Items.Insert(5, new ListItem("2016", "2016"));
              
            }

            ds = objdb.ByProcedure("SpAdminOffice",
                        new string[] { "flag" },
                        new string[] { "23" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                if (ViewState["Office_ID"].ToString() == "1")
                {
                    ddlOffice.Enabled = true;
                }
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillDetail();
    }
    protected void btnSave_Click_Click(object sender, EventArgs e)
    {
        try
        {

            int count = gvDetails.Rows.Count;
            int FromYear = int.Parse(ddlFromYear.SelectedValue.ToString());
            foreach (GridViewRow gvrow in gvDetails.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                HiddenField HF_Emp_ID = (HiddenField)gvrow.FindControl("HF_Emp_ID");
                HiddenField HF_Dep_ID = (HiddenField)gvrow.FindControl("HF_Dep_ID");
                HiddenField HF_Des_ID = (HiddenField)gvrow.FindControl("HF_Des_ID");

                if (chk.Checked == true)
                {
                    string Dep_Id = HF_Dep_ID.Value.ToString();
                    string Des_Id = HF_Des_ID.Value.ToString();
                    string Emp_Id = HF_Emp_ID.Value.ToString();
                    string Updated_By = ViewState["Emp_ID"].ToString();

                    DropDownList ddlGrade = (DropDownList)gvrow.FindControl("ddlGrade");
                    ds = objdb.ByProcedure("SpHrEmpACR",
                        new string[] { "flag", "OfficeID", "Emp_ID", "Department_ID", "Designation_ID","Year","Remark","Grade","Updated_By" },
                        new string[] { "0", ViewState["Office_IDD"].ToString(), Emp_Id, Dep_Id, Des_Id, FromYear.ToString(), "", ddlGrade.SelectedValue.ToString(), Updated_By }, "dataset");
                }
               // else
                //{
                    //string Approval_Emp_Id = HF_Emp_ID.Value.ToString();
                    //string Approval_Auth_Id = ddlEmpList.SelectedValue.ToString();
                    //ds = objdb.ByProcedure("SpHRBalanceLeaveDetail",
                    //    new string[] { "flag", "Approval_Emp_Id", "Approval_Auth_Id", "LeaveType_ID" },
                    //    new string[] { "16", Approval_Emp_Id, Approval_Auth_Id, ddlLeaveTpye.SelectedValue.ToString() }, "dataset");
                //}
            }
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
}