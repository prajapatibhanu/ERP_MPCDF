using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_dailyplan_Milk_powder_Conversion : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    string Fdate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                lblMsg.Text = "";

                if (!IsPostBack)
                {
                    txtDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                    txtDate.Attributes.Add("readonly", "readonly");
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    btnPrint.Visible = false;
                    btnExport.Visible = false;
                    divprint.InnerHtml = "";
                    FillOffice();
                    GetSectionView(sender, e);
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            
            gvInflow.DataSource = null;
            gvInflow.DataBind();
            gvOutflow.DataSource = null;
            gvOutflow.DataBind();
            gvVariation.DataSource = null;
            gvVariation.DataBind();
             ds = objdb.ByProcedure("USP_MilkPowderConversionEntry", new string[] { "flag", "EntryDate", "Office_ID" },
                new string[] { "3", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd") ,objdb.Office_ID()}, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0 && ds.Tables[2].Rows.Count > 0)
                {
                    btnPrint.Visible = true;
                    btnExport.Visible = true;
                    lblOfficeName.Text = Session["Office_Name"].ToString();
                    lbldairy.Text = "DAIRY PLANT";
                    lblSheetName.Text = "Milk Powder Conversion Report";
                    lblDate.Text =  txtDate.Text;
                    gvInflow.DataSource = ds.Tables[0];
                    gvInflow.DataBind();
                    gvInflow.FooterRow.Cells[1].Text = "<b>Total </b>";
                    decimal IQty = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Qty"));
                    decimal IFatInKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    decimal ISnfInKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SNFInKg"));
                    gvInflow.FooterRow.Cells[2].Text = "<b>" + IQty.ToString() + "</b>";
                    gvInflow.FooterRow.Cells[4].Text = "<b>" + IFatInKg.ToString() + "</b>";
                    gvInflow.FooterRow.Cells[6].Text = "<b>" + ISnfInKg.ToString() + "</b>";
                   

                    gvOutflow.DataSource = ds.Tables[1];
                    gvOutflow.DataBind();
                    gvOutflow.FooterRow.Cells[1].Text = "<b>Total </b>";
                    decimal OQty = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Qty"));
                    decimal OFatInKg = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    decimal OSnfInKg = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SNFInKg"));
                    gvOutflow.FooterRow.Cells[2].Text = "<b>" + OQty.ToString() + "</b>";
                    gvOutflow.FooterRow.Cells[4].Text = "<b>" + OFatInKg.ToString() + "</b>";
                    gvOutflow.FooterRow.Cells[6].Text = "<b>" + OSnfInKg.ToString() + "</b>";
                   
                    

                    gvVariation.DataSource = ds.Tables[2];
                    gvVariation.DataBind();


                    //gvVariation.FooterRow.Cells[0].Text = "<b>Total </b>";
                    //decimal VQty = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    //decimal VFatInKg = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("SNFInKg"));
                    //gvVariation.FooterRow.Cells[1].Text = "<b>" + VQty.ToString() + "</b>";
                    //gvVariation.FooterRow.Cells[2].Text = "<b>" + VFatInKg.ToString() + "</b>";
                }
            }
        }
        catch (Exception ex)
        {

            throw; 
        }
    }
    protected void btnGetTotal_Click(object sender, EventArgs e)
    {

    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "Milk Powder Conversion Report" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            dvdetails.RenderControl(htmlWrite);

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