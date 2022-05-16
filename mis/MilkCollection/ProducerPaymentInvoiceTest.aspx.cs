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


public partial class mis_MilkCollection_ProducerPaymentInvoiceTest : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    string Fdate = "";
    string Tdate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
        {

            if (!IsPostBack)
            {
                lblMsg.Text = "";

                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtSociatyName.Text = Session["Office_Name"].ToString();
                lblOfficeName.Text = Session["Office_Name"].ToString();
                //lblOfficeCode.Text = Session["OfficeCode"].ToString();

                //FillSociety();
                txtFdt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                txtTdt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                lblFromDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                lblToDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");

            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
            //objdb.redirectToHome();
        }
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

            // divIteminfo.Visible = false;
            //FillPrint();
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
                        new string[] { "16", Fdate, Tdate, ViewState["Office_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                lblFromDate.Text = txtFdt.Text;
                lblToDate.Text = txtTdt.Text;
                lblPrintDate.Text = CDate;
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblOfficeName.Text = txtSociatyName.Text;

                        int Count = ds.Tables[0].Rows.Count;
                        Repeater1.DataSource = ds.Tables[0];
                        Repeater1.DataBind();
                        decimal TotalAvg_Fat = 0;
                        decimal TotalAvg_SNF = 0;
                        decimal TotalAvg_CLR = 0;
                        decimal TotalQuantity = 0;
                        decimal TotalPayableAmount = 0;
                        decimal TotalAdjustAmount = 0;
                        decimal TotalSaleValue = 0;
                        decimal TotalEarningValue = 0;
                        TotalAvg_Fat = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Avg_Fat"));
                        TotalAvg_SNF = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Avg_SNF"));
                        TotalAvg_CLR = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Avg_CLR"));
                        TotalQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalLtr_MilkQty"));
                        TotalPayableAmount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("MilkValue"));
                        TotalAdjustAmount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("AdjustAmount"));
                        TotalSaleValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("SaleValue"));

                        TotalEarningValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("EarningValue"));
                        TotalAvg_Fat = Math.Round(TotalAvg_Fat / Count, 2);
                        TotalAvg_SNF = Math.Round(TotalAvg_SNF / Count, 2);
                        TotalAvg_CLR = Math.Round(TotalAvg_CLR / Count, 2);


                        Label1.Text = TotalQuantity.ToString();
                        Label2.Text = TotalAvg_Fat.ToString();
                        Label3.Text = TotalAvg_SNF.ToString();
                        Label4.Text = TotalAvg_CLR.ToString();
                        Label5.Text = TotalPayableAmount.ToString();
                        Label6.Text = TotalEarningValue.ToString();
                        Label7.Text = TotalSaleValue.ToString();
                        Label8.Text = TotalAdjustAmount.ToString();
                        Label9.Text = (TotalPayableAmount + TotalEarningValue - TotalSaleValue - TotalAdjustAmount).ToString();


                    }
                    else
                    {

                        // divIteminfo.Visible = false;
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Sorry!", "No Record Found");

                    }
                }

                else
                {

                    //divIteminfo.Visible = false;
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Sorry!", "No Record Found");

                }

            }
            else
            {

                // divIteminfo.Visible = false;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Sorry!", "No Record Found");
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

}