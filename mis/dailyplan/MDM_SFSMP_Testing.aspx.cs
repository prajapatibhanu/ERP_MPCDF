using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_dailyplan_MDM_SFSMP_Testing : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null)
        {
            if (!IsPostBack)
            {
                lblMsg.Text = "";
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                txtEntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtEntryDate.Attributes.Add("readonly", "readonly");
                txtUsedByDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtUsedByDate.Attributes.Add("readonly", "readonly");
                txtFromDate.Text = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
                txtFromDate.Attributes.Add("readonly", "readonly");
                txttoDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txttoDate.Attributes.Add("readonly", "readonly");
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx", false);
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void FillGrid()
    {
        try
        {
            btnExcel.Visible = false;
            ds = objdb.ByProcedure("Sp_tblMDM_SFSMP_Testing",
                new string[] { "flag", "Office_ID", "FromDate", "Todate" },
                new string[] { "2", objdb.Office_ID(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txttoDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnExcel.Visible = true;
                    gvDetail.DataSource = ds;
                    gvDetail.DataBind();
                }
                else
                {
                    gvDetail.DataSource = string.Empty;
                    gvDetail.DataBind();
                }
            }
            else
            {
                gvDetail.DataSource = string.Empty;
                gvDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        // txtEntryDate.Text = "";
        txtAcidity.Text = "";
        txtAppearance.Text = "";
        txtBatchNo.Text = "";
        txtColor.Text = "";
        txtFat.Text = "";
        txtFlavour.Text = "";
        txtInsolubiLity.Text = "";
        ddlNeutralizer.ClearSelection();
        txtSweetness.Text = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                lblMsg.Text = "";
                string IsActive = "1";
                ds = objdb.ByProcedure("Sp_tblMDM_SFSMP_Testing",
                                       new string[] { "flag", "MfgDate", "UsedByDate", "BNo", "Flavour", "OT", "Apperance", "Color", "Sweetness", "Fat_Per", "Acidity", "Neutralizer", "Insolubility", "Office_ID", "Isactive", "CreatedBy", "CreatedByIP" },
                                       new string[] { "1", Convert.ToDateTime(txtEntryDate.Text, cult).ToString("yyyy/MM/dd"),Convert.ToDateTime(txtUsedByDate.Text, cult).ToString("yyyy/MM/dd"), 
                                           txtBatchNo.Text, txtFlavour.Text, ddlOT.SelectedValue.ToString(), txtAppearance.Text, txtColor.Text, txtSweetness.Text,txtFat.Text, txtAcidity.Text, ddlNeutralizer.SelectedValue.ToString(), txtInsolubiLity.Text, objdb.Office_ID(),"1",  objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", Success.ToString());

                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                    {
                        string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", Warning.ToString());

                    }
                    else
                    {
                        string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Danger.ToString());
                    }
                    ClearText();
                    FillGrid();
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = "MDM_SFSMP_Testing_" + DateTime.Now.ToString("dd/MM/yyyy");
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gvDetail.RenderControl(htmlWrite);
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
        Response.Redirect("MDM_SFSMP_Testing.aspx", false);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }
}