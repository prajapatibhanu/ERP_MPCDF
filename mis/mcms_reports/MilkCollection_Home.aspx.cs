using System;
using System.Data;
using System.Globalization;
using System.Web.UI;


public partial class mis_mcms_reports_MilkCollection_Home : System.Web.UI.Page
{
    APIProcedure apiprocedure = new APIProcedure();
    IFormatProvider cult = new CultureInfo("en-IN", true);
    DataSet ds, ds2;

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

            if (apiprocedure.OfficeType_ID() == "6") // 6 Is DCS
            {
                gvReceivedEntry.Visible = false;
                gvDispatchEntry.Visible = true;



                lblHeading.Text = "Dispatch Milk Details";
                ds = null;
                ds = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_Dcs",
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
                ds = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_Dcs",
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
            ds = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_Dcs",
                            new string[] { "flag", "V_ChallanNo" },
                            new string[] { "11", e.CommandArgument.ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
            {
                lblChallanNo.Text = e.CommandArgument.ToString();


                gvQCDetailsForCC.DataSource = ds.Tables[0]; //For DSC
                gvQCDetailsForCC.DataBind();

                lbldcsname.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString();

                gvQCDetailsForDS.DataSource = ds.Tables[1]; //For CC/BMC
                gvQCDetailsForDS.DataBind();

                lblccbmcname.Text = ds.Tables[1].Rows[0]["Office_Name"].ToString();

                if (ds.Tables[1].Rows[0]["AdulteratedMilk"].ToString() == "Yes")
                {
                    if (ds.Tables[0].Rows.Count > 0 || ds.Tables[2].Rows.Count > 0)
                    {
                        lblAdulterationMilkRemarks.Text = "Remarks - " + ds.Tables[2].Rows[0]["AdulteratedRemarks"].ToString();
                        milktestdetail.Visible = true;
                        gvmilkAdulterationtestdetail.DataSource = ds.Tables[2]; //For CC/BMC
                        gvmilkAdulterationtestdetail.DataBind();
                    }
                    else
                    {
                        lblAdulterationMilkRemarks.Text = "";
                        milktestdetail.Visible = false;
                        gvmilkAdulterationtestdetail.DataSource = string.Empty; //For CC/BMC
                        gvmilkAdulterationtestdetail.DataBind();
                    }

                }
                else
                {
                    lblAdulterationMilkRemarks.Text = "";
                    milktestdetail.Visible = false;
                    gvmilkAdulterationtestdetail.DataSource = string.Empty; //For CC/BMC
                    gvmilkAdulterationtestdetail.DataBind();
                }


                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowQCDetails();", true);
            }
            else
            {
                gvQCDetailsForCC.DataSource = null;
                gvQCDetailsForCC.DataBind();

                gvQCDetailsForDS.DataSource = null;
                gvQCDetailsForDS.DataBind();

                lblMsg.Text = apiprocedure.Alert("fa-info", "alert-info", "Info!", "No Data Found!");
            }
        }
    }

}