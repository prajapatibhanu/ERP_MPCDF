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

public partial class mis_MilkCollection_MonthlyRootwiseMISRpt : System.Web.UI.Page
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

            ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntry", new string[] { "flag", "CC_ID", "Month", "Year" }, new string[] { "11", ddlccbmcdetail.SelectedValue, Month, Year }, "dataset");
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
                    sb.Append("<table  class='table'>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='5' style='font-size:16px; text-align:center;'><b>" + Session["Office_Name"] + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='5' style='font-size:16px; text-align:center;'><b>CC :     " + ddlccbmcdetail.SelectedItem.Text + "<br/><b>Monthly Routwise MIS Report</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td>Quantity</th>");
                    sb.Append("<td>Kg.Fat</th>");
                    sb.Append("<td>Kg.SNF</th>");
                    //sb.Append("<td>Head Load</th>");
                    sb.Append("<td>No of Funct.DCS</th>");                 
                    sb.Append("</tr>");
                    int Count = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td style='width:2%;'>**</td>");
                        sb.Append("<td>Route Code  " + ds.Tables[0].Rows[i]["BMCTankerRootName"].ToString() + "</td>");
                        sb.Append("<td>"+ MonthName.ToString()+"</td>");
                        sb.Append("<td>" + Year.ToString() + "</td>");
                        sb.Append("<td></td>");
                       // sb.Append("<td></td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td style='width:2%;'>**</td>");
                        sb.Append("<td>Subtotal&nbsp;&nbsp;&nbsp;&nbsp;**</td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        //sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td style='width:2%;'></td>");                     
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["MilkQuantity"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["FatInKg"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["SnfInKg"].ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[i]["HeadLoad"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["NooffuncDCS"].ToString() + "</td>");
                        sb.Append("</tr>");
                            
                           
                           
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
            Response.AddHeader("content-disposition", "attachment;filename=" + "MonthlyRoutWiseMISReport" + DateTime.Now + ".xls");
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