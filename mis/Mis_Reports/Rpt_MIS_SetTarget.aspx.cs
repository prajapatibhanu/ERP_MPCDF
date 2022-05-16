using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Mis_Reports_Rpt_MIS_SetTarget : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds5, ds6 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetFinancialYear();

              
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void GetFinancialYear()
    {
        try
        {

            ds1 = objdb.ByProcedure("USP_MIS_Trn_SetTargetMilkProcurementOrSaleDSwise",
                 new string[] { "Flag" },
                   new string[] { "1" }, "dataset");
            if (ds1 != null)
            {
                ddlFiancialYear.DataTextField = "FinancialYear";
                ddlFiancialYear.DataValueField = "Phase_id";
                ddlFiancialYear.DataSource = ds1.Tables[0];
                ddlFiancialYear.DataBind();
                ddlFiancialYear.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            GetData();
        }
    }
    private void GetData()
    {
        try
        {
            lblMsg.Text = string.Empty;
            ds2 = objdb.ByProcedure("USP_MIS_Trn_SetTargetMilkProcurementOrSaleDSwise"
                , new string[] { "Flag", "FYId" }
                , new string[] { "3", ddlFiancialYear.SelectedValue }, "dataset");
             if (ds2 != null && ds2.Tables.Count > 0)
             {
                 pnlData.Visible = true;

                 DataTable dt3 = new DataTable();
                 dt3 = ds2.Tables[2];
                
                 DataTable dt1 = new DataTable();
                 dt1 = ds2.Tables[0];
                 int Count_Office = dt1.Rows.Count;
                 int ColCount_office = dt1.Columns.Count;
                 StringBuilder sb1 = new StringBuilder();
                 sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                 sb1.Append("<thead>");
                
                
                 for (int i = 0; i < Count_Office; i++)
                 {
                     for (int K = 0; K < ColCount_office; K++)
                     {
                         string ColName = dt1.Columns[K].ToString();
                         if (ColName != "Office_ID")
                         {
                             if(i==0)
                             {
                                 sb1.Append("<tr>");
                                 sb1.Append("<td colspan='" + ((Count_Office*2) + 3) + "' style='text-align:center;border: 1px solid #000000 !important;'><b>TARGETS OF MILK PROCUREMENT AND MILK SALE  FOR THE YEAR " + ddlFiancialYear.SelectedItem.Text + "</b></td>");
                                 sb1.Append("</tr>");
                               
                                 sb1.Append("<tr>");
                                 sb1.Append("<td style='border: 1px solid #000000 !important;'></td>");
                                 sb1.Append("<td colspan='2' style='border: 1px solid #000000 !important;'><b>" + dt1.Rows[i][ColName].ToString() + "</b></td>");
                             }
                             else if (i == Count_Office-1)
                             {
                                 sb1.Append("<td colspan='2' style='border: 1px solid #000000 !important;'><b>" + dt1.Rows[i][ColName].ToString() + "</b></td>");
                                 sb1.Append("<td colspan='2' style='border: 1px solid #000000 !important;'><b>MPCDF</b></td>");
                                 sb1.Append("</tr>");
                             }
                             else
                             {
                                 sb1.Append("<td colspan='2' style='border: 1px solid #000000 !important;'><b>" + dt1.Rows[i][ColName].ToString() + "</b></td>");
                             }
                            
                         }

                     }
                 }
                 for (int i = 0; i < Count_Office; i++)
                 {
                     for (int K = 0; K < ColCount_office; K++)
                     {
                         string ColName = dt1.Columns[K].ToString();
                         if (ColName != "Office_ID")
                         {
                             if (i == 0)
                             {
                                 sb1.Append("<tr>");
                                 sb1.Append("<td style='border: 1px solid #000000 !important;'><b>MONTH</b></td>");
                                 sb1.Append("<td style='border: 1px solid #000000 !important;'><b>PROCUREMENT</br>(KGPD)</b></td>");
                                 sb1.Append("<td style='border: 1px solid #000000 !important;'><b>MILK SALE</br>(LPD)</b></td>");
                             }
                             else if (i == Count_Office - 1)
                             {
                                 sb1.Append("<td style='border: 1px solid #000000 !important;'><b>PROCUREMENT</br>(KGPD)</b></td>");
                                 sb1.Append("<td style='border: 1px solid #000000 !important;'><b>MILK SALE</br>(LPD)</b></td>");
                                 sb1.Append("<td style='border: 1px solid #000000 !important;'><b>PROCUREMENT</br>(KGPD)</b></td>");
                                 sb1.Append("<td style='border: 1px solid #000000 !important;'><b>MILK SALE</br>(LPD)</b></td>");
                                 sb1.Append("</tr>");
                             }
                             else
                             {
                                 sb1.Append("<td style='border: 1px solid #000000 !important;'><b>PROCUREMENT</br>(KGPD)</b></td>");
                                 sb1.Append("<td style='border: 1px solid #000000 !important;'><b>MILK SALE</br>(LPD)</b></td>");
                             }

                         }

                     }
                 }                 
                 sb1.Append("</thead>");
                 DataTable dt2 = new DataTable();
                 dt2 = ds2.Tables[1];
                 int Count_Month = dt2.Rows.Count;
                 int ColCount_Month = dt2.Columns.Count;
                 for (int ii = 0; ii < Count_Month; ii++)
                 {
                     sb1.Append("<tr>");
                     for (int Kk = 0; Kk < ColCount_Month; Kk++)
                     {
                           decimal mp_total_mpcdf=0,ms_total_mpcdf=0;
                         string ColName11 = dt2.Columns[Kk].ToString();
                         if (ColName11 == "MonthName")
                         {

                             sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + dt2.Rows[ii][ColName11].ToString() + "</b></td>");
                         }
                         else
                         {
                             for (int i = 0; i < Count_Office; i++)
                             {
                                 for (int K = 0; K < ColCount_office; K++)
                                 {
                                     string ColName = dt1.Columns[K].ToString();
                                     if (ColName != "Office_Code")
                                     {
                                         string mp="", ms="";
                                         decimal mp_dec = 0, ms_dec = 0;
                                         DataRow[] filteredRows = dt3.Select("Office_ID=" + dt1.Rows[i][ColName].ToString() + "and TMonth=" + dt2.Rows[ii][ColName11].ToString() + "");
                                         foreach (DataRow row in filteredRows)
                                         {
                                             mp = row["MilkProcurement"].ToString();
                                             ms = row["MilkSale"].ToString();
                                         }

                                         if (mp == "" && ms =="")
                                         {
                                             mp_dec = 0;
                                             ms_dec = 0;

                                            
                                             if (i == Count_Office - 1)
                                             {
                                                 sb1.Append("<td style='border: 1px solid #000000 !important;'>" + mp_dec.ToString("0") + "</td>");
                                                 sb1.Append("<td style='border: 1px solid #000000 !important;'>" + ms_dec.ToString("0") + "</td>");

                                                 mp_total_mpcdf += mp_dec;
                                                 ms_total_mpcdf += ms_dec;

                                                 sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + mp_total_mpcdf.ToString("0") + "</b></td>");
                                                 sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + ms_total_mpcdf.ToString("0") + "</b></td>");
                                                 sb1.Append("</tr>");
                                                 mp_total_mpcdf=0;
                                                 ms_total_mpcdf=0;
                                             }
                                             else
                                             {
                                                 sb1.Append("<td style='border: 1px solid #000000 !important;'>" + mp_dec.ToString("0") + "</td>");
                                                 sb1.Append("<td style='border: 1px solid #000000 !important;'>" + ms_dec.ToString("0") + "</td>");

                                                 mp_total_mpcdf += mp_dec;
                                                 ms_total_mpcdf += ms_dec;
                                             }
                                            
                                         }
                                         else
                                         {
                                             mp_dec = Convert.ToDecimal(mp);
                                             ms_dec = Convert.ToDecimal(ms);

                                            

                                             if (i == Count_Office - 1)
                                             {
                                                 sb1.Append("<td style='border: 1px solid #000000 !important;'>" + mp_dec.ToString("0") + "</td>");
                                                 sb1.Append("<td style='border: 1px solid #000000 !important;'>" + ms_dec.ToString("0") + "</td>");

                                                 mp_total_mpcdf += mp_dec;
                                                 ms_total_mpcdf += ms_dec;

                                                 sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + mp_total_mpcdf.ToString("0") + "</b></td>");
                                                 sb1.Append("<td style='border: 1px solid #000000 !important;'><b>" + ms_total_mpcdf.ToString("0") + "</b></td>");
                                                 sb1.Append("</tr>");
                                                 mp_total_mpcdf = 0;
                                                 ms_total_mpcdf = 0;
                                             }
                                             else
                                             {
                                                 sb1.Append("<td style='border: 1px solid #000000 !important;'>" + mp_dec.ToString("0") + "</td>");
                                                 sb1.Append("<td style='border: 1px solid #000000 !important;'>" + ms_dec.ToString("0") + "</td>");

                                                 mp_total_mpcdf += mp_dec;
                                                 ms_total_mpcdf += ms_dec;
                                             }
                                         }
                                         

                                     }

                                 }
                             } 
                         }

                        
                     }
                     
                 }
                 sb1.Append("</table>");
                 div_page_content.InnerHtml = sb1.ToString();
                 Print.InnerHtml = sb1.ToString();
            
             }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
   
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "TargetReport" + "-" + ddlFiancialYear.SelectedItem.Text + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            Print.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
    }
}