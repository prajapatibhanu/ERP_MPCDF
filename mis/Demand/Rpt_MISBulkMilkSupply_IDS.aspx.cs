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

public partial class mis_Demand_Rpt_MISBulkMilkSupply_IDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds2, ds3, ds4, ds5 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetCategory();
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Text = Date;
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Text = Date;
                txtToDate.Attributes.Add("readonly", "readonly");
                GetOfficeDetails();
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
            ddlItemCategory.DataTextField = "ItemCatName";
            ddlItemCategory.DataValueField = "ItemCat_id";
            ddlItemCategory.DataSource = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlItemCategory.DataBind();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    private void GetMISBulkMilkSupply(string fromdate, string todate)
    {
        try
        {
           

            ds2 = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
                             new string[] { "flag", "FromDate", "ToDate", "ItemCat_id", "Office_ID" },
                               new string[] { "9", fromdate, todate,ddlItemCategory.SelectedValue, objdb.Office_ID() }, "dataset");

            if (ds2.Tables[0].Rows.Count != 0)
            {

                btnPrint.Visible = true;
                btnExcel.Visible = true;

                StringBuilder sb1 = new StringBuilder();
                int Count1 = ds2.Tables[0].Rows.Count;
                int ColCount1 = ds2.Tables[0].Columns.Count;

                sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center;' colspan='" + (ColCount1) + "'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center;' colspan='" + (ColCount1) + "'><b>MIS BULK " + ddlItemCategory.SelectedItem.Text + " SUPPLY REPORT</b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center;' colspan='" + (ColCount1) + "'><bDate </b>" + txtFromDate.Text + " - " + txtFromDate.Text + "</td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                sb1.Append("<table class='table1' style='width:100%;'>");
                sb1.Append("<thead>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>S.N0.</b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>City / Party Name</b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Qty (In KG)</b></td>");
                sb1.Append("</tr>");
                sb1.Append("</thead>");
              
                decimal inkgs = 0;
                for (int i = 0; i < Count1; i++)
                {
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;text-align:center;'><b>" + (i+1) + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;text-align:left;'><b>" + ds2.Tables[0].Rows[i]["TransferTo"] + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["QuantityInKG"] + "</td>");
                        sb1.Append("</tr>");

                        inkgs += Convert.ToDecimal(ds2.Tables[0].Rows[i]["QuantityInKG"]);
                }
                sb1.Append("<tr>");
                sb1.Append("<td style='border:1px solid black;text-align:right;' colspan='2'><b>Total<b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>" + inkgs.ToString("0.00") + "</b></td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                div_page_content.InnerHtml = sb1.ToString();
                Print.InnerHtml = sb1.ToString();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", " No Record Found.");
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Product DM ", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    private void GetCompareDate()
    {
        try
        {
            string myStringfromdat = txtFromDate.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string fdat = fdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            
            string myStringtodate = txtToDate.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string tdat = tdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            GetMISBulkMilkSupply(fdat, tdat);
            if (fdate <= tdate)
            {
                lblMsg.Text = string.Empty;
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
                GetMISBulkMilkSupply(fdat, tdat);
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblMsg.Text = string.Empty;

            GetCompareDate();
           

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        btnPrint.Visible = false;
        btnExcel.Visible = false;
        div_page_content.InnerHtml = "";
        Print.InnerHtml = "";
        lblMsg.Text = string.Empty;
       
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "CityWiseMISReport" + DateTime.Now + ".xls");
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
}