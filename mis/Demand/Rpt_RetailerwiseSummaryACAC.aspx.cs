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

public partial class mis_Demand_Rpt_RetailerwiseSummaryACAC : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds3, ds5 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetOfficeDetails();
                GetLocation();
                GetRoute();
                txtFromDate.Attributes.Add("readonly", "true");
                txtToDate.Attributes.Add("readonly", "true");
            }
        }
        else
        {
            objdb.redirectToHome();
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
            ddlRoute.DataTextField = "RName";
            ddlRoute.DataValueField = "RouteId";
            ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "AreaId", "ItemCat_id", "Office_ID" },
                   new string[] { "9", ddlLocation.SelectedValue, objdb.GetMilkCatId(), objdb.Office_ID() }, "dataset");
            ddlRoute.DataBind();
            ddlRoute.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }
    private void GetRetailer()
    {
        try
        {
            ddlRetailer.DataTextField = "BoothName";
            ddlRetailer.DataValueField = "BoothId";
            ddlRetailer.DataSource = objdb.ByProcedure("USP_Mst_BoothReg",
                     new string[] { "flag", "RouteId", "ItemCat_id" },
                       new string[] { "12", ddlRoute.SelectedValue, objdb.GetMilkCatId() }, "dataset");
            ddlRetailer.DataBind();

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
                RouteWiseMilkSummaryACAC();
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
    private void RouteWiseMilkSummaryACAC()
    {
        lblMsg.Text = string.Empty;
        div_page_content.InnerHtml = "";
        Print.InnerHtml = "";
        string boothid = "", boothname = "";
        int rr = 0;
        StringBuilder sb1 = new StringBuilder();
        foreach (ListItem item in ddlRetailer.Items)
        {
            if (item.Selected)
            {

                boothid = item.Value;
                boothname = item.Text;
                DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
                DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
                DateTime enddate = date4.AddDays(1);
                string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                TimeSpan ts = (enddate - date3);
                int differenceInDays = ts.Days; // This is in int

                ds5 = objdb.ByProcedure("USP_Trn_SupplSummaryACAC",
                         new string[] { "Flag", "FromDate", "ToDate", "ItemCat_id", "RouteId", "BoothId", "Office_ID" },
                           new string[] { "2", fromdat, todat, objdb.GetMilkCatId(), ddlRoute.SelectedValue, boothid, objdb.Office_ID() }, "dataset");
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
                    sb1.Append("<td style='border: 1px solid #000000;padding: 2px 5px;' colspan='2'><b>" + ddlRoute.SelectedItem.Text + "</b></td>");
                    sb1.Append("<td style='border: 1px solid #000000;padding: 2px 5px;text-align: left;'colspan='" + (ColCount1) + "'><b>Date  :-" + txtFromDate.Text + " -" + txtToDate.Text + "</b></td>");

                    sb1.Append("</tr>");

                        sb1.Append("<tr>");
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>S.no</b></td>");
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Retailer Name</b></td>");
                        for (int j = 0; j < ColCount1; j++)
                        {
                            if (ds5.Tables[0].Columns[j].ToString() != "BoothId")
                            {
                                string ColName = ds5.Tables[0].Columns[j].ToString();
                                sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");
                            }
                        }
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Avg In Ltr</b></td>");
                        sb1.Append("</tr>");
                        sb1.Append("</thead>");
                        for (int i = 0; i < Count1; i++)
                        {
                            sb1.Append("<tr>");
                            sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + rr.ToString() + "</td>");
                            sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + boothname + "</td>");
                            for (int K = 0; K < ColCount1; K++)
                            {
                                if (ds5.Tables[0].Columns[K].ToString() != "BoothId")
                                {
                                    string ColName = ds5.Tables[0].Columns[K].ToString();
                                    if (ColName == "In Ltr")
                                    {
                                        sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + Convert.ToDecimal(ds5.Tables[0].Rows[i][ColName]) + "</b></td>");
                                        sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + (differenceInDays == 0 ? Convert.ToDecimal(ds5.Tables[0].Rows[i][ColName]) : (Convert.ToDecimal(ds5.Tables[0].Rows[i][ColName]) / differenceInDays)).ToString("0.00") + "</b></td>");
                                    }
                                    else
                                    {
                                        if (ColName == "Total Packet")
                                        {
                                            sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + (ds5.Tables[0].Rows[i][ColName].ToString() == "" ? "0" : ds5.Tables[0].Rows[i][ColName].ToString()) + "</b></td>");
                                        }
                                        else
                                        {
                                            sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + (ds5.Tables[0].Rows[i][ColName].ToString() == "" ? "0" : ds5.Tables[0].Rows[i][ColName].ToString()) + "</td>");
                                        }
                                    }
                                    
                                   

                                }
                            }
                            sb1.Append("</tr>");

                        }
                    }
                    else
                    {
                        for (int i = 0; i < Count1; i++)
                        {
                            sb1.Append("<tr>");
                            sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + rr.ToString() + "</td>");
                            sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + boothname + "</td>");
                            for (int K = 0; K < ColCount1; K++)
                            {
                                if (ds5.Tables[0].Columns[K].ToString() != "BoothId")
                                {
                                    string ColName = ds5.Tables[0].Columns[K].ToString();
                                    if (ColName == "In Ltr")
                                    {
                                        sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + Convert.ToDecimal(ds5.Tables[0].Rows[i][ColName]) + "</b></td>");
                                        sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + (differenceInDays == 0 ? Convert.ToDecimal(ds5.Tables[0].Rows[i][ColName]) : (Convert.ToDecimal(ds5.Tables[0].Rows[i][ColName]) / differenceInDays)).ToString("0.00") + "</b></td>");
                                    }
                                    else
                                    {
                                        if (ColName == "Total Packet")
                                        {
                                            sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'><b>" + (ds5.Tables[0].Rows[i][ColName].ToString() == "" ? "0" : ds5.Tables[0].Rows[i][ColName].ToString()) + "</b></td>");
                                        }
                                        else
                                        {
                                            sb1.Append("<td style='text-align: center;border: 1px solid #000000 !important;'>" + (ds5.Tables[0].Rows[i][ColName].ToString() == "" ? "0" : ds5.Tables[0].Rows[i][ColName].ToString()) + "</td>");
                                        }
                                       
                                    }

                                }
                            }
                            sb1.Append("</tr>");

                        }
                    }
                }
                if (ds5 != null) { ds5.Dispose(); }
            }

        }
        sb1.Append("</table>");
        div_page_content.InnerHtml = sb1.ToString();
        Print.InnerHtml = sb1.ToString();
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
            Response.AddHeader("content-disposition", "attachment; filename=" + "RetailerwiseSummary" + DateTime.Now + ".xls");
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
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRoute.SelectedValue != "0")
        {
            GetRetailer();
        }
    }
}