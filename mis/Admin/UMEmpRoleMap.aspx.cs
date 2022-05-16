using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_Admin_UMEmpRoleMap : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divGrid.Visible = false;
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
    protected void ddlEmployye_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        divGrid.Visible = false;
        lblMsg.Text = "";
        if (ddlEmployye_Name.SelectedIndex > 0)
        {
            divGrid.Visible = true;
            ds = objdb.ByProcedure("SpEmployeeRoleMap",
                       new string[] { "flag", "Emp_ID", "UserTypeId" },
                       new string[] { "4", ddlEmployye_Name.SelectedValue.ToString(), ddlUserType.SelectedValue }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                btnSave.Visible = true;
            }
            else if (ds != null && ds.Tables[0].Rows.Count == 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                btnSave.Visible = false;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                btnSave.Visible = false;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            bool minoneselect = false;
            int RowNo = 0;

            ds = objdb.ByProcedure("SpEmployeeRoleMap", 
                new string[] { "flag", "Emp_ID", "UserTypeId" }, 
                new string[] { "3", ddlEmployye_Name.SelectedValue.ToString(), ddlUserType.SelectedValue }, "dataset");

            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.Cells[0].FindControl("chkSelect") as CheckBox;
                if (chk.Checked)
                {
                    minoneselect = true;
                    string RoleID = GridView1.Rows[RowNo].Cells[2].Text;
                    string EmployeeID = ddlEmployye_Name.SelectedValue.ToString();


                    objdb.ByProcedure("SpEmployeeRoleMap",
                        new string[] { "flag", "Emp_ID", "Role_ID", "UserTypeId", "EmpRoleMap_UpdatedBy" },
                        new string[] { "2", EmployeeID, RoleID, ddlUserType.SelectedValue,ViewState["Emp_ID"].ToString() }, "dataset");
                    RowNo++;
                    divGrid.Visible = true;
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else
                {
                    minoneselect = true;
                    RowNo++;
                    divGrid.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlUserType_Init(object sender, EventArgs e)
    {
        try
        {
            ds = objdb.ByProcedure("SpEmployeeRoleMap",
                        new string[] { "flag" },
                        new string[] { "7" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlUserType.DataSource = ds;
                ddlUserType.DataTextField = "UserTypeName";
                ddlUserType.DataValueField = "UserTypeId";
                ddlUserType.DataBind();
                ddlUserType.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ds.Clear();
                ddlUserType.DataSource = ds;
                ddlUserType.DataBind();
                ddlUserType.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divGrid.Visible = false;
            lblMsg.Text = "";
            if (ddlUserType.SelectedIndex > 0)
            {
                lblUserName.Text = ddlUserType.SelectedItem.Text;
                divGrid.Visible = true;
                ds = objdb.ByProcedure("SpEmployeeRoleMap",
                           new string[] { "flag", "UserTypeId" },
                           new string[] { "6", ddlUserType.SelectedValue }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlEmployye_Name.DataSource = ds;
                    ddlEmployye_Name.DataTextField = "Emp";
                    ddlEmployye_Name.DataValueField = "Emp_ID";
                    ddlEmployye_Name.DataBind();
                    ddlEmployye_Name.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ds.Clear();
                    ddlEmployye_Name.DataSource = ds;
                    ddlEmployye_Name.DataBind();
                    ddlEmployye_Name.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                lblUserName.Text = "Employee";
                ds.Clear();
                ddlEmployye_Name.DataSource = ds;
                ddlEmployye_Name.DataBind();
                ddlEmployye_Name.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}