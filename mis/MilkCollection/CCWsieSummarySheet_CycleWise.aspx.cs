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

public partial class mis_MilkCollection_CCWsieSummarySheet_CycleWise : System.Web.UI.Page
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
                            ddlccbmcdetail.Items.Insert(0, new ListItem("All", "0"));
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
            StringBuilder sb = new StringBuilder();
            int ColumnCount = 0;
            int RowCount = 0;
            ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports", new string[] { "flag", "Month", "Year", "Office_ID", "GenerateFrom_Office_ID","OfficeType_ID" }, new string[] { "15", Month, Year, ddlccbmcdetail.SelectedValue,objdb.Office_ID(),objdb.OfficeType_ID() }, "dataset");
           
            
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
                 sb.Append("<td colspan=" + ColumnCount + 1 + " style='text-align:center; font-size:18px;'><b>DCS  Milk Purchase Summary - CC:-" + ddlccbmcdetail.SelectedItem.Text + "</b></td>");
                 sb.Append("</tr>");
                sb.Append("<tr  >");
                sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; text-align:left; font-size:15px;'colspan='3' ><b>Particulars</b></td>");
                
                for (int i = 0; i < ColumnCount; i++)
                {
                    if (ds.Tables[0].Columns[i].ToString() != "MilkType" && ds.Tables[0].Columns[i].ToString() != "MilkQuality" && ds.Tables[0].Columns[i].ToString() != "ColName")
                    {
                        
                        sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; font-size:15px;'><b>" + ds.Tables[0].Columns[i].ToString() + "</b></td>");
                    }
                }
                sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; font-size:15px;'><b>Total</b></td>");
                sb.Append("</tr>");
				sb.Append("</thead>");
                string MilkType = "";
                string MilkQuality = "";
                for (int i = 0; i < RowCount; i++)
                {
                    double Total = 0.00;
                    
                    if (i == 0)
                    {
                        sb.Append("<tr >");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["MilkType"].ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["MilkQuality"].ToString() + "</td>");
                        if (ds.Tables[0].Rows[i]["ColName"].ToString() == "RFat")
                        {
                            sb.Append("<td style='border-top:1px dashed black;'>Fat</td>");

                        }
                        else
                        {
                            sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["ColName"].ToString() + "</td>");
                        }
                        for (int j = 0; j < ColumnCount; j++)
                        {
                            if (ds.Tables[0].Columns[j].ToString() != "MilkType" && ds.Tables[0].Columns[j].ToString() != "MilkQuality" && ds.Tables[0].Columns[j].ToString() != "ColName")
                            {
                                
                                string Columns = ds.Tables[0].Columns[j].ToString();
                                if (ds.Tables[0].Rows[i][Columns]== DBNull.Value || ds.Tables[0].Rows[i][Columns].ToString() == null)
                                {
                                    sb.Append("<td style='border-top:1px dashed black;'>0.00</td>");
                                    Total += 0.00;
                                }
                                else
                                {
                                    sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i][Columns].ToString() + "</td>");
                                    Total += double.Parse(ds.Tables[0].Rows[i][Columns].ToString());
                                }
                                
                            }
                        }
                        sb.Append("<td style='border-top:1px dashed black;'>" + Total + "</td>");
                        sb.Append("</tr>");
                        MilkType = ds.Tables[0].Rows[i]["MilkType"].ToString();
                        MilkQuality = ds.Tables[0].Rows[i]["MilkQuality"].ToString();
                    }
                    else if(ds.Tables[0].Rows[i]["MilkType"].ToString() == MilkType.ToString())
                    {
                        sb.Append("<tr>");
                        sb.Append("<td ></td>");

                        if(ds.Tables[0].Rows[i]["MilkQuality"].ToString() == MilkQuality.ToString())
                        {
                            sb.Append("<td></td>");
                            if (ds.Tables[0].Rows[i]["ColName"].ToString() == "RFat")
                            {
                                sb.Append("<td>Fat</td>");

                            }
                            else
                            {
                                sb.Append("<td>" + ds.Tables[0].Rows[i]["ColName"].ToString() + "</td>");
                            }
                        }
                        else
                        {
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["MilkQuality"].ToString() + "</td>");
                            if (ds.Tables[0].Rows[i]["ColName"].ToString() == "RFat")
                            {
                                sb.Append("<td>Fat</td>");

                            }
                            else
                            {
                                sb.Append("<td>" + ds.Tables[0].Rows[i]["ColName"].ToString() + "</td>");
                            }
                        }
                        for (int j = 0; j < ColumnCount; j++)
                        {
                            if (ds.Tables[0].Columns[j].ToString() != "MilkType" && ds.Tables[0].Columns[j].ToString() != "MilkQuality" && ds.Tables[0].Columns[j].ToString() != "ColName")
                            {
                                string Columns = ds.Tables[0].Columns[j].ToString();
                                if (ds.Tables[0].Rows[i][Columns] == DBNull.Value || ds.Tables[0].Rows[i][Columns].ToString() == null)
                                {
                                    sb.Append("<td>0.00</td>");
                                    Total += 0.00;
                                }
                                else
                                {
                                    sb.Append("<td>" + ds.Tables[0].Rows[i][Columns].ToString() + "</td>");
                                    Total += double.Parse(ds.Tables[0].Rows[i][Columns].ToString());
                                }
                            }
                        }
                        sb.Append("<td >" + Total + "</td>");
                        sb.Append("</tr>");
                        MilkType = ds.Tables[0].Rows[i]["MilkType"].ToString();
                        MilkQuality = ds.Tables[0].Rows[i]["MilkQuality"].ToString();

                    }
                    else
                    {
                        
                        sb.Append("<tr>");
                        sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'>" + ds.Tables[0].Rows[i]["MilkType"].ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'>" + ds.Tables[0].Rows[i]["MilkQuality"].ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'>" + ds.Tables[0].Rows[i]["ColName"].ToString() + "</td>");
                        for (int j = 0; j < ColumnCount; j++)
                        {
                            if (ds.Tables[0].Columns[j].ToString() != "MilkType" && ds.Tables[0].Columns[j].ToString() != "MilkQuality" && ds.Tables[0].Columns[j].ToString() != "ColName")
                            {
                                string Columns = ds.Tables[0].Columns[j].ToString();
                                if (ds.Tables[0].Rows[i][Columns] == DBNull.Value || ds.Tables[0].Rows[i][Columns].ToString() == null)
                                {
                                    sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'>0.00</td>");
                                    Total += 0.00;
                                }
                                else
                                {
                                    sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'>" + ds.Tables[0].Rows[i][Columns].ToString() + "</td>");
                                    Total += double.Parse(ds.Tables[0].Rows[i][Columns].ToString());
                                }
                                
                            }
                        }
                        sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;'>" + Total + "</td>");
                        sb.Append("</tr>");
                        MilkType = ds.Tables[0].Rows[i]["MilkType"].ToString();
                        MilkQuality = ds.Tables[0].Rows[i]["MilkQuality"].ToString();
                    }

                }
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
}