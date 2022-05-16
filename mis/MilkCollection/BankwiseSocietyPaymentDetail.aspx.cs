using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using System.Web.UI;
using System.Web;

public partial class mis_MilkCollection_BankwiseSocietyPaymentDetail : System.Web.UI.Page
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
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtFdt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                txtTdt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                GetBank();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    private void GetBank()
    {
        try
        {
            ddlBank.DataSource = objdb.ByProcedure("sp_tblPUBankMaster",
                            new string[] { "flag" },
                            new string[] { "1" }, "dataset");
            ddlBank.DataTextField = "BankName";
            ddlBank.DataValueField = "Bank_id";
            ddlBank.DataBind();
            ddlBank.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }


    //protected void ddlMilkCollectionUnit_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillSociety();
    //}

    //protected void FillSociety()
    //{
    //    try
    //    {
    //        if (ddlMilkCollectionUnit.SelectedValue != "0")
    //        {
    //            ds = null;
    //            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
    //                              new string[] { "flag", "Office_ID", "OfficeType_ID" },
    //                              new string[] { "10", ViewState["Office_ID"].ToString(), ddlMilkCollectionUnit.SelectedValue }, "dataset");

    //            if (ds.Tables.Count > 0)
    //            {
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    ddlSociety.DataTextField = "Office_Name";
    //                    ddlSociety.DataValueField = "Office_ID";
    //                    ddlSociety.DataSource = ds.Tables[2];
    //                    ddlSociety.DataBind();
    //                    ddlSociety.Items.Insert(0, new ListItem("All", "0"));
    //                }
    //                else
    //                {
    //                    ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
    //                }
    //            }
    //            else
    //            {
    //                ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
    //            }
    //        }
    //        else
    //        {
    //            Response.Redirect("SocietywiseMilkCollectionInvoiceDetails.aspx", false);
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    //protected void lblviewresult_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
    //        LinkButton lblviewresult = (LinkButton)gv_SocietyMilkDispatchDetail.Rows[selRowIndex].FindControl("lblviewresult");
    //        ds = objdb.ByProcedure("USP_SocietywiseMilkCollectionInvoice",
    //                new string[] { "flag", "MilkCollectionInvoice_ID" },
    //                 new string[] { "5", lblviewresult.CommandArgument }, "dataset");

    //        if (ds != null)
    //        {
    //            if (ds.Tables.Count > 0)
    //            {
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    StringBuilder sb = new StringBuilder();
    //                    sb.Append("<div class='content' style='border:1px solid black; width:100%'>");
    //                    sb.Append("<table class='table1'  style='width:100%; margin:10px;'>");
    //                    sb.Append("<tr>");
    //                    sb.Append("<td>Society Name And Code: " + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "  /  " + ds.Tables[0].Rows[0]["Office_Code"].ToString() + "");
    //                    sb.Append("</td>");
    //                    sb.Append("<td style='text-align:right'>Route: " + ds.Tables[0].Rows[0]["RouteNo"].ToString() + "");
    //                    sb.Append("</td>");
    //                    sb.Append("</tr>");
    //                    sb.Append("<tr >");
    //                    sb.Append("<td >Bank Name / IFSC / Account  : " + ds.Tables[0].Rows[0]["BankName"].ToString() + " /" + ds.Tables[0].Rows[0]["IFSC"].ToString() + " /" + ds.Tables[0].Rows[0]["BankAccountNo"].ToString());
    //                    sb.Append("</td>");
    //                    sb.Append("<td style='text-align:right'>Billing Period  : " + ds.Tables[0].Rows[0]["FromDate"].ToString() + " To " + ds.Tables[0].Rows[0]["ToDate"].ToString() + "");
    //                    sb.Append("</td>");
    //                    sb.Append("</tr>");
    //                    sb.Append("</table>");

    //                    sb.Append("<table  class='table1'  style='width:100%; margin-top:20px;'>");
    //                    sb.Append("<tr style='border-bottom:dashed; border-top:dashed'>");
    //                    sb.Append("<td>Date</td>");
    //                    sb.Append("<td>M / E</td>");
    //                    sb.Append("<td>Buf / Cow</td>");
    //                    sb.Append("<td>Fat</td>");
    //                    sb.Append("<td>S.N.F</td>");
    //                    sb.Append("<td>Kg.Wt</td>");
    //                    sb.Append("<td>Kg.Fat</td>");
    //                    sb.Append("<td>Kg.Snf</td>");
    //                    sb.Append("<td>Value</td>");
    //                    sb.Append("<td>Clr</td>");
    //                    sb.Append("<td>Quality</td>");
    //                    sb.Append("</tr>");
    //                    int Count = ds.Tables[1].Rows.Count;

    //                    for (int i = 0; i < Count; i++)
    //                    {
    //                        sb.Append("<tr>");
    //                        sb.Append("<td>" + ds.Tables[1].Rows[i]["DispDate"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[1].Rows[i]["Shift"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkType"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[1].Rows[i]["FatPer"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[1].Rows[i]["SnfPer"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkQuantity"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[1].Rows[i]["FatInKg"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[1].Rows[i]["SnfInKg"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkValue"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[1].Rows[i]["CLR"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkQuality"].ToString() + "</td>");
    //                        sb.Append("</tr>");
    //                    }
    //                    decimal FatPer = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("FatPer"));
    //                    decimal SnfPer = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SnfPer"));
    //                    decimal MilkQuantity = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("MilkQuantity"));
    //                    decimal FatInKg = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
    //                    decimal SnfInKg = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
    //                    decimal MilkValue = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("MilkValue"));

    //                    sb.Append("<tr style='border-top:dashed; padding-top:20px;'>");
    //                    sb.Append("<td>Total</td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td>" + FatPer.ToString() + "</td>");
    //                    sb.Append("<td>" + SnfPer.ToString() + "</td>");
    //                    sb.Append("<td>" + MilkQuantity.ToString() + "</td>");
    //                    sb.Append("<td>" + FatInKg.ToString() + "</td>");
    //                    sb.Append("<td>" + SnfInKg.ToString() + "</td>");
    //                    sb.Append("<td>" + FatPer.ToString() + "</td>");
    //                    sb.Append("<td>" + MilkValue.ToString() + "</td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("<td></td>");
    //                    sb.Append("</tr>");
    //                    sb.Append("<tr>");
    //                    sb.Append("<td colspan='11'>Particulars:</td>");
    //                    sb.Append("</tr>");

    //                    int BufCount = ds.Tables[2].Rows.Count;

    //                    for (int j = 0; j < BufCount; j++)
    //                    {
    //                        if (j == 0)
    //                        {
    //                            sb.Append("<td>" + ds.Tables[2].Rows[j]["MilkType"].ToString() + "</td>");
    //                        }
    //                        else
    //                        {
    //                            sb.Append("<td></td>");
    //                        }
    //                        sb.Append("<td>" + ds.Tables[2].Rows[j]["MilkQuality"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[2].Rows[j]["FatPer"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[2].Rows[j]["SnfPer"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[2].Rows[j]["MilkQuantity"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[2].Rows[j]["FatInKg"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[2].Rows[j]["SnfInKg"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[2].Rows[j]["MilkValue"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[2].Rows[j]["CLR"].ToString() + "</td>");

    //                        sb.Append("</tr>");

    //                    }
    //                    int CowCount = ds.Tables[3].Rows.Count;

    //                    for (int j = 0; j < CowCount; j++)
    //                    {
    //                        if (j == 0)
    //                        {
    //                            sb.Append("<td>" + ds.Tables[3].Rows[j]["MilkType"].ToString() + "</td>");
    //                        }
    //                        else
    //                        {
    //                            sb.Append("<td></td>");
    //                        }
    //                        sb.Append("<td>" + ds.Tables[3].Rows[j]["MilkQuality"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[3].Rows[j]["FatPer"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[3].Rows[j]["SnfPer"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[3].Rows[j]["MilkQuantity"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[3].Rows[j]["FatInKg"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[3].Rows[j]["SnfInKg"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[3].Rows[j]["MilkValue"].ToString() + "</td>");
    //                        sb.Append("<td>" + ds.Tables[3].Rows[j]["CLR"].ToString() + "</td>");

    //                        sb.Append("</tr>");



    //                    }
    //                    sb.Append("<tr>");
    //                    sb.Append("<td colspan='2'>MilkValue:</td>");
    //                    sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["MilkValue"].ToString() + "</td>");
    //                    sb.Append("<td colspan='2'>Commission - :</td>");
    //                    sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["Commission"].ToString() + "</td>");
    //                    sb.Append("</tr>");
    //                    int HeadTypeCount = ds.Tables[4].Rows.Count;
    //                    sb.Append("<tr style='border-top:dashed; padding:20px;'>");

    //                    sb.Append("<td >Earn.Head:</td>");
    //                    sb.Append("<td >Amount:</td>");
    //                    sb.Append("<td colspan='3'>Remark:</td>");
    //                    sb.Append("<td >Deduction.Head:</td>");
    //                    sb.Append("<td >Amount:</td>");
    //                    sb.Append("<td colspan='3'>Remark:</td>");
    //                    sb.Append("<td ></td>");
    //                    sb.Append("</tr>");
    //                    string HeadType = "";
    //                    for (int i = 0; i < HeadTypeCount; i++)
    //                    {
    //                        sb.Append("<tr>");

    //                        if (ds.Tables[4].Rows[i]["HeadType"].ToString() == "ADDITION")
    //                        {
    //                            sb.Append("<td>" + ds.Tables[4].Rows[i]["HeadName"].ToString() + "</td>");
    //                            sb.Append("<td >" + ds.Tables[4].Rows[i]["HeadAmount"].ToString() + "</td>");
    //                            sb.Append("<td colspan='3'></td>");
    //                            sb.Append("<td ></td>");
    //                            sb.Append("<td ></td>");
    //                            sb.Append("<td colspan='3'></td>");
    //                            sb.Append("<td ></td>");

    //                        }
    //                        else if (ds.Tables[4].Rows[i]["HeadType"].ToString() == "DEDUCTION")
    //                        {
    //                            sb.Append("<td ></td>");
    //                            sb.Append("<td></td>");
    //                            sb.Append("<td colspan='3'></td>");
    //                            sb.Append("<td>" + ds.Tables[4].Rows[i]["HeadName"].ToString() + "</td> ");
    //                            sb.Append("<td >" + ds.Tables[4].Rows[i]["HeadAmount"].ToString() + "</td>");
    //                            sb.Append("<td colspan='3'></td>");
    //                            sb.Append("<td ></td>");
    //                        }



    //                        sb.Append("</tr>");
    //                    }
    //                    sb.Append("<tr style='border-top:dashed; padding:20px;'>");


    //                    sb.Append("<td colspan='3' style='text-align:left'>GROSS EARNING:</td>");
    //                    sb.Append("<td>" + ds.Tables[0].Rows[0]["GrossEarning"].ToString() + "</td>");
    //                    sb.Append("<td>Deductions:</td>");
    //                    sb.Append("<td>" + ds.Tables[0].Rows[0]["DeductionAdditionValue"].ToString() + "</td>");
    //                    sb.Append("<td >Producer Payment:</td>");
    //                    sb.Append("<td colspan='3'>" + ds.Tables[0].Rows[0]["ProducerPayment"].ToString() + "</td>");
    //                    sb.Append("<td ></td>");
    //                    sb.Append("</tr>");
    //                    sb.Append("<tr>");


    //                    sb.Append("<td colspan='3' style='text-align:left'>Producer Adjust Amount:</td>");
    //                    sb.Append("<td>" + ds.Tables[0].Rows[0]["ProducerAdjPayment"].ToString() + "</td>");
    //                    sb.Append("<td>Society Adjust Amount</td>");
    //                    sb.Append("<td>" + ds.Tables[0].Rows[0]["SocietyAdjPayment"].ToString() + "</td>");
    //                    sb.Append("<td >Chilling Cost:</td>");
    //                    sb.Append("<td colspan='3'>" + ds.Tables[0].Rows[0]["ChillingCost"].ToString() + "</td>");
    //                    sb.Append("<td ></td>");
    //                    sb.Append("</tr>");

    //                    sb.Append("<tr>");


    //                    sb.Append("<td colspan='3' style='text-align:left'>Head Load Charges:</td>");
    //                    sb.Append("<td>" + ds.Tables[0].Rows[0]["HeadLoadCharges"].ToString() + "</td>");

    //                    sb.Append("<td >Net:</td>");
    //                    sb.Append("<td colspan='3'>" + ds.Tables[0].Rows[0]["NetAmount"].ToString() + "</td>");
    //                    sb.Append("<td ></td>");
    //                    sb.Append("</tr>");
    //                    sb.Append("</table>");
    //                    sb.Append("</div>");

    //                    dvReport.InnerHtml = sb.ToString();
    //                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "window.print();", true);
    //                }
    //                else
    //                {

    //                }
    //            }

    //            else
    //            {

    //            }


    //        }
    //        else
    //        {


    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
    //    }
    //}


    private void ExportGridToExcel()
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "BankWiseSocietPayment" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gv_SocietyMilkDispatchDetail.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {

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
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {

            string filePath = (sender as LinkButton).CommandArgument;
            filePath = filePath.Replace("txt", "005");

            //Response.ContentType = ContentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            //Response.WriteFile(filePath);
            //Response.End();
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.WriteFile(filePath);
            Response.End();
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
            lblmsgshow.Visible = false;
            string Fdate = "";
            string Tdate = "";
			gv_SocietyMilkDispatchDetail.DataSource = string.Empty;
            gv_SocietyMilkDispatchDetail.DataBind();
            if (txtFdt.Text != "")
            {
                Fdate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy-MM-dd");
            }

            if (txtTdt.Text != "")
            {
                Tdate = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy-MM-dd");
            }

            ds = null;

            ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports",
                    new string[] { "flag", "Bank_id", "FromDate", "ToDate", "Office_ID" },
                     new string[] { "7", ddlBank.SelectedValue, Fdate, Tdate,objdb.Office_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        FS_DailyReport.Visible = true;
                        gv_SocietyMilkDispatchDetail.DataSource = ds.Tables[0];
                        gv_SocietyMilkDispatchDetail.DataBind();
                    }
                    else
                    {
                        //lblmsgshow.Visible = true;
                        //lblmsgshow.Text = "No Record Found";
                        FS_DailyReport.Visible = true;
                        gv_SocietyMilkDispatchDetail.DataSource = string.Empty;
                        gv_SocietyMilkDispatchDetail.DataBind();
                    }
                }

                else
                {
                    //lblmsgshow.Visible = true;
                    //lblmsgshow.Text = "No Record Found";
                    FS_DailyReport.Visible = true;
                    gv_SocietyMilkDispatchDetail.DataSource = string.Empty;
                    gv_SocietyMilkDispatchDetail.DataBind();
                }


            }
            else
            {
                // lblmsgshow.Visible = true;
                // lblmsgshow.Text = "No Record Found";
                FS_DailyReport.Visible = true;

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
}