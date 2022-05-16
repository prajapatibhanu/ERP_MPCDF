using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Payroll_PayrollFYSalaryLedger : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Emp_ID"] != null)
            {

                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillDropdown();

            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
    }
    protected void FillDropdown()
    {
        try
        {
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = ds;
                ddlYear.DataTextField = "Financial_Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            }
            ds = null;
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag" }, new string[] { "9" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataTextField = "Office_Name";
                ddlOfficeName.DataValueField = "Office_ID";
                ddlOfficeName.DataBind();
                ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
            }
            ddlOfficeName.SelectedValue = ViewState["Office_ID"].ToString();
            ds = null;
            ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "11", ddlOfficeName.SelectedValue.ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlEmployee.DataSource = ds;
                ddlEmployee.DataTextField = "Emp_Name";
                ddlEmployee.DataValueField = "Emp_ID";
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            string selectedEmpDetail = "Salary Ledger for Financial Year  " + ddlYear.SelectedItem.Text + " , Officer/ Employee Name: " + ddlEmployee.SelectedItem.Text+"  , Office: "+ddlOfficeName.SelectedItem.Text;
            lblEmpDetail.Text=selectedEmpDetail.ToString();
            ds = objdb.ByProcedure("SpPayrollEarnDeductionDetail_FY", new string[] { "flag", "Year", "Emp_ID" }, new string[] { "4", ddlYear.SelectedValue.ToString(), ddlEmployee.SelectedValue.ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                DataTable edd = new DataTable();
                edd = ds.Tables[0].DefaultView.ToTable();

                DataRow[] drb = edd.Select("EarnDeduction_Name ='" + "BASIC SALARY" + "'");
                if (drb.Length != 0)
                {
                    DataRow newRow = edd.NewRow();
                    // We "clone" the row
                    newRow.ItemArray = drb[0].ItemArray;
                    // We remove the old and insert the new
                    edd.Rows.Remove(drb[0]);
                    edd.Rows.InsertAt(newRow, 0);
                }
                DataRow[] drd = edd.Select("EarnDeduction_Name ='" + "DEARNESS ALLOWANCE" + "'");
                if (drd.Length != 0)
                {
                    DataRow newRow = edd.NewRow();
                    // We "clone" the row
                    newRow.ItemArray = drd[0].ItemArray;
                    // We remove the old and insert the new
                    edd.Rows.Remove(drd[0]);
                    edd.Rows.InsertAt(newRow, 1);
                }
                DataRow[] dr = edd.Select("EarnDeduction_Name ='" + "TOTAL EARNING" + "'");
                if (dr.Length != 0)
                {
                    DataRow newRow = edd.NewRow();
                    // We "clone" the row
                    newRow.ItemArray = dr[0].ItemArray;
                    // We remove the old and insert the new
                    edd.Rows.Remove(dr[0]);
                    edd.Rows.InsertAt(newRow, edd.Rows.Count+1);
                }
                DataRow[] dr1 = edd.Select("EarnDeduction_Name ='" + "TOTAL DEDUCTION" + "'");
                if (dr1.Length != 0)
                {
                    DataRow newRow = edd.NewRow();
                    // We "clone" the row
                    newRow.ItemArray = dr1[0].ItemArray;
                    // We remove the old and insert the new
                    edd.Rows.Remove(dr1[0]);
                    edd.Rows.InsertAt(newRow, edd.Rows.Count + 1);
                }
                DataRow[] dr2 = edd.Select("EarnDeduction_Name ='" + "NET SALARY" + "'");
                if (dr1.Length != 0)
                {
                    DataRow newRow = edd.NewRow();
                    // We "clone" the row
                    newRow.ItemArray = dr2[0].ItemArray;
                    // We remove the old and insert the new
                    edd.Rows.Remove(dr2[0]);
                    edd.Rows.InsertAt(newRow, edd.Rows.Count + 1);
                }
                DataSet ds222 = new DataSet();
                ds222.Merge(edd);

                GetDetails(ds222);
                //GridView1.DataSource = ds;
                //GridView1.DataBind();
                //GridView1.UseAccessibleHeader = true;
                //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
              
            }
        }
        catch (Exception ex)
        {
           
        }

    }
    private void GetDetails(DataSet ds2)
    {
        try
        {
                StringBuilder sb1 = new StringBuilder();
                int Count1 = ds2.Tables[0].Rows.Count;
                int Count0 = ds2.Tables[0].Columns.Count;
                btnPrint.Visible = true;
                btnExcel.Visible = true;
                sb1.Append("<table class='table1' style='width:100%;'>");
                sb1.Append("<thead>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center;' colspan='26'><b>" + ddlOfficeName.SelectedItem.Text + "<b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center;' colspan='26'><Officer/ Employee Name </b>" + ddlEmployee.SelectedItem.Text + "</td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border: 1px solid #000000;text-align:center;' colspan='26'><b>Salary Statement for the Financial Year " + ddlYear.SelectedItem.Text + "</b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Head</b></td>");
                //sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Earning & Deduction</b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Total In FY</b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Apr </b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Apr (Arr)</b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>May </b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>May (Arr)</b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Jun </b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Jun (Arr)</b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Jul </b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Jul (Arr)</b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Aug </b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Aug (Arr)</b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Sep In FY</b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Sep (Arr)</b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Oct </b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Oct (Arr)</b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Nov </b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Nov (Arr)</b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Dec </b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Dec (Arr)</b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Jan </b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Jan (Arr)</b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Feb </b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Feb (Arr)</b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Mar </b></td>");
                sb1.Append("<td style='border:1px solid black;text-align:center;'><b>Mar (Arr)</b></td>");
                sb1.Append("</tr>");
                sb1.Append("</thead>");
              
                decimal inkgs = 0;
                for (int i = 0; i < Count1; i++)
                {
                        sb1.Append("<tr>");
                        sb1.Append("<td style='border:1px solid black;text-align:left;'><b>" + ds2.Tables[0].Rows[i]["EarnDeduction_Name"] + "</b></td>");
                        //sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["EarnDeduction_Type"] + "</td>");
                        sb1.Append("<td style='border:1px solid black;text-align:left;'><b>" + ds2.Tables[0].Rows[i]["Total"] + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["April"] + "</td>");
                        sb1.Append("<td style='border:1px solid black;text-align:left;'><b>" + ds2.Tables[0].Rows[i]["AprilArr"] + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["May"] + "</td>");
                        sb1.Append("<td style='border:1px solid black;text-align:left;'><b>" + ds2.Tables[0].Rows[i]["MayArr"] + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["June"] + "</td>");
                        sb1.Append("<td style='border:1px solid black;text-align:left;'><b>" + ds2.Tables[0].Rows[i]["JuneArr"] + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["July"] + "</td>");
                        sb1.Append("<td style='border:1px solid black;text-align:left;'><b>" + ds2.Tables[0].Rows[i]["JulyArr"] + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["August"] + "</td>");
                        sb1.Append("<td style='border:1px solid black;text-align:left;'><b>" + ds2.Tables[0].Rows[i]["AugustArr"] + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["September"] + "</td>");
                        sb1.Append("<td style='border:1px solid black;text-align:left;'><b>" + ds2.Tables[0].Rows[i]["SeptemberArr"] + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["October"] + "</td>");
                        sb1.Append("<td style='border:1px solid black;text-align:left;'><b>" + ds2.Tables[0].Rows[i]["OctoberArr"] + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["November"] + "</td>");
                        sb1.Append("<td style='border:1px solid black;text-align:left;'><b>" + ds2.Tables[0].Rows[i]["NovemberArr"] + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["December"] + "</td>");
                        sb1.Append("<td style='border:1px solid black;text-align:left;'><b>" + ds2.Tables[0].Rows[i]["DecemberArr"] + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["January"] + "</td>");
                        sb1.Append("<td style='border:1px solid black;text-align:left;'><b>" + ds2.Tables[0].Rows[i]["JanuaryArr"] + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["February"] + "</td>");
                        sb1.Append("<td style='border:1px solid black;text-align:left;'><b>" + ds2.Tables[0].Rows[i]["FebruaryArr"] + "</b></td>");
                        sb1.Append("<td style='border:1px solid black;text-align:center;'>" + ds2.Tables[0].Rows[i]["March"] + "</td>");
                        sb1.Append("<td style='border:1px solid black;text-align:left;'><b>" + ds2.Tables[0].Rows[i]["MarchArr"] + "</b></td>");
                         sb1.Append("</tr>");

                       // inkgs += Convert.ToDecimal(ds2.Tables[0].Rows[i]["QuantityInKG"]);
                }
                //sb1.Append("<tr>");
                //sb1.Append("<td style='border:1px solid black;text-align:right;' colspan='2'><b>Total<b></td>");
                //sb1.Append("<td style='border:1px solid black;text-align:center;'><b>" + inkgs.ToString("0.00") + "</b></td>");
                //sb1.Append("</tr>");
                sb1.Append("</table>");
                div_page_content.InnerHtml = sb1.ToString();
                Print.InnerHtml = sb1.ToString();
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + ddlEmployee.SelectedItem.Text + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            div_page_content.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);
    }
}