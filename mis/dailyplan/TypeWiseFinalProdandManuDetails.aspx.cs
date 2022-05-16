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

public partial class mis_dailyplan_TypeWiseFinalProdandManuDetails : System.Web.UI.Page
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
                    FillType();
                    txtFDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtTDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    GetSectionView(sender, e);
                    btnprint.Visible = false;
                    btnExport.Visible = false;
                    divprint.InnerHtml = "";
                    //txtDate_TextChanged(sender, e);
                    

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
    protected void FillType()
    {
        try
        {
            ddlType.Items.Clear();
            ds = objdb.ByProcedure("Usp_Production_ProductIPSheetMaster",
                  new string[] { "flag", "Office_ID"},
                  new string[] { "13", ddlDS.SelectedValue }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    ddlType.DataSource = ds.Tables[0];
                    ddlType.DataTextField = "ItemTypeName";
                    ddlType.DataValueField = "ItemType_id";
                    ddlType.DataBind();
                    
                    

                }
            }
            ddlType.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch(Exception ex)
        {

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
           
            divprint.InnerHtml = "";
			divreport.InnerHtml = "";
            lblMsg.Text = "";
            string ItemTypeName = "";
            string Date = "";
			decimal OpeningBalance = 0;
            decimal TotalReturn = 0;
            decimal TotalManufactured = 0;
            decimal TotalSample = 0;
            decimal TotalDispatch = 0;
            decimal ClosingBalance = 0;
			 btnprint.Visible = false;
             btnExport.Visible = false;
            StringBuilder sb = new StringBuilder();
            ds = objdb.ByProcedure("Usp_Production_ProductIPSheetMaster", new string[] { "flag", "FDate", "TDate", "Office_ID", "ItemType_id" }, new string[] { "14", Convert.ToDateTime(txtFDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTDate.Text, cult).ToString("yyyy/MM/dd"), objdb.Office_ID(),ddlType.SelectedValue }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
				
                if (ds.Tables[0].Rows.Count > 0)
                {

                    btnprint.Visible = true;
                    btnExport.Visible = true;
                    sb.Append("<div class='row'");
                    sb.Append("<div class='col-md-12' style='text-align:center'><h3 style='font-weight: 800; font-size: 20px;'>" + Session["Office_Name"] + "</h3><h5 style='font-weight: 500; font-size: 13px;'>DAIRY PLANT</h5><h5 style='font-weight: 800; font-size: 20px;'>TYPE WISE FINAL PRODUCT AND MANUFACTURED DETAILS</h5></div>");
                    sb.Append("<div class='col-md-12' style='text-align:center'>Date:" + txtFDate.Text + " - " + txtTDate.Text + "</div>");
                    sb.Append("<table class='table table-bordered'>");
                    sb.Append("<tr>");
                    sb.Append("<th style='text-align:center;'>Date</th>");
                    sb.Append("<th style='text-align:center;'>Name</th>");
                    sb.Append("<th style='text-align:center;'>Packing Size</th>");
                    sb.Append("<th style='text-align:center;'>Opening balance</th>");
                    sb.Append("<th style='text-align:center;'>Return</th>");
                    sb.Append("<th style='text-align:center;'>Manufactured</th>");
                    sb.Append("<th style='text-align:center;'>Sample</th>");
                    sb.Append("<th style='text-align:center;'>Dispatch</th>");
                    sb.Append("<th style='text-align:center;'>Closing Balance</th>");
                    sb.Append("</tr>");
                    
                    int Count = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < Count; i++)
                    {
						if (ds.Tables[0].Rows[i]["Date"].ToString() == txtFDate.Text)
                        {
                            OpeningBalance += decimal.Parse(ds.Tables[0].Rows[i]["OpeningBalance"].ToString());
                        }
                          sb.Append("<tr>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Date"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["ItemTypeName"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["PackingSize"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["OpeningBalance"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Return"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Manufactured"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Sample"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Dispatch"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["ClosingBalance"].ToString() + "</td>");


                            sb.Append("</tr>");
                       
                       
                    }

					TotalReturn = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Return"));
                    TotalManufactured = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Manufactured"));
                    TotalSample = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Sample"));
                    TotalDispatch = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Dispatch"));

                    ClosingBalance = OpeningBalance + TotalReturn + TotalManufactured - TotalSample - TotalDispatch;

                    sb.Append("<tr>");
                    sb.Append("<td colspan='3'>Total</td>");
                    sb.Append("<td>" + OpeningBalance.ToString() + "</td>");
                    sb.Append("<td>" + TotalReturn.ToString() + "</td>");
                    sb.Append("<td>" + TotalManufactured.ToString() + "</td>");
                    sb.Append("<td>" + TotalSample.ToString() + "</td>");
                    sb.Append("<td>" + TotalDispatch.ToString() + "</td>");
                    sb.Append("<td>" + ClosingBalance.ToString() + "</td>");
                    sb.Append("</tr>");

                    sb.Append("</table>");                    
                    sb.Append("</div>");
                    divreport.InnerHtml = sb.ToString();
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