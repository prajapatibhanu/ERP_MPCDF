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

public partial class mis_MilkCollection_DateWiseSocietyMilkCollectionStatus : System.Web.UI.Page
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
                            ddlccbmcdetail.Items.Insert(0, new ListItem("All", "0"));
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
            //ds = objdb.ByProcedure("Usp_SocietyMilkCollectionStatusReports", new string[] { "flag", "FromDate", "ToDate", "Office_ID",  "V_Shift" }, new string[] { "2", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ddlccbmcdetail.SelectedValue, ddlShift.SelectedValue.ToString() }, "dataset");
			 ds = objdb.ByProcedure("Usp_SocietyMilkCollectionStatusReports", new string[] { "flag", "FromDate", "ToDate", "Office_ID", "V_Shift", "Office_Parant_ID", "OfficeType_ID" }, new string[] { "2", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ddlccbmcdetail.SelectedValue, ddlShift.SelectedValue.ToString(),objdb.Office_ID(),objdb.OfficeType_ID() }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    gvReports.DataSource = ds;
                    gvReports.DataBind();
                    btnExport.Visible = true;
					
					lblCount.Text = "No of Society having Milk Collection:  " + ds.Tables[1].Rows[0]["Count"].ToString();
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
            string FileName = "Society Milk Collection Status Report";
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
    protected void gvReports_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            lblMsg.Text = "";
            string Office_ID = e.CommandArgument.ToString();
            GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Label lblBMC = (Label)gvr.FindControl("lblBMC");

            if(e.CommandName == "ViewRecord")
            {
                ds = objdb.ByProcedure("Usp_SocietyMilkCollectionStatusReports", new string[] { "flag", "FromDate", "ToDate", "Office_Id", "V_Shift" }, new string[] { "3", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), Office_ID.ToString(),ddlShift.SelectedValue.ToString() }, "dataset");
                if(ds != null && ds.Tables.Count > 0)
                {
                    if(ds.Tables[0].Rows.Count > 0)
                    {
                        GVDetails.DataSource = ds;
                        GVDetails.DataBind();
                        decimal TotalQty = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("I_MilkSupplyQty"));
                        GVDetails.FooterRow.Cells[4].Text = "<b>Total : </b>";
                        GVDetails.FooterRow.Cells[5].Text = "<b>" + TotalQty.ToString() + "</b>";
                        
                        spnOfficeName.InnerHtml = lblBMC.Text;
                        spnDate.InnerHtml = txtFromDate.Text + " - " + txtToDate.Text ;
                        spnShift.InnerHtml = "Shift - " + ddlShift.SelectedValue;
                    }
                    else
                    {
                        GVDetails.DataSource = string.Empty;
                        GVDetails.DataBind();
                    }
                }
                else
                {
                    GVDetails.DataSource = string.Empty;
                    GVDetails.DataBind();
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModalDetail();", true);
            }
            if (e.CommandName == "ViewProducer")
            {
                ds = objdb.ByProcedure("Usp_SocietyMilkCollectionStatusReports", new string[] { "flag", "FromDate", "ToDate", "Office_Id", "V_Shift" }, new string[] { "4", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), Office_ID.ToString(), ddlShift.SelectedValue.ToString() }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvProducerDetails.DataSource = ds;
                        gvProducerDetails.DataBind();
                        spnOfficeName_P.InnerHtml = lblBMC.Text;
                        spnDate_P.InnerHtml = txtFromDate.Text + " - " + txtToDate.Text;
                    }
                    else
                    {
                        gvProducerDetails.DataSource = string.Empty;
                        gvProducerDetails.DataBind();
                    }
                }
                else
                {
                    gvProducerDetails.DataSource = string.Empty;
                    gvProducerDetails.DataBind();
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ProducerDetail();", true);
            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    protected void btnModalExport_Click(object sender, EventArgs e)
    {
        try
        {
            string FileName = "Milk Collection Report" + "_" + spnOfficeName.InnerHtml + " ( " + spnDate.InnerHtml + ")" + "_Shift -" + ddlShift.SelectedValue;
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            GVDetails.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
}