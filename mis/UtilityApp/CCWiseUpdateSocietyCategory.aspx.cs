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

public partial class mis_UtilityApp_CCWiseUpdateSocietyCategory : System.Web.UI.Page
{
    DataSet dsreturn, ds, ds_producer, ds_upload = new DataSet();
    AbstApiDBApi objdb = new APIProcedure();
    APIProcedure objdb2 = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
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

                   

                    ds = objdb.ByProcedure("SpAdminOffice",
                            new string[] { "flag", "OfficeType_ID", "Office_ID" },
                            new string[] { "41", objdb2.OfficeType_ID(), objdb2.Office_ID() }, "dataset");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        if (objdb2.OfficeType_ID() == "2")
                        {
                            ddlOffice.DataSource = ds;
                            ddlOffice.DataTextField = "Office_Name";
                            ddlOffice.DataValueField = "Office_ID";
                            ddlOffice.DataBind();
                            ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
                            ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                        }
                        else
                        {
                            ddlOffice.DataSource = ds;
                            ddlOffice.DataTextField = "Office_Name";
                            ddlOffice.DataValueField = "Office_ID";
                            ddlOffice.DataBind();

                        }

                    }
                    GetCCDetails();

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
    protected void GetCCDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                     new string[] { "flag", "I_OfficeID", "I_OfficeTypeID" },
                     new string[] { "3", objdb2.Office_ID(), objdb2.OfficeType_ID() }, "dataset");

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

                        if (objdb2.OfficeType_ID() == "2")
                        {
                            ddlccbmcdetail.Items.Insert(0, new ListItem("Select", "0"));
                        }
                        else
                        {
                            ddlccbmcdetail.SelectedValue = objdb2.Office_ID();
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
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string Extension = Path.GetExtension(FileUpload1.FileName).ToLower();
            //string path = Server.MapPath("MyFolder/" + Guid.NewGuid() + "-" + FileName);
            string path = Server.MapPath("~/mis/UtilityApp/MyFolder/" + FileName);
            FileUpload1.SaveAs(path);


            if (Extension == ".xls" || Extension == ".xlsx")
            {

                Import_To_GridPrompt(path, Extension, "Yes");
                
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Invalid File ", "Please convert file into .xls or .xlsx");
            }

        }
    }
    
    
    private void Import_To_GridPrompt(string FilePath, string Extension, string isHDR)
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
        string SocietyCategory = "1";
        StringDiv.InnerHtml = "";
        try
        {
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            //cmdExcel.CommandText = "SELECT * From [" + SheetName + "] order by 1 asc, 2 desc";
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "] WHERE [M_SOC_CD] IS NOT NULL";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            /**********************/
            StringBuilder htmlStr = new StringBuilder();
            htmlStr.Append("");

            if (dt.Rows.Count > 0)
            {
                int Count = dt.Rows.Count;

                //if (rbSheetType.SelectedItem.Text == "Milk Collection")
                //{

                htmlStr.Append("<table class='main-heading-print' id='SalaryTable'>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<th>M_SNo</th>");
                htmlStr.Append("<th>M_SOC_CD</th>");
                htmlStr.Append("<th>M_CATG</th>");
               
               

                htmlStr.Append("</tr>");

                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<tr class='center_align'>");
                    htmlStr.Append("<tr class='center_align'>");
                    htmlStr.Append("<td class='left_align'>" + (i + 1).ToString() + "</td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["M_SOC_CD"].ToString() + " </ td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["M_CATG"].ToString() + " </ td>");
                   
                    
                    ds_producer = objdb.ByProcedure("SpAdminOffice",
                    new string[] { "flag", "Office_Code", "Office_Parant_ID", "Category" },
                    new string[] { "55", dt.Rows[i]["M_SOC_CD"].ToString(), objdb2.Office_ID(), dt.Rows[i]["M_CATG"].ToString()}, "dataset");
                    if (ds_producer.Tables.Count > 0 && ds_producer.Tables[0].Rows.Count > 0)
                    {

                    
                       htmlStr.Append("<td class='left_align'>"+ds_producer.Tables[0].Rows[0]["ErrorMsg"].ToString()+"</td>");
                       
                    }
                    else
                    {

                       

                    }


                   
                    htmlStr.Append("</tr>");

                }



            }
            else
            {
                htmlStr.Append("<div><p>Invalid File Format, Unable to upload it. <br/> Please recheck the file and upload again.</p></div>");
            }

            StringDiv.InnerHtml = htmlStr.ToString();
            // Response.Clear();
            // Response.AddHeader("content-disposition", "attachment;filename=" + "CCWiseOfficeDetail" + DateTime.Now + ".xls");
            // Response.Charset = "";
            // Response.Cache.SetCacheability(HttpCacheability.NoCache);
            // Response.ContentType = "application/vnd.xls";
            // System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            // System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            // StringDiv.RenderControl(htmlWrite);

            // Response.Write(stringWrite.ToString());

            // Response.End();
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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=" + "CCWiseOfficeDetail" + DateTime.Now + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        StringDiv.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();
    }
   
}