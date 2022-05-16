using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class mis_dailyplan_Rpt_QualityWiseCansCollectionDetails : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtFromDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                    txtToDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    btnPrint.Visible = false;
                    btnExcel.Visible = false;
                    FillOffice();
                    GetSectionView();
                    
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }

        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("USP_MilkProductionEntry_New",
                  new string[] { "flag" },
                  new string[] { "11" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDS.DataSource = ds.Tables[0];
                ddlDS.DataTextField = "Office_Name";
                ddlDS.DataValueField = "Office_ID";
                ddlDS.DataBind();
                ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                ddlDS.SelectedValue = ViewState["Office_ID"].ToString();
                ddlDS.Enabled = false;
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetSectionView()
    {

        try
        {

            ds = null;

            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID", "OfficeType_ID" },
                  new string[] { "21", ddlDS.SelectedValue, objdb.OfficeType_ID() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPSection.DataSource = ds.Tables[0];
                ddlPSection.DataTextField = "ProductSection_Name";
                ddlPSection.DataValueField = "ProductSection_ID";
                ddlPSection.DataBind();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.SelectedValue = "1";
                ddlPSection.Enabled = false;

            }
            else
            {
                ddlPSection.Items.Clear();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.DataBind();
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
            decimal Fat = 0;
            decimal Snf = 0;
            decimal FatInKg = 0;
            decimal SnfInKg = 0;
            decimal QtyInKg = 0;
			 btnPrint.Visible = false;
            btnExcel.Visible = false;
			StringBuilder sb = new StringBuilder();
			DivDetail.InnerHtml = "";
            ds = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                new string[] { "flag", "Office_ID", "ProductSection_ID", "MilkQuality", "FromDate", "Todate" },
                new string[] { "31", ddlDS.SelectedValue, ddlPSection.SelectedValue, ddlMilkQuality.SelectedValue, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd")}, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                btnPrint.Visible = true;
                btnExcel.Visible = true;
                sb.Append("<table border='1' class='table table-bordered'>");
                //sb.Append("<tr>");
                //sb.Append("<th colspan='10'>" + ddlCCName.SelectedItem.Text + "&nbsp; CC Milk " + ddlType.SelectedItem.Text + "&nbsp;Report</th>");
                //sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th>S.No.</th>");               
                sb.Append("<th>Date(dd-MM-yyyy)</th>");
                sb.Append("<th>Variant</th>");
                sb.Append("<th>Milk Quality</th>");
                sb.Append("<th>QTY(kg)</th>");
                sb.Append("<th>FAT %</th>");
                sb.Append("<th>SNF %</th>");
                sb.Append("<th>FAT(Kg)</th>");
                sb.Append("<th>SNF(Kg)</th>");
                sb.Append("</tr>");
                int Count = ds.Tables[0].Rows.Count;
                for (int i = 0; i < Count; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + (i + 1).ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["EntryDate"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["Variant"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["MilkQuality"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["QtyInKg"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["Fat_Per"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["Snf_Per"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["KgFat"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["KgSnf"].ToString() + "</td>");
                    sb.Append("</tr>");
                }
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='text-align:right'><b>GRAND TOTAL</td>");
                QtyInKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                FatInKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                SnfInKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));
                Fat = Math.Round(((FatInKg / QtyInKg) * 100),2);
                Snf = Math.Round(((SnfInKg / QtyInKg) * 100),2);
                sb.Append("<td><b>" + QtyInKg.ToString() + "</b></td>");
                sb.Append("<td><b>" + Fat.ToString() + "</b></td>");
                sb.Append("<td><b>" + Snf.ToString() + "</b></td>");
                sb.Append("<td><b>" + FatInKg.ToString() + "</b></td>");
                sb.Append("<td><b>" + SnfInKg.ToString() + "</b></td>");
                


                sb.Append("</tr>");
                sb.Append("</table>");
                DivDetail.InnerHtml = sb.ToString();
            }
			 else
            {
                sb.Append("<table>");
                sb.Append("<tr>");
                sb.Append("<td>No Record Found</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                DivDetail.InnerHtml = sb.ToString();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment; filename=QualityWiseCansCollectionReport" + (System.DateTime.Now.ToString("dd-MM-yyyy_HH_mm_ss_fff")) + ".xls");
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite =
        new HtmlTextWriter(stringWrite);
        DivDetail.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }
}