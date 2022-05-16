using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web;

public partial class mis_MilkCollection_AdditionDeductionPriorityMaster : System.Web.UI.Page
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
                FillOffice();
                FillDetails();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
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
                ddlDS.SelectedValue = ViewState["Office_ID"].ToString();
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
    protected void ddlHeadType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (ddlHeadType.SelectedValue != "0")
            {
                ds = null;
                 ds = objdb.ByProcedure("USP_Mst_ItemBillingHeadMaster",
               new string[] { "flag", "ItemBillingHead_Type", "Office_ID", "OfficeType_ID" },
               new string[] { "8", ddlHeadType.SelectedValue,objdb.Office_ID(),objdb.OfficeType_ID() }, "dataset");

                ddlHeaddetails.DataTextField = "ItemBillingHead_Name";
                ddlHeaddetails.DataValueField = "ItemBillingHead_ID";
                ddlHeaddetails.DataSource = ds;
                ddlHeaddetails.DataBind();
                ddlHeaddetails.Items.Insert(0, new ListItem("Select", "0"));

            }
            else
            {
                ddlHeadType.ClearSelection();
                ddlHeaddetails.Items.Clear();
                ddlHeaddetails.Items.Add(new ListItem("Select", "0"));
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    protected void ddlHeaddetails_Init(object sender, EventArgs e)
    {
        try
        {
            ddlHeaddetails.Items.Add(new ListItem("Select", "0"));
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
            if(btnSave.Text == "Save")
            {
                ds = objdb.ByProcedure("Usp_Mst_SetAdditionDeductionPriority", new string[] {"flag",
                                                                                         "Office_ID",
                                                                                         "EffectiveDate",
                                                                                         "ItemBillingHead_ID",
                                                                                         "ItemBillingHead_Type",
                                                                                         "PriorityNo",
                                                                                         "IsActive",
                                                                                         "CreatedBy",
                                                                                         "CreatedByIP"},
                                                                              new string[] { "0",
                                                                                          ddlDS.SelectedValue,
                                                                                          Convert.ToDateTime(txtEffectiveDate.Text,cult).ToString("yyyy/MM/dd"),
                                                                                          ddlHeaddetails.SelectedValue,
                                                                                          ddlHeadType.SelectedValue,
                                                                                          txtPriorityNo.Text,
                                                                                          IsActive,
                                                                                          objdb.createdBy(),
                                                                                          objdb.GetLocalIPAddress()}, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", Success.ToString());
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                            string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", Warning.ToString());
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Not Ok")
                        {
                            string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Danger.ToString());
                        }
                    }

                }
               
               
            }
            else if (btnSave.Text == "Update")
            {
                ds = objdb.ByProcedure("Usp_Mst_SetAdditionDeductionPriority", new string[] {"flag",
                                                                                         "ADPriorityID",
                                                                                         "EffectiveDate",
                                                                                         "ItemBillingHead_ID",
                                                                                         "ItemBillingHead_Type",
                                                                                         "PriorityNo",
                                                                                         "IsActive",
                                                                                         "CreatedBy",
                                                                                         "CreatedByIP"},
                                                                       new string[] { "3",
                                                                                          ViewState["ADPriorityID"].ToString(),
                                                                                          Convert.ToDateTime(txtEffectiveDate.Text,cult).ToString("yyyy/MM/dd"),
                                                                                          ddlHeaddetails.SelectedValue,
                                                                                          ddlHeadType.SelectedValue,
                                                                                          txtPriorityNo.Text,
                                                                                          IsActive,
                                                                                          objdb.createdBy(),
                                                                                          objdb.GetLocalIPAddress()}, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", Success.ToString());
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                            string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", Warning.ToString());
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Not Ok")
                        {
                            string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Danger.ToString());
                        }
                    }

                }
            }
            btnSave.Text = "Save";
            FillDetails();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillDetails()
    
    {
        try
        {
           // lblMsg.Text = "";
            gvDetail.DataSource = string.Empty;
            gvDetail.DataBind();
           DataSet ds1 = objdb.ByProcedure("Usp_Mst_SetAdditionDeductionPriority", new string[] { "flag", "Office_ID" }, new string[] {"2",objdb.Office_ID() }, "dataset");
           if (ds1 != null && ds1.Tables.Count > 0)
            {
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    gvDetail.DataSource = ds1.Tables[0];
                    gvDetail.DataBind();
                }
                else
                {
                    gvDetail.DataSource = string.Empty;
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
            lblMsg.Text = "";
            string ADPriorityID = e.CommandArgument.ToString();
            ViewState["ADPriorityID"] = ADPriorityID.ToString();
            if(e.CommandName == "EditRecord")
            {
                DataSet dsRecord = objdb.ByProcedure("Usp_Mst_SetAdditionDeductionPriority", new string[] { "flag", "ADPriorityID" }, new string[] { "4", ADPriorityID.ToString() }, "dataset");
                if (dsRecord != null && dsRecord.Tables.Count > 0)
                {
                    if (dsRecord.Tables[0].Rows.Count > 0)
                    {
                        txtEffectiveDate.Text = dsRecord.Tables[0].Rows[0]["EffectiveDate"].ToString();
                        ddlHeadType.ClearSelection();
                        ddlHeadType.Items.FindByValue(dsRecord.Tables[0].Rows[0]["ItemBillingHead_Type"].ToString()).Selected = true;
                        ddlHeadType_SelectedIndexChanged(sender, e);
                        ddlHeaddetails.ClearSelection();
                        ddlHeaddetails.Items.FindByValue(dsRecord.Tables[0].Rows[0]["ItemBillingHead_ID"].ToString()).Selected = true;
                        txtPriorityNo.Text = dsRecord.Tables[0].Rows[0]["PriorityNo"].ToString();
                        btnSave.Text = "Update";

                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
}