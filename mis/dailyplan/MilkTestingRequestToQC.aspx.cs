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

public partial class mis_dailyplan_MilkTestingRequestToQC : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    string Fdate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                lblMsg.Text = "";

                if (!IsPostBack)
                {

                    if (Session["IsSuccess"] != null)
                    {
                        if ((Boolean)Session["IsSuccess"] == true)
                        {
                            Session["IsSuccess"] = false;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Testing Request Successfully Send!')", true);

                        }
                    }
                    divVariant.Visible = false;
                    FillVariant();
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["QCParameterID"] = "0";
                    ViewState["TestRequest_ID"] = "0";
                    FillShift();
                    FillUnit();
                    FillOffice();
                    GetSectionView();
                    txtFilterDT_TextChanged(sender, e);

                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }

        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        try
        {


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    #region Init Function
    protected void ddlUnit_Init(object sender, EventArgs e)
    {
        try
        {
            ddlUnit.Items.Insert(0, new ListItem("N/A", "0"));
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    #endregion
    #region User Defined Function
    protected void FillUnit()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpUnit",
                                       new string[] { "flag" },
                                       new string[] { "1" }, "dataset");

            ddlUnit.DataSource = ds.Tables[0];
            ddlUnit.DataTextField = "UnitName";
            ddlUnit.DataValueField = "Unit_Id";
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillShift()
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                 new string[] { "flag" },
                 new string[] { "2" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlShift.DataSource = ds.Tables[0];
                ddlShift.DataTextField = "Name";
                ddlShift.DataValueField = "Shift_Id";
                ddlShift.DataBind();
                ddlShift.SelectedValue = ds.Tables[1].Rows[0]["Shift_Id"].ToString();
                txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                //Convert.ToDateTime(ds.Tables[1].Rows[0]["currentDate"].ToString(), cult).ToString("dd/MM/yyyy");
                txtDate.Enabled = false;
                ddlShift.Enabled = false;

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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


                ddlFilterOffice.DataSource = ds.Tables[0];
                ddlFilterOffice.DataTextField = "Office_Name";
                ddlFilterOffice.DataValueField = "Office_ID";
                ddlFilterOffice.DataBind();
                ddlFilterOffice.Items.Insert(0, new ListItem("Select", "0"));
                ddlFilterOffice.SelectedValue = ViewState["Office_ID"].ToString();
                ddlFilterOffice.Enabled = false;
                txtFilterDT.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

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

    private void GetSectionView()
    {

        try
        {

            ds = null;

            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID" },
                  new string[] { "6", ddlDS.SelectedValue }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPSection.DataSource = ds.Tables[0];
                ddlPSection.DataTextField = "ProductSection_Name";
                ddlPSection.DataValueField = "ProductSection_ID";
                ddlPSection.DataBind();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));

            }
            else
            {
                ddlPSection.Items.Clear();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void FillVariant()
    {
        try
        {
            lblMsg.Text = "";
            ddlVariant.DataSource = objdb.ByProcedure("Usp_Production_TankPosition",
                      new string[] { "flag" },
                      new string[] { "4" }, "dataset");
            ddlVariant.DataValueField = "ItemType_id";
            ddlVariant.DataTextField = "ItemTypeName";
            ddlVariant.DataBind();
            ddlVariant.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region GridView Row Event
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCalculationMethod = e.Row.FindControl("lblCalculationMethod") as Label;
                Label lblMinValue = e.Row.FindControl("lblMinValue") as Label;
                Label lblMaxValue = e.Row.FindControl("lblMaxValue") as Label;
                Label lblOptionDetails = e.Row.FindControl("lblOptionDetails") as Label;
                Label lblValueRang = e.Row.FindControl("lblValueRang") as Label;

                if (lblCalculationMethod.Text == "Value" || lblCalculationMethod.Text == "Use By Days")
                {
                    lblValueRang.Text = lblMinValue.Text + " - " + lblMaxValue.Text;
                }

                if (lblCalculationMethod.Text == "Adulteration Test" || lblCalculationMethod.Text == "Manufacturing Date" || lblCalculationMethod.Text == "OT")
                {
                    lblValueRang.Text = lblOptionDetails.Text;
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void gvResultDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCalculationMethod = e.Row.FindControl("lblCalculationMethod") as Label;
                Label lblMinValue = e.Row.FindControl("lblMinValue") as Label;
                Label lblMaxValue = e.Row.FindControl("lblMaxValue") as Label;
                Label lblOptionDetails = e.Row.FindControl("lblOptionDetails") as Label;
                Label lblValueRang = e.Row.FindControl("lblValueRang") as Label;

                if (lblCalculationMethod.Text == "Value" || lblCalculationMethod.Text == "Use By Days")
                {
                    lblValueRang.Text = " ( " + lblMinValue.Text + " - " + lblMaxValue.Text + " ) ";
                }

                if (lblCalculationMethod.Text == "Adulteration Test" || lblCalculationMethod.Text == "Manufacturing Date" || lblCalculationMethod.Text == "OT")
                {
                    lblValueRang.Text = lblOptionDetails.Text;
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvExport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblRowNumber = (Label)e.Row.FindControl("lblRowNumber");
            Label lblTestRequest_No = (Label)e.Row.FindControl("lblTestRequest_No");
            Label lblTestRequestFor = (Label)e.Row.FindControl("lblTestRequestFor");
            Label lblSampleBatch_No = (Label)e.Row.FindControl("lblSampleBatch_No");
            Label lblSampleLot_No = (Label)e.Row.FindControl("lblSampleLot_No");

            GridView gvResultDetail = (GridView)e.Row.FindControl("gvResultDetail");
            Label lblTestRequestType = (Label)e.Row.FindControl("lblTestRequestType");
            Label lblTestRequestFor_ID = (Label)e.Row.FindControl("lblTestRequestFor_ID");

            if (lblTestRequestType.Text == "Milk" || lblTestRequestType.Text == "Product")
            {

                ds = objdb.ByProcedure("spProductionMilkContainerMaster"
                , new string[] { "flag", "Office_ID", "TestRequest_ID", "ProductID" }
                , new string[] { "8", ViewState["Office_ID"].ToString(), lblRowNumber.ToolTip.ToString(), lblTestRequestFor_ID.Text }, "dataset");

                if (ds != null)
                {
                    gvResultDetail.DataSource = ds;
                    gvResultDetail.DataBind();

                }
                else
                {
                    gvResultDetail.DataSource = string.Empty;
                    gvResultDetail.DataBind();
                }
            }

            else
            {
                if (lblTestRequestType.Text == "Silo")
                {
                    lblTestRequestFor_ID.Text = "-1";
                }
                if (lblTestRequestType.Text == "PMT")
                {
                    lblTestRequestFor_ID.Text = "-2";
                }
                if (lblTestRequestType.Text == "RMT")
                {
                    lblTestRequestFor_ID.Text = "-3";
                }

                if (lblTestRequestType.Text == "Tank")
                {
                    lblTestRequestFor_ID.Text = "-4";
                }

                ds = objdb.ByProcedure("spProductionMilkContainerMaster"
                , new string[] { "flag", "Office_ID", "TestRequest_ID", "ProductID" }
                , new string[] { "11", ViewState["Office_ID"].ToString(), lblRowNumber.ToolTip.ToString(), lblTestRequestFor_ID.Text }, "dataset");
                if (ds != null)
                {
                    gvResultDetail.DataSource = ds;
                    gvResultDetail.DataBind();

                }
                else
                {
                    gvResultDetail.DataSource = string.Empty;
                    gvResultDetail.DataBind();
                }

            }


        }
    }
    #endregion
    #region Changed Event
    protected void ddlPSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divVariant.Visible = false;
            ddlTestRequestType.Items.Clear();
            ddlTestRequestType.Items.Insert(0, new ListItem("Select", "0"));
            ddlmilktestrequestfor.Items.Clear();
            ddlmilktestrequestfor.Items.Insert(0, new ListItem("Select", "0"));

            if (ddlPSection.SelectedValue == "1")
            {
                ddlTestRequestType.Items.Clear();
                ddlTestRequestType.Items.Insert(0, new ListItem("Select", "0"));
                ddlTestRequestType.Items.Insert(1, new ListItem("Silo", "Silo"));
                ddlTestRequestType.Items.Insert(2, new ListItem("PMT", "PMT"));
                ddlTestRequestType.Items.Insert(3, new ListItem("RMT", "RMT"));
                divVariant.Visible = true;
                //ddlTestRequestType.Items.Insert(4, new ListItem("Packaging[Milk]", "Milk"));
            }

            if (ddlPSection.SelectedValue == "2")
            {
                ddlTestRequestType.Items.Clear();
                ddlTestRequestType.Items.Insert(0, new ListItem("Select", "0"));
                ddlTestRequestType.Items.Insert(1, new ListItem("Loose Product", "Tank"));
                ddlTestRequestType.Items.Insert(2, new ListItem("Packaging[Product]", "Product"));

            }
            if (ddlPSection.SelectedValue == "9")
            {
                ddlTestRequestType.Items.Clear();
                ddlTestRequestType.Items.Insert(0, new ListItem("Select", "0"));
                ddlTestRequestType.Items.Insert(1, new ListItem("Packaging[Milk]", "Milk"));

            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlTestRequestType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTestRequestType.SelectedValue == "Milk" || ddlTestRequestType.SelectedValue == "Product")
            {
                string ddtestid = "";

                if (ddlTestRequestType.SelectedValue == "Milk")
                {
                    ddtestid = "1";
                }

                if (ddlTestRequestType.SelectedValue == "Product")
                {
                    ddtestid = "2";
                }

                ds = null;
                ds = objdb.ByProcedure("spProductionMilkContainerMaster",
                      new string[] { "flag", "ProductSection_ID", "Office_Id" },
                      new string[] { "3", ddtestid, objdb.Office_ID() }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlmilktestrequestfor.DataSource = ds.Tables[0];
                    ddlmilktestrequestfor.DataTextField = "ItemName";
                    ddlmilktestrequestfor.DataValueField = "Item_id";
                    ddlmilktestrequestfor.DataBind();
                    ddlmilktestrequestfor.Items.Insert(0, new ListItem("Select", "0"));

                }
                else
                {
                    ddlmilktestrequestfor.Items.Clear();
                    ddlmilktestrequestfor.Items.Insert(0, new ListItem("Select", "0"));
                    ddlmilktestrequestfor.DataBind();
                }
            }
            else
            {

                ds = null;
                 ds = objdb.ByProcedure("spProductionMilkContainerMaster",
                       new string[] { "flag", "V_MCType", "Office_Id" },
                       new string[] { "2", ddlTestRequestType.SelectedValue, objdb.Office_ID() }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlmilktestrequestfor.DataSource = ds.Tables[0];
                    ddlmilktestrequestfor.DataTextField = "MCDetail";
                    ddlmilktestrequestfor.DataValueField = "I_MCID";
                    ddlmilktestrequestfor.DataBind();
                    ddlmilktestrequestfor.Items.Insert(0, new ListItem("Select", "0"));

                }
                else
                {
                    ddlmilktestrequestfor.Items.Clear();
                    ddlmilktestrequestfor.Items.Insert(0, new ListItem("Select", "0"));
                    ddlmilktestrequestfor.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtFilterDT_TextChanged(object sender, EventArgs e)
    {
        try
        {
            btnExport.Visible = false;
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtFilterDT.Text, cult).ToString("yyyy/MM/dd");
            }


            GridView3.DataSource = null;
            GridView3.DataBind();
            gvExport.DataSource = null;
            gvExport.DataBind();

            ds = null;
            ds = objdb.ByProcedure("spProductionMilkContainerMaster"
            , new string[] { "flag", "Office_Id", "TestRequest_DT" }
            , new string[] { "9", ViewState["Office_ID"].ToString(), Fdate }, "dataset");

            if (ds != null)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    GridView3.DataSource = ds;
                    GridView3.DataBind();
                    gvExport.DataSource = ds;
                    gvExport.DataBind();

                    btnExport.Visible = true;
                }
                else
                {
                    GridView3.DataSource = string.Empty;
                    GridView3.DataBind();
                    gvExport.DataSource = string.Empty;
                    gvExport.DataBind();
                }
            }
            else
            {
                GridView3.DataSource = string.Empty;
                GridView3.DataBind();
                gvExport.DataSource = string.Empty;
                gvExport.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Button Click Event
    protected void btnCrossButton_Click(object sender, EventArgs e)
    {
        ddlTestRequestType_SelectedIndexChanged(sender, e);
    }

    protected void lbFilterReq_Click(object sender, EventArgs e)
    {

        try
        {
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            Label lblRowNumber = (Label)GridView3.Rows[selRowIndex].FindControl("lblRowNumber");
            Label lblTestRequest_No = (Label)GridView3.Rows[selRowIndex].FindControl("lblTestRequest_No");
            Label lblTestRequestFor = (Label)GridView3.Rows[selRowIndex].FindControl("lblTestRequestFor");
            Label lblSampleBatch_No = (Label)GridView3.Rows[selRowIndex].FindControl("lblSampleBatch_No");
            Label lblSampleLot_No = (Label)GridView3.Rows[selRowIndex].FindControl("lblSampleLot_No");

            Label2.Text = lblTestRequest_No.Text;
            Label2.ToolTip = lblRowNumber.ToolTip.ToString();
            Label3.Text = lblTestRequestFor.Text;
            Label4.Text = lblSampleBatch_No.Text;
            Label5.Text = lblSampleLot_No.Text;
            Label lblTestRequestType = (Label)GridView3.Rows[selRowIndex].FindControl("lblTestRequestType");
            Label lblTestRequestFor_ID = (Label)GridView3.Rows[selRowIndex].FindControl("lblTestRequestFor_ID");

            if (lblTestRequestType.Text == "Milk" || lblTestRequestType.Text == "Product")
            {

                ds = objdb.ByProcedure("spProductionMilkContainerMaster"
                , new string[] { "flag", "Office_ID", "TestRequest_ID", "ProductID" }
                , new string[] { "8", ViewState["Office_ID"].ToString(), lblRowNumber.ToolTip.ToString(), lblTestRequestFor_ID.Text }, "dataset");

                if (ds != null)
                {
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                    txtRR.Text = ds.Tables[0].Rows[0]["TestRequest_Remark"].ToString();
                    // lblmpR.Text = objdb.Alert("fa-check", "alert-success", ds.Tables[0].Rows[0]["TestRequest_Result_Status"].ToString(), "");
                }
                else
                {
                    GridView2.DataSource = string.Empty;
                    GridView2.DataBind();
                }
            }

            else
            {
                if (lblTestRequestType.Text == "Silo")
                {
                    lblTestRequestFor_ID.Text = "-1";
                }
                if (lblTestRequestType.Text == "PMT")
                {
                    lblTestRequestFor_ID.Text = "-2";
                }
                if (lblTestRequestType.Text == "RMT")
                {
                    lblTestRequestFor_ID.Text = "-3";
                }

                if (lblTestRequestType.Text == "Tank")
                {
                    lblTestRequestFor_ID.Text = "-4";
                }

                ds = objdb.ByProcedure("spProductionMilkContainerMaster"
                , new string[] { "flag", "Office_ID", "TestRequest_ID", "ProductID" }
                , new string[] { "11", ViewState["Office_ID"].ToString(), lblRowNumber.ToolTip.ToString(), lblTestRequestFor_ID.Text }, "dataset");
                if (ds != null)
                {
                    GridView2.DataSource = ds;
                    GridView2.DataBind();
                    txtRR.Text = ds.Tables[0].Rows[0]["TestRequest_Remark"].ToString();
                    // lblmpR.Text = objdb.Alert("fa-check", "alert-success", ds.Tables[0].Rows[0]["TestRequest_Result_Status"].ToString(), "");
                }
                else
                {
                    GridView2.DataSource = string.Empty;
                    GridView2.DataBind();
                }

            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ViewResultParameterModal()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {

            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                string ItemType_id = "0";
                if (txtSampleQuantity.Text == "")
                {
                    txtSampleQuantity.Text = "0";
                }
                if(ddlPSection.SelectedValue == "1")
                {
                    ItemType_id = ddlVariant.SelectedValue.ToString();
                }

                if (btnSave.Text == "Send" && ViewState["TestRequest_ID"].ToString() == "0")
                {
                    ds = null;
                    ds = objdb.ByProcedure("spProductionMilkContainerMaster",
                    new string[] { "flag", "Office_Id", "Shift_ID", "ProductSection_ID", "TestRequestType",
                        "TestRequestFor","ItemType_id", "SampleNameNo", "SampleQuantity", "Unit_Id", "SampleBatch_No", "SampleLot_No",
                        "CreatedBy","CreatedBy_IP","TestRequestFor_ID"},
                    new string[] { "4", ViewState["Office_ID"].ToString(),ddlShift.SelectedValue,ddlPSection.SelectedValue,
                        ddlTestRequestType.SelectedValue,ddlmilktestrequestfor.SelectedItem.Text,ItemType_id, txtSampleName.Text,txtSampleQuantity.Text,
                       ddlUnit.SelectedValue,txtSampleBatch_No.Text,txtSampleLot_No.Text,ViewState["Emp_ID"].ToString(),objdb.GetLocalIPAddress(),ddlmilktestrequestfor.SelectedValue }, "dataset");

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            ViewState["QCParameterID"] = "0";
                            Session["IsSuccess"] = true;
                            Response.Redirect("MilkTestingRequestToQC.aspx", false);


                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                            if (error == "Already Exists.")
                            {
                                Session["IsSuccess"] = false;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Testing Request already Send.');", true);

                            }
                            else
                            {
                                Session["IsSuccess"] = false;
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + error);
                            }
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

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            gvExport.Visible = true;

            string FileName = "MilkTestingRequestToQC";
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gvExport.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
            gvExport.Visible = false;
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    #endregion
   
}