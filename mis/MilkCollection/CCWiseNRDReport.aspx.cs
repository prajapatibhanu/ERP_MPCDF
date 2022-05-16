using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class mis_MilkCollection_CCWiseNRDReport : System.Web.UI.Page
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
                    divData.Visible = false;
                    FillOffice();
                    FillCC();
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
               
                if(objdb.OfficeType_ID() == "2")
                {
                    ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                    ddlDS.SelectedValue = ViewState["Office_ID"].ToString();
                    ddlDS.Enabled = false;
                }
                else
                {
                    DataSet ds1 = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "Office_ID" }, new string[] { "58", ViewState["Office_ID"].ToString() }, "dataset");
                    if(ds1 != null && ds1.Tables.Count > 0)
                    {
                        if(ds1.Tables[0].Rows.Count > 0)
                        {
                            ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                            ddlDS.SelectedValue = ds1.Tables[0].Rows[0]["Office_Parant_ID"].ToString();
                            ddlDS.Enabled = false;
                        }
                    }
                }
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
    protected void FillCC()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU", new string[] { "flag", "I_OfficeID","I_OfficeTypeID" },
                      new string[] { "3", objdb.Office_ID(),objdb.OfficeType_ID() }, "dataset");

            ddlCCName.DataSource = ds.Tables[0];
            ddlCCName.DataTextField = "Office_Name";
            ddlCCName.DataValueField = "Office_ID";
            ddlCCName.DataBind();
            ddlCCName.Items.Insert(0, new ListItem("All", "0"));
            if(objdb.OfficeType_ID() == "2")
            {

            }
            else
            {
                ddlCCName.SelectedValue = ViewState["Office_ID"].ToString();
                ddlCCName.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            FillDetail();
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
		gvDetail.AllowPaging = false;
        FillDetail();
        
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment; filename=CCWiseNRDReport" + (System.DateTime.Now.ToString("dd-MM-yyyy_HH_mm_ss_fff")) + ".xls");
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite =
        new HtmlTextWriter(stringWrite);
        gvDetail.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        gvDetail.AllowPaging = true;
        Response.End();
    }
    protected void FillDetail()
    {
        gvDetail.DataSource = null;
        gvDetail.DataBind();
        ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports",
            new string[] { "flag", "CCID", "Office_ID", "FromDate", "Todate" },
            new string[] { "24", ddlCCName.SelectedValue, ddlDS.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            divData.Visible = true;
            lblOfficeName.Text = "<b>" + ddlDS.SelectedItem.Text + "</b>";
            lblCCName.Text = "<b>" + ddlCCName.SelectedItem.Text + "</b>";
            lblPeriod.Text = "<b>" + Convert.ToDateTime(txtFromDate.Text, cult).ToString("dd/MM/yyyy") + " - " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd/MM/yyyy") + "</b>";
            btnPrint.Visible = true;
            btnExcel.Visible = true;
            gvDetail.DataSource = ds;
            gvDetail.DataBind();
            gvDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            gvDetail.UseAccessibleHeader = true;
            gvDetail.FooterRow.Cells[2].Text = "<b>Total : </b>";
            decimal HeadAmount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("HeadAmount"));
            gvDetail.FooterRow.Cells[3].Text = "<b>" + HeadAmount.ToString() + "</b>";
        }
    }
    protected void gvDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            FillDetail();
            gvDetail.PageIndex = e.NewPageIndex;
            gvDetail.DataBind();
        }
        catch (Exception)
        {

            throw;
        }
    }
}