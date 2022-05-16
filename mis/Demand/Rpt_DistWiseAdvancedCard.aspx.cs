using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;
using System.Data;

public partial class mis_Demand_Rpt_DistWiseAdvancedCard : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet  ds2, ds3,ds4 = new DataSet();
   
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetCategory();
                GetLocation();
                GetOfficeDetails();
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
    protected void GetCategory()
    {
        try
        {
            ds4 = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds4.Tables[0].Rows.Count != 0)
            {
                ddlItemCategory.DataTextField = "ItemCatName";
                ddlItemCategory.DataValueField = "ItemCat_id";
                ddlItemCategory.DataSource = ds4.Tables[0];
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds4 != null) { ds4.Dispose(); }
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
            ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
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
            ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                 new string[] { "7", objdb.Office_ID(), ddlLocation.SelectedValue, ddlItemCategory.SelectedValue }, "dataset");
            ddlRoute.DataTextField = "RName";
            ddlRoute.DataValueField = "RouteId";
            ddlRoute.DataBind();
            ddlRoute.Items.Insert(0, new ListItem("Select", "0"));



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    } 
    private void GetDisOrSSByRouteID()
    {
        try
        {
            ddlDitributor.DataTextField = "DTName";
            ddlDitributor.DataValueField = "DistributorId";
            ddlDitributor.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "Office_ID", "RouteId", "ItemCat_id" },
                   new string[] { "5", objdb.Office_ID(), ddlRoute.SelectedValue,objdb.GetMilkCatId() }, "dataset");
            ddlDitributor.DataBind();
            ddlDitributor.Items.Insert(0, new ListItem("Select", "0"));


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
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
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetDisOrSSByRouteID();
    }
    private void GetDistWiseAdvancedCardDetail()
    {
        try
        {
            lblMsg.Text = "";
            btnPrint.Visible = false;
            //btnExcel.Visible = false;
            div_page_content.InnerHtml = "";
            int differenceInDays = 0;
            lblMsg.Text = "";
            string fm = "16/" + txtMonth.Text;
            
            DateTime fmonth = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
            string fromnonth = fmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            DateTime addmonth = fmonth.AddMonths(1);
            DateTime minusday = addmonth.AddDays(-1);

            TimeSpan ts = (minusday.Date - fmonth.Date);

            differenceInDays = ts.Days +1; // This is in int

            string tmnth = minusday.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tmonth = DateTime.ParseExact(tmnth, "dd/MM/yyyy", culture);
            string tomonth = tmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            string tm = tmonth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            string Distirubutor = ddlDitributor.SelectedItem.Text;
            string[] InstName = Distirubutor.Split('[');
            ds2 = objdb.ByProcedure("USP_Trn_MilkAdvancedCardDetails",
                     new string[] { "Flag", "FromDate", "ToDate", "ItemCat_id", "RouteId", "DistributorId", "Office_ID", "OrganizationId", "AreaId", "differenceInDays" },
                     new string[] { "5", fromnonth, tomonth, ddlItemCategory.SelectedValue, ddlRoute.SelectedValue, ddlDitributor.SelectedValue, objdb.Office_ID(), "0", ddlLocation.SelectedValue, differenceInDays.ToString() }, "dataset");
            if (ds2 != null && ds2.Tables.Count > 0)
            {
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    btnPrint.Visible = true;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<div style='border:1px solid black; padding:10px;'>");
                    sb.Append("<table class='table'>");
                    int Count = ds2.Tables[0].Rows.Count;
                    int ColCount = ds2.Tables[0].Columns.Count;
                    sb.Append("<thead>");
                    sb.Append("<tr>");

                    sb.Append("<td colspan='" + (ColCount) + "' style='padding: 2px 5px;text-align: center;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");

                    sb.Append("<td colspan='" + (ColCount) + "' style='padding: 2px 5px;text-align: center;'>Advanced Card for the period -" + fm + " To - " + tm + "</td>");
                    sb.Append("</tr>");
                    //sb.Append("<tr>");
                    //sb.Append("<td  colspan='" + ColCount + "' style='text-align: Left;'>" + Distirubutor + "");
                    //sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                    sb.Append("</table>");
                    sb.Append("<table class='table' >");
                    sb.Append("<thead>");
                    sb.Append("<tr >");
                   // sb.Append("<td style='border-bottom:1px dashed black !important;'>Date<br>Shift</td>");
                    sb.Append("<td colspan='2' style='border:1px solid black'>" + ddlRoute.SelectedItem.Text + "<br>" + Distirubutor + "</td>");
                    sb.Append("<td style='border:1px solid black'>Shift</td>");
                    for (int j = 0; j < ColCount; j++)
                    {

                        //if (ds1.Tables[0].Columns[j].ToString() != "S.No." && ds1.Tables[0].Columns[j].ToString() != "RouteId" && ds1.Tables[0].Columns[j].ToString() != "Route" && ds1.Tables[0].Columns[j].ToString() != "Total Supply" && ds1.Tables[0].Columns[j].ToString() != "Total Crate" && ds1.Tables[0].Columns[j].ToString() != "Total Supply in Litre")
                        if (ds2.Tables[0].Columns[j].ToString() != "tmp_EFDate" && ds2.Tables[0].Columns[j].ToString() != "ShiftName" && ds2.Tables[0].Columns[j].ToString() != "Shift_id" && ds2.Tables[0].Columns[j].ToString() != "BoothId" && ds2.Tables[0].Columns[j].ToString() != "BName" && ds2.Tables[0].Columns[j].ToString() != "BCode")
                        {
                            string ColName = ds2.Tables[0].Columns[j].ToString();
                            if (j == (ColCount-1))
                            {
                                sb.Append("<td style='border-top:1px solid black; border-bottom:1px solid black; border-right:1px solid black';>" + ColName + "</td>");
                            }
                            else
                            {
                                sb.Append("<td style='border-top:1px solid black; border-bottom:1px solid black';>" + ColName + "</td>");
                            }


                        }

                    }
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                   

                        sb.Append("<tr>");
                        sb.Append("<td style='border-left:1px solid black;'>Booth No.</td>");
                        sb.Append("<td >Booth Name.</td>");
                        sb.Append("<td style='border-right:1px solid black; border-left:1px solid black;'></td>");
                        for (int K = 0; K < ColCount; K++)
                        {
                            if (ds2.Tables[0].Columns[K].ToString() != "tmp_EFDate" && ds2.Tables[0].Columns[K].ToString() != "ShiftName" && ds2.Tables[0].Columns[K].ToString() != "Shift_id" && ds2.Tables[0].Columns[K].ToString() != "BoothId" && ds2.Tables[0].Columns[K].ToString() != "BName" && ds2.Tables[0].Columns[K].ToString() != "BCode")
                            {
                                if (K == (ColCount - 1))
                                {
                                    sb.Append("<td style='border-right:1px solid black;'></td>");
                                }
                                else
                                {
                                    sb.Append("<td ></td>");
                                }
                                
                            }
                            
                        }

                        sb.Append("</tr>");

                   string BName = "";
                    for (int i = 0; i < Count; i++)
                    {

                        sb.Append("<tr>");

                        if (i == (Count - 1))
                        {
                            if (BName == ds2.Tables[0].Rows[i]["BName"].ToString())
                            {
                                sb.Append("<td style='border-left:1px solid black; border-bottom:1px solid black;'></td>");
                                sb.Append("<td style='border-right:1px solid black; border-bottom:1px solid black;'></td>");
                                sb.Append("<td style='border-right:1px solid black; border-bottom:1px solid black;'>" + ds2.Tables[0].Rows[i]["ShiftName"].ToString() + "</td>");
                            }
                            else
                            {
                                sb.Append("<td style='border-left:1px solid black; border-bottom:1px solid black;'>" + ds2.Tables[0].Rows[i]["BCode"].ToString() + "</td>");
                                sb.Append("<td style='border-right:1px solid black; border-bottom:1px solid black;'>" + ds2.Tables[0].Rows[i]["BName"].ToString() + "</td>");
                                sb.Append("<td style='border-right:1px solid black; border-bottom:1px solid black;'>" + ds2.Tables[0].Rows[i]["ShiftName"].ToString() + "</td>");
                            }

                            
                        }
                        else
                        {
                           if (BName == ds2.Tables[0].Rows[i]["BName"].ToString())
                           {
                               sb.Append("<td style='border-left:1px solid black;'></td>");
                               sb.Append("<td style='border-right:1px solid black;'></td>");
                               sb.Append("<td style='border-right:1px solid black;'>" + ds2.Tables[0].Rows[i]["ShiftName"].ToString() + "</td>");
                           }
                           else
                           {
                               sb.Append("<td style='border-left:1px solid black;'>" + ds2.Tables[0].Rows[i]["BCode"].ToString() + "</td>");
                               sb.Append("<td style='border-right:1px solid black;'>" + ds2.Tables[0].Rows[i]["BName"].ToString() + "</td>");
                               sb.Append("<td style='border-right:1px solid black;'>" + ds2.Tables[0].Rows[i]["ShiftName"].ToString() + "</td>");
                           }
                           // sb.Append("<td style='border-left:1px solid black;'>" + ds2.Tables[0].Rows[i]["BCode"].ToString() + "</td>");
                           // sb.Append("<td style='border-right:1px solid black;'>" + ds2.Tables[0].Rows[i]["BName"].ToString() + "</td>");
                        }
                        string Value = "0";
                        for (int K = 0; K < ColCount; K++)
                        {

                            
                            if (ds2.Tables[0].Columns[K].ToString() != "tmp_EFDate" && ds2.Tables[0].Columns[K].ToString() != "ShiftName" && ds2.Tables[0].Columns[K].ToString() != "Shift_id" && ds2.Tables[0].Columns[K].ToString() != "BoothId" && ds2.Tables[0].Columns[K].ToString() != "BName" && ds2.Tables[0].Columns[K].ToString() != "BCode")
                            {
                                string ColName = ds2.Tables[0].Columns[K].ToString();
                                if (ds2.Tables[0].Rows[i][ColName].ToString() == "")
                                {
                                    Value = "0";
                                }
                                else
                                {
                                    Value = ds2.Tables[0].Rows[i][ColName].ToString();
                                }
                                if (i == (Count - 1))
                                {
                                    if (K == (ColCount - 1))
                                    {

                                        sb.Append("<td style='border-bottom:1px solid black; border-right:1px solid black;'>" + Value.ToString() + "</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style='border-bottom:1px solid black;'>" + Value.ToString() + "</td>");
                                    }
                                }
                                else if( K == (ColCount-1))
                                {
                                    sb.Append("<td style='border-right:1px solid black;'>" + Value.ToString() + "</td>");
                                }
                                else
                                {
                                    sb.Append("<td>" + Value.ToString() + "</td>");
                                }

                            }

                        }
                        
                        BName = ds2.Tables[0].Rows[i]["BName"].ToString();

                    }
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='3' style='border:1px solid black; text-align:right'><b>TOTAL</b></td>");
                    DataTable dt = new DataTable();
                    dt = ds2.Tables[0];
                    
                    foreach (DataRow drow in dt.Rows)
                    {
                        foreach (DataColumn column in dt.Columns)
                        {
                            //if (column.ToString() != "S.No." && column.ToString() != "RouteId" && column.ToString() != "Route" && column.ToString() != "Total Supply" && column.ToString() != "Total Crate" && column.ToString() != "Total Supply in Litre'")
                            if (column.ToString() != "tmp_EFDate" && column.ToString() != "ShiftName" && column.ToString() != "Shift_id" && column.ToString() != "BoothId" && column.ToString() != "BName" && column.ToString() != "BCode")
                            {

                                if (drow.IsNull(column) || DBNull.Value.Equals(drow[column]))
                                {
                                    drow[column] = 0;
                                }

                            }

                        }
                    }
                    DataTable dt1 = new DataTable();
                    dt1 = dt;
                    decimal sum11 = 0;
                    
                        foreach (DataColumn column in dt1.Columns)
                        {
                            if (column.ToString() != "tmp_EFDate" && column.ToString() != "ShiftName" && column.ToString() != "Shift_id" && column.ToString() != "BoothId" && column.ToString() != "BName" && column.ToString() != "BCode")
                            {
                                var index = column.Ordinal + 1; // Current column index
                                if (index == dt1.Columns.Count)
                                {
                                    sum11 = Convert.ToDecimal(dt1.Compute("SUM([" + column + "])", string.Empty));
                                    sb.Append("<td style='border-bottom:1px solid black;  border-right:1px solid black;'><b>" + sum11.ToString() + "</b></td>");
                                }
                                else
                                {
                                    sum11 = Convert.ToDecimal(dt1.Compute("SUM([" + column + "])", string.Empty));
                                    sb.Append("<td style='border-bottom:1px solid black;'><b>" + sum11.ToString() + "</b></td>");
                                }

                            }
                        }
                   
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");

                    ////////////////End Of Route Wise Print Code   ///////////////////////
                    //btnPrint.Visible = true;
                    //btnExcel.Visible = true;
                    div_page_content.InnerHtml = sb.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetDistWiseAdvancedCardDetail();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtMonth.Text = string.Empty;
        ddlRoute.SelectedIndex = 0;
        ddlDitributor.SelectedIndex = 0;
        ddlLocation.SelectedIndex = 0;
        ddlRoute.Items.Clear();
        ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
        btnPrint.Visible = false;
        ddlItemCategory.SelectedIndex = 0;
        div_page_content.InnerHtml = "";
    }
}