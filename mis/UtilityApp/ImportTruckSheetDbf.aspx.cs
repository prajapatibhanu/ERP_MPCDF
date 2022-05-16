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

public partial class mis_UtilityApp_ImportTruckSheetDbf : System.Web.UI.Page
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
            //status = status + " // has file";
            //lbluploadingData.Text = status;
            string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            //string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            //string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            //string FilePath = Server.MapPath(FolderPath + FileName);
            //FileUpload1.SaveAs(FilePath);

            string Extension = Path.GetExtension(FileUpload1.FileName).ToLower();
            //getting the path of the file   
            //string path = Server.MapPath("MyFolder/" + Guid.NewGuid() + "-" + FileName);
            //~/mis/HR/UploadDoc/AttendanceDoc/
            string path = Server.MapPath("~/mis/UtilityApp/MyFolder/" + FileName);
            //saving the file inside the MyFolder of the server  
            FileUpload1.SaveAs(path);


            if (Extension == ".xls" || Extension == ".xlsx")
            {
                //status = status + " // xcel file";
                //lbluploadingData.Text = status;
                Import_To_Grid(path, Extension, "Yes");
                //Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
            }
            else if (Extension == ".DBF" || Extension == ".dbf")
            {
                //status = status + " // DBF file";
                //lbluploadingData.Text = status;
                //lbluploadingData.Text = "<p class='loading'>Uploading Data <br /><span class='one'>..</span><span class='two'>..</span><span class='three'>..</span></p>";
                Import_To_Grid2(path, Extension, "Yes");
                //Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Invalid File ", "Please convert file into .xls or .xlsx or .DBF");
            }

        }
    }
    private void Import_To_Grid(string FilePath, string Extension, string isHDR)
    {
        string conStr = "";
        lblMsg.Text = "";




        //switch (Extension)
        //{
        //    case ".xls": //Excel 97-03
        //        //conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
        //        conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
        //        break;
        //    case ".xlsx": //Excel 07
        //        //conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
        //        conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        //        break;

        //}

        conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        conStr = String.Format(conStr, FilePath, isHDR);
        OleDbConnection connExcel = new OleDbConnection(conStr);
        OleDbCommand cmdExcel = new OleDbCommand();
        OleDbDataAdapter oda = new OleDbDataAdapter();
        DataTable dt = new DataTable();
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
            /**********************/
            StringBuilder htmlStr = new StringBuilder();
            htmlStr.Append("");

            if (dt.Rows.Count > 0)
            {
                int Count = dt.Rows.Count;
                htmlStr.Append("<table class='main-heading-print' id='SalaryTable'>");
                htmlStr.Append("<tr class=''>");
                htmlStr.Append("<th class=''>SNo.</th>");
                htmlStr.Append("<th>Union Code</th>");
                htmlStr.Append("<th>Society Code</th>");
                htmlStr.Append("<th>Date</th>");
                htmlStr.Append("<th>Shift</th>");
                htmlStr.Append("<th>Milk Type</th>");
                htmlStr.Append("<th>Category</th>");
                htmlStr.Append("<th>Quantity</th>");
                htmlStr.Append("<th>FAT</th>");
                htmlStr.Append("<th>SNF</th>");
                htmlStr.Append("<th>CLR</th>");
                htmlStr.Append("<th>Status</th>");
                htmlStr.Append("</tr>");

                for (int i = 0; i < Count; i++)
                {
					
					 if (dt.Rows[i]["T_SNF"].ToString() == "")
                   {
                      dt.Rows[i]["T_SNF"] = Math.Round(Obj_MC.GetSNFPer_DCS(decimal.Parse(dt.Rows[i]["T_FAT"].ToString()), decimal.Parse(dt.Rows[i]["T_SNF"].ToString())),3).ToString();
                   }
                    htmlStr.Append("<tr class=''>");
                    htmlStr.Append("<td>" + (i + 1).ToString() + "</td>");
                    htmlStr.Append("<td>" + dt.Rows[i]["T_UN_CD"].ToString() + " </ td>");
                    htmlStr.Append("<td>" + dt.Rows[i]["T_SOC_CD"].ToString() + " </ td>");
                    htmlStr.Append("<td>" + Convert.ToDateTime(dt.Rows[i]["T_DATE"].ToString(), cult).ToString("dd-MM-yyyy") + "</td>");
                    htmlStr.Append("<td>" + dt.Rows[i]["T_SHIFT"].ToString() + "</td>");
                    htmlStr.Append("<td>" + dt.Rows[i]["T_BFCW_IND"].ToString() + "</td>");
                    htmlStr.Append("<td>" + dt.Rows[i]["T_CATG"].ToString() + "</td>");
                    htmlStr.Append("<td  class='right_align'>" + dt.Rows[i]["T_QTY"].ToString() + "</td>");
                    htmlStr.Append("<td  class='right_align'>" + dt.Rows[i]["T_FAT"].ToString() + "</td>");
                    htmlStr.Append("<td  class='right_align'>" + dt.Rows[i]["T_SNF"].ToString() + "</td>");
                    htmlStr.Append("<td  class='right_align'>" + dt.Rows[i]["T_CLR"].ToString() + "</td>");
                    /**********************/
                    /// ------------Do Your Database Activity Here------------///  

                    string returnStatus = "Uploaded";
                    /**
					dsreturn = objdb.ByProcedure("USP_Trn_ExcelUploadTruck", new string[] { "flag", "UnionOffice_ID", "T_UN_CD", "T_SOC_CD", "T_DATE", "T_SHIFT", "T_BFCW_IND", "T_CATG", "T_QTY", "T_FAT", "T_SNF", "T_CLR", "UpdatedBy", "InsertedBy" }, new string[] { "0", ViewState["Office_ID"].ToString(), dt.Rows[i]["T_UN_CD"].ToString(), dt.Rows[i]["T_SOC_CD"].ToString(), Convert.ToDateTime(dt.Rows[i]["T_DATE"].ToString(), cult).ToString("yyyy-MM-dd"), dt.Rows[i]["T_SHIFT"].ToString(), dt.Rows[i]["T_BFCW_IND"].ToString(), dt.Rows[i]["T_CATG"].ToString(), dt.Rows[i]["T_QTY"].ToString(), dt.Rows[i]["T_FAT"].ToString(), dt.Rows[i]["T_SNF"].ToString(), dt.Rows[i]["T_CLR"].ToString(), ViewState["Emp_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");

                    if (dsreturn.Tables.Count != 0 && dsreturn.Tables[0].Rows.Count != 0)
                    {
                        returnStatus = dsreturn.Tables[0].Rows[0]["ReturnStatus"].ToString();
                    }
                    ***/
                    /**********************/
                    if (returnStatus.ToString() == "Uploaded")
                    {
                        htmlStr.Append("<td class='right_align' style='color:green; Padding:5px !important'> Uploaded </td>");
                    }
                    else if (returnStatus.ToString() == "Updated")
                    {
                        htmlStr.Append("<td class='right_align' style='color:blue; Padding:5px !important'> Updated </td>");
                    }
                    else
                    {
                        htmlStr.Append("<td class='right_align' style='color:red; Padding:5px !important'> Not Uploaded </td>");
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

    private void Import_To_Grid2(string FilePath, string Extension, string isHDR)
    {

        lblMsg.Text = "";
        /*************/
        //string constr = "Provider=Microsoft.ACE.OLEDB.12.0; Driver={Microsoft dBASE Driver (*.dbf)};";
        string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directory.GetParent(FilePath).FullName + ";Extended Properties=dBASE IV;";
        //string constr = "Provider=VFPOLEDB.1;SourceType=DBF;Data Source=" + Directory.GetParent(FilePath).FullName;
        //string ExcelFileName = AppDomain.CurrentDomain.BaseDirectory + "converted_file.xls";
        DataTable dt2 = new DataTable();
        using (OleDbConnection con = new OleDbConnection(constr))
        {
            var sql = "select * from " + Path.GetFileName(FilePath) + ";";
            OleDbCommand cmd = new OleDbCommand(sql, con);
            try
            {
                con.Open();

            }

            catch (Exception ex)
            {
                //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "456 ", ex.Message.ToString());

            }

            if (con.State == ConnectionState.Open)
            {
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);               
                da.Fill(dt2);

            }

            if (con.State == ConnectionState.Open)
            {

                try
                {
                    con.Close();
                }

                catch (Exception ex)
                {
                    //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", " 123", ex.Message.ToString());

                }

            }
        }
        /*************/
        StringDiv.InnerHtml = "";
        try
        {
            /**********************/
            StringBuilder htmlStr = new StringBuilder();
            htmlStr.Append("");

            if (dt2.Rows.Count > 0)
            {
                int Count = dt2.Rows.Count;
                htmlStr.Append("<table class='main-heading-print' id='SalaryTable'>");
                htmlStr.Append("<tr class=''>");
                htmlStr.Append("<th class=''>SNo.</th>");
                htmlStr.Append("<th>Union Code</th>");
                htmlStr.Append("<th>Society Code</th>");
                htmlStr.Append("<th>Date</th>");
                htmlStr.Append("<th>Shift</th>");
                htmlStr.Append("<th>Milk Type</th>");
                htmlStr.Append("<th>Category</th>");
                htmlStr.Append("<th>Quantity</th>");
                htmlStr.Append("<th>FAT</th>");
                htmlStr.Append("<th>SNF</th>");
                htmlStr.Append("<th>CLR</th>");
                htmlStr.Append("<th>Status</th>");
                htmlStr.Append("</tr>");

                for (int i = 0; i < Count; i++)
                {
/***
                    string datetime = Convert.ToDateTime(dt2.Rows[i]["t_date"].ToString(), cult).ToString("yyyy-MM-dd").ToString();
                    if (!string.IsNullOrEmpty(dt2.Rows[i]["t_date"].ToString()))
                    {
                        datetime = Convert.ToDateTime(DateTime.Parse(dt2.Rows[i]["t_date"].ToString()), cult).ToString("yyyy-MM-dd").ToString();

                    }
****/				
					if (dt2.Rows[i]["t_snf"].ToString() == "")
                    {
                        dt2.Rows[i]["t_snf"] = Math.Round(Obj_MC.GetSNFPer_DCS(decimal.Parse(dt2.Rows[i]["t_fat"].ToString()), decimal.Parse(dt2.Rows[i]["t_clr"].ToString())),3).ToString();
                    }
                    htmlStr.Append("<tr class=''>");
                    htmlStr.Append("<td>" + (i + 1).ToString() + "</td>");
                    htmlStr.Append("<td>" + dt2.Rows[i]["t_un_cd"].ToString() + " </ td>");
                    htmlStr.Append("<td>" + dt2.Rows[i]["t_soc_cd"].ToString() + " </ td>");
					htmlStr.Append("<td>" + dt2.Rows[i]["t_date"].ToString() + "</td>");
                    //htmlStr.Append("<td>" + Convert.ToDateTime(dt2.Rows[i]["t_date"].ToString(), cult).ToString("dd-MM-yyyy") + "</td>");
                    htmlStr.Append("<td>" + dt2.Rows[i]["t_shift"].ToString() + "</td>");
                    htmlStr.Append("<td>" + dt2.Rows[i]["t_bfcw_ind"].ToString() + "</td>");
                    htmlStr.Append("<td>" + dt2.Rows[i]["t_catg"].ToString() + "</td>");
                    htmlStr.Append("<td  class='right_align'>" + dt2.Rows[i]["t_qty"].ToString() + "</td>");
                    htmlStr.Append("<td  class='right_align'>" + dt2.Rows[i]["t_fat"].ToString() + "</td>");
                    htmlStr.Append("<td  class='right_align'>" + dt2.Rows[i]["t_snf"].ToString() + "</td>");
                    htmlStr.Append("<td  class='right_align'>" + dt2.Rows[i]["t_clr"].ToString() + "</td>");
                    /**********************/
                    /// ------------Do Your Database Activity Here------------///  
                    string returnStatus = "Uploaded";
                    /***
					dsreturn = objdb.ByProcedure("USP_Trn_ExcelUploadTruck", new string[] { "flag", "UnionOffice_ID", "T_UN_CD", "T_SOC_CD", "T_DATE", "T_SHIFT", "T_BFCW_IND", "T_CATG", "T_QTY", "T_FAT", "T_SNF", "T_CLR", "UpdatedBy", "InsertedBy" }, new string[] { "0", ViewState["Office_ID"].ToString(), dt2.Rows[i]["t_un_cd"].ToString(), dt2.Rows[i]["t_soc_cd"].ToString(), Convert.ToDateTime(dt2.Rows[i]["t_date"].ToString(), cult).ToString("yyyy-MM-dd"), dt2.Rows[i]["t_shift"].ToString(), dt2.Rows[i]["t_bfcw_ind"].ToString(), dt2.Rows[i]["t_catg"].ToString(), dt2.Rows[i]["t_qty"].ToString(), dt2.Rows[i]["t_fat"].ToString(), dt2.Rows[i]["t_snf"].ToString(), dt2.Rows[i]["t_clr"].ToString(), ViewState["Emp_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
***/
                    /************************************/
                    decimal FatInKg = 0, SnfInKg = 0, Fat = 0 ;
                    FatInKg = GetFAT_InKG(dt2.Rows[i]["t_qty"].ToString(), dt2.Rows[i]["t_fat"].ToString());
                    SnfInKg = GetSNF_InKG(dt2.Rows[i]["t_qty"].ToString(), dt2.Rows[i]["t_fat"].ToString(), dt2.Rows[i]["t_clr"].ToString());
                    Fat = decimal.Parse(dt2.Rows[i]["t_fat"].ToString());
					string CreatedByIP = objdbAPI.GetLocalIPAddress();
                    string CowBuff = "Cow";
                    string Shift = "Morning";
                    string CategoryQuality = "Good";
					if (Fat > 5.5M)
                    {
                        CowBuff = "Buf";
                    }
                    // if (((dt2.Rows[i]["t_bfcw_ind"].ToString()).Trim() == "C") || ((dt2.Rows[i]["t_bfcw_ind"].ToString()).Trim() == "c"))
                    // {
                        // CowBuff = "Cow";
                    // }

                    if (((dt2.Rows[i]["t_shift"].ToString()).Trim() == "E") || ((dt2.Rows[i]["t_shift"].ToString()).Trim() == "e"))
                    {
                        Shift = "Evening";
                    }

                    if ((dt2.Rows[i]["t_catg"].ToString()).Trim() == "1")
                    {
                        CategoryQuality = "Good";
                    }
                    else if ((dt2.Rows[i]["t_catg"].ToString()).Trim() == "2")
                    {
                        CategoryQuality = "Sour";
                    }
                    else
                    {
                        CategoryQuality = "Curd";
                    }


                    string truckSheetEtryStatus = "";
					//Convert.ToDateTime(dt2.Rows[i]["t_date"].ToString(), cult).ToString("yyyy/MM/dd")
					string iDate = dt2.Rows[i]["t_date"].ToString();
					DateTime oDate = DateTime.Parse(iDate);

                    ds = objdb.ByProcedure("USP_Trn_ExcelUploadTruck", new string[] { "flag", "T_UN_CD", "T_SOC_CD", "UnionOffice_ID", "supplyUnit", "OfficeType_ID" }, new string[] { "2", objdbAPI.Office_ID(), dt2.Rows[i]["t_soc_cd"].ToString(), ddlOffice.SelectedValue.ToString(), ddlccbmcdetail.SelectedValue, objdbAPI.OfficeType_ID() }, "dataset");

                    if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        //string cc_id = ds.Tables[0].Rows[0]["cc_id"].ToString();
                        string soc_id = ds.Tables[0].Rows[0]["soc_id"].ToString();
                        //if (cc_id.Trim() != "0" && soc_id.Trim() != "0")
					    if (soc_id.Trim() != "0")
                        {
                        
						 if (Shift.ToString() != "" && CowBuff.ToString() != "" && CategoryQuality.ToString() != "" && dt2.Rows[i]["t_qty"].ToString() != "" && dt2.Rows[i]["t_fat"].ToString() != "" && dt2.Rows[i]["t_snf"].ToString() != "" && dt2.Rows[i]["t_clr"].ToString() != "" && FatInKg.ToString() != "" && SnfInKg.ToString() != "")
                            {
                                ds = objdb.ByProcedure("USP_Trn_ExcelUploadTruck", new string[] { "flag", "UnionOffice_ID", "T_UN_CD", "T_SOC_CD", "T_DATE", "T_SHIFT", "T_BFCW_IND", "T_CATG", "T_QTY", "T_FAT", "T_SNF", "T_CLR", "UpdatedBy", "InsertedBy", "CreatedByIP", "FatInKg", "SnfInKg" }, new string[] { "1", ViewState["Office_ID"].ToString(), ddlccbmcdetail.SelectedValue, soc_id, Convert.ToDateTime(oDate, cult).ToString("yyyy/MM/dd"), Shift.ToString(), CowBuff.ToString(), CategoryQuality.ToString(), dt2.Rows[i]["t_qty"].ToString(), dt2.Rows[i]["t_fat"].ToString(), dt2.Rows[i]["t_snf"].ToString(), dt2.Rows[i]["t_clr"].ToString(), ViewState["Emp_ID"].ToString(), ViewState["Emp_ID"].ToString(), CreatedByIP.ToString(), FatInKg.ToString(), SnfInKg.ToString() }, "dataset");
								if(ds != null && ds.Tables.Count > 0)
                                {
                                    if(ds.Tables[0].Rows.Count > 0)
                                    {
                                        if(ds.Tables[0].Rows[0]["status"].ToString() == "true")
                                        {
                                            truckSheetEtryStatus = "<small style='Color:green'>( Data Updated</small> )";
                                        }
                                        else
                                        {
                                            truckSheetEtryStatus = "<small style='Color:green'>( Bill already has been Geneartad)</small> )";
                                        }
                                    }
                                }
                                //truckSheetEtryStatus = "<small style='Color:green'>( Data Updated</small> )";
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
                    /************************************/

/***
                    if (dsreturn.Tables.Count != 0 && dsreturn.Tables[0].Rows.Count != 0)
                    {
                        returnStatus = dsreturn.Tables[0].Rows[0]["ReturnStatus"].ToString();
                    }
****/
                    /**********************/
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
    private decimal GetSNF(string FAT, string CLR)
    {
        decimal snf = 0, clr = 0, fat = 1;
        try
        {
            //Formula for Get SNF as given below:
            if (CLR != "")
            { clr = Convert.ToDecimal(CLR); }
            if (FAT != "")
            { fat = Convert.ToDecimal(FAT); }

            //SNF = (CLR / 4) + (FAT * 0.2) + 0.7 -- Formula Before
            //SNF = (CLR / 4) + (0.25 * FAT) + 0.44 -- Formula After Mail dt: 27 Jan 2020, 16:26
            //snf = ((clr / 4) + (fat * Convert.ToDecimal(0.25)) + Convert.ToDecimal(0.44));
            snf = Obj_MC.GetSNFPer_DCS(fat, clr);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
        return Math.Round(snf, 3);
    }

    private decimal GetSNF_InKG(string MilkQuantity, string FAT, string CLR)
    {
        decimal clr = 0, fat = 0, snf_Per = 0, MilkQty = 0, SNF_InKG = 0;

        try
        {
            if (MilkQuantity == "") { MilkQty = 0; } else { MilkQty = Convert.ToDecimal(MilkQuantity); }

            if (FAT == "") { fat = 0; } else { fat = Convert.ToDecimal(FAT); }

            if (CLR == "") { clr = 0; } else { clr = Convert.ToDecimal(CLR); }

            snf_Per = Obj_MC.GetSNFPer_DCS(fat, clr);

            SNF_InKG = Obj_MC.GetSNFInKg(MilkQty, snf_Per);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }

        return Math.Round(SNF_InKG, 3);
    }

    private decimal GetFAT_InKG(string MilkQuantity, string FAT)
    {
        decimal fat_Per = 0, MilkQty = 0, FAT_InKG = 0;

        try
        {
            if (MilkQuantity == "") { MilkQty = 0; } else { MilkQty = Convert.ToDecimal(MilkQuantity); }

            if (FAT == "") { fat_Per = 0; } else { fat_Per = Convert.ToDecimal(FAT); }

            FAT_InKG = Obj_MC.GetSNFInKg(MilkQty, fat_Per);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }

        return Math.Round(FAT_InKG, 3);
    }

}