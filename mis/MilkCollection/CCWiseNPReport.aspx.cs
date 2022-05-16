using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;

public partial class mis_MilkCollection_CCWiseNPReport : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!Page.IsPostBack)
            {
                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }

                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                
                GetCCDetails();
               
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            divReport.InnerHtml = "";
            divprint.InnerHtml = "";
            divshow.Visible = false;
            StringBuilder sb = new StringBuilder();
            ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports", new string[] { "flag", "FromDate", "ToDate", "Office_ID", "Office_Parant_ID" }, new string[] { "21", Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd"), ddlccbmcdetail.SelectedValue,objdb.Office_ID() }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                divshow.Visible = true;
                sb.Append("<table class='table table-responsive'>");
				sb.Append("<thead class='header'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4'  style='text-align:left; font-size:18px;'><b>" + Session["Office_Name"].ToString() + "</b></td>");
                sb.Append("<td colspan='4'  style='text-align:left; font-size:18px;'><b>CC: - " + ddlccbmcdetail.SelectedItem.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='text-align:left; font-size:18px;'><b>DWM Report</b></td>");
                sb.Append("<td colspan='4' style='text-align:left; font-size:18px;'><b>PERIOD : " + txtFdt.Text + " - " + txtTdt.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th style='border-bottom:1px dashed black; border-top:1px dashed black;'>S.NO</th>");
                sb.Append("<th style='border-bottom:1px dashed black; border-top:1px dashed black;'>DCS CODE</th>");
                sb.Append("<th style='border-bottom:1px dashed black; border-top:1px dashed black;'>NAME</th>");
                sb.Append("<th style='border-bottom:1px dashed black; border-top:1px dashed black;'>BILLING PERIOD</th>");
                sb.Append("<th style='border-bottom:1px dashed black; border-top:1px dashed black;'>HEAD NAME</th>");
                sb.Append("<th style='border-bottom:1px dashed black; border-top:1px dashed black;'>AMOUNT</th>");
                sb.Append("<th style='border-bottom:1px dashed black; border-top:1px dashed black;'>REMARK</th>");

                sb.Append("</tr>");
				sb.Append("</thead>");
                if(ds.Tables[0].Rows.Count > 0)
                {
                   
                    
                    
                    int ColumnCount = ds.Tables[0].Columns.Count;
                    int RowCount = ds.Tables[0].Rows.Count;
                    decimal TotalAmount = 0;
                    
                    for (int i = 0; i < RowCount; i++)
                    {
                        
                        sb.Append("<tr>");

                        sb.Append("<td style='width:10%'>" + (i+1).ToString() + "</td>");
                        sb.Append("<td style='width:10%'>" + ds.Tables[0].Rows[i]["Office_Code"].ToString() + "</td>");
                        sb.Append("<td style='width:20%'>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                        sb.Append("<td style='width:20%'>" + ds.Tables[0].Rows[i]["BillingPeriod"].ToString() + "</td>");
                        sb.Append("<td style='width:20%'>" + ds.Tables[0].Rows[i]["ItemBillingHead_Name"].ToString() + "</td>");
                        sb.Append("<td style='width:20%'>" + ds.Tables[0].Rows[i]["HeadAmount"].ToString() + "</td>");
                        sb.Append("<td style='width:20%'>" + ds.Tables[0].Rows[i]["HeadRemark"].ToString() + "</td>");
                        
                        sb.Append("</tr>");

                        
                    }
                    TotalAmount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("HeadAmount"));
                    
                    sb.Append("<tr>");
                    sb.Append("<td colspan='5' style='border-top:1px dashed black; text-align:right'>TOTAL</td>");
                    sb.Append("<td style='border-top:1px dashed black'>" + TotalAmount + "</td>");
                    sb.Append("<td style='border-top:1px dashed black'></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='7' style='border-bottom:1px dashed black'></td>");
                    sb.Append("</tr>");

                    
                }
                else
                {
                    sb.Append("<tr>");
                    sb.Append("<td>No Record Found</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                divReport.InnerHtml = sb.ToString();
                divprint.InnerHtml = sb.ToString();
            }
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
            Response.AddHeader("content-disposition", "attachment;filename=" + "CCWiseNPReport" + ddlccbmcdetail.SelectedItem.Text +"_" + DateTime.Now + ".xls");
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