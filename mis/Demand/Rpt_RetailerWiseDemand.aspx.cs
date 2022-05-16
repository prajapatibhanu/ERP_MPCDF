using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Demand_Rpt_RetailerWiseDemand : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds2,ds3, ds6 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    int sum22 = 0, sumFF = 0;
    decimal SumLL = 0;
    int i_Qty = 0, i_NaQty = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
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
            ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void GetOfficeDetails()
    {
        try
        {
            ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                ViewState["Office_Name"] = ds2.Tables[0].Rows[0]["Office_Name"].ToString();
                ViewState["Office_GST"] = ds2.Tables[0].Rows[0]["Office_Gst"].ToString();
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

    private void GetRoute()
    {
        try
        {
            ddlRoute.DataTextField = "RName";
            ddlRoute.DataValueField = "RouteId";
            ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "AreaId", "ItemCat_id", "Office_ID" },
                   new string[] { "7", ddlLocation.SelectedValue, objdb.GetMilkCatId(), objdb.Office_ID() }, "dataset");
            ddlRoute.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }
    #endregion========================================================
    #region=========== init or changed even===========================



    #endregion===========================
    #region=========== click event for grdiview row bound event===========================

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetCompareDate();
        }
    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0")
        {
            lblMsg.Text = string.Empty;
            GetRoute();
        }
    }
    private void GetRetailerDetails()
    {
        lblMsg.Text = string.Empty;
        div_page_content.InnerHtml = "";
        string routeid = "", routename = "";
        int rr = 0;
        DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
        DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
        DateTime enddate = date4.AddDays(1);
        string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
        string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
        TimeSpan ts = (enddate - date3);
        int differenceInDays = ts.Days; // This is in int
        foreach (ListItem item in ddlRoute.Items)
        {
            if (item.Selected)
            {
               
                routeid = item.Value;
                routename = item.Text;
                
                
                ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandRouteOrDistwise",
                        new string[] { "flag", "Office_ID", "ItemCat_id", "FromDate", "ToDate", "RouteId" },
                          new string[] { "8", objdb.Office_ID(), objdb.GetMilkCatId(), fromdat, todat, routeid }, "dataset");
                if (ds6.Tables[0].Rows.Count > 0)
                {
                    btnPrint.Visible = true;
                    btnExcel.Visible = true;
                    ++rr;
                    DataTable dt = new DataTable();
                    dt = ds6.Tables[0];

                    DataTable dtcrate = new DataTable();// create dt for Crate total
                    DataRow drcrate;

                    dtcrate.Columns.Add("ItemName", typeof(string));
                    dtcrate.Columns.Add("CrateQty", typeof(int));
                    dtcrate.Columns.Add("CratePacketQty", typeof(String));
                    drcrate = dtcrate.NewRow();
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() != "tmp_BoothId" && column.ToString() != "Total Ltr" && column.ToString() != "BandOName" && column.ToString() != "Total Packet")
                        {

                            sum22 = dt.AsEnumerable().Sum(r => r.Field<int?>("" + column + "") ?? 0);

                            //  below code for crate calculation
                            ds3 = objdb.ByProcedure("USP_Mst_SetItemCarriageMode",
                                new string[] { "Flag", "ItemCat_id", "Itemname", "Office_ID", "EffectiveDate" },
                                new string[] { "7", objdb.GetMilkCatId(), column.ToString(), objdb.Office_ID(), fromdat }, "dataset");

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

                        }
                    }
                    ////////////////////

                    if (rr > 1)
                    {
                        div_page_content.InnerHtml += "<div style='margin-top:25px;' class='pagebreak'></div>";
                    }
                    StringBuilder sb1 = new StringBuilder();

                    sb1.Append("<table class='table'>");
                    int Count1 = ds6.Tables[0].Rows.Count;
                    int ColCount1 = ds6.Tables[0].Columns.Count;
                    sb1.Append("<thead>");
                    sb1.Append("<tr>");                   
                    sb1.Append("<td style='border: 1px solid #000000;padding: 2px 5px;text-align: center'  colspan='" + (ColCount1 + 1 ) + "'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                  
                    
                    sb1.Append("</tr>");
                    sb1.Append("<tr>");
                    sb1.Append("<td style='border: 1px solid #000000;padding: 2px 5px;' colspan='2'><b>" + routename + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;padding: 2px 5px;text-align: left;'colspan='" + (ColCount1) + "'><b>Date  :-" + txtFromDate.Text + " -" + txtToDate.Text + "</b></td>");

                    sb1.Append("</tr>");
                    sb1.Append("<tr>");
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>S.no</b></td>");
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Retailer/Institution Name</b></td>");
                    for (int j = 0; j < ColCount1; j++)
                    {
                        if (ds6.Tables[0].Columns[j].ToString() != "tmp_BoothId" && ds6.Tables[0].Columns[j].ToString() != "Total Ltr" && ds6.Tables[0].Columns[j].ToString() != "BandOName")
                        {
                            string ColName = ds6.Tables[0].Columns[j].ToString();
                            sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");
                        }

                    }
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Total Ltr</b></td>");
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Avg In Ltr</b></td>");
                    sb1.Append("</tr>");
                    sb1.Append("</thead>");
                    for (int i = 0; i < Count1; i++)
                    {
                        sb1.Append("<tr>");
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + (i + 1).ToString() + "</td>");
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + ds6.Tables[0].Rows[i]["BandOName"] + "</td>");
                        for (int K = 0; K < ColCount1; K++)
                        {
                            if (ds6.Tables[0].Columns[K].ToString() != "tmp_BoothId" && ds6.Tables[0].Columns[K].ToString() != "BandOName")
                            {
                                string ColName = ds6.Tables[0].Columns[K].ToString();
                                if (ColName == "Total Ltr")
                                {
                                    sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + Convert.ToDecimal(ds6.Tables[0].Rows[i][ColName]) + "</b></td>");
                                    sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + (differenceInDays == 0 ? Convert.ToDecimal(ds6.Tables[0].Rows[i][ColName]) : (Convert.ToDecimal(ds6.Tables[0].Rows[i][ColName]) / differenceInDays)).ToString("0.00") + "</b></td>");
                                }
                                else if(ColName == "Total Packet")
                                {
                                    sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + (ds6.Tables[0].Rows[i][ColName].ToString() == "" ? "0" : ds6.Tables[0].Rows[i][ColName].ToString()) + "</b></td>");
                                }
                                else
                                {
                                    sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + (ds6.Tables[0].Rows[i][ColName].ToString() == "" ? "0" : ds6.Tables[0].Rows[i][ColName].ToString()) + "</td>");
                                }
                            }
                        }
                        sb1.Append("</tr>");
                       

                        //////////////////////////
                        //////////////////////////////
                    }
                    sb1.Append("<tr>");
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'></td>");
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Total</b></td>");
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() != "Total Ltr" && column.ToString() != "tmp_BoothId" && column.ToString() != "BandOName")
                        {
                            sumFF = dt.AsEnumerable().Sum(r => r.Field<int?>("" + column + "") ?? 0);
                            sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + sumFF.ToString() + "</b></td>");

                        }
                        if (column.ToString() == "Total Ltr")
                        {
                            SumLL = dt.AsEnumerable().Sum(r => r.Field<decimal?>("" + column + "") ?? 0);
                            sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + SumLL.ToString() + "</b></td>");
                            sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + (differenceInDays == 0 ? SumLL : (SumLL / differenceInDays)).ToString("0.00") + "</b></td>");

                        }
                    }
                    sb1.Append("</tr>");
                    // for crate value
                    sb1.Append("<tr>");
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'></td>");
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>CRATE DETAILS	</b></td>");
                    int tmpcrateroutcount = dtcrate.Rows.Count;
                    for (int i = 0; i < tmpcrateroutcount; i++)
                    {
                        sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + dtcrate.Rows[i]["CrateQty"].ToString() + "</b></td>");


                    }
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'></td>");
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'></td>");
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'></td>");

                    sb1.Append("</tr>");
                    // end of crate value

                    // for crate EXTRA PKT(+/-)
                    sb1.Append("<tr>");
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'></td>");
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>EXTRA PKT(+/-)</b></td>");
                  
                    for (int i = 0; i < tmpcrateroutcount; i++)
                    {
                        sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + dtcrate.Rows[i]["CratePacketQty"].ToString() + "</b></td>");


                    }
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'></td>");
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'></td>");
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'></td>");
                    sb1.Append("</tr>");
                    // end of crate EXTRA PKT(+/-)
                    sb1.Append("</table>");
                    div_page_content.InnerHtml += sb1.ToString();

                    
                      //  div_page_content.InnerHtml += ViewState["CrateDetails"].ToString();
                   

                    if (dtcrate != null) { dtcrate.Dispose(); }

                }
                //if (rr == 1)
                //{
                //    div_page_content.InnerHtml += "<div style='margin-top:25px;' class='pagebreak'></div>";
                //    rr = 0;
                //}
                if (ds6 != null) { ds6.Dispose(); }
            }

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "Retailer Demand Rpt" + ddlLocation.SelectedItem.Text + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            div_page_content.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    private void GetCompareDate()
    {
        try
        {
            string myStringfromdat = txtFromDate.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtToDate.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                lblMsg.Text = string.Empty;
                GetRetailerDetails();
            }
            else
            {
                txtToDate.Text = string.Empty;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
        }
    }
    #endregion===========================
}