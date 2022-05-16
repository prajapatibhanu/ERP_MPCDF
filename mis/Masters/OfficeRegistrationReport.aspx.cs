using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Globalization;

public partial class mis_Masters_OfficeRegistrationReport : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds2;
    IFormatProvider cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {

            if (!Page.IsPostBack)
            {
                               
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                GetMilkSupplyUnit();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    private void GetMilkSupplyUnit()
    {
        try
        {

            ddlMilkSupplyUnit.Items.Clear();
            ddlMilkSupplyUnit.Items.Insert(0, new ListItem("Select", "0"));
            ddlMilkSupplyUnit.Items.Insert(1, "Plant");
            ddlMilkSupplyUnit.Items.Insert(2, "CC");

            ddlMilkSupplyUnit.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetSupplyUnit()
    {
        try
        {
            string code = "";
            string Office_Parant_ID = "";
            ddlSupplyUnit.Items.Clear();
            //if (ddlMilkSupplyUnit.SelectedItem.Text.Trim() == "All")
            //{
            //    code = string.Empty;
            //    Office_Parant_ID = string.Empty;
            //}
            if (ddlMilkSupplyUnit.SelectedItem.Text.Trim() == "Plant")
            {
                code = "2";
                Office_Parant_ID = "1";
            }
            else if (ddlMilkSupplyUnit.SelectedItem.Text.Trim() == "CC")
            {
                code = "4";
                Office_Parant_ID = objdb.Office_ID();
            }

            ds = null;
            ds = objdb.ByProcedure("SpAdminOffice",
                                       new string[] { "flag", "OfficeType_ID", "Office_Parant_ID", "Office_ID" },
                                       new string[] { "36", code, Office_Parant_ID,objdb.Office_ID() }, "TableSave");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSupplyUnit.DataTextField = "Office_Name";
                    ddlSupplyUnit.DataValueField = "Office_ID";
                    ddlSupplyUnit.DataSource = ds;
                    ddlSupplyUnit.DataBind();

                    //ddlSU.SelectedValue = objdb.Office_ID();
                }
            }
            ddlSupplyUnit.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlMilkSupplyUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GetSupplyUnit();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetOfficeDetails()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice",
                   new string[] { "flag","MilkSupplyto", "supplyUnit" },
                   new string[] { "37", ddlMilkSupplyUnit.SelectedItem.Text, ddlSupplyUnit.SelectedValue }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetOfficeDetails();
    }
}