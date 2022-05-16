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

public partial class mis_MilkCollection_CCWiseMilkCollectionandBillingStatus : System.Web.UI.Page
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
                txtFromDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                txtToDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                
               // FillBMCRoot();
                GetCCDetails();

            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    //public void FillBMCRoot()
    //{

    //    try
    //    {
    //        ds = null;
    //        ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
    //                  new string[] { "flag","Office_ID" },
    //                  new string[] { "1",objdb.Office_ID() }, "dataset");

    //        if (ds.Tables[0].Rows.Count != 0)
    //        {
    //            ddlBMCTankerRootName.DataTextField = "BMCTankerRootName";
    //            ddlBMCTankerRootName.DataValueField = "BMCTankerRoot_Id";
    //            ddlBMCTankerRootName.DataSource = ds;
    //            ddlBMCTankerRootName.DataBind();
    //            ddlBMCTankerRootName.Items.Insert(0, new ListItem("Select", "0"));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
    //    }

    //}
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            btnExport.Visible = false;
            lblCount.Text = "";
            ds = objdb.ByProcedure("Usp_SocietyMilkCollectionStatusReports", new string[] { "flag", "FromDate", "ToDate", "Office_ID" }, new string[] { "5", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ddlccbmcdetail.SelectedValue}, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    gvReports.DataSource = ds;
                    gvReports.DataBind();
                    btnExport.Visible = true;

                    //lblCount.Text = "No of Society having 0 Milk Collection:  " + ds.Tables[1].Rows[0]["Count"].ToString();
                }

                else
                {
                    gvReports.DataSource = string.Empty;
                    gvReports.DataBind();
                    lblCount.Text = "";
                }
            }
            else
            {
                gvReports.DataSource = string.Empty;
                gvReports.DataBind();
                lblCount.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = "Society Milk Collection Entry/Billing Status Report";
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gvReports.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    
    


}