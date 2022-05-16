using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_MilkCollection_LocalMilkSale : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.SessionID != null)
            {
                if (!IsPostBack)
                {
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDate.Attributes.Add("readonly", "readonly");
                    txtRemainingQty.Attributes.Add("readonly", "readonly");
                    txtRate.Attributes.Add("readonly", "readonly");
                    txtMilkQty.Enabled = false;
                }
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
            ds = objdb.ByProcedure("Sp_MstLocalSale",
                                                     new string[] { "flag", "Dt_Date", "V_Shift" },
                                                     new string[] { "3", Convert.ToDateTime(txtDate.Text, cult).ToString("dd/MM/yyyy"), ddlShift.SelectedValue.ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvRemainingMilkDetail.DataSource = ds;
                gvRemainingMilkDetail.DataBind();
            }
            else
            {
                gvRemainingMilkDetail.DataSource = ds;
                gvRemainingMilkDetail.DataBind();
                txtRemainingQty.Text = "";
                txtMilkQty.Enabled = false;
                ddlShift.ClearSelection();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlShift.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("Sp_MstLocalSale",
                                                         new string[] { "flag", "Dt_Date", "V_Shift" },
                                                         new string[] { "1", Convert.ToDateTime(txtDate.Text, cult).ToString("dd/MM/yyyy"), ddlShift.SelectedValue.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtRemainingQty.Text = ds.Tables[0].Rows[0]["TotalMilk"].ToString();
                    txtMilkQty.Enabled = true;
                }
                else
                {
                    txtRemainingQty.Text = "";
                    txtMilkQty.Enabled = false;
                    ddlShift.ClearSelection();
                }
                FillGrid();
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
            decimal MilkQty = 0;
            foreach (GridViewRow row in gvRemainingMilkDetail.Rows)
            {
                Label qty = (Label)row.FindControl("lblgvI_MilkSupplyQty");
                MilkQty = Convert.ToDecimal(qty.Text);
            }
            objdb.ByProcedure("Sp_MstLocalSale", new string[] { "flag", "Office_ID", "DT_Date", "V_Shift", "D_TotalMilk", "D_SaleMilkQty", "D_Rate", "CreatedBy", "CreatedIP" },
                new string[] { "2", ViewState["Office_ID"].ToString(), txtDate.Text, ddlShift.SelectedValue.ToString(), MilkQty.ToString(), txtMilkQty.Text, txtRate.Text, ViewState["Emp_ID"].ToString(), objdb.GetLocalIPAddress() }, "dataset");
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Record Saved Successfully ");
            ddlShift.ClearSelection();
            txtMilkQty.Text = "";
            txtRemainingQty.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ddlShift.ClearSelection();
            gvRemainingMilkDetail.DataSource = null;
            gvRemainingMilkDetail.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}