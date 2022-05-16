using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_DemandSupply_RptDistributorPaymentSheet : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1 = new DataSet();
    IFormatProvider culture = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetLocation();
                // GetRoute();


            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================
    protected void GetLocation()
    {
        try
        {
            ddlLocation.DataTextField = "AreaName";
            ddlLocation.DataValueField = "AreaId";
            ddlLocation.DataSource = objdb.ByProcedure("USP_Mst_Area",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
    }
    private void FillGrid()
    {
        try
        {
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            lblMsg.Text = string.Empty;
            string FromDate = Convert.ToDateTime(txtFromDate.Text, culture).ToString("yyyy/MM/dd");
            string ToDate = Convert.ToDateTime(txtToDate.Text, culture).ToString("yyyy/MM/dd");
            ds = objdb.ByProcedure("USP_Trn_DistributorPaymentSheet"
                , new string[] { "flag", "FromDate", "ToDate", "Office_ID", "RouteId", "DistributorId" }
                , new string[] { "5", FromDate, ToDate, objdb.Office_ID(), ddlRoute.SelectedValue, ddlDitributor.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                btnExport.Visible = true;
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                decimal MilkAmount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                decimal ChequeAmount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalPaidAmt"));
                decimal Balance = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Balance"));
                GridView1.FooterRow.Cells[3].Text = "<b>Total</b>";

                GridView1.FooterRow.Cells[4].Text = "<b>" + MilkAmount.ToString() + "</b>";
                GridView1.FooterRow.Cells[6].Text = "<b>" + ChequeAmount.ToString() + "</b>";
                GridView1.FooterRow.Cells[7].Text = "<b>" + Balance.ToString() + "</b>";

            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                btnExport.Visible = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1:", ex.Message.ToString());
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
                FillGrid();
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
    private void GetRoute()
    {
        try
        {

            ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                 new string[] { "7", objdb.Office_ID(), ddlLocation.SelectedValue, "3" }, "dataset");
            ddlRoute.DataTextField = "RName";
            ddlRoute.DataValueField = "RouteId";
            ddlRoute.DataBind();
            ddlRoute.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1:", ex.Message.ToString());
        }
    }
    private void GetDisOrSSByRouteID()
    {
        try
        {
            ddlDitributor.DataTextField = "DTName";
            ddlDitributor.DataValueField = "DistributorId";
            ddlDitributor.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "Office_ID", "RouteId", "ItemCat_id" },
                   new string[] { "5", objdb.Office_ID(), ddlRoute.SelectedValue,objdb.GetMilkCatId() }, "dataset");
            ddlDitributor.DataBind();
            ddlDitributor.Items.Insert(0, new ListItem("Select", "0"));


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }

    private void ClearText()
    {
        ddlRoute.SelectedIndex = 0;
        ddlDitributor.SelectedIndex = 0;

    }
    #endregion

    #region=========== init or changed even===========================
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetDisOrSSByRouteID();
    }

    #endregion===========================


    #region=========== Button Event===========================
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                GetCompareDate();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }
    #endregion===========================
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRoute();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + ddlDitributor.SelectedItem.Text + "-" + txtFromDate.Text + "-" + txtToDate.Text + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            GridView1.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-Consolidated " + ex.Message.ToString());
        }
    }

}