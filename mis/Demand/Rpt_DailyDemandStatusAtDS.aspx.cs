using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Demand_Rpt_DailyDemandStatusAtDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds3, ds4 = new DataSet();
    double sum1, sum2 = 0,sum3=0;
    int sum11, sum22 = 0,sum33=0;
    int dsum11 = 0, dsum22 = 0, dsum33 = 0, dsum44 = 0;
    int cellIndex = 2;
    int cellIndexbooth = 2;
    int cellIndexDist = 3;
    int i_Qty = 0, i_NaQty = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetShift();
                GetLocation();
                GetOfficeDetails();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================

   
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
            ds = objdb.ByProcedure("USP_Mst_ShiftMaster",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataSource = ds;
                ddlShift.DataBind();
               // ddlShift.Items.Insert(0, new ListItem("Select", "0"));
                ddlShift.Items.Insert(0, new ListItem("All", "0"));

            }
            else
            {
                ddlShift.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void GetDemandRouteWise()
    {
        try
        {
            lblMsg.Text = "";
            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandRouteOrDistwise",
                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date","AreaId" },
                       new string[] { "1", objdb.Office_ID(), ddlShift.SelectedValue, objdb.GetMilkCatId(), orderedate,ddlLocation.SelectedValue }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                btnPrintRoutWise.Visible = true;
                btnExportAll.Visible = true;
                pnltotalDemandInLitre.Visible = true;
                pnldemand.Visible = true;
                lblTotalDemandValue.Text = "";
                lblTotalDemandInLtrValue.Text = "";
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                ViewState["PrintData"] = dt;
                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() != "RouteId" && column.ToString() != "Route" && column.ToString() != "Total Demand In Pkt" && column.ToString() != "Total Demand in Litre")
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
                    if (column.ToString() != "RouteId" && column.ToString() != "Route" && column.ToString() != "Total Demand In Pkt" && column.ToString() != "Total Demand in Litre")
                    {

                        sum11 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView1.FooterRow.Cells[cellIndex].Text = sum11.ToString();
                        GridView1.FooterRow.Cells[cellIndex].Font.Bold = true;
                        cellIndex = cellIndex + 1;
                    }
                }
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "Total Demand In Pkt")
                    {

                        dsum11 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView1.FooterRow.Cells[cellIndex].Text = dsum11.ToString();
                        GridView1.FooterRow.Cells[cellIndex].Font.Bold = true;
                        cellIndex = cellIndex + 1;
                        lblTotalDemandValue.Text = dsum11.ToString();
                    }
                }
               
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() == "Total Demand in Litre")
                        {

                            sum1 = Convert.ToDouble(dt.Compute("SUM([" + column + "])", string.Empty));

                            GridView1.FooterRow.Cells[cellIndex].Text = sum1.ToString("N2");
                            GridView1.FooterRow.Cells[cellIndex].Font.Bold = true;
                            cellIndex = cellIndex + 1;

                            lblTotalDemandInLtrValue.Text = sum1.ToString("N2");
                        }
                    }
               
              
             
                    int rowcount = GridView1.FooterRow.Cells.Count - 2;
                    GridView1.FooterRow.Cells[rowcount].Visible = false;
                    GridView1.FooterRow.Cells[rowcount + 1].Visible = false;
            }
            else
            {
                Print.InnerHtml = "";
                pnltotalDemandInLitre.Visible = false;
                pnldemand.Visible = false;
                lblTotalDemandValue.Text = "";
                lblTotalDemandInLtrValue.Text = "";
                btnPrintRoutWise.Visible = false;
                btnExportAll.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Routewise demand Report ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void GetDemandDistributorWise()
    {
        try
        {
            lblMsg.Text = "";
            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandRouteOrDistwise",
                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date", "AreaId" },
                       new string[] { "2", objdb.Office_ID(), ddlShift.SelectedValue, objdb.GetMilkCatId(), orderedate,ddlLocation.SelectedValue }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                btnPrintRoutWise.Visible = true;
                btnExportAll.Visible = true;
                pnltotalDemandInLitre.Visible = true;
                pnldemand.Visible = true;
                lblTotalDemandValue.Text = "";
                lblTotalDemandInLtrValue.Text = "";
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                ViewState["PrintData1"] = dt;
                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() != "DistributorId" && column.ToString() != "Distributor Name" && column.ToString() != "Total Demand In Pkt" && column.ToString() != "Total Demand in Litre")
                        {

                            if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                            {
                                drow[column] = 0;
                            }

                        }

                    }
                }
                // dt.Columns.Remove("DistributorId");
                GridView2.DataSource = dt;
                GridView2.DataBind();

                GridView2.FooterRow.Cells[1].Text = "Total";
                GridView2.FooterRow.Cells[1].Font.Bold = true;
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() != "DistributorId" && column.ToString() != "Distributor Name" && column.ToString() != "Total Demand In Pkt" && column.ToString() != "Total Demand in Litre")
                    {

                        sum11 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView2.FooterRow.Cells[cellIndex].Text = sum11.ToString();
                        GridView2.FooterRow.Cells[cellIndex].Font.Bold = true;
                        cellIndex = cellIndex + 1;
                    }
                }
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "Total Demand In Pkt")
                    {

                        dsum22 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView2.FooterRow.Cells[cellIndex].Text = dsum22.ToString();
                        GridView2.FooterRow.Cells[cellIndex].Font.Bold = true;
                        cellIndex = cellIndex + 1;
                        lblTotalDemandValue.Text = dsum22.ToString();
                    }
                }
               
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() == "Total Demand in Litre")
                        {

                            sum1 = Convert.ToDouble(dt.Compute("SUM([" + column + "])", string.Empty));

                            GridView2.FooterRow.Cells[cellIndex].Text = sum1.ToString("0.00");
                            GridView2.FooterRow.Cells[cellIndex].Font.Bold = true;
                            cellIndex = cellIndex + 1;

                            lblTotalDemandInLtrValue.Text = sum1.ToString("0.00");
                        }
                    }
               
              
               
                    int rowcount = GridView2.FooterRow.Cells.Count - 2;
                    GridView2.FooterRow.Cells[rowcount].Visible = false;
                    GridView2.FooterRow.Cells[rowcount + 1].Visible = false;

            }
            else
            {
                pnltotalDemandInLitre.Visible = false;
                pnldemand.Visible = false;
                lblTotalDemandValue.Text = "";
                lblTotalDemandInLtrValue.Text = "";
                btnPrintRoutWise.Visible = false;
                btnExportAll.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Distwise demand Report ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }

    private void GetDemandNotSubmittedDetails()
    {
        try
        {
            lblMsg.Text = "";
            pnltotalDemandInLitre.Visible = false;
            pnldemand.Visible = false;
            lblTotalDemandValue.Text = "";
            lblTotalDemandInLtrValue.Text = "";
            DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
            string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandRouteOrDistwise",
                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date", "AreaId" },
                       new string[] { "3", objdb.Office_ID(), ddlShift.SelectedValue,objdb.GetMilkCatId(), orderedate,ddlLocation.SelectedValue }, "dataset");
            
            GridView3.DataSource = ds.Tables[0];
            GridView3.DataBind();

            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            ViewState["PrintData3"] = dt;
           

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }

    private void GetDemandStatusByRoute()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridView1.Visible = true;
            GridView2.Visible = false;
            GridView3.Visible = false;
            pnlData.Visible = true;
            pnlrouteOrDistOrNotSubmitteddata.Visible = true;
            GetDemandRouteWise();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }

    private void GetDemandStatusByDistributor()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridView2.Visible = true;
            GridView1.Visible = false;
            GridView3.Visible = false;
            pnlData.Visible = true;
            pnlrouteOrDistOrNotSubmitteddata.Visible = true;
            GetDemandDistributorWise();



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void GetDemandStatusBySuperStockist()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView3.DataSource = null;
          
            GridView2.Visible = false;
            GridView3.Visible = false;
            GridView1.Visible = false;
            pnlData.Visible = true;
            pnlrouteOrDistOrNotSubmitteddata.Visible = true;

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void GetDemandNotSubmitted()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView3.DataSource = null;
            GridView3.DataBind();
            GridView1.Visible = false;
            GridView2.Visible = false;
            GridView3.Visible = true;
            pnlData.Visible = true;
            pnlrouteOrDistOrNotSubmitteddata.Visible = true;

            GetDemandNotSubmittedDetails();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }

    #endregion========================================================
    #region=========== init or changed even===========================
    
    private void callrblReportType()
    {
        if (rblReportType.SelectedValue == "1")
        {
            pnllegand.InnerHtml = rblReportType.SelectedItem.Text + "Item Details";
            GetDemandStatusByRoute();
        }
        else if (rblReportType.SelectedValue == "2")
        {
            pnllegand.InnerHtml = rblReportType.SelectedItem.Text + "Item Details";
            GetDemandStatusByDistributor();
        }
        else if (rblReportType.SelectedValue == "3")
        {
            pnllegand.InnerHtml = rblReportType.SelectedItem.Text + "Retailer / Institution List";
            GetDemandNotSubmitted();

        }
        else
        {
            pnlData.Visible = false;
            pnlpopupdata.Visible = false;
            pnlrouteOrDistOrNotSubmitteddata.Visible = false;

        }
    }
   
    #endregion=====================================================


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
			
			int d = e.Row.Cells.Count;
            e.Row.Cells[d - 2].Font.Size = 11;
            e.Row.Cells[d - 1].Font.Size = 11;
            e.Row.Cells[d - 2].Font.Bold = true;
            e.Row.Cells[d - 1].Font.Bold = true;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
        }

    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
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
    private void GetParlourDetails(string rid, string did, string oid)
    {
        DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
        string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
        ViewState["CrateDetails"] = "";
        string RetailerTypeID = "0";
        if (oid != "0")
        {
            RetailerTypeID = objdb.GetInstRetailerTypeId();
        }
        ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandRouteOrDistwise",
                new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date", "RouteId", "DistributorId", "OrganizationId", "RetailerTypeID","AreaId" },
                  new string[] { "4", objdb.Office_ID(), ddlShift.SelectedValue, objdb.GetMilkCatId(), orderedate, rid, did, oid, RetailerTypeID,ddlLocation.SelectedValue }, "dataset");
        if (ds.Tables[0].Rows.Count > 0)
        {
            pnlpopupdata.Visible = true;
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            ViewState["PrintData4"] = dt;
            foreach (DataRow drow in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() != "tmp_MilkOrProductDemandId" && column.ToString() != "tmp_OrderId" && column.ToString() != "BandOName" && column.ToString() != "Total Demand")
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

            GridView4.FooterRow.Cells[1].Text = "Total";
            GridView4.FooterRow.Cells[1].Font.Bold = true;

            DataTable dtcrate = new DataTable();// create dt for Crate total
            DataRow drcrate;

            dtcrate.Columns.Add("ItemName", typeof(string));
            dtcrate.Columns.Add("CrateQty", typeof(int));
            dtcrate.Columns.Add("CratePacketQty", typeof(String));
            drcrate = dtcrate.NewRow();
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ToString() != "tmp_MilkOrProductDemandId" && column.ToString() != "tmp_OrderId" && column.ToString() != "BandOName" && column.ToString() != "Total Demand In Pkt")
                {

                    sum22 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                    GridView4.FooterRow.Cells[cellIndexbooth].Text = sum22.ToString();
                    GridView4.FooterRow.Cells[cellIndexbooth].Font.Bold = true;
                    cellIndexbooth = cellIndexbooth + 1;

                   
                        //  below code for crate calculation
                        ds3 = objdb.ByProcedure("USP_Mst_SetItemCarriageMode",
                            new string[] { "Flag", "ItemCat_id", "Itemname", "Office_ID","EffectiveDate" },
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
			if(objdb.Office_ID()!="2")
            {
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ToString() == "Total Demand In Pkt")
                {

                    dsum44 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                    GridView4.FooterRow.Cells[cellIndexbooth].Text = dsum44.ToString();
                    GridView4.FooterRow.Cells[cellIndexbooth].Font.Bold = true;
                    cellIndexbooth = cellIndexbooth + 1;
                }
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
                sbtable.Append("<td style='text-align: right;border: 1px solid #000000 !important;'><b> CRATE DETAILS</<b>");
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
          
           
                if(objdb.Office_ID()=="2")
                {
                    int rowcount1 = GridView4.FooterRow.Cells.Count - 4;
                    GridView4.FooterRow.Cells[rowcount1].Visible = false;
                    GridView4.FooterRow.Cells[rowcount1 + 1].Visible = false;
                   GridView4.FooterRow.Cells[rowcount1 + 2].Visible = false;
                   GridView4.FooterRow.Cells[rowcount1 + 3].Visible = false;
                }
                else
                {
                    int rowcount1 = GridView4.FooterRow.Cells.Count - 3;
                    GridView4.FooterRow.Cells[rowcount1].Visible = false;
                    GridView4.FooterRow.Cells[rowcount1 + 1].Visible = false;
                    GridView4.FooterRow.Cells[rowcount1 + 2].Visible = false;
                }
          
           
            btnParlorWisePrint.Visible = true;
            btnConsRoutePrint.Visible = true;
           
           
         
            //////////////////////////
            //////////////////////////////
        }
        else
        {
            Print1.InnerHtml="";
            divtable.InnerHtml = "";
            pnlpopupdata.Visible = true;
            GridView4.DataSource = null;
            GridView4.DataBind();
            btnParlorWisePrint.Visible = false;
            btnConsRoutePrint.Visible = false;
        }


    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RoutwiseBooth")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    lblModalMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    LinkButton lnkbtnRoute = (LinkButton)row.FindControl("lnkbtnRoute");

                    modalRootOrDistName.InnerHtml = lnkbtnRoute.Text;
                    modaldate.InnerHtml = txtOrderDate.Text;
                    modelShift.InnerHtml = ddlShift.SelectedItem.Text;
                    btnParlorWisePrint.Visible = true;
                    GetParlourDetails(e.CommandArgument.ToString(), "0", "0");


                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 7 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
			
			 if(objdb.Office_ID()=="2")
            {
                int dd = e.Row.Cells.Count;
                e.Row.Cells[dd - 1].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
			
			 if (objdb.Office_ID() == "2")
            {
                int ddd = e.Row.Cells.Count;
                e.Row.Cells[ddd - 1].Visible = false;
            }
        }
    }
    protected void GridView6_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
        }
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "DistwiseBooth")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    lblModalMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
                    string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    LinkButton lnkbtnDistributor = (LinkButton)row.FindControl("lnkbtnDistributor");
                    btnParlorWisePrint.Visible = true;
                    modalRootOrDistName.InnerHtml = lnkbtnDistributor.Text;
                    modaldate.InnerHtml = txtOrderDate.Text;
                    modelShift.InnerHtml = ddlShift.SelectedItem.Text;


                    GetParlourDetails("0", e.CommandArgument.ToString(), "0");

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    protected void btnPrintRoutWise_Click(object sender, EventArgs e)
    {
        if (rblReportType.SelectedValue == "1")
        {
            PrintAll();
        }
        else if (rblReportType.SelectedValue == "2")
        {
            PrintAll_Dist();
        }
        else
        {
            Print_NotSubmitted();
        }
        Print.InnerHtml = ViewState["Sb"].ToString();
        Print1.InnerHtml = "";
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

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
    protected void btnConsRoutePrint_Click(object sender, EventArgs e)
    {
        
            Print.InnerHtml = "";
            Print1.InnerHtml = "";
            Print_ParlourDetails();
        Print.InnerHtml = "";
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

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
            callrblReportType();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtOrderDate.Text = string.Empty;
        ddlShift.SelectedIndex = 0;
        rblReportType.ClearSelection();
        pnlData.Visible = false;
        pnlrouteOrDistOrNotSubmitteddata.Visible = false;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {
            if(rblReportType.SelectedValue=="1")
            {
                PrintAll(); 
            }
            else if (rblReportType.SelectedValue == "2")
            {
                PrintAll_Dist();
            }
            else
            {
                Print_NotSubmitted();
            }
           
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + rblReportType.SelectedItem.Text + " DemandStatusReport" + DateTime.Now + ".xls");
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
           
            Print_ParlourDetails();
            
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
        ////////////////Start Of Route Wise Print Code   ///////////////////////

        DataTable dt1 = (DataTable)ViewState["PrintData"];
        StringBuilder sb = new StringBuilder();


        DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
        string deliverydate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
        string s = date3.DayOfWeek.ToString();
        sb.Append("<table style='width:100%; height:100%'>");
        sb.Append("<tr>");
        sb.Append("<td style='padding: 2px 5px;font-size: 18px;'><b>Shift :-" + ddlShift.SelectedItem.Text + "</b></td>");
        sb.Append("<td style='padding: 2px 5px;'></td>");
        sb.Append("<td style='padding: 2px 5px;text-align: center;font-size: 18px;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
        sb.Append("<td style='padding: 2px 5px;text-align: right;font-size: 18px;'><b>Date  :-" + txtOrderDate.Text + "</b></td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td style='padding: 2px 5px;'></td>");
        sb.Append("<td style='padding: 2px 5px;'></td>");
        sb.Append("<td style='padding: 2px 5px;text-align: center;font-size: 18px;'><b>DAY: " + s + "(" + objdb.GetMilkCategoryName() + " Demand)<b></td>");
        sb.Append("<td style='padding: 2px 5px;'></td>");
        sb.Append("</tr>");

        sb.Append("</table>");
        sb.Append("<table class='table'>");
        int Count = dt1.Rows.Count;
        int ColCount = dt1.Columns.Count;
        sb.Append("<thead>");
        sb.Append("<td style='border: 1px solid #000000 !important;'><b>S.no</b></td>");
        sb.Append("<td style='border: 1px solid #000000 !important;'><b>NAME</b></td>");
        for (int j = 0; j < ColCount; j++)
        {

            if (dt1.Columns[j].ToString() != "RouteId" && dt1.Columns[j].ToString() != "Route" && dt1.Columns[j].ToString() != "Total Demand In Pkt" && dt1.Columns[j].ToString() != "Total Demand in Litre")
            {
                string ColName = dt1.Columns[j].ToString();
                sb.Append("<td style='border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");

            }

        }
		 if(objdb.Office_ID()!="2")
        {
        sb.Append("<td style='border: 1px solid #000000 !important;'><b>Total Demand In Pkt</b></td>");
        sb.Append("<td style='border: 1px solid #000000 !important;'><b>Total Demand in LIT</b></td>");
		}
        sb.Append("</thead>");




        for (int i = 0; i < Count; i++)
        {

            sb.Append("<tr>");
            sb.Append("<td style='border: 1px solid #000000 !important;'>" + (i + 1).ToString() + "</td>");
            sb.Append("<td style='border: 1px solid #000000 !important;font-size: 18px;'><b>" + dt1.Rows[i]["Route"] + "</b></td>");
            for (int K = 0; K < ColCount; K++)
            {
                if (dt1.Columns[K].ToString() != "S.No." && dt1.Columns[K].ToString() != "RouteId" && dt1.Columns[K].ToString() != "Route" && dt1.Columns[K].ToString() != "Total Demand In Pkt" && dt1.Columns[K].ToString() != "Total Demand in Litre")
                {
                    string ColName = dt1.Columns[K].ToString();
                    sb.Append("<td style='border: 1px solid #000000 !important;'>" + dt1.Rows[i][ColName].ToString() + "</td>");


                }

            }
	    if(objdb.Office_ID()!="2")
        {
            sb.Append("<td style='border: 1px solid #000000 !important;font-size: 18px;font-weight: bold;'>" + dt1.Rows[i]["Total Demand In Pkt"].ToString() + "</td>");

            sb.Append("<td style='border: 1px solid #000000 !important;font-size: 18px;font-weight: bold;'>" + dt1.Rows[i]["Total Demand in Litre"].ToString() + "</td>");
		}

            sb.Append("</tr>");


        }
        sb.Append("<tr>");
        int ColumnCount=0;
        if (objdb.Office_ID() != "2")
        {
            ColumnCount = GridView1.FooterRow.Cells.Count - 2;
        }
        else 
        {
            ColumnCount = GridView1.FooterRow.Cells.Count - 4;
        }
        for (int i = 0; i < ColumnCount; i++)
        {
            if (i == 1)
            {
                sb.Append("<td style='border: 1px solid #000000 !important;font-size: 18px;'><b>" + GridView1.FooterRow.Cells[i].Text + "</b></td>");
            }
            else
            {
                sb.Append("<td style='border: 1px solid #000000 !important;font-size: 18px;'><b>" + GridView1.FooterRow.Cells[i].Text + "</b></td>");
            }



        }
        sb.Append("</tr>");
        sb.Append("</table>");
        sb.Append("<b><span style='padding-top:20px;font-size: 18px;'>Total Demand In Pkt:  " + lblTotalDemandValue.Text + "</span><br></b>");
        sb.Append("<b><span style='padding-top:20px;font-size: 18px;'>Demand In Ltr :  " + lblTotalDemandInLtrValue.Text + "</span></b>");
	   
        ViewState["Sb"] = sb.ToString();

        ////////////////End Of Route Wise Print Code   ///////////////////////
    }

    private void PrintAll_Dist()
    {
        ////////////////Start Of Distributor Wise Print Code   ///////////////////////
        StringBuilder sb = new StringBuilder();
        DataTable dt2 = (DataTable)ViewState["PrintData1"];
        DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
        string deliverydate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
        string s = date3.DayOfWeek.ToString();
        sb.Append("<table style='width:100%; height:100%'>");
        sb.Append("<tr>");
        sb.Append("<td style='padding: 2px 5px;'><b>Shift :-" + ddlShift.SelectedItem.Text + "</b></td>");
        sb.Append("<td style='padding: 2px 5px;'></td>");
        sb.Append("<td style='padding: 2px 5px;text-align: center;'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
        sb.Append("<td style='padding: 2px 5px;text-align: right;'><b>Date  :-" + txtOrderDate.Text + "</b></td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td style='padding: 2px 5px;'></td>");
        sb.Append("<td style='padding: 2px 5px;'></td>");
        sb.Append("<td style='padding: 2px 5px;text-align: center;'><b>DAY: " + s + "(" + objdb.GetMilkCategoryName() + " Demand)<b></td>");
        sb.Append("<td style='padding: 2px 5px;'></td>");
        sb.Append("</tr>");

        sb.Append("</table>");
        sb.Append("<table class='table'>");
        int Count = dt2.Rows.Count;
        int ColCount = dt2.Columns.Count;
        sb.Append("<thead>");
        sb.Append("<td style='border: 1px solid #000000 !important;'><b>S.no</b></td>");
        sb.Append("<td style='border: 1px solid #000000 !important;'><b>Distributor Name</b></td>");
        for (int j = 0; j < ColCount; j++)
        {

            if (dt2.Columns[j].ToString() != "S.No." && dt2.Columns[j].ToString() != "DistributorId" && dt2.Columns[j].ToString() != "Distributor Name" && dt2.Columns[j].ToString() != "Total Demand In Pkt" && dt2.Columns[j].ToString() != "Total Demand in Litre")
            {
                string ColName = dt2.Columns[j].ToString();
                sb.Append("<td style='border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");

            }

        }
        sb.Append("<td style='border: 1px solid #000000 !important;'><b>Total Demand In Pkt</b></td>");

        sb.Append("<td style='border: 1px solid #000000 !important;'><b>Total Demand in LIT</b></td>");

        sb.Append("</thead>");




        for (int i = 0; i < Count; i++)
        {

            sb.Append("<tr>");
            sb.Append("<td style='border: 1px solid #000000 !important;'>" + (i + 1).ToString() + "</td>");
            sb.Append("<td style='border: 1px solid #000000 !important;'>" + dt2.Rows[i]["Distributor Name"] + "</td>");
            for (int K = 0; K < ColCount; K++)
            {
                if (dt2.Columns[K].ToString() != "S.No." && dt2.Columns[K].ToString() != "DistributorId" && dt2.Columns[K].ToString() != "Distributor Name" && dt2.Columns[K].ToString() != "Total Demand In Pkt" && dt2.Columns[K].ToString() != "Total Demand in Litre")
                {
                    string ColName = dt2.Columns[K].ToString();
                    sb.Append("<td style='border: 1px solid #000000 !important;'>" + dt2.Rows[i][ColName].ToString() + "</td>");


                }

            }
            sb.Append("<td style='border: 1px solid #000000 !important;'>" + dt2.Rows[i]["Total Demand In Pkt"].ToString() + "</td>");

            sb.Append("<td style='border: 1px solid #000000 !important;'>" + dt2.Rows[i]["Total Demand in Litre"].ToString() + "</td>");

            sb.Append("</tr>");




        }
        sb.Append("<tr>");
        int ColumnCount = GridView2.FooterRow.Cells.Count - 2;
        for (int i = 0; i < ColumnCount; i++)
        {
            if (i == 1)
            {
                sb.Append("<td style='border: 1px solid #000000 !important;'><b>" + GridView2.FooterRow.Cells[i].Text + "</b></td>");
            }
            else
                sb.Append("<td style='border: 1px solid #000000 !important;'><b>" + GridView2.FooterRow.Cells[i].Text + "</b></td>");
            {
            }



        }
        sb.Append("</tr>");
        sb.Append("</table>");
        sb.Append("<b><span style='padding-top:20px;'>Total Demand In Pkt:  " + lblTotalDemandValue.Text + "</span><br></b>");


        sb.Append("<b><span style='padding-top:20px;'>Demand In Ltr :  " + lblTotalDemandInLtrValue.Text + "</span></b>");

        ViewState["Sb"] = sb.ToString();


        ////////////////End Of Distributor Wise Print Code   ///////////////////////
    }


    private void Print_NotSubmitted()
    {
        ////////////////Start Of not Submitted Code   ///////////////////////
        DataTable dt3 = (DataTable)ViewState["PrintData3"];
        StringBuilder sb = new StringBuilder();
        DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
        string deliverydate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
        string s = date3.DayOfWeek.ToString();
        sb.Append("<table style='width:100%; height:100%'>");
        sb.Append("<tr>");
        sb.Append("<td style='padding: 2px 5px;'><b>Shift :-" + ddlShift.SelectedItem.Text + "</<b>b></td>");

        sb.Append("<td  style='padding: 2px 5px;text-align: center;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
        sb.Append("<td style='padding: 2px 5px;text-align: right;'><b>Date  :-" + txtOrderDate.Text + "</b></td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td colspan='3' style='padding: 2px 5px;text-align: center;'><b>DAY: " + s + "(" + objdb.GetMilkCategoryName() + " (Not Submitted RETAILER/INSTITUTION LIST))<b></td>");

        sb.Append("</tr>");

        sb.Append("</table>");
        sb.Append("<table class='table table1-bordered' style='width:100%;'>");
        int Count = dt3.Rows.Count;
        int ColCount = dt3.Columns.Count;
        sb.Append("<thead>");
        sb.Append("<tr>");
        sb.Append("<td style='border: 1px solid #000000 !important;'><b>S.no</b></td>");

        sb.Append("<td style='border: 1px solid #000000 !important;'><b>Route</b></td>");
        sb.Append("<td style='border: 1px solid #000000 !important;'><b>Retailer </b></td>");
        sb.Append("</tr>");
        sb.Append("</thead>");

        for (int i = 0; i < Count; i++)
        {

            sb.Append("<tr>");
            sb.Append("<td style='border: 1px solid #000000 !important;'>" + (i + 1).ToString() + "</td>");
            sb.Append("<td style='border: 1px solid #000000 !important;'>" + dt3.Rows[i]["RName"].ToString() + "</td>");
            sb.Append("<td style='border: 1px solid #000000 !important;'>" + dt3.Rows[i]["BoothName"].ToString() + "</td>");
            sb.Append("</tr>");
        }
        sb.Append("</table>");

        ViewState["Sb"] = sb.ToString();

        ////////////////End Of Submitted Wise Print Code   ///////////////////////
    }
    private void Print_ParlourDetails()
    {
        ////////////////Start Of Parlor Wise Print Code   ///////////////////////

        DataTable dt4 = (DataTable)ViewState["PrintData4"];
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
            if (rblReportType.SelectedValue.ToString() == "2")
            {
                sb.Append("<td style='padding: 2px 5px;font-size: 20px;'><b>Distributor Name: " + modalRootOrDistName.InnerText + "</b></td>");
            }
            else if (rblReportType.SelectedValue.ToString() == "1")
            {
                sb.Append("<td style='padding: 2px 5px;font-size: 20px;'><b>Route No: " + modalRootOrDistName.InnerText + "</b></td>");
            }



            sb.Append("<td style='padding: 2px 5px;'><b>Date:  " + modaldate.InnerText + "</b></td>");
            sb.Append("<td style='padding: 2px 5px;'><b>Shift:  " + modelShift.InnerText + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            string BandOName = dt4.Rows[i]["BandOName"].ToString();
            string[] Booth = BandOName.Split('[');
            string[] BN0 = Booth[1].Split(']');
            sb.Append("<td style='padding: 2px 5px;'><b>Booth Name:  " + Booth[0].ToString() + "</b></td>");
            sb.Append("<td style='padding: 2px 5px;'></td>");
            sb.Append("<td style='padding: 2px 5px;'><b>(Bno): " + BN0[0].ToString() + "</b></td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<table class='table' style='width:100%; height:100%'>");
            sb.Append("<tr>");
            sb.Append("<td style=' border: 1px solid #000000 !important;'><b>" + objdb.GetMilkCategoryName() + "</b></td>");
            for (int j = 0; j < ColCount; j++)
            {
                if (dt4.Columns[j].ToString() != "S.No." && dt4.Columns[j].ToString() != "tmp_MilkOrProductDemandId" && dt4.Columns[j].ToString() != "tmp_OrderId" && dt4.Columns[j].ToString() != "tmp_ChallanNo" && dt4.Columns[j].ToString() != "BandOName" && dt4.Columns[j].ToString() != "Total Demand In Pkt")
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
                if (dt4.Columns[K].ToString() != "S.No." && dt4.Columns[K].ToString() != "tmp_OrderId" && dt4.Columns[K].ToString() != "tmp_MilkOrProductDemandId" && dt4.Columns[K].ToString() != "tmp_ChallanNo" && dt4.Columns[K].ToString() != "BandOName" && dt4.Columns[K].ToString() != "Total Demand In Pkt")
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

        //////////////////////////////
        //////////////////////////////
        ////////////////////
        StringBuilder sb1 = new StringBuilder();

        //string s = date3.DayOfWeek.ToString();
        sb1.Append("<table style='width:100%; height:100%'>");
        sb1.Append("<tr>");
        sb1.Append("<td style='padding: 2px 5px;font-size: 20px;'><b>Shift :-" + ddlShift.SelectedItem.Text + "</b></td>");
        sb1.Append("<td style='padding: 2px 5px;'></td>");
        sb1.Append("<td style='padding: 2px 5px;text-align:center;font-size: 20px;'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
        sb1.Append("<td style='padding: 2px 5px;text-align: right;font-size: 20px;'><b>Date  :-" + txtOrderDate.Text + "</b></td>");
        sb1.Append("</tr>");
        sb1.Append("<tr>");
        sb1.Append("<td style='padding: 2px 5px;font-size: 20px;'>" + modalRootOrDistName.InnerHtml + "</td>");
        sb1.Append("<td style='padding: 2px 5px;'></td>");
        sb1.Append("<td class='text-center' style='font-size: 20px;'><b>DAY:(" + objdb.GetMilkCategoryName() + " SALE)<b></td>");
        sb1.Append("<td style='padding: 2px 5px;'></td>");
        sb1.Append("</tr>");

        sb1.Append("</table>");
        sb1.Append("<table class='table'>");
        int Count1 = dt4.Rows.Count;
        int ColCount1 = dt4.Columns.Count;
        sb1.Append("<thead>");
        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>S.no</b></td>");
        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Retailer/Institution Name</b></td>");
        for (int j = 0; j < ColCount; j++)
        {

            if (dt4.Columns[j].ToString() != "S.No." && dt4.Columns[j].ToString() != "tmp_MilkOrProductDemandId" && dt4.Columns[j].ToString() != "tmp_OrderId" && dt4.Columns[j].ToString() != "BandOName" && dt4.Columns[j].ToString() != "Total Demand In Pkt")
            {
                string ColName = dt4.Columns[j].ToString();
                sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");

            }

        }
       if(objdb.Office_ID()!="2")
        {
        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Total Demand In Pkt</b></td>");
        }
        sb1.Append("</thead>");




        for (int i = 0; i < Count; i++)
        {

            sb1.Append("<tr>");
            sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + (i + 1).ToString() + "</td>");

            sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + dt4.Rows[i]["BandOName"] + "</td>");
            for (int K = 0; K < ColCount; K++)
            {
                if (dt4.Columns[K].ToString() != "S.No." && dt4.Columns[K].ToString() != "tmp_OrderId" && dt4.Columns[K].ToString() != "tmp_MilkOrProductDemandId" && dt4.Columns[K].ToString() != "BandOName" && dt4.Columns[K].ToString() != "Total Demand In Pkt")
                {
                    string ColName = dt4.Columns[K].ToString();
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + dt4.Rows[i][ColName].ToString() + "</td>");


                }

            }
            if (objdb.Office_ID() != "2")
            {
                sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + dt4.Rows[i]["Total Demand In Pkt"].ToString() + "</td>");

            }



            sb1.Append("</tr>");




        }
        sb1.Append("<tr>");
        int ColumnCount=0;
        if(objdb.Office_ID()=="2")
        {
           ColumnCount= GridView4.FooterRow.Cells.Count - 4;
        }
        else
        {
            ColumnCount = GridView4.FooterRow.Cells.Count - 3;
        }
        for (int i = 0; i < ColumnCount; i++)
        {
            if (i == 1)
            {
                sb1.Append("<td style=' border: 1px solid #000000 !important;font-size: 20px'><b>" + GridView4.FooterRow.Cells[i].Text + "</b></td>");
            }
            else
            {
                sb1.Append("<td style=' border: 1px solid #000000 !important;font-size:20px'><b>" + GridView4.FooterRow.Cells[i].Text + "</b></td>");
            }



        }
        sb1.Append("</tr>");
        sb1.Append("</table>");

        Print1.InnerHtml = sb1.ToString();
        divtable.Visible = true;
        Print1.InnerHtml += ViewState["CrateDetails"].ToString();

        ////////////////Start Of Parlor Wise Print Code   ///////////////////////
    }
}