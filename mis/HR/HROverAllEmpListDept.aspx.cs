using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;

public partial class mis_HR_HROverAllEmpListDept : System.Web.UI.Page
{
    DataSet ds, dsRecord;
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
                ds = objdb.ByProcedure("SpHrEmployeeReport",
                        new string[] { "flag" },
                        new string[] { "1" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlOffice.DataSource = ds;
                    ddlOffice.DataTextField = "Office_Name";
                    ddlOffice.DataValueField = "Office_ID";
                    ddlOffice.DataBind();
                    ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                   
                }
                //FillDetail();

                ds = objdb.ByProcedure("SpHrEmployeeReport",
                        new string[] { "flag" },
                        new string[] { "0" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlDepartment.DataSource = ds;
                    ddlDepartment.DataTextField = "Department_Name";
                    ddlDepartment.DataValueField = "Department_ID";
                    ddlDepartment.DataBind();
                    ddlDepartment.Items.Insert(0, new ListItem("All", "0"));

                }

            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }

    protected void FillDetail()
    {
        try
        {
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();


            if(ddlOffice.SelectedValue.ToString()=="0"){
                ds = objdb.ByProcedure("SpHrEmployeeReport", new string[] { "flag", "Office_ID", "Department_ID" }, new string[] { "4", ddlOffice.SelectedValue.ToString(), ddlDepartment.SelectedValue.ToString() }, "dataset");
            }
            else if(ddlOffice.SelectedValue.ToString() == "1")
            {
                ds = objdb.ByProcedure("SpHrEmployeeReport", new string[] { "flag", "Office_ID", "Department_ID" }, new string[] { "2", ddlOffice.SelectedValue.ToString(), ddlDepartment.SelectedValue.ToString() }, "dataset");
            }
            else
            {
                ds = objdb.ByProcedure("SpHrEmployeeReport", new string[] { "flag", "Office_ID", "Department_ID" }, new string[] { "3", ddlOffice.SelectedValue.ToString(), ddlDepartment.SelectedValue.ToString() }, "dataset");
            }
            


            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FillDetail();
    }
}