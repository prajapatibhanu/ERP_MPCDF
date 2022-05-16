using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Globalization;

public partial class mis_Demand_MilkOrProductOrderList_IUSDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2, ds33, ds5, dsadd = new DataSet();

    string orderdate = "", currentdate = "", currrentime = "";
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && ddlItemCategory.SelectedValue != null)
        {
            if (!Page.IsPostBack)
            {
                GetShift();
                GetCategory();
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtOrderDate.Text = Date;
                txtOrderDate.Attributes.Add("readonly", "readonly");
                GetParty();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    private void GetParty()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                new string[] { "Flag", "Office_ID" },
                  new string[] { "9", objdb.Office_ID() }, "dataset");
            ddlPartyName.Items.Clear();
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddlPartyName.DataTextField = "SSName";
                ddlPartyName.DataValueField = "SuperStockistId";
                ddlPartyName.DataSource = ds1.Tables[0];
                ddlPartyName.DataBind();
                ddlPartyName.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlPartyName.Items.Insert(0, new ListItem("No Record Found.", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    protected void GetCategory()
    {
        try
        {

            ddlItemCategory.DataTextField = "ItemCatName";
            ddlItemCategory.DataValueField = "ItemCat_id";
            ddlItemCategory.DataSource = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlItemCategory.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void GetShift()
    {
        try
        {
            ddlShift.DataTextField = "ShiftName";
            ddlShift.DataValueField = "Shift_id";
            ddlShift.DataSource = objdb.ByProcedure("USP_Mst_ShiftMaster",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlShift.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error S ", ex.Message.ToString());
        }
    }
    protected void GetOrderDetails()
    {
        try
        {
            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            orderdate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandBySS",
                     new string[] { "flag", "Office_ID", "SuperStockistId", "Demand_Date", "Shift_id", "ItemCat_id", "Demand_Status" },
                       new string[] { "8", objdb.Office_ID(), ddlPartyName.SelectedValue, orderdate, ddlShift.SelectedValue, ddlItemCategory.SelectedValue, ddlStatus.SelectedValue }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                lblMsg.Text = string.Empty;
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                pnlsearch.Visible = true;
                GetDatatableHeaderDesign();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                pnlsearch.Visible = true;
                GetDatatableHeaderDesign();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Data ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetOrderDetails();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        txtOrderDate.Text = string.Empty;

        ddlShift.SelectedIndex = 0;
        pnlsearch.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ItemOrdered")
        {
            Control ctrl = e.CommandSource as Control;
            if (ctrl != null)
            {
                lblMsg.Text = string.Empty;
                lblModalMsg.Text = string.Empty;

                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                Label lblDemandDate = (Label)row.FindControl("lblDemandDate");
                Label lblBandOName = (Label)row.FindControl("lblBandOName");
                Label lblShiftName = (Label)row.FindControl("lblShiftName");
                Label lblItemCatid = (Label)row.FindControl("lblItemCatid");
                Label lblDemandStatus = (Label)row.FindControl("lblDemandStatus");
                ViewState["rowid"] = e.CommandArgument.ToString();
                ViewState["rowitemcatid"] = lblItemCatid.Text;
                GetItemDetailByDemandID();

                modalBoothName.InnerHtml = lblBandOName.Text;
                modaldate.InnerHtml = lblDemandDate.Text;
                modalshift.InnerHtml = lblShiftName.Text;
                modalorderStatus.InnerHtml = lblDemandStatus.Text;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                GetDatatableHeaderDesign();

            }
        }
    }
    private void GetItemDetailByDemandID()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                   new string[] { "flag", "MilkOrProductDemandId", "ItemCat_id", "Office_ID" },
                     new string[] { "9", ViewState["rowid"].ToString(), ViewState["rowitemcatid"].ToString(), objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count > 0 && ds1 != null)
            {
                GridView4.DataSource = ds1.Tables[0];
                GridView4.DataBind();
            }
            else
            {
                GridView4.DataSource = null;
                GridView4.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }

    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblItemCatName = e.Row.FindControl("lblItemCatName") as Label;

                if (lblItemCatName.Text == "Milk")
                {
                    e.Row.CssClass = "columnmilk";
                }
                else
                {
                    e.Row.CssClass = "columnproduct";
                }

            }
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 20 : " + ex.Message.ToString());
        }
    }
}