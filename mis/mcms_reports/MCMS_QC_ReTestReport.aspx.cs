using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Globalization;



public partial class mis_mcms_reports_MCMS_QC_ReTestReport : System.Web.UI.Page
{
    APIProcedure apiprocedure = new APIProcedure();
    IFormatProvider cult = new CultureInfo("en-IN", true);
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.SessionID != null)
        {
            if (!IsPostBack)
            {

                txtFdt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtTdt.Text = DateTime.Now.ToString("dd/MM/yyyy");

                GetDS(sender, e);
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
    protected void GetDS(object sender, EventArgs e)
    {
        try
        {
            DataSet ds1 = apiprocedure.ByProcedure("SpAdminOffice",
                                  new string[] { "flag", "OfficeType_ID" },
                                  new string[] { "5", "2" }, "dataset");


            ddlDSName3.DataSource = ds1;
            ddlDSName3.DataTextField = "Office_Name";
            ddlDSName3.DataValueField = "Office_ID";
            ddlDSName3.DataBind();
            ddlDSName3.Items.Insert(0, new ListItem("Select", "0"));

            ddlDSName3.SelectedValue = apiprocedure.Office_ID();
            ddlDSName3.Enabled = false;
            

        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1:" + ex.Message.ToString());
        }
    }

    protected void btnCCWiseQCReTestReport_Click(object sender, EventArgs e)
    {
        try
        {
            btnExport.Visible = false;
            ds = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetailsReTest",
                                  new string[] { "flag", "I_OfficeID", "FromDate", "ToDate" },
                                  new string[] { "5", ddlDSName3.SelectedValue, Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");


           if(ds != null && ds.Tables.Count > 0)
           {
               if(ds.Tables[0].Rows.Count > 0)
               {
                   btnExport.Visible = true;
                   gv_CCWiseQCReTestReport.DataSource = ds;
                   gv_CCWiseQCReTestReport.DataBind();
               }
               else
               {
                   gv_CCWiseQCReTestReport.DataSource = string.Empty;
                   gv_CCWiseQCReTestReport.DataBind();
               }
           }
           else
           {
               gv_CCWiseQCReTestReport.DataSource = string.Empty;
               gv_CCWiseQCReTestReport.DataBind();
           }


        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1:" + ex.Message.ToString());
        }
    }
    protected void gv_CCWiseQCReTestReport_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Test Entry";
            HeaderCell.ColumnSpan = 10;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "Re Test Entry";
            HeaderCell.ColumnSpan = 10;
            HeaderGridRow.Cells.Add(HeaderCell);



            gv_CCWiseQCReTestReport.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = "QC ReTest Report";
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gv_CCWiseQCReTestReport.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();

        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
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
}