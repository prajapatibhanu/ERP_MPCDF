using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_dailyplan_PortableWaterTesting : System.Web.UI.Page
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
            ds = objdb.ByProcedure("SP_TblPortableWaterTesting", new string[] { "flag", "Office_ID", "FromDate", "ToDate" }, new string[] { "4", objdb.Office_ID(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txttoDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
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
        //txtEntryDate.Text = "";
        txtR_PH.Text = "";
        txtR_TDSPPM.Text = "";
        txtR_HardnessPPM.Text = "";
        txtS_PH.Text = "";
        txtS_TDSPPM.Text = "";
        txtS_HardnessPPM.Text = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                lblMsg.Text = "";
                string IsActive = "1";
                ds = objdb.ByProcedure("SP_TblPortableWaterTesting",
                                       new string[] { "flag", "EnrtyDate", "Row_PH", "Row_TDSPPM", "Row_HardnessPPM", "Soft_PH", "Soft_TDSPPM", "Soft_HardnessPPM", "IsActive", "Office_ID", "CreatedBy", "CreatedByIP" },
                                       new string[] { "1", Convert.ToDateTime(txtEntryDate.Text, cult).ToString("yyyy/MM/dd"), txtR_PH.Text, txtR_TDSPPM.Text, txtR_HardnessPPM.Text, txtS_PH.Text, txtS_TDSPPM.Text, txtS_HardnessPPM.Text, "1", objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");
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
            string FileName = "PortableWaterTesting_" + DateTime.Now.ToString("dd/MM/yyyy");
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
        Response.Redirect("PortableWaterTesting.aspx", false);
    }
    protected void gvDetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Row Water";
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "Soft Water";
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow.Cells.Add(HeaderCell);

            gvDetail.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
}