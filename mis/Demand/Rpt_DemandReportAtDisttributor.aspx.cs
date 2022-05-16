using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Demand_Rpt_DemandReportAtDisttributor : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2, ds3, ds4 = new DataSet();
    double sum1, sum2 = 0;
    int sum11, sum22 = 0;
    int cellIndex = 2;
    int cellIndexbooth = 2;
    int dsum11 = 0,i_Qty=0,i_NaQty=0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"].ToString() != null && Session["Office_ID"].ToString() != null)
        {
            if (!Page.IsPostBack)
            {
                //GetOfficeDetails();
                //GetShift();
                //GetCategory();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ViewState["createdBy"] = Session["Emp_ID"].ToString();
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtOrderDate.Text = Date;
                txtOrderDate.Attributes.Add("readonly", "readonly");
                GetDetails();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================

    private void GetDetails()
    {
        try
        {
            ds2 = objdb.ByProcedure("USP_Trn_DistributorDemandPage",
                            new string[] { "Flag", "Office_ID", "DistributorId", "ItemCat_id" },
                            new string[] { "1", ViewState["Office_ID"].ToString(), ViewState["createdBy"].ToString(), "3" }, "dataset");
            if (ds2.Tables[0].Rows.Count > 0)
            {
                ddlItemCategory.DataTextField = "ItemCatName";
                ddlItemCategory.DataValueField = "ItemCat_id";
                ddlItemCategory.DataSource = ds2.Tables[0];
                ddlItemCategory.DataBind();

                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataSource = ds2.Tables[1];
                ddlShift.DataBind();

                if (ds2.Tables[4].Rows.Count > 0)
                {
                    ViewState["Office_Name"] = ds2.Tables[4].Rows[0]["Office_Name"].ToString();
                    ViewState["Office_ContactNo"] = ds2.Tables[4].Rows[0]["Office_ContactNo"].ToString();
                }

            }

        }
        catch (Exception)
        {

            throw;
        }
    }
    //protected void GetOfficeDetails()
    //{
    //    try
    //    {
    //        ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", ViewState["Office_ID"].ToString() }, "dataset");
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            ViewState["Office_Name"] = ds.Tables[0].Rows[0]["Office_Name"].ToString();
    //            ViewState["Office_ContactNo"] = ds.Tables[0].Rows[0]["Office_ContactNo"].ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
    //    }
    //}
    //protected void GetShift()
    //{
    //    try
    //    {
           
    //            ddlShift.DataTextField = "ShiftName";
    //            ddlShift.DataValueField = "Shift_id";
    //            ddlShift.DataSource = objdb.ByProcedure("USP_Mst_ShiftMaster",
    //                 new string[] { "flag" },
    //                   new string[] { "1" }, "dataset");
    //            ddlShift.DataBind();

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
    //    }
    //}
    //protected void GetCategory()
    //{
    //    try
    //    {
    //            ddlItemCategory.DataTextField = "ItemCatName";
    //            ddlItemCategory.DataValueField = "ItemCat_id";
    //            ddlItemCategory.DataSource = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
    //                 new string[] { "flag" },
    //                   new string[] { "1" }, "dataset");
    //            ddlItemCategory.DataBind();
    //            ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
    //            ddlItemCategory.SelectedValue = objdb.GetMilkCatId();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
    //    }
    //}

    //private void GetDemandDistributorWise()
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
    //        string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
    //        ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandRouteOrDistwise",
    //                 new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date", "DistributorId" },
    //                   new string[] { "6", ViewState["Office_ID"].ToString(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, orderedate,ViewState["createdBy"].ToString() }, "dataset");

    //        if (ds.Tables[0].Rows.Count != 0)
    //        {
    //            GridView1.Visible = true;
    //            DataTable dt = new DataTable();
    //            dt = ds.Tables[0];

    //            foreach (DataRow drow in dt.Rows)
    //            {
    //                foreach (DataColumn column in dt.Columns)
    //                {
    //                    //if (column.ToString() != "S.No." && column.ToString() != "DistributorId" && column.ToString() != "Distributor/Superstockist Name" && column.ToString() != "TotalSupply" && column.ToString() != "Total Demand in Litre")
    //                    if (column.ToString() != "S.No." && column.ToString() != "DistributorId" && column.ToString() != "Distributor Name" && column.ToString() != "Total Demand")
    //                    {

    //                        if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
    //                        {
    //                            drow[column] = 0;
    //                        }

    //                    }

    //                }
    //            }
    //            // dt.Columns.Remove("DistributorId");
    //            GridView1.DataSource = dt;
    //            GridView1.DataBind();

    //            GridView1.FooterRow.Cells[1].Text = "Total Demand";
    //            GridView1.FooterRow.Cells[1].Font.Bold = true;

    //            DataTable dtcrate = new DataTable();// create dt for Crate total
    //            DataRow drcrate;

    //            dtcrate.Columns.Add("ItemName", typeof(string));
    //            dtcrate.Columns.Add("CrateQty", typeof(int));
    //            dtcrate.Columns.Add("CratePacketQty", typeof(String));
    //            drcrate = dtcrate.NewRow();
    //            foreach (DataColumn column in dt.Columns)
    //            {
    //                //if (column.ToString() != "S.No." && column.ToString() != "DistributorId" && column.ToString() != "Distributor/Superstockist Name" && column.ToString() != "Total Demand in Litre")
    //                if (column.ToString() != "S.No." && column.ToString() != "DistributorId" && column.ToString() != "Distributor Name" && column.ToString() != "Total Demand")
    //                {

    //                    sum11 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

    //                    GridView1.FooterRow.Cells[cellIndex].Text = sum11.ToString();
    //                    GridView1.FooterRow.Cells[cellIndex].Font.Bold = true;
    //                    cellIndex = cellIndex + 1;

    //                    //  below code for crate calculation

    //                if (ddlItemCategory.SelectedValue == "3")
    //                {
    //                    //  below code for crate calculation
    //                    ds3 = objdb.ByProcedure("USP_Mst_SetItemCarriageMode",
    //                        new string[] { "Flag", "ItemCat_id", "Itemname", "Office_ID","EffectiveDate" },
    //                        new string[] { "7", ddlItemCategory.SelectedValue, column.ToString(), ViewState["Office_ID"].ToString(), orderedate }, "dataset");

    //                    if (ds3.Tables[0].Rows.Count > 0)
    //                    {
    //                        i_Qty = Convert.ToInt32(ds3.Tables[0].Rows[0]["ItemQtyByCarriageMode"].ToString());
    //                        i_NaQty = Convert.ToInt32(ds3.Tables[0].Rows[0]["NotIssueQty"].ToString());
    //                    }
    //                    else
    //                    {
    //                        i_Qty = 0;
    //                        i_NaQty = 0;
    //                    }
    //                    if (ds3 != null) { ds3.Dispose(); }
    //                    if (i_Qty != 0 && i_NaQty != 0)
    //                    {
    //                        int Actualcrate = 0, remenderCrate = 0, FinalCrate = 0;
    //                        string Extrapacket = "0";
    //                        Actualcrate = sum11 / i_Qty;
    //                        remenderCrate = sum11 % i_Qty;

    //                        if (remenderCrate <= i_NaQty)
    //                        {
    //                            FinalCrate = Actualcrate;
    //                            Extrapacket = remenderCrate.ToString();

    //                        }
    //                        else
    //                        {
    //                            FinalCrate = Actualcrate + 1;
    //                            Extrapacket = "-" + (i_Qty - remenderCrate);
    //                        }
    //                        drcrate[0] = column.ToString();
    //                        drcrate[1] = FinalCrate;
    //                        drcrate[2] = Extrapacket;
    //                    }
    //                    else
    //                    {
    //                        drcrate[0] = column.ToString();
    //                        drcrate[1] = "0";
    //                        drcrate[2] = "0";
    //                    }
    //                    dtcrate.Rows.Add(drcrate.ItemArray);

    //                    //  end code for crate calculation
    //                }
    //                     //  end code for crate calculation
    //                }

                    
    //            }
    //            if (ddlItemCategory.SelectedValue == "3")
    //            {
    //                //  sum and bind data in string  builder
    //                int cratetotal = Convert.ToInt32(dtcrate.Compute("SUM([" + "CrateQty" + "])", string.Empty));
    //                int Rowcount = dtcrate.Rows.Count;
    //                StringBuilder sbtable = new StringBuilder();
    //                sbtable.Append("<div style='margin-left:450px; margin-right:450px; margin-bottom:10px; text-align:center;'><b>CRATE SUMMARY</b></div>");
    //                sbtable.Append("<table style='width:100%; height:100%'>");
    //                sbtable.Append("<tr>");
    //                sbtable.Append("<td style='border: 1px solid #000000 !important;text-align:right;'>");
    //                sbtable.Append("</td>");

    //                for (int i = 0; i < Rowcount; i++)
    //                {
    //                    sbtable.Append("<td style='border: 1px solid #000000 !important;'><b>" + dtcrate.Rows[i]["ItemName"].ToString() + "</b>");



    //                }
    //                sbtable.Append("<td style='text-align: center;border: 1px solid #000000 !important;' colspan='" + (Rowcount + 1) + "'><b>TOTAL</b>");
    //                sbtable.Append("</td>");
    //                sbtable.Append("</tr>");

    //                sbtable.Append("<tr>");
    //                sbtable.Append("<td style='text-align: right;border: 1px solid #000000 !important;'><b> CRATE DETAILS</<b>");
    //                sbtable.Append("</td>");
    //                for (int i = 0; i < Rowcount; i++)
    //                {
    //                    sbtable.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + dtcrate.Rows[i]["CrateQty"].ToString() + "");


    //                }
    //                sbtable.Append("<td style='text-align: center;border: 1px solid #000000 !important;' colspan='" + (Rowcount + 1) + "'><b>" + cratetotal.ToString());

    //                sbtable.Append("</tr>");
    //                sbtable.Append("<tr>");
    //                sbtable.Append("<td style='text-align: right;border: 1px solid #000000 !important;'><b> EXTRA PKT(+/-)</b>");
    //                sbtable.Append("</td>");

    //                for (int i = 0; i < Rowcount; i++)
    //                {
    //                    sbtable.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + dtcrate.Rows[i]["CratePacketQty"].ToString() + "");


    //                }
    //                sbtable.Append("<td style='text-align: center;border: 1px solid #000000 !important;' colspan='" + (Rowcount + 1) + "'>");
    //                sbtable.Append("</tr>");
    //                sbtable.Append("</table>");
    //              //  divtable.InnerHtml = sbtable.ToString();
    //                ViewState["CrateDetails"] = sbtable.ToString();
    //                div1.InnerHtml = sbtable.ToString();
    //                if (dtcrate != null) { dtcrate.Dispose(); }
    //                // end  of crate binding and
    //            }
    //             // end  of crate binding and

    //            foreach (DataColumn column in dt.Columns)
    //            {
    //                if (column.ToString() == "Total Demand")
    //                {

    //                    dsum11 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

    //                    GridView1.FooterRow.Cells[cellIndex].Text = dsum11.ToString();
    //                    GridView1.FooterRow.Cells[cellIndex].Font.Bold = true;
    //                    cellIndex = cellIndex + 1;
    //                }
    //            }
    //            //if (ddlItemCategory.SelectedValue != "2") // for milk category
    //            //{
    //            //    foreach (DataColumn column in dt.Columns)
    //            //    {
    //            //        if (column.ToString() == "Total Demand in Litre")
    //            //        {

    //            //            sum1 = Convert.ToDouble(dt.Compute("SUM([" + column + "])", string.Empty));

    //            //            GridView1.FooterRow.Cells[cellIndex].Text = sum1.ToString("N2");
    //            //            GridView1.FooterRow.Cells[cellIndex].Font.Bold = true;
    //            //            cellIndex = cellIndex + 1;
    //            //        }
    //            //    }
    //            //}
    //            //if (ddlItemCategory.SelectedValue != "2")
    //            //{
    //                int rowcount = GridView1.FooterRow.Cells.Count - 2;
    //                GridView1.FooterRow.Cells[rowcount].Visible = false;
    //                GridView1.FooterRow.Cells[rowcount + 1].Visible = false;
    //            //}
    //            //else
    //            //{
    //            //    int rowcount = GridView1.FooterRow.Cells.Count - 3;
    //            //    GridView1.FooterRow.Cells[rowcount].Visible = false;
    //            //    GridView1.FooterRow.Cells[rowcount + 1].Visible = false;
    //            //    GridView1.FooterRow.Cells[rowcount + 2].Visible = false;
    //            //}

    //        }
    //        else
    //        {
    //            divtable.InnerHtml = "";
    //            GridView1.DataSource = null ;
    //            GridView1.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Distwise demand Report ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds != null) { ds.Dispose(); }
            
    //    }
    //}

    
    #endregion========================================================

    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {

           
    //        //e.Row.Cells[2].Controls.Add(lnkbuttton);

    //      //  int maxrowcell1 = e.Row.Cells.Count - 1;

    //        e.Row.Cells[2].Visible = false;
    //        e.Row.Cells[3].Visible = false;

    //        //if (ddlItemCategory.SelectedValue != "3")
    //        //{
    //        //    e.Row.Cells[maxrowcell1].Visible = false;
    //        //}
    //    }
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //       // int maxheadercell1 = e.Row.Cells.Count - 1;
    //        e.Row.Cells[2].Visible = false;
    //        e.Row.Cells[3].Visible = false;

    //        //if (ddlItemCategory.SelectedValue != "3")
    //        //{
    //        //    e.Row.Cells[maxheadercell1].Visible = false;
    //        //}
    //    }

    //}
    
    private void GetParlourDetails(string rid, string did, string oid)
    {
        
        DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
        string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
        string RetailerTypeID = "0";
        if (oid != "0")
        {
            RetailerTypeID = objdb.GetInstRetailerTypeId();
        }
        ds4 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandRouteOrDistwise",
                new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Demand_Date", "RouteId", "DistributorId", "OrganizationId", "RetailerTypeID" },
                  new string[] { "7", ViewState["Office_ID"].ToString(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, orderedate, rid, did, oid, RetailerTypeID }, "dataset");
        if (ds4.Tables[0].Rows.Count > 0)
        {
            btnConsRoutePrint.Visible = true;
            btnCExoprt.Visible = true;
            pnlpopupdata.Visible = true;
            DataTable dt = new DataTable();
            dt = ds4.Tables[0];
            ViewState["PrintData4"] = dt;
            foreach (DataRow drow in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    //if (column.ToString() != "S.No." && column.ToString() != "tmp_MilkOrProductDemandId" && column.ToString() != "tmp_OrderId" && column.ToString() != "BandOName" && column.ToString() != "Total Demand" && column.ToString() != "Total Demand in Litre'")
                    if (column.ToString() != "S.No." && column.ToString() != "tmp_MilkOrProductDemandId" && column.ToString() != "tmp_OrderId" && column.ToString() != "BandOName" && column.ToString() != "Total Demand")
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

            GridView4.FooterRow.Cells[1].Text = "Total Demand";
            GridView4.FooterRow.Cells[1].Font.Bold = true;
            DataTable dtcrate = new DataTable();// create dt for Crate total
            DataRow drcrate;

            dtcrate.Columns.Add("ItemName", typeof(string));
            dtcrate.Columns.Add("CrateQty", typeof(int));
            dtcrate.Columns.Add("CratePacketQty", typeof(String));
            drcrate = dtcrate.NewRow();
            foreach (DataColumn column in dt.Columns)
            {
                //if (column.ToString() != "S.No." && column.ToString() != "tmp_MilkOrProductDemandId" && column.ToString() != "tmp_OrderId" && column.ToString() != "BandOName" && column.ToString() != "Total Demand in Litre")
                if (column.ToString() != "S.No." && column.ToString() != "tmp_MilkOrProductDemandId" && column.ToString() != "tmp_OrderId" && column.ToString() != "BandOName" && column.ToString() != "Total Demand")
                {

                    sum22 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                    GridView4.FooterRow.Cells[cellIndexbooth].Text = sum22.ToString();
                    GridView4.FooterRow.Cells[cellIndexbooth].Font.Bold = true;
                    cellIndexbooth = cellIndexbooth + 1;

                    if (ddlItemCategory.SelectedValue == "3")
                    {
                        //  below code for crate calculation
                        ds3 = objdb.ByProcedure("USP_Mst_SetItemCarriageMode",
                            new string[] { "Flag", "ItemCat_id", "Itemname", "Office_ID", "EffectiveDate" },
                            new string[] { "7", ddlItemCategory.SelectedValue, column.ToString(), ViewState["Office_ID"].ToString(), orderedate }, "dataset");

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
            }
            if (dt != null) { dt.Dispose(); }
            //if (ddlItemCategory.SelectedValue != "2") // for milk category
            //{
            //    foreach (DataColumn column in dt.Columns)
            //    {
            //        if (column.ToString() == "Total Demand in Litre")
            //        {

            //            sum2 = Convert.ToDouble(dt.Compute("SUM([" + column + "])", string.Empty));

            //            GridView4.FooterRow.Cells[cellIndexbooth].Text = sum2.ToString("N2");
            //            GridView4.FooterRow.Cells[cellIndexbooth].Font.Bold = true;
            //            cellIndexbooth = cellIndexbooth + 1;
            //        }
            //    }
            //}
            //if (ddlItemCategory.SelectedValue != "2")
            //{
                int rowcount1 = GridView4.FooterRow.Cells.Count - 3;
                GridView4.FooterRow.Cells[rowcount1].Visible = false;
                GridView4.FooterRow.Cells[rowcount1 + 1].Visible = false;
                GridView4.FooterRow.Cells[rowcount1 + 2].Visible = false;
            //}
            //else
            //{
            //    int rowcount1 = GridView4.FooterRow.Cells.Count - 4;
            //    GridView4.FooterRow.Cells[rowcount1].Visible = false;
            //    GridView4.FooterRow.Cells[rowcount1 + 1].Visible = false;
            //    GridView4.FooterRow.Cells[rowcount1 + 2].Visible = false;
            //    GridView4.FooterRow.Cells[rowcount1 + 3].Visible = false;
            //}

                ////////////////////
                
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
                    //  divtable.InnerHtml = sbtable.ToString();
                    ViewState["CrateDetails"] = sbtable.ToString();
                    divtable.InnerHtml = sbtable.ToString();
                    if (dtcrate != null) { dtcrate.Dispose(); }
                    // end  of crate binding and
               
            // end  of crate binding and
                
        }
        else
        {
           // Print1.InnerHtml = "";
           // div1.InnerHtml = "";
            btnConsRoutePrint.Visible = false;
            btnCExoprt.Visible = false;
            pnlpopupdata.Visible = true;
            GridView4.DataSource = null;
            GridView4.DataBind();
        }


    }
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           // int maxrowcell1 = e.Row.Cells.Count - 1;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            //if (ddlItemCategory.SelectedValue != "3")
            //{
            //    e.Row.Cells[maxrowcell1].Visible = false;
            //}
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //int maxheadercell1 = e.Row.Cells.Count - 1;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            //if (ddlItemCategory.SelectedValue != "3")
            //{
            //    e.Row.Cells[maxheadercell1].Visible = false;
            //}
        }
    }
    //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        if (e.CommandName == "DistwiseBooth")
    //        {
    //            Control ctrl = e.CommandSource as Control;
    //            if (ctrl != null)
    //            {
    //                lblMsg.Text = string.Empty;
    //                lblModalMsg.Text = string.Empty;
    //                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
    //                DateTime date3 = DateTime.ParseExact(txtOrderDate.Text, "dd/MM/yyyy", culture);
    //                string orderedate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
    //                LinkButton lnkbtnDistributor = (LinkButton)row.FindControl("lnkbtnDistributor");

    //                modalRootOrDistName.InnerHtml = lnkbtnDistributor.Text;
    //                modaldate.InnerHtml = txtOrderDate.Text;
    //                modelShift.InnerHtml = ddlShift.SelectedItem.Text;


    //                GetParlourDetails("0", e.CommandArgument.ToString(), "0");

    //                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);

    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds4 != null) { ds4.Dispose(); }
    //    }
    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            GetParlourDetails("0", ViewState["createdBy"].ToString(), "0");
           // GetDemandDistributorWise();
        }
    }
    protected void btnConsRoutePrint_Click(object sender, EventArgs e)
    {
        Print1.InnerHtml = "";
        Print_ParlourDetails();
       
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

    }
    //protected void btnClear_Click(object sender, EventArgs e)
    //{       
    //    lblMsg.Text = string.Empty;
    //    //GridView1.DataSource = null;
    //    //GridView1.DataBind();
    //    //divtable.InnerHtml = "";
    //    //GridView1.Visible = false;
    //}
    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btnCExoprt_Click(object sender, EventArgs e)
    {
        try
        {
            Print_ParlourDetails();
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
        ////////////////Start Of Parlor Wise Print Code   ///////////////////////

        DataTable dt4 = (DataTable)ViewState["PrintData4"];
        int Count = dt4.Rows.Count;
        int ColCount = dt4.Columns.Count;
       
        //////////////////////////////
        //////////////////////////////
        ////////////////////
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
        sb1.Append("<td style='padding: 2px 5px;'>" + modalRootOrDistName.InnerHtml + "</td>");
        sb1.Append("<td style='padding: 2px 5px;'></td>");
        sb1.Append("<td class='text-center'><b>DAY:(" + objdb.GetMilkCategoryName() + " SALE)<b></td>");
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

            if (dt4.Columns[j].ToString() != "S.No." && dt4.Columns[j].ToString() != "tmp_MilkOrProductDemandId" && dt4.Columns[j].ToString() != "tmp_OrderId" && dt4.Columns[j].ToString() != "BandOName" && dt4.Columns[j].ToString() != "Total Demand")
            {
                string ColName = dt4.Columns[j].ToString();
                sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");

            }

        }
        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Total Demand</b></td>");
        sb1.Append("</thead>");




        for (int i = 0; i < Count; i++)
        {

            sb1.Append("<tr>");
            sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + (i + 1).ToString() + "</td>");

            sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + dt4.Rows[i]["BandOName"] + "</td>");
            for (int K = 0; K < ColCount; K++)
            {
                if (dt4.Columns[K].ToString() != "S.No." && dt4.Columns[K].ToString() != "tmp_OrderId" && dt4.Columns[K].ToString() != "tmp_MilkOrProductDemandId" && dt4.Columns[K].ToString() != "BandOName" && dt4.Columns[K].ToString() != "Total Demand")
                {
                    string ColName = dt4.Columns[K].ToString();
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + dt4.Rows[i][ColName].ToString() + "</td>");


                }

            }
            sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + dt4.Rows[i]["Total Demand"].ToString() + "</td>");



            sb1.Append("</tr>");




        }
        sb1.Append("<tr>");
        int ColumnCount = GridView4.FooterRow.Cells.Count - 3;
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

        ////////////////Start Of Parlor Wise Print Code   ///////////////////////
    }
}