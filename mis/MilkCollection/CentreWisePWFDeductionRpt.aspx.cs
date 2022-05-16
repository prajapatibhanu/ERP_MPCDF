using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;
public partial class mis_MilkCollection_CentreWisePWFDeductionRpt : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                ds = objdb.ByProcedure("SpAdminOffice",
                           new string[] { "flag", "OfficeType_ID", "Office_ID" },
                           new string[] { "41", objdb.OfficeType_ID(), objdb.Office_ID() }, "dataset");
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    if (objdb.OfficeType_ID() == "2")
                    {
                        ddlOffice.DataSource = ds;
                        ddlOffice.DataTextField = "Office_Name";
                        ddlOffice.DataValueField = "Office_ID";
                        ddlOffice.DataBind();
                        ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
                        ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                    }
                    else
                    {
                        ddlOffice.DataSource = ds;
                        ddlOffice.DataTextField = "Office_Name";
                        ddlOffice.DataValueField = "Office_ID";
                        ddlOffice.DataBind();

                    }

                }
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void FillDetail()
    {
        try
        {
            divreport.InnerHtml = "";
            divprint.InnerHtml = "";
            divshow.Visible = false;
            string[] MonthyearPart = txtFdt.Text.Split('/');
            lblMsg.Text = "";
            string MonthName = Convert.ToDateTime(txtFdt.Text,cult).ToString("MMMM");
            string Month = MonthyearPart[0];
            string Year = MonthyearPart[1];
            int NoofDays = DateTime.DaysInMonth(int.Parse(Year), int.Parse(Month));
			
            ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports", new string[] { "flag", "GenerateFrom_Office_ID", "Month", "Year" }, new string[] { "41", ddlOffice.SelectedValue, Month, Year }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    divshow.Visible = true;
                    StringBuilder sb = new StringBuilder();
                    int Colcount = ds.Tables[0].Columns.Count;
                    int Rowcount = ds.Tables[0].Rows.Count;
                    sb.Append("<div class='table-responsive'>");
                    sb.Append("<table class='table table-bordered' border='1'>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='" + (Colcount+1) + "' style='text-align:center; font-size:18px;'><b>" + Session["Office_Name"].ToString() + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='" + (Colcount + 1) + "' style='text-align:center; font-size:18px;'><b>Centre wise PWF Amt Deduction from DCS 1/- Rs Per Kg Fat Summary Period : " + MonthName + "(" + Year + ")   Amt in Rs</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<th>S.No</th>");
                   
                    sb.Append("<th>Month Yr</th>");
                    
                    for (int i = 0; i < Colcount; i++)
                    {
                        string ColName = ds.Tables[0].Columns[i].ToString();
                        if(ColName != "BillingCycleFromDate" && ColName != "BillingCycleToDate")
                        {
                            sb.Append("<th>" + ColName + "</th>");
                        }
                        
                    }
                    sb.Append("<th>Total</th>");                   
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<th>1</th>");

                    sb.Append("<th>" + MonthName + "- " + Year + "</th>");                   
                    for (int j = 0; j < (Colcount-2); j++)
                    {
                       
                        sb.Append("<th></th>");
                    }
                    sb.Append("<th></th>");
                    sb.Append("</tr>");
                    for (int k = 0; k < Rowcount; k++)
                    {
                        decimal Total = 0;
                        sb.Append("<tr>");
                        sb.Append("<td></td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[k]["BillingCycleFromDate"] + " - " + ds.Tables[0].Rows[k]["BillingCycleToDate"] + "</td>");
                        for (int l = 0; l < Colcount; l++)
                        {
                            string ColName = ds.Tables[0].Columns[l].ToString();
                            if (ColName != "BillingCycleFromDate" && ColName != "BillingCycleToDate")
                            {
                                if (ds.Tables[0].Rows[k][ColName] == DBNull.Value || ds.Tables[0].Rows[k][ColName].ToString() == null)
                                {
                                    Total += 0;
                                    sb.Append("<td>0</td>");
                                }
                                else
                                {
                                    Total += decimal.Parse(ds.Tables[0].Rows[k][ColName].ToString());
                                    sb.Append("<td>" + ds.Tables[0].Rows[k][ColName] + "</td>");
                                    
                                }
                                
                            }
                        }
                        sb.Append("<td>" + Total.ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>Total</b></td>");
                    decimal Grandtotal = 0;
                    for (int m = 0; m < Colcount; m++)
                    {

                        string ColName = ds.Tables[0].Columns[m].ToString();
                        if (ColName != "BillingCycleFromDate" && ColName != "BillingCycleToDate")
                        {
                            Grandtotal += ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>(ColName));
                            sb.Append("<td><b>" + ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal?>(ColName)) + "</b></td>");
                        }

                    }
                    sb.Append("<td><b>" + Grandtotal + "</b></td>");
                    sb.Append("</tr>");
                
                    sb.Append("</table>");
                    sb.Append("</div>");
                    divreport.InnerHtml = sb.ToString();
                    divprint.InnerHtml = sb.ToString();

                }
            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FillDetail();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "CentrewisePWFAmtDeductionReport" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            divprint.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());

            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}