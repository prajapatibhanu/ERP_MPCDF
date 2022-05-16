using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using System.IO;

public partial class mis_DemandSupply_CMApp_CitizenTrn_Rpt : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1 = new DataSet();
   
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetRetailer();
                GetCitizen();
                GetOrderStatus();
                txtFromDate.Attributes.Add("readonly", "true");
                txtToDate.Attributes.Add("readonly", "true");
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }

    protected void GetRetailer()
    {
        try
        {
            ddlRetailer.DataTextField = "BoothName";
            ddlRetailer.DataValueField = "BoothId";
            ddlRetailer.DataSource = objdb.ByProcedure("USP_CMApp_CitizenOrderRpt",
                 new string[] { "Flag", "Office_ID" },
                   new string[] { "1",objdb.Office_ID() }, "dataset");
            ddlRetailer.DataBind();
            ddlRetailer.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Retailer", ex.Message.ToString());
        }
    }

    protected void GetCitizen()
    {
        try
        {
            ddlCitizen.DataTextField = "CName";
            ddlCitizen.DataValueField = "CitizenId";
            ddlCitizen.DataSource = objdb.ByProcedure("USP_CMApp_CitizenOrderRpt",
                 new string[] { "Flag" },
                   new string[] { "2" }, "dataset");
            ddlCitizen.DataBind();
            ddlCitizen.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Citizen", ex.Message.ToString());
        }
    }
    protected void GetOrderStatus()
    {
        try
        {
            ddlOrderStatus.DataTextField = "OrderStatus";
            ddlOrderStatus.DataValueField = "OrderStatusId";
            ddlOrderStatus.DataSource = objdb.ByProcedure("USP_CMApp_Mst_OrderStatus",
                 new string[] { "Flag" },
                   new string[] { "1" }, "dataset");
            ddlOrderStatus.DataBind();
            ddlOrderStatus.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Order Status", ex.Message.ToString());
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
                GetCitizenOrderReport();
            }
            else
            {
                txtToDate.Text = string.Empty;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1: ", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            lblMsg.Text = string.Empty;
            GetCompareDate();
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
    private void GetCitizenOrderReport()
    {
        try
        {
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds1 = objdb.ByProcedure("USP_CMApp_CitizenOrderRpt",
                     new string[] { "Flag", "FromDate", "ToDate", "BoothId", "CitizenId", "OrderStatusId", "Office_ID" },
                       new string[] { "3", fromdat, todat, ddlRetailer.SelectedValue,ddlCitizen.SelectedValue
                             ,ddlOrderStatus.SelectedValue, objdb.Office_ID() }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0 && ds1 != null)
            {
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();
                GetDatatableHeaderDesign();
                pnlsearch.Visible = true;
            }
            else
            {
                pnlsearch.Visible = false;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! ", "Record Not Found.");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2: ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text=string.Empty;
        ddlCitizen.SelectedIndex = 0;
        ddlOrderStatus.SelectedIndex = 0;
        ddlRetailer.SelectedIndex = 0;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        pnlsearch.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();

    }
}