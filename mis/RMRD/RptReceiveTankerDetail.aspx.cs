using System;
using System.Data;
using System.Globalization;
using System.Web.UI;

public partial class mis_MilkCollection_mcms_home : System.Web.UI.Page
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

    #region User Defined Function
    private void FillGrid()
    {
        try
        {
           

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
                gvReceivedEntry.Visible = true;
                ds = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_RMRD",
                                new string[] { "flag", "I_OfficeID", "FromDate", "ToDate" },
                                new string[] { "16", apiprocedure.Office_ID(),FromDate, ToDate }, "dataset");
               if(ds != null && ds.Tables[0].Rows.Count > 0)
               {
                   
                   gvReceivedEntry.DataSource = ds;
                   gvReceivedEntry.DataBind();
               }

                

           
           
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    #endregion

    #region Button Event
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    #endregion

    #region row command Event
    protected void gvDispatchEntry_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
       if (e.CommandName == "ViewEntry")
       {
           ds = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_RMRD",
                           new string[] { "flag", "BI_MilkInOutRMRDRefID" },
                           new string[] { "17", e.CommandArgument.ToString() }, "dataset");

           if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
           {
               lblVehicleNo.Text = e.CommandArgument.ToString();

               gvQCDetailsForDS.DataSource = ds.Tables[0];
               gvQCDetailsForDS.DataBind();

              

               Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowQCDetails();", true);
           }
           else
           {
               gvQCDetailsForDS.DataSource = null;
               gvQCDetailsForDS.DataBind();

              

               lblMsg.Text = apiprocedure.Alert("fa-info", "alert-info", "Info!", "No Data Found!");
           }
       }
    }
    #endregion

}