using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Dashboard_MDDashboard_New : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillCatalog();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }

    protected void FillCatalog()
    {
        try
        {
            ds = null;

            ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID" }, new string[] { "14", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblds.Text = ds.Tables[0].Rows[0]["DSCOUNT"].ToString();
                lblCC.Text = ds.Tables[2].Rows[0]["CCCOUNT"].ToString();
                lblBMC.Text = ds.Tables[3].Rows[0]["BMCCOUNT"].ToString();
                lblDCS.Text = ds.Tables[4].Rows[0]["DCSCOUNT"].ToString();
                lblMDP.Text = ds.Tables[5].Rows[0]["MDPCOUNT"].ToString();
                lblProducer.Text = ds.Tables[6].Rows[0]["P_Count"].ToString();
              //  lblDistributor.Text = ds.Tables[7].Rows[0]["Dstcount"].ToString();
                lblParlour.Text = ds.Tables[8].Rows[0]["B_Count"].ToString();
                lblRTI.Text = ds.Tables[9].Rows[0]["RTI_Count"].ToString();
               // lbltotalmilkcollection.Text = ds.Tables[10].Rows[0]["Milk_InKGCount"].ToString();
                lblLegalCases.Text = ds.Tables[11].Rows[0]["Case_count"].ToString();
                lblGrievance.Text = ds.Tables[12].Rows[0]["Grv_Count"].ToString();
                lblemployee.Text = ds.Tables[13].Rows[0]["EMP_Count"].ToString();
              //  lblSuperStockist.Text = ds.Tables[14].Rows[0]["SS_Count"].ToString();
                lblMilkVariantCount.Text = ds.Tables[15].Rows[0]["MilkVariantCount"].ToString();
                lblProductVariantCount.Text = ds.Tables[16].Rows[0]["ProductVariant"].ToString();
				lblSuperStockistDistributor.Text = "440";

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID" }, new string[] { "14", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                GrdDS.DataSource = ds.Tables[1];
                GrdDS.DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DSModelF()", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void btnCrossButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("MDDashboard_New.aspx", false);
    }

    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID" }, new string[] { "15", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GVCC.DataSource = ds;
                GVCC.DataBind();
                Label lbltotalcount = (GVCC.FooterRow.FindControl("lbltotalcount") as Label);
                lbltotalcount.Text = lblCC.Text;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CCModelF()", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }



    protected void LinkButton4_Click(object sender, EventArgs e)
    {

        try
        {
            ds = null;
            ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID" }, new string[] { "16", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GVBMC.DataSource = ds;
                GVBMC.DataBind();
                Label lbltotalcount = (GVBMC.FooterRow.FindControl("lbltotalcount") as Label);
                lbltotalcount.Text = lblBMC.Text;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "BMCModelF()", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {

        try
        {
            ds = null;
            ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID" }, new string[] { "17", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GVDCS.DataSource = ds;
                GVDCS.DataBind();
                Label lbltotalcount = (GVDCS.FooterRow.FindControl("lbltotalcount") as Label);
                lbltotalcount.Text = lblDCS.Text;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DCSModelF()", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }



    }
    protected void LinkButton9_Click(object sender, EventArgs e)
    {

        try
        {
            ds = null;
            ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID" }, new string[] { "18", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GVMDP.DataSource = ds;
                GVMDP.DataBind();
                Label lbltotalcount = (GVMDP.FooterRow.FindControl("lbltotalcount") as Label);
                lbltotalcount.Text = lblMDP.Text;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MDPModelF()", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }
    protected void LinkButton11_Click(object sender, EventArgs e)
    {

        try
        {
            ds = null;
            ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID" }, new string[] { "19", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GVPC.DataSource = ds;
                GVPC.DataBind();
                Label lbltotalcount = (GVPC.FooterRow.FindControl("lbltotalcount") as Label);
                lbltotalcount.Text = lblProducer.Text;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "PModelF()", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }


   //protected void lbDistributor_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ds = null;
    //        ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID" }, new string[] { "20", ViewState["Office_ID"].ToString() }, "dataset");

    //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //            GVDSIT.DataSource = ds;
    //            GVDSIT.DataBind();
    //            Label lbltotalcount = (GVDSIT.FooterRow.FindControl("lbltotalcount") as Label);
    //            lbltotalcount.Text = lblDistributor.Text;
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "DstModelF()", true);
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }


    //}


    protected void lbParlour_Click(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID" }, new string[] { "21", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GVParlour.DataSource = ds;
                GVParlour.DataBind();
                Label lbltotalcount = (GVParlour.FooterRow.FindControl("lbltotalcount") as Label);
                lbltotalcount.Text = lblParlour.Text;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParlourModelF()", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void lbRTI_Click(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID" }, new string[] { "22", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GVRTI.DataSource = ds;
                GVRTI.DataBind();
                Label lbltotalcount = (GVRTI.FooterRow.FindControl("lbltotalcount") as Label);
                lbltotalcount.Text = lblRTI.Text;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "RTIModelF()", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lbLegalCases_Click(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID" }, new string[] { "23", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvLegalCases.DataSource = ds;
                gvLegalCases.DataBind();
                Label lbltotalcount = (gvLegalCases.FooterRow.FindControl("lbltotalcount") as Label);
                lbltotalcount.Text = lblLegalCases.Text;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "LegalCasesModelF()", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void lbGrv_Click(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID" }, new string[] { "24", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GVGrievance.DataSource = ds;
                GVGrievance.DataBind();
                Label lbltotalcount = (GVGrievance.FooterRow.FindControl("lbltotalcount") as Label);
                lbltotalcount.Text = lblGrievance.Text;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "GrievanceModelF()", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lbempcount_Click(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag", "DS_Office_ID" }, new string[] { "25", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GVEMP.DataSource = ds;
                GVEMP.DataBind();
                Label lbltotalcount = (GVEMP.FooterRow.FindControl("lbltotalcount") as Label);
                lbltotalcount.Text = lblemployee.Text;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "EMPModelF()", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
	
    //protected void LinkButton18_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ds = null;
    //        ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag" }, new string[] { "26" }, "dataset");

    //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //            gvMilkCollectionDetails.DataSource = ds;
    //            gvMilkCollectionDetails.DataBind();
    //            Label lbltotalMilkInKg = (gvMilkCollectionDetails.FooterRow.FindControl("lbltotalMilkInKg") as Label);
    //            lbltotalMilkInKg.Text = lbltotalmilkcollection.Text;
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MilkCollectionModel()", true);
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
     //protected void lnkSuperStockist_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ds = null;
    //        ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag" }, new string[] { "27" }, "dataset");

    //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
    //        {
    //            gvSS.DataSource = ds;
    //            gvSS.DataBind();
    //            Label lbltotalcount = (gvSS.FooterRow.FindControl("lbltotalcount") as Label);
    //            lbltotalcount.Text = lblSuperStockist.Text;
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "SSModel()", true);
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void lnkMilkVariant_Click(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag" }, new string[] { "28" }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvMilkVaraiant.DataSource = ds;
                gvMilkVaraiant.DataBind();
                Label lbltotalcount = (gvMilkVaraiant.FooterRow.FindControl("lbltotalcount") as Label);
                lbltotalcount.Text = lblMilkVariantCount.Text;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "MilkVariant()", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkProductVariant_Click(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("Sp_Dashboard_DS", new string[] { "flag" }, new string[] { "29" }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvProduct.DataSource = ds;
                gvProduct.DataBind();
                Label lbltotalcount = (gvProduct.FooterRow.FindControl("lbltotalcount") as Label);
                lbltotalcount.Text = lblProductVariantCount.Text;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ProductVariant()", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}