using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using System.Web.UI;
using System.Web;

public partial class mis_MilkCollection_DCSWiseMilkCollectionSummary : System.Web.UI.Page
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

                GetCCDetails();
				FillSociety();
                txtFDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFDate.Attributes.Add("readonly", "readonly");

                txtTDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtTDate.Attributes.Add("readonly", "readonly");



               
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void GetCCDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                     new string[] { "flag", "I_OfficeID", "I_OfficeTypeID" },
                     new string[] { "3", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlccbmcdetail.DataTextField = "Office_Name";
                        ddlccbmcdetail.DataValueField = "Office_ID";


                        ddlccbmcdetail.DataSource = ds;
                        ddlccbmcdetail.DataBind();

                        if (objdb.OfficeType_ID() == "2")
                        {
                            ddlccbmcdetail.Items.Insert(0, new ListItem("Select", "0"));
                        }
                        else
                        {
                            ddlccbmcdetail.SelectedValue = objdb.Office_ID();
                            //GetMCUDetails();
                            ddlccbmcdetail.Enabled = false;
                        }
                       


                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlccbmcdetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSociety();
    }
    protected void FillSociety()
    {
        try
        {
            ddlSociety.ClearSelection();
            ddlSociety.Items.Clear();
            if (ddlccbmcdetail.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("Usp_MilkCollectionRoutWiseAdditionsDeductionsEntry",
                         new string[] { "flag", "Office_ID", "Office_Parant_ID", "OfficeType_ID" },
                         new string[] { "8", ddlccbmcdetail.SelectedValue.ToString(), objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSociety.DataTextField = "Office_Name";
                        ddlSociety.DataValueField = "Office_ID";
                        ddlSociety.DataSource = ds.Tables[0];
                        ddlSociety.DataBind();
                        // ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    else
                    {
                        //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));

                    }
                }
                else
                {
                    //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                // Response.Redirect("SocietywiseMilkCollectionInvoiceDetails.aspx", false);
            }
            ddlSociety.Items.Insert(0, new ListItem("All", "0"));

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
            lblMsg.Text = "";
            btnExport.Visible = false;
            btnprint.Visible = false;
            GvReport.DataSource = string.Empty;
            GvReport.DataBind();
            GvReport1.DataSource = string.Empty;
            GvReport1.DataBind();
            tblreport.Visible = false;
            decimal KgFat = 0;
            decimal KgSnf = 0;
            decimal Quantity = 0;
            ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports", new string[] { "flag", "CCID", "Office_ID", "FromDate", "ToDate", "Shift", "MilkQuality" },
                new string[] { "37", ddlccbmcdetail.SelectedValue, ddlSociety.SelectedValue, Convert.ToDateTime(txtFDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTDate.Text, cult).ToString("yyyy/MM/dd"),ddlShift.SelectedItem.Text, ddlQualityType.SelectedItem.Text }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    tblreport.Visible = true;
                    btnExport.Visible = true;
                    btnprint.Visible = true;
                    spnofc.InnerText = Session["Office_Name"].ToString();
                    spnsociety.InnerText = ddlSociety.SelectedItem.Text;
                    spndate.InnerText = txtFDate.Text + "  -  " + txtTDate.Text;
                    Span1.InnerText = Session["Office_Name"].ToString();
                    Span2.InnerText = ddlSociety.SelectedItem.Text;
                    Span3.InnerText = txtFDate.Text + "  -  " + txtTDate.Text;
                    Quantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("MilkQuantity"));
                    KgFat = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    KgSnf = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                    GvReport.DataSource = ds;
                    GvReport.DataBind();
                    GvReport.FooterRow.Cells[6].Text = "<b>Total : </b>";
                    GvReport.FooterRow.Cells[7].Text = "<b>" + Quantity.ToString() + "</b>";
                    GvReport.FooterRow.Cells[11].Text = "<b>" + KgFat.ToString() + "</b>";
                    GvReport.FooterRow.Cells[12].Text = "<b>" + KgSnf.ToString() + "</b>";
                    GvReport.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    GvReport.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                    GvReport.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                    GvReport1.DataSource = ds;
                    GvReport1.DataBind();
                    GvReport1.FooterRow.Cells[6].Text = "<b>Total : </b>";
                    GvReport1.FooterRow.Cells[7].Text = "<b>" + Quantity.ToString() + "</b>";
                    GvReport1.FooterRow.Cells[11].Text = "<b>" + KgFat.ToString() + "</b>";
                    GvReport1.FooterRow.Cells[12].Text = "<b>" + KgSnf.ToString() + "</b>";
                    GvReport1.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    GvReport1.FooterRow.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                    GvReport1.FooterRow.Cells[12].HorizontalAlign = HorizontalAlign.Right;


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
            Response.AddHeader("content-disposition", "attachment;filename=" + "DCSMilkPurchaseSummary(CycleWise)" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            tblreport.RenderControl(htmlWrite);

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
        try
        {


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
}