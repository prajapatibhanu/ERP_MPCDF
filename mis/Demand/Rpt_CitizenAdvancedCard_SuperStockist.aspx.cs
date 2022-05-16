using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Globalization;
using System.Text;
using System.Linq;

public partial class mis_Demand_Rpt_CitizenAdvancedCard_SuperStockist : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds5, ds2, ds3 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetShift();
                GetOfficeDetails();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Shift ", ex.Message.ToString());
        }
    }
    private void GetAdvancedCardDetailsSSWise()
    {
        try
        {
            lblMsg.Text = "";
            string fm = "16/" + txtFromMonth.Text;
            string tm = "15/" + txtToMonth.Text;
            DateTime fmonth = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
            string fromnonth = fmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            DateTime tmonth = DateTime.ParseExact(tm, "dd/MM/yyyy", culture);
            string tomonth = tmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds5 = objdb.ByProcedure("USP_Mst_CitizenAdvancedCard_Rpt",
                     new string[] { "flag", "SuperStockistId", "Shift_id", "ItemCat_id", "FromDate", "ToDate", "Office_ID" },
                       new string[] { "3", objdb.createdBy(), ddlShift.SelectedValue, "3", fromnonth, tomonth, objdb.Office_ID() }, "dataset");

            if (ds5.Tables[0].Rows.Count != 0)
            {
                btnPrintRoutWise.Visible = true;
                int Count = ds5.Tables[0].Rows.Count;
                int ColCount = ds5.Tables[0].Columns.Count;
                StringBuilder sb1 = new StringBuilder();

                sb1.Append("<table style='width:100%; height:100%'>");
                sb1.Append("<tr>");
                sb1.Append("<td style='padding: 2px 5px;text-align:center' colspan='2'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='padding: 2px 5px;text-align: lefg;'><b>Section  :- Retailer Advanced Card Report (In Pkt)</b></td>");
                sb1.Append("<td style='padding: 2px 5px;text-align: right;'><b>Period  :-" + fm + " To " + tm + "</b></td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                sb1.Append("<table class='table'>");
                sb1.Append("<thead>");
                sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>S.no</b></td>");
                for (int j = 0; j < ColCount; j++)
                {

                    if (ds5.Tables[0].Columns[j].ToString() != "BoothId")
                    {
                        string ColName = ds5.Tables[0].Columns[j].ToString();
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");

                    }

                }
                sb1.Append("</thead>");

                for (int i = 0; i < Count; i++)
                {

                    sb1.Append("<tr>");
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + (i + 1).ToString() + "</td>");
                    for (int K = 0; K < ColCount; K++)
                    {
                        if (ds5.Tables[0].Columns[K].ToString() != "BoothId")
                        {
                            string ColName = ds5.Tables[0].Columns[K].ToString();
                            sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + (ds5.Tables[0].Rows[i][ColName].ToString() == "" ? "0" : ds5.Tables[0].Rows[i][ColName].ToString()) + "</td>");

                        }

                    }
                    sb1.Append("</tr>");

                }
                sb1.Append("<tr>");
                sb1.Append("<td style=' border: 1px solid #000000 !important;text-align:center; 'colspan='2'><b>Total</b></td>");
                DataTable dt = new DataTable();
                dt = ds5.Tables[0];
                int sum11 = 0;
                //  decimal dsum11 = 0;
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() != "BoothId" && column.ToString() != "Retailer Name")
                    {

                        sum11 = dt.AsEnumerable().Sum(r => r.Field<int?>("" + column + "") ?? 0);
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + sum11.ToString() + "</b></td>");



                    }
                }
                sb1.Append("</tr>");
                sb1.Append("</table>");
                div_page_content.InnerHtml = sb1.ToString();
                ViewState["CitizenAdvanceCardDetails"] = sb1.ToString();
                if (dt != null) { dt.Dispose(); }

            }
            else
            {
                div_page_content.InnerHtml = "";
                ViewState["CitizenAdvanceCardDetails"] = "";
                btnPrintRoutWise.Visible = false;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! :", "No Record Found");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Citizen Advanced Card Report ", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetAdvancedCardDetailsSSWise();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        btnPrintRoutWise.Visible = false;
        div_page_content.InnerHtml = "";
        ViewState["CitizenAdvanceCardDetails"] = "";
    }
    protected void btnPrintRoutWise_Click(object sender, EventArgs e)
    {

        Print.InnerHtml = ViewState["CitizenAdvanceCardDetails"].ToString();
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
}