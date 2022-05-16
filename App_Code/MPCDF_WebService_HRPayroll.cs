using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;
using System.Net.Mail;


/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class MPCDF_WebService_HRPayroll : System.Web.Services.WebService
{

    string securityKey = "SFA_MPCDF-ERP";
    APIProcedure obj = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    StringBuilder sb = new StringBuilder();
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();

    public MPCDF_WebService_HRPayroll()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Login(string Key, string UserName, string Password)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string saltkey = obj.GenerateSaltKey();
                DataSet ds1 = new DataSet();
                ds1 = obj.ByProcedure("sp_Login",
                                 new string[] { "flag", "UserName" },
                                 new string[] { "0", UserName }, "dataset");
                if (ds1.Tables.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "This User is not registered." }));
                }
                else if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Login Credentials." }));
                }
                else
                {
                    if (obj.CompaireHashCode(ds1.Tables[0].Rows[0]["Password"].ToString(), Password, saltkey))
                    {
                        if (ds1.Tables[0].Rows[0]["IsActive"].ToString() == "True" || ds1.Tables[0].Rows[0]["IsActive"].ToString() == "1")
                        {
                            //Re-Generated Salt Key
                            saltkey = obj.GenerateSaltKey();

                            dt = ds1.Tables[0];
                            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                            Dictionary<string, object> row = null;
                            foreach (DataRow rs in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {
                                    row.Add(col.ColumnName, rs[col]);
                                }
                                rows.Add(row);
                            }
                            this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));
                        }
                        else
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Access denied! Kindly contact administrator." }));
                        }
                    }
                    else
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Login Credentials." }));
                    }
                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ForgetPassword(string Key, string UserName)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("sp_Login",
                         new string[] { "flag", "UserName" },
                         new string[] { "0", UserName }, "dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {
                    string otp = SendOTP(ds.Tables[0].Rows[0]["MobileNo"].ToString());

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        row.Add("OTP", otp);
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    private string SendOTP(string MobileNo)
    {
        Random random = new Random();
        string otp = random.Next(100000, 999999).ToString();

        string txtmsg = "Your One Time Password is : " + otp;

        //Your authentication key
        string authKey = "3597C1493C124F";

        //Sender ID
        string senderId = "SANCHI";

        string link = "http://mysms.mssinfotech.com/app/smsapi/index.php?key=" + authKey + "&type=unicode&routeid=288&contacts=" + MobileNo + "&senderid=" + senderId + "&msg=" + Server.UrlEncode(txtmsg);
        HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
        Stream stream = response.GetResponseStream();

        //End Sending OTP SMS
        return otp;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ForgetedUpdateNewPassword(string Key, string UserName, string Password)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();

                ds1 = obj.ByProcedure("sp_Login",
                         new string[] { "flag", "UserName" },
                         new string[] { "0", UserName }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "Invalid Credentials." }));
                }
                else
                {
                    //Update New Password by UserName
                    ds = obj.ByProcedure("sp_Login",
                             new string[] { "flag", "UserName", "Password" },
                             new string[] { "3", UserName, obj.SHA512_HASH(Password) }, "dataset");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                    }
                    else
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                    }
                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //Change Password
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ChangePassword(string Key, string UserName, string NewPassword, string OldPassword)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = new DataSet();


                ds1 = obj.ByProcedure("sp_Login",
                         new string[] { "flag", "UserName" },
                         new string[] { "0", UserName.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Credentials." }));
                }
                else
                {
                    string saltkey = obj.GenerateSaltKey();
                    if (obj.CompaireHashCode(ds1.Tables[0].Rows[0]["Password"].ToString(), OldPassword, saltkey))
                    {
                        //Update New Password by UserName prefix
                        ds = obj.ByProcedure("sp_Login",
                                 new string[] { "flag", "UserName", "Password" },
                                 new string[] { "3", UserName, obj.SHA512_HASH(NewPassword) }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                        }
                        else
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                        }
                    }
                    else
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Old Password Not Matched." }));
                    }
                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }


    //Employee Login Role
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Login_Role(string Key, string Emp_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        APIProcedure apiprocedure = new APIProcedure();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string saltkey = apiprocedure.GenerateSaltKey();
                DataSet ds1 = new DataSet();

                ds1 = apiprocedure.ByProcedure("sp_Login",
                               new string[] { "flag", "Emp_ID" },
                               new string[] { "7", Emp_ID }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Login Credentials." }));
                }
                else
                {

                    dt = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }
                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                }
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }



    //Salary Slip
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_EmpPayrollSalarySlip(string Key, string Emp_ID, string Office_ID, string Year, string Month)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSPaySlip", new string[] { "flag", "Emp_ID", "Office_ID", "Year", "MonthNo" }, new string[] { "6", Emp_ID, Office_ID, Year, Month }, "dataset");

                int i = 0;
                if (ds != null)
                {
                    i = ds.Tables.Count;
                    List<Dictionary<string, object>> rowsTable0 = new List<Dictionary<string, object>>();
                    List<Dictionary<string, object>> rowsTable1 = new List<Dictionary<string, object>>();
                    List<Dictionary<string, object>> rowsTable2 = new List<Dictionary<string, object>>();

                    Dictionary<string, object> row = null;
                    {
                        for (int j = 0; j < i; j++)
                        {
                            dt = ds.Tables[j];

                            foreach (DataRow rs in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {
                                    row.Add(col.ColumnName, rs[col]);
                                }
                                if (j == 0)
                                {
                                    rowsTable0.Add(row);
                                }
                                if (j == 1)
                                {
                                    rowsTable1.Add(row);
                                }
                                if (j == 2)
                                {
                                    rowsTable2.Add(row);
                                }
                            }
                        }
                    }

                    if ((ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0) || (ds.Tables[0].Rows.Count == 0))
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List0 = "", List1 = "", List2 = "", status = "1", Error = "No record found." }));
                    }
                    else
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List0 = rowsTable0, List1 = rowsTable1, List2 = rowsTable2, status = "1", Error = "" }));
                    }
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }


    // Personal AND Official Detail
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_MyProfile_Prsn_Office(string Key, string Emp_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Employee", new string[] { "flag", "Emp_ID" }, new string[] { "13", Emp_ID }, "dataset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {
                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    // Bank And Children Detail
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_MyProfile_Bank_Child(string Key, string Emp_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Employee", new string[] { "flag", "Emp_ID" }, new string[] { "14", Emp_ID }, "dataset");

                int i = 0;
                if (ds != null)
                {
                    i = ds.Tables.Count;
                    List<Dictionary<string, object>> rowsTable0 = new List<Dictionary<string, object>>();
                    List<Dictionary<string, object>> rowsTable1 = new List<Dictionary<string, object>>();

                    Dictionary<string, object> row = null;
                    {
                        for (int j = 0; j < i; j++)
                        {
                            dt = ds.Tables[j];

                            foreach (DataRow rs in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {
                                    row.Add(col.ColumnName, rs[col]);
                                }
                                if (j == 0)
                                {
                                    rowsTable0.Add(row);
                                }
                                if (j == 1)
                                {
                                    rowsTable1.Add(row);
                                }
                            }
                        }
                    }

                    if (ds.Tables[0].Rows.Count == 0 & ds.Tables[1].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List0 = "", List1 = "", status = "1", Error = "No record found." }));
                    }
                    else
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List0 = rowsTable0, List1 = rowsTable1, status = "1", Error = "" }));
                    }
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }


    // Nominee and Other Detail
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_MyProfile_Nominee_Other(string Key, string Emp_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Employee", new string[] { "flag", "Emp_ID" }, new string[] { "15", Emp_ID }, "dataset");

                int i = 0;
                if (ds != null)
                {
                    i = ds.Tables.Count;
                    List<Dictionary<string, object>> rowsTable0 = new List<Dictionary<string, object>>();
                    List<Dictionary<string, object>> rowsTable1 = new List<Dictionary<string, object>>();
                    List<Dictionary<string, object>> rowsTable2 = new List<Dictionary<string, object>>();

                    Dictionary<string, object> row = null;
                    {
                        for (int j = 0; j < i; j++)
                        {
                            dt = ds.Tables[j];

                            foreach (DataRow rs in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {
                                    row.Add(col.ColumnName, rs[col]);
                                }
                                if (j == 0)
                                {
                                    rowsTable0.Add(row);
                                }
                                if (j == 1)
                                {
                                    rowsTable1.Add(row);
                                }
                                if (j == 2)
                                {
                                    rowsTable2.Add(row);
                                }
                            }
                        }
                    }

                    if ((ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0))
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List0 = "", List1 = "", List2 = "", status = "1", Error = "No record found." }));
                    }
                    else
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List0 = rowsTable0, List1 = rowsTable1, List2 = rowsTable2, status = "1", Error = "" }));
                    }
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Tour_EmpWisetourList(string Key, string Emp_ID, string Year, string Month)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHRTourApplication", new string[] { "flag", "Emp_ID", "Year", "Month" }, new string[] { "0", Emp_ID, Year, Month }, "dataset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_TourList(string Key, string Office_ID, string Year, string MonthNo)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHRTourApplication", new string[] { "flag", "Office_ID", "Year", "MonthNo" }, new string[] { "1", Office_ID, Year, MonthNo }, "dataset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_TourRequests(string Key, string TourApproveAuthority, string Year, string MonthNo)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHRTourApplication", new string[] { "flag", "TourApproveAuthority", "Year", "MonthNo" }, new string[] { "3", TourApproveAuthority, Year, MonthNo }, "dataset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }


    /****************Apply Tour******************************/
    /****************Tour Type List******************************/
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Tour_TourTypeList(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHRTourApplication", new string[] { "flag" }, new string[] { "4" }, "dataset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    /**********************Apply Tour **************************/
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Tour_ApplyTour(string Key, string Emp_ID, string Office_ID, string TourType, string TourApproveAuthority, string FromDate, string ToDate, string TourRemark, string TourDocument, string TourDay)
    {
        string GUID = Guid.NewGuid().ToString();
        var DocPathWithName = "";
        var DocFullPathWithName = "";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string FileFullPath = "";
                if (TourDocument != "")
                {
                    DocPathWithName = "../HR/UploadDoc/TourDoc/" + GUID + "_" + Emp_ID + ".jpg";
                    DocFullPathWithName = Server.MapPath(@"~/mis/HR/UploadDoc/TourDoc/" + GUID + "_" + Emp_ID);
                    FileFullPath = DocFullPathWithName + ".jpg";
                    byte[] imgByteArray = Convert.FromBase64String(TourDocument);
                    File.WriteAllBytes(FileFullPath, imgByteArray);
                }
                ds = obj.ByProcedure("Sp_WSHRTourApplication"
                                    , new string[] { "flag", "Emp_ID", "Office_ID", "TourType", "TourApproveAuthority", "TourFromDate", "TourToDate", "TourDescription", "TourDocument", "TourStatus", "IsActive", "TourDay" }
                                    , new string[] { "5", Emp_ID.ToString(), Office_ID, TourType, TourApproveAuthority, Convert.ToDateTime(FromDate, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(ToDate, cult).ToString("yyyy/MM/dd"), TourRemark, DocPathWithName, "Pending", "1", TourDay }, "datatset");

                int i = 0;
                if (ds != null)
                {
                    i = ds.Tables.Count;
                    List<Dictionary<string, object>> rowsTable0 = new List<Dictionary<string, object>>();
                    List<Dictionary<string, object>> rowsTable1 = new List<Dictionary<string, object>>();

                    Dictionary<string, object> row = null;
                    {
                        for (int j = 0; j < i; j++)
                        {
                            dt = ds.Tables[j];

                            foreach (DataRow rs in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {
                                    row.Add(col.ColumnName, rs[col]);
                                }
                                if (j == 0)
                                {
                                    rowsTable0.Add(row);
                                }
                                if (j == 1)
                                {
                                    rowsTable1.Add(row);
                                }
                            }
                        }
                    }

                    if (ds.Tables[0].Rows.Count == 0 & ds.Tables[1].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List0 = "", List1 = "", status = "1", Error = "No record found." }));
                    }
                    else
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List0 = rowsTable0, List1 = rowsTable1, status = "1", Error = "" }));
                    }
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Tour_ListOfApprovalAuthority(string Key, string Emp_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "Emp_ID" },
                 new string[] { "23", Emp_ID }, "datatset");  //  Emp_ID pass in this ,for removing login person name from the list.

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Tour_UpdateTour(string Key, string TourId, string TourStatus, string RemarkByApprovalAuth, string TourApprovalOrderNo, string TourApprovalOrderDate, string TourApprovalOrderFile)
    {
        string GUID = Guid.NewGuid().ToString();
        var DocPathWithName = "";
        var DocFullPathWithName = "";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string FileFullPath = "";
                if (TourApprovalOrderFile != "")
                {
                    DocPathWithName = "../HR/UploadDoc/TourApproveDoc/" + GUID + "_" + TourId + ".jpg";
                    DocFullPathWithName = Server.MapPath(@"~/mis/HR/UploadDoc/TourApproveDoc/" + GUID + "_" + TourId);
                    FileFullPath = DocFullPathWithName + ".jpg";
                    byte[] imgByteArray = Convert.FromBase64String(TourApprovalOrderFile);
                    File.WriteAllBytes(FileFullPath, imgByteArray);
                }
                string OrderDate = "";
                if (TourApprovalOrderDate != "")
                {
                    OrderDate = Convert.ToDateTime(TourApprovalOrderDate, cult).ToString("yyyy/MM/dd");
                }
                ds = obj.ByProcedure("Sp_WSHRTourApplication", new string[] { "flag", "TourId", "TourStatus", "RemarkByApprovalAuth", "TourApprovalOrderNo", "TourApprovalOrderDate", "TourApprovalOrderFile" },
                 new string[] { "6", TourId, TourStatus, RemarkByApprovalAuth, TourApprovalOrderNo, OrderDate, DocPathWithName }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Tour_CheckAlreadyApplied(string Key, string Emp_ID, string TourFromDate, string TourToDate)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHRTourApplication", new string[] { "flag", "Emp_ID", "TourFromDate", "TourToDate" },
                  new string[] { "7", Emp_ID, Convert.ToDateTime(TourFromDate, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(TourToDate, cult).ToString("yyyy/MM/dd") }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    /******** Leave Type ********************/
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_LeaveTypeList(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR",
                        new string[] { "flag" },
                                new string[] { "8" }, "dataset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_ApplyLeave(string Key, string Emp_ID, string Office_ID, string LeaveType, string LeaveApproveAuthority, string FromDate, string ToDate, string LeaveRemark, string LeaveDocument, string LeaveDay)
    {
        string GUID = Guid.NewGuid().ToString();
        var DocPathWithName = "";
        var DocFullPathWithName = "";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            int EmailBlockStatus = 0;
            try
            {
                string FileFullPath = "";
                if (LeaveDocument != "")
                {
                    DocPathWithName = "../HR/UploadDoc/LeaveDoc/" + GUID + "_" + Emp_ID + ".jpg";
                    DocFullPathWithName = Server.MapPath(@"~/mis/HR/UploadDoc/LeaveDoc/" + GUID + "_" + Emp_ID);
                    FileFullPath = DocFullPathWithName + ".jpg";
                    byte[] imgByteArray = Convert.FromBase64String(LeaveDocument);
                    File.WriteAllBytes(FileFullPath, imgByteArray);
                }
                ds = obj.ByProcedure("Sp_WSHR_Leave_New"
                                    , new string[] { "flag", "Emp_ID", "Office_ID", "LeaveType", "LeaveApproveAuthority", "LeaveFromDate", "LeaveToDate", "LeaveRemark", "LeaveDocument", "LeaveStatus", "IsActive", "LeaveDay" }
                                    , new string[] { "0", Emp_ID.ToString(), Office_ID, LeaveType, LeaveApproveAuthority, Convert.ToDateTime(FromDate, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(ToDate, cult).ToString("yyyy/MM/dd"), LeaveRemark, DocPathWithName, "Pending", "1", LeaveDay }, "datatset");

                int i = 0;
                if (ds != null)
                {
                    i = ds.Tables.Count;
                    List<Dictionary<string, object>> rowsTable0 = new List<Dictionary<string, object>>();
                    List<Dictionary<string, object>> rowsTable1 = new List<Dictionary<string, object>>();

                    Dictionary<string, object> row = null;
                    {
                        for (int j = 0; j < i; j++)
                        {
                            dt = ds.Tables[j];

                            foreach (DataRow rs in dt.Rows)
                            {
                                row = new Dictionary<string, object>();
                                foreach (DataColumn col in dt.Columns)
                                {
                                    row.Add(col.ColumnName, rs[col]);
                                }
                                if (j == 0)
                                {
                                    rowsTable0.Add(row);
                                }
                                if (j == 1)
                                {
                                    rowsTable1.Add(row);
                                }
                            }
                        }
                    }

                    /*********Get Leave Type**************/
                    string LeaveTypeName;
                    DataSet ds3;
                    ds3 = obj.ByProcedure("Sp_WSHR_Leave_New",
                        new string[] { "flag", "LeaveType_ID" },
                        new string[] { "19", LeaveType.ToString() }, "datatset");
                    if (ds3.Tables[0].Rows.Count > 0)
                    {
                        LeaveTypeName = ds3.Tables[0].Rows[0]["Leave_Type"].ToString();
                    }
                    else
                    {
                        LeaveTypeName = "";
                    }

                    /*********Get MobileNo of approval authority and send SMS**************/
                    DataSet ds2;
                    ds2 = obj.ByProcedure("Sp_WSHR_Leave_New",
                        new string[] { "flag", "Emp_ID", "LeaveApproveAuthority" },
                        new string[] { "17", Emp_ID.ToString(), LeaveApproveAuthority }, "datatset");
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        /*******Message For Employee********/
                        string EmpMobileNo = ds2.Tables[0].Rows[0]["EmpMobile"].ToString();
                        string EmpName = ds2.Tables[0].Rows[0]["EmpName"].ToString();
                        if (EmpMobileNo != "9893098930" && EmpMobileNo != "0000000000" && EmpMobileNo != null)
                        {
                            string Empmessage = "Your " + LeaveTypeName.ToString() + " request from " + Convert.ToDateTime(FromDate, cult).ToString("dd/MM/yyyy") + " to " + Convert.ToDateTime(ToDate, cult).ToString("dd/MM/yyyy") + " has been sent for approval.";
                            string link = "http://mysms.mssinfotech.com/app/smsapi/index.php?key=3597C1493C124F&type=unicode&routeid=2&contacts=" + EmpMobileNo + "&senderid=SANCHI&msg=" + Empmessage;
                            HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
                            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                            Stream stream = response.GetResponseStream();
                        }
                        /***************/
                        /*******Message For Authority********/
                        string AuthorityMobileNo = ds2.Tables[0].Rows[0]["AuthMobile"].ToString();
                        if (AuthorityMobileNo != "9893098930" && AuthorityMobileNo != "0000000000" && AuthorityMobileNo != null)
                        {
                            string Authoritymessage = EmpName + " has applied " + LeaveTypeName.ToString() + " from " + Convert.ToDateTime(FromDate, cult).ToString("dd/MM/yyyy") + " to " + Convert.ToDateTime(ToDate, cult).ToString("dd/MM/yyyy") + ".";
                            string link2 = "http://mysms.mssinfotech.com/app/smsapi/index.php?key=3597C1493C124F&type=unicode&routeid=2&contacts=" + AuthorityMobileNo + "&senderid=SANCHI&msg=" + Authoritymessage;
                            HttpWebRequest request2 = WebRequest.Create(link2) as HttpWebRequest;
                            HttpWebResponse response2 = request2.GetResponse() as HttpWebResponse;
                            Stream stream2 = response2.GetResponseStream();
                        }
                        /***************/
                        /***************/


                        if (ds != null && ds.Tables[2].Rows.Count != 0)
                        {
                            EmailBlockStatus = 1;

                            string Authoritymessage2 = EmpName + " has applied " + LeaveTypeName.ToString() + " from " + Convert.ToDateTime(FromDate, cult).ToString("dd/MM/yyyy") + " to " + Convert.ToDateTime(ToDate, cult).ToString("dd/MM/yyyy") + ".";

                            sb.Append("<div class='table-responsive'>");
                            sb.Append("<table class='table table-bordered table-striped Grid earning-table' style='width: 100%;'>");
                            sb.Append("<tbody>");
                            sb.Append("<tr>");
                            sb.Append("<th colspan='2'>");
                            sb.Append("<p>" + Authoritymessage2.ToString() + "</p>");

                            sb.Append("</th>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td>");

                            sb.Append("<p><a href='http://erpdairy.com/mis/HR/HREmpLeaveRequestsEmail.aspx?LeaveStatus=Approved&FY=" + ds.Tables[2].Rows[0]["FY"].ToString() + "&LeaveId=" + ds.Tables[2].Rows[0]["LID"].ToString() + "&LeaveType=" + ds.Tables[2].Rows[0]["TypeOfLeave"].ToString() + "&EmpID=" + obj.Encrypt(ds.Tables[2].Rows[0]["LeaveApprovalAuth"].ToString()) + "&Office=" + obj.Encrypt(ds.Tables[2].Rows[0]["Office"].ToString()) + "'>Click Here To Approve Leave</a></p>");


                            sb.Append("</td>");
                            sb.Append("<td>");

                            sb.Append("<p><a href='http://erpdairy.com/mis/HR/HREmpLeaveRequestsEmail.aspx?LeaveStatus=Rejected&FY=" + ds.Tables[2].Rows[0]["FY"].ToString() + "&LeaveId=" + ds.Tables[2].Rows[0]["LID"].ToString() + "&LeaveType=" + ds.Tables[2].Rows[0]["TypeOfLeave"].ToString() + "&EmpID=" + obj.Encrypt(ds.Tables[2].Rows[0]["LeaveApprovalAuth"].ToString()) + "&Office=" + obj.Encrypt(ds.Tables[2].Rows[0]["Office"].ToString()) + "'>Click Here To Reject Leave</a></p>");

                            sb.Append("</td>");
                            sb.Append("</tr>");
                            sb.Append("</tbody>");
                            sb.Append("</table>");

                            MailMessage mail = new MailMessage();
                            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                            SmtpServer.EnableSsl = false;

                            mail.From = new MailAddress("carempcdf@gmail.com");
                            mail.To.Add(ds.Tables[2].Rows[0]["AuthEmail"].ToString());
                            mail.Subject = "Leave Application";

                            mail.IsBodyHtml = true;
                            string htmlBody;
                            htmlBody = "<html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /> <title></title><link href='https://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet'> <style> .Grid td {             padding: 3px !important;         }              .Grid td input {                 padding: 3px 3px !important;                 text-align: right !important;                 font-size: 12px !important;                 height: 26px !important;             }          .Grid th {             text-align: center;         }          .ss {             text-align: left !important;         }          .bgcolor {             background-color: #eeeeee !important;         }          .box {             min-height: initial !important;         } .table-striped > tbody > tr:nth-of-type(odd) {   background-color: #f9f9f9; } .content {min-height: 700px; } .box { position: relative;border-radius: 3px;background: #ffffff;border-top: 3px solid #d2d6de;margin-bottom: 20px; width: 100%;box-shadow: 0 1px 1px rgba(0,0,0,0.1);box-shadow: none;border-top: none; }.table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {border: 1px solid #e1e1e1;}.text-center h3 {font-size: 15px; font-family: monospace;}.table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {padding: 0px 2px;}#subheading-salary {font-size: 13px;}.salary-logo {-webkit-filter: grayscale(100%);filter: grayscale(100%);width: 40px;         }          .printbutton {             border-top: 1px dashed #838383;             margin-top: 5px;             padding-top: 5px;         }          table h4 {             font-size: 15px;         }          .table {             margin-bottom: 5px;         }          th, td, h3 {             text-transform: uppercase !important;         }         .watermark {   width: 300px;   height: 100px;   display: block;   position: relative; }  .watermark::after {   content:'';  background:url('http://erpdairy.com/mis/image/sanchi_logo_blue.png');  opacity: 0.2;   top: 0;   left: 0;   bottom: 0;   right: 0;   position: absolute;   z-index: -1;   }</style></head><body style='font-family: ' open sans', sans-serif;'>" + sb.ToString() + "</body></html>";
                            mail.Body = htmlBody;
                            //SmtpServer.Port = 587;
                            //SmtpServer.Credentials = new System.Net.NetworkCredential("carempagro@gmail.com", "mpagro@123");
                            //SmtpServer.EnableSsl = true;
                            //SmtpServer.Send(mail);
                            SmtpServer.Credentials = new System.Net.NetworkCredential("carempcdf@gmail.com", "sfa@2365");
                            SmtpServer.EnableSsl = true;
                            SmtpServer.Send(mail);
                        }
                        /***************/

                    }

                    if (ds.Tables[0].Rows.Count == 0 & ds.Tables[1].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List0 = "", List1 = "", status = "1", Error = "No record found." }));
                    }
                    else
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List0 = rowsTable0, List1 = rowsTable1, status = "1", Error = "" }));
                    }
                }
            }
            catch (Exception ex)
            {
                if (EmailBlockStatus > 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List0 = "", List1 = "", status = "1", Error = "" }));
                }
                else
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
                }
            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_EmpWiseLeaveStatus(string Key, string Emp_ID, string FinancialYear)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave", new string[] { "flag", "Emp_ID", "FinancialYear" },
                  new string[] { "2", Emp_ID, FinancialYear }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {
                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_EmpWiseBalanceLeave(string Key, string Emp_ID, string Financial_Year)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "Emp_ID", "Financial_Year" },
                  new string[] { "1", Emp_ID, Financial_Year }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_EmpWiseActionForLeave(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave", new string[] { "flag" },
                  new string[] { "3" }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_EmpWiseBalanceLeaveDetail(string Key, string Emp_ID, string Financial_Year, string LeaveType_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "Emp_ID", "Financial_Year", "LeaveType_ID" },
                  new string[] { "18", Emp_ID, Financial_Year, LeaveType_ID }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }




    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_CheckAppliedOptionalHolidays(string Key, string Emp_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "Emp_ID" },
                  new string[] { "3", Emp_ID }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_CheckAlreadyAppliedLeave(string Key, string Emp_ID, string LeaveFromDate, string LeaveToDate)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "Emp_ID", "LeaveFromDate", "LeaveToDate" },
                  new string[] { "6", Emp_ID, Convert.ToDateTime(LeaveFromDate, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(LeaveToDate, cult).ToString("yyyy/MM/dd") }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod(Description="List Of Leave Approval Authority")]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_ListOfApprovalAuthority(string Key, string Emp_ID, string LeaveType_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "Emp_ID","LeaveType_ID"},
                 new string[] { "2", Emp_ID, LeaveType_ID }, "datatset");  //  Emp_ID pass in this ,for removing login person name from the list.

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod(Description="Pending Leave Requests")]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_ListOfPendingLeaveRequest(string Key, string Emp_ID, string Office_ID, string Financial_Year)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "Office_ID", "LeaveApproveAuthority", "Financial_Year" },
                 new string[] { "7", Office_ID, Emp_ID, Financial_Year }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_DetailOfPendingLeave(string Key, string LeaveId)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "LeaveId" },
                 new string[] { "8", LeaveId }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_NoOfDays_LeaveType(string Key, string LeaveType_ID, string Financial_Year)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "LeaveType_ID", "Financial_Year" },
                 new string[] { "9", LeaveType_ID, Financial_Year }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_StatusOfHalf_FullDay(string Key, string LeaveId)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "LeaveId" },
                 new string[] { "10", LeaveId }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_DayOfHolidayLeave(string Key, string LeaveId)// Day name of leave date
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "LeaveId" },
                 new string[] { "11", LeaveId }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_IsRegularHoliday(string Key, string LeaveId)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "LeaveId" },
                 new string[] { "12", LeaveId }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_CalculateBalanceLeave(string Key, string Emp_ID, string LeaveType, string FinancialYear, string TotalAllowedLeave, string TotalTakenLeave)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "Emp_ID", "LeaveType", "FinancialYear", "TotalAllowedLeave", "TotalTakenLeave" },
                 new string[] { "13", Emp_ID, LeaveType, FinancialYear, TotalAllowedLeave, TotalTakenLeave }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

        [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_UpdateLeaveStatus(string Key, string LeaveId, string LeaveStatus, string RemarkByApprovalAuth, string LeaveApprovalOrderNo, string LeaveApprovalOrderDate, string LeaveApprovalOrderFile)
    {
        string GUID = Guid.NewGuid().ToString();
        var DocPathWithName = "";
        var DocFullPathWithName = "";
        DataSet ds,ds1 = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                /**************************/
                string TakenLeave = "";
                int DayCount = 0;
                decimal TotalTakenLeave = 0;


                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "LeaveId" }, new string[] { "24", LeaveId }, "datatset");
                string LeaveType_ID = ds.Tables[0].Rows[0]["LeaveType"].ToString();
                string Emp_ID = ds.Tables[0].Rows[0]["Emp_ID"].ToString();
                string Financial_Year = ds.Tables[0].Rows[0]["FinancialYear"].ToString();

                /**************************/
                ds = obj.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveType_ID", "Financial_Year", "Emp_ID" },
            new string[] { "38", LeaveType_ID, Financial_Year, Emp_ID }, "datatset");

                string financialYear = ds.Tables[0].Rows[0]["Financial_Year"].ToString();
                string LeaveDay1 = ds.Tables[0].Rows[0]["Leave_Days"].ToString();

                if (LeaveStatus == "Approved")
                {
                    ds1 = obj.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId" },
                new string[] { "8", LeaveId }, "datatset");
                    if (ds1 != null && ds1.Tables.Count != 0)
                    {
                        if (ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "First Half" || ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "Second Half" || ds1.Tables[0].Rows[0]["LeaveDay"].ToString() == "Half Day")
                        {
                            TakenLeave = "0.5";
                            TotalTakenLeave = Convert.ToDecimal(TakenLeave.ToString());
                        }
                        else
                        {
                            int count = ds1.Tables[1].Rows.Count;
                            for (int i = 0; i < count; i++)
                            {
                                string DayName = ds1.Tables[1].Rows[i]["DayName"].ToString();
                                string Leave_Date = ds1.Tables[1].Rows[i]["LeaveDate"].ToString();
                                if (DayName == "Sunday")
                                {
                                    DayCount = DayCount;
                                }
                                else
                                {
                                    int status = 1;
                                    DataSet ds2 = obj.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "LeaveId" },
                 new string[] { "21", LeaveId }, "datatset");
                                    if (ds1 != null)
                                    {
                                        int leaveCount = ds2.Tables[0].Rows.Count;
                                        if (ds2 != null && ds2.Tables[0].Rows.Count != 0)
                                        {
                                            for (int j = 0; j < leaveCount; j++)
                                            {
                                                string Holiday_Date = ds2.Tables[0].Rows[j]["Holiday_Date"].ToString();
                                                if (Leave_Date == Holiday_Date)
                                                {
                                                    status = 0;
                                                    break;
                                                }
                                                else
                                                {
                                                    status = 1;
                                                }
                                            }
                                        }
                                        if (status == 1)
                                        {
                                            DayCount++;
                                        }
                                        else
                                        {
                                            DayCount = DayCount;
                                        }
                                    }
                                }
                            }
                            TotalTakenLeave = DayCount;
                            //TotalTakenLeave = Convert.ToDecimal(ds1.Tables[0].Rows[0]["TakenLeave"].ToString());
                        }
                    }

                }
                else
                {
                    TotalTakenLeave = 0;
                }
                obj.ByProcedure("SpHRLeaveApplication", new string[] { "flag", "Emp_ID", "LeaveType", "TotalAllowedLeave", "TotalTakenLeave", "FinancialYear" },
                new string[] { "2", Emp_ID, LeaveType_ID, LeaveDay1, TotalTakenLeave.ToString(), financialYear }, "datatset");

                /**************************/
                string FileFullPath = "";
                if (LeaveApprovalOrderFile != "")
                {
                    DocPathWithName = "../HR/UploadDoc/LeaveApproveDoc/" + GUID + "_" + LeaveId + ".jpg";
                    DocFullPathWithName = Server.MapPath(@"~/mis/HR/UploadDoc/LeaveApproveDoc/" + GUID + "_" + LeaveId);
                    FileFullPath = DocFullPathWithName + ".jpg";
                    byte[] imgByteArray = Convert.FromBase64String(LeaveApprovalOrderFile);
                    File.WriteAllBytes(FileFullPath, imgByteArray);
                }
                string OrderDate = "";
                if (LeaveApprovalOrderDate != "")
                {
                    OrderDate = Convert.ToDateTime(LeaveApprovalOrderDate, cult).ToString("yyyy/MM/dd");
                }
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "LeaveId", "LeaveStatus", "RemarkByApprovalAuth", "LeaveApprovalOrderNo", "LeaveApprovalOrderDate", "LeaveApprovalOrderFile" },
                 new string[] { "14", LeaveId, LeaveStatus, RemarkByApprovalAuth, LeaveApprovalOrderNo, OrderDate, DocPathWithName }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }
	
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_ListOfLeaveAppliedDate(string Key, string Emp_ID, string LeaveFromDate, string LeaveToDate)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "Emp_ID", "LeaveFromDate", "LeaveToDate" },
                  new string[] { "15", Emp_ID, Convert.ToDateTime(LeaveFromDate, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(LeaveToDate, cult).ToString("yyyy/MM/dd") }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_ListOfAppRejLeave(string Key, string FinancialYear, string LeaveApproveAuthority)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "FinancialYear", "LeaveApproveAuthority" },
                 new string[] { "16", FinancialYear, LeaveApproveAuthority }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

  /*********************Upcoming Birthdays in next 30 days***************************/
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Birthday_Next30Days(string Key, string Office_ID, string Emp_Dob)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        try
        {
            ds = obj.ByProcedure("Sp_WSHR",
                           new string[] { "flag", "Office_ID", "Emp_Dob" },
                           new string[] { "17", Office_ID, Convert.ToDateTime(Emp_Dob, cult).ToString("yyyy/MM/dd") }, "dataset");

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "" }));
            }
            else
            {
                dt = ds.Tables[0];
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row = null;
                foreach (DataRow rs in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, rs[col]);
                    }
                    rows.Add(row);
                }

                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
            }
        }
        catch (Exception ex)
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
        }
    }
	 /******* Tour List (Approved & Rejected) ************/
    [WebMethod(Description = "Tour List (Approved & Rejected) ")]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Tour_ListOfAppRejTour(string Key, string Year, string LeaveApproveAuthority, string Month)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHRTourApplication", new string[] { "flag", "Year", "TourApproveAuthority", "MonthNo" },
                 new string[] { "8", Year, LeaveApproveAuthority, Month }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }


    /******** Is Employee Approval Authority ********************/
    [WebMethod(Description = "Check Is Employee Approval Authority Or Not")]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_IsEmpApprovalAuthority(string Key, string LeaveApproveAuthority)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave",
                        new string[] { "flag", "LeaveApproveAuthority" },
                                new string[] { "6", LeaveApproveAuthority }, "dataset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod(Description = "Forward Leave To Other Officer")]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_UpdateLeaveForward(string Key, string LeaveId, string LeaveStatus, string RemarkByApprovalAuth, string ApprovalAuthority, string NewApprovalAuthority)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave", new string[] { "flag", "LeaveID", "RemarkByApprovalAuth", "Emp_ID", "LeaveApproveAuthority" },
                 new string[] { "7", LeaveId, RemarkByApprovalAuth, ApprovalAuthority, NewApprovalAuthority }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }
    /*****List Of Forwarding Details*********/
    [WebMethod(Description = "List Of Forwarding detail of Leave")]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_ForwardingDetails(string Key, string LeaveId, string Login_Emp_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "LeaveId", "Emp_ID"},
                 new string[] { "20", LeaveId, Login_Emp_ID }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    /*****Is Visible Approval Box Or Not*********/
    [WebMethod(Description = "Approval & Forwarding Section Visible Or Not (Active = Visible, Inactive=Not Visible)")]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_ApprovalSectionVisibility(string Key, string LeaveId, string Login_Emp_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "LeaveId", "Emp_ID" },
                 new string[] { "21", LeaveId, Login_Emp_ID }, "datatset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_Leave_ListOfForwardingAuthority(string Key, string LeaveBy_Emp_ID, string ForwardBy_Emp_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "Emp_ID", "LeaveApproveAuthority" },
                 new string[] { "22", LeaveBy_Emp_ID, ForwardBy_Emp_ID }, "datatset");  //  Emp_ID pass in this ,for removing login person name from the list.

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));  // Able to apply on this date.
                }
                else
                {

                    dt = ds.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            row.Add(col.ColumnName, rs[col]);
                        }
                        rows.Add(row);
                    }

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
                }
            }
            catch (Exception ex)
            {

                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));

            }
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }


}