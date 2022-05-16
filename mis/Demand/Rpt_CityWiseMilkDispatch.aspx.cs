using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;
using System.IO;
using System.Data;

public partial class mis_Demand_Rpt_CityWiseMilkDispatch : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds3,ds4, ds5 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetOfficeDetails();
                GetCityName();
                GetShift();
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Attributes.Add("readonly", "true");
                txtToDate.Attributes.Add("readonly", "true");
                txtFromDate.Text = Date;
                txtToDate.Text = Date;
            }
        }
        else
        {
            objdb.redirectToHome();
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
    private void GetCityName()
    {
        try
        {
            ddlCity.DataTextField = "RouteHeadName";
            ddlCity.DataValueField = "RouteHeadId";
            ddlCity.DataSource = objdb.ByProcedure("USP_Mst_RouteHead",
                 new string[] { "flag", "Office_ID" },
                   new string[] { "1",objdb.Office_ID() }, "dataset");
            ddlCity.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
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
    private void HeadRouteWiseMilkDisptachDetails()
    {
        try
        {
            lblMsg.Text = string.Empty;
            div_page_content.InnerHtml = "";
            Print.InnerHtml = "";
            string HeadRouteId = "", MultiHeadRoutId = "", HeadRouteName = "";
            int rr = 0, iddata = 0;
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            StringBuilder sb1 = new StringBuilder();
            foreach (ListItem item in ddlCity.Items)
            {
                if (item.Selected)
                {
                    HeadRouteId = item.Value;
                    HeadRouteName = item.Text;

                    ++iddata;
                    if (iddata == 1)
                    {
                        MultiHeadRoutId = HeadRouteId;

                    }
                    else
                    {
                        MultiHeadRoutId += "," + HeadRouteId;

                    }


                    ds5 = objdb.ByProcedure("USP_Trn_MilkDisptachReport",
                             new string[] { "Flag", "FromDate", "ToDate", "ItemCat_id", "RouteHeadId", "Office_ID" },
                               new string[] { "1", fromdat, todat, objdb.GetMilkCatId(), HeadRouteId, objdb.Office_ID() }, "dataset");
                    if (ds5.Tables[0].Rows.Count > 0)
                    {

                        btnPrint.Visible = true;
                        btnExcel.Visible = true;
                        rr++;
                        int Count1 = ds5.Tables[0].Rows.Count;
                        int ColCount1 = ds5.Tables[0].Columns.Count;

                        if (rr == 1)
                        {
                            sb1.Append("<table class='table'>");
                            sb1.Append("<thead>");
                            sb1.Append("<tr>");
                            sb1.Append("<td style='border: 1px solid #000000;padding: 2px 5px;text-align: center'  colspan='" + (ColCount1 + 2) + "'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");

                            sb1.Append("</tr>");
                            sb1.Append("<tr>");
                            sb1.Append("<td style='border: 1px solid #000000;padding: 2px 5px;text-align: center;'colspan='" + (ColCount1) + "'>City Wise Milk Dispatch Report (In Ltr)  Date  : " + txtFromDate.Text + " - " + txtToDate.Text + "  Shift - " + ddlShift.SelectedItem.Text + "</td>");

                            sb1.Append("</tr>");

                            sb1.Append("<tr>");
                            for (int j = 0; j < ColCount1; j++)
                            {
                                if (ds5.Tables[0].Columns[j].ToString() != "SuperStockistId")
                                {
                                    string ColName = ds5.Tables[0].Columns[j].ToString();
                                    sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");
                                }
                            }

                            sb1.Append("</tr>");
                            sb1.Append("</thead>");
                            sb1.Append("<tr>");
                            sb1.Append("<td colspan='" + ColCount1 + "' style=' border: 1px solid #000000 !important;'><b>" + HeadRouteName + "</b></td>");
                            sb1.Append("</tr>");
                            for (int i = 0; i < Count1; i++)
                            {
                                sb1.Append("<tr>");
                                for (int K = 0; K < ColCount1; K++)
                                {
                                    if (ds5.Tables[0].Columns[K].ToString() == "Name")
                                    {
                                        string ColName = ds5.Tables[0].Columns[K].ToString();
                                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + (ds5.Tables[0].Rows[i][ColName].ToString() == "" ? "0" : ds5.Tables[0].Rows[i][ColName].ToString()) + "</td>");
                                    }
                                    if (ds5.Tables[0].Columns[K].ToString() != "SuperStockistId" && ds5.Tables[0].Columns[K].ToString() != "Name")
                                    {
                                        string ColName = ds5.Tables[0].Columns[K].ToString();
                                        sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + (ds5.Tables[0].Rows[i][ColName].ToString() == "" ? "0" : ds5.Tables[0].Rows[i][ColName].ToString()) + "</td>");
                                    }
                                }
                                sb1.Append("</tr>");


                            }
                        }
                        else
                        {
                            sb1.Append("<tr>");
                            sb1.Append("<td colspan='" + ColCount1 + "' style=' border: 1px solid #000000 !important;'><b>" + HeadRouteName + "</b></td>");
                            sb1.Append("</tr>");
                            for (int i = 0; i < Count1; i++)
                            {
                                
                                sb1.Append("<tr>");
                                for (int K = 0; K < ColCount1; K++)
                                {
                                    if (ds5.Tables[0].Columns[K].ToString() == "Name")
                                    {
                                        string ColName = ds5.Tables[0].Columns[K].ToString();
                                        sb1.Append("<td style='text-align: left;border: 1px solid #000000 !important;'>" + (ds5.Tables[0].Rows[i][ColName].ToString() == "" ? "0" : ds5.Tables[0].Rows[i][ColName].ToString()) + "</td>");
                                    }
                                    if (ds5.Tables[0].Columns[K].ToString() != "SuperStockistId" && ds5.Tables[0].Columns[K].ToString() != "Name")
                                    {
                                        string ColName = ds5.Tables[0].Columns[K].ToString();
                                        sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + (ds5.Tables[0].Rows[i][ColName].ToString() == "" ? "0" : ds5.Tables[0].Rows[i][ColName].ToString()) + "</td>");
                                    }
                                }
                                sb1.Append("</tr>");

                            }
                        }

                        sb1.Append("<tr>");
                        sb1.Append("<td style='text-align: center;border:1px solid black !important;'><b>Sub Total</b></td>");
                        //Sub Total
                        DataTable dt = new DataTable();
                        dt = ds5.Tables[0];

                        decimal sum11 = 0;


                        foreach (DataColumn column in dt.Columns)
                        {

                            if (column.ToString() != "SuperStockistId" && column.ToString() != "Name")
                            {

                                sum11 = dt.AsEnumerable().Sum(r => r.Field<decimal?>("" + column + "") ?? 0);
                                sb1.Append("<td style='text-align: center;border:1px solid black !important;'><b>" + sum11.ToString() + "</b></td>");

                            }

                        }
                        sb1.Append("</tr>");
                        if (dt != null) { dt.Dispose(); }
                        if (ds5 != null) { ds5.Dispose(); }
                    }
                    
                }

            }

            ds4 = objdb.ByProcedure("USP_Trn_MilkDisptachReport",
                             new string[] { "Flag", "FromDate", "ToDate", "ItemCat_id", "MultiRouteHeadId", "Office_ID" },
                               new string[] { "2", fromdat, todat, objdb.GetMilkCatId(), MultiHeadRoutId, objdb.Office_ID() }, "dataset");
            if (ds4.Tables[0].Rows.Count > 0)
            {
                sb1.Append("<tr>");
                sb1.Append("<td style='text-align: center;border:1px solid black !important;' ><b>Grant Total</b></td>");
                //Grand total
                DataTable dt = new DataTable();
                dt = ds4.Tables[0];

                decimal sum12 = 0;
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() != "SuperStockistId" && column.ToString() != "SSName")
                    {
                        sum12 = dt.AsEnumerable().Sum(r => r.Field<decimal?>("" + column + "") ?? 0);
                        sb1.Append("<td style='text-align: center;border:1px solid black !important;'><b>" + sum12.ToString() + "</b></td>");

                    }

                }
                sb1.Append("</tr>");
                if (dt != null) { dt.Dispose(); }

            }
            if (ds4 != null) { sb1.Append("</table>"); }
           
            if (ds4 != null) { ds4.Dispose(); }
            div_page_content.InnerHtml = sb1.ToString();
            Print.InnerHtml = sb1.ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
            btnPrint.Visible = false;
            btnExcel.Visible = false;
            div_page_content.InnerHtml = "";
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
        btnPrint.Visible = false;
        btnExcel.Visible = false;
        div_page_content.InnerHtml = "";
        lblMsg.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "CityWiseMilkDispatch" + DateTime.Now + ".xls");
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
            int Fmonth = fdate.Month;
            int Tmonth = tdate.Month;
            if ((fdate <= tdate) && (Fmonth == Tmonth))
            {
                lblMsg.Text = string.Empty;
                HeadRouteWiseMilkDisptachDetails();
            }
            else
            {
                txtToDate.Text = string.Empty;
                Fmonth = 0;
                Tmonth = 0;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate and Month must be same(ex : 01/01/2021 - 31/01/2021).");
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
                btnPrint.Visible = false;
                btnExcel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
        }
    }
}