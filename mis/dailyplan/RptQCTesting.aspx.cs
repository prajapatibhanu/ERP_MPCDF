using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_dailyplan_RptQCTesting : System.Web.UI.Page
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
                FillOffice();
                FillShift();
                FillProductSection();
                txtOrderDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtOrderDate.Attributes.Add("ReadOnly", "ReadOnly");
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }
    protected void FillShift()
    {
        try
        {
            ddlShift.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("USP_Mst_ShiftMaster", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlShift.DataSource = ds;
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataBind();
                ddlShift.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "12" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDS.DataSource = ds.Tables[0];
                ddlDS.DataTextField = "Office_Name";
                ddlDS.DataValueField = "Office_ID";
                ddlDS.DataBind();
                ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                ddlDS.SelectedValue = ViewState["Office_ID"].ToString();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillProductSection()
    {
        try
        {

            lblMsg.Text = "";
            ddlProductSection.Items.Clear();
            ds = objdb.ByProcedure("SpProductionProduct_DS_Mapping",
                 new string[] { "flag", "Office_ID" },
                 new string[] { "0", ddlDS.SelectedValue.ToString() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlProductSection.DataSource = ds.Tables[0];
                ddlProductSection.DataTextField = "ProductSection_Name";
                ddlProductSection.DataValueField = "ProductSection_ID";
                ddlProductSection.DataBind();

            }
            ddlProductSection.Items.Insert(0, new ListItem("Select", "0"));
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

    protected void ddlDS_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillProductSection();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlProductSection_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void FillGrid()
    {
        try
        {

            GridView2.DataSource = new string[] { };
            GridView2.DataBind();
            ds = null;
            ds = objdb.ByProcedure("spProductionRpt_LabTesting", new string[] { "flag", "Office_ID", "Section_ID", "TDate", "Shift_ID" }, new string[] { "1", ddlDS.SelectedValue.ToString(), ddlProductSection.SelectedValue.ToString(), Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd"), ddlShift.SelectedValue.ToString() }, "dataset");
            if (ds != null)
            {
                GridView2.DataSource = ds.Tables[0];
                GridView2.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblLabTest_ID = (Label)e.Row.FindControl("lblLabTest_ID");
            GridView GVResult = (GridView)e.Row.FindControl("GVResult");
            DataSet ds1 = null;

            ds1 = objdb.ByProcedure("spProductionRpt_LabTesting", new string[] { "flag", "LabTest_ID" }, new string[] { "2", lblLabTest_ID.Text}, "dataset");
            if (ds1 != null)
            {
                GVResult.DataSource = ds1.Tables[0];
                GVResult.DataBind();
            }
        }

    }

}