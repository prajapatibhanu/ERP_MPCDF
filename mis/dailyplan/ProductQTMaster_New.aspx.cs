using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Web.UI;

public partial class mis_dailyplan_ProductQTMaster_New : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

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
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('QC Testing Parameter Inserted/Updated Successfully!')", true);

                        }
                    }

                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["QCParameterID"] = "0";

                    FillOffice();
                    FillUnit();
                    FillTestParameter();
                    GetSectionView();

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


    protected void ddlParameter_Init(object sender, EventArgs e)
    {
        try
        {
            ddlParameter.Items.Insert(0, new ListItem("N/A", "0"));
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillTestParameter()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("spProductionQCProductMapping",
                                       new string[] { "flag" },
                                       new string[] { "7" }, "dataset");

            ddlParameter.DataSource = ds.Tables[0];
            ddlParameter.DataTextField = "QCParameterName";
            ddlParameter.DataValueField = "QCParameterID";
            ddlParameter.DataBind();
            ddlParameter.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

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
            ddlUnit.Items.Insert(0, new ListItem("N/A", "0"));

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

    protected void ddlPSection_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {

            if (ddlPSection.SelectedValue != "0")
            {
                ddlProduct.Items.Clear();
                ds = null;
                ds = objdb.ByProcedure("SpProductionProduct_RecipeMaster",
                      new string[] { "flag", "Office_ID", "ProductSection_ID" },
                      new string[] { "14", ddlDS.SelectedValue, ddlPSection.SelectedValue }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlProduct.DataSource = ds.Tables[0];
                    ddlProduct.DataTextField = "ItemTypeName";
                    ddlProduct.DataValueField = "ItemType_id";
                    ddlProduct.DataBind();

                    if (ddlPSection.SelectedValue == "1")
                    {
                        ddlProduct.Items.Insert(0, new ListItem("RMT", "-3"));
                        ddlProduct.Items.Insert(0, new ListItem("PMT", "-2"));
                        ddlProduct.Items.Insert(0, new ListItem("Silo", "-1"));
                        ddlProduct.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    if (ddlPSection.SelectedValue == "2")
                    {
                        ddlProduct.Items.Insert(0, new ListItem("Loose Product", "-4"));
                        ddlProduct.Items.Insert(0, new ListItem("Select", "0"));
                    }

                }
                else
                {
                    ddlProduct.Items.Clear();
                    ddlProduct.Items.Insert(0, new ListItem("Select", "0"));
                    ddlProduct.DataBind();
                }

            }
            else
            {
                ddlProduct.Items.Clear();
                ddlProduct.Items.Insert(0, new ListItem("Select", "0"));
                ddlProduct.DataBind();
            }

        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void ddlproductdetails_Init(object sender, EventArgs e)
    {
        try
        {
            ddlProduct.Items.Insert(0, new ListItem("Select", "0"));
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            if (ddlProduct.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("spProductionQCProductMapping"
                , new string[] { "flag", "Office_ID", "ProductID" }
                , new string[] { "11", ViewState["Office_ID"].ToString(), ddlProduct.SelectedValue.ToString() }, "dataset");
                if (ds != null)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = string.Empty;
                    GridView1.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["QCParameterID"] = GridView1.SelectedDataKey.Value.ToString();
            ds = objdb.ByProcedure("spProductionQCProductMapping", new string[] { "flag", "QCParameterID" }, new string[] { "5", ViewState["QCParameterID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlParameter.ClearSelection();
                ddlParameter.Items.FindByText(ds.Tables[0].Rows[0]["QCParameterName"].ToString()).Selected = true;
                //ddlParameter.SelectedItem.Text = ds.Tables[0].Rows[0]["QCParameterName"].ToString();
                ddlCalculationMethod.ClearSelection();
                ddlCalculationMethod.Items.FindByValue(ds.Tables[0].Rows[0]["CalculationMethod"].ToString()).Selected = true;

                if (ddlCalculationMethod.SelectedValue.ToString() == "Value" || ddlCalculationMethod.SelectedValue.ToString() == "Use By Days")
                {
                    ddlCalculationMethod_SelectedIndexChanged(sender, e);
                    
                    txtMinValue.Text = ds.Tables[0].Rows[0]["MinValue"].ToString();
                    txtMaxValue.Text = ds.Tables[0].Rows[0]["MaxValue"].ToString();
                    if (ds.Tables[0].Rows[0]["Unit"].ToString() != "")
                        // ddlUnit.Items.FindByValue(ds.Tables[0].Rows[0]["Unit"].ToString()).Selected = true; 
                        ddlUnit.SelectedValue = ds.Tables[0].Rows[0]["Unit"].ToString();


                }
                
                else
                {

                    ddlCalculationMethod_SelectedIndexChanged(sender, e);
                    ddloption.Items.FindByText(ds.Tables[0].Rows[0]["OptionDetails"].ToString()).Selected = true;
                    if (ds.Tables[0].Rows[0]["Unit"].ToString() != "")
                        // ddlUnit.Items.FindByValue(ds.Tables[0].Rows[0]["Unit"].ToString()).Selected = true; 
                        ddlUnit.SelectedValue = ds.Tables[0].Rows[0]["Unit"].ToString();
                }


                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
         {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            CheckBox chk = (CheckBox)GridView1.Rows[selRowIndex].FindControl("chkSelect");
            string QCParameterID = chk.ToolTip.ToString();
            string IsActive = "0";
            if (chk != null & chk.Checked)
            {
                IsActive = "1";
            }
            objdb.ByProcedure("spProductionQCProductMapping",
                       new string[] { "flag", "IsActive", "QCParameterID", "UpdatedBy" },
                       new string[] { "4", IsActive, QCParameterID, ViewState["Emp_ID"].ToString() }, "dataset");
            FillGrid();
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

                if (btnSave.Text == "Save" && ViewState["QCParameterID"].ToString() == "0")
                {
                    ds = objdb.ByProcedure("spProductionQCProductMapping",
                    new string[] { "flag", "Office_ID", "ProductID", "QCParameterName", "CalculationMethod", "MinValue", "MaxValue", "Unit", "UpdatedBy", "IsActive", "ProductSection_ID", "OptionDetails" },
                    new string[] { "0", ViewState["Office_ID"].ToString(), ddlProduct.SelectedValue.ToString(), ddlParameter.SelectedItem.Text, ddlCalculationMethod.SelectedValue.ToString(), txtMinValue.Text, txtMaxValue.Text, ddlUnit.SelectedValue, ViewState["Emp_ID"].ToString(), "1", ddlPSection.SelectedValue, ddloption.SelectedValue }, "dataset");

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows[0]["Status"].ToString() == "True")
                        {
                            ViewState["QCParameterID"] = "0";
                            Session["IsSuccess"] = true;
                            Response.Redirect("ProductQTMaster_New.aspx", false);
                            // lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                            // FillGrid();
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Parameter Name already exist.');", true);
                        }
                    }

                }

                else if (btnSave.Text == "Update" && ViewState["QCParameterID"].ToString() != "0")
                {
                    ds = objdb.ByProcedure("spProductionQCProductMapping",
                   new string[] { "flag", "QCParameterID", "Office_ID", "ProductID", "QCParameterName", "CalculationMethod", "MinValue", "MaxValue", "Unit", "UpdatedBy", "OptionDetails" },
                   new string[] { "3", ViewState["QCParameterID"].ToString(), ViewState["Office_ID"].ToString(), ddlProduct.SelectedValue.ToString(), ddlParameter.SelectedItem.Text, ddlCalculationMethod.SelectedValue.ToString(), txtMinValue.Text, txtMaxValue.Text, ddlUnit.SelectedValue, ViewState["Emp_ID"].ToString(),ddloption.SelectedValue }, "dataset");

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows[0]["Status"].ToString() == "True")
                        {
                            ViewState["QCParameterID"] = "0";
                            Session["IsSuccess"] = true;
                            Response.Redirect("ProductQTMaster_New.aspx", false);
                            // lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                            // FillGrid();
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Test Parameter Name already exist.');", true);
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


    protected void ddlCalculationMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
             //if (ddlProduct.SelectedItem.Text == "Silo" || ddlProduct.SelectedItem.Text == "RMT" || ddlProduct.SelectedItem.Text == "PMT" || ddlProduct.SelectedItem.Text == "Loose Product")
            //{
            //    DivCM_Value1.Visible = false;
            //    DivCM_Value2.Visible = false;
            //    DivCM_Option.Visible = false;
            //}
            //else
            //{

            //}

                if (ddlCalculationMethod.SelectedValue == "Value" || ddlCalculationMethod.SelectedValue == "Use By Days")
                {
                    DivCM_Value1.Visible = true;
                    DivCM_Value2.Visible = true;
                    DivCM_Option.Visible = false;
                }

                if (ddlCalculationMethod.SelectedValue == "Adulteration Test" || ddlCalculationMethod.SelectedValue == "Manufacturing Date" || ddlCalculationMethod.SelectedValue == "OT")
                {

                    DivCM_Value1.Visible = false;
                    DivCM_Value2.Visible = false;
                    DivCM_Option.Visible = true;

                    if (ddlCalculationMethod.SelectedValue == "Adulteration Test")
                    {
                        ddloption.Items.Clear();
                        ddloption.Items.Insert(0, new ListItem("Select", "0"));
                        ddloption.Items.Insert(1, new ListItem("Negative", "Negative"));
                        ddloption.Items.Insert(2, new ListItem("Positive", "Positive"));
                    }

                    if (ddlCalculationMethod.SelectedValue == "Manufacturing Date")
                    {
                        ddloption.Items.Clear();
                        ddloption.Items.Insert(0, new ListItem("Select", "0"));
                        ddloption.Items.Insert(1, new ListItem("Yes", "Yes"));
                        ddloption.Items.Insert(2, new ListItem("No", "No"));

                    }

                    if (ddlCalculationMethod.SelectedValue == "OT")
                    {
                        ddloption.Items.Clear();
                       DataSet ds1 = objdb.ByProcedure("USP_Mst_MilkQualityList",
                                    new string[] { "flag" },
                                    new string[] { "1" }, "dataset"); ;
                        ddloption.DataSource = ds1;
                        ddloption.DataValueField = "V_MilkQualityList";
                        ddloption.DataTextField = "V_MilkQualityList";
                        ddloption.DataBind();
                        ddloption.Items.Insert(0, new ListItem("Select", "0"));
                    }


                }
            

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }


    protected void Clear()
    {
        txtV_ParameterName.Text = "";
        chkIsActive.Checked = true;
    }

    protected void GetData()
    {
        try
        {
            gv_ParameterDetails.DataSource = objdb.ByProcedure("spProductionQCProductMapping",
                            new string[] { "flag" },
                            new string[] { "8" }, "dataset");
            gv_ParameterDetails.DataBind();
        }
        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
        }
    }

    protected void lblAddTanker_Click(object sender, EventArgs e)
    {
        GetData();
        Clear();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
    }

    protected void btnCrossButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductQTMaster_New.aspx", false);
    }


    protected void btnYesT_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                lblPopupMsg.Text = "";
                int isactive = 1;
                if (chkIsActive.Checked)
                {
                    isactive = 1;
                }
                else
                {
                    isactive = 0;
                }

                if (btnSavePMDetails.Text == "Save")
                {
                    ds = null;
                    ds = objdb.ByProcedure("spProductionQCProductMapping", new string[] { "flag", "QCParameterName", "IsActive" },
                                                                           new string[] { "9", txtV_ParameterName.Text, isactive.ToString() }, "dataset");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblPopupMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        Clear();
                        GetData();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
                    }
                    else
                    {

                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                        if (error == "Already Exists.")
                        {
                            lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Data No Already Exists");
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
                        }
                        else
                        {
                            lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + error);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
                        }
                    }
                }
                if (btnSavePMDetails.Text == "Update")
                {
                    ds = null;
                    ds = objdb.ByProcedure("spProductionQCProductMapping", new string[] { "flag", "QCParameterName", "IsActive", "QCParameterID" },
                                                                          new string[] { "10", txtV_ParameterName.Text, isactive.ToString(), ViewState["rowid"].ToString() }, "dataset");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {

                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                        lblPopupMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        Clear();
                        GetData();
                        btnSavePMDetails.Text = "Save";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                        if (error == "Already Exists.")
                        {
                            lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Data No Already Exists");
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
                        }
                        else
                        {
                            lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + error);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
                        }
                    }
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
            }
        }

        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
        }
    }


    protected void gv_ParameterDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblPopupMsg.Text = string.Empty;

            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                    Label lblIsActive = (Label)row.FindControl("lblIsActive");

                    Label lblParameterName = (Label)row.FindControl("lblParameterName");

                    if (lblIsActive.Text == "InActive")
                    {
                        chkIsActive.Checked = false;
                    }
                    else
                    {
                        chkIsActive.Checked = true;
                    }

                    txtV_ParameterName.Text = lblParameterName.Text;

                    ViewState["rowid"] = e.CommandArgument;

                    btnSavePMDetails.Text = "Update";

                    foreach (GridViewRow gvRow in gv_ParameterDetails.Rows)
                    {
                        if (gv_ParameterDetails.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            gv_ParameterDetails.SelectedIndex = gvRow.DataItemIndex;
                            gv_ParameterDetails.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                }

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
            }

        }
        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
        }

    }



}