using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Collections.Generic;
using System.Web.UI;
using System.Web;
using System.Data.OleDb;

public partial class mis_MilkCollection_RMRDChallanEntryReportViaCanes : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!IsPostBack)
            {
                txtFromDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Attributes.Add("readonly", "readonly");
                FillCC();

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
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    //protected void FillGrid()
    //{
    //    try
    //    {

    //        string FromDate = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
    //        string ToDate = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
    //        ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntryViaCanes", new string[] { "flag", "FromDate", "ToDate", "CC_Id"}, new string[] { "4", FromDate, ToDate, ddlCC.SelectedValue}, "dataset");
    //        if (ds != null && ds.Tables.Count > 0)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {

    //                gv_MilkCollectionChallanEntryDetails.DataSource = ds;
    //                gv_MilkCollectionChallanEntryDetails.DataBind();
    //                decimal TotalMilkQuantity = 0;

    //                decimal TotalFATInKg = 0;
    //                decimal TotalSnfInKg = 0;

    //                TotalMilkQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("MilkQuantity"));
    //                TotalFATInKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
    //                TotalSnfInKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
    //                gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[4].Text = "<b>Grand Total : </b>";
    //                gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[5].Text = "<b>" + TotalMilkQuantity.ToString() + "</b>";
    //                gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[9].Text = "<b>" + TotalFATInKg.ToString() + "</b>";
    //                gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[10].Text = "<b>" + TotalSnfInKg.ToString() + "</b>";
    //                gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
    //                gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Left;
    //                gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Left;
    //                GetDatatableHeaderDesign();
    //                GetDatatableFooterDesign();

    //            }
    //            else
    //            {
    //                gv_MilkCollectionChallanEntryDetails.DataSource = string.Empty;
    //                gv_MilkCollectionChallanEntryDetails.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            gv_MilkCollectionChallanEntryDetails.DataSource = string.Empty;
    //            gv_MilkCollectionChallanEntryDetails.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

    //    }
    //}
    protected void FillGrid()
    {
        try
        {
            divdetail.Visible = false;
            btnExport.Visible = false;
            string FromDate = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            string ToDate = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntryViaCanes", new string[] { "flag", "FromDate", "ToDate", "CC_Id", "Office_Parant_ID", "OfficeType_ID" }, new string[] { "8", FromDate, ToDate, ddlCC.SelectedValue, objdb.Office_ID(),objdb.OfficeType_ID() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    divdetail.Visible = true;
                    btnExport.Visible = true;
                    gv_MilkCollectionChallanEntryDetails.DataSource = ds;
                    gv_MilkCollectionChallanEntryDetails.DataBind();
                    spndate.InnerHtml = "दिनांक  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd/MM/yyyy") + " - " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd/MM/yyyy");
                    spncc.InnerHtml = "शीत केंद्र  " + "- " + ddlCC.SelectedItem.Text;
                    Span1.InnerHtml = "दिनांक  " + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd/MM/yyyy") + " - " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd/MM/yyyy");
                    Span2.InnerHtml = "शीत केंद्र  " + "- " + ddlCC.SelectedItem.Text;
                    //spnShift.InnerHtml = 
                    gvPrint.DataSource = ds;
                    gvPrint.DataBind();
                    decimal TotalBGoodMilkQuantity = 0;
                    decimal TotalBSourMilkQuantity = 0;
                    decimal TotalBCurdMilkQuantity = 0;
                    decimal TotalCGoodMilkQuantity = 0;
                    decimal TotalCSourMilkQuantity = 0;
                    decimal TotalCCurdMilkQuantity = 0;
                    decimal TotalBFATInKg = 0;
                    decimal TotalBFAT = 0;
                    decimal TotalBClr = 0;
                    decimal TotalBSnf = 0;
                    decimal TotalCFAT = 0;
                    decimal TotalCClr = 0;
                    decimal TotalCSnf = 0;


                    decimal TotalBSnfInKg = 0;
                    decimal TotalCFATInKg = 0;
                    decimal TotalCSnfInKg = 0;

                    TotalBGoodMilkQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("GBMilkQuantity"));
                    TotalBSourMilkQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SBMilkQuantity"));
                    TotalBCurdMilkQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("CBMilkQuantity"));
                    TotalCGoodMilkQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("GCMilkQuantity"));
                    TotalCSourMilkQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SCMilkQuantity"));
                    TotalCCurdMilkQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("CCMilkQuantity"));
                    TotalBFAT = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BFat"));
                    TotalBClr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BClr"));
                    TotalBSnf = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BSnf"));
                    TotalBFATInKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BFatInKg"));
                    TotalCFAT = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("CFat"));
                    TotalCClr = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("CClr"));
                    TotalCSnf = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("CSnf"));
                    TotalBSnfInKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("BSnfInKg"));
                    TotalCFATInKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("CFatInKg"));
                    TotalCSnfInKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("CSnfInKg"));
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[5].Text = "<b>Grand Total : </b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[6].Text = "<b>" + TotalBGoodMilkQuantity.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[7].Text = "<b>" + TotalBSourMilkQuantity.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[8].Text = "<b>" + TotalBCurdMilkQuantity.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[9].Text = "<b>" + TotalBFAT.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[10].Text = "<b>" + TotalBClr.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[11].Text = "<b>" + TotalBSnf.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[12].Text = "<b>" + TotalCGoodMilkQuantity.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[13].Text = "<b>" + TotalCSourMilkQuantity.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[14].Text = "<b>" + TotalCCurdMilkQuantity.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[15].Text = "<b>" + TotalCFAT.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[16].Text = "<b>" + TotalCClr.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[17].Text = "<b>" + TotalCSnf.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[18].Text = "<b>" + TotalBFATInKg.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[19].Text = "<b>" + TotalBSnfInKg.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[20].Text = "<b>" + TotalCFATInKg.ToString() + "</b>";
                    gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[21].Text = "<b>" + TotalCSnfInKg.ToString() + "</b>";

                    gvPrint.FooterRow.Cells[5].Text = "<b>Grand Total : </b>";
                    gvPrint.FooterRow.Cells[6].Text = "<b>" + TotalBGoodMilkQuantity.ToString() + "</b>";
                    gvPrint.FooterRow.Cells[7].Text = "<b>" + TotalBSourMilkQuantity.ToString() + "</b>";
                    gvPrint.FooterRow.Cells[8].Text = "<b>" + TotalBCurdMilkQuantity.ToString() + "</b>";
                    gvPrint.FooterRow.Cells[9].Text = "<b>" + TotalBFAT.ToString() + "</b>";
                    gvPrint.FooterRow.Cells[10].Text = "<b>" + TotalBClr.ToString() + "</b>";
                    gvPrint.FooterRow.Cells[11].Text = "<b>" + TotalBSnf.ToString() + "</b>";
                    gvPrint.FooterRow.Cells[12].Text = "<b>" + TotalCGoodMilkQuantity.ToString() + "</b>";
                    gvPrint.FooterRow.Cells[13].Text = "<b>" + TotalCSourMilkQuantity.ToString() + "</b>";
                    gvPrint.FooterRow.Cells[14].Text = "<b>" + TotalCCurdMilkQuantity.ToString() + "</b>";
                    gvPrint.FooterRow.Cells[15].Text = "<b>" + TotalCFAT.ToString() + "</b>";
                    gvPrint.FooterRow.Cells[16].Text = "<b>" + TotalCClr.ToString() + "</b>";
                    gvPrint.FooterRow.Cells[17].Text = "<b>" + TotalCSnf.ToString() + "</b>";
                    gvPrint.FooterRow.Cells[18].Text = "<b>" + TotalBFATInKg.ToString() + "</b>";
                    gvPrint.FooterRow.Cells[19].Text = "<b>" + TotalBSnfInKg.ToString() + "</b>";
                    gvPrint.FooterRow.Cells[20].Text = "<b>" + TotalCFATInKg.ToString() + "</b>";
                    gvPrint.FooterRow.Cells[21].Text = "<b>" + TotalCSnfInKg.ToString() + "</b>";
                    //gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                    //gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Left;
                    //gv_MilkCollectionChallanEntryDetails.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Left;

                    //GetDatatableHeaderDesign();
                    //GetDatatableFooterDesign();

                }
                else
                {
                    gv_MilkCollectionChallanEntryDetails.DataSource = string.Empty;
                    gv_MilkCollectionChallanEntryDetails.DataBind();
                }
            }
            else
            {
                gv_MilkCollectionChallanEntryDetails.DataSource = string.Empty;
                gv_MilkCollectionChallanEntryDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    private void GetDatatableFooterDesign()
    {
        try
        {
            if (gv_MilkCollectionChallanEntryDetails.Rows.Count > 0)
            {
                gv_MilkCollectionChallanEntryDetails.FooterRow.TableSection = TableRowSection.TableFooter;
                //gv_MilkCollectionChallanEntryDetails.foo = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    protected void FillCC()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                   new string[] { "flag", "I_OfficeID", "I_OfficeTypeID" },
                   new string[] { "3", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlCC.DataTextField = "Office_Name";
                        ddlCC.DataValueField = "Office_ID";
                        ddlCC.DataSource = ds;
                        ddlCC.DataBind();
                        ddlCC.Items.Insert(0, new ListItem("All", "0"));
						
						if (objdb.OfficeType_ID() == "4" || objdb.OfficeType_ID() == "4")
                        {
                            ddlCC.SelectedValue = objdb.Office_ID();
                            ddlCC.Enabled = false;
                        }

                    }
                }
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (gv_MilkCollectionChallanEntryDetails.Rows.Count > 0)
            {
                gv_MilkCollectionChallanEntryDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                gv_MilkCollectionChallanEntryDetails.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }


    protected void gv_MilkCollectionChallanEntryDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 6;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "भैंस के दूध का प्रकार";
            HeaderCell.ColumnSpan = 6;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "गाय के दूध का प्रकार";
            HeaderCell.ColumnSpan = 6;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "भैंस";
            HeaderCell.ColumnSpan = 2;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "गाय";
            HeaderCell.ColumnSpan = 2;
            HeaderGridRow.Cells.Add(HeaderCell);
            gv_MilkCollectionChallanEntryDetails.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvPrint_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 6;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "भैंस के दूध का प्रकार";
            HeaderCell.ColumnSpan = 6;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "गाय के दूध का प्रकार";
            HeaderCell.ColumnSpan = 6;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "भैंस";
            HeaderCell.ColumnSpan = 2;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "गाय";
            HeaderCell.ColumnSpan = 2;
            HeaderGridRow.Cells.Add(HeaderCell);
            gvPrint.Controls[0].Controls.AddAt(0, HeaderGridRow);

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
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = "TruckSheetReport";
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            divprint.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();

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
            GenerateData();
            CreateDBFFile();
            //lblMsg.Text = "";
            //gvDbf.Visible = true;
            //Response.Clear();
            //String sDate = Convert.ToDateTime(txtDate.Text, cult).ToString("MM/dd/yyyy HH:mm");
            //DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            //String dy = datevalue.Day.ToString();
            //String mn = datevalue.Month.ToString();
            //String yy = datevalue.Year.ToString();
            //Response.AddHeader("content-disposition", "attachment;filename=" + "MB" + mn + dy + ".dbf");
            ////Response.AddHeader("content-disposition", "attachment;filename=" + "TruckSheet"  + ".dbf");
            //Response.Charset = "";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "application/vnd.dbf";
            //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            //gvDbf.RenderControl(htmlWrite);

            //Response.Write(stringWrite.ToString());
            //gvDbf.Visible = false;
            //Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private DataTable GenerateData()
    {
        DataTable dt = new DataTable();

        DataRow dr = null;

        dt.Columns.Add(new DataColumn("T_UN_CD", typeof(string)));

        dt.Columns.Add(new DataColumn("T_SOC_CD", typeof(string)));

        dt.Columns.Add(new DataColumn("T_DATE", typeof(string)));

        dt.Columns.Add(new DataColumn("T_SHIFT", typeof(string)));

        dt.Columns.Add(new DataColumn("T_BFCW_IND", typeof(string)));

        dt.Columns.Add(new DataColumn("T_CATG", typeof(string)));

        dt.Columns.Add(new DataColumn("T_QTY", typeof(string)));

        dt.Columns.Add(new DataColumn("T_FAT", typeof(string)));

        dt.Columns.Add(new DataColumn("T_SNF", typeof(string)));

        dt.Columns.Add(new DataColumn("T_CLR", typeof(string)));

        ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntryViaCanes", new string[] { "flag", "Office_Parant_ID", "FromDate", "ToDate", "CC_Id", "OfficeType_ID" }, new string[] { "10", objdb.Office_ID(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ddlCC.SelectedValue, objdb.OfficeType_ID() }, "dataset");
        if (ds != null && ds.Tables.Count > 0)
        {
            int Count = ds.Tables[0].Rows.Count;
            for (int i = 0; i < Count; i++)
            {
                dr = dt.NewRow();

                dr["T_UN_CD"] = ds.Tables[0].Rows[i]["T_UN_CD"].ToString();

                dr["T_SOC_CD"] = ds.Tables[0].Rows[i]["T_SOC_CD"].ToString();

                dr["T_DATE"] = ds.Tables[0].Rows[i]["EntryDate"].ToString();

                dr["T_SHIFT"] = ds.Tables[0].Rows[i]["T_SHIFT"].ToString();

                dr["T_BFCW_IND"] = ds.Tables[0].Rows[i]["T_BFCW_IND"].ToString();

                dr["T_CATG"] = ds.Tables[0].Rows[i]["T_CATG"].ToString();

                dr["T_QTY"] = ds.Tables[0].Rows[i]["MilkQuantity"].ToString();

                dr["T_FAT"] = ds.Tables[0].Rows[i]["Fat"].ToString();

                dr["T_SNF"] = ds.Tables[0].Rows[i]["Snf"].ToString();

                dr["T_CLR"] = ds.Tables[0].Rows[i]["Clr"].ToString();

                dt.Rows.Add(dr);
            }
        }



        return dt;
    }
    private void CreateDBFFile()
    {
        string filepath = null;

        filepath = Server.MapPath("~//Download//");

        string TableName = "T" + DateTime.Now.ToLongTimeString().Replace(":", "").Replace("AM", "").Replace("PM", "");

        using (dBaseConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; " + " Data Source=" + filepath + "; " + "Extended Properties=dBase IV"))
        {
            dBaseConnection.Open();

            OleDbCommand olecommand = dBaseConnection.CreateCommand();

            if ((System.IO.File.Exists(filepath + "" + TableName + ".dbf")))
            {
                System.IO.File.Delete(filepath + "" + TableName + ".dbf");
                olecommand.CommandText = "CREATE TABLE [" + TableName + "] ([T_UN_CD]  varchar(10), [T_SOC_CD]  varchar(10),  [T_DATE] varchar(10),[T_SHIFT] varchar(10),[T_BFCW_IND] varchar(10),[T_CATG] varchar(10),[T_QTY] varchar(10),[T_FAT] varchar(10),[T_SNF] varchar(10),[T_CLR] varchar(10))";
                olecommand.ExecuteNonQuery();
            }
            else
            {
                olecommand.CommandText = "CREATE TABLE [" + TableName + "] ([T_UN_CD]  varchar(10), [T_SOC_CD]  varchar(10),  [T_DATE] varchar(10),[T_SHIFT] varchar(10),[T_BFCW_IND] varchar(10),[T_CATG] varchar(10),[T_QTY] varchar(10),[T_FAT] varchar(10),[T_SNF] varchar(10),[T_CLR] varchar(10))";
                olecommand.ExecuteNonQuery();
            }

            OleDbDataAdapter oleadapter = new OleDbDataAdapter(olecommand);
            OleDbCommand oleinsertCommand = dBaseConnection.CreateCommand();

            foreach (DataRow dr in GenerateData().Rows)
            {
                string Column1 = dr["T_UN_CD"].ToString();
                string Column2 = dr["T_SOC_CD"].ToString();
                string Column3 = dr["T_DATE"].ToString();
                string Column4 = dr["T_SHIFT"].ToString();
                string Column5 = dr["T_BFCW_IND"].ToString();
                string Column6 = dr["T_CATG"].ToString();
                string Column7 = dr["T_QTY"].ToString();
                string Column8 = dr["T_FAT"].ToString();
                string Column9 = dr["T_SNF"].ToString();
                string Column10 = dr["T_CLR"].ToString();


                oleinsertCommand.CommandText = "INSERT INTO [" + TableName + "] ([T_UN_CD], [T_SOC_CD],[T_DATE],[T_SHIFT],[T_BFCW_IND], [T_CATG],[T_QTY],[T_FAT],[T_SNF],[T_CLR]) VALUES ('" + Column1 + "','" + Column2 + "','" + Column3 + "','" + Column4 + "','" + Column5 + "','" + Column6 + "','" + Column7 + "','" + Column8 + "','" + Column9 + "','" + Column10 + "')";

                oleinsertCommand.ExecuteNonQuery();
            }
        }
        String FDate = Convert.ToDateTime(txtFromDate.Text, cult).ToString("MM/dd/yyyy HH:mm");
        DateTime Fdatevalue = (Convert.ToDateTime(FDate.ToString()));
        String Fdy = Fdatevalue.Day.ToString();
        String Fmn = Fdatevalue.Month.ToString();
        String Fyy = Fdatevalue.Year.ToString();

        String TDate = Convert.ToDateTime(txtToDate.Text, cult).ToString("MM/dd/yyyy HH:mm");
        DateTime Tdatevalue = (Convert.ToDateTime(TDate.ToString()));
        String Tdy = Tdatevalue.Day.ToString();
        String Tmn = Tdatevalue.Month.ToString();
        String Tyy = Tdatevalue.Year.ToString();

        FileStream sourceFile = new FileStream(filepath + "" + TableName + ".dbf", FileMode.Open);
        float FileSize = 0;
        FileSize = sourceFile.Length;
        byte[] getContent = new byte[Convert.ToInt32(Math.Truncate(FileSize))];
        sourceFile.Read(getContent, 0, Convert.ToInt32(sourceFile.Length));
        sourceFile.Close();
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Buffer = true;
        Response.ContentType = "application/dbf";
        Response.AddHeader("Content-Length", getContent.Length.ToString());
        Response.AddHeader("content-disposition", "attachment;filename=" + "MB" + Fdy + Tdy + Fmn + Fyy + ".dbf");
        Response.BinaryWrite(getContent);
        Response.Flush();
        System.IO.File.Delete(filepath + "" + TableName + ".dbf");
        Response.End();
    }

    public System.Data.OleDb.OleDbConnection dBaseConnection { get; set; }
}