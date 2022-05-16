using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;

public partial class mis_MilkCollection_NegativePaymentDCSReport : System.Web.UI.Page
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            divReport.InnerHtml = "";
            divprint.InnerHtml = "";
            divshow.Visible = false;
            btnExport.Visible = false;
            btnprint.Visible = false;
            StringBuilder sb = new StringBuilder();
            ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports", new string[] { "flag", "FromDate", "ToDate", "CCID" }, new string[] { "39", Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd"), ddlccbmcdetail.SelectedValue }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                divshow.Visible = true;
                if(ds.Tables[0].Rows.Count > 0)
                {
                    btnExport.Visible = true;
                    btnprint.Visible = true;
                    int ColumnCount = ds.Tables[0].Columns.Count;
                    int RowCount = ds.Tables[0].Rows.Count;
                    sb.Append("<table class='table table-responsive' style='width:100%;'>");
					sb.Append("<thead class='header'>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4'  style='text-align:left; font-size:14px;'><b>" + Session["Office_Name"].ToString() + "</b></td>");
                    sb.Append("<td colspan=" + (ColumnCount - 2) + " style='text-align:left; font-size:14px;'><b>CC : -" + ddlccbmcdetail.SelectedItem.Text + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4' style='text-align:left; font-size:14px;'><b>LIST OF NEGATIVE PAYMENT DCS</b></td>");
                    sb.Append("<td colspan=" + (ColumnCount - 2) + " style='text-align:left; font-size:14px;'><b>PERIOD : " + txtFdt.Text + " - " + txtTdt.Text + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; border-left:1px dashed black; border-right:1px dashed black; font-size:13px;'><b>S.NO</b></td>");
                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; font-size:13px; border-left:1px dashed black; border-right:1px dashed black;'><b>DCS CODE & NAME</b></td>");
                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; font-size:13px; border-left:1px dashed black; border-right:1px dashed black;'><b>QUANTITY</b></td>");
                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; font-size:13px; border-left:1px dashed black; border-right:1px dashed black;'><b>TOT.EARNING</b></td>");
                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; font-size:13px; border-left:1px dashed black; border-right:1px dashed black;'><b>TOT.DEDUCTION</b></td>");
                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; font-size:13px; border-left:1px dashed black; border-right:1px dashed black;'><b>N E T</b></td>");
                    
                    sb.Append("</tr>");
					sb.Append("</thead>");
                   
                    for (int i = 0; i < RowCount; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; border-left:1px dashed black; border-right:1px dashed black;'>" + (i + 1).ToString() + "</td>");
                        sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; border-left:1px dashed black; border-right:1px dashed black;'>" + ds.Tables[0].Rows[i]["Office_Code"].ToString() + " - " + ds.Tables[0].Rows[i]["Particular"].ToString() + "</td>");
                        sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; border-left:1px dashed black; border-right:1px dashed black;'>" + ds.Tables[0].Rows[i]["Quantity"].ToString() + "</td>");
                        sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; border-left:1px dashed black; border-right:1px dashed black;'>" + ds.Tables[0].Rows[i]["ADDITION"].ToString() + "</td>");
                        sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; border-left:1px dashed black; border-right:1px dashed black;'>" + ds.Tables[0].Rows[i]["DEDUCTION"].ToString() + "</td>");
                        sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; border-left:1px dashed black; border-right:1px dashed black;'>" + ds.Tables[0].Rows[i]["NET"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                    decimal TotQty = 0;
                    decimal TotEarning = 0;
                    decimal TotDeduction = 0;
                    decimal TotNet = 0;
                    TotQty = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Quantity"));
                    TotEarning = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ADDITION"));
                    TotDeduction = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("DEDUCTION"));
                    TotNet = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("NET"));
                    sb.Append("<tr>");
                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; border-left:1px dashed black; border-right:1px dashed black;' colspan='2'>C Y C L E T O T A L</td>");

                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; border-left:1px dashed black; border-right:1px dashed black;' >" + TotQty.ToString() + "</td>");
                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; border-left:1px dashed black; border-right:1px dashed black;' >" + TotEarning.ToString() + "</td>");
                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; border-left:1px dashed black; border-right:1px dashed black;' >" + TotDeduction.ToString() + "</td>");
                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; border-left:1px dashed black; border-right:1px dashed black;' >" + TotNet.ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    divReport.InnerHtml = sb.ToString();
                    divprint.InnerHtml = sb.ToString();
                }
                else
                {
                    sb.Append("<table>");
                    sb.Append("<tr>");
                    sb.Append("<td>No Record Found</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    divReport.InnerHtml = sb.ToString();
                    divprint.InnerHtml = sb.ToString();
                }
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
            Response.AddHeader("content-disposition", "attachment;filename=" + "Listofnegativepaymentdcs" + DateTime.Now + ".xls");
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