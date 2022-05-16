using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web;

public partial class mis_MilkCollection_BMCTankerCollectionReport : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    MilkCalculationClass Obj_MC = new MilkCalculationClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {

            if (!IsPostBack)
            {
                lblMsg.Text = "";

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                //txtDate.Text = System.DateTime.Now.ToString();
               
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            btnExport.Visible = false;
            btnPrint.Visible = false;
            gvBMCTankerCollectionDetails.DataSource = string.Empty;
            gvBMCTankerCollectionDetails.DataBind();
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New", new string[] { "flag", "OfficeId", "D_Date" }, new string[] { "18", objdb.Office_ID(),Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd") }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    btnExport.Visible = true;
                    btnPrint.Visible = true;
                    gvBMCTankerCollectionDetails.DataSource = ds;
                    gvBMCTankerCollectionDetails.DataBind();
                    decimal TQty = 0;
                    decimal TFatkg = 0;
                    decimal TSnfkg = 0;

                    TQty = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Quantity"));
                    TFatkg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("FatKg"));
                    TSnfkg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SnfKg"));

                    gvBMCTankerCollectionDetails.FooterRow.Cells[3].Text = "<b>Total : </b>";
                    gvBMCTankerCollectionDetails.FooterRow.Cells[4].Text = "<b>" + TQty.ToString() + "</b>";
                    gvBMCTankerCollectionDetails.FooterRow.Cells[7].Text = "<b>" + TFatkg.ToString() + "</b>";
                    gvBMCTankerCollectionDetails.FooterRow.Cells[8].Text = "<b>" + TSnfkg.ToString() + "</b>";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        try
        {


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = "BMCTankerCollectionReport";
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gvBMCTankerCollectionDetails.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    protected void gvBMCTankerCollectionDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "<b>BMC</b>";
            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderGridRow.Style.Add("text-align", "center");
            gvBMCTankerCollectionDetails.Controls[0].Controls.AddAt(0, HeaderGridRow);

            string Date = txtDate.Text;
            GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell1 = new TableCell();
            HeaderCell1.Text = "<b>Date: " + Date + "</b>";
            HeaderCell1.ColumnSpan = 9;
            HeaderGridRow1.Cells.Add(HeaderCell1);

            gvBMCTankerCollectionDetails.Controls[0].Controls.AddAt(1, HeaderGridRow1);
        }
    }
}