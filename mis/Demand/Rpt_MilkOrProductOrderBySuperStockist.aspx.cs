using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_Demand_Rpt_MilkOrProductOrderBySuperStockist : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds = new DataSet();
    string orderdate = "";
    Int32 totalqty = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetCategory();
                GetShift();
                GetRetailer();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Text = Date;
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Text = Date;
                txtToDate.Attributes.Add("readonly", "readonly");
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    #region=======================user defined function========================
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1: " + ex.Message.ToString());
        }
    }
    private void Clear()
    {
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        // ddlShift.SelectedIndex = 0;

    }
    protected void GetShift()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Mst_ShiftMaster",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataSource = ds;
                ddlShift.DataBind();
                ddlShift.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlShift.Items.Insert(0, new ListItem("No Record Found", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    protected void GetCategory()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlItemCategory.DataTextField = "ItemCatName";
                ddlItemCategory.DataValueField = "ItemCat_id";
                ddlItemCategory.DataSource = ds;
                ddlItemCategory.DataBind();
            }
            else
            {
                ddlItemCategory.Items.Insert(0, new ListItem("No Record Found", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void GetRetailer()
    {
        try
        {
            ddlRetailer.Items.Clear();
            ds = objdb.ByProcedure("USP_Mst_SuperStockistDistributorMapping",
                     new string[] { "Flag", "Office_ID", "ItemCat_id", "SuperStockistId" },
                       new string[] { "5", objdb.Office_ID(), ddlItemCategory.SelectedValue, objdb.createdBy() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlRetailer.DataTextField = "BName";
                ddlRetailer.DataValueField = "BoothId";
                ddlRetailer.DataSource = ds.Tables[0];
                ddlRetailer.DataBind();
                ddlRetailer.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlRetailer.Items.Insert(0, new ListItem("No Record Found", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }

    }
    private void GetItemDetails()
    {
        try
        {
            lblMsg.Text = string.Empty;
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandBySS",
                new string[] { "flag", "FromDate", "ToDate", "Shift_id", "ItemCat_id", "BoothId", "SuperStockistId", "Office_ID" },
                       new string[] { "2", fromdat.ToString(), todat.ToString(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, ddlRetailer.SelectedValue, objdb.createdBy(),objdb.Office_ID() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {

                pnldata.Visible = true;
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                GetDatatableHeaderDesign();
            }
            else
            {
                pnldata.Visible = true;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }

    private void GetCompareDate()
    {
        try
        {
            string myStringfromdat = txtFromDate.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtToDate.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                lblMsg.Text = string.Empty;
                GetItemDetails();
            }
            else
            {
                txtToDate.Text = string.Empty;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 6: ", ex.Message.ToString());
        }
    }
    #endregion====================================end of user defined function

    #region============ button click event & GridView Event ============================
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetCompareDate();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        pnldata.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();
        Clear();
    }

    #endregion=============end of button click funciton==================


    //protected void ddlRetailer_Init(object sender, EventArgs e)
    //{
    //    GetRetailer();
    //}
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ItemOrdered")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    lblModalMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblBoothName = (Label)row.FindControl("lblBoothName");
                    Label lblDemandDate = (Label)row.FindControl("lblDemandDate");
                    Label lblShiftName = (Label)row.FindControl("lblShiftName");
                    Label lblItemCatid = (Label)row.FindControl("lblItemCatid");

                    modalBoothName.InnerHtml = lblBoothName.Text;
                    modaldate.InnerHtml = lblDemandDate.Text;
                    modelShift.InnerHtml = lblShiftName.Text;

                    GridView2.DataSource = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                   new string[] { "flag", "MilkOrProductDemandId", "ItemCat_id", "Office_ID" },
                     new string[] { "9", e.CommandArgument.ToString(), lblItemCatid.Text, objdb.Office_ID() }, "dataset");
                    GridView2.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    GetDatatableHeaderDesign();
                }
            }
        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  7: " + ex.Message.ToString());
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
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
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 8: " + ex.Message.ToString());
        }
    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRetailer();
    }
}