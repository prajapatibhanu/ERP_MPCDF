using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Payroll_Rpt_SupplimentryBill : System.Web.UI.Page
{
    DataSet ds5=new DataSet();
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
        {
            if (!IsPostBack)
            {
                GetYear();
                GetOffice();
            }
        }
    }
    private void GetOffice()
    {
        
            ddlOfficeName.DataSource = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            ddlOfficeName.DataTextField = "Office_Name";
            ddlOfficeName.DataValueField = "Office_ID";
            ddlOfficeName.DataBind();
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlOfficeName.SelectedValue = Session["Office_ID"].ToString();
    }
    private void GetYear()
    {
        try
        {
                // ==== Month ====
                ddlYear.DataSource = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
                // ==== Search Year ====
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void SearchData()
    {
        try
        {
             div_page_content.InnerHtml = "";
            int rr = 0;
            ds5 = objdb.ByProcedure("USP_tblPayrollEmpSupplimentryDetail",
                             new string[] { "Flag", "FromYear", "FromMonth", "Office_ID" },
                               new string[] { "6", ddlYear.SelectedValue,ddlMonth.SelectedValue,ddlOfficeName.SelectedValue }, "dataset");
            if (ds5.Tables[0].Rows.Count > 0)
            {
              
                btnPrint.Visible = true;
                btnExcel.Visible = true;
             
                StringBuilder sb1 = new StringBuilder();
                int Count1 = ds5.Tables[0].Rows.Count;
                int ColCount1 = ds5.Tables[0].Columns.Count;

                sb1.Append("<table class='table1' style='width:100%;'>");
                sb1.Append("<thead>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center;' colspan='" + (ColCount1) + "'><b>" + ddlOfficeName.SelectedItem.Text + "<b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center;' colspan='" + (ColCount1) + "'><b>SUPPLYMENTRY BILL REPORT " + ddlMonth.SelectedItem.Text +  " - " + ddlYear.SelectedItem.Text + "</b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                for (int j = 0; j < ColCount1; j++)
                {

                    string ColName = ds5.Tables[0].Columns[j].ToString();
                    if (ColName != "NameDesig")
                    {
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");
                    }
                    else
                    {
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>NAME - DESIG</b></td>");
                    }
                    
                   
                }
                sb1.Append("</tr>");
                sb1.Append("</thead>");

                for (int i = 0; i < Count1; i++)
                {

                    sb1.Append("<tr>");
                   
                    for (int K = 0; K < ColCount1; K++)
                    {

                        string ColName = ds5.Tables[0].Columns[K].ToString();
                        sb1.Append("<td style='border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i][ColName].ToString() + "</td>");

                    }
                    sb1.Append("</tr>");


                }
                sb1.Append("<tr>");
                sb1.Append("<td style='text-align: center;border:1px solid black !important;' colspan='2' ><b>Grand Total</b></td>");
                //Grand total
                DataTable dt = new DataTable();
                dt = ds5.Tables[0];

                decimal sum12 = 0;
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() != "NameDesig" && column.ToString() != "Month Days")
                    {
                        sum12 = dt.AsEnumerable().Sum(r => r.Field<decimal?>("" + column + "") ?? 0);
                        sb1.Append("<td style='text-align: center;border:1px solid black !important;'><b>" + sum12.ToString("0.00") + "</b></td>");

                    }

                }

                if (dt != null) { dt.Dispose(); }
                sb1.Append("</tr>");
                div_page_content.InnerHtml = sb1.ToString();
               
            }
            else
            {
                div_page_content.InnerHtml = "";
                btnPrint.Visible = false;
                btnExcel.Visible = false;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", " No Record Found.");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            SearchData();
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "SUPPLYMENTRY_BILL_REPORT" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            div_page_content.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        btnPrint.Visible = false;
        btnExcel.Visible = false;
        div_page_content.InnerHtml = "";
        lblMsg.Text = string.Empty;
    }
}