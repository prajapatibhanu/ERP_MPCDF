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

public partial class mis_MilkCollection_CCwiseMilkCollectionInvoiceDetails_Adiwasi : System.Web.UI.Page
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
                GetCCDetails();
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
                        sb.Append("<table style='width:100%; margin:5px;'>");
                        sb.Append("<tr>");
                        sb.Append("<td style='text-align:center' colspan='2'>" + Session["Office_Name"].ToString() + "");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td>Society Name And Code: " + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "  /  " + ds.Tables[0].Rows[0]["Office_Code"].ToString() + "");
                        sb.Append("</td>");
                        sb.Append("<td style='text-align:right'>CC: " + ddlccbmcdetail.SelectedItem.Text + "");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr >");
                        sb.Append("<td >Bank Name / IFSC / Account  : " + ds.Tables[0].Rows[0]["BankName"].ToString() + " /" + ds.Tables[0].Rows[0]["IFSC"].ToString() + " /" + ds.Tables[0].Rows[0]["BankAccountNo"].ToString());
                        sb.Append("</td>");
                        sb.Append("<td style='text-align:right'>Billing Period  : " + ds.Tables[0].Rows[0]["FromDate"].ToString() + " To " + ds.Tables[0].Rows[0]["ToDate"].ToString() + "");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");

                        sb.Append("<table style='width:100%; margin-top:20px;'>");
                        sb.Append("<tr style='border-bottom:dashed; border-top:dashed'>");
                        sb.Append("<td>Date</td>");
                        sb.Append("<td>M / E</td>");
                        sb.Append("<td>Buf / Cow</td>");
                        sb.Append("<td>Kg.Wt</td>");
                        sb.Append("<td>Fat</td>");
                        sb.Append("<td>Clr</td>");
                        sb.Append("<td>S.N.F</td>");
                        sb.Append("<td>Kg.Fat</td>");
                        sb.Append("<td>Kg.Snf</td>");
                        sb.Append("<td>Value</td>");

                        sb.Append("<td>Quality</td>");
                        sb.Append("<td>Rate Per Ltr</td>");
                        sb.Append("</tr>");
                        int Count = ds.Tables[1].Rows.Count;

                        for (int i = 0; i < Count; i++)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["DispDate"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["Shift"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkType"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["FatPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["CLR"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["SnfPer"].ToString() + "</td>");

                            sb.Append("<td>" + ds.Tables[1].Rows[i]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["SnfInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkValue"].ToString() + "</td>");

                            sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkQuality"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["RatePerLtr"].ToString() + "</td>");
                            sb.Append("</tr>");
                        }
                        decimal FatPer = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("FatPer"));
                        decimal SnfPer = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SnfPer"));
                        decimal MilkQuantity = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("MilkQuantity"));
                        decimal FatInKg = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                        decimal SnfInKg = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                        decimal MilkValue = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("MilkValue"));
                        decimal RatePerLtr = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("RatePerLtr"));
                        sb.Append("<tr style='border-top:dashed; padding-top:20px;'>");
                        sb.Append("<td>Total</td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td>" + MilkQuantity.ToString() + "</td>");
                        //sb.Append("<td>" + FatPer.ToString() + "</td>");
                        //sb.Append("<td>" + SnfPer.ToString() + "</td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td>" + FatInKg.ToString() + "</td>");
                        sb.Append("<td>" + SnfInKg.ToString() + "</td>");
                        //sb.Append("<td>" + FatPer.ToString() + "</td>");
                        sb.Append("<td>" + MilkValue.ToString() + "</td>");


                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        //sb.Append("<td>" + RatePerLtr.ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td>Particulars:</td>");
                        sb.Append("<td colspan='8'></td>");
                        sb.Append("<td>Rate:</td>");
                        sb.Append("</tr>");
                        decimal RateBuff = 0;
                        decimal RateCow = 0;
                        if (ds.Tables[6].Rows.Count > 0)
                        {
                            RateBuff = decimal.Parse(ds.Tables[6].Rows[0]["BuffRate"].ToString());
                            RateCow = decimal.Parse(ds.Tables[6].Rows[0]["CowRate"].ToString());
                        }
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
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["FatPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["CLR"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["SnfPer"].ToString() + "</td>");                     
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["SnfInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["MilkValue"].ToString() + "</td>");
                            if (ds.Tables[2].Rows[j]["MilkQuality"].ToString() == "Good")
                            {
                                sb.Append("<td>" + RateBuff + "</td>");

                            }
                            else if (ds.Tables[2].Rows[j]["MilkQuality"].ToString() == "Sour")
                            {
                                sb.Append("<td>" + ((RateBuff) * 50 / 100) + "</td>");

                            }
                            else
                            {
                                sb.Append("<td>" + ((RateBuff) * 30 / 100) + "</td>");
                            }
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
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["FatPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["CLR"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["SnfPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["SnfInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["MilkValue"].ToString() + "</td>");
                            if (ds.Tables[3].Rows[j]["MilkQuality"].ToString() == "Good")
                            {
                                sb.Append("<td>" + RateCow + "</td>");

                            }
                            else if (ds.Tables[3].Rows[j]["MilkQuality"].ToString() == "Sour")
                            {
                                sb.Append("<td>" + ((RateCow) * 50 / 100) + "</td>");

                            }
                            else
                            {
                                sb.Append("<td>" + ((RateCow) * 30 / 100) + "</td>");
                            }

                            sb.Append("</tr>");



                        }
                        if (objdb.Office_ID() == "4")
                        {
                            sb.Append("<tr>");
                             //sb.Append("<td colspan='8'>" + ds.Tables[5].Rows[0]["CommissionNarration"].ToString() + "" + ds.Tables[0].Rows[0]["Commission"].ToString() + "</td>");
                            sb.Append("<td colspan='8'>" + ds.Tables[5].Rows[0]["CommissionNarration"].ToString() + "</td>");
                            sb.Append("<td colspan='2'>Commission - :</td>");
                            sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["Commission"].ToString() + "</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td colspan='2'>MilkValue:</td>");
                            sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["MilkValue"].ToString() + "</td>");

                            sb.Append("</tr>");
                        }
                        else
                        {
                            sb.Append("<tr>");
                            sb.Append("<td colspan='2'>MilkValue:</td>");
                            sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["MilkValue"].ToString() + "</td>");
                            sb.Append("<td colspan='2'>Commission - :</td>");
                            sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["Commission"].ToString() + "</td>");
                            sb.Append("</tr>");
                        }


                        sb.Append("</table>");
                        sb.Append("<table>");
                        int HeadTypeCount = ds.Tables[4].Rows.Count;
                        sb.Append("<tr style='border-top:dashed; padding:20px;'>");
                        sb.Append("<td>");
                        sb.Append("<table>");
                        sb.Append("<tr>");
                        sb.Append("<td  >Earn.Head:</td>");
                        sb.Append("<td >Amount:</td>");
                        sb.Append("<td >Remark:</td>");
                        //sb.Append("<td >Deduction.Head:</td>");
                        //sb.Append("<td >Amount:</td>");
                        //sb.Append("<td colspan='4'>Remark:</td>");
                        //sb.Append("<td ></td>");
                        sb.Append("</tr>");
                        string HeadType = "";

                        for (int i = 0; i < HeadTypeCount; i++)
                        {
                            sb.Append("<tr>");

                            if (ds.Tables[4].Rows[i]["HeadType"].ToString() == "ADDITION")
                            {

                                sb.Append("<td style='width:30%'>" + ds.Tables[4].Rows[i]["HeadName"].ToString() + "</td>");
                                sb.Append("<td style='width:10%'>" + ds.Tables[4].Rows[i]["HeadAmount"].ToString() + "</td>");
                                sb.Append("<td style='width:10%'>" + ds.Tables[4].Rows[i]["HeadRemark"].ToString() + "</td>");
                                //sb.Append("<td ></td>");
                                //sb.Append("<td ></td>");
                                //sb.Append("<td colspan='4'></td>");
                                //sb.Append("<td ></td>");

                            }
                            //else if (ds.Tables[4].Rows[i]["HeadType"].ToString() == "DEDUCTION")
                            //{

                            //    sb.Append("<td ></td>");
                            //    sb.Append("<td></td>");
                            //    sb.Append("<td colspan='4'></td>");
                            //    sb.Append("<td>" + ds.Tables[4].Rows[i]["HeadName"].ToString() + "</td> ");
                            //    sb.Append("<td >" + ds.Tables[4].Rows[i]["HeadAmount"].ToString() + "</td>");
                            //    sb.Append("<td colspan='4'>" + ds.Tables[4].Rows[i]["HeadRemark"].ToString() + "</td>");
                            //    sb.Append("<td ></td>");
                            //}



                            sb.Append("</tr>");
                        }
                        sb.Append("</table>");
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append("<table>");
                        sb.Append("<tr>");
                        sb.Append("<td >Deduction.Head:</td>");
                        sb.Append("<td >Amount:</td>");
                        sb.Append("<td >Remark:</td>");
                        //sb.Append("<td >Deduction.Head:</td>");
                        //sb.Append("<td >Amount:</td>");
                        //sb.Append("<td colspan='4'>Remark:</td>");
                        //sb.Append("<td ></td>");
                        sb.Append("</tr>");


                        for (int i = 0; i < HeadTypeCount; i++)
                        {
                            sb.Append("<tr>");

                            if (ds.Tables[4].Rows[i]["HeadType"].ToString() == "DEDUCTION")
                            {

                                sb.Append("<td style='width:30%'>" + ds.Tables[4].Rows[i]["HeadName"].ToString() + "</td>");
                                sb.Append("<td style='width:10%'>" + ds.Tables[4].Rows[i]["HeadAmount"].ToString() + "</td>");
                                sb.Append("<td style='width:10%'>" + ds.Tables[4].Rows[i]["HeadRemark"].ToString() + "</td>");
                                //sb.Append("<td ></td>");
                                //sb.Append("<td ></td>");
                                //sb.Append("<td colspan='4'></td>");
                                //sb.Append("<td ></td>");

                            }
                            //else if (ds.Tables[4].Rows[i]["HeadType"].ToString() == "DEDUCTION")
                            //{

                            //    sb.Append("<td ></td>");
                            //    sb.Append("<td></td>");
                            //    sb.Append("<td colspan='4'></td>");
                            //    sb.Append("<td>" + ds.Tables[4].Rows[i]["HeadName"].ToString() + "</td> ");
                            //    sb.Append("<td >" + ds.Tables[4].Rows[i]["HeadAmount"].ToString() + "</td>");
                            //    sb.Append("<td colspan='4'>" + ds.Tables[4].Rows[i]["HeadRemark"].ToString() + "</td>");
                            //    sb.Append("<td ></td>");
                            //}



                            sb.Append("</tr>");
                        }
                        sb.Append("</table>");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        sb.Append("<table>");
                        sb.Append("<tr style='border-top:dashed; padding:20px;'>");


                        sb.Append("<td >GROSS EARNING:&nbsp;&nbsp;&nbsp;&nbsp" + ds.Tables[0].Rows[0]["GrossEarning"].ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["GrossEarning"].ToString() + "</td>");
                        sb.Append("<td >Deductions:&nbsp;&nbsp;&nbsp;&nbsp;" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[0]["DeductionAdditionValue"].ToString())).ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["DeductionAdditionValue"].ToString() + "</td>");
                        //sb.Append("<td style='width:20%'>Producer Payment:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ProducerPayment"].ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["ProducerPayment"].ToString() + "</td>");
                        //sb.Append("<td style='width:40%'>Producer Adj Amount:&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ProducerAdjPayment"].ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["ProducerAdjPayment"].ToString() + "</td>");
                       // sb.Append("</tr>");

                        //sb.Append("<tr>");

                       // sb.Append("<td style='width:40%'>Society Adjust Amount:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ProducerAdjPayment"].ToString() + "</td>");
                        // sb.Append("<td>" + ds.Tables[0].Rows[0]["SocietyAdjPayment"].ToString() + "</td>");
                        sb.Append("<td >Chilling Cost:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ChillingCost"].ToString() + "</td>");
                        //  sb.Append("<td>" + ds.Tables[0].Rows[0]["ChillingCost"].ToString() + "</td>");



                        sb.Append("<td >Head Load Charges:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["HeadLoadCharges"].ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["HeadLoadCharges"].ToString() + "</td>");

                        sb.Append("<td >Net:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["NetAmount"].ToString() + "</td>");
                        //sb.Append("<td >" + ds.Tables[0].Rows[0]["NetAmount"].ToString() + "</td>");

                        sb.Append("</tr>");
                        sb.Append("</table>");
						
						 if (ds.Tables[8].Rows.Count > 0)
                        {
                            sb.Append("<table>");
                            sb.Append("<tr>");

                            sb.Append("<td>Remark:   " + ds.Tables[8].Rows[0]["Remark"].ToString() + "</td>");
                            sb.Append("</tr>");
                            sb.Append("</table>");
                        }
                        if (objdb.Office_ID() == "2" || objdb.Office_ID() == "4")
                        {
                            if (ds.Tables[7].Rows.Count > 0)
                            {
                                sb.Append("<table>");
                                sb.Append("<tr>");
                                sb.Append("<td>* CURRENT LEDGER POSITION , TODAY *   </td>");
                                sb.Append("<td></td>");
                                sb.Append("</tr>");
                                sb.Append("<tr>");
                                sb.Append("<td>*************************************************</td>");
                                sb.Append("<td>* A M O U N T *</td>");
                                sb.Append("</tr>");
                                int LedgerCount = ds.Tables[7].Rows.Count;
                                for (int i = 0; i < LedgerCount; i++)
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td>" + ds.Tables[7].Rows[i]["ItemBillingHead_Name"].ToString() + "</td>");
                                    sb.Append("<td>" + ds.Tables[7].Rows[i]["BalanceAmount"].ToString() + "</td>");
                                    sb.Append("</tr>");
                                }
								sb.Append("<tr>");
								sb.Append("<td>Total</td>");
								sb.Append("<td>" + ds.Tables[7].AsEnumerable().Sum(row => row.Field<decimal>("BalanceAmount")).ToString() + "</td>");
								sb.Append("</tr>");

                                sb.Append("</table>");
                            }
                        }
                        
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
                     new string[] { "17", Fdate, Tdate, ddlccbmcdetail.SelectedValue }, "dataset");


            //DataTable dt = new DataTable();
            //dt = ds.Tables[0];

            //using (XLWorkbook wb = new XLWorkbook())
            //{
            //    wb.Worksheets.Add(dt);

            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.Charset = "";
            //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //    Response.AddHeader("content-disposition", "attachment;filename=" + "SocietyPaymentProcess" + DateTime.Now + ".xlsx");
            //    using (MemoryStream MyMemoryStream = new MemoryStream())
            //    {
            //        wb.SaveAs(MyMemoryStream);
            //        MyMemoryStream.WriteTo(Response.OutputStream);
            //        Response.Flush();
            //        Response.End();
            //    }
            //}
              gv_SocietyMilkDispatchDetail.Columns[0].Visible = false;
            gv_SocietyMilkDispatchDetail.Columns[4].Visible = true;
            gv_SocietyMilkDispatchDetail.Columns[5].Visible = true;
            gv_SocietyMilkDispatchDetail.Columns[15].Visible = false;
            gv_SocietyMilkDispatchDetail.Columns[16].Visible = false;
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
              gv_SocietyMilkDispatchDetail.Columns[0].Visible = false;
            gv_SocietyMilkDispatchDetail.Columns[4].Visible = true;
            gv_SocietyMilkDispatchDetail.Columns[5].Visible = true;
            gv_SocietyMilkDispatchDetail.Columns[15].Visible = false;
            gv_SocietyMilkDispatchDetail.Columns[16].Visible = false;
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
		string fileDirPath = @"C:\inetpub\wwwroot\MPCDF_ERP\H2H\" + ClientCode + DateTime.Now.ToString("ddMM") + "." + strFileCount + ".005"; ;
        //string fileDirPath = @"C:\inetpub\wwwroot\MPCDFERP\H2H\" + ClientCode + DateTime.Now.ToString("ddMM") + "." + strFileCount + ".005"; ;
        //string fileDirPath = @"F:\MPCDFERP\H2H\" + ClientCode + DateTime.Now.ToString("ddMM") + "." + strFileCount + ".005"; 

        FileStream fs1 = new FileStream(fileDirPath, FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter writer = new StreamWriter(fs1);
        writer.Write(txt);
        
        ds = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails",
                         new string[] { "flag", "H2HClientCode", "FilePath", "Office_ID" },
                         new string[] { "10", ClientCode, "~/H2H/" + GetFPath,objdb.Office_ID()}, "dataset");
        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "File Successfully Saved - " + fileDirPath);
        if(ds != null && ds.Tables[0].Rows.Count > 0)
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
                         new string[] { "11", H2HPaymentId, MilkCollectionInvoice_ID}, "dataset");
                }
            }
        }

        //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "File Successfully Saved - <a href='" + "E:/CDF_09082020/H2H/" + GetFPath + "' Target='_blank'>View File</a>");
        writer.Close();

    }
     protected void WriteNewforExcel(DataTable dt, string ClientCode, string strFileCount)
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
                    txt += "\t";
                }
                i++;
            }

            txt += "\r\n";
        }



       string GetFPath = "";
       string fileDirPath = "";
       if (objdb.Office_ID() == "4")
       {
           GetFPath = ClientCode + DateTime.Now.ToString("ddMM") + strFileCount + ".xls";
       }
       else
       {
           GetFPath = ClientCode + DateTime.Now.ToString("ddMM") + "-" + strFileCount + ".xls";
       }
       if (objdb.Office_ID() == "4")
       {
           fileDirPath = @"C:\inetpub\wwwroot\MPCDF_ERP\H2H\" + ClientCode + DateTime.Now.ToString("ddMM") + strFileCount + ".xls";
       }
       else
       {
           fileDirPath = @"C:\inetpub\wwwroot\MPCDF_ERP\H2H\" + ClientCode + DateTime.Now.ToString("ddMM") + "-" + strFileCount + ".xls";
       }


       //string fileDirPath = @"C:\inetpub\wwwroot\MPCDFERP\H2H\" + ClientCode + DateTime.Now.ToString("ddMM") + "." + strFileCount + ".005"; ;
       //string fileDirPath = @"F:\MPCDFERP\H2H\" + ClientCode + DateTime.Now.ToString("ddMM") + "." + strFileCount + ".005"; 

        // FileStream fs1 = new FileStream(fileDirPath, FileMode.OpenOrCreate, FileAccess.Write);
        // StreamWriter writer = new StreamWriter(fs1);
        // using (StringWriter sw = new StringWriter())
        // {
            // HtmlTextWriter hw = new HtmlTextWriter(sw);

            // //To Export all pages
            // if (objdb.Office_ID() == "4")
            // {
                // gvIDSh2h.Visible = true;
                // gvIDSh2h.DataSource = dt;
                // gvIDSh2h.DataBind();
            // }
            // else
            // {
                // gvh2h.Visible = true;
                // gvh2h.DataSource = dt;
                // gvh2h.DataBind();
            // }


            // if (objdb.Office_ID() == "4")
            // {
                // foreach (GridViewRow row in gvIDSh2h.Rows)
                // {

                    // foreach (TableCell cell in row.Cells)
                    // {

                        // row.Cells[0].CssClass = "textmode";
                        // row.Cells[1].CssClass = "nummode";
                        // row.Cells[2].CssClass = "textmode";
                        // row.Cells[3].CssClass = "textmode";
                        // row.Cells[4].CssClass = "textmode";
                        // row.Cells[5].CssClass = "textmode";
                        // row.Cells[6].CssClass = "textmode";
                        // row.Cells[7].CssClass = "textmode";
                        // row.Cells[8].CssClass = "textmode";
                        // row.Cells[9].CssClass = "textmode";
                        // row.Cells[10].CssClass = "textmode";
                        // row.Cells[11].CssClass = "textmode";
                        // row.Cells[12].CssClass = "textmode";
                    // }
                // }
            // }
            // else
            // {
                // foreach (GridViewRow row in gvh2h.Rows)
                // {

                    // foreach (TableCell cell in row.Cells)
                    // {

                        // cell.CssClass = "textmode";
                    // }
                // }
            // }
            // if (objdb.Office_ID() == "4")
            // {
                // gvIDSh2h.RenderControl(hw);
            // }

            // else
            // {
                // gvh2h.RenderControl(hw);
            // }


            // //style to format numbers to string


            // string style = @"<style> .textmode {mso-number-format:\@; } .nummode {mso-number-format:'0.00';  } </style>";
            // writer.Write(style);
            // writer.Write(sw.ToString());
            // gvh2h.Visible = false;
            // gvIDSh2h.Visible = false;
        // }

        //writer.Write(txt);
		
		using (XLWorkbook wb = new XLWorkbook())
        {

            var ws = wb.Worksheets.Add(GetFPath);
			if(objdb.Office_ID() == "7" || objdb.Office_ID() == "2")
			{
				ws.Cell(1, 1).InsertTable(dt);
			}
			else
			{
				ws.FirstRow().FirstCell().InsertData(dt.Rows);
			}
            
           
            //ws.Cell(4, 1).InsertTable(dt);
           
            int rowcount = dt.Rows.Count;
          
            for (int i = 0; i <= rowcount; i++)
            {
                if (i < rowcount)
                {
                    
                  ws.Cell((i + 1), 1).Style.NumberFormat.Format = "@";
				  
                  ws.Cell((i + 1), 2).Style.NumberFormat.Format = "0.00";
                  ws.Cell((i + 1), 3).Style.NumberFormat.Format = "@";
                  ws.Cell((i + 1), 4).Style.NumberFormat.Format = "@";
                  ws.Cell((i + 1), 5).Style.NumberFormat.Format = "@";
                  ws.Cell((i + 1), 6).Style.NumberFormat.Format = "@";
                  ws.Cell((i + 1), 7).Style.NumberFormat.Format = "@";
                  ws.Cell((i + 1), 8).Style.NumberFormat.Format = "@";
                  ws.Cell((i + 1), 9).Style.NumberFormat.Format = "@";
                  ws.Cell((i + 1), 10).Style.NumberFormat.Format = "@";
                  ws.Cell((i + 1), 11).Style.NumberFormat.Format = "@";
                    
                    // ws.Cell(i + 5, 6).Style.Fill.BackgroundColor = XLColor.Yellow;
                    //ws.Cell(i + 5, 7).Style.Fill.BackgroundColor = XLColor.Yellow;
                    //ws.Cell(i + 5, 8).Style.Fill.BackgroundColor = XLColor.Yellow;
                    //ws.Cell(i + 5, 9).Style.Fill.BackgroundColor = XLColor.Yellow;
                   
                }

            }
            //ws.Columns().AdjustToContents();
            if(objdb.Office_ID() == "4")
            {
                ws.Columns("9").Width = 7;
            }
            if (objdb.Office_ID() == "7")
            {
                ws.Columns("7").Width = 7;
            }
             if (objdb.Office_ID() == "5")
            {
                ws.Columns("4").Width = 7;
            }


            
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(fileDirPath);
                MyMemoryStream.WriteTo(Response.OutputStream);
                //Response.Flush();
                //Response.End();
            }


        }

       ds = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails",
                        new string[] { "flag", "H2HClientCode", "FilePath", "Office_ID" },
                        new string[] { "10", ClientCode, "~/H2H/" + GetFPath, objdb.Office_ID() }, "dataset");
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
       //writer.Close();

   }
    //protected void WriteNewforExcel(DataTable dt, string ClientCode, string strFileCount)
    //{
     
     // string GetFPath = "";
     // string fileDirPath = "";
     // if (objdb.Office_ID() == "4")
     // {
         // GetFPath = ClientCode + DateTime.Now.ToString("ddMM") + strFileCount + ".xlsx";
     // }
     // else
     // {
         // GetFPath = ClientCode + DateTime.Now.ToString("ddMM") + "-" + strFileCount + ".xlsx";
     // }
     // if (objdb.Office_ID() == "4")
     // {
         // fileDirPath = @"C:\inetpub\wwwroot\MPCDF_ERP\H2H\" + ClientCode + DateTime.Now.ToString("ddMM") + strFileCount + ".xlsx";
     // }
     // else
     // {
         // fileDirPath = @"C:\inetpub\wwwroot\MPCDF_ERP\H2H\" + ClientCode + DateTime.Now.ToString("ddMM") + "-" + strFileCount + ".xlsx";
     // }
     

     // ds = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails",
                      // new string[] { "flag", "H2HClientCode", "FilePath", "Office_ID" },
                      // new string[] { "10", ClientCode, "~/H2H/" + GetFPath, objdb.Office_ID() }, "dataset");
     // lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "File Successfully Saved - " + fileDirPath);
     // if (ds != null && ds.Tables[0].Rows.Count > 0)
     // {
         // string H2HPaymentId = ds.Tables[0].Rows[0]["H2HPaymentId"].ToString();
         // foreach (GridViewRow rows in gv_SocietyMilkDispatchDetail.Rows)
         // {
             // CheckBox chk = (CheckBox)rows.FindControl("CheckBox1");
             // Label lblMilkCollectionInvoice_ID = (Label)rows.FindControl("lblMilkCollectionInvoice_ID");
             // if (chk.Checked)
             // {
                 // string MilkCollectionInvoice_ID = lblMilkCollectionInvoice_ID.Text;
                 // objdb.ByProcedure("USP_Trn_ProducerPaymentDetails",
                      // new string[] { "flag", "H2HPaymentId", "MilkCollectionInvoice_ID" },
                      // new string[] { "11", H2HPaymentId, MilkCollectionInvoice_ID }, "dataset");
             // }
         // }
     // }
	// using (XLWorkbook wb = new XLWorkbook())
     // {

         // var ws = wb.Worksheets.Add("Customers");
         // ws.FirstRow().FirstCell().InsertData(dt.Rows);

         // using (MemoryStream MyMemoryStream = new MemoryStream())
         // {
             
             // wb.SaveAs(fileDirPath);
             // MyMemoryStream.WriteTo(Response.OutputStream);
            
         // }
     // }
     
     

   // }
    protected void btnh2hFile_Click(object sender, EventArgs e)
    {

        try
        {
            string Status = "0";
            string flag = "";
            string ClientCode;
           
            if (objdb.Office_ID() == "4")
            {
                flag = "13";
                ClientCode = "ISDS31AXIS";
            }
            else if (objdb.Office_ID() == "6")
            {
                flag = "8";
                ClientCode = "USDSMCX";
            }
            else if (objdb.Office_ID() == "7")
            {
                flag = "14";
                ClientCode = "BKSDS31ICICI";
            }
			else if (objdb.Office_ID() == "5")
            {
                flag = "15";
                ClientCode = "JSDS31IDBI";
            }
			 else if (objdb.Office_ID() == "3")
            {
                flag = "8";
                ClientCode = "GSDSMCX";
            }
            else
            {
                flag = "8";
                ClientCode = "BSD31RBI";
            }
            ViewState["MilkCollectionInvoice_ID"] = "";
            
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
           
            foreach (GridViewRow rows in gv_SocietyMilkDispatchDetail.Rows)
            {
                CheckBox chk = (CheckBox)rows.FindControl("CheckBox1");
                Label lblMilkCollectionInvoice_ID = (Label)rows.FindControl("lblMilkCollectionInvoice_ID");
                if (chk.Checked)
                {
                    ViewState["MilkCollectionInvoice_ID"] += lblMilkCollectionInvoice_ID.Text + ",";
                    Status = "1";
                }
            }
            if (Status == "1")
            {
                ds = null;

                ds = objdb.ByProcedure("USP_SocietywiseMilkCollectionInvoice",
                        new string[] { "flag", "MilkCollectionInvoice_ID_Mlt","OfficeID" },
                         new string[] { flag, ViewState["MilkCollectionInvoice_ID"].ToString(),objdb.Office_ID() }, "dataset");

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            DataTable dt = new DataTable();
                            dt = ds.Tables[0];
                            int Count = ds.Tables[2].Rows.Count;
                            if (ds.Tables[2].Rows.Count > 0)
                            {
                                for (int i = 0; i < Count; i++)
                                {
                                    if (ds.Tables[2].Rows[i]["Mobile"].ToString() != "")
                                    {
                                        ViewState["Mobile"] = ds.Tables[2].Rows[i]["Mobile"].ToString();
                                        ViewState["TextMsg"] = "Amount of INR" + ds.Tables[2].Rows[i]["NetAmount"].ToString() + " processed for your milk cycle " + Convert.ToDateTime(txtFdt.Text, cult).ToString("dd/MM/yyyy") + " to " + Convert.ToDateTime(txtTdt.Text, cult).ToString("dd/MM/yyyy") + " , Thanks !";
                                        // ViewState["TextMsg"] = "दिनांक " + Convert.ToDateTime(txtFdt.Text, cult).ToString("dd/MM/yyyy")+ " से " + Convert.ToDateTime(txtTdt.Text, cult).ToString("dd/MM/yyyy")+ " समिति को भुगतान हो गया है, कृपया तीन दिवस में संपर्क करे, धन्यवाद !";
                                        SendSMS(ViewState["Mobile"].ToString(), ViewState["TextMsg"].ToString());
                                    }
                                }
                            }
                            if (objdb.Office_ID() == "4" || objdb.Office_ID() == "7" || objdb.Office_ID() == "5")
                            {

                                WriteNewforExcel(dt, ClientCode, ds.Tables[1].Rows[0]["V_FileNo"].ToString());
                            }
                            else
                            {
                                WriteNew(dt, ClientCode, ds.Tables[1].Rows[0]["V_FileNo"].ToString());
                            }
                            //WriteNew(dt, ClientCode, ds.Tables[1].Rows[0]["V_FileNo"].ToString());
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Society Data Not Available In Data For This Billing Cycle");
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Society Data Not Available In Data For This Billing Cycle");
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Society Data Not Available In Data For This Billing Cycle");
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Please Select At Least One CheckBox Row");
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : ). " + ex.Message.ToString());
        }
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
    //protected void lblGnerateH2HFile_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
    //        LinkButton lblGnerateH2HFile = (LinkButton)gv_SocietyMilkDispatchDetail.Rows[selRowIndex].FindControl("lblGnerateH2HFile");
    //        ViewState["MilkCollectionInvoice_ID"] = lblGnerateH2HFile.CommandArgument;
    //        ds = null;

    //        ds = objdb.ByProcedure("USP_SocietywiseMilkCollectionInvoice",
    //                new string[] { "flag", "MilkCollectionInvoice_ID" },
    //                 new string[] { "8", lblGnerateH2HFile.CommandArgument }, "dataset");

    //        if (ds != null)
    //        {
    //            if (ds.Tables.Count > 0)
    //            {
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {

    //                    DataTable dt = new DataTable();
    //                    dt = ds.Tables[0];
    //                    WriteNew(dt, "BSD31RBI", ds.Tables[1].Rows[0]["V_FileNo"].ToString());
    //                    FillGrid();
    //                }
    //                else
    //                {
    //                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Producer Data Not Available In Data For This Billing Cycle");
    //                }
    //            }
    //            else
    //            {
    //                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Producer Data Not Available In Data For This Billing Cycle");
    //            }
    //        }
    //        else
    //        {
    //            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Producer Data Not Available In Data For This Billing Cycle");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
    //    }
    //}
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
                    new string[] { "flag", "BillingCycleFromDate", "BillingCycleToDate", "GenerateTo_Office_ID", "Office_ID", "OfficeType_ID", "AdiwasiSociety" },
                     new string[] { "16", Fdate, Tdate, ddlccbmcdetail.SelectedValue,objdb.Office_ID() ,objdb.OfficeType_ID(),"1"}, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
						 hfOffice.Value = Session["Office_Name"].ToString();
                        hfcc.Value = ddlccbmcdetail.SelectedItem.Text;
                        hfBillingPeriod.Value = "Billing Period: " + txtFdt.Text + " to " + txtTdt.Text;
                        FS_DailyReport.Visible = true;
                        gv_SocietyMilkDispatchDetail.DataSource = ds.Tables[0];
                        gv_SocietyMilkDispatchDetail.DataBind();
						GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();
                        decimal TotalNetAmount = 0;
						decimal TotalQuantity = 0;
                        decimal TotalMilkValue = 0;
                        decimal TotalCommission = 0;
                        decimal TotalGrossEarning = 0;
						decimal TotalEarning = 0;
                        decimal TotalDeductionAdditionValue = 0;
                        decimal TotalProducerPayment = 0;
                        decimal TotalProducerAdjPayment = 0;
                        decimal TotalSocietyAdjPayment = 0;
                        decimal TotalHeadLoadCharges = 0;
                        decimal TotalChillingCost = 0;
                        TotalNetAmount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("NetAmount"));
                        TotalMilkValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("MilkValue"));
                        TotalCommission = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Commission"));
						TotalEarning = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Earning"));
                        TotalGrossEarning = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("GrossEarning"));
                        TotalDeductionAdditionValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Deduction"));
                        TotalProducerPayment = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ProducerPayment"));
                        TotalProducerAdjPayment = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ProducerAdjPayment"));
                        TotalSocietyAdjPayment = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SocietyAdjPayment"));
                        TotalHeadLoadCharges = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("HeadLoadCharges"));
                        TotalChillingCost = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ChillingCost"));
						TotalQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("MilkQuantity"));

                        gv_SocietyMilkDispatchDetail.FooterRow.Cells[3].Text = "<b>Total : </b>";
                        gv_SocietyMilkDispatchDetail.FooterRow.Cells[6].Text = "<b>" + TotalQuantity.ToString() + "</b>";
                        gv_SocietyMilkDispatchDetail.FooterRow.Cells[7].Text = "<b>" + TotalMilkValue.ToString() + "</b>";
                        gv_SocietyMilkDispatchDetail.FooterRow.Cells[8].Text = "<b>" + TotalCommission.ToString() + "</b>";
						gv_SocietyMilkDispatchDetail.FooterRow.Cells[9].Text = "<b>" + TotalEarning.ToString() + "</b>";
                        gv_SocietyMilkDispatchDetail.FooterRow.Cells[10].Text = "<b>" + TotalGrossEarning.ToString() + "</b>";
                        gv_SocietyMilkDispatchDetail.FooterRow.Cells[11].Text = "<b>" + TotalDeductionAdditionValue.ToString() + "</b>";
                        
                        gv_SocietyMilkDispatchDetail.FooterRow.Cells[12].Text = "<b>" + TotalHeadLoadCharges.ToString() + "</b>";
                        gv_SocietyMilkDispatchDetail.FooterRow.Cells[13].Text = "<b>" + TotalChillingCost.ToString() + "</b>";
                        gv_SocietyMilkDispatchDetail.FooterRow.Cells[14].Text = "<b>" + TotalNetAmount.ToString() + "</b>";
						
						 GridView1.FooterRow.Cells[2].Text = "<b>Total : </b>";
                        GridView1.FooterRow.Cells[5].Text = "<b>" + TotalQuantity.ToString() + "</b>";
                        GridView1.FooterRow.Cells[6].Text = "<b>" + TotalMilkValue.ToString() + "</b>";
                        GridView1.FooterRow.Cells[7].Text = "<b>" + TotalCommission.ToString() + "</b>";
                        GridView1.FooterRow.Cells[8].Text = "<b>" + TotalEarning.ToString() + "</b>";
                        GridView1.FooterRow.Cells[9].Text = "<b>" + TotalGrossEarning.ToString() + "</b>";
                        GridView1.FooterRow.Cells[10].Text = "<b>" + TotalDeductionAdditionValue.ToString() + "</b>";

                        GridView1.FooterRow.Cells[11].Text = "<b>" + TotalHeadLoadCharges.ToString() + "</b>";
                        GridView1.FooterRow.Cells[12].Text = "<b>" + TotalChillingCost.ToString() + "</b>";
                        GridView1.FooterRow.Cells[13].Text = "<b>" + TotalNetAmount.ToString() + "</b>";
                       
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
    
    protected void btnDwnldH2HFile_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
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
            string filePath = "";
            DataSet dsH2H = objdb.ByProcedure("USP_SocietywiseMilkCollectionInvoice",
                    new string[] { "flag", "BillingCycleFromDate", "BillingCycleToDate", "GenerateTo_Office_ID","Office_ID" },
                     new string[] { "18", Fdate, Tdate, ddlccbmcdetail.SelectedValue,objdb.Office_ID() }, "dataset");
            if (dsH2H != null && dsH2H.Tables[0].Rows.Count > 0)
            {
                if (objdb.Office_ID() == "4" || objdb.Office_ID() == "7" || objdb.Office_ID() == "5")
                {
                   filePath = dsH2H.Tables[0].Rows[0]["FilePath"].ToString();
				   filePath = filePath.Replace("~",@"C:/inetpub/wwwroot/MPCDF_ERP/");
				   FileInfo file = new FileInfo(filePath);
                   if (file.Exists)
                   {
                       Response.Clear();
                       Response.ClearHeaders();
                       Response.ClearContent();
                       Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                       Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(filePath));
                       //Response.AddHeader("content-disposition", "attachment; filename=" + Path.GetFileName(filePath));
                       //Response.AddHeader("Content-Type", "application/Excel");
                       //Response.ContentType = "application/vnd.xls";
                       Response.AddHeader("Content-Length", file.Length.ToString());
                       Response.WriteFile(file.FullName);
                       Response.End();
                   }
                   else
                   {
                       Response.Write("This file does not exist.");
                   }
				  
                }
                else
                {
                    filePath = dsH2H.Tables[0].Rows[0]["FilePath"].ToString();
                    filePath = filePath.Replace("txt", "005");
					Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = ContentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.WriteFile(filePath);
                    Response.End();
					 
                }
                    
					
               
               
            }
            if (dsH2H != null)
            {
                dsH2H.Dispose();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void btnPrintInvoice_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        dvReport.InnerHtml = "";
        string Status = "0";
        foreach(GridViewRow rows in gv_SocietyMilkDispatchDetail.Rows)
        {
            Label MilkCollectionInvoice_ID = (Label)rows.FindControl("lblMilkCollectionInvoice_ID");
            CheckBox CheckBox1 = (CheckBox)rows.FindControl("CheckBox1");
            if (CheckBox1.Checked == true)
            {
                Status = "1";
                ds = objdb.ByProcedure("USP_SocietywiseMilkCollectionInvoice",
                   new string[] { "flag", "MilkCollectionInvoice_ID" },
                    new string[] { "5", MilkCollectionInvoice_ID.Text }, "dataset");

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<div class='content pagebreak' style='border:1px solid black; width:100%; margin-top:30px;'>");
                        sb.Append("<table  style='width:100%; margin:5px;'>");
                        sb.Append("<tr>");
                        sb.Append("<td style='text-align:center' colspan='2'>" + Session["Office_Name"].ToString() + "");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td>Society Name And Code: " + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "  /  " + ds.Tables[0].Rows[0]["Office_Code"].ToString() + "");
                        sb.Append("</td>");
                        sb.Append("<td style='text-align:right'>CC: " + ddlccbmcdetail.SelectedItem.Text + "");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr >");
                        sb.Append("<td >Bank Name / IFSC / Account  : " + ds.Tables[0].Rows[0]["BankName"].ToString() + " /" + ds.Tables[0].Rows[0]["IFSC"].ToString() + " /" + ds.Tables[0].Rows[0]["BankAccountNo"].ToString());
                        sb.Append("</td>");
                        sb.Append("<td style='text-align:right'>Billing Period  : " + ds.Tables[0].Rows[0]["FromDate"].ToString() + " To " + ds.Tables[0].Rows[0]["ToDate"].ToString() + "");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");

                        sb.Append("<table  style='width:100%; margin-top:20px;'>");
                        sb.Append("<tr style='border-bottom:dashed; border-top:dashed'>");
                        sb.Append("<td>Date</td>");
                        sb.Append("<td>M / E</td>");
                        sb.Append("<td>Buf / Cow</td>");
                        sb.Append("<td>Kg.Wt</td>");
                        sb.Append("<td>Fat</td>");
                        sb.Append("<td>Clr</td>");
                        sb.Append("<td>S.N.F</td>");
                        sb.Append("<td>Kg.Fat</td>");
                        sb.Append("<td>Kg.Snf</td>");
                        sb.Append("<td>Value</td>");

                        sb.Append("<td>Quality</td>");
                        sb.Append("<td>Rate Per Ltr</td>");
                        sb.Append("</tr>");
                        int Count = ds.Tables[1].Rows.Count;

                        for (int i = 0; i < Count; i++)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["DispDate"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["Shift"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkType"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["FatPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["CLR"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["SnfPer"].ToString() + "</td>");

                            sb.Append("<td>" + ds.Tables[1].Rows[i]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["SnfInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkValue"].ToString() + "</td>");

                            sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkQuality"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[1].Rows[i]["RatePerLtr"].ToString() + "</td>");
                            sb.Append("</tr>");
                        }
                        decimal FatPer = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("FatPer"));
                        decimal SnfPer = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SnfPer"));
                        decimal MilkQuantity = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("MilkQuantity"));
                        decimal FatInKg = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                        decimal SnfInKg = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                        decimal MilkValue = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("MilkValue"));
                        decimal RatePerLtr = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("RatePerLtr"));
                        sb.Append("<tr style='border-top:dashed; padding-top:20px;'>");
                        sb.Append("<td>Total</td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td>" + MilkQuantity.ToString() + "</td>");
                        //sb.Append("<td>" + FatPer.ToString() + "</td>");
                        //sb.Append("<td>" + SnfPer.ToString() + "</td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td>" + FatInKg.ToString() + "</td>");
                        sb.Append("<td>" + SnfInKg.ToString() + "</td>");
                        //sb.Append("<td>" + FatPer.ToString() + "</td>");
                        sb.Append("<td>" + MilkValue.ToString() + "</td>");


                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        //sb.Append("<td>" + RatePerLtr.ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td>Particulars:</td>");
                        sb.Append("<td colspan='8'></td>");
                        sb.Append("<td>Rate:</td>");
                        sb.Append("</tr>");
                        decimal RateBuff = 0;
                        decimal RateCow = 0;
                        if (ds.Tables[6].Rows.Count > 0)
                        {
                            RateBuff = decimal.Parse(ds.Tables[6].Rows[0]["BuffRate"].ToString());
                            RateCow = decimal.Parse(ds.Tables[6].Rows[0]["CowRate"].ToString());
                        }
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
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["FatPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["CLR"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["SnfPer"].ToString() + "</td>");                     
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["SnfInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[2].Rows[j]["MilkValue"].ToString() + "</td>");
                            if (ds.Tables[2].Rows[j]["MilkQuality"].ToString() == "Good")
                            {
                                sb.Append("<td>" + RateBuff + "</td>");

                            }
                            else if (ds.Tables[2].Rows[j]["MilkQuality"].ToString() == "Sour")
                            {
                                sb.Append("<td>" + ((RateBuff) * 50 / 100) + "</td>");

                            }
                            else
                            {
                                sb.Append("<td>" + ((RateBuff) * 30 / 100) + "</td>");
                            }
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
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["FatPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["CLR"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["SnfPer"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["SnfInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[3].Rows[j]["MilkValue"].ToString() + "</td>");
                            if (ds.Tables[3].Rows[j]["MilkQuality"].ToString() == "Good")
                            {
                                sb.Append("<td>" + RateCow + "</td>");

                            }
                            else if (ds.Tables[3].Rows[j]["MilkQuality"].ToString() == "Sour")
                            {
                                sb.Append("<td>" + ((RateCow) * 50 / 100) + "</td>");

                            }
                            else
                            {
                                sb.Append("<td>" + ((RateCow) * 30 / 100) + "</td>");
                            }

                            sb.Append("</tr>");



                        }
                        if (objdb.Office_ID() == "4")
                        {
                            sb.Append("<tr>");
                            //sb.Append("<td colspan='8'>" + ds.Tables[5].Rows[0]["CommissionNarration"].ToString() + "" + ds.Tables[0].Rows[0]["Commission"].ToString() + "</td>");
                            sb.Append("<td colspan='8'>" + ds.Tables[5].Rows[0]["CommissionNarration"].ToString() + "</td>");
                            sb.Append("<td colspan='2'>Commission - :</td>");
                            sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["Commission"].ToString() + "</td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td colspan='2'>MilkValue:</td>");
                            sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["MilkValue"].ToString() + "</td>");

                            sb.Append("</tr>");
                        }
                        else
                        {
                            sb.Append("<tr>");
                            sb.Append("<td colspan='2'>MilkValue:</td>");
                            sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["MilkValue"].ToString() + "</td>");
                            sb.Append("<td colspan='2'>Commission - :</td>");
                            sb.Append("<td colspan='2'>" + ds.Tables[0].Rows[0]["Commission"].ToString() + "</td>");
                            sb.Append("</tr>");
                        }


                        sb.Append("</table>");
                        sb.Append("<table class='table'>");
                        int HeadTypeCount = ds.Tables[4].Rows.Count;
                        sb.Append("<tr style='border-top:dashed; padding:20px;'>");
                        sb.Append("<td>");
                        sb.Append("<table>");
                        sb.Append("<tr>");
                        sb.Append("<td  >Earn.Head:</td>");
                        sb.Append("<td >Amount:</td>");
                        sb.Append("<td >Remark:</td>");
                        //sb.Append("<td >Deduction.Head:</td>");
                        //sb.Append("<td >Amount:</td>");
                        //sb.Append("<td colspan='4'>Remark:</td>");
                        //sb.Append("<td ></td>");
                        sb.Append("</tr>");
                        string HeadType = "";

                        for (int i = 0; i < HeadTypeCount; i++)
                        {
                            sb.Append("<tr>");

                            if (ds.Tables[4].Rows[i]["HeadType"].ToString() == "ADDITION")
                            {

                                sb.Append("<td style='width:30%'>" + ds.Tables[4].Rows[i]["HeadName"].ToString() + "</td>");
                                sb.Append("<td style='width:10%'>" + ds.Tables[4].Rows[i]["HeadAmount"].ToString() + "</td>");
                                sb.Append("<td style='width:10%'>" + ds.Tables[4].Rows[i]["HeadRemark"].ToString() + "</td>");
                                //sb.Append("<td ></td>");
                                //sb.Append("<td ></td>");
                                //sb.Append("<td colspan='4'></td>");
                                //sb.Append("<td ></td>");

                            }
                            //else if (ds.Tables[4].Rows[i]["HeadType"].ToString() == "DEDUCTION")
                            //{

                            //    sb.Append("<td ></td>");
                            //    sb.Append("<td></td>");
                            //    sb.Append("<td colspan='4'></td>");
                            //    sb.Append("<td>" + ds.Tables[4].Rows[i]["HeadName"].ToString() + "</td> ");
                            //    sb.Append("<td >" + ds.Tables[4].Rows[i]["HeadAmount"].ToString() + "</td>");
                            //    sb.Append("<td colspan='4'>" + ds.Tables[4].Rows[i]["HeadRemark"].ToString() + "</td>");
                            //    sb.Append("<td ></td>");
                            //}



                            sb.Append("</tr>");
                        }
                        sb.Append("</table>");
                        sb.Append("</td>");
                        sb.Append("<td>");
                        sb.Append("<table>");
                        sb.Append("<tr>");
                        sb.Append("<td >Deduction.Head:</td>");
                        sb.Append("<td >Amount:</td>");
                        sb.Append("<td >Remark:</td>");
                        //sb.Append("<td >Deduction.Head:</td>");
                        //sb.Append("<td >Amount:</td>");
                        //sb.Append("<td colspan='4'>Remark:</td>");
                        //sb.Append("<td ></td>");
                        sb.Append("</tr>");


                        for (int i = 0; i < HeadTypeCount; i++)
                        {
                            sb.Append("<tr>");

                            if (ds.Tables[4].Rows[i]["HeadType"].ToString() == "DEDUCTION")
                            {

                                sb.Append("<td style='width:30%'>" + ds.Tables[4].Rows[i]["HeadName"].ToString() + "</td>");
                                sb.Append("<td style='width:10%'>" + ds.Tables[4].Rows[i]["HeadAmount"].ToString() + "</td>");
                                sb.Append("<td style='width:10%'>" + ds.Tables[4].Rows[i]["HeadRemark"].ToString() + "</td>");
                                //sb.Append("<td ></td>");
                                //sb.Append("<td ></td>");
                                //sb.Append("<td colspan='4'></td>");
                                //sb.Append("<td ></td>");

                            }
                            //else if (ds.Tables[4].Rows[i]["HeadType"].ToString() == "DEDUCTION")
                            //{

                            //    sb.Append("<td ></td>");
                            //    sb.Append("<td></td>");
                            //    sb.Append("<td colspan='4'></td>");
                            //    sb.Append("<td>" + ds.Tables[4].Rows[i]["HeadName"].ToString() + "</td> ");
                            //    sb.Append("<td >" + ds.Tables[4].Rows[i]["HeadAmount"].ToString() + "</td>");
                            //    sb.Append("<td colspan='4'>" + ds.Tables[4].Rows[i]["HeadRemark"].ToString() + "</td>");
                            //    sb.Append("<td ></td>");
                            //}



                            sb.Append("</tr>");
                        }
                        sb.Append("</table>");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        sb.Append("<table>");
                        sb.Append("<tr style='border-top:dashed; padding:20px;'>");


                        sb.Append("<td> GROSS EARNING:&nbsp;&nbsp;&nbsp;&nbsp" + ds.Tables[0].Rows[0]["GrossEarning"].ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["GrossEarning"].ToString() + "</td>");
                        sb.Append("<td >Deductions:&nbsp;&nbsp;&nbsp;&nbsp;" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[0]["DeductionAdditionValue"].ToString())).ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["DeductionAdditionValue"].ToString() + "</td>");
                        //sb.Append("<td >Producer Payment:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ProducerPayment"].ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["ProducerPayment"].ToString() + "</td>");
                        //sb.Append("<td style='width:40%'>Producer Adj Amount:&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ProducerAdjPayment"].ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["ProducerAdjPayment"].ToString() + "</td>");
                        //sb.Append("</tr>");

                       // sb.Append("<tr>");

                       // sb.Append("<td style='width:40%'>Society Adjust Amount:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ProducerAdjPayment"].ToString() + "</td>");
                        // sb.Append("<td>" + ds.Tables[0].Rows[0]["SocietyAdjPayment"].ToString() + "</td>");
                        sb.Append("<td >Chilling Cost:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ChillingCost"].ToString() + "</td>");
                        //  sb.Append("<td>" + ds.Tables[0].Rows[0]["ChillingCost"].ToString() + "</td>");



                        sb.Append("<td >Head Load Charges:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["HeadLoadCharges"].ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["HeadLoadCharges"].ToString() + "</td>");

                        sb.Append("<td >Net:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["NetAmount"].ToString() + "</td>");
                        //sb.Append("<td >" + ds.Tables[0].Rows[0]["NetAmount"].ToString() + "</td>");

                        sb.Append("</tr>");
                        sb.Append("</table>");
						if (ds.Tables[8].Rows.Count > 0)
                        {
                            sb.Append("<table>");
                            sb.Append("<tr>");

                            sb.Append("<td>Remark:   " + ds.Tables[8].Rows[0]["Remark"].ToString() + "</td>");
                            sb.Append("</tr>");
                            sb.Append("</table>");
                        }
                        if (objdb.Office_ID() == "2" || objdb.Office_ID() == "4")
                        {
                            if (ds.Tables[7].Rows.Count > 0)
                            {
                                sb.Append("<table>");
                                sb.Append("<tr>");
                                sb.Append("<td>* CURRENT LEDGER POSITION , TODAY *   </td>");
                                sb.Append("<td></td>");
                                sb.Append("</tr>");
                                sb.Append("<tr>");
                                sb.Append("<td>*************************************************</td>");
                                sb.Append("<td>* A M O U N T *</td>");
                                sb.Append("</tr>");
                                int LedgerCount = ds.Tables[7].Rows.Count;
                                for (int i = 0; i < LedgerCount; i++)
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td>" + ds.Tables[7].Rows[i]["ItemBillingHead_Name"].ToString() + "</td>");
                                    sb.Append("<td>" + ds.Tables[7].Rows[i]["BalanceAmount"].ToString() + "</td>");
                                    sb.Append("</tr>");
                                }
								sb.Append("<tr>");
								sb.Append("<td>Total</td>");
								sb.Append("<td>" + ds.Tables[7].AsEnumerable().Sum(row => row.Field<decimal>("BalanceAmount")).ToString() + "</td>");
								sb.Append("</tr>");

                                sb.Append("</table>");
                            }
                        }
                        
                        sb.Append("</div>");
						dvReport.InnerHtml += sb.ToString();

                        }
                        else
                        {

                        }
                    }

                    else
                    {

                    }


                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "window.print();", true);
            }
            

        }
        if(Status == "0")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction","alert('Please Select at Least one checkbox')", true);
        }
       
    }
    private void SendSMS(string MobileNo, string SMSText)
    {
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //Your authentication key
        //string authKey = "3597C1493C124F";
        //Sender ID
        //string senderId = "MPSCDF";

        //string link = "http://mysms.mssinfotech.com/app/smsapi/index.php?key=" + authKey + "&type=unicode&routeid=288&contacts=" + //MobileNo + "&senderid=" + senderId + "&msg=" + Server.UrlEncode(SMSText);
        string link = "http://digimate.airtel.in:15181/BULK_API/SendMessage?loginID=mpmilk_api&password=mpmilk@123&mobile=" + MobileNo + "&text=" + Server.UrlEncode(SMSText) + "&senderid=MPMILK&DLT_TM_ID=1001096933494158&DLT_CT_ID=1407162468489054757&DLT_PE_ID=1401514060000041644&route_id=DLT_SERVICE_IMPLICT&Unicode=2&camp_name=mpmilk_user";
        HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
        Stream stream = response.GetResponseStream();

    }
    protected void btnProducerMsg_Click(object sender, EventArgs e)
    {
        try
        {
            string Mobile = "";
            string Msg = "";
            ds = objdb.ByProcedure("USP_SocietywiseMilkCollectionInvoice", new string[] { "flag", "CC_Id", "BillingCycleFromDate", "BillingCycleToDate", "AdiwasiSociety" },
                                                                            new string[] { "23", ddlccbmcdetail.SelectedValue, Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd"),"1" }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int Count = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["Mobile"].ToString() != "")
                        {
                            Mobile = ds.Tables[0].Rows[i]["Mobile"].ToString();
                            //ViewState["TextMsg"] = "Amount of INR" + ds.Tables[0].Rows[i]["NetAmount"].ToString() + " processed for your milk cycle " + Convert.ToDateTime(txtFdt.Text, cult).ToString("dd/MM/yyyy") + " to " + Convert.ToDateTime(txtTdt.Text, cult).ToString("dd/MM/yyyy") + " , Thanks !";
                            Msg = "दिनांक " + Convert.ToDateTime(txtFdt.Text, cult).ToString("dd/MM/yyyy") + " से " + Convert.ToDateTime(txtTdt.Text, cult).ToString("dd/MM/yyyy") + " समिति को भुगतान हो गया है, कृपया तीन दिवस में संपर्क करे, धन्यवाद !";
                            SendSMS(Mobile, Msg);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Sorry !", ex.Message.ToString());
        }
    }
}