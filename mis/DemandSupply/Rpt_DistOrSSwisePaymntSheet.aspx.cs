using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_DemandSupply_Rpt_DistOrSSwisePaymntSheet : System.Web.UI.Page
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
               
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================

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
                , new string[] { "flag", "FromDate", "ToDate", "Office_ID", "DistributorId" }
                , new string[] { "8", FromDate, ToDate, objdb.Office_ID(), objdb.createdBy() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                pnldata.Visible = true;
                btnExport.Visible = true;
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                decimal MilkAmount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                decimal ChequeAmount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ChequeAmount"));
                decimal Balance = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Balance"));
                GridView1.FooterRow.Cells[2].Text = "<b>Total</b>";

                GridView1.FooterRow.Cells[3].Text = "<b>" + MilkAmount.ToString() + "</b>";
                GridView1.FooterRow.Cells[7].Text = "<b>" + ChequeAmount.ToString() + "</b>";
                GridView1.FooterRow.Cells[8].Text = "<b>" + Balance.ToString() + "</b>";

            }
            else
            {
                pnldata.Visible = true;
                btnExport.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1:", ex.Message.ToString());
        }
    }
    
    private void ClearText()
    {
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        GridView1.DataSource = null;
        GridView1.DataBind();
        pnldata.Visible = false;

    }
    #endregion


    #region=========== Button Event===========================
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
           if(Page.IsValid)
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearText();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "PaymentSheet" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

          
            GridView1.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
}