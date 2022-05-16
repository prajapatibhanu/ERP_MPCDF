using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_MilkCollection_MilkFilledTankerChallanDetails : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    IFormatProvider cult = new CultureInfo("en-IN", true);
	 
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            GetmilkChallanDetails();
        }

    }

    private void GetmilkChallanDetails()
    {
        try
        {
            string V_EntryType = "";
            string flag = "";
            if (objdb.Decrypt(Request.QueryString["Cid"]) != null)
            {
                if (objdb.OfficeType_ID() == "2")
                {
                    V_EntryType = "Out";
                    flag = "43";
                    spn.InnerHtml = "Milk Tanker Dispatch Delivery Challan";
                }
                else
                {
                    V_EntryType = "In";
                    flag = "44";
                    spn.InnerHtml = "Milk Tanker Received Delivery Challan";
                }
                string Cid = objdb.Decrypt(Request.QueryString["Cid"].ToString());

                ds = null;

                ds = objdb.ByProcedure("Usp_MilkInwardOutwardDetails",
                    new string[] { "flag", "I_EntryID", "V_EntryType" },
                    new string[] { flag, Cid, V_EntryType }, "dataset");

                if (ds.Tables[0].Rows.Count != 0)
                {
                     lblcofficename.Text = "दुग्ध शीत केन्द्र " + ds.Tables[0].Rows[0]["Office_Name"].ToString();
                    //lblcofficename.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                    lblofficeaddress.Text = ds.Tables[0].Rows[0]["Office_Address"].ToString();
                    lblpincode.Text = ds.Tables[0].Rows[0]["Office_Pincode"].ToString();

                    lblProduUnitName.Text = "दुग्ध शीत केन्द्र - "+ds.Tables[0].Rows[0]["Office_Name"].ToString();
                    lblPuContactNo.Text = ds.Tables[0].Rows[0]["Office_ContactNo"].ToString();
                    lblPuEmail.Text = ds.Tables[0].Rows[0]["Office_Email"].ToString();
                    lblpuAddress.Text = ds.Tables[0].Rows[0]["Office_Address"].ToString();
                    lblPuPincode.Text = ds.Tables[0].Rows[0]["Office_Pincode"].ToString();

                    lblRProduUnitName.Text = ds.Tables[0].Rows[0]["Rofficename"].ToString();
                    lblRPuContactNo.Text = ds.Tables[0].Rows[0]["RofficeContactno"].ToString();
                    lblRPuEmail.Text = ds.Tables[0].Rows[0]["Rofficeemail"].ToString();
                    lblRpuAddress.Text = ds.Tables[0].Rows[0]["RofficeAdd"].ToString();
                    lblRPuPincode.Text = ds.Tables[0].Rows[0]["Rofficepincode"].ToString();

                    lblC_ReferenceNo.Text = ds.Tables[0].Rows[0]["C_ReferenceNo"].ToString();
                    lblchallanno.Text = ds.Tables[0].Rows[0]["V_ReferenceCode"].ToString();
                    //lblDT_TankerDispatchDate.Text = ds.Tables[0].Rows[0]["DT_DispatchDateTime"].ToString();
					if(objdb.OfficeType_ID() == "2")
                    {
                        spndate.InnerHtml = "Dispatch Date Time:";
                        DateTime ADate = Convert.ToDateTime(string.Concat(Convert.ToDateTime(ds.Tables[0].Rows[0]["DT_DispatchDateTime"].ToString(), cult).ToString("yyyy/MM/dd"), " ", Convert.ToDateTime(ds.Tables[0].Rows[0]["DT_DispatchDateTime"].ToString(), cult).ToString("hh:mm:ss tt")), cult);
                        lblDT_TankerDispatchDate.Text = ADate.ToString();
                    }
                    else
                    {
                        spndate.InnerHtml = "Recieved Date Time:";
                        DateTime ADate = Convert.ToDateTime(string.Concat(Convert.ToDateTime(ds.Tables[0].Rows[0]["DT_ArrivalDateTime"].ToString(), cult).ToString("yyyy/MM/dd"), " ", Convert.ToDateTime(ds.Tables[0].Rows[0]["DT_DispatchDateTime"].ToString(), cult).ToString("hh:mm:ss tt")), cult);
                        lblDT_TankerDispatchDate.Text = ADate.ToString();
                    }
					
                    lblV_VehicleNo.Text = ds.Tables[0].Rows[0]["V_VehicleNo"].ToString();
                    lblV_DriverName.Text = ds.Tables[0].Rows[0]["V_DriverName"].ToString();
                    lblV_DriverMobileNo.Text = ds.Tables[0].Rows[0]["V_DriverMobileNo"].ToString();
                    lblLicence.Text = ds.Tables[0].Rows[0]["NV_DriverDrivingLicenceNo"].ToString();
                    lblRepresentativename.Text = ds.Tables[0].Rows[0]["V_RepresentativeName"].ToString();
                    lblRepresentativemobile.Text = ds.Tables[0].Rows[0]["V_RepresentativeMobileNo"].ToString();
                    



                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        gvfillDetails.DataSource = ds.Tables[0];
                        gvfillDetails.DataBind();
                        if (objdb.OfficeType_ID() == "2")
                        {
                            gvfillDetails.Columns[2].Visible = true;
                            gvfillDetails.Columns[1].Visible = false;
                            gvfillDetails.Columns[3].Visible = false;
                        }
                        else
                        {
                            gvfillDetails.Columns[1].Visible = false;
                            gvfillDetails.Columns[2].Visible = false;
                            gvfillDetails.Columns[3].Visible = true;
                        }
                    }
                    if (ds.Tables[1].Rows.Count != 0)
                    {
                        gvSealDetails.DataSource = ds.Tables[1];
                        gvSealDetails.DataBind();
                    }

                }

                else
                {
                    //objdb.redirectToHome();
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
        Response.Redirect("../mcms_reports/mcms-home_FilledTanker.aspx");
    }
	
	protected void gvfillDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            foreach (GridViewRow row1 in gvfillDetails.Rows)
            {
                Label lblDT_ArrivalDateTime = (Label)row1.FindControl("lblDT_ArrivalDateTime");
                Label lblDT_DispatchDateTime = (Label)row1.FindControl("lblDT_DispatchDateTime");


                 if (lblDT_ArrivalDateTime.Text != "")
                {

                    DateTime ADate1 = Convert.ToDateTime(string.Concat(Convert.ToDateTime(lblDT_ArrivalDateTime.Text, cult).ToString("dd/MM/yyyy"), " ", Convert.ToDateTime(lblDT_ArrivalDateTime.Text, cult).ToString("hh:mm:ss tt")), cult);
                    lblDT_ArrivalDateTime.Text = ADate1.ToString();

                }

                if (lblDT_DispatchDateTime.Text != "")
                {
                    DateTime ADate2 = Convert.ToDateTime(string.Concat(Convert.ToDateTime(lblDT_DispatchDateTime.Text, cult).ToString("dd/MM/yyyy"), " ", Convert.ToDateTime(lblDT_DispatchDateTime.Text, cult).ToString("hh:mm:ss tt")), cult);
                    lblDT_DispatchDateTime.Text = ADate2.ToString();
                }


            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


}