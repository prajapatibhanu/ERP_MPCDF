using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for MPCDF_WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class MPCDF_WebService : System.Web.Services.WebService
{
    string securityKey = "SFA_MPCDF-ERP";
    APIProcedure apiprocedure = new APIProcedure();
    CommanddlFill commanddlfill = new CommanddlFill();
    CultureInfo cult = new CultureInfo("en-IN", true);
    IFormatProvider culture = new CultureInfo("en-US", true);

    public MPCDF_WebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
	
	[WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetDSProducerDetailByQRCode_TinyURLToLong(string Key, string TinyURL)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                System.Net.HttpWebRequest objWebRequest;
                System.Net.HttpWebResponse objWebResponse;
                System.IO.StreamReader srReader;
                string strHTML;
                objWebRequest = (System.Net.HttpWebRequest)System.Net
                   .WebRequest.Create(TinyURL);
                objWebRequest.Method = "Get";
                objWebResponse = (System.Net.HttpWebResponse)objWebRequest
                   .GetResponse();
                srReader = new System.IO.StreamReader(objWebResponse
                   .GetResponseStream());
                strHTML = objWebResponse.ResponseUri.ToString();
                srReader.Close();
                objWebResponse.Close();
                objWebRequest.Abort();
                this.Context.Response.Write(serializer.Serialize(new { List = strHTML, status = "1", Error = "Success" }));

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
    public void GetDSProducerDetailByQRCode_New(string Key, string ProducerId)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                
                DataSet ds1 = apiprocedure.ByProcedure("sp_Mst_DailyMilkCollection",
                    new string[] { "Flag", "ProducerId" },
                    new string[] { "15", ProducerId }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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
	
	
	
	    #region --  Society Passbook Web Services --

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Society_Passbook_Info(string Key, string OfficeId)
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

                ds1 = apiprocedure.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                          new string[] { "flag", "OfficeId" },
                          new string[] { "15", OfficeId }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Society_Passbook_InfoDateWise(string Key, string FromDate, string ToDate, string OfficeId)
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

                ds1 = apiprocedure.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                          new string[] { "flag", "FDT", "TDT", "OfficeId" },
                          new string[] { "16", FromDate, ToDate, OfficeId }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Society_Passbook_InfoShiftWise(string Key, string FromDate, string ToDate, string OfficeId, string ShiftName)
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

                ds1 = apiprocedure.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                          new string[] { "flag", "FDT", "TDT", "OfficeId", "EntryShift" },
                          new string[] { "17", FromDate, ToDate, OfficeId, ShiftName }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    #endregion

    #region --  Producer Passbook Web Services --

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Producer_Passbook_Info(string Key, string FromDate, string ToDate, string OfficeId, string ProducerId)
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

                ds1 = apiprocedure.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                          new string[] { "flag", "FDT", "TDT", "OfficeId", "ProducerId" },
                          new string[] { "9", FromDate, ToDate, OfficeId, ProducerId }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Producer_Passbook_InfoDateWise(string Key, string FromDate, string ToDate, string OfficeId, string ProducerId)
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

                ds1 = apiprocedure.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                          new string[] { "flag", "FDT", "TDT", "OfficeId", "ProducerId" },
                          new string[] { "10", FromDate, ToDate, OfficeId, ProducerId }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Producer_Passbook_InfoShiftWise(string Key, string FromDate, string ToDate, string OfficeId, string ProducerId, string ShiftName)
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

                ds1 = apiprocedure.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                          new string[] { "flag", "FDT", "TDT", "OfficeId", "ProducerId", "EntryShift" },
                          new string[] { "11", FromDate, ToDate, OfficeId, ProducerId, ShiftName }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Producer_Passbook_InfoTotalPurchaseValue(string Key, string FromDate, string ToDate, string OfficeId, string ProducerId)
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

                ds1 = apiprocedure.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                          new string[] { "flag", "FDT", "TDT", "OfficeId", "ProducerId" },
                          new string[] { "12", FromDate, ToDate, OfficeId, ProducerId }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Producer_Passbook_InfoTotalPurchaseDetails(string Key, string FromDate, string ToDate, string OfficeId, string ProducerId)
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

                string Fdate = "";
                string Tdate = "";

                if (FromDate != "")
                {
                    Fdate = Convert.ToDateTime(FromDate, cult).ToString("yyyy/MM/dd");
                }

                if (ToDate != "")
                {
                    Tdate = Convert.ToDateTime(ToDate, cult).ToString("yyyy/MM/dd");
                }

                ds1 = apiprocedure.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                            new string[] { "flag", "ProducerId", "FDT", "TDT", "OfficeId" },
                            new string[] { "18", ProducerId, Fdate, Tdate, OfficeId }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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
     
    #endregion

    #region -- LocalSale  Web Services --


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void LocalSale_SetMilkRateByOffice_Get(string Key, string Office_ID, string ItemCat_id, string ItemType_id)
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

                ds1 = apiprocedure.ByProcedure("USP_Mst_ItemSaleRate",
                             new string[] { "flag", "Office_ID", "ItemCat_id", "ItemType_id" },
                             new string[] { "3", Office_ID, ItemCat_id, ItemType_id }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void LocalSale_SetMilkRateByOffice_Insert(string Key, string ItemCat_id, string ItemType_id, string Item_id,
       string Unit_id, string Office_ID, string DistributorDCSPrice, string DCSMargine, string SecretaryPrice,
        string SecretaryMargine, string MRP, string EffectiveDate, string Emp_Id)
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

                 DateTime date3 = DateTime.ParseExact(EffectiveDate, "yyyy/MM/dd", cult);


                ds1 = apiprocedure.ByProcedure("USP_Mst_ItemSaleRate",
                      new string[] { "flag", "ItemCat_id", "ItemType_id", "Item_id", "Unit_id", "Office_ID", "DistributorDCSPrice",
                          "DCSMargine", "SecretaryPrice", "SecretaryMargine", "MRP", "EffectiveDate", "CreatedBy" },
                      new string[] { "0", ItemCat_id, ItemType_id, Item_id, Unit_id, Office_ID, DistributorDCSPrice,
                          DCSMargine, SecretaryPrice, SecretaryMargine, MRP, date3.ToString(), Emp_Id }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void LocalSale_SetMilkRateByOffice_ViewData(string Key, string Office_ID)
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

                ds1 = apiprocedure.ByProcedure("USP_Mst_ItemSaleRate",
                                        new string[] { "flag", "Office_Id" },
                                        new string[] { "4", Office_ID }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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
     
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void LocalSale_GetAvailableItemStock(string Key, string Office_Id, string ItemCat_id, string ItemType_id, string Item_id)
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

                ds1 = apiprocedure.ByProcedure("USP_Trn_LocalSaleDetail_Dcs",
                                        new string[] { "flag", "Office_Id", "ItemCat_id", "ItemType_id", "Item_id" },
                                        new string[] { "5", Office_Id, ItemCat_id, ItemType_id, Item_id }, "Dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void LocalSale_GetAvailableRowMilkStock(string Key, string TodayDate, string EntryShift, string Office_Id,
        string ItemCat_id, string ItemType_id, string Item_id, string I_OfficeTypeID)
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

                ds1 = apiprocedure.ByProcedure("USP_Trn_LocalSaleDetail_Dcs",
                         new string[] { "flag", "EntryDate", "EntryShift", "Office_Id", "ItemCat_id", "ItemType_id", "Item_id", "I_OfficeTypeID" },
                         new string[] { "4", Convert.ToDateTime(TodayDate, cult).ToString("yyyy-MM-dd"), EntryShift, Office_Id, ItemCat_id, 
                             ItemType_id,Item_id, I_OfficeTypeID }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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


    #endregion
      
    #region -- Item Inward Page Web Services --
    // Item Inward Page

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void InwardItem_View(string Key, string Office_ID, string FilterDate)
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

                string date = Convert.ToDateTime(FilterDate, cult).ToString("yyyy/MM/dd");

                ds1 = apiprocedure.ByProcedure("Sp_tblSpItemStock",
                                        new string[] { "flag", "Office_Id", "Warehouse_id", "TranDt" },
                                        new string[] { "6", Office_ID, "0", date }, "dataset");

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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void InwardItem_Delete(string Key, string ItmStock_id, string Emp_Id, string IPAddress)
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
                ds1 = apiprocedure.ByProcedure("Sp_tblSpItemStock",
                                               new string[] { "flag", "ItmStock_id", "CreatedBy", "PageName", "Remark", "ipaddress" },
                                               new string[] { "8", ItmStock_id, Emp_Id, "InwardItem.aspx", "Item Inward Record Deleted", IPAddress }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Login Credentials." }));
                }
                else
                {
                    string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();

                    this.Context.Response.Write(serializer.Serialize(new { List = success, status = "1", Error = "Success" }));

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
    public void InwardItem_Insert(string Key, string ItemCat_id, string ItemType_id, string Item_id,
        string Item_Quantity, string Inward_date, string Office_Id, string Emp_Id, string Remark, string ipaddress, string MRP, string Amount)
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

                ds1 = apiprocedure.ByProcedure("Sp_tblSpItemStock",
                                               new string[] { "flag", "ItemCat_id", "ItemType_id", "Item_id", "Cr", "Dr", "Rate", "Amount", "Warehouse_id", "Depart_Id", "TranDt", "Office_Id", "CreatedBy", "PageName", "Remark", "ipaddress" },
                                               new string[] { "5", ItemCat_id, ItemType_id, Item_id, Item_Quantity, "0", MRP, Amount, "0", "0", Inward_date, Office_Id, Emp_Id, "InwardItem.aspx", Remark, ipaddress }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Login Credentials." }));
                }
                else
                {
                    string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();

                    this.Context.Response.Write(serializer.Serialize(new { List = success, status = "1", Error = "Success" }));

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
    public void InwardItem_Update(string Key, string ItemCat_id, string ItemType_id, string Item_id,
        string Item_Quantity, string Inward_date, string Office_Id, string Emp_Id, string Remark, string ipaddress, string ItmStock_id, string MRP, string Amount)
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


                ds1 = apiprocedure.ByProcedure("Sp_tblSpItemStock",
                                               new string[] { "flag", "ItemCat_id", "ItemType_id", "Item_id", "Cr", "Dr", "Rate", "Amount", "Warehouse_id", "Depart_Id", "TranDt", "Office_Id", "ItmStock_id", "CreatedBy", "PageName", "Remark", "ipaddress" },
                                               new string[] { "7", ItemCat_id, ItemType_id, Item_id, Item_Quantity, "0", MRP, "0", Amount, "0", Inward_date, Office_Id, ItmStock_id, Emp_Id, "InwardItem.aspx", Remark, ipaddress }, "dataset");


                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Login Credentials." }));
                }
                else
                {
                    string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();

                    this.Context.Response.Write(serializer.Serialize(new { List = success, status = "1", Error = "Success" }));

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


    #endregion
     
    #region -- Milk Collection Web Services --

    // DailyMilkCollection

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void DailyMilkCollection_ItemCategory(string Key)
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

                ds1 = apiprocedure.ByProcedure("Sp_MstLocalSale",
                               new string[] { "flag" },
                               new string[] { "4" }, "dataset");

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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void DailyMilkCollection_ItemType(string Key, string ItemCat_id)
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

                ds1 = apiprocedure.ByProcedure("Sp_MstLocalSale",
                               new string[] { "flag", "ItemCat_id" },
                               new string[] { "5", ItemCat_id }, "dataset");

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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void DailyMilkCollection_Item(string Key, string ItemCat_id, string ItemType_id, string Office_ID)
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

                ds1 = apiprocedure.ByProcedure("Sp_MstLocalSale",
                             new string[] { "flag", "ItemCat_id", "ItemType_id", "Office_ID" },
                             new string[] { "6", ItemCat_id, ItemType_id, Office_ID }, "dataset");

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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void DailyMilkCollection_ItemDetail(string Key, string ItemCat_id, string ItemType_id, string Item_id, string Office_ID)
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


                if (Item_id == apiprocedure.DcsRawMilkItemId_ID())
                {
                    ds1 = apiprocedure.ByProcedure("Sp_MstLocalSale",
                            new string[] { "flag", "ItemCat_id", "ItemType_id", "Office_ID", "Item_id" },
                            new string[] { "8", ItemCat_id, ItemType_id, Office_ID, Item_id }, "dataset");

                }
                else
                {
                    ds1 = apiprocedure.ByProcedure("Sp_MstLocalSale",
                            new string[] { "flag", "ItemCat_id", "ItemType_id", "Item_id", "Office_ID" },
                            new string[] { "7", ItemCat_id, ItemType_id, Item_id, Office_ID }, "dataset");
                }


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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void DailyMilkCollectionItem_Insert_LocalSale(string Key, string Producer_Type,
        string Office_ID, string ProducerId, string ProducerCode, string ProducerName, string Emp_ID, string GetLocalIPAddress
        , string ItemCat_id, string ItemType_id, string Item_id, string Unit_id, string I_Quantity, string DistributorDCSPrice
        , string DCSMargine, string SecretaryPrice, string SecretaryMargine, string MRP, string TotalAmount)
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

                if (Producer_Type == "" && Office_ID == "" && ProducerId == "" && ProducerCode == "" && ProducerName == "" && Emp_ID == "" && GetLocalIPAddress == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    if (
                        ItemCat_id.ToString().Split(',').Length != ItemType_id.ToString().Split(',').Length ||
                        ItemCat_id.ToString().Split(',').Length != Item_id.ToString().Split(',').Length ||
                        ItemCat_id.ToString().Split(',').Length != Unit_id.ToString().Split(',').Length ||
                        ItemCat_id.ToString().Split(',').Length != I_Quantity.ToString().Split(',').Length ||
                        ItemCat_id.ToString().Split(',').Length != DistributorDCSPrice.ToString().Split(',').Length ||
                        ItemCat_id.ToString().Split(',').Length != DCSMargine.ToString().Split(',').Length ||
                        ItemCat_id.ToString().Split(',').Length != SecretaryPrice.ToString().Split(',').Length ||

                        ItemCat_id.ToString().Split(',').Length != SecretaryMargine.ToString().Split(',').Length ||
                        ItemCat_id.ToString().Split(',').Length != MRP.ToString().Split(',').Length ||
                        ItemCat_id.ToString().Split(',').Length != TotalAmount.ToString().Split(',').Length
                        )
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Item Id Type pass " + ItemCat_id.ToString().Split(',').Length + " argument(s) and Item Quantity pass " + ItemType_id.ToString().Split(',').Length + " argument(s) and Adv Card pass " }));
                    }
                    else
                    {

                        DataTable dtable = new DataTable();
                        DataRow dr;

                        dtable.Columns.Add(new DataColumn("ItemCat_id", typeof(int)));
                        dtable.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
                        dtable.Columns.Add(new DataColumn("Item_id", typeof(int)));
                        dtable.Columns.Add(new DataColumn("Unit_id", typeof(int)));
                        dtable.Columns.Add(new DataColumn("I_Quantity", typeof(decimal)));
                        dtable.Columns.Add(new DataColumn("DistributorDCSPrice", typeof(decimal)));
                        dtable.Columns.Add(new DataColumn("DCSMargine", typeof(decimal)));
                        dtable.Columns.Add(new DataColumn("SecretaryPrice", typeof(decimal)));
                        dtable.Columns.Add(new DataColumn("SecretaryMargine", typeof(decimal)));
                        dtable.Columns.Add(new DataColumn("MRP", typeof(decimal)));
                        dtable.Columns.Add(new DataColumn("NetAmount", typeof(decimal)));

                        int len = ItemCat_id.ToString().Split(',').Length;

                        for (int i = 0; i < len; i++)
                        {
                            dr = dtable.NewRow();
                            dr[0] = ItemCat_id.ToString().Split(',')[i];
                            dr[1] = ItemType_id.ToString().Split(',')[i];
                            dr[2] = Item_id.ToString().Split(',')[i];
                            dr[3] = Unit_id.ToString().Split(',')[i];
                            dr[4] = I_Quantity.ToString().Split(',')[i];
                            dr[5] = DistributorDCSPrice.ToString().Split(',')[i];
                            dr[6] = DCSMargine.ToString().Split(',')[i];
                            dr[7] = SecretaryPrice.ToString().Split(',')[i];
                            dr[8] = SecretaryMargine.ToString().Split(',')[i];
                            dr[9] = MRP.ToString().Split(',')[i];
                            dr[10] = TotalAmount.ToString().Split(',')[i];

                            dtable.Rows.Add(dr);


                        }

                        if (dtable.Rows.Count == 0)
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Multipal Value can't Blank" }));
                        }
                        else
                        {
                            ds1 = apiprocedure.ByProcedure("USP_Trn_LocalSaleDetail_Dcs",
                                                        new string[] { "Flag" 
                                                        ,"Office_ID"
                                                        ,"Producer_Type" 
                                                        ,"ProducerId" 
                                                        ,"UserName"
                                                        ,"ProducerName"
                                                        ,"NetAmount"
                                                        ,"CreatedBy" 
                                                        ,"CreatedBy_IP" 
                                            },
                                                        new string[] { "0"
                                                      ,Office_ID
                                                      ,Producer_Type
                                                      ,ProducerId
                                                      ,ProducerCode
                                                      ,ProducerName
                                                      ,"0.00"
                                                      ,Emp_ID
                                                      ,GetLocalIPAddress  
                                            },
                                                       new string[] { "type_Trn_LocalSaleDetailChild_Dcs" },
                                                       new DataTable[] { dtable }, "TableSave");
                        }


                    }

                }


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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void DailyMilkCollection_SaleInvoiceDetail(string Key, string Office_ID, string FilterDate)
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

                string date = Convert.ToDateTime(FilterDate, cult).ToString("yyyy/MM/dd");

                ds1 = apiprocedure.ByProcedure("USP_Trn_LocalSaleDetail_Dcs",
                         new string[] { "flag", "Office_ID", "InvoiceDt" },
                         new string[] { "1", Office_ID, date }, "dataset");

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


    #endregion
     
    #region -- DCS Web Services --

    // DCS MILK DISPATCH & CC/BMC Milk Receive

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void DCSMilkDispatch_SaleDetail(string Key, string Office_ID, string FilterDate)
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
                string date = "";

                if (FilterDate != "")
                {
                    date = Convert.ToDateTime(FilterDate, cult).ToString("yyyy-MM-dd");
                }
                else
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Date" }));
                }

                ds1 = apiprocedure.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                         new string[] { "flag", "OfficeId", "FilterDate", "Item_id" },
                         new string[] { "13", Office_ID, date, apiprocedure.DcsRawMilkItemId_ID() }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void DCSMilkDispatch_InsertData(string Key, string I_OfficeID, string I_OfficeTypeID, string ReportingUnitID,
                string V_MilkDispatchType, string I_TotalCans, string V_VehicleNo, string V_DriverName,
                string V_DriverMobileNo, string V_EntryType, string D_MilkQuality, string V_Shift,
                string D_MilkQuantityInLtr, string FATPer, string CLR, string SNFPer, string Emp_Id,
                string V_Temp, string MilkSaleQty, string MilkSaleRatePerLtr, string MilkSaleAmount,
                string NetMilkQtyInKG, string NetFATInKG, string NetSNFInKG, string Sealtype, string ChamberType,
                string TI_SealColorID, string V_SealNo, string V_SealRemark,string Rate,string Amount)
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

                DataTable dtable = new DataTable();
                DataRow dr;


                if (I_OfficeID == "" || I_OfficeTypeID == "" || ReportingUnitID == "" || V_MilkDispatchType == "" || V_VehicleNo == ""
                || V_DriverName == "" || V_DriverMobileNo == "" || V_EntryType == "" || D_MilkQuality == "" || V_Shift == "" || D_MilkQuantityInLtr == ""
                || FATPer == "" || CLR == "" || SNFPer == "" || Emp_Id == "" || V_Temp == "" || NetMilkQtyInKG == "" || NetFATInKG == "" || NetSNFInKG == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                    return;
                }


                if (V_MilkDispatchType == "Cans")
                {
                    dtable = null;

                    if (I_TotalCans == "" || I_TotalCans == "0" || I_TotalCans == "0.00")
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill Total Cans For Milk Dispatch Type - Cans" }));
                        return;
                    }
                }

                if (V_MilkDispatchType == "Tanker")
                {
                    I_TotalCans = "";

                    if (Sealtype == "" || ChamberType == "" || TI_SealColorID == "" || V_SealNo == "")
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill Sealtype,ChamberType,TI_SealColorID,V_SealNo For Milk Dispatch Type - Tanker" }));
                        return;
                    }

                    else
                    {
                        dtable.Columns.Add(new DataColumn("Sealtype", typeof(string)));
                        dtable.Columns.Add(new DataColumn("ChamberType", typeof(string)));
                        dtable.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));
                        dtable.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
                        dtable.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));

                        int len = Sealtype.ToString().Split(',').Length;

                        for (int i = 0; i < len; i++)
                        {
                            dr = dtable.NewRow();
                            dr[0] = Sealtype.ToString().Split(',')[i];
                            dr[1] = ChamberType.ToString().Split(',')[i];
                            dr[2] = TI_SealColorID.ToString().Split(',')[i];
                            dr[3] = V_SealNo.ToString().Split(',')[i];
                            dr[4] = V_SealRemark.ToString().Split(',')[i];
                            dtable.Rows.Add(dr);
                        }

                    }
                }

                DataSet dsValidationRuntime = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_Dcs",
                     new string[] { "flag", "DT_Date", "V_Shift", "I_OfficeID" },
                     new string[] { "3", Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"), cult).ToString("yyyy-MM-dd"), V_Shift, I_OfficeID }, "dataset");


                if (dsValidationRuntime.Tables[0].Rows.Count > 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Only 1 time Dispatch Alow For Every Shift" }));
                    return;
                }


                else
                {
                    ds1 = null;
                    ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_Dcs",
                                     new string[] { "flag", 
                                                "I_OfficeID",
                                                "I_OfficeTypeID",
                                                "AttachedToCC",
                                                "V_MilkDispatchType",
                                                "I_TotalCans",
                                                "V_VehicleNo",
                                                "V_DriverName",
                                                "V_DriverMobileNo",
                                                "V_EntryType",
                                                "D_MilkQuality",
                                                "V_Shift",
                                                "D_MilkQuantity",
                                                "FAT",
                                                "CLR",
                                                "SNF", 
                                                "I_CreatedByEmpID",
                                                "V_Remark",
                                                "V_EntryFrom",
                                                "V_Temp",
                                                "MilkSaleQty",
                                                "MilkSaleRatePerLtr",
                                                "MilkSaleAmount",
                                                "NetMilkQtyInKG",
                                                "NetFATInKG",
                                                "NetSNFInKG",
												"Rate",
												"Amount"
                                                 },

                                    new string[] { "2",  
                                                I_OfficeID,
                                                I_OfficeTypeID, 
                                                ReportingUnitID,
                                                V_MilkDispatchType,
                                                I_TotalCans,
                                                V_VehicleNo,
                                                V_DriverName,
                                                V_DriverMobileNo,
                                                V_EntryType,
                                                D_MilkQuality,
                                                V_Shift,
                                                D_MilkQuantityInLtr,
                                                FATPer,
                                                CLR,
                                                SNFPer, 
                                                Emp_Id,
                                                "",
                                                "App",
                                                V_Temp,
                                                MilkSaleQty,
                                                MilkSaleRatePerLtr,
                                                MilkSaleAmount,
                                                NetMilkQtyInKG,
                                                NetFATInKG,
                                                NetSNFInKG,
												Rate,
												Amount
                                                },
                                 new string[] { "type_Trn_TankerSealDetails_Dcs" },
                                 new DataTable[] { dtable }, "TableSave");



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
    public void DCSMilkDispatch_ReceiveChallanDetails(string Key, string Office_ID)
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

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_Dcs",
                     new string[] { "flag", "AttachedToCC", "V_EntryType" },
                     new string[] { "6", Office_ID, "Out" }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void DCSMilkDispatch_ReceiveChallanWiseMoreDetails(string Key, string Office_ID, string I_EntryID)
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

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_Dcs",
                         new string[] { "flag", "AttachedToCC", "V_EntryType", "I_EntryID" },
                         new string[] { "7", Office_ID, "Out", I_EntryID }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void DCSMilkReceive_InsertData(string Key, string I_OfficeID, string I_OfficeTypeID,
                string V_MilkDispatchType, string I_TotalCans, string V_VehicleNo, string V_DriverName,
                string V_DriverMobileNo, string V_EntryType, string D_MilkQuality, string V_Shift,
                string D_MilkQuantityInLtr, string FATPer, string CLR, string SNFPer, string Emp_Id,
                string V_Temp, string MilkSaleQty, string MilkSaleRatePerLtr, string MilkSaleAmount,
                string NetMilkQtyInKG, string NetFATInKG, string NetSNFInKG, string Sealtype, string ChamberType,
                string TI_SealColorID, string V_SealNo, string V_SealRemark, string V_ChallanNo,string Rate,string Amount)
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

                DataTable dtable = new DataTable();
                DataRow dr;


                if (V_ChallanNo == "" || I_OfficeID == "" || I_OfficeTypeID == "" || V_MilkDispatchType == "" || V_VehicleNo == ""
                || V_DriverName == "" || V_DriverMobileNo == "" || V_EntryType == "" || D_MilkQuality == "" || V_Shift == "" || D_MilkQuantityInLtr == ""
                || FATPer == "" || CLR == "" || SNFPer == "" || Emp_Id == "" || V_Temp == "" || NetMilkQtyInKG == "" || NetFATInKG == "" || NetSNFInKG == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                    return;
                }


                if (V_MilkDispatchType == "Cans")
                {
                    dtable = null;

                    if (I_TotalCans == "" || I_TotalCans == "0" || I_TotalCans == "0.00")
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill Total Cans For Milk Dispatch Type - Cans" }));
                        return;
                    }
                }

                if (V_MilkDispatchType == "Tanker")
                {
                    I_TotalCans = "";

                    if (Sealtype == "" || ChamberType == "" || TI_SealColorID == "" || V_SealNo == "")
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill Sealtype,ChamberType,TI_SealColorID,V_SealNo For Milk Dispatch Type - Tanker" }));
                        return;
                    }

                    else
                    {
                        dtable.Columns.Add(new DataColumn("Sealtype", typeof(string)));
                        dtable.Columns.Add(new DataColumn("ChamberType", typeof(string)));
                        dtable.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));
                        dtable.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
                        dtable.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));

                        int len = Sealtype.ToString().Split(',').Length;

                        for (int i = 0; i < len; i++)
                        {
                            dr = dtable.NewRow();
                            dr[0] = Sealtype.ToString().Split(',')[i];
                            dr[1] = ChamberType.ToString().Split(',')[i];
                            dr[2] = TI_SealColorID.ToString().Split(',')[i];
                            dr[3] = V_SealNo.ToString().Split(',')[i];
                            dr[4] = V_SealRemark.ToString().Split(',')[i];
                            dtable.Rows.Add(dr);
                        }

                    }
                }

                DataSet dsValidationRuntime = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_Dcs",
                     new string[] { "flag", "V_ChallanNo", "V_EntryType", "I_OfficeID" },
                     new string[] { "12", V_ChallanNo, "In", I_OfficeID }, "dataset");

                if (dsValidationRuntime.Tables[0].Rows.Count > 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "This Challan NO - " + V_ChallanNo + " Already Exist" }));
                    return;
                }


                else
                {
                    ds1 = null;
                    ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_Dcs",
                                     new string[] { "flag", 
                                                "I_OfficeID",
                                                "I_OfficeTypeID",
                                                "AttachedToCC",
                                                "V_MilkDispatchType",
                                                "I_TotalCans",
                                                "V_VehicleNo",
                                                "V_DriverName",
                                                "V_DriverMobileNo",
                                                "V_EntryType",
                                                "D_MilkQuality",
                                                "V_Shift",
                                                "D_MilkQuantity",
                                                "FAT",
                                                "CLR",
                                                "SNF", 
                                                "I_CreatedByEmpID",
                                                "V_Remark",
                                                "V_EntryFrom",
                                                "V_Temp",
                                                "MilkSaleQty",
                                                "MilkSaleRatePerLtr",
                                                "MilkSaleAmount",
                                                "NetMilkQtyInKG",
                                                "NetFATInKG",
                                                "NetSNFInKG",
                                                "V_ChallanNo",
												"Rate",
												"Amount"
                                                 },

                                    new string[] { "8",  
                                                I_OfficeID,
                                                I_OfficeTypeID, 
                                                I_OfficeTypeID,
                                                V_MilkDispatchType,
                                                I_TotalCans,
                                                V_VehicleNo,
                                                V_DriverName,
                                                V_DriverMobileNo,
                                                V_EntryType,
                                                D_MilkQuality,
                                                V_Shift,
                                                D_MilkQuantityInLtr,
                                                FATPer,
                                                CLR,
                                                SNFPer, 
                                                Emp_Id,
                                                "",
                                                "App",
                                                V_Temp,
                                                MilkSaleQty,
                                                MilkSaleRatePerLtr,
                                                MilkSaleAmount,
                                                NetMilkQtyInKG,
                                                NetFATInKG,
                                                NetSNFInKG,
                                                V_ChallanNo,
												Rate,
												Amount
                                                },
                                 new string[] { "type_Trn_TankerSealDetails_Dcs" },
                                 new DataTable[] { dtable }, "TableSave");



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
    public void DCSMilkDispatch_Receive_Report(string Key, string Office_ID, string V_EntryType, string FromDate, string ToDate, string V_Shift)
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

                FromDate = Convert.ToDateTime(FromDate, cult).ToString("yyyy/MM/dd");
                ToDate = Convert.ToDateTime(ToDate, cult).ToString("yyyy/MM/dd");


                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_Dcs",
                                new string[] { "flag", "I_OfficeID ", "V_EntryType", "FromDate", "ToDate", "V_Shift" },
                                new string[] { "10", Office_ID, V_EntryType, FromDate, ToDate, V_Shift }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void DCSMilkDispatch_Compare_Report_Challan_Wise(string Key, string V_ChallanNo)
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

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_Dcs",
                                new string[] { "flag", "V_ChallanNo" },
                                new string[] { "13", V_ChallanNo }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void DCSMilkReceive_Compare_Report_Challan_Wise(string Key, string V_ChallanNo)
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

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_Dcs",
                                new string[] { "flag", "V_ChallanNo" },
                                new string[] { "14", V_ChallanNo }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void DCSMilkDispatch_DetailsDateShiftOfficeWise(string Key, string Office_ID, string FDate, string V_Shift)
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

                FDate = Convert.ToDateTime(FDate, cult).ToString("yyyy/MM/dd");

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_Dcs",
                     new string[] { "flag", "DT_Date", "V_Shift", "I_OfficeID" },
                     new string[] { "15", FDate,
                         V_Shift, Office_ID }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    #endregion
     
    #region -- BMC Web Services --

    // For BMC QC Validation 

    [WebMethod] // 1. BMC QC VALIDATION FOR ALREADY ENTRY DONE/NOT
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void BMC_RuntimeChallan_In_QC_Exist_Validation(string Key, string BI_MilkInOutRefID, string ChallanNo)
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

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                               new string[] { "flag", "BI_MilkInOutRefID", "V_ChallanNo", "V_EntryType" },
                               new string[] { "21", BI_MilkInOutRefID, ChallanNo, "In" }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    // 2. BMC QC VALIDATION BMC_ChallanRunTime_GrossWeight_Validation

    [WebMethod]  // 3. Check Net Weight Runtime Validation Enter / Not
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void BMC_RuntimeChallan_In_QC_NetWeight_Validation(string Key, string ChallanNo)
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

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                               new string[] { "flag", "V_ChallanNo", },
                               new string[] { "22", ChallanNo }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    // For Validation GENERATE CHALLAN FROM BMC

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void BMC_ReferenceRunTimeCancelStatus(string Key, string BI_MilkInOutRefID)
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

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                               new string[] { "flag", "BI_MilkInOutRefID" },
                               new string[] { "18", BI_MilkInOutRefID }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void BMC_ChallanRunTimeValidation(string Key, string BI_MilkInOutRefID, string Office_ID)
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

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                               new string[] { "flag", "BI_MilkInOutRefID", "OfficeId" },
                               new string[] { "19", BI_MilkInOutRefID, Office_ID }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void BMC_ChallanRunTime_GrossWeight_Validation(string Key, string BI_MilkInOutRefID)
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

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                               new string[] { "flag", "BI_MilkInOutRefID" },
                               new string[] { "20", BI_MilkInOutRefID }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    // DS/CC To BMC/MDP MILK Received --

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void BMC_ReceiveMilk_QC_ReferenceNo(string Key, string I_OfficeID)
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

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                                  new string[] { "flag", "I_OfficeID", "V_EntryType" },
                                  new string[] { "8", I_OfficeID.ToString(), "Out" }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void BMC_ReceiveMilk_QC_ChallanNo_RefIDWise(string Key, string BI_MilkInOutRefID)
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

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                               new string[] { "flag", "BI_MilkInOutRefID", "V_EntryType" },
                               new string[] { "9", BI_MilkInOutRefID.ToString(), "Out" }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void BMC_ReceiveMilk_QC_ChallanNo_Details(string Key, string BI_MilkInOutRefID, string I_EntryID)
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

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                               new string[] { "flag", "BI_MilkInOutRefID", "I_EntryID" },
                               new string[] { "10", BI_MilkInOutRefID.ToString(), I_EntryID.ToString() }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void BMC_ReceiveMilk_QC_Insert(string Key, string BI_MilkInOutRefID, string V_ChallanNo, string I_OfficeID, string I_OfficeTypeID,
     string ReportedUnitID, string ChamberType, string V_VehicleNo, string V_DriverName, string V_DriverMobileNo, string D_MilkQuality,
        string V_Shift, string FATPer, string CLR, string SNFPer, string V_Temp, string SampalNo, string Emp_Id, string ArrivalDate,
        string ArrivalTime
        , string SealLocation, string AdulterationType, string AdulterationValue)
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

                if (BI_MilkInOutRefID == "" || V_ChallanNo == "" || I_OfficeID == "" || I_OfficeTypeID == "" || ReportedUnitID == "" || ChamberType == "" || V_VehicleNo == ""
                || V_DriverName == "" || V_DriverMobileNo == "" || D_MilkQuality == "" || V_Shift == "" || FATPer == "" || CLR == "" || SNFPer == ""
                || Emp_Id == "" || V_Temp == "" || ArrivalDate == "" || ArrivalTime == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                    return;
                }



                DataTable dtAdultration = new DataTable();
                DataRow drAdultration;

                if (AdulterationType.ToString().Split(',').Length != AdulterationValue.ToString().Split(',').Length)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Adulteration Type pass " + AdulterationType.ToString().Split(',').Length + " argument(s) and Adulteration Value pass " + AdulterationValue.ToString().Split(',').Length + " argument(s), Kindly pass equal both arrays arguments and value." }));
                }
                else
                {
                    dtAdultration.Columns.Add("V_SealLocation", typeof(string));
                    dtAdultration.Columns.Add("V_AdulterationType", typeof(string));
                    dtAdultration.Columns.Add("V_AdulterationValue", typeof(string));

                    int len = AdulterationType.ToString().Split(',').Length;

                    for (int i = 0; i < len; i++)
                    {
                        drAdultration = dtAdultration.NewRow();
                        drAdultration[0] = SealLocation;
                        drAdultration[1] = AdulterationType.ToString().Split(',')[i];
                        drAdultration[2] = AdulterationValue.ToString().Split(',')[i];
                        dtAdultration.Rows.Add(drAdultration);
                    }
                }


                DataTable dtMquality = new DataTable();
                DataRow drMquality;

                if (FATPer == "" || SNFPer == "" || CLR == "" || V_Shift == "" || I_OfficeID == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill * MILK QUALITY DETAILS" }));
                    return;
                }

                else
                {

                    dtMquality.Columns.Add(new DataColumn("I_MilkQuantity", typeof(decimal)));
                    dtMquality.Columns.Add(new DataColumn("D_FAT", typeof(decimal)));
                    dtMquality.Columns.Add(new DataColumn("D_SNF", typeof(decimal)));
                    dtMquality.Columns.Add(new DataColumn("D_CLR", typeof(decimal)));
                    dtMquality.Columns.Add(new DataColumn("EntryShift", typeof(string)));
                    dtMquality.Columns.Add(new DataColumn("Office_ID", typeof(string)));

                    drMquality = dtMquality.NewRow();
                    drMquality[0] = Convert.ToDecimal("0.00"); ;
                    drMquality[1] = Convert.ToDecimal(FATPer);
                    drMquality[2] = Convert.ToDecimal(SNFPer);
                    drMquality[3] = Convert.ToDecimal(CLR);
                    drMquality[4] = V_Shift;
                    drMquality[5] = I_OfficeID;
                    dtMquality.Rows.Add(drMquality);

                }


                if (dtMquality.Rows.Count == 0 || dtAdultration.Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Quality Data Can't Empty" }));
                    return;
                }
                else
                {
                    DateTime ADate = Convert.ToDateTime(string.Concat(Convert.ToDateTime(ArrivalDate, cult).ToString("yyyy/MM/dd"), " ", Convert.ToDateTime(ArrivalTime, cult).ToString("hh:mm:ss tt")), cult);


                    ds = null;
                    ds = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                            new string[] { "flag", 
                                                 "V_ChallanNo",
                                                "I_OfficeID",
                                                "I_OfficeTypeID",
                                                "AttachedToCC",
                                                "V_MilkDispatchType",
                                                "I_TotalCans",
                                                "V_VehicleNo",
                                                "V_DriverName",
                                                "V_DriverMobileNo",
                                                "V_EntryType",
                                                "D_MilkQuality",
                                                "V_Shift",
                                                "D_MilkQuantity",
                                                "FAT",
                                                "CLR",
                                                "SNF",  
                                                "DT_ArrivalDateTime",
                                                "I_CreatedByEmpID",
                                                "V_Remark",
                                                "V_EntryFrom",
                                                "V_Temp",
                                                "MilkSaleQty",
                                                "MilkSaleRatePerLtr",
                                                "MilkSaleAmount",
                                                "NetMilkQtyInKG",
                                                "NetFATInKG",
                                                "NetSNFInKG",
                                                "BI_MilkInOutRefID",
                                                "SampalNo",
                                                "ScaleReading" 
                                                 },

                                                new string[] { "15",
  
                                                V_ChallanNo,
                                                I_OfficeID,
                                                I_OfficeTypeID, 
                                                ReportedUnitID,
                                                ChamberType,
                                                "0",
                                                V_VehicleNo,
                                                V_DriverName,
                                                V_DriverMobileNo,
                                                "In",
                                                D_MilkQuality,
                                                V_Shift,
                                                "0.00",
                                                FATPer,
                                                CLR,
                                                SNFPer,
                                                ADate.ToString(),
                                                Emp_Id,
                                                "",
                                                "APP",
                                                V_Temp,
                                                "0.00",
                                                "0.00",
                                                "0.00",
                                                "0.00",
                                                "0.00",
                                                "0.00",
                                                BI_MilkInOutRefID,
                                                SampalNo,
                                                "0.00" 
                                                },
                                             new string[] { "type_Trn_tblAdulterationTest_MCU", "type_Trn_MilkQualityDetails_MCU" },
                                             new DataTable[] { dtAdultration, dtMquality }, "TableSave");


                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Login Credentials." }));
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
                        this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

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
    public void BMC_ReceiveMilk_QC_DetailDateWise(string Key, string Office_ID, string FilterDate)
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
                string date = "";

                if (FilterDate != "")
                {
                    date = Convert.ToDateTime(FilterDate, cult).ToString("yyyy-MM-dd");
                }
                else
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Date" }));
                }


                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                     new string[] { "flag", "I_OfficeID", "V_EntryType", "Filter_Date" },
                     new string[] { "14", Office_ID, "In", date }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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



    // BMC/MDP MILK DISPATCH

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void BMC_MilkDispatch_ReferenceNo(string Key, int I_OfficeID)
    {
        DataSet ds = new DataSet();
        DataTable dt1 = new DataTable();
        APIProcedure apiprocedure = new APIProcedure();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string saltkey = apiprocedure.GenerateSaltKey();
                DataSet ds1 = new DataSet();

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                               new string[] { "flag", "I_OfficeID" },
                               new string[] { "6", I_OfficeID.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    DataRow dr;
                    dt1.Columns.Add(new DataColumn("C_ReferenceNo", typeof(string)));
                    dt1.Columns.Add(new DataColumn("BI_MilkInOutRefID", typeof(string)));

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        if (ds1.Tables[0].Rows[i]["I_EntryID"].ToString() == "" && ds1.Tables[0].Rows[i]["I_OfficeID"].ToString() == I_OfficeID.ToString())
                        {
                            dr = dt1.NewRow();
                            dr[0] = ds1.Tables[0].Rows[i]["C_ReferenceNo"].ToString();
                            dr[1] = ds1.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString();
                            dt1.Rows.Add(dr);
                        }
                    }
                }



                if (dt1.Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {


                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt1.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt1.Columns)
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
            dt1.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void BMC_MilkDispatch_ReferenceNo_Details(string Key, string BI_MilkInOutRefID)
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

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                                new string[] { "flag", "BI_MilkInOutRefID" },
                                new string[] { "5", BI_MilkInOutRefID }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void BMC_MilkDispatch_SelfMilkCollection(string Key, string Office_ID, string FilterDate)
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
                string date = "";

                if (FilterDate != "")
                {
                    date = Convert.ToDateTime(FilterDate, cult).ToString("yyyy-MM-dd");
                }
                else
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Date" }));
                }

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                         new string[] { "flag", "OfficeId", "FDate" },
                         new string[] { "7", Office_ID, date }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void BMC_MilkDispatch_DCSReceivedMilk(string Key, string Office_ID, string FilterDate)
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
                string date = "";

                if (FilterDate != "")
                {
                    date = Convert.ToDateTime(FilterDate, cult).ToString("yyyy-MM-dd");
                }
                else
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Date" }));
                }

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                         new string[] { "flag", "OfficeId", "FDate" },
                         new string[] { "8", Office_ID, date }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    // public void DCSMilkDispatch_SaleDetail(string Key, string Office_ID, string FilterDate) For Local Sale.

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void BMC_MilkDispatch_AllMilkCollectionWith_MilkQuality(string Key, string Office_ID, string FilterDate)
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
                string date = "";

                if (FilterDate != "")
                {
                    date = Convert.ToDateTime(FilterDate, cult).ToString("yyyy-MM-dd");
                }
                else
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Date" }));
                }

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                         new string[] { "flag", "OfficeId", "FDate" },
                         new string[] { "9", Office_ID, date }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    // [WebMethod]
    // [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    // public void BMC_MilkDispatch_InsertData(string Key, string ReferenceNo, string I_OfficeID, string I_OfficeTypeID, string ReportingUnitID,
                // string V_MilkDispatchType, string V_VehicleNo, string V_DriverName,
                // string V_DriverMobileNo, string D_MilkQuality, string V_Shift,
                // string D_MilkQuantityInLtr, string FATPer, string CLR, string SNFPer, string Emp_Id,
                // string V_Temp, string MilkSaleQty, string MilkSaleRatePerLtr, string MilkSaleAmount,
                // string NetMilkQtyInKG, string NetFATInKG, string NetSNFInKG, string SampalNo, string ScaleReading, string Sealtype, string ChamberType,
                // string TI_SealColorID, string V_SealNo, string V_SealRemark)
    // {
        // DataSet ds = new DataSet();
        // DataTable dt = new DataTable();
        // APIProcedure apiprocedure = new APIProcedure();
        // System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        // this.Context.Response.ContentType = "application/json; charset=utf-8";
        // if (Key == securityKey)
        // {
            // try
            // {
                // string saltkey = apiprocedure.GenerateSaltKey();
                // DataSet ds1 = new DataSet();

                // DataTable dtsealF = new DataTable();
                // DataRow dr;
                // DataTable dtMquality = new DataTable();

                // if (ReferenceNo == "" || I_OfficeID == "" || I_OfficeTypeID == "" || ReportingUnitID == "" || V_MilkDispatchType == "" || V_VehicleNo == ""
                // || V_DriverName == "" || V_DriverMobileNo == "" || D_MilkQuality == "" || V_Shift == "" || D_MilkQuantityInLtr == ""
                // || FATPer == "" || CLR == "" || SNFPer == "" || Emp_Id == "" || V_Temp == "" || NetMilkQtyInKG == "" || NetFATInKG == "" || NetSNFInKG == ""
                    // || SampalNo == "" || ScaleReading == "")
                // {
                    // this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                    // return;
                // }

                // DataSet DSMQD = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                          // new string[] { "flag", "OfficeId", "FDate" },
                          // new string[] { "9", I_OfficeID, Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"), cult).ToString("yyyy-MM-dd") }, "dataset");

                // if (DSMQD.Tables[0].Rows.Count == 0)
                // {
                    // this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Quality Data Can't Empty" }));
                    // return;
                // }
                // else
                // {
                    // dtMquality = DSMQD.Tables[0];

                // }

                // if (V_MilkDispatchType == "Tanker")
                // {

                    // if (Sealtype == "" || ChamberType == "" || TI_SealColorID == "" || V_SealNo == "")
                    // {
                        // this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill Sealtype,ChamberType,TI_SealColorID,V_SealNo For Milk Dispatch Type - Tanker" }));
                        // return;
                    // }

                    // else
                    // {
                        // dtsealF.Columns.Add(new DataColumn("Sealtype", typeof(string)));
                        // dtsealF.Columns.Add(new DataColumn("ChamberType", typeof(string)));
                        // dtsealF.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));
                        // dtsealF.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
                        // dtsealF.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));

                        // int len = Sealtype.ToString().Split(',').Length;

                        // for (int i = 0; i < len; i++)
                        // {
                            // dr = dtsealF.NewRow();
                            // dr[0] = Sealtype.ToString().Split(',')[i];
                            // dr[1] = ChamberType.ToString().Split(',')[i];
                            // dr[2] = TI_SealColorID.ToString().Split(',')[i];
                            // dr[3] = V_SealNo.ToString().Split(',')[i];
                            // dr[4] = V_SealRemark.ToString().Split(',')[i];
                            // dtsealF.Rows.Add(dr);
                        // }

                    // }
                // }

                // DataSet dsValidationRuntime = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                     // new string[] { "flag", "DT_Date", "I_OfficeID", "BI_MilkInOutRefID" },
                     // new string[] { "3", Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"), cult).ToString("yyyy-MM-dd"), I_OfficeID, ReferenceNo }, "dataset");

                // if (dsValidationRuntime.Tables[0].Rows.Count > 0)
                // {
                    // this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Only 1 time Dispatch Alow For Every Day" }));
                    // return;
                // }


                // else
                // {
                    // if (MilkSaleQty == "")
                    // {
                        // MilkSaleQty = "0.00";
                    // }
                    // if (MilkSaleRatePerLtr == "")
                    // {
                        // MilkSaleRatePerLtr = "0.00";
                    // }

                    // if (MilkSaleAmount == "")
                    // {
                        // MilkSaleAmount = "0.00";
                    // }

                    // if (NetFATInKG == "")
                    // {
                        // NetFATInKG = "0.00";
                    // }

                    // if (NetSNFInKG == "")
                    // {
                        // NetSNFInKG = "0.00";
                    // }


                    // ds1 = null;
                    // ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                            // new string[] { "flag", 
                                                // "I_OfficeID",
                                                // "I_OfficeTypeID",
                                                // "AttachedToCC",
                                                // "V_MilkDispatchType",
                                                // "I_TotalCans",
                                                // "V_VehicleNo",
                                                // "V_DriverName",
                                                // "V_DriverMobileNo",
                                                // "V_EntryType",
                                                // "D_MilkQuality",
                                                // "V_Shift",
                                                // "D_MilkQuantity",
                                                // "FAT",
                                                // "CLR",
                                                // "SNF", 
                                                // "I_CreatedByEmpID",
                                                // "V_Remark",
                                                // "V_EntryFrom",
                                                // "V_Temp",
                                                // "MilkSaleQty",
                                                // "MilkSaleRatePerLtr",
                                                // "MilkSaleAmount",
                                                // "NetMilkQtyInKG",
                                                // "NetFATInKG",
                                                // "NetSNFInKG",
                                                // "BI_MilkInOutRefID",
                                                // "SampalNo",
                                                // "ScaleReading"
                                                 // },

                                                // new string[] { "2",  
                                                // I_OfficeID,
                                                // I_OfficeTypeID, 
                                                // ReportingUnitID,
                                                // V_MilkDispatchType,
                                                // "0",
                                                // V_VehicleNo,
                                                // V_DriverName,
                                                // V_DriverMobileNo,
                                                // "Out",
                                                // D_MilkQuality,
                                                // V_Shift,
                                                // D_MilkQuantityInLtr,
                                                // FATPer,
                                                // CLR,
                                                // SNFPer, 
                                                // Emp_Id,
                                                // "",
                                                // "APP",
                                                // V_Temp,
                                                // MilkSaleQty,
                                                // MilkSaleRatePerLtr,
                                                // MilkSaleAmount,
                                                // NetMilkQtyInKG,
                                                // NetFATInKG,
                                                // NetSNFInKG,
                                                // ReferenceNo,
                                                // SampalNo,
                                                // ScaleReading
                                                // },
                                             // new string[] { "type_Trn_TankerSealDetails_MCU", "type_Trn_MilkQualityDetails_MCU" },
                                             // new DataTable[] { dtsealF, dtMquality }, "TableSave");



                    // if (ds1.Tables[0].Rows.Count == 0)
                    // {
                        // this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Login Credentials." }));
                    // }
                    // else
                    // {

                        // dt = ds1.Tables[0];
                        // List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                        // Dictionary<string, object> row = null;
                        // foreach (DataRow rs in dt.Rows)
                        // {
                            // row = new Dictionary<string, object>();
                            // foreach (DataColumn col in dt.Columns)
                            // {
                                // row.Add(col.ColumnName, rs[col]);
                            // }
                            // rows.Add(row);
                        // }
                        // this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

                    // }
                // }

            // }
            // catch (Exception ex)
            // {
                // this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            // }
            // dt.Clear();
            // ds.Clear();
        // }
        // else
        // {
            // this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        // }
    // }
 [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void BMC_MilkDispatch_InsertData(string Key, string ReferenceNo, string I_OfficeID, string I_OfficeTypeID, string ReportingUnitID,
                string V_MilkDispatchType, string V_VehicleNo, string V_DriverName,
                string V_DriverMobileNo, string D_MilkQuality, string V_Shift,
                string D_MilkQuantityInLtr, string FATPer, string CLR, string SNFPer, string Emp_Id,
                string V_Temp, string MilkSaleQty, string MilkSaleRatePerLtr, string MilkSaleAmount,
                string NetMilkQtyInKG, string NetFATInKG, string NetSNFInKG, string SampalNo, string ScaleReading, string Sealtype, string ChamberType,
                string TI_SealColorID, string V_SealNo, string V_SealRemark,
        string DT_ArrivalDateTime, string DT_Date, string DT_DispatchDateTime,
        string AdulterationTestStatus, string SealLocation, string AdulterationType, string AdulterationValue)
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


                DataTable dtAdultration = new DataTable();
                DataRow drAdultration;

                if (AdulterationTestStatus == "Yes")
                {
                    if (AdulterationType.ToString().Split(',').Length != AdulterationValue.ToString().Split(',').Length)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Adulteration Type pass " + AdulterationType.ToString().Split(',').Length + " argument(s) and Adulteration Value pass " + AdulterationValue.ToString().Split(',').Length + " argument(s), Kindly pass equal both arrays arguments and value." }));
                    }
                    else
                    {
                        dtAdultration.Columns.Add("V_SealLocation", typeof(string));
                        dtAdultration.Columns.Add("V_AdulterationType", typeof(string));
                        dtAdultration.Columns.Add("V_AdulterationValue", typeof(string));

                        int len = AdulterationType.ToString().Split(',').Length;

                        for (int i = 0; i < len; i++)
                        {
                            drAdultration = dtAdultration.NewRow();
                            drAdultration[0] = SealLocation;
                            drAdultration[1] = AdulterationType.ToString().Split(',')[i];
                            drAdultration[2] = AdulterationValue.ToString().Split(',')[i];
                            dtAdultration.Rows.Add(drAdultration);
                        }
                    }
                }
                else
                {
                    dtAdultration.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
                    dtAdultration.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));
                    dtAdultration.Columns.Add(new DataColumn("V_AdulterationValue", typeof(string)));
                }




                DataTable dtsealF = new DataTable();
                DataRow dr;
                DataTable dtMquality = new DataTable();

                if (ReferenceNo == "" || I_OfficeID == "" || I_OfficeTypeID == "" || ReportingUnitID == "" || V_MilkDispatchType == "" || V_VehicleNo == ""
                || V_DriverName == "" || V_DriverMobileNo == "" || D_MilkQuality == "" || V_Shift == "" || D_MilkQuantityInLtr == ""
                || FATPer == "" || CLR == "" || SNFPer == "" || Emp_Id == "" || V_Temp == "" || NetMilkQtyInKG == "" || NetFATInKG == "" || NetSNFInKG == ""
                    || SampalNo == "" || ScaleReading == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                    return;
                }

                DataSet DSMQD = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                          new string[] { "flag", "OfficeId", "FDate" },
                          new string[] { "9", I_OfficeID, Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"), cult).ToString("yyyy-MM-dd") }, "dataset");

                if (DSMQD.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Quality Data Can't Empty" }));
                    return;
                }
                else
                {
                    dtMquality = DSMQD.Tables[0];

                }

                if (V_MilkDispatchType == "Tanker")
                {

                    if (Sealtype == "" || ChamberType == "" || TI_SealColorID == "" || V_SealNo == "")
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill Sealtype,ChamberType,TI_SealColorID,V_SealNo For Milk Dispatch Type - Tanker" }));
                        return;
                    }

                    else
                    {
                        dtsealF.Columns.Add(new DataColumn("Sealtype", typeof(string)));
                        dtsealF.Columns.Add(new DataColumn("ChamberType", typeof(string)));
                        dtsealF.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));
                        dtsealF.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
                        dtsealF.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));

                        int len = Sealtype.ToString().Split(',').Length;

                        for (int i = 0; i < len; i++)
                        {
                            dr = dtsealF.NewRow();
                            dr[0] = Sealtype.ToString().Split(',')[i];
                            dr[1] = ChamberType.ToString().Split(',')[i];
                            dr[2] = TI_SealColorID.ToString().Split(',')[i];
                            dr[3] = V_SealNo.ToString().Split(',')[i];
                            dr[4] = V_SealRemark.ToString().Split(',')[i];
                            dtsealF.Rows.Add(dr);
                        }

                    }
                }

                DataSet dsValidationRuntime = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                     new string[] { "flag", "DT_Date", "I_OfficeID", "BI_MilkInOutRefID" },
                     new string[] { "3", Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"), cult).ToString("yyyy-MM-dd"), I_OfficeID, ReferenceNo }, "dataset");

                if (dsValidationRuntime.Tables[0].Rows.Count > 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Only 1 time Dispatch Alow For Every Day" }));
                    return;
                }


                else
                {
                    if (MilkSaleQty == "")
                    {
                        MilkSaleQty = "0.00";
                    }
                    if (MilkSaleRatePerLtr == "")
                    {
                        MilkSaleRatePerLtr = "0.00";
                    }

                    if (MilkSaleAmount == "")
                    {
                        MilkSaleAmount = "0.00";
                    }

                    if (NetFATInKG == "")
                    {
                        NetFATInKG = "0.00";
                    }

                    if (NetSNFInKG == "")
                    {
                        NetSNFInKG = "0.00";
                    }


                    ds1 = null;
                    ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                            new string[] { "flag", 
                                                "I_OfficeID",
                                                "I_OfficeTypeID",
                                                "AttachedToCC",
                                                "V_MilkDispatchType",
                                                "I_TotalCans",
                                                "V_VehicleNo",
                                                "V_DriverName",
                                                "V_DriverMobileNo",
                                                "V_EntryType",
                                                "D_MilkQuality",												
                                                "V_Shift",
                                                "D_MilkQuantity",
                                                "FAT",
                                                "CLR",
                                                "SNF", 
                                                "I_CreatedByEmpID",
                                                "V_Remark",
                                                "V_EntryFrom",
                                                "V_Temp",
                                                "MilkSaleQty",
                                                "MilkSaleRatePerLtr",
                                                "MilkSaleAmount",
                                                "NetMilkQtyInKG",
                                                "NetFATInKG",
                                                "NetSNFInKG",
                                                "BI_MilkInOutRefID",
                                                "SampalNo",
                                                "ScaleReading",
                                                "DT_ArrivalDateTime",
												"DT_Date",
                                                "DT_DispatchDateTime",
                                                "AdulterationTestStatus"
                                                 },

                                                new string[] { "2",  
                                                I_OfficeID,
                                                I_OfficeTypeID, 
                                                ReportingUnitID,
                                                V_MilkDispatchType,
                                                "0",
                                                V_VehicleNo,
                                                V_DriverName,
                                                V_DriverMobileNo,
                                                "Out",
                                                D_MilkQuality,											
                                                V_Shift,
                                                D_MilkQuantityInLtr,
                                                FATPer,
                                                CLR,
                                                SNFPer, 
                                                Emp_Id,
                                                "",
                                                "APP",
                                                V_Temp,
                                                MilkSaleQty,
                                                MilkSaleRatePerLtr,
                                                MilkSaleAmount,
                                                NetMilkQtyInKG,
                                                NetFATInKG,
                                                NetSNFInKG,
                                                ReferenceNo,
                                                SampalNo,
                                                ScaleReading,
                                                Convert.ToDateTime(DT_ArrivalDateTime,cult).ToString("yyyy/MM/dd"),
												Convert.ToDateTime(DT_Date,cult).ToString("yyyy/MM/dd"),
                                                Convert.ToDateTime(DT_DispatchDateTime,cult).ToString("yyyy/MM/dd"),
                                                AdulterationTestStatus 
                                                },
                                             new string[] { "type_Trn_TankerSealDetails_MCU", "type_Trn_MilkQualityDetails_MCU", "type_Trn_tblAdulterationTest_MCU" },
                                             new DataTable[] { dtsealF, dtMquality, dtAdultration }, "TableSave");



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
    public void BMC_MilkDispatch_DetailDateWise(string Key, string Office_ID, string FilterDate)
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
                string date = "";

                if (FilterDate != "")
                {
                    date = Convert.ToDateTime(FilterDate, cult).ToString("yyyy-MM-dd");
                }
                else
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Date" }));
                }

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                     new string[] { "flag", "FDate", "OfficeId" },
                     new string[] { "4", Convert.ToDateTime(FilterDate, cult).ToString("yyyy-MM-dd"), Office_ID }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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


    // BMC/MDP MILK DISPATCH Rpt API

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void BMC_MilkDispatch_DetailUsingFilter(string Key, string Office_ID, string FromDate, string ToDate, string V_Shift, string V_EntryType)
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

                FromDate = Convert.ToDateTime(FromDate, cult).ToString("yyyy-MM-dd");
                ToDate = Convert.ToDateTime(ToDate, cult).ToString("yyyy-MM-dd");


                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                     new string[] { "flag", "FromDate", "ToDate", "I_OfficeID", "V_Shift", "V_EntryType" },
                     new string[] { "10", FromDate, ToDate, Office_ID, V_Shift, V_EntryType }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void BMC_MilkDispatch_ComparisonRpt_DBMCMDP(string Key, string V_ChallanNo)
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

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                       new string[] { "flag", "V_ChallanNo" },
                       new string[] { "11", V_ChallanNo }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void BMC_MilkDispatch_ComparisonRpt_DNet(string Key, string V_ChallanNo)
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

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                       new string[] { "flag", "V_ChallanNo" },
                       new string[] { "12", V_ChallanNo }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void BMC_MilkDispatch_ComparisonRpt_DSCCReceived(string Key, string V_ChallanNo)
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

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                       new string[] { "flag", "V_ChallanNo" },
                       new string[] { "13", V_ChallanNo }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
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



    #endregion
	
	// For QC APP Validation 

    // 1. MCMS_RuntimeChallan_In_QC_Validation  

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MCMS_RuntimeChallan_In_QC_Exist_Validation(string Key, string ChallanNo)
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

                ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                               new string[] { "flag", "V_ReferenceCode", },
                               new string[] { "31", ChallanNo }, "dataset");

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

    // 2. MCMS_RuntimeChallan_In_QC_Grossweight_Validation 
    // Note - User Priv API For QC Gross Weight Validation Like - MCMS_ChallanRunTime_GrossWeight_Validation

    // 3. Check Net Weight Runtime Validation Enter / Not

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MCMS_RuntimeChallan_In_QC_NetWeight_Validation(string Key, string ChallanNo)
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

                ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                               new string[] { "flag", "V_ReferenceCode", },
                               new string[] { "32", ChallanNo }, "dataset");

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

    // 4. Check Chamber Location In QC

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MCMS_RuntimeChallan_In_QC_ChamberLocation_Validation(string Key, string ChallanNo, string ChamberLocation)
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

                ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                               new string[] { "flag", "V_ReferenceCode", "V_SealLocation" },
                               new string[] { "33", ChallanNo, ChamberLocation }, "dataset");

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


    // For CC APP Validation 

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


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MCMS_ChallanRunTime_ChamberLocationValidation(string Key, string BI_MilkInOutRefID, string ChamberLocation)
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

                ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                               new string[] { "flag", "BI_MilkInOutRefID", "V_SealLocation" },
                               new string[] { "30", BI_MilkInOutRefID, ChamberLocation }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Data Not Found." }));
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


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MCMS_ChallanRunTime_GrossWeight_Validation(string Key, string BI_MilkInOutRefID)
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

                ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                               new string[] { "flag", "BI_MilkInOutRefID" },
                               new string[] { "29", BI_MilkInOutRefID }, "dataset");

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


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MCMS_ChallanRunTimeValidation(string Key, string BI_MilkInOutRefID, string Office_ID)
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

                ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                               new string[] { "flag", "BI_MilkInOutRefID", "Office_ID" },
                               new string[] { "28", BI_MilkInOutRefID, Office_ID }, "dataset");

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


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MCMS_ReferenceRunTimeCancelStatus(string Key, string BI_MilkInOutRefID)
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

                ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                               new string[] { "flag", "BI_MilkInOutRefID" },
                               new string[] { "27", BI_MilkInOutRefID }, "dataset");

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


    // MCMS WEB SERVICES START

     [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MCMS_AduletrationTestlist_DS(string Key)
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

                ds1 = apiprocedure.ByProcedure("USP_Mst_AdulterationTestList",
                                new string[] { "flag" },
                                new string[] { "1" }, "dataset");

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



[WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MCMS_AduletrationTestlist_CC(string Key)
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

                ds1 = apiprocedure.ByProcedure("USP_Mst_AdulterationTestList",
                                new string[] { "flag" },
                                new string[] { "3" }, "dataset");

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


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MCMS_MilkQualityList(string Key)
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

                ds1 = apiprocedure.ByProcedure("USP_Mst_MilkQualityList",
                                new string[] { "flag" },
                                new string[] { "1" }, "dataset");

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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MCMS_MilkTypeList(string Key)
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

                ds1 = apiprocedure.ByProcedure("USP_Mst_MilkTypeList",
                                new string[] { "flag" },
                                new string[] { "1" }, "dataset");

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


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MCMS_ReferenceNoListForCC(string Key, int I_OfficeID)
    {
        DataSet ds = new DataSet();
        DataTable dt1 = new DataTable();
        APIProcedure apiprocedure = new APIProcedure();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string saltkey = apiprocedure.GenerateSaltKey();
                DataSet ds1 = new DataSet();

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                               new string[] { "flag", "I_OfficeID" },
                               new string[] { "6", I_OfficeID.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    DataRow dr;
                    dt1.Columns.Add(new DataColumn("C_ReferenceNo", typeof(string)));
                    dt1.Columns.Add(new DataColumn("BI_MilkInOutRefID", typeof(string)));

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        if (ds1.Tables[0].Rows[i]["I_EntryID"].ToString() == "" && ds1.Tables[0].Rows[i]["I_OfficeID"].ToString() == I_OfficeID.ToString())
                        {
                            dr = dt1.NewRow();
                            dr[0] = ds1.Tables[0].Rows[i]["C_ReferenceNo"].ToString();
                            dr[1] = ds1.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString();
                            dt1.Rows.Add(dr);
                        }
                    }
                }



                if (dt1.Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {


                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt1.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt1.Columns)
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
            dt1.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MCMS_ReferenceNoListForDC(string Key, int I_OfficeID)
    {
        DataSet ds = new DataSet();
        DataTable dt1 = new DataTable();
        APIProcedure apiprocedure = new APIProcedure();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string saltkey = apiprocedure.GenerateSaltKey();

                DataSet ds1 = new DataSet();

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                               new string[] { "flag", "I_OfficeID", "V_EntryType" },
                               new string[] { "24", I_OfficeID.ToString(), "Out" }, "dataset");


                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {
                    dt1 = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt1.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt1.Columns)
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
            dt1.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MCMS_ChallanListByReferenceIDInDS(string Key, int ReferenceID)
    {
        DataSet ds = new DataSet();
        DataTable dt1 = new DataTable();
        APIProcedure apiprocedure = new APIProcedure();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string saltkey = apiprocedure.GenerateSaltKey();

                DataSet ds1 = new DataSet();

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                           new string[] { "flag", "BI_MilkInOutRefID", "V_EntryType" },
                           new string[] { "9", ReferenceID.ToString(), "Out" }, "dataset");


                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {
                    dt1 = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt1.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt1.Columns)
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
            dt1.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MCMS_DetailsForChallanNumberDS(string Key, int ReferenceID, int ChallanID)
    {
        DataSet ds = new DataSet();
        DataTable dt1 = new DataTable();
        APIProcedure apiprocedure = new APIProcedure();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string saltkey = apiprocedure.GenerateSaltKey();

                DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                               new string[] { "flag", "BI_MilkInOutRefID", "I_EntryID" },
                               new string[] { "17", ReferenceID.ToString(), ChallanID.ToString() }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {
                    dt1 = ds1.Tables[0];
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt1.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt1.Columns)
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
            dt1.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }


    // MCMS WEB SERVICES END



    // DCS_Farmer APIs START


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void DCSMC_FarmerList(string Key, int DCSId)
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

                ds1 = apiprocedure.ByProcedure("SpProducerMaster",
                               new string[] { "flag", "DCSId" },
                               new string[] { "13", DCSId.ToString() }, "dataset");

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

    // DCS_Farmer APIs END

    #region -- New MPCDF CC/DS Web Services --

     [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void Login(string Key, string UserName, string Password)
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

                switch (UserName.Substring(0, 1))
                {
                    case "E": //for Employees Login offices like HO, DS, DCS, CC, BMC, MDP 
                        ds1 = apiprocedure.ByProcedure("sp_Login",
                                 new string[] { "flag", "UserName" },
                                 new string[] { "0", UserName }, "dataset");
                        break;
                    case "P": //for Producer Login
                        ds1 = apiprocedure.ByProcedure("SptblPUProducerReg",
                                new string[] { "flag", "UserName" },
                                new string[] { "10", UserName }, "dataset");
                        break;
                    case "D": //for Distributor Login
                        ds1 = apiprocedure.ByProcedure("USP_Mst_DistributorReg",
                                new string[] { "flag", "UserName" },
                                new string[] { "6", UserName }, "dataset");
                        break;
                    case "S": //for Sub-Distributor Login
                        ds1 = apiprocedure.ByProcedure("USP_Mst_SubDistributorReg",
                                new string[] { "flag", "UserName", },
                                new string[] { "7", UserName }, "dataset");
                        break;
                    case "B": //for Booth/Parlour Login
                        ds1 = apiprocedure.ByProcedure("USP_Mst_BoothReg",
                                new string[] { "flag", "UserName" },
                                new string[] { "10", UserName }, "dataset");
                        break;
                    case "M": //for Temp Employees Login of offices like HO, DS, DCS, CC, BMC, MDP 
                        ds1 = apiprocedure.ByProcedure("usp_Mst_McuUserMaster",
                                new string[] { "flag", "UserName" },
                                new string[] { "6", UserName }, "dataset");
                        break;
                    default:
                        if (ds1 != null)
                        {
                            ds1.Clear();
                        }
                        break;
                }
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
                    if (apiprocedure.CompaireHashCode(ds1.Tables[0].Rows[0]["Password"].ToString(), Password, saltkey))
                    {
                        if (ds1.Tables[0].Rows[0]["IsActive"].ToString() == "True" || ds1.Tables[0].Rows[0]["IsActive"].ToString() == "1")
                        {
                            //Re-Generated Salt Key
                            saltkey = apiprocedure.GenerateSaltKey();

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
                switch (UserName.Substring(0, 1))
                {
                    case "E": //for Employees Login offices like HO, DS, DCS, CC, BMC, MDP 
                        ds = apiprocedure.ByProcedure("sp_Login",
                                 new string[] { "flag", "UserName" },
                                 new string[] { "0", UserName }, "dataset");
                        break;
                    case "P": //for Producer Login
                        ds = apiprocedure.ByProcedure("SptblPUProducerReg",
                                new string[] { "flag", "UserName" },
                                new string[] { "0", UserName }, "dataset");
                        break;
                    case "D": //for Distributor Login
                        ds = apiprocedure.ByProcedure("USP_Mst_DistributorReg",
                                new string[] { "flag", "UserName" },
                                new string[] { "0", UserName }, "dataset");
                        break;
                    case "S": //for Sub-Distributor Login
                        ds = apiprocedure.ByProcedure("USP_Mst_SubDistributorReg",
                                new string[] { "flag", "UserName", },
                                new string[] { "0", UserName }, "dataset");
                        break;
                    case "B": //for Booth/Parlour Login
                        ds = apiprocedure.ByProcedure("USP_Mst_BoothReg",
                                new string[] { "flag", "UserName" },
                                new string[] { "0", UserName }, "dataset");
                        break;
                    case "M": //for Temp Employees Login of offices like HO, DS, DCS, CC, BMC, MDP 
                        ds = apiprocedure.ByProcedure("usp_Mst_McuUserMaster",
                                new string[] { "flag", "UserName" },
                                new string[] { "5", UserName }, "dataset");
                        break;
                    default:
                        if (ds != null)
                        {
                            ds.Clear();
                        }
                        break;
                }

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

                switch (UserName.Substring(0, 1))
                {
                    case "E": //for Employees Login offices like HO, DS, DCS, CC, BMC, MDP 
                        ds1 = apiprocedure.ByProcedure("sp_Login",
                                 new string[] { "flag", "UserName" },
                                 new string[] { "0", UserName }, "dataset");
                        break;
                    case "P": //for Producer Login
                        ds1 = apiprocedure.ByProcedure("SptblPUProducerReg",
                                new string[] { "flag", "UserName" },
                                new string[] { "0", UserName }, "dataset");
                        break;
                    case "D": //for Distributor Login
                        ds1 = apiprocedure.ByProcedure("USP_Mst_DistributorReg",
                                new string[] { "flag", "UserName" },
                                new string[] { "0", UserName }, "dataset");
                        break;
                    case "S": //for Sub-Distributor Login
                        ds1 = apiprocedure.ByProcedure("USP_Mst_SubDistributorReg",
                                new string[] { "flag", "UserName", },
                                new string[] { "0", UserName }, "dataset");
                        break;
                    case "B": //for Booth/Parlour Login
                        ds1 = apiprocedure.ByProcedure("USP_Mst_BoothReg",
                                new string[] { "flag", "UserName" },
                                new string[] { "0", UserName }, "dataset");
                        break;
                    case "M": //for Temp Employees Login of offices like HO, DS, DCS, CC, BMC, MDP 
                        ds1 = apiprocedure.ByProcedure("usp_Mst_McuUserMaster",
                                new string[] { "flag", "UserName" },
                                new string[] { "5", UserName }, "dataset");
                        break;
                    default:
                        if (ds1 != null)
                        {
                            ds1.Clear();
                        }
                        break;
                }

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "1", Error = "Invalid Credentials." }));
                }
                else
                {
                    //Update New Password by UserName

                    switch (UserName.Substring(0, 1))
                    {
                        case "E": //for Employees Login offices like HO, DS, DCS, CC, BMC, MDP 
                            ds = apiprocedure.ByProcedure("sp_Login",
                                     new string[] { "flag", "UserName", "Password" },
                                     new string[] { "3", UserName, apiprocedure.SHA512_HASH(Password) }, "dataset");
                            break;
                        case "P": //for Producer Login
                            ds = apiprocedure.ByProcedure("SptblPUProducerReg",
                                    new string[] { "flag", "UserName", "Password" },
                                    new string[] { "9", UserName, apiprocedure.SHA512_HASH(Password) }, "dataset");
                            break;
                        case "D": //for Distributor Login
                            ds = apiprocedure.ByProcedure("USP_Mst_DistributorReg",
                                    new string[] { "flag", "UserName", "Password" },
                                    new string[] { "5", UserName, apiprocedure.SHA512_HASH(Password) }, "dataset");
                            break;
                        case "S": //for Sub-Distributor Login
                            ds = apiprocedure.ByProcedure("USP_Mst_SubDistributorReg",
                                    new string[] { "flag", "UserName", "Password" },
                                    new string[] { "6", UserName, apiprocedure.SHA512_HASH(Password) }, "dataset");
                            break;
                        case "B": //for Booth/Parlour Login
                            ds = apiprocedure.ByProcedure("USP_Mst_BoothReg",
                                    new string[] { "flag", "UserName", "Password" },
                                    new string[] { "5", UserName, apiprocedure.SHA512_HASH(Password) }, "dataset");
                            break;
                        case "M": //for Temp Employees Login offices like HO, DS, DCS, CC, BMC, MDP 
                            ds = apiprocedure.ByProcedure("usp_Mst_McuUserMaster",
                                    new string[] { "flag", "UserName", "Password" },
                                    new string[] { "4", UserName, apiprocedure.SHA512_HASH(Password) }, "dataset");
                            break;
                        default:
                            if (ds != null)
                            {
                                ds.Clear();
                            }
                            break;
                    }

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

                switch (UserName.Substring(0, 1))
                {
                    case "E": //for Employees Login offices like HO, DS, DCS, CC, BMC, MDP 
                        ds1 = apiprocedure.ByProcedure("sp_Login",
                                 new string[] { "flag", "UserName" },
                                 new string[] { "0", UserName.ToString() }, "dataset");
                        break;
                    case "P": //for Producer Login
                        ds1 = apiprocedure.ByProcedure("SptblPUProducerReg",
                                new string[] { "flag", "UserName" },
                                new string[] { "0", UserName.ToString() }, "dataset");
                        break;
                    case "D": //for Distributor Login
                        ds1 = apiprocedure.ByProcedure("USP_Mst_DistributorReg",
                                new string[] { "flag", "UserName" },
                                new string[] { "0", UserName.ToString() }, "dataset");
                        break;
                    case "S": //for Sub-Distributor Login
                        ds1 = apiprocedure.ByProcedure("USP_Mst_SubDistributorReg",
                                new string[] { "flag", "UserName", },
                                new string[] { "0", UserName.ToString() }, "dataset");
                        break;
                    case "B": //for Booth/Parlour Login
                        ds1 = apiprocedure.ByProcedure("USP_Mst_BoothReg",
                                new string[] { "flag", "UserName" },
                                new string[] { "0", UserName.ToString() }, "dataset");
                        break;
                    case "M": //for Temp Employees Login of offices like HO, DS, DCS, CC, BMC, MDP 
                        ds1 = apiprocedure.ByProcedure("usp_Mst_McuUserMaster",
                                new string[] { "flag", "UserName" },
                                new string[] { "0", UserName.ToString() }, "dataset");
                        break;
                    default:
                        if (ds1 != null)
                        {
                            ds1.Clear();
                        }
                        break;
                }

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Credentials." }));
                }
                else
                {
                    string saltkey = apiprocedure.GenerateSaltKey();
                    if (apiprocedure.CompaireHashCode(ds1.Tables[0].Rows[0]["Password"].ToString(), OldPassword, saltkey))
                    {
                        //Update New Password by UserName prefix

                        switch (UserName.Substring(0, 1))
                        {
                            case "E": //for Employees Login offices like HO, DS, DCS, CC, BMC, MDP 
                                ds = apiprocedure.ByProcedure("sp_Login",
                                         new string[] { "flag", "UserName", "Password" },
                                         new string[] { "3", UserName, apiprocedure.SHA512_HASH(NewPassword) }, "dataset");
                                break;
                            case "P": //for Producer Login
                                ds = apiprocedure.ByProcedure("SptblPUProducerReg",
                                        new string[] { "flag", "UserName", "Password" },
                                        new string[] { "9", UserName, apiprocedure.SHA512_HASH(NewPassword) }, "dataset");
                                break;
                            case "D": //for Distributor Login
                                ds = apiprocedure.ByProcedure("USP_Mst_DistributorReg",
                                        new string[] { "flag", "UserName", "Password" },
                                        new string[] { "5", UserName, apiprocedure.SHA512_HASH(NewPassword) }, "dataset");
                                break;
                            case "S": //for Sub-Distributor Login
                                ds = apiprocedure.ByProcedure("USP_Mst_SubDistributorReg",
                                        new string[] { "flag", "UserName", "Password" },
                                        new string[] { "6", UserName, apiprocedure.SHA512_HASH(NewPassword) }, "dataset");
                                break;
                            case "B": //for Booth/Parlour Login
                                ds = apiprocedure.ByProcedure("USP_Mst_BoothReg",
                                        new string[] { "flag", "UserName", "Password" },
                                        new string[] { "5", UserName, apiprocedure.SHA512_HASH(NewPassword) }, "dataset");
                                break;
                            case "M": //for Temp Employees Login offices like HO, DS, DCS, CC, BMC, MDP 
                                ds = apiprocedure.ByProcedure("usp_Mst_McuUserMaster",
                                        new string[] { "flag", "UserName", "Password" },
                                        new string[] { "4",  UserName, apiprocedure.SHA512_HASH(NewPassword) }, "dataset");
                                break;
                            default:
                                if (ds != null)
                                {
                                    ds.Clear();
                                }
                                break;
                        }

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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void InsertMilkInwardOutwardDetails(string Key, int RefrenceId, int OfficeID, int OfficeTypeID, string Date, string ChallanNo, string TankerType, string VehicleNo, string EntryType, string ArrivalDateTime, string DispatchDateTime, string DriverName, string DriverMobileNo, string RepresentativeName, string RepresentativeMobileNo, string MilkType, string EntryDateTime, string CreatedByEmpID, string NextTankerDate, string NextTankerShift, string ClosingBalance, string Latitude, string Longitude, string TankerCapacity, string TankerCount, string Remark)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
				 if (ClosingBalance=="")
                {
                    ClosingBalance = "0";
                }
				
                DataSet ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                    new string[] { "Flag", 
                                   "Office_ID"
                                   ,"OfficeType_ID"
                                   ,"BI_MilkInOutRefID"
                                   ,"DT_Date"
                                   ,"V_ReferenceCode"
                                   ,"V_TankerType"
                                   ,"V_VehicleNo"
                                   ,"V_EntryType"
                                   ,"DT_ArrivalDateTime"
                                   ,"DT_DispatchDateTime"
                                   ,"V_DriverName"
                                   ,"V_DriverMobileNo"
                                   ,"V_RepresentativeName"
                                   ,"V_RepresentativeMobileNo"
                                   ,"V_MilkType"
                                   ,"I_CreatedByEmpID"
                                   ,"D_ClosingBalance"
                                   ,"DT_NextTankerDate"
                                   ,"V_Remark"
                                   ,"V_Latitude"
                                   ,"V_Longitude"
                                   ,"V_EntryFrom"
                                   ,"D_TankerCapacity"
                                   ,"I_TankerCount"
                                   ,"V_Shift"
                                    },
                    new string[] { "3", 
                                    OfficeID.ToString()
                                   ,OfficeTypeID.ToString()
                                   ,RefrenceId.ToString()
                                   ,Convert.ToDateTime(Date, cult).ToString("yyyy/MM/dd")
                                   ,ChallanNo
                                   ,TankerType
                                   ,VehicleNo
                                   ,EntryType
                                   ,Convert.ToDateTime(ArrivalDateTime, cult).ToString("yyyy/MM/dd hh:mm:ss tt")
                                   ,(DispatchDateTime == "" ? "" : Convert.ToDateTime(DispatchDateTime, cult).ToString("yyyy/MM/dd hh:mm:ss tt"))
                                   ,DriverName
                                   ,DriverMobileNo
                                   ,RepresentativeName
                                   ,RepresentativeMobileNo
                                   ,MilkType    
                                   ,CreatedByEmpID
                                   ,ClosingBalance
                                   ,(NextTankerDate == "" ? "" : Convert.ToDateTime(NextTankerDate, cult).ToString("yyyy/MM/dd"))
                                   ,Remark
                                   ,Latitude
                                   ,Longitude
                                   ,"App"
                                   ,TankerCapacity == "" ? "0.00" : TankerCapacity
                                   ,TankerCount
                                   ,NextTankerShift
                                    }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Something wents wrong." }));
                }
                else
                {
                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
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
                    else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Exists")
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
                        this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "0", Error = "Warning" }));
                    }
                    else
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
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

    private int CheckQCDifference(string ReferenceCode, string MobileNo, string CCName, string TankerNo)
    {
        int status = 0;
        try
        {
            DataSet ds2 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                               new string[] { "flag", "V_ReferenceCode" },
                               new string[] { "15", ReferenceCode }, "dataset");

            if (ds2.Tables[0].Rows.Count > 0)
            {
                string msg = "CC Name : " + CCName + "\nTanker No. : " + TankerNo + "\n";

                //Fat - F, R, S Compartment
                if (ds2.Tables[0].Select("V_SealLocation = 'F'").Length > 0)
                {
                    if (Convert.ToDecimal(ds2.Tables[0].Select("V_SealLocation = 'F'")[0]["Diff_FAT"].ToString()) >= Convert.ToDecimal("0.2"))
                    {
                        msg += "FAT diff. of Front chamber : " + ds2.Tables[0].Select("V_SealLocation = 'F'")[0]["Diff_FAT"].ToString() + "\n";
                    }
                }

                if (ds2.Tables[0].Select("V_SealLocation = 'R'").Length > 0)
                {
                    if (Convert.ToDecimal(ds2.Tables[0].Select("V_SealLocation = 'R'")[0]["Diff_FAT"].ToString()) >= Convert.ToDecimal("0.2"))
                    {
                        msg += "FAT diff. of Rear chamber : " + ds2.Tables[0].Select("V_SealLocation = 'R'")[0]["Diff_FAT"].ToString() + "\n";
                    }
                }

                if (ds2.Tables[0].Select("V_SealLocation = 'S'").Length > 0)
                {
                    if (Convert.ToDecimal(ds2.Tables[0].Select("V_SealLocation = 'S'")[0]["Diff_FAT"].ToString()) >= Convert.ToDecimal("0.2"))
                    {
                        msg += "FAT diff. of Single chamber : " + ds2.Tables[0].Select("V_SealLocation = 'S'")[0]["Diff_FAT"].ToString() + "\n";
                    }
                }
                //End of Fat - F, R, S Compartment

                //SNF - F, R, S Compartment
                if (ds2.Tables[0].Select("V_SealLocation = 'F'").Length > 0)
                {
                    if (Convert.ToDecimal(ds2.Tables[0].Select("V_SealLocation = 'F'")[0]["Diff_SNF"].ToString()) >= Convert.ToDecimal("0.25"))
                    {
                        msg += "SNF diff. of Front chamber : " + ds2.Tables[0].Select("V_SealLocation = 'F'")[0]["Diff_SNF"].ToString() + "\n";
                    }
                }

                if (ds2.Tables[0].Select("V_SealLocation = 'R'").Length > 0)
                {
                    if (Convert.ToDecimal(ds2.Tables[0].Select("V_SealLocation = 'R'")[0]["Diff_SNF"].ToString()) >= Convert.ToDecimal("0.25"))
                    {
                        msg += "SNF diff. of Rear chamber : " + ds2.Tables[0].Select("V_SealLocation = 'R'")[0]["Diff_SNF"].ToString() + "\n";
                    }
                }

                if (ds2.Tables[0].Select("V_SealLocation = 'S'").Length > 0)
                {
                    if (Convert.ToDecimal(ds2.Tables[0].Select("V_SealLocation = 'S'")[0]["Diff_SNF"].ToString()) >= Convert.ToDecimal("0.25"))
                    {
                        msg += "SNF diff. of Single chamber : " + ds2.Tables[0].Select("V_SealLocation = 'S'")[0]["Diff_SNF"].ToString() + "\n";
                    }
                }
                //End of SNF - F, R, S Compartment

                //int i = SendMsg(MobileNo, msg);
                int i = 1;
                if (i == 1)
                {
                    status = 1;
                }
            }
        }
        catch (Exception)
        {
            status = 0;
        }
        return status;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void InsertMilkQualityDetailsByEntryID(string Key, int EntryID, string MilkQuantity, string SealLocation, string FAT, string SNF, string CLR, string Temp, string Acidity, string COB, string MBRT, string Alcohol, string MilkQuality)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                    new string[] { "Flag"
                                  ,"I_EntryID"
                                  ,"I_MilkQuantity"
                                  ,"V_SealLocation"
                                  ,"D_FAT"
                                  ,"D_SNF"
                                  ,"D_CLR"
                                  ,"V_Temp"
                                  ,"V_Acidity"
                                  ,"V_COB"
                                  ,"V_Alcohol"
                                  ,"V_MBRT"
                                  ,"V_MilkQuality" },
                    new string[] { "4" ,
                                    EntryID.ToString()
                                   ,MilkQuantity
                                   ,SealLocation
                                   ,FAT
                                   ,SNF
                                   ,CLR
                                   ,Temp
                                   ,Acidity
                                   ,COB
                                   ,Alcohol
                                   ,MBRT
                                   ,MilkQuality
                                   }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Something wents wrong." }));
                }
                else
                {
                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        int i = 0;
                        string Mobile = "";
                        DataSet ds3 = apiprocedure.ByProcedure("Usp_UnitMaster",
                                                         new string[] { "flag", "I_EntryID" },
                                                         new string[] { "10", EntryID.ToString() }, "dataset");
                        if (ds3.Tables[0].Rows.Count > 0)
                        {
                            switch (ds3.Tables[0].Rows[0]["V_EntryType"].ToString().ToLower())
                            {
                                case "out":
                                    DataSet ds4 = apiprocedure.ByProcedure("Usp_UnitMaster",
                                                             new string[] { "flag", "I_UnitID" },
                                                             new string[] { "11", ds3.Tables[0].Rows[0]["I_UnitID"].ToString() }, "dataset");

                                    if (ds4.Tables[0].Rows.Count > 0)
                                    {
                                        Mobile = ds4.Tables[0].Rows[0]["DSMobileNo"].ToString();
                                        i = CheckAcidity(ds3.Tables[0].Rows[0]["V_ReferenceCode"].ToString(), ds4.Tables[0].Rows[0]["DSMobileNo"].ToString(), ds3.Tables[0].Rows[0]["V_UnitName"].ToString(), ds3.Tables[0].Rows[0]["V_VehicleNo"].ToString());
                                    }
                                    break;

                                case "in":
                                    Mobile = ds3.Tables[0].Rows[0]["V_MobileNo"].ToString();

                                    i = CheckQCDifference(ds3.Tables[0].Rows[0]["V_ReferenceCode"].ToString(), ds3.Tables[0].Rows[0]["V_MobileNo"].ToString(), ds3.Tables[0].Rows[0]["V_UnitName"].ToString(), ds3.Tables[0].Rows[0]["V_VehicleNo"].ToString());
                                    break;
                                default:
                                    //no condition for default
                                    break;
                            }
                        }

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
                            if (i == 1)
                            {
                                row.Add("Alert", "Sent to Mobile No. " + Mobile);
                            }
                        }
                        this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));
                    }
                    else
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
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
    public void InsertTankerSealDetailsByEntryID(string Key, string EntryID, string SealColorID, string SealNo, string SealLocation, string SealRemark)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (EntryID == "" && SealColorID == "" && SealNo == "" && SealLocation == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    if (SealColorID.ToString().Split(',').Length != SealNo.ToString().Split(',').Length && SealNo.ToString().Split(',').Length != SealLocation.ToString().Split(',').Length)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Tanker seal number value passed " + SealNo.ToString().Split(',').Length + " argument(s) and tanker seal location value passed " + SealLocation.ToString().Split(',').Length + " argument(s), Kindly pass equal both arrays arguments and value." }));
                    }
                    else
                    {
                        DataTable dtable = new DataTable();
                        DataRow dr;

                        dtable.Columns.Add("V_SealNo", typeof(string));
                        dtable.Columns.Add("TI_SealColor", typeof(string));
                        dtable.Columns.Add("V_SealLocation", typeof(string));
                        dtable.Columns.Add("V_SealRemark", typeof(string));

                        int len = SealNo.ToString().Split(',').Length;

                        for (int i = 0; i < len; i++)
                        {
                            dr = dtable.NewRow();
                            dr[0] = SealNo.ToString().Split(',')[i];
                            dr[1] = SealColorID.ToString().Split(',')[i];
                            dr[2] = SealLocation.ToString().Split(',')[i];
                            dr[3] = SealRemark.ToString().Split(',')[i];
                            dtable.Rows.Add(dr);

                        }

                        DataSet ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                                    new string[] { "Flag", "I_EntryID" },
                                    new string[] { "5", EntryID.ToString() },
                                    "type_Trn_TankerSealDetails", dtable, "TableSave");

                        if (ds1.Tables[0].Rows.Count == 0)
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Something wents wrong." }));
                        }
                        else
                        {
                            if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
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
                            else
                            {
                                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                            }
                        }
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
    public void InsertMilkSampleDetailsByEntryID(string Key, string EntryID, string SampleNo, string SealLocation, string SampleRemark)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (EntryID == "" && SampleNo == "" && SealLocation == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    if (SampleNo.Split(',').Length != SealLocation.Split(',').Length)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Milk Sample number value passed " + SampleNo.ToString().Split(',').Length + " argument(s) and tanker seal location value passed " + SealLocation.ToString().Split(',').Length + " argument(s), Kindly pass equal both arrays arguments and value." }));
                    }
                    else
                    {
                        DataTable dtable = new DataTable();
                        DataRow dr;

                        dtable.Columns.Add("V_SealLocation", typeof(string));
                        dtable.Columns.Add("V_SampleNo", typeof(string));
                        dtable.Columns.Add("V_SampleRemark", typeof(string));

                        int len = SampleNo.Split(',').Length;

                        for (int i = 0; i < len; i++)
                        {
                            dr = dtable.NewRow();
                            dr[0] = SealLocation.Split(',')[i];
                            dr[1] = SampleNo.Split(',')[i];
                            dr[2] = SampleRemark.Split(',')[i];
                            dtable.Rows.Add(dr);
                        }

                        DataSet ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                                    new string[] { "Flag", "I_EntryID" },
                                    new string[] { "17", EntryID.ToString() },
                                    "type_Trn_MilkSampleDetail", dtable, "TableSave");

                        if (ds1.Tables[0].Rows.Count == 0)
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Something wents wrong." }));
                        }
                        else
                        {
                            if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
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
                            else
                            {
                                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                            }
                        }
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
    public void InsertAdulterationTestByEntryID(string Key, string EntryID, string SealLocation, string AdulterationType, string AdulterationValue)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (EntryID == "" && SealLocation == "" && AdulterationType == "" && AdulterationValue == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    if (AdulterationType.ToString().Split(',').Length != AdulterationValue.ToString().Split(',').Length)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Adulteration Type pass " + AdulterationType.ToString().Split(',').Length + " argument(s) and Adulteration Value pass " + AdulterationValue.ToString().Split(',').Length + " argument(s), Kindly pass equal both arrays arguments and value." }));
                    }
                    else
                    {
                        DataTable dtable = new DataTable();
                        DataRow dr;

                        dtable.Columns.Add("V_SealLocation", typeof(string));
                        dtable.Columns.Add("V_AdulterationType", typeof(string));
                        dtable.Columns.Add("V_AdulterationValue", typeof(string));

                        int len = AdulterationType.ToString().Split(',').Length;

                        for (int i = 0; i < len; i++)
                        {
                            dr = dtable.NewRow();
                            dr[0] = SealLocation;
                            dr[1] = AdulterationType.ToString().Split(',')[i];
                            dr[2] = AdulterationValue.ToString().Split(',')[i];
                            dtable.Rows.Add(dr);
                        }

                        DataSet ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                                    new string[] { "Flag", "I_EntryID" },
                                    new string[] { "9", EntryID.ToString() },
                                    "type_Trn_tblAdulterationTest", dtable, "TableSave");

                        if (ds1.Tables[0].Rows.Count == 0)
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Something wents wrong." }));
                        }
                        else
                        {
                            if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
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
                            else
                            {
                                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                            }
                        }
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

    //----------------------19/02/2020-------------------

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void InsertMilkInwardOfficeSequenceDetail(string Key, string MilkInOutRefID, string SequenceNo, string OfficeID, string SequenceStatus)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (MilkInOutRefID == "" && SequenceNo == "" && OfficeID == "" && SequenceStatus == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    if (SequenceNo.ToString().Split(',').Length != OfficeID.ToString().Split(',').Length)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Sequence No Type pass " + SequenceNo.ToString().Split(',').Length + " argument(s) and Office ID pass " + OfficeID.ToString().Split(',').Length + " argument(s), Kindly pass equal both arrays arguments and value." }));
                    }
                    else
                    {
                        DataTable dtable = new DataTable();
                        DataRow dr;

                       dtable.Columns.Add("TI_SequenceNo", typeof(string));
                        dtable.Columns.Add("I_OfficeID", typeof(string));
                        dtable.Columns.Add("B_SequenceStatus", typeof(string));

                        int len = SequenceNo.ToString().Split(',').Length;

                        for (int i = 0; i < len; i++)
                        {
                            dr = dtable.NewRow();
                            dr[0] = SequenceNo.ToString().Split(',')[i];
                            dr[1] = OfficeID.ToString().Split(',')[i];
                            dr[2] = SequenceStatus;
                            dtable.Rows.Add(dr);
                        }

                        DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                                    new string[] { "Flag", "BI_MilkInOutRefID" },
                                    new string[] { "18", MilkInOutRefID.ToString() },
                                    "type_Trn_MilkInwardOfficeSequenceDetails", dtable, "TableSave");

                        if (ds1.Tables[0].Rows.Count == 0)
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Something wents wrong." }));
                        }
                        else
                        {
                            if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
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
                            else
                            {
                                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                            }
                        }
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

    //--------------------------------------------------
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetOfficeType(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("SpAdminOfficeType",
                    new string[] { "flag", },
                    new string[] { "7" }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Data Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetOfficeByOfficeTypeID(string Key, string OfficeTypeID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("SpAdminOffice",
                    new string[] { "flag", "OfficeType_ID" },
                    new string[] { "5", OfficeTypeID }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Data Found." }));
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

    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public void GetTankerDetailByOfficeID(string Key, string OfficeID)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    this.Context.Response.ContentType = "application/json; charset=utf-8";
    //    if (Key == securityKey)
    //    {
    //        try
    //        {
    //            DataSet ds1 = apiprocedure.ByProcedure("Usp_UnitTankerMapping",
    //                new string[] { "Flag", "I_OfficeID" },
    //                new string[] { "2", OfficeID }, "dataset");

    //            if (ds1.Tables[0].Rows.Count == 0)
    //            {
    //                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Data Found." }));
    //            }
    //            else
    //            {
    //                dt = ds1.Tables[0];
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
    //                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));

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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetMilkQuantityByDateAndOfficeID(string Key, string OfficeID, string DT_Date)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (DT_Date != "")
                {
                    DataSet ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                    new string[] { "Flag", "Office_ID", "DT_Date" },
                    new string[] { "6", OfficeID, Convert.ToDateTime(DT_Date, cult).ToString("yyyy/MM/dd") }, "dataset");

                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Data Found." }));
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
                else
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Date field should not blank." }));
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
    public void GetDispatchDetailbyReferenceNo(string Key, string Reference_Id)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                                new string[] { "flag", "BI_MilkInOutRefID" },
                                new string[] { "5", Reference_Id }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Data Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetReferenceNoDetailbyDateAndOfficeID(string Key, string OfficeID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (OfficeID == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill Unit ID" }));
                }
                else
                {
                    DataSet ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                        new string[] { "Flag", "Office_ID" },
                        new string[] { "11", OfficeID }, "dataset");

                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Data Found." }));
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
    public void GetSNFByCLRAndFAT(string Key, string FAT, string CLR)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (FAT == "" || CLR == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to input field of FAT or CLR" }));
                }
                else
                {
                    decimal snf = 0, clr = 0, fat = 1;
                    try
                    {
                        clr = Convert.ToDecimal(CLR);
                        fat = Convert.ToDecimal(FAT);

                        //SNF = (CLR / 4) + (FAT * 0.2) + 0.7 -- Formula Before
                        //SNF = (CLR / 4) + (0.25 * FAT) + 0.44 -- Formula After Mail dt: 27 Jan 2020, 16:26
                        snf = ((clr / 4) + (fat * Convert.ToDecimal(0.25)) + Convert.ToDecimal(0.44));

                        DataRow dr;
                        dt.Columns.Add(new DataColumn("SNF", typeof(decimal)));
                        dr = dt.NewRow();
                        dr[0] = Math.Round(snf, 2).ToString();
                        dt.Rows.Add(dr);

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
                    catch (Exception ex)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
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

    private int CheckAcidity(string ReferenceCode, string MobileNo, string CCName, string TankerNo)
    {
        int status = 0;
        try
        {

            DataSet ds2 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                               new string[] { "flag", "V_ReferenceCode" },
                               new string[] { "16", ReferenceCode }, "dataset");

            if (ds2.Tables[0].Rows.Count > 0)
            {
                string msg = "CC Name : " + CCName + "\nTanker No. : " + TankerNo + "\n";

                //Acidity - F, R, S Compartment
                if (ds2.Tables[0].Select("V_SealLocation = 'F'").Length > 0)
                {
                    if (Convert.ToDecimal(ds2.Tables[0].Select("V_SealLocation = 'F'")[0]["V_Acidity"].ToString()) >= Convert.ToDecimal("0.150"))
                    {
                        msg += "Acidity of Front chamber : " + ds2.Tables[0].Select("V_SealLocation = 'F'")[0]["V_Acidity"].ToString() + "\n";
                    }
                }

                if (ds2.Tables[0].Select("V_SealLocation = 'R'").Length > 0)
                {
                    if (Convert.ToDecimal(ds2.Tables[0].Select("V_SealLocation = 'R'")[0]["V_Acidity"].ToString()) >= Convert.ToDecimal("0.150"))
                    {
                        msg += "Acidity of Rear chamber : " + ds2.Tables[0].Select("V_SealLocation = 'R'")[0]["V_Acidity"].ToString() + "\n";
                    }
                }

                if (ds2.Tables[0].Select("V_SealLocation = 'S'").Length > 0)
                {
                    if (Convert.ToDecimal(ds2.Tables[0].Select("V_SealLocation = 'S'")[0]["V_Acidity"].ToString()) >= Convert.ToDecimal("0.150"))
                    {
                        msg += "Acidity of Milk : " + ds2.Tables[0].Select("V_SealLocation = 'S'")[0]["V_Acidity"].ToString() + "\n";
                    }
                }
                //End of Acidity - F, R, S Compartment

                //int i = SendMsg(MobileNo, msg);
                int i = 1;
                if (i == 1)
                {
                    status = 1;
                }
            }
        }
        catch (Exception)
        {
            status = 0;
        }
        return status;
    }

    public int SendMsg(string MobileNo, string msg)
    {
        int status = 0;
        try
        {
            //Your authentication key
            string authKey = "3597C1493C124F";

            //Sender ID
            string senderId = "SANCHI";

            string link = "http://mysms.mssinfotech.com/app/smsapi/index.php?key=" + authKey + "&type=unicode&routeid=288&contacts=" + MobileNo + "&senderid=" + senderId + "&msg=" + Server.UrlEncode(msg);
            HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream stream = response.GetResponseStream();

            //End Sending OTP SMS
            status = 1;
        }
        catch (Exception)
        {
            status = 0;
        }
        return status;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetCCQCDetailbyReferenceNo(string Key, string ReferenceNo)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                    new string[] { "Flag", "V_ReferenceCode" },
                    new string[] { "14", ReferenceNo }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Data Found." }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetDSQCDetailbyReferenceNo(string Key, string ReferenceNo)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                    new string[] { "Flag", "V_ReferenceCode" },
                    new string[] { "23", ReferenceNo }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Data Found." }));
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


    //----------20/02/2020-----------DS--------

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetDSProducerDetailByQRCode(string Key, string QRCode)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string QRCodeValue = QRCode.ToString();
                string DsID = QRCodeValue.ToString().Split('.')[0].ToString();
                string SID = QRCodeValue.ToString().Split('.')[1].ToString();
                string UserName = QRCodeValue.ToString().Split('.')[2].ToString();
                DataSet ds1 = apiprocedure.ByProcedure("sp_Mst_DailyMilkCollection",
                    new string[] { "Flag", "Office_Id", "UserName" },
                    new string[] { "14", SID, UserName }, "dataset");

                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Data Found." }));
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
                        row.Add("DCS_ID", SID);
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

     // [WebMethod]
    // [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    // public void InsertDailyMilkCollectionDetail(string Key, string SocietyName, string Office_Id, string Date, string Shift, string Producer_ID, string MilkSupplyQty, string MilkType, string Review, string CreatedBy)
    // {
        // DataSet ds = new DataSet();
        // DataTable dt = new DataTable();
        // System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        // this.Context.Response.ContentType = "application/json; charset=utf-8";
        // if (Key == securityKey)
        // {
            // try
            // {
                // if (Office_Id == "" && Date == "" && Shift == "" && Producer_ID == "" && MilkSupplyQty == "" && MilkType == "" && CreatedBy == "")
                // {
                    // this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                // }
                // else
                // {
                    // DataSet ds1 = apiprocedure.ByProcedure("sp_Mst_DailyMilkCollection",
                  // new string[] { "Flag" 
                      // ,"Office_Id"
			         // ,"V_SocietyName"
			         // ,"Dt_Date"
			         // ,"V_Shift"
			         // ,"I_Producer_ID"
			         // ,"I_MilkSupplyQty"
			         // ,"V_MilkType"
			         // ,"V_Review"
			         // ,"CreatedBy"
			         // ,"CreatedBy_IP" },
                  // new string[] { "12" 
                      // ,Office_Id.ToString()
                      // ,SocietyName.ToString()
                      // ,Convert.ToDateTime(Date, cult).ToString("yyyy/MM/dd")
                      // ,Shift.ToString()
                      // ,Producer_ID.ToString()
                      // ,MilkSupplyQty.ToString()
                      // ,MilkType.ToString()
                      // ,Review.ToString()
                      // ,CreatedBy.ToString()
                      // ,apiprocedure.GetMACAddress()                      
                  // }, "dataset");
                    // if (ds1.Tables[0].Rows.Count == 0)
                    // {
                        // this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["Msg"].ToString() }));
                    // }
                    // else
                    // {
                        // if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        // {
                            // dt = ds1.Tables[0];
                            // List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                            // Dictionary<string, object> row = null;
                            // foreach (DataRow rs in dt.Rows)
                            // {
                                // row = new Dictionary<string, object>();
                                // foreach (DataColumn col in dt.Columns)
                                // {
                                    // row.Add(col.ColumnName, rs[col]);
                                // }
                                // row.Add("Producer_ID", Producer_ID);
                                // rows.Add(row);
                            // }
                            // this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));
                        // }
                        // else
                        // {
                            // this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["Msg"].ToString() }));
                        // }
                    // }
                // }

            // }
            // catch (Exception ex)
            // {
                // this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            // }
            // dt.Clear();
            // ds.Clear();
        // }
        // else
        // {
            // this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        // }
    // }
	
	
	    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public void InsertDailyMilkCollectionDetail(string Key, string SocietyName, string Office_Id, string Date, string Shift, string Producer_ID, string MilkSupplyQty, string MilkType, string Review, string Quality, string Fat, string CLR, string SNF, string Rate, string Amount, string CreatedBy, string CreatedIP)
    //{
    //    DataSet ds = new DataSet();
    //    DataTable dt = new DataTable();
    //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    //    this.Context.Response.ContentType = "application/json; charset=utf-8";
    //    if (Key == securityKey)
    //    {
    //        try
    //        {
    //            if (Office_Id == "" && Date == "" && Shift == "" && Producer_ID == "" && MilkSupplyQty == "" && MilkType == "" && CreatedBy == "")
    //            {
    //                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
    //            }
    //            else
    //            {
    //                DataSet ds1 = apiprocedure.ByProcedure("sp_Mst_DailyMilkCollection",
    //              new string[] { "Flag"
    //                  ,"Office_Id"
    //    ,"V_SocietyName"
    //    ,"Dt_Date"
    //    ,"V_Shift"
    //    ,"I_Producer_ID"
    //    ,"I_MilkSupplyQty"
    //    ,"V_MilkType"
    //    ,"V_Review"
    //                 ,"Quality"
    //                 ,"Fat"
    //                 ,"CLR"
    //                 ,"SNF"
    //                 ,"Rate"
    //                 ,"Amount"
    //                 ,"TotalSNFInKg"
    //                 ,"TotalFatInKg"
    //    ,"CreatedBy"
    //    ,"CreatedBy_IP" },
    //              new string[] { "12"
    //                  ,Office_Id.ToString()
    //                  ,SocietyName.ToString()
    //                  ,Convert.ToDateTime(Date, cult).ToString("yyyy/MM/dd")
    //                  ,Shift.ToString()
    //                  ,Producer_ID.ToString()
    //                  ,MilkSupplyQty.ToString()
    //                  ,MilkType.ToString()
    //                  ,Review.ToString()
    //                  ,Quality.ToString()
    //                  ,Fat.ToString()
    //                  ,CLR.ToString()
    //                  ,SNF.ToString()
    //                  ,Rate.ToString()
    //                  ,Amount.ToString()
    //                  ,"0.00"
    //                  ,"0.00"
    //                  ,CreatedBy.ToString()
    //                  ,CreatedIP.ToString()                      
    //              }, "dataset");
    //                if (ds1.Tables[0].Rows.Count == 0)
    //                {
    //                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["Msg"].ToString() }));
    //                }
    //                else
    //                {
    //                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
    //                    {
    //                        dt = ds1.Tables[0];
    //                        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
    //                        Dictionary<string, object> row = null;
    //                        foreach (DataRow rs in dt.Rows)
    //                        {
    //                            row = new Dictionary<string, object>();
    //                            foreach (DataColumn col in dt.Columns)
    //                            {
    //                                row.Add(col.ColumnName, rs[col]);
    //                            }
    //                            row.Add("Producer_ID", Producer_ID);
    //                            rows.Add(row);
    //                        }
    //                        this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));
    //                    }
    //                    else
    //                    {
    //                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["Msg"].ToString() }));
    //                    }
    //                }
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
	
	// NEW API
	
	
	    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void InsertDailyMilkCollectionDetail(string Key, string SocietyName, string Office_Id, string Date, string Shift, string Producer_ID, string MilkSupplyQty, string MilkType, string Review, string Quality, string Fat, string CLR, string SNF, string Rate, string Amount, string CreatedBy, string CreatedIP)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (Office_Id == "" && Date == "" && Shift == "" && Producer_ID == "" && MilkSupplyQty == "" && MilkType == "" && CreatedBy == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    string ShiftNameSMS = "";
                    string MilkTypeSMS = "";
                    string ProducerNameCodeSMS = "";
                    string P_MobileNo = "";
                    string P_MSG = "";
                    int SMSSTATUS = 0;
                    if (Shift == "Morning")
                    {
                        ShiftNameSMS = "M";
                    }
                    else
                    {
                        ShiftNameSMS = "E";
                    }

                    if (MilkType == "Buffalo")
                    {
                        MilkTypeSMS = "B";
                    }
                    else if (MilkType == "Cow")
                    {
                        MilkTypeSMS = "C";
                    }
                    else
                    {
                        MilkTypeSMS = "M";
                    }

                    DataSet dsP_Info = apiprocedure.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "ProducerId" },
                              new string[] { "4", Producer_ID }, "dataset");

                    if (dsP_Info != null && dsP_Info.Tables.Count > 0 && dsP_Info.Tables[0].Rows.Count > 0)
                    {
                        P_MobileNo = dsP_Info.Tables[0].Rows[0]["Mobile"].ToString();

                        ProducerNameCodeSMS = dsP_Info.Tables[0].Rows[0]["ProducerNameEnglish"].ToString() + "(" + dsP_Info.Tables[0].Rows[0]["ProducerCardNo"].ToString() + ")";

                        P_MSG = "D:" + Convert.ToDateTime(Date, cult).ToString("dd/MM/yyyy")
                                     + " Shift:" + ShiftNameSMS
                                     + " " + ProducerNameCodeSMS
                                     + " Milk Type:" + MilkTypeSMS
                                     + " Qty:" + MilkSupplyQty
                                     + " Fat:" + Fat
                                     + " SNF:" + SNF
                                     + " CLR:" + CLR
                                     + " RTPL:" + Rate
                                     + " Amount:" + Amount;

                    }



                    DataSet ds1 = apiprocedure.ByProcedure("sp_Mst_DailyMilkCollection",
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
                                ,"Remark"},
                  new string[] { "12"
                      ,Office_Id.ToString()
                      ,SocietyName.ToString()
                      ,Convert.ToDateTime(Date, cult).ToString("yyyy/MM/dd")
                      ,Shift.ToString()
                      ,Producer_ID.ToString()
                      ,MilkSupplyQty.ToString()
                      ,MilkType.ToString()
                      ,Review.ToString()
                      ,Quality.ToString()
                      ,Fat.ToString()
                      ,CLR.ToString()
                      ,SNF.ToString()
                      ,Rate.ToString()
                      ,Amount.ToString()
                      ,"0.00"
                      ,"0.00"
                      ,CreatedBy.ToString()
                      ,CreatedIP.ToString() 
                      ,P_MSG
                  }, "dataset");


                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["Msg"].ToString() }));
                    }
                    else
                    {
                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            if (P_MobileNo != "")
                            {
                                if (P_MSG != "")
                                {
                                    SMSSTATUS = SendMsgFormMBC(P_MobileNo, P_MSG);
                                }
                            }

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
                                row.Add("Producer_ID", Producer_ID);
                                row.Add("Sms_Status", SMSSTATUS);
                                rows.Add(row);
                            }
                            this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));
                        }
                        else
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["Msg"].ToString() }));
                        }
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
    public void GetFreeChamberLocationByReferenceID_MCMS(string Key, string BI_MilkInOutRefID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (BI_MilkInOutRefID == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                                new string[] { "flag", "BI_MilkInOutRefID" },
                                new string[] { "23", BI_MilkInOutRefID }, "dataset");

                    if (ds1.Tables[0].Rows.Count > 0)
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
                    else
                    {
                        dt = ds1.Tables[0];
                        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                        Dictionary<string, object> row = null;

                        row = new Dictionary<string, object>();
                        row.Add("V_EntryType", "Out");
                        row.Add("V_SealLocationText", "Front");
                        row.Add("V_SealLocation", "F");
                        rows.Add(row);

                        row = new Dictionary<string, object>();
                        row.Add("V_EntryType", "Out");
                        row.Add("V_SealLocationText", "Rear");
                        row.Add("V_SealLocation", "R");
                        rows.Add(row);

                        this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));
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
    public void GetDecrypt(string Key, string Value)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (Value == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;

                    row = new Dictionary<string, object>();
                    row.Add("DeccryptValue", apiprocedure.Decrypt(Value));
                    rows.Add(row);

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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetEncrypt(string Key, string Value)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (Value == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;

                    row = new Dictionary<string, object>();
                    row.Add("EncryptValue", apiprocedure.Encrypt(Value));
                    rows.Add(row);

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


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void InsertSocietyWiseMilkProcess(string Key, string Office_Id, string EntryDate, string EntryShift, string Producer_ID, string Quality, string Fat, string CLR, string SNF, string Rate, string Amount, string Remark, string TotalSNFInKg, string TotalFatInKg, string CreatedBy, string MilkType)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (Office_Id == "" && EntryDate == "" && EntryShift == "" && Producer_ID == "" && Quality == "" && Fat == "" && CLR == "" && SNF == "" && CreatedBy == "" && MilkType == "" && TotalSNFInKg == "" && TotalFatInKg == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    DataSet ds1 = apiprocedure.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                  new string[] { "Flag" 
                      ,"OfficeId"
                      ,"EntryDate"
                      ,"EntryShift"
                      ,"Producer_ID"
                      ,"Quality",
                      "Fat"
                      ,"CLR"
                      ,"SNF"
                      ,"Rate"
                      ,"Amount"
                      ,"Remark"
                      ,"TotalSNFInKg"
                      ,"TotalFatInKg"
                      ,"CreatedBy"
                      ,"CreatedIP"
                      ,"MilkType" },
                  new string[] { "3" 
                      ,Office_Id.ToString()
                      ,Convert.ToDateTime(EntryDate, cult).ToString("yyyy/MM/dd") 
                      ,EntryShift.ToString()
                      ,Producer_ID.ToString()
                      ,Quality.ToString()
                      ,Fat.ToString()
                      ,CLR.ToString()
                      ,SNF.ToString()
                      ,Rate.ToString()
                      ,Amount.ToString()
                      ,Remark.ToString()
                      ,TotalSNFInKg.ToString()
                      ,TotalFatInKg.ToString()
                      ,CreatedBy.ToString()
                      ,apiprocedure.GetLocalIPAddress()
                      ,MilkType.ToString()                      
                  }, "dataset");
                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                    }
                    else
                    {
                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
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
                        else
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                        }
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
    public void GetFilledChamberLocationByReferenceID_MCMS(string Key, string ChallanNo)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (ChallanNo == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    DataSet ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                                             new string[] { "flag", "V_ReferenceCode" },
                                             new string[] { "20", ChallanNo }, "dataset");

                    if (ds1.Tables[0].Rows.Count > 0)
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
    public void GetSealColorList(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("USP_Mst_SealColor",
                            new string[] { "flag" },
                            new string[] { "1" }, "dataset");

                if (ds1.Tables[0].Rows.Count > 0)
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
    #endregion
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void InsertCitizenAdvanceCardRegistrationDetail_Demand(string Key, string Office_ID, string CitizenName, string MobileNo, string CreatedBy, string CreatedByIP)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (Office_ID == "" && CitizenName == "" && MobileNo == "" && CreatedBy == "" && CreatedByIP == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    DataSet ds1 = apiprocedure.ByProcedure("USP_Mst_AdvanceCard", new string[] { "Flag" 
                    ,"CitizenName"
					,"MobNo"
					,"Office_ID"
					,"CreatedBy"
					,"CreatedByIP" },
                    new string[] { "13" 
                    ,CitizenName.ToString()
                    ,MobileNo.ToString()
                    ,Office_ID.ToString()
                    ,CreatedBy.ToString()
                    ,CreatedByIP.ToString()          
                  }, "dataset");
                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                    }
                    else
                    {
                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
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
                        else
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                        }
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
    public void UpdateCitizenAdvanceCardRegistrationDetail_Demand(string Key, string Office_ID, string CitizenId, string CitizenName, string MobileNo, string PageName, string Remark, string CreatedBy, string CreatedByIP)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (Office_ID == "" && CitizenId == "" && CitizenName == "" && MobileNo == "" && PageName == "" && Remark == "" && CreatedBy == "" && CreatedByIP == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    DataSet ds1 = apiprocedure.ByProcedure("USP_Mst_AdvanceCard", new string[] { "Flag" 
                    ,"CitizenId"
                    ,"CitizenName"
                    ,"MobNo"
                    ,"Office_ID"
                    ,"CreatedBy"
                    ,"CreatedByIP"
                    ,"PageName"
                    ,"Remark"
                    },
                    new string[] { "14" 
                    ,CitizenId
                    ,CitizenName.ToString()
                    ,MobileNo.ToString()
                    ,Office_ID.ToString()
                    ,CreatedBy.ToString()
                    ,CreatedByIP.ToString()   
                    ,PageName.ToString()
                    ,Remark.ToString()
                  }, "dataset");
                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                    }
                    else
                    {
                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
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
                        else
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                        }
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
    public void DeleteCitizenAdvanceCardRegistrationDetail_Demand(string Key, string CitizenId, string CreatedBy, string CreatedByIP, string PageName, string Remark)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (CitizenId == "" && CreatedBy == "" && CreatedByIP == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    DataSet ds1 = apiprocedure.ByProcedure("USP_Mst_AdvanceCard", new string[] { "Flag" 
                    ,"CitizenId"
                    ,"CreatedBy"
                    ,"CreatedByIP"
                    ,"PageName"
                    ,"Remark"
                    },
                    new string[] { "15" 
                    ,CitizenId
                    ,CreatedBy.ToString()
                    ,CreatedByIP.ToString()    
                    ,PageName.ToString()
                    ,Remark.ToString()
                  }, "dataset");
                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                    }
                    else
                    {
                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
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
                        else
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                        }
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
    public void GetCitizenAdvanceCardRegistrationDetail_Demand(string Key, string CreatedBy, string Office_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("USP_Mst_AdvanceCard", new string[] { "Flag"
                    , "CreatedBy"
                    , "Office_ID" },
                new string[] { "1"
                    ,CreatedBy.ToString()
                    ,Office_ID.ToString()
                }, "dataset");
                if (ds1.Tables.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No record found." }));
                }
                else if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No record found." }));
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


    //------------------03-03-2020-03-40-PM--------------

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetItemCategory_Demand(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("USP_Mst_AdvanceCard", new string[] { "Flag" },
                new string[] { "6" }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No data found" }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetItemName_Demand(string Key, string ItemCat_id, string Office_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("USP_Mst_AdvanceCard", new string[] { "Flag", "ItemCat_id", "Office_ID" },
                new string[] { "7" ,ItemCat_id.ToString(),Office_ID.ToString()
                }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No data found" }));
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


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetShift_Demand(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("USP_Mst_ShiftMaster",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No data found" }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void InsertCitizenAndItemMappingDetail_Demand(string Key, string Office_ID, string CitizenId, string ItemCat_id, string Item_id, string Total_ItemQty, string Shift_id, string EffectiveFromDate, string EffectiveToDate, string CreatedBy, string CreatedByIP)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (Office_ID == "" && CitizenId == "" && ItemCat_id == "" && Item_id == "" && Total_ItemQty == "" && Shift_id == "" && EffectiveFromDate == "" && EffectiveToDate == "" && CreatedBy == "" && CreatedByIP == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    DataSet ds1 = apiprocedure.ByProcedure("USP_Mst_AdvanceCard", new string[] { "Flag" 
                    ,"CitizenId"
                    ,"ItemCat_id"
                    ,"Item_id"
                    ,"Total_ItemQty"
                    ,"Shift_id"
                    ,"EffectiveFromDate"
                    ,"EffectiveToDate"
                    ,"Office_ID"
                    ,"CreatedBy"
                    ,"CreatedByIP"
                    ,"PlatformType" },
                    new string[] { "8" 
                    ,CitizenId.ToString()
                    ,ItemCat_id.ToString()
                    ,Item_id.ToString()
                    ,Total_ItemQty.ToString()
                    ,Shift_id.ToString()
                    ,Convert.ToDateTime(EffectiveFromDate, cult).ToString("yyyy/MM/dd") 
                    ,Convert.ToDateTime(EffectiveToDate, cult).ToString("yyyy/MM/dd") 
                    ,Office_ID.ToString()
                    ,CreatedBy.ToString()
                    ,CreatedByIP.ToString()   
                    ,"2"
                  }, "dataset");
                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                    }
                    else
                    {
                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
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
                        else
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                        }
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
    public void UpdateCitizenAndItemMappingDetail_Demand(string Key, string CitizenItemMappingId, string Total_ItemQty, string CreatedBy, string CreatedByIP, string PageName, string Remark)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (CitizenItemMappingId == "" && Total_ItemQty == "" && CreatedBy == "" && CreatedByIP == "" && PageName == "" && Remark == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    DataSet ds1 = apiprocedure.ByProcedure("USP_Mst_AdvanceCard",
                               new string[] { "flag"
                                   , "CitizenItemMappingId"
                                   , "Total_ItemQty"
                                   , "CreatedBy"
                                   , "CreatedByIP"
                                   , "PageName"
                                   , "Remark" },
                               new string[] { "11"
                                   ,CitizenItemMappingId.ToString()
                                   ,Total_ItemQty.ToString()
                                   ,CreatedBy.ToString()
                                   ,CreatedByIP.ToString()     
                                   ,PageName.ToString()
                                   ,Remark.ToString()
                               }, "dataset");
                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                    }
                    else
                    {
                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
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
                        else
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                        }
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
    public void DeleteCitizenAndItemMappingDetail_Demand(string Key, string CitizenItemMappingId, string CreatedBy, string CreatedByIP, string PageName, string Remark)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (CitizenItemMappingId == "" && CreatedBy == "" && CreatedByIP == "" && PageName == "" && Remark == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    DataSet ds1 = apiprocedure.ByProcedure("USP_Mst_AdvanceCard",
                              new string[] { "flag"
                                  , "CitizenItemMappingId"
                                  , "CreatedBy"
                                  , "CreatedByIP"
                                  , "PageName"
                                  , "Remark" },
                              new string[] { "10"
                                  ,CitizenItemMappingId.ToString()
                                  ,CreatedBy.ToString()
                                  ,CreatedByIP.ToString()
                                  ,PageName,ToString()
                                  ,Remark.ToString()
                              }, "dataset");
                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                    }
                    else
                    {
                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
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
                        else
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                        }
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


    //---------------03-03-2020------05-35-PM----------
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetCitizenAndItemMappingDetail_Demand(string Key, string CreatedBy)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (CreatedBy == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    DataSet ds1 = apiprocedure.ByProcedure("USP_Mst_AdvanceCard",
                             new string[] { "flag", "CreatedBy" },
                            new string[] { "9", CreatedBy }, "dataset");
                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
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
    public void GetItemBy_Category_Demand(string Key, string Office_ID, string ItemCat_id)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("USP_Mst_SetItemCarriageMode",
                     new string[] { "flag", "Office_ID", "ItemCat_id" },
                       new string[] { "2", Office_ID.ToString(), ItemCat_id.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No data found" }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void InsertNewItem_DistributorLogin_ExistingDemand(string Key, string MilkOrProductDemandId, string Item_id, string ItemQty, string TotalQty)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (MilkOrProductDemandId == "" && Item_id == "" && ItemQty == "" && TotalQty == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    DataTable dtable = new DataTable();
                    DataRow dr;

                    dtable.Columns.Add("ItemName", typeof(string));
                    dtable.Columns.Add("ItemQty", typeof(string));
                    dtable.Columns.Add("AdvCard", typeof(int));
                    dtable.Columns.Add("TotalItemQty", typeof(int));
                    dtable.Columns.Add("Status", typeof(int));
                    dtable.Columns.Add("Msg", typeof(string));


                    dr = dtable.NewRow();
                    dr[0] = Item_id.ToString();
                    dr[1] = ItemQty.ToString();

                    dr[2] = TotalQty.ToString();
                    dtable.Rows.Add(dr);


                    DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkOrProductDemand",
                    new string[] { "flag"
                              ,"MilkOrProductDemandId"
                              ,"Item_id"
                              ,"ItemQty"
                              ,"TotalQty"
                              },
                   new string[] { "27"
                                       ,MilkOrProductDemandId.ToString()
                                       ,Item_id.ToString()
                                       ,ItemQty.ToString()
                                       ,TotalQty.ToString()
                                      }, "dataset");
                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
                    }
                    else
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                dr[4] = "1";
                                dr[5] = "Record Saved Successfully";
                                ds1.Clear();
                            }
                            else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                            {
                                dr[4] = "2";
                                dr[5] = "Record Already Exist";
                                ds1.Clear();
                            }
                            else
                            {
                                dr[4] = "0";
                                dr[5] = "Record Not Saved";
                                ds1.Clear();
                            }
                        }
                        //dt = ds1.Tables[0];                                
                        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                        Dictionary<string, object> row = null;
                        foreach (DataRow rs in dtable.Rows)
                        {
                            row = new Dictionary<string, object>();
                            foreach (DataColumn col in dtable.Columns)
                            {
                                row.Add(col.ColumnName, rs[col]);
                            }
                            rows.Add(row);
                        }
                        this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));
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
    public void GetUserType(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("SpAdminOffice",
                         new string[] { "flag" },
                        new string[] { "21" }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
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
	
	
	
 [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetDetailForPlaceOrderFromBooth_Demand(string Key, string Demand_Date, string Shift_id, string ItemCat_id, string CreatedBy, string Office_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {

                DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag"
                         ,"Demand_Date"
                         ,"Shift_id"
                         ,"ItemCat_id"
                         ,"CreatedBy"
                         ,"Office_ID" },
                       new string[] { "3"
                           , Convert.ToDateTime(Demand_Date, cult).ToString("yyyy/MM/dd")
                           , Shift_id.ToString()
                           , ItemCat_id.ToString()
                           , CreatedBy.ToString()
                           , Office_ID.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void InsertPlaceOrderFromBooth_Demand(string Key, string BoothId, string ItemCat_id, string Item_id, string ItemQty, string AdvCard, string TotalQty, string Demand_Date, string Shift_id, string Demand_Status, string UserTypeId, string Office_ID, string CreatedBy, string CreatedByIP, string Delivary_Date, string DelivaryShift_id, string RetailerTypeID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (BoothId == "" && ItemCat_id == "" && Item_id == "" && ItemQty == "" && AdvCard == "" && TotalQty == "" && Demand_Date == "" && Shift_id == "" && Demand_Status == "" && UserTypeId == "" && Office_ID == "" && CreatedBy == "" && CreatedByIP == "" && Delivary_Date == "" && DelivaryShift_id == "" && RetailerTypeID == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    if (Item_id.ToString().Split(',').Length != ItemQty.ToString().Split(',').Length && AdvCard.ToString().Split(',').Length != TotalQty.ToString().Split(',').Length)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Item Id Type pass " + Item_id.ToString().Split(',').Length + " argument(s) and Item Quantity pass " + ItemQty.ToString().Split(',').Length + " argument(s) and Adv Card pass " + AdvCard.ToString().Split(',').Length + " argument(s) and Total Qty pass" + TotalQty.ToString().Split(',').Length + ", Kindly pass equal both arrays arguments and value." }));
                    }
                    else
                    {
                        DataTable dtable = new DataTable();
                        DataRow dr;

                        dtable.Columns.Add("ItemName", typeof(string));
                        dtable.Columns.Add("ItemQty", typeof(string));
                        dtable.Columns.Add("AdvCard", typeof(int));
                        dtable.Columns.Add("TotalItemQty", typeof(int));
                        dtable.Columns.Add("Status", typeof(int));
                        dtable.Columns.Add("Msg", typeof(string));

                        int len = Item_id.ToString().Split(',').Length;

                        for (int i = 0; i < len; i++)
                        {
                            dr = dtable.NewRow();
                            dr[0] = Item_id.ToString().Split(',')[i];
                            dr[1] = ItemQty.ToString().Split(',')[i];
                            dr[2] = AdvCard.ToString().Split(',')[i];
                            dr[3] = TotalQty.ToString().Split(',')[i];
                            dtable.Rows.Add(dr);

                            DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkOrProductDemand",
                          new string[] { "flag"
                                 ,"BoothId"
                                 ,"ItemCat_id"
                                 ,"Item_id"
                                 ,"ItemQty"
                                 ,"AdvCard"
                                 ,"TotalQty"
                                 ,"Demand_Date"
                                 ,"Shift_id"
                                 ,"Demand_Status"
                                 ,"UserTypeId"
                                 ,"Office_ID"
                                 ,"CreatedBy"
                                 ,"CreatedByIP"
                                 ,"PlatformType"
                                 ,"Delivary_Date"
                                 ,"DelivaryShift_id"
                                 ,"RetailerTypeID" },
                         new string[] { "4"
                               ,BoothId.ToString()
                               ,ItemCat_id.ToString()
                               ,dr[0].ToString()
                               ,dr[1].ToString()
                               ,dr[2].ToString()
                               ,dr[3].ToString()
                               ,Convert.ToDateTime(Demand_Date, cult).ToString("yyyy/MM/dd")
                               ,Shift_id.ToString()
                               ,Demand_Status.ToString()
                               ,UserTypeId.ToString()
                               ,Office_ID.ToString()
                               ,CreatedBy.ToString()
                               ,CreatedByIP.ToString()
                               ,"2"
                               ,Convert.ToDateTime(Delivary_Date, cult).ToString("yyyy/MM/dd")
                               ,DelivaryShift_id.ToString()
                               ,RetailerTypeID.ToString() }, "dataset");

                            if (ds1.Tables[0].Rows.Count == 0)
                            {
                                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
                            }
                            else
                            {
                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                    {
                                        dr[4] = "1";
                                        dr[5] = "Record Saved Successfully";
                                        ds1.Clear();
                                    }
                                    else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                                    {
                                        dr[4] = "2";
                                        dr[5] = "Record Already Exist";
                                        ds1.Clear();
                                    }
                                    else
                                    {
                                        dr[4] = "0";
                                        dr[5] = "Record Not Saved";
                                        ds1.Clear();
                                    }
                                }
                                //dt = ds1.Tables[0];                                
                                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                                Dictionary<string, object> row = null;
                                foreach (DataRow rs in dtable.Rows)
                                {
                                    row = new Dictionary<string, object>();
                                    foreach (DataColumn col in dtable.Columns)
                                    {
                                        row.Add(col.ColumnName, rs[col]);
                                    }
                                    rows.Add(row);
                                }
                                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));
                            }
                        }
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
    public void EditOrderDetailAfterPlaceOrderFromBooth_Demand(string Key, string MilkOrProductDemandChildId, string ItemQty, string TotalQty, string CreatedBy, string CreatedByIP, string PageName, string Remark)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {

                DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkOrProductDemand",
                                 new string[] { "flag"
                                     , "MilkOrProductDemandChildId"
                                     , "ItemQty"
                                     , "TotalQty"
                                     , "CreatedBy"
                                     , "CreatedByIP"
                                     , "PageName"
                                     , "Remark" },
                                 new string[] { "5"
                                    , MilkOrProductDemandChildId.ToString()
                                    , ItemQty.ToString()
                                    , TotalQty.ToString()
                                    , CreatedBy.ToString()
                                    , CreatedByIP.ToString()
                                    , PageName.ToString()
                                    , Remark.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetDateAndShiftWiseReport_Demand(string Key, string FromDate, string ToDate, string Shift_id, string BoothId)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkOrProductDemand",
                    new string[] { "flag"
                        , "FromDate"
                        , "ToDate"
                        , "Shift_id"
                        , "BoothId" },
                      new string[] { "6"
                          ,Convert.ToDateTime(FromDate, cult).ToString("yyyy/MM/dd")
                          ,Convert.ToDateTime(ToDate, cult).ToString("yyyy/MM/dd")
                          ,Shift_id
                          ,BoothId }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ViewDateAndShiftWisePlacedOrder_Demand(string Key, string DemandDate, string Shift_id, string Office_ID, string BoothId)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkOrProductDemand",
                new string[] { "flag"
                    , "Demand_Date"
                    , "Shift_id"
                    , "Office_ID"
                    , "BoothId" },
                  new string[] { "10"
                      , Convert.ToDateTime(DemandDate, cult).ToString("yyyy/MM/dd")
                      , Shift_id.ToString()
                      , Office_ID.ToString()
                      , BoothId.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
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
	
	#region -- 05-03-2020 --

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void InsertProducerRegistrationDetail_MilkCollection(string Key, string DCSId, string ProducerName, string FatherHusbandName, string DOB, string CategoryId, string Gender, string Mobile, string FamilyMembers, string BhumiStithi, string FarmerType, string Education, string Email, string CattleNo, string CowNo, string Cowbreed, string BuffelowNo, string BuffBreed, string MilkProduce, string BankId, string BankBranch, string IFSC, string AccountNo, string AadharPath, string PassbookPath, string CreatedBy, string CardNo, string AadharNo, string Address, string UserTypeId)
    {
        string GUID = Guid.NewGuid().ToString();
        var AadharPathWithName = "";
        var AadharFullPathWithName = "";
        var PassbookPathWithName = "";
        var PassbookFullPathWithName = "";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (DCSId == "" && ProducerName == "" && FatherHusbandName == "" && DOB == "" && CategoryId == "" && Gender == "" && Mobile == "" && FamilyMembers == "" && BhumiStithi == "" && FarmerType == "" && Education == "" && Email == "" && CattleNo == "" && CowNo == "" && Cowbreed == "" && BuffelowNo == "" && BuffBreed == "" && MilkProduce == "" && BankId == "" && BankBranch == "" && IFSC == "" && AccountNo == "" && AadharPath == "" && PassbookPath == "" && CreatedBy == "" && CardNo == "" && AadharNo == "" && Address == "" && UserTypeId == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    string AadharFullPath = "";
                    string PassbookFullPath = "";
                    if (AadharPath != "")
                    {
                        AadharPathWithName = "../FileUpload/" + GUID + ".jpg";
                        AadharFullPathWithName = Server.MapPath(@"~/FileUpload/" + GUID);
                        AadharFullPath = AadharFullPathWithName + ".jpg";
                        byte[] imgByteArray = Convert.FromBase64String(AadharPath);
                        File.WriteAllBytes(AadharFullPath, imgByteArray);
                    }
                    if (PassbookPath != "")
                    {
                        PassbookPathWithName = "../FileUpload/" + GUID + ".jpg";
                        PassbookFullPathWithName = Server.MapPath(@"~/FileUpload/" + GUID);
                        PassbookFullPath = PassbookFullPathWithName + ".jpg";
                        byte[] imgByteArray = Convert.FromBase64String(PassbookPath);
                        File.WriteAllBytes(PassbookFullPath, imgByteArray);
                    }
                    DataSet ds1 = apiprocedure.ByProcedure("SpProducerMaster",
                        new string[] { "flag"
                        ,"DCSId"
                        ,"ProducerName"
                        ,"FatherHusbandName"
                        ,"DOB"
                        ,"CategoryId"
                        ,"Gender"
                        ,"Mobile"
                        ,"FamilyMembers"
                        ,"BhumiStithi"
                        ,"FarmerType"
                        ,"Education"
                        ,"Email"
                        ,"CattleNo"
                        ,"CowNo"
                        ,"Cowbreed"
                        ,"BuffelowNo"
                        ,"BuffBreed"
                        ,"MilkProduce"
                        ,"BankId"
                        ,"BankBranch"
                        ,"IFSC"
                        ,"AccountNo"
                        ,"AadharPath"
                        ,"PassbookPath"
                        ,"CreatedBy"
                        ,"CardNo"
                        ,"AadharNo"
                        ,"Address"
                        ,"UserTypeId" },
                          new string[] { "1"
                        ,DCSId.ToString()
                        ,ProducerName.ToString()
                        ,FatherHusbandName.ToString()
                        ,Convert.ToDateTime(DOB.Trim(),cult).ToString("yyyy/MM/dd")
                        ,CategoryId.ToString()
                        ,Gender.ToString()
                        ,Mobile.ToString()
                        ,FamilyMembers.ToString()
                        ,BhumiStithi.ToString()
                        ,FarmerType.ToString()
                        ,Education.ToString()
                        ,Email.ToString()
                        ,CattleNo.ToString()
                        ,CowNo.ToString()
                        ,Cowbreed.ToString()
                        ,BuffelowNo.ToString()
                        ,BuffBreed.ToString()
                        ,MilkProduce.ToString()
                        ,BankId.ToString()
                        ,BankBranch.ToString()
                        ,IFSC.ToString()
                        ,AccountNo.ToString()
                        ,AadharPath.ToString()
                        ,PassbookPath.ToString()
                        ,CreatedBy.ToString()
                        ,CardNo.ToString()
                        ,AadharNo.ToString()
                        ,Address.ToString()
                        ,UserTypeId.ToString() }, "dataset");
                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
                    }
                    else
                    {
                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
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
                        else
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["Msg"].ToString() }));
                        }
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
    public void GetBankNameDetail(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("sp_tblPUBankMaster",
                            new string[] { "flag" },
                            new string[] { "1" }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetDetailDailyMilkCollection(string Key, string Date, string Shift, string Office_Id)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("sp_Mst_DailyMilkCollection",
                             new string[] { "flag"
                                 , "FDT"
                                 , "V_Shift"
                                 , "Office_Id" },
                             new string[] { "6"
                                 , Convert.ToDateTime(Date.Trim(),cult).ToString("yyyy/MM/dd")
                                 , Shift.ToString()
                                 , Office_Id.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
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

[WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetProducePassBookAtDCSLevel(string Key, string fromDate, string ToDate,string V_Shift, string I_Producer_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("sp_Mst_DailyMilkCollection",
                            new string[] { "flag"
                                ,"fromDate"
                                ,"ToDate"
                                ,"V_Shift"
                                ,"I_Producer_ID" },
                            new string[] { "13"
                                ,Convert.ToDateTime(fromDate.Trim(), cult).ToString("yyyy/MM/dd")
                                ,Convert.ToDateTime(ToDate.Trim(), cult).ToString("yyyy/MM/dd")
                                ,V_Shift.ToString()
                                ,I_Producer_ID.ToString()
                            }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetDetailTotalMilkQuantityOfMilkToday(string Key, string Date, string Shift, string Office_Id)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("sp_Mst_DailyMilkCollection",
                             new string[] { "flag"
                                 , "FDT"
                                 , "V_Shift"
                                 , "Office_Id" },
                             new string[] { "6"
                                 , Convert.ToDateTime(Date.Trim(),cult).ToString("yyyy/MM/dd")
                                 , Shift.ToString()
                                 , Office_Id.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
                }
                else
                {
                    dt = ds1.Tables[1];
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
	
	[WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetItemDetailByDemandID_Demand(string Key, string MilkOrProductDemandId)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {

                DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkOrProductDemand",
                  new string[] { "flag", "MilkOrProductDemandId" },
                    new string[] { "9", MilkOrProductDemandId, }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
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
	
	[WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetRetailerDetailByDistributorID_Demand(string Key, string DistributorID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {

                DataSet ds1 = apiprocedure.ByProcedure("USP_Mst_DistributorParlourMapping",
                     new string[] { "flag", "DistributorId" },
                       new string[] { "6", DistributorID.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
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
	
	
	//-------------06-03-2020----11_27---------------

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ViewDateAndShiftWiseParloursDemandByDistributorLogin_Demand(string Key, string Demand_Date, string Shift_id, string Office_ID, string DistributorId)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkOrProductDemand",
                new string[] { "flag"
                    ,"Demand_Date"
                    ,"Shift_id"
                    ,"Office_ID"
                    ,"DistributorId" },
                  new string[] { "8"
                      ,Convert.ToDateTime(Demand_Date.Trim(), cult).ToString("yyyy/MM/dd")
                      ,Shift_id.ToString()
                      ,Office_ID.ToString()
                      ,DistributorId.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetDetailByDemantIDOnDistributorLogin_Demand(string Key, string MilkOrProductDemandId)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkOrProductDemand",
                  new string[] { "flag", "MilkOrProductDemandId" },
                    new string[] { "9", MilkOrProductDemandId.ToString(), }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void EditPlacedOrderListOnDistributorLogin_Demand(string Key, string MilkOrProductDemandChildId, string ItemQty, string TotalQty, string CreatedBy, string CreatedByIP, string PageName, string Remark)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkOrProductDemand",
                                     new string[] { "flag"
                                         ,"MilkOrProductDemandChildId"
                                         ,"ItemQty"
                                         ,"TotalQty"
                                         ,"CreatedBy"
                                         ,"CreatedByIP"
                                         ,"PageName", "Remark" },
                                     new string[] { "5"
                                         ,MilkOrProductDemandChildId.ToString()
                                         ,ItemQty.ToString()
                                         ,TotalQty.ToString()
                                         ,CreatedBy.ToString()
                                         ,CreatedByIP.ToString()
                                         ,PageName.ToString()
                                         ,Remark.ToString() }, "TableSave");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetDetailForPlaceOrderByDistributorLogin_Demand(string Key, string Demand_Date, string Shift_id, string ItemCat_id, string RetailerID, string Office_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkOrProductDemand",
                  new string[] { "flag"
                              , "Demand_Date"
                              , "Shift_id"
                              , "ItemCat_id"
                              , "CreatedBy"
                              , "Office_ID" },
                    new string[] { "3"
                        ,Convert.ToDateTime(Demand_Date.Trim(), cult).ToString("yyyy/MM/dd")
                        ,Shift_id.ToString()
                        ,ItemCat_id.ToString()
                        ,RetailerID.ToString()
                        ,Office_ID.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
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

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void InsertPlaceOrderFromDistributorLogin_Demand(string Key, string BoothId, string ItemCat_id, string Item_id, string ItemQty, string AdvCard, string TotalQty, string Demand_Date, string Shift_id, string Demand_Status, string UserTypeId, string Office_ID, string CreatedBy, string CreatedByIP, string Delivary_Date, string DelivaryShift_id, string RetailerTypeID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (BoothId == "" && ItemCat_id == "" && Item_id == "" && ItemQty == "" && AdvCard == "" && TotalQty == "" && Demand_Date == "" && Shift_id == "" && Demand_Status == "" && UserTypeId == "" && Office_ID == "" && CreatedBy == "" && CreatedByIP == "" && Delivary_Date == "" && DelivaryShift_id == "" && RetailerTypeID == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    if (Item_id.ToString().Split(',').Length != ItemQty.ToString().Split(',').Length && AdvCard.ToString().Split(',').Length != TotalQty.ToString().Split(',').Length)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Item Id Type pass " + Item_id.ToString().Split(',').Length + " argument(s) and Item Quantity pass " + ItemQty.ToString().Split(',').Length + " argument(s) and Adv Card pass " + AdvCard.ToString().Split(',').Length + " argument(s) and Total Qty pass" + TotalQty.ToString().Split(',').Length + ", Kindly pass equal both arrays arguments and value." }));
                    }
                    else
                    {
                        DataTable dtable = new DataTable();
                        DataRow dr;

                        dtable.Columns.Add("ItemName", typeof(string));
                        dtable.Columns.Add("ItemQty", typeof(string));
                        dtable.Columns.Add("AdvCard", typeof(int));
                        dtable.Columns.Add("TotalItemQty", typeof(int));
                        dtable.Columns.Add("Status", typeof(int));
                        dtable.Columns.Add("Msg", typeof(string));

                        int len = Item_id.ToString().Split(',').Length;

                        for (int i = 0; i < len; i++)
                        {
                            dr = dtable.NewRow();
                            dr[0] = Item_id.ToString().Split(',')[i];
                            dr[1] = ItemQty.ToString().Split(',')[i];
                            dr[2] = AdvCard.ToString().Split(',')[i];
                            dr[3] = TotalQty.ToString().Split(',')[i];
                            dtable.Rows.Add(dr);


                            DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkOrProductDemand",
                          new string[] { "flag"
                              ,"BoothId"
                              ,"ItemCat_id"
                              ,"Item_id"
                              ,"ItemQty"
                              ,"AdvCard"
                              ,"TotalQty"
                              ,"Demand_Date"
                              ,"Shift_id"
                              ,"Demand_Status"
                              ,"UserTypeId"
                              ,"Office_ID"
                              ,"CreatedBy"
                              ,"CreatedByIP"
                              ,"PlatformType"
                              ,"Delivary_Date"
                              ,"DelivaryShift_id"
                              ,"RetailerTypeID" },
                         new string[] { "4"
                                       ,BoothId.ToString()
                                       ,ItemCat_id.ToString()
                                       ,dr[0].ToString()
                                       ,dr[1].ToString()
                                       ,dr[2].ToString()
                                       ,dr[3].ToString()
                                       ,Convert.ToDateTime(Demand_Date, cult).ToString("yyyy/MM/dd")
                                       ,Shift_id.ToString()
                                       ,Demand_Status.ToString()
                                       ,UserTypeId.ToString()
                                       ,Office_ID.ToString()
                                       ,CreatedBy.ToString()
                                       ,CreatedByIP.ToString()
									   ,"2"
                                       ,Convert.ToDateTime(Delivary_Date, cult).ToString("yyyy/MM/dd")
                                       ,DelivaryShift_id.ToString()
                                       ,RetailerTypeID.ToString()                        
                         }, "dataset");
                            if (ds1.Tables[0].Rows.Count == 0)
                            {
                                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
                            }
                            else
                            {
                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                    {
                                        dr[4] = "1";
                                        dr[5] = "Record Saved Successfully";
                                        ds1.Clear();
                                    }
                                    else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                                    {
                                        dr[4] = "2";
                                        dr[5] = "Record Already Exist";
                                        ds1.Clear();
                                    }
                                    else
                                    {
                                        dr[4] = "0";
                                        dr[5] = "Record Not Saved";
                                        ds1.Clear();
                                    }
                                }
                                //dt = ds1.Tables[0];                                
                                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                                Dictionary<string, object> row = null;
                                foreach (DataRow rs in dtable.Rows)
                                {
                                    row = new Dictionary<string, object>();
                                    foreach (DataColumn col in dtable.Columns)
                                    {
                                        row.Add(col.ColumnName, rs[col]);
                                    }
                                    rows.Add(row);
                                }
                                this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));
                            }
                        }
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
    public void GetDetailForPlaceOrderByDistributorLoginReport_Demand(string Key, string FromDate, string ToDate, string Shift_id, string BoothId, string DistributorId)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkOrProductDemand",
                    new string[] { "flag"
                        , "FromDate"
                        , "ToDate"
                        , "Shift_id"
                        , "BoothId"
                        , "DistributorId" },
                      new string[] { "11"
                          ,Convert.ToDateTime(FromDate.Trim(), cult).ToString("yyyy/MM/dd")
                          ,Convert.ToDateTime(ToDate.Trim(), cult).ToString("yyyy/MM/dd")
                          ,Shift_id.ToString()
                          ,BoothId.ToString()
                          ,DistributorId.ToString()
                      }, "dataset");
                if (ds1.Tables[0].Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
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

     [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetSNFByCLRAndFAT_DCS(string Key, string FAT, string CLR, string OfficeId)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (FAT == "" || CLR == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to input field of FAT or CLR" }));
                }
                else
                {
                    decimal snf = 0, clr = 0, fat = 1, RatePerLtr = 0;
                    try
                    {

                        DataSet ds1 = new DataSet();

                        ds1 = apiprocedure.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                                       new string[] { "flag", "Fat", "CLR", "OfficeId" },
                                       new string[] { "7", FAT, CLR, OfficeId }, "dataset");

                        if (ds1.Tables[0].Rows.Count != 0)
                        {
                            RatePerLtr = Convert.ToDecimal(ds1.Tables[0].Rows[0]["Rate_Per_Ltr"].ToString());
                        }



                        clr = Convert.ToDecimal(CLR);
                        fat = Convert.ToDecimal(FAT);
                        RatePerLtr = Convert.ToDecimal(RatePerLtr);
                        //SNF = (CLR / 4) + (FAT * 0.2) + 0.7 -- Formula Before
                        //SNF = (CLR / 4) + (0.25 * FAT) + 0.44 -- Formula After Mail dt: 27 Jan 2020, 16:26

                        snf = ((clr / 4) + (Convert.ToDecimal(0.2) * fat) + Convert.ToDecimal(0.7));

                        DataRow dr;
                        dt.Columns.Add(new DataColumn("SNF", typeof(decimal)));
                        dt.Columns.Add(new DataColumn("Rate_Per_Ltr", typeof(decimal)));
                        dr = dt.NewRow();
                        dr[0] = Math.Round(snf, 2).ToString();
                        dr[1] = Math.Round(RatePerLtr, 2).ToString();
                        dt.Rows.Add(dr);

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
                    catch (Exception ex)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
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
    public void DeleteChallanbyEntryID(string Key, string EntryID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (EntryID == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill Entry ID" }));
                }
                else
                {
                    DataSet ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                        new string[] { "Flag", "I_EntryID" },
                        new string[] { "24", EntryID }, "dataset");

                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Data Found." }));
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
	
	 //-----------11-03-2020------11-16-AM-------

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetPreviousOrderDetailByBoothLogin_Demand(string Key, string Shift_id, string ItemCat_id, string BoothId, string Office_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (Shift_id == "" && ItemCat_id == "" && BoothId == "" && Office_ID == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill Entry ID" }));
                }
                else
                {
                    DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag"
                         ,"Shift_id"
                         ,"ItemCat_id"
                         ,"BoothId"
                         ,"Office_ID" },
                       new string[] { "15"
                           ,Shift_id.ToString()
                           ,ItemCat_id.ToString()
                           ,BoothId.ToString()
                           ,Office_ID.ToString() }, "dataset");

                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Data Found." }));
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
    public void GetPreviousOrderDetailByDistributorLogin_Demand(string Key, string Shift_id, string ItemCat_id, string BoothId, string Office_ID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (Shift_id == "" && ItemCat_id == "" && BoothId == "" && Office_ID == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill Entry ID" }));
                }
                else
                {
                    DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag"
                         ,"Shift_id"
                         ,"ItemCat_id"
                         ,"BoothId"
                         ,"Office_ID" },
                       new string[] { "15"
                           ,Shift_id.ToString()
                           ,ItemCat_id.ToString()
                           ,BoothId.ToString()
                           ,Office_ID.ToString() }, "dataset");

                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Data Found." }));
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
	
    #endregion
	
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetCLRBySNFAndFAT(string Key, string FAT, string SNF, string OfficeId)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (FAT == "" || SNF == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to input field of FAT or SNF" }));
                }
                else
                {
                    decimal fat = 1 ,snf = 0, clr = 0, RatePerLtr = 0;
                    try
                    { 

                        fat = Convert.ToDecimal(FAT);
                        snf = Convert.ToDecimal(SNF);

                        MilkCalculationClass Obj_MC = new MilkCalculationClass();

                        clr = Obj_MC.GetCLR_DCS(fat, snf);

                        DataSet ds1 = new DataSet();

                        ds1 = apiprocedure.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                                       new string[] { "flag", "Fat", "CLR", "OfficeId" },
                                       new string[] { "7", FAT, clr.ToString(), OfficeId }, "dataset");

                        if (ds1.Tables[0].Rows.Count != 0)
                        {
                            RatePerLtr = Convert.ToDecimal(ds1.Tables[0].Rows[0]["Rate_Per_Ltr"].ToString());
                        }
                         
                        DataRow dr;
                        dt.Columns.Add(new DataColumn("CLR", typeof(decimal)));
                        dt.Columns.Add(new DataColumn("Rate_Per_Ltr", typeof(decimal)));
                        dr = dt.NewRow();
                        dr[0] = Math.Round(clr, 2).ToString();
                        dr[1] = Math.Round(RatePerLtr, 2).ToString();
                        dt.Rows.Add(dr);

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
                    catch (Exception ex)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
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
	
	public int SendMsgFormMBC(string MobileNo, string SMSText)
    {
        int status = 0;
        try
        {
            //Your authentication key
            string authKey = "3597C1493C124F";
            //Sender ID
            string senderId = "MPSCDF";

            string link = "http://mysms.mssinfotech.com/app/smsapi/index.php?key=" + authKey + "&type=unicode&routeid=2&contacts=" + MobileNo + "&senderid=" + senderId + "&msg=" + Server.UrlEncode(SMSText);
            HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Stream stream = response.GetResponseStream();

            //End Sending OTP SMS
            status = 1;
        }
        catch (Exception)
        {
            status = 0;
        }
        return status;
    }
	[WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void DailyMilkCollectionLocalSale_ItemType(string Key, string ItemCat_id)
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

                ds1 = apiprocedure.ByProcedure("Sp_MstLocalSale",
                               new string[] { "flag", "ItemCat_id" },
                               new string[] { "9", ItemCat_id }, "dataset");

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
	 //Producer Payment Invoice done by Mohini on 5feb 2021
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ProducerPaymentInvoice(string Key, string FromDate, string ToDate, string OfficeId)
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

                ds1 = apiprocedure.ByProcedure("USP_Trn_ProducerPaymentDetails",
                       new string[] { "flag", "PaymentFromDt", "PaymentToDt", "Office_ID" },
                       new string[] { "14", Convert.ToDateTime(FromDate, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(ToDate, cult).ToString("yyyy/MM/dd"), OfficeId }, "dataset");

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


    //Producer Payment Invoice_OffICE Info done by Mohini on 5feb 2021
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ProducerPaymentInvoice_OfficeInfo(string Key, string FromDate, string ToDate, string OfficeId)
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

                ds1 = apiprocedure.ByProcedure("USP_Trn_ProducerPaymentDetails",
                       new string[] { "flag", "PaymentFromDt", "PaymentToDt", "Office_ID" },
                       new string[] { "15", Convert.ToDateTime(FromDate, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(ToDate, cult).ToString("yyyy/MM/dd"), OfficeId }, "dataset");

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

    //GetProducerDetails done by Mohini on 5feb 2021
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetProducerDetails(string Key, string ProducerId)
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

                ds1 = apiprocedure.ByProcedure("SpProducerMaster",
                       new string[] { "flag", "ProducerId"},
                       new string[] { "16", ProducerId }, "dataset");

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


    //Update Producer Details done by Mohini on 5feb 2021
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void UpdateProducerRegistrationDetail_MilkCollection(string Key, string DCSId, string ProducerName, string FatherHusbandName, string DOB, string CategoryId, string Gender, string Mobile, string FamilyMembers, string BhumiStithi, string FarmerType, string Education, string Email, string CattleNo, string CowNo, string Cowbreed, string BuffelowNo, string BuffBreed, string MilkProduce, string BankId, string BankBranch, string IFSC, string AccountNo, string AadharPath, string PassbookPath, string CreatedBy, string CardNo, string AadharNo, string Address, string ProducerId)
    {
        string GUID = Guid.NewGuid().ToString();
        var AadharPathWithName = "";
        var AadharFullPathWithName = "";
        var PassbookPathWithName = "";
        var PassbookFullPathWithName = "";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (DCSId == "" && ProducerName == "" && FatherHusbandName == "" && DOB == "" && CategoryId == "" && Gender == "" && Mobile == "" && FamilyMembers == "" && BhumiStithi == "" && FarmerType == "" && Education == "" && Email == "" && CattleNo == "" && CowNo == "" && Cowbreed == "" && BuffelowNo == "" && BuffBreed == "" && MilkProduce == "" && BankId == "" && BankBranch == "" && IFSC == "" && AccountNo == "" && AadharPath == "" && PassbookPath == "" && CreatedBy == "" && CardNo == "" && AadharNo == "" && Address == "" && ProducerId == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    string AadharFullPath = "";
                    string PassbookFullPath = "";
                    if (AadharPath != "")
                    {
                        AadharPathWithName = "../FileUpload/" + GUID + ".jpg";
                        AadharFullPathWithName = Server.MapPath(@"~/FileUpload/" + GUID);
                        AadharFullPath = AadharFullPathWithName + ".jpg";
                        byte[] imgByteArray = Convert.FromBase64String(AadharPath);
                        File.WriteAllBytes(AadharFullPath, imgByteArray);
                    }
                    if (PassbookPath != "")
                    {
                        PassbookPathWithName = "../FileUpload/" + GUID + ".jpg";
                        PassbookFullPathWithName = Server.MapPath(@"~/FileUpload/" + GUID);
                        PassbookFullPath = PassbookFullPathWithName + ".jpg";
                        byte[] imgByteArray = Convert.FromBase64String(PassbookPath);
                        File.WriteAllBytes(PassbookFullPath, imgByteArray);
                    }
                    DataSet ds1 = apiprocedure.ByProcedure("SpProducerMaster",
                        new string[] { "flag"
                        ,"DCSId"
                        ,"ProducerName"
                        ,"FatherHusbandName"
                        ,"DOB"
                        ,"CategoryId"
                        ,"Gender"
                        ,"Mobile"
                        ,"FamilyMembers"
                        ,"BhumiStithi"
                        ,"FarmerType"
                        ,"Education"
                        ,"Email"
                        ,"CattleNo"
                        ,"CowNo"
                        ,"Cowbreed"
                        ,"BuffelowNo"
                        ,"BuffBreed"
                        ,"MilkProduce"
                        ,"BankId"
                        ,"BankBranch"
                        ,"IFSC"
                        ,"AccountNo"
                        ,"AadharPath"
                        ,"PassbookPath"
                        ,"CreatedBy"
                        ,"CardNo"
                        ,"AadharNo"
                        ,"Address"
                        ,"ProducerId" },
                          new string[] { "17"
                        ,DCSId.ToString()
                        ,ProducerName.ToString()
                        ,FatherHusbandName.ToString()
                        ,Convert.ToDateTime(DOB.Trim(),cult).ToString("yyyy/MM/dd")
                        ,CategoryId.ToString()
                        ,Gender.ToString()
                        ,Mobile.ToString()
                        ,FamilyMembers.ToString()
                        ,BhumiStithi.ToString()
                        ,FarmerType.ToString()
                        ,Education.ToString()
                        ,Email.ToString()
                        ,CattleNo.ToString()
                        ,CowNo.ToString()
                        ,Cowbreed.ToString()
                        ,BuffelowNo.ToString()
                        ,BuffBreed.ToString()
                        ,MilkProduce.ToString()
                        ,BankId.ToString()
                        ,BankBranch.ToString()
                        ,IFSC.ToString()
                        ,AccountNo.ToString()
                        ,AadharPath.ToString()
                        ,PassbookPath.ToString()
                        ,CreatedBy.ToString()
                        ,CardNo.ToString()
                        ,AadharNo.ToString()
                        ,Address.ToString()
                        ,ProducerId }, "dataset");
                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found" }));
                    }
                    else
                    {
                        if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
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
                        else
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["Msg"].ToString() }));
                        }
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
	//Filled Tanker Functionalty in MCMS Web Services


    //Bind Filled Tanker Reference No at CC
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MCMS_FilledTankerReferenceNoListForCC(string Key, int I_OfficeID)
    {
        DataSet ds = new DataSet();
        DataTable dt1 = new DataTable();
        APIProcedure apiprocedure = new APIProcedure();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string saltkey = apiprocedure.GenerateSaltKey();
                DataSet ds1 = new DataSet();

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                               new string[] { "flag", "I_OfficeID", "V_EntryType" },
                               new string[] { "34", I_OfficeID.ToString(), "Out" }, "dataset");
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    DataRow dr;
                    dt1.Columns.Add(new DataColumn("C_ReferenceNo", typeof(string)));
                    dt1.Columns.Add(new DataColumn("BI_MilkInOutRefID", typeof(string)));

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        dr = dt1.NewRow();
                        dr[0] = ds1.Tables[0].Rows[i]["C_ReferenceNo"].ToString();
                        dr[1] = ds1.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString();
                        dt1.Rows.Add(dr);
                    }
                }



                if (dt1.Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {


                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt1.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt1.Columns)
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
            dt1.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }


    //Bind Challan No  at CC
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MCMS_FilledTankerChallanNoForCCByRefID(string Key, int BI_MilkInOutRefID, int I_OfficeID)
    {
        DataSet ds = new DataSet();
        DataTable dt1 = new DataTable();
        APIProcedure apiprocedure = new APIProcedure();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string saltkey = apiprocedure.GenerateSaltKey();
                DataSet ds1 = new DataSet();

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                               new string[] { "flag", "BI_MilkInOutRefID", "I_OfficeID", "V_EntryType", },
                               new string[] { "33",BI_MilkInOutRefID.ToString(), I_OfficeID.ToString(), "out" }, "dataset");
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    DataRow dr;
                    dt1.Columns.Add(new DataColumn("Challanno", typeof(string)));
                    dt1.Columns.Add(new DataColumn("I_EntryID", typeof(string)));

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        dr = dt1.NewRow();
                        dr[0] = ds1.Tables[0].Rows[i]["Challanno"].ToString();
                        dr[1] = ds1.Tables[0].Rows[i]["I_EntryID"].ToString();
                        dt1.Rows.Add(dr);
                    }
                }



                if (dt1.Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {


                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt1.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt1.Columns)
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
            dt1.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }


    ////Bind Seal Location By ReferenceCode
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MCMS_SealLocationByReferenceCode(string Key, string V_ReferenceCode)
    {
        DataSet ds = new DataSet();
        DataTable dt1 = new DataTable();
        APIProcedure apiprocedure = new APIProcedure();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string saltkey = apiprocedure.GenerateSaltKey();
                DataSet ds1 = new DataSet();

                ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                               new string[] { "flag", "V_ReferenceCode" },
                               new string[] { "20", V_ReferenceCode }, "dataset");
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    DataRow dr;
                    dt1.Columns.Add(new DataColumn("SealLocation", typeof(string)));
                    dt1.Columns.Add(new DataColumn("Value", typeof(string)));

                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        dr = dt1.NewRow();
                        dr[0] = ds1.Tables[0].Rows[i]["SealLocation"].ToString();
                        dr[1] = ds1.Tables[0].Rows[i]["Value"].ToString();
                        dt1.Rows.Add(dr);
                    }
                }



                if (dt1.Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {


                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt1.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt1.Columns)
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
            dt1.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }


    ////Bind Other Details By Challan No
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void MCMS_OtherDetailsByChallanNoAtCC(string Key,int BI_MilkInOutRefID, int I_EntryID)
    {
        DataSet ds = new DataSet();
        DataTable dt1 = new DataTable();
        APIProcedure apiprocedure = new APIProcedure();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string saltkey = apiprocedure.GenerateSaltKey();
                DataSet ds1 = new DataSet();

                ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                               new string[] { "flag", "BI_MilkInOutRefID", "I_EntryID" },
                               new string[] { "36", BI_MilkInOutRefID.ToString(), I_EntryID.ToString() }, "dataset");
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    DataRow dr;
                    dt1.Columns.Add(new DataColumn("V_DriverName", typeof(string)));
                    dt1.Columns.Add(new DataColumn("V_DriverMobileNo", typeof(string)));
                    dt1.Columns.Add(new DataColumn("V_VehicleNo", typeof(string)));
                    dt1.Columns.Add(new DataColumn("V_TankerType", typeof(string)));
                    dt1.Columns.Add(new DataColumn("V_RepresentativeName", typeof(string)));
                    dt1.Columns.Add(new DataColumn("V_RepresentativeMobileNo", typeof(string)));
					 dt1.Columns.Add(new DataColumn("D_VehicleCapacity", typeof(string)));
					 dt1.Columns.Add(new DataColumn("Frontchambercapacity", typeof(string)));
					 dt1.Columns.Add(new DataColumn("Rearchambercapacity", typeof(string)));
					
                                
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        dr = dt1.NewRow();
                        dr[0] = ds1.Tables[0].Rows[i]["V_DriverName"].ToString();
                        dr[1] = ds1.Tables[0].Rows[i]["V_DriverMobileNo"].ToString();
                        dr[2] = ds1.Tables[0].Rows[i]["V_VehicleNo"].ToString();
                        dr[3] = ds1.Tables[0].Rows[i]["V_TankerType"].ToString();
                        dr[4] = ds1.Tables[0].Rows[i]["V_RepresentativeName"].ToString();
                        dr[5] = ds1.Tables[0].Rows[i]["V_RepresentativeMobileNo"].ToString();
						 dr[6] = ds1.Tables[0].Rows[i]["D_VehicleCapacity"].ToString();
						 dr[7] = ds1.Tables[0].Rows[i]["Frontchambercapacity"].ToString();
						 dr[8] = ds1.Tables[0].Rows[i]["Rearchambercapacity"].ToString();
                        dt1.Rows.Add(dr);
                    }
                }



                if (dt1.Rows.Count == 0)
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Record Found." }));
                }
                else
                {


                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row = null;
                    foreach (DataRow rs in dt1.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dt1.Columns)
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
            dt1.Clear();
            ds.Clear();
        }
        else
        {
            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        }
    }
	//For Filled Tanker
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetMilkQuantityByDateAndOfficeIDForFT(string Key, string OfficeID, string DT_Date)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (DT_Date != "")
                {
                    DataSet ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                    new string[] { "Flag", "Office_ID", "DT_Date" },
                    new string[] { "45", OfficeID, Convert.ToDateTime(DT_Date, cult).ToString("yyyy/MM/dd") }, "dataset");

                    if (ds1.Tables[0].Rows.Count == 0)
                    {
                        this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "No Data Found." }));
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
                else
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Date field should not blank." }));
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
    public void InsertMilkQualityDetailsByEntryIDForFilledTanker(string Key,string BI_MilkInOutRefID, string EntryID, string MilkQuantity, string SealLocation, string FAT, string SNF, string CLR, string Temp, string Acidity, string COB, string MBRT, string Alcohol, string MilkQuality)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                if (BI_MilkInOutRefID == "" && EntryID == "" && MilkQuantity == "" && SealLocation == "" && FAT == "" && SNF == "" && CLR == "" && Temp == "" && Acidity == "" && COB == "" && MBRT == "" && Alcohol == "" && MilkQuality == "")
                {
                    this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Required to fill all mandatory fields" }));
                }
                else
                {
                    
                        DataTable dtable = new DataTable();
                        DataRow dr;
						dtable.Columns.Add("I_MilkQuantity", typeof(string));
                        dtable.Columns.Add("V_SealLocation", typeof(string));
                        
                        dtable.Columns.Add("D_FAT", typeof(string));
                        dtable.Columns.Add("D_SNF", typeof(string));
                        dtable.Columns.Add("D_CLR", typeof(string));
                        dtable.Columns.Add("V_Temp", typeof(string));
                        dtable.Columns.Add("V_Acidity", typeof(string));
                        dtable.Columns.Add("V_COB", typeof(string));
                        dtable.Columns.Add("V_Alcohol", typeof(string));
                        dtable.Columns.Add("V_MBRT", typeof(string));
                        dtable.Columns.Add("V_MilkQuality", typeof(string));

                        int len = MilkQuantity.ToString().Split(',').Length;

                        for (int i = 0; i < len; i++)
                        {
                            dr = dtable.NewRow();
                            dr[1] = SealLocation.ToString().Split(',')[i];
                            dr[0] = MilkQuantity.ToString().Split(',')[i];
                           
                            dr[2] = FAT.ToString().Split(',')[i];
                            dr[3] = SNF.ToString().Split(',')[i];
                            dr[4] = CLR.ToString().Split(',')[i];
                            dr[5] = Temp.ToString().Split(',')[i];
                            dr[6] = Acidity.ToString().Split(',')[i];
                            dr[7] = COB.ToString().Split(',')[i];
                            dr[8] = Alcohol.ToString().Split(',')[i];
                            dr[9] = MBRT.ToString().Split(',')[i];
                           
                            dr[10] = MilkQuality.ToString().Split(',')[i];
                            dtable.Rows.Add(dr);
                        }

                        DataSet ds1 = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                                    new string[] { "Flag", "I_EntryID", "BI_MilkInOutRefID" },
                                    new string[] { "46", EntryID.ToString(), BI_MilkInOutRefID },
                                    "type_Trn_MilkQualityDetailsFilledTankerAPI", dtable, "TableSave");

                        if (ds1.Tables[0].Rows.Count == 0)
                        {
                            this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Something wents wrong." }));
                        }
                        else
                        {
                            if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
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
                            else
                            {
                                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString() }));
                            }
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
	[WebMethod] //All District List//Online complaint registration from website
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetAllDistrict(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = apiprocedure.ByProcedure("SpAdminDistrict", new string[] { "flag" },
                  new string[] { "16" }, "datatset");
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
	
	  [WebMethod] //Insert Grievance Detail//Online complaint registration from website
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void InsertGrievanceDetail(string Key, string Emp_ID, string Office_ID, string Complaint_Name, string ContactNo, string Applicantion_IsActive, string Application_GrvStatus, string DistrictID, string EmailID, string ComplaintNo, string Location, string Application_Subject, string Application_GrievanceType, string Application_Descritption, string Application_Doc1, string Extension1, string Application_Doc2, string Extension2, string Application_UpdatedBy)
    {
        string GUID = Guid.NewGuid().ToString();
        var Doc1PathWithName = "";
        var Doc1FullPathWithName = "";
        var Doc2PathWithName = "";
        var Doc2FullPathWithName = "";
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        APIProcedure apiprocedure = new APIProcedure();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                string FileFullPath = "";
                string FileFullPath1 = "";
                if (Application_Doc1 != "")
                {
                    FileFullPath = GUID + Extension1;
                    Doc1PathWithName = GUID + Extension1;
                    Doc1FullPathWithName = Server.MapPath(@"~/mis/Grievance/Upload_Doc/" + GUID);
                    Doc1PathWithName = Doc1FullPathWithName + Extension1;
                    byte[] imgByteArray = Convert.FromBase64String(Application_Doc1);
                    File.WriteAllBytes(Doc1PathWithName, imgByteArray);
                }

                if (Application_Doc2 != "")
                {
                    FileFullPath1 = GUID + Extension2;
                    Doc2PathWithName = GUID + Extension2;
                    Doc2FullPathWithName = Server.MapPath(@"~/mis/Grievance/Upload_Doc/" + GUID);
                    Doc2PathWithName = Doc2FullPathWithName + Extension2;
                    byte[] imgByteArray = Convert.FromBase64String(Application_Doc2);
                    File.WriteAllBytes(Doc2PathWithName, imgByteArray);
                }

                string saltkey = apiprocedure.GenerateSaltKey();
                DataSet ds1 = new DataSet();

                ds1 = apiprocedure.ByProcedure("SpGrvApplicantDetail",
                               new string[] { "flag", "Emp_ID", "Office_ID", "Complaint_Name", "ContactNo", "Applicantion_IsActive", "Application_GrvStatus", "DistrictID","EmailID", "ComplaintNo", "Location", "Application_Subject", "Application_GrievanceType", "Application_Descritption", "Application_Doc1", "Application_Doc2", "Application_UpdatedBy" },
                               new string[] { "0", Emp_ID, Office_ID, Complaint_Name, ContactNo, Applicantion_IsActive, Application_GrvStatus, DistrictID,EmailID, ComplaintNo, Location, Application_Subject, Application_GrievanceType, Application_Descritption, FileFullPath, FileFullPath1, Application_UpdatedBy }, "dataset");

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
	
	// [WebMethod] //Insert Grievance Detail//Online complaint registration from website
    // [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    // public void InsertGrievanceDetail(string Key, string Emp_ID, string Office_ID, string Complaint_Name, string ContactNo, string Applicantion_IsActive, string Application_GrvStatus, string DistrictID, string ComplaintNo, string Location, string Application_Subject, string Application_GrievanceType, string Application_Descritption, string Application_Doc1, string Extension1,string Application_Doc2, string Extension2, string Application_UpdatedBy)
    // {
        // string GUID = Guid.NewGuid().ToString();
        // var Doc1PathWithName = "";
        // var Doc1FullPathWithName = "";
        // var Doc2PathWithName = "";
        // var Doc2FullPathWithName = "";
        // DataSet ds = new DataSet();
        // DataTable dt = new DataTable();
        // APIProcedure apiprocedure = new APIProcedure();
        // System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        // this.Context.Response.ContentType = "application/json; charset=utf-8";
        // if (Key == securityKey)
        // {
            // try
            // {
                // string FileFullPath = "";
                // string FileFullPath1 = "";
                // if (Application_Doc1 != "")
                // {
					// Doc1PathWithName = GUID + Extension1;
					// Doc1FullPathWithName = Server.MapPath(@"~/mis/Grievance/Upload_Doc/" + GUID);
					// Doc1PathWithName = Doc1FullPathWithName + Extension1;
					// byte[] imgByteArray = Convert.FromBase64String(Application_Doc1);
					// File.WriteAllBytes(Doc1PathWithName, imgByteArray);
                // }
				
                // if (Application_Doc2 != "")
                // {
                    // Doc2PathWithName = GUID + Extension2;
					// Doc2FullPathWithName = Server.MapPath(@"~/mis/Grievance/Upload_Doc/" + GUID);
					// Doc2PathWithName = Doc2FullPathWithName + Extension2;
					// byte[] imgByteArray = Convert.FromBase64String(Application_Doc2);
					// File.WriteAllBytes(Doc2PathWithName, imgByteArray);
                // }

                // string saltkey = apiprocedure.GenerateSaltKey();
                // DataSet ds1 = new DataSet();

                // ds1 = apiprocedure.ByProcedure("SpGrvApplicantDetail",
                               // new string[] { "flag", "Emp_ID", "Office_ID","Complaint_Name","ContactNo", "Applicantion_IsActive", "Application_GrvStatus", "DistrictID", "ComplaintNo", "Location", "Application_Subject", "Application_GrievanceType", "Application_Descritption", "Application_Doc1", "Application_Doc2", "Application_UpdatedBy" },
                               // new string[] { "0", Emp_ID, Office_ID, Complaint_Name, ContactNo, Applicantion_IsActive, Application_GrvStatus, DistrictID, ComplaintNo, Location, Application_Subject, Application_GrievanceType, Application_Descritption, Doc1PathWithName, Doc2PathWithName, Application_UpdatedBy }, "dataset");

                // if (ds1.Tables[0].Rows.Count == 0)
                // {
                    // this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Invalid Login Credentials." }));
                // }
                // else
                // {

                    // dt = ds1.Tables[0];
                    // List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    // Dictionary<string, object> row = null;
                    // foreach (DataRow rs in dt.Rows)
                    // {
                        // row = new Dictionary<string, object>();
                        // foreach (DataColumn col in dt.Columns)
                        // {
                            // row.Add(col.ColumnName, rs[col]);
                        // }
                        // rows.Add(row);
                    // }
                    // this.Context.Response.Write(serializer.Serialize(new { List = rows, status = "1", Error = "Success" }));
                // }
            // }
            // catch (Exception ex)
            // {
                // this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            // }
            // dt.Clear();
            // ds.Clear();
        // }
        // else
        // {
            // this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = "Enter valid Key" }));
        // }
    // }
	
	[WebMethod] //Get data from application Ref No & Mobile No//Online complaint registration from website
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetGrvDataFromMobileNoAndAppRefNo(string Key , string ContactNo , string ApplicationRefNo)
    {
        string GUID = Guid.NewGuid().ToString();
        var DocPathWithName = "";
        var DocFullPathWithName = "";
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
                ds1 = apiprocedure.ByProcedure("SpGrvApplicantDetail",
                               new string[] { "flag", "ContactNo", "Application_RefNo" },
                               new string[] { "12", ContactNo, ApplicationRefNo  }, "dataset");

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
	
	[WebMethod] //Get data from application Ref No & Mobile No//Online complaint registration from website
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SendComplaintMsg(string Key, string MobileNo, string AppRefNo)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            { 
                string SMS = "शिकायत दर्ज | शिकायत क्र " + AppRefNo + " |";
                ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string link = "http://digimate.airtel.in:15181/BULK_API/SendMessage?loginID=mpmilk_api&password=mpmilk@123&mobile=" + MobileNo + "&text=" + Server.UrlEncode(SMS) + "&senderid=MPMILK&DLT_TM_ID=1001096933494158&DLT_CT_ID=1407164387730055647&DLT_PE_ID=1401514060000041644&route_id=DLT_SERVICE_IMPLICT&Unicode=2&camp_name=mpmilk_user";
                HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream stream = response.GetResponseStream();
                this.Context.Response.Write(serializer.Serialize(new { List = "1", status = "1", Error = "Success" }));
            }
            catch (Exception ex)
            {
                this.Context.Response.Write(serializer.Serialize(new { List = "", status = "0", Error = ex.Message.ToString() }));
            }
        }
    }
	[WebMethod] //All DS List
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetAllDSName(string Key)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = apiprocedure.ByProcedure("SpAdminOffice", new string[] { "flag" },
                  new string[] { "57" }, "datatset");
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
	[WebMethod] //DS Name From District Id
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void GetDSNameFromDistrictID(string Key, string DistrictID)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.Context.Response.ContentType = "application/json; charset=utf-8";
        if (Key == securityKey)
        {
            try
            {
                ds = apiprocedure.ByProcedure("SpAdminDistrict", new string[] { "flag", "District_ID" },
                  new string[] { "17", DistrictID }, "datatset");
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
