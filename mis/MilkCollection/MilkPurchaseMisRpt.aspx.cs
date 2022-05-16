using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;
public partial class mis_MilkCollection_MilkPurchaseMisRpt : System.Web.UI.Page
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
			decimal TotalMilkAmountCommisison = 0;
			decimal TotalHeadLoad = 0;
			decimal TotalNetAmount = 0;
            decimal TotalQty = 0;
            decimal TotalFatKg = 0;
            decimal TotalSnfKg = 0;
            decimal TotalFatPer = 0;
            decimal TotalSnfPer = 0;
            decimal TotalUnit = 0;
            decimal TotalAvgPerDay = 0;
			decimal TotalDCS = 0;
            ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports", new string[] { "flag", "GenerateFrom_Office_ID", "Month", "Year" }, new string[] { "34", ddlOffice.SelectedValue, Month, Year }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    divshow.Visible = true;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table class='table table-bordered' border='1'>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='13' style='text-align:center; font-size:18px;'><b>" + Session["Office_Name"].ToString() + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='13' style='text-align:center; font-size:18px;'><b>Milk Purchase as Per BILL " + MonthName + "("+Year+")</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<th>CC Name</th>");
                   
                    sb.Append("<th>Qty</th>");
                    sb.Append("<th>FAT (KG)</th>");
                    sb.Append("<th>SNF (KG)</th>");
                    sb.Append("<th>Milk Amount + Commission</th>");
					sb.Append("<th>HeadLoad</th>");
                    sb.Append("<th>NetAmount</th>");
                    sb.Append("<th>Fat%</th>");
                    sb.Append("<th>SNF%</th>");
                    sb.Append("<th>Unit</th>");
                    sb.Append("<th>Avg/Day</th>");
					sb.Append("<th>No of DCS</th>");
                    sb.Append("</tr>");
                    int Count = ds.Tables[0].Rows.Count;
                    
                    for (int i = 0; i < Count; i++)
                    {
                       
                        decimal MilkAmountCommisison = decimal.Parse(ds.Tables[0].Rows[i]["MilkValue"].ToString()) + decimal.Parse(ds.Tables[0].Rows[i]["Commisssion"].ToString());
                        decimal FatPer = Math.Round((decimal.Parse(ds.Tables[0].Rows[i]["FatKg"].ToString()) * 100) / decimal.Parse(ds.Tables[0].Rows[i]["Qty"].ToString()),1);
                        decimal SnfPer = Math.Round((decimal.Parse(ds.Tables[0].Rows[i]["SnfKg"].ToString()) * 100) / decimal.Parse(ds.Tables[0].Rows[i]["Qty"].ToString()), 1);                 
                        decimal Unit = Math.Round(MilkAmountCommisison / decimal.Parse(ds.Tables[0].Rows[i]["Qty"].ToString()),2);
                        decimal Avgperday = Math.Round(decimal.Parse(ds.Tables[0].Rows[i]["Qty"].ToString())/NoofDays,0);
						
						TotalMilkAmountCommisison = TotalMilkAmountCommisison + MilkAmountCommisison;
                        
                        TotalAvgPerDay = TotalAvgPerDay + Avgperday;
						
                        sb.Append("<tr>");
                        sb.Append("<td style='background-color:#DDD9C4'>" + ds.Tables[0].Rows[i]["CCName"].ToString().ToUpper() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["Qty"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["FatKg"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["SnfKg"].ToString() + "</td>");
                        sb.Append("<td><b>" + MilkAmountCommisison.ToString() + "</b></td>");
						sb.Append("<td>" + ds.Tables[0].Rows[i]["HeadLoad"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["NetAmount"].ToString() + "</td>");
                        sb.Append("<td>" + FatPer.ToString() + " %</td>");
                        sb.Append("<td>" + SnfPer.ToString() + " %</td>");
                        sb.Append("<td>" + Unit.ToString() + "</td>");
                        sb.Append("<td><b>" + Avgperday.ToString() + "</b></td>");
						sb.Append("<td>" + ds.Tables[0].Rows[i]["NoOfDCS"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
					TotalQty = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Qty"));
					TotalUnit = Math.Round((TotalMilkAmountCommisison / TotalQty), 2);
                    TotalFatKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("FatKg"));
                    TotalSnfKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SnfKg"));
                    TotalFatPer = Math.Round((TotalFatKg * 100) / (TotalQty), 1);
                    TotalSnfPer = Math.Round((TotalSnfKg * 100) / (TotalQty), 1);
                    TotalSnfKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SnfKg"));
					TotalHeadLoad = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("HeadLoad"));
					TotalNetAmount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("NetAmount"));
					//TotalDCS = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("NoOfDCS"));
                    sb.Append("<tr>");
                    sb.Append("<td style='background-color:#DDD9C4'>UNION</td>");
                    sb.Append("<td>" + TotalQty.ToString() + "</td>");
                    sb.Append("<td>" + TotalFatKg.ToString() + "</td>");
                    sb.Append("<td>" + TotalSnfKg.ToString() + "</td>");
                    sb.Append("<td><b>" + TotalMilkAmountCommisison.ToString() + "</b></td>");
					sb.Append("<td><b>" + TotalHeadLoad.ToString() + "</b></td>");
					sb.Append("<td><b>" + TotalNetAmount.ToString() + "</b></td>");
                    sb.Append("<td>" + TotalFatPer.ToString() + " %</td>");
                    sb.Append("<td>" + TotalSnfPer.ToString() + " %</td>");
                    sb.Append("<td>" + TotalUnit.ToString() + "</td>");
                    sb.Append("<td><b>" + TotalAvgPerDay.ToString() + "</b></td>");
					sb.Append("<td></td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
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
            Response.AddHeader("content-disposition", "attachment;filename=" + "MilkPurchaseMisReport" + DateTime.Now + ".xls");
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