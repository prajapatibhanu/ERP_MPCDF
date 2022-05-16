using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class mis_Dashboard_MonitoringDashboard : System.Web.UI.Page
{
    APIProcedure objapi = new APIProcedure();
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        DashBoardRptBYDate();
    }
    private void DashBoardRptBYDate()
    {
        ds = new DataSet();
        ds = objapi.ByProcedure("TRN_Monioring_Report_By_Date", new string[] { "flag", "OfficeID", "CurnDate" }, new string[] { "0", objapi.Office_ID(), txtOrderDate.Text }, "dataset");

        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblchallanentrydate.Text = Convert.ToString(ds.Tables[0].Rows[0]["Echallandate"]);
                lblchallanentryRecord.Text = Convert.ToString(ds.Tables[0].Rows[0]["EchallanCount"]);
                lblRMRDDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["RMRDDate"]);
                lblRMRDRecord.Text = Convert.ToString(ds.Tables[0].Rows[0]["RMRDCount"]);
                lblmarketingentry.Text = Convert.ToString(ds.Tables[0].Rows[0]["DemandDate"]);
                lblDemand.Text = Convert.ToString(ds.Tables[0].Rows[0]["SupplyTotalQty"]);
                lblDemandltr.Text = Convert.ToString(ds.Tables[0].Rows[0]["SupplyTotalQtyLtr"]);
                lblPlantDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["PlantDate"]);
                lblPlantRecord.Text = Convert.ToString(ds.Tables[0].Rows[0]["PlantCount"]);
                lblFat.Text = Convert.ToString(ds.Tables[0].Rows[0]["FatCount"]);
                lblSNF.Text = Convert.ToString(ds.Tables[0].Rows[0]["SNFCount"]);
                lblQCDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["QCDate"]);
                lblQCRecord.Text = Convert.ToString(ds.Tables[0].Rows[0]["QCCount"]);
                lbFINANCEDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["VoucherDate"]);
                lbFINANCERecord.Text = Convert.ToString(ds.Tables[0].Rows[0]["VoucherCount"]);
                lblfiletrackingDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["FileDate"]);
                lblfiletrackingRecord.Text = Convert.ToString(ds.Tables[0].Rows[0]["FileCount"]);
                lblProductDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProductDemandDate"]);
                lblProductDemand.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProductDemandQty"]);
                lblProductSupply.Text = Convert.ToString(ds.Tables[0].Rows[0]["ProductSupplyQty"]);
                lblHRRecord.Text = Convert.ToString(ds.Tables[0].Rows[0]["EmpCount"]);
                lblPAYROLLDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["PayrolMonth"]);
                lblPayrolyear.Text = Convert.ToString(ds.Tables[0].Rows[0]["PayrolYear"]);
                lblPAYROLLrecord.Text = Convert.ToString(ds.Tables[0].Rows[0]["PayrolCount"]);
            }

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DashBoardRptBYDate();
    }
    protected void Linkchallan_Click(object sender, EventArgs e)
    {
        string orderdate = objapi.Encrypt(lblchallanentrydate.Text);
        Response.Redirect("/mis/Dashboard/EChallanDetailreport.aspx?Rid=" + orderdate);
    }
    protected void lnkQC_Click(object sender, EventArgs e)
    {
        string orderdate = objapi.Encrypt(lblQCDate.Text);
        Response.Redirect("/mis/Dashboard/QCDetailReports.aspx?Rid=" + orderdate);
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        string orderdate = objapi.Encrypt(lblfiletrackingDate.Text);
        Response.Redirect("/mis/Dashboard/FileTrackingDetailReport.aspx?Rid=" + orderdate);
    }
    protected void lnkVoucher_Click(object sender, EventArgs e)
    {
        string orderdate = objapi.Encrypt(lbFINANCEDate.Text);
        Response.Redirect("/mis/Dashboard/Voucher_Details_By_Date.aspx?Rid=" + orderdate);
    }
    protected void lnkEmp_Click(object sender, EventArgs e)
    {
        Response.Redirect("/mis/Dashboard/Emp_Details_Under_Office.aspx");
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {

        string Milkdate = objapi.Encrypt(lblmarketingentry.Text);
        string Productdate = objapi.Encrypt(lblProductDate.Text);

        Response.Redirect("/mis/Dashboard/MilkorProductDemand_DetailUnderOffice.aspx?Mid=" + Milkdate + "&PID=" + Productdate);
    }

    protected void lnkRMRD_Click(object sender, EventArgs e)
    {
        string orderdate = objapi.Encrypt(lblRMRDDate.Text);
        Response.Redirect("/mis/Dashboard/RMRD_Details_under_Office.aspx?Rid=" + orderdate);
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        string orderdate = objapi.Encrypt(lblPlantDate.Text);
        Response.Redirect("/mis/Dashboard/PlantSectionDetail.aspx?Rid=" + orderdate);
    }
    protected void lnkPayrol_Click(object sender, EventArgs e)
    {
        string orderdate = string.Empty;
        if (txtOrderDate.Text != string.Empty)
            orderdate = objapi.Encrypt(txtOrderDate.Text);
        Response.Redirect("/mis/Dashboard/Payroll_Detal_Employee.aspx?Rid=" + orderdate);
    }
}