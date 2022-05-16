using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Demand_Rpt_ProductMonthwiseLedger : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetLocation();
                GetRoute();
                GetOfficeDetails();
                txtMonth.Attributes.Add("readonly", "readonly");
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
            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                ViewState["Office_Name"] = ds1.Tables[0].Rows[0]["Office_Name"].ToString();
                ViewState["Office_ContactNo"] = ds1.Tables[0].Rows[0]["Office_ContactNo"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
    }
    private void GetRoute()
    {
        try
        {
                ddlRoute.Items.Clear();
                ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                         new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                         new string[] { "9", objdb.Office_ID(), ddlLocation.SelectedValue, objdb.GetProductCatId() }, "dataset");
                ddlRoute.DataTextField = "RName";
                ddlRoute.DataValueField = "RouteId";
                ddlRoute.DataBind();
                ddlRoute.Items.Insert(0, new ListItem("All", "0"));
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2: ", ex.Message.ToString());
        }
    }
    private void GetDistByRouteID()
    {
        try
        {
            if (ddlRoute.SelectedValue != "0")
            {
                ddlDitributor.DataTextField = "DTName";
                ddlDitributor.DataValueField = "DistributorId";
                ddlDitributor.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "Office_ID", "RouteId", "ItemCat_id" },
                 new string[] { "5", objdb.Office_ID(), ddlRoute.SelectedValue, objdb.GetProductCatId() }, "dataset");
                ddlDitributor.DataBind();

            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Dist ", ex.Message.ToString());
        }
       
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        txtMonth.Text = string.Empty;
        ddlLocation.SelectedIndex = 0;
        ddlDitributor.Items.Clear();
        ddlDitributor.Items.Insert(0, new ListItem("Select", "0"));
        lblMsg.Text = "";
        btnPrint.Visible = false;
        btnExcel.Visible = false;
        div_page_content.InnerHtml = "";
        ddlRoute.SelectedIndex = 0;
        ddlRoute.Items.Clear();
        ddlRoute.Items.Insert(0, new ListItem("Select", "0"));

    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetRoute();
    }
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        if(txtMonth.Text!="")
        {
            GetDistByRouteID();
            string fm = "01/" + txtMonth.Text;
            GetTcsTax(fm);
        }
        else
        {
            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "warning! : ", " Select momth");
        }
        
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GenerateReport();
        }
    }
    public static DateTime LastDayOfMonth(DateTime dt)
    {
        DateTime ss = new DateTime(dt.Year, dt.Month, 1);
        return ss.AddMonths(1).AddDays(-1);
    }
    protected void GenerateReport()
    {
        try
        {
            lblMsg.Text = "";
            btnPrint.Visible = false;
            btnExcel.Visible = false;
            div_page_content.InnerHtml = "";
            string fm = "01/" + txtMonth.Text;
            //  Code for current month date
            DateTime fmonth = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
            string fromnonth = fmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            DateTime addmonth = fmonth.AddMonths(1);
            DateTime minusday = addmonth.AddDays(-1);

            string tmnth = minusday.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tmonth = DateTime.ParseExact(tmnth, "dd/MM/yyyy", culture);
            string tomonth = tmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            string tm = tmonth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            // End of Current month date



            ds2 = objdb.ByProcedure("USP_Trn_ProductDM"
                , new string[] { "flag", "FromDate", "ToDate", "ItemCat_id", "RouteId", "DistributorId", "Office_ID" }
                , new string[] { "11", fromnonth, tomonth, objdb.GetProductCatId()
                                  ,ddlRoute.SelectedValue,ddlDitributor.SelectedValue, objdb.Office_ID() }, "dataset");
            if (ds2 != null && ds2.Tables.Count > 0)
            {
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    lblMsg.Text = string.Empty;
                    StringBuilder sb = new StringBuilder();
                    decimal Total_GV = 0, Total_V = 0, Total_CGST = 0, Total_SGST = 0, Total_TCSTax = 0;


                    int Count = ds2.Tables[0].Rows.Count;
                    sb.Append("<table class='table'>");
                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: center;' colspan='8'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: Left;' colspan='8'><b>" + ddlDitributor.SelectedItem.Text + "</b></br>" + ddlRoute.SelectedItem.Text + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: Left;' colspan='8'>" + fm + " To " + tm + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>Date</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>Particulars</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>DM ChallanNo.</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>Value</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>Gross Value</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>CGST</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>SGST</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>Tcs on Sales @ " + ViewState["Tval"].ToString() + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                    string Date = "";
                    for (int i = 0; i < Count; i++)
                    {

                        sb.Append("<tr>");
                        sb.Append("<td style='border:1px solid black;text-align: center'>" + ds2.Tables[0].Rows[i]["Date"] + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + ddlDitributor.SelectedItem.Text + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["DMChallanNo"] + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + ds2.Tables[0].Rows[i]["AmountWithoutGST"] + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + Convert.ToDecimal(ds2.Tables[0].Rows[i]["GrossTotal"]).ToString("0.00") + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + Convert.ToDecimal(ds2.Tables[0].Rows[i]["CGSTAmt"]).ToString("0.00") + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + Convert.ToDecimal(ds2.Tables[0].Rows[i]["SGSTAmt"]).ToString("0.00") + "</td>");
                        sb.Append("<td style='border:1px solid black;text-align: center;'>" + Convert.ToDecimal(ds2.Tables[0].Rows[i]["TCSTaxAmt"]).ToString("0.00") + "</td>");
                        sb.Append("</tr>");
                      //  urban
                        Total_V += Convert.ToDecimal(ds2.Tables[0].Rows[i]["AmountWithoutGST"]);
                        Total_GV += Convert.ToDecimal(ds2.Tables[0].Rows[i]["GrossTotal"]);
                        Total_CGST += Convert.ToDecimal(ds2.Tables[0].Rows[i]["CGSTAmt"]);
                        Total_SGST += Convert.ToDecimal(ds2.Tables[0].Rows[i]["SGSTAmt"]);
                        Total_TCSTax += Convert.ToDecimal(ds2.Tables[0].Rows[i]["TCSTaxAmt"]);

                        Date = ds2.Tables[0].Rows[i]["Date"].ToString();
                    }
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;text-align: center;' colspan='3'><b>Total</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_V + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_GV.ToString("0.00") + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_CGST.ToString("0.00") + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_SGST.ToString("0.00") + "</b></td>");
                    sb.Append("<td style='border:1px solid black;text-align: center;'><b>" + Total_TCSTax.ToString("0.00") + "</b></td>");
                    sb.Append("</tr>");

                  
                    sb.Append("</table>");
                    div_page_content.InnerHtml = sb.ToString();
                    btnPrint.Visible = true;
                    btnExcel.Visible = true;
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning !  ", "No Record Found.");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
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
            Response.AddHeader("content-disposition", "attachment; filename=" + "ProductCrate" + ddlRoute.SelectedItem.Text + DateTime.Now + ".xls");
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


    private void GetTcsTax(string date)
    {
        try
        {
            if (ddlDitributor.SelectedValue != "0")
            {
                ViewState["Tval"] = "";
                DateTime Ddate = DateTime.ParseExact(date, "dd/MM/yyyy", culture);
                string deld = Ddate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                ds3 = objdb.ByProcedure("USP_Mst_TcsTax",
                     new string[] { "Flag", "Office_ID", "EffectiveDate", "DistributorId" },
                       new string[] { "0", objdb.Office_ID(), deld, ddlDitributor.SelectedValue }, "dataset");

                if (ds3.Tables[0].Rows.Count > 0)
                {
                    ViewState["Tval"] = ds3.Tables[0].Rows[0]["Tval"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : TCS TAX ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }
}