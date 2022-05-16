using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Demand_Rpt_DemandReportAtSuperStockist : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds3, ds4 = new DataSet();
    double sum1, sum2 = 0;
    int sum11, sum22 = 0;
    int cellIndex = 2;
    int cellIndexbooth = 4;
    int dsum11 = 0, i_Qty = 0, i_NaQty = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && ddlItemCategory.SelectedValue != null)
        {
            if (!Page.IsPostBack)
            {
                GetOfficeDetails();
                GetShift();
                GetCategory();
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtOrderDate.Text = Date;
                txtOrderDate.Attributes.Add("readonly", "readonly");
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================
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
    }
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
   
    #endregion========================================================

   
    private void GetParlourDetails()
    {
        
        DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
        string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        ds4 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandRouteOrDistwise",
                new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date", "SuperStockistId","MultiDemand_Status" },
                  new string[] { "10", objdb.Office_ID(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, orderedate,objdb.createdBy(),"1,3"}, "dataset");
        if (ds4.Tables[0].Rows.Count > 0)
        {
            GridView4.Visible = true;
            btnParlorWisePrint.Visible = true;
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

                    GridView4.FooterRow.Cells[cellIndexbooth].Text = sum22.ToString();
                    GridView4.FooterRow.Cells[cellIndexbooth].Font.Bold = true;
                    cellIndexbooth = cellIndexbooth + 1;

                    //  below code for crate calculation
                    ds3 = objdb.ByProcedure("USP_Mst_SetItemCarriageMode",
                        new string[] { "Flag", "ItemCat_id", "Itemname", "Office_ID", "EffectiveDate" },
                        new string[] { "7", ddlItemCategory.SelectedValue, column.ToString(), objdb.Office_ID(), orderedate }, "dataset");

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
            sbtable.Append("<td style='text-align: right;border: 1px solid #000000 !important;'><b> CRATE DETAILS </b></td>");
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
           // divtable.InnerHtml = sbtable.ToString();
            ViewState["CrateDetails"] = sbtable.ToString();
            div1.InnerHtml = sbtable.ToString();
            if (dtcrate != null) { dtcrate.Dispose(); }
            // end  of crate binding and
            int rowcount1 = GridView4.FooterRow.Cells.Count - 4;
            GridView4.FooterRow.Cells[rowcount1].Visible = false;
            GridView4.FooterRow.Cells[rowcount1 + 1].Visible = false;
            GridView4.FooterRow.Cells[rowcount1 + 2].Visible = false;
            GridView4.FooterRow.Cells[rowcount1 + 3].Visible = false;


            ////////////////////
            int Count = ds4.Tables[0].Rows.Count;
            int ColCount = ds4.Tables[0].Columns.Count;
            StringBuilder sb1 = new StringBuilder();

            //string s = date3.DayOfWeek.ToString();
            sb1.Append("<table style='width:100%; height:100%'>");
            sb1.Append("<tr>");
            sb1.Append("<td style='padding: 2px 5px;'><b>Shift :-" + ddlShift.SelectedItem.Text + "</b></td>");
            sb1.Append("<td style='padding: 2px 5px;'></td>");
            sb1.Append("<td style='padding: 2px 5px;text-align:center'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
            sb1.Append("<td style='padding: 2px 5px;text-align: right;'><b>Date  :-" + txtOrderDate.Text + "</b></td>");
            sb1.Append("</tr>");
            sb1.Append("<tr>");
            sb1.Append("<td style='padding: 2px 5px;'>" + "" + "</td>");
            sb1.Append("<td style='padding: 2px 5px;'></td>");
            sb1.Append("<td class='text-center'><b>DAY:(" + objdb.GetMilkCategoryName() + " SALE)<b></td>");
            sb1.Append("<td style='padding: 2px 5px;'></td>");
            sb1.Append("</tr>");

            sb1.Append("</table>");
            sb1.Append("<table class='table'>");
            int Count1 = ds4.Tables[0].Rows.Count;
            int ColCount1 = ds4.Tables[0].Columns.Count;
            sb1.Append("<thead>");
            sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>S.no</b></td>");
            sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Distributor Name</b></td>");
            sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>OrderId</b></td>");
            sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Vehicle No</b></td>");
            for (int j = 0; j < ColCount; j++)
            {

                if (ds4.Tables[0].Columns[j].ToString() != "tmp_MilkOrProductDemandId" && ds4.Tables[0].Columns[j].ToString() != "tmp_OrderId" && ds4.Tables[0].Columns[j].ToString() != "SDName" && ds4.Tables[0].Columns[j].ToString() != "Total Demand In Pkt" && ds4.Tables[0].Columns[j].ToString() != "VehicleNo")
                {
                    string ColName = ds4.Tables[0].Columns[j].ToString();
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");

                }

            }
            sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Total Demand In Pkt</b></td>");
            sb1.Append("</thead>");




            for (int i = 0; i < Count; i++)
            {

                sb1.Append("<tr>");
                sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + (i + 1).ToString() + "</td>");

                sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + ds4.Tables[0].Rows[i]["SDName"] + "</td>");
                sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + ds4.Tables[0].Rows[i]["tmp_OrderId"] + "</td>");
                sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + ds4.Tables[0].Rows[i]["VehicleNo"] + "</td>");
                for (int K = 0; K < ColCount; K++)
                {
                    if (ds4.Tables[0].Columns[K].ToString() != "tmp_OrderId" && ds4.Tables[0].Columns[K].ToString() != "tmp_MilkOrProductDemandId" && ds4.Tables[0].Columns[K].ToString() != "SDName" && ds4.Tables[0].Columns[K].ToString() != "Total Demand In Pkt" && ds4.Tables[0].Columns[K].ToString() != "VehicleNo")
                    {
                        string ColName = ds4.Tables[0].Columns[K].ToString();
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + ds4.Tables[0].Rows[i][ColName].ToString() + "</td>");


                    }

                }
                sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + ds4.Tables[0].Rows[i]["Total Demand In Pkt"].ToString() + "</td>");
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

           // divtable.Visible = true;
            Print1.InnerHtml += ViewState["CrateDetails"].ToString();
            ViewState["AllPrint"] = Print1.InnerHtml;

            //////////////////////////
        }
        else
        {
            Print1.InnerHtml = "";
            div1.InnerHtml = "";
            btnConsRoutePrint.Visible = false;
            btnCExoprt.Visible = false;
            btnParlorWisePrint.Visible = false;
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
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetParlourDetails();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        btnParlorWisePrint.Visible = false;
        btnConsRoutePrint.Visible = false;
        btnCExoprt.Visible = false;
        GridView4.DataSource = null;
        GridView4.DataBind();
        GridView4.Visible = false;
    }
    protected void btnParlorWisePrint_Click(object sender, EventArgs e)
    {

        Print.InnerHtml = "";
        Print1.InnerHtml = "";
        Print_ParlourDetails();       
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
       
    }
    protected void btnConsRoutePrint_Click(object sender, EventArgs e)
    {
        Print.InnerHtml = "";
        Print1.InnerHtml = "";
        Print1.InnerHtml = ViewState["AllPrint"].ToString();
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btnCExoprt_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + objdb.Emp_Name() + DateTime.Now + ".xls");
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
            string BandOName = dt4.Rows[i]["SDName"].ToString();
            string[] Booth = BandOName.Split('[');
            string[] BN0 = Booth[1].Split(']');
            sb.Append("<tr>");
            sb.Append("<td style='padding: 2px 5px;'><b>Name: " + Booth[0].ToString() + "</b></td>");

            sb.Append("<td style='padding: 2px 5px;'><b>Date:  " + txtOrderDate.Text + "</b></td>");
            sb.Append("<td style='padding: 2px 5px;'><b>Shift:  " + ddlShift.SelectedItem.Text + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
           
            sb.Append("<td style='padding: 2px 5px;'><b>Distributor Name:  " + Booth[0].ToString() + "</b></td>");
            sb.Append("<td style='padding: 2px 5px;'>" + dt4.Rows[i]["VehicleNo"].ToString() + "</td>");
            sb.Append("<td style='padding: 2px 5px;'><b>(Bno): " + BN0[0].ToString() + "</b></td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<table class='table' style='width:100%; height:100%'>");
            sb.Append("<tr>");
            sb.Append("<td style=' border: 1px solid #000000 !important;'><b>" + objdb.GetMilkCategoryName() + "</b></td>");
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
}