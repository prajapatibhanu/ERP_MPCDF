using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web;

public partial class mis_MilkCollection_BMCDCSRootWiseTestingReport : System.Web.UI.Page
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
                txtDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDate.Attributes.Add("readonly", "readonly");
               
                

            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    private void FillGrid()
    {
        try
        {
            btnExcel.Visible = false;
            gvReport.DataSource = string.Empty;
            gvReport.DataBind();
            ds = objdb.ByProcedure("Usp_Trn_MilkCollectionBMCEntryAtQC",
                                new string[] { "flag", "EntryDate", "Office_ID" },
                                new string[] { "9", Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),objdb.Office_ID() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvReport.DataSource = ds;
                    gvReport.DataBind();
                    btnExcel.Visible = true;
                    

                }
                else
                {
                    gvReport.DataSource = string.Empty;
                    gvReport.DataBind();
                }
            }
            else
            {
                gvReport.DataSource = string.Empty;
                gvReport.DataBind();
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5:" + ex.Message.ToString());
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
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            
            string FileName = "BMC/DCS RootWise Testing Report";
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gvReport.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
}