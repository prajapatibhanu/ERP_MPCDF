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

public partial class mis_HRCRReport : System.Web.UI.Page
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

            ds = objdb.ByProcedure("SpHrEmpACR", new string[] { "flag", "OfficeID","FromYear","ToYear" },
                    new string[] { "4",ddlOffice.SelectedValue.ToString(),ddlFromYear.SelectedValue.ToString(),ddlToYear.SelectedValue.ToString()}, "datatset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();

                gvDetails.UseAccessibleHeader = true;
                gvDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
              
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

            //int count = gvDetails.Rows.Count;
            //foreach (GridViewRow gvrow in gvDetails.Rows)
            //{
            //    CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
            //    HiddenField HF_Emp_ID = (HiddenField)gvrow.FindControl("HF_Emp_ID");
            //    if (chk.Checked == true)
            //    {
            //        string Approval_Emp_Id = HF_Emp_ID.Value.ToString();
            //        string Approval_Updated_By = ViewState["Emp_ID"].ToString();
            //        string Approval_Auth_Id = ddlEmpList.SelectedValue.ToString();
            //        ds = objdb.ByProcedure("SpHRBalanceLeaveDetail",
            //            new string[] { "flag", "Approval_Emp_Id", "Approval_Auth_Id", "Emp_ID", "LeaveType_ID" },
            //            new string[] { "12", Approval_Emp_Id, Approval_Auth_Id, Approval_Updated_By, ddlLeaveTpye.SelectedValue.ToString() }, "dataset");
            //    }
            //    else
            //    {
            //        string Approval_Emp_Id = HF_Emp_ID.Value.ToString();
            //        string Approval_Auth_Id = ddlEmpList.SelectedValue.ToString();
            //        ds = objdb.ByProcedure("SpHRBalanceLeaveDetail",
            //            new string[] { "flag", "Approval_Emp_Id", "Approval_Auth_Id", "LeaveType_ID" },
            //            new string[] { "16", Approval_Emp_Id, Approval_Auth_Id, ddlLeaveTpye.SelectedValue.ToString() }, "dataset");
            //    }
            //}
            //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");


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



            ddlFromYear.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
           
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ds1 = ds;
                string currentYear = DateTime.Now.Year.ToString();
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
                ddlFromYear.SelectedValue = currentYear.ToString();


                ddlToYear.DataSource = ds1;
                ddlToYear.DataTextField = "Year";
                ddlToYear.DataValueField = "Year";
                ddlToYear.DataBind();
                ddlToYear.Items.Insert(0, new ListItem("2011", "2011"));
                ddlToYear.Items.Insert(1, new ListItem("2012", "2012"));
                ddlToYear.Items.Insert(2, new ListItem("2013", "2013"));
                ddlToYear.Items.Insert(3, new ListItem("2014", "2014"));
                ddlToYear.Items.Insert(4, new ListItem("2015", "2015"));
                ddlToYear.Items.Insert(5, new ListItem("2016", "2016"));
                ddlToYear.SelectedValue = currentYear.ToString();
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
    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        //int fromyear = int.Parse(ddlFromYear.SelectedValue.ToString());
        //int toyear = int.Parse(ddlToYear.SelectedValue.ToString());
        //int yearcount = toyear-fromyear;
        

        if (e.Row.RowType == DataControlRowType.DataRow)
        {



            //TextBox txtyears = new TextBox();
            //txtyears.ID = "txtYear";
            //txtyears.Text = (e.Row.DataItem as DataRowView).Row["2011"].ToString();
            //e.Row.Cells[3].Controls.Add(txtyears);


            //for (int i = 3; i <= (yearcount + 3); i++)
            //{
            //    TextBox txtyear = new TextBox();
            //    txtyear.ID = "txtYear" + ((fromyear - 3) + i);
            //    txtyear.Text = (e.Row.DataItem as DataRowView).Row[((fromyear - 3) + i)].ToString();
            //    e.Row.Cells[i].Controls.Add(txtyear);
            //}

            //DropDownList ddllist = new DropDownList();
            //ddllist.DataTextField = (e.Row.DataItem as DataRowView).Row["2020"].ToString();
            //ddllist.DataValueField = (e.Row.DataItem as DataRowView).Row["2020"].ToString();
            //ddllist.AppendDataBoundItems = true;
            //e.Row.Cells[4].Controls.Add(ddllist);

            //ddlToYear.DataTextField = (e.Row.DataItem as DataRowView).Row["2020"].ToString();
            //ddlToYear.DataValueField = (e.Row.DataItem as DataRowView).Row["2020"].ToString();
            
            //ddlToYear.Items.Insert(0, new ListItem("2011", "2011"));
            //ddlToYear.Items.Insert(1, new ListItem("2012", "2012"));
            //ddlToYear.Items.Insert(2, new ListItem("2013", "2013"));
            //ddlToYear.Items.Insert(3, new ListItem("2014", "2014"));
            //ddlToYear.Items.Insert(4, new ListItem("2015", "2015"));
            //ddlToYear.Items.Insert(5, new ListItem("2016", "2016"));
            //ddllist.AppendDataBoundItems = true;
            //e.Row.Cells[4].Controls.Add(ddllist);
        }
    }
}