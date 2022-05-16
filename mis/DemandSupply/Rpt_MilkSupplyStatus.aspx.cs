using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_DemandSupply_Rpt_MilkSupplyStatus : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2, ds3, ds4, ds5 = new DataSet();
    double sum1, sum2 = 0;
    int sum11=0, sum44 = 0;
    int cellIndex = 2;
    int cellIndexboothOrg = 2;
    int i_Qty = 0, i_NaQty = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetLocation();
                GetShift();
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
            ds2 = objdb.ByProcedure("USP_Mst_ShiftMaster",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds2.Tables[0].Rows.Count != 0)
            {
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataSource = ds2.Tables[0];
                ddlShift.DataBind();
                ddlShift.Items.Insert(0, new ListItem("All", "0"));

            }
            else
            {
                ddlShift.Items.Insert(0, new ListItem("No Reocrd Found", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    private void GetSupplyRouteWise()
    {
        try
        {
            lblMsg.Text = "";
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            string FDate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            DateTime date4 = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", culture);
            string TDate = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply",
                     new string[] { "flag", "FromDate", "ToDate", "Office_ID", "Shift_id", "ItemCat_id", "AreaId" },
                       new string[] { "17", FDate, TDate, objdb.Office_ID(), ddlShift.SelectedValue, objdb.GetMilkCatId(), ddlLocation.SelectedValue }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                pnlData.Visible = true;
                DataTable dt = new DataTable();
                dt = ds1.Tables[0];

                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() != "RouteId" && column.ToString() != "Route" && column.ToString() != "Total Supply In Pkt" && column.ToString() != "Total Supply in Litre")
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
                    if (column.ToString() != "RouteId" && column.ToString() != "Route" && column.ToString() != "Total Supply in Litre")
                    {

                        sum11 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView1.FooterRow.Cells[cellIndex].Text = sum11.ToString();
                        GridView1.FooterRow.Cells[cellIndex].Font.Bold = true;
                        cellIndex = cellIndex + 1;
                    }
                }
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() == "Total Supply in Litre")
                        {

                            sum1 = Convert.ToDouble(dt.Compute("SUM([" + column + "])", string.Empty));

                            GridView1.FooterRow.Cells[cellIndex].Text = sum1.ToString("N2");
                            GridView1.FooterRow.Cells[cellIndex].Font.Bold = true;
                            cellIndex = cellIndex + 1;
                        }
                    }
               
               
               
                    int rowcount = GridView1.FooterRow.Cells.Count - 2;
                    GridView1.FooterRow.Cells[rowcount].Visible = false;
                    GridView1.FooterRow.Cells[rowcount + 1].Visible = false;
               
                ////////////////Start Of Route Wise Print Code   ///////////////////////
                StringBuilder sb = new StringBuilder();

                string s = date3.DayOfWeek.ToString();
                sb.Append("<table style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td style='padding: 2px 5px;'><b>Shift :-" + ddlShift.SelectedItem.Text + "</b></td>");
                sb.Append("<td style='padding: 2px 5px;'></td>");
                sb.Append("<td style='padding: 2px 5px;text-align: center;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                sb.Append("<td style='padding: 2px 5px;text-align: right;'><b>Date  :-" + DateTime.Now.ToString("dd/MM/yyyy") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='padding: 2px 5px;'></td>");
                sb.Append("<td style='padding: 2px 5px;'></td>");
                sb.Append("<td style='padding: 2px 5px;text-align: center;'><b>Period: " + txtFromDate.Text  + " - " + txttodate.Text + "(" + objdb.GetMilkCategoryName() + " SALE)</b></td>");
                sb.Append("<td style='padding: 2px 5px;'></td>");
                sb.Append("</tr>");

                sb.Append("</table>");
                sb.Append("<table class='table'>");
                int Count = ds1.Tables[0].Rows.Count;
                int ColCount = ds1.Tables[0].Columns.Count;
                sb.Append("<thead>");
                sb.Append("<td style='border: 1px solid #000000 !important;'><b>S.No</b></td>");
                sb.Append("<td style='border: 1px solid #000000 !important;'><b>NAME</b></td>");
                for (int j = 0; j < ColCount; j++)
                {
                    if (ds1.Tables[0].Columns[j].ToString() != "RouteId" && ds1.Tables[0].Columns[j].ToString() != "Route" && ds1.Tables[0].Columns[j].ToString() != "Total Supply In Pkt" && ds1.Tables[0].Columns[j].ToString() != "Total Supply in Litre")
                    {
                        string ColName = ds1.Tables[0].Columns[j].ToString();
                        sb.Append("<td style='border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");

                    }

                }
                sb.Append("<td style='border: 1px solid #000000 !important;'><b>Total Supply In Pkt </b></td>");
                sb.Append("<td style='border: 1px solid #000000 !important;'><b>Total Sale in LIT</b></td>");
                
                sb.Append("</thead>");

                
                for (int i = 0; i < Count; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td style='border: 1px solid #000000 !important;'>" + (i + 1).ToString() + "</td>");
                    sb.Append("<td style='border: 1px solid #000000 !important;'>" + ds1.Tables[0].Rows[i]["Route"] + "</td>");
                    for (int K = 0; K < ColCount; K++)
                    {
                        if (ds1.Tables[0].Columns[K].ToString() != "RouteId" && ds1.Tables[0].Columns[K].ToString() != "Route" && ds1.Tables[0].Columns[K].ToString() != "Total Supply In Pkt" && ds1.Tables[0].Columns[K].ToString() != "Total Supply in Litre")
                        {
                            string ColName = ds1.Tables[0].Columns[K].ToString();
                            sb.Append("<td style='border: 1px solid #000000 !important;'>" + ds1.Tables[0].Rows[i][ColName].ToString() + "</td>");


                        }

                    }
                    sb.Append("<td style='border: 1px solid #000000 !important;'>" + ds1.Tables[0].Rows[i]["Total Supply In Pkt"].ToString() + "</td>");
                    
                    
                        sb.Append("<td style='border: 1px solid #000000 !important;'>" + ds1.Tables[0].Rows[i]["Total Supply in Litre"].ToString() + "</td>");
                    
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

                ////////////////End Of Route Wise Print Code   ///////////////////////
            }
            else
            {
                pnlData.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Routewise Approved Supply ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }

    private void GetBoothandOrganizationDetails(string rid)
    {
        try
        {
            lblMsg.Text = "";
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            string FDat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            DateTime date4 = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", culture);
            string TDat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);



            ds4 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply",
                     new string[] { "flag", "FromDate", "ToDate", "Office_ID", "Shift_id", "ItemCat_id", "RouteId" },
                       new string[] { "18", FDat, TDat, objdb.Office_ID(), ddlShift.SelectedValue, objdb.GetMilkCatId(), rid }, "dataset");

            if (ds4.Tables[0].Rows.Count != 0)
            {

                DataTable dt1 = new DataTable();
                dt1 = ds4.Tables[0];

                foreach (DataRow drow in dt1.Rows)
                {
                    foreach (DataColumn column in dt1.Columns)
                    {
                        if (column.ToString() != "BoothId" && column.ToString() != "BandOName" && column.ToString() != "Total Supply In Pkt" && column.ToString() != "Total Supply In Litre")
                        {

                            if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                            {
                                drow[column] = 0;
                            }

                        }

                    }
                }
                GridView4.DataSource = dt1;
                GridView4.DataBind();

                GridView4.FooterRow.Cells[1].Text = "Total";
                GridView4.FooterRow.Cells[1].Font.Bold = true;




                DataTable dtcrate = new DataTable();// create dt for Crate total
                DataRow drcrate;

                dtcrate.Columns.Add("ItemName", typeof(string));
                dtcrate.Columns.Add("CrateQty", typeof(int));
                dtcrate.Columns.Add("CratePacketQty", typeof(String));
                drcrate = dtcrate.NewRow();



                foreach (DataColumn column in dt1.Columns)
                {
                    if (column.ToString() != "BoothId" && column.ToString() != "BandOName" && column.ToString() != "Total Supply in Litre")
                    {

                        sum44 = Convert.ToInt32(dt1.Compute("SUM([" + column + "])", string.Empty));

                        GridView4.FooterRow.Cells[cellIndexboothOrg].Text = sum44.ToString();
                        GridView4.FooterRow.Cells[cellIndexboothOrg].Font.Bold = true;
                        cellIndexboothOrg = cellIndexboothOrg + 1;
                        if (dt1.Rows.Count > 0 &&  column.ToString() != "Total Supply In Pkt")
                        {
                            //  below code for crate calculation
                            ds3 = objdb.ByProcedure("USP_Mst_SetItemCarriageMode",
                            new string[] { "Flag", "ItemCat_id", "Itemname", "Office_ID", "EffectiveDate" },
                            new string[] { "7", objdb.GetMilkCatId(), column.ToString(), objdb.Office_ID(), FDat }, "dataset");

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
                                Actualcrate = sum44 / i_Qty;
                                remenderCrate = sum44 % i_Qty;

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
                foreach (DataColumn column in dt1.Columns)
                {
                    if (column.ToString() == "Total Supply in Litre")
                    {

                        sum2 = Convert.ToDouble(dt1.Compute("SUM([" + column + "])", string.Empty));

                        GridView4.FooterRow.Cells[cellIndexboothOrg].Text = sum2.ToString("0.00");
                        GridView4.FooterRow.Cells[cellIndexboothOrg].Font.Bold = true;
                        cellIndexboothOrg = cellIndexboothOrg + 1;
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
                sbtable.Append("<td style='border: 1px solid #000000 !important;text-align:center;'><b>TOTAL</b>");
                sbtable.Append("</td>");
                sbtable.Append("</tr>");

                sbtable.Append("<tr>");
                sbtable.Append("<td style='border: 1px solid #000000 !important;text-align:right;'> <b>CRATE DETAILS</b>");
                sbtable.Append("</td>");
                for (int i = 0; i < Rowcount; i++)
                {
                    sbtable.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + dtcrate.Rows[i]["CrateQty"].ToString() + "");


                }
                sbtable.Append("<td style='border: 1px solid #000000 !important;text-align:center;'>" + cratetotal.ToString());

                sbtable.Append("</tr>");
                sbtable.Append("<tr>");
                sbtable.Append("<td style='text-align:right;border: 1px solid #000000 !important;'><b> EXTRA PKT(+/-)</b>");
                sbtable.Append("</td>");

                for (int i = 0; i < Rowcount; i++)
                {
                    sbtable.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + dtcrate.Rows[i]["CratePacketQty"].ToString() + "");


                }
                sbtable.Append("<td style='text-align:center;border: 1px solid #000000 !important;'>");
                sbtable.Append("</tr>");
                sbtable.Append("</table>");
                divtable.InnerHtml = sbtable.ToString();
                ViewState["CrateDetails"] = sbtable.ToString();
                if (dtcrate != null) { dtcrate.Dispose(); }
                // end  of crate binding and




                int rowcount1 = GridView4.FooterRow.Cells.Count - 2;
                GridView4.FooterRow.Cells[rowcount1].Visible = false;
                GridView4.FooterRow.Cells[rowcount1 + 1].Visible = false;
                dt1.Dispose();



                StringBuilder sb1 = new StringBuilder();

               
                sb1.Append("<table style='width:100%; height:100%'>");
                sb1.Append("<tr>");
                sb1.Append("<td style='padding: 2px 5px;'><b>Shift :-" + ddlShift.SelectedItem.Text + "</b></td>");
                sb1.Append("<td style='padding: 2px 5px;'></td>");
                sb1.Append("<td style='padding: 2px 5px;text-align: center;'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sb1.Append("<td style='padding: 2px 5px;text-align: right;'><b>Date  :-" + DateTime.Now.ToString("dd/MM/yyyy") + "</b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='padding: 2px 5px;'><b>" + modalRootOrDistName.InnerHtml + "</b></td>");
                sb1.Append("<td style='padding: 2px 5px;'></td>");
                sb1.Append("<td style='padding: 2px 5px;text-align: center;'><b>Period:" + txtFromDate.Text + " - " + txttodate.Text + "(" + objdb.GetMilkCategoryName() + " SALE)</b></td>");
                //   sb1.Append("<td class='text-right'>Vehicle No:" + modalVehicleNo.InnerText + "</td>");
                sb1.Append("</tr>");

                sb1.Append("</table>");
                sb1.Append("<table class='table'>");
                int Count1 = ds4.Tables[0].Rows.Count;
                int ColCount1 = ds4.Tables[0].Columns.Count;
                sb1.Append("<thead>");
                sb1.Append("<td style='border: 1px solid #000000 !important;'><b>S.no</b></td>");
                sb1.Append("<td style='border: 1px solid #000000 !important;'><b>Retailer/Institution Name</b></td>");
                for (int j = 0; j < ColCount1; j++)
                {
                    if (ds4.Tables[0].Columns[j].ToString() != "BoothId" && ds4.Tables[0].Columns[j].ToString() != "BandOName")
                    {
                        string ColName = ds4.Tables[0].Columns[j].ToString();
                        sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");

                    }

                }
                sb1.Append("</thead>");



                for (int i = 0; i < Count1; i++)
                {

                    sb1.Append("<tr>");
                    sb1.Append("<td style='border: 1px solid #000000 !important;'>" + (i + 1).ToString() + "</td>");
                    sb1.Append("<td style='border: 1px solid #000000 !important;'>" + ds4.Tables[0].Rows[i]["BandOName"] + "</td>");
                    for (int K = 0; K < ColCount1; K++)
                    {
                        if (ds4.Tables[0].Columns[K].ToString() != "BoothId" && ds4.Tables[0].Columns[K].ToString() != "BandOName")
                        {
                            string ColName = ds4.Tables[0].Columns[K].ToString();
                            sb1.Append("<td style='border: 1px solid #000000 !important;'>" + ds4.Tables[0].Rows[i][ColName].ToString() + "</td>");


                        }

                    }
                    sb1.Append("</tr>");
                }
                sb1.Append("<tr>");
                int ColumnCount = GridView4.FooterRow.Cells.Count - 2;
                for (int i = 0; i < ColumnCount; i++)
                {
                    if (i == 2)
                    {
                        sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + GridView4.FooterRow.Cells[i].Text + "</b></td>");
                    }
                    else
                    {
                        sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + GridView4.FooterRow.Cells[i].Text + "</b></td>");
                    }



                }
                sb1.Append("</tr>");
                sb1.Append("</table>");

                Print1.InnerHtml = sb1.ToString();
                divtable.Visible = true;
                Print1.InnerHtml += ViewState["CrateDetails"].ToString();

               
                btnConsRoutePrint.Visible = true;
                btnCExoprt.Visible = true;
                //////////////////////////
            }
            else
            {
                GridView4.DataSource = null;
                GridView4.DataBind();
                btnConsRoutePrint.Visible = false;
                btnCExoprt.Visible = false;
                divtable.InnerHtml = "";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Modal Popup ", ex.Message.ToString());
        }
        finally
        {
            if (ds4 != null) { ds4.Dispose(); }
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
    protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
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
                    modalRootOrDistName.InnerHtml = lnkbtnRoute.Text;
                    modaldate.InnerHtml = txtFromDate.Text + "-" + txttodate.Text;
                    modelShift.InnerHtml = ddlShift.SelectedItem.Text;
                    // GetVehicleNo(e.CommandArgument.ToString(), "0", "0");
                    GetBoothandOrganizationDetails(e.CommandArgument.ToString());

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 7 ", ex.Message.ToString());
        }
    }
    protected void btnPrintRoutWise_Click(object sender, EventArgs e)
    {

        Print.InnerHtml = ViewState["Sb"].ToString();
        // Print.InnerHtml += ViewState["CratePrint"].ToString();
        Print1.InnerHtml = "";
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
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetCompareDate();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = string.Empty;
        txttodate.Text = string.Empty;
        ddlShift.SelectedIndex = 0;
        ddlLocation.SelectedIndex = 0;
        pnlData.Visible = false;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "MilkSupplyReport" + DateTime.Now + ".xls");
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
    private void GetCompareDate()
    {
        try
        {
            string myStringfromdat = txtFromDate.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txttodate.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                lblMsg.Text = string.Empty;
                GetSupplyRouteWise();
            }
            else
            {
                txttodate.Text = string.Empty;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
        }
    }
}