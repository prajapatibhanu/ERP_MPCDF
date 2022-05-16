using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;

public partial class mis_MilkCollection_CCWiseValidationReport : System.Web.UI.Page
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
            ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports", new string[] { "flag", "FromDate", "ToDate", "Office_ID", "Office_Parant_ID" }, new string[] { "8", Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd"), ddlccbmcdetail.SelectedValue,objdb.Office_ID() }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                divshow.Visible = true;
                decimal TotalqtySum = 0;
                decimal TotalfatSum = 0;
                decimal TotalsnfSum = 0;
                decimal TotalclrSum = 0;
                int ColumnCount = ds.Tables[0].Columns.Count;
                int RowCount = ds.Tables[0].Rows.Count;
                sb.Append("<table class='table table-responsive'>");
				sb.Append("<thead class='header'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4'  style='text-align:left; font-size:18px;'><b>" + Session["Office_Name"].ToString() + "</b></td>");
                sb.Append("<td colspan='4'  style='text-align:left; font-size:18px;'><b>CC: - " + ddlccbmcdetail.SelectedItem.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='text-align:left; font-size:18px;'><b>VALIDATION REPORT OF MILK DATA</b></td>");
                sb.Append("<td colspan='4' style='text-align:left; font-size:18px;'><b>PERIOD : " + txtFdt.Text + " - " + txtTdt.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th style='border-bottom:1px dashed black; border-top:1px dashed black;'>Date</th>");
                sb.Append("<th style='border-bottom:1px dashed black; border-top:1px dashed black;'>Shift</th>");
                sb.Append("<th style='border-bottom:1px dashed black; border-top:1px dashed black;'>TOT QTY</th>");
                sb.Append("<th style='border-bottom:1px dashed black; border-top:1px dashed black;'>TOT FAT%</th>");
                sb.Append("<th style='border-bottom:1px dashed black; border-top:1px dashed black;'>TOT SNF%</th>");
                sb.Append("<th style='border-bottom:1px dashed black; border-top:1px dashed black;'>TOT CLR</th>");
                sb.Append("<th style='border-bottom:1px dashed black; border-top:1px dashed black;'>REMARK</th>");
                sb.Append("</tr>");
				sb.Append("</thead>");
                if(ds.Tables[0].Rows.Count > 0)
                {
                    
                    string Previousdate = "";
                    for (int i = 0; i < RowCount; i++)
                    {
                        
                        sb.Append("<tr>");
                        if(i == 0)
                        {
                             sb.Append("<td>" + ds.Tables[0].Rows[i]["Date"].ToString() + "</td>");
                        }
                        else
                        {
                            if(Previousdate.ToString() == ds.Tables[0].Rows[i]["Date"].ToString())
                            {
                                sb.Append("<td></td>");
                            }
                            else
                            {
                                sb.Append("<td>" + ds.Tables[0].Rows[i]["Date"].ToString() + "</td>");
                            }
                        }
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["Shift"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["TotalQty"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["Fat"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["Snf"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["TotalClr"].ToString() + "</td>");
                        sb.Append("<td></td>");
                        sb.Append("</tr>");

                        Previousdate = ds.Tables[0].Rows[i]["Date"].ToString();
                    }
                    TotalqtySum = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalQty"));
                    TotalfatSum = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Fat"));
                    TotalsnfSum = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Snf"));
                    TotalclrSum = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalClr"));
                    sb.Append("<tr>");
                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black;' colspan='2'>Period Total</td>");
                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black;'>" + TotalqtySum + "</td>");
                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black;'>" + TotalfatSum + "</td>");
                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black;'>" + TotalsnfSum + "</td>");
                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black;'>" + TotalclrSum + "</td>");
                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black;'></td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td  style='padding-top:30px; border-top:1px dashed black;' colspan='4'>ENTERED BY</td>");
                    sb.Append("<td style='padding-top:30px; border-top:1px dashed black;' colspan='4'>CHECKED & VERIFY BY</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td style='padding-top:30px;' colspan='7'>Negative Payment Amount (Rs)  :</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td colspan='7'>Summary Net Amount (Rs)  :</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td colspan='7'>Bank Payment Amount (Rs)  :</td>");
                    sb.Append("</tr>");
                   
                   
                }
                else
                {
                    sb.Append("<tr>");
                    sb.Append("<td colspan='6'>No Record Found</td>");
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
            Response.AddHeader("content-disposition", "attachment;filename=" + "CCWiseValidationReport" + DateTime.Now + ".xls");
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