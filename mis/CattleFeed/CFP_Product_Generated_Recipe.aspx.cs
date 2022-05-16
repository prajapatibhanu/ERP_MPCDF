using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_CattleFeed_CFP_Product_Generated_Recipe : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objapi = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        fillProdUnit(); lblMsg.Text = string.Empty;
    }
    private void fillProdUnit()
    {
        try
        {
            ddlProdUnit.Items.Clear();
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFPOffice_By_DS", new string[] { "flag", "DSID" }, new string[] { "0", objapi.Office_ID() }, "dataset");
            ddlProdUnit.Items.Insert(0, new ListItem("-- Select --", "0"));
            ddlProdUnit.DataSource = ds;
            ddlProdUnit.DataValueField = "CFPOfficeID";
            ddlProdUnit.DataTextField = "CFPName";
            ddlProdUnit.DataBind();
            ddlProdUnit.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    private void fillgrd()
    {
        ds = new DataSet();
        ds = objapi.ByProcedure("SP_CFP_Product_Recipe_BY_CFPID_List", new string[] { "flag", "CFPID", "OfficeID" }, new string[] { "0", ddlProdUnit.SelectedValue,objapi.Office_ID() }, "dataset");
        gvProductItems.DataSource = ds;
        gvProductItems.DataBind();
    }
    private void print(DataTable dt)
    {
        string attachment = "attachment; filename=Recipe.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";
        string tab = "";
        foreach (DataColumn dc in dt.Columns)
        {
            Response.Write(tab + dc.ColumnName);
            tab = "\t";
        }
        Response.Write("\n");
        int i;
        foreach (DataRow dr in dt.Rows)
        {
            tab = "";
            for (i = 0; i < dt.Columns.Count; i++)
            {
                Response.Write(tab + dr[i].ToString());
                tab = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }
    protected void gvProductItems_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        

        string command = Convert.ToString(e.CommandArgument);
        string[] commandArg = command.Split('|');
        hdnvalue.Value = Convert.ToString(commandArg[0]);
        string BannerName = Convert.ToString(commandArg[1]);
        string Quantity = Convert.ToString(commandArg[2]);
        Session["Table"] = null;
        switch (e.CommandName)
        {
            case "Detail":
                DataSet ds1 = new DataSet();
                lblpro.Text = BannerName + " " + Quantity+" MT.";
                ds1 = objapi.ByProcedure("SP_CFP_Product_Item_Recipe_BY_CFPProductRecipeID_List", new string[] { "flag", "CFPProductRecipeID" }, new string[] { "0", Convert.ToString(hdnvalue.Value) }, "dataset");
                grd.DataSource = ds1;
                grd.DataBind();
                Session["Table"] = ds1.Tables[0];
                GC.SuppressFinalize(ds1);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupAddDates();", true);
                break;
            default:
                break;
        }
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        fillgrd();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["Table"];
        DataColumn col = dt.Columns[0];
        dt.Columns.Remove(col);
        print(dt);

    }
}