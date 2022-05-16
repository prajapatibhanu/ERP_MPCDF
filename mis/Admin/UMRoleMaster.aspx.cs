using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_Admin_UMRoleMaster : System.Web.UI.Page
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
                    ViewState["Role_ID"] = "0";
                    FillGrid();
                    lblMsg.Text = "";
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
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
            GridView1.DataSource = null;
            GridView1.DataBind();

            ds = objdb.ByProcedure("SpUMRoleMaster",
                new string[] { "flag" },
                new string[] { "1" }, "dataset");
            GridView1.DataSource = ds;
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            string Role_IsActive = "1";
            if (txtRole_Name.Text.Trim() == "")
            {
                msg += "Enter Role Name";
            }
            if (msg.Trim() == "")
            {
                ds = objdb.ByProcedure("SpUMRoleMaster",
                      new string[] { "flag", "Role_Name", "Role_ID" },
                      new string[] { "4", txtRole_Name.Text.Trim(),ViewState["Role_ID"].ToString() }, "dataset");

                    if (btnSave.Text == "Save"  && ViewState["Role_ID"].ToString() == "0" && ds.Tables[0].Rows.Count == 0)
                    {
                        objdb.ByProcedure("SpUMRoleMaster",
                        new string[] { "flag", "Role_IsActive", "Role_Name", "Role_UpdatedBy" },
                        new string[] { "0", Role_IsActive, txtRole_Name.Text.Trim(), ViewState["Emp_ID"].ToString() }, "dataset");

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    }

                   else if (btnSave.Text == "Edit"  && ViewState["Role_ID"].ToString() != "0" && ds.Tables[0].Rows.Count == 0)
                    {
                        objdb.ByProcedure("SpUMRoleMaster",
                        new string[] { "flag", "Role_ID", "Role_Name", "Role_UpdatedBy" },
                        new string[] { "5", ViewState["Role_ID"].ToString(), txtRole_Name.Text.Trim(), ViewState["Emp_ID"].ToString() }, "dataset");

                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                        ViewState["Role_ID"] = "0";
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", "This Role Is Already Exist.");
                    }

                txtRole_Name.Text = "";
                btnSave.Text = "Save";
                FillGrid();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            CheckBox chk = (CheckBox)GridView1.Rows[selRowIndex].FindControl("chkSelect");
            string Role_ID = chk.ToolTip.ToString();
            string Role_IsActive = "0";
            if (chk != null & chk.Checked)
            {
                Role_IsActive = "1";
            }
            objdb.ByProcedure("SpUMRoleMaster",
                       new string[] { "flag", "Role_IsActive", "Role_ID", "Role_UpdatedBy" },
                       new string[] { "6", Role_IsActive, Role_ID, ViewState["Emp_ID"].ToString() }, "dataset");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["Role_ID"] = GridView1.SelectedValue.ToString();
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpUMRoleMaster",
                       new string[] { "flag", "Role_ID" },
                       new string[] { "3", ViewState["Role_ID"].ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtRole_Name.Text = ds.Tables[0].Rows[0]["Role_Name"].ToString();
                btnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}