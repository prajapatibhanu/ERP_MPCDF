using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_DemandSupply_CrateSummary_Rpt : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2 = new DataSet();
    int total_IssuedCrate = 0;
    int total_ReturnCrate = 0;
    int total_ShortExcess = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetLocation();
                GetShift();
               // GetItemCategory();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================

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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
    }
    //protected void GetItemCategory()
    //{
    //    try
    //    {
    //        ds1 = objdb.ByProcedure("USP_Trn_RetailerAdvanceCard",
    //              new string[] { "flag" },
    //             new string[] { "1" }, "dataset");

    //        if (ds1.Tables[0].Rows.Count != 0)
    //        {
    //            ddlItemCategory.DataTextField = "ItemCatName";
    //            ddlItemCategory.DataValueField = "ItemCat_id";
    //            ddlItemCategory.DataSource = ds1.Tables[0];
    //            ddlItemCategory.DataBind();
    //            ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds1 != null) { ds1.Dispose(); }
    //    }
    //}
    private void GetRoute()
    {
        try
        {
            if (ddlLocation.SelectedValue != "0")
            {
                ddlRoute.Items.Clear();
                ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                       new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                  new string[] { "7", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetMilkCatId() }, "dataset");
                ddlRoute.DataTextField = "RName";
                ddlRoute.DataValueField = "RouteId";
                ddlRoute.DataBind();
                ddlRoute.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2: ", ex.Message.ToString());
        }
    }

    private void GetCrateDetails()
    {
        try
        {
            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            // ds1 = objdb.ByProcedure("USP_Trn_MilkCrateMgmt",
                     // new string[] { "flag", "FromDate", "ToDate", "AreaID", "RouteId", "Office_ID", "ShiftId", "ItemCat_id" },
                       // new string[] { "3", fromdat, todat, ddlLocation.SelectedValue, ddlRoute.SelectedValue, objdb.Office_ID(),ddlShift.SelectedValue,objdb.GetMilkCatId() }, "dataset");
					   ds1 = objdb.ByProcedure("USP_Trn_MilkCrateMgmt_BDS_urben",
                      new string[] { "flag", "FromDate", "ToDate", "AreaID", "RouteId", "Office_ID", "ShiftId", "ItemCat_id" },
                        new string[] { "3", fromdat, todat, ddlLocation.SelectedValue, ddlRoute.SelectedValue, objdb.Office_ID(),ddlShift.SelectedValue,objdb.GetMilkCatId() }, "dataset");

            if (ds1.Tables[0].Rows.Count > 0)
            {
                pnlData.Visible = true;
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();

                DataTable dt1 = new DataTable();
                dt1 = ds1.Tables[0];

                StringBuilder sb = new StringBuilder();


                sb.Append("<table class='table table1-bordered'>");
                sb.Append("<tr>");
                sb.Append("<td style='width:250px;'> From : " + txtFromDate.Text + " To :" + txtToDate.Text + "</td>");
                sb.Append("<td style='text-align:center'>CRATE SUMMARY</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='5' class='text-center'>Route : " + ddlRoute.SelectedItem.Text + "</td>");
                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("<table class='table table1-bordered' style='margin-top:-18px'>");
               
                int Count = dt1.Rows.Count;

                sb.Append("<thead>");
                sb.Append("<td>S.No.</td>");
                sb.Append("<td>Route</td>");
                sb.Append("<td>Crate Issued</td>");
                sb.Append("<td>Crate Return</td>");
                sb.Append("<td>Short / Excess</td>");
                sb.Append("</thead>");
                for (int i = 0; i < Count; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + (i + 1).ToString() + "</td>");
                    sb.Append("<td>" + dt1.Rows[i]["RName"] + "</td>");
                    sb.Append("<td>" + dt1.Rows[i]["IssueCrate"] + "</td>");
                    sb.Append("<td>" + dt1.Rows[i]["ReturnCrate"] + "</td>");
                    sb.Append("<td>" + dt1.Rows[i]["ShortExcessCrate"] + "</td>");

                    sb.Append("</tr>");
                }
                sb.Append("<tr>");
                int ColCount = dt1.Columns.Count - 1;
                for (int i = 0; i <= ColCount; i++)
                {
                    if (i == 0)
                    {
                        sb.Append("<td colspan='2' style='text-align:right;'>Grand Total</td>");
                    }
                    else if (i == 2)
                    {
                        sb.Append("<td>" + Convert.ToInt32(dt1.Compute("SUM([" + "IssueCrate" + "])", string.Empty)) + "</td>");
                    }
                    else if (i == 3)
                    {
                        sb.Append("<td>" + Convert.ToInt32(dt1.Compute("SUM([" + "ReturnCrate" + "])", string.Empty)) + "</td>");
                    }
                    else if (i == 4)
                    {
                        sb.Append("<td>" + Convert.ToInt32(dt1.Compute("SUM([" + "ShortExcessCrate" + "])", string.Empty)) + "</td>");
                    }
                    else
                    {

                    }



                }
                sb.Append("</tr>");
                sb.Append("</table>");
                ViewState["Sb"] = sb.ToString();
                if (dt1 != null) { dt1.Dispose(); }
            }
            else
            {
                pnlData.Visible = true;
                GridView1.DataSource = null;
                GridView1.DataBind();
                ViewState["Sb"] = "";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 3: ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
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
                GetCrateDetails();
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
    #endregion========================================================
    #region=========== init or changed event or gridview events===========================
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0")
        {
            GetRoute();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetCompareDate();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblIssueCrate = (e.Row.FindControl("lblIssueCrate") as Label);
                Label lblReturnCrate = (e.Row.FindControl("lblReturnCrate") as Label);
                Label lblShortExcessCrate = (e.Row.FindControl("lblShortExcessCrate") as Label);
                total_IssuedCrate += Convert.ToInt32(lblIssueCrate.Text);
                total_ReturnCrate += Convert.ToInt32(lblReturnCrate.Text);
                total_ShortExcess += Convert.ToInt32(lblShortExcessCrate.Text);

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalIssueCrate = (e.Row.FindControl("lblTotalIssueCrate") as Label);
                Label lblTotalReturnCrate = (e.Row.FindControl("lblTotalReturnCrate") as Label);
                Label lblTotalShortExcessCrate = (e.Row.FindControl("lblTotalShortExcessCrate") as Label);
                lblTotalIssueCrate.Text = total_IssuedCrate.ToString();
                lblTotalReturnCrate.Text = total_ReturnCrate.ToString();
                lblTotalShortExcessCrate.Text = total_ShortExcess.ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 5: ", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        ddlLocation.SelectedIndex = 0;
        ddlRoute.Items.Clear();
        ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
        pnlData.Visible = false;
        ddlShift.SelectedIndex = 0;
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
    protected void btnPrintRoutWise_Click(object sender, EventArgs e)
    {

        Print.InnerHtml = ViewState["Sb"].ToString();
        // Print.InnerHtml += ViewState["CratePrint"].ToString();
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

    }
    #endregion========================================================
    //public override void VerifyRenderingInServerForm(Control control)
    //{

    //}
    //protected void btnExport_Click(object sender, EventArgs e)
    //{
    //    Response.Clear();

    //    Response.AddHeader("content-disposition", "attachment;filename=CrateSummary.xls");

    //    Response.Charset = "";

    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);

    //    Response.ContentType = "application/vnd.xls";

    //    System.IO.StringWriter stringWrite = new System.IO.StringWriter();

    //    System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

    //    pnlexport.RenderControl(htmlWrite);

    //    Response.Write(stringWrite.ToString());

    //    Response.End();
    //}
}