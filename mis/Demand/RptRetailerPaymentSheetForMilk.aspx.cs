using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Demand_RptRetailerPaymentSheetForMilk : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds3, ds1,ds2 = new DataSet();
    IFormatProvider culture = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetRouteIDByDistributor();
                GetRetailer();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================
    private void GetRetailer()
    {
        try
        {
            ddlRetailer.Items.Clear();
            ds1 = objdb.ByProcedure("USP_Mst_BoothReg",
                    new string[] { "flag", "RouteId", "ItemCat_id" },
                      new string[] { "12", ViewState["RouteId"].ToString(), objdb.GetMilkCatId() }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                ddlRetailer.DataTextField = "BoothName";
                ddlRetailer.DataValueField = "BoothId";
                ddlRetailer.DataSource = ds1.Tables[0];
                ddlRetailer.DataBind();
                ddlRetailer.Items.Insert(0, new ListItem("Select", "0"));

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }

    }
    private void GetRouteIDByDistributor()
    {
        try
        {
            ds2 = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                     new string[] { "flag", "Office_ID", "DistributorId" },
                       new string[] { "4", objdb.Office_ID(), objdb.createdBy() }, "dataset");

            if (ds2.Tables[0].Rows.Count != 0)
            {
                ViewState["AreaId"] = ds2.Tables[0].Rows[0]["AreaId"].ToString();
                ViewState["RouteId"] = ds2.Tables[0].Rows[0]["RouteId"].ToString();
            }
            else
            {
                ViewState["AreaId"] = "0";
                ViewState["RouteId"] = "0";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error route ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
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
            ds3 = objdb.ByProcedure("USP_Trn_RetailerPaymentSheetForMilk"
                , new string[] { "flag", "FromDate", "ToDate", "Office_ID", "RouteId", "DistributorId" }
                , new string[] { "4", FromDate, ToDate, objdb.Office_ID(), ViewState["RouteId"].ToString(), objdb.createdBy() }, "dataset");
            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {
                btnExport.Visible = true;
                GridView1.DataSource = ds3.Tables[0];
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                decimal MilkAmount = ds3.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("MilkAmount"));
                decimal ChequeAmount = ds3.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("PaymentAmount"));
                decimal Balance = ds3.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Balance"));
                GridView1.FooterRow.Cells[2].Text = "<b>Total</b>";

                GridView1.FooterRow.Cells[3].Text = "<b>" + MilkAmount.ToString() + "</b>";
                GridView1.FooterRow.Cells[7].Text = "<b>" + ChequeAmount.ToString() + "</b>";
                GridView1.FooterRow.Cells[8].Text = "<b>" + Balance.ToString() + "</b>";

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
            if (ds3 != null) { ds3.Dispose(); }
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
    
    #endregion

   

    #region=========== Button Event===========================
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = string.Empty;
                GetCompareDate();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }
    #endregion===========================
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + ddlRetailer.SelectedItem.Text + "-" + txtFromDate.Text + "-" + txtToDate.Text + ".xls");
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