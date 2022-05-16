using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web.Script.Services;
using System.Web.Services;
using System.Linq;

/// <summary>
/// Summary description for MPCDF_WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class MPCDF_DCS_WebService : System.Web.Services.WebService
{
    string securityKey_DCS = "SFA_MPCDF_DCS-ERP";
    APIProcedure apiprocedure = new APIProcedure();
    CommanddlFill commanddlfill = new CommanddlFill();
    CultureInfo cult = new CultureInfo("en-IN", true);
    IFormatProvider culture = new CultureInfo("en-US", true);

    public MPCDF_DCS_WebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //get Year
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_Years(string strKey)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (strKey == securityKey_DCS)
        {
            try
            {
                ds = apiprocedure.ByProcedure("Get_year_and_month"
                   , new string[] { "flag" }
                   , new string[] { "0" }, "dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
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
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //Get DCS_code
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_DCS_Code_byoffice_Id(string strKey, string strOffice_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (strKey == securityKey_DCS)
        {
            try
            {
                ds = apiprocedure.ByProcedure("Get_DCS_Info_byoffice_Id"
                   , new string[] { "flag", "Office_ID" }
                   , new string[] { "0", strOffice_ID }, "dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
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
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    //Get DCS Info
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Get_DCS_info_byoffice_Id(string strKey, string strOffice_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (strKey == securityKey_DCS)
        {
            try
            {
                ds = apiprocedure.ByProcedure("Get_DCS_Info_byoffice_Id"
                   , new string[] { "flag", "Office_ID" }
                   , new string[] { "1", strOffice_ID }, "dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
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
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }
	

    ////deisel Pump
    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public void Insert_Deisel_pump(string strKey, string strOffice_ID, string strOp_bal_qty, string strOp_bal_Rate, string strOp_bal_AMT, string strIOV_qty, string strIOV_Rate, string strIOV_AMT, string strITO_qty, string strITO_Rate, string strITO_AMT, string strCL_bal_qty, string strCL_bal_Rate, string strCL_bal_AMT, string strPDM_count, string strMonth, string strYear, string strCreatedBy)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    this.Context.Response.ContentType = "application/json; charset=utf-8";
    //    if (strKey == securityKey_DCS)
    //    {
    //        try
    //        {
    //            ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_diesel_Pump_API",
    //               new string[] { "flag", "Office_ID", "Op_bal_qty" , "Op_bal_Rate" ,"Op_bal_AMT" ,"IOV_qty ","IOV_Rate" ,"IOV_AMT" ,"ITO_qty" ,"ITO_Rate" ,"ITO_AMT" ,"CL_bal_qty" ,"CL_bal_Rate" ,"CL_bal_AMT" ,"PDM_count", "Month",
    //                                 "Year", "CreatedBy" },
    //                 new string[] { "0", strOffice_ID,  strOp_bal_qty,  strOp_bal_Rate,  strOp_bal_AMT,  strIOV_qty,  strIOV_Rate,  strIOV_AMT,  strITO_qty,  strITO_Rate,  strITO_AMT,  strCL_bal_qty,  strCL_bal_Rate,  strCL_bal_AMT,  strPDM_count,  strMonth,  strYear,  strCreatedBy }, "dataset");
    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
    //            }
    //            else
    //            {

    //                dt = ds.Tables[0];
    //                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //                Dictionary<string, object> row = null;

    //                foreach (DataRow rs in dt.Rows)
    //                {
    //                    row = new Dictionary<string, object>();
    //                    foreach (DataColumn col in dt.Columns)
    //                    {
    //                        row.Add(col.ColumnName, rs[col]);
    //                    }
    //                    rows.Add(row);
    //                }

    //                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
    //        }
    //        dt.Clear();
    //        ds.Clear();
    //    }
    //    else
    //    {
    //        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
    //    }
    //}

    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public void Insert_Deisel_pump_Detail(string strKey, string strMPR_DP_Id, string strPDM_Date, string strQuantity, string strRate, string strAmount)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    this.Context.Response.ContentType = "application/json; charset=utf-8";
    //    if (strKey == securityKey_DCS)
    //    {
    //        try
    //        {
    //            ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_diesel_Pump_API",
    //                      new string[] { "flag", "MPR_DP_Id", "PDM_Date", "Quantity", "Rate",
    //                                 "Amount" },
    //                        new string[] { "1", strMPR_DP_Id,  strPDM_Date,  strQuantity,  strRate,  strAmount}, "dataset");
    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
    //            }
    //            else
    //            {

    //                dt = ds.Tables[0];
    //                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //                Dictionary<string, object> row = null;

    //                foreach (DataRow rs in dt.Rows)
    //                {
    //                    row = new Dictionary<string, object>();
    //                    foreach (DataColumn col in dt.Columns)
    //                    {
    //                        row.Add(col.ColumnName, rs[col]);
    //                    }
    //                    rows.Add(row);
    //                }

    //                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
    //        }
    //        dt.Clear();
    //        ds.Clear();
    //    }
    //    else
    //    {
    //        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
    //    }
    //}


    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    //public void Get_month_Yearwise_Deisel_pump_report(string strKey, string strOffice_ID, string strCreatedBy, string strMonth, string strYear)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    this.Context.Response.ContentType = "application/json; charset=utf-8";
    //    if (strKey == securityKey_DCS)
    //    {
    //        try
    //        {
    //            ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_diesel_Pump_API",
    //           new string[] { "flag", "Office_ID", "CreatedBy","Month",
    //                                 "Year" },
    //             new string[] { "6", strOffice_ID,  strCreatedBy,  strMonth,  strYear}, "dataset");
    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
    //            }
    //            else
    //            {

    //                dt = ds.Tables[0];
    //                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //                Dictionary<string, object> row = null;

    //                foreach (DataRow rs in dt.Rows)
    //                {
    //                    row = new Dictionary<string, object>();
    //                    foreach (DataColumn col in dt.Columns)
    //                    {
    //                        row.Add(col.ColumnName, rs[col]);
    //                    }
    //                    rows.Add(row);
    //                }

    //                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
    //        }
    //        dt.Clear();
    //        ds.Clear();
    //    }
    //    else
    //    {
    //        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
    //    }
    //}


    ////deisel Cunsumption
    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public void Insert_deisel_Cunsumption(string strKey, string strOffice_ID, string strVehicle_Type_ID, string strVehicle_Type_Name, string strMonth, string strYear, string strCreatedBy)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    this.Context.Response.ContentType = "application/json; charset=utf-8";
    //    if (strKey == securityKey_DCS)
    //    {
    //        try
    //        {
    //            ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_diesel_cunsumption_API",
    //                new string[] { "flag", "Office_ID", "Vehicle_Type_ID", "Vehicle_Type_Name", "Month",
    //                                 "Year", "CreatedBy" },
    //                  new string[] { "0", strOffice_ID,  strVehicle_Type_ID,  strVehicle_Type_Name,  strMonth,  strYear,  strCreatedBy }, "dataset");
    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
    //            }
    //            else
    //            {

    //                dt = ds.Tables[0];
    //                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //                Dictionary<string, object> row = null;

    //                foreach (DataRow rs in dt.Rows)
    //                {
    //                    row = new Dictionary<string, object>();
    //                    foreach (DataColumn col in dt.Columns)
    //                    {
    //                        row.Add(col.ColumnName, rs[col]);
    //                    }
    //                    rows.Add(row);
    //                }

    //                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
    //        }
    //        dt.Clear();
    //        ds.Clear();
    //    }
    //    else
    //    {
    //        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
    //    }
    //}

    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public void Insert_deisel_Cunsumption_Detail(string strKey, string strDC_Id, string strVehicle_No, string strOpening, string strClosing, string strOpbal, string strOwnpump, string strFromCC, string strMarket_purchase_Ltr, string strMarket_purchase_Rs, string strClobal)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    this.Context.Response.ContentType = "application/json; charset=utf-8";
    //    if (strKey == securityKey_DCS)
    //    {
    //        try
    //        {
    //            ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_diesel_cunsumption_API",
    //                      new string[] { "flag", "DC_Id", "Vehicle_No", "Opening", "Closing",
    //                                 "Opbal", "Ownpump","FromCC","Market_purchase_Ltr","Market_purchase_Rs","Clobal" },
    //                        new string[] { "1", strDC_Id,  strVehicle_No,  strOpening,  strClosing,  strOpbal,  strOwnpump,  strFromCC,  strMarket_purchase_Ltr,  strMarket_purchase_Rs,  strClobal }, "dataset");
    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
    //            }
    //            else
    //            {

    //                dt = ds.Tables[0];
    //                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //                Dictionary<string, object> row = null;

    //                foreach (DataRow rs in dt.Rows)
    //                {
    //                    row = new Dictionary<string, object>();
    //                    foreach (DataColumn col in dt.Columns)
    //                    {
    //                        row.Add(col.ColumnName, rs[col]);
    //                    }
    //                    rows.Add(row);
    //                }

    //                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
    //        }
    //        dt.Clear();
    //        ds.Clear();
    //    }
    //    else
    //    {
    //        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
    //    }
    //}


    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public void Get_month_Yearwise_deisel_Cunsumption_report(string strKey, string strOffice_ID, string strCreatedBy, string strMonth, string strYear)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    this.Context.Response.ContentType = "application/json; charset=utf-8";
    //    if (strKey == securityKey_DCS)
    //    {
    //        try
    //        {
    //            ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_diesel_cunsumption_API",
    //           new string[] { "flag", "Office_ID", "CreatedBy","Month",
    //                                 "Year" },
    //             new string[] { "4", strOffice_ID, strCreatedBy, strMonth, strYear }, "dataset");
    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
    //            }
    //            else
    //            {

    //                dt = ds.Tables[0];
    //                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //                Dictionary<string, object> row = null;

    //                foreach (DataRow rs in dt.Rows)
    //                {
    //                    row = new Dictionary<string, object>();
    //                    foreach (DataColumn col in dt.Columns)
    //                    {
    //                        row.Add(col.ColumnName, rs[col]);
    //                    }
    //                    rows.Add(row);
    //                }

    //                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
    //        }
    //        dt.Clear();
    //        ds.Clear();
    //    }
    //    else
    //    {
    //        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
    //    }
    //}


    ////Monthly Expenses
    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public void Insert_Monthly_Expenses(string strKey, string strOffice_ID, string strVehicle_Type_ID, string strVehicle_Type_Name, string strMonth, string strYear, string strCreatedBy)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    this.Context.Response.ContentType = "application/json; charset=utf-8";
    //    if (strKey == securityKey_DCS)
    //    {
    //        try
    //        {
    //            ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_Expenses_API",
    //                new string[] { "flag", "Office_ID", "Vehicle_Type_ID", "Vehicle_Type_Name", "Month",
    //                                 "Year", "CreatedBy" },
    //                  new string[] { "0", strOffice_ID, strVehicle_Type_ID, strVehicle_Type_Name, strMonth, strYear, strCreatedBy }, "dataset");
    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
    //            }
    //            else
    //            {

    //                dt = ds.Tables[0];
    //                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //                Dictionary<string, object> row = null;

    //                foreach (DataRow rs in dt.Rows)
    //                {
    //                    row = new Dictionary<string, object>();
    //                    foreach (DataColumn col in dt.Columns)
    //                    {
    //                        row.Add(col.ColumnName, rs[col]);
    //                    }
    //                    rows.Add(row);
    //                }

    //                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
    //        }
    //        dt.Clear();
    //        ds.Clear();
    //    }
    //    else
    //    {
    //        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
    //    }
    //}

    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public void Insert_Monthly_Expenses_Detail(string strKey, string strME_Id, string strVehicle_No, string strOil, string strGrease, string strSpares, string strLabour, string strToll_Tax, string strOthers)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    this.Context.Response.ContentType = "application/json; charset=utf-8";
    //    if (strKey == securityKey_DCS)
    //    {
    //        try
    //        {
    //            ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_Expenses_API",
    //                    new string[] { "flag", "ME_Id", "Vehicle_No", "Oil", "Grease",
    //                                 "Spares", "Labour","Toll_Tax","Others" },
    //                        new string[] { "1", strME_Id,  strVehicle_No,  strOil,  strGrease,  strSpares,  strLabour,  strToll_Tax,  strOthers }, "dataset");
    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
    //            }
    //            else
    //            {

    //                dt = ds.Tables[0];
    //                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //                Dictionary<string, object> row = null;

    //                foreach (DataRow rs in dt.Rows)
    //                {
    //                    row = new Dictionary<string, object>();
    //                    foreach (DataColumn col in dt.Columns)
    //                    {
    //                        row.Add(col.ColumnName, rs[col]);
    //                    }
    //                    rows.Add(row);
    //                }

    //                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
    //        }
    //        dt.Clear();
    //        ds.Clear();
    //    }
    //    else
    //    {
    //        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
    //    }
    //}


    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public void Get_month_Yearwise_Monthly_Expenses_report(string strKey, string strOffice_ID, string strCreatedBy, string strMonth, string strYear)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    this.Context.Response.ContentType = "application/json; charset=utf-8";
    //    if (strKey == securityKey_DCS)
    //    {
    //        try
    //        {
    //            ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_Expenses_API",
    //           new string[] { "flag", "Office_ID", "CreatedBy","Month",
    //                                 "Year" },
    //             new string[] { "4", strOffice_ID, strCreatedBy, strMonth, strYear }, "dataset");
    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
    //            }
    //            else
    //            {

    //                dt = ds.Tables[0];
    //                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //                Dictionary<string, object> row = null;

    //                foreach (DataRow rs in dt.Rows)
    //                {
    //                    row = new Dictionary<string, object>();
    //                    foreach (DataColumn col in dt.Columns)
    //                    {
    //                        row.Add(col.ColumnName, rs[col]);
    //                    }
    //                    rows.Add(row);
    //                }

    //                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
    //        }
    //        dt.Clear();
    //        ds.Clear();
    //    }
    //    else
    //    {
    //        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
    //    }
    //}


    ////Allocated Expenses
    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public void Insert_Allocated_Expenses(string strKey, string strOffice_ID, string strVehicle_Type_ID, string strVehicle_Type_Name, string strMonth, string strYear, string strCreatedBy)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    this.Context.Response.ContentType = "application/json; charset=utf-8";
    //    if (strKey == securityKey_DCS)
    //    {
    //        try
    //        {
    //            ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_Allocated_Expenses_API",
    //                new string[] { "flag", "Office_ID", "Vehicle_Type_ID", "Vehicle_Type_Name", "Month",
    //                                 "Year", "CreatedBy" },
    //                  new string[] { "0", strOffice_ID, strVehicle_Type_ID, strVehicle_Type_Name, strMonth, strYear, strCreatedBy }, "dataset");
    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
    //            }
    //            else
    //            {

    //                dt = ds.Tables[0];
    //                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //                Dictionary<string, object> row = null;

    //                foreach (DataRow rs in dt.Rows)
    //                {
    //                    row = new Dictionary<string, object>();
    //                    foreach (DataColumn col in dt.Columns)
    //                    {
    //                        row.Add(col.ColumnName, rs[col]);
    //                    }
    //                    rows.Add(row);
    //                }

    //                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
    //        }
    //        dt.Clear();
    //        ds.Clear();
    //    }
    //    else
    //    {
    //        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
    //    }
    //}

    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public void Insert_Allocated_Expenses_Detail(string strKey, string strAE_Id, string strVehicle_No, string strRoad_tax, string strPermit_tax, string strInsurance, string strTyre, string strBattery, string strMajor_repaires)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    this.Context.Response.ContentType = "application/json; charset=utf-8";
    //    if (strKey == securityKey_DCS)
    //    {
    //        try
    //        {
    //            ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_Allocated_Expenses_API",
    //                    new string[] { "flag", "AE_Id", "Vehicle_No", "Road_tax", "Permit_tax",
    //                                 "Insurance", "Tyre","Battery","Major_repaires" },
    //                        new string[] { "1", strAE_Id,  strVehicle_No,  strRoad_tax,  strPermit_tax,  strInsurance,  strTyre,  strBattery,  strMajor_repaires }, "dataset");
    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
    //            }
    //            else
    //            {

    //                dt = ds.Tables[0];
    //                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //                Dictionary<string, object> row = null;

    //                foreach (DataRow rs in dt.Rows)
    //                {
    //                    row = new Dictionary<string, object>();
    //                    foreach (DataColumn col in dt.Columns)
    //                    {
    //                        row.Add(col.ColumnName, rs[col]);
    //                    }
    //                    rows.Add(row);
    //                }

    //                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
    //        }
    //        dt.Clear();
    //        ds.Clear();
    //    }
    //    else
    //    {
    //        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
    //    }
    //}


    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public void Get_month_Yearwise_Allocated_Expenses_report(string strKey, string strOffice_ID, string strCreatedBy, string strMonth, string strYear)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    this.Context.Response.ContentType = "application/json; charset=utf-8";
    //    if (strKey == securityKey_DCS)
    //    {
    //        try
    //        {
    //            ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_Allocated_Expenses_API",
    //           new string[] { "flag", "Office_ID", "CreatedBy","Month",
    //                                 "Year" },
    //             new string[] { "4", strOffice_ID, strCreatedBy, strMonth, strYear }, "dataset");
    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
    //            }
    //            else
    //            {

    //                dt = ds.Tables[0];
    //                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //                Dictionary<string, object> row = null;

    //                foreach (DataRow rs in dt.Rows)
    //                {
    //                    row = new Dictionary<string, object>();
    //                    foreach (DataColumn col in dt.Columns)
    //                    {
    //                        row.Add(col.ColumnName, rs[col]);
    //                    }
    //                    rows.Add(row);
    //                }

    //                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
    //        }
    //        dt.Clear();
    //        ds.Clear();
    //    }
    //    else
    //    {
    //        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
    //    }
    //}


    ////Contractual Labour
    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public void Insert_Contractual_Labour(string strKey, string strOffice_ID, string strMonth, string strYear, string strNum_unskilled, string strNum_semi_skilled, string strNum_skilled, string strWBA_unskilled, string strWBA_semi_skilled, string strWBA_skilled, string strCreatedBy)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    this.Context.Response.ContentType = "application/json; charset=utf-8";
    //    if (strKey == securityKey_DCS)
    //    {
    //        try
    //        {
    //            ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_Contractual_labour_API",
    //                new string[] { "flag", "Office_ID", "Month",
    //                                 "Year", "Num_unskilled", "Num_semi_skilled","Num_skilled","WBA_unskilled","WBA_semi_skilled","WBA_skilled", "CreatedBy" },
    //                  new string[] { "0", strOffice_ID,  strMonth,  strYear,  strNum_unskilled,  strNum_semi_skilled,  strNum_skilled,  strWBA_unskilled,  strWBA_semi_skilled,  strWBA_skilled,  strCreatedBy}, "dataset");
    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
    //            }
    //            else
    //            {

    //                dt = ds.Tables[0];
    //                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //                Dictionary<string, object> row = null;

    //                foreach (DataRow rs in dt.Rows)
    //                {
    //                    row = new Dictionary<string, object>();
    //                    foreach (DataColumn col in dt.Columns)
    //                    {
    //                        row.Add(col.ColumnName, rs[col]);
    //                    }
    //                    rows.Add(row);
    //                }

    //                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
    //        }
    //        dt.Clear();
    //        ds.Clear();
    //    }
    //    else
    //    {
    //        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
    //    }
    //}

   
    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public void Get_month_Yearwise_Contractual_Labour_report(string strKey, string strOffice_ID, string strCreatedBy, string strMonth, string strYear)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    this.Context.Response.ContentType = "application/json; charset=utf-8";
    //    if (strKey == securityKey_DCS)
    //    {
    //        try
    //        {
    //            ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_Contractual_labour_API",
    //           new string[] { "flag", "Office_ID", "CreatedBy","Month",
    //                                 "Year" },
    //             new string[] { "4", strOffice_ID, strCreatedBy, strMonth, strYear }, "dataset");
    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
    //            }
    //            else
    //            {

    //                dt = ds.Tables[0];
    //                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //                Dictionary<string, object> row = null;

    //                foreach (DataRow rs in dt.Rows)
    //                {
    //                    row = new Dictionary<string, object>();
    //                    foreach (DataColumn col in dt.Columns)
    //                    {
    //                        row.Add(col.ColumnName, rs[col]);
    //                    }
    //                    rows.Add(row);
    //                }

    //                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
    //        }
    //        dt.Clear();
    //        ds.Clear();
    //    }
    //    else
    //    {
    //        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
    //    }
    //}



    // Semi Fixed Nature Information of DCS
   

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Insert_Semi_Fixed_Nature_Information(string strKey, string strOffice_ID, string strDCS_Code, string strTNM_GC_LL, string strTNM_GC_MF, string strTNM_GC_SF, string strTNM_GC_LFO, string strTNM_SC_LL, string strTNM_SC_MF, string strTNM_SC_SF, string strTNM_SC_LFO, string strTNM_ST_LL, string strTNM_ST_MF, string strTNM_ST_SF, string strTNM_ST_LFO, string strTNM_OBC_LL, string strTNM_OBC_MF, string strTNM_OBC_SF, string strTNM_OBC_LFO, string strSCA_LL, string strSCA_MF, string strSCA_SF, string strSCA_LFO, string strWM_GC_LL, string strWM_GC_MF, string strWM_GC_SF, string strWM_GC_LFO, string strWM_SC_LL, string strWM_SC_MF, string strWM_SC_SF, string strWM_SC_LFO, string strWM_ST_LL, string strWM_ST_MF, string strWM_ST_SF, string strWM_ST_LFO, string strWM_OBC_LL, string strWM_OBC_MF, string strWM_OBC_SF, string strWM_OBC_LFO, string strMonth, string strYear, string strCreatedBy)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (strKey == securityKey_DCS)
        {
            try
            {
                ds = apiprocedure.ByProcedure("usp_insertupdate_SEmi_fixed_nature_info_byDCS_API",
                     new string[] { "flag", "Office_ID", "DCS_Code", "TNM_GC_LL", "TNM_GC_MF", "TNM_GC_SF", "TNM_GC_LFO", "TNM_SC_LL", "TNM_SC_MF", "TNM_SC_SF", "TNM_SC_LFO", "TNM_ST_LL", "TNM_ST_MF", "TNM_ST_SF", "TNM_ST_LFO", "TNM_OBC_LL", "TNM_OBC_MF", "TNM_OBC_SF", "TNM_OBC_LFO", "SCA_LL", "SCA_MF", "SCA_SF", "SCA_LFO", "WM_GC_LL", "WM_GC_MF", "WM_GC_SF", "WM_GC_LFO", "WM_SC_LL", "WM_SC_MF", "WM_SC_SF", "WM_SC_LFO", "WM_ST_LL", "WM_ST_MF", "WM_ST_SF", "WM_ST_LFO", "WM_OBC_LL", "WM_OBC_MF", "WM_OBC_SF", "WM_OBC_LFO", "Month",
                                     "Year", "CreatedBy" },
                      new string[] { "0", strOffice_ID,  strDCS_Code,  strTNM_GC_LL,  strTNM_GC_MF,  strTNM_GC_SF,  strTNM_GC_LFO,  strTNM_SC_LL,  strTNM_SC_MF,  strTNM_SC_SF,  strTNM_SC_LFO,  strTNM_ST_LL,  strTNM_ST_MF,  strTNM_ST_SF,  strTNM_ST_LFO,  strTNM_OBC_LL,  strTNM_OBC_MF,  strTNM_OBC_SF,  strTNM_OBC_LFO,  strSCA_LL,  strSCA_MF,  strSCA_SF,  strSCA_LFO,  strWM_GC_LL,  strWM_GC_MF,  strWM_GC_SF,  strWM_GC_LFO,  strWM_SC_LL,  strWM_SC_MF,  strWM_SC_SF,  strWM_SC_LFO,  strWM_ST_LL,  strWM_ST_MF,  strWM_ST_SF,  strWM_ST_LFO,  strWM_OBC_LL,  strWM_OBC_MF,  strWM_OBC_SF,  strWM_OBC_LFO,  strMonth,  strYear,  strCreatedBy }, "dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
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
    public void Get_month_Yearwise_Semi_Fixed_Nature_Information_report(string strKey, string strOffice_ID, string strCreatedBy, string strMonth, string strYear)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (strKey == securityKey_DCS)
        {
            try
            {
                ds = apiprocedure.ByProcedure("usp_insertupdate_SEmi_fixed_nature_info_byDCS_API",
               new string[] { "flag", "Office_ID", "CreatedBy","Month",
                                     "Year" },
                 new string[] { "6", strOffice_ID, strCreatedBy, strMonth, strYear }, "dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
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
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    // Monthly Financial Information to be Feed By DCS


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Insert_Monthly_Finanacial_info_byDCS(string strKey, string strOffice_ID, string strDCS_Code, string strOI_Milk_amount, string strOI_DCS_Commition, string strOI_Ghee_Commition, string strOI_Cattle_feed_Commition, string strOI_Miniral_mixture_Commition, string strOI_Head_load, string strOI_BMC_Chilling_charges, string strOI_Local_milk_sale_amount, string strOI_Sample_milk_sale_amount, string strOI_Other, string strOE_Payment_to_producer, string strOE_Head_load, string strOE_Camical_detergent, string strOE_Traveling, string strOE_Stationary, string strOE_Computer_expense, string strOE_Office_expense, string strOE_General_body_meeting, string strOE_STS_Secretary, string strOE_STS_Tester_helper, string strOE_STS_AHC_AIworker, string strOE_Other, string strTotal_operating_income, string strTotal_operating_Expense, string strTotal_Profit_Loss, string strMonth, string strYear, string strCreatedBy)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (strKey == securityKey_DCS)
        {
            try
            {
                ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_Finanacial_info_byDCS_API",
                      new string[] { "flag", "Office_ID", "DCS_Code" ,"OI_Milk_amount","OI_DCS_Commition","OI_Ghee_Commition","OI_Cattle_feed_Commition","OI_Miniral_mixture_Commition","OI_Head_load","OI_BMC_Chilling_charges","OI_Local_milk_sale_amount","OI_Sample_milk_sale_amount","OI_Other","OE_Payment_to_producer","OE_Head_load","OE_Camical_detergent","OE_Traveling","OE_Stationary","OE_Computer_expense","OE_Office_expense","OE_General_body_meeting","OE_STS_Secretary","OE_STS_Tester_helper","OE_STS_AHC_AIworker","OE_Other","Total_operating_income","Total_operating_Expense","Total_Profit_Loss", "Month","Year", "CreatedBy" },
                      new string[] { "0", strOffice_ID,  strDCS_Code,  strOI_Milk_amount,  strOI_DCS_Commition,  strOI_Ghee_Commition,  strOI_Cattle_feed_Commition,  strOI_Miniral_mixture_Commition,  strOI_Head_load,  strOI_BMC_Chilling_charges,  strOI_Local_milk_sale_amount,  strOI_Sample_milk_sale_amount,  strOI_Other,  strOE_Payment_to_producer,  strOE_Head_load,  strOE_Camical_detergent,  strOE_Traveling,  strOE_Stationary,  strOE_Computer_expense,  strOE_Office_expense,  strOE_General_body_meeting,  strOE_STS_Secretary,  strOE_STS_Tester_helper,  strOE_STS_AHC_AIworker,  strOE_Other,  strTotal_operating_income,  strTotal_operating_Expense,  strTotal_Profit_Loss,  strMonth,  strYear,  strCreatedBy }, "dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
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
    public void Get_month_Yearwise_Monthly_Finanacial_info_byDCS_report(string strKey, string strOffice_ID, string strCreatedBy, string strMonth, string strYear)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (strKey == securityKey_DCS)
        {
            try
            {
                ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_Finanacial_info_byDCS_API",
               new string[] { "flag", "Office_ID", "CreatedBy","Month",
                                     "Year" },
                 new string[] { "6", strOffice_ID, strCreatedBy, strMonth, strYear }, "dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
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
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }



    //// Monthly Chilling Centre Report Feeded by Manager CC


    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public void Insert_Monthly_Chilling_Centre_Report(string strKey, string strOffice_ID, string OB_M_Qty, string strOB_M_Fat, string strOB_M_SNF, string strMilk_pur_good_Qty, string strMilk_pur_good_Fat, string strMilk_pur_good_SNF, string strMilk_pur_Sour_Qty, string strMilk_pur_Sour_Fat, string strMilk_pur_Sour_SNF, string strMilk_pur_Curdle_Qty, string strMilk_pur_Curdle_Fat, string strMilk_pur_Curdle_SNF, string strSC_milk_for_prod_Qty, string strSC_milk_for_prod_Fat, string strSC_milk_for_prod_SNF, string strMilk_dispatch_dairy_Qty, string strMilk_dispatch_dairy_Fat, string strMilk_dispatch_dairy_SNF, string strCB_M_Qty, string strCB_M_Fat, string strCB_M_SNF, string strWhite_butter_SC_OB_Qty, string strWhite_butter_SC_OB_Fat, string strWhite_butter_SC_OB_SNF, string strWhite_butter_SC_WBM_Qty, string strWhite_butter_SC_WBM_Fat, string strWhite_butter_SC_WBM_SNF, string strWhite_butter_SC_CB_Qty, string strWhite_butter_SC_CB_Fat, string strWhite_butter_SC_CB_SNF, string strMilk_Hand_variation_Q_Qty, string strMilk_Hand_variation_Q_Fat, string strMilk_Hand_variation_Q_SNF, string strMilk_Hand_variation_Per_Qty, string strMilk_Hand_variation_Per_Fat, string strMilk_Hand_variation_Per_SNF, string strProduct_mfg_variation_Q_Qty, string strProduct_mfg_variation_Q_Fat, string strProduct_mfg_variation_Q_SNF, string strProduct_mfg_variation_Per_Qty, string strProduct_mfg_variation_Per_Fat, string strProduct_mfg_variation_Per_SNF, string strTanker_milk_Rec_DP_Qty, string strTanker_milk_Rec_DP_Fat, string strTanker_milk_Rec_DP_SNF, string strTanker_milk_variation_Q_Qty, string strTanker_milk_variation_Q_Fat, string strTanker_milk_variation_Q_SNF, string strTanker_milk_variation_Per_Qty, string strTanker_milk_variation_Per_Fat, string strTanker_milk_variation_Per_SNF, string strStock_OB_WB, string strStock_OB_Ghee, string strStock_OB_Cattlefeed, string strStock_Manufa_WB, string strStock_Manufa_Ghee, string strStock_Manufa_Cattlefeed, string strStock_Rec_WB, string strStock_Rec_Ghee, string strStock_Rec_Cattlefeed, string strStock_Sold_WB, string strStock_Sold_Ghee, string strStock_Sold_Cattlefeed, string strStock_Dispatch_DP_WB, string strStock_Dispatch_DP_Ghee, string strStock_Dispatch_DP_Cattlefeed, string strStock_CB_WB, string strStock_CB_Ghee, string strStock_CB_Cattlefeed, string strDCS_CCTE_Headload_dcsnos_Unit, string strDCS_CCTE_Headload_dcsnos_AMT, string strDCS_CCTE_Vehicle_nos_Unit, string strDCS_CCTE_Vehicle_nos_AMT, string strCattle_FT_Vehicle_nos_Unit, string strCattle_FT_Vehicle_nos_AMT, string strCattle_FT_Loading_nos_Unit, string strCattle_FT_Loading_nos_AMT, string strCattle_FT_UnLoading_nos_Unit, string strCattle_FT_UnLoading_nos_AMT, string strExpend_Electricity_Unit, string strExpend_Electricity_AMT, string strExpend_Deisel_Unit, string strExpend_Deisel_AMT, string strExpend_Che_acid_Unit, string strExpend_Che_acid_AMT, string strExpend_Che_Alcohol_Unit, string strExpend_Che_Alcohol_AMT, string strExpend_Detgt_SS_Unit, string strExpend_Detgt_SS_AMT, string strExpend_Detgt_CS_Unit, string strExpend_Detgt_CS_AMT, string strExpend_Detgt_WS_Unit, string strExpend_Detgt_WS_AMT, string strExpend_CLabour_Unit, string strExpend_CLabour_AMT, string strExpend_Security_Unit, string strExpend_Security_AMT, string strExpend_Stationary_Unit, string strExpend_Stationary_AMT, string strOther_Expend_Count, string strMonth, string strYear, string strCreatedBy)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    this.Context.Response.ContentType = "application/json; charset=utf-8";
    //    if (strKey == securityKey_DCS)
    //    {
    //        try
    //        {
    //            ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_Chilling_center_Report_By_manager_API",
    //                  new string[] { "flag", "Office_ID","OB_M_Qty","OB_M_Fat","OB_M_SNF","Milk_pur_good_Qty","Milk_pur_good_Fat","Milk_pur_good_SNF","Milk_pur_Sour_Qty","Milk_pur_Sour_Fat","Milk_pur_Sour_SNF","Milk_pur_Curdle_Qty","Milk_pur_Curdle_Fat","Milk_pur_Curdle_SNF","SC_milk_for_prod_Qty","SC_milk_for_prod_Fat","SC_milk_for_prod_SNF","Milk_dispatch_dairy_Qty","Milk_dispatch_dairy_Fat","Milk_dispatch_dairy_SNF","CB_M_Qty","CB_M_Fat","CB_M_SNF","White_butter_SC_OB_Qty","White_butter_SC_OB_Fat","White_butter_SC_OB_SNF","White_butter_SC_WBM_Qty","White_butter_SC_WBM_Fat","White_butter_SC_WBM_SNF","White_butter_SC_CB_Qty","White_butter_SC_CB_Fat","White_butter_SC_CB_SNF","Milk_Hand_variation_Q_Qty","Milk_Hand_variation_Q_Fat","Milk_Hand_variation_Q_SNF","Milk_Hand_variation_Per_Qty","Milk_Hand_variation_Per_Fat","Milk_Hand_variation_Per_SNF","Product_mfg_variation_Q_Qty","Product_mfg_variation_Q_Fat","Product_mfg_variation_Q_SNF","Product_mfg_variation_Per_Qty","Product_mfg_variation_Per_Fat","Product_mfg_variation_Per_SNF","Tanker_milk_Rec_DP_Qty","Tanker_milk_Rec_DP_Fat","Tanker_milk_Rec_DP_SNF","Tanker_milk_variation_Q_Qty","Tanker_milk_variation_Q_Fat","Tanker_milk_variation_Q_SNF","Tanker_milk_variation_Per_Qty","Tanker_milk_variation_Per_Fat","Tanker_milk_variation_Per_SNF","Stock_OB_WB","Stock_OB_Ghee","Stock_OB_Cattlefeed","Stock_Manufa_WB","Stock_Manufa_Ghee","Stock_Manufa_Cattlefeed","Stock_Rec_WB","Stock_Rec_Ghee","Stock_Rec_Cattlefeed","Stock_Sold_WB","Stock_Sold_Ghee","Stock_Sold_Cattlefeed","Stock_Dispatch_DP_WB","Stock_Dispatch_DP_Ghee","Stock_Dispatch_DP_Cattlefeed","Stock_CB_WB","Stock_CB_Ghee","Stock_CB_Cattlefeed","DCS_CCTE_Headload_dcsnos_Unit","DCS_CCTE_Headload_dcsnos_AMT","DCS_CCTE_Vehicle_nos_Unit","DCS_CCTE_Vehicle_nos_AMT","Cattle_FT_Vehicle_nos_Unit","Cattle_FT_Vehicle_nos_AMT","Cattle_FT_Loading_nos_Unit","Cattle_FT_Loading_nos_AMT","Cattle_FT_UnLoading_nos_Unit","Cattle_FT_UnLoading_nos_AMT","Expend_Electricity_Unit","Expend_Electricity_AMT","Expend_Deisel_Unit","Expend_Deisel_AMT","Expend_Che_acid_Unit","Expend_Che_acid_AMT","Expend_Che_Alcohol_Unit","Expend_Che_Alcohol_AMT","Expend_Detgt_SS_Unit","Expend_Detgt_SS_AMT","Expend_Detgt_CS_Unit","Expend_Detgt_CS_AMT","Expend_Detgt_WS_Unit","Expend_Detgt_WS_AMT","Expend_CLabour_Unit","Expend_CLabour_AMT","Expend_Security_Unit","Expend_Security_AMT","Expend_Stationary_Unit","Expend_Stationary_AMT","Other_Expend_Count","Month",
    //                                 "Year", "CreatedBy" },
    //                  new string[] { "0", strOffice_ID,  strOB_M_Fat,  strOB_M_SNF,  strMilk_pur_good_Qty,  strMilk_pur_good_Fat,  strMilk_pur_good_SNF,  strMilk_pur_Sour_Qty,  strMilk_pur_Sour_Fat,  strMilk_pur_Sour_SNF,  strMilk_pur_Curdle_Qty,  strMilk_pur_Curdle_Fat,  strMilk_pur_Curdle_SNF,  strSC_milk_for_prod_Qty,  strSC_milk_for_prod_Fat,  strSC_milk_for_prod_SNF,  strMilk_dispatch_dairy_Qty,  strMilk_dispatch_dairy_Fat,  strMilk_dispatch_dairy_SNF,  strCB_M_Qty,  strCB_M_Fat,  strCB_M_SNF,  strWhite_butter_SC_OB_Qty,  strWhite_butter_SC_OB_Fat,  strWhite_butter_SC_OB_SNF,  strWhite_butter_SC_WBM_Qty,  strWhite_butter_SC_WBM_Fat,  strWhite_butter_SC_WBM_SNF,  strWhite_butter_SC_CB_Qty,  strWhite_butter_SC_CB_Fat,  strWhite_butter_SC_CB_SNF,  strMilk_Hand_variation_Q_Qty,  strMilk_Hand_variation_Q_Fat,  strMilk_Hand_variation_Q_SNF,  strMilk_Hand_variation_Per_Qty,  strMilk_Hand_variation_Per_Fat,  strMilk_Hand_variation_Per_SNF,  strProduct_mfg_variation_Q_Qty,  strProduct_mfg_variation_Q_Fat,  strProduct_mfg_variation_Q_SNF,  strProduct_mfg_variation_Per_Qty,  strProduct_mfg_variation_Per_Fat,  strProduct_mfg_variation_Per_SNF,  strTanker_milk_Rec_DP_Qty,  strTanker_milk_Rec_DP_Fat,  strTanker_milk_Rec_DP_SNF,  strTanker_milk_variation_Q_Qty,  strTanker_milk_variation_Q_Fat,  strTanker_milk_variation_Q_SNF,  strTanker_milk_variation_Per_Qty,  strTanker_milk_variation_Per_Fat,  strTanker_milk_variation_Per_SNF,strStock_OB_WB,  strStock_OB_Ghee,  strStock_OB_Cattlefeed,  strStock_Manufa_WB,  strStock_Manufa_Ghee,  strStock_Manufa_Cattlefeed,  strStock_Rec_WB,  strStock_Rec_Ghee,  strStock_Rec_Cattlefeed,  strStock_Sold_WB,  strStock_Sold_Ghee,  strStock_Sold_Cattlefeed,  strStock_Dispatch_DP_WB,  strStock_Dispatch_DP_Ghee,  strStock_Dispatch_DP_Cattlefeed,  strStock_CB_WB,  strStock_CB_Ghee,  strStock_CB_Cattlefeed,  strDCS_CCTE_Headload_dcsnos_Unit,  strDCS_CCTE_Headload_dcsnos_AMT,  strDCS_CCTE_Vehicle_nos_Unit,  strDCS_CCTE_Vehicle_nos_AMT,  strCattle_FT_Vehicle_nos_Unit,  strCattle_FT_Vehicle_nos_AMT,  strCattle_FT_Loading_nos_Unit,  strCattle_FT_Loading_nos_AMT,  strCattle_FT_UnLoading_nos_Unit,  strCattle_FT_UnLoading_nos_AMT,  strExpend_Electricity_Unit,  strExpend_Electricity_AMT,  strExpend_Deisel_Unit,  strExpend_Deisel_AMT,  strExpend_Che_acid_Unit,  strExpend_Che_acid_AMT,  strExpend_Che_Alcohol_Unit,  strExpend_Che_Alcohol_AMT,  strExpend_Detgt_SS_Unit,  strExpend_Detgt_SS_AMT,  strExpend_Detgt_CS_Unit,  strExpend_Detgt_CS_AMT,  strExpend_Detgt_WS_Unit,  strExpend_Detgt_WS_AMT,  strExpend_CLabour_Unit,  strExpend_CLabour_AMT,  strExpend_Security_Unit,  strExpend_Security_AMT,  strExpend_Stationary_Unit,  strExpend_Stationary_AMT,  strOther_Expend_Count,  strMonth,  strYear,  strCreatedBy }, "dataset");
    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
    //            }
    //            else
    //            {

    //                dt = ds.Tables[0];
    //                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //                Dictionary<string, object> row = null;

    //                foreach (DataRow rs in dt.Rows)
    //                {
    //                    row = new Dictionary<string, object>();
    //                    foreach (DataColumn col in dt.Columns)
    //                    {
    //                        row.Add(col.ColumnName, rs[col]);
    //                    }
    //                    rows.Add(row);
    //                }

    //                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
    //        }
    //        dt.Clear();
    //        ds.Clear();
    //    }
    //    else
    //    {
    //        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
    //    }
    //}

    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public void Insert_Monthly_Chilling_Centre_Report_Detail(string strKey, string strMCCR_ID, string strExpendditure_name, string strUnit, string strAmount)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    this.Context.Response.ContentType = "application/json; charset=utf-8";
    //    if (strKey == securityKey_DCS)
    //    {
    //        try
    //        {
    //            ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_Chilling_center_Report_By_manager_API",
    //                    new string[] { "flag", "MCCR_ID", "Expendditure_name", "Unit",
    //                                 "Amount" },
    //                        new string[] { "1", strMCCR_ID,  strExpendditure_name,  strUnit,  strAmount}, "dataset");
    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
    //            }
    //            else
    //            {

    //                dt = ds.Tables[0];
    //                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //                Dictionary<string, object> row = null;

    //                foreach (DataRow rs in dt.Rows)
    //                {
    //                    row = new Dictionary<string, object>();
    //                    foreach (DataColumn col in dt.Columns)
    //                    {
    //                        row.Add(col.ColumnName, rs[col]);
    //                    }
    //                    rows.Add(row);
    //                }

    //                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
    //        }
    //        dt.Clear();
    //        ds.Clear();
    //    }
    //    else
    //    {
    //        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
    //    }
    //}

    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public void Get_month_Yearwise_Monthly_Chilling_Centre_Report_report(string strKey, string strOffice_ID, string strCreatedBy, string strMonth, string strYear)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    this.Context.Response.ContentType = "application/json; charset=utf-8";
    //    if (strKey == securityKey_DCS)
    //    {
    //        try
    //        {
    //            ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_Chilling_center_Report_By_manager_API",
    //           new string[] { "flag", "Office_ID", "CreatedBy","Month",
    //                                 "Year" },
    //             new string[] { "6", strOffice_ID, strCreatedBy, strMonth, strYear }, "dataset");
    //            if (ds.Tables[0].Rows.Count == 0)
    //            {
    //                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
    //            }
    //            else
    //            {

    //                dt = ds.Tables[0];
    //                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //                Dictionary<string, object> row = null;

    //                foreach (DataRow rs in dt.Rows)
    //                {
    //                    row = new Dictionary<string, object>();
    //                    foreach (DataColumn col in dt.Columns)
    //                    {
    //                        row.Add(col.ColumnName, rs[col]);
    //                    }
    //                    rows.Add(row);
    //                }

    //                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "" }));
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
    //        }
    //        dt.Clear();
    //        ds.Clear();
    //    }
    //    else
    //    {
    //        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
    //    }
    //}


    // DCS Master Information


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Insert_DCS_Master_Information(string strKey, string strOffice_ID, string DCS_Code, string strVillage_name, string strVillage_Gis_code, string strMilk_route, string strAffiliated_chilling_center, string strDistrict, string strTehsil, string strBlock, string strField_officer, string strVEO, string strScheme_in_which_DCS_Open, string strCorporate_reg_number, string strCorporate_reg_Date, string strResistered_as_woman, string strUnion_mem_number, string strUnion_mem_Date, string strAi_center_type, string strB_M_C_capecity, string strB_M_C_Scheme, string strB_M_C_date, string strAffiliated_with_BMC, string strAssembly_Area, string strParliament_Area, string strMonth, string strYear, string strCreatedBy)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (strKey == securityKey_DCS)
        {
            try
            {
                ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_DCS_Information_system_API",
                      new string[] { "flag", "Office_ID", "DCS_Code" , "Village_name" ,"Village_Gis_code" ,"Milk_route ","Affiliated_chilling_center" ,"District" ,"Tehsil" ,"Block" ,"Field_officer" ,"VEO" ,"Scheme_in_which_DCS_Open" ,"Corporate_reg_number" ,"Corporate_reg_Date","Resistered_as_woman","Union_mem_number","Union_mem_Date","Ai_center_type","B_M_C_capecity","B_M_C_Scheme","B_M_C_date","Affiliated_with_BMC","Assembly_Area","Parliament_Area", "Month",
                                     "Year", "CreatedBy" },
                      new string[] { "0", strOffice_ID,  DCS_Code,  strVillage_name,  strVillage_Gis_code,  strMilk_route,  strAffiliated_chilling_center,  strDistrict,  strTehsil,  strBlock,  strField_officer,  strVEO,  strScheme_in_which_DCS_Open,  strCorporate_reg_number,  strCorporate_reg_Date,  strResistered_as_woman,  strUnion_mem_number,  strUnion_mem_Date,  strAi_center_type,  strB_M_C_capecity,  strB_M_C_Scheme,  strB_M_C_date,  strAffiliated_with_BMC,  strAssembly_Area,  strParliament_Area,  strMonth,  strYear,  strCreatedBy}, "dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
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
    public void Insert_DCS_Master_Information_Detail(string strKey, string strDCS_IS_ID, string strAEFFacility_Name, string strAEF_OWN, string strAEF_Scheme, string strAEF_InstDate)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (strKey == securityKey_DCS)
        {
            try
            {
                ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_DCS_Information_system_API",
                        new string[] { "flag", "DCS_IS_ID", "AEFFacility_Name", "AEF_OWN", "AEF_Scheme",
                                     "AEF_InstDate" },
                        new string[] { "1", strDCS_IS_ID,  strAEFFacility_Name,  strAEF_OWN,  strAEF_Scheme,  strAEF_InstDate}, "dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
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
    public void Insert_DCS_Master_Information_Detail_new(string strKey, string strDCS_IS_ID, string strAEFFacility_Name, string strAEF_OWN, string strAEF_Scheme, string strAEF_InstDate, string strrow)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (strKey == securityKey_DCS)
        {
            try
            {
                string[] AEFFacility_Name = strAEFFacility_Name.Split(',');
                string[] AEF_OWN = strAEF_OWN.Split(',');
                string[] AEF_Scheme = strAEF_Scheme.Split(',');
                string[] AEF_InstDate = strAEF_InstDate.Split(',');
                string abc = AEFFacility_Name[0].ToString();
                for (int i = 0; i < int.Parse(strrow); i++)
                {
                    ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_DCS_Information_system_API",
                            new string[] { "flag", "DCS_IS_ID", "AEFFacility_Name", "AEF_OWN", "AEF_Scheme",
                                     "AEF_InstDate" },
                            new string[] { "1", strDCS_IS_ID, AEFFacility_Name[i], AEF_OWN[i], AEF_Scheme[i], AEF_InstDate[i] }, "dataset");
                }
                    if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
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
    public void Get_month_Yearwise_DCS_Master_Information_report(string strKey, string strOffice_ID, string strCreatedBy, string strMonth, string strYear)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (strKey == securityKey_DCS)
        {
            try
            {
                ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_DCS_Information_system_API",
               new string[] { "flag", "Office_ID", "CreatedBy","Month",
                                     "Year" },
                 new string[] { "7", strOffice_ID, strCreatedBy, strMonth, strYear }, "dataset");
                if (ds.Tables.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
                }
                else
                {
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
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
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }


    // Monthly BMC Information to be Feed By DCS
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Insert_Monthly_BMC_Information_ByDCS(string strKey, string strOffice_ID, string strDCS_Code, string strMilk_handeled_during_month, string strBMC_running_hours, string strElectricity_unit, string strElectricity_Amount, string strDiesel_consumed, string strDiesel_Amount, string strOil_consumed, string strOil_Amount, string strCleaning_agent_consumed, string strCleaning_agent_Amount, string strRepair_mantenance_expence, string strAdditional_M_P_deploy_num, string strAdditional_M_P_deploy_Amount_salary, string strMonth, string strYear, string strCreatedBy)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (strKey == securityKey_DCS)
        {
            try
            {
                ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_BMC_Information_system_API",
                      new string[] { "flag", "Office_ID", "DCS_Code" , "Milk_handeled_during_month" ,"BMC_running_hours" ,"Electricity_unit","Electricity_Amount" ,"Diesel_consumed" ,"Diesel_Amount" ,"Oil_consumed" ,"Oil_Amount" ,"Cleaning_agent_consumed" ,"Cleaning_agent_Amount" ,"Repair_mantenance_expence" ,"Additional_M_P_deploy_num","Additional_M_P_deploy_Amount_salary", "Month",
                                     "Year", "CreatedBy" },
                      new string[] { "0", strOffice_ID,  strDCS_Code,  strMilk_handeled_during_month,  strBMC_running_hours,  strElectricity_unit,  strElectricity_Amount,  strDiesel_consumed,  strDiesel_Amount,  strOil_consumed,  strOil_Amount,  strCleaning_agent_consumed,  strCleaning_agent_Amount,  strRepair_mantenance_expence,  strAdditional_M_P_deploy_num,  strAdditional_M_P_deploy_Amount_salary,  strMonth,  strYear,  strCreatedBy }, "dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Input" }));
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
    public void Get_month_Yearwise_Monthly_BMC_Information_ByDCS_report(string strKey, string strOffice_ID, string strCreatedBy, string strMonth, string strYear)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (strKey == securityKey_DCS)
        {
            try
            {
                ds = apiprocedure.ByProcedure("usp_insertupdate_monthly_BMC_Information_system_API",
               new string[] { "flag", "Office_ID", "CreatedBy","Month",
                                     "Year" },
                 new string[] { "6", strOffice_ID, strCreatedBy, strMonth, strYear }, "dataset");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
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
            dt.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }
}
