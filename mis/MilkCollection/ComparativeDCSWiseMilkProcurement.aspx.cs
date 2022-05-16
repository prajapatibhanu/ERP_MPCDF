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

public partial class mis_MilkCollection_ComparativeDCSWiseMilkProcurement : System.Web.UI.Page
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
            string [] MonthyearPart= txtFdt.Text.Split('/');
            lblMsg.Text = "";
            divshow.Visible = true;
            string Month = MonthyearPart[0];
            string Year = MonthyearPart[1];
            string [] DataCycle1 ;
            string [] DataCycle2 ;
            DataCycle1 = ddlBillingCycle1.SelectedValue.Split('-');
            DataCycle2= ddlBillingCycle2.SelectedValue.Split('-');
            StringBuilder sb = new StringBuilder();
            int ColumnCount = 0;
            int RowCount = 0;

            ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports", new string[] { "flag", "Month", "Year", "CCID", "DataCycle1", "DataCycle2" }, new string[] { "28", Month, Year, ddlccbmcdetail.SelectedValue, Convert.ToDateTime(DataCycle1[1].ToString(), cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(DataCycle2[1].ToString(), cult).ToString("yyyy/MM/dd") }, "dataset");
           
            
            if(ds != null && ds.Tables.Count > 0)
            {
                divshow.Visible = true;
                 ColumnCount = ds.Tables[0].Columns.Count;
                 RowCount = ds.Tables[0].Rows.Count;
                 sb.Append("<table class='table'>");
				 sb.Append("<thead class='report-header'>");
                 sb.Append("<tr>");
                 sb.Append("<td colspan=" + ColumnCount + 1 + " style='text-align:center; font-size:18px;'><b>" + Session["Office_Name"].ToString() + "</b></td>");
                 sb.Append("</tr>");
                 sb.Append("<tr>");
                 sb.Append("<td colspan=" + ColumnCount + 1 + " style='text-align:center; font-size:18px;'><b>COMPARATIVE DCS WISE MILK PROCUREMENT - CC:-" + ddlccbmcdetail.SelectedItem.Text + "</b></td>");
                 sb.Append("</tr>");
                sb.Append("<tr  >");
                sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; text-align:left; font-size:15px;' ><b>S.NO</b></td>");
                sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; text-align:left; font-size:15px;' ><b>SOCIETY CODE/NAME</b></td>");
                
                for (int i = 0; i < ColumnCount; i++)
                {
                    if (ds.Tables[0].Columns[i].ToString() != "SocietyCodeName")
                    {
                        
                        sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; font-size:15px;'><b>" + ds.Tables[0].Columns[i].ToString() + "</b></td>");
                    }
                }
                sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; font-size:15px;'><b>GROWTH</b></td>");
                sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; font-size:15px;'><b>DOWNFALL</b></td>");
                sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; font-size:15px;'><b>REMARK</b></td>");
                sb.Append("</tr>");
				sb.Append("</thead>");
                decimal Col1Total = 0;
                decimal Col2Total = 0;
                decimal GrowthTotal = 0;
                decimal DownfallTotal = 0;
                decimal Qty = 0;
                decimal GrowthSocietyQtyTotal = 0;
                decimal DownfallSocietyQtyTotal = 0;
                decimal EqualSocietyQtyTotal = 0;
                int GrowthCount = 0;
                int DownfallCount = 0;
                int EqualCount = 0;
                for (int i = 0; i < RowCount; i++)
                {
                    decimal Total = 0;
                    
                    
                        sb.Append("<tr >");
                        sb.Append("<td>" + (i+1).ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["SocietyCodeName"].ToString() + "</td>");
                        
                       // sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["ColName"].ToString() + "</td>");
                        
                        for (int j = 0; j < ColumnCount; j++)
                        {
                            if (ds.Tables[0].Columns[j].ToString() != "SocietyCodeName")
                            {
                                string Columns = ds.Tables[0].Columns[j].ToString();
                                
                                if (ds.Tables[0].Rows[i][Columns] == DBNull.Value || ds.Tables[0].Rows[i][Columns].ToString() == null)
                                {
                                    if(j == 1)
                                    {
                                        Col1Total += 0;
                                    }
                                    if (j == 2)
                                    {
                                        Col2Total += 0;
                                    }
                                    sb.Append("<td>0.00</td>");
                                    Qty = 0;

                                }
                                else
                                {
                                    sb.Append("<td>" + ds.Tables[0].Rows[i][Columns].ToString() + "</td>");
                                    Total = decimal.Parse(ds.Tables[0].Rows[i][Columns].ToString()) - Total;
                                    Qty  =decimal.Parse(ds.Tables[0].Rows[i][Columns].ToString());
                                    if (j == 1)
                                    {
                                        Col1Total += decimal.Parse(ds.Tables[0].Rows[i][Columns].ToString());
                                    }
                                    if (j == 2)
                                    {
                                        Col2Total += decimal.Parse(ds.Tables[0].Rows[i][Columns].ToString());
                                    }
                                }
                               
                                // 
                                //if (ds.Tables[0].Rows[i][Columns]== DBNull.Value || ds.Tables[0].Rows[i][Columns].ToString() == null)
                                //{
                                //    sb.Append("<td style='border-top:1px dashed black;'>0.00</td>");
                                //    Total += 0.00;
                                //}
                                //else
                                //{
                                //    sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i][Columns].ToString() + "</td>");
                                //    Total += double.Parse(ds.Tables[0].Rows[i][Columns].ToString());
                                //}
                                
                            }
                        }
                        
                        if (Total > 0)
                        {
                            GrowthSocietyQtyTotal += Math.Abs(Qty);
                            GrowthTotal += Math.Abs(Total);
                            GrowthCount += 1;
                            sb.Append("<td>" + Math.Abs(Total) + "</td>");
                            sb.Append("<td></td>");
                           sb.Append("<td ></td>");
                        }
                        else if (Total < 0)
                        {
                            DownfallSocietyQtyTotal += Math.Abs(Qty);
                            DownfallTotal += Math.Abs(Total);
                            DownfallCount += 1;
                            sb.Append("<td ></td>");
                            sb.Append("<td >" + Math.Abs(Total)+ "</td>");
                            sb.Append("<td ></td>");
                        }
                        else
                        {
                            EqualSocietyQtyTotal += Math.Abs(Qty);
                            EqualCount += 1;
                            sb.Append("<td ></td>");
                            sb.Append("<td ></td>");
                            sb.Append("<td >Equal</td>");
                        }
                        sb.Append("</tr>");
                       
             
                   

                }
                sb.Append("<tr>");
                sb.Append("<td style='border-top:1px dashed black;' colspan='2'>CENTRE TOTAL</td>");
                sb.Append("<td style='border-top:1px dashed black;'>" + Col1Total + "</td>");
                sb.Append("<td style='border-top:1px dashed black;'>" + Col2Total + "</td>");
                sb.Append("<td style='border-top:1px dashed black;'>" + (Col2Total - Col1Total) + "</td>");
                sb.Append("<td style='border-top:1px dashed black;'></td>");
                sb.Append("<td style='border-top:1px dashed black;'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td  colspan='2'>Total Number of DCS</td>");
                sb.Append("<td >(" + RowCount + ")</td>");
                sb.Append("<td >(" + RowCount + ")</td>");
                sb.Append("<td >(" + GrowthCount + ")</td>");
                sb.Append("<td >(" + DownfallCount + ")</td>");
                sb.Append("<td >(" + EqualCount + ")</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td  colspan='2'>Milk Quantity(kgPD)</td>");
                sb.Append("<td ></td>");
                sb.Append("<td ></td>");
                sb.Append("<td >" + GrowthTotal + "</td>");
                sb.Append("<td >" + DownfallTotal + "</td>");
                sb.Append("<td ></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='border-bottom:1px dashed black;' colspan='2'>Total Milk Quantity(kgPD)</td>");
                sb.Append("<td style='border-bottom:1px dashed black;'></td>");
                sb.Append("<td style='border-bottom:1px dashed black;'></td>");
                sb.Append("<td style='border-bottom:1px dashed black;'>"+GrowthSocietyQtyTotal+"</td>");
                sb.Append("<td style='border-bottom:1px dashed black;'>"+DownfallSocietyQtyTotal+"</td>");
                sb.Append("<td style='border-bottom:1px dashed black;'>"+EqualSocietyQtyTotal+"</td>");
                sb.Append("</tr>");
            }
            else
            {
                sb.Append("<tr>");
                sb.Append("<td>No Record Found</td>");
                sb.Append("</tr>");
            }
           
            sb.Append("</table>");
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
    protected void txtFdt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] MonthyearPart = txtFdt.Text.Split('/');
            lblMsg.Text = "";
            //divshow.Visible = true;
            string Month = MonthyearPart[0];
            string Year = MonthyearPart[1];
            ddlBillingCycle1.Items.Clear();
            ddlBillingCycle2.Items.Clear();
            ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports", new string[] { "flag", "CCID", "Month", "Year" }, new string[] { "27", ddlccbmcdetail.SelectedValue, Month, Year }, "dataset");
            if(ds !=null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    ddlBillingCycle1.DataTextField = "BillingPeriod";
                    ddlBillingCycle1.DataValueField = "BillingPeriod";
                    ddlBillingCycle1.DataSource = ds;
                    ddlBillingCycle1.DataBind();

                    ddlBillingCycle2.DataTextField = "BillingPeriod";
                    ddlBillingCycle2.DataValueField = "BillingPeriod";
                    ddlBillingCycle2.DataSource = ds;
                    ddlBillingCycle2.DataBind();

                   

                    
                }
            }
            ddlBillingCycle1.Items.Insert(0, new ListItem("Select", "0"));
            ddlBillingCycle2.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}