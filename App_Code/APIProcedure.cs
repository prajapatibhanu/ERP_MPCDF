using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Globalization;

public abstract class AbstApiDBApi
{
    public abstract void WriteLog(string _msg);
    public abstract string ReplaceVal(string msg);
    public abstract string Encrypt(string sData);
    public abstract string Decrypt(string sData);
    public abstract bool isNumber(string s);
    public abstract bool isDecimal(string s);
    public abstract void ByText(string Query);
    public abstract void ByText(string Query, SqlConnection Con, SqlTransaction Tran);
    public abstract DataSet ByDataSet(string Query);
    public abstract void ResizeImage(Image ImageId, int ResizeWidth, int ResizeHeight);
    public abstract DataSet ByProcedure(string ProcedureName, string[] Parameter, string[] Values, string OutPutParamName, out int TotRecord, string ByDataSetAlert);
    public abstract DataSet ByProcedure(string ProcedureName, string[] Parameter, string[] Values, string ByDataSetAlert);
    public abstract DataTable ByProcedure(string ProcedureName, string[] Parameter, string[] Values, string OutPutParamName, out int TotRecord, string ByDataTableAlert, string PassEmptyText);
    public abstract DataTable ByProcedure(string ProcedureName, string[] Parameter, string[] Values, string ByDataTableAlert, string PassEmptyText);
    public abstract DataSet ByProcedure(string ProcedureName, string[] Parameter, string[] Values, SqlConnection Cnn, SqlTransaction Tran, string ByDataTableAlert);
    public abstract DataSet ByProcedure(string ProcedureName, string[] Parameter, string[] Values, string[] TableParam, DataTable[] TableType, string ByDataSetAlert);
    public abstract DataSet ByProcedure(string ProcedureName, string[] Parameter, string[] Values, string TableParam, DataTable TableType, string ByDataSetAlert);
    public abstract DataSet ByProcedure(string ProcedureName, string ByDataSetAlert);
    public abstract void ByProcedure(string ProcedureName, string[] SaveParameter, string[] SaveValues, Char BySaveAlert);
    public abstract void ByProcedure(string ProcedureName, string[] SaveParameter, string[] SaveValues, string byWithTranSaveAlert, SqlConnection Cnn, SqlTransaction Trans);
    public abstract bool CheckFileHeader(byte[] buffer, string extension);
    public abstract string getdate();
    public abstract string getfy(string Date);
    public abstract string uploadFile(FileUpload FileUpload1, string FolderName, int FileSize);
    public abstract string Alert(string icon, string AlertClass, string Heading, string Description);
}

public class APIProcedure : AbstApiDBApi
{
    public int _NewWidth, _NewHeight;
    public string _ErrorMessage;
    public string _mobileNo, _textMsg, _firstname, _bymemId, _verificationcode,
      _loginpwd, _transPwd, _amount, _description, _pinqty, _tomemID, _generateAPI,
        _FileName, _fileExt;
    public string FileExt
    {
        get
        {
            return _fileExt;
        }
        set
        {
            _fileExt = value;
        }
    }
    public string FullFileName
    {
        set
        {
            _FileName = value;
        }
        get
        {
            return _FileName;
        }
    }
    public string TomemID
    {
        get { return string.IsNullOrEmpty(_tomemID) ? "" : _tomemID; }
        set { _tomemID = value; }
    }
    public string Pinqty
    {
        get { return string.IsNullOrEmpty(_pinqty) ? "" : _pinqty; }
        set { _pinqty = value; }
    }
    public string Descrp
    {
        get { return string.IsNullOrEmpty(_description) ? "" : _description; }
        set { _description = value; }
    }
    public string Amt
    {
        get { return string.IsNullOrEmpty(_amount) ? "" : _amount; }
        set { _amount = value; }
    }

    public string TransPwd
    {
        get { return string.IsNullOrEmpty(_transPwd) ? "" : _transPwd; }
        set { _transPwd = value; }
    }
    public string Mobileno
    {
        get { return string.IsNullOrEmpty(_mobileNo) ? "" : _mobileNo; }
        set { _mobileNo = value; }
    }
    public string TextMsg
    {
        get { return string.IsNullOrEmpty(_textMsg) ? "" : _textMsg; }
        set { _textMsg = value; }
    }

    public string Firstname
    {
        get { return string.IsNullOrEmpty(_firstname) ? "" : _firstname; }
        set { _firstname = value; }
    }
    public string BymemId
    {
        get { return string.IsNullOrEmpty(_bymemId) ? "" : _bymemId; }
        set { _bymemId = value; }
    }
    public string Verificationcode
    {
        get { return string.IsNullOrEmpty(_verificationcode) ? "" : _verificationcode; }
        set { _verificationcode = value; }
    }
    public string Loginpwd
    {
        get { return string.IsNullOrEmpty(_loginpwd) ? "" : _loginpwd; }
        set { _loginpwd = value; }
    }
    public string GenerateAPI
    {
        get { return string.IsNullOrEmpty(_generateAPI) ? "" : _generateAPI; }
        set { _generateAPI = value; }
    }
    public int NewWidth
    {
        get { return _NewWidth; }
        set { _NewWidth = value; }
    }
    public int NewHeight
    {
        get { return _NewHeight; }
        set { _NewHeight = value; }
    }
    public string ErrorMessage
    {
        get { return _ErrorMessage; }
        set { _ErrorMessage = value; }
    }
    public string Cn;
    public string ConnMIS;
    public DataSet ds;
    public DataTable dt;
    public SqlCommand cmd;
    public SqlCommand cmd1;
    //Dal objConn = new Dal();
    public string getconnection
    {

        get
        {
            try
            {

                Cn = System.Configuration.ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;// objdb.getconnection();
            }
            catch { ErrorMessage = "Yes"; throw new Exception("Please Provide Connection Object Name:Conn"); }

            return Cn;
        }
    }
    public string getconnectionMis
    {

        get
        {
            try
            {

                ConnMIS = System.Configuration.ConfigurationManager.ConnectionStrings["ConnMIS"].ConnectionString;// objdb.getconnection();
            }
            catch { ErrorMessage = "Yes"; throw new Exception("Please Provide Connection Object Name:Conn"); }

            return ConnMIS;
        }
    }

    public bool checkDateFormat(string date)
    {
        try
        {
            DateTime dt = DateTime.Parse(date);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public string OuterUpdateMessage(string message)
    {
        string text = "";
        text = @"<table cellpadding='0' cellspacing='0' class='ErrorMsgForUID'>        
        <tr> <td  align='center'> <div style='float:left;'><img src='UserPanel_Images/sucess.png' alt='' /> </div>
        <div style='float:left;padding-left:5px;padding-top:1px;'>" + message + "</div> </td>        </tr>        </table>";
        return text;
    }
    public string OuterErrorMessage(string message)
    {
        string text = "";
        text = @"<table cellpadding='0' cellspacing='0' class='ErrorMsgForUID'>        
        <tr> <td  align='center'> <div style='float:left;'><img src='UserPanel_Images/Error.png' alt='' /> </div>
        <div style='float:left;padding-left:5px;padding-top:1px;'>" + message + "</div> </td>        </tr>        </table>";
        return text;
    }
    public string OuterEmptyMessage(string message)
    {
        string text = "";
        text = @"<table cellpadding='0' cellspacing='0' class='ErrorMsgForUID'>        
        <tr> <td  align='center'> <div style='float:left;'><img src='UserPanel_Images/Empty_Message.png' alt='' /> </div>
        <div style='float:left;padding-left:5px;padding-top:1px;'>" + message + "</div> </td>        </tr>        </table>";
        return text;
    }
    public string UpdateMessage(string message)
    {
        string text = "";
        text = @"<div  class='alert alert-success'> " + message + " </div>";

        return text;
    }
    public string ErrorMsg(string message)
    {
        string text = "";
        text = @"<div  class='alert alert-danger'> " + message + " </div>";
        return text;
    }
    public string EmptyMessage(string message)
    {
        string text = "";
        text = @"<table cellpadding='0' cellspacing='0' class='ErrorMsgForUID'>        
        <tr> <td > <div style='float:left;'><img src='../UserPanel_Images/Empty_Message.png' alt='' /> </div>
        <div style='float:left;padding-left:5px;padding-top:1px;'>" + message + "</div> </td>        </tr>        </table>";
        return text;
    }
    public string AdminUpdateMessage(string message)
    {
        string text = "";
        text = @"<table cellpadding='0' cellspacing='0' class='ErrorMsgForUID'>        
        <tr> <td > <div style='float:left;'><img src='../UserPanel_Images/sucess.png' alt='' /> </div>
        <div style='float:left;padding-left:5px;padding-top:1px;'>" + message + "</div> </td>        </tr>        </table>";
        return text;
    }
    public string AdminErrorMessage(string message)
    {
        string text = "";
        text = @"<table cellpadding='0' cellspacing='0' class='ErrorMsgForUID'>        
        <tr> <td > <div style='float:left;'><img src='../UserPanel_Images/Error.png' alt='' /> </div>
        <div style='float:left;padding-left:5px;padding-top:1px;'>" + message + "</div> </td>        </tr>        </table>";
        return text;
    }
    public string AdminEmptyMessage(string message)
    {
        string text = "";
        text = @"<table cellpadding='0' cellspacing='0' class='ErrorMsgForUID'>        
        <tr> <td > <div style='float:left;'><img src='../UserPanel_Images/Empty_Message.png' alt='' /> </div>
        <div style='float:left;padding-left:5px;padding-top:1px;'>" + message + "</div> </td>        </tr>        </table>";
        return text;
    }
    public override bool CheckFileHeader(byte[] buffer, string extension)
    {
        ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        string header = enc.GetString(buffer);
        switch (extension)
        {
            case "csv":
                return true;
            case "pdf":
                return header.StartsWith("%PDF-");
            case "jpg":
                return header.StartsWith("????") && !header.StartsWith("?????");
            case "jpeg":
                return header.StartsWith("????") && !header.StartsWith("?????");
            case "doc":
                return header.StartsWith("??_???_?");
            case "docx":
                return header.StartsWith("??_???_?");
            case "png":
                return header.StartsWith(".PNG") || header.StartsWith("?PNG");
            case "gif":
                return header.StartsWith("GIF87a") || header.StartsWith("GIF89a");
            case "OnlyImages":
                if (header.StartsWith("????") && !header.StartsWith("?????"))
                {
                    return true;
                }
                else if (header.StartsWith(".PNG") || header.StartsWith("?PNG"))
                {
                    return true;
                }
                else if (header.StartsWith("MM*"))
                {
                    return true;
                }
                else if (header.StartsWith("MM*"))
                {
                    return true;
                }
                else if (header.StartsWith("GIF87a") || header.StartsWith("GIF89a"))
                {
                    return true;
                }
                else if (header.StartsWith("\\0\\0"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                return false;
        }
    }
    public override string uploadFile(FileUpload FileUpload1, string FolderName, int MaxFileSizeInKB)
    {
        string Msg = "", SaveSts = "";
        int NewFileSizeInKB = 0;
        try
        {
            byte[] FileInBytes = null;

            // 1 MB= 1000 KB
            //1024 Bytes =1 KB
            //1 kb  =1*1024=1 MB 
            NewFileSizeInKB = MaxFileSizeInKB * 1024;
            StringBuilder sb = new StringBuilder();
            string dirName = HttpContext.Current.Server.MapPath("~/" + FolderName);
            Random Rnd = new Random();
            //Create Directory if not exist.
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
            else
            {
                HttpFileCollection uploadFilCol = HttpContext.Current.Request.Files;

                for (int i = 0; i < uploadFilCol.Count; i++)
                {
                    HttpPostedFile file = uploadFilCol[i];
                    string fileExt = Path.GetExtension(file.FileName).ToLower();
                    FileInBytes = new byte[file.ContentLength];
                    file.InputStream.Read(FileInBytes, 0, FileInBytes.Length);
                    //get file extention like .jpg
                    string FilePath = HttpContext.Current.Server.MapPath("~/" + file.FileName);
                    //Uploaded File Location 
                    int ContentFileSize = file.ContentLength;
                    if (NewFileSizeInKB > ContentFileSize || MaxFileSizeInKB == 0)
                    {
                        //File Extention
                        if (CheckFileHeader(FileInBytes, fileExt.Replace(".", "")))
                        {
                            if (fileExt == ".jpg" || fileExt == ".pdf" || fileExt == ".jpeg" || fileExt == ".png")
                            {
                                Guid obj = Guid.NewGuid();

                                FullFileName = obj.ToString() + fileExt;
                                file.SaveAs(dirName + "/" + FullFileName);
                                //File save in to Directory With New Name.
                                //FileInfo fileinfo = new FileInfo(dirName + "\\" + FileFullName);
                                //sb.Append(dirName + "\\" + FileFullName + " :: <span style='Color:Green'>File Size : </span>" + fileinfo.Length * 1024 + " <span style='Color:Green'> bytes </span>" + fileinfo.Length / 1024 + " <span style='Color:Green'>KB </span>" + fileinfo.Length / 1024000 + " <span style='Color:Red'>MB </span></br>");
                                SaveSts = "Ok";
                                break;
                            }
                            else
                            {
                                SaveSts = "NotOk";
                                Msg = "File format not recognised." + " jpg/jpeg/png/pdf formats";
                            }
                        }
                        else
                        {
                            SaveSts = "NotOk";
                            Msg = "File format not recognised." + " jpg/jpeg/gif/png/pdf/doc/docx/csv formats";
                        }
                    }
                    else
                    {
                        SaveSts = "NotOk";
                        Msg = "<span style='Color:#fff'> Maximum length of uploading file should be " + MaxFileSizeInKB + " KB.</span>";
                    }
                }
                if (SaveSts == "Ok" || SaveSts == "NotOk")
                {
                    if (SaveSts == "NotOk")
                    {

                    }
                    else
                    {
                        Msg = SaveSts;
                    }
                }
                else
                {
                    Msg = "Please upload files.";
                }
            }
        }
        catch (Exception ex) {  HttpContext.Current.Response.Redirect("~/mis/Login.aspx", true); }
        return Msg;
    }
    public string SingleuploadFile(FileUpload FileUpload1, string FolderName, int MaxFileSizeInKB)
    {
        string Msg = "", SaveSts = "";
        int NewFileSizeInKB = 0;
        try
        {
            byte[] FileInBytes = null;
            // 1 MB= 1000 KB
            //1024 Bytes =1 KB
            //1 kb  =1*1024=1 MB 
            NewFileSizeInKB = MaxFileSizeInKB * 1024;
            StringBuilder sb = new StringBuilder();
            string dirName = HttpContext.Current.Server.MapPath("~/" + FolderName);
            Random Rnd = new Random();
            //Create Directory if not exist.
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
            else
            {

                // HttpPostedFile file = FileUpload1; //uploadFilCol[i];
                string fileExt = Path.GetExtension(FileUpload1.FileName).ToLower();
                //get file extention like .jpg
                string FilePath = HttpContext.Current.Server.MapPath("~/" + FileUpload1.FileName);
                //Uploaded File Location 
                long ContentFileSize = FileUpload1.FileContent.Length;
                FileInBytes = new byte[FileUpload1.FileContent.Length];
                FileUpload1.FileContent.Read(FileInBytes, 0, FileInBytes.Length);
                if (NewFileSizeInKB > ContentFileSize || MaxFileSizeInKB == 0)
                {
                    if (CheckFileHeader(FileInBytes, fileExt.Replace(".", "")))
                    {
                        //File Extention
                        if (fileExt == ".jpg" || fileExt == ".csv" || fileExt == ".pdf" || fileExt == ".gif" || fileExt == ".jpeg" || fileExt == ".png" || fileExt == ".doc" || fileExt == ".docx")
                        {
                            FileExt = fileExt;
                            Guid obj = Guid.NewGuid();
                            FullFileName = obj.ToString() + fileExt;
                            FileUpload1.SaveAs(dirName + "/" + FullFileName);
                            //File save in to Directory With New Name.
                            //FileInfo fileinfo = new FileInfo(dirName + "\\" + FileFullName);
                            //sb.Append(dirName + "\\" + FileFullName + " :: <span style='Color:Green'>File Size : </span>" + fileinfo.Length * 1024 + " <span style='Color:Green'> bytes </span>" + fileinfo.Length / 1024 + " <span style='Color:Green'>KB </span>" + fileinfo.Length / 1024000 + " <span style='Color:Red'>MB </span></br>");
                            SaveSts = "Ok";
                        }
                        else
                        {
                            SaveSts = "NotOk";
                            Msg = "File format not recognised." + " jpg/jpeg/gif/png/pdf/doc/docx/csv formats";
                        }
                    }
                    else
                    {
                        SaveSts = "NotOk";
                        Msg = "File format not recognised." + " jpg/jpeg/gif/png/pdf/doc/docx/csv formats";
                    }
                }
                else
                {
                    SaveSts = "NotOk";
                    Msg = "<span style='Color:Red'> Maximum length of uploading file should be " + MaxFileSizeInKB + " KB.</span>";
                }

                if (SaveSts == "Ok" || SaveSts == "NotOk")
                {
                    if (SaveSts == "NotOk")
                    {

                    }
                    else
                    {
                        Msg = SaveSts;
                    }
                }
                else
                {
                    Msg = "Please upload files.";
                }
            }
        }
        catch (Exception ex) {  HttpContext.Current.Response.Redirect("~/mis/Login.aspx", true); }
        return Msg;
    }
    public override void WriteLog(string _msg)
    {
        string filepath;
        try
        {
            filepath = HttpContext.Current.Server.MapPath("~/exLog.html");
            if (System.IO.File.Exists(filepath))
            {
                using (StreamWriter writer = new StreamWriter(HttpContext.Current.Server.MapPath("~/exLog.html"), true))
                {
                    //for writing time....

                    writer.Write("<br>" + "[<b style='color:Red;'>Date:</b> " + DateTime.Now.ToLongDateString() + "] & [<b style='color:Red;'>Time:</b> " + DateTime.Now.ToLongTimeString() + "]");
                    writer.WriteLine();
                    //actual write cintent.....            

                    writer.Write("<br>" + ReplaceVal(_msg));
                    writer.WriteLine();
                    //For Record Sepration....            

                    writer.WriteLine("<br><hr>");
                    writer.Close();
                }
            }
            else
            {
                System.IO.StreamWriter sw = System.IO.File.CreateText(filepath);
                //for writing time....            

                using (StreamWriter writer = new StreamWriter(HttpContext.Current.Server.MapPath("~/exLog.html"), true))
                {
                    //for writing time....

                    writer.Write("[<b style='color:Red;'>Date:</b> " + DateTime.Now.ToLongDateString() + "] & [<b style='color:Red;'>Time:</b> " + DateTime.Now.ToLongTimeString() + "]");
                    writer.WriteLine();

                    //actual write cintent.....            

                    writer.Write("<br>" + ReplaceVal(_msg));
                    writer.WriteLine();
                    //For Record Sepration....            

                    writer.WriteLine("<br><hr>");
                    writer.Close();
                }
            }
        }
        catch (Exception ex)
        {
            WriteLog(" Error Msg :" + ex.Message + "\n" + "Event Info :" + ex.StackTrace);
        }
    }
    public override string ReplaceVal(string msg)
    {
        string Msg = "";
        try
        {
            Msg = msg;
            Msg.Replace(@"at System.Data.SqlClient.SqlConnection.OnError(SqlException exception, Boolean breakConnection)
   at System.Data.SqlClient.SqlInternalConnection.OnError(SqlException exception, Boolean breakConnection)
   at System.Data.SqlClient.TdsParser.ThrowExceptionAndWarning(TdsParserStateObject stateObj)
   at System.Data.SqlClient.TdsParser.Run(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj)
   at System.Data.SqlClient.SqlCommand.RunExecuteNonQueryTds(String methodName, Boolean async)
   at System.Data.SqlClient.SqlCommand.InternalExecuteNonQuery(DbAsyncResult result, String methodName, Boolean sendToPipe)
   at System.Data.SqlClient.SqlCommand.ExecuteNonQuery()", "").Replace("Error Msg :", "<b style='color:Red;'>Error Msg :</b>").Replace("Event Info :", "<b style='color:Red;'>Event Info :</b>");
        }
        catch (Exception ex)
        {
            WriteLog(" Error Msg :" + ex.Message + "\n" + "Event Info :" + ex.StackTrace);
        }
        return Msg;
    }
    public override string Decrypt(string sData)
    {

        string EncryptionKey = "%&$:";
        byte[] cipherBytes = Convert.FromBase64String(sData.Replace(" ", "+"));
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                sData = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return sData;
    }
    public override string Encrypt(string sData)
    {

        string EncryptionKey = "%&$:";
        byte[] clearBytes = Encoding.Unicode.GetBytes(sData.Replace(" ", ""));
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64 });//, 0x76, 0x65, 0x64, 0x65, 0x76 
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                sData = Convert.ToBase64String(ms.ToArray());
            }
        }
        return sData;
    }

    static public string Client_Encrypt(string sData)
    {

        string EncryptionKey = "%&$:";
        byte[] clearBytes = Encoding.Unicode.GetBytes(sData.Replace(" ", ""));
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64 });//, 0x76, 0x65, 0x64, 0x65, 0x76 
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                sData = Convert.ToBase64String(ms.ToArray());
            }
        }
        return sData;
    }
    static public string Client_Decrypt(string sData)
    {

        string EncryptionKey = "%&$:";
        byte[] cipherBytes = Convert.FromBase64String(sData.Replace(" ", "+"));
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                sData = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return sData;
    }
    public override void ByText(string Query)
    {
        try
        {
            using (SqlConnection cn = new SqlConnection(getconnection))
            {
                using (SqlDataAdapter adp = new SqlDataAdapter())
                {
                    adp.SelectCommand = new SqlCommand(Query, cn);

                    ds = new DataSet();
                    adp.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {
            WriteLog(" Error Msg :" + ex.Message + "\n" + "Event Info :" + ex.StackTrace);
        }
        finally
        {
            if (ErrorMessage != "Yes")
            {
                //cmd.Dispose();
                ds.Dispose();
            }
        }

    }
    public override void ByText(string Query, SqlConnection Con, SqlTransaction Tran)
    {
        try
        {
            cmd = new SqlCommand(Query, Con, Tran);
            cmd.CommandTimeout = 3600;
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            WriteLog(" Error Msg :" + ex.Message + "\n" + "Event Info :" + ex.StackTrace);
        }
        finally { cmd.Dispose(); }
    }
    public override DataSet ByDataSet(string Query)
    {
        try
        {

            using (SqlConnection cn = new SqlConnection(getconnection))
            {
                cmd = new SqlCommand(Query, cn);
                cmd.CommandTimeout = 3600;
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    ds = new DataSet();
                    adp.Fill(ds);
                }
            }

        }
        catch (Exception ex)
        {
            WriteLog(" Error Msg :" + ex.Message + "\n" + "Event Info :" + ex.StackTrace);
        }
        finally
        {
            if (ErrorMessage != "Yes")
            {
                //cmd.Dispose();
                ds.Dispose();
            }
        }
        return ds;
    }

    public override void ResizeImage(Image ImageId, int ResizeWidth, int ResizeHeight)
    {
        int width = 0, height = 0, newWidth = 0, newHeight = 0, wHStatus = 0, MainWidth = 0, MainHeight = 0;
        try
        {
            System.Drawing.Image image101 = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(ImageId.ImageUrl));
            width = image101.Width;
            height = image101.Height;

            if (width > height)
            { wHStatus = 1; }
            if (width < height)
            { wHStatus = 2; }
            if (width == height)
            { wHStatus = 3; }

            if (wHStatus == 1)
            {
                if (width > ResizeWidth)
                {
                    MainWidth = ResizeWidth;
                    double ratioX = (double)ResizeWidth / image101.Width;
                    double ratioY = (double)ResizeHeight / image101.Height;
                    double ratio1 = Math.Min(ratioX, ratioY);

                    newWidth = (int)(image101.Width * ratio1);
                    newHeight = (int)(image101.Height * ratio1);
                    MainHeight = newHeight;
                    //check = 1;
                }
                else
                {
                    MainWidth = width;

                    if (height > ResizeHeight)
                    {
                        double ratioX = (double)ResizeWidth / image101.Width;
                        double ratioY = (double)ResizeHeight / image101.Height;
                        double ratio1 = Math.Min(ratioX, ratioY);

                        newWidth = (int)(image101.Width * ratio1);
                        newHeight = (int)(image101.Height * ratio1);
                        MainHeight = newHeight;
                        MainWidth = newWidth;
                    }
                    else
                    {
                        MainHeight = height;
                    }
                }
            }
            if (wHStatus == 2)
            {
                if (height > ResizeHeight)
                {
                    double ratioX = (double)ResizeWidth / image101.Width;
                    double ratioY = (double)ResizeHeight / image101.Height;
                    double ratio1 = Math.Min(ratioX, ratioY);

                    newWidth = (int)(image101.Width * ratio1);
                    newHeight = (int)(image101.Height * ratio1);
                    MainHeight = newHeight;
                    MainWidth = newWidth;
                }
                else
                {
                    MainHeight = height;
                    if (width > ResizeWidth)
                    {
                        MainWidth = ResizeWidth;
                        double ratioX = (double)ResizeWidth / image101.Width;
                        double ratioY = (double)ResizeHeight / image101.Height;
                        double ratio1 = Math.Min(ratioX, ratioY);

                        newWidth = (int)(image101.Width * ratio1);
                        newHeight = (int)(image101.Height * ratio1);
                        MainHeight = newHeight;
                        //check = 1;
                    }
                    else
                    {
                        MainWidth = width;
                    }
                }
            }
            if (wHStatus == 3)
            {
                if (width > ResizeWidth)
                {
                    MainWidth = ResizeWidth;
                    double ratioX = (double)ResizeWidth / image101.Width;
                    double ratioY = (double)ResizeHeight / image101.Height;
                    double ratio1 = Math.Min(ratioX, ratioY);

                    newWidth = (int)(image101.Width * ratio1);
                    newHeight = (int)(image101.Height * ratio1);
                    MainHeight = newHeight;
                    //check = 1;
                }
                else
                {
                    MainWidth = width;

                    if (height > ResizeHeight)
                    {
                        double ratioX = (double)ResizeWidth / image101.Width;
                        double ratioY = (double)ResizeHeight / image101.Height;
                        double ratio1 = Math.Min(ratioX, ratioY);

                        newWidth = (int)(image101.Width * ratio1);
                        newHeight = (int)(image101.Height * ratio1);
                        MainHeight = newHeight;
                        MainWidth = newWidth;
                    }
                    else
                    {
                        MainHeight = height;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            WriteLog(" Error Msg :" + ex.Message + "\n" + "Event Info :" + ex.StackTrace);
        }
        NewWidth = MainWidth;
        NewHeight = MainHeight;

    }
    public override DataSet ByProcedure(string ProcedureName, string[] Parameter, string[] Values, string OutPutParamName, out int TotRecord, string ByDataSetAlert)
    {
        try
        {

            using (SqlConnection cn = new SqlConnection(getconnection))
            {
                cmd = new SqlCommand(ProcedureName, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@" + OutPutParamName, SqlDbType.Int).Direction = ParameterDirection.Output;
                for (int i = 0; i < Parameter.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@" + Parameter[i].ToString(), Values[i].ToString());
                }
                cmd.CommandTimeout = 3600;
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    ds = new DataSet();
                    adp.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {
            WriteLog(" Error Msg :" + ex.Message + "\n" + "Event Info :" + ex.StackTrace);
             HttpContext.Current.Response.Redirect("~/mis/Login.aspx", true);
        }
        finally
        {

            if (ErrorMessage != "Yes")
            {
                ds.Dispose();
                cmd.Dispose();
            }
        }
        TotRecord = (int)cmd.Parameters["@" + OutPutParamName].Value;
        return ds;
    }
    public override DataSet ByProcedure(string ProcedureName, string[] Parameter, string[] Values, string ByDataSetAlert)
    {
        try
        {
            using (SqlConnection cn = new SqlConnection(getconnection))
            {
                cmd = new SqlCommand(ProcedureName, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < Parameter.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@" + Parameter[i].ToString(), Values[i].ToString());
                }
                cmd.CommandTimeout = 3600;
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    ds = new DataSet();
                    adp.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {
            WriteLog(" Error Msg :" + ex.Message + "\n" + "Event Info :" + ex.StackTrace);
             HttpContext.Current.Response.Redirect("~/mis/Login.aspx", true);
        }
        finally
        {
            if (ErrorMessage != "Yes")
            {

                ds.Dispose();
                cmd.Dispose();
            }
        }
        return ds;
    }

    public override DataSet ByProcedure(string ProcedureName, string[] Parameter, string[] Values, string TableParam, DataTable TableType, string ByDataSetAlert)
    {
        try
        {
            using (SqlConnection cn = new SqlConnection(getconnection))
            {
                cmd = new SqlCommand(ProcedureName, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < Parameter.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@" + Parameter[i].ToString(), Values[i].ToString());
                }
                cmd.Parameters.AddWithValue("@" + TableParam, TableType);
                cmd.CommandTimeout = 3600;
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    ds = new DataSet();
                    adp.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {
            WriteLog(" Error Msg :" + ex.Message + "\n" + "Event Info :" + ex.StackTrace);
             HttpContext.Current.Response.Redirect("~/mis/Login.aspx", true);
        }
        finally
        {
            if (ErrorMessage != "Yes")
            {

                ds.Dispose();
                cmd.Dispose();
            }
        }
        return ds;
    }

    public override DataSet ByProcedure(string ProcedureName, string[] Parameter, string[] Values, string[] TableParam, DataTable[] TableType, string ByDataSetAlert)
    {
        try
        {
            using (SqlConnection cn = new SqlConnection(getconnection))
            {
                cmd = new SqlCommand(ProcedureName, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < Parameter.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@" + Parameter[i].ToString(), Values[i].ToString());
                }

                for (int j = 0; j < TableParam.Length; j++)
                {
                    cmd.Parameters.AddWithValue("@" + TableParam[j], TableType[j]);
                }
                cmd.CommandTimeout = 3600;
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    ds = new DataSet();
                    adp.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {
            WriteLog(" Error Msg :" + ex.Message + "\n" + "Event Info :" + ex.StackTrace);
             HttpContext.Current.Response.Redirect("~/mis/Login.aspx", true);
        }
        finally
        {
            if (ErrorMessage != "Yes")
            {

                ds.Dispose();
                cmd.Dispose();
            }
        }
        return ds;
    }

    public override DataTable ByProcedure(string ProcedureName, string[] Parameter, string[] Values, string OutPutParamName, out int TotRecord, string ByDataTableAlert, string PassEmptyText)
    {
        try
        {
            using (SqlConnection cn = new SqlConnection(getconnection))
            {
                cmd = new SqlCommand(ProcedureName, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@" + OutPutParamName, SqlDbType.Int).Direction = ParameterDirection.Output;
                for (int i = 0; i < Parameter.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@" + Parameter[i].ToString(), Values[i].ToString());
                }
                cmd.CommandTimeout = 3600;
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    dt = new DataTable();
                    adp.Fill(dt);
                }
            }

        }
        catch (Exception ex)
        {
            WriteLog(" Error Msg :" + ex.Message + "\n" + "Event Info :" + ex.StackTrace);
             HttpContext.Current.Response.Redirect("~/mis/Login.aspx", true);
        }
        finally
        {
            if (ErrorMessage != "Yes")
            {
                dt.Dispose();
                cmd.Dispose();
            }
        }
        TotRecord = (int)cmd.Parameters["@" + OutPutParamName].Value;
        return dt;
    }
    public override DataTable ByProcedure(string ProcedureName, string[] Parameter, string[] Values, string ByDataTableAlert, string PassEmptyText)
    {
        try
        {
            using (SqlConnection cn = new SqlConnection(getconnection))
            {
                cmd = new SqlCommand(ProcedureName, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < Parameter.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@" + Parameter[i].ToString(), Values[i].ToString());
                }
                cmd.CommandTimeout = 3600;
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    dt = new DataTable();
                    adp.Fill(dt);
                }
            }
        }
        catch (Exception ex)
        {
            WriteLog(" Error Msg :" + ex.Message + "\n" + "Event Info :" + ex.StackTrace);
             HttpContext.Current.Response.Redirect("~/mis/Login.aspx", true);
        }
        finally
        {
            if (ErrorMessage != "Yes")
            {
                dt.Dispose();
                cmd.Dispose();
            }
        }
        return dt;
    }

    public override DataSet ByProcedure(string ProcedureName, string[] Parameter, string[] Values, SqlConnection Cnn, SqlTransaction Tran, string ByDataTableAlert)
    {
        try
        {
            cmd = new SqlCommand(ProcedureName, Cnn, Tran);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < Parameter.Length; i++)
            {
                cmd.Parameters.AddWithValue("@" + Parameter[i].ToString(), Values[i].ToString());
            }
            cmd.CommandTimeout = 3600;
            using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
            {
                ds = new DataSet();
                adp.Fill(ds);
            }
        }
        catch (Exception ex)
        {
            WriteLog(" Error Msg :" + ex.Message + "\n" + "Event Info :" + ex.StackTrace);
             HttpContext.Current.Response.Redirect("~/mis/Login.aspx", true);
        }
        finally
        {
            if (ErrorMessage != "Yes")
            {
                ds.Dispose();
                cmd.Dispose();
            }
        }
        return ds;
    }

    public override DataSet ByProcedure(string ProcedureName, string ByDataSetAlert)
    {
        try
        {
            using (SqlConnection cn = new SqlConnection(getconnection))
            {
                cmd = new SqlCommand(ProcedureName, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 3600;
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    ds = new DataSet();
                    adp.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {
            WriteLog(" Error Msg :" + ex.Message + "\n" + "Event Info :" + ex.StackTrace);
             HttpContext.Current.Response.Redirect("~/mis/Login.aspx", true);
        }
        finally
        {
            if (ErrorMessage != "Yes")
            {
                ds.Dispose();
                cmd.Dispose();
            }
        }
        return ds;
    }
    public override void ByProcedure(string ProcedureName, string[] SaveParameter, string[] SaveValues, Char BySaveAlert)
    {
        try
        {
            using (SqlConnection cn = new SqlConnection(getconnection))
            {
                cmd = new SqlCommand(ProcedureName, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 3600;
                for (int i = 0; i < SaveParameter.Length; i++)
                {
                    cmd.Parameters.AddWithValue("@" + SaveParameter[i].ToString(), SaveValues[i].ToString());
                }
                using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                {
                    ds = new DataSet();
                    adp.Fill(ds);
                }
            }
        }
        catch (Exception ex)
        {
            WriteLog(" Error Msg :" + ex.Message + "\n" + "Event Info :" + ex.StackTrace);
             HttpContext.Current.Response.Redirect("~/mis/Login.aspx", true);
        }
        finally
        {
            if (ErrorMessage != "Yes")
            {
                cmd.Dispose();
                ds.Dispose();
            }
        }

    }
    public override void ByProcedure(string ProcedureName, string[] SaveParameter, string[] SaveValues, string byWithTranSaveAlert, SqlConnection Cnn, SqlTransaction Trans)
    {
        try
        {
            cmd = new SqlCommand(ProcedureName, Cnn, Trans);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < SaveParameter.Length; i++)
            {
                cmd.Parameters.AddWithValue("@" + SaveParameter[i].ToString(), SaveValues[i].ToString());
            }
            cmd.CommandTimeout = 3600;
            using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
            {
                ds = new DataSet();
                adp.Fill(ds);
            }
        }
        catch (Exception ex)
        {
            WriteLog(" Error Msg :" + ex.Message + "\n" + "Event Info :" + ex.StackTrace);
             HttpContext.Current.Response.Redirect("~/mis/Login.aspx", true);
        }
        finally
        {
            cmd.Dispose();
            ds.Dispose();

        }
    }



    // Converts the specified JSON string to an object of type T
    public T Deserialize<T>(string context)
    {
        string jsonData = context;

        //cast to specified objectType
        var obj = (T)new JavaScriptSerializer().Deserialize<T>(jsonData);
        return obj;
    }


    public String changeToWords(String numb)
    {
        String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
        String endStr = (" Only.");
        try
        {
            int decimalPlace = numb.IndexOf(".");
            if (decimalPlace > 0)
            {
                wholeNo = numb.Substring(0, decimalPlace);
                points = numb.Substring(decimalPlace + 1);
                if (Convert.ToInt32(points) > 0)
                {
                    //int p = points.Length;
                    //char[] TPot = points.ToCharArray();
                    andStr = (" and ");// just to separate whole numbers from points/Rupees                   
                    //for(int i=0;i<p;i++)
                    //{
                    //    andStr += ones(Convert.ToString(TPot[i]))+" ";
                    //}
                    andStr += translateWholeNumber(points).Trim() + " Paise";

                }
            }
            val = String.Format("{0} {1}{2} {3}", translateWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
        }
        catch
        {
            ;
        }
        return val;
    }

    private String translateWholeNumber(String number)
    {
        string word = "";
        try
        {
            bool beginsZero = false;//tests for 0XX
            bool isDone = false;//test if already translate
            double dblAmt = (Convert.ToDouble(number));
            //if ((dblAmt > 0) && number.StartsWith("0"))

            if (dblAmt > 0)
            {//test for zero or digit zero in a nuemric
                beginsZero = number.StartsWith("0");
                int numDigits = number.Length;
                int pos = 0;//store digit grouping
                String place = "";//digit grouping name:hundres,thousand,etc...
                switch (numDigits)
                {
                    case 1://ones' range
                        word = ones(number);
                        isDone = true;
                        break;
                    case 2://tens' range
                        word = tens(number);
                        isDone = true;
                        break;
                    case 3://hundreds' range
                        pos = (numDigits % 3) + 1;
                        place = " Hundred ";
                        break;
                    case 4://thousands' range
                    case 5:
                        pos = (numDigits % 4) + 1;
                        place = " Thousand ";
                        break;
                    case 6:

                    case 7://millions' range
                        pos = (numDigits % 6) + 1;
                        // place = " Million ";
                        place = " Lakh ";
                        break;
                    case 8:
                    case 9:

                    case 10://Billions's range
                        pos = (numDigits % 8) + 1;
                        place = " Core ";
                        break;
                    //add extra case options for anything above Billion...
                    default:
                        isDone = true;
                        break;
                }
                if (!isDone)
                {//if transalation is not done, continue...(Recursion comes in now!!)
                    if (beginsZero) place = "";
                    word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos));
                    //check for trailing zeros
                    if (beginsZero) word = " and " + word.Trim();
                }
                //ignore digit grouping names
                if (word.Trim().Equals(place.Trim())) word = "";
            }
        }
        catch
        {
            ;
        }
        return word.Trim();
    }

    private String tens(String digit)
    {
        int digt = Convert.ToInt32(digit);
        String name = null;
        switch (digt)
        {
            case 10:
                name = "Ten";
                break;
            case 11:
                name = "Eleven";
                break;
            case 12:
                name = "Twelve";
                break;
            case 13:
                name = "Thirteen";
                break;
            case 14:
                name = "Fourteen";
                break;
            case 15:
                name = "Fifteen";
                break;
            case 16:
                name = "Sixteen";
                break;
            case 17:
                name = "Seventeen";
                break;
            case 18:
                name = "Eighteen";
                break;
            case 19:
                name = "Nineteen";
                break;
            case 20:
                name = "Twenty";
                break;
            case 30:
                name = "Thirty";
                break;
            case 40:
                name = "Fourty";
                break;
            case 50:
                name = "Fifty";
                break;
            case 60:
                name = "Sixty";
                break;
            case 70:
                name = "Seventy";
                break;
            case 80:
                name = "Eighty";
                break;
            case 90:
                name = "Ninety";
                break;
            default:
                if (digt > 0)
                {
                    name = tens(digit.Substring(0, 1) + "0") + " " + ones(digit.Substring(1));
                }
                break;
        }
        return name;
    }

    private String ones(String digit)
    {
        int digt = Convert.ToInt32(digit);
        String name = "";
        switch (digt)
        {
            case 1:
                name = "One";
                break;
            case 2:
                name = "Two";
                break;
            case 3:
                name = "Three";
                break;
            case 4:
                name = "Four";
                break;
            case 5:
                name = "Five";
                break;
            case 6:
                name = "Six";
                break;
            case 7:
                name = "Seven";
                break;
            case 8:
                name = "Eight";
                break;
            case 9:
                name = "Nine";
                break;
        }
        return name;
    }
    //end function

    public override string Alert(string icon, string AlertClass, string Heading, string Description)
    {
        return "<div class='box-body'> <div class='alert " + AlertClass + " alert-dismissible'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'><span id='count'></span> [�]</button><h4><i class='icon fa " + icon + "'></i>" + Heading + "</h4>" + Description + "</div> </div>";
    }

    public string createdBy()
    {
        if (HttpContext.Current.Session["Emp_ID"] == null)
        {
            return null;
        }
        else
        {
            return HttpContext.Current.Session["Emp_ID"].ToString();
        }
    }

    public string Office_ID()
    {
        if (HttpContext.Current.Session["Office_ID"] == null)
        {
            return null;
        }
        else
        {
            return HttpContext.Current.Session["Office_ID"].ToString();
        }
    }

    public string RetailerType_ID()
    {
        if (HttpContext.Current.Session["RetailerTypeID"] == null)
        {
            return null;
        }
        else
        {
            return HttpContext.Current.Session["RetailerTypeID"].ToString();
        }
    }



    public int GetHOId()
    {
        return 1;
    }

    public int GetHOType_Id()
    {
        return 1;
    }

    public int GetDCSType_Id() { return 6; }

    public int GetDSType_Id() { return 2; }

    public int GetLtrId() { return 3; }

    public string UserTypeID()
    {
        if (HttpContext.Current.Session["UserTypeId"] == null)
        {
            return null;
        }
        else
        {
            return HttpContext.Current.Session["UserTypeId"].ToString();
        }
    }

    public string Office_Name()
    {
        if (HttpContext.Current.Session["Office_Name"] == null)
        {
            return null;
        }
        else
        {
            return HttpContext.Current.Session["Office_Name"].ToString();
        }
    }

    public string OfficeType_ID()
    {
        if (HttpContext.Current.Session["OfficeType_ID"] == null)
        {
            return null;
        }
        else
        {
            return HttpContext.Current.Session["OfficeType_ID"].ToString();
        }
    }

    public string Emp_Name()
    {
        if (HttpContext.Current.Session["Emp_Name"] == null)
        {
            return null;
        }
        else
        {
            return HttpContext.Current.Session["Emp_Name"].ToString();
        }
    }

    public string Department_ID()
    {
        if (HttpContext.Current.Session["Department_ID"] == null)
        {
            return null;
        }
        else
        {
            return HttpContext.Current.Session["Department_ID"].ToString();
        }
    }

    public string Designation_ID()
    {
        if (HttpContext.Current.Session["Designation_ID"] == null)
        {
            return null;
        }
        else
        {
            return HttpContext.Current.Session["Designation_ID"].ToString();
        }
    }
    public string GetUserName()
    {
        if (HttpContext.Current.Session["UserName"] == null)
        {
            return null;
        }
        else
        {
            return HttpContext.Current.Session["UserName"].ToString();
        }
    }
    public string GetItemCat_id()
    {
        if (HttpContext.Current.Session["ItemCat_id"] == null)
        {
            return null;
        }
        else
        {
            return HttpContext.Current.Session["ItemCat_id"].ToString();
        }
    }
    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        String ipaddress = string.Empty;
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ipaddress = ip.ToString();
            }
        }
        return ipaddress.Length > 0 ? ipaddress : null;
    }
    public void redirectToHome()
    {
        HttpContext.Current.Response.Redirect("~/mis/Login.aspx");
    }
    public string GetMACAddress()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        String sMacAddress = string.Empty;
        foreach (NetworkInterface adapter in nics)
        {
            if (sMacAddress == String.Empty)// only return MAC Address from first card  
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                sMacAddress = adapter.GetPhysicalAddress().ToString();
            }
        } return sMacAddress;
    }

    public override string getdate()
    {
        DateTime CurrentDate = DateTime.Now;
        string CurrentDateDay = CurrentDate.Day <= 9 ? "0" + CurrentDate.Day.ToString() : CurrentDate.Day.ToString();
        string CurrentDateMonth = CurrentDate.Month <= 9 ? "0" + CurrentDate.Month.ToString() : CurrentDate.Month.ToString();
        return CurrentDateDay + "/" + CurrentDateMonth + "/" + CurrentDate.Year;
    }
    public override string getfy(string Date)
    {
        CultureInfo cult = new CultureInfo("gu-IN", true);
        string sDate = (Convert.ToDateTime(Date, cult).ToString("yyyy/MM/dd")).ToString();
        DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
        int Month = int.Parse(datevalue.Month.ToString());
        int Year = int.Parse(datevalue.Year.ToString());
        int FY = Year;
        string FinancialYear = Year.ToString();
        string LFY = FinancialYear.Substring(FinancialYear.Length - 2);
        FinancialYear = "";
        if (Month <= 3)
        {
            FY = Year - 1;
            FinancialYear = FY.ToString() + "-" + LFY.ToString();
        }
        else
        {

            FinancialYear = FY.ToString() + "-" + (int.Parse(LFY) + 1).ToString();
        }
        return FinancialYear;
    }
	
	public string SHA512_HASH(string rawData)
    {
        //Create a SHA512   
        using (SHA512 sha512Hash = SHA512.Create())
        {
            // ComputeHash - returns byte array  
            byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public bool CompaireHashCode(string DataBasePasswordWithSHA512, string ClientPasswordWithoutSHA512, string saltkey)
    {
        bool i;
        if (SHA512_HASH(String.Concat(DataBasePasswordWithSHA512, saltkey)).Equals(SHA512_HASH(String.Concat(SHA512_HASH(ClientPasswordWithoutSHA512), saltkey))))
        { i = true; }
        else { i = false; }
        return i;
    }

    public string GenerateSaltKey()
    {
        //Generate Salt Key
        StringBuilder randomText = new StringBuilder();
        string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz!@#$%&*~";
        Random r = new Random();
        for (int j = 0; j <= 10; j++)
        { randomText.Append(alphabets[r.Next(alphabets.Length)]); }
        return randomText.ToString();
    }

    // Created by Rajendra check valid Number 
    public override bool isNumber(string s)
    {
        int num;
        return Int32.TryParse(s, out num);
    }
    // Created by Rajendra check valid Decimal 
    public override bool isDecimal(string s)
    {
        int num;
        double num1;
        if (Int32.TryParse(s, out num))
        {
            return Int32.TryParse(s, out num);
        }
        else
        {
            return double.TryParse(s, out num1);
        }
    }

    public string GetItemId()
    {
        throw new NotImplementedException();
    }

    public decimal GetFileSize()
    {
        throw new NotImplementedException();
    }
	public string Route_ID()
    {
        if (HttpContext.Current.Session["RouteId"] == null)
        {
            return null;
        }
        else
        {
            return HttpContext.Current.Session["RouteId"].ToString();
        }
    }
	
	public string DcsRawMilkItemCategoryId_ID() { return "3"; }
    public string DcsRawMilkItemTypeId_ID() { return "47"; }
	public string DcsRawMilkItemId_ID() { return "80"; }
	
	
	public string GetCrateItem_Id() { return "146"; }

    public string GetCrateType_Id() { return "86"; }

    public string GetCrateItemCat_Id() { return "1"; }
	
	
	public string SkimmedMilkItemCategoryId_ID() { return "3"; }
    public string SkimmedMilkItemTypeId_ID() { return "87"; }
    public string SkimmedMilkItemId_ID() { return "149"; }

    public string WholeMilkItemCategoryId_ID() { return "3"; }
    public string WholeMilkItemTypeId_ID() { return "92"; }
    public string WholeMilkItemId_ID() { return "222"; }
	
	public string ProductionRawMaterialCategoryId_ID() { return "10"; }
    public string ProductionRawMaterialType_ID() { return "91"; }
	
	public string ChakkeMilkItemCategoryId_ID() { return "2"; }
    public string ChakkeMilkItemTypeId_ID() { return "93"; }
    public string ChakkeMilkItemId_ID() { return "228"; }
	
	
	public string LooseGheeItemCategoryId_ID() { return "2"; }
    public string LooseGheeItemTypeId_ID() { return "34"; } 
    public string LooseCreamItemCategoryId_ID() { return "11"; }
    public string LooseCreamItemTypeId_ID() { return "103"; }
    public string LooseButterItemCategoryId_ID() { return "11"; }
    public string LooseButterItemTypeId_ID() { return "104"; } 
	public string LooseSMPItemCategoryId_ID() { return "11"; }
    public string LooseSMPItemTypeId_ID() { return "107"; }
	
	public string CowMilkItemCategoryId_ID() { return "11"; }
    public string CowMilkItemTypeId_ID() { return "128"; }
	
	public string GetInstRetailerTypeId() { return "3"; }
	public string GetProductCatId() { return "2"; }
    public string GetMilkCatId() { return "3"; }
    public string GetShiftMorId() { return "1"; }
    public string GetShiftEveId() { return "2"; }
    public string GetShiftMorningName() { return "Morning"; }
    public string GetShiftEveName() { return "Evening"; }
    public string GetMilkCategoryName() { return "Milk"; }
    public string GetProductCategoryName() { return "Product"; }
}




