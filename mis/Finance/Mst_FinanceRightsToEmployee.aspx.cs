using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_Mst_FinanceRightsToEmployee : System.Web.UI.Page
{
    DataSet ds;
    static DataSet dsStatic;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
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
                    ViewState["Office_FinAddress"] = Session["Office_FinAddress"].ToString();
                    FillDropdown();
                    FillEmployee();
                    FillGrid();
                    

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
    protected void FillDropdown()
    {
        try
        {
            if (ViewState["Office_ID"].ToString() == "1")
            {
                ddlOffice.Enabled = true;
            }
            ds = objdb.ByProcedure("SpFinVoucherTx",
                    new string[] { "flag" },
                    new string[] { "26" }, "dataset");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
                //ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                //ddlOffice.Enabled = false;
            }
            else
            {
                ddlOffice.Items.Clear();
                ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillEmployee()
    {
        try
        {

            ds = objdb.ByProcedure("Usp_tblFinEditRightstoEmployee",
                    new string[] { "flag", "Office_ID" },
                    new string[] { "1",ddlOffice.SelectedValue}, "dataset");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmpName.DataSource = ds;
                ddlEmpName.DataTextField = "Emp_Name";
                ddlEmpName.DataValueField = "Emp_ID";
                ddlEmpName.DataBind();
                ddlEmpName.Items.Insert(0, new ListItem("Select", "0"));
                
            }
            else
            {
                ddlEmpName.Items.Clear();
                ddlEmpName.Items.Insert(0, new ListItem("Select", "0"));
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
            ds = objdb.ByProcedure("Usp_tblFinEditRightstoEmployee", new string[] { "flag", "Office_ID", "Emp_ID", "CreatedBy", "CreatedByIP", "IsActive" }, new string[] {"0",ddlOffice.SelectedValue,ddlEmpName.SelectedValue,objdb.createdBy(),objdb.GetLocalIPAddress(),"1" }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", Success);
                        ddlEmpName.ClearSelection();
                       
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                    {
                        string warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "warning!", warning);
                    }
                    else
                    {
                        string danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", danger);
                    }
                }
                ddlEmpName.ClearSelection();
                FillGrid();
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
            GridView1.DataSource = string.Empty;
            GridView1.DataBind();
            ds = objdb.ByProcedure("Usp_tblFinEditRightstoEmployee", new string[] { "flag", "Office_ID"}, new string[] { "2", ViewState["Office_ID"].ToString()}, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
                
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void chkActive_CheckedChanged(object sender, EventArgs e)
    {
		CheckBox chk = (CheckBox)sender;
        if (chk.Checked == false)
        {
            string FinanceEditRights_ID = chk.ToolTip.ToString();
            objdb.ByProcedure("Usp_tblFinEditRightstoEmployee", new string[] { "flag", "FinanceEditRights_ID" }, new string[] { "3", FinanceEditRights_ID }, "dataset");
            FillGrid();
        }
    }
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployee();
    }
}