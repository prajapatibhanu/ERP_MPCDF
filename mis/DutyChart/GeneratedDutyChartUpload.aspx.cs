using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using System.Text;
using System.Globalization;
using System.Data.Odbc;

public partial class mis_DutyChart_GeneratedDutyChartUpload : System.Web.UI.Page
{
    DataSet dsreturn, ds = new DataSet();
    AbstApiDBApi objdb = new APIProcedure();
    APIProcedure objdbAPI = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    MilkCalculationClass Obj_MC = new MilkCalculationClass();
    string status = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    if (ViewState["Office_ID"].ToString() == "2206")
                    {
                        lblHeading.Text = "दुग्ध शीत केंद्र बैतूल और मुलताई बी.एम.सी";
                    }
                    else
                    {
                        lblHeading.Text = Session["Office_Name"].ToString();
                    }
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        status = status + " // button clicked";
        if (FileUpload1.HasFile)
        {
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string Extension = Path.GetExtension(FileUpload1.FileName).ToLower();
            string path = Server.MapPath("~/mis/DutyChart/DutyChartUpload/" + FileName);
            FileUpload1.SaveAs(path);


            if (Extension == ".xls" || Extension == ".xlsx")
            {
                Import_To_Grid(path, Extension, "Yes");
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Invalid File ", "Please convert file into .xls");
            }

        }
    }
    private void Import_To_Grid(string FilePath, string Extension, string isHDR)
    {
        string conStr = "";
        lblMsg.Text = "";
        conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        conStr = String.Format(conStr, FilePath, isHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;
        string SheetName = "";
        try
        {
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "] order by 1 asc, 2 desc";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            if (dt.Rows.Count > 0)
            {
                objdb.ByProcedure("Sp_tblDutyChartMapping", new string[] { "flag", "OfficeId", "Year", "Month" },
                        new string[] { "5", ViewState["Office_ID"].ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString() }, "dataset");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string RootName = dt.Rows[i]["Root Name"].ToString();
                    string TankerNo = dt.Rows[i]["Vehicle No"].ToString();
                    string VehicleCapacity = dt.Rows[i]["Vehicle Capacity"].ToString();
                    string DriverName = dt.Rows[i]["Driver Name"].ToString();
                    string TesterName = dt.Rows[i]["Tester Name"].ToString();
                    string Attendant = dt.Rows[i]["Attendant"].ToString();

                    objdb.ByProcedure("Sp_tblDutyChartMapping", new string[] { "flag", "OfficeId", "Year", "Month", "RouteName", "TankerNo", "VehicleCapacity", "DriverName", "TesterName", "CleanerName", "CreatedBy" },
                        new string[] { "3", ViewState["Office_ID"].ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), RootName, TankerNo, VehicleCapacity, DriverName, TesterName, Attendant, ViewState["Emp_ID"].ToString() }, "dataset");
                }
                FillData();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Invalid Columns Names ", ex.Message.ToString());
        }
        finally
        {
            connExcel.Close();
        }
    }
    protected void FillData()
    {
        try
        {
            GvDetail.DataSource = null;
            GvDetail.DataBind();
            DivData.Visible = false;
            if (ddlYear.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("Sp_tblDutyChartMapping", new string[] { "flag", "OfficeId", "Year", "Month" }, new string[] { "4", ViewState["Office_ID"].ToString(), ddlYear.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    lblMonth.InnerText = ddlMonth.SelectedItem.Text;
                    lblMonth2.InnerText = ddlMonth.SelectedItem.Text;
                    GvDetail.DataSource = ds;
                    GvDetail.DataBind();
                    lblDate.InnerHtml = ds.Tables[0].Rows[0]["CreatedOn"].ToString();
                    DivData.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Invalid Columns Names ", ex.Message.ToString());
        }
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillData();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillData();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}