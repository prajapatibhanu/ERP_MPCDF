using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;

public partial class mis_EngneeringDepartment_EngneeringSectionEntryReport : System.Web.UI.Page
{
    DataSet ds, ds1 = new DataSet();
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                BindFY();
                GetOfficeDetails();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    private void BindFY()
    {
        ds = objdb.ByProcedure("Sp_FY", new string[] { "Flag" }, new string[] { "1" }, "dataset");

        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlYear.Items.Clear();
                    ddlYear.DataSource = ds.Tables[0];
                    ddlYear.DataTextField = "FY1";
                    ddlYear.DataValueField = "FY1";
                    ddlYear.DataBind();
                    ddlYear.Items.Insert(0, new ListItem("Select", "0"));


                }
            }
        }
    }
    protected void GetOfficeDetails()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string Officeid = objdb.Office_ID();
               // ViewState["Office_Name"] = ds.Tables[0].Rows[0]["Office_Name_E"].ToString();
                ViewState["Office_Name"] = ds.Tables[0].Rows[0]["Office_Name"].ToString() ;

                ViewState["Office_GST"] = ds.Tables[0].Rows[0]["Office_Gst"].ToString();
                ViewState["Office_Address"] = ds.Tables[0].Rows[0]["Office_Address"].ToString() + " ,Pin Code - " + ds.Tables[0].Rows[0]["Office_Pincode"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 6 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try

        {
            lblMsg.Text = "";
            if (ddlYear.SelectedIndex > 0)
            {
                if (ddlMonth.SelectedIndex > 0)
                {
                    misdetail.InnerHtml = "";
                    Print.InnerHtml = "";
                    GetSectionEntryDetails();
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "warning! ", "Please Select Month");
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "warning!  ", "Please Select Year");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void GetSectionEntryDetails()
    {
        try
        {
            string Date = DateTime.Now.ToString("dd/MM/yyyy");
            DateTime odate = DateTime.ParseExact(Date, "dd/MM/yyyy", culture);
           string Reportdate = odate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


             ds = objdb.ByProcedure("usp_Mst_ENGSectionEntry",
                                        new string[] { "flag", "Office_ID", "EntryMonth", "CreatedBy", "EntryYear" },
                                        new string[] { "6",objdb.Office_ID(),ddlMonth.SelectedValue, objdb.createdBy(),ddlYear.SelectedValue
                                            }, "dataset");
            string pstatus = "";
            //string monthyear = ddlMonth.SelectedValue+" - " + DateTime.Now.ToString("yyyy");
            string monthyear = ddlMonth.SelectedValue + " - " + ddlYear.SelectedValue;
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    ////////////////Start Of Route Wise Print Code   ///////////////////////
                    StringBuilder sb = new StringBuilder();
                   
                    sb.Append("<div class='table-responsive'");
                    sb.Append("<div class='content' style='border: 0px solid black'>");
                    sb.Append("<table class='table1' style='width:100%; height:100%;line-height:1.000000'>");
                    sb.Append("<tr >");
                    sb.Append("<td rowspan='3' style='width:200px;'  ><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                    sb.Append("<td class='text-center' style='font-size:20px'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                    sb.Append("<td rowspan='3' style='width:200px;'  ></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr class='text-center'>");
                    //sb.Append("<td></td>");
                    sb.Append("<td  style='font-size:15px;'><b>" + ViewState["Office_Address"].ToString() + "</b></td>");
                    //sb.Append("<td></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr class='text-center'>");
                    //sb.Append("<td></td>");
                    sb.Append("<td  style='font-size:15px;'><b>MIS REPORT</b></td>");
                    //sb.Append("<td></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");

                    sb.Append("<td ></td>");
                    //sb.Append("<td></td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    //sb.Append("<td></td>");
                    //sb.Append("<td></td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td class='text-left' colspan='2' ><b>Date :- " + ds.Tables[0].Rows[0]["EntryDate"].ToString() + "</b></td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td class='text-left' colspan='2'><b>Month :- " + ddlMonth.SelectedValue + "</b></td>");
                    sb.Append("</tr>");


                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("</tr>");

                    sb.Append("</table>");
                    sb.Append("<table class='table table1-bordered' > ");
                    int Count = ds.Tables[0].Rows.Count;
                    sb.Append("<thead style='padding-left:0px;'>");
                    sb.Append("<td colspan='5' class='text-center'><b>" + "Engg. Section MIS Report Month Of " + monthyear + "</b></td>");
                    //sb.Append("<td>Month</td>");

                   

                    sb.Append("</thead>");
                    //int ColCount = ds.Tables[0].Columns.Count;
                    sb.Append("<thead style='padding-left:0px;'>");
                    sb.Append("<td style='width:120px'><b>S No.</b></td>");
                    //sb.Append("<td>Month</td>");

                    sb.Append("<td><b>Head Name</b></td>");
                    sb.Append("<td><b>Sub Head Name</b></td>");
                    //sb.Append("<td><b>Number</b></td>");
                    sb.Append("<td><b>Qty</td>");
                    sb.Append("<td><b>Amount</b></td>");
                    //sb.Append("<td><b>Remark</b></td>");
                    //sb.Append("<td><b>Entry Date</b></td>");

                    sb.Append("</thead>");

                    for (int i = 0; i < Count; i++)
                    {
                        int SNO = i + 1;
                        sb.Append("<tr>");

                        sb.Append("<td style='text-align:left'>" + SNO + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[i]["Month"] + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["ENGHeadName"] + "</td>");
                        ds1 = objdb.ByProcedure("usp_Mst_ENGSectionEntry",
                                            new string[] { "flag", "Office_ID", "EntryMonth", "CreatedBy", "ENGHeadId","EntryYear" },
                                            new string[] { "7",objdb.Office_ID(),ddlMonth.SelectedValue, objdb.createdBy(),ds.Tables[0].Rows[i]["ENGHeadId"].ToString()
											,ddlYear.SelectedValue
                                            }, "dataset");
                        int subcount = ds1.Tables[0].Rows.Count;
                        for (int j = 0; j < subcount; j++)
                        {

                            if (j >0)
                            {
                                sb.Append("<tr>");

                                sb.Append("<td style='text-align:left'></td>");
                                sb.Append("<td></td>");
                            }
                            sb.Append("<td>" + ds1.Tables[0].Rows[j]["ENGSubHeadName"] + "</br>Unit - " + ds1.Tables[0].Rows[j]["Number"] + " " + "</br> Remark - " + ds1.Tables[0].Rows[j]["Remark"] + "" + "</td>");
                            //sb.Append("<td>" + ds1.Tables[0].Rows[j]["Number"] + "</td>");
                            sb.Append("<td>" + ds1.Tables[0].Rows[j]["Qty"] + "</td>");





                            sb.Append("<td>" + ds1.Tables[0].Rows[j]["Amount"] + "</td>");
                            //sb.Append("<td>" + ds1.Tables[0].Rows[j]["Remark"] + "</td>");
                            //sb.Append("<td>" + ds1.Tables[0].Rows[j]["EntryDate"] + "</td>");
                            //sb.Append("<td>" + ds.Tables[0].Rows[i]["Amount"] + "</td>");
                            //sb.Append("<td>" + ds.Tables[0].Rows[i]["Amount"] + "</td>");

                            sb.Append("</tr>");
                        }



                    }
                    sb.Append("<tr>");
                   // int ColumnCount = ds.Tables[0].Columns.Count - 2;






                    sb.Append("</table>");
                    sb.Append("<table class='table1' style='width:100%; height:100%;'>");
                    sb.Append("<tr style='height:50px'>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td class='text-right'><b>I/C (ENGG.)</b></td>");
                    sb.Append("</tr>");

                    sb.Append("</table>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    misdetail.InnerHtml = sb.ToString();
                    Print.InnerHtml = sb.ToString();
                    pnlData.Visible = true;
                   // btnPrint.Visible = true;
                    //ViewState["DistData"] += sb.ToString();
                    //ViewState["PStatus"] = "1";
                    ////////////////End Of Route Wise Print Code   ///////////////////////
                }
                else
                {
                    btnprint.Visible = false;
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Info! : ", "NO Record Found");
                }
            }
            else
            {
                btnprint.Visible = false;
                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Info! : ", "NO Record Found");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 4 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }

    }

    public IFormatProvider culture { get; set; }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename="+ddlMonth.SelectedItem.Text + "-" + "MISRpt.xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            misdetail.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
    }
}