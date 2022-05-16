using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using System.Web.UI;
using System.Web;

public partial class mis_MilkCollection_JabalpurHeadLoadChargesMaster : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtEffectiveDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                
                FillGrid();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void ddlMilkCollectionUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSociety();
    }
    protected void FillSociety()
    {
        try
        {
            if (ddlMilkCollectionUnit.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                                  new string[] { "flag", "Office_ID", "OfficeType_ID" },
                                  new string[] { "10", ViewState["Office_ID"].ToString(), ddlMilkCollectionUnit.SelectedValue }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSociety.DataTextField = "Office_Name";
                        ddlSociety.DataValueField = "Office_ID";
                        ddlSociety.DataSource = ds.Tables[2];
                        ddlSociety.DataBind();
                        ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    else
                    {
                        ddlSociety.Items.Insert(0, new ListItem("Select", "0"));

                    }
                }
                else
                {
                    ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                // Response.Redirect("SocietywiseMilkCollectionInvoiceDetails.aspx", false);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                lblMsg.Text = "";
                string IsActive = "1";
                string Maximum = "0";
                string Minimum = "0";
                if (ddlMilkQuantity.SelectedValue == "25-50")
                {
                    Minimum = "25";
                    Maximum = "50";
                }
                else if (ddlMilkQuantity.SelectedValue == "51-100")
                {
                    Minimum = "51";
                    Maximum = "100";
                }
                else if (ddlMilkQuantity.SelectedValue == "101-200")
                {
                    Minimum = "101";
                    Maximum = "200";
                }
                else if (ddlMilkQuantity.SelectedValue == "201-300")
                {
                    Minimum = "201";
                    Maximum = "300";
                }
                else if (ddlMilkQuantity.SelectedValue == "301-400")
                {
                    Minimum = "301";
                    Maximum = "400";
                }
                else if (ddlMilkQuantity.SelectedValue == "Above 401")
                {
                    Minimum = "401";
                    Maximum = "10000";
                }
                if(btnSubmit.Text == "Save")
                {
                    ds = objdb.ByProcedure("Usp_JabalpurHeadLoadChargesMaster",
                                          new string[] {"flag",
                                                     "EffectiveDate", 
                                                     "Office_ID", 
                                                     "Office_Parant_ID",
                                                     "Distance", 
                                                     "MilkQuantityRange", 
                                                     "MinimumMilkQuanity",
                                                     "MaximumMilkQuanity",
                                                     "RateperPali", 
                                                     "CreatedAt", 
                                                     "CreatedBy", 
                                                     "CreatedByIP", 
                                                     "IsActive"
                                                   },
                                         new string[] {"1"
                                                  ,Convert.ToDateTime(txtEffectiveDate.Text,cult).ToString("yyyy/MM/dd")
                                                  ,ddlSociety.SelectedValue
                                                  ,objdb.Office_ID()
                                                  ,txtDistance.Text
                                                  ,ddlMilkQuantity.SelectedValue
                                                  ,Minimum
                                                  ,Maximum
                                                  ,txtRateperPali.Text
                                                  ,objdb.Office_ID()
                                                  ,objdb.createdBy()
                                                  ,objdb.GetLocalIPAddress()
                                                  ,IsActive                                     
                                     }, "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", Success);
                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                            {
                                string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", Warning);
                            }
                            else
                            {
                                string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Danger!", Danger);
                            }

                        }
                    }
                }
                else if (btnSubmit.Text == "Update")
                {
                    ds = objdb.ByProcedure("Usp_JabalpurHeadLoadChargesMaster",
                                          new string[] {"flag",
                                                     "JbpHeadLoadCharges_ID",
                                                     "EffectiveDate", 
                                                     "Office_ID", 
                                                     "Office_Parant_ID",
                                                     "Distance", 
                                                     "MilkQuantityRange", 
                                                     "MinimumMilkQuanity",
                                                     "MaximumMilkQuanity",
                                                     "RateperPali", 
                                                     "CreatedAt", 
                                                     "CreatedBy", 
                                                     "CreatedByIP", 
                                                     
                                                   },
                                         new string[] {"3"
                                                  ,ViewState["JbpHeadLoadCharges_ID"].ToString()
                                                  ,Convert.ToDateTime(txtEffectiveDate.Text,cult).ToString("yyyy/MM/dd")
                                                  ,ddlSociety.SelectedValue
                                                  ,objdb.Office_ID()
                                                  ,txtDistance.Text
                                                  ,ddlMilkQuantity.SelectedValue
                                                  ,Minimum
                                                  ,Maximum
                                                  ,txtRateperPali.Text
                                                  ,objdb.Office_ID()
                                                  ,objdb.createdBy()
                                                  ,objdb.GetLocalIPAddress()
                                                                                       
                                     }, "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", Success);
                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                            {
                                string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", Warning);
                            }
                            else
                            {
                                string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Danger!", Danger);
                            }

                        }
                    }
                }
                FillGrid();
                btnSubmit.Text = "Save";
                ddlMilkCollectionUnit.Enabled = true;
                ddlSociety.Enabled = true;
                ddlMilkCollectionUnit.ClearSelection();
                ddlMilkCollectionUnit_SelectedIndexChanged(sender, e);
                txtDistance.Text = "";
                ddlSociety.ClearSelection();
                ddlMilkQuantity.ClearSelection();
                txtRateperPali.Text = "";
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            gvDetail.DataSource = string.Empty;
            gvDetail.DataBind();
            ds = objdb.ByProcedure("Usp_JabalpurHeadLoadChargesMaster", new string[] { "flag", "Office_Parant_ID" }, new string[] { "2", objdb.Office_ID() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDetail.DataSource = ds;
                    gvDetail.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            string JbpHeadLoadCharges_ID = e.CommandArgument.ToString();
            ViewState["JbpHeadLoadCharges_ID"] = JbpHeadLoadCharges_ID;
            Label lblEffectiveDate = (Label)gvr.FindControl("lblEffectiveDate");
            Label lblOffice_ID = (Label)gvr.FindControl("lblOffice_ID");
            Label lblOfficeType_ID = (Label)gvr.FindControl("lblOfficeType_ID");
            Label lblIDistance = (Label)gvr.FindControl("lblIDistance");
            Label lblMilkQuantityRange = (Label)gvr.FindControl("lblMilkQuantityRange");
            Label lblRateperPali = (Label)gvr.FindControl("lblRateperPali");
            
            if(e.CommandName == "EditRecord")
            {
                txtEffectiveDate.Text = lblEffectiveDate.Text;
                ddlMilkCollectionUnit.ClearSelection();
                ddlMilkCollectionUnit.SelectedValue = lblOfficeType_ID.Text;
                ddlMilkCollectionUnit_SelectedIndexChanged(sender, e);
                ddlSociety.ClearSelection();
                ddlSociety.SelectedValue = lblOffice_ID.Text;
                txtDistance.Text = lblIDistance.Text;
                ddlMilkQuantity.ClearSelection();
                ddlMilkQuantity.SelectedValue = lblMilkQuantityRange.Text;
                txtRateperPali.Text = lblRateperPali.Text;
                btnSubmit.Text = "Update";
                ddlMilkCollectionUnit.Enabled = false;
                ddlSociety.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}