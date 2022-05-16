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

public partial class mis_MilkCollection_GwaliorHeadLoadChargesMaster : System.Web.UI.Page
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
                GetCCDetails();
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
    //protected void ddlMilkCollectionUnit_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillSociety();
    //}
    protected void GetCCDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                     new string[] { "flag", "I_OfficeID", "I_OfficeTypeID" },
                     new string[] { "3", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlccbmcdetail.DataTextField = "Office_Name";
                        ddlccbmcdetail.DataValueField = "Office_ID";


                        ddlccbmcdetail.DataSource = ds;
                        ddlccbmcdetail.DataBind();

                        if (objdb.OfficeType_ID() == "2")
                        {
                            ddlccbmcdetail.Items.Insert(0, new ListItem("Select", "0"));
                        }
                        else
                        {
                            ddlccbmcdetail.SelectedValue = objdb.Office_ID();
                            //GetMCUDetails();
                            ddlccbmcdetail.Enabled = false;
                        }
                        ddlccbmcdetail_flt.DataTextField = "Office_Name";
                        ddlccbmcdetail_flt.DataValueField = "Office_ID";


                        ddlccbmcdetail_flt.DataSource = ds;
                        ddlccbmcdetail_flt.DataBind();

                        if (objdb.OfficeType_ID() == "2")
                        {
                            ddlccbmcdetail_flt.Items.Insert(0, new ListItem("All", "0"));
                        }
                        else
                        {
                            ddlccbmcdetail.SelectedValue = objdb.Office_ID();
                            //GetMCUDetails();
                            ddlccbmcdetail.Enabled = false;
                        }


                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillSociety()
    {
        try
        {
            ddlSociety.ClearSelection();
            ddlSociety.Items.Clear();
            if (ddlccbmcdetail.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("Usp_MilkCollectionRoutWiseAdditionsDeductionsEntry",
                         new string[] { "flag", "Office_ID", "Office_Parant_ID", "OfficeType_ID" },
                         new string[] { "8", ddlccbmcdetail.SelectedValue.ToString(), objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSociety.DataTextField = "Office_Name";
                        ddlSociety.DataValueField = "Office_ID";
                        ddlSociety.DataSource = ds.Tables[0];
                        ddlSociety.DataBind();
                       // ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    else
                    {
                        //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));

                    }
                }
                else
                {
                    //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                // Response.Redirect("SocietywiseMilkCollectionInvoiceDetails.aspx", false);
            }
            ddlSociety.Items.Insert(0, new ListItem("Select", "0"));

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
                
                if(btnSubmit.Text == "Save")
                {
                    ds = objdb.ByProcedure("Usp_GwaliorHeadLoadChargesMaster",
                                          new string[] {"flag",
                                                     "EffectiveDate", 
                                                     "UpToEffective",
                                                     "Office_ID",                                                     
                                                     "Office_Parant_ID", 
													 "CC_ID",													 
                                                     "Rateperlitre", 
                                                     "CreatedAt", 
                                                     "CreatedBy", 
                                                     "CreatedByIP", 
                                                     "IsActive"
                                                   },
                                         new string[] {"1"
                                                  ,Convert.ToDateTime(txtEffectiveDate.Text,cult).ToString("yyyy/MM/dd")
                                                  ,Convert.ToDateTime(txtUptoEffectiveDate.Text,cult).ToString("yyyy/MM/dd")
                                                  ,ddlSociety.SelectedValue
                                                  ,objdb.Office_ID()
                                                  ,ddlccbmcdetail.SelectedValue
                                                  ,txtRateperlitre.Text
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
                    ds = objdb.ByProcedure("Usp_GwaliorHeadLoadChargesMaster",
                                          new string[] {"flag",
                                                     "GwalrHeadLoadCharges_ID",
                                                     "EffectiveDate",
                                                     "UpToEffective",
                                                     "Office_ID", 
                                                     "Office_Parant_ID",
                                                     "Rateperlitre", 
                                                     "CreatedAt", 
                                                     "CreatedBy", 
                                                     "CreatedByIP", 
                                                     
                                                   },
                                         new string[] {"3"
                                                  ,ViewState["GwalrHeadLoadCharges_ID"].ToString()
                                                  ,Convert.ToDateTime(txtEffectiveDate.Text,cult).ToString("yyyy/MM/dd")
                                                  ,Convert.ToDateTime(txtUptoEffectiveDate.Text,cult).ToString("yyyy/MM/dd")
                                                  ,ddlSociety.SelectedValue
                                                  ,objdb.Office_ID()
                                                  ,txtRateperlitre.Text
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
                
                ddlSociety.Enabled = true;
                ddlccbmcdetail.Enabled = true;
                ddlccbmcdetail.ClearSelection();
                ddlccbmcdetail_SelectedIndexChanged(sender, e);
                
                ddlSociety.ClearSelection();
                
                txtRateperlitre.Text = "";
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
            ds = objdb.ByProcedure("Usp_GwaliorHeadLoadChargesMaster", new string[] { "flag", "Office_Parant_ID", "CC_ID" }, new string[] { "2", objdb.Office_ID(),ddlccbmcdetail_flt.SelectedValue }, "dataset");
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
            ViewState["GwalrHeadLoadCharges_ID"] = JbpHeadLoadCharges_ID;
            Label lblEffectiveDate = (Label)gvr.FindControl("lblEffectiveDate");
            Label lblUptoEffective = (Label)gvr.FindControl("lblUptoEffective");
            Label lblOffice_ID = (Label)gvr.FindControl("lblOffice_ID");
            Label lblOfficeType_ID = (Label)gvr.FindControl("lblOfficeType_ID");
            Label lblRateperlitre = (Label)gvr.FindControl("lblRateperlitre");
            Label lblCC_ID = (Label)gvr.FindControl("lblCC_ID");
            
            if(e.CommandName == "EditRecord")
            {
                txtEffectiveDate.Text = lblEffectiveDate.Text;
                txtUptoEffectiveDate.Text = lblUptoEffective.Text;
                ddlccbmcdetail.ClearSelection();
                ddlccbmcdetail.SelectedValue = lblCC_ID.Text;
                ddlccbmcdetail_SelectedIndexChanged(sender, e);
                //ddlMilkCollectionUnit.ClearSelection();
                //ddlMilkCollectionUnit.SelectedValue = lblOfficeType_ID.Text;
                //ddlMilkCollectionUnit_SelectedIndexChanged(sender, e);
                ddlSociety.ClearSelection();
                ddlSociety.SelectedValue = lblOffice_ID.Text;                
                txtRateperlitre.Text = lblRateperlitre.Text;
                btnSubmit.Text = "Update";
                //ddlMilkCollectionUnit.Enabled = false;
                ddlSociety.Enabled = false;
                ddlccbmcdetail.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlccbmcdetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSociety();
    }
    protected void ddlccbmcdetail_flt_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
}