using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class mis_Finance_ItemCostingMethod : System.Web.UI.Page
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
                    lblMsg.Text = "";
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillOffice();
                    FillItem();
                    ddlOffice.Enabled = false;


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
    protected void FillOffice()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinItemCostingMethod",
                   new string[] { "flag" },
                   new string[] { "0" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();

            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillItem()
    {
        try
        {
            
                ds = objdb.ByProcedure("SpFinItemCostingMethod",
                   new string[] { "flag", "Office_ID" },
                   new string[] { "1",ddlOffice.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlItem.DataSource = ds;
                    ddlItem.DataTextField = "ItemName";
                    ddlItem.DataValueField = "Item_id";
                    ddlItem.DataBind();
                    ddlItem.Items.Insert(0, new ListItem("Select", "0"));

                }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            decimal StandardCost;
            lblMsg.Text = "";
            string msg = "";
            if(ddlItem.SelectedIndex == 0)
            {
                msg += "Select Item.\\n";
            }
            if (ddlcostingmethod.SelectedIndex == 0)
            {
                msg += "Select Costing Method.\\n";
            }
            if (ddlcostingmethod.SelectedIndex > 0)
            {
                if (ddlcostingmethod.SelectedIndex == 2)
                {
                    if(txtstandardcost.Text == "")
                    {
                        msg += "Enter Standard Cost.";
                    }
                }
                
            }
            if(msg == "")
            {
                if(txtstandardcost.Text == "")
                {
                    StandardCost = 0;
                }
                else
                {
                    StandardCost = decimal.Parse(txtstandardcost.Text);
                }
                objdb.ByProcedure("SpFinItemCostingMethod", new string[] { "flag", "Office_ID", "Item_id", "ItemCostingMethod", "ItemStandardCost" }, new string[] { "2", ddlOffice.SelectedValue.ToString(), ddlItem.SelectedValue.ToString(), ddlcostingmethod.SelectedItem.Text, StandardCost.ToString() }, "dataset");
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Operation Completed Successfully.");
                ddlItem.ClearSelection();
                ddlcostingmethod.ClearSelection();
                txtstandardcost.Text = "";
                txtstandardcost.Enabled = true;

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
    protected void ddlcostingmethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            EnableDisableStandardCost();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if(ddlItem.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpFinItemCostingMethod", new string[] { "flag", "Item_id", "Office_ID" }, new string[] {"3",ddlItem.SelectedValue.ToString(),ddlOffice.SelectedValue.ToString() }, "dataset");
                if(ds!= null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlcostingmethod.ClearSelection();
                    ddlcostingmethod.Items.FindByText(ds.Tables[0].Rows[0]["ItemCostingMethod"].ToString()).Selected = true;
                    txtstandardcost.Text = ds.Tables[0].Rows[0]["ItemStandardCost"].ToString();                   
                }
                else
                {
                    ddlcostingmethod.ClearSelection();
                    txtstandardcost.Text = "";
                }
                EnableDisableStandardCost();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void EnableDisableStandardCost()
    {
        try
        {
            lblMsg.Text = "";
             if(ddlcostingmethod.SelectedIndex > 0)
            {
                if (ddlcostingmethod.SelectedValue.ToString() == "Average Costing")
                {
                    txtstandardcost.Enabled = false;
                    txtstandardcost.Text = "";
                }
                else if (ddlcostingmethod.SelectedValue.ToString() == "Standard Costing")
                {
                    txtstandardcost.Enabled = true;
                    txtstandardcost.Text = "";
                }
                else if (ddlcostingmethod.SelectedValue.ToString() == "NRV")
                {
                    txtstandardcost.Enabled = false;
                    txtstandardcost.Text = "";
                    
                }
                else if (ddlcostingmethod.SelectedValue.ToString() == "FIFO")
                {
                    txtstandardcost.Enabled = false;
                    txtstandardcost.Text = "";

                }
            }
            else
            {
                txtstandardcost.Enabled = true;
                txtstandardcost.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}