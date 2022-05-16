using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_dailyplan_ProductTypeMapping : System.Web.UI.Page
{
    DataSet ds;
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
                    FillOffice();
                    GetSectionView();
                    //FillUnit();
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
    protected void FillGrid()
    {
        try
        {
            ds = objdb.ByProcedure("SpLooseProductionProductEntry", new string[] { "flag", "ProductSection_ID", "Office_ID" }, new string[] { "4", ddlPSection.SelectedValue, ddlDS.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
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
    protected void chkStatus_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string Productsheetstatus = "0";
            int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            CheckBox chk = (CheckBox)GridView1.Rows[selRowIndex].FindControl("chkStatus");
            string ItemType_id = chk.ToolTip.ToString();
            if (chk.Checked)
            {
                Productsheetstatus = "1";
                objdb.ByProcedure("SpItemType", new string[] { "flag", "ItemType_id", "Productsheetstatus", "UpdatedBy", "UpdatedBy_IP" }, new string[] { "13", ItemType_id, Productsheetstatus, objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");
                FillGrid();
            }
            else
            {
                Productsheetstatus = "0";
                objdb.ByProcedure("SpItemType", new string[] { "flag", "ItemType_id", "Productsheetstatus", "UpdatedBy", "UpdatedBy_IP" }, new string[] { "13", ItemType_id, Productsheetstatus, objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");
                FillGrid();
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
                ddlDS.Enabled = false;


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

    private void GetSectionView()
    {

        try
        {

            ds = null;

            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID" },
                  new string[] { "6", ddlDS.SelectedValue }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPSection.DataSource = ds.Tables[0];
                ddlPSection.DataTextField = "ProductSection_Name";
                ddlPSection.DataValueField = "ProductSection_ID";
                ddlPSection.DataBind();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));

            }
            else
            {
                ddlPSection.Items.Clear();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }
    protected void ddlPSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
}