using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Globalization;
using System.Text;

public partial class mis_dailyplan_RptCleaningofTankerAtPlant : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null)
        {
            if (!IsPostBack)
            {
                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }
                lblMsg.Text = "";
                
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Attributes.Add("readonly", "readonly");
                

            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx", false);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    
    protected void FillGrid()
    {
        try
        {
            //lblMsg.Text = "";
            
            ds = objdb.ByProcedure("Usp_tbl_Production_CleaningofTanker", new string[] { "flag", "I_OfficeID", "FromDate", "ToDate" }, new string[] { "8", objdb.Office_ID(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                   
                }
                else
                {
                    GridView1.DataSource = string.Empty;
                    GridView1.DataBind();
                }

            }
            else
            {
                GridView1.DataSource = string.Empty;
                GridView1.DataBind();
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
            string FileName = Session["Office_Name"].ToString() + "_" + "CleaningOfTankerDetails";
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            GridView1.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());

            Response.End();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string CleaningOfTanker_ID = e.CommandArgument.ToString();
        if (e.CommandName == "ViewRecord")
        {
            FillViewDetail(CleaningOfTanker_ID);

        }
    }
    protected void FillViewDetail(string CleaningOfTanker_ID)
    {
        ds = objdb.ByProcedure("Usp_tbl_Production_CleaningofTanker", new string[] { "flag", "CleaningOfTanker_ID" }, new string[] { "10", CleaningOfTanker_ID }, "dataset");
        if (ds != null && ds.Tables.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            if (ds.Tables[0].Rows.Count > 0)
            {

                sb.Append("<table class='table'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='text-align:center;'><b>" + Session["Office_Name"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='text-align:center;'><b>गुण नियंत्रण शाखा</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='text-align:center;'><b>Tanker/Tank/Silo क्लीनिंग रिपोर्ट</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>दिनांक</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[0]["TankerCleanedDate"].ToString() + "</td>");
                sb.Append("<td>समय</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[0]["TankerCleanedTime"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>क्रं</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[0]["TankerCleaningRequest_No"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>टैंकर क्रमांक</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[0]["V_VehicleNo"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>टैंक/आर.एम.टी./पी.एम.टी.नं.</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[0]["Tank_RMT_PMT"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>मेन होल/गैसकिट</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[0]["MainHole_GasKit"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>एयर बेंट</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[0]["AirBent"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>अनलोडिंग वाल्व</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[0]["UnLoadingValve"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>इनर शैल</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[0]["InnerShell"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>रिमार्क</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[0]["CleanedRemark"].ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");

                sb.Append("</tr>");
                sb.Append("<tr >");
                sb.Append("<td style='padding-top:70px;'><b>उत्पादन शाखा</b></td>");
                sb.Append("<td colspan='3' style='text-align:right; padding-top:70px;'><b>जांचकर्ता</b></td>");
                sb.Append("</tr>");
                sb.Append("</table");
                divPrint.InnerHtml = sb.ToString();
                //divViewDetail.InnerHtml = sb.ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "window.print();", true);

            }
        }


    }
}