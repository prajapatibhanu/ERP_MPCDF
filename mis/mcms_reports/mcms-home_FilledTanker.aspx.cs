using System;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_MilkCollection_mcms_home_FilledTanker : System.Web.UI.Page
{
    APIProcedure apiprocedure = new APIProcedure();
    IFormatProvider cult = new CultureInfo("en-IN", true);
    DataSet ds, ds2;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            
            FillGrid();
        }
    }

    private void FillGrid()
    {
        try
        {
            gvDispatchEntry.DataSource = null;
            gvDispatchEntry.DataBind();

            gvReceivedEntry.DataSource = null;
            gvReceivedEntry.DataBind();
            btnExport.Visible = false;
            string FromDate = "", ToDate = "";
            if (txtFromDate.Text != "")
            {
                FromDate = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            }
            if (txtToDate.Text != "")
            {
                ToDate = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            }

            if (apiprocedure.OfficeType_ID() == "2") // 1 Is DS
            {
                gvReceivedEntry.Visible = true;
                gvDispatchEntry.Visible = false;
                btnExport.Visible = false;

                ds = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                                new string[] { "flag", "Office_ID ", "V_EntryType", "FromDate", "ToDate" },
                                new string[] { "42", apiprocedure.Office_ID(), "out", FromDate, ToDate }, "dataset");

                gvReceivedEntry.DataSource = ds;
                gvReceivedEntry.DataBind();

            }
            else if (apiprocedure.OfficeType_ID() == "4") // 2 Is CC
            {
                gvReceivedEntry.Visible = false;
                gvDispatchEntry.Visible = true;
                btnExport.Visible = true;
                ds = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                                new string[] { "flag", "Office_ID ", "V_EntryType", "FromDate", "ToDate" },
                                new string[] { "42", apiprocedure.Office_ID(), "In", FromDate, ToDate }, "dataset");
                gvDispatchEntry.DataSource = ds;
                gvDispatchEntry.DataBind();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void gvDispatchEntry_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewEntry")
        {
            ds = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                            new string[] { "flag", "V_ReferenceCode" },
                            new string[] { "14", e.CommandArgument.ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
            {
                lblVehicleNo.Text = e.CommandArgument.ToString();

                gvQCDetailsForCC.DataSource = ds.Tables[0]; //For CC
                gvQCDetailsForCC.DataBind();

                gvQCDetailsForDS.DataSource = ds.Tables[1]; //For DS
                gvQCDetailsForDS.DataBind();

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowQCDetails();", true);
            }
            else
            {
                gvQCDetailsForCC.DataSource = null;
                gvQCDetailsForCC.DataBind();

                gvQCDetailsForDS.DataSource = null;
                gvQCDetailsForDS.DataBind();

                lblMsg.Text = apiprocedure.Alert("fa-info", "alert-info", "Info!", "No Data Found!");
            }
        }
    }

    protected void gvDispatchEntry_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtn = (LinkButton)e.Row.FindControl("lnkViewMore");
            GridView gridview1 = (GridView)e.Row.FindControl("gridview1");
            GridView gridview2 = (GridView)e.Row.FindControl("gridview2");
            ds = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                            new string[] { "flag", "V_ReferenceCode" },
                            new string[] { "14", lbtn.CommandArgument.ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
            {


                gridview1.DataSource = ds.Tables[0]; //For CC
                gridview1.DataBind();

                gridview2.DataSource = ds.Tables[1]; //For DS
                gridview2.DataBind();

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowQCDetails();", true);
            }
            else
            {
                gridview1.DataSource = null;
                gridview1.DataBind();

                gridview2.DataSource = null;
                gvQCDetailsForDS.DataBind();

                //lblMsg.Text = apiprocedure.Alert("fa-info", "alert-info", "Info!", "No Data Found!");
            }

        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            gvDispatchEntry.Columns[10].Visible = false;
            gvDispatchEntry.Columns[11].Visible = true;
            gvDispatchEntry.Columns[12].Visible = false;
            lblMsg.Text = "";
            string FileName = Session["Office_Name"].ToString() + "_" + "DispatchTankerDetails";
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gvDispatchEntry.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            gvDispatchEntry.Columns[10].Visible = true;
            gvDispatchEntry.Columns[11].Visible = false;
            gvDispatchEntry.Columns[12].Visible = true;
            Response.End();

        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        try
        {


        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
}