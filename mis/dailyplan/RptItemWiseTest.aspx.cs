using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_dailyplan_RptItemWiseTest : System.Web.UI.Page
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
                FillDropdown();
                txtOrderDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtOrderDate.Attributes.Add("ReadOnly", "ReadOnly");
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }
    protected void FillDropdown()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("spProductionQCProductMapping", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlProduct.DataSource = ds;
                ddlProduct.DataTextField = "Product_Name";
                ddlProduct.DataValueField = "Product_ID";
                ddlProduct.DataBind();
            }
            ddlProduct.Items.Insert(0, new ListItem("Select", "0"));
            ds = null;
            ds = objdb.ByProcedure("USP_Mst_ShiftMaster", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlShift.DataSource = ds;
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataBind();
            }
            ddlShift.Items.Insert(0, new ListItem("All", "0"));

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
            FillGrid();
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
            lblMsg.Text = "";
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            ds = null;
            ds = objdb.ByProcedure("spProductionRpt_LabTesting", new string[] { "flag", "Office_ID", "Item_ID", "TDate", "Shift_ID" }, new string[] { "3", ViewState["Office_ID"].ToString(), ddlProduct.SelectedValue.ToString(), Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd"), ddlShift.SelectedValue.ToString() }, "dataset");
            if (ds != null)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridView4.DataSource = new string[] { };
            GridView4.DataBind();
            lblProductName.Text = "Item : " + ddlProduct.SelectedItem.ToString();
            ds = null;
            if (e.CommandName == "View")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                Label lblLabTest_ID = (Label)row.Cells[0].FindControl("lblLabTest_ID");


                ds = objdb.ByProcedure("spProductionRpt_LabTesting", new string[] { "flag", "LabTest_ID" }, new string[] { "2", lblLabTest_ID.Text }, "dataset");
                if (ds != null)
                {
                    GridView4.DataSource = ds.Tables[0];
                    GridView4.DataBind();
                }
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ModalResultFun()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}