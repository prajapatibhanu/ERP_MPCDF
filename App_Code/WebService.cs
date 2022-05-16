using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using System.Drawing;
using System.Drawing.Imaging;


/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    string securityKey = "SFA_MPAGRO";
    APIProcedure obj = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    DataSet ds = new DataSet();
    DataTable dt = new DataTable();

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_A_Login(string Key, string UserName, string Password)
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
                        new string[] { "flag", "UserName", "Password" },
                                new string[] { "12", UserName, Password }, "dataset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid UserName and Password" }));
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
    public void HR_B_ClassList(string Key)
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
                                new string[] { "6" }, "dataset");

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
    public void HR_C_OfficeList(string Key)
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
                                new string[] { "7" }, "dataset");

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
    public void HR_C_LeaveTypeList(string Key)
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
    public void HR_D_EmployeeWithOffice(string Key, string Emp_ID)
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
                        new string[] { "flag", "Emp_ID" },
                                new string[] { "4", Emp_ID }, "dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Employee ID" }));
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
    public void HR_E_DesignationList(string Key)
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
                                new string[] { "15" }, "dataset");

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
    public void HR_F_DepartmentList(string Key)
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
                                new string[] { "16" }, "dataset");

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
    public void HR_G_MyProfile_Prsn_Office(string Key, string Emp_ID)
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_H_MyProfile_Bank_Child(string Key, string Emp_ID)
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_I_MyProfile_Nominee_Other(string Key, string Emp_ID)
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Grv_A_GrievianceEmpWiseList(string Key, string Emp_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSGrievance",
                        new string[] { "flag", "Emp_ID" },
                                new string[] { "0", Emp_ID }, "dataset");

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
    public void Grv_B_GrievianceDetail(string Key, string Application_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSGrievance",
                        new string[] { "flag", "Application_ID" },
                                new string[] { "1", Application_ID }, "dataset");

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
    public void Grv_C_GrievianceReplyDetail(string Key, string Application_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSGrievance",
                        new string[] { "flag", "Application_ID" },
                                new string[] { "2", Application_ID }, "dataset");

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
    public void Grv_D_GrievianceIntlDiscussion(string Key, string Application_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSGrievance",
                        new string[] { "flag", "Application_ID" },
                                new string[] { "3", Application_ID }, "dataset");

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
    public void Legal_A_CaseList(string Key, string Office_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSLegal",
                        new string[] { "flag", "Office_ID" },
                                new string[] { "0", Office_ID }, "dataset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Office ID." }));
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
    public void Legal_B_LegalDashboard(string Key, string Office_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSLegal",
                        new string[] { "flag", "Office_ID" },
                                new string[] { "1", Office_ID }, "dataset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Office ID." }));
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
    public void RTI_A_RTIRequest(string Key, string RTI_ByOfficeID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSRTI",
                        new string[] { "flag", "RTI_ByOfficeID" },
                                new string[] { "0", RTI_ByOfficeID }, "dataset");

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
    public void RTI_B_RTIRequestDetail(string Key, string RTI_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSRTI",
                        new string[] { "flag", "RTI_ID" },
                                new string[] { "2", RTI_ID }, "dataset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "" }));
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
    public void RTI_C_RTIReplyDetail(string Key, string RTI_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSRTI",
                        new string[] { "flag", "RTI_ID" },
                                new string[] { "3", RTI_ID }, "dataset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No record found." }));
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
    public void RTI_D_RTIInternalDiscussion(string Key, string Emp_ID, string RTI_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSRTI",
                        new string[] { "flag", "RTI_ID", "Emp_ID" },
                                new string[] { "12", RTI_ID, Emp_ID }, "dataset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No record found." }));
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
    public void RTI_E_RTIFirstRequest(string Key, string RTI_ByOfficeID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSRTI",
                        new string[] { "flag", "RTI_ByOfficeID" },
                                new string[] { "1", RTI_ByOfficeID }, "dataset");

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
    public void RTI_F_RTIFirstReqDetail(string Key, string RTI_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSRTI",
                        new string[] { "flag", "RTI_ID" },
                                new string[] { "5", RTI_ID }, "dataset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "" }));
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
    public void RTI_G_RTIFirstReplyDetail(string Key, string RTI_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSRTI", new string[] { "flag", "RTI_ID" }, new string[] { "6", RTI_ID }, "dataset");

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
    public void RTI_H_RTIFirstInternalDiscussion(string Key, string Emp_ID, string RTI_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                //ds = obj.ByProcedure("Sp_WSRTI",
                //        new string[] { "flag", "RTI_ID" },
                //                new string[] { "7", RTI_ID }, "dataset");

                ds = obj.ByProcedure("Sp_WSRTI",
                       new string[] { "flag", "RTI_ID", "Emp_ID" },
                               new string[] { "13", RTI_ID, Emp_ID }, "dataset");

                int i = 0;
                if (ds != null)
                {
                    i = ds.Tables.Count;
                    List<Dictionary<string, object>> rowsTable0 = new List<Dictionary<string, object>>();
                   // List<Dictionary<string, object>> rowsTable1 = new List<Dictionary<string, object>>();

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
                                //if (j == 1)
                                //{
                                //    rowsTable1.Add(row);
                                //}
                            }
                        }
                    }

                    if (ds.Tables[0].Rows.Count == 0 )
                    {
                        //this.Context.Response.Write(serializer.Serialize(new { List0 = "", List1 = "", status = "1", Error = "No record found." }));
                        this.Context.Response.Write(serializer.Serialize(new { List0 = "", status = "1", Error = "No record found." }));
                    }
                    else
                    {
                        //this.Context.Response.Write(serializer.Serialize(new { List0 = rowsTable0, List1 = rowsTable1, status = "1", Error = "" }));
                        this.Context.Response.Write(serializer.Serialize(new { List0 = rowsTable0, status = "1", Error = "" }));
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
    public void RTI_I_EmployeeBasisRTI(string Key, string Emp_ID, string RTI_ByOfficeID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSRTI",
                        new string[] { "flag", "Emp_ID", "RTI_ByOfficeID" },
                                new string[] { "8", Emp_ID, RTI_ByOfficeID }, "dataset");

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
    public void RTI_J_EmployeeBasisFirstAppeal(string Key, string Emp_ID, string RTI_ByOfficeID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSRTI",
                        new string[] { "flag", "Emp_ID", "RTI_ByOfficeID" },
                                new string[] { "9", Emp_ID, RTI_ByOfficeID }, "dataset");

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
    public void RTI_K_RTIRTIDashboardEmpWise(string Key, string Emp_ID, string RTI_ByOfficeID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSRTI",
                        new string[] { "flag", "Emp_ID", "RTI_ByOfficeID" },
                                new string[] { "10", Emp_ID, RTI_ByOfficeID }, "dataset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "" }));
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
    public void RTI_L_ListOfRTIRecievingOfficer(string Key, string Emp_ID, string RTI_ID, string Role_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSRTI",
                        new string[] { "flag", "Emp_ID", "RTI_ID", "Role_ID" },
                                new string[] { "11", Emp_ID, RTI_ID, Role_ID }, "dataset");

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No record found." }));
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
    public void WH_A_WareHouseList(string Key, string Office_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSWareHouse",
                        new string[] { "flag", "Office_ID" },
                                new string[] { "0", Office_ID }, "dataset");

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
    public void WH_B_WareHouseDetail(string Key, string Warehouse_id, string Month, string Year)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSWareHouse",
                    new string[] { "flag", "Warehouse_id", "Month", "Year" },
                                new string[] { "2", Warehouse_id, Month, Year }, "dataset");

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
    public void HR_AA_EmpWiseApplyLeave(string Key, string Emp_ID, string Office_ID, string LeaveType, string LeaveApproveAuthority, string FromDate, string ToDate, string LeaveRemark, string LeaveDocument, string LeaveDay)
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

                if (ds.Tables[1].Rows[0]["Status"].ToString() == "True")
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

                    this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));
                }

                else
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "" }));
                }

                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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
    public void HR_AB_EmpWiseLeaveStatus(string Key, string Emp_ID, string FinancialYear)
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
    public void HR_AC_EmpWiseBalanceLeave(string Key, string Emp_ID, string Financial_Year)
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
    public void HR_AD_EmpWiseActionForLeave(string Key)
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
    public void HR_AE_EmpWiseAttendance(string Key, string Emp_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Employee", new string[] { "flag", "Emp_ID" },
                  new string[] { "0", Emp_ID }, "datatset");

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

    [WebMethod]  // For All Login Emp
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_AF_EmpWiseDailyAttendance(string Key, string Emp_ID, string SelectDate)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Employee", new string[] { "flag", "Emp_ID", "SelectDate" },
                  new string[] { "1", Emp_ID, Convert.ToDateTime(SelectDate, cult).ToString("yyyy/MM/dd") }, "datatset");

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

    [WebMethod]  // Of all Emp
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_AG_EmpWiseTodayAttendance(string Key, string Office_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Employee", new string[] { "flag", "Office_ID" },
                  new string[] { "4", Office_ID }, "datatset");
                int i = 0;
                if (ds != null)
                {
                    i = ds.Tables.Count;
                    List<Dictionary<string, object>> rowsTable0 = new List<Dictionary<string, object>>();
                    List<Dictionary<string, object>> rowsTable1 = new List<Dictionary<string, object>>();
                    List<Dictionary<string, object>> rowsTable2 = new List<Dictionary<string, object>>();
                    List<Dictionary<string, object>> rowsTable3 = new List<Dictionary<string, object>>();

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
                                if (j == 3)
                                {
                                    rowsTable3.Add(row);
                                }
                            }
                        }
                    }

                    if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0 && ds.Tables[3].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List0 = "", List1 = "", List2 = "", List3 = "", status = "1", Error = "No record found." }));
                    }
                    else
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List0 = rowsTable0, List1 = rowsTable1, List2 = rowsTable2, List3 = rowsTable3, status = "1", Error = "" }));
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
    public void HR_AH_EmpWiseDirectory(string Key, string Office_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Employee", new string[] { "flag", "Office_ID" },
                  new string[] { "2", Office_ID }, "datatset");

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
    public void HR_AI_EmpWiseMeeting(string Key, string Meeting_Date)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR", new string[] { "flag", "Meeting_Date" },
                  new string[] { "10", Convert.ToDateTime(Meeting_Date, cult).ToString("yyyy/MM/dd") }, "datatset");

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
    public void HR_AJ_EmpPayrollAllEmpList(string Key)  //string Year, string Month
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                //ds = obj.ByProcedure("Sp_WSPaySlip", new string[] { "flag", "Emp_ID", "Year", "MonthNo" }, new string[] { "5", Emp_ID, Year, Month }, "dataset");
                ds = obj.ByProcedure("Sp_WSPaySlip", new string[] { "flag" }, new string[] { "1" }, "dataset");

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
    public void HR_AK_EmpPayrollOfficeWiseList(string Key, string Office_ID) //, string Emp_ID, string Office_ID, string Year, string Month
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSPaySlip", new string[] { "flag", "Office_ID" }, new string[] { "2", Office_ID }, "dataset");

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
    public void HR_AL_EmpPayrollSalarySlip(string Key, string Emp_ID, string Office_ID, string Year, string Month)
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_AM_ChangePassword(string Key, string Emp_ID, string Password, string NewPassword)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        try
        {
            ds = obj.ByProcedure("Sp_WSHR_Employee",
                           new string[] { "flag", "Emp_ID", "Password", "NewPassword" },
                           new string[] { "5", Emp_ID, Password, NewPassword }, "dataset");

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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_AN_UpcomingRetirementList(string Key)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        try
        {
            //ds = obj.ByProcedure("Sp_WSHR",
            //               new string[] { "flag", "Office_ID" },
            //               new string[] { "2", Office_ID }, "dataset");

            ds = obj.ByProcedure("Sp_WSHR",
                           new string[] { "flag" },
                           new string[] { "2" }, "dataset");

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found" }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_AO_UpcomingBirthdayList(string Key, string Office_ID, string Month)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        try
        {
            ds = obj.ByProcedure("Sp_WSHR",
                           new string[] { "flag", "Office_ID", "Month" },
                           new string[] { "9", Office_ID, Month }, "dataset");

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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_AP_ClassWiseEmpList(string Key, string Emp_Class)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        try
        {
            ds = obj.ByProcedure("Sp_WSHR",
                           new string[] { "flag", "Emp_Class" },
                           new string[] { "4", Emp_Class }, "dataset");

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found" }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_AQ_OfficeWiseEmpList(string Key, string Office_ID)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        try
        {
            ds = obj.ByProcedure("Sp_WSHR",
                           new string[] { "flag", "Office_ID" },
                           new string[] { "5", Office_ID }, "dataset");

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "No Record Found" }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_AR_LoginEmpMeeting(string Key, string Emp_ID, string Meeting_Date)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Employee", new string[] { "flag", "Emp_ID", "Meeting_Date" },
                  new string[] { "3", Emp_ID, Convert.ToDateTime(Meeting_Date, cult).ToString("yyyy/MM/dd") }, "datatset");

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
    public void HR_AS_LMLoginEmpAttendance(string Key, string Emp_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Employee", new string[] { "flag", "Emp_ID" },
                  new string[] { "7", Emp_ID }, "datatset");

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
    public void HR_AT_CurrentMonthPresentDays(string Key, string Emp_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Employee", new string[] { "flag", "Emp_ID" },
                  new string[] { "11", Emp_ID }, "datatset");

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
    public void HR_AU_CurrentMonthAbsentDays(string Key, string Emp_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Employee", new string[] { "flag", "Emp_ID" },
                  new string[] { "9", Emp_ID }, "datatset");

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
    public void HR_AV_CurrentMonthLateComingDays(string Key, string Emp_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Employee", new string[] { "flag", "Emp_ID" },
                  new string[] { "10", Emp_ID }, "datatset");

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
    public void HR_AW_MonthlyWiseMeeting(string Key, string Emp_ID, string MonthNo, string Year)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Employee", new string[] { "flag", "Emp_ID", "MonthNo", "Year" },
                  new string[] { "12", Emp_ID, MonthNo, Year }, "datatset");
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                    }
                    else
                    {
                        dt = ds.Tables[0];
                        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                        Dictionary<string, object> row = null;
                        foreach (DataRow dr in dt.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dt.Columns)
                            {
                                row.Add(col.ColumnName, dr[col]);
                            }
                            rows.Add(row);
                        }
                        this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
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
    public void HR_AX_EmpAssignedRoles(string Key, string Emp_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR", new string[] { "flag", "Emp_ID" },
                  new string[] { "11", Emp_ID }, "datatset");

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
    public void HR_AY_HolidayCalender(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag" },
                  new string[] { "4" }, "datatset");

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
    public void HR_AZ_IsOptionalDateORNot(string Key, string FromDate, string ToDate)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "FromDate", "ToDate" },
                  new string[] { "5", Convert.ToDateTime(FromDate, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(ToDate, cult).ToString("yyyy/MM/dd") }, "datatset");

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
    public void HR_BA_CheckAppliedOptionalHolidays(string Key, string Emp_ID)
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
    public void HR_BB_CheckAlreadyAppliedLeave(string Key, string Emp_ID, string LeaveFromDate, string LeaveToDate)
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_BC_ListOfApprovalAuthority(string Key, string Emp_ID)
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
                 new string[] { "2", Emp_ID }, "datatset");  //  Emp_ID pass in this ,for removing login person name from the list.

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
    public void HR_BD_ListOfPendingLeaveRequest(string Key, string Emp_ID, string Office_ID, string Financial_Year)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "Office_ID", "LeaveApproveAuthority", "Financial_Year " },
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
    public void HR_BE_DetailOfPendingLeave(string Key, string LeaveId)
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
    public void HR_BF_NoOfDays_LeaveType(string Key, string LeaveType_ID, string Financial_Year)
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
    public void HR_BG_StatusOfHalf_FullDay(string Key, string LeaveId)
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
    public void HR_BH_DayOfHolidayLeave(string Key, string LeaveId)// Day name of leave date
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
    public void HR_BI_IsRegularHoliday(string Key, string LeaveId)
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
    public void HR_BJ_CalculateBalanceLeave(string Key, string Emp_ID, string LeaveType, string FinancialYear, string TotalAllowedLeave, string TotalTakenLeave)
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
    public void HR_BK_UpdateLeaveStatus(string Key, string LeaveId, string LeaveStatus, string RemarkByApprovalAuth, string LeaveApprovalOrderNo, string LeaveApprovalOrderDate, string LeaveApprovalOrderFile)
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
                if (LeaveApprovalOrderFile != "")
                {
                    DocPathWithName = "../HR/UploadDoc/LeaveApproveDoc/" + GUID + "_" + LeaveId + ".jpg";
                    DocFullPathWithName = Server.MapPath(@"~/mis/HR/UploadDoc/LeaveApproveDoc/" + GUID + "_" + LeaveId);
                    FileFullPath = DocFullPathWithName + ".jpg";
                    byte[] imgByteArray = Convert.FromBase64String(LeaveApprovalOrderFile);
                    File.WriteAllBytes(FileFullPath, imgByteArray);
                }
                string OrderDate = "";
                if(LeaveApprovalOrderDate != ""){
                    OrderDate = Convert.ToDateTime(LeaveApprovalOrderDate, cult).ToString("yyyy/MM/dd");
                }
                ds = obj.ByProcedure("Sp_WSHR_Leave_New", new string[] { "flag", "LeaveId", "LeaveStatus", "RemarkByApprovalAuth", "LeaveApprovalOrderNo", "LeaveApprovalOrderDate", "LeaveApprovalOrderFile" },
                 new string[] { "14", LeaveId, LeaveStatus, RemarkByApprovalAuth, LeaveApprovalOrderNo, OrderDate, DocFullPathWithName }, "datatset");

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
    public void HR_BL_ListOfLeaveAppliedDate(string Key, string Emp_ID, string LeaveFromDate, string LeaveToDate)
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
    public void HR_BM_ListOfAppRejLeave(string Key, string FinancialYear, string LeaveApproveAuthority)
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HR_BN_SeniorityList(string Key, string Designation_ID, string SLYear)
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
                    new string[] { "flag", "SLYear", "Designation_ID" },
                    new string[] { "13", SLYear, Designation_ID }, "dataset");

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
    public void HR_BO_PromotionListHistory(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSHR", new string[] { "flag" }, new string[] { "14" }, "dataset");

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
    public void FT_A_FileOnMyDesk(string Key, string Emp_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSFileTracking", new string[] { "flag", "Emp_ID" },
                  new string[] { "0", Emp_ID }, "datatset");
                if (ds != null)
                {
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
    public void FT_B_FileForwardDetail(string Key, string File_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSFileTracking", new string[] { "flag", "File_ID" },
                  new string[] { "1", File_ID }, "datatset");
                if (ds != null)
                {
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
    public void SM_A_ListOfScheme(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSSchemeM", new string[] { "flag" },
                  new string[] { "1"}, "datatset");
                if (ds != null)
                {
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
    public void SM_B_SchemeFinancialYear(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSSchemeM", new string[] { "flag" },
                  new string[] { "2" }, "datatset");
                if (ds != null)
                {
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
    public void SM_C_ListOfBeneficiary(string Key, string Ben_FinancialYear, string SchemeID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSSchemeM", new string[] { "flag", "Ben_FinancialYear" },
                  new string[] { "3", "Ben_FinancialYear", "SchemeID" }, "datatset");
                if (ds != null)
                {
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

    [WebMethod(Description = "<b>NOTE:</b> For Year Need to pass <b>Phase_id</b> Of API (SM_B_SchemeFinancialYear). Like 10, 9 etc")]  
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SM_D_ExpenditureReport(string Key, string Ben_Category, string SchemeID, string Year, string Office_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string flag = "5";
                if (Office_ID == "0")
                {
                    flag = "4";
                }
                ds = obj.ByProcedure("Sp_WSSchemeM",
                new string[] { "flag", "Year", "SchemeID", "Ben_Category", "Office_ID" },
                new string[] { flag, Year, SchemeID, Ben_Category, Office_ID }, "dataset");
                if (ds != null)
                {
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
    public void FIN_AA_GetVoucherDate(string Key, string Office_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSFinanceReport", new string[] { "flag", "Office_ID" },
                  new string[] { "1", Office_ID }, "datatset");
                if (ds != null)
                {
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

    [WebMethod]  //Getting voucher list according to date and office id
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FIN_AB_DayBookVoucherList(string Key, string Office_ID, string VoucherTx_Date)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSFinanceReport", new string[] { "flag", "Office_ID", "VoucherTx_Date" },
                  new string[] { "2", Office_ID, Convert.ToDateTime(VoucherTx_Date, cult).ToString("yyyy/MM/dd") }, "datatset");
                if (ds != null)
                {
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

    [WebMethod] //Getting Voucher detail by Voucher ID
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FIN_AC_GetVoucherDetail(string Key, string Office_ID, string VoucherTx_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = obj.ByProcedure("Sp_WSFinanceReport", new string[] { "flag", "Office_ID", "VoucherTx_ID" },
                  new string[] { "3", Office_ID, VoucherTx_ID }, "datatset");
                if (ds != null)
                {
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
