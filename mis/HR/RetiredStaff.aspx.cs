using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_HR_RetiredStaff : System.Web.UI.Page
{
    DataSet ds;
    string flag;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillOffice();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void FillOffice()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "23" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlAdminOffice_ID.DataSource = ds;
                ddlAdminOffice_ID.DataTextField = "Office_Name";
                ddlAdminOffice_ID.DataValueField = "Office_ID";
                ddlAdminOffice_ID.DataBind();
                ddlAdminOffice_ID.Items.Insert(0, new ListItem("All", "0"));
                ddlAdminOffice_ID.SelectedValue = ViewState["Office_ID"].ToString();
                if (ViewState["Office_ID"].ToString() == "1")
                {
                    ddlAdminOffice_ID.Enabled = true;
                }
            }
            else {
            }
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Error 1", ex.Message.ToString());
        }
    }
    protected void FillList()
    {
        try
        {
           
            if(ds!=null)
            {
                ds.Clear();
            }
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "38", ddlAdminOffice_ID.SelectedValue }, "dataset");
            int rowcount = ds.Tables[0].Rows.Count;
            lblNoOfEmployee.Text = rowcount.ToString();
            if (ds != null && rowcount > 0)
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }
            else
            {
                ds.Clear();
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
                lblMsg.Text = "No Record Found.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Error 2!", ex.Message.ToString());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            FillList();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Error 2!", ex.Message.ToString());
        }
    }
}