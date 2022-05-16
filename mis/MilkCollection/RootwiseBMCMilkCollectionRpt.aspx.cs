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

public partial class mis_MilkCollection_RootwiseBMCMilkCollectionRpt : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    MilkCalculationClass Obj_MC = new MilkCalculationClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!IsPostBack)
            {
                //txtFromDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                GetCCDetails();
                //txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //txtFromDate.Attributes.Add("readonly", "readonly");
                //txtToDate.Attributes.Add("readonly", "readonly");


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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblrptmsg.Text = "";
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    protected void FillGrid()
    {
        try
        {
            lblrptmsg.Text = "";
            lblMsg.Text = "";
            btnprint.Visible = false;
            btnExport.Visible = false;
            divRpt.InnerHtml = "";
            divprint.InnerHtml = "";
            string[] MonthyearPart = txtFdt.Text.Split('/');
            lblMsg.Text = "";
            string MonthName = Convert.ToDateTime(txtFdt.Text, cult).ToString("MMMM");
            string Month = MonthyearPart[0];
            string Year = MonthyearPart[1];

            ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntry", new string[] { "flag", "CC_ID", "Month", "Year" }, new string[] { "10", ddlccbmcdetail.SelectedValue, Month, Year }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnprint.Visible = true;
                    btnExport.Visible = true;
                    string BMC = "";
                    int J = 0;
                    int K = 0;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table border='1' class='table table-bordered'>");
                    sb.Append("<tr>");
                    //sb.Append("<td colspan='12' style='font-size:16px; text-align:center;'><b>" + Session["Office_Name"] + "(BULK MILK COOLER):" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "&nbsp;&nbsp;&nbsp;DATE:" + ds.Tables[0].Rows[i]["EntryDate"].ToString() + "&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[i]["Shift"].ToString() + "</b></td>");
                    sb.Append("<td colspan='8' style='font-size:16px; text-align:center;'><b><u>दुग्ध शीत केंद्र      " + ddlccbmcdetail.SelectedItem.Text + ",    बी.एम.सी. समितियों की संकलन जानकारी<br/></u><u>माह - " + MonthName + "</b></u></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<th style='width:50px; text-align:center;'>क्रं</th>");
                    sb.Append("<th style='text-align:center'>बी.एम.सी. समिति का नाम</th>");
                    sb.Append("<th style='text-align:center'>मार्ग क्रं</th>");
                    sb.Append("<th style='text-align:center'>मार्ग का नाम</th>");
                    sb.Append("<th style='text-align:center'>बी.एम.सी. से जुडी डी.सी.एस. के नाम</th>");
                    sb.Append("<th style='text-align:center'>दूध मात्रा</th>");
                    sb.Append("<th style='text-align:center'>Kg Fat</th>");
                    sb.Append("<th style='text-align:center'>KgSnf</th>");

                    sb.Append("</tr>");
                    int Count = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < Count; i++)
                    {
                        if (i == 0)
                        {
                            K += 1;
                            J +=1;
                            sb.Append("<tr>");
                            sb.Append("<td style='padding:20px;'>" + J.ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["BMC"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["BMCTankerRootName"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["BMCTankerRootDescription"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + K + "." + ds.Tables[0].Rows[i]["AttachedDCS"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["SnfInKg"].ToString() + "</td>");
                            sb.Append("</tr>");
                           


                        }
                        //else if (EntryDate == ds.Tables[0].Rows[i]["EntryDate"].ToString() && AttachedBMC_ID == ds.Tables[0].Rows[i]["AttachedBMC_ID"].ToString() && Shift == ds.Tables[0].Rows[i]["Shift"].ToString())
                        else if (BMC == ds.Tables[0].Rows[i]["BMC"].ToString())
                        {
                            K += 1;
                            sb.Append("<tr>");
                            sb.Append("<td style='padding:20px;'></td>");
                            sb.Append("<td style='padding:20px;'></td>");
                            sb.Append("<td style='padding:20px;'></td>");
                            sb.Append("<td style='padding:20px;'></td>");
                            sb.Append("<td style='padding:20px;'>" + K + "." + ds.Tables[0].Rows[i]["AttachedDCS"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["SnfInKg"].ToString() + "</td>");
                            sb.Append("</tr>");


                        }
                        else
                        {
                            K = 0;
                            K += 1;
                            J += 1;
                            sb.Append("<tr>");
                            sb.Append("<td style='padding:20px;'>" + J.ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["BMC"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["BMCTankerRootName"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["BMCTankerRootDescription"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + K + "." + ds.Tables[0].Rows[i]["AttachedDCS"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["SnfInKg"].ToString() + "</td>");
                            sb.Append("</tr>");
                            sb.Append("</tr>");
                            


                        }
                        BMC = ds.Tables[0].Rows[i]["BMC"].ToString();

                    }
                    sb.Append("</table>");
                    divRpt.InnerHtml = sb.ToString();
                    divprint.InnerHtml = sb.ToString();

                }
                else
                {
                    lblrptmsg.Text = "No Record Found";
                }
            }
            else
            {
                lblrptmsg.Text = "No Record Found";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "RootWiseBMCMilkCollectionMonthlyReport" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            divRpt.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());

            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}