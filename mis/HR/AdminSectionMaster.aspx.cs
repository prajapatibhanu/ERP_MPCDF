using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_HR_AdminSectionMaster : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["Section_ID"] = "0";
                FillDropDown();
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }

    protected void FillDropDown()
    {
        try
        {
              ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "23" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataBind();
                ddlOfficeName.SelectedValue = ViewState["Office_ID"].ToString();
                ddlOfficeName.Enabled = false;
                if (ViewState["Office_ID"].ToString() == "1")
                {
                    ddlOfficeName.Enabled = true;
                }
            }
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
            if (txtSection_Name.Text == "")
            {
                msg += "Enter Section Name. \\n";
            }
            if(txtOrderNo.Text =="")
            {
                msg += "Enter Section Order. \\n";
            }
            if (txtSection_No.Text == "")
            {
                msg += "Enter Section No. \\n";
            }
            
            //if(ddlOfficeName.SelectedIndex ==0)
            //{
            //    msg += "Select Office. \\n";
            //}
            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("SpHRSectionMaster", new string[] { "flag", "Section_Name", "Office_ID", "Section_ID", "Section_No" }, new string[] { "5", txtSection_Name.Text, ddlOfficeName.SelectedValue.ToString(), ViewState["Section_ID"].ToString(), txtSection_No.Text }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }

                if (btnSave.Text == "Save" && ViewState["Section_ID"].ToString() == "0" && Status == 0)
                {
                    objdb.ByProcedure("SpHRSectionMaster",
                    new string[] { "flag", "Section_Name", "Section_No", "Office_ID", "OrderNo", "Section_UpdatedBy" },
                    new string[] { "0", txtSection_Name.Text, txtSection_No.Text, ddlOfficeName.SelectedValue.ToString(), txtOrderNo.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();

                }
                else if (btnSave.Text == "Edit" && ViewState["Section_ID"].ToString() != "0" && Status == 0)
                {
                    objdb.ByProcedure("SpHRSectionMaster",
                    new string[] { "flag", "Section_ID", "Section_Name", "Section_No", "Office_ID", "OrderNo", "Section_UpdatedBy" },
                    new string[] { "2", ViewState["Section_ID"].ToString(), txtSection_Name.Text, txtSection_No.Text, ddlOfficeName.SelectedValue.ToString(), txtOrderNo.Text, ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Section name is already exist.');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
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
            if (ViewState["Office_ID"].ToString() == "1")
            {
                ds = objdb.ByProcedure("SpHRSectionMaster", new string[] { "flag" }, new string[] { "6" }, "dataset");
            }
            else
            {
                ds = objdb.ByProcedure("SpHRSectionMaster", new string[] { "flag", "Office_ID" }, new string[] { "1", ddlOfficeName.SelectedValue.ToString() }, "dataset");
            }
            

           if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
           else
           {
               GridView1.DataSource = null;
               GridView1.DataBind();
           }
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
            lblMsg.Text = "";
            ClearText();
            ViewState["Section_ID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpHRSectionMaster", new string[] { "flag", "Section_ID" }, new string[] { "4", ViewState["Section_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtSection_Name.Text = ds.Tables[0].Rows[0]["Section_Name"].ToString();
                txtSection_No.Text = ds.Tables[0].Rows[0]["Section_No"].ToString();
                txtOrderNo.Text = ds.Tables[0].Rows[0]["OrderNo"].ToString();
                ddlOfficeName.Items.FindByValue(ds.Tables[0].Rows[0]["Office_ID"].ToString()).Selected = true;
                btnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {

        try
        {
            string Section_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string IsActive = "0";
            objdb.ByProcedure("SpHRSectionMaster",
                   new string[] { "flag", "Section_ID", "IsActive", "Section_UpdatedBy" },
                   new string[] { "3", Section_ID, IsActive, ViewState["Emp_ID"].ToString() }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();
            lblMsg.Text = "";
            ClearText();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ClearText();
            GridView1.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        txtSection_Name.Text = "";
        txtSection_No.Text = "";
        txtOrderNo.Text = "";
        ddlOfficeName.ClearSelection();
        ViewState["Section_ID"] = "0";
        btnSave.Text = "Save";
    }
}