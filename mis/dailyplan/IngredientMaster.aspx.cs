using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_dailyplan_IngredientMaster : System.Web.UI.Page
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
                ds = objdb.ByProcedure("spProductionIngredientMaster", new string[] { "flag" }, new string[] { "7" }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlUnitId.DataSource = ds;
                    ddlUnitId.DataTextField = "UnitName";
                    ddlUnitId.DataValueField = "Unit_id";
                    ddlUnitId.DataBind();
                    ddlUnitId.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlUnitId.Items.Insert(0, new ListItem("Select", "0"));
                }

                ViewState["Item_id"] = "0";
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (txtIngredientName.Text == "")
            {
                msg += "Enter Ingredient Name. \\n";
            }
            if (ddlUnitId.SelectedIndex == 0)
            {
                msg += "Select Unit. \\n";
            }
            if (msg.Trim() == "")
            {
                int Status = 0;
                ds = objdb.ByProcedure("spProductionIngredientMaster", new string[] { "flag", "Item_id", "Item_Name", "Unit_id", }, new string[] { "5", ViewState["Item_id"].ToString(), txtIngredientName.Text, ddlUnitId.SelectedValue.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (btnSave.Text == "Save" && ViewState["Item_id"].ToString() == "0" && Status == 0)
                {
                    objdb.ByProcedure("spProductionIngredientMaster",
                    new string[] { "flag", "Item_Name", "Item_Name_Hindi", "Unit_id", "UpdatedBy" },
                    new string[] { "0", txtIngredientName.Text, txtIngredientNameHindi.Text, ddlUnitId.SelectedValue.ToString(), ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();

                }
                else if (btnSave.Text == "Edit" && ViewState["Item_id"].ToString() != "0" && Status == 0)
                {
                    objdb.ByProcedure("spProductionIngredientMaster",
                    new string[] { "flag", "Item_id", "Item_Name", "Item_Name_Hindi", "Unit_id", "UpdatedBy" },
                    new string[] { "2", ViewState["Item_id"].ToString(), txtIngredientName.Text, txtIngredientNameHindi.Text, ddlUnitId.SelectedValue.ToString(), ViewState["Emp_ID"].ToString() }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                    FillGrid();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Grade Pay already exist.');", true);
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
            ds = objdb.ByProcedure("spProductionIngredientMaster", new string[] { "flag" }, new string[] { "1" }, "dataset");
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
            ViewState["Item_id"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("spProductionIngredientMaster", new string[] { "flag", "Item_id" }, new string[] { "4", ViewState["Item_id"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlUnitId.Items.FindByValue(ds.Tables[0].Rows[0]["Unit_id"].ToString()).Selected = true;
                txtIngredientName.Text = ds.Tables[0].Rows[0]["Item_Name"].ToString();
                txtIngredientNameHindi.Text = ds.Tables[0].Rows[0]["Item_Name_Hindi"].ToString();
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
            lblMsg.Text = "";
            ClearText();
            string GradePay_ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("spProductionIngredientMaster",
                   new string[] { "flag", "Item_id", "UpdatedBy" },
                   new string[] { "3", GradePay_ID, ViewState["Emp_ID"].ToString() }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();

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
        ddlUnitId.ClearSelection();
        txtIngredientName.Text = "";
        txtIngredientNameHindi.Text = "";
        ViewState["Item_id"] = "0";
        btnSave.Text = "Save";
    }
}