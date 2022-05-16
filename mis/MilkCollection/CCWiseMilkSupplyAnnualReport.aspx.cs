using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using System.Web.UI;
using System.Web;

public partial class mis_MilkCollection_CCWiseMilkSupplyAnnualReport : System.Web.UI.Page
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
                GetCCDetails();
                
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
    protected void GetCCDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                     new string[] { "flag", "I_OfficeID", "I_OfficeTypeID" },
                     new string[] { "3", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlccbmcdetail.DataTextField = "Office_Name";
                        ddlccbmcdetail.DataValueField = "Office_ID";


                        ddlccbmcdetail.DataSource = ds;
                        ddlccbmcdetail.DataBind();

                        if (objdb.OfficeType_ID() == "2")
                        {
                            ddlccbmcdetail.Items.Insert(0, new ListItem("Select", "0"));
                        }
                        else
                        {
                            ddlccbmcdetail.SelectedValue = objdb.Office_ID();
                            //GetMCUDetails();
                            ddlccbmcdetail.Enabled = false;
                        }


                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
  
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            dvReport.InnerHtml = "";
            divprint.InnerHtml = "";
            
            lblMsg.Text = "";
            divshow.Visible = true;
            
            StringBuilder sb = new StringBuilder();
            int ColumnCount = 0;
            int RowCount = 0;
            ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports", new string[] { "flag", "Year", "CCID"}, new string[] { "31", txtFdt.Text, ddlccbmcdetail.SelectedValue}, "dataset");
            DataTable dt = new DataTable();
            dt.Columns.Add("MonthName", typeof(string));
            dt.Columns.Add("Qty", typeof(decimal));
            
            if(ds != null && ds.Tables.Count > 0)
            {
                divshow.Visible = true;
                 ColumnCount = ds.Tables[0].Columns.Count;
                 RowCount = ds.Tables[0].Rows.Count;
                 sb.Append("<div class='table-responsive'>");
                 sb.Append("<table class='table'>");
				 sb.Append("<thead class='report-header'>");
                 sb.Append("<tr>");
                 sb.Append("<td colspan=" + ColumnCount + 1 + " style='text-align:center;  font-size:18px;'><b>" + Session["Office_Name"].ToString() + "</b></td>");
                 sb.Append("</tr>");
                 sb.Append("<tr>");
                 sb.Append("<td colspan=" + ColumnCount + 1 + " style='text-align:center; font-size:18px;'><b>Milk Supply Anually Report - CC:-" + ddlccbmcdetail.SelectedItem.Text + "   FY :"+txtFdt.Text+"</b></td>");
                 sb.Append("</tr>");
                sb.Append("<tr  >");
                sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black;'>S.No</td>");
                sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black;'><b>SocietyCode & Name</b></td>");
               
                for (int i = 0; i < ColumnCount; i++)
                {
                    if (ds.Tables[0].Columns[i].ToString() != "Office_Name")
                    {
                        
                        sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; text-align:center;' colspan='2'><b>" + ds.Tables[0].Columns[i].ToString() + "</b></td>");
                    }
                }
                sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; text-align:center;' colspan='2'><b>Total</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr  >");
                sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black;'></td>");
                sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black;'></td>");
                for (int i = 0; i < ColumnCount; i++)
                {
                    if (ds.Tables[0].Columns[i].ToString() != "Office_Name")
                    {

                        sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black;'><b>No of Days</b></td>");
                        sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black;'><b>QtyInKg</b></td>");
                    }
                }


                sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black;'><b>No of Days</b></td>");
                sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black;'><b>QtyInKg</b></td>");
				sb.Append("</thead>");
               
                for (int i = 0; i < RowCount; i++)
                {
                    int TotalNoOfDays = 0;
                    decimal TotalQty = 0;
                    
                        sb.Append("<tr >");
                        sb.Append("<td style='border-top:1px dashed black;'>" + (i+1).ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                        
                        for (int j = 0; j < ColumnCount; j++)
                        {
                            if (ds.Tables[0].Columns[j].ToString() != "Office_Name")
                            {
                                
                                string Columns = ds.Tables[0].Columns[j].ToString();
                                string[] ColData = ds.Tables[0].Rows[i][Columns].ToString().Split('-');
                                if (ds.Tables[0].Rows[i][Columns]== DBNull.Value || ds.Tables[0].Rows[i][Columns].ToString() == null)
                                {
                                    sb.Append("<td style='border-top:1px dashed black;'></td>");
                                    sb.Append("<td style='border-top:1px dashed black;'></td>");
                                    dt.Rows.Add(Columns, "0");
                                    
                                }
                                else
                                {
                                    TotalNoOfDays += int.Parse(ColData[0]);
                                    TotalQty += decimal.Parse(ColData[1]);
                                    sb.Append("<td style='border-top:1px dashed black;'>" + ColData[0] + "</td>");
                                    sb.Append("<td style='border-top:1px dashed black;'>" + ColData[1] + "</td>");
                                    dt.Rows.Add(Columns, ColData[1]);
                                    
                                }
                                
                            }
                        }
                        sb.Append("<td style='border-top:1px dashed black;'>" + TotalNoOfDays + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + TotalQty + "</td>");
                        sb.Append("</tr>");
                        dt.Rows.Add("Total", TotalQty);
                    

                }
                sb.Append("<tr>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'><b>GRAND TOTAL</b></td>");
                
                for (int j = 0; j < ColumnCount; j++)
                {
                     if (ds.Tables[0].Columns[j].ToString() != "Office_Name")
                     {
                         string Columns = ds.Tables[0].Columns[j].ToString();
                         sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'></td>");
                         dt.AsEnumerable().Where(row => row.Field<string>("MonthName") == "May");
                         sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'>" + dt.AsEnumerable().Where(row => row.Field<string>("MonthName") == Columns).Sum(row => row.Field<decimal>("Qty")) + "</td>");
                     }
                    
                   
                }
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'>" + dt.AsEnumerable().Where(row => row.Field<string>("MonthName") == "Total").Sum(row => row.Field<decimal>("Qty")) + "</td>");
                sb.Append("</tr>");
            }
            else
            {
                sb.Append("<tr>");
                sb.Append("<td>No Record Found</td>");
                sb.Append("</tr>");
            }
           
            sb.Append("</table>");
            sb.Append("</div>");
            dvReport.InnerHtml = sb.ToString();
            divprint.InnerHtml = sb.ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "DCSMilkPurchaseSummary(CycleWise)" + DateTime.Now + ".xls");
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