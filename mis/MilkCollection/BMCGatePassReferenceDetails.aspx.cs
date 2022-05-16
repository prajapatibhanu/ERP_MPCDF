using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_MilkCollection_BMCGatePassReferenceDetails : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    IFormatProvider cult = new CultureInfo("en-IN", true);
     
    protected void Page_Load(object sender, EventArgs e)
    {
         
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetRefGatePassDetails();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }

    private void GetRefGatePassDetails()
    {
        try
        {
            if (objdb.Decrypt(Request.QueryString["BMCRid"]) != null)
            {
                string BMCRid = objdb.Decrypt(Request.QueryString["BMCRid"].ToString());

                ds = null;

                ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                    new string[] { "flag", "BI_MilkInOutRefID" },
                    new string[] { "5", BMCRid }, "dataset");

                if (ds.Tables[0].Rows.Count != 0)
                {

                    DateTime ADate = Convert.ToDateTime(string.Concat(Convert.ToDateTime(ds.Tables[0].Rows[0]["DT_TankerDispatchDate"].ToString(), cult).ToString("yyyy/MM/dd"), " ", Convert.ToDateTime(ds.Tables[0].Rows[0]["DT_TankerDispatchDate"].ToString(), cult).ToString("hh:mm:ss tt")), cult);
                    lblDT_TankerDispatchDate.Text = ADate.ToString();

                    lblProduUnitName.Text = ds.Tables[0].Rows[0]["Oname"].ToString();
                    lblOfficeName.Text = ds.Tables[0].Rows[0]["Oname"].ToString();
                    
                    lblOfficeAddress.Text = ds.Tables[0].Rows[0]["Office_Address"].ToString() + " - " + ds.Tables[0].Rows[0]["Office_Pincode"].ToString();
                    lblPuContactNo.Text = ds.Tables[0].Rows[0]["Office_ContactNo"].ToString();
                    lblPuEmail.Text = ds.Tables[0].Rows[0]["Office_Email"].ToString();
                    lblpuAddress.Text = ds.Tables[0].Rows[0]["Office_Address"].ToString();
                    lblPuPincode.Text = ds.Tables[0].Rows[0]["Office_Pincode"].ToString();
                    lblC_ReferenceNo.Text = ds.Tables[0].Rows[0]["C_ReferenceNo"].ToString();
                    //lblDT_TankerDispatchDate.Text = ds.Tables[0].Rows[0]["DT_TankerDispatchDate"].ToString();
                    lblV_VehicleNo.Text = ds.Tables[0].Rows[0]["V_VehicleNo"].ToString();
                    lblLicence.Text = ds.Tables[0].Rows[0]["NV_DriverDrivingLicenceNo"].ToString();
                    lblV_DriverName.Text = ds.Tables[0].Rows[0]["V_DriverName"].ToString();
                    lblV_DriverMobileNo.Text = ds.Tables[0].Rows[0]["V_DriverMobileNo"].ToString();
                    lblV_TesterName.Text = ds.Tables[0].Rows[0]["V_TesterName"].ToString();
                    lblV_TesterMobileNo.Text = ds.Tables[0].Rows[0]["V_TesterMobileNo"].ToString();

                    if (ds.Tables[0].Rows[0]["I_OfficeTypeID"].ToString() == "2")
                    {
                        imgLogo.ImageUrl = "~/images/bds_logo.png";


                    }
                    else
                    {
                        lblProduUnitName.Text = "दुग्ध शीत केन्द्र - " + lblProduUnitName.Text;
                        lblOfficeName.Text = "दुग्ध शीत केन्द्र - " + lblOfficeName.Text;

                        imgLogo.ImageUrl = "~/images/Logo/logo_dcs.jpg";
                    }

                    if (ds.Tables[1].Rows.Count != 0)
                    {
                        GridView1.DataSource = ds.Tables[1];
                        GridView1.DataBind();
                    }


                }


            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void Unnamed_Click(object sender, EventArgs e)
    {
        Response.Redirect("../MilkCollection/BMCGenerateGatePass.aspx");
    }
}