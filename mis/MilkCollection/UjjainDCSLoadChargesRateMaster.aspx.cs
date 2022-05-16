using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Web;
using System.Web.UI;

public partial class mis_MilkCollection_UjjainDCSLoadChargesRateMaster : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            if (!IsPostBack)
            {
                //Get Page Token
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                
                //FillSociety();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDate.Attributes.Add("readonly", "readonly");
                FillOffice();
                GetDistanceTable();
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
    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "12" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDS.DataSource = ds.Tables[0];
                ddlDS.DataTextField = "Office_Name";
                ddlDS.DataValueField = "Office_ID";
                ddlDS.DataBind();
                ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                ddlDS.SelectedValue = objdb.Office_ID();
                ddlDS.Enabled = false;              

            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetDistanceTable()
    {
        DataTable dtDistance = new DataTable();
        dtDistance.Columns.Add("DistanceKmRange", typeof(string));
        dtDistance.Columns.Add("MaxDistance", typeof(string));
        dtDistance.Columns.Add("MinDistance", typeof(string));
        dtDistance.Columns.Add("Rate", typeof(string));

        ViewState["Distance"] = dtDistance;
        gvDetail.DataSource = dtDistance;
        gvDetail.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            int Status = 0;
            foreach(GridViewRow rows in gvDetail.Rows)
            {
                Label lblDist = (Label)rows.FindControl("lblDist");
                if(lblDist.Text == ddlDistanceInKm.SelectedValue)
                {
                    Status = 1;
                    break;
                }
            }
            if(Status == 0)
            {
                string Maximum = "0";
                string Minimum = "0";
                if(ddlDistanceInKm.SelectedValue == "16")
                {
                    Minimum = "0";
                    Maximum = "16";
                }
                else if (ddlDistanceInKm.SelectedValue == "16.1-28")
                {
                    Minimum = "16.1";
                    Maximum = "28";
                }
                else if (ddlDistanceInKm.SelectedValue == "Above 40.1")
                {
                    Minimum = "28.1";
                    Maximum = "10000";
                }
                DataTable dtDistance = (DataTable)ViewState["Distance"];
                dtDistance.Rows.Add(ddlDistanceInKm.SelectedItem.Text,Maximum,Minimum, txtRate.Text);
                ViewState["Distance"] = dtDistance;
                gvDetail.DataSource = dtDistance;
                gvDetail.DataBind();
                ddlDistanceInKm.ClearSelection();
                txtRate.Text = "";
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Record Already Exists");

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
            lblMsg.Text = "";
            if(e.CommandName == "DeleteRecord")
            {
                string Distance = e.CommandArgument.ToString();
                DataTable dtDistance = (DataTable)ViewState["Distance"];
                int count = dtDistance.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    DataRow dr = dtDistance.Rows[i];
                    if (dr["Distance"].ToString() == Distance.ToString())
                    {
                        dr.Delete();
                        break;
                    }
                }
                dtDistance.AcceptChanges();
                ViewState["Distance"] = dtDistance;
                gvDetail.DataSource = dtDistance;
                gvDetail.DataBind();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string IsActive = "1";
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                DataTable dt = (DataTable)ViewState["Distance"];
                string Maximum = "0";
                string Minimum = "0";
                if (ddlMilkQuantity.SelectedValue == "21-50")
                {
                    Minimum = "21";
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
                else if (ddlMilkQuantity.SelectedValue == "201-400")
                {
                    Minimum = "201";
                    Maximum = "400";
                }
              
                else if (ddlMilkQuantity.SelectedValue == "Above 401")
                {
                    Minimum = "401";
                    Maximum = "10000";
                }
                if (dt.Rows.Count > 0)
                {
                    ds = objdb.ByProcedure("SP_Mst_DCSHeadLoadCharges",
                                            new string[] 
                                        {"flag",
                                         "Office_ID",
                                         "EffectiveDate",
                                         "MilkQualityRange",
                                         "Maximum",
                                         "Minimum",
                                         "IsActive",
                                         "CreatedAt",
                                         "CreatedBy",
                                         "CreatedByIP"
                                        },
                                            new string[] 
                                        {"1",
                                         objdb.Office_ID(),
                                         Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                         ddlMilkQuantity.SelectedValue,
                                         Maximum,
                                         Minimum,
                                         IsActive,
                                         objdb.Office_ID(),
                                         objdb.createdBy(),
                                         objdb.GetLocalIPAddress(),
                                        },
                                            new string[] 
                                        {"type_Mst_HeadLoadChargesChild"
                                        },
                                             new DataTable[] { dt },
                                            "TableSave");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou!", Success.ToString());

                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                            {
                                string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", Warning.ToString());

                            }
                            else
                            {
                                string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Danger.ToString());
                            }
                            ClearText();
                            FillGrid();
                        }
                    }

                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
       // lblMsg.Text = "";
        ddlMilkQuantity.ClearSelection();
        GetDistanceTable();
    }
    protected void FillGrid()
    {
        try
        {
            ds = objdb.ByProcedure("SP_Mst_DCSHeadLoadCharges", new string[] { "flag", "Office_ID" }, new string[] {"2",objdb.Office_ID() }, "dataset");
            if(ds!= null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    gvReports.DataSource = ds;
                    gvReports.DataBind();
                }
                else
                {
                    gvReports.DataSource = string.Empty;
                    gvReports.DataBind();
                }
            }
            else
            {
                gvReports.DataSource = string.Empty;
                gvReports.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvReports_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = "";
        GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

        Label lblEffectiveDate = (Label)row.FindControl("lblEffectiveDate");
        Label lblHeadLoadCharges_ID = (Label)row.FindControl("lblHeadLoadCharges_ID");
        
        Label lblMilkQualityRange = (Label)row.FindControl("lblMilkQualityRange");
        Label lblRate = (Label)row.FindControl("lblRate");
        Label lblDistanceKmRange = (Label)row.FindControl("lblDistanceKmRange");
        TextBox txtEffDate = (TextBox)row.FindControl("txtDate");
        DropDownList ddlMilkQuantity = (DropDownList)row.FindControl("ddlMilkQuantity");
        TextBox txtRate = (TextBox)row.FindControl("txtRate");
        DropDownList ddlDistanceInKm = (DropDownList)row.FindControl("ddlDistanceInKm");
        LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
        LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
        if(e.CommandName == "EditRecord")
        {
            
            lblEffectiveDate.Visible = false;
            lblMilkQualityRange.Visible = false;
            lblRate.Visible = false;
            lblDistanceKmRange.Visible = false;
            lnkEdit.Visible = false;
            txtEffDate.Visible = true;
            ddlMilkQuantity.Visible = true;
            txtRate.Visible = true;
            ddlDistanceInKm.Visible = true;
            lnkUpdate.Visible = true;
            txtEffDate.Text = lblEffectiveDate.Text;
            ddlMilkQuantity.SelectedValue = lblMilkQualityRange.Text;
            ddlDistanceInKm.SelectedValue = lblDistanceKmRange.Text;
            txtRate.Text = lblRate.Text;

        }
        if(e.CommandName == "UpdateRecord")
        {
            string HeadLoadChargesChild_ID = e.CommandArgument.ToString();
            string MilkQtyMaximum = "0";
            string MilkQtyMinimum = "0";
            string DistMaximum = "0";
            string DistMinimum = "0";
            if (ddlMilkQuantity.SelectedValue == "21-50")
            {
                MilkQtyMinimum = "21";
                MilkQtyMaximum = "50";
            }
            else if (ddlMilkQuantity.SelectedValue == "51-100")
            {
                MilkQtyMinimum = "51";
                MilkQtyMaximum = "100";
            }
            else if (ddlMilkQuantity.SelectedValue == "101-200")
            {
                MilkQtyMinimum = "101";
                MilkQtyMaximum = "200";
            }
            else if (ddlMilkQuantity.SelectedValue == "201-400")
            {
                MilkQtyMinimum = "201";
                MilkQtyMaximum = "400";
            }

            else if (ddlMilkQuantity.SelectedValue == "Above 401")
            {
                MilkQtyMinimum = "401";
                MilkQtyMaximum = "10000";
            }
            
            if (ddlDistanceInKm.SelectedValue == "16")
            {
                DistMinimum = "0";
                DistMaximum = "16";
            }
            else if (ddlDistanceInKm.SelectedValue == "16.1-28")
            {
                DistMinimum = "16.1";
                DistMaximum = "28";
            }

            else if (ddlDistanceInKm.SelectedValue == "Above 40.1")
            {
                DistMinimum = "40.1";
                DistMaximum = "10000";
            }
            ds = objdb.ByProcedure("SP_Mst_DCSHeadLoadCharges",
                new string[] { "flag", "Office_ID", "HeadLoadCharges_ID", "HeadLoadChargesChild_ID", "EffectiveDate", "MilkQualityRange", "DistanceKmRange", "Rate", "MilkQtyMaximum", "MilkQtyMinimum", "DistMaximum", "DistMinimum" },
                new string[] { "3", objdb.Office_ID(), lblHeadLoadCharges_ID.Text, HeadLoadChargesChild_ID, Convert.ToDateTime(txtEffDate.Text, cult).ToString("yyyy/MM/dd"), ddlMilkQuantity.SelectedValue, ddlDistanceInKm.SelectedValue,txtRate.Text, MilkQtyMaximum, MilkQtyMinimum, DistMaximum, DistMinimum }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou!", Success.ToString());

                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                    {
                        string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", Warning.ToString());

                    }
                    else
                    {
                        string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Danger.ToString());
                    }
                    
                    FillGrid();
                }
            }
        }
    }
}