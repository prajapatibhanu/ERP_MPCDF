using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class mis_MilkCollection_Dcs_ChallanDetails : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds; 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetDSCChallanDetails();
        }
    }

    private void GetDSCChallanDetails()
    {
        try
        {
            if (objdb.Decrypt(Request.QueryString["DCSCH_ID"]) != null)
            {
                string DCSCH_ID = objdb.Decrypt(Request.QueryString["DCSCH_ID"].ToString());
                ds = null;
                ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardDetails_Dcs",
                    new string[] { "flag", "I_EntryID" },
                    new string[] { "4", DCSCH_ID }, "dataset");

                if (ds.Tables[0].Rows.Count != 0)
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        gvProductDetails.DataSource = ds.Tables[0];
                        gvProductDetails.DataBind();

                        
                        lblRPuContactNo.Text = ds.Tables[0].Rows[0]["Office_ContactNo"].ToString();
                        lblRPuEmail.Text = ds.Tables[0].Rows[0]["Office_Email"].ToString();
                        lblRpuAddress.Text = ds.Tables[0].Rows[0]["Office_Address"].ToString();
                        lblRPuPincode.Text = ds.Tables[0].Rows[0]["Office_Pincode"].ToString();

                        if (ds.Tables[0].Rows[0]["I_OfficeTypeID"].ToString() == "2")
                        { 

                        }
                        else
                        {
                            lblRProduUnitName.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString() + " - दुग्ध शीत केन्द्र";
                            
                        }
                         
                    }

                }

                 
                DataSet dsfordcs = objdb.ByProcedure("USP_Trn_MilkInwardOutwardDetails_Dcs",
                     new string[] { "flag", "I_EntryID" },
                     new string[] { "5", DCSCH_ID }, "dataset");

                if (dsfordcs.Tables[0].Rows.Count != 0)
                {

                    lblcofficename.Text = "दुग्ध समिति - " +  dsfordcs.Tables[0].Rows[0]["Office_Name"].ToString();
                    lblofficeaddress.Text = dsfordcs.Tables[0].Rows[0]["Office_Address"].ToString();
                    lblpincode.Text = dsfordcs.Tables[0].Rows[0]["Office_Pincode"].ToString();

                    lblProduUnitName.Text = "दुग्ध समिति - " + dsfordcs.Tables[0].Rows[0]["Office_Name"].ToString();
                    lblPuContactNo.Text = dsfordcs.Tables[0].Rows[0]["Office_ContactNo"].ToString();
                    lblPuEmail.Text = dsfordcs.Tables[0].Rows[0]["Office_Email"].ToString();
                    lblpuAddress.Text = dsfordcs.Tables[0].Rows[0]["Office_Address"].ToString();
                    lblPuPincode.Text = dsfordcs.Tables[0].Rows[0]["Office_Pincode"].ToString();


                    lblchallanno.Text = ds.Tables[0].Rows[0]["V_ChallanNo"].ToString();
                    lblDT_TankerDispatchDate.Text = ds.Tables[0].Rows[0]["DT_DispatchDateTime"].ToString();
                    lblV_VehicleNo.Text = ds.Tables[0].Rows[0]["V_VehicleNo"].ToString();
                    lblV_DriverName.Text = ds.Tables[0].Rows[0]["V_DriverName"].ToString();
                    lblV_DriverMobileNo.Text = ds.Tables[0].Rows[0]["V_DriverMobileNo"].ToString();

                     

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
        Response.Redirect("../MilkCollection/DCSMilkDispatch.aspx");
    }
}