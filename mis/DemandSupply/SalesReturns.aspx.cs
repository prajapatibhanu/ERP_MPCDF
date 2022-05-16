using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Demand_Supply_SalesReturns : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2, ds3, ds4 = new DataSet();
    double sum1, sum2 = 0, sum3 = 0;
    int sum11 = 0, sum22 = 0, sum33 = 0;
    //int dsum11 = 0, dsum22 = 0, dsum33 = 0;
    //int csum11 = 0, csum22 = 0, csum33 = 0;
    int cellIndex = 2;
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
                GetBackRecord();
               
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
            ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
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
                ddlShift.Items.Insert(0, new ListItem("Select", "0"));

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
    private void GetApprovedDemandRouteWise()
    {
        try
        {
            lblMsg.Text = "";
            DateTime date3 = DateTime.ParseExact(txtSupplyDate.Text, "dd/MM/yyyy", culture);
            string supplydate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductReturnSupply",
                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Delivery_Date","AreaId" },
                       new string[] { "1", objdb.Office_ID(), ddlShift.SelectedValue, objdb.GetMilkCatId(), supplydate,ddlLocation.SelectedValue }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                btnViewSalesReturnDetails.Visible = true;
                btnPrintRoutWise.Visible = true;
                DataTable dt, dt1 = new DataTable();
                dt = ds1.Tables[0];
                dt1 = ds1.Tables[1];
                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() != "RouteId" && column.ToString() != "Route" && column.ToString() != "Total Supply(In Pkt)")
                        {

                            if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                            {
                                drow[column] = 0;
                            }

                        }

                    }
                }
                foreach (DataRow drow in dt1.Rows)
                {
                    foreach (DataColumn column in dt1.Columns)
                    {
                        if (column.ToString() != "RouteId" && column.ToString() != "Route" && column.ToString() != "Total Supply(In Pkt)")
                        {

                            if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                            {
                                drow[column] = 0;
                            }

                        }

                    }
                }
                //GridView1.DataSource = ds1.Tables[1];
                //GridView1.DataBind();
                GridView1.DataSource = dt1;
                GridView1.DataBind();

                GridView1.FooterRow.Cells[1].Text = "Total (In Pkt)";
                GridView1.FooterRow.Cells[1].Font.Bold = true;
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() != "S.No." && column.ToString() != "RouteId" && column.ToString() != "Route")
                    {

                        sum11 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView1.FooterRow.Cells[cellIndex].Text = sum11.ToString();
                        GridView1.FooterRow.Cells[cellIndex].Font.Bold = true;
                        cellIndex = cellIndex + 1;
                    }
                }

                int rowcount = GridView1.FooterRow.Cells.Count - 2;
                GridView1.FooterRow.Cells[rowcount].Visible = false;
                GridView1.FooterRow.Cells[rowcount + 1].Visible = false;

                if (dt != null) { dt.Dispose(); }
                if (dt1 != null) { dt1.Dispose(); }
                ////////////////Start Of Route Wise Print Code   ///////////////////////
                
                StringBuilder sb = new StringBuilder();

                string s = date3.DayOfWeek.ToString();
                sb.Append("<table class='table1' style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td>Shift :-" + ddlShift.SelectedItem.Text + "</td>");
                sb.Append("<td></td>");
                sb.Append("<td class='text-center'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sb.Append("<td class='text-right'>Date  :-" + txtSupplyDate.Text + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td class='text-center'><b>DAY: " + s + "(" + objdb.GetMilkCategoryName() + " SALE)<b></td>");
                sb.Append("<td></td>");
                sb.Append("</tr>");

                sb.Append("</table>");
                sb.Append("<table class='table table1-bordered'>");
                int Count = ds1.Tables[1].Rows.Count;
                int ColCount = ds1.Tables[1].Columns.Count;
                sb.Append("<thead>");
                sb.Append("<td>S.No</td>");
                sb.Append("<td>NAME</td>");
                for (int j = 0; j < ColCount; j++)
                {

                    //if (ds1.Tables[1].Columns[j].ToString() != "S.No." && ds1.Tables[1].Columns[j].ToString() != "RouteId" && ds1.Tables[1].Columns[j].ToString() != "Route" && ds1.Tables[1].Columns[j].ToString() != "Total Supply" && ds1.Tables[1].Columns[j].ToString() != "Total Crate" && ds1.Tables[1].Columns[j].ToString() != "Total Supply in Litre")
                    if (ds1.Tables[1].Columns[j].ToString() != "S.No." && ds1.Tables[1].Columns[j].ToString() != "RouteId" && ds1.Tables[1].Columns[j].ToString() != "Route" && ds1.Tables[1].Columns[j].ToString() != "Total Supply(In Pkt)")
                    {
                        string ColName = ds1.Tables[1].Columns[j].ToString();
                        sb.Append("<td>" + ColName + "</td>");

                    }

                }
                sb.Append("<td>Total Supply(In Pkt)</td>");
               
                // sb.Append("<td>Total Sale in LIT</td>");
                sb.Append("</thead>");




                for (int i = 0; i < Count; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td>" + (i + 1).ToString() + "</td>");
                    sb.Append("<td>" + ds1.Tables[1].Rows[i]["Route"] + "</td>");
                    for (int K = 0; K < ColCount; K++)
                    {
                        //if (ds1.Tables[1].Columns[K].ToString() != "S.No." && ds1.Tables[1].Columns[K].ToString() != "RouteId" && ds1.Tables[1].Columns[K].ToString() != "Route" && ds1.Tables[1].Columns[K].ToString() != "Total Supply" && ds1.Tables[1].Columns[K].ToString() != "Total Crate" && ds1.Tables[1].Columns[K].ToString() != "Total Supply in Litre")
                        if (ds1.Tables[1].Columns[K].ToString() != "S.No." && ds1.Tables[1].Columns[K].ToString() != "RouteId" && ds1.Tables[1].Columns[K].ToString() != "Route" && ds1.Tables[1].Columns[K].ToString() != "Total Supply(In Pkt)")
                        {
                            string ColName = ds1.Tables[1].Columns[K].ToString();
                            sb.Append("<td>" + ds1.Tables[1].Rows[i][ColName].ToString() + "</td>");


                        }

                    }
                    sb.Append("<td>" + ds1.Tables[1].Rows[i]["Total Supply(In Pkt)"].ToString() + "</td>");
                   
                    // sb.Append("<td>" + ds1.Tables[1].Rows[i]["Total Supply in Litre"].ToString() + "</td>");
                    sb.Append("</tr>");




                }
                sb.Append("<tr>");
                int ColumnCount = GridView1.FooterRow.Cells.Count - 2;
                for (int i = 0; i < ColumnCount; i++)
                {
                    if (i == 1)
                    {
                        sb.Append("<td><b>" + GridView1.FooterRow.Cells[i].Text + "</b></td>");
                    }
                    else
                    {
                        sb.Append("<td>" + GridView1.FooterRow.Cells[i].Text + "</td>");
                    }



                }
                sb.Append("</tr>");
                sb.Append("</table>");
               
                //  sb.Append("<b><span style='padding-top:20px;'>Total Crate Required :  " + lblTotalCrateValue.Text + "</span></b>");
                ViewState["Sb"] = sb.ToString();

                ////////////////End Of Route Wise Print Code   ///////////////////////
            }
            else
            {
                btnViewSalesReturnDetails.Visible = false;
                btnPrintRoutWise.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Routewise ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    private void GetApprovedDemandDistributorWise()
    {
        try
        {
            lblMsg.Text = "";
            DateTime date3 = DateTime.ParseExact(txtSupplyDate.Text, "dd/MM/yyyy", culture);
            string supplydate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductReturnSupply",
                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Delivery_Date","AreaId" },
                       new string[] { "2", objdb.Office_ID(), ddlShift.SelectedValue, objdb.GetMilkCatId(), supplydate,ddlLocation.SelectedValue }, "dataset");

            if (ds2.Tables[0].Rows.Count != 0)
            {
                btnViewSalesReturnDetails.Visible = true;
                btnPrintRoutWise.Visible = true;
                DataTable dt, dt2 = new DataTable();
                dt = ds2.Tables[0];
                dt2 = ds2.Tables[1];

                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() != "DistributorId" && column.ToString() != "Distributor Name" && column.ToString() != "Total Supply(In Pkt)")
                        {

                            if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                            {
                                drow[column] = 0;
                            }

                        }

                    }
                }
                foreach (DataRow drow in dt2.Rows)
                {
                    foreach (DataColumn column in dt2.Columns)
                    {
                        if (column.ToString() != "DistributorId" && column.ToString() != "Distributor Name" && column.ToString() != "Total Supply(In Pkt)")
                        {

                            if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                            {
                                drow[column] = 0;
                            }

                        }

                    }
                }
                //GridView2.DataSource = ds2.Tables[1];
                //GridView2.DataBind();
                GridView2.DataSource = dt2;
                GridView2.DataBind();

                GridView2.FooterRow.Cells[1].Text = "Total (In Pkt)";
                GridView2.FooterRow.Cells[1].Font.Bold = true;
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() != "DistributorId" && column.ToString() != "Distributor Name")
                    {

                        sum22 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView2.FooterRow.Cells[cellIndex].Text = sum22.ToString();
                        GridView2.FooterRow.Cells[cellIndex].Font.Bold = true;
                        cellIndex = cellIndex + 1;
                    }
                }
                int rowcount = GridView2.FooterRow.Cells.Count - 2;
                GridView2.FooterRow.Cells[rowcount].Visible = false;
                GridView2.FooterRow.Cells[rowcount + 1].Visible = false;

                if (dt != null) { dt.Dispose(); }
                if (dt2 != null) { dt2.Dispose(); }
                ////////////////Start Of Distributor Wise Print Code   ///////////////////////
                StringBuilder sb = new StringBuilder();
                string s = date3.DayOfWeek.ToString();
                sb.Append("<table class='table1' style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td>Shift :-" + ddlShift.SelectedItem.Text + "</td>");
                sb.Append("<td></td>");
                sb.Append("<td class='text-center'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sb.Append("<td class='text-right'>Date  :-" + txtSupplyDate.Text + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td class='text-center'><b>DAY: " + s + "(" + objdb.GetMilkCategoryName() + " SALE)<b></td>");
                sb.Append("<td></td>");
                sb.Append("</tr>");

                sb.Append("</table>");
                sb.Append("<table class='table table1-bordered'>");
                int Count = ds2.Tables[1].Rows.Count;
                int ColCount = ds2.Tables[1].Columns.Count;
                sb.Append("<thead>");
                sb.Append("<td>S.No</td>");
                sb.Append("<td>NAME</td>");
                for (int j = 0; j < ColCount; j++)
                {

                    //if (ds2.Tables[1].Columns[j].ToString() != "S.No." && ds2.Tables[1].Columns[j].ToString() != "DistributorId" && ds2.Tables[1].Columns[j].ToString() != "Distributor/Superstockist Name" && ds2.Tables[1].Columns[j].ToString() != "Total Supply" && ds2.Tables[1].Columns[j].ToString() != "Total Crate" && ds2.Tables[1].Columns[j].ToString() != "Total Supply in Litre")
                    if (ds2.Tables[1].Columns[j].ToString() != "S.No." && ds2.Tables[1].Columns[j].ToString() != "DistributorId" && ds2.Tables[1].Columns[j].ToString() != "Distributor Name" && ds2.Tables[1].Columns[j].ToString() != "Total Supply(In Pkt)")
                    {
                        string ColName = ds2.Tables[1].Columns[j].ToString();
                        sb.Append("<td>" + ColName + "</td>");

                    }

                }
                sb.Append("<td>Total Supply(In Pkt)</td>");
                
                sb.Append("</thead>");




                for (int i = 0; i < Count; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td>" + (i + 1).ToString() + "</td>");
                    sb.Append("<td>" + ds2.Tables[1].Rows[i]["Distributor Name"] + "</td>");
                    for (int K = 0; K < ColCount; K++)
                    {
                        //if (ds2.Tables[1].Columns[K].ToString() != "S.No." && ds2.Tables[1].Columns[K].ToString() != "DistributorId" && ds2.Tables[1].Columns[K].ToString() != "Distributor/Superstockist Name" && ds2.Tables[1].Columns[K].ToString() != "Total Supply" && ds2.Tables[1].Columns[K].ToString() != "Total Crate" && ds2.Tables[1].Columns[K].ToString() != "Total Supply in Litre")
                        if (ds2.Tables[1].Columns[K].ToString() != "S.No." && ds2.Tables[1].Columns[K].ToString() != "DistributorId" && ds2.Tables[1].Columns[K].ToString() != "Distributor Name" && ds2.Tables[1].Columns[K].ToString() != "Total Supply(In Pkt)")
                        {
                            string ColName = ds2.Tables[1].Columns[K].ToString();
                            sb.Append("<td>" + ds2.Tables[1].Rows[i][ColName].ToString() + "</td>");


                        }

                    }
                    sb.Append("<td>" + ds2.Tables[1].Rows[i]["Total Supply(In Pkt)"].ToString() + "</td>");
                    
                    sb.Append("</tr>");




                }
                sb.Append("<tr>");
                int ColumnCount = GridView2.FooterRow.Cells.Count - 2;
                for (int i = 0; i < ColumnCount; i++)
                {
                    if (i == 1)
                    {
                        sb.Append("<td><b>" + GridView2.FooterRow.Cells[i].Text + "</b></td>");
                    }
                    else
                    {
                        sb.Append("<td>" + GridView2.FooterRow.Cells[i].Text + "</td>");
                    }



                }
                sb.Append("</tr>");
                sb.Append("</table>");
               
                // sb.Append("<b><span style='padding-top:20px;'>Total Crate Required :  " + lblTotalCrateValue.Text + "</span></b>");
                ViewState["Sb"] = sb.ToString();

                ////////////////End Of Distributor Wise Print Code   ///////////////////////

            }
            else
            {
                btnViewSalesReturnDetails.Visible = false;
                btnPrintRoutWise.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error DistributorWise ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    private void GetApprovedDemandOrganizationWise()
    {
        try
        {
            lblMsg.Text = "";
            DateTime date3 = DateTime.ParseExact(txtSupplyDate.Text, "dd/MM/yyyy", culture);
            string supplydate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds3 = objdb.ByProcedure("USP_Trn_MilkOrProductReturnSupply",
                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Delivery_Date","AreaId" },
                       new string[] { "3", objdb.Office_ID(), ddlShift.SelectedValue, objdb.GetMilkCatId(), supplydate,ddlLocation.SelectedValue }, "dataset");

            if (ds3.Tables[0].Rows.Count != 0)
            {
                btnViewSalesReturnDetails.Visible = true;
                btnPrintRoutWise.Visible = true;
                DataTable dt, dt3 = new DataTable();
                dt = ds3.Tables[0];
                dt3 = ds3.Tables[1];
                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        //if (column.ToString() != "S.No." && column.ToString() != "OrganizationId" && column.ToString() != "Organization Name" && column.ToString() != "Total Supply")
                        if (column.ToString() != "S.No." && column.ToString() != "BoothId" && column.ToString() != "Institution Name" && column.ToString() != "Total Supply(In Pkt)")
                        {

                            if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                            {
                                drow[column] = 0;
                            }

                        }

                    }
                }
                foreach (DataRow drow in dt3.Rows)
                {
                    foreach (DataColumn column in dt3.Columns)
                    {
                        //if (column.ToString() != "S.No." && column.ToString() != "OrganizationId" && column.ToString() != "Organization Name" && column.ToString() != "Total Supply")
                        if (column.ToString() != "S.No." && column.ToString() != "BoothId" && column.ToString() != "Institution Name" && column.ToString() != "Total Supply(In Pkt)")
                        {

                            if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                            {
                                drow[column] = 0;
                            }

                        }

                    }
                }
                GridView3.DataSource = dt3;
                GridView3.DataBind();
                //GridView3.DataSource = ds3.Tables[1];
                //GridView3.DataBind();

                GridView3.FooterRow.Cells[1].Text = "Total (In Pkt)";
                GridView3.FooterRow.Cells[1].Font.Bold = true;
                foreach (DataColumn column in dt.Columns)
                {
                    //if (column.ToString() != "S.No." && column.ToString() != "OrganizationId" && column.ToString() != "Organization Name")
                    if (column.ToString() != "S.No." && column.ToString() != "BoothId" && column.ToString() != "Institution Name")
                    {

                        sum33 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView3.FooterRow.Cells[cellIndex].Text = sum33.ToString();
                        GridView3.FooterRow.Cells[cellIndex].Font.Bold = true;
                        cellIndex = cellIndex + 1;
                    }
                }

                int rowcount = GridView3.FooterRow.Cells.Count - 2;
                GridView3.FooterRow.Cells[rowcount].Visible = false;
                GridView3.FooterRow.Cells[rowcount + 1].Visible = false;

                if (dt != null) { dt.Dispose(); }
                if (dt3 != null) { dt3.Dispose(); }
                ////////////////Start Of Distributor Wise Print Code   ///////////////////////
                StringBuilder sb = new StringBuilder();
                string s = date3.DayOfWeek.ToString();
                sb.Append("<table class='table1' style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td>Shift :-" + ddlShift.SelectedItem.Text + "</td>");
                sb.Append("<td></td>");
                sb.Append("<td class='text-center'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sb.Append("<td class='text-right'>Date  :-" + txtSupplyDate.Text + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td class='text-center'><b>DAY: " + s + "(" + objdb.GetMilkCategoryName() + " SALE)<b></td>");
                sb.Append("<td></td>");
                sb.Append("</tr>");

                sb.Append("</table>");
                sb.Append("<table class='table table1-bordered'>");
                int Count = ds3.Tables[1].Rows.Count;
                int ColCount = ds3.Tables[1].Columns.Count;
                sb.Append("<thead>");
                sb.Append("<td>S.No</td>");
                sb.Append("<td>NAME</td>");
                for (int j = 0; j < ColCount; j++)
                {

                    //if (ds3.Tables[1].Columns[j].ToString() != "S.No." && ds3.Tables[1].Columns[j].ToString() != "OrganizationId" && ds3.Tables[1].Columns[j].ToString() != "Organization Name" && ds3.Tables[1].Columns[j].ToString() != "Total Supply" && ds3.Tables[1].Columns[j].ToString() != "Total Crate" && ds3.Tables[1].Columns[j].ToString() != "Total Supply in Litre")
                    //if (ds3.Tables[1].Columns[j].ToString() != "S.No." && ds3.Tables[1].Columns[j].ToString() != "OrganizationId" && ds3.Tables[1].Columns[j].ToString() != "Organization Name" && ds3.Tables[1].Columns[j].ToString() != "Total Supply" && ds3.Tables[1].Columns[j].ToString() != "Total Crate")
                    if (ds3.Tables[1].Columns[j].ToString() != "S.No." && ds3.Tables[1].Columns[j].ToString() != "BoothId" && ds3.Tables[1].Columns[j].ToString() != "Institution Name" && ds3.Tables[1].Columns[j].ToString() != "Total Supply(In Pkt)")
                    {
                        string ColName = ds3.Tables[1].Columns[j].ToString();
                        sb.Append("<td>" + ColName + "</td>");

                    }

                }
                sb.Append("<td>Total Supply(In Pkt)</td>");
              
                sb.Append("</thead>");




                for (int i = 0; i < Count; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td>" + (i + 1).ToString() + "</td>");
                    //sb.Append("<td>" + ds3.Tables[1].Rows[i]["Organization Name"] + "</td>");
                    sb.Append("<td>" + ds3.Tables[1].Rows[i]["Institution Name"] + "</td>");
                    for (int K = 0; K < ColCount; K++)
                    {
                        //if (ds3.Tables[1].Columns[K].ToString() != "S.No." && ds3.Tables[1].Columns[K].ToString() != "OrganizationId" && ds3.Tables[1].Columns[K].ToString() != "Organization Name" && ds3.Tables[1].Columns[K].ToString() != "Total Supply" && ds3.Tables[1].Columns[K].ToString() != "Total Crate" && ds3.Tables[1].Columns[K].ToString() != "Total Supply in Litre")
                        //if (ds3.Tables[1].Columns[K].ToString() != "S.No." && ds3.Tables[1].Columns[K].ToString() != "OrganizationId" && ds3.Tables[1].Columns[K].ToString() != "Organization Name" && ds3.Tables[1].Columns[K].ToString() != "Total Supply" && ds3.Tables[1].Columns[K].ToString() != "Total Crate")
                        if (ds3.Tables[1].Columns[K].ToString() != "S.No." && ds3.Tables[1].Columns[K].ToString() != "BoothId" && ds3.Tables[1].Columns[K].ToString() != "Institution Name" && ds3.Tables[1].Columns[K].ToString() != "Total Supply(In Pkt)")
                        {
                            string ColName = ds3.Tables[1].Columns[K].ToString();
                            sb.Append("<td>" + ds3.Tables[1].Rows[i][ColName].ToString() + "</td>");


                        }

                    }
                    sb.Append("<td>" + ds3.Tables[1].Rows[i]["Total Supply(In Pkt)"].ToString() + "</td>");
                  
                    sb.Append("</tr>");




                }
                sb.Append("<tr>");
                int ColumnCount = GridView3.FooterRow.Cells.Count - 2;
                for (int i = 0; i < ColumnCount; i++)
                {
                    if (i == 1)
                    {
                        sb.Append("<td><b>" + GridView3.FooterRow.Cells[i].Text + "</b></td>");
                    }
                    else
                    {
                        sb.Append("<td>" + GridView3.FooterRow.Cells[i].Text + "</td>");
                    }



                }
                sb.Append("</tr>");
                sb.Append("</table>");
                
                ViewState["Sb"] = sb.ToString();

                ////////////////End Of Institution Wise Print Code   ///////////////////////
            }
            else
            {
                btnViewSalesReturnDetails.Visible = false;
                btnPrintRoutWise.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Institution ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
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
            pnlrouteOrDistOrInstwisedata.Visible = true;
            GetApprovedDemandRouteWise();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
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
            pnlrouteOrDistOrInstwisedata.Visible = true;
            GetApprovedDemandDistributorWise();
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
    private void GetDemandStatusByInstitute()
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
            pnlrouteOrDistOrInstwisedata.Visible = true;
            GetApprovedDemandOrganizationWise();

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
    private void callrblReportType()
    {
        if (txtSupplyDate.Text != "" && ddlShift.SelectedValue != "0")
        {
            if (rblReportType.SelectedValue == "1")
            {
                pnllegand.InnerHtml = rblReportType.SelectedItem.Text + "Item Details for Sales Return";
                GetDemandStatusByRoute();
            }
            else if (rblReportType.SelectedValue == "2")
            {
                pnllegand.InnerHtml = rblReportType.SelectedItem.Text + "Item Details for Sales Return";
                GetDemandStatusByDistributor();
            }
            else if (rblReportType.SelectedValue == "3")
            {
                pnllegand.InnerHtml = rblReportType.SelectedItem.Text + "Item Details for Sales Return";
                GetDemandStatusByInstitute();

            }
            else
            {
                pnlData.Visible = false;
                pnlrouteOrDistOrInstwisedata.Visible = false;

            }
        }
    }
    #endregion========================================================
    #region=========== init or changed even===========================
    
    //protected void txtSupplyDate_TextChanged(object sender, EventArgs e)
    //{
    //    ddlShift.SelectedIndex = 0;
    //    ddlItemCategory.SelectedIndex = 0;
    //    pnlSearchBy.Visible = false;
    //    rblReportType.ClearSelection();
    //    pnlData.Visible = false;
    //    pnlrouteOrDistOrInstwisedata.Visible = false;
    //}
    //protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (txtSupplyDate.Text != "" || ddlShift.SelectedValue != "0")
    //    {
    //        ddlItemCategory.SelectedIndex = 0;
    //        pnlSearchBy.Visible = false;
    //        rblReportType.ClearSelection();
    //        pnlData.Visible = false;
    //        pnlrouteOrDistOrInstwisedata.Visible = false;
    //    }

    //}
    //protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (txtSupplyDate.Text != "" && ddlShift.SelectedValue != "0" && ddlItemCategory.SelectedValue != "0")
    //    {
    //        if (ddlShift.SelectedItem.Text == "Evening" && ddlItemCategory.SelectedItem.Text == "Product")
    //        {
    //            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :  ", "Order of " + ddlItemCategory.SelectedItem.Text + " list can show only in Morning shift.");
    //            ddlItemCategory.SelectedIndex = 0;
    //            rblReportType.ClearSelection();
    //            pnlSearchBy.Visible = false;
    //            pnlData.Visible = false;
    //            pnlrouteOrDistOrInstwisedata.Visible = false;
    //        }
    //        else
    //        {
    //            lblMsg.Text = string.Empty;
    //            pnlSearchBy.Visible = true;
    //            rblReportType.ClearSelection();
    //            pnlData.Visible = false;
    //            pnlrouteOrDistOrInstwisedata.Visible = false;
    //        }

    //    }
    //    else
    //    {
    //        rblReportType.ClearSelection();
    //        pnlSearchBy.Visible = false;
    //        pnlData.Visible = false;
    //        pnlrouteOrDistOrInstwisedata.Visible = false;
    //    }
    //}
    //protected void rblReportType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    callrblReportType();
    //}
    #endregion=====================================================

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
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
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
            if (e.CommandName == "RoutwiseBooth")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    LinkButton lnkbtnRoute = (LinkButton)row.FindControl("lnkbtnRoute");
                    Session["SDate"] = txtSupplyDate.Text;
                    Session["SShift"] = ddlShift.SelectedValue;
                    Session["SShiftName"] = ddlShift.SelectedItem.Text;
                    Session["SCategory"] = objdb.GetMilkCatId();
                    Session["SCategoryName"] = objdb.GetMilkCategoryName();
                    Session["SRDIType"] = rblReportType.SelectedValue;
                    Session["S_RDIName"] = lnkbtnRoute.Text;
                    Session["S_RouteId"] = e.CommandArgument.ToString();
                    Session["S_DistributorId"] = "0";
                    Session["S_OrganizationId"] = "0";
                    Session["SLocation"] = ddlLocation.SelectedValue;
                    Response.Redirect("SalesReturnsChild.aspx");



                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
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
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    LinkButton lnkbtnDistributor = (LinkButton)row.FindControl("lnkbtnDistributor");
                    Session["SDate"] = txtSupplyDate.Text;
                    Session["SShift"] = ddlShift.SelectedValue;
                    Session["SShiftName"] = ddlShift.SelectedItem.Text;
                    Session["SCategory"] = objdb.GetMilkCatId();
                    Session["SCategoryName"] = objdb.GetMilkCategoryName();
                    Session["SRDIType"] = rblReportType.SelectedValue;
                    Session["S_RDIName"] = lnkbtnDistributor.Text;
                    Session["S_RouteId"] = "0";
                    Session["S_DistributorId"] = e.CommandArgument.ToString();
                    Session["S_OrganizationId"] = "0";
                    Session["SLocation"] = ddlLocation.SelectedValue;
                    Response.Redirect("SalesReturnsChild.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 7 ", ex.Message.ToString());
        }
    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Orgwise")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    LinkButton lnkbtnOrganization = (LinkButton)row.FindControl("lnkbtnOrganization");
                    Session["SDate"] = txtSupplyDate.Text;
                    Session["SShift"] = ddlShift.SelectedValue;
                    Session["SShiftName"] = ddlShift.SelectedItem.Text;
                    Session["SCategory"] = objdb.GetMilkCatId();
                    Session["SCategoryName"] = objdb.GetMilkCategoryName();
                    Session["SRDIType"] = rblReportType.SelectedValue;
                    Session["S_RDIName"] = lnkbtnOrganization.Text;
                    Session["S_RouteId"] = "0";
                    Session["S_DistributorId"] = "0";
                    Session["S_OrganizationId"] = e.CommandArgument.ToString();
                    Session["SLocation"] = ddlLocation.SelectedValue;
                    Response.Redirect("SalesReturnsChild.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 8 ", ex.Message.ToString());
        }
    }
    private void GetBackRecord()
    {
        if (Session["SDate"] != null && Session["SShift"] != null && Session["SShiftName"] != null && Session["SRDIType"] != null)
        {
            txtSupplyDate.Text = Session["SDate"].ToString();
            ddlShift.SelectedValue = Session["SShift"].ToString();
          //  ddlItemCategory.SelectedValue = Session["SCategory"].ToString();
            rblReportType.SelectedValue = Session["SRDIType"].ToString();
            ddlLocation.SelectedValue = Session["SLocation"].ToString();
            pnlSearchBy.Visible = true;
            callrblReportType();
            Session["SDate"] = "";
            Session["SShift"] = "";
            Session["SCategory"] = "";
            Session["SRDIType"] = "";
            Session["SCategoryName"] = "";
            Session["S_RDIName"] = "";
            Session["SShiftName"] = "";
            Session["S_RouteId"] = "";
            Session["S_DistributorId"] = "";
            Session["S_OrganizationId"] = "";
            Session["SLocation"] = "";
        }
        else
        {
            txtSupplyDate.Text = string.Empty;
            ddlShift.SelectedIndex = 0;
           // ddlItemCategory.SelectedIndex = 0;
            rblReportType.ClearSelection();
            pnlData.Visible = false;
            pnlrouteOrDistOrInstwisedata.Visible = false;
        }
    }
    protected void BindStringbuilderRouteWise()
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            if (ViewState["RoutOrDisOrInstWiseTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["RoutOrDisOrInstWiseTable"];
                int totalrow = dt.Rows.Count;
                int totalcolumn = dt.Columns.Count;

                sb.Append("<table class='table table-striped table-bordered'><tr>");
                sb.Append("<th>S.No.</th>");
                for (int j = 0; j < totalcolumn; j++)
                {
                    if (j == 0 || j == (totalcolumn - 3) || j == (totalcolumn - 1))
                    {
                        sb.Append("<th style='display:none;'>" + dt.Columns[j].ColumnName.ToString() + "</th>");
                    }
                    else
                    {
                        sb.Append("<th>" + dt.Columns[j].ColumnName.ToString() + "</th>");
                    }
                }
                sb.Append("</tr>");
                for (int i = 0; i < totalrow; i++)
                {
                    sb.Append("<tr><td>" + (i + 1) + "</td>");
                    for (int j = 0; j < totalcolumn; j++)
                    {
                        if (j == 0 || j == (totalcolumn - 3) || j == (totalcolumn - 1))
                        {
                            sb.Append("<td style='display:none;'>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + "</td>");
                        }
                        else if (j == (totalcolumn - 4))
                        {
                            Int32 checkvalue = Convert.ToInt32(dt.Rows[i][dt.Columns[j + 1].ColumnName.ToString()].ToString());

                            if (checkvalue > 0)
                            {
                                sb.Append("<td>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + " [-<span style='color:red'>" + checkvalue.ToString() + "</span>] </td>");
                            }
                            else
                            {
                                sb.Append("<td>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + " [" + checkvalue.ToString() + "] </td>");
                            }

                        }
                        else if (j == (totalcolumn - 2))
                        {
                            decimal checkvalue1 = Convert.ToDecimal(dt.Rows[i][dt.Columns[j + 1].ColumnName.ToString()].ToString());

                            if (checkvalue1 > 0)
                            {
                                sb.Append("<td>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + " [-<span style='color:red'>" + checkvalue1.ToString() + "</span>] </td>");
                            }
                            else
                            {
                                sb.Append("<td>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + " [" + checkvalue1.ToString() + "] </td>");
                            }

                        }
                        else
                        {
                            sb.Append("<td>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + "</td>");
                        }
                    }
                    sb.Append("</tr>");

                }
                string bindtr = "", bindtr1 = "";
                sb.Append("<tr><td></td><td>Total</td>");
                for (int j = 0; j < totalcolumn; j++)
                {
                    int totalmorp = 0;
                    int totalmorpbracket = 0;
                    decimal totalmorLtr = 0;
                    decimal totalmorLtrbracket = 0;
                    string Showtotal = "0";
                    string ShowNetSupply = "0";
                    if (dt.Columns[j].ColumnName.ToString() != "RouteId" && dt.Columns[j].ColumnName.ToString() != "Route")
                    {
                        for (int i = 0; i < totalrow; i++)
                        {
                            string colm = "";
                            if (dt.Columns[j].ColumnName.ToString() == "Total Supply(In Pkt)")
                            {
                                colm = dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString() + " [-" + dt.Rows[i][dt.Columns[j + 1].ColumnName.ToString()].ToString().ToString() + "]";
                            }
                            else if (dt.Columns[j].ColumnName.ToString() == "Total Sales Return(In Pkt)")
                            {
                                colm = dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString() + " [-" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString() + "]";
                            }
                            else if (dt.Columns[j].ColumnName.ToString() == "Total Supply(In Ltr)")
                            {
                                colm = dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString() + " [-" + dt.Rows[i][dt.Columns[j+1].ColumnName.ToString()].ToString().ToString() + "]";
                            }
                            else if (dt.Columns[j].ColumnName.ToString() == "Total Sales Return(In Ltr)")
                            {
                                colm = dt.Rows[i][dt.Columns[j - 1].ColumnName.ToString()].ToString().ToString() + " [-" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString() + "]";
                            }
                            else
                            {
                                colm = dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString();
                            }

                            string[] s = colm.Split(' ');
                            string[] sa1;
                            string ABC = "0";
                            if (dt.Columns[j].ColumnName.ToString() == "Total Supply(In Ltr)" || dt.Columns[j].ColumnName.ToString() == "Total Sales Return(In Ltr)")
                            {
                                totalmorLtr += Convert.ToDecimal(s[0]);
                            }
                            else
                            {
                                totalmorp += Convert.ToInt32(s[0]);
                            }
                            
                            if (s[1].Contains("-"))
                            {
                                string[] sa = colm.Split('-');
                                sa1 = sa[1].Split(']');
                                ABC = sa1[0];
                            }
                            else
                            {
                                ABC = "0";
                            }

                            if (dt.Columns[j].ColumnName.ToString() == "Total Supply(In Ltr)" || dt.Columns[j].ColumnName.ToString() == "Total Sales Return(In Ltr)")
                            {
                                totalmorLtrbracket += Convert.ToDecimal(ABC);
                                Showtotal = "<span style='color:green;'>" + totalmorLtr.ToString() + "</span>" + " [<span style='color:red;'>-" + totalmorLtrbracket.ToString() + "</span>]";
                                ShowNetSupply = (totalmorLtr - totalmorLtrbracket).ToString();
                            }
                            else
                            {
                                totalmorpbracket += Convert.ToInt32(ABC);
                                Showtotal = "<span style='color:green;'>" + totalmorp.ToString() + "</span>" + " [<span style='color:red;'>-" + totalmorpbracket.ToString() + "</span>]";
                                ShowNetSupply = (totalmorp - totalmorpbracket).ToString();
                            }
                            

                         

                        }
                        if (j == totalcolumn - 3)
                        {
                            sb.Append("<td  style='display:none;'>" + Showtotal + "</td>");
                            bindtr += "<td style='display:none;'>" + totalmorpbracket.ToString() + "</td>";
                            bindtr1 += "<td style='display:none;'>" + ShowNetSupply.ToString() + "</td>";
                        }
                        else if (j == totalcolumn - 1)
                        {
                            sb.Append("<td  style='display:none;'>" + Showtotal + "</td>");
                            bindtr += "<td style='display:none;'>" + totalmorLtrbracket.ToString() + "</td>";
                            bindtr1 += "<td style='display:none;'>" + ShowNetSupply.ToString() + "</td>";
                        }
                        else
                        {
                            sb.Append("<td>" + Showtotal + "</td>");
                            bindtr += "<td style='color:red;'>" + totalmorpbracket.ToString() + "</td>";
                            bindtr1 += "<td style='color:green;'>" + ShowNetSupply.ToString() + "</td>";
                        }
                    }
                }
                sb.Append("</tr>");
                sb.Append("<tr><td></td><td>Total Sales Return</td>" + bindtr + "</tr>");
                sb.Append("<tr><td></td><td>Total Net Supply</td>" + bindtr1 + "</tr>");
                sb.Append("</table><br/><br/><br/> ");
                divStringBuilder.InnerHtml = sb.ToString();
                StringBuilder sb1 = new StringBuilder();
                DateTime date31 = DateTime.ParseExact(txtSupplyDate.Text, "dd/MM/yyyy", culture);
                string s1 = date31.DayOfWeek.ToString();
                sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                sb1.Append("<tr>");
                sb1.Append("<td>Shift :-" + ddlShift.SelectedItem.Text + "</td>");
                sb1.Append("<td></td>");
                sb1.Append("<td class='text-center'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sb1.Append("<td class='text-right'>Date  :-" + txtSupplyDate.Text + "</td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td></td>");
                sb1.Append("<td></td>");
                sb1.Append("<td class='text-center'><b>DAY: " + s1 + "(" + objdb.GetMilkCategoryName() + " SALE)<b></td>");
                sb1.Append("<td></td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                Print.InnerHtml = sb1.ToString();
                Print.InnerHtml += sb.ToString();
                dt.Dispose();
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 9 ", ex.Message.ToString());
        }
    }
    protected void BindStringbuilderDistWise()
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            if (ViewState["RoutOrDisOrInstWiseTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["RoutOrDisOrInstWiseTable"];
                int totalrow = dt.Rows.Count;
                int totalcolumn = dt.Columns.Count;

                sb.Append("<table class='table table-striped table-bordered'><tr>");
                sb.Append("<th>S.No.</th>");
                for (int j = 0; j < totalcolumn; j++)
                {
                    if (j == 0 || j == (totalcolumn - 3) || j == (totalcolumn - 1))
                    {
                        sb.Append("<th style='display:none;'>" + dt.Columns[j].ColumnName.ToString() + "</th>");
                    }
                    else
                    {
                        sb.Append("<th>" + dt.Columns[j].ColumnName.ToString() + "</th>");
                    }
                }
                sb.Append("</tr>");
                for (int i = 0; i < totalrow; i++)
                {
                    sb.Append("<tr><td>" + (i + 1) + "</td>");
                    for (int j = 0; j < totalcolumn; j++)
                    {
                        if (j == 0 || j == (totalcolumn - 3) || j == (totalcolumn - 1))
                        {
                            sb.Append("<td style='display:none;'>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + "</td>");
                        }
                        else if (j == (totalcolumn - 4))
                        {
                            Int32 checkvalue = Convert.ToInt32(dt.Rows[i][dt.Columns[j + 1].ColumnName.ToString()].ToString());

                            if (checkvalue > 0)
                            {
                                sb.Append("<td>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + " [-<span style='color:red'>" + checkvalue.ToString() + "</span>] </td>");
                            }
                            else
                            {
                                sb.Append("<td>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + " [" + checkvalue.ToString() + "] </td>");
                            }

                        }
                        else if (j == (totalcolumn - 2))
                        {
                            decimal checkvalue1 = Convert.ToDecimal(dt.Rows[i][dt.Columns[j + 1].ColumnName.ToString()].ToString());

                            if (checkvalue1 > 0)
                            {
                                sb.Append("<td>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + " [-<span style='color:red'>" + checkvalue1.ToString() + "</span>] </td>");
                            }
                            else
                            {
                                sb.Append("<td>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + " [" + checkvalue1.ToString() + "] </td>");
                            }

                        }
                        else
                        {
                            sb.Append("<td>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + "</td>");
                        }
                    }
                    sb.Append("</tr>");

                }
                string bindtr = "", bindtr1 = "";
                sb.Append("<tr><td></td><td>Total</td>");
                for (int j = 0; j < totalcolumn; j++)
                {
                    int totalmorp = 0;
                    int totalmorpbracket = 0;
                    decimal totalmorLtr = 0;
                    decimal totalmorLtrbracket = 0;
                    string Showtotal = "0";
                    string ShowNetSupply = "0";
                    if (dt.Columns[j].ColumnName.ToString() != "DistributorId" && dt.Columns[j].ColumnName.ToString() != "Distributor Name")
                    {
                        for (int i = 0; i < totalrow; i++)
                        {
                            string colm = "";
                            if (dt.Columns[j].ColumnName.ToString() == "Total Supply(In Pkt)")
                            {
                                colm = dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString() + " [-" + dt.Rows[i][dt.Columns[j + 1].ColumnName.ToString()].ToString().ToString() + "]";
                            }
                            else if (dt.Columns[j].ColumnName.ToString() == "Total Sales Return(In Pkt)")
                            {
                                colm = dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString() + " [-" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString() + "]";
                            }
                            else if (dt.Columns[j].ColumnName.ToString() == "Total Supply(In Ltr)")
                            {
                                colm = dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString() + " [-" + dt.Rows[i][dt.Columns[j + 1].ColumnName.ToString()].ToString().ToString() + "]";
                            }
                            else if (dt.Columns[j].ColumnName.ToString() == "Total Sales Return(In Ltr)")
                            {
                                colm = dt.Rows[i][dt.Columns[j - 1].ColumnName.ToString()].ToString().ToString() + " [-" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString() + "]";
                            }
                            else
                            {
                                colm = dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString();
                            }

                            string[] s = colm.Split(' ');
                            string[] sa1;
                            string ABC = "0";

                            if (dt.Columns[j].ColumnName.ToString() == "Total Supply(In Ltr)" || dt.Columns[j].ColumnName.ToString() == "Total Sales Return(In Ltr)")
                            {
                                totalmorLtr += Convert.ToDecimal(s[0]);
                            }
                            else
                            {
                                totalmorp += Convert.ToInt32(s[0]);
                            }
                            
                            if (s[1].Contains("-"))
                            {
                                string[] sa = colm.Split('-');
                                sa1 = sa[1].Split(']');
                                ABC = sa1[0];
                            }
                            else
                            {
                                ABC = "0";
                            }


                            if (dt.Columns[j].ColumnName.ToString() == "Total Supply(In Ltr)" || dt.Columns[j].ColumnName.ToString() == "Total Sales Return(In Ltr)")
                            {
                                totalmorLtrbracket += Convert.ToDecimal(ABC);
                                Showtotal = "<span style='color:green;'>" + totalmorLtr.ToString() + "</span>" + " [<span style='color:red;'>-" + totalmorLtrbracket.ToString() + "</span>]";
                                ShowNetSupply = (totalmorLtr - totalmorLtrbracket).ToString();
                            }
                            else
                            {
                                totalmorpbracket += Convert.ToInt32(ABC);
                                Showtotal = "<span style='color:green;'>" + totalmorp.ToString() + "</span>" + " [<span style='color:red;'>-" + totalmorpbracket.ToString() + "</span>]";
                                ShowNetSupply = (totalmorp - totalmorpbracket).ToString();
                            }

                        }
                        if (j == totalcolumn - 3)
                        {
                            sb.Append("<td  style='display:none;'>" + Showtotal + "</td>");
                            bindtr += "<td style='display:none;'>" + totalmorpbracket.ToString() + "</td>";
                            bindtr1 += "<td style='display:none;'>" + ShowNetSupply.ToString() + "</td>";
                        }
                        else if (j == totalcolumn - 1)
                        {
                            sb.Append("<td  style='display:none;'>" + Showtotal + "</td>");
                            bindtr += "<td style='display:none;'>" + totalmorLtrbracket.ToString() + "</td>";
                            bindtr1 += "<td style='display:none;'>" + ShowNetSupply.ToString() + "</td>";
                        }
                        else
                        {
                            sb.Append("<td>" + Showtotal + "</td>");
                            bindtr += "<td style='color:red;'>" + totalmorpbracket.ToString() + "</td>";
                            bindtr1 += "<td style='color:green;'>" + ShowNetSupply.ToString() + "</td>";
                        }
                    }
                }
                sb.Append("</tr>");
                sb.Append("<tr><td></td><td>Total Sales Return</td>" + bindtr + "</tr>");
                sb.Append("<tr><td></td><td>Total Net Supply</td>" + bindtr1 + "</tr>");
                sb.Append("</table><br/><br/><br/> ");
                divStringBuilder.InnerHtml = sb.ToString();
                StringBuilder sb1 = new StringBuilder();
                DateTime date31 = DateTime.ParseExact(txtSupplyDate.Text, "dd/MM/yyyy", culture);
                string s1 = date31.DayOfWeek.ToString();
                sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                sb1.Append("<tr>");
                sb1.Append("<td>Shift :-" + ddlShift.SelectedItem.Text + "</td>");
                sb1.Append("<td></td>");
                sb1.Append("<td class='text-center'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sb1.Append("<td class='text-right'>Date  :-" + txtSupplyDate.Text + "</td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td></td>");
                sb1.Append("<td></td>");
                sb1.Append("<td class='text-center'><b>DAY: " + s1 + "(" + objdb.GetMilkCategoryName() + " SALE)<b></td>");
                sb1.Append("<td></td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                Print.InnerHtml = sb1.ToString();
                Print.InnerHtml += sb.ToString();
                dt.Dispose();
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    protected void BindStringbuilderInstWise()
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            if (ViewState["RoutOrDisOrInstWiseTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["RoutOrDisOrInstWiseTable"];
                int totalrow = dt.Rows.Count;
                int totalcolumn = dt.Columns.Count;

                sb.Append("<table class='table table-striped table-bordered'><tr>");
                sb.Append("<th>S.No.</th>");
                for (int j = 0; j < totalcolumn; j++)
                {
                    if (j == 0 || j == (totalcolumn - 3) || j == (totalcolumn - 1))
                    {
                        sb.Append("<th style='display:none;'>" + dt.Columns[j].ColumnName.ToString() + "</th>");
                    }
                    else
                    {
                        sb.Append("<th>" + dt.Columns[j].ColumnName.ToString() + "</th>");
                    }
                }
                sb.Append("</tr>");
                for (int i = 0; i < totalrow; i++)
                {
                    sb.Append("<tr><td>" + (i + 1) + "</td>");
                    for (int j = 0; j < totalcolumn; j++)
                    {
                        if (j == 0 || j == (totalcolumn - 3) || j == (totalcolumn - 1))
                        {
                            sb.Append("<td style='display:none;'>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + "</td>");
                        }
                        else if (j == (totalcolumn - 4))
                        {
                            Int32 checkvalue = Convert.ToInt32(dt.Rows[i][dt.Columns[j + 1].ColumnName.ToString()].ToString());

                            if (checkvalue > 0)
                            {
                                sb.Append("<td>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + " [-<span style='color:red'>" + checkvalue.ToString() + "</span>] </td>");
                            }
                            else
                            {
                                sb.Append("<td>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + " [" + checkvalue.ToString() + "] </td>");
                            }

                        }
                        else if (j == (totalcolumn - 2))
                        {
                            decimal checkvalue1 = Convert.ToDecimal(dt.Rows[i][dt.Columns[j + 1].ColumnName.ToString()].ToString());

                            if (checkvalue1 > 0)
                            {
                                sb.Append("<td>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + " [-<span style='color:red'>" + checkvalue1.ToString() + "</span>] </td>");
                            }
                            else
                            {
                                sb.Append("<td>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + " [" + checkvalue1.ToString() + "] </td>");
                            }

                        }
                        else
                        {
                            sb.Append("<td>" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString() + "</td>");
                        }
                    }
                    sb.Append("</tr>");

                }
                string bindtr = "", bindtr1 = "";
                sb.Append("<tr><td></td><td>Total</td>");
                for (int j = 0; j < totalcolumn; j++)
                {
                    int totalmorp = 0;
                    int totalmorpbracket = 0;
                    decimal totalmorLtr = 0;
                    decimal totalmorLtrbracket = 0;
                    string Showtotal = "0";
                    string ShowNetSupply = "0";
                    if (dt.Columns[j].ColumnName.ToString() != "BoothId" && dt.Columns[j].ColumnName.ToString() != "Institution Name")
                    {
                        for (int i = 0; i < totalrow; i++)
                        {
                            string colm = "";
                            if (dt.Columns[j].ColumnName.ToString() == "Total Supply(In Pkt)")
                            {
                                colm = dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString() + " [-" + dt.Rows[i][dt.Columns[j + 1].ColumnName.ToString()].ToString().ToString() + "]";
                            }
                            else if (dt.Columns[j].ColumnName.ToString() == "Total Sales Return(In Pkt)")
                            {
                                colm = dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString() + " [-" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString() + "]";
                            }
                            else if (dt.Columns[j].ColumnName.ToString() == "Total Supply(In Ltr)")
                            {
                                colm = dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString() + " [-" + dt.Rows[i][dt.Columns[j + 1].ColumnName.ToString()].ToString().ToString() + "]";
                            }
                            else if (dt.Columns[j].ColumnName.ToString() == "Total Sales Return(In Ltr)")
                            {
                                colm = dt.Rows[i][dt.Columns[j - 1].ColumnName.ToString()].ToString().ToString() + " [-" + dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString() + "]";
                            }
                            else
                            {
                                colm = dt.Rows[i][dt.Columns[j].ColumnName.ToString()].ToString().ToString();
                            }

                            string[] s = colm.Split(' ');
                            string[] sa1;
                            string ABC = "0";
                            if (dt.Columns[j].ColumnName.ToString() == "Total Supply(In Ltr)" || dt.Columns[j].ColumnName.ToString() == "Total Sales Return(In Ltr)")
                            {
                                totalmorLtr += Convert.ToDecimal(s[0]);
                            }
                            else
                            {
                                totalmorp += Convert.ToInt32(s[0]);
                            }
                            if (s[1].Contains("-"))
                            {
                                string[] sa = colm.Split('-');
                                sa1 = sa[1].Split(']');
                                ABC = sa1[0];
                            }
                            else
                            {
                                ABC = "0";
                            }


                            if (dt.Columns[j].ColumnName.ToString() == "Total Supply(In Ltr)" || dt.Columns[j].ColumnName.ToString() == "Total Sales Return(In Ltr)")
                            {
                                totalmorLtrbracket += Convert.ToDecimal(ABC);
                                Showtotal = "<span style='color:green;'>" + totalmorLtr.ToString() + "</span>" + " [<span style='color:red;'>-" + totalmorLtrbracket.ToString() + "</span>]";
                                ShowNetSupply = (totalmorLtr - totalmorLtrbracket).ToString();
                            }
                            else
                            {
                                totalmorpbracket += Convert.ToInt32(ABC);
                                Showtotal = "<span style='color:green;'>" + totalmorp.ToString() + "</span>" + " [<span style='color:red;'>-" + totalmorpbracket.ToString() + "</span>]";
                                ShowNetSupply = (totalmorp - totalmorpbracket).ToString();
                            }

                        }
                        if (j == totalcolumn - 3)
                        {
                            sb.Append("<td  style='display:none;'>" + Showtotal + "</td>");
                            bindtr += "<td style='display:none;'>" + totalmorpbracket.ToString() + "</td>";
                            bindtr1 += "<td style='display:none;'>" + ShowNetSupply.ToString() + "</td>";
                        }
                        else if (j == totalcolumn - 1)
                        {
                            sb.Append("<td  style='display:none;'>" + Showtotal + "</td>");
                            bindtr += "<td style='display:none;'>" + totalmorLtrbracket.ToString() + "</td>";
                            bindtr1 += "<td style='display:none;'>" + ShowNetSupply.ToString() + "</td>";
                        }
                        else
                        {
                            sb.Append("<td>" + Showtotal + "</td>");
                            bindtr += "<td style='color:red;'>" + totalmorpbracket.ToString() + "</td>";
                            bindtr1 += "<td style='color:green;'>" + ShowNetSupply.ToString() + "</td>";
                        }
                    }
                }
                sb.Append("</tr>");
                sb.Append("<tr><td></td><td>Total Sales Return</td>" + bindtr + "</tr>");
                sb.Append("<tr><td></td><td>Total Net Supply</td>" + bindtr1 + "</tr>");
                sb.Append("</table><br/><br/><br/> ");
                divStringBuilder.InnerHtml = sb.ToString();
                StringBuilder sb1 = new StringBuilder();
                DateTime date31 = DateTime.ParseExact(txtSupplyDate.Text, "dd/MM/yyyy", culture);
                string s1 = date31.DayOfWeek.ToString();
                sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                sb1.Append("<tr>");
                sb1.Append("<td>Shift :-" + ddlShift.SelectedItem.Text + "</td>");
                sb1.Append("<td></td>");
                sb1.Append("<td class='text-center'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sb1.Append("<td class='text-right'>Date  :-" + txtSupplyDate.Text + "</td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td></td>");
                sb1.Append("<td></td>");
                sb1.Append("<td class='text-center'><b>DAY: " + s1 + "(" + objdb.GetMilkCategoryName() + " SALE)<b></td>");
                sb1.Append("<td></td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                Print.InnerHtml = sb1.ToString();
                Print.InnerHtml += sb.ToString();
                dt.Dispose();
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 11 ", ex.Message.ToString());
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
    }
    protected void btnViewSalesReturnDetails_Click(object sender, EventArgs e)
    {
        DataTable dt1 = new DataTable();
        try
        {
            DateTime sdate3 = DateTime.ParseExact(txtSupplyDate.Text, "dd/MM/yyyy", culture);
            string sdate = sdate3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            modalRDIName.InnerHtml = pnllegand.InnerHtml;
            modaldate.InnerHtml = txtSupplyDate.Text.Trim();
            modalshift.InnerHtml = ddlShift.SelectedItem.Text;
            modalcategory.InnerHtml = objdb.GetMilkCategoryName();

            if (rblReportType.SelectedValue == "1")
            {

                ds4 = objdb.ByProcedure("USP_Trn_MilkOrProductReturnSupply",
                         new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Delivery_Date","AreaId" },
                           new string[] { "7", objdb.Office_ID(), ddlShift.SelectedValue, objdb.GetMilkCatId(), sdate,ddlLocation.SelectedValue }, "dataset");

                if (ds4.Tables[0].Rows.Count != 0)
                {
                    dt1 = ds4.Tables[0];
                    foreach (DataRow drow in dt1.Rows)
                    {
                        foreach (DataColumn column in dt1.Columns)
                        {
                            if (column.ToString() != "RouteId" && column.ToString() != "Route" && column.ToString() != "Total Supply(In Pkt)" && column.ToString() != "Total Sales Return(In Pkt)" && column.ToString() != "Total Supply(In Ltr)" && column.ToString() != "Total Sales Return(In Ltr)")
                            {

                                if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                                {
                                    drow[column] = 0 + " [" + "0" + "]";
                                }

                            }

                        }
                    }
                    ViewState["RoutOrDisOrInstWiseTable"] = dt1;
                    BindStringbuilderRouteWise();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    ViewState["RoutOrDisOrInstWiseTable"] = "";
                    if (dt1 != null) { dt1.Dispose(); }
                }
            }
            else if (rblReportType.SelectedValue == "2")
            {

                ds4 = objdb.ByProcedure("USP_Trn_MilkOrProductReturnSupply",
                         new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Delivery_Date","AreaId" },
                           new string[] { "8", objdb.Office_ID(), ddlShift.SelectedValue,objdb.GetMilkCatId(), sdate,ddlLocation.SelectedValue }, "dataset");

                if (ds4.Tables[0].Rows.Count != 0)
                {
                    dt1 = ds4.Tables[0];
                    foreach (DataRow drow in dt1.Rows)
                    {
                        foreach (DataColumn column in dt1.Columns)
                        {
                            if (column.ToString() != "DistributorId" && column.ToString() != "Distributor Name" && column.ToString() != "Total Supply(In Pkt)" && column.ToString() != "Total Sales Return(In Pkt)" && column.ToString() != "Total Supply(In Ltr)" && column.ToString() != "Total Sales Return(In Ltr)") ;
                            {

                                if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                                {
                                    drow[column] = 0 + " [" + "0" + "]";
                                }

                            }

                        }
                    }
                    ViewState["RoutOrDisOrInstWiseTable"] = dt1;
                    BindStringbuilderDistWise();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    ViewState["RoutOrDisOrInstWiseTable"] = "";
                    if (dt1 != null) { dt1.Dispose(); }
                }
            }
            else if (rblReportType.SelectedValue == "3")
            {

                ds4 = objdb.ByProcedure("USP_Trn_MilkOrProductReturnSupply",
                         new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Delivery_Date","AreaId" },
                           new string[] { "9", objdb.Office_ID(), ddlShift.SelectedValue, objdb.GetMilkCatId(), sdate,ddlLocation.SelectedValue }, "dataset");

                if (ds4.Tables[0].Rows.Count != 0)
                {
                    dt1 = ds4.Tables[0];
                    foreach (DataRow drow in dt1.Rows)
                    {
                        foreach (DataColumn column in dt1.Columns)
                        {
                            if (column.ToString() != "S.No." && column.ToString() != "BoothId" && column.ToString() != "Institution Name" && column.ToString() != "Total Supply(In Pkt)" && column.ToString() != "Total Sales Return(In Pkt)" && column.ToString() != "Total Supply(In Ltr)" && column.ToString() != "Total Sales Return(In Ltr)");
                            {

                                if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                                {
                                    drow[column] = 0 + " [" + "0" + "]";
                                }

                            }

                        }
                    }
                    ViewState["RoutOrDisOrInstWiseTable"] = dt1;
                    BindStringbuilderInstWise();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    ViewState["RoutOrDisOrInstWiseTable"] = "";
                    if (dt1 != null) { dt1.Dispose(); }
                }
            }
            else
            {
                lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record found.");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 12 ", ex.Message.ToString());
        }
        finally
        {
            if (ds4 != null) { ds4.Dispose(); }
            if (dt1 != null) { dt1.Dispose(); }
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
        txtSupplyDate.Text = string.Empty;
        ddlShift.SelectedIndex = 0;
       // ddlItemCategory.SelectedIndex = 0;
        rblReportType.ClearSelection();
        pnlData.Visible = false;
        pnlrouteOrDistOrInstwisedata.Visible = false;
        ddlLocation.SelectedIndex = 0;
    }
    protected void btnPrintRoutWise_Click(object sender, EventArgs e)
    {

        Print.InnerHtml = ViewState["Sb"].ToString();
        Print1.InnerHtml = "";
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {

    }
}