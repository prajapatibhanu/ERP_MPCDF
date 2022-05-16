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

public partial class mis_MilkCollection_SocietywiseMilkCollectionNegativePaymentDetail : System.Web.UI.Page
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
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }


    protected void ddlMilkCollectionUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSociety();
    }

    protected void FillSociety()
    {
        try
        {
            if (ddlMilkCollectionUnit.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                                  new string[] { "flag", "Office_ID", "OfficeType_ID" },
                                  new string[] { "10", ViewState["Office_ID"].ToString(), ddlMilkCollectionUnit.SelectedValue }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSociety.DataTextField = "Office_Name";
                        ddlSociety.DataValueField = "Office_ID";
                        ddlSociety.DataSource = ds.Tables[2];
                        ddlSociety.DataBind();
                        ddlSociety.Items.Insert(0, new ListItem("All", "0"));
                    }
                    else
                    {
                        ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
                else
                {
                    ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                Response.Redirect("SocietywiseMilkCollectionInvoiceDetails.aspx", false);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

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

    protected void lblviewresult_Click(object sender, EventArgs e)
    {
        try
        {
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lblviewresult = (LinkButton)gv_SocietyMilkDispatchDetail.Rows[selRowIndex].FindControl("lblviewresult");
            ds = objdb.ByProcedure("USP_SocietywiseMilkCollectionInvoice",
                    new string[] { "flag", "MilkCollectionInvoice_ID" },
                     new string[] { "5", lblviewresult.CommandArgument }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<div class='content' style='border:1px solid black; width:100%'>");
                        sb.Append("<table class='table1'  style='width:100%; margin:10px;'>");
                        sb.Append("<tr>");
                        sb.Append("<td>Society Name And Code: " + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "  /  " + ds.Tables[0].Rows[0]["Office_Code"].ToString() + "");
                        sb.Append("</td>");
                        sb.Append("<td style='text-align:right'>Route: " + ds.Tables[0].Rows[0]["RouteNo"].ToString() + "");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr >");
                        sb.Append("<td >Bank Name / IFSC / Account  : " + ds.Tables[0].Rows[0]["BankName"].ToString() + " /" + ds.Tables[0].Rows[0]["IFSC"].ToString() + " /" + ds.Tables[0].Rows[0]["BankAccountNo"].ToString());
                        sb.Append("</td>");
                        sb.Append("<td style='text-align:right'>Billing Period  : " + ds.Tables[0].Rows[0]["FromDate"].ToString() + " To " + ds.Tables[0].Rows[0]["ToDate"].ToString() + "");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");

                        sb.Append("<table  class='table1'  style='width:100%; margin-top:20px;'>");
                        sb.Append("<tr style='border-bottom:dashed; border-top:dashed'>");
                        sb.Append("<td>Date</td>");
                        sb.Append("<td>M / E</td>");
                        sb.Append("<td>Buf / Cow</td>");
                        sb.Append("<td>Fat</td>");
                        sb.Append("<td>S.N.F</td>");
                        sb.Append("<td>Kg.Wt</td>");
                        sb.Append("<td>Kg.Fat</td>");
                        sb.Append("<td>Kg.Snf</td>");
                        sb.Append("<td>Value</td>");
                        sb.Append("<td>Clr</td>");
                        sb.Append("<td>Quality</td>");
                        sb.Append("</tr>");
                        int Count = ds.Tables[1].Rows.Count;

                        for (int i = 0; i < Count; i++)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["DispDate"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["Shift"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkType"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["FatPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["SnfPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["SnfInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkValue"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["CLR"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkQuality"].ToString() + "</td>");
                            sb.Append("</tr>");
                        }
                        decimal FatPer = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("FatPer"));
                        decimal SnfPer = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SnfPer"));
                        decimal MilkQuantity = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("MilkQuantity"));
                        decimal FatInKg = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                        decimal SnfInKg = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                        decimal MilkValue = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("MilkValue"));

                        sb.Append("<tr style='border-top:dashed; padding-top:20px;'>");
                        sb.Append("<td>Total</td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td>" + FatPer.ToString() + "</td>");
                        sb.Append("<td>" + SnfPer.ToString() + "</td>");
                        sb.Append("<td>" + MilkQuantity.ToString() + "</td>");
                        sb.Append("<td>" + FatInKg.ToString() + "</td>");
                        sb.Append("<td>" + SnfInKg.ToString() + "</td>");
                        sb.Append("<td>" + FatPer.ToString() + "</td>");
                        sb.Append("<td>" + MilkValue.ToString() + "</td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td colspan='11'>Particulars:</td>");
                        sb.Append("</tr>");

                        int BufCount = ds.Tables[2].Rows.Count;

                        for (int j = 0; j < BufCount; j++)
                        {
                            if (j == 0)
                            {
                                sb.Append("<td>" + ds.Tables[2].Rows[j]["MilkType"].ToString() + "</td>");
                            }
                            else
                            {
                                sb.Append("<td></td>");
                            }
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["MilkQuality"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["FatPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["SnfPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["SnfInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["MilkValue"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["CLR"].ToString() + "</td>");

                            sb.Append("</tr>");

                        }
                        int CowCount = ds.Tables[3].Rows.Count;

                        for (int j = 0; j < CowCount; j++)
                        {
                            if (j == 0)
                            {
                                sb.Append("<td>" + ds.Tables[3].Rows[j]["MilkType"].ToString() + "</td>");
                            }
                            else
                            {
                                sb.Append("<td></td>");
                            }
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["MilkQuality"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["FatPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["SnfPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["SnfInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["MilkValue"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["CLR"].ToString() + "</td>");

                            sb.Append("</tr>");



                        }
                        sb.Append("<tr>");
                        sb.Append("<td colspan='2'>MilkValue:</td>");
                        sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["MilkValue"].ToString() + "</td>");
                        sb.Append("<td colspan='2'>Commission - :</td>");
                        sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["Commission"].ToString() + "</td>");
                        sb.Append("</tr>");
                        int HeadTypeCount = ds.Tables[4].Rows.Count;
                        sb.Append("<tr style='border-top:dashed; padding:20px;'>");

                        sb.Append("<td >Earn.Head:</td>");
                        sb.Append("<td >Amount:</td>");
                        sb.Append("<td colspan='3'>Remark:</td>");
                        sb.Append("<td >Deduction.Head:</td>");
                        sb.Append("<td >Amount:</td>");
                        sb.Append("<td colspan='3'>Remark:</td>");
                        sb.Append("<td ></td>");
                        sb.Append("</tr>");
                        string HeadType = "";
                        for (int i = 0; i < HeadTypeCount; i++)
                        {
                            sb.Append("<tr>");

                            if (ds.Tables[4].Rows[i]["HeadType"].ToString() == "ADDITION")
                            {
                                sb.Append("<td>" + ds.Tables[4].Rows[i]["HeadName"].ToString() + "</td>");
                                sb.Append("<td >" + ds.Tables[4].Rows[i]["HeadAmount"].ToString() + "</td>");
                                sb.Append("<td colspan='4'>" + ds.Tables[4].Rows[i]["HeadRemark"].ToString() + "</td>");
                                sb.Append("<td ></td>");
                                sb.Append("<td ></td>");
                                sb.Append("<td colspan='3'></td>");
                                sb.Append("<td ></td>");

                            }
                            else if (ds.Tables[4].Rows[i]["HeadType"].ToString() == "DEDUCTION")
                            {
                                sb.Append("<td ></td>");
                                sb.Append("<td></td>");
                                sb.Append("<td colspan='3'></td>");
                                sb.Append("<td>" + ds.Tables[4].Rows[i]["HeadName"].ToString() + "</td> ");
                                sb.Append("<td >" + ds.Tables[4].Rows[i]["HeadAmount"].ToString() + "</td>");
                                sb.Append("<td colspan='4'>" + ds.Tables[4].Rows[i]["HeadRemark"].ToString() + "</td>");
                                sb.Append("<td ></td>");
                            }



                            sb.Append("</tr>");
                        }
                        sb.Append("<tr style='border-top:dashed; padding:20px;'>");


                        sb.Append("<td colspan='3' style='text-align:left'>GROSS EARNING:</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[0]["GrossEarning"].ToString() + "</td>");
                        sb.Append("<td>Deductions:</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[0]["DeductionAdditionValue"].ToString() + "</td>");
                        sb.Append("<td >Producer Payment:</td>");
                        sb.Append("<td colspan='3'>" + ds.Tables[0].Rows[0]["ProducerPayment"].ToString() + "</td>");
                        sb.Append("<td ></td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");


                        sb.Append("<td colspan='3' style='text-align:left'>Producer Adjust Amount:</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[0]["ProducerAdjPayment"].ToString() + "</td>");
                        sb.Append("<td>Society Adjust Amount</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[0]["SocietyAdjPayment"].ToString() + "</td>");
                        sb.Append("<td >Chilling Cost:</td>");
                        sb.Append("<td colspan='3'>" + ds.Tables[0].Rows[0]["ChillingCost"].ToString() + "</td>");
                        sb.Append("<td ></td>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");


                        sb.Append("<td colspan='3' style='text-align:left'>Head Load Charges:</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[0]["HeadLoadCharges"].ToString() + "</td>");

                        sb.Append("<td >Net:</td>");
                        sb.Append("<td colspan='3'>" + ds.Tables[0].Rows[0]["NetAmount"].ToString() + "</td>");
                        sb.Append("<td ></td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        sb.Append("</div>");

                        dvReport.InnerHtml = sb.ToString();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "window.print();", true);
                    }
                    else
                    {

                    }
                }

                else
                {

                }


            }
            else
            {


            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }


    private void ExportGridToExcel()
    {
        try
        {

            string Fdate = "";
            string Tdate = "";

            if (txtFdt.Text != "")
            {
                Fdate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy-MM-dd");
            }

            if (txtTdt.Text != "")
            {
                Tdate = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy-MM-dd");
            }

            ds = null;

            ds = objdb.ByProcedure("USP_SocietywiseMilkCollectionInvoice",
                    new string[] { "flag", "BillingCycleFromDate", "BillingCycleToDate", "GenerateTo_Office_ID" },
                     new string[] { "11", Fdate, Tdate, ddlSociety.SelectedValue }, "dataset");
            gv_SocietyMilkDispatchDetail.Columns[0].Visible = false;
            gv_SocietyMilkDispatchDetail.Columns[2].Visible = true;
            gv_SocietyMilkDispatchDetail.Columns[3].Visible = true;
            gv_SocietyMilkDispatchDetail.Columns[14].Visible = false;
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "SocietyPaymentProcess" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gv_SocietyMilkDispatchDetail.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());

            Response.End();
            gv_SocietyMilkDispatchDetail.Columns[0].Visible = true;
            gv_SocietyMilkDispatchDetail.Columns[2].Visible = false;
            gv_SocietyMilkDispatchDetail.Columns[3].Visible = false;
            gv_SocietyMilkDispatchDetail.Columns[14].Visible = true;
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

    protected void WriteNew(DataTable dt, string ClientCode, string strFileCount)
    {
        string txt = string.Empty;

        foreach (DataRow row in dt.Rows)
        {
            int i = 1;
            int ColCount = dt.Columns.Count;
            foreach (DataColumn column in dt.Columns)
            {

                txt += row[column.ColumnName].ToString();
                if (i < ColCount)
                {
                    txt += ",";
                }
                i++;
            }

            txt += "\r\n";
        }

        string GetFPath = ClientCode + DateTime.Now.ToString("ddMM") + "." + strFileCount + ".005";
        string fileDirPath = @"C:\inetpub\wwwroot\MPCDFERP\H2H\" + ClientCode + DateTime.Now.ToString("ddMM") + "." + strFileCount + ".005"; ;
        //string fileDirPath = @"F:\MPCDFERP\H2H\" + ClientCode + DateTime.Now.ToString("ddMM") + "." + strFileCount + ".005"; 

        FileStream fs1 = new FileStream(fileDirPath, FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter writer = new StreamWriter(fs1);
        writer.Write(txt);

        ds = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails",
                         new string[] { "flag", "H2HClientCode", "FilePath" },
                         new string[] { "10", ClientCode, "~/H2H/" + GetFPath }, "dataset");
        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "File Successfully Saved - " + fileDirPath);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            string H2HPaymentId = ds.Tables[0].Rows[0]["H2HPaymentId"].ToString();
            foreach (GridViewRow rows in gv_SocietyMilkDispatchDetail.Rows)
            {
                CheckBox chk = (CheckBox)rows.FindControl("CheckBox1");
                Label lblMilkCollectionInvoice_ID = (Label)rows.FindControl("lblMilkCollectionInvoice_ID");
                if (chk.Checked)
                {
                    string MilkCollectionInvoice_ID = lblMilkCollectionInvoice_ID.Text;
                    objdb.ByProcedure("USP_Trn_ProducerPaymentDetails",
                         new string[] { "flag", "H2HPaymentId", "MilkCollectionInvoice_ID" },
                         new string[] { "11", H2HPaymentId, MilkCollectionInvoice_ID }, "dataset");
                }
            }
        }

        //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "File Successfully Saved - <a href='" + "E:/CDF_09082020/H2H/" + GetFPath + "' Target='_blank'>View File</a>");
        writer.Close();

    }

    protected void lnkbtnUpdatePayHis_Click(object sender, EventArgs e)
    {
        txtNP_ProducerDateTime.Text = "";
        txtNP_ProducerPayment.Text = "";
        txtNP_ProducerUTRNo.Text = "";
        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
        LinkButton lblUpdatePayHis = (LinkButton)gv_SocietyMilkDispatchDetail.Rows[selRowIndex].FindControl("lblUpdatePayHis");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModal();", true);
        ViewState["MilkCollectionInvoice_ID"] = lblUpdatePayHis.CommandArgument;

    }
    protected void btnYes_Click(object sender, EventArgs e)
    {

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("USP_SocietywiseMilkCollectionInvoice", new string[] { "flag", "MilkCollectionInvoice_ID", "NP_ProducerPayment", "NP_ProducerDateTime", "NP_ProducerUTRNo" }, new string[] { "7", ViewState["MilkCollectionInvoice_ID"].ToString(), txtNP_ProducerPayment.Text, Convert.ToDateTime(txtNP_ProducerDateTime.Text, cult).ToString("yyyy/MM/dd"), txtNP_ProducerUTRNo.Text }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou!", Success.ToString());
                    FillGrid();
                }
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

            if (txtFdt.Text != "")
            {
                Fdate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy-MM-dd");
            }

            if (txtTdt.Text != "")
            {
                Tdate = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy-MM-dd");
            }

            ds = null;

            ds = objdb.ByProcedure("USP_SocietywiseMilkCollectionInvoice",
                    new string[] { "flag", "BillingCycleFromDate", "BillingCycleToDate", "GenerateTo_Office_ID", "Office_ID" },
                     new string[] { "10", Fdate, Tdate, ddlSociety.SelectedValue,objdb.Office_ID() }, "dataset");

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
                        lblmsgshow.Visible = true;
                        lblmsgshow.Text = "No Record Found";
                        FS_DailyReport.Visible = false;
                        gv_SocietyMilkDispatchDetail.DataSource = string.Empty;
                        gv_SocietyMilkDispatchDetail.DataBind();
                    }
                }

                else
                {
                    lblmsgshow.Visible = true;
                    lblmsgshow.Text = "No Record Found";
                    FS_DailyReport.Visible = false;
                    gv_SocietyMilkDispatchDetail.DataSource = string.Empty;
                    gv_SocietyMilkDispatchDetail.DataBind();
                }


            }
            else
            {
                lblmsgshow.Visible = true;
                lblmsgshow.Text = "No Record Found";
                FS_DailyReport.Visible = false;

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
}