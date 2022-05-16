using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RTI_ViewRTI : System.Web.UI.Page
{
   // common obj = new common();
    DataSet ds = new DataSet();
    public static string reg;
    public static string RegistrationNo;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ViewState["APID"] = Request.QueryString["APID"];
            GenerateNewRandom();
           // SaveRegistrationNo();
            RegistrationNo = "1234/AAS/" + reg;
            //string RegistrationNo = spanRegistration.InnerText;
        }
    }

    protected void SaveRegistrationNo()
    {
        try
        {
           // spanRegistration.InnerText = "1234/AAS/" + reg;
           // string RegistrationNo = spanRegistration.InnerText;

          //  obj.ByProcedure("SpRTIApplicantDetail", new string[] { "flag", "ApplicationID", "RegistrationNo" }, new string[] { "7", ViewState["APID"].ToString(), RegistrationNo }, "dataset");
        }
        catch (Exception ex)
        {
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        
    }
    public static string GenerateNewRandom()
    {
        Random generator = new Random();
        String r = generator.Next(0, 10000).ToString("D4");
        if (r.Distinct().Count() == 1)
        {
            r = GenerateNewRandom();
        }
        reg = r;
        return r;
       
    }
}