using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_Demand_Rpt_DailyDemandStatusAtDock : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds3, ds4 = new DataSet();
    double sum1, sum2 = 0, sum3 = 0;
    int sum11, sum22 = 0, sum33 = 0;
    int dsum11 = 0, dsum22 = 0, dsum33 = 0, dsum44 = 0;
    int cellIndex = 2;
    int cellIndexSS = 2;
    int cellIndexDist = 4;
    int i_Qty = 0, i_NaQty = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtOrderDate.Text = Date;
                txtOrderDate.Attributes.Add("readonly", "true");
                GetShift();
                GetLocation();
                GetOfficeDetails();
                GetCategory();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================
    protected void GetCategory()
    {
        try
        {

            ddlItemCategory.DataTextField = "ItemCatName";
            ddlItemCategory.DataValueField = "ItemCat_id";
            ddlItemCategory.DataSource = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlItemCategory.DataBind();
            ddlItemCategory.SelectedValue = objdb.GetMilkCatId();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }

    }

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
            ddlLocation.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
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

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
    }
    private void GetDemandStatudReport()
    {
        try
        {
            lblMsg.Text = "";
            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandRouteOrDistwise",
                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date", "AreaId" },
                       new string[] { "11", objdb.Office_ID(), ddlShift.SelectedValue,ddlItemCategory.SelectedValue, orderedate, ddlLocation.SelectedValue }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                pnlData.Visible = true;
                btnPrintRoutWise.Visible = true;
                btnExportAll.Visible = true;
                // hide  after click data
                pnlSSiwist_btn.Visible = false;
                pnlpopupdata.Visible = false;
                pnlbackbutton.Visible = false;
                GridView4.DataSource = null;
                GridView4.DataBind();

                // show after click in back button
                pnlmain_btn.Visible = true;
                pnlmaindata.Visible = true;

                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                ViewState["PrintData"] = dt;
                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() != "SuperStockistId" && column.ToString() != "SuperStockist Name" && column.ToString() != "Total Demand In Pkt" && column.ToString() != "Total Demand In Ltr")
                        {

                            if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                            {
                                drow[column] = 0;
                            }

                        }

                    }
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();

                GridView1.FooterRow.Cells[1].Text = "Total";
                GridView1.FooterRow.Cells[1].Font.Bold = true;
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() != "SuperStockistId" && column.ToString() != "SuperStockist Name" && column.ToString() != "Total Demand In Ltr")
                    {

                        sum11 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView1.FooterRow.Cells[cellIndexSS].Text = sum11.ToString();
                        GridView1.FooterRow.Cells[cellIndexSS].Font.Bold = true;
                        cellIndexSS = cellIndexSS + 1;
                    }
                }
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "Total Demand In Ltr")
                    {

                        sum1 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView1.FooterRow.Cells[cellIndexSS].Text = sum1.ToString("0.00");
                        GridView1.FooterRow.Cells[cellIndexSS].Font.Bold = true;
                        cellIndexSS = cellIndexSS + 1;
                    }
                }
                int rowcount = GridView1.FooterRow.Cells.Count - 2;
                GridView1.FooterRow.Cells[rowcount].Visible = false;
                GridView1.FooterRow.Cells[cellIndexSS + 1].Visible = false;

            }
            else
            {
                pnlData.Visible = false;
                btnPrintRoutWise.Visible = false;
                btnExportAll.Visible = false;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found.");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error demand Report ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    #endregion========================================================
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
        }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "SSwiseDist")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                   // lblModalMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
                    string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    LinkButton lnkbtnSuperStockistId = (LinkButton)row.FindControl("lnkbtnSuperStockistId");

                    modalRootOrDistName.InnerHtml = lnkbtnSuperStockistId.Text;
                    modaldate.InnerHtml = txtOrderDate.Text;
                    modelShift.InnerHtml = ddlShift.SelectedItem.Text;
                   


                    // hide  after click data
                    pnlSSiwist_btn.Visible = true;
                    pnlpopupdata.Visible = true;
                    GridView4.DataSource = null;
                    GridView4.DataBind();

                    GetParlourDetails(e.CommandArgument.ToString());

                    // show after click in back button

                    pnlbackbutton.Visible = true;
                    pnlmain_btn.Visible = false;
                    pnlmaindata.Visible = false;

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
        finally
        {
            if (ds4 != null) { ds4.Dispose(); }
        }
    }
    private void GetParlourDetails(string ssid)
    {

        DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
        string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        ds4 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandRouteOrDistwise",
                new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date", "SuperStockistId", "MultiDemand_Status" },
                  new string[] { "10", objdb.Office_ID(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, orderedate, ssid,"3" }, "dataset");
        if (ds4.Tables[0].Rows.Count > 0)
        {
            pnlpopupdata.Visible = true;
            btnConsRoutePrint.Visible = true;
            btnCExoprt.Visible = true;
            DataTable dt = new DataTable();
            dt = ds4.Tables[0];
            ViewState["PrintData1"] = dt;
            foreach (DataRow drow in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() != "tmp_MilkOrProductDemandId" && column.ToString() != "tmp_OrderId" && column.ToString() != "SDName" && column.ToString() != "Total Demand In Pkt" && column.ToString() != "VehicleNo")
                    {

                        if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                        {
                            drow[column] = 0;
                        }

                    }

                }
            }
            GridView4.DataSource = dt;
            GridView4.DataBind();

            GridView4.FooterRow.Cells[3].Text = "Total";
            GridView4.FooterRow.Cells[3].Font.Bold = true;

            DataTable dtcrate = new DataTable();// create dt for Crate total
            DataRow drcrate;

            dtcrate.Columns.Add("ItemName", typeof(string));
            dtcrate.Columns.Add("CrateQty", typeof(int));
            dtcrate.Columns.Add("CratePacketQty", typeof(String));
            drcrate = dtcrate.NewRow();
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ToString() != "tmp_MilkOrProductDemandId" && column.ToString() != "tmp_OrderId" && column.ToString() != "SDName" && column.ToString() != "VehicleNo")
                {

                    sum22 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                    GridView4.FooterRow.Cells[cellIndexDist].Text = sum22.ToString();
                    GridView4.FooterRow.Cells[cellIndexDist].Font.Bold = true;
                    cellIndexDist = cellIndexDist + 1;

                    //  below code for crate calculation
                    ds3 = objdb.ByProcedure("USP_Mst_SetItemCarriageMode",
                        new string[] { "Flag", "ItemCat_id", "Itemname", "Office_ID", "EffectiveDate" },
                        new string[] { "7", objdb.GetMilkCatId(), column.ToString(), objdb.Office_ID(), orderedate }, "dataset");

                    if (ds3.Tables[0].Rows.Count > 0)
                    {
                        i_Qty = Convert.ToInt32(ds3.Tables[0].Rows[0]["ItemQtyByCarriageMode"].ToString());
                        i_NaQty = Convert.ToInt32(ds3.Tables[0].Rows[0]["NotIssueQty"].ToString());
                    }
                    else
                    {
                        i_Qty = 0;
                        i_NaQty = 0;
                    }
                    if (ds3 != null) { ds3.Dispose(); }
                    if (i_Qty != 0 && i_NaQty != 0)
                    {
                        int Actualcrate = 0, remenderCrate = 0, FinalCrate = 0;
                        string Extrapacket = "0";
                        Actualcrate = sum22 / i_Qty;
                        remenderCrate = sum22 % i_Qty;

                        if (remenderCrate <= i_NaQty)
                        {
                            FinalCrate = Actualcrate;
                            Extrapacket = remenderCrate.ToString();

                        }
                        else
                        {
                            FinalCrate = Actualcrate + 1;
                            Extrapacket = "-" + (i_Qty - remenderCrate);
                        }
                        drcrate[0] = column.ToString();
                        drcrate[1] = FinalCrate;
                        drcrate[2] = Extrapacket;
                    }
                    else
                    {
                        drcrate[0] = column.ToString();
                        drcrate[1] = "0";
                        drcrate[2] = "0";
                    }
                    dtcrate.Rows.Add(drcrate.ItemArray);

                    //  end code for crate calculation

                }
            }

            //  sum and bind data in string  builder
            int cratetotal = Convert.ToInt32(dtcrate.Compute("SUM([" + "CrateQty" + "])", string.Empty));
            int Rowcount = dtcrate.Rows.Count;
            StringBuilder sbtable = new StringBuilder();
            sbtable.Append("<div style='margin-left:450px; margin-right:450px; margin-bottom:10px; text-align:center;'><b>CRATE SUMMARY</b></div>");
            sbtable.Append("<table style='width:100%; height:100%'>");
            sbtable.Append("<tr>");
            sbtable.Append("<td style='border: 1px solid #000000 !important;text-align:right;'>");
            sbtable.Append("</td>");

            for (int i = 0; i < Rowcount; i++)
            {
                sbtable.Append("<td style='border: 1px solid #000000 !important;'><b>" + dtcrate.Rows[i]["ItemName"].ToString() + "</b>");



            }
            sbtable.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>TOTAL</b>");
            sbtable.Append("</td>");
            sbtable.Append("</tr>");

            sbtable.Append("<tr>");
            sbtable.Append("<td style='text-align: right;border: 1px solid #000000 !important;'><b> CRATE DETAILS</b>");
            sbtable.Append("</td>");
            for (int i = 0; i < Rowcount; i++)
            {
                sbtable.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + dtcrate.Rows[i]["CrateQty"].ToString() + "");


            }
            sbtable.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + cratetotal.ToString());

            sbtable.Append("</tr>");
            sbtable.Append("<tr>");
            sbtable.Append("<td style='text-align: right;border: 1px solid #000000 !important;'><b> EXTRA PKT(+/-)</b>");
            sbtable.Append("</td>");

            for (int i = 0; i < Rowcount; i++)
            {
                sbtable.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + dtcrate.Rows[i]["CratePacketQty"].ToString() + "");


            }
            sbtable.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>");
            sbtable.Append("</tr>");
            sbtable.Append("</table>");
            divtable.InnerHtml = sbtable.ToString();
            ViewState["CrateDetails"] = sbtable.ToString();
            if (dtcrate != null) { dtcrate.Dispose(); }
            // end  of crate binding and
            int rowcount1 = GridView4.FooterRow.Cells.Count - 4;
            GridView4.FooterRow.Cells[rowcount1].Visible = false;
            GridView4.FooterRow.Cells[rowcount1 + 1].Visible = false;
           GridView4.FooterRow.Cells[rowcount1 + 2].Visible = false;
           GridView4.FooterRow.Cells[rowcount1 + 3].Visible = false;


        }
        else
        {
            Print1.InnerHtml = "";           
            pnlpopupdata.Visible = true;
            GridView4.DataSource = null;
            GridView4.DataBind();
        }


    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
          
        }
    }
    protected void GetOfficeDetails()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["Office_Name"] = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                ViewState["Office_ContactNo"] = ds.Tables[0].Rows[0]["Office_ContactNo"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetDemandStatudReport();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        // hide  after click data
        pnlData.Visible = false;
        pnlSSiwist_btn.Visible = false;
        pnlpopupdata.Visible = false;
        GridView4.DataSource = null;
        GridView4.DataBind();

        GridView1.DataSource = null;
        GridView1.DataBind();

        // show after click in back button

        pnlbackbutton.Visible = false;
        pnlmain_btn.Visible = false;
        pnlmaindata.Visible = false;
    }
    protected void btnParlorWisePrint_Click(object sender, EventArgs e)
    {
        Print.InnerHtml = "";
        Print_ParlourDetails();

        Print1.InnerHtml = "";
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

    }
    protected void btnPrintRoutWise_Click(object sender, EventArgs e)
    {
        PrintAll();
        Print.InnerHtml = ViewState["Sb"].ToString();
        Print1.InnerHtml = "";
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

    }
    protected void btnConsRoutePrint_Click(object sender, EventArgs e)
    {
        PrintSuperStockistWise();
        Print.InnerHtml = "";
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {
            PrintAll(); 
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + ddlItemCategory.SelectedItem.Text + "DemandStatusReport" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            ExportAllData.InnerHtml = ViewState["Sb"].ToString();
            ExportAllData.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
    protected void btnCExoprt_Click(object sender, EventArgs e)
    {
        try
        {

            PrintSuperStockistWise();
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + modalRootOrDistName.InnerHtml + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            Print1.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-Consolidated " + ex.Message.ToString());
        }
    }
    private void PrintAll()
    {
        ////////////////Start Of Distributor Print Code   ///////////////////////

        DataTable dt1 = (DataTable)ViewState["PrintData"];
        StringBuilder sb = new StringBuilder();


        DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
        string deliverydate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
        string s = date3.DayOfWeek.ToString();
        sb.Append("<table style='width:100%; height:100%'>");
        sb.Append("<tr>");
        sb.Append("<td style='padding: 2px 5px;'><b>Shift :-" + ddlShift.SelectedItem.Text + "</b></td>");
        sb.Append("<td style='padding: 2px 5px;'></td>");
        sb.Append("<td style='padding: 2px 5px;text-align: center;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
        sb.Append("<td style='padding: 2px 5px;text-align: right;'><b>Date  :-" + txtOrderDate.Text + "</b></td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td style='padding: 2px 5px;'></td>");
        sb.Append("<td style='padding: 2px 5px;'></td>");
        sb.Append("<td style='padding: 2px 5px;text-align: center;'><b>DAY: " + s + "(" + ddlItemCategory.SelectedItem.Text + " Demand)<b></td>");
        sb.Append("<td style='padding: 2px 5px;'></td>");
        sb.Append("</tr>");

        sb.Append("</table>");
        sb.Append("<table class='table'>");
        int Count = dt1.Rows.Count;
        int ColCount = dt1.Columns.Count;
        sb.Append("<thead>");
        sb.Append("<td style='border: 1px solid #000000 !important;'><b>S.No</b></td>");
        sb.Append("<td style='border: 1px solid #000000 !important;'><b>NAME</b></td>");
        for (int j = 0; j < ColCount; j++)
        {
            if (dt1.Columns[j].ToString() != "SuperStockistId" && dt1.Columns[j].ToString() != "SuperStockist Name")
            {
                string ColName = dt1.Columns[j].ToString();
                sb.Append("<td style='border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");

            }

        }
        sb.Append("</thead>");




        for (int i = 0; i < Count; i++)
        {

            sb.Append("<tr>");
            sb.Append("<td style='border: 1px solid #000000 !important;'>" + (i + 1).ToString() + "</td>");
            sb.Append("<td style='border: 1px solid #000000 !important;'>" + dt1.Rows[i]["SuperStockist Name"] + "</td>");
            for (int K = 0; K < ColCount; K++)
            {
                if (dt1.Columns[K].ToString() != "SuperStockistId" && dt1.Columns[K].ToString() != "SuperStockist Name")
                {
                    string ColName = dt1.Columns[K].ToString();
                    sb.Append("<td style='border: 1px solid #000000 !important;'>" + dt1.Rows[i][ColName].ToString() + "</td>");
                }

            }

            sb.Append("</tr>");
        }
        sb.Append("<tr>");
        int ColumnCount = GridView1.FooterRow.Cells.Count - 2;
        for (int i = 0; i < ColumnCount; i++)
        {
            if (i == 1)
            {
                sb.Append("<td style='border: 1px solid #000000 !important;'><b>" + GridView1.FooterRow.Cells[i].Text + "</b></td>");
            }
            else
            {
                sb.Append("<td style='border: 1px solid #000000 !important;'><b>" + GridView1.FooterRow.Cells[i].Text + "</b></td>");
            }
        }
        sb.Append("</tr>");
        sb.Append("</table>");
        ViewState["Sb"] = sb.ToString();

        ////////////////End Of Distributor wise Print Code   ///////////////////////
    }

    private void PrintSuperStockistWise()
    {

        ////////////////Start Of Superstickist wise Print Code   ///////////////////////
        DataTable dt2 = (DataTable)ViewState["PrintData1"];
       
        StringBuilder sb1 = new StringBuilder();       
        sb1.Append("<table style='width:100%; height:100%'>");
        sb1.Append("<tr>");
        sb1.Append("<td style='padding: 2px 5px;'><b>Shift :-" + ddlShift.SelectedItem.Text + "</b></td>");
        sb1.Append("<td style='padding: 2px 5px;'></td>");
        sb1.Append("<td style='padding: 2px 5px;text-align:center'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
        sb1.Append("<td style='padding: 2px 5px;text-align: right;'><b>Date  :-" + txtOrderDate.Text + "</b></td>");
        sb1.Append("</tr>");
        sb1.Append("<tr>");
        sb1.Append("<td style='padding: 2px 5px;'>" + modalRootOrDistName.InnerHtml + "</td>");
        sb1.Append("<td style='padding: 2px 5px;'></td>");
        sb1.Append("<td class='text-center'><b>DAY:(" + ddlItemCategory.SelectedItem.Text + " SALE)<b></td>");
        sb1.Append("<td style='padding: 2px 5px;'></td>");
        sb1.Append("</tr>");

        sb1.Append("</table>");
        sb1.Append("<table class='table'>");
        int Count1 = dt2.Rows.Count;
        int ColCount1 = dt2.Columns.Count;
        sb1.Append("<thead>");
        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>S.no</b></td>");
        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Distributor Name</b></td>");
        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>OrderId</b></td>");
        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Vehicle No</b></td>");
        for (int j = 0; j < ColCount1; j++)
        {

            if (dt2.Columns[j].ToString() != "tmp_MilkOrProductDemandId" && dt2.Columns[j].ToString() != "tmp_OrderId" && dt2.Columns[j].ToString() != "SDName" && dt2.Columns[j].ToString() != "Total Demand In Pkt" && dt2.Columns[j].ToString() != "VehicleNo")
            {
                string ColName = dt2.Columns[j].ToString();
                sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");

            }

        }
        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Total Demand In Pkt</b></td>");
        sb1.Append("</thead>");
        for (int i = 0; i < Count1; i++)
        {

            sb1.Append("<tr>");
            sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + (i + 1).ToString() + "</td>");

            sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + dt2.Rows[i]["SDName"] + "</td>");
            sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + dt2.Rows[i]["tmp_OrderId"] + "</td>");
            sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + dt2.Rows[i]["VehicleNo"] + "</td>");
            for (int K = 0; K < ColCount1; K++)
            {
                if (dt2.Columns[K].ToString() != "tmp_OrderId" && dt2.Columns[K].ToString() != "tmp_MilkOrProductDemandId" && dt2.Columns[K].ToString() != "SDName" && dt2.Columns[K].ToString() != "Total Demand In Pkt" && dt2.Columns[K].ToString() != "VehicleNo")
                {
                    string ColName = dt2.Columns[K].ToString();
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + dt2.Rows[i][ColName].ToString() + "</td>");


                }

            }
            sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + dt2.Rows[i]["Total Demand In Pkt"].ToString() + "</td>");
            sb1.Append("</tr>");




        }
        sb1.Append("<tr>");
        int ColumnCount = GridView4.FooterRow.Cells.Count - 4;
        for (int i = 0; i < ColumnCount; i++)
        {
            if (i == 1)
            {
                sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + GridView4.FooterRow.Cells[i].Text + "</b></td>");
            }
            else
            {
                sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + GridView4.FooterRow.Cells[i].Text + "</b></td>");
            }



        }
        sb1.Append("</tr>");
        sb1.Append("</table>");

        Print1.InnerHtml = sb1.ToString();

        divtable.Visible = true;
        Print1.InnerHtml += ViewState["CrateDetails"].ToString();


        //////////////////////////
    }

    private void Print_ParlourDetails()
    {
        ////////////////Start Of Superstickist wise Print Code    ///////////////////////

        DataTable dt4 = (DataTable)ViewState["PrintData1"];
        StringBuilder sb = new StringBuilder();
        int Count = dt4.Rows.Count;
        int ColCount = dt4.Columns.Count;
        for (int i = 0; i < Count; i++)
        {

            sb.Append("<table style='width:100%; height:100%'>");
            sb.Append("<tr>");
            sb.Append("<td style='padding: 2px 5px;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style='padding: 2px 5px;'></td>");
            sb.Append("<td style='padding: 2px 5px;text-align: center;'><b>Phone: " + ViewState["Office_ContactNo"].ToString() + "</b></td>");
            sb.Append("<td style='padding: 2px 5px;'></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style='padding: 2px 5px;'><b>Name: " + modalRootOrDistName.InnerText + "</b></td>");

            sb.Append("<td style='padding: 2px 5px;'><b>Date:  " + modaldate.InnerText + "</b></td>");
            sb.Append("<td style='padding: 2px 5px;'><b>Shift:  " + modelShift.InnerText + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            string BandOName = dt4.Rows[i]["SDName"].ToString();
            string[] Booth = BandOName.Split('[');
            string[] BN0 = Booth[1].Split(']');
            sb.Append("<td style='padding: 2px 5px;'><b>Distributor Name:  " + Booth[0].ToString() + "</b></td>");
            sb.Append("<td style='padding: 2px 5px;'>" + dt4.Rows[i]["VehicleNo"].ToString() + "</td>");
            sb.Append("<td style='padding: 2px 5px;'><b>(Bno): " + BN0[0].ToString() + "</b></td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<table class='table' style='width:100%; height:100%'>");
            sb.Append("<tr>");
            sb.Append("<td style=' border: 1px solid #000000 !important;'><b>" + ddlItemCategory.SelectedItem.Text + "</b></td>");
            for (int j = 0; j < ColCount; j++)
            {
                if (dt4.Columns[j].ToString() != "S.No." && dt4.Columns[j].ToString() != "tmp_MilkOrProductDemandId" && dt4.Columns[j].ToString() != "tmp_OrderId" && dt4.Columns[j].ToString() != "SDName" && dt4.Columns[j].ToString() != "VehicleNo" && dt4.Columns[j].ToString() != "Total Demand In Pkt")
                {
                    string ColName = dt4.Columns[j].ToString();
                    sb.Append("<td style=' border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");

                }

            }
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td style=' border: 1px solid #000000 !important;'><b>Demand In Pkt</b></td>");
            for (int K = 0; K < ColCount; K++)
            {
                if (dt4.Columns[K].ToString() != "S.No." && dt4.Columns[K].ToString() != "tmp_OrderId" && dt4.Columns[K].ToString() != "tmp_MilkOrProductDemandId" && dt4.Columns[K].ToString() != "SDName" && dt4.Columns[K].ToString() != "VehicleNo" && dt4.Columns[K].ToString() != "Total Demand In Pkt")
                {
                    string ColName = dt4.Columns[K].ToString();
                    sb.Append("<td style=' border: 1px solid #000000 !important;'>" + dt4.Rows[i][ColName].ToString() + "</td>");


                }

            }
            sb.Append("</tr>");
            sb.Append("</table>");
            if (i == (Count - 1))
            {

            }
            else
            {
                sb.Append("<div class='pagebreak'></div>");
            }


        }
        Print.InnerHtml = sb.ToString();

        ////////////////Start Of Parlor Wise Print Code   ///////////////////////
    }

    protected void lnkbtnback_Click(object sender, EventArgs e)
    {
        // hide  after click data
        pnlSSiwist_btn.Visible = false;
        pnlpopupdata.Visible = false;
        GridView4.DataSource = null;
        GridView4.DataBind();

        // show after click in back button

        pnlbackbutton.Visible = false;
        pnlmain_btn.Visible = true;
        pnlmaindata.Visible = true;
    }
}