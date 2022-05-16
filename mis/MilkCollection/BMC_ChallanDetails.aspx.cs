using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class mis_MilkCollection_BMC_ChallanDetails : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    decimal grQtyTotal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
		if (!Page.IsPostBack)
        {
            GetBMCChallanDetails();
        }
        // if (objdb.createdBy() != null && objdb.Office_ID() != null)
        // {
            // if (!Page.IsPostBack)
            // {
                // GetBMCChallanDetails();
            // }
        // }
        else
        {
            objdb.redirectToHome();
        }
    }

    private void GetBMCChallanDetails()
    {
        try
        {
            if (objdb.Decrypt(Request.QueryString["BMCCH_ID"]) != null)
            {
                string DCSCH_ID = objdb.Decrypt(Request.QueryString["BMCCH_ID"].ToString());
                ds = null;
                ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                    new string[] { "flag", "I_EntryID" },
                    new string[] { "5", DCSCH_ID }, "dataset");

                if (ds.Tables[0].Rows.Count != 0)
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        gvDetails.DataSource = ds.Tables[0];
                        gvDetails.DataBind();
                        gvTankerSealDetails.DataSource = ds.Tables[1];
                        gvTankerSealDetails.DataBind();



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




                DataSet dsfordcs = objdb.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                     new string[] { "flag", "I_EntryID" },
                     new string[] { "6", DCSCH_ID }, "dataset");

                if (dsfordcs.Tables[0].Rows.Count != 0)
                {

                    lblcofficename.Text = "दुग्ध समिति - " + dsfordcs.Tables[0].Rows[0]["Office_Name"].ToString();
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

                if (dsfordcs.Tables[1].Rows.Count != 0)
                {
                    gv_dcsmilkreceive.DataSource = dsfordcs.Tables[1];
                    gv_dcsmilkreceive.DataBind();
                }


            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }


    protected void gv_dcsmilkreceive_RowDataBound(object sender, GridViewRowEventArgs e)
    {
   if (e.Row.RowType == DataControlRowType.DataRow)
   {
       decimal tmpTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "I_MilkQuantity").ToString());
       grQtyTotal += tmpTotal;

       //if (e.Row.RowIndex > 0)
       //{
       //    GridViewRow previousRow = gv_dcsmilkreceive.Rows[e.Row.RowIndex -1];
       //    if (e.Row.Cells[0].Text == previousRow.Cells[0].Text)
       //    {
       //        if (previousRow.Cells[0].RowSpan == 0)
       //        {
       //            previousRow.Cells[0].RowSpan += 2;
       //            e.Row.Cells[0].Visible = false;
       //        }
       //    }
       //}
   }

   if (e.Row.RowType == DataControlRowType.Footer)
   {
       Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
       lblTotalqty.Text = grQtyTotal.ToString();
   }

    }

    protected void Unnamed_Click(object sender, EventArgs e)
    {
        Response.Redirect("../MilkCollection/BMCMilkDispatch.aspx");
    }
}