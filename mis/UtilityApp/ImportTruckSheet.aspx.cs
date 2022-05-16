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

public partial class mis_UtilityApp_ImportTruckSheet : System.Web.UI.Page
{
    DataSet dsreturn, ds = new DataSet();
    AbstApiDBApi objdb = new APIProcedure();
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

                    if (ViewState["Office_ID"].ToString() == "1")
                    {
                        //ddlOffice.Enabled = true;
                    }
                    ds = objdb.ByProcedure("SpAdminOffice",
                           new string[] { "flag" },
                           new string[] { "23" }, "dataset");
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlOffice.DataSource = ds;
                        ddlOffice.DataTextField = "Office_Name";
                        ddlOffice.DataValueField = "Office_ID";
                        ddlOffice.DataBind();
                        ddlOffice.Items.Insert(0, new ListItem("Select", "0"));

                        ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
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
            //string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
            //string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            //string FilePath = Server.MapPath(FolderPath + FileName);
            //FileUpload1.SaveAs(FilePath);

            string Extension = Path.GetExtension(FileUpload1.FileName).ToLower();
            //getting the path of the file   
            //string path = Server.MapPath("MyFolder/" + Guid.NewGuid() + "-" + FileName);
            string path = Server.MapPath("MyFolder/"+ FileName);
            //saving the file inside the MyFolder of the server  
            FileUpload1.SaveAs(path);


            if (Extension == ".xls" || Extension == ".xlsx")
            {

                Import_To_Grid(path, Extension, "Yes");
                //Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Invalid File ", "Please convert file into .xls or .xlsx");
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

                //if (rbSheetType.SelectedItem.Text == "Milk Collection")
                //{

                //    htmlStr.Append("<table class='main-heading-print' id='SalaryTable'>");
                //    htmlStr.Append("<tr>");
                //    htmlStr.Append("<th>M_SNo</th>");
                //    htmlStr.Append("<th>M_CODE</th>");
                //    htmlStr.Append("<th>M_NAME</th>");
                //    htmlStr.Append("<th>M_DATE</th>");
                //    htmlStr.Append("<th>M_SHIFT</th>");
                //    htmlStr.Append("<th>M_MILKTYPE</th>");
                //    htmlStr.Append("<th>M_QUANTITY</th>");
                //    htmlStr.Append("<th>M_FAT_PER</th>");
                //    htmlStr.Append("<th>M_SNF_PER</th>");
                //    htmlStr.Append("<th>M_CLR</th>");
                //    htmlStr.Append("<th>M_RATE_P_L</th>");
                //    htmlStr.Append("<th>M_AMOUNT</th>");
                //    htmlStr.Append("<th>M_WATER_PER</th>");
                //    htmlStr.Append("<th>M_PROTEIN</th>");
                //    htmlStr.Append("<th>M_DENSITY</th>");
                //    htmlStr.Append("<th>M_LACTOSE</th>");
                //    htmlStr.Append("<th>Status</th>");

                //    htmlStr.Append("</tr>");

                //    for (int i = 0; i < Count; i++)
                //    {
                //        htmlStr.Append("<tr class='center_align'>");
                //        htmlStr.Append("<td class='left_align'>" + (i + 1).ToString() + "</td>");
                //        htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["M_CODE"].ToString() + " </ td>");
                //        htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["M_NAME"].ToString() + "</td>");
                //        htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["M_DATE"].ToString() + "</td>");
                //        htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["M_SHIFT"].ToString() + "</td>");
                //        htmlStr.Append("<td class='left_align'>" + dt.Rows[i]["M_MILKTYPE"].ToString() + "</td>");
                //        htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["M_QUANTITY"].ToString() + "</td>");
                //        htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["M_FAT_PER"].ToString() + "</td>");
                //        htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["M_SNF_PER"].ToString() + " </ td>");
                //        htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["M_CLR"].ToString() + "</td>");
                //        htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["M_RATE_P_L"].ToString() + "</td>");
                //        htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["M_AMOUNT"].ToString() + "</td>");
                //        htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["M_WATER_PER"].ToString() + "</td>");
                //        htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["M_PROTEIN"].ToString() + "</td>");
                //        htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["M_DENSITY"].ToString() + "</td>");
                //        htmlStr.Append("<td class='right_align'>" + dt.Rows[i]["M_LACTOSE"].ToString() + "</td>");
                //        htmlStr.Append("<td class='right_align' style='color:green'> Uploaded </td>");
                //        htmlStr.Append("</tr>");

                //        /**********************/
                //        /// ------------Do Your Database Activity Here------------///                      
                //        /**********************/

                //    }


                //}
                //else if (rbSheetType.SelectedItem.Text == "Truck Sheet")
                //{
                htmlStr.Append("<table class='main-heading-print' id='SalaryTable'>");
                htmlStr.Append("<tr class=''>");
                htmlStr.Append("<th class=''>SNo.</th>");
                htmlStr.Append("<th>T_UN_CD</th>");
                htmlStr.Append("<th>T_SOC_CD</th>");
                htmlStr.Append("<th>T_DATE</th>");
                htmlStr.Append("<th>T_SHIFT</th>");
                htmlStr.Append("<th>T_BFCW_IND</th>");
                htmlStr.Append("<th>T_CATG</th>");
                htmlStr.Append("<th>T_QTY</th>");
                htmlStr.Append("<th>T_FAT</th>");
                htmlStr.Append("<th>T_SNF</th>");
                htmlStr.Append("<th>T_CLR</th>");
                htmlStr.Append("<th>Status</th>");
                htmlStr.Append("</tr>");

                for (int i = 0; i < Count; i++)
                {

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

                    string returnStatus = "";
                    dsreturn = objdb.ByProcedure("USP_Trn_ExcelUploadTruck", new string[] { "flag", "UnionOffice_ID", "T_UN_CD", "T_SOC_CD", "T_DATE", "T_SHIFT", "T_BFCW_IND", "T_CATG", "T_QTY", "T_FAT", "T_SNF", "T_CLR", "UpdatedBy", "InsertedBy" }, new string[] { "0", ViewState["Office_ID"].ToString(), dt.Rows[i]["T_UN_CD"].ToString(), dt.Rows[i]["T_SOC_CD"].ToString(), Convert.ToDateTime(dt.Rows[i]["T_DATE"].ToString(), cult).ToString("yyyy-MM-dd"), dt.Rows[i]["T_SHIFT"].ToString(), dt.Rows[i]["T_BFCW_IND"].ToString(), dt.Rows[i]["T_CATG"].ToString(), dt.Rows[i]["T_QTY"].ToString(), dt.Rows[i]["T_FAT"].ToString(), dt.Rows[i]["T_SNF"].ToString(), dt.Rows[i]["T_CLR"].ToString(), ViewState["Emp_ID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");

                    if (dsreturn.Tables.Count != 0 && dsreturn.Tables[0].Rows.Count != 0)
                    {
                        returnStatus = dsreturn.Tables[0].Rows[0]["ReturnStatus"].ToString();
                    }

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
                //}



            }
            else
            {
                htmlStr.Append("<div><p>Invalid File Format, Unable to upload it. <br/> Please recheck the file and upload again.</p></div>");
            }

            StringDiv.InnerHtml = htmlStr.ToString();

        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex.Message);
            //lblMsg.Text = ex.Message.ToString();
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Invalid Columns Names ", ex.Message.ToString());
        }
        finally
        {
            connExcel.Close();
        }
        //GridView1.Caption = Path.GetFileName(FilePath);
        //GridView1.DataSource = dt;
        //GridView1.DataBind();
    }
}