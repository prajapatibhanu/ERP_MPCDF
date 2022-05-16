using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Net;
using System.IO;

public partial class mis_MilkCollection_Notifications : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure apiprocedure = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (apiprocedure.createdBy() != null)
        {
            if (!IsPostBack)
            {
                GetNotificationCount();
                
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx", false);
        }
        
    }
    protected void GetNotificationCount()
    {
        DataSet NotificationDS = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                                      new string[] { "flag", "I_OfficeID", "V_EntryType" },
                                      new string[] { "8", apiprocedure.Office_ID(), "Out" }, "dataset");

        if (NotificationDS.Tables[0].Rows.Count != 0)
        {

            if (NotificationDS != null)
            {

                spn2.InnerText = NotificationDS.Tables[0].Rows.Count.ToString();
                
            }
            else
            {
                spn2.InnerText = "0";
            }

        }
        else
        {
            spn2.InnerText = "0";
        }
        ds = null;
        ds = apiprocedure.ByProcedure("spProductionMilkContainerMaster"
        , new string[] { "flag", "Office_Id" }
        , new string[] { "10", apiprocedure.Office_ID() }, "dataset");

        if (ds != null)
        {
            spn1.InnerText = ds.Tables[0].Rows.Count.ToString();
           
        }
        else
        {
            spn1    .InnerText = "0";
        }
    }
}