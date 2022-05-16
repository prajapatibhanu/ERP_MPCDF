using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_HR_HRPostMaster : System.Web.UI.Page
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
                FillDropDown();
                btnSave.Visible = false;
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
            ddlClass.Items.Insert(0, new ListItem("Select", "0"));

            ds = objdb.ByProcedure("SpHRClass", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                ddlClass.DataSource = ds;
                ddlClass.DataTextField = "Class_Name";
                ddlClass.DataValueField = "Class_Name";
                ddlClass.DataBind();
                ddlClass.Items.Insert(0, new ListItem("Select", "0"));
            }

            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "23" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataBind();
                ddlOfficeName.Items.Insert(0, new ListItem("All", "0"));
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
            if (ddlClass.SelectedIndex == 0)
            {
                msg = msg + "Select Class. \n";
            }
           
            if (msg.Trim() == "")
            {
                int TotalRow = GridView1.Rows.Count - 1;
                for (int rowIndex = 0; TotalRow >= rowIndex; rowIndex++)
                {

                    string Designation_ID = GridView1.DataKeys[rowIndex]["Designation_ID"].ToString();
                    TextBox txtSanctionPost = GridView1.Rows[rowIndex].Cells[2].Controls[1] as TextBox;
                    if (txtSanctionPost.Text == "")
                    {
                        txtSanctionPost.Text = "0";
                    }
                    objdb.ByProcedure("SpHRPostMaster", new string[] { "flag", "Class", "Designation_ID", "SanctionPost", "Post_UpdatedBy","Office_Id" }, new string[] { "0", ddlClass.SelectedValue.ToString(), Designation_ID, txtSanctionPost.Text, ViewState["Emp_ID"].ToString(),ddlOfficeName.SelectedValue.ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                FillGrid();

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
            btnSave.Visible = false;
            lblheadingFirst.Text = "Office Name : " + ddlOfficeName.SelectedItem.ToString() +",  Class : " + ddlClass.SelectedItem.ToString();
            ds = objdb.ByProcedure("SpHRPostMaster", new string[] { "flag", "Class","Office_ID" }, new string[] { "1", ddlClass.SelectedValue.ToString(),ddlOfficeName.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                if (ddlOfficeName.SelectedValue.ToString()!="0")
                {
                    btnSave.Visible = true;
                }
                
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
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView2.DataSource = null;
            GridView2.DataBind();
            lblMsg.Text = "";
            string Designation_ID = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("SpHRPostMaster", new string[] { "flag", "Class", "Designation_ID", "Office_ID" }, new string[] { "2", ddlClass.SelectedValue.ToString(), Designation_ID,ddlOfficeName.SelectedValue.ToString()}, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();
                GridView2.UseAccessibleHeader = true;
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;          
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModal()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridView1.DataSource = null;
            GridView1.DataBind();

            btnSave.Visible = false;
            if (ddlClass.SelectedIndex > 0)
            {
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}