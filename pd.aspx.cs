using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Script.Services;
using System.Web.Services;
using System.Drawing;
using QRCoder;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net;


public partial class pd : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    IFormatProvider cult = new CultureInfo("en-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetProducerData();
        }
    }


    private string LongURL(string strURL)
    {

        //string URL;
        //URL = "http://tinyurl.com/api-create.php?url=" +
        //   strURL.ToLower(); 
        System.Net.HttpWebRequest objWebRequest;
        System.Net.HttpWebResponse objWebResponse;
        System.IO.StreamReader srReader;
        string strHTML;
        objWebRequest = (System.Net.HttpWebRequest)System.Net
           .WebRequest.Create(strURL);
        objWebRequest.Method = "Get";
        objWebResponse = (System.Net.HttpWebResponse)objWebRequest
           .GetResponse();
        srReader = new System.IO.StreamReader(objWebResponse
           .GetResponseStream());
        strHTML = objWebResponse.ResponseUri.ToString();
        // strHTML = srReader.ReadToEnd(); 
        srReader.Close();
        objWebResponse.Close();
        objWebRequest.Abort();
        return (strHTML);
    }

    public void GetProducerData()
    {

        try
        {
            if (objdb.Decrypt(Request.QueryString["P_Id"]) != null)
            {
                string P_Id = objdb.Decrypt(Request.QueryString["P_Id"].ToString());


                ds = objdb.ByProcedure("SpProducerMaster",
                          new string[] { "flag", "ProducerId" },
                          new string[] { "11", P_Id }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string QR = null;
                        string dcsid = ds.Tables[0].Rows[0]["DCSId"].ToString();
                        DataSet ds1 = objdb.ByProcedure("SpProducerMaster",
                                            new string[] { "flag", "DCSId" },
                                            new string[] { "12", dcsid }, "dataset");
                        if (ds1.Tables.Count > 0)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                lblds.Text = ds1.Tables[0].Rows[0]["DS"].ToString();
                                lblSociety.Text = ds1.Tables[1].Rows[0]["DCS"].ToString();
                                lblDS1.Text = ds1.Tables[0].Rows[0]["DS"].ToString();
                            }
                        }
                        lblProducer.Text = ds.Tables[0].Rows[0]["ProducerName"].ToString();
                        lblSociety.Text = ds.Tables[0].Rows[0]["SocietyName"].ToString();
                        lblBlock.Text = ds.Tables[0].Rows[0]["Block_Name"].ToString();
                        lblMobile.Text = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        lblCode.Text = ds.Tables[0].Rows[0]["UserName"].ToString();

                       // string TinyURL = ToTinyURLS("http://erpdairy.com/mis/MilkCollection/ProducerDetail.aspx?P_Id=" + objdb.Encrypt(P_Id));

                        string webURL = "http://erpdairy.com/pd.aspx?P_Id=" + objdb.Encrypt(P_Id);

                        //Response.Write("Long URL" + TinyURL); 
                        //QR = ds1.Tables[0].Rows[0]["parent"].ToString() + "." + ds.Tables[0].Rows[0]["DCSId"].ToString() + "." + ds.Tables[0].Rows[0]["UserName"].ToString();
                         
                        QRGenerator(webURL);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModal();", true);

                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }


    public void QRGenerator(string QR)
    {
        string Code = QR;
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(Code, QRCodeGenerator.ECCLevel.Q);
        System.Web.UI.WebControls.Image imgQRCode = new System.Web.UI.WebControls.Image();
        imgQRCode.Height = 80;
        imgQRCode.Width = 80;
        using (Bitmap bitmap = qrCode.GetGraphic(50))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                imgQRCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }
            PlaceHolder1.Controls.Add(imgQRCode);
        }
    }


    protected string ToTinyURLS(string txt)
    {
        Regex regx = new Regex("http://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?", RegexOptions.IgnoreCase);

        MatchCollection mactches = regx.Matches(txt);

        foreach (Match match in mactches)
        {
            string tURL = MakeTinyUrl(match.Value);
            txt = txt.Replace(match.Value, tURL);
        }

        return txt;
    }

    public static string MakeTinyUrl(string Url)
    {
        try
        {
            if (Url.Length <= 12)
            {
                return Url;
            }
            if (!Url.ToLower().StartsWith("http") && !Url.ToLower().StartsWith("ftp"))
            {
                Url = "http://" + Url;
            }
            var request = WebRequest.Create("http://tinyurl.com/api-create.php?url=" + Url);
            var res = request.GetResponse();
            string text;
            using (var reader = new StreamReader(res.GetResponseStream()))
            {
                text = reader.ReadToEnd();
            }
            return text;
        }
        catch (Exception)
        {
            return Url;
        }
    }

}