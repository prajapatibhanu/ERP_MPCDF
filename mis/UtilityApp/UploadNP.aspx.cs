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

public partial class mis_UtilityApp_UploadNP : System.Web.UI.Page
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
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDate.Attributes.Add("readonly", "readonly");
                   

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
                    if (objdb2.Office_ID() == "2")
                    {
                        ddlBillingCycle.SelectedValue = "5 days";
                    }
                    else
                    {
                        ddlBillingCycle.SelectedValue = "10 days";
                    }
                    txtDate_TextChanged(sender, e);

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
        StringDiv.InnerHtml = "";
        try
        {
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            //cmdExcel.CommandText = "SELECT * From [" + SheetName + "] order by 1 asc, 2 desc";
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "] WHERE [L_SOC_CD] IS NOT NULL";
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
                htmlStr.Append("<th>L_SOC_CD</th>");
                htmlStr.Append("<th>L_LOAN_CD</th>");
                htmlStr.Append("<th>L_AMOUNT</th>");
                htmlStr.Append("<th>L_REMARK</th>");
                htmlStr.Append("<th>Status</th>");

                htmlStr.Append("</tr>");

                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<tr class='center_align'>");
                    htmlStr.Append("<td class='left_align'>" + (i + 1).ToString() + "</td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["L_SOC_CD"].ToString() + " </ td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["L_LOAN_CD"].ToString() + " </ td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["L_AMOUNT"].ToString() + " </ td>");
                    htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["L_REMARK"].ToString() + "</td>");

                    string ErrorSatus = "";
					string flag = "";
                    if(objdb2.Office_ID() == "4")
                    {
                        flag = "13";
                    }
                    else
                    {
                        flag = "13";
                    }
                    ds_producer = objdb.ByProcedure("Usp_MilkCollectionRoutWiseAdditionsDeductionsEntry",
                    new string[] { "flag", "Office_Code", "Office_Parant_ID", "BMCTankerRoot_Id", "L_LOAN_CD", "HeadAmount", "HeadRemark", "CreatedAt", "CreatedBy", "CreatedByIP", "EntryDate", "BillingPeriodFromDate", "BillingPeriodToDate" },
                    new string[] { flag, dt.Rows[i]["L_SOC_CD"].ToString(), objdb2.Office_ID(), ddlccbmcdetail.SelectedValue, dt.Rows[i]["L_LOAN_CD"].ToString(), dt.Rows[i]["L_AMOUNT"].ToString(), dt.Rows[i]["L_REMARK"].ToString(), objdb2.Office_ID(), objdb2.createdBy(), objdb2.GetLocalIPAddress(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                    if (ds_producer.Tables.Count > 0 && ds_producer.Tables[0].Rows.Count > 0)
                    {


                        htmlStr.Append("<td class='left_align'>" + ds_producer.Tables[0].Rows[0]["ErrorMsg"].ToString() + "</td>");               
                       //if (ds_producer.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                       //{
                       //    ErrorSatus = " <span style='color:green'> (Updated.) </span>";
                       //}
                       //else
                       //{
                       //    ErrorSatus = "<span style='color:blue'> (Already Exist.) </span>";
                       //}
                    }
                    else
                    {

                      //htmlStr.Append("<td class='left_align'></td>");
                      //htmlStr.Append("<td class='left_align'></td>");
                      //htmlStr.Append("<td class='left_align'></td>");
                      //htmlStr.Append("<td class='left_align'></td>");
                      //htmlStr.Append("<td class='left_align'></td>");
                      //htmlStr.Append("<td class='left_align'>(Office Code  Found)</td>");

                    }


                    //string returnStatus = "Uploaded";
                    ///**********************/
                    //if (returnStatus.ToString() == "Uploaded")
                    //{
                    //    htmlStr.Append("<td class='right_align' style='color:green; Padding:5px !important'> Read   " + ErrorSatus + " </td>");
                    //}
                    //else if (returnStatus.ToString() == "Updated")
                    //{
                    //    htmlStr.Append("<td class='right_align' style='color:blue; Padding:5px !important'> Read   " + ErrorSatus + " </td>");
                    //}
                    //else
                    //{
                    //    htmlStr.Append("<td class='right_align' style='color:red; Padding:5px !important'> Not Uploaded  " + ErrorSatus + " </td>");
                    //}

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
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (txtDate.Text != "")
            {
                string BillingCycle = ddlBillingCycle.SelectedItem.Text;
                string[] DatePart = txtDate.Text.Split('/');
                int Day = int.Parse(DatePart[0]);
                int Month = int.Parse(DatePart[1]);
                int Year = int.Parse(DatePart[2]);
                string SelectedFromDate = "";
                string SelectedToDate = "";
                if (BillingCycle == "5 days")
                {
                    if (Day >= 1 && Day < 6)
                    {
                        SelectedFromDate = "01";
                        SelectedToDate = "05";
                    }
                    else if (Day > 5 && Day < 11)
                    {
                        SelectedFromDate = "6";
                        SelectedToDate = "10";
                    }
                    else if (Day > 10 && Day < 16)
                    {
                        SelectedFromDate = "11";
                        SelectedToDate = "15";
                    }
                    else if (Day > 15 && Day < 21)
                    {
                        SelectedFromDate = "16";
                        SelectedToDate = "20";
                    }
                    else if (Day > 20 && Day < 26)
                    {
                        SelectedFromDate = "21";
                        SelectedToDate = "25";
                    }
                    else if (Day > 25 && Day <= 31)
                    {
                        SelectedFromDate = "26";
                        if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12)
                        {
                            SelectedToDate = "31";
                        }
                        else if (Month == 2)
                        {
                            SelectedToDate = "28";
                        }
                        else
                        {
                            SelectedToDate = "30";
                        }

                    }
                    SelectedFromDate = SelectedFromDate + "/" + Month + '/' + Year;
                    SelectedToDate = SelectedToDate + "/" + Month + '/' + Year;

                }
                else
                {
                    if (Day >= 1 && Day < 11)
                    {
                        SelectedFromDate = "01";
                        SelectedToDate = "10";
                    }
                    else if (Day > 10 && Day < 21)
                    {
                        SelectedFromDate = "11";
                        SelectedToDate = "20";
                    }
                    else if (Day > 20 && Day <= 31)
                    {
                        SelectedFromDate = "21";
                        if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12)
                        {
                            SelectedToDate = "31";
                        }
                        else if (Month == 2)
                        {
                            SelectedToDate = "28";
                        }
                        else
                        {
                            SelectedToDate = "30";
                        }
                    }

                    SelectedFromDate = SelectedFromDate + "/" + Month + '/' + Year;
                    SelectedToDate = SelectedToDate + "/" + Month + '/' + Year;
                }
                txtFdt.Text = Convert.ToDateTime(SelectedFromDate, cult).ToString("dd/MM/yyyy");
                txtTdt.Text = Convert.ToDateTime(SelectedToDate, cult).ToString("dd/MM/yyyy");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void ddlBillingCycle_TextChanged(object sender, EventArgs e)
    {
        txtDate_TextChanged(sender, e);
    }
}