using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mis_mcms_reports_MilkCollection_Home_BMC : System.Web.UI.Page
{
    APIProcedure apiprocedure = new APIProcedure();
    IFormatProvider cult = new CultureInfo("en-IN", true);
    DataSet ds, ds2;
    decimal grQtyTotal = 0;
     
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            FillGrid();
        }
    }


    private void FillGrid()
    {
        try
        {
            gvDispatchEntry.DataSource = null;
            gvDispatchEntry.DataBind();

            gvReceivedEntry.DataSource = null;
            gvReceivedEntry.DataBind();

            string FromDate = "", ToDate = "";

            if (txtFromDate.Text != "")
            {
                FromDate = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            }
            if (txtToDate.Text != "")
            {
                ToDate = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            }

            if (apiprocedure.OfficeType_ID() == "5" || apiprocedure.OfficeType_ID() == "3") // 5 Is BMC
            {
                gvReceivedEntry.Visible = false;
                gvDispatchEntry.Visible = true;



                lblHeading.Text = "Dispatch Milk Details";
                ds = null;
                ds = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                                new string[] { "flag", "I_OfficeID ", "V_EntryType", "FromDate", "ToDate", "V_Shift" },
                                new string[] { "10", apiprocedure.Office_ID(), "Out", FromDate, ToDate, ddlShift.SelectedValue }, "dataset");
                gvDispatchEntry.DataSource = ds;
                gvDispatchEntry.DataBind();
            }
            else
            {
                lblHeading.Text = "Receive Milk Details";

                gvReceivedEntry.Visible = true;
                gvDispatchEntry.Visible = false;


                ds = null;
                ds = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                                  new string[] { "flag", "I_OfficeID ", "V_EntryType", "FromDate", "ToDate", "V_Shift" },
                                  new string[] { "10", apiprocedure.Office_ID(), "In", FromDate, ToDate, ddlShift.SelectedValue }, "dataset");

                gvReceivedEntry.DataSource = ds;
                gvReceivedEntry.DataBind();

            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void gvDispatchEntry_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewEntry")
        {

            //FOR BMC/MDP Comp MILKCOLLECTION(self+dcs) WEB/APP

            DataSet dsBMCMDP_SD = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                     new string[] { "flag", "V_ChallanNo" },
                     new string[] { "11", e.CommandArgument.ToString() }, "dataset");

            if (dsBMCMDP_SD.Tables[0].Rows.Count != 0)
            {
                gv_dcsmilkreceive.DataSource = dsBMCMDP_SD.Tables[0];
                gv_dcsmilkreceive.DataBind();
            }
            else
            {
                gv_dcsmilkreceive.DataSource = null;
                gv_dcsmilkreceive.DataBind();
                lblMsg.Text = apiprocedure.Alert("fa-info", "alert-info", "Info!", "No Data Found!");
            }

            // FOR BMC/MDP Comp MILKCOLLECTION(Total Collection) WEB/APP

            DataSet dsBMCMDP_TC = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                   new string[] { "flag", "V_ChallanNo" },
                   new string[] { "12", e.CommandArgument.ToString() }, "dataset");

            if (dsBMCMDP_TC.Tables[0].Rows.Count != 0)
            {
                lblChallanNo.Text = dsBMCMDP_TC.Tables[0].Rows[0]["V_ChallanNo"].ToString();
                lblofficeName.Text = " - " + dsBMCMDP_TC.Tables[0].Rows[0]["Office_Name"].ToString();
                lblrecieveOffice.Text = " - " + dsBMCMDP_TC.Tables[0].Rows[0]["ReportingUnit"].ToString();

                gvQCDetailsForCC.DataSource = dsBMCMDP_TC.Tables[0];
                gvQCDetailsForCC.DataBind();
            }
            else
            {
                gvQCDetailsForCC.DataSource = null;
                gvQCDetailsForCC.DataBind();
                lblMsg.Text = apiprocedure.Alert("fa-info", "alert-info", "Info!", "No Data Found!");
            }

            // FOR DS/CC Comp MILKCOLLECTION WEB/APP

            DataSet dsCCDS_SD = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                       new string[] { "flag", "V_ChallanNo" },
                       new string[] { "13", e.CommandArgument.ToString() }, "dataset");

            if (dsCCDS_SD.Tables[0].Rows.Count != 0)
            {
                gvQCDetailsForDS.DataSource = dsCCDS_SD.Tables[0];  
                gvQCDetailsForDS.DataBind();
            }
            else
            {
                gvQCDetailsForDS.DataSource = null;
                gvQCDetailsForDS.DataBind();
                lblMsg.Text = apiprocedure.Alert("fa-info", "alert-info", "Info!", "No Data Found at " + lblrecieveOffice.Text);
            }
             
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowQCDetails();", true); 

        }
    }


    protected void gv_dcsmilkreceive_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            decimal tmpTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "I_MilkQuantity").ToString());
            grQtyTotal += tmpTotal;

            if (e.Row.RowIndex > 0)
            {
                GridViewRow previousRow = gv_dcsmilkreceive.Rows[e.Row.RowIndex - 1];
                if (e.Row.Cells[0].Text == previousRow.Cells[0].Text)
                {
                    if (previousRow.Cells[0].RowSpan == 0)
                    {
                        previousRow.Cells[0].RowSpan += 2;
                        e.Row.Cells[0].Visible = false;
                    }
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
            lblTotalqty.Text = grQtyTotal.ToString();
        }

    }

}