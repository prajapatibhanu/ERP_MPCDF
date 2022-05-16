using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Reflection;
using System.Net;
using System.IO;

public partial class Login : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure apiprocedure = new APIProcedure();
    CommanddlFill commanddlFill = new CommanddlFill();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session.Abandon();
            Session.RemoveAll();
            GetRandomText();
        }
    }

    #region -- User Defined Function --

    void Re_GenerateSessoin_ID()
    {
        System.Web.SessionState.SessionIDManager manager = new System.Web.SessionState.SessionIDManager();
        string oldId = manager.GetSessionID(Context);
        string newId = manager.CreateSessionID(Context);
        bool isAdd = false, isRedir = false;
        manager.SaveSessionID(Context, newId, out isRedir, out isAdd);
        HttpApplication ctx = (HttpApplication)HttpContext.Current.ApplicationInstance;
        HttpModuleCollection mods = ctx.Modules;
        System.Web.SessionState.SessionStateModule ssm = (SessionStateModule)mods.Get("Session");
        System.Reflection.FieldInfo[] fields = ssm.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        SessionStateStoreProviderBase store = null;
        System.Reflection.FieldInfo rqIdField = null, rqLockIdField = null, rqStateNotFoundField = null;
        foreach (System.Reflection.FieldInfo field in fields)
        {
            if (field.Name.Equals("_store")) store = (SessionStateStoreProviderBase)field.GetValue(ssm);
            if (field.Name.Equals("_rqId")) rqIdField = field;
            if (field.Name.Equals("_rqLockId")) rqLockIdField = field;
            if (field.Name.Equals("_rqSessionStateNotFound")) rqStateNotFoundField = field;
        }
        object lockId = rqLockIdField.GetValue(ssm);
        if ((lockId != null) && (oldId != null)) store.ReleaseItemExclusive(Context, oldId, lockId);
        rqStateNotFoundField.SetValue(ssm, true);
        Session["Session_id"] = newId;
        rqIdField.SetValue(ssm, newId);
    }

    public void GetRandomText()
    {
        StringBuilder randomText = new StringBuilder();
        string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz!@#$%&*~";
        Random r = new Random();
        for (int j = 0; j <= 10; j++)
        { randomText.Append(alphabets[r.Next(alphabets.Length)]); }
        ViewState["RandomText"] = apiprocedure.Encrypt(randomText.ToString());
    }

    private string ConvertText_SHA512_And_Salt(string Text)
    {
        return apiprocedure.SHA512_HASH(String.Concat(apiprocedure.SHA512_HASH(Text), ViewState["RandomText"].ToString()));
    }

    private bool CompaireHashCode(string DataBasePassword, string ClientPasswordWithHashing)
    {
        bool i;
        if (apiprocedure.SHA512_HASH(String.Concat(DataBasePassword, ViewState["RandomText"].ToString())).Equals(ClientPasswordWithHashing))
        { i = true; }
        else { i = false; }
        return i;
    }

    public int SendOTP(string MobileNo)
    {
        int status = 0;
        try
        {
            Random random = new Random();
            string otp = random.Next(100000, 999999).ToString();
            ViewState["otp"] = otp;

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
            status = 1;
        }
        catch (Exception)
        {
            status = 0;
        }
        return status;
    }

    #endregion

    #region -- Button Events --

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                if (txtUserName.Text != "" && txtUserPassword.Text != "")
                {
                    switch (txtUserName.Text.Substring(0, 1))
                    {
                        case "E": //for Employees Login offices like HO, DS, DCS, CC, BMC, MDP 
                            ds = apiprocedure.ByProcedure("sp_Login",
                                     new string[] { "flag", "UserName" },
                                     new string[] { "0", txtUserName.Text }, "dataset");
                            break;
                        case "P": //for Producer Login
                            ds = apiprocedure.ByProcedure("SptblPUProducerReg",
                                    new string[] { "flag", "UserName" },
                                    new string[] { "0", txtUserName.Text }, "dataset");
                            break;
                        case "D": //for Distributor Login
                            ds = apiprocedure.ByProcedure("USP_Mst_DistributorReg",
                                    new string[] { "flag", "UserName" },
                                    new string[] { "0", txtUserName.Text }, "dataset");
                            break;
                        case "S": //for Super Stockist Login
                            ds = apiprocedure.ByProcedure("USP_Mst_SuperStockistReg",
                                    new string[] { "flag", "UserName" },
                                    new string[] { "0", txtUserName.Text }, "dataset");
                            break;
                        case "B": //for Booth/Parlour Login
                            ds = apiprocedure.ByProcedure("USP_Mst_BoothReg",
                                    new string[] { "flag", "UserName" },
                                    new string[] { "0", txtUserName.Text }, "dataset");
                            break;
                        case "M": //for Temp Employees Login of offices like HO, DS, DCS, CC, BMC, MDP 
                            ds = apiprocedure.ByProcedure("usp_Mst_McuUserMaster",
                                    new string[] { "flag", "UserName" },
                                    new string[] { "0", txtUserName.Text }, "dataset");
                            break;
                        default:
                            if (ds != null)
                            {
                                ds.Clear();
                            }
                            break;
                    }

                    if (ds.Tables[0].Rows.Count > 0 && ds != null)
                    {
                        if (CompaireHashCode(ds.Tables[0].Rows[0]["Password"].ToString(), txtUserPassword.Text))
                        {
                            //CheckRemember();
                            Re_GenerateSessoin_ID();
                            GetRandomText();
                            LblMsg.Text = "";

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["IsActive"].ToString() == "1" || ds.Tables[0].Rows[0]["IsActive"].ToString() == "True")
                                {
                                    switch (ds.Tables[0].Rows[0]["UserName"].ToString().Substring(0, 1))
                                    {
                                        case "E": //for Employees Login offices like HO, DS, DCS, CC, BMC, MDP 
                                            Session["Emp_ID"] = ds.Tables[0].Rows[0]["Emp_ID"].ToString();
                                            Session["Office_ID"] = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                                            Session["Office_Name"] = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                                            Session["OfficeType_ID"] = ds.Tables[0].Rows[0]["OfficeType_ID"].ToString();
                                            Session["UserTypeId"] = ds.Tables[0].Rows[0]["UserTypeId"].ToString();
                                            Session["Emp_Name"] = ds.Tables[0].Rows[0]["Emp_Name"].ToString();
                                            Session["Department_ID"] = ds.Tables[0].Rows[0]["Department_ID"].ToString();
                                            Session["Designation_ID"] = ds.Tables[0].Rows[0]["Designation_ID"].ToString();
                                            Session["UserName"] = ds.Tables[0].Rows[0]["UserName"].ToString();
											Session["Office_Code"] = ds.Tables[0].Rows[0]["Office_Code"].ToString();
											Session["Office_FinAddress"] = ds.Tables[0].Rows[0]["Office_FinAddress"].ToString();
											
                                            GetAccess(Session["Emp_ID"].ToString(), Session["OfficeType_ID"].ToString());
											 Session["Office_Gst"] = ds.Tables[0].Rows[0]["Office_Gst"].ToString();
                                            //Response.Redirect("Dashboard/Home.aspx?IsMainPage=1");
											 if (Session["UserName"].ToString() == "E0001")
                                            {
                                                Response.Redirect("Dashboard/UnionWiseProgressReport.aspx?IsMainPage=1");
                                            }
                                            else
                                            {
                                                Response.Redirect("Dashboard/Home.aspx?IsMainPage=1");
                                            }
                                            break;
                                        case "P": //for Producer Login
                                            Session["Emp_ID"] = ds.Tables[0].Rows[0]["ProducerId"].ToString();
                                            Session["Office_ID"] = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                                            Session["Office_Name"] = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                                            Session["OfficeType_ID"] = ds.Tables[0].Rows[0]["OfficeType_ID"].ToString();
                                            Session["UserTypeId"] = ds.Tables[0].Rows[0]["UserTypeId"].ToString();
                                            Session["Emp_Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                                            Session["UserName"] = ds.Tables[0].Rows[0]["UserName"].ToString();
											
											
                                            GetAccess(Session["Emp_ID"].ToString(), Session["OfficeType_ID"].ToString());
                                            Response.Redirect("Dashboard/Home.aspx?IsMainPage=1");
                                            break;
                                        case "D": //for Distributor Login
                                            Session["Emp_ID"] = ds.Tables[0].Rows[0]["DistributorId"].ToString();
                                            Session["Office_ID"] = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                                            Session["Office_Name"] = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                                            Session["OfficeType_ID"] = ds.Tables[0].Rows[0]["OfficeType_ID"].ToString();
                                            Session["UserTypeId"] = ds.Tables[0].Rows[0]["UserTypeId"].ToString();
                                            Session["Emp_Name"] = ds.Tables[0].Rows[0]["DName"].ToString();
                                            Session["UserName"] = ds.Tables[0].Rows[0]["UserName"].ToString();
											Session["ItemCat_id"] = ds.Tables[0].Rows[0]["ItemCat_id"].ToString();
											
											
                                            GetAccess(Session["Emp_ID"].ToString(), Session["OfficeType_ID"].ToString());
                                            Response.Redirect("Dashboard/Home.aspx?IsMainPage=1");
                                            break;
                                        case "S": //for Super Stockist Login
                                            Session["Emp_ID"] = ds.Tables[0].Rows[0]["SuperStockistId"].ToString();
                                            Session["Office_ID"] = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                                            Session["Office_Name"] = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                                            Session["OfficeType_ID"] = ds.Tables[0].Rows[0]["OfficeType_ID"].ToString();
                                            Session["UserTypeId"] = ds.Tables[0].Rows[0]["UserTypeId"].ToString();
                                            Session["Emp_Name"] = ds.Tables[0].Rows[0]["SSName"].ToString();
                                            Session["UserName"] = ds.Tables[0].Rows[0]["UserName"].ToString();
											 Session["ItemCat_id"] = ds.Tables[0].Rows[0]["ItemCat_id"].ToString();
											 
											 
                                            GetAccess(Session["Emp_ID"].ToString(), Session["OfficeType_ID"].ToString());
                                            Response.Redirect("Dashboard/Home.aspx?IsMainPage=1");
                                            break;
                                        case "B": //for Booth/Parlour Login
                                            Session["Emp_ID"] = ds.Tables[0].Rows[0]["BoothId"].ToString();
                                            Session["Office_ID"] = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                                            Session["Office_Name"] = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                                            Session["OfficeType_ID"] = ds.Tables[0].Rows[0]["OfficeType_ID"].ToString();
                                            Session["UserTypeId"] = ds.Tables[0].Rows[0]["UserTypeId"].ToString();
                                            Session["Emp_Name"] = ds.Tables[0].Rows[0]["BName"].ToString();
                                            Session["UserName"] = ds.Tables[0].Rows[0]["UserName"].ToString();
                                            Session["RetailerTypeID"] = ds.Tables[0].Rows[0]["RetailerTypeID"].ToString();
											Session["RouteId"] = ds.Tables[0].Rows[0]["RouteId"].ToString();
											

                                            GetAccess(Session["Emp_ID"].ToString(), Session["OfficeType_ID"].ToString());
                                            Response.Redirect("Dashboard/Home.aspx?IsMainPage=1");
                                            break;
                                        case "M": //for temp Employees Login offices like HO, DS, DCS, CC, BMC, MDP 
                                            Session["Emp_ID"] = ds.Tables[0].Rows[0]["UserId"].ToString();
                                            Session["Office_ID"] = ds.Tables[0].Rows[0]["Office_ID"].ToString();
                                            Session["Office_Name"] = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                                            Session["OfficeType_ID"] = ds.Tables[0].Rows[0]["OfficeType_ID"].ToString();
                                            Session["UserTypeId"] = ds.Tables[0].Rows[0]["UserTypeId"].ToString();
                                            Session["Emp_Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                                            Session["UserName"] = ds.Tables[0].Rows[0]["UserName"].ToString();
											

                                            GetAccess(Session["Emp_ID"].ToString(), Session["OfficeType_ID"].ToString());
                                            Response.Redirect("Dashboard/Home.aspx?IsMainPage=1");
                                            break;
                                        default:
                                            if (ds != null) { ds.Clear(); }
                                            break;
                                    }
                                }
                                else
                                {
                                    LblMsg.ForeColor = System.Drawing.Color.Red;
                                    LblMsg.Text = "Access denied! Kindly contact administrator.";
                                }
                            }
                            else
                            {
                                LblMsg.ForeColor = System.Drawing.Color.Red;
                                LblMsg.Text = "Invalid Login Credentials";
                            }
                        }
                        else
                        {
                            LblMsg.ForeColor = System.Drawing.Color.Red;
                            LblMsg.Text = "Invalid Login Credentials!";
                        }
                    }
                    else
                    {
                        LblMsg.ForeColor = System.Drawing.Color.Red;
                        LblMsg.Text = "Invalid Login Credentials!";
                    }
                    txtUserName.Text = "";
                    txtUserPassword.Attributes["value"] = "";
                }
                else
                {
                    LblMsg.ForeColor = System.Drawing.Color.Red;
                    LblMsg.Text = "Invalid Login Credentials";
                }
            }
            catch (Exception ex)
            {
                LblMsg.ForeColor = System.Drawing.Color.Red;
                LblMsg.Text = ex.Message.ToString();
                btnLogin.Enabled = true;
            }
        }
    }

    private void GetAccess(string Emp_ID, string UserTypeId)
    {
        //*********** FORM ACCESS VALIDATION *************
        DataSet FormPath = apiprocedure.ByProcedure("SpUMHome",
            new string[] { "flag", "Emp_ID" },
        new string[] { "3", Emp_ID.ToString() }, "dataset");

        Session["FormPath"] = FormPath;
		
		string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
        string IPadd2 = Request.UserHostAddress;
        apiprocedure.ByProcedure("SpLogDetail",
        new string[] { "flag", "Emp_ID", "User_Name", "Office_ID", "IPAddress" },
        new string[] { "0", Session["Emp_ID"].ToString(), Session["UserName"].ToString(), Session["Office_ID"].ToString(), IPAddress }, "dataset");

    }

    protected void lnkForget_Click(object sender, EventArgs e)
    {
        try
        {
            LblMsg.Text = "";
            login.Visible = false;
            txtUserName.Text = "";
            txtMobileNo.Text = "";
            txtUserPassword.Text = "";
            ForgetPassword.Visible = true;
            forget.Visible = true;
            btnContinue.Visible = true;
            btnValidate.Visible = false;
            SetFocus(txtMobileNo);
        }
        catch (Exception ex)
        {
            LblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void lnkbtnBack_Click(object sender, EventArgs e)
    {
        try
        {
            LblMsg.Text = "";
            login.Visible = true;
            ForgetPassword.Visible = false;
            ChangePass.Visible = false;
            btnSave.Visible = false;
            forget.Visible = false;
            otp.Visible = false;
            ViewState["otp"] = null;
        }
        catch (Exception ex)
        {
            LblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnValidate.Text == "Validate")
            {
                //ds = apiprocedure.ByProcedure("sp_Login",
                //        new string[] { "flag", "Emp_MobileNo" },
                //        new string[] { "6", ViewState["mobile"].ToString() }, "dataset");

                if (txtOtp.Text == ViewState["otp"].ToString())
                {
                    LblMsg.Text = "";
                    ChangePass.Visible = true;
                    btnSave.Visible = true;
                    otp.Visible = false;
                    txtOtp.Text = "";
                    forget.Visible = false;
                    btnValidate.Visible = false;
                }
                else
                {
                    LblMsg.ForeColor = System.Drawing.Color.Red;
                    LblMsg.Text = "<b>Invalid OTP!</b><br /><br />";
                }
            }
        }
        catch (Exception ex)
        {
            LblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnContinue_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (btnContinue.Text == "Continue")
                {
                    switch (txtMobileNo.Text.Substring(0, 1))
                    {
                        case "E": //for Employees Login offices like HO, DS, DCS, CC, BMC, MDP 
                            ds = apiprocedure.ByProcedure("sp_Login",
                                     new string[] { "flag", "UserName" },
                                     new string[] { "0", txtMobileNo.Text }, "dataset");
                            break;
                        case "P": //for Producer Login
                            ds = apiprocedure.ByProcedure("SptblPUProducerReg",
                                    new string[] { "flag", "UserName" },
                                    new string[] { "0", txtMobileNo.Text }, "dataset");
                            break;
                        case "D": //for Distributor Login
                            ds = apiprocedure.ByProcedure("USP_Mst_DistributorReg",
                                    new string[] { "flag", "UserName" },
                                    new string[] { "0", txtMobileNo.Text }, "dataset");
                            break;
                        case "S": //for Super Stockist Login
                            ds = apiprocedure.ByProcedure("USP_Mst_SuperStockistReg",
                                    new string[] { "flag", "UserName", },
                                    new string[] { "0", txtMobileNo.Text }, "dataset");
                            break;
                        case "B": //for Booth/Parlour Login
                            ds = apiprocedure.ByProcedure("USP_Mst_BoothReg",
                                    new string[] { "flag", "UserName" },
                                    new string[] { "0", txtMobileNo.Text }, "dataset");
                            break;
                        case "M": //for Temp Employees Login of offices like HO, DS, DCS, CC, BMC, MDP 
                            ds = apiprocedure.ByProcedure("usp_Mst_McuUserMaster",
                                    new string[] { "flag", "UserName" },
                                    new string[] { "0", txtMobileNo.Text }, "dataset");
                            break;
                        default:
                            if (ds != null)
                            {
                                ds.Clear();
                            }
                            break;
                    }

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ViewState["UserName"] = ds.Tables[0].Rows[0]["UserName"].ToString();
                        ViewState["mobile"] = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        SendOTP(ViewState["mobile"].ToString());

                        LblMsg.ForeColor = System.Drawing.Color.Green;
                        LblMsg.Text = "<b>OTP sent to your registered Mobile No.</b><br /><br />";
                        forget.Visible = false;
                        otp.Visible = true;
                        SetFocus(txtOtp);
                        btnContinue.Visible = false;
                        btnValidate.Visible = true;

                    }
                    else
                    {
                        LblMsg.ForeColor = System.Drawing.Color.Red;
                        LblMsg.Text = "<b>Your Mobile No. is not registered with us.<b/><br /><br />";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            LblMsg.Text = "";
            if (Page.IsValid)
            {
                switch (apiprocedure.GetUserName().Substring(0, 1))
                {
                    case "E": //for Employees Login offices like HO, DS, DCS, CC, BMC, MDP 
                        ds = apiprocedure.ByProcedure("sp_Login",
                                 new string[] { "flag", "UserName" },
                                 new string[] { "0", ViewState["UserName"].ToString() }, "dataset");
                        break;
                    case "P": //for Producer Login
                        ds = apiprocedure.ByProcedure("SptblPUProducerReg",
                                new string[] { "flag", "UserName" },
                                new string[] { "0", ViewState["UserName"].ToString() }, "dataset");
                        break;
                    case "D": //for Distributor Login
                        ds = apiprocedure.ByProcedure("USP_Mst_DistributorReg",
                                new string[] { "flag", "UserName" },
                                new string[] { "0", ViewState["UserName"].ToString() }, "dataset");
                        break;
                    case "S": //for Super Stockist Login
                        ds = apiprocedure.ByProcedure("USP_Mst_SuperStockistReg",
                                new string[] { "flag", "UserName", },
                                new string[] { "0", ViewState["UserName"].ToString() }, "dataset");
                        break;
                    case "B": //for Booth/Parlour Login
                        ds = apiprocedure.ByProcedure("USP_Mst_BoothReg",
                                new string[] { "flag", "UserName" },
                                new string[] { "0", ViewState["UserName"].ToString() }, "dataset");
                        break;
                    case "M": //for Temp Employees Login of offices like HO, DS, DCS, CC, BMC, MDP 
                        ds = apiprocedure.ByProcedure("usp_Mst_McuUserMaster",
                                new string[] { "flag", "UserName" },
                                new string[] { "0", txtUserName.Text }, "dataset");
                        break;
                    default:
                        if (ds != null)
                        {
                            ds.Clear();
                        }
                        break;
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (txtConfirmPassword.Text != "")
                    {
                        switch (ds.Tables[0].Rows[0]["UserName"].ToString().Substring(0, 1))
                        {
                            case "E": //for Employees Login offices like HO, DS, DCS, CC, BMC, MDP 
                                ds = apiprocedure.ByProcedure("sp_Login",
                                         new string[] { "flag", "UserName", "Password" },
                                         new string[] { "3", ds.Tables[0].Rows[0]["UserName"].ToString(), txtConfirmPassword.Text }, "dataset");
                                break;
                            case "P": //for Producer Login
                                ds = apiprocedure.ByProcedure("SptblPUProducerReg",
                                        new string[] { "flag", "UserName", "Password" },
                                        new string[] { "9", ds.Tables[0].Rows[0]["UserName"].ToString(), txtConfirmPassword.Text }, "dataset");
                                break;
                            case "D": //for Distributor Login
                                ds = apiprocedure.ByProcedure("USP_Mst_DistributorReg",
                                        new string[] { "flag", "UserName", "Password" },
                                        new string[] { "5", ds.Tables[0].Rows[0]["UserName"].ToString(), txtConfirmPassword.Text }, "dataset");
                                break;
                            case "S": //for Super Stockist Login
                                ds = apiprocedure.ByProcedure("USP_Mst_SuperStockistReg",
                                        new string[] { "flag", "UserName", "Password" },
                                        new string[] { "6", ds.Tables[0].Rows[0]["UserName"].ToString(), txtConfirmPassword.Text }, "dataset");
                                break;
                            case "B": //for Booth/Parlour Login
                                ds = apiprocedure.ByProcedure("USP_Mst_BoothReg",
                                        new string[] { "flag", "UserName", "Password" },
                                        new string[] { "5", ds.Tables[0].Rows[0]["UserName"].ToString(), txtConfirmPassword.Text }, "dataset");
                                break;
                            case "M": //for Temp Employees Login offices like HO, DS, DCS, CC, BMC, MDP 
                                ds = apiprocedure.ByProcedure("usp_Mst_McuUserMaster",
                                        new string[] { "flag", "UserName", "Password" },
                                        new string[] { "4", ds.Tables[0].Rows[0]["UserName"].ToString(), txtConfirmPassword.Text }, "dataset");
                                break;
                            default:
                                if (ds != null)
                                {
                                    ds.Clear();
                                }
                                break;
                        }

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                LblMsg.ForeColor = System.Drawing.Color.Green;
                                LblMsg.Text = "<b>Password Change Successfully.</b><br />";
                            }
                            else
                            {
                                LblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Password Change Failed!");
                            }
                        }
                        else
                        {
                            LblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Invalid Credentials");
                        }

                        otp.Visible = false;
                        txtOtp.Text = "";
                        txtConfirmPassword.Text = "";
                        txtNewPassword.Text = "";
                        forget.Visible = false;
                        btnValidate.Visible = false;
                        ChangePass.Visible = false;
                        btnSave.Visible = false;

                        ViewState["mobile"] = null;
                        ViewState["otp"] = null;
                        ds.Clear();
                    }
                    else
                    {
                        LblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Ooops!", "New and Confirm Password not Match!");
                    }
                }
                else
                {
                    LblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Ooops!", "Old Password not Match, Try Again!");
                    ds.Clear();
                }
            }
        }
        catch (Exception ex)
        {
            LblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    #endregion
}