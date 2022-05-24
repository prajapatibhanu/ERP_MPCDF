using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_dailyplan_DateWiseMilkSheetSummaryRpt : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    string Fdate = "";
    string Tdate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                lblMsg.Text = "";

                if (!IsPostBack)
                {
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillOffice();
                    GetSectionView(sender, e);
                    
                    spnOfficeName.InnerHtml = Session["Office_Name"].ToString();
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

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
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

    private void GetSectionView(object sender, EventArgs e)
    {

        try
        {

            ds = null;

            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID", "OfficeType_ID" },
                  new string[] { "21", ddlDS.SelectedValue,objdb.OfficeType_ID() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPSection.DataSource = ds.Tables[0];
                ddlPSection.DataTextField = "ProductSection_Name";
                ddlPSection.DataValueField = "ProductSection_ID";
                ddlPSection.DataBind();
                //ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.SelectedValue = "1";
                ddlPSection.Enabled = false;
                ddlPSection_SelectedIndexChanged(sender, e);

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

    

    protected void ddlPSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlPSection.SelectedValue != "0")
        //{
        //    divfinal.Visible = true;
        //    GetSectionDetail();
        //    ViewFinalDisposalSheet();
        //}
        //else
        //{
        //    gvDDSheet.DataSource = string.Empty;
        //    gvDDSheet.DataBind();
        //    divfinal.Visible = false;
        //}
    }

    private void ViewFinalDisposalSheet()
    {

        try
        {
            Fdate = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            Tdate = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            StringBuilder sb = new StringBuilder();
            ds = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
               new string[] { "flag", "Office_ID", "FromDate", "ToDate", "ProductSection_ID","OfficeType_ID" },
               new string[] { "28", objdb.Office_ID(), Fdate,Tdate, ddlPSection.SelectedValue,objdb.OfficeType_ID() }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                divfinal.Visible = true;
               
                sb.Append("<table class='table table-bordered' border='1'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4' style='text-align:center;'><b>DATE  WISE FAT/SNF ACCOUNT OF PROCESSING SECTION<b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='4'><b>Date:" + txtFromDate.Text + " to  " + txtToDate.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th colspan='4'>InFlow</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th>PARTICULARS</th>");
                sb.Append("<th>QTY-KG</th>");
                sb.Append("<th>FAT-KG</th>");
                sb.Append("<th>SNF-KG</th>");               
                sb.Append("</tr>");
                
                int InFlowCount = ds.Tables[0].Rows.Count;
                decimal TIFQtyInKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                decimal TIFFatInKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                decimal TIFSnfInKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                decimal TOFQtyInKg = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                decimal TOFFatInKg = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                decimal TOFSnfInKg = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));


                for (int i = 0; i < InFlowCount; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["Particular"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["QtyInKg"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["FatInKg"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["SnfInKg"].ToString() + "</td>");
                    
                                
   
                    sb.Append("</tr>");

                  

                }
                sb.Append("<tr>");
                sb.Append("<td><b>TOTAL</b></td>");
                sb.Append("<td><b>" + TIFQtyInKg + "</b></td>");
                sb.Append("<td><b>" + TIFFatInKg + "</b></td>");
                sb.Append("<td><b>" + TIFSnfInKg + "</b></td>");
                sb.Append("</tr>");


                sb.Append("<tr>");
                sb.Append("<th colspan='4'>OutFlow</th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th>PARTICULARS</th>");
                sb.Append("<th>QTY-KG</th>");
                sb.Append("<th>FAT-KG</th>");
                sb.Append("<th>SNF-KG</th>");      
                sb.Append("</tr>");

                int OutFlowCount = ds.Tables[1].Rows.Count;
                for (int i = 0; i < OutFlowCount; i++)
                {
                    sb.Append("<tr>");
                    sb.Append("<td>" + ds.Tables[1].Rows[i]["Particular"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[1].Rows[i]["QtyInKg"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[1].Rows[i]["FatInKg"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[1].Rows[i]["SnfInKg"].ToString() + "</td>");



                    sb.Append("</tr>");



                }
                sb.Append("<tr>");
                sb.Append("<td><b>TOTAL</b></td>");
                sb.Append("<td><b>" + TOFQtyInKg + "</b></td>");
                sb.Append("<td><b>" + TOFFatInKg + "</b></td>");
                sb.Append("<td><b>" + TOFSnfInKg + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td><b>VARIATION</b></td>");
                sb.Append("<td><b>" + (TOFQtyInKg -TIFQtyInKg)  + "</b></td>");
                sb.Append("<td><b>" + (TOFFatInKg -TIFFatInKg)  + "</b></td>");
                sb.Append("<td><b>" + (TOFSnfInKg - TIFSnfInKg) + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<td><b>VARIATION %<span style='color:red'>(only 0.5% should be accepatable)</span></b></td>");
                sb.Append("<td></td>");
                sb.Append("<td><b>" + Math.Round(((TOFFatInKg - TIFFatInKg) / TIFFatInKg), 2) + "</b></td>");
                sb.Append("<td><b>" + Math.Round(((TOFSnfInKg - TIFSnfInKg) / TIFSnfInKg), 2) + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("</tr>");
                sb.Append("</table>");

                divrpt.InnerHtml = sb.ToString();
            }
           

        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlPSection.SelectedValue != "0")
        {


            ViewFinalDisposalSheet();

        }
        else
        {
            divfinal.Visible = false;

        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "DateWiseMilkSheetSummaryReport" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            divrpt.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());

            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    
}