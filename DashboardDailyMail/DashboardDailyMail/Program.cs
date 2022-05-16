using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DashboardDailyMail
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection();
            //con.ConnectionString = "Data Source=.;Initial Catalog=DB_MPCDF_ERP;Integrated Security=True";
            con.ConnectionString = "Data Source=SFAVM2\\SQLEXPRESS;Initial Catalog=DB_MPCDF_ERP;User ID=sa;Password=SFASQL@1920";
            con.Open();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("USP_Trn_UnionwiseProgressReport", con);
            cmd.Parameters.Add(new SqlParameter("@flag", "1"));
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(ds);
            StringBuilder sb = new StringBuilder();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("carempcdf@gmail.com");
            mail.ReplyTo = new MailAddress("carempcdf@gmail.com");
            //mail.To.Add("deshmukhhimani26@gmail.com");
            mail.To.Add("shamim.iasmp@gmail.com,sanchi.bhopal@gmail.com,sanchimsids@gmail.com, ujjaindairy@gmail.com, sanchibkdssagar@gmail.com, jdssanchi@gmail.com, gwaliordairy@gmail.com");
            mail.CC.Add("ho.mpcdf@gmail.com, ho.mpcdf@nic.in, himanshu61@gmail.com, sambhav2091@gmail.com");
            mail.Bcc.Add("deshmukhhimani26@gmail.com");
            mail.Subject = "Union Wise Progress Report";
            mail.IsBodyHtml = true;
            string Body;
            sb.Append("<div>");
            sb.Append("<h3>Dear Sir,</h3>");
            sb.Append("<p>Progress @MPCDF Dated:" + DateTime.Now.ToString("dd/MM/yyyy") + " For your kind information.</p>");
            sb.Append("<table style='width:100%;'>");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style='padding-top : 10px;'>");
                    sb.Append("<table border='1' style='border-collapse : collapse; width:100%;padding : 5px;'>");
                    sb.Append("<tr>");
                    sb.Append("<th colspan='6' style='text-align : center;'>MCMS</th>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<th style='width : 5%;'>Sr.No</th>");
                    sb.Append("<th>DS Name</th>");
                    sb.Append("<th>Total CC</th>");
                    sb.Append("<th>Total GatePass</th>");
                    sb.Append("<th>Reported CC</th>");
                    sb.Append("<th>Challan Entry Date</th>");
                    int MCMSCount = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < MCMSCount; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["TotalCC"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["TotalCCGatepass"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["TotalChallan"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["LastDate"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style='padding-left : 10px;padding-top : 10px;'>");
                    sb.Append("<table border='1' style='border-collapse : collapse; width:100%;padding : 5px;'>");
                    sb.Append("<tr>");
                    sb.Append("<th colspan='4' style='text-align : center;'>MARKETING</th>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<th style='width : 5%;'>Sr.No</th>");
                    sb.Append("<th>DS Name</th>");
                    sb.Append("<th>Demand Date</th>");
                    sb.Append("<th>Last Invoice Date</th>");
                    int MarketingCount = ds.Tables[1].Rows.Count;
                    for (int i = 0; i < MarketingCount; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        sb.Append("<td>" + ds.Tables[1].Rows[i]["Office_Name"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[1].Rows[i]["LastDate"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[1].Rows[i]["LastInvoiceDate"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style='padding-top : 10px;'>");
                    sb.Append("<table border='1' style='border-collapse : collapse; width:100%;padding : 5px;'>");
                    sb.Append("<tr>");
                    sb.Append("<th colspan='4' style='text-align : center;'>MILK COLLECTION(CANES COLLECTION/TRUCK SHEET/RMRD CHALLAN ENTRY)</th>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<th style='width : 5%;'>Sr.No</th>");
                    sb.Append("<th>DS Name</th>");
                    sb.Append("<th>MilkCollection (In Ltr.)</th>");
                    sb.Append("<th>Collection Date</th>");
                    int RMRDCount = ds.Tables[2].Rows.Count;
                    for (int i = 0; i < RMRDCount; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        sb.Append("<td>" + ds.Tables[2].Rows[i]["Office_Name"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[2].Rows[i]["MilkCollection"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[2].Rows[i]["LastDate"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                }
                if (ds.Tables[4].Rows.Count > 0)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style='padding-left : 10px;padding-top : 10px;'>");
                    sb.Append("<table border='1' style='border-collapse : collapse; width:100%;padding : 5px;'>");
                    sb.Append("<tr>");
                    sb.Append("<th colspan='4' style='text-align : center;'>SOCIETY BILLING</th>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<th style='width : 5%;'>Sr.No</th>");
                    sb.Append("<th>DS Name</th>");
                    sb.Append("<th>Last Bill Cycle</th>");
                    sb.Append("<th>Total Society</th>");
                    int FOCount = ds.Tables[4].Rows.Count;
                    for (int i = 0; i < FOCount; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        sb.Append("<td>" + ds.Tables[4].Rows[i]["Office_Name"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[4].Rows[i]["LastBillCycle"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[4].Rows[i]["TotalSociety"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                }



                if (ds.Tables[3].Rows.Count > 0)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style='padding-top : 10px;'>");
                    sb.Append("<p></p></hr>");
                    sb.Append("<table border='1' style='border-collapse : collapse; width:100%; padding : 5px;'>");
                    sb.Append("<tr>");
                    sb.Append("<th colspan='3' style='text-align : center;'>PLANT OPERATION</th>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<th style='width : 5%;'>Sr.No</th>");
                    sb.Append("<th>DS Name</th>");
                    sb.Append("<th>Last Date</th>");
                    int POCount = ds.Tables[3].Rows.Count;
                    for (int i = 0; i < POCount; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        sb.Append("<td>" + ds.Tables[3].Rows[i]["Office_Name"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[3].Rows[i]["LastDate"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                }
                if (ds.Tables[5].Rows.Count > 0)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style='padding-left : 10px;padding-top : 10px;'>");
                    sb.Append("<table border='1' style='border-collapse : collapse; width:100%;padding : 5px;'>");
                    sb.Append("<tr>");
                    sb.Append("<th colspan='3' style='text-align : center;'>FINANCE</th>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<th style='width : 5%;'>Sr.No</th>");
                    sb.Append("<th>DS Name</th>");
                    sb.Append("<th>Last Date</th>");
                    int FINANCECount = ds.Tables[5].Rows.Count;
                    for (int i = 0; i < FINANCECount; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        sb.Append("<td>" + ds.Tables[5].Rows[i]["Office_Name"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[5].Rows[i]["LastDate"].ToString() + "</td>");

                        sb.Append("</tr>");
                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                }

                if (ds.Tables[6].Rows.Count > 0)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style='padding-top : 10px;'>");
                    sb.Append("<table border='1' style='border-collapse : collapse; width:100%; padding : 5px;'>");
                    sb.Append("<tr>");
                    sb.Append("<th colspan='3' style='text-align : center;'>PAYROLL</th>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<th style='width : 5%;'>Sr.No</th>");
                    sb.Append("<th>DS Name</th>");
                    sb.Append("<th>Salary Month</th>");
                    int PAYROLLCount = ds.Tables[6].Rows.Count;
                    for (int i = 0; i < PAYROLLCount; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        sb.Append("<td>" + ds.Tables[6].Rows[i]["Office_Name"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[6].Rows[i]["Salary_Month"].ToString() + "</td>");

                        sb.Append("</tr>");
                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                }
                if (ds.Tables[7].Rows.Count > 0)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style='padding-top : 10px;'>");
                    sb.Append("<table border='1' style='border-collapse : collapse; width:100%;padding : 5px;'>");
                    sb.Append("<tr>");
                    sb.Append("<th colspan='4' style='text-align : center;'>MILK COLLECTION(TRUCK SHEET)</th>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<th style='width : 5%;'>Sr.No</th>");
                    sb.Append("<th>DS Name</th>");
                    sb.Append("<th>MilkCollection (In Ltr.)</th>");
                    sb.Append("<th>Collection Date</th>");
                    int TSCount = ds.Tables[7].Rows.Count;
                    for (int i = 0; i < TSCount; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        sb.Append("<td>" + ds.Tables[7].Rows[i]["Office_Name"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[7].Rows[i]["MilkCollection"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[7].Rows[i]["LastDate"].ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                }
                string URL = "http://45.114.143.126:8222/WebService.asmx/WS_Office_Wise_Last_Indent_Date";
                URL = URL + "?key=SFA_MPCDFINV";
                var request = (HttpWebRequest)WebRequest.Create(URL);

                request.Method = "GET";

                var response = (HttpWebResponse)request.GetResponse();

                string jsonString = string.Empty;

                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        jsonString = sr.ReadToEnd();
                    }
                }
                DataTable DT = JsonStringToDataTable(jsonString);
                DataTable dtGrd = new DataTable();
                dtGrd.Columns.Add("Office_Name", typeof(string));
                dtGrd.Columns.Add("LastDate", typeof(string));
                int Count = DT.Rows.Count;
                for (int i = 0; i < (Count - 5); i++)
                {
                    DataRow dr = DT.Rows[i];
                    dtGrd.Rows.Add(dr["Office_Name"].ToString(), dr["Indent_Date"].ToString());
                }
                if (dtGrd.Rows.Count > 0)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style='padding-left : 10px;padding-top : 10px;'>");
                    sb.Append("<table border='1' style='border-collapse : collapse; width:100%; padding : 5px;'>");
                    sb.Append("<tr>");
                    sb.Append("<th colspan='3' style='text-align : center;'>INVENTORY</th>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<th style='width : 5%;'>Sr.No</th>");
                    sb.Append("<th>DS Name</th>");
                    sb.Append("<th>Indent Date</th>");
                    int INVENTORY = dtGrd.Rows.Count;
                    for (int i = 0; i < INVENTORY; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        sb.Append("<td>" + dtGrd.Rows[i]["Office_Name"].ToString() + "</td>");
                        sb.Append("<td>" + dtGrd.Rows[i]["LastDate"].ToString() + "</td>");

                        sb.Append("</tr>");
                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("</tr>");

                }
                sb.Append("</table>");
                // sb.Append("<p>This email was sent from a notification-only that cannot accept incoming email. Please do not reply to this message</p>");
                sb.Append("<h4>Thanks</h4>");
                sb.Append("<h4>ERP-MPCDF</h4>");
                sb.Append("</div>");
                mail.Body = sb.ToString();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                SmtpServer.Credentials = new System.Net.NetworkCredential("carempcdf@gmail.com", "sfa@2365");

                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
        }
        static DataTable JsonStringToDataTable(string jsonString)
        {
            DataTable dt = new DataTable();
            string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
            List<string> ColumnsName = new List<string>();
            foreach (string jSA in jsonStringArray)
            {
                string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                foreach (string ColumnsNameData in jsonStringData)
                {
                    try
                    {
                        int idx = ColumnsNameData.IndexOf(":");
                        string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                        if (!ColumnsName.Contains(ColumnsNameString))
                        {
                            ColumnsName.Add(ColumnsNameString);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                    }
                }
                break;
            }
            foreach (string AddColumnName in ColumnsName)
            {
                dt.Columns.Add(AddColumnName);
            }
            foreach (string jSA in jsonStringArray)
            {
                string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = dt.NewRow();
                foreach (string rowData in RowData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                        string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                        nr[RowColumns] = RowDataString;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                dt.Rows.Add(nr);
            }
            return dt;
        }
    }

}
