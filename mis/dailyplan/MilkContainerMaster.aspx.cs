using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_dailyplan_MilkContainerMaster : System.Web.UI.Page
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
                FillUnit();
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }
    protected void FillUnit()
    {
        try
        {
            ds = objdb.ByProcedure("spProductionMilkContainerMaster", new string[] { "flag" }, new string[] { "13" }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlUnit_id.DataSource = ds;
                        ddlUnit_id.DataTextField = "UnitName";
                        ddlUnit_id.DataValueField = "Unit_id";
                        ddlUnit_id.DataBind();
                        ddlUnit_id.Items.Insert(0, new ListItem("Select", "0"));
                    }
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
            string B_MCStatus = "1";
            if (ddlMCType.SelectedIndex == 0)
            {
                msg += "Select Milk Container  Type. \\n";
            }
            if (txtMCName.Text == "")
            {
                msg += "Enter Milk Container Name. \\n";
            }
            if (txtMCCapacity.Text == "")
            {
                msg += "Enter Milk Container Capacity. \\n";
            }
            if (ddlUnit_id.SelectedIndex == 0)
            {
                msg += "Select Unit. \\n";
            }
            if (msg.Trim() == "")
            {

                if (btnSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("spProductionMilkContainerMaster",
                    new string[] { "flag", "V_MCType", "V_MCName", "V_MCCapacity", "Unit_id", "B_MCStatus", "Office_Id", "Created_By" },
                    new string[] { "12", ddlMCType.SelectedValue.ToString(), txtMCName.Text, txtMCCapacity.Text, ddlUnit_id.SelectedValue.ToString(),B_MCStatus, Session["Office_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        FillGrid();
                        ClearText();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Error :" + error);
                    }


                }
                else if (btnSave.Text == "Edit")
                {
                    ds = objdb.ByProcedure("spProductionMilkContainerMaster",
                    new string[] { "flag", "I_MCID", "V_MCType", "V_MCName", "V_MCCapacity", "Unit_id", "Office_Id" },
                    new string[] { "17", ViewState["I_MCID"].ToString(), ddlMCType.SelectedValue.ToString(), txtMCName.Text, txtMCCapacity.Text, ddlUnit_id.SelectedValue.ToString(), Session["Office_ID"].ToString()}, "dataset");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        FillGrid();
                        ClearText();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Error :" + error);
                    }

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Milk Container Name already exist.');", true);
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
            GridView1.DataSource = null;
            GridView1.DataBind();
            ds = objdb.ByProcedure("spProductionMilkContainerMaster", new string[] { "flag", "Office_Id" }, new string[] { "14", Session["Office_Id"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
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
            ViewState["I_MCID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("spProductionMilkContainerMaster", new string[] { "flag", "I_MCID" }, new string[] { "15", ViewState["I_MCID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlUnit_id.Items.FindByValue(ds.Tables[0].Rows[0]["Unit_id"].ToString()).Selected = true;
                ddlMCType.Items.FindByValue(ds.Tables[0].Rows[0]["V_MCType"].ToString()).Selected = true;
                txtMCName.Text = ds.Tables[0].Rows[0]["V_MCName"].ToString();
                txtMCCapacity.Text = ds.Tables[0].Rows[0]["V_MCCapacity"].ToString();
                btnSave.Text = "Edit";
            }
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
        ddlUnit_id.ClearSelection();
        ddlMCType.ClearSelection();
        txtMCCapacity.Text = "";
        txtMCName.Text = "";        
        btnSave.Text = "Save";
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        CheckBox chk = (CheckBox)GridView1.Rows[selRowIndex].FindControl("chkSelect");
        string I_MCID = chk.ToolTip.ToString();
        string B_MCStatus = "0";
        if (chk != null & chk.Checked)
        {
            B_MCStatus = "1";
        }
        objdb.ByProcedure("spProductionMilkContainerMaster",
                    new string[] { "flag", "I_MCID", "B_MCStatus" },
                    new string[] { "16", I_MCID, B_MCStatus }, "dataset");

        

    }
}