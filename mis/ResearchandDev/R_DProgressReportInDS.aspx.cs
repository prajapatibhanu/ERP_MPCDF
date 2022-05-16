using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class mis_ResearchandDev_R_DProgressReportInDS : System.Web.UI.Page
{
    APIProcedure objapi = new APIProcedure();
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        fildropdown();
       
    }
    private void fillgrd()
    {
        ds = new DataSet();
        ds = objapi.ByProcedure("SP_RD_Plan_Implementation_DS_By_DSID_List", new string[] { "flag", "DSID" }, new string[] { "0", ddlDS.SelectedValue }, "dataset");
        grdlist.DataSource = ds;
        grdlist.DataBind();
        GC.SuppressFinalize(objapi);
        GC.SuppressFinalize(ds);

    }
    private void fildropdown()
    {
        ds = new DataSet();
        ds = objapi.ByProcedure("SP_RD_DS_Office_List", new string[] { "flag" }, new string[] { "0" }, "dataset");
        ddlDS.DataSource = ds;
        ddlDS.DataTextField = "OfficeName";
        ddlDS.DataValueField = "OfficeID";
        ddlDS.DataBind();
        ddlDS.Items.Insert(0, new ListItem("-- Select Dudh Sangh  --", "0"));
        GC.SuppressFinalize(objapi);
        GC.SuppressFinalize(ds);

    }
    protected void grdlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string command = Convert.ToString(e.CommandArgument);
        string[] commandArg = command.Split('|');
        int RDPlanID = Convert.ToInt32(commandArg[0]);
        int RDPlanImplementationID = Convert.ToInt32(commandArg[1]);
        switch (e.CommandName)
        {
            case "Detail":
                ds = new DataSet();
                DataSet ds1 = new DataSet();
                ds = objapi.ByProcedure("SP_RD_Plan_By_ID_List", new string[] { "flag", "RDPlanID" }, new string[] { "1", Convert.ToString(RDPlanID) }, "dataset");
                ds1 = objapi.ByProcedure("SP_RD_Plan_ImplementationFinalStage_DS_By_ID", new string[] { "flag", "PlanImplementationID", "RDPlanID" }, new string[] { "0", Convert.ToString(RDPlanImplementationID), Convert.ToString(RDPlanID) }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblRDType.Text = Convert.ToString(ds.Tables[0].Rows[0]["RDType"]);
                    // lblPlanType.Text = Convert.ToString(ds.Tables[0].Rows[0]["PlanType"]);
                    lblTitle.Text = Convert.ToString(ds.Tables[0].Rows[0]["ResearchTitle"]);
                    //lblStartDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["StartDate"]);
                    lbldetails.Text = Convert.ToString(ds.Tables[0].Rows[0]["ResearchDetails"]);
                    lblOutcomes.Text = Convert.ToString(ds.Tables[0].Rows[0]["ActualOutcomes"]);
                }
                grd.DataSource = ds1;
                grd.DataBind();
                GC.SuppressFinalize(objapi);
                GC.SuppressFinalize(ds);
                GC.SuppressFinalize(ds1);

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupAddDates();", true);
                break;

            default:
                break;
        }
    }
    protected void btnlist_Click(object sender, EventArgs e)
    {
        fillgrd();
    }
}