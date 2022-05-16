using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;
using System.Configuration;
using System.Web.Configuration;
using System.Text;


public partial class mis_Mis_Reports_MilkProcSale : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
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
                Year();

            }
        }
        else
        {
            objdb.redirectToHome();
        }
       
    }

    protected void Year()
    {

        for (int i = 2010; i <= DateTime.Now.Year; i++)
        {
            ddlYear.Items.Add(i.ToString());


        }
        ddlYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            btnprint.Visible = false;
            btnExport.Visible = false;
            divprint.InnerHtml = "";
            ds = objdb.ByProcedure("USP_MIS_Trn_MilkOtherProcurement", new string[] { "flag", "Year" }, new string[] { "2", ddlYear.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnprint.Visible = true;
                    btnExport.Visible = true;
                    int rowcount = ds.Tables[0].Rows.Count;
                    int colcount = ds.Tables[0].Columns.Count;
                    decimal TotalMilkProc = 0;
                    decimal TotalMilkSale = 0;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table class='table table-bordered' border='1'>");
                    sb.Append("<tr>");
                    sb.Append("<th>Month</th>");
                    sb.Append("<th colspan='2' style='text-align:center;'>BDS</th>");
                    sb.Append("<th colspan='2' style='text-align:center;'>IDS</th>");
                    sb.Append("<th colspan='2' style='text-align:center;'>GDS</th>");
                    sb.Append("<th colspan='2' style='text-align:center;'>JDS</th>");
                    sb.Append("<th colspan='2' style='text-align:center;'>UDS</th>");
                    sb.Append("<th colspan='2' style='text-align:center;'>BKD</th>");
                    sb.Append("<th colspan='2' style='text-align:center;'>TOTAL</th>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<th></th>");
                    for (int i = 0; i < colcount; i++)
                    {
                        if (ds.Tables[0].Columns[i].ToString() != "Month")
                        {
                            string Value = ds.Tables[0].Columns[i].ToString();
                            Value = Value.Remove(0, 4);
                            sb.Append("<th >" + Value.ToString() + "</th>");
                        }


                    }
                    sb.Append("<th>Milk Procut</th>");
                    sb.Append("<th>Milk Sale</th>");
                    sb.Append("</tr>");
                    for (int i = 0; i < rowcount; i++)
                    {
                        TotalMilkProc = 0;
                        TotalMilkSale = 0;
                        sb.Append("<tr>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["Month"].ToString() + "</td>");
                        for (int j = 0; j < colcount; j++)
                        {

                            if (ds.Tables[0].Columns[j].ToString() != "Month")
                            {
                                string Columns = ds.Tables[0].Columns[j].ToString();
                                if (ds.Tables[0].Rows[i][Columns] == DBNull.Value || ds.Tables[0].Rows[i][Columns].ToString() == null)
                                {

                                    sb.Append("<td>0.00</td>");

                                    TotalMilkProc += 0;
                                    TotalMilkSale += 0;

                                }
                                else
                                {
                                    string Value = ds.Tables[0].Columns[j].ToString();
                                    Value = Value.Remove(0, 4);
                                    sb.Append("<td>" + ds.Tables[0].Rows[i][Columns].ToString() + "</td>");
                                    if (Value == "Milk Procut")
                                    {
                                        TotalMilkProc += decimal.Parse(ds.Tables[0].Rows[i][Columns].ToString());
                                    }
                                    else
                                    {
                                        TotalMilkSale += decimal.Parse(ds.Tables[0].Rows[i][Columns].ToString());
                                    }
                                }
                            }
                        }
                        sb.Append("<td>" + TotalMilkProc.ToString() + "</td>");
                        sb.Append("<td>" + TotalMilkSale.ToString() + "</td>");
                        sb.Append("</tr>");

                    }
                    //sb.Append("<tr>");
                    //sb.Append("<td>Total</td>");
                    //DataTable dt = new DataTable();
                    //dt = ds.Tables[0];
                    //string ColumnName1 = "";
                    //foreach (DataColumn column in dt.Columns)
                    //{
                    //    if (column.ToString() != "Month")
                    //    {
                    //        ColumnName1 = column.ColumnName;
                    //        double sum1 = Convert.ToDouble(dt.Compute("SUM([" + ColumnName1 + "])", string.Empty));
                    //        sb.Append("<td>" + sum1.ToString() + "</td>");
                    //    }
                    //}

                    //sb.Append("</tr>");
                    sb.Append("</table>");
                    divReport.InnerHtml = sb.ToString();
                    divprint.InnerHtml = sb.ToString();

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "YearWiseMilkProcurementandSaleReport" + DateTime.Now + ".xls");
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

