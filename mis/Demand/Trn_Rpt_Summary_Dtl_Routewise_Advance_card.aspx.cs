using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using System.IO;

public partial class mis_Demand_Trn_Rpt_Summary_Dtl_Routewise_Advance_card : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds3, ds4 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    StringBuilder sb = new StringBuilder();
    DataTable dt, dts = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtMonth.Attributes.Add("readonly", "readonly");
            GetOfficeDetails();
            fillLocation();
            ddlRout.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                btnExportAll.Visible = false;
                btnPrintRoutWise.Visible = false;
                htmDiv.InnerHtml = "";
                DivHtml.InnerHtml = "";

                string fm = "01/" + txtMonth.Text;

                DateTime Monthdate = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
                string FromDate = Monthdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                string My = Monthdate.ToString("MMMM/yyyy", CultureInfo.InvariantCulture);
                string MonthYear = My.Replace("/", "-");
                DateTime origDT = Convert.ToDateTime(FromDate);
                DateTime lastDate = new DateTime(origDT.Year, origDT.Month, 1).AddMonths(1).AddDays(-1);
                string ToDate = lastDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                ds = objdb.ByProcedure("USP_Rpt_Routewise_AdvCard", new string[] { "Flag", "Office_ID", "RouteId", "FromDate", "ToDate" },
                                                                    new string[] { "4", objdb.Office_ID(), ddlRout.SelectedValue, FromDate, ToDate }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        btnExportAll.Visible = true;
                        btnPrintRoutWise.Visible = true;

                        dts = ds.Tables[0];
                        DivHtml.InnerHtml = summryDtl(dts, MonthYear, ddlRout.SelectedItem.ToString());
                        ViewState["GPandProduct"] = summryDtl(dts, MonthYear, ddlRout.SelectedItem.ToString());
                        Print.InnerHtml = ViewState["GPandProduct"].ToString();
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Sorry! : ", "No Record Found.");
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Sorry! : ", "No Record Found.");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
    //protected void ddlRout_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ddlRetailer.Items.Clear();
    //    if (ddlRout.SelectedValue != "0")
    //    {
    //        ds = objdb.ByProcedure("USP_Rpt_Routewise_AdvCard", new string[] { "Flag", "Office_ID", "RouteId" }, new string[] { "2", objdb.Office_ID(), ddlRout.SelectedValue }, "dataset");
    //        if (ds != null && ds.Tables.Count > 0)
    //        {
    //            ddlRetailer.DataSource = ds;
    //            ddlRetailer.DataTextField = "BName";
    //            ddlRetailer.DataValueField = "BoothId";
    //            ddlRetailer.DataBind();
    //            ddlRetailer.Items.Insert(0, new ListItem("Select", "0"));
    //            ddlRetailer.Items.Insert(1, new ListItem("All", "-1"));
    //        }
    //    }
    //}
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            btnExportAll.Visible = false;
            btnPrintRoutWise.Visible = false;
            htmDiv.InnerHtml = "";
            DivHtml.InnerHtml = "";
            Print.InnerHtml = "";
            ddlRout.Items.Clear();
            if (ddlLocation.SelectedValue != "0")
            {

                ds = objdb.ByProcedure("USP_Rpt_Routewise_AdvCard", new string[] { "Flag", "Office_ID", "AreaId", "ItemCat_id" }, new string[] { "1", objdb.Office_ID(), ddlLocation.SelectedValue, "3" }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    ddlRout.DataSource = ds;
                    ddlRout.DataTextField = "RName";
                    ddlRout.DataValueField = "RouteId";
                    ddlRout.DataBind();
                    ddlRout.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlRout.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlRout.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
    protected void btnPrintRoutWise_Click(object sender, EventArgs e)
    {
        try
        {
            Print.InnerHtml = ViewState["GPandProduct"].ToString();
            ClientScriptManager CSM = Page.ClientScript;
            string strScript = "<script>";
            strScript += "window.print();";

            strScript += "</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "Location-" + ddlLocation.SelectedItem.Text + DateTime.Now + ".xls");
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
    protected void fillLocation()
    {
        try { 

        ddlLocation.Items.Clear();
        ds = objdb.ByProcedure("USP_Mst_Area", new string[] { "Flag", }, new string[] { "1" }, "dataset");
        if (ds != null && ds.Tables.Count > 0)
        {
            ddlLocation.DataSource = ds;
            ddlLocation.DataTextField = "AreaName";
            ddlLocation.DataValueField = "AreaId";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
        }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
    public string summryDtl(DataTable dts, string month_year, string strRoute)
    {
        StringBuilder ssb = new StringBuilder();
        ssb.Append("");
        ssb.Append("<div class='pagebreak'></div>");
        ssb.Append("<table border=1 class='table1' style='width:100%; height:100%'>");
        ssb.Append("<tr>");
        ssb.Append("<td style='text-align:center' colspan='5'><b> " + ViewState["Office_Name"].ToString() + "</b></td>");
        ssb.Append("</tr>");
        ssb.Append("<tr>");
        ssb.Append("<td style='text-align:center' colspan='5'><b>Route Sales Summary</b> </td>");
        ssb.Append("</tr>");
        ssb.Append("<tr>");
        ssb.Append("<td style='text-align:center' colspan='5'><b>Bill Month : </b> " + month_year + "</td>");
        ssb.Append("</tr>");
        ssb.Append("<tr>");
        ssb.Append("<td style='text-align:center' colspan='5'><b>Route : </b>" + strRoute + " </td>");
        ssb.Append("</tr>");
        ssb.Append("<tr>");
        ssb.Append("<td style='text-align:center'><b>Product</b></td>");
        ssb.Append("<td style='text-align:center'><b>Packet Size</b></td>");
        ssb.Append("<td style='text-align:center'><b>Shift</b></td>");
        ssb.Append("<td style='text-align:center'><b>Qty</b></td>");
        ssb.Append("<td style='text-align:center'><b>Amount</b></td>");
        ssb.Append("</tr>");


        for (int i = 0; i < dts.Rows.Count; i++)
        {
            ssb.Append("<tr>");

            for (int j = 0; j < dts.Columns.Count; j++)
            {

                ssb.Append("<td style='text-align:center'>" + dts.Rows[i][j].ToString() + "</td>");
            }

            ssb.Append("</tr>");
        }
        Decimal TotalPrice = Convert.ToDecimal(dts.Compute("SUM(Amount)", string.Empty));
        Decimal Totalqty = Convert.ToDecimal(dts.Compute("SUM(ItemQty)", string.Empty));
        ssb.Append("<tr>");
        ssb.Append("<td style='text-align:center' colspan='2'></td>");
        ssb.Append("<td style='text-align:center' ><b>Total</b></td>");
        ssb.Append("<td style='text-align:center'><b>" + Totalqty + "</b></td>");
        ssb.Append("<td style='text-align:center'><b>" + TotalPrice + "</b></td>");
        ssb.Append("</tr>");
        ssb.Append("</table>");
        return ssb.ToString();
    }


    protected void btnClr_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        txtMonth.Text = "";
        ddlRout.Items.Clear();
        ddlRout.Items.Insert(0, new ListItem("Select", "0"));
        ddlLocation.ClearSelection();
        btnExportAll.Visible = false;
        btnPrintRoutWise.Visible = false;
        htmDiv.InnerHtml = "";
        DivHtml.InnerHtml = "";
        Print.InnerHtml = "";
    }
}