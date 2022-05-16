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

public partial class mis_MilkCollection_SocietyMilkCollectionInvoiceDetails : System.Web.UI.Page
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
                FillSociety();
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


    

    protected void FillSociety()
    {
        try
        {

            ds = null;
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "Office_ID" },
                              new string[] { "10", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSociety.DataTextField = "Office_Name";
                    ddlSociety.DataValueField = "Office_ID";
                    ddlSociety.DataSource = ds;
                    ddlSociety.DataBind();
                    ddlSociety.Enabled = false;

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
                        sb.Append("<table class='table1'  style='width:100%; margin:5px;'>");
                        sb.Append("<thead class='header'>");
                        sb.Append("<tr>");
                        sb.Append("<td style='text-align:center' colspan='2'>" + Session["Office_Name"].ToString() + "");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td>Society Name And Code: " + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "  /  " + ds.Tables[0].Rows[0]["Office_Code"].ToString() + "");
                        sb.Append("</td>");
                        //sb.Append("<td style='text-align:right'>CC: " + ddlccbmcdetail.SelectedItem.Text + "");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr >");
                        sb.Append("<td >Bank Name / IFSC / Account  : " + ds.Tables[0].Rows[0]["BankName"].ToString() + " /" + ds.Tables[0].Rows[0]["IFSC"].ToString() + " /" + ds.Tables[0].Rows[0]["BankAccountNo"].ToString());
                        sb.Append("</td>");
                        sb.Append("<td style='text-align:right'>Billing Period  : " + ds.Tables[0].Rows[0]["FromDate"].ToString() + " To " + ds.Tables[0].Rows[0]["ToDate"].ToString() + "");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("</thead>");
                        sb.Append("</table>");

                        sb.Append("<table  class='table1'  style='width:100%; margin-top:20px;'>");
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
                        sb.Append("<table class='table'>");
                        sb.Append("<tr style='border-top:dashed; padding:20px;'>");


                        sb.Append("<td style='text-align:left'>GROSS EARNING:&nbsp;&nbsp;&nbsp;&nbsp" + ds.Tables[0].Rows[0]["GrossEarning"].ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["GrossEarning"].ToString() + "</td>");
                        sb.Append("<td >Deductions:&nbsp;&nbsp;&nbsp;&nbsp;" + Math.Abs(decimal.Parse(ds.Tables[0].Rows[0]["DeductionAdditionValue"].ToString())).ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["DeductionAdditionValue"].ToString() + "</td>");
                        //sb.Append("<td style='width:20%'>Producer Payment:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ProducerPayment"].ToString() + "</td>");
                        ////sb.Append("<td>" + ds.Tables[0].Rows[0]["ProducerPayment"].ToString() + "</td>");
                        //sb.Append("<td style='width:40%'>Producer Adj Amount:&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ProducerAdjPayment"].ToString() + "</td>");
                        ////sb.Append("<td>" + ds.Tables[0].Rows[0]["ProducerAdjPayment"].ToString() + "</td>");
                        //sb.Append("</tr>");

                        //sb.Append("<tr>");

                        //sb.Append("<td style='width:40%'>Society Adjust Amount:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ProducerAdjPayment"].ToString() + "</td>");
                        // sb.Append("<td>" + ds.Tables[0].Rows[0]["SocietyAdjPayment"].ToString() + "</td>");
                        sb.Append("<td >Chilling Cost:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["ChillingCost"].ToString() + "</td>");
                        //  sb.Append("<td>" + ds.Tables[0].Rows[0]["ChillingCost"].ToString() + "</td>");



                        sb.Append("<td >Head Load Charges:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["HeadLoadCharges"].ToString() + "</td>");
                        //sb.Append("<td>" + ds.Tables[0].Rows[0]["HeadLoadCharges"].ToString() + "</td>");

                        sb.Append("<td >Net:&nbsp;&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["NetAmount"].ToString() + "</td>");
                        //sb.Append("<td >" + ds.Tables[0].Rows[0]["NetAmount"].ToString() + "</td>");

                        sb.Append("</tr>");
                        sb.Append("</table>");
                        if (objdb.Office_ID() == "2")
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
                    new string[] { "flag", "BillingCycleFromDate", "BillingCycleToDate", "GenerateTo_Office_ID" },
                     new string[] { "12", Fdate, Tdate, ddlSociety.SelectedValue }, "dataset");

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
    
}