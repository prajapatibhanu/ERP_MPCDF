using System;
using System.Web.UI;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Mail;

public partial class mis_ChangePassword : System.Web.UI.Page
{
    DataSet ds, ds3;
    APIProcedure apiprocedure = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                GetRandomText();

                txtOTP.Text = "";
                txtNewPassword.Text = "";
                txtOldPassword.Text = "";
                txtConfirmPassword.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
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

    private bool CompaireHashCode(string DataBasePassword, string ClientPasswordWithHashing)
    {
        bool i;
        if (apiprocedure.SHA512_HASH(String.Concat(DataBasePassword,
            ViewState["RandomText"].ToString())).Equals(ClientPasswordWithHashing))
        { i = true; }
        else { i = false; }
        return i;
    }

    //public string SendOTP(string MobileNo)
    //{
    //    Random random = new Random();
    //    string otp = random.Next(100000, 999999).ToString();

    //    string txtmsg = "Your One Time Password is : " + otp;

    //    //Your authentication key
    //    string authKey = "3597C1493C124F";

    //    //Sender ID
    //    string senderId = "SANCHI";

    //    string link = "http://mysms.mssinfotech.com/app/smsapi/index.php?key=" + authKey + "&type=unicode&routeid=288&contacts=" + MobileNo + "&senderid=" + senderId + "&msg=" + Server.UrlEncode(txtmsg);

    //    HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
    //    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
    //    Stream stream = response.GetResponseStream();

    //    //End Sending OTP SMS
    //    return otp;
    //}
    public string SendOTP(string Email)
    {
        Random random = new Random();
        string otp = random.Next(100000, 999999).ToString();

        string txtmsg = "Your One Time Password is : " + otp;
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("carempcdf@gmail.com");
        
        mail.To.Add(Email);
        mail.Subject = "Change Password OTP";
        mail.IsBodyHtml = true;

        mail.Body = "Dear" + " " + Session["Emp_Name"] + ",<br/><br/><br/>" + "Your Change Password OTP is   " + otp + "<br/><br/><br/>" + "This is a system generated e-mail and please do not reply.";
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
        SmtpServer.Credentials = new System.Net.NetworkCredential("carempcdf@gmail.com", "sfa@2365");

        SmtpServer.EnableSsl = true;
        SmtpServer.Send(mail);
        
        return otp;
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (Page.IsValid)
            {
                if (txtOTP.Text == ViewState["otp"].ToString())
                {
                    switch (apiprocedure.GetUserName().Substring(0, 1))
                    {
                        case "E": //for Employees Login offices like HO, DS, DCS, CC, BMC, MDP 
                            ds = apiprocedure.ByProcedure("sp_Login",
                                     new string[] { "flag", "UserName", "Password" },
                                     new string[] { "3", apiprocedure.GetUserName().ToString(), ViewState["Password"].ToString() }, "dataset");
                            break;
                        case "P": //for Producer Login
                            ds = apiprocedure.ByProcedure("SptblPUProducerReg",
                                    new string[] { "flag", "UserName", "Password" },
                                    new string[] { "9", apiprocedure.GetUserName().ToString(), ViewState["Password"].ToString() }, "dataset");
                            break;
                        case "D": //for Distributor Login
                            ds = apiprocedure.ByProcedure("USP_Mst_DistributorReg",
                                    new string[] { "flag", "UserName", "Password" },
                                    new string[] { "5", apiprocedure.GetUserName().ToString(), ViewState["Password"].ToString() }, "dataset");
                            break;
                        case "S": //for Sub-Distributor Login
                            ds = apiprocedure.ByProcedure("USP_Mst_SubDistributorReg",
                                    new string[] { "flag", "UserName", "Password" },
                                    new string[] { "6", apiprocedure.GetUserName().ToString(), ViewState["Password"].ToString() }, "dataset");
                            break;
                        case "B": //for Booth/Parlour Login
                            ds = apiprocedure.ByProcedure("USP_Mst_BoothReg",
                                    new string[] { "flag", "UserName", "Password" },
                                    new string[] { "5", apiprocedure.GetUserName().ToString(), ViewState["Password"].ToString() }, "dataset");
                            break;
                        case "M": //for Temp Employees Login offices like HO, DS, DCS, CC, BMC, MDP 
                            ds = apiprocedure.ByProcedure("usp_Mst_McuUserMaster",
                                    new string[] { "flag", "UserName", "Password" },
                                    new string[] { "4", apiprocedure.GetUserName().ToString(), ViewState["Password"].ToString() }, "dataset");
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
                            txtOTP.Text = "";
                            dv_otpSection.Visible = false;
                            dv_PasswordSection.Visible = true;
                            btnGenerateOtp.Visible = true;
                            btnCancel.Visible = true;

                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Password Changed Successfully.,');", true);
                            //lblMsg.Text = apiprocedure.Alert("fa-success", "alert-success", "Success!", "Password Changed!");
                        }
                        else
                        {
                            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Password Change Failed!");
                        }
                    }
                    else
                    {
                        lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Invalid Credentials");
                    }
                }
                else
                {
                    lblMsg.Text = apiprocedure.Alert("fa-info", "alert-info", "Info!", "Invalid OTP!");
                    txtOTP.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        txtOldPassword.Text = "";
        txtNewPassword.Text = "";
        txtConfirmPassword.Text = "";
    }

    protected void btnGenerateOtp_Click(object sender, EventArgs e)
    {
        if (txtOldPassword.Text != "" && txtNewPassword.Text != "" && txtConfirmPassword.Text != "")
        {
            switch (apiprocedure.GetUserName().Substring(0, 1))
            {
                case "E": //for Employees Login offices like HO, DS, DCS, CC, BMC, MDP 
                    ds3 = apiprocedure.ByProcedure("sp_Login",
                             new string[] { "flag", "UserName" },
                             new string[] { "0", apiprocedure.GetUserName().ToString() }, "dataset");
                    break;
                case "P": //for Producer Login
                    ds3 = apiprocedure.ByProcedure("SptblPUProducerReg",
                            new string[] { "flag", "UserName" },
                            new string[] { "0", apiprocedure.GetUserName().ToString() }, "dataset");
                    break;
                case "D": //for Distributor Login
                    ds3 = apiprocedure.ByProcedure("USP_Mst_DistributorReg",
                            new string[] { "flag", "UserName" },
                            new string[] { "0", apiprocedure.GetUserName().ToString() }, "dataset");
                    break;
                case "S": //for Sub-Distributor Login
                    ds3 = apiprocedure.ByProcedure("USP_Mst_SubDistributorReg",
                            new string[] { "flag", "UserName", },
                            new string[] { "0", apiprocedure.GetUserName().ToString() }, "dataset");
                    break;
                case "B": //for Booth/Parlour Login
                    ds3 = apiprocedure.ByProcedure("USP_Mst_BoothReg",
                            new string[] { "flag", "UserName" },
                            new string[] { "0", apiprocedure.GetUserName().ToString() }, "dataset");
                    break;
                case "M": //for Temp Employees Login of offices like HO, DS, DCS, CC, BMC, MDP 
                    ds = apiprocedure.ByProcedure("usp_Mst_McuUserMaster",
                            new string[] { "flag", "UserName" },
                            new string[] { "0", apiprocedure.GetUserName().ToString() }, "dataset");
                    break;
                default:
                    if (ds3 != null)
                    {
                        ds3.Clear();
                    }
                    break;
            }

            if (ds3.Tables[0].Rows.Count > 0 || ds3 != null)
            {
                if (CompaireHashCode(ds3.Tables[0].Rows[0]["Password"].ToString(), txtOldPassword.Text))
                {
                    GetRandomText();
                    lblMsg.Text = "";

                    if (ds3.Tables[0].Rows[0]["MobileNo"].ToString() != "")
                    {
                        //ViewState["otp"] = SendOTP(ds3.Tables[0].Rows[0]["MobileNo"].ToString());
                        ViewState["otp"] = SendOTP(ds3.Tables[0].Rows[0]["Email"].ToString());
                        ViewState["Password"] = txtConfirmPassword.Text;

                        dv_otpSection.Visible = true;
                        dv_PasswordSection.Visible = false;
                        btnGenerateOtp.Visible = false;
                        btnCancel.Visible = false;

                        txtOTP.Focus();
                    }
                    else
                    {
                        //lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Warning!", "Mobile Number not found kindly register first!");

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Mobile Number not found kindly register first!');", true);
                    }
                }
                else
                {
                    //lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Invalid Credentials");

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Invalid Credentials');", true);

                    dv_otpSection.Visible = false;
                    dv_PasswordSection.Visible = true;
                    btnGenerateOtp.Visible = true;
                    btnCancel.Visible = true;
                }
            }
        }
        else
        {
            //lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Invalid Credentials");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Invalid Credentials');", true);
        }
    }
}