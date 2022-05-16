using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_DemandSupply_Rpt_Distributor_Merge_dues_BGJDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds2, ds3, ds4, ds5, ds7 = new DataSet();
    decimal Totalvalue = 0, ClosingBalance = 0;
    decimal TotalMvalue = 0, TotalPvalue = 0, TotalMPay = 0, TotalPPay = 0, transpotttotal = 0;
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
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
   
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
                MergePayment_Report();


            }
        }
        catch
        {

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
    private void MergePayment_Report()
    {
        try
        {
            DateTime date1 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            string fromdate = date1.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            DateTime date2 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string todate = date2.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            lblMsg.Text = "";
           
            

            ds2 = objdb.ByProcedure("USP_Trn_merge_dues_report_New",
                     new string[] { "flag", "FromDate1", "ToDate1 ", "AreaID", "RouteId", "Office_ID", "DistributorId" },
                       new string[] { "1", fromdate.ToString(), todate.ToString(),"0", "0", objdb.Office_ID(), objdb.createdBy() }, "dataset");
            if (ds2.Tables.Count != 0)
            {
                if (ds2.Tables[0].Rows.Count != 0)
                {
                    pnlData.Visible = true;
                    btnExportAll.Visible = true;
                    DataTable dt = new DataTable();
                    dt = ds2.Tables[0];


                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table style='width:100%; height:100%'>");
                    sb.Append("<tr>");
                    sb.Append("<td style='padding-left: 250px;text-align: center;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='text-align: left;'><b>Date  :-" + txtFromDate.Text + " To " + txtToDate.Text + "</b></td>");
                    sb.Append("<td style='text-align: center;'><b>Merge Dues Report</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='padding: 2px 5px;'></td>");
                    sb.Append("<td style='padding: 2px 5px;'></td>");

                    sb.Append("<td style='padding: 2px 5px;'></td>");
                    sb.Append("</tr>");

                    sb.Append("</table>");
                    sb.Append("<table class='table1'>");

                    int Count = ds2.Tables[0].Rows.Count;

                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black;width:50px'><b>S.No.</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Route</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Party Name</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Date</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Opening (Milk+Product)</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Milk Value</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Milk Payment</b></td>");
                    if(objdb.Office_ID()=="5")
                    {
                        sb.Append("<td style='border:1px solid black;'><b>Transport Amt </b></td>");
                    }
                    else
                    {
                      
                    }
                    
                    sb.Append("<td style='border:1px solid black;'><b>Product Value</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>Product Payment</b></td>");

                    sb.Append("<td style='border:1px solid black;'><b>Closing (Milk+Product) </b></td>");
                    sb.Append("</tr>");





                    for (int i = 0; i < Count; i++)
                    {
                        decimal closing = Math.Round((Convert.ToDecimal(ds2.Tables[0].Rows[i]["Opening_M"])) + (Convert.ToDecimal(ds2.Tables[0].Rows[i]["Opening_P"])) + (Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount_M"])) + (Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount_P"])) - ((Convert.ToDecimal(ds2.Tables[0].Rows[i]["PaidAmt_M"])) + (Convert.ToDecimal(ds2.Tables[0].Rows[i]["PaidAmt_P"]))), 2);
                        //  DateTime delivery_date = DateTime.ParseExact(ds2.Tables[0].Rows[i]["Delivary_Date"].ToString(), "dd/MM/yyyy", culture);

                        sb.Append("<tr>");
                        sb.Append("<td style='border:1px solid black;width:50px'>" + (i + 1).ToString() + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["RName"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["DName"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["ddate"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + (Convert.ToDecimal(ds2.Tables[0].Rows[i]["Opening_M"]) + Convert.ToDecimal(ds2.Tables[0].Rows[i]["Opening_P"])) + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["Amount_M"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaidAmt_M"] + "</td>");
                        if (objdb.Office_ID() == "5")
                        {
                            sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["trnsportcharge"] + "</td>");
                        }
                        else
                        {
                            
                        }
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["Amount_P"] + "</td>");
                        sb.Append("<td style='border:1px solid black;'>" + ds2.Tables[0].Rows[i]["PaidAmt_P"] + "</td>");


                        sb.Append("<td style='border:1px solid black;'>" + closing.ToString("0.00") + "</td>");



                        sb.Append("</tr>");
                        TotalMvalue += (Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount_M"]));
                        TotalPvalue += (Convert.ToDecimal(ds2.Tables[0].Rows[i]["Amount_P"]));
                        TotalMPay += (Convert.ToDecimal(ds2.Tables[0].Rows[i]["PaidAmt_M"]));
                        TotalPPay += (Convert.ToDecimal(ds2.Tables[0].Rows[i]["PaidAmt_P"]));
                        if (objdb.Office_ID() == "5")
                        {
                            transpotttotal += (Convert.ToDecimal(ds2.Tables[0].Rows[i]["trnsportcharge"]));
                        }
                        else
                        {
                            
                        }


                    }
                    sb.Append("<tr>");
                    sb.Append("<td colspan='5'><b>Total</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>" + TotalMvalue + "</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>" + TotalMPay + "</b></td>");
                    if (objdb.Office_ID() == "5")
                    {
                        sb.Append("<td style='border:1px solid black;'><b>" + transpotttotal + "</b></td>");
                    }
                    else
                    {
                       
                    }
                    sb.Append("<td style='border:1px solid black;'><b>" + TotalPvalue + "</b></td>");
                    sb.Append("<td style='border:1px solid black;'><b>" + TotalPPay + "</b></td>");


                    sb.Append("<td></td>");


                    sb.Append("</tr>");
                    sb.Append("</table>");

                    div_page_content.InnerHtml = sb.ToString();
                    Print.InnerHtml = sb.ToString();



                }
                else
                {
                    pnlData.Visible = false;
                    btnExportAll.Visible = false;
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning!", "No Record Found.");

                }
            }
            else
            {
                pnlData.Visible = false;
                btnExportAll.Visible = false;
                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning!", "No Record Found.");

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



    protected void btnClear_Click(object sender, EventArgs e)
    {

        Response.Redirect("Merge_dues_report_New.aspx");
    }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + txtFromDate.Text + "-" + txtToDate.Text + "-" + objdb.Emp_Name() + "-" + "DuesPaymentRpt.xls");
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
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
    }
}