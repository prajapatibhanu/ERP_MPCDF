using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_MilkCollection_RptGeneralGT : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds5, ds6 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                lblMsg.Text = "";
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            lblRptMsg.Text = "";
            lblMsg.Text = "";
            DateTime Fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            string Dt_Date_ShiftE = Fromdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            DateTime Todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string Dt_Date_ShiftM = Todate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
          
            ds1 = objdb.ByProcedure("Usp_RptGenerateGT", new string[] {"flag"
                ,"Office_Id"
                ,"Dt_Date_ShiftE"
                ,"Dt_Date_ShiftM"
                ,"EntryShift_E"
                ,"EntryShift_M" }, new string[] {"0"
                    ,objdb.Office_ID()
                    ,Dt_Date_ShiftE
                    ,Dt_Date_ShiftM
                    ,ddlShift_E.SelectedValue
                    ,ddlShift_M.SelectedValue }, "dataset");
            if(ds1 != null)
            {
                if(ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0 && ds1.Tables[1].Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<table class='table2' style='width:100%'>");
                        sb.Append("<tr>");
                        sb.Append("<th>Milk Purchase</th>");
                        sb.Append("<th>Buffalo</th>");
                        sb.Append("<th>Cow</th>");
                        sb.Append("<th>Total</th>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td>Milk Quantity(Litre)</td>");
                        sb.Append("<td>" + ds1.Tables[1].Rows[0]["Total_sum_MilkIn_Ltr"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[1].Rows[1]["Total_sum_MilkIn_Ltr"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[0].Rows[0]["sum_MilkIn_Ltr"].ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td>FAT(%)</td>");
                        
                        sb.Append("<td>" + ds1.Tables[1].Rows[0]["Avg_FatPer"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[1].Rows[1]["Avg_FatPer"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[0].Rows[0]["Total_Avg_FatPer"].ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td>SNF(%)</td>");

                        sb.Append("<td>" + ds1.Tables[1].Rows[0]["Avg_SNFPer"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[1].Rows[1]["Avg_SNFPer"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[0].Rows[0]["Total_Avg_SNFPer"].ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td>CLR(%)</td>");

                        sb.Append("<td>" + ds1.Tables[1].Rows[0]["Avg_CLRPer"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[1].Rows[1]["Avg_CLRPer"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[0].Rows[0]["Total_Avg_CLRPer"].ToString() + "</td>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td>Amount</td>");

                        sb.Append("<td>" + ds1.Tables[1].Rows[0]["Amount"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[1].Rows[1]["Amount"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[0].Rows[0]["Amount"].ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");

                        dvreport.InnerHtml = sb.ToString();
                    }
                    else
                    {
                        dvreport.InnerHtml = "";
                        lblRptMsg.Text = "No Record Found";
                    }
                }
                else
                {
                    dvreport.InnerHtml = "";
                    lblRptMsg.Text = "No Record Found";
                }
            }
            else
            {
                dvreport.InnerHtml = "";
                lblRptMsg.Text = "No Record Found";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
}