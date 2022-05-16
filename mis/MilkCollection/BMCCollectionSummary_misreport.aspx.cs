using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;

public partial class mis_MilkCollection_BMCCollectionSummary_misreport : System.Web.UI.Page
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
            lblrecordmsg.Text = "";
            divReport.InnerHtml = "";
            divprint.InnerHtml = "";
            btnExport.Visible = false;
            btnprint.Visible = false;
            divshow.Visible = false;
            ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports", new string[] { "flag", "CCID", "FromDate", "ToDate" }, new string[] { "29", ddlccbmcdetail.SelectedValue, Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if(ds!= null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    divshow.Visible = true;
                    btnExport.Visible = true;
                    btnprint.Visible = true;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<div class='table-responsive'>");
                    sb.Append("<table class='table'>");
                    sb.Append("<thead class='header'>");
                    sb.Append("<tr>");
                    sb.Append("<td style='text-align:center;' colspan='12'><b>" + Session["Office_Name"].ToString() + "</b></td>");

                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td style='text-align:center;' colspan='13'><b>BMC Collection Summary</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='text-align:left;' colspan='6'><b>CC:-" + ddlccbmcdetail.SelectedItem.Text + "</b></td>");
                    sb.Append("<td style='text-align:right;' colspan='6' ><b>From : " + txtFdt.Text + " -  " + txtTdt.Text + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("</thead>");
                    sb.Append("<tr>");
                    sb.Append("<th colspan='2' style='border-top: 1px dashed black; border-bottom: 1px dashed black;'></th>");
                    sb.Append("<th colspan='3' style='border-top: 1px dashed black; border-bottom: 1px dashed black;'><--------------SWEET--------------></th>");
                    sb.Append("<th colspan='3' style='border-top: 1px dashed black; border-bottom: 1px dashed black;'><--------------SOUR--------------></th>");
                    sb.Append("<th colspan='3' style='border-top: 1px dashed black; border-bottom: 1px dashed black;'><--------------CURD--------------></th>");
                    sb.Append("<th style='border-top: 1px dashed black; border-bottom: 1px dashed black;'><-----TOTAL-----></th>");                
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<th style='text-align:center; border-top: 1px dashed black; border-bottom: 1px dashed black;'>S.NO</th>");
                    sb.Append("<th style='text-align:center; border-top: 1px dashed black; border-bottom: 1px dashed black;'>NAME</th>");
                    sb.Append("<th style='text-align:center; border-top: 1px dashed black; border-bottom: 1px dashed black;'>QTY</th>");
                    sb.Append("<th style='text-align:center; border-top: 1px dashed black; border-bottom: 1px dashed black;'>KGFAT</th>");
                    sb.Append("<th style='text-align:center; border-top: 1px dashed black; border-bottom: 1px dashed black;'>KGSNF</th>");
                    sb.Append("<th style='text-align:center; border-top: 1px dashed black; border-bottom: 1px dashed black;'>QTY</th>");
                    sb.Append("<th style='text-align:center; border-top: 1px dashed black; border-bottom: 1px dashed black;'>KGFAT</th>");
                    sb.Append("<th style='text-align:center; border-top: 1px dashed black; border-bottom: 1px dashed black;'>KGSNF</th>");
                    sb.Append("<th style='text-align:center; border-top: 1px dashed black; border-bottom: 1px dashed black;'>QTY</th>");
                    sb.Append("<th style='text-align:center; border-top: 1px dashed black; border-bottom: 1px dashed black;'>KGFAT</th>");
                    sb.Append("<th style='text-align:center; border-top: 1px dashed black; border-bottom: 1px dashed black;'>KGSNF</th>");
                    sb.Append("<th style='text-align:center; border-top: 1px dashed black; border-bottom: 1px dashed black;'>QTY</th>");           
                    sb.Append("</tr>");

                    int Count = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1).ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["Society"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["GMilkQuantity"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["GFatInKg"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["GSnfInKg"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["SMilkQuantity"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["SFatInKg"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["SSnfInKg"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["CMilkQuantity"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["CFatInKg"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["CSnfInKg"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["Total"].ToString() + "</td>");
                       
                        sb.Append("</tr>");

                    }
                    sb.Append("<tr>");
                    sb.Append("<td colspan = '2' style='border-top: 1px dashed black; border-bottom: 1px dashed black;'><b>GRAND TOTAL</b></td>");
                    sb.Append("<td style='border-top: 1px dashed black; border-bottom: 1px dashed black;'><b>" + ds.Tables[1].Rows[0]["GTMilkQuantity"].ToString() + "</b></td>");
                    sb.Append("<td style='border-top: 1px dashed black; border-bottom: 1px dashed black;'><b>" + ds.Tables[1].Rows[0]["GTFatInKg"].ToString() + "</b></td>");
                    sb.Append("<td style='border-top: 1px dashed black; border-bottom: 1px dashed black;'><b>" + ds.Tables[1].Rows[0]["GTSnfInKg"].ToString() + "</b></td>");
                    sb.Append("<td style='border-top: 1px dashed black; border-bottom: 1px dashed black;'><b>" + ds.Tables[1].Rows[0]["STMilkQuantity"].ToString() + "</b></td>");
                    sb.Append("<td style='border-top: 1px dashed black; border-bottom: 1px dashed black;'><b>" + ds.Tables[1].Rows[0]["STFatInKg"].ToString() + "</b></td>");
                    sb.Append("<td style='border-top: 1px dashed black; border-bottom: 1px dashed black;'><b>" + ds.Tables[1].Rows[0]["STSnfInKg"].ToString() + "</b></td>");
                    sb.Append("<td style='border-top: 1px dashed black; border-bottom: 1px dashed black;'><b>" + ds.Tables[1].Rows[0]["CTMilkQuantity"].ToString() + "</b></td>");
                    sb.Append("<td style='border-top: 1px dashed black; border-bottom: 1px dashed black;'><b>" + ds.Tables[1].Rows[0]["CTFatInKg"].ToString() + "</b></td>");
                    sb.Append("<td style='border-top: 1px dashed black; border-bottom: 1px dashed black;'><b>" + ds.Tables[1].Rows[0]["CTSnfInKg"].ToString() + "</b></td>");
                    sb.Append("<td style='border-top: 1px dashed black; border-bottom: 1px dashed black;'><b>" + ds.Tables[1].Rows[0]["GTotal"].ToString() + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");
                    divReport.InnerHtml = sb.ToString();
                    divprint.InnerHtml = sb.ToString();
                }
                else
                {
                    lblrecordmsg.Text = "No Record Found";
                }
            }
            else
            {
                lblrecordmsg.Text = "No Record Found";
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
            Response.AddHeader("content-disposition", "attachment;filename=" + "BMCCollectionSummary" + DateTime.Now + ".xls");
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