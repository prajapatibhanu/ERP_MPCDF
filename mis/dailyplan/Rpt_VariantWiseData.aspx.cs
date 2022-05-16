using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class mis_dailyplan_Rpt_VariantWiseData : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    StringBuilder sb = new StringBuilder();
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
                    FillItemType();
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
    protected void FillItemType()
    {
        try
        {
            ds = objdb.ByProcedure("USP_VariantWiseReport", new string[] { "flag", "Office_ID", "OfficeType_ID" },
                      new string[] { "3", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlItemType.DataTextField = "ItemTypeName";
                ddlItemType.DataValueField = "ItemType_id";
                ddlItemType.DataSource = ds.Tables[0];
                ddlItemType.DataBind();
                ddlItemType.Items.Insert(0, new ListItem("All", "0"));
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
            gvDetail.DataSource = null;
            gvDetail.DataBind();
            ds = objdb.ByProcedure("USP_VariantWiseReport",
                new string[] { "flag", "Office_ID", "ProductSection_ID", "ItemType_id", "FromDate", "Todate" },
                new string[] { "2", ddlDS.SelectedValue, ddlPSection.SelectedValue, ddlItemType.SelectedValue, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                btnPrint.Visible = true;
                btnExcel.Visible = true;
                gvDetail.DataSource = ds;
                gvDetail.DataBind();
                decimal QtyInKg = ds.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("QtyInKg") ?? 0);
                decimal QtyInLtr = ds.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("QtyInLtr") ?? 0);
                //decimal FatPer = ds.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("Fat_Per") ?? 0);
                //decimal SNFPer = ds.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("Snf_Per") ?? 0);
                decimal FatInKg = ds.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("KgFat") ?? 0);
                decimal SNFinKg = ds.Tables[0].AsEnumerable().Sum(r => r.Field<decimal?>("KgSnf") ?? 0);
                gvDetail.FooterRow.Cells[2].Text = "<b>Total : </b>";
                gvDetail.FooterRow.Cells[3].Text = "<b>" + QtyInKg.ToString() + "</b>";
                gvDetail.FooterRow.Cells[4].Text = "<b>" + QtyInLtr.ToString() + "</b>";
                //gvDetail.FooterRow.Cells[5].Text = "<b>" + FatPer.ToString() + "</b>";
                //gvDetail.FooterRow.Cells[6].Text = "<b>" + SNFPer.ToString() + "</b>";
                gvDetail.FooterRow.Cells[7].Text = "<b>" + FatInKg.ToString() + "</b>";
                gvDetail.FooterRow.Cells[8].Text = "<b>" + SNFinKg.ToString() + "</b>";
            }
        }
        catch (Exception)
        {

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment; filename=VariantWiseReport" + (System.DateTime.Now.ToString("dd-MM-yyyy_HH_mm_ss_fff")) + ".xls");
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite =
        new HtmlTextWriter(stringWrite);
        gvDetail.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }
}