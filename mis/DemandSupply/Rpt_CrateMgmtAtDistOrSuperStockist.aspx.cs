using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Globalization;

public partial class mis_DemandSupply_Rpt_CrateMgmtAtDistOrSuperStockist : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds,ds1 = new DataSet();
    Int32 totalqty = 0;
    int sum11 = 0, sum22 = 0, sum33 = 0, sum44 = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
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
        ddlShift.SelectedIndex = 0;

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
    //protected void GetCategory()
    //{
    //    try
    //    {
    //        ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
    //                 new string[] { "flag" },
    //                   new string[] { "1" }, "dataset");

    //        if (ds.Tables[0].Rows.Count != 0)
    //        {
    //            ddlItemCategory.DataTextField = "ItemCatName";
    //            ddlItemCategory.DataValueField = "ItemCat_id";
    //            ddlItemCategory.DataSource = ds;
    //            ddlItemCategory.DataBind();
    //            ddlItemCategory.Items.Insert(0, new ListItem("All", "0"));
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds != null) { ds.Dispose(); }
    //    }
    //}
    private void GetRetailer()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Mst_DistributorParlourMapping",
                     new string[] { "flag", "DistributorId" },
                       new string[] { "6", objdb.createdBy() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlRetailer.DataTextField = "BoothName";
                ddlRetailer.DataValueField = "BoothId";
                ddlRetailer.DataSource = ds;
                ddlRetailer.DataBind();
                ddlRetailer.Items.Insert(0, new ListItem("All", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 3", ex.Message.ToString());
        }

    }

    private void GetCrateDetailsOfRetailer()
    {
        try
        {
            lblMsg.Text = string.Empty;
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds1 = objdb.ByProcedure("USP_Trn_MilkCrateDetails",
                new string[] { "flag", "FromDate", "ToDate", "DelivaryShift_id", "ItemCat_id", "BoothId", "DistributorId", "Office_ID" },
                       new string[] { "2", fromdat.ToString(), todat.ToString(), ddlShift.SelectedValue,"3", ddlRetailer.SelectedValue, objdb.createdBy(),objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                pnldata.Visible = true;
                DataTable dt = new DataTable();
                dt = ds1.Tables[0];

                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();

                GridView1.FooterRow.Cells[1].Text = "Total";
                GridView1.FooterRow.Cells[1].Font.Bold = true;

                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "Total_SuppliedCrate")
                    {

                        sum11 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView1.FooterRow.Cells[6].Text = sum11.ToString();
                        GridView1.FooterRow.Cells[6].Font.Bold = true;
                    }
                }
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString()=="Total_ReceivedCrate")
                    {

                        sum22 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView1.FooterRow.Cells[7].Text = sum22.ToString();
                        GridView1.FooterRow.Cells[7].Font.Bold = true;
                    }
                }
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "Total_BrokenCrate")
                    {

                        sum33 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView1.FooterRow.Cells[8].Text = sum33.ToString();
                        GridView1.FooterRow.Cells[8].Font.Bold = true;
                    }
                }
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "Total_MissingCrate")
                    {

                        sum44 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView1.FooterRow.Cells[9].Text = sum44.ToString();
                        GridView1.FooterRow.Cells[9].Font.Bold = true;
                    }
                }
                if(dt!=null){dt.Dispose();}
                GetDatatableHeaderDesign();
                sum11 = 0; sum22 = 0; sum33 = 0; sum44 = 0;
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
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
                GetCrateDetailsOfRetailer();
            }
            else
            {
                txtToDate.Text = string.Empty;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 5: ", ex.Message.ToString());
        }
    }
    #endregion====================================end of user defined function

    #region=============== init or changed event for controls =================

    protected void ddlShift_Init(object sender, EventArgs e)
    {
        GetShift();
    }
    //protected void ddlItemCategory_Init(object sender, EventArgs e)
    //{
    //    GetCategory();
    //}
    #endregion============ end of changed event for controls===========


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
    protected void ddlRetailer_Init(object sender, EventArgs e)
    {
        GetRetailer();
    }
}