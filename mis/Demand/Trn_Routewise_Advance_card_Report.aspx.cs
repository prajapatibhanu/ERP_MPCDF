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

public partial class mis_Demand_Trn_Routewise_Advance_card_Report : System.Web.UI.Page
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
        }
        //if(!IsPostBack)
        //{
        //    string fm = "01/" + txtMonth.Text;
        //    DateTime Monthdate = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
        //    string FromDate = Monthdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

        //    DateTime origDT = Convert.ToDateTime(FromDate);
        //    DateTime lastDate = new DateTime(origDT.Year, origDT.Month, 1).AddMonths(1).AddDays(-1);
        //    string ToDate = lastDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
        //}
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
        try
        {

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
            else
            {
                ddlLocation.Items.Insert(0, new ListItem("Select", "0"));

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
    //protected void fillRoute()
    //{
    //    ddlRout.Items.Clear();
    //    ds = objdb.ByProcedure("USP_Rpt_Routewise_AdvCard", new string[] { "Flag", "Office_ID" }, new string[] { "1", objdb.Office_ID() }, "dataset");
    //    if (ds != null && ds.Tables.Count > 0)
    //    {
    //        ddlRout.DataSource = ds;
    //        ddlRout.DataTextField = "RName";
    //        ddlRout.DataValueField = "RouteId";
    //        ddlRout.DataBind();
    //        ddlRout.Items.Insert(0, new ListItem("Select", "0"));


    //    }
    //}
    protected void ddlRout_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            btnExportAll.Visible = false;
            btnPrintRoutWise.Visible = false;
            htmDiv.InnerHtml = "";
            DivHtml.InnerHtml = "";
            Print.InnerHtml = "";
            ddlRetailer.Items.Clear();
            if (ddlRout.SelectedValue != "0")
            {
                ds = objdb.ByProcedure("USP_Rpt_Routewise_AdvCard", new string[] { "Flag", "Office_ID", "RouteId" }, new string[] { "2", objdb.Office_ID(), ddlRout.SelectedValue }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    ddlRetailer.DataSource = ds;
                    ddlRetailer.DataTextField = "BName";
                    ddlRetailer.DataValueField = "BoothId";
                    ddlRetailer.DataBind();
                    //ddlRetailer.Items.Insert(0, new ListItem("Select", "0"));
                    ddlRetailer.Items.Insert(0, new ListItem("All", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
    public string summryDtl(DataTable dts)
    {
        StringBuilder ssb = new StringBuilder();
        ssb.Append("");
        ssb.Append("<div class='pagebreak'></div>");
        ssb.Append("<table border=1 class='table1' style='width:50%; height:100%'>");
        ssb.Append("<tr>");
        ssb.Append("<td style='text-align:center' colspan='5'><b> " + ViewState["Office_Name"].ToString() + "</b></td>");
        ssb.Append("</tr>");
        ssb.Append("<tr>");
        ssb.Append("<td style='text-align:center' colspan='5'><b>Summary Details</b> </td>");
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
        ssb.Append("<td style='text-align:center' colspan='2'><b></b></td>");
        ssb.Append("<td style='text-align:center' ><b>Total</b></td>");
        ssb.Append("<td style='text-align:center'><b>" + Totalqty + "</b></td>");
        ssb.Append("<td style='text-align:center'><b>" + TotalPrice + "</b></td>");
        ssb.Append("</tr>");
        ssb.Append("</table>");
        return ssb.ToString();
    }
    public string RptBillMonth(DataTable dt, string mName, string bName)
    {

        StringBuilder sbb = new StringBuilder();

        sbb.Append("<table border=1 class='table1' style='width:100%; height:100%'>");
        sbb.Append("<tr>");
        sbb.Append("<td style='text-align:center' colspan='9'><b> " + ViewState["Office_Name"].ToString() + "</b></td>");
        sbb.Append("</tr>");
        sbb.Append("<tr>");
        sbb.Append("<td style='text-align:center' colspan='9'><b>Bill Month : </b>" + mName + "</td>");
        sbb.Append("</tr>");
        sbb.Append("<tr>");
        sbb.Append("<td style='text-align:center' colspan='9'><b>Booth : </b> " + bName + "</td>");
        sbb.Append("</tr>");
        sbb.Append("<tr>");
        sbb.Append("<td style='text-align:center'><b>Route</b></td>");
        sbb.Append("<td style='text-align:center'><b>Booth ID/NAme</b></td>");
        sbb.Append("<td style='text-align:center'><b>Card Number</b></td>");
        sbb.Append("<td style='text-align:center'><b>Customer Details</b></td>");
        sbb.Append("<td style='text-align:center'><b>Product</b></td>");
        sbb.Append("<td style='text-align:center'><b>Packet Size</b></td>");
        sbb.Append("<td style='text-align:center'><b>Shift</b></td>");
        sbb.Append("<td style='text-align:center'><b>Qty</b></td>");
        sbb.Append("<td style='text-align:center'><b>Amount</b></td>");
        sbb.Append("</tr>");
        int Count1 = dt.Rows.Count;
        string orderid = "";
        int rowspn = 1;
        for (int j = 0; j < Count1; j++)
        {
            if (j == 0)
            {
                sbb.Append("<tr>");
                sbb.Append("<td style='text-align:center' rowspan='" + rowspn + "'>" + dt.Rows[j]["R_Name"] + "</td>");
                sbb.Append("<td style='text-align:center' rowspan='" + rowspn + "'>" + dt.Rows[j]["B_Name"] + "</td>");
                sbb.Append("<td style='text-align:center' rowspan='" + rowspn + "'>" + dt.Rows[j]["OrderId"] + "</td>");
                sbb.Append("<td style='text-align:center' rowspan='" + rowspn + "'>" + dt.Rows[j]["Customer_Dtl"] + "</td>");
                sbb.Append("<td style='text-align:center'>" + dt.Rows[j]["Product"] + "</td>");
                sbb.Append("<td style='text-align:center'>" + dt.Rows[j]["Packet_Size"] + "</td>");
                sbb.Append("<td style='text-align:center'>" + dt.Rows[j]["ShiftName"] + "</td>");
                sbb.Append("<td style='text-align:center'>" + dt.Rows[j]["ItemQty"] + "</td>");
                sbb.Append("<td style='text-align:center'>" + dt.Rows[j]["Amount"] + "</td>");
                sbb.Append("</tr>");
            }
            else
            {
                sbb.Append("<tr>");
                if (orderid == Convert.ToString(dt.Rows[j]["OrderId"]))
                {

                    sbb.Append("<td style='text-align:center'></td>");
                    sbb.Append("<td style='text-align:center'></td>");
                    sbb.Append("<td style='text-align:center'></td>");
                    sbb.Append("<td style='text-align:center'></td>");
                    sbb.Append("<td style='text-align:center'>" + dt.Rows[j]["Product"] + "</td>");
                    sbb.Append("<td style='text-align:center'>" + dt.Rows[j]["Packet_Size"] + "</td>");
                    sbb.Append("<td style='text-align:center'>" + dt.Rows[j]["ShiftName"] + "</td>");
                    sbb.Append("<td style='text-align:center'>" + dt.Rows[j]["ItemQty"] + "</td>");
                    sbb.Append("<td style='text-align:center'>" + dt.Rows[j]["Amount"] + "</td>");
                }
                else
                {

                    sbb.Append("<td style='text-align:center'>" + dt.Rows[j]["R_Name"] + "</td>");
                    sbb.Append("<td style='text-align:center'>" + dt.Rows[j]["B_Name"] + "</td>");
                    sbb.Append("<td style='text-align:center'>" + dt.Rows[j]["OrderId"] + "</td>");
                    sbb.Append("<td style='text-align:center'>" + dt.Rows[j]["Customer_Dtl"] + "</td>");
                    sbb.Append("<td style='text-align:center'>" + dt.Rows[j]["Product"] + "</td>");
                    sbb.Append("<td style='text-align:center'>" + dt.Rows[j]["Packet_Size"] + "</td>");
                    sbb.Append("<td style='text-align:center'>" + dt.Rows[j]["ShiftName"] + "</td>");
                    sbb.Append("<td style='text-align:center'>" + dt.Rows[j]["ItemQty"] + "</td>");
                    sbb.Append("<td style='text-align:center'>" + dt.Rows[j]["Amount"] + "</td>");
                }
                sbb.Append("</tr>");
            }
            orderid = Convert.ToString(dt.Rows[j]["OrderId"]);
        }



        //string sub = "";
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    sbb.Append("<tr>";
        //    int count = dt.Select("OrderId ='" + dt.Rows[i][2].ToString() + "'").Count();
        //    for (int j = 0; j < dt.Columns.Count; j++)
        //    {
        //        if (j < 4)
        //        {
        //            if (sub != dt.Rows[i][2].ToString())
        //            {
        //                sbb.Append("<td style='text-align:center' rowspan='" + count + "'>" + dt.Rows[i][j].ToString() + "</td>";
        //            }
        //            continue;
        //        }
        //        sbb.Append("<td style='text-align:center'>" + dt.Rows[i][j].ToString() + "</td>";
        //    }
        //    sub = dt.Rows[i][2].ToString();
        //    sbb.Append("</tr>";
        //}
        sbb.Append("<tr>");
        sbb.Append("<td style='text-align:center' colspan='6' ><b></b></td>");
        sbb.Append("<td style='text-align:center' ><b>Total</b></td>");
        Decimal TotalBillqty = Convert.ToDecimal(dts.Compute("SUM(ItemQty)", string.Empty));
        sbb.Append("<td style='text-align:center' ><b>" + TotalBillqty + "</b></td>");
        Decimal TotalBillPrice = Convert.ToDecimal(dt.Compute("SUM(Amount)", string.Empty));
        sbb.Append("<td style='text-align:center'><b>" + TotalBillPrice + "</b></td>");
        sbb.Append("</tr>");
        sbb.Append("</table>");

        return sbb.ToString();
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
                ds = objdb.ByProcedure("USP_Rpt_Routewise_AdvCard", new string[] { "Flag", "Office_ID", "RouteId", "BoothId", "FromDate", "ToDate" }, new string[] { "3", objdb.Office_ID(), ddlRout.SelectedValue, ddlRetailer.SelectedValue, FromDate, ToDate }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        btnExportAll.Visible = true;
                        btnPrintRoutWise.Visible = true;
                        dt = ds.Tables[0];
                        dts = ds.Tables[1];
                        htmDiv.InnerHtml = RptBillMonth(dt, MonthYear, ddlRetailer.SelectedItem.ToString());
                        DivHtml.InnerHtml = summryDtl(dts);
                        ViewState["GPandProduct"] = RptBillMonth(dt, MonthYear, ddlRetailer.SelectedItem.ToString());
                        ViewState["GPandProduct"] += summryDtl(dts);
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
                    RequiredFieldValidator3.Enabled = false;
                }
                else
                {
                    RequiredFieldValidator3.Enabled = true;
                }
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        txtMonth.Text = string.Empty;
        ddlLocation.SelectedIndex = 0;
        ddlRout.Items.Clear();
        lblMsg.Text = string.Empty;
        ddlRout.Items.Insert(0, new ListItem("Select", "0"));
        ddlRetailer.Items.Clear();
        ddlRetailer.Items.Insert(0, new ListItem("Select", "0"));
        btnExportAll.Visible = false;
        btnPrintRoutWise.Visible = false;
        htmDiv.InnerHtml = "";
        DivHtml.InnerHtml = "";
        Print.InnerHtml = "";
    }
}