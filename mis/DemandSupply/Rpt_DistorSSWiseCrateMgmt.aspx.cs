using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_DemandSupply_Rpt_DistorSSWiseCrateMgmt : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1 = new DataSet();
    IFormatProvider culture = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.GetItemCat_id()!=null)
        {
            if (!Page.IsPostBack)
            {
                GetShift();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================
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
            ddlShift.Items.Insert(0, new ListItem("All", "0"));
            if (objdb.GetItemCat_id() == objdb.GetProductCatId())
            {
                ddlShift.SelectedValue = objdb.GetShiftMorId();
                ddlShift.Enabled = false;
            }
            else
            {
                ddlShift.Enabled = true;
                ddlShift.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
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
    private void GetCrateMgmtDetails()
    {
        try
        {
             DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds1 = objdb.ByProcedure("USP_Trn_MilkCrateMgmt",
                     new string[] { "flag","FromDate","ToDate" , "ShiftId", "Office_ID" ,"DistributorId" },
                     new string[] { "4", fromdat, todat,ddlShift.SelectedValue, objdb.Office_ID(), objdb.createdBy() }, "dataset");

            if (ds1.Tables[0].Rows.Count > 0)
            {
                pnldata.Visible = true;
                btnExport.Visible = true;
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();

                int ICrate = ds1.Tables[0].AsEnumerable().Sum(row => row.Field<int>("IssueCrate"));
                int RCrate = ds1.Tables[0].AsEnumerable().Sum(row => row.Field<int>("ReturnCrate"));
                int BalanceCrate = ds1.Tables[0].AsEnumerable().Sum(row => row.Field<int>("ShortExcessCrate"));
                GridView1.FooterRow.Cells[3].Text = "<b>Total</b>";

                GridView1.FooterRow.Cells[4].Text = "<b>" + ICrate.ToString() + "</b>";
                GridView1.FooterRow.Cells[5].Text = "<b>" + RCrate.ToString() + "</b>";
                GridView1.FooterRow.Cells[6].Text = "<b>" + BalanceCrate.ToString() + "</b>";
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
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
                GetCrateMgmtDetails();
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
            Response.AddHeader("content-disposition", "attachment; filename=" + "CrateDetails" + DateTime.Now + ".xls");
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