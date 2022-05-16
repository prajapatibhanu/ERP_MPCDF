using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public partial class mis_Mis_Report : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1 = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Mis_ReportID"] = "0";
                GetOfficeDetails();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void GetOfficeDetails()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                ViewState["Office_Name"] = ds1.Tables[0].Rows[0]["Office_Name"].ToString();
                ViewState["Office_ContactNo"] = ds1.Tables[0].Rows[0]["Office_ContactNo"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    
    protected void ClearText()
    {
        //// Start Total Milk Collection
        txtFromDate.Text = "";
        txtCurrentTGT.Text = "";
        txtOnReportingDate.Text = "";
        txtFat.Text = "";
        txtSNF.Text = "";
        txtMonthlyAverageTillDate.Text = "";
        txtLastYearSameMonth.Text = "";
        txtLastMonth_Provisional.Text = "";
        txtCummulativeTillDate_CurrentYear.Text = "";
        txtCummulativeTillDate_LastYear.Text = "";
        txtGrowthPercentage.Text = "";
        //// End Total Milk Collection

        //// Start  Total Milk Sale
        txtSaleCurrentMonthTarget.Text = "";
        txtSaleOnReportingDate.Text = "";
        txtSaleMonthlyAverageTillDate.Text = "";
        txtSaleLastYearSameMonth.Text = "";
        txtSaleLastMonth_Provisional.Text = "";
        txtSaleCummulativeTillDate_CurrentYear.Text = "";
        txtSaleCummulativeTillDate_LastYear.Text = "";
        txtSaleGrowth.Text = "";
        //// End  Total Milk Sale

        //// Milk Collection Price
        txtMCPriceBuffaloMilk.Text = "";
        txtMCPriceLastYearSameMonthBuffalo.Text = "";
        txtMCPriceCowMilk.Text = "";
        txtMCPriceLastYearSameMonthCow.Text = "";
        //// Milk Collection Price


        /// Start Payment Made Upto
        txtPaymentMadeUpto.Text = "";
        /// End Payment Made Upto

        txtS_PriceFullCreamMilk.Text = "";
        txtS_PriceLastYearSameMonthFullCreamMilk.Text = "";
        txtS_PriceStandardMilk.Text = "";
        txtS_PriceLastYearSameMonthStandardMilk.Text = "";
        txtS_PriceTonnedMilk.Text = "";
        txtS_PriceLastYearSameMonthTonnedMilk.Text = "";
        txtS_PriceDoubleTonnedMilk.Text = "";
        txtS_PriceLastYearSameMonthTonnedDoubleTonnedMilk.Text = "";
        txtS_PriceSkimMilk.Text = "";
        txtS_PriceLastYearSameMonthSkimMilk.Text = "";
        txtS_PriceChahMilk.Text = "";
        txtS_PriceLastYearSameMonthChahMilk.Text = "";
        txtS_PriceTeaSpecail.Text = "";
        txtS_PriceCowMilk.Text = "";


        txtProductSale_Ghee.Text = "";
        txtProductSale_SMP_WMP.Text = "";
        txtProductSale_WhiteButter.Text = "";
        txtProductSale_TableButter.Text = "";
        txtProductSale_SweetSMP.Text = "";
        txtProductSale_ShriKhand.Text = "";

        txtProductSale_Paneer.Text = "";
        txtProductSale_FlavouredMilk.Text = "";
        txtProductSale_ButterMilk.Text = "";
        txtProductSale_Lassi.Text = "";
        txtProductSale_Peda.Text = "";
        txtProductSale_SweetCurd.Text = "";
        txtProductSale_PlainCurd.Text = "";
        txtProductSale_ProbioticCurd.Text = "";
        txtProductSale_ChhenaKheer_Rabadi.Text = "";
        txtProductSale_Khowa_Mawa.Text = "";
        txtProductSale_Rasgulla.Text = "";
        txtProductSale_GulabJamun.Text = "";
        txtProductSale_MilkCake.Text = "";
        txtProductSale_Chakka.Text = "";
        txtProductSale_Thandai.Text = "";
        txtProductSale_F_MilkBottle.Text = "";
        txtProductSale_LassiLite.Text = "";
        txtProductSale_NariyalBarfi.Text = "";
        txtProductSale_GulabJamunMix.Text = "";
        txtProductSale_PaneerAchaar.Text = "";
        txtProductSale_CoffeeMixPowder.Text = "";
        txtProductSale_CookingButter.Text = "";
        txtProductSale_LowFatPaneer.Text = "";
        txtProductSale_PedaPrasad.Text = "";
        txtProductSale_SanchiIceCream.Text = "";
        txtProductSale_SanchiGoldenMilk.Text = "";
        txtClosingSMP.Text = "";
        txtClosingWhiteButter.Text = "";
        txtClosingGhee.Text = "";
        txtCommoditySMPToday.Text = "";
        txtCommoditySMPCummulative.Text = "";
        txtCommodityWhiteButterTo.Text = "";
        txtCommodityWhiteButterCU.Text = "";
        txtMilkUsedForIndigenousProduct.Text = "";
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
           
            txtGrowthPercentage.Text = Convert.ToString(hdnTotalCollection.Value);
            txtSaleGrowth.Text = Convert.ToString(hdnSaleGrowth.Value); 
            lblMsg.Text = "";
            string msg = "";
            string FromDate = "";
            string PaymentMadeUpto = "";
            if (txtFromDate.Text == "")
            {
                msg += "Enter Date. \\n";
            }
            else
            {
                FromDate = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            }

            if (txtPaymentMadeUpto.Text == "")
            {
                
            }
            else
            {
                PaymentMadeUpto = Convert.ToDateTime(txtPaymentMadeUpto.Text, cult).ToString("yyyy/MM/dd");
            }
            if (msg.Trim() == "")
            {

                int Status = 0;
                ds = objdb.ByProcedure("sp_Mis_ReportDaily",
                    new string[] { "flag", "Tmc_FromDate", "OfficeID", "Mis_ReportID" },
                    new string[] { "3", FromDate, objdb.Office_ID(), ViewState["Mis_ReportID"].ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (btnSave.Text == "Save" && ViewState["Mis_ReportID"].ToString() == "0" && Status == 0)
                {

                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    string IPadd2 = Request.UserHostAddress;
                    objdb.ByProcedure("sp_Mis_ReportDaily",
                    new string[] { "flag", "OfficeID", "OfficeName", "Tmc_FromDate", "Tmc_CurrentTGT", "Tmc_OnReportingDate", "Tmc_Fat", "Tmc_SNF", "Tmc_MonthlyAverageTillDate", "Tmc_LastYearSameMonth", "Tmc_LastMonth_Provisional", "Tmc_CummulativeTillDate_CurrentYear", "Tmc_CummulativeTillDate_LastYear", "Tmc_GrowthPercentage", "Tms_CurrentMonthTarget", "Tms_OnReportingDate", "Tms_MonthlyAverageTillDate", "Tms_LastYearSameMonth", "Tms_LastMonth_Provisional", "Tms_CummulativeTillDate_CurrentYear", "Tms_CummulativeTillDate_LastYear", "Tms_GrowthPercentage", "Mcp_BuffaloMilk", "Mcp_LastYearSameMonthBuffalo", "Mcp_CowMilk", "Mcp_LastYearSameMonthCow", "PaymentMadeUpto", "Sp_FullCreamMilk", "Sp_LastYearSameMonthFullCreamMilk", "Sp_StandardMilk", "Sp_LastYearSameMonthStandardMilk", "Sp_TonnedMilk", "Sp_LastYearSameMonthTonnedMilk", "Sp_DoubleTonnedMilk", "Sp_LastYearSameMonthTonnedDoubleTonnedMilk", "Sp_SkimMilk", "Sp_LastYearSameMonthSkimMilk", "Sp_ChahMilk", "Sp_LastYearSameMonthChahMilk", "Sp_TeaSpecail", "Sp_CowMilk", "Ps_Ghee", "Ps_SMP_WMP", "Ps_WhiteButter", "Ps_TableButter", "Ps_SweetSMP", "Ps_ShriKhand", "Ps_Paneer", "Ps_FlavouredMilk", "Ps_ButterMilk", "Ps_Lassi", "Ps_Peda", "Ps_SweetCurd", "Ps_PlainCurd", "Ps_ProbioticCurd", "Ps_ChhenaKheer_Rabadi", "Ps_Khowa_Mawa", "Ps_Rasgulla", "Ps_GulabJamun", "Ps_MilkCake", "Ps_Chakka", "Ps_Thandai", "Ps_F_MilkBottle", "Ps_LassiLite", "Ps_NariyalBarfi", "Ps_GulabJamunMix", "Ps_PaneerAchaar", "Ps_CoffeeMixPowder", "Ps_CookingButter", "Ps_LowFatPaneer", "Ps_PedaPrasad", "Ps_SanchiIceCream", "Ps_SanchiGoldenMilk", "Cs_ClosingSMP", "Cs_ClosingWhiteButter", "Cs_ClosingGhee", "Cu_CommoditySMPToday", "Cu_CommoditySMPCummulative", "Cu_CommodityWhiteButterTo", "Cu_CommodityWhiteButterCU", "MilkUsedForIndigenousProduct", "CreatedBy", "CreatedByIP" },
                    new string[] { "1", objdb.Office_ID(), ViewState["Office_Name"].ToString(), FromDate, txtCurrentTGT.Text.Trim(), txtOnReportingDate.Text.Trim(), txtFat.Text.Trim(), txtSNF.Text.Trim(), txtMonthlyAverageTillDate.Text.Trim(), txtLastYearSameMonth.Text.Trim(), txtLastMonth_Provisional.Text.Trim(), txtCummulativeTillDate_CurrentYear.Text.Trim(), txtCummulativeTillDate_LastYear.Text.Trim(), txtGrowthPercentage.Text.Trim(), txtSaleCurrentMonthTarget.Text.Trim(), txtSaleOnReportingDate.Text.Trim(), txtSaleMonthlyAverageTillDate.Text.Trim(), txtSaleLastYearSameMonth.Text.Trim(), txtSaleLastMonth_Provisional.Text.Trim(), txtSaleCummulativeTillDate_CurrentYear.Text.Trim(), txtSaleCummulativeTillDate_LastYear.Text.Trim(), txtSaleGrowth.Text.Trim(), txtMCPriceBuffaloMilk.Text.Trim(), txtMCPriceLastYearSameMonthBuffalo.Text.Trim(), txtMCPriceCowMilk.Text.Trim(), txtMCPriceLastYearSameMonthCow.Text.Trim(), PaymentMadeUpto, txtS_PriceFullCreamMilk.Text.Trim(), txtS_PriceLastYearSameMonthFullCreamMilk.Text.Trim(), txtS_PriceStandardMilk.Text.Trim(), txtS_PriceLastYearSameMonthStandardMilk.Text.Trim(), txtS_PriceTonnedMilk.Text.Trim(), txtS_PriceLastYearSameMonthTonnedMilk.Text.Trim(), txtS_PriceDoubleTonnedMilk.Text.Trim(), txtS_PriceLastYearSameMonthTonnedDoubleTonnedMilk.Text.Trim(), txtS_PriceSkimMilk.Text.Trim(), txtS_PriceLastYearSameMonthSkimMilk.Text.Trim(), txtS_PriceChahMilk.Text.Trim(), txtS_PriceLastYearSameMonthChahMilk.Text.Trim(), txtS_PriceTeaSpecail.Text.Trim(), txtS_PriceCowMilk.Text.Trim(), txtProductSale_Ghee.Text.Trim(), txtProductSale_SMP_WMP.Text.Trim(), txtProductSale_WhiteButter.Text.Trim(), txtProductSale_TableButter.Text.Trim(), txtProductSale_SweetSMP.Text.Trim(), txtProductSale_ShriKhand.Text.Trim(), txtProductSale_Paneer.Text.Trim(), txtProductSale_FlavouredMilk.Text.Trim(), txtProductSale_ButterMilk.Text.Trim(), txtProductSale_Lassi.Text.Trim(), txtProductSale_Peda.Text.Trim(), txtProductSale_SweetCurd.Text.Trim(), txtProductSale_PlainCurd.Text.Trim(), txtProductSale_ProbioticCurd.Text.Trim(), txtProductSale_ChhenaKheer_Rabadi.Text.Trim(), txtProductSale_Khowa_Mawa.Text.Trim(), txtProductSale_Rasgulla.Text.Trim(), txtProductSale_GulabJamun.Text.Trim(), txtProductSale_MilkCake.Text.Trim(), txtProductSale_Chakka.Text.Trim(), txtProductSale_Thandai.Text.Trim(), txtProductSale_F_MilkBottle.Text.Trim(), txtProductSale_LassiLite.Text.Trim(), txtProductSale_NariyalBarfi.Text.Trim(), txtProductSale_GulabJamunMix.Text.Trim(), txtProductSale_PaneerAchaar.Text.Trim(), txtProductSale_CoffeeMixPowder.Text.Trim(), txtProductSale_CookingButter.Text.Trim(), txtProductSale_LowFatPaneer.Text.Trim(), txtProductSale_PedaPrasad.Text.Trim(), txtProductSale_SanchiIceCream.Text.Trim(), txtProductSale_SanchiGoldenMilk.Text.Trim(), txtClosingSMP.Text.Trim(), txtClosingWhiteButter.Text.Trim(), txtClosingGhee.Text.Trim(), txtCommoditySMPToday.Text.Trim(), txtCommoditySMPCummulative.Text.Trim(), txtCommodityWhiteButterTo.Text.Trim(), txtCommodityWhiteButterCU.Text.Trim(), txtMilkUsedForIndigenousProduct.Text.Trim(), ViewState["Emp_ID"].ToString(), IPAddress }, "dataset");

                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    ClearText();
                }
                    
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Date is already exist.');", true);
                }
                 Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}