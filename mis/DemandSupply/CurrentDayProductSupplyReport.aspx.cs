using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;
using System.Data;

public partial class mis_DemandSupply_CurrentDayProductSupplyReport : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds2, ds3, ds4, ds5 = new DataSet();
    double sum1 = 0, pc = 0, tcsamt = 0, fa = 0;
    int sum11, sum22 = 0, sum33 = 0, sum44 = 0, tp = 0;
    int cellIndex = 7;
    int cellIndexboothOrg = 3;
    int i_Qty = 0, i_NaQty = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtDeliveryDate.Text = Date;
                txtDeliveryDate.Attributes.Add("readonly", "true");
                GetLocation();
                GetRoute();
                GetOfficeDetails();
                GetShift();
                txtDeliveryDate.Attributes.Add("readonly", "true");
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void GetOfficeDetails()
    {
        try
        {
            ds3 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {
                ViewState["Office_Name"] = ds3.Tables[0].Rows[0]["Office_Name"].ToString();
                ViewState["Office_ContactNo"] = ds3.Tables[0].Rows[0]["Office_ContactNo"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null)
            {
                ds3.Dispose();
            }
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
            ddlShift.Items.Insert(0, new ListItem("All", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
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
    private void GetRoute()
    {
        try
        {
            ddlRoute.Items.Clear();
            ds5 = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                 new string[] { "9", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetProductCatId() }, "dataset");
            if (ds5.Tables[0].Rows.Count > 0)
            {
                ddlRoute.DataTextField = "RName";
                ddlRoute.DataValueField = "RouteId";
                ddlRoute.DataSource = ds5.Tables[0];
                ddlRoute.DataBind();
                ddlRoute.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlRoute.Items.Insert(0, new ListItem("No Record Found", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetRoute();
    }
    private void CurrentDayProductSupply_Report()
    {
        try
        {
            lblMsg.Text = "";
            DateTime date3 = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
            string deliverydate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds2 = objdb.ByProcedure("USP_Trn_ProductDM",
                     new string[] { "flag", "Delivary_Date", "DelivaryShift_id", "ItemCat_id", "AreaId", "RouteId", "Office_ID" },
                       new string[] { "10", deliverydate, ddlShift.SelectedValue, objdb.GetProductCatId(), ddlLocation.SelectedValue, ddlRoute.SelectedValue, objdb.Office_ID() }, "dataset");

            if (ds2.Tables[0].Rows.Count != 0)
            {
                pnlData.Visible = true;
                DataTable dt = new DataTable();
                dt = ds2.Tables[0];

                foreach (DataRow drow in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() != "DMChallanNo" && column.ToString() != "DCode" && column.ToString() != "DName" && column.ToString() != "Total Packet" && column.ToString() != "Cost of Product" && column.ToString() != "TcsTax Amt" && column.ToString() != "Final Amt")
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
                DataTable dtcrate = new DataTable();// create dt for Crate total
                DataRow drcrate;

                dtcrate.Columns.Add("ItemName", typeof(string));
                dtcrate.Columns.Add("CrateQty", typeof(int));
                dtcrate.Columns.Add("CratePacketQty", typeof(String));
                drcrate = dtcrate.NewRow();
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() != "DMChallanNo" && column.ToString() != "DCode" && column.ToString() != "DName" && column.ToString() != "Total Packet" && column.ToString() != "Cost of Product" && column.ToString() != "TcsTax Amt" && column.ToString() != "Final Amt")
                    {

                        sum22 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView1.FooterRow.Cells[cellIndex].Text = sum22.ToString();
                        GridView1.FooterRow.Cells[cellIndex].Font.Bold = true;
                        cellIndex = cellIndex + 1;


                        if (dt.Rows.Count > 0)
                        {

                            //  below code for crate calculation
                            ds3 = objdb.ByProcedure("USP_Mst_SetItemCarriageMode",
                            new string[] { "Flag", "ItemCat_id", "Itemname", "Office_ID", "EffectiveDate" },
                            new string[] { "7", objdb.GetProductCatId(), column.ToString(), objdb.Office_ID(), deliverydate }, "dataset");

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

                if (dtcrate.Rows.Count > 0)
                {
                    ViewState["CrateDetails"] = "";
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
                        sbtable.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + dtcrate.Rows[i]["ItemName"].ToString() + "</b>");

                    }
                    sbtable.Append("<td style='text-align: center;border: 1px solid #000000 !important;text-align:center;width: 20px;'><b>TOTAL</b>");
                    sbtable.Append("</td>");
                    sbtable.Append("</tr>");

                    sbtable.Append("<tr>");
                    sbtable.Append("<td style='border: 1px solid #000000 !important;text-align:right;'> <b>CRATES</b>");
                    sbtable.Append("</td>");
                    for (int i = 0; i < Rowcount; i++)
                    {
                        sbtable.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + dtcrate.Rows[i]["CrateQty"].ToString() + "");


                    }
                    sbtable.Append("<td style='border: 1px solid #000000 !important;text-align:center;width: 20px;'>" + cratetotal.ToString());

                    sbtable.Append("</tr>");
                    sbtable.Append("</table>");
                    divtable.InnerHtml = sbtable.ToString();
                    ViewState["CrateDetails"] = sbtable.ToString();
                    if (dtcrate != null) { dtcrate.Dispose(); }
                    // end  of crate binding and
                }

                
                int rowcount = GridView1.FooterRow.Cells.Count - 7;  // previous default is : 4
                GridView1.FooterRow.Cells[rowcount].Visible = false;
                GridView1.FooterRow.Cells[rowcount + 1].Visible = false;
                GridView1.FooterRow.Cells[rowcount + 2].Visible = false;
                GridView1.FooterRow.Cells[rowcount + 3].Visible = false;
                GridView1.FooterRow.Cells[rowcount + 4].Visible = false;
                GridView1.FooterRow.Cells[rowcount + 5].Visible = false;
                GridView1.FooterRow.Cells[rowcount + 6].Visible = false;

                ////////////////Start Of Order Wise Print Code   ///////////////////////
                StringBuilder sb = new StringBuilder();
                string s = date3.DayOfWeek.ToString();
                sb.Append("<table style='width:100%; height:100%'>");
                sb.Append("<tr>");
                sb.Append("<td style='padding: 2px 5px;'><b>Shift :-" + objdb.GetShiftMorningName() + "</b></td>");
                sb.Append("<td style='padding: 2px 5px;'></td>");
                sb.Append("<td style='padding: 2px 5px;text-align: center;'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sb.Append("<td style='padding: 2px 5px;text-align: right;'><b>Date  :-" + txtDeliveryDate.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='padding: 2px 5px;'></td>");
                sb.Append("<td style='padding: 2px 5px;'></td>");
                sb.Append("<td style='padding: 2px 5px;text-align: center;'><b>DAY: " + s + "(" + objdb.GetProductCategoryName() + " SALE)<b></td>");
                sb.Append("<td style='padding: 2px 5px;'></td>");
                sb.Append("</tr>");

                sb.Append("</table>");
                sb.Append("<table class='table'>");
                int Count = ds2.Tables[0].Rows.Count;
                int ColCount = ds2.Tables[0].Columns.Count;
                sb.Append("<thead>");
                sb.Append("<td style='border: 1px solid #000000 !important;'><b>S.No</b></td>");
                sb.Append("<td style='border: 1px solid #000000 !important;'><b>Order No</b></td>");
                sb.Append("<td style='border: 1px solid #000000 !important;'><b>Party Code</b></td>");
                sb.Append("<td style='border: 1px solid #000000 !important;'><b>NAME</b></td>");
                sb.Append("<td style='border: 1px solid #000000 !important;'><b>Total Pkt</b></td>");
                sb.Append("<td style='border: 1px solid #000000 !important;'><b>Cost Of Product</b></td>");
                sb.Append("<td style='border: 1px solid #000000 !important;'><b>TcsTax Amt</b></td>");
                sb.Append("<td style='border: 1px solid #000000 !important;'><b>Final Amt</b></td>");
                for (int j = 0; j < ColCount; j++)
                {

                    if (ds2.Tables[0].Columns[j].ToString() != "DMChallanNo" && ds2.Tables[0].Columns[j].ToString() != "DCode" && ds2.Tables[0].Columns[j].ToString() != "DName" && ds2.Tables[0].Columns[j].ToString() != "Total Packet" && ds2.Tables[0].Columns[j].ToString() != "Cost of Product" && ds2.Tables[0].Columns[j].ToString() != "TcsTax Amt" && ds2.Tables[0].Columns[j].ToString() != "Final Amt")
                    {
                        string ColName = ds2.Tables[0].Columns[j].ToString();
                        sb.Append("<td style='border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");

                    }

                }

                sb.Append("</thead>");


                for (int i = 0; i < Count; i++)
                {

                    sb.Append("<tr>");
                    sb.Append("<td style='border: 1px solid #000000 !important;'>" + (i + 1).ToString() + "</td>");
                    sb.Append("<td style='border: 1px solid #000000 !important;'>" + ds2.Tables[0].Rows[i]["DMChallanNo"] + "</td>");
                    sb.Append("<td style='border: 1px solid #000000 !important;'>" + ds2.Tables[0].Rows[i]["DCode"] + "</td>");
                    sb.Append("<td style='border: 1px solid #000000 !important;'>" + ds2.Tables[0].Rows[i]["DName"] + "</td>");
                    sb.Append("<td style='border: 1px solid #000000 !important;'>" + ds2.Tables[0].Rows[i]["Total Packet"].ToString() + "</td>");
                    sb.Append("<td style='border: 1px solid #000000 !important;'>" + ds2.Tables[0].Rows[i]["Cost of Product"].ToString() + "</td>");
                    sb.Append("<td style='border: 1px solid #000000 !important;'>" + ds2.Tables[0].Rows[i]["TcsTax Amt"].ToString() + "</td>");
                    sb.Append("<td style='border: 1px solid #000000 !important;'>" + ds2.Tables[0].Rows[i]["Final Amt"].ToString() + "</td>");

                    for (int K = 0; K < ColCount; K++)
                    {
                        if (ds2.Tables[0].Columns[K].ToString() != "DMChallanNo" && ds2.Tables[0].Columns[K].ToString() != "DCode" && ds2.Tables[0].Columns[K].ToString() != "DName" && ds2.Tables[0].Columns[K].ToString() != "Total Packet" && ds2.Tables[0].Columns[K].ToString() != "Cost of Product" && ds2.Tables[0].Columns[K].ToString() != "TcsTax Amt" && ds2.Tables[0].Columns[K].ToString() != "Final Amt")
                        {
                            string ColName = ds2.Tables[0].Columns[K].ToString();
                            sb.Append("<td style='border: 1px solid #000000 !important;'>" + ds2.Tables[0].Rows[i][ColName].ToString() + "</td>");


                        }
                    }
                    sb.Append("</tr>");
                }
                sb.Append("<tr>");
                int ColumnCount = GridView1.FooterRow.Cells.Count - 7;
                sb.Append("<td style='border: 1px solid #000000 !important;'><b></b></td>");

                for (int i = 0; i < ColumnCount; i++)
                {
                    if (i == 0)
                    {
                        sb.Append("<td style='border: 1px solid #000000 !important;'></td>");
                    }
                    else if (i == 1)
                    {
                        sb.Append("<td style='border: 1px solid #000000 !important;'><b>Total Amt/Pkt</b></td>");
                    }
                    else if (i == 2)
                    {
                        sb.Append("<td style='border: 1px solid #000000 !important;'><b>" + tp + " </b></td>");
                    }
                    else if (i == 3)
                    {
                        sb.Append("<td style='border: 1px solid #000000 !important;'><b>" + pc + "  </b></td>");
                    }
                    else if (i == 4)
                    {
                        sb.Append("<td style='border: 1px solid #000000 !important;'><b>" + tcsamt + "  </b></td>");
                    }
                    else if (i == 5)
                    {
                        sb.Append("<td style='border: 1px solid #000000 !important;'><b>" + fa + "  </b></td>");
                    }
                    else
                    {
                        sb.Append("<td style='border: 1px solid #000000 !important;'><b>" + GridView1.FooterRow.Cells[i].Text + "</b></td>");
                    }

                }
                sb.Append("</tr>");
                sb.Append("</table>");

                Print.InnerHtml = sb.ToString();
                divtable.Visible = true;
                Print.InnerHtml += ViewState["CrateDetails"].ToString();

                btnPrintRoutWise.Visible = true;
                btnExportAll.Visible = true;
                tp = 0;
                pc = 0;
                tcsamt = 0;
                fa = 0;

                ////////////////End Of Distributor Wise Print Code   ///////////////////////
            }
            else
            {
                pnlData.Visible = false;
                divtable.Visible = false;
                btnPrintRoutWise.Visible = false;
                btnExportAll.Visible = false;
                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning!", "No Record Found.");

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Current DM ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int totalpkt = e.Row.Cells.Count - 4;
            int costofprod = e.Row.Cells.Count - 3;
            int tcxtax = e.Row.Cells.Count - 2;
            int finalamt = e.Row.Cells.Count - 1;

            Label lblTotalPkt = (e.Row.FindControl("lblTotalPkt") as Label);
            Label lblCostOfProduct = (e.Row.FindControl("lblCostOfProduct") as Label);
            Label lblTcsTaxAmt = (e.Row.FindControl("lblTcsTaxAmt") as Label);
            Label lblFinalAmt = (e.Row.FindControl("lblFinalAmt") as Label);

            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
           

            e.Row.Cells[totalpkt].Visible = false;
            e.Row.Cells[costofprod].Visible = false;
            e.Row.Cells[tcxtax].Visible = false;
            e.Row.Cells[finalamt].Visible = false;

            tp += Convert.ToInt32(lblTotalPkt.Text);
            pc += Convert.ToDouble(lblCostOfProduct.Text);
            tcsamt += Convert.ToDouble(lblTcsTaxAmt.Text);
            fa += Convert.ToDouble(lblFinalAmt.Text);
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            int maxheadercelltotalpkt = e.Row.Cells.Count - 4;
            int maxheadercellcostofprod = e.Row.Cells.Count - 3;
            int maxheadercelltcxtax = e.Row.Cells.Count - 2;
            int maxheadercellfinalamt = e.Row.Cells.Count - 1;

            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;

            e.Row.Cells[maxheadercelltotalpkt].Visible = false;
            e.Row.Cells[maxheadercellcostofprod].Visible = false;
            e.Row.Cells[maxheadercelltcxtax].Visible = false;
            e.Row.Cells[maxheadercellfinalamt].Visible = false;

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblFTotalPkt = (e.Row.FindControl("lblFTotalPkt") as Label);
            Label lblFCostOfProduct = (e.Row.FindControl("lblFCostOfProduct") as Label);
            Label lblFTcsTaxAmt = (e.Row.FindControl("lblFTcsTaxAmt") as Label);
            Label lblFFinalAmt = (e.Row.FindControl("lblFFinalAmt") as Label);

            lblFTotalPkt.Text = tp.ToString();
            lblFCostOfProduct.Text = pc.ToString("0.00");
            lblFTcsTaxAmt.Text = tcsamt.ToString("0.00");
            lblFFinalAmt.Text = fa.ToString();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            CurrentDayProductSupply_Report();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        pnlData.Visible = false;

        divtable.Visible = false;
        btnPrintRoutWise.Visible = false;
        btnExportAll.Visible = false;
        lblMsg.Text = string.Empty;
        ddlLocation.SelectedIndex = 0;
        ddlRoute.Items.Clear();
        ddlRoute.Items.Insert(0, new ListItem("All", "0"));
    }
    protected void btnPrintRoutWise_Click(object sender, EventArgs e)
    {

       
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

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "CurrenDaySupplyReportLocation-" + ddlLocation.SelectedItem.Text + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            Print.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
}