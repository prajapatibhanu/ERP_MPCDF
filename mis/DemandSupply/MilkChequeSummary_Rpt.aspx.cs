using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_DemandSupply_MilkChequeSummary_Rpt : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds2,ds6 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);

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
            FillRouteWiseMilkAmountDetails();
        }
    }

    private void FillRouteWiseMilkAmountDetails()
    {
        try
        {
            lblMsg.Text = string.Empty;
            div_page_content.InnerHtml = "";
            string routeid = "";
            int rowcount = 0, chk = 0 ;
            decimal totalamt = 0, tcstamt = 0, tdstamt = 0, fpaybleamt = 0;

            StringBuilder sb1 = new StringBuilder();
            sb1.Append("<table style='width:100%; height:100%' class='table'>");
            sb1.Append("<thead class='header'>");
            sb1.Append("<tr>");
            sb1.Append("<td colspan='7' style='text-align: center;font-size: 14px;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
            sb1.Append("</tr>");
            sb1.Append("<tr>");
            sb1.Append("<td colspan='7' style='text-align: left;font-size: 14px;'><b>Check summary  for the Period  :-" + txtDeliveryDate.Text + " To " + txtDeliveryDate.Text + "</b></td>");
            sb1.Append("</tr>");
            sb1.Append("<tr>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;width:10%'><b>S.No.</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;text-align : center;'><b>Route</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;text-align : center;'><b>Ditributor</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;text-align : center;'><b>Amount</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;text-align : center;'><b>TCS Tax</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;text-align : center;'><b>TCS Tax Amount</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;text-align : center;'><b>TDS Tax</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;text-align : center;'><b>TDS Tax Amount</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;text-align : center;'><b>Payble Amount</b></td>");
            sb1.Append("</tr>");
            sb1.Append("</thead>");
            
            foreach (ListItem item in ddlRoute.Items)
            {
                if (item.Selected)
                {
                    chk = 1;
                    routeid = item.Value;
                    DateTime odate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
        string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


        ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandInvoice",
                     new string[] { "flag", "Delivary_Date", "ItemCat_id", "Office_ID", "RouteId", "OrganizationId", "AreaId", "DelivaryShift_id", "FromDate", "ToDate" },
                       new string[] { "8", deliverydate.ToString(), objdb.GetMilkCatId(), objdb.Office_ID(), routeid.ToString(), "0", ddlLocation.SelectedValue, "0", deliverydate.ToString(), deliverydate.ToString() }, "dataset");
        string pstatus = "";
        if (ds6.Tables[0].Rows.Count > 0 && ds6.Tables[1].Rows.Count > 0 && ds6 != null)
        {
            ++rowcount;

            sb1.Append("<tr>");
            sb1.Append("<td style='font-family:Arial;font-size: 12px;border-top:1px dashed black; width:10%'><b>" + rowcount + "</b></td>");
            sb1.Append("<td style='font-family:Arial;font-size: 12px;border-top:1px dashed black;text-align : center;'><b>" + ds6.Tables[0].Rows[0]["RName"] + "</b></td>");
            sb1.Append("<td style='font-family:Arial;font-size: 12px;border-top:1px dashed black;text-align : center;'><b>" + ds6.Tables[0].Rows[0]["DName"] + "</b></td>");
            sb1.Append("<td style='font-family:Arial;font-size: 12px;border-top:1px dashed black;text-align : center;'><b>" + ds6.Tables[0].Rows[0]["MilkAmount"] + "</b></td>");
            sb1.Append("<td style='font-family:Arial;font-size: 12px;border-top:1px dashed black;text-align : center;'><b>" + ds6.Tables[1].Rows[0]["Tval"] + "</b></td>");
            //sb1.Append("<td style='font-family:Arial;font-size: 12px;border-top:1px dashed black;text-align : center;'><b>" + ((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmount"]) * Convert.ToDecimal(ds6.Tables[1].Rows[0]["Tval"])) / 100).ToString("0.000") + "</b></td>");
            sb1.Append("<td style='font-family:Arial;font-size: 12px;border-top:1px dashed black;text-align : center;'><b>" + ((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmountNew"]) * Convert.ToDecimal(ds6.Tables[1].Rows[0]["Tval"])) / 100).ToString("0.000") + "</b></td>");
            sb1.Append("<td style='font-family:Arial;font-size: 12px;border-top:1px dashed black;text-align : center;'><b>" + ds6.Tables[1].Rows[0]["Tdsval"] + "</b></td>");
            //sb1.Append("<td style='font-family:Arial;font-size: 12px;border-top:1px dashed black;text-align : center;'><b>" + ((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmount"]) * Convert.ToDecimal(ds6.Tables[1].Rows[0]["Tdsval"])) / 100).ToString("0.000") + "</b></td>");
            sb1.Append("<td style='font-family:Arial;font-size: 12px;border-top:1px dashed black;text-align : center;'><b>" + ((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmountNew"]) * Convert.ToDecimal(ds6.Tables[1].Rows[0]["Tdsval"])) / 100).ToString("0.000") + "</b></td>");
            //sb1.Append("<td style='font-family:Arial;font-size: 12px;border-top:1px dashed black;text-align : center;'><b>" + ((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmount"]) + ((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmount"]) * Convert.ToDecimal(ds6.Tables[1].Rows[0]["Tval"])) / 100)) - ((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmount"]) * Convert.ToDecimal(ds6.Tables[1].Rows[0]["Tdsval"])) / 100)).ToString("0") + "</b></td>");
            sb1.Append("<td style='font-family:Arial;font-size: 12px;border-top:1px dashed black;text-align : center;'><b>" + ((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmount"]) + ((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmountNew"]) * Convert.ToDecimal(ds6.Tables[1].Rows[0]["Tval"])) / 100)) - ((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmountNew"]) * Convert.ToDecimal(ds6.Tables[1].Rows[0]["Tdsval"])) / 100)).ToString("0") + "</b></td>");
           
            sb1.Append("</tr>");
            totalamt += Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmount"]);
            //tcstamt += Convert.ToDecimal(((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmount"]) * Convert.ToDecimal(ds6.Tables[1].Rows[0]["Tval"])) / 100).ToString("0.000"));
            //tdstamt += Convert.ToDecimal(((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmount"]) * Convert.ToDecimal(ds6.Tables[1].Rows[0]["Tdsval"])) / 100).ToString("0.000"));
            //fpaybleamt += Convert.ToDecimal(((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmount"]) + ((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmount"]) * Convert.ToDecimal(ds6.Tables[1].Rows[0]["Tval"])) / 100)) - (Convert.ToDecimal(((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmount"]) * Convert.ToDecimal(ds6.Tables[1].Rows[0]["Tdsval"])) / 100).ToString("0.000")))).ToString("0"));
            tcstamt += Convert.ToDecimal(((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmountNew"]) * Convert.ToDecimal(ds6.Tables[1].Rows[0]["Tval"])) / 100).ToString("0.000"));
            tdstamt += Convert.ToDecimal(((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmountNew"]) * Convert.ToDecimal(ds6.Tables[1].Rows[0]["Tdsval"])) / 100).ToString("0.000"));
            fpaybleamt += Convert.ToDecimal(((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmount"]) + ((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmountNew"]) * Convert.ToDecimal(ds6.Tables[1].Rows[0]["Tval"])) / 100)) - (Convert.ToDecimal(((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmountNew"]) * Convert.ToDecimal(ds6.Tables[1].Rows[0]["Tdsval"])) / 100).ToString("0.000")))).ToString("0"));
          }
        }
            
            }
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;text-align : center;'></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;text-align : center;'></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;text-align : center;'><b>Grand Total</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;text-align : center;'><b>" + totalamt + "</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;text-align : center;'></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;text-align : center;'><b>" + tcstamt + "</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;text-align : center;'></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;text-align : center;'><b>" + tdstamt + "</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;text-align : center;'><b>" + fpaybleamt + "</b></td>");
            sb1.Append("</table>");

            if (chk == 1)
            {
               if(rowcount==0)
               {
                   sb1.Append("<tr>");
                   sb1.Append("<td style='font-family: Lucida Console;font-size: 14px;border-top:1px dashed black;'><b>No Record found.</b></td>");
                   sb1.Append("</tr>");
                   btnPrint.Visible = false;
                   btnExportAll.Visible = false;
               }
               else
               {
                   btnPrint.Visible = true;
                   btnExportAll.Visible = true;
               }
            }
            else
            {
                
                sb1.Append("<tr>");
                sb1.Append("<td style='font-family: Lucida Console;font-size: 14px;border-top:1px dashed black;'><b>No Record found.</b></td>");
                sb1.Append("</tr>");
                btnPrint.Visible = false;
                btnExportAll.Visible = false;
            }
            
            div_page_content.InnerHtml = sb1.ToString();
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
        finally
        {
            if (ds6 != null) { ds6.Dispose(); }
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
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "CheckSummary-" + ddlLocation.SelectedItem.Text + DateTime.Now + ".xls");
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
    #endregion===========================
}