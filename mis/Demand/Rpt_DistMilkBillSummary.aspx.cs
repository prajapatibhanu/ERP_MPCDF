using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Demand_Rpt_DistMilkBillSummary : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds2,ds3,ds4, ds6 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.GetItemCat_id() != null)
        {
            if (!Page.IsPostBack)
            {
                GetShift();
                GetOfficeDetails();
                GetRouteIDByDistributor();
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
            ds4 = objdb.ByProcedure("USP_Mst_ShiftMaster",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds4.Tables[0].Rows.Count != 0)
            {
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataSource = ds4.Tables[0];
                ddlShift.DataBind();
                ddlShift.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlShift.Items.Insert(0, new ListItem("No Record found", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
        finally
        {
            if (ds4 != null) { ds4.Dispose(); }
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
    private void GetRouteIDByDistributor()
    {
        try
        {
            // ds3 = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                     // new string[] { "flag", "Office_ID", "DistributorId" },
                       // new string[] { "4", objdb.Office_ID(), objdb.createdBy() }, "dataset");
			ds3 = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                    new string[] { "flag", "Office_ID", "DistributorId", "ItemCat_id" },
                      new string[] { "4", objdb.Office_ID(), objdb.createdBy(), objdb.GetItemCat_id() }, "dataset");		   

            if (ds3.Tables[0].Rows.Count != 0)
            {
                ViewState["RouteId"] = ds3.Tables[0].Rows[0]["RouteId"].ToString();
            }
            else
            {
                ViewState["RouteId"] = "0";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error route ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
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
            int rowcount = 0, chk = 0;
            decimal totalamt = 0, tcstamt = 0, fpaybleamt = 0;

            StringBuilder sb1 = new StringBuilder();
            sb1.Append("<table style='width:100%; height:100%' class='table'>");
            sb1.Append("<thead class='header'>");
            sb1.Append("<tr>");
            sb1.Append("<td colspan='7' style='text-align: center;font-size: 14px;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
            sb1.Append("</tr>");
            sb1.Append("<tr>");
            sb1.Append("<td colspan='6' style='text-align: left;font-size: 14px;'><b>Check summary  for the Period  :-" + txtDeliveryDate.Text + " To " + txtDeliveryDate.Text + "</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;width:10%'> <b>Shift :- " + ddlShift.SelectedItem.Text + "</b></td>");
            sb1.Append("</tr>");
            sb1.Append("<tr>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;width:10%'><b>S.No.</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;'><b>Route</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;'><b>Ditributor</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;'><b>Amount</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;'><b>TCS Tax</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;'><b>TCS Tax Amount</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;'><b>Payble Amount</b></td>");
            sb1.Append("</tr>");
            sb1.Append("</thead>");

           
                   
                    DateTime odate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
                    string deliverydate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


                    ds6 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandInvoice",
                                 new string[] { "Flag", "Delivary_Date", "ItemCat_id", "Office_ID", "RouteId", "OrganizationId", "DelivaryShift_id" },
                                   new string[] { "10", deliverydate.ToString(), objdb.GetMilkCatId(), objdb.Office_ID(), ViewState["RouteId"].ToString(), "0",ddlShift.SelectedValue }, "dataset");
                    string pstatus = "";
                    if (ds6.Tables[0].Rows.Count > 0 && ds6 != null)
                    {
                        ++rowcount;

                        sb1.Append("<tr>");
                        sb1.Append("<td style='font-family:Arial;font-size: 12px;border-top:1px dashed black; width:10%'><b>" + rowcount + "</b></td>");
                        sb1.Append("<td style='font-family:Arial;font-size: 12px;border-top:1px dashed black;'><b>" + ds6.Tables[0].Rows[0]["RName"] + "</b></td>");
                        sb1.Append("<td style='font-family:Arial;font-size: 12px;border-top:1px dashed black;'><b>" + ds6.Tables[0].Rows[0]["DName"] + "</b></td>");
                        sb1.Append("<td style='font-family:Arial;font-size: 12px;border-top:1px dashed black;'><b>" + ds6.Tables[0].Rows[0]["MilkAmount"] + "</b></td>");
                        sb1.Append("<td style='font-family:Arial;font-size: 12px;border-top:1px dashed black;'><b>" + ds6.Tables[0].Rows[0]["Tval"] + "</b></td>");
                        sb1.Append("<td style='font-family:Arial;font-size: 12px;border-top:1px dashed black;'><b>" + ((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmount"]) * Convert.ToDecimal(ds6.Tables[0].Rows[0]["Tval"])) / 100).ToString("0.000") + "</b></td>");
                        sb1.Append("<td style='font-family:Arial;font-size: 12px;border-top:1px dashed black;'><b>" + (Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmount"]) + ((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmount"]) * Convert.ToDecimal(ds6.Tables[0].Rows[0]["Tval"])) / 100)).ToString("0") + "</b></td>");
                        sb1.Append("</tr>");
                        totalamt += Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmount"]);
                        tcstamt += Convert.ToDecimal(((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmount"]) * Convert.ToDecimal(ds6.Tables[0].Rows[0]["Tval"])) / 100).ToString("0.000"));
                        fpaybleamt += Convert.ToDecimal((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmount"]) + ((Convert.ToDecimal(ds6.Tables[0].Rows[0]["MilkAmount"]) * Convert.ToDecimal(ds6.Tables[0].Rows[0]["Tval"])) / 100)).ToString("0"));
                    }
               

           
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;'></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;'></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;'><b>Grand Total</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;'><b>" + totalamt + "</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;'></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;'><b>" + tcstamt + "</b></td>");
            sb1.Append("<td style='font-size: 13px;border-top:1px dashed black; border-bottom:1px dashed black;'><b>" + fpaybleamt + "</b></td>");
            sb1.Append("</table>");

            if (ds6.Tables[0].Rows.Count>0)
            {
                if (ds6.Tables[0].Rows.Count == 0)
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
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "BillSummary-" + objdb.Emp_Name() + DateTime.Now + ".xls");
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