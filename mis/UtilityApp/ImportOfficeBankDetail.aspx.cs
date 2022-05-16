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

public partial class mis_UtilityApp_ImportOfficeBankDetail : System.Web.UI.Page
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
        if (FileUpload1.HasFile)
        {
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string Extension = Path.GetExtension(FileUpload1.FileName).ToLower();
            //string path = Server.MapPath("MyFolder/" + Guid.NewGuid() + "-" + FileName);
            string path = Server.MapPath("MyFolder/milkcollection_file.xls");
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
        StringDiv.InnerHtml = "";
        try
        {
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            //cmdExcel.CommandText = "SELECT * From [" + SheetName + "] order by 1 asc, 2 desc";
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "] WHERE [M_OFC_CD] IS NOT NULL";
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
                htmlStr.Append("<th>M_OFC_CD</th>");
                htmlStr.Append("<th>M_BANK_AC</th>");
                htmlStr.Append("<th>M_IFSC_CD</th>");
                htmlStr.Append("<th>C_ACHOLDER</th>");
                htmlStr.Append("<th>Status</th>");

                htmlStr.Append("</tr>");

                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<tr class='center_align'>");
                    htmlStr.Append("<td class='left_align'>" + (i + 1).ToString() + "</td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["M_OFC_CD"].ToString() + " </ td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["M_BANK_AC"].ToString() + " </ td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["M_IFSC_CD"].ToString() + "</td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["C_ACHOLDER"].ToString() + "</td>");


                   


                    string ErrorSatus = "";
                    ds_producer = objdb.ByProcedure("SpAdminOffice",
       new string[] { "flag", "Office_Code", "Office_Parant_ID" },
       new string[] { "42", dt.Rows[i]["M_OFC_CD"].ToString(), objdb2.Office_ID() }, "dataset");
                    if (ds_producer.Tables.Count > 0 && ds_producer.Tables[0].Rows.Count > 0)
                    {

                        ds_upload = null;
                        ds_upload = objdb.ByProcedure("SpAdminOffice",
                        new string[] { "Flag"
                                                ,"Office_ID"
                                                ,"BankAccountNo"
                                                ,"IFSC" 
                                                ,"AccountHolderName"
                                                ,"CreatedBy"
                                                ,"CreatedBy_IP"                                              
                                            },
                        new string[] { "43"
                                                ,ds_producer.Tables[0].Rows[0]["Office_ID"].ToString()
                                                ,dt.Rows[i]["M_BANK_AC"].ToString()
                                                ,dt.Rows[i]["M_IFSC_CD"].ToString()
                                                ,dt.Rows[i]["C_ACHOLDER"].ToString()
                                                ,ViewState["Emp_ID"].ToString()
                                                ,objdb2.GetLocalIPAddress() 
                                                

                                            }, "dataset");

                        if (ds_upload.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            ErrorSatus = " <span style='color:green'> (Updated.) </span>";
                        }
                        else
                        {
                            ErrorSatus = "<span style='color:blue'> (Already Exist.) </span>";
                        }
                    }
                    else
                    {

                        ErrorSatus = "<span style='color:orange'> (Office Code Not Found) </span>";

                    }


                    string returnStatus = "Uploaded";
                    /**********************/
                    if (returnStatus.ToString() == "Uploaded")
                    {
                        htmlStr.Append("<td class='right_align' style='color:green; Padding:5px !important'> Read   " + ErrorSatus + " </td>");
                    }
                    else if (returnStatus.ToString() == "Updated")
                    {
                        htmlStr.Append("<td class='right_align' style='color:blue; Padding:5px !important'> Read   " + ErrorSatus + " </td>");
                    }
                    else
                    {
                        htmlStr.Append("<td class='right_align' style='color:red; Padding:5px !important'> Not Uploaded  " + ErrorSatus + " </td>");
                    }

                    htmlStr.Append("</tr>");

                }



            }
            else
            {
                htmlStr.Append("<div><p>Invalid File Format, Unable to upload it. <br/> Please recheck the file and upload again.</p></div>");
            }

            StringDiv.InnerHtml = htmlStr.ToString();

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
   
}