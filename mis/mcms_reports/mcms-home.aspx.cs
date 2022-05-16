using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;


public partial class mis_MilkCollection_mcms_home : System.Web.UI.Page
{
    APIProcedure apiprocedure = new APIProcedure();
    IFormatProvider cult = new CultureInfo("en-IN", true);
    DataSet ds, ds2;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtFDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtTDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            FillGrid();
        }
    }

    private void FillGrid()
    {
        try
        {
            gvDispatchEntry.DataSource = null;
            gvDispatchEntry.DataBind();

            gvReceivedEntry.DataSource = null;
            gvReceivedEntry.DataBind();
			
			btnExportDispatch.Visible = false;
            btnExportReceived.Visible = false;

            string FromDate = "", ToDate = "";
            if (txtFromDate.Text != "")
            {
                FromDate = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            }
            if (txtToDate.Text != "")
            {
                ToDate = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            }

            if (apiprocedure.OfficeType_ID() == "2") // 1 Is DS
            {
				
                btnExportDispatch.Visible = false;
                btnExportReceived.Visible = true;
                gvReceivedEntry.Visible = true;
                gvDispatchEntry.Visible = false;
                row_pendingtankerdetails.Visible = true;

                ds = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                                new string[] { "flag", "Office_ID ", "V_EntryType", "FromDate", "ToDate" },
                                new string[] { "8", apiprocedure.Office_ID(), "In", FromDate, ToDate }, "dataset");

                gvReceivedEntry.DataSource = ds;
                gvReceivedEntry.DataBind();
				
				decimal TotalQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("I_MilkQuantity"));


                gvReceivedEntry.FooterRow.Cells[6].Text = "<b>Total : </b>";
                gvReceivedEntry.FooterRow.Cells[7].Text = "<b>" + TotalQuantity.ToString() + "</b>";
                //gvReceivedEntry.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;

            }
            else if (apiprocedure.OfficeType_ID() == "4") // 2 Is CC
            {
                btnExportDispatch.Visible = true;
                btnExportReceived.Visible = false;
                gvReceivedEntry.Visible = false;
                gvDispatchEntry.Visible = true;
                row_pendingtankerdetails.Visible = false;

                ds = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                                new string[] { "flag", "Office_ID ", "V_EntryType", "FromDate", "ToDate" },
                                new string[] { "8", apiprocedure.Office_ID(), "Out", FromDate, ToDate }, "dataset");
                gvDispatchEntry.DataSource = ds;
                gvDispatchEntry.DataBind();
                decimal TotalQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("I_MilkQuantity"));


                gvDispatchEntry.FooterRow.Cells[6].Text = "<b>Total : </b>";
                gvDispatchEntry.FooterRow.Cells[7].Text = "<b>" + TotalQuantity.ToString() + "</b>";
               // gvReceivedEntry.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
	public override void VerifyRenderingInServerForm(Control control)
    {
        try
        {


        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void gvDispatchEntry_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewEntry")
        {
            ds = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                            new string[] { "flag", "V_ReferenceCode" },
                            new string[] { "14", e.CommandArgument.ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
            {
                lblVehicleNo.Text = e.CommandArgument.ToString();

                gvQCDetailsForCC.DataSource = ds.Tables[0]; //For CC
                gvQCDetailsForCC.DataBind();

                gvQCDetailsForDS.DataSource = ds.Tables[1]; //For DS
                gvQCDetailsForDS.DataBind();

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowQCDetails();", true);
            }
            else
            {
                gvQCDetailsForCC.DataSource = null;
                gvQCDetailsForCC.DataBind();

                gvQCDetailsForDS.DataSource = null;
                gvQCDetailsForDS.DataBind();

                lblMsg.Text = apiprocedure.Alert("fa-info", "alert-info", "Info!", "No Data Found!");
            }
        }
    }

    protected void btnSrch_Click(object sender, EventArgs e)
    {
        try
        {
            string FromDate = "", ToDate = "";
            if (txtFDate.Text != "")
            {
                FromDate = Convert.ToDateTime(txtFDate.Text, cult).ToString("yyyy/MM/dd");
            }
            if (txtTDate.Text != "")
            {
                ToDate = Convert.ToDateTime(txtTDate.Text, cult).ToString("yyyy/MM/dd");
            }

            if (apiprocedure.OfficeType_ID() == "2") // 2 Is DS
            {
                gv_PendingTankerDetails.Visible = true;
                heading_PendingTankerDetails.Visible = true;

                ds2 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                                new string[] { "flag", "Office_ID", "V_EntryType", "FromDate", "ToDate" },
                                new string[] { "19", apiprocedure.Office_ID(), "Out", FromDate, ToDate }, "dataset");

                gv_PendingTankerDetails.DataSource = ds2;
                gv_PendingTankerDetails.DataBind();
            }
            else if (apiprocedure.OfficeType_ID() == "4") // 4 Is CC
            {
                gv_PendingTankerDetails.Visible = false;
                heading_PendingTankerDetails.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
	protected void btnExportDispatch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            gvDispatchEntry.Columns[10].Visible = false;
			gvDispatchEntry.Columns[11].Visible = false;
			gvDispatchEntry.Columns[12].Visible = false;
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "mcmsReport" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gvDispatchEntry.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            gvDispatchEntry.Columns[10].Visible = true;
				gvDispatchEntry.Columns[11].Visible = true;
			gvDispatchEntry.Columns[12].Visible = true;
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnExportReceived_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            gvReceivedEntry.Columns[10].Visible = false;
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "mcmsReport" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gvReceivedEntry.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            gvReceivedEntry.Columns[10].Visible = true;
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}