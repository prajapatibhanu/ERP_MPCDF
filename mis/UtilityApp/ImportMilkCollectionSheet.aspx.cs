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

public partial class mis_UtilityApp_ImportMilkCollectionSheet : System.Web.UI.Page
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

                    //if (ViewState["Office_ID"].ToString() == "1")
                    //{
                    //    //ddlOffice.Enabled = true;
                    //}
                    //ds = objdb.ByProcedure("SpAdminOffice",
                    //       new string[] { "flag" },
                    //       new string[] { "23" }, "dataset");
                    //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    //{
                    //    ddlOffice.DataSource = ds;
                    //    ddlOffice.DataTextField = "Office_Name";
                    //    ddlOffice.DataValueField = "Office_ID";
                    //    ddlOffice.DataBind();
                    //    ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
                    //    ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                    //}

                    ds = objdb.ByProcedure("SpAdminOffice",
                           new string[] { "flag", "Office_ID" },
                           new string[] { "38", ViewState["Office_ID"].ToString() }, "dataset");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        txtOfficeName.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                        hfOffice_Code.Value = ds.Tables[0].Rows[0]["Office_Code"].ToString();
                        hfOffice_ID.Value = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                        hfParent_Office_ID.Value = ds.Tables[0].Rows[0]["Office_Parant_ID"].ToString();
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
                if(ddlFileForamt.SelectedValue == "1")
                {
                    Import_To_GridNDDB(path, Extension, "Yes");
                }
                else if (ddlFileForamt.SelectedValue == "2")
                {
                    Import_To_GridNDDB_English(path, Extension, "Yes");
                }
                else if (ddlFileForamt.SelectedValue == "4")
                {
                    Import_To_GridReil(path, Extension, "Yes");
                }
                else 
                {
                    Import_To_GridPrompt(path, Extension, "Yes");
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Invalid File ", "Please convert file into .xls or .xlsx");
            }

        }
    }
    
    //private void Import_To_Grid(string FilePath, string Extension, string isHDR)
    //{
    //    string conStr = "";
    //    lblMsg.Text = "";

    //    conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
    //    conStr = String.Format(conStr, FilePath, isHDR);
    //    OleDbConnection connExcel = new OleDbConnection(conStr);
    //    OleDbCommand cmdExcel = new OleDbCommand();
    //    OleDbDataAdapter oda = new OleDbDataAdapter();
    //    DataTable dt = new DataTable();
    //    cmdExcel.Connection = connExcel;
    //    string SheetName = "";
    //    StringDiv.InnerHtml = "";
    //    try
    //    {
    //        connExcel.Open();
    //        DataTable dtExcelSchema;
    //        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
    //        SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
    //        //cmdExcel.CommandText = "SELECT * From [" + SheetName + "] order by 1 asc, 2 desc";
    //        cmdExcel.CommandText = "SELECT * From [" + SheetName + "] WHERE [M_DATE] IS NOT NULL";
    //        oda.SelectCommand = cmdExcel;
    //        oda.Fill(dt);
    //        connExcel.Close();
    //        /**********************/
    //        StringBuilder htmlStr = new StringBuilder();
    //        htmlStr.Append("");

    //        if (dt.Rows.Count > 0)
    //        {
    //            int Count = dt.Rows.Count;

    //            //if (rbSheetType.SelectedItem.Text == "Milk Collection")
    //            //{

    //            htmlStr.Append("<table class='main-heading-print' id='SalaryTable'>");
    //            htmlStr.Append("<tr>");
    //            htmlStr.Append("<th>M_SNo</th>");
    //            htmlStr.Append("<th>M_SAMPLE_NO</th>");
    //            htmlStr.Append("<th>M_CODE</th>");
    //            htmlStr.Append("<th>M_NAME</th>");
    //            htmlStr.Append("<th>M_DATE</th>");
    //            htmlStr.Append("<th>M_SHIFT</th>");
    //            htmlStr.Append("<th>M_MILKTYPE</th>");
    //            htmlStr.Append("<th>M_QUANTITY</th>");
    //            htmlStr.Append("<th>M_FAT_PER</th>");
    //            htmlStr.Append("<th>M_SNF_PER</th>");
    //            htmlStr.Append("<th>M_CLR</th>");
    //            htmlStr.Append("<th>M_RATE_P_L</th>");
    //            htmlStr.Append("<th>M_AMOUNT</th>");
    //            htmlStr.Append("<th>M_WATER_PER</th>");
    //            htmlStr.Append("<th>M_PROTEIN</th>");
    //            htmlStr.Append("<th>M_DENSITY</th>");
    //            htmlStr.Append("<th>M_LACTOSE</th>");
    //            htmlStr.Append("<th>Status</th>");

    //            htmlStr.Append("</tr>");

    //            for (int i = 0; i < Count; i++)
    //            {
    //                htmlStr.Append("<tr class='center_align'>");
    //                htmlStr.Append("<td class='left_align'>" + (i + 1).ToString() + "</td>");
    //                htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["M_SAMPLE_NO"].ToString() + " </ td>");
    //                htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["M_CODE"].ToString() + " </ td>");
    //                htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["M_NAME"].ToString() + "</td>");
    //                htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["M_DATE"].ToString() + "</td>");
    //                htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["M_SHIFT"].ToString() + "</td>");
    //                htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["M_MILKTYPE"].ToString() + "</td>");
    //                htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["M_QUANTITY"].ToString() + "</td>");
    //                htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["M_FAT_PER"].ToString() + "</td>");
    //                htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["M_SNF_PER"].ToString() + " </ td>");
    //                htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["M_CLR"].ToString() + "</td>");
    //                htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["M_RATE_P_L"].ToString() + "</td>");
    //                htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["M_AMOUNT"].ToString() + "</td>");
    //                htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["M_WATER_PER"].ToString() + "</td>");
    //                htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["M_PROTEIN"].ToString() + "</td>");
    //                htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["M_DENSITY"].ToString() + "</td>");
    //                htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["M_LACTOSE"].ToString() + "</td>");


    //                /**********************/
    //                /// ------------Do Your Database Activity Here------------///  

    //                /********
    //                dsreturn = objdb.ByProcedure("USP_Trn_ExcelUploadMilkCollection", new string[] { "flag", "UnionOffice_ID", "M_SAMPLE_NO", "M_CODE", "M_NAME", "M_DATE", "M_SHIFT", "M_MILKTYPE", "M_QUANTITY", "M_FAT_PER", "M_SNF_PER", "M_CLR", "M_RATE_P_L", "M_AMOUNT", "M_WATER_PER", "M_PROTEIN", "M_DENSITY", "M_LACTOSE", "UpdatedBy", "InsertedBy" }, new string[] { "0", ViewState["Office_ID"].ToString(), dt.Rows[i]["M_SAMPLE_NO"].ToString(), dt.Rows[i]["M_CODE"].ToString(), dt.Rows[i]["M_NAME"].ToString(), Convert.ToDateTime(dt.Rows[i]["M_DATE"].ToString(), cult).ToString("yyyy/MM/dd"), dt.Rows[i]["M_SHIFT"].ToString(), dt.Rows[i]["M_MILKTYPE"].ToString(), dt.Rows[i]["M_QUANTITY"].ToString(), dt.Rows[i]["M_FAT_PER"].ToString(), dt.Rows[i]["M_SNF_PER"].ToString(), dt.Rows[i]["M_CLR"].ToString(), dt.Rows[i]["M_RATE_P_L"].ToString(), dt.Rows[i]["M_AMOUNT"].ToString(), dt.Rows[i]["M_WATER_PER"].ToString(), dt.Rows[i]["M_PROTEIN"].ToString(), dt.Rows[i]["M_DENSITY"].ToString(), dt.Rows[i]["M_LACTOSE"].ToString(), ViewState["Emp_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
    //               ********/

    //                //dsreturn = objdb.ByProcedure("USP_Trn_ExcelUploadMilkCollection", new string[] { "flag", "UnionOffice_ID", "M_SAMPLE_NO", "M_CODE", "M_NAME", "M_DATE", "M_SHIFT", "M_MILKTYPE", "M_QUANTITY", "M_FAT_PER", "M_SNF_PER", "M_CLR", "M_RATE_P_L", "M_AMOUNT" ,"UpdatedBy", "InsertedBy" }, new string[] { "0", ViewState["Office_ID"].ToString(), dt.Rows[i]["M_SAMPLE_NO"].ToString(), dt.Rows[i]["M_CODE"].ToString(), dt.Rows[i]["M_NAME"].ToString(), Convert.ToDateTime(dt.Rows[i]["M_DATE"].ToString(), cult).ToString("yyyy/MM/dd"), dt.Rows[i]["M_SHIFT"].ToString(), dt.Rows[i]["M_MILKTYPE"].ToString(), dt.Rows[i]["M_QUANTITY"].ToString(), dt.Rows[i]["M_FAT_PER"].ToString(), dt.Rows[i]["M_SNF_PER"].ToString(), dt.Rows[i]["M_CLR"].ToString(), dt.Rows[i]["M_RATE_P_L"].ToString(), dt.Rows[i]["M_AMOUNT"].ToString() ,ViewState["Emp_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");

    //                //if (dsreturn.Tables.Count != 0 && dsreturn.Tables[0].Rows.Count != 0)
    //                //{
    //                //    returnStatus = dsreturn.Tables[0].Rows[0]["ReturnStatus"].ToString();
    //                //}

    //                string V_Shift = dt.Rows[i]["M_SHIFT"].ToString();
    //                if (V_Shift.Trim().ToLower() == "m")
    //                {
    //                    V_Shift = "Morning";
    //                }
    //                else
    //                {
    //                    V_Shift = "Evening";
    //                }

    //                string V_MilkType = dt.Rows[i]["M_MILKTYPE"].ToString();
    //                if (V_MilkType.Trim().ToLower() == "buff")
    //                {
    //                    V_MilkType = "Buffalo";
    //                }
    //                else
    //                {
    //                    V_MilkType = "Cow";
    //                }


    //                string ErrorSatus = "";
    //                ds_producer = objdb.ByProcedure("USP_Trn_ExcelUploadMilkCollection",
    //   new string[] { "flag", "ProducerCardNo", "DCSMaster_ID" },
    //   new string[] { "3", dt.Rows[i]["M_CODE"].ToString(), hfOffice_ID.Value.ToString() }, "dataset");
    //                if (ds_producer.Tables.Count > 0 && ds_producer.Tables[0].Rows.Count > 0)
    //                {

    //                    ds_upload = null;
    //                    ds_upload = objdb.ByProcedure("USP_Trn_ExcelUploadMilkCollection",
    //                    new string[] { "Flag"
    //                                            ,"Office_Id"
    //                                            ,"V_SocietyName"
    //                                            ,"Dt_Date"
    //                                            ,"V_Shift"
    //                                            ,"I_Producer_ID"
    //                                            ,"I_MilkSupplyQty"
    //                                            ,"V_MilkType"
    //                                            ,"V_Review"
    //                                            ,"Quality"
    //                                            ,"Fat"
    //                                            ,"CLR"
    //                                            ,"SNF"
    //                                            ,"Rate"
    //                                            ,"Amount"
    //                                            ,"TotalSNFInKg"
    //                                            ,"TotalFatInKg"
    //                                            ,"CreatedBy"
    //                                            ,"CreatedBy_IP"
    //                                            ,"Remark" 
    //                                            ,"EntryMode"
    //                                        },
    //                    new string[] { "1"
    //                                            ,ViewState["Office_ID"].ToString()
    //                                            ,txtOfficeName.Text.ToString()
    //                                            ,Convert.ToDateTime(dt.Rows[i]["M_DATE"].ToString(), cult).ToString("yyyy/MM/dd")
    //                                            ,V_Shift.ToString()
    //                                            ,ds_producer.Tables[0].Rows[0]["ProducerId"].ToString()
    //                                            ,dt.Rows[i]["M_QUANTITY"].ToString()
    //                                            ,V_MilkType.ToString()
    //                                            ,""
    //                                            ,"Good"
    //                                            ,dt.Rows[i]["M_FAT_PER"].ToString()
    //                                            ,dt.Rows[i]["M_CLR"].ToString()
    //                                            ,dt.Rows[i]["M_SNF_PER"].ToString()
    //                                            ,dt.Rows[i]["M_RATE_P_L"].ToString()
    //                                            ,dt.Rows[i]["M_AMOUNT"].ToString()
    //                                            ,"0.0"
    //                                            ,"0.0"
    //                                            ,ViewState["Emp_ID"].ToString()
    //                                            ,objdb2.GetLocalIPAddress() 
    //                                            ,""
    //                                            ,"AMCU"

    //                                        }, "dataset");

    //                    if (ds_upload.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
    //                    {
    //                        ErrorSatus = " <span style='color:green'> (Inserted.) </span>";
    //                    }
    //                    else
    //                    {
    //                        ErrorSatus = "<span style='color:blue'> (Already Exist.) </span>";
    //                    }
    //                }
    //                else
    //                {

    //                    ErrorSatus = "<span style='color:orange'> (Producer Not Found) </span>";

    //                }


    //                string returnStatus = "Uploaded";
    //                /**********************/
    //                if (returnStatus.ToString() == "Uploaded")
    //                {
    //                    htmlStr.Append("<td class='right_align' style='color:green; Padding:5px !important'> Read   " + ErrorSatus + " </td>");
    //                }
    //                else if (returnStatus.ToString() == "Updated")
    //                {
    //                    htmlStr.Append("<td class='right_align' style='color:blue; Padding:5px !important'> Read   " + ErrorSatus + " </td>");
    //                }
    //                else
    //                {
    //                    htmlStr.Append("<td class='right_align' style='color:red; Padding:5px !important'> Not Uploaded  " + ErrorSatus + " </td>");
    //                }

    //                htmlStr.Append("</tr>");

    //            }



    //        }
    //        else
    //        {
    //            htmlStr.Append("<div><p>Invalid File Format, Unable to upload it. <br/> Please recheck the file and upload again.</p></div>");
    //        }

    //        StringDiv.InnerHtml = htmlStr.ToString();

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Invalid Columns Names ", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        connExcel.Close();
    //    }
    //}
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
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "] WHERE [Date] IS NOT NULL";
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
                htmlStr.Append("<th>Sample Number</th>");
                htmlStr.Append("<th>Code</th>");
                htmlStr.Append("<th>Name</th>");
                htmlStr.Append("<th>Date</th>");
                htmlStr.Append("<th>Shift</th>");
                htmlStr.Append("<th>Milk</th>");
                htmlStr.Append("<th>Qty(Ltr)</th>");
                htmlStr.Append("<th>Fat%</th>");
                htmlStr.Append("<th>SNF%</th>");
                htmlStr.Append("<th>CLR</th>");
                htmlStr.Append("<th>Rate/Ltr</th>");
                htmlStr.Append("<th>Amount</th>");
                htmlStr.Append("<th>Water</th>");
                htmlStr.Append("<th>Protein</th>");
                htmlStr.Append("<th>Density</th>");
                htmlStr.Append("<th>Lactose</th>");
                htmlStr.Append("<th>Status</th>");

                htmlStr.Append("</tr>");

                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<tr class='center_align'>");
                    htmlStr.Append("<td class='left_align'>" + (i + 1).ToString() + "</td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["Sample Number"].ToString() + " </ td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["Code"].ToString() + " </ td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["Name"].ToString() + "</td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["Date"].ToString() + "</td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["Shift"].ToString() + "</td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["Milk"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Qty(Ltr)"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Fat%"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["SNF%"].ToString() + " </ td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["CLR"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Rate/Ltr"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Amount"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Water"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Protein"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Density"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Lactose"].ToString() + "</td>");


                    /**********************/
                    /// ------------Do Your Database Activity Here------------///  

                    /********
                    dsreturn = objdb.ByProcedure("USP_Trn_ExcelUploadMilkCollection", new string[] { "flag", "UnionOffice_ID", "M_SAMPLE_NO", "M_CODE", "M_NAME", "M_DATE", "M_SHIFT", "M_MILKTYPE", "M_QUANTITY", "M_FAT_PER", "M_SNF_PER", "M_CLR", "M_RATE_P_L", "M_AMOUNT", "M_WATER_PER", "M_PROTEIN", "M_DENSITY", "M_LACTOSE", "UpdatedBy", "InsertedBy" }, new string[] { "0", ViewState["Office_ID"].ToString(), dt.Rows[i]["M_SAMPLE_NO"].ToString(), dt.Rows[i]["M_CODE"].ToString(), dt.Rows[i]["M_NAME"].ToString(), Convert.ToDateTime(dt.Rows[i]["M_DATE"].ToString(), cult).ToString("yyyy/MM/dd"), dt.Rows[i]["M_SHIFT"].ToString(), dt.Rows[i]["M_MILKTYPE"].ToString(), dt.Rows[i]["M_QUANTITY"].ToString(), dt.Rows[i]["M_FAT_PER"].ToString(), dt.Rows[i]["M_SNF_PER"].ToString(), dt.Rows[i]["M_CLR"].ToString(), dt.Rows[i]["M_RATE_P_L"].ToString(), dt.Rows[i]["M_AMOUNT"].ToString(), dt.Rows[i]["M_WATER_PER"].ToString(), dt.Rows[i]["M_PROTEIN"].ToString(), dt.Rows[i]["M_DENSITY"].ToString(), dt.Rows[i]["M_LACTOSE"].ToString(), ViewState["Emp_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
                   ********/

                    //dsreturn = objdb.ByProcedure("USP_Trn_ExcelUploadMilkCollection", new string[] { "flag", "UnionOffice_ID", "M_SAMPLE_NO", "M_CODE", "M_NAME", "M_DATE", "M_SHIFT", "M_MILKTYPE", "M_QUANTITY", "M_FAT_PER", "M_SNF_PER", "M_CLR", "M_RATE_P_L", "M_AMOUNT" ,"UpdatedBy", "InsertedBy" }, new string[] { "0", ViewState["Office_ID"].ToString(), dt.Rows[i]["M_SAMPLE_NO"].ToString(), dt.Rows[i]["M_CODE"].ToString(), dt.Rows[i]["M_NAME"].ToString(), Convert.ToDateTime(dt.Rows[i]["M_DATE"].ToString(), cult).ToString("yyyy/MM/dd"), dt.Rows[i]["M_SHIFT"].ToString(), dt.Rows[i]["M_MILKTYPE"].ToString(), dt.Rows[i]["M_QUANTITY"].ToString(), dt.Rows[i]["M_FAT_PER"].ToString(), dt.Rows[i]["M_SNF_PER"].ToString(), dt.Rows[i]["M_CLR"].ToString(), dt.Rows[i]["M_RATE_P_L"].ToString(), dt.Rows[i]["M_AMOUNT"].ToString() ,ViewState["Emp_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");

                    //if (dsreturn.Tables.Count != 0 && dsreturn.Tables[0].Rows.Count != 0)
                    //{
                    //    returnStatus = dsreturn.Tables[0].Rows[0]["ReturnStatus"].ToString();
                    //}

                    string V_Shift = dt.Rows[i]["Shift"].ToString();
                    if (V_Shift.Trim().ToLower() == "m" || V_Shift.Trim().ToLower() == "सुबह")
                    {
                        V_Shift = "Morning";
                    }
                    else
                    {
                        V_Shift = "Evening";
                    }

                    string V_MilkType = dt.Rows[i]["Milk"].ToString();
                    if (V_MilkType.Trim().ToLower() == "buff" || V_MilkType.Trim().ToLower() == "भैंस")
                    {
                        V_MilkType = "Buffalo";
                    }
                    else
                    {
                        V_MilkType = "Cow";
                    }


                    string ErrorSatus = "";
                    ds_producer = objdb.ByProcedure("USP_Trn_ExcelUploadMilkCollection",
       new string[] { "flag", "ProducerCardNo", "DCSMaster_ID" },
       new string[] { "3", dt.Rows[i]["Code"].ToString(), hfOffice_ID.Value.ToString() }, "dataset");
                    if (ds_producer.Tables.Count > 0 && ds_producer.Tables[0].Rows.Count > 0)
                    {

                        ds_upload = null;
                        ds_upload = objdb.ByProcedure("USP_Trn_ExcelUploadMilkCollection",
                        new string[] { "Flag"
                                                ,"Office_Id"
                                                ,"V_SocietyName"
                                                ,"Dt_Date"
                                                ,"V_Shift"
                                                ,"I_Producer_ID"
                                                ,"I_MilkSupplyQty"
                                                ,"V_MilkType"
                                                ,"V_Review"
                                                ,"Quality"
                                                ,"Fat"
                                                ,"CLR"
                                                ,"SNF"
                                                ,"Rate"
                                                ,"Amount"
                                                ,"TotalSNFInKg"
                                                ,"TotalFatInKg"
                                                ,"CreatedBy"
                                                ,"CreatedBy_IP"
                                                ,"Remark" 
                                                ,"EntryMode"
                                            },
                        new string[] { "1"
                                                ,ViewState["Office_ID"].ToString()
                                                ,txtOfficeName.Text.ToString()
                                                ,Convert.ToDateTime(dt.Rows[i]["Date"].ToString(), cult).ToString("yyyy/MM/dd")
                                                ,V_Shift.ToString()
                                                ,ds_producer.Tables[0].Rows[0]["ProducerId"].ToString()
                                                ,dt.Rows[i]["Qty(Ltr)"].ToString()
                                                ,V_MilkType.ToString()
                                                ,""
                                                ,"Good"
                                                ,dt.Rows[i]["Fat%"].ToString()
                                                ,dt.Rows[i]["CLR"].ToString()
                                                ,dt.Rows[i]["SNF%"].ToString()
                                                ,dt.Rows[i]["Rate/Ltr"].ToString()
                                                ,dt.Rows[i]["Amount"].ToString()
                                                ,"0.0"
                                                ,"0.0"
                                                ,ViewState["Emp_ID"].ToString()
                                                ,objdb2.GetLocalIPAddress() 
                                                ,""
                                                ,"PROMPT"

                                            }, "dataset");

                        if (ds_upload.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            ErrorSatus = " <span style='color:green'> (Inserted.) </span>";
                        }
                        else
                        {
                            ErrorSatus = "<span style='color:blue'> (Already Exist.) </span>";
                        }
                    }
                    else
                    {

                        ErrorSatus = "<span style='color:orange'> (Producer Not Found) </span>";

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
    private void Import_To_GridNDDB(string FilePath, string Extension, string isHDR)
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
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "] WHERE [दिनांक] IS NOT NULL";
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
                htmlStr.Append("<th>सेंपल नंबर</th>");
                htmlStr.Append("<th>कोड</th>");
                htmlStr.Append("<th>नाम</th>");
                htmlStr.Append("<th>दिनांक</th>");
                htmlStr.Append("<th>शिफ्ट</th>");
                htmlStr.Append("<th>दूध प्रकार</th>");
                htmlStr.Append("<th>मात्रा(ली)</th>");
                htmlStr.Append("<th>फेट%</th>");
                htmlStr.Append("<th>एसएनएफ%</th>");
                htmlStr.Append("<th>सीएलआर</th>");
                htmlStr.Append("<th>रेट प्रति लिटर</th>");
                htmlStr.Append("<th>राशि (रु#)</th>");
                htmlStr.Append("<th>पानी(%)</th>");
                htmlStr.Append("<th>प्रोटीन</th>");
                htmlStr.Append("<th>घनत्व</th>");
                htmlStr.Append("<th>लैक्टोज</th>");
                htmlStr.Append("<th>Status</th>");

                htmlStr.Append("</tr>");

                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<tr class='center_align'>");
                    htmlStr.Append("<td class='left_align'>" + (i + 1).ToString() + "</td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["सेंपल नंबर"].ToString() + " </ td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["कोड"].ToString() + " </ td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["नाम"].ToString() + "</td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["दिनांक"].ToString() + "</td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["शिफ्ट"].ToString() + "</td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["दूध प्रकार"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["मात्रा(ली)"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["फेट%"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["एसएनएफ%"].ToString() + " </ td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["सीएलआर"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["रेट प्रति लिटर"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["राशि (रु#)"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["पानी(%)"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["प्रोटीन"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["घनत्व"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["लैक्टोज"].ToString() + "</td>");


                    /**********************/
                    /// ------------Do Your Database Activity Here------------///  

                    /********
                    dsreturn = objdb.ByProcedure("USP_Trn_ExcelUploadMilkCollection", new string[] { "flag", "UnionOffice_ID", "M_SAMPLE_NO", "M_CODE", "M_NAME", "M_DATE", "M_SHIFT", "M_MILKTYPE", "M_QUANTITY", "M_FAT_PER", "M_SNF_PER", "M_CLR", "M_RATE_P_L", "M_AMOUNT", "M_WATER_PER", "M_PROTEIN", "M_DENSITY", "M_LACTOSE", "UpdatedBy", "InsertedBy" }, new string[] { "0", ViewState["Office_ID"].ToString(), dt.Rows[i]["M_SAMPLE_NO"].ToString(), dt.Rows[i]["M_CODE"].ToString(), dt.Rows[i]["M_NAME"].ToString(), Convert.ToDateTime(dt.Rows[i]["M_DATE"].ToString(), cult).ToString("yyyy/MM/dd"), dt.Rows[i]["M_SHIFT"].ToString(), dt.Rows[i]["M_MILKTYPE"].ToString(), dt.Rows[i]["M_QUANTITY"].ToString(), dt.Rows[i]["M_FAT_PER"].ToString(), dt.Rows[i]["M_SNF_PER"].ToString(), dt.Rows[i]["M_CLR"].ToString(), dt.Rows[i]["M_RATE_P_L"].ToString(), dt.Rows[i]["M_AMOUNT"].ToString(), dt.Rows[i]["M_WATER_PER"].ToString(), dt.Rows[i]["M_PROTEIN"].ToString(), dt.Rows[i]["M_DENSITY"].ToString(), dt.Rows[i]["M_LACTOSE"].ToString(), ViewState["Emp_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
                   ********/

                    //dsreturn = objdb.ByProcedure("USP_Trn_ExcelUploadMilkCollection", new string[] { "flag", "UnionOffice_ID", "M_SAMPLE_NO", "M_CODE", "M_NAME", "M_DATE", "M_SHIFT", "M_MILKTYPE", "M_QUANTITY", "M_FAT_PER", "M_SNF_PER", "M_CLR", "M_RATE_P_L", "M_AMOUNT" ,"UpdatedBy", "InsertedBy" }, new string[] { "0", ViewState["Office_ID"].ToString(), dt.Rows[i]["M_SAMPLE_NO"].ToString(), dt.Rows[i]["M_CODE"].ToString(), dt.Rows[i]["M_NAME"].ToString(), Convert.ToDateTime(dt.Rows[i]["M_DATE"].ToString(), cult).ToString("yyyy/MM/dd"), dt.Rows[i]["M_SHIFT"].ToString(), dt.Rows[i]["M_MILKTYPE"].ToString(), dt.Rows[i]["M_QUANTITY"].ToString(), dt.Rows[i]["M_FAT_PER"].ToString(), dt.Rows[i]["M_SNF_PER"].ToString(), dt.Rows[i]["M_CLR"].ToString(), dt.Rows[i]["M_RATE_P_L"].ToString(), dt.Rows[i]["M_AMOUNT"].ToString() ,ViewState["Emp_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");

                    //if (dsreturn.Tables.Count != 0 && dsreturn.Tables[0].Rows.Count != 0)
                    //{
                    //    returnStatus = dsreturn.Tables[0].Rows[0]["ReturnStatus"].ToString();
                    //}

                    string V_Shift = dt.Rows[i]["शिफ्ट"].ToString();
                    if (V_Shift.Trim().ToLower() == "m" || V_Shift.Trim().ToLower() == "सुबह")
                    {
                        V_Shift = "Morning";
                    }
                    else
                    {
                        V_Shift = "Evening";
                    }

                    string V_MilkType = dt.Rows[i]["दूध प्रकार"].ToString();
                    if (V_MilkType.Trim().ToLower() == "buff" || V_MilkType.Trim().ToLower() == "भैंस")
                    {
                        V_MilkType = "Buffalo";
                    }
                    else
                    {
                        V_MilkType = "Cow";
                    }


                    string ErrorSatus = "";
                    ds_producer = objdb.ByProcedure("USP_Trn_ExcelUploadMilkCollection",
       new string[] { "flag", "ProducerCardNo", "DCSMaster_ID" },
       new string[] { "3", dt.Rows[i]["कोड"].ToString(), hfOffice_ID.Value.ToString() }, "dataset");
                    if (ds_producer.Tables.Count > 0 && ds_producer.Tables[0].Rows.Count > 0)
                    {

                        ds_upload = null;
                        ds_upload = objdb.ByProcedure("USP_Trn_ExcelUploadMilkCollection",
                        new string[] { "Flag"
                                                ,"Office_Id"
                                                ,"V_SocietyName"
                                                ,"Dt_Date"
                                                ,"V_Shift"
                                                ,"I_Producer_ID"
                                                ,"I_MilkSupplyQty"
                                                ,"V_MilkType"
                                                ,"V_Review"
                                                ,"Quality"
                                                ,"Fat"
                                                ,"CLR"
                                                ,"SNF"
                                                ,"Rate"
                                                ,"Amount"
                                                ,"TotalSNFInKg"
                                                ,"TotalFatInKg"
                                                ,"CreatedBy"
                                                ,"CreatedBy_IP"
                                                ,"Remark" 
                                                ,"EntryMode"
                                            },
                        new string[] { "1"
                                                ,ViewState["Office_ID"].ToString()
                                                ,txtOfficeName.Text.ToString()
                                                ,Convert.ToDateTime(dt.Rows[i]["दिनांक"].ToString(), cult).ToString("yyyy/MM/dd")
                                                ,V_Shift.ToString()
                                                ,ds_producer.Tables[0].Rows[0]["ProducerId"].ToString()
                                                ,dt.Rows[i]["मात्रा(ली)"].ToString()
                                                ,V_MilkType.ToString()
                                                ,""
                                                ,"Good"
                                                ,dt.Rows[i]["फेट%"].ToString()
                                                ,dt.Rows[i]["सीएलआर"].ToString()
                                                ,dt.Rows[i]["एसएनएफ%"].ToString()
                                                ,dt.Rows[i]["रेट प्रति लिटर"].ToString()
                                                ,dt.Rows[i]["राशि (रु#)"].ToString()
                                                ,"0.0"
                                                ,"0.0"
                                                ,ViewState["Emp_ID"].ToString()
                                                ,objdb2.GetLocalIPAddress() 
                                                ,""
                                                ,"NDDB"

                                            }, "dataset");

                        if (ds_upload.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            ErrorSatus = " <span style='color:green'> (Inserted.) </span>";
                        }
                        else
                        {
                            ErrorSatus = "<span style='color:blue'> (Already Exist.) </span>";
                        }
                    }
                    else
                    {

                        ErrorSatus = "<span style='color:orange'> (Producer Not Found) </span>";

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
    private void Import_To_GridNDDB_English(string FilePath, string Extension, string isHDR)
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
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "] WHERE [Date] IS NOT NULL";
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
                htmlStr.Append("<th>Sample No#</th>");
                htmlStr.Append("<th>M# Code</th>");
                htmlStr.Append("<th>M# Name</th>");
                htmlStr.Append("<th>Date</th>");
               // htmlStr.Append("<th>SDate</th>");
                htmlStr.Append("<th>Shift</th>");
                htmlStr.Append("<th>Milk Type</th>");
                htmlStr.Append("<th>Qty(Ltr#)</th>");
                htmlStr.Append("<th>Fat %</th>");
                htmlStr.Append("<th>SNF %</th>");
                htmlStr.Append("<th>CLR</th>");
                htmlStr.Append("<th>RTPL</th>");
                htmlStr.Append("<th>Amount (Rs#)</th>");
                htmlStr.Append("<th>Water(%)</th>");
                htmlStr.Append("<th>Protein</th>");
                htmlStr.Append("<th>Density</th>");
                htmlStr.Append("<th>Lactose</th>");
                htmlStr.Append("<th>Status</th>");

                htmlStr.Append("</tr>");

                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<tr class='center_align'>");
                    htmlStr.Append("<td class='left_align'>" + (i + 1).ToString() + "</td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["Sample No#"].ToString() + " </ td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["M# Code"].ToString() + " </ td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["M# Name"].ToString() + "</td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["Date"].ToString() + "</td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["Shift"].ToString() + "</td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["Milk Type"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Qty(Ltr#)"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Fat %"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["SNF %"].ToString() + " </ td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["CLR"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["RTPL"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Amount (Rs#)"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Water(%)"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Protein"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Density"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Lactose"].ToString() + "</td>");


                    /**********************/
                    /// ------------Do Your Database Activity Here------------///  

                    /********
                    dsreturn = objdb.ByProcedure("USP_Trn_ExcelUploadMilkCollection", new string[] { "flag", "UnionOffice_ID", "M_SAMPLE_NO", "M_CODE", "M_NAME", "M_DATE", "M_SHIFT", "M_MILKTYPE", "M_QUANTITY", "M_FAT_PER", "M_SNF_PER", "M_CLR", "M_RATE_P_L", "M_AMOUNT", "M_WATER_PER", "M_PROTEIN", "M_DENSITY", "M_LACTOSE", "UpdatedBy", "InsertedBy" }, new string[] { "0", ViewState["Office_ID"].ToString(), dt.Rows[i]["M_SAMPLE_NO"].ToString(), dt.Rows[i]["M_CODE"].ToString(), dt.Rows[i]["M_NAME"].ToString(), Convert.ToDateTime(dt.Rows[i]["M_DATE"].ToString(), cult).ToString("yyyy/MM/dd"), dt.Rows[i]["M_SHIFT"].ToString(), dt.Rows[i]["M_MILKTYPE"].ToString(), dt.Rows[i]["M_QUANTITY"].ToString(), dt.Rows[i]["M_FAT_PER"].ToString(), dt.Rows[i]["M_SNF_PER"].ToString(), dt.Rows[i]["M_CLR"].ToString(), dt.Rows[i]["M_RATE_P_L"].ToString(), dt.Rows[i]["M_AMOUNT"].ToString(), dt.Rows[i]["M_WATER_PER"].ToString(), dt.Rows[i]["M_PROTEIN"].ToString(), dt.Rows[i]["M_DENSITY"].ToString(), dt.Rows[i]["M_LACTOSE"].ToString(), ViewState["Emp_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
                   ********/

                    //dsreturn = objdb.ByProcedure("USP_Trn_ExcelUploadMilkCollection", new string[] { "flag", "UnionOffice_ID", "M_SAMPLE_NO", "M_CODE", "M_NAME", "M_DATE", "M_SHIFT", "M_MILKTYPE", "M_QUANTITY", "M_FAT_PER", "M_SNF_PER", "M_CLR", "M_RATE_P_L", "M_AMOUNT" ,"UpdatedBy", "InsertedBy" }, new string[] { "0", ViewState["Office_ID"].ToString(), dt.Rows[i]["M_SAMPLE_NO"].ToString(), dt.Rows[i]["M_CODE"].ToString(), dt.Rows[i]["M_NAME"].ToString(), Convert.ToDateTime(dt.Rows[i]["M_DATE"].ToString(), cult).ToString("yyyy/MM/dd"), dt.Rows[i]["M_SHIFT"].ToString(), dt.Rows[i]["M_MILKTYPE"].ToString(), dt.Rows[i]["M_QUANTITY"].ToString(), dt.Rows[i]["M_FAT_PER"].ToString(), dt.Rows[i]["M_SNF_PER"].ToString(), dt.Rows[i]["M_CLR"].ToString(), dt.Rows[i]["M_RATE_P_L"].ToString(), dt.Rows[i]["M_AMOUNT"].ToString() ,ViewState["Emp_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");

                    //if (dsreturn.Tables.Count != 0 && dsreturn.Tables[0].Rows.Count != 0)
                    //{
                    //    returnStatus = dsreturn.Tables[0].Rows[0]["ReturnStatus"].ToString();
                    //}

                    string V_Shift = dt.Rows[i]["Shift"].ToString();
                    if (V_Shift.Trim() == "Morning" || V_Shift.Trim().ToLower() == "सुबह")
                    {
                        V_Shift = "Morning";
                    }
                    else
                    {
                        V_Shift = "Evening";
                    }

                    string V_MilkType = dt.Rows[i]["Milk Type"].ToString();
                    if (V_MilkType.Trim() == "Buffalo" || V_MilkType.Trim().ToLower() == "भैंस")
                    {
                        V_MilkType = "Buffalo";
                    }
                    else
                    {
                        V_MilkType = "Cow";
                    }


                    string ErrorSatus = "";
                    ds_producer = objdb.ByProcedure("USP_Trn_ExcelUploadMilkCollection",
       new string[] { "flag", "ProducerCardNo", "DCSMaster_ID" },
       new string[] { "3", dt.Rows[i]["M# Code"].ToString(), hfOffice_ID.Value.ToString() }, "dataset");
                    if (ds_producer.Tables.Count > 0 && ds_producer.Tables[0].Rows.Count > 0)
                    {

                        ds_upload = null;
                        ds_upload = objdb.ByProcedure("USP_Trn_ExcelUploadMilkCollection",
                        new string[] { "Flag"
                                                ,"Office_Id"
                                                ,"V_SocietyName"
                                                ,"Dt_Date"
                                                ,"V_Shift"
                                                ,"I_Producer_ID"
                                                ,"I_MilkSupplyQty"
                                                ,"V_MilkType"
                                                ,"V_Review"
                                                ,"Quality"
                                                ,"Fat"
                                                ,"CLR"
                                                ,"SNF"
                                                ,"Rate"
                                                ,"Amount"
                                                ,"TotalSNFInKg"
                                                ,"TotalFatInKg"
                                                ,"CreatedBy"
                                                ,"CreatedBy_IP"
                                                ,"Remark" 
                                                ,"EntryMode"
                                            },
                        new string[] { "1"
                                                ,ViewState["Office_ID"].ToString()
                                                ,txtOfficeName.Text.ToString()
                                                ,Convert.ToDateTime(dt.Rows[i]["Date"].ToString().Trim(), cult).ToString("yyyy/MM/dd")
                                                ,V_Shift.ToString()
                                                ,ds_producer.Tables[0].Rows[0]["ProducerId"].ToString()
                                                ,dt.Rows[i]["Qty(Ltr#)"].ToString()
                                                ,V_MilkType.ToString()
                                                ,""
                                                ,"Good"
                                                ,dt.Rows[i]["Fat %"].ToString()
                                                ,dt.Rows[i]["CLR"].ToString()
                                                ,dt.Rows[i]["SNF %"].ToString()
                                                ,dt.Rows[i]["RTPL"].ToString()
                                                ,dt.Rows[i]["Amount (Rs#)"].ToString()
                                                ,"0.0"
                                                ,"0.0"
                                                ,ViewState["Emp_ID"].ToString()
                                                ,objdb2.GetLocalIPAddress() 
                                                ,""
                                                ,"NDDB"

                                            }, "dataset");

                        if (ds_upload.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            ErrorSatus = " <span style='color:green'> (Inserted.) </span>";
                        }
                        else
                        {
                            ErrorSatus = "<span style='color:blue'> (Already Exist.) </span>";
                        }
                    }
                    else
                    {

                        ErrorSatus = "<span style='color:orange'> (Producer Not Found) </span>";

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
    private void Import_To_GridReil(string FilePath, string Extension, string isHDR)
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
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "] WHERE [Date] IS NOT NULL";
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
                //htmlStr.Append("<th>Sample Number</th>");
                htmlStr.Append("<th>code</th>");
                htmlStr.Append("<th>name</th>");
                htmlStr.Append("<th>Date</th>");
                htmlStr.Append("<th>Shift</th>");
                htmlStr.Append("<th>Milk</th>");
                htmlStr.Append("<th>Qty(Ltr)</th>");
                htmlStr.Append("<th>Fat%</th>");
                htmlStr.Append("<th>SNF%</th>");
                htmlStr.Append("<th>CLR</th>");
                htmlStr.Append("<th>Rate/Ltr</th>");
                htmlStr.Append("<th>Amount</th>");
                htmlStr.Append("<th>Water</th>");
                htmlStr.Append("<th>Protein</th>");
                htmlStr.Append("<th>Density</th>");
                htmlStr.Append("<th>Lactose</th>");
                htmlStr.Append("<th>Status</th>");

                htmlStr.Append("</tr>");

                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<tr class='center_align'>");
                    htmlStr.Append("<td class='left_align'>" + (i + 1).ToString() + "</td>");
                    //htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["Sample Number"].ToString() + " </ td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["Code"].ToString() + " </ td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["Name"].ToString() + "</td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["Date"].ToString() + "</td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["Shift"].ToString() + "</td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["Milk"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Qty(Ltr)"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Fat%"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["SNF%"].ToString() + " </ td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["CLR"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Rate/Ltr"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Amount"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Water"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Protein"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Density"].ToString() + "</td>");
                    htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["Lactose"].ToString() + "</td>");


                    /**********************/
                    /// ------------Do Your Database Activity Here------------///  

                    /********
                    dsreturn = objdb.ByProcedure("USP_Trn_ExcelUploadMilkCollection", new string[] { "flag", "UnionOffice_ID", "M_SAMPLE_NO", "M_CODE", "M_NAME", "M_DATE", "M_SHIFT", "M_MILKTYPE", "M_QUANTITY", "M_FAT_PER", "M_SNF_PER", "M_CLR", "M_RATE_P_L", "M_AMOUNT", "M_WATER_PER", "M_PROTEIN", "M_DENSITY", "M_LACTOSE", "UpdatedBy", "InsertedBy" }, new string[] { "0", ViewState["Office_ID"].ToString(), dt.Rows[i]["M_SAMPLE_NO"].ToString(), dt.Rows[i]["M_CODE"].ToString(), dt.Rows[i]["M_NAME"].ToString(), Convert.ToDateTime(dt.Rows[i]["M_DATE"].ToString(), cult).ToString("yyyy/MM/dd"), dt.Rows[i]["M_SHIFT"].ToString(), dt.Rows[i]["M_MILKTYPE"].ToString(), dt.Rows[i]["M_QUANTITY"].ToString(), dt.Rows[i]["M_FAT_PER"].ToString(), dt.Rows[i]["M_SNF_PER"].ToString(), dt.Rows[i]["M_CLR"].ToString(), dt.Rows[i]["M_RATE_P_L"].ToString(), dt.Rows[i]["M_AMOUNT"].ToString(), dt.Rows[i]["M_WATER_PER"].ToString(), dt.Rows[i]["M_PROTEIN"].ToString(), dt.Rows[i]["M_DENSITY"].ToString(), dt.Rows[i]["M_LACTOSE"].ToString(), ViewState["Emp_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
                   ********/

                    //dsreturn = objdb.ByProcedure("USP_Trn_ExcelUploadMilkCollection", new string[] { "flag", "UnionOffice_ID", "M_SAMPLE_NO", "M_CODE", "M_NAME", "M_DATE", "M_SHIFT", "M_MILKTYPE", "M_QUANTITY", "M_FAT_PER", "M_SNF_PER", "M_CLR", "M_RATE_P_L", "M_AMOUNT" ,"UpdatedBy", "InsertedBy" }, new string[] { "0", ViewState["Office_ID"].ToString(), dt.Rows[i]["M_SAMPLE_NO"].ToString(), dt.Rows[i]["M_CODE"].ToString(), dt.Rows[i]["M_NAME"].ToString(), Convert.ToDateTime(dt.Rows[i]["M_DATE"].ToString(), cult).ToString("yyyy/MM/dd"), dt.Rows[i]["M_SHIFT"].ToString(), dt.Rows[i]["M_MILKTYPE"].ToString(), dt.Rows[i]["M_QUANTITY"].ToString(), dt.Rows[i]["M_FAT_PER"].ToString(), dt.Rows[i]["M_SNF_PER"].ToString(), dt.Rows[i]["M_CLR"].ToString(), dt.Rows[i]["M_RATE_P_L"].ToString(), dt.Rows[i]["M_AMOUNT"].ToString() ,ViewState["Emp_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");

                    //if (dsreturn.Tables.Count != 0 && dsreturn.Tables[0].Rows.Count != 0)
                    //{
                    //    returnStatus = dsreturn.Tables[0].Rows[0]["ReturnStatus"].ToString();
                    //}

                    string V_Shift = dt.Rows[i]["Shift"].ToString();
                    if (V_Shift.Trim().ToLower() == "m" || V_Shift.Trim().ToLower() == "सुबह")
                    {
                        V_Shift = "Morning";
                    }
                    else
                    {
                        V_Shift = "Evening";
                    }

                    string V_MilkType = dt.Rows[i]["Milk"].ToString();
                    if (V_MilkType.Trim().ToLower() == "buff" || V_MilkType.Trim().ToLower() == "भैंस")
                    {
                        V_MilkType = "Buffalo";
                    }
                    else
                    {
                        V_MilkType = "Cow";
                    }


                    string ErrorSatus = "";
                    ds_producer = objdb.ByProcedure("USP_Trn_ExcelUploadMilkCollection",
       new string[] { "flag", "ProducerCardNo", "DCSMaster_ID" },
       new string[] { "3", dt.Rows[i]["Code"].ToString(), hfOffice_ID.Value.ToString() }, "dataset");
                    if (ds_producer.Tables.Count > 0 && ds_producer.Tables[0].Rows.Count > 0)
                    {

                        ds_upload = null;
                        ds_upload = objdb.ByProcedure("USP_Trn_ExcelUploadMilkCollection",
                        new string[] { "Flag"
                                                ,"Office_Id"
                                                ,"V_SocietyName"
                                                ,"Dt_Date"
                                                ,"V_Shift"
                                                ,"I_Producer_ID"
                                                ,"I_MilkSupplyQty"
                                                ,"V_MilkType"
                                                ,"V_Review"
                                                ,"Quality"
                                                ,"Fat"
                                                ,"CLR"
                                                ,"SNF"
                                                ,"Rate"
                                                ,"Amount"
                                                ,"TotalSNFInKg"
                                                ,"TotalFatInKg"
                                                ,"CreatedBy"
                                                ,"CreatedBy_IP"
                                                ,"Remark" 
                                                ,"EntryMode"
                                            },
                        new string[] { "1"
                                                ,ViewState["Office_ID"].ToString()
                                                ,txtOfficeName.Text.ToString()
                                                ,Convert.ToDateTime(dt.Rows[i]["Date"].ToString(), cult).ToString("yyyy/dd/MM")
                                                ,V_Shift.ToString()
                                                ,ds_producer.Tables[0].Rows[0]["ProducerId"].ToString()
                                                ,dt.Rows[i]["Qty(Ltr)"].ToString()
                                                ,V_MilkType.ToString()
                                                ,""
                                                ,"Good"
                                                ,dt.Rows[i]["Fat%"].ToString()
                                                ,dt.Rows[i]["CLR"].ToString()
                                                ,dt.Rows[i]["SNF%"].ToString()
                                                ,dt.Rows[i]["Rate/Ltr"].ToString()
                                                ,dt.Rows[i]["Amount"].ToString()
                                                ,"0.0"
                                                ,"0.0"
                                                ,ViewState["Emp_ID"].ToString()
                                                ,objdb2.GetLocalIPAddress() 
                                                ,""
                                                ,"REIL"

                                            }, "dataset");

                        if (ds_upload.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            ErrorSatus = " <span style='color:green'> (Inserted.) </span>";
                        }
                        else
                        {
                            ErrorSatus = "<span style='color:blue'> (Already Exist.) </span>";
                        }
                    }
                    else
                    {

                        ErrorSatus = "<span style='color:orange'> (Producer Not Found) </span>";

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