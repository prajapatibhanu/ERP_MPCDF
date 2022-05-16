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

public partial class mis_dailyplan_ProductIPSheetReport : System.Web.UI.Page
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
                lblMsg.Text = "";

                if (!IsPostBack)
                {
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillOffice();
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    GetSectionView(sender, e);
                    txtDate_TextChanged(sender, e);
                    

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
            ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "12" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDS.DataSource = ds.Tables[0];
                ddlDS.DataTextField = "Office_Name";
                ddlDS.DataValueField = "Office_ID";
                ddlDS.DataBind();
                ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                ddlDS.SelectedValue = ViewState["Office_ID"].ToString();
                ddlDS.Enabled = false;
                btnprint.Visible = false;
                btnExport.Visible = false;
                divprint.InnerHtml = "";
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
                  new string[] { "flag", "Office_ID" },
                  new string[] { "6", ddlDS.SelectedValue }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPSection.DataSource = ds.Tables[0];
                ddlPSection.DataTextField = "ProductSection_Name";
                ddlPSection.DataValueField = "ProductSection_ID";
                ddlPSection.DataBind();
                ddlPSection.SelectedValue = "2";
                ddlPSection.Enabled = false;

               

            }
            else
            {
                ddlPSection.Items.Clear();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.DataBind();
                ddlPSection.Enabled = false;
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }
   

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {

            divprint.InnerHtml = "";
            lblMsg.Text = "";
            StringBuilder sb = new StringBuilder();
            ds = objdb.ByProcedure("Usp_Production_ProductIPSheetMaster", new string[] { "flag", "Date", "Office_ID" }, new string[] {"4",Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),objdb.Office_ID() }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {

                    btnprint.Visible = true;
                    btnExport.Visible = true;
                    sb.Append("<div class='row'");
                    sb.Append("<div class='col-md-12' style='text-align:center'><h3 style='font-weight: 800; font-size: 20px;'>" + Session["Office_Name"] + "</h3><h5 style='font-weight: 500; font-size: 13px;'>DAIRY PLANT</h5><h5 style='font-weight: 800; font-size: 20px;'>IP SHEET REPORT</h5></div>");
                    sb.Append("<div class='col-md-12' style='text-align:center'>Date:" + txtDate.Text +" </div>");
                    sb.Append("<table class='table table-bordered'>");
                    sb.Append("<tr>");
                    sb.Append("<th rowspan='2' style='text-align:center;'>Product Name</th>");
                    sb.Append("<th colspan='3' style='text-align:center;'>MilkRequired</th>");
                    sb.Append("<th colspan='4' style='text-align:center;'>First Stage Manufacturing</th>");
                    sb.Append("<th colspan='4' style='text-align:center;'>Second Stage Manufacturing</th>");
                    sb.Append("<th colspan='5' style='text-align:center;'>Losses</th>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                 
                    sb.Append("<th>Qty In Kg</th>");
                    sb.Append("<th>Fat In Kg</th>");
                    sb.Append("<th>Snf In Kg</th>");
                    sb.Append("<th>Name</th>");
                    sb.Append("<th>Qty In Kg</th>");
                    sb.Append("<th>Fat In Kg</th>");
                    sb.Append("<th>Snf In Kg</th>");
                    sb.Append("<th>Name</th>");
                    sb.Append("<th>Qty In Kg</th>");
                    sb.Append("<th>Fat In Kg</th>");
                    sb.Append("<th>Snf In Kg</th>");
                    sb.Append("<th>Qty In Kg</th>");
                    sb.Append("<th>Fat In Kg</th>");
                    sb.Append("<th>Snf In Kg</th>");
                    sb.Append("<th>FAT %</th>");
                    sb.Append("<th>SNF %</th>");

                    sb.Append("</tr>");
                    int Count = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["ItemTypeName"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["MRQty"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["MRFatKg"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["MRSnfKg"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["FSItemTypeName"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["FSQty"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["FSFatKg"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["FSSnfKg"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["ItemTypeName"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["SSQty"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["SSFatKg"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["SSSnfKg"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["LossesQty"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["LossesFatKg"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["LossesSnfKg"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["FatPer"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["SnfPer"].ToString() + "</td>");
                       
                        sb.Append("</tr>");
                    }
					decimal TotMrQty = 0;
                    decimal TotfatInKgQty = 0;
                    decimal TotSnfInKgQty = 0;

                    TotMrQty = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("MRQty"));
                    TotfatInKgQty = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("MRFatKg"));
                    TotSnfInKgQty = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("MRSnfKg"));
                    sb.Append("<tr>");
                    sb.Append("<td><b>Grand Total</b></td>");
                    sb.Append("<td><b>" + TotMrQty.ToString() + "</b></td>");
                    sb.Append("<td><b>" + TotfatInKgQty.ToString() + "</b></td>");
                    sb.Append("<td><b>" + TotSnfInKgQty.ToString() + "</b></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");

                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");
                }
            }
            
            divReport.InnerHtml = sb.ToString();
            divprint.InnerHtml = sb.ToString();
           
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
            Response.AddHeader("content-disposition", "attachment;filename=" + "Final Prodand Manu Details" + DateTime.Now + ".xls");
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
    public override void VerifyRenderingInServerForm(Control control)
    {


    }  
}