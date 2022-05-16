using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using System.Net;
using System.Text;
using System.Linq;

public partial class mis_MilkCollection_ProducerNegativePaymentInvoice : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    string Fdate = "";
    string Tdate = "";

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
    protected void ddlMilkCollectionUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSociety();
    }
    protected void FillSociety()
    {
        try
        {
            // lblMsg.Text = "";
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "Office_ID" },
                              new string[] { "10", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtSociatyName.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                   
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            divIteminfo.Visible = false;
            FillPrint();
            string CDate = DateTime.Now.ToString("dd/MM/yyyy");

            if (txtFdt.Text != "")
            {
                Fdate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
            }

            if (txtTdt.Text != "")
            {
                Tdate = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
            }

            ds = null;
            ds = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails",
                        new string[] { "flag", "PaymentFromDt", "PaymentToDt", "Office_ID" },
                        new string[] { "12", Fdate, Tdate, ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        divIteminfo.Visible = true;
                        
                        int Count = ds.Tables[1].Rows.Count;
                        decimal TotalAvg_Fat = 0;
                        decimal TotalAvg_SNF = 0;
                        decimal TotalAvg_CLR = 0;
                        decimal TotalQuantity = 0;
                        decimal TotalPayableAmount = 0;
                        decimal TotalSaleValue = 0;
                        decimal TotalAdjustAmount = 0;
                        decimal TotalEarningValue = 0;

                        StringBuilder sb = new StringBuilder();
                        sb.Append("<section class='content' style='border:1px solid black'>");                      
                        sb.Append("<p style='text-align:center;'><b>पेमेंट एडवाइस रजिस्टर</b></p>");
                       
                        sb.Append("<table class='table1'>");
                       
                        sb.Append("<tr>");
                        sb.Append("<td><b>समिति कोड</b></td>");
                        sb.Append("<td>"+ds.Tables[0].Rows[0]["Office_Code"].ToString()+"</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td><b>समिति का नाम</b></td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td><b>समिति पेमेंट साइकिल कोड</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[0]["BillingCode"].ToString() + "</td>");
                        sb.Append("<td><b>तारीख से</b></td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[0]["FromDate"].ToString() + "</td>");
                        sb.Append("<td><b>तारीख तक</b></td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[0]["ToDate"].ToString() + "</td>");
                        sb.Append("<td colspan='9' style='padding-left:250px;'><b>प्रिंट दिनांक</b></td>");
                        sb.Append("<td>" + CDate.ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td ><b>भुगतान के प्रकार:</b></td>");
                        sb.Append("<td >सभी</td>");
                        sb.Append("</tr>");
                        
                        sb.Append("</table>");
                         
                        sb.Append("<hr style='border:1px solid black'>");
                        sb.Append("<div class='table-responsive'>");
                        sb.Append("<table class='table2 page' style='max-width:100%'>");
                       
                        sb.Append("<tr>");
                        sb.Append("<th>क्रमांक</th>");
                        //sb.Append("<th>कोड</th>");
                        sb.Append("<th>नाम</th>");
                        sb.Append("<th>कुल शिफ्ट</th>");
                        sb.Append("<th>बैंक खता नंबर</th>");
                        sb.Append("<th>आईएफएससी कोड</th>");
                        sb.Append("<th>मात्रा(ली.)</th>");
                        sb.Append("<th>औसत. फैट(%)</th>");
                        sb.Append("<th>औसत. एस.एन.एफ(%)</th>");
                        sb.Append("<th>औसत.सीएल.आर.(%)</th>");
                        sb.Append("<th>राशि(रु.)</th>");
                        sb.Append("<th>Earning</th>");
                        sb.Append("<th>कटोती(रु.)</th>");
                        sb.Append("<th>Adjust Amount</th>");
                        sb.Append("<th>Final Amount</th>");
                        //sb.Append("<th>Rounding Amount</th>");
                        sb.Append("<th>हस्ताक्षर</th>");
                        sb.Append("</tr>");
                       
                        sb.Append("<tr>");
                        sb.Append("<td colspan='16'>पेमेंट प्रकार - </td>");
                        sb.Append("</tr>");
                        
                        for (int i = 0; i < Count; i++)
                        {
                            
                            
                                sb.Append("<tr>");
                                sb.Append("<td>" + (i + 1).ToString() + "</td>");
                                //sb.Append("<td>" + ds.Tables[1].Rows[i]["ProducerCardNo"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["ProducerName"].ToString() + "(" + ds.Tables[1].Rows[i]["ProducerCardNo"].ToString() + ")</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["TotalShift"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["AccountNo"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["IFSC"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["TotalLtr_MilkQty"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["Avg_Fat"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["Avg_SNF"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["Avg_CLR"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkValue"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["EarningValue"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["SaleValue"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["AdjustAmount"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["PayableAmount"].ToString() + "</td>");
                                //sb.Append("<td></td>");
                                sb.Append("<td></td>");
                                sb.Append("</tr>");
                            
                            
                            //TotalAvg_Fat +=  decimal.Parse(ds.Tables[1].Rows[i]["Avg_Fat"].ToString());
                            //TotalAvg_SNF += decimal.Parse(ds.Tables[1].Rows[i]["Avg_SNF"].ToString());
                            //TotalAvg_CLR += decimal.Parse(ds.Tables[1].Rows[i]["Avg_CLR"].ToString());
                            //TotalQuantity += decimal.Parse(ds.Tables[1].Rows[i]["TotalLtr_MilkQty"].ToString());
                            //TotalPayableAmount += decimal.Parse(ds.Tables[1].Rows[i]["MilkValue"].ToString());
                            //TotalAdjustAmount += decimal.Parse(ds.Tables[1].Rows[i]["AdjustAmount"].ToString());
                            //TotalSaleValue += decimal.Parse(ds.Tables[1].Rows[i]["SaleValue"].ToString());
                            //TotalEarningValue += decimal.Parse(ds.Tables[1].Rows[i]["EarningValue"].ToString());
                        }
                        TotalAvg_Fat = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Avg_Fat"));
                        TotalAvg_SNF = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Avg_SNF"));
                        TotalAvg_CLR = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Avg_CLR"));
                        TotalQuantity = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TotalLtr_MilkQty"));
                        TotalPayableAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("MilkValue"));
                        TotalAdjustAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("AdjustAmount"));
                        TotalSaleValue = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SaleValue"));

                        TotalAvg_Fat = Math.Round(TotalAvg_Fat / Count,2);
                        TotalAvg_SNF =  Math.Round(TotalAvg_SNF / Count,2);
                        TotalAvg_CLR = Math.Round(TotalAvg_CLR / Count, 2);
                        
                        sb.Append("<tr>");
                        sb.Append("<td colspan='5'><b>कैश अनुमान टोटल</b></td>");
                        sb.Append("<td>" + TotalQuantity.ToString() + "</td>");
                        sb.Append("<td>" + TotalAvg_Fat.ToString() + "</td>");
                        sb.Append("<td>" + TotalAvg_SNF.ToString() + "</td>");
                        sb.Append("<td>" + TotalAvg_CLR.ToString() + "</td>");
                        sb.Append("<td>" + TotalPayableAmount.ToString() + "</td>");
                        sb.Append("<td>" + TotalEarningValue.ToString() + "</td>");
                        sb.Append("<td>" + TotalSaleValue.ToString() + "</td>");
                        sb.Append("<td>" + TotalAdjustAmount.ToString() + "</td>");
                        sb.Append("<td>" + (TotalPayableAmount + TotalEarningValue - TotalSaleValue - TotalAdjustAmount) + "</td>");
                        //sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("</tr>");
                        //sb.Append("<tr>");
                        //sb.Append("<td colspan='6'><b>कुल जोड़</b></td>");
                        //sb.Append("<td>" + TotalQuantity.ToString() + "</td>");
                        //sb.Append("<td>" + TotalAvg_Fat.ToString() + "</td>");
                        //sb.Append("<td>" + TotalAvg_SNF.ToString() + "</td>");
                        //sb.Append("<td>" + TotalAvg_CLR.ToString() + "</td>");
                        //sb.Append("<td>" + TotalPayableAmount.ToString() + "</td>");
                        //sb.Append("<td>" + TotalSaleValue.ToString() + "</td>");
                      
                        //sb.Append("<td>" + TotalPayableAmount.ToString() + "</td>");
                        //sb.Append("<td></td>");
                        //sb.Append("<td></td>");
                        //sb.Append("</tr>");
                        sb.Append("</table>");
                        
                        sb.Append("</div>");
                        
                        sb.Append("</section>");
                        divdetails.InnerHtml = sb.ToString();

                    }
                    else
                    {
                        
                        divIteminfo.Visible = false;
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Sorry!", "No Record Found");

                    }
                }

                else
                {
                   
                    divIteminfo.Visible = false;
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Sorry!", "No Record Found");

                }

            }
            else
            {
               
                divIteminfo.Visible = false;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Sorry!", "No Record Found");
            }

           

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void FillPrint()
    {
        try
        {
            string CDate = DateTime.Now.ToString("dd/MM/yyyy");

            if (txtFdt.Text != "")
            {
                Fdate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
            }

            if (txtTdt.Text != "")
            {
                Tdate = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
            }

            ds = null;
            ds = objdb.ByProcedure("USP_Trn_ProducerPaymentDetails",
                        new string[] { "flag", "PaymentFromDt", "PaymentToDt", "Office_ID" },
                        new string[] { "12", Fdate, Tdate, ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {

                        int PCount = 0;
                        int Count = ds.Tables[1].Rows.Count;
                        decimal TotalAvg_Fat = 0;
                        decimal TotalAvg_SNF = 0;
                        decimal TotalAvg_CLR = 0;
                        decimal TotalQuantity = 0;
                        decimal TotalPayableAmount = 0;
                        decimal TotalSaleValue = 0;
                        decimal TotalAdjustAmount = 0;
                        decimal TotalEarningValue = 0;
                        StringBuilder sb = new StringBuilder();


                        sb.Append("<p style='text-align:center;'><b>पेमेंट एडवाइस रजिस्टर</b></p>");
                        sb.Append("<div class='header'>");
                        sb.Append("<table class='table1 headertable'>");

                        sb.Append("<tr>");
                        sb.Append("<td><b>समिति कोड</b></td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[0]["Office_Code"].ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td><b>समिति का नाम</b></td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td><b>समिति पेमेंट साइकिल कोड</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[0]["BillingCode"].ToString() + "</td>");
                        sb.Append("<td><b>तारीख से</b></td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[0]["FromDate"].ToString() + "</td>");
                        sb.Append("<td><b>तारीख तक</b></td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[0]["ToDate"].ToString() + "</td>");
                        sb.Append("<td colspan='9' style='padding-left:250px;'><b>प्रिंट दिनांक</b></td>");
                        sb.Append("<td>" + CDate.ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td ><b>भुगतान के प्रकार:</b></td>");
                        sb.Append("<td >सभी</td>");
                        sb.Append("</tr>");

                        sb.Append("</table>");
                        sb.Append("</div>");
                        sb.Append("<table>");
                        sb.Append("<tr class='headersub'></tr>");
                        sb.Append("</table>");
                        sb.Append("<hr style='border:1px solid black'>");

                        sb.Append("<table class='table2 page'>");

                        sb.Append("<tr>");
                        sb.Append("<th>क्रमांक</th>");
                        //sb.Append("<th>कोड</th>");
                        sb.Append("<th>नाम</th>");
                        sb.Append("<th>कुल शिफ्ट</th>");
                        sb.Append("<th>बैंक खता नंबर</th>");
                        sb.Append("<th>आईएफएससी कोड</th>");
                        sb.Append("<th>मात्रा(ली.)</th>");
                        sb.Append("<th>औसत. फैट(%)</th>");
                        sb.Append("<th>औसत. एस.एन.एफ(%)</th>");
                        sb.Append("<th>औसत.सीएल.आर.(%)</th>");
                        sb.Append("<th>राशि(रु.)</th>");
                        sb.Append("<th>Earning</th>");
                        sb.Append("<th>कटोती(रु.)</th>");
                        sb.Append("<th>Adjust Amount</th>");
                        sb.Append("<th>Final Amount</th>");
                        
                        sb.Append("<th>हस्ताक्षर</th>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td colspan='16'>पेमेंट प्रकार - </td>");
                        sb.Append("</tr>");

                        for (int i = 0; i < Count; i++)
                        {
                            PCount += 1;
                            if ((PCount) % 15 == 0)
                            {
                                sb.Append("</table>");
                                sb.Append("<div class='header'>");
                                sb.Append("<table class='table1' style='width:100%'>");

                                sb.Append("<tr>");
                                sb.Append("<td><b>समिति कोड</b></td>");
                                sb.Append("<td>" + ds.Tables[0].Rows[0]["Office_Code"].ToString() + "</td>");
                                sb.Append("</tr>");
                                sb.Append("<tr>");
                                sb.Append("<td><b>समिति का नाम</b></td>");
                                sb.Append("<td>" + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "</td>");
                                sb.Append("</tr>");
                                sb.Append("<tr>");
                                sb.Append("<td><b>समिति पेमेंट साइकिल कोड</td>");
                                sb.Append("<td>" + ds.Tables[0].Rows[0]["BillingCode"].ToString() + "</td>");
                                sb.Append("<td><b>तारीख से</b></td>");
                                sb.Append("<td>" + ds.Tables[0].Rows[0]["FromDate"].ToString() + "</td>");
                                sb.Append("<td><b>तारीख तक</b></td>");
                                sb.Append("<td>" + ds.Tables[0].Rows[0]["ToDate"].ToString() + "</td>");
                                sb.Append("<td colspan='9' style='padding-left:250px;'><b>प्रिंट दिनांक</b></td>");
                                sb.Append("<td>" + CDate.ToString() + "</td>");
                                sb.Append("</tr>");
                                sb.Append("<tr>");
                                sb.Append("<td ><b>भुगतान के प्रकार:</b></td>");
                                sb.Append("<td >सभी</td>");
                                sb.Append("</tr>");

                                sb.Append("</table>");

                                sb.Append("<table>");
                                sb.Append("<tr class='headersub'></tr>");
                                sb.Append("</table>");
                                sb.Append("<hr style='border:1px solid black'>");

                                sb.Append("<table class='table2 page' style='width:100%'>");

                                sb.Append("<tr>");
                                sb.Append("<th>क्रमांक</th>");
                                //sb.Append("<th>कोड</th>");
                                sb.Append("<th>नाम</th>");
                                sb.Append("<th>कुल शिफ्ट</th>");
                                sb.Append("<th>बैंक खता नंबर</th>");
                                sb.Append("<th>आईएफएससी कोड</th>");
                                sb.Append("<th>मात्रा(ली.)</th>");
                                sb.Append("<th>औसत. फैट(%)</th>");
                                sb.Append("<th>औसत. एस.एन.एफ(%)</th>");
                                sb.Append("<th>औसत.सीएल.आर.(%)</th>");
                                sb.Append("<th>राशि(रु.)</th>");
                                sb.Append("<th>Earning</th>");
                                sb.Append("<th>कटोती(रु.)</th>");
                                sb.Append("<th>Adjust Amount</th>");
                                sb.Append("<th>Final Amount</th>");
                                
                                sb.Append("<th>हस्ताक्षर</th>");
                                sb.Append("</tr>");

                                sb.Append("<tr>");
                                sb.Append("<td colspan='15'>पेमेंट प्रकार - </td>");
                                sb.Append("</tr>");
                                sb.Append("<tr>");
                                sb.Append("<td>" + (i + 1).ToString() + "</td>");
                                //sb.Append("<td>" + ds.Tables[1].Rows[i]["ProducerCardNo"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["ProducerName"].ToString() + "(" + ds.Tables[1].Rows[i]["ProducerCardNo"].ToString() + ")</td>");
                                
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["TotalShift"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["AccountNo"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["IFSC"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["TotalLtr_MilkQty"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["Avg_Fat"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["Avg_SNF"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["Avg_CLR"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkValue"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["EarningValue"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["SaleValue"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["AdjustAmount"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["PayableAmount"].ToString() + "</td>");
                                //sb.Append("<td></td>");
                                sb.Append("<td></td>");
                                sb.Append("</tr>");
                            }
                            else
                            {
                                sb.Append("<tr>");
                                sb.Append("<td>" + (i + 1).ToString() + "</td>");
                                //sb.Append("<td>" + ds.Tables[1].Rows[i]["ProducerCardNo"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["ProducerName"].ToString() + "(" + ds.Tables[1].Rows[i]["ProducerCardNo"].ToString() + ")</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["TotalShift"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["AccountNo"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["IFSC"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["TotalLtr_MilkQty"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["Avg_Fat"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["Avg_SNF"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["Avg_CLR"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkValue"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["EarningValue"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["SaleValue"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["AdjustAmount"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[1].Rows[i]["PayableAmount"].ToString() + "</td>");
                                //sb.Append("<td></td>");
                                sb.Append("<td></td>");
                                sb.Append("</tr>");
                            }

                            //TotalAvg_Fat += decimal.Parse(ds.Tables[1].Rows[i]["Avg_Fat"].ToString());
                            //TotalAvg_SNF += decimal.Parse(ds.Tables[1].Rows[i]["Avg_SNF"].ToString());
                            //TotalAvg_CLR += decimal.Parse(ds.Tables[1].Rows[i]["Avg_CLR"].ToString());
                            //TotalQuantity += decimal.Parse(ds.Tables[1].Rows[i]["TotalLtr_MilkQty"].ToString());
                            //TotalPayableAmount += decimal.Parse(ds.Tables[1].Rows[i]["MilkValue"].ToString());
                            //TotalAdjustAmount += decimal.Parse(ds.Tables[1].Rows[i]["AdjustAmount"].ToString());
                            //TotalSaleValue += decimal.Parse(ds.Tables[1].Rows[i]["SaleValue"].ToString());
                            //TotalEarningValue += decimal.Parse(ds.Tables[1].Rows[i]["EarningValue"].ToString());
                        }

                        TotalAvg_Fat = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Avg_Fat"));
                        TotalAvg_SNF = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Avg_SNF"));
                        TotalAvg_CLR = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("Avg_CLR"));
                        TotalQuantity = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("TotalLtr_MilkQty"));
                        TotalPayableAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("MilkValue"));
                        TotalAdjustAmount = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("AdjustAmount"));
                        TotalSaleValue = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SaleValue"));

                        TotalAvg_Fat = Math.Round(TotalAvg_Fat / Count, 2);
                        TotalAvg_SNF = Math.Round(TotalAvg_SNF / Count, 2);
                        TotalAvg_CLR = Math.Round(TotalAvg_CLR / Count, 2);

                        sb.Append("<tr>");
                        sb.Append("<td colspan='5'><b>कैश अनुमान टोटल</b></td>");
                        sb.Append("<td>" + TotalQuantity.ToString() + "</td>");
                        sb.Append("<td>" + TotalAvg_Fat.ToString() + "</td>");
                        sb.Append("<td>" + TotalAvg_SNF.ToString() + "</td>");
                        sb.Append("<td>" + TotalAvg_CLR.ToString() + "</td>");
                        sb.Append("<td>" + TotalPayableAmount.ToString() + "</td>");
                        sb.Append("<td>" + TotalEarningValue.ToString() + "</td>");
                        sb.Append("<td>" + TotalSaleValue.ToString() + "</td>");
                        sb.Append("<td>" + TotalAdjustAmount.ToString() + "</td>");
                        sb.Append("<td>" + (TotalPayableAmount + TotalEarningValue - TotalSaleValue - TotalAdjustAmount) + "</td>");
                        //sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("</tr>");

                        sb.Append("</table>");





                        divprint.InnerHtml = sb.ToString();

                    }

                }



            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
}