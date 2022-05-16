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

public partial class mis_UtilityApp_ImportChillingCostDbf : System.Web.UI.Page
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

                    if (ViewState["Office_ID"].ToString() == "1")
                    {
                        //ddlOffice.Enabled = true;
                    }
                     ds = objdb.ByProcedure("SpAdminOffice",
                           new string[] { "flag", "OfficeType_ID", "Office_ID" },
                           new string[] { "41", objdbAPI.OfficeType_ID(),objdbAPI.Office_ID() }, "dataset");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        
                        if (objdbAPI.OfficeType_ID() == "2")
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


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        status = status + " // button clicked";
        if (FileUpload1.HasFile)
        {
            
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
           

            string Extension = Path.GetExtension(FileUpload1.FileName).ToLower();
            //getting the path of the file   
           
            string path = Server.MapPath("~/mis/UtilityApp/MyFolder/" + FileName);
            //saving the file inside the MyFolder of the server  
            FileUpload1.SaveAs(path);


            if (Extension == ".xls" || Extension == ".xlsx")
            {
                
                Import_To_Grid2(path, Extension, "Yes");
              
            }
           
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Invalid File ", "Please convert file into .xls or .xlsx");
            }

        }
    }
   
    private void Import_To_Grid2(string FilePath, string Extension, string isHDR)
    {
        string conStr = "";
        lblMsg.Text = "";

        
        /*************/
       
        //string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directory.GetParent(FilePath).FullName + ";Extended Properties=dBASE IV;";
        conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        conStr = String.Format(conStr, FilePath, isHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        cmdExcel.Connection = connExcel;
        
        cmdExcel.Connection = connExcel;
        string SheetName = "";
        //GridView1.DataSource = null;
        //GridView1.DataBind();
        StringDiv.InnerHtml = "";
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
        /*************/
         /**********************/
            StringBuilder htmlStr = new StringBuilder();
            htmlStr.Append("");

            if (dt.Rows.Count > 0)
            {
                int Count = dt.Rows.Count;
                htmlStr.Append("<table class='main-heading-print' id='SalaryTable'>");
                htmlStr.Append("<tr class=''>");
                htmlStr.Append("<th class=''>SNo.</th>");               
                htmlStr.Append("<th>Society Code</th>");
                htmlStr.Append("<th>Date</th>");
                htmlStr.Append("<th>Temp</th>");
                htmlStr.Append("<th>Quantity</th>");
                htmlStr.Append("<th>Status</th>");
                htmlStr.Append("</tr>");

                for (int i = 0; i < Count; i++)
                {
			
					
                    htmlStr.Append("<tr class=''>");
                    htmlStr.Append("<td>" + (i + 1).ToString() + "</td>");
                    htmlStr.Append("<td>" + dt.Rows[i]["T_SOC_CD"].ToString() + " </ td>");
                    htmlStr.Append("<td>" + dt.Rows[i]["T_DATE"].ToString() + " </ td>");
                    htmlStr.Append("<td>" + dt.Rows[i]["T_TEMP"].ToString() + "</td>");
                    htmlStr.Append("<td>" + dt.Rows[i]["T_QTY"].ToString() + "</td>");
                    /**********************/
                    /// ------------Do Your Database Activity Here------------///  
                    string returnStatus = "Uploaded";
                    /***
					dsreturn = objdb.ByProcedure("USP_Trn_ExcelUploadTruck", new string[] { "flag", "UnionOffice_ID", "T_UN_CD", "T_SOC_CD", "T_DATE", "T_SHIFT", "T_BFCW_IND", "T_CATG", "T_QTY", "T_FAT", "T_SNF", "T_CLR", "UpdatedBy", "InsertedBy" }, new string[] { "0", ViewState["Office_ID"].ToString(), dt2.Rows[i]["t_un_cd"].ToString(), dt2.Rows[i]["t_soc_cd"].ToString(), Convert.ToDateTime(dt2.Rows[i]["t_date"].ToString(), cult).ToString("yyyy-MM-dd"), dt2.Rows[i]["t_shift"].ToString(), dt2.Rows[i]["t_bfcw_ind"].ToString(), dt2.Rows[i]["t_catg"].ToString(), dt2.Rows[i]["t_qty"].ToString(), dt2.Rows[i]["t_fat"].ToString(), dt2.Rows[i]["t_snf"].ToString(), dt2.Rows[i]["t_clr"].ToString(), ViewState["Emp_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
***/
                    /************************************/



                    string CreatedByIP = objdbAPI.GetLocalIPAddress();
                    string truckSheetEtryStatus = "";
					//Convert.ToDateTime(dt2.Rows[i]["t_date"].ToString(), cult).ToString("yyyy/MM/dd")
                    string iDate = dt.Rows[i]["T_DATE"].ToString();
					DateTime oDate = DateTime.Parse(iDate);

                    ds = objdb.ByProcedure("USP_Trn_ExcelUploadChillingCostBMCData", new string[] { "flag", "T_SOC_CD", "UnionOffice_ID", "supplyUnit" }, new string[] { "2", dt.Rows[i]["T_SOC_CD"].ToString(), ddlOffice.SelectedValue.ToString(), ddlccbmcdetail.SelectedValue }, "dataset");

                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        //string cc_id = ds.Tables[0].Rows[0]["cc_id"].ToString();
                        string soc_id = ds.Tables[0].Rows[0]["soc_id"].ToString();
                        //if (cc_id.Trim() != "0" && soc_id.Trim() != "0")
					    if (soc_id.Trim() != "0")
                        {

                            if (dt.Rows[i]["T_QTY"].ToString() != "" && dt.Rows[i]["T_TEMP"].ToString() != "" && dt.Rows[i]["T_DATE"].ToString() != "" && dt.Rows[i]["T_SOC_CD"].ToString() != "")
                            {
                                ds = objdb.ByProcedure("USP_Trn_ExcelUploadChillingCostBMCData", new string[] { "flag", "CC_ID", "Office_ID", "CollectionDate", "Temp", "Quantity", "CreatedAt", "CreatedBy", "CreatedByIP", "IsActive" },
                                    new string[] { "1", ddlccbmcdetail.SelectedValue, soc_id, Convert.ToDateTime(oDate, cult).ToString("yyyy/MM/dd"), dt.Rows[i]["T_TEMP"].ToString(), dt.Rows[i]["T_QTY"].ToString(), ViewState["Office_ID"].ToString(), ViewState["Emp_ID"].ToString(), CreatedByIP.ToString(), "1" }, "dataset");

                                truckSheetEtryStatus = "<small style='Color:green'>( Data Updated</small> )";
                            }
                            else
                            {
                                truckSheetEtryStatus = "<small style='Color:tomato'>( All fields are mandatory data cannot be blank</small> )";
                            }
                        }
                        else
                        {
                            truckSheetEtryStatus = "<small style='Color:tomato'>( Mapping is missing )</small>";
                        }

                    }
                    
                    if (returnStatus.ToString() == "Uploaded")
                    {
                        htmlStr.Append("<td class='right_align' style='color:green; Padding:5px !important'> Read " + truckSheetEtryStatus + " </td>");
                    }
                    else if (returnStatus.ToString() == "Updated")
                    {
                        htmlStr.Append("<td class='right_align' style='color:blue; Padding:5px !important'> Read " + truckSheetEtryStatus + "</td>");
                    }
                    else
                    {
                        htmlStr.Append("<td class='right_align' style='color:red; Padding:5px !important'> Not Uploaded " + truckSheetEtryStatus + "</td>");
                    }

                    htmlStr.Append("</tr>");

                }
                //}


                lblMsg.Text = objdb.Alert("fa-ban", "alert-success", "Success", "Below data has been uploaded in server.");
            }
            else
            {
                htmlStr.Append("<div><p>Invalid File Format, Unable to upload it. <br/> Please recheck the file and upload again.</p></div>");
            }

            StringDiv.InnerHtml = htmlStr.ToString();

            //lbluploadingData.Text = "";


        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex.Message);
            //lblMsg.Text = ex.Message.ToString();
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Invalid Columns Names ", ex.Message.ToString());
        }
        finally
        {
            //connExcel.Close();
        }
    }

    protected void GetCCDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                     new string[] { "flag", "I_OfficeID", "I_OfficeTypeID" },
                     new string[] { "3", objdbAPI.Office_ID(), objdbAPI.OfficeType_ID() }, "dataset");

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

                        if (objdbAPI.OfficeType_ID() == "2")
                        {
                            ddlccbmcdetail.Items.Insert(0, new ListItem("Select", "0"));
                        }
                        else
                        {
                            ddlccbmcdetail.SelectedValue = objdbAPI.Office_ID();
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
   

}