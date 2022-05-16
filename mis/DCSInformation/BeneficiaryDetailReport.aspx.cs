using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_DCSInformation_BeneficiaryDetailReport : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && objdb.Office_ID() != null)
            {
                if (!Page.IsPostBack)
                {

                    lblMsg.Text = "";
                    FillGrid();

                }
            }
            else
            {
                objdb.redirectToHome();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            //gvDetails.DataSource = new string[] { };
            //ds = objdb.ByProcedure("SpKCCBeneficiaryDetail", new string[]{ "flag", "UpdatedBy" },new string[]{ "1", Session["Emp_ID"].ToString()}, "dataset");
            ds = objdb.ByProcedure("sp_BenReport", new string[] { "flag", "UpdatedBy" }, new string[] { "1", Session["Emp_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "View")
            {
                string Beneficiary_ID = e.CommandArgument.ToString();
                ds = objdb.ByProcedure("sp_BenReport", new string[] { "flag", "Beneficiary_ID" }, new string[] { "2", Beneficiary_ID }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dvDetail.DataSource = ds;
                    dvDetail.DataBind();

                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModal();", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}