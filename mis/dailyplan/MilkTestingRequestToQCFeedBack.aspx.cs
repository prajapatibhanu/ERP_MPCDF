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

public partial class mis_dailyplan_MilkTestingRequestToQCFeedBack : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    string Fdate = "";
    MilkCalculationClass Obj_MC = new MilkCalculationClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                lblMsg.Text = "";
                
                if (!IsPostBack)
                {
                    ViewState["ItemType_id"] = "";
                    if (Session["IsSuccess"] != null)
                    {
                        if ((Boolean)Session["IsSuccess"] == true)
                        {
                            Session["IsSuccess"] = false;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Testing Request Successfully Send!')", true);

                        }
                    }

                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();

                    ViewState["TestRequestFor_ID"] = "0";
                    ViewState["TestRequest_ID"] = "0";

                    FillShift();
                    FillOffice();
                    GetSectionView();
                    Timer1_Tick(sender, e);
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

    #region User Defined Function
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
                //ddlShift.Enabled = false;
                txtDate.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["currentDate"].ToString(), cult).ToString("dd/MM/yyyy");

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

    protected void FillGrid()
    {
        try
        {
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }
            btnExport.Visible = false;
            gvtestingrequestdetail.DataSource = null;
            gvtestingrequestdetail.DataBind();
            GvExportDetail.DataSource = null;
            GvExportDetail.DataBind();

            ds = null;
            ds = objdb.ByProcedure("spProductionMilkContainerMaster"
            , new string[] { "flag", "Office_Id", "Shift_ID", "ProductSection_ID", "TestRequestType", "TestRequest_DT" }
            , new string[] { "5", ViewState["Office_ID"].ToString(), ddlShift.SelectedValue, ddlPSection.SelectedValue, ddlTestRequestType.SelectedValue, Fdate }, "dataset");

            if (ds != null)
            {
                gvtestingrequestdetail.DataSource = ds;
                gvtestingrequestdetail.DataBind();
                GvExportDetail.DataSource = ds;
                GvExportDetail.DataBind();
                btnExport.Visible = true;
            }
            else
            {
                gvtestingrequestdetail.DataSource = string.Empty;
                gvtestingrequestdetail.DataBind();
                GvExportDetail.DataSource = string.Empty;
                GvExportDetail.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    private DataTable GetTestResult()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        decimal MinVal = 0;
        decimal MaxVal = 0;
        decimal FinalVal = 0;
        string TResult_P = "";

        dt.Columns.Add(new DataColumn("TestRequest_ID", typeof(int)));
        dt.Columns.Add(new DataColumn("TestRequestFor", typeof(string)));
        dt.Columns.Add(new DataColumn("QCParameterName", typeof(string)));
        dt.Columns.Add(new DataColumn("TestRequest_Result", typeof(string)));
        dt.Columns.Add(new DataColumn("TestFinalResult", typeof(string)));

        foreach (GridViewRow row in GridView1.Rows)
        {
            Label lblCalculationMethod = (Label)row.FindControl("lblCalculationMethod");
            TextBox txtvalue = (TextBox)row.FindControl("txtvalue");
            DropDownList ddloption = (DropDownList)row.FindControl("ddloption");
            Label lblQCParameterName = (Label)row.FindControl("lblQCParameterName");
            Label lblMinValue = (Label)row.FindControl("lblMinValue");
            Label lblMaxValue = (Label)row.FindControl("lblMaxValue");
            Label lblOptionDetails = (Label)row.FindControl("lblOptionDetails");

            if (lblCalculationMethod.Text == "Value" || lblCalculationMethod.Text == "Use By Days")
            {
                if (txtvalue.Text != "")
                {
                    if (lblMinValue.Text == "")
                    {
                        lblMinValue.Text = "0";
                    }
                    if (lblMaxValue.Text == "")
                    {
                        lblMaxValue.Text = "0";
                    }

                    MinVal = Convert.ToDecimal(lblMinValue.Text);
                    MaxVal = Convert.ToDecimal(lblMaxValue.Text);
                    FinalVal = Convert.ToDecimal(txtvalue.Text);

                    if (FinalVal >= MinVal && FinalVal <= MaxVal)
                    {
                        TResult_P = "Pass";
                    }
                    else
                    {
                        TResult_P = "Failed";
                    }

                    if (lbltesttype.Text == "Milk" || lbltesttype.Text == "Product")
                    {

                    }
                    else
                    {
                        TResult_P = "Submitted";
                    }

                    dr = dt.NewRow();
                    dr[0] = lblRequestNo.ToolTip.ToString();
                    dr[1] = lblRequestFor.Text;
                    dr[2] = lblQCParameterName.Text;
                    dr[3] = TResult_P;
                    dr[4] = txtvalue.Text;
                    dt.Rows.Add(dr);
                }

            }

            if (lblCalculationMethod.Text == "Adulteration Test" || lblCalculationMethod.Text == "Manufacturing Date" || lblCalculationMethod.Text == "OT")
            {
                if (ddloption.SelectedValue != "0")
                {
                    if (lblOptionDetails.Text == ddloption.SelectedItem.Text)
                    {
                        TResult_P = "Pass";
                    }
                    else
                    {
                        if (ddloption.SelectedItem.Text == "Satisfactory")
                        {
                            TResult_P = "Pass";
                        }
                        else
                        {
                            TResult_P = "Failed";
                        }
                    }

                    //else
                    //{
                    //    TResult_P = "Failed";
                    //}

                    if (lbltesttype.Text == "Milk" || lbltesttype.Text == "Product")
                    {

                    }
                    else
                    {
                        TResult_P = "Submitted";
                    }

                    dr = dt.NewRow();
                    dr[0] = lblRequestNo.ToolTip.ToString();
                    dr[1] = lblRequestFor.Text;
                    dr[2] = lblQCParameterName.Text;
                    dr[3] = TResult_P;
                    dr[4] = ddloption.SelectedItem.Text;
                    dt.Rows.Add(dr);

                }
            }


        }

        return dt;

    }
    protected void GetSNF(decimal Fat, decimal CLR)
    {
        decimal SNF = 0;
        if (ViewState["ItemType_id"].ToString() == "58")
        {
            SNF = (CLR / 4) + (Fat * Convert.ToDecimal(.25)) + Convert.ToDecimal(.44);
        }
        else if (ViewState["ItemType_id"].ToString() == "59" || ViewState["ItemType_id"].ToString() == "60" || ViewState["ItemType_id"].ToString() == "61")
        {
            SNF = (CLR / 4) + (Fat * Convert.ToDecimal(.25)) + Convert.ToDecimal(.52);
        }
        else if (ViewState["ItemType_id"].ToString() == "62")
        {
            SNF = (CLR / 4) + (Fat * Convert.ToDecimal(.25)) + Convert.ToDecimal(.72);
        }
        else if (ViewState["ItemType_id"].ToString() == "87")
        {
            SNF = (CLR / 4) + (Fat * Convert.ToDecimal(.25)) + Convert.ToDecimal(.60);
        }
        else
        {
            SNF = Obj_MC.GetSNFPer(Fat, CLR);
        }
        foreach (GridViewRow rows in GridView1.Rows)
        {
            Label ParameterName = (Label)rows.FindControl("lblQCParameterName");
            TextBox txtvalue = (TextBox)rows.FindControl("txtvalue");
            if (ParameterName.Text == "SNF")
            {
                txtvalue.Text = SNF.ToString();
            }
        }
    }
    #endregion

    #region Changed Event
    protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
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

            ddlTestRequestType.Items.Clear();
            ddlTestRequestType.Items.Insert(0, new ListItem("Select", "0"));

            if (ddlPSection.SelectedValue == "1")
            {
                ddlTestRequestType.Items.Clear();
                ddlTestRequestType.Items.Insert(0, new ListItem("Select", "0"));
                ddlTestRequestType.Items.Insert(1, new ListItem("Silo", "Silo"));
                ddlTestRequestType.Items.Insert(2, new ListItem("PMT", "PMT"));
                ddlTestRequestType.Items.Insert(3, new ListItem("RMT", "RMT"));
                //ddlTestRequestType.Items.Insert(4, new ListItem("Packaging[Milk]", "Milk"));
            }

            if (ddlPSection.SelectedValue == "2")
            {
                ddlTestRequestType.Items.Clear();
                ddlTestRequestType.Items.Insert(0, new ListItem("Select", "0"));
                ddlTestRequestType.Items.Insert(1, new ListItem("Loose Product", "Tank"));
                ddlTestRequestType.Items.Insert(1, new ListItem("Packaging[Product]", "Product"));

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
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void txtvalue_TextChanged(object sender, EventArgs e)
    {
        lblModalMsg.Text = "";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        int index = row.RowIndex;
        Label lblQCParameterName = (Label)GridView1.Rows[index].FindControl("lblQCParameterName");
        decimal Fat = 0;
        decimal CLR = 0;

        if (lblQCParameterName.Text == "FAT" || lblQCParameterName.Text == "CLR")
        {
            foreach (GridViewRow rows in GridView1.Rows)
            {
                Label ParameterName = (Label)rows.FindControl("lblQCParameterName");
                TextBox txtvalue = (TextBox)rows.FindControl("txtvalue");

                if (ParameterName.Text == "FAT")
                {
                    if (txtvalue.Text == "")
                    {
                        Fat = 0;
                    }
                    else
                    {
                        Fat = decimal.Parse(txtvalue.Text);
                    }

                }
                if (ParameterName.Text == "CLR")
                {
                    if (txtvalue.Text == "")
                    {
                        CLR = 0;
                    }
                    else
                    {
                        CLR = decimal.Parse(txtvalue.Text);
                    }

                }

            }
            GetSNF(Fat, CLR);

        }
    }

    #endregion

    #region Button Click Event
    protected void LBAccept_Click(object sender, EventArgs e)
    {

        try
        {
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lblBI_MilkInOutRefID = (LinkButton)gvtestingrequestdetail.Rows[selRowIndex].FindControl("LBAccept");
            objdb.ByProcedure("spProductionMilkContainerMaster"
           , new string[] { "flag", "Office_Id", "TestRequest_ID", "TestRequest_Status" }
           , new string[] { "6", ViewState["Office_ID"].ToString(), lblBI_MilkInOutRefID.CommandArgument, "Accept" }, "dataset");
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Test Request Accepted Successfully!");
            FillGrid();

        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void LBSubmitTestResult_Click(object sender, EventArgs e)
    {
        lblModalMsg.Text = "";
        //txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
        //txtDate.Enabled = false;

        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
        Label lblRowNumber = (Label)gvtestingrequestdetail.Rows[selRowIndex].FindControl("lblRowNumber");
        Label lblTestRequest_No = (Label)gvtestingrequestdetail.Rows[selRowIndex].FindControl("lblTestRequest_No");
        Label lblTestRequestFor = (Label)gvtestingrequestdetail.Rows[selRowIndex].FindControl("lblTestRequestFor");
        Label lblSampleBatch_No = (Label)gvtestingrequestdetail.Rows[selRowIndex].FindControl("lblSampleBatch_No");
        Label lblSampleLot_No = (Label)gvtestingrequestdetail.Rows[selRowIndex].FindControl("lblSampleLot_No");

        Label lblTestRequestType = (Label)gvtestingrequestdetail.Rows[selRowIndex].FindControl("lblTestRequestType");
        Label lblTestRequestFor_ID = (Label)gvtestingrequestdetail.Rows[selRowIndex].FindControl("lblTestRequestFor_ID");
        Label lblItemType_id = (Label)gvtestingrequestdetail.Rows[selRowIndex].FindControl("lblItemType_id");
        lblRequestNo.Text = lblTestRequest_No.Text;
        lblRequestNo.ToolTip = lblRowNumber.ToolTip.ToString();
        lblRequestFor.Text = lblTestRequestFor.Text;
        lblBatchNo.Text = lblSampleBatch_No.Text;
        lblLotNo.Text = lblSampleLot_No.Text;
        ViewState["ItemType_id"] = lblItemType_id.Text;
        if (lblTestRequestType.Text == "Milk" || lblTestRequestType.Text == "Product")
        {
            lbGCheckResult.Visible = true;
            lbltesttype.Text = lblTestRequestType.Text;

            ds = null;
            ds = objdb.ByProcedure("spProductionQCProductMapping"
               , new string[] { "flag", "Office_ID", "ProductID" }
               , new string[] { "6", ViewState["Office_ID"].ToString(), lblTestRequestFor_ID.Text }, "dataset");
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

        else
        {
            lbGCheckResult.Visible = false;
            lbltesttype.Text = lblTestRequestType.Text;

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


            ds = null;
            ds = objdb.ByProcedure("spProductionQCProductMapping"
                , new string[] { "flag", "Office_ID", "ProductID" }
                , new string[] { "11", ViewState["Office_ID"].ToString(), lblTestRequestFor_ID.Text }, "dataset");
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

        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
    }

    protected void btnCrossButton_Click(object sender, EventArgs e)
    {
        ddlTestRequestType_SelectedIndexChanged(sender, e);
        Timer1_Tick(sender, e);
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCalculationMethod = e.Row.FindControl("lblCalculationMethod") as Label;

            Label lblMinValue = e.Row.FindControl("lblMinValue") as Label;
            Label lblMaxValue = e.Row.FindControl("lblMaxValue") as Label;
            Label lblOptionDetails = e.Row.FindControl("lblOptionDetails") as Label;
            Label lblValueRang = e.Row.FindControl("lblValueRang") as Label;

            TextBox txtvalue = e.Row.FindControl("txtvalue") as TextBox;
            DropDownList ddloption = e.Row.FindControl("ddloption") as DropDownList;

            if (lblCalculationMethod.Text == "Value" || lblCalculationMethod.Text == "Use By Days")
            {
                txtvalue.Visible = true;
                ddloption.Visible = false;
                lblValueRang.Text = lblMinValue.Text + " - " + lblMaxValue.Text;
            }


            if (lblCalculationMethod.Text == "Adulteration Test" || lblCalculationMethod.Text == "Manufacturing Date" || lblCalculationMethod.Text == "OT")
            {

                txtvalue.Visible = false;
                ddloption.Visible = true;
                lblValueRang.Text = lblOptionDetails.Text;

                if (lblCalculationMethod.Text == "Adulteration Test")
                {
                    ddloption.Items.Clear();
                    ddloption.Items.Insert(0, new ListItem("Negative", "Negative"));
                    ddloption.Items.Insert(1, new ListItem("Positive", "Positive"));
                }

                if (lblCalculationMethod.Text == "Manufacturing Date")
                {
                    ddloption.Items.Clear();
                    ddloption.Items.Insert(0, new ListItem("Select", "0"));
                    ddloption.Items.Insert(1, new ListItem("Yes", "Yes"));
                    ddloption.Items.Insert(2, new ListItem("No", "No"));

                }

                if (lblCalculationMethod.Text == "OT")
                {
                    ddloption.Items.Clear();
                    ds = null;
                    ddloption.DataSource = objdb.ByProcedure("USP_Mst_MilkQualityList",
                                new string[] { "flag" },
                                new string[] { "1" }, "dataset");
                    ddloption.DataValueField = "V_MilkQualityList";
                    ddloption.DataTextField = "V_MilkQualityList";
                    ddloption.DataBind();
                    ddloption.Items.Insert(0, new ListItem("Select", "0"));
                }

            }


        }

    }

    protected void lbGCheckResult_Click(object sender, EventArgs e)
    {
        try
        {
            decimal MinVal = 0;
            decimal MaxVal = 0;
            decimal FinalVal = 0;
            string TResult_P = "";
            string TResult_F = "";
            lblModalMsg.Text = "";

            foreach (GridViewRow row in GridView1.Rows)
            {
                Label lblCalculationMethod = (Label)row.FindControl("lblCalculationMethod");
                TextBox txtvalue = (TextBox)row.FindControl("txtvalue");
                DropDownList ddloption = (DropDownList)row.FindControl("ddloption");
                Label lblQCParameterName = (Label)row.FindControl("lblQCParameterName");
                Label lblMinValue = (Label)row.FindControl("lblMinValue");
                Label lblMaxValue = (Label)row.FindControl("lblMaxValue");
                Label lblOptionDetails = (Label)row.FindControl("lblOptionDetails");

                if (lblCalculationMethod.Text == "Value" || lblCalculationMethod.Text == "Use By Days")
                {
                    if (txtvalue.Text != "")
                    {
                        MinVal = Convert.ToDecimal(lblMinValue.Text);
                        MaxVal = Convert.ToDecimal(lblMaxValue.Text);
                        FinalVal = Convert.ToDecimal(txtvalue.Text);

                        if (FinalVal >= MinVal && FinalVal <= MaxVal)
                        {
                            TResult_P = "Pass";
                        }
                        else
                        {
                            TResult_F = "Failed";
                        }
                    }
                    else
                    {
                        lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Field Result For " + lblQCParameterName.Text + " Can't Blank!");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
                        return;
                    }
                }

                if (lblCalculationMethod.Text == "Adulteration Test" || lblCalculationMethod.Text == "Manufacturing Date" || lblCalculationMethod.Text == "OT")
                {

                    if (ddloption.SelectedValue != "0")
                    {

                        if (lblOptionDetails.Text == ddloption.SelectedItem.Text)
                        {
                            TResult_P = "Pass";
                        }
                        else
                        {
                            if (ddloption.SelectedItem.Text == "Satisfactory")
                            {
                                TResult_P = "Pass";
                            }
                            else
                            {
                                TResult_F = "Failed";
                            }

                        }
                    }
                    else
                    {
                        lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Field Result For " + lblQCParameterName.Text + " Can't Select!");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
                        return;
                    }

                }


            }

            if (TResult_F == "Failed")
            {
                lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", TResult_F, "");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
            }
            else
            {
                if (TResult_P != "")
                {
                    lblModalMsg.Text = objdb.Alert("fa-check", "alert-success", TResult_P, "");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
                }
                else
                {
                    lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Please Fill All Test Parameters Details!");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
                }

            }


        }
        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
        }
    }

    protected void btnYesT_Click(object sender, EventArgs e)
    {

        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                decimal MinVal = 0;
                decimal MaxVal = 0;
                decimal FinalVal = 0;
                string TResult_P = "";
                string TResult_F = "";
                lblModalMsg.Text = "";
                string Save_Result = "";
              
                foreach (GridViewRow row in GridView1.Rows)
                {
                    Label lblCalculationMethod = (Label)row.FindControl("lblCalculationMethod");
                    TextBox txtvalue = (TextBox)row.FindControl("txtvalue");
                    DropDownList ddloption = (DropDownList)row.FindControl("ddloption");
                    Label lblQCParameterName = (Label)row.FindControl("lblQCParameterName");
                    Label lblMinValue = (Label)row.FindControl("lblMinValue");
                    Label lblMaxValue = (Label)row.FindControl("lblMaxValue");
                    Label lblOptionDetails = (Label)row.FindControl("lblOptionDetails");

                    if (lblCalculationMethod.Text == "Value" || lblCalculationMethod.Text == "Use By Days")
                    {
                        if (txtvalue.Text != "")
                        {
                            if (lblMinValue.Text == "")
                            {
                                lblMinValue.Text = "0";
                            }
                            if (lblMaxValue.Text == "")
                            {
                                lblMaxValue.Text = "0";
                            }

                            MinVal = Convert.ToDecimal(lblMinValue.Text);
                            MaxVal = Convert.ToDecimal(lblMaxValue.Text);
                            FinalVal = Convert.ToDecimal(txtvalue.Text);

                            if (FinalVal >= MinVal && FinalVal <= MaxVal)
                            {
                                TResult_P = "Pass";
                            }
                            else
                            {
                                TResult_F = "Failed";
                            }
                        }
                        else
                        {
                            lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Field Result For " + lblQCParameterName.Text + " Can't Blank!");
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
                            return;
                        }
                    }

                    if (lblCalculationMethod.Text == "Adulteration Test" || lblCalculationMethod.Text == "Manufacturing Date" || lblCalculationMethod.Text == "OT")
                    {

                        if (ddloption.SelectedValue != "0")
                        {
                            if (lblOptionDetails.Text == ddloption.SelectedItem.Text)
                            {
                                TResult_P = "Pass";
                            }

                            else
                            {
                                if (ddloption.SelectedItem.Text == "Satisfactory")
                                {
                                    TResult_P = "Pass";
                                }
                                else
                                {
                                    TResult_F = "Failed";
                                }

                            }
                            //else
                            //{
                            //    TResult_F = "Failed";
                            //}
                        }
                        else
                        {
                            lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Field Result For " + lblQCParameterName.Text + " Can't Select!");
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
                            return;
                        }

                    }
                }

                if (TResult_F == "Failed")
                {
                    //lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", TResult_F, "");

                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
                    Save_Result = TResult_F;
                }
                else
                {
                    if (TResult_P != "")
                    {
                        //lblModalMsg.Text = objdb.Alert("fa-check", "alert-success", TResult_P, "");
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
                        Save_Result = TResult_P;
                    }
                    else
                    {
                        lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Please Fill All Test Parameters Details!");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
                    }

                }

                if (lbltesttype.Text == "Milk" || lbltesttype.Text == "Product")
                {

                }
                else
                {
                    Save_Result = "Submitted";
                }


                if (txtRemarks_R.Text == "")
                {
                    lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Please Enter Remarks");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
                    return;
                }

                DataTable dtTR = new DataTable();
                dtTR = GetTestResult();


                // Insert Result


                if (dtTR.Rows.Count > 0)
                {

                    ds = null;
                    ds = objdb.ByProcedure("spProductionMilkContainerMaster",
                                              new string[] { "Flag" 
                                                ,"Office_Id"
                                                ,"TestRequest_Result_Status" 
                                                ,"TestRequest_Remark" 
                                                ,"CreatedBy" 
                                                ,"CreatedBy_IP"
                                                ,"TestRequest_ID"
                                    },
                                              new string[] { "7"
                                              ,objdb.Office_ID()
                                              , Save_Result
                                              , txtRemarks_R.Text
                                              , ViewState["Emp_ID"].ToString()
                                              ,objdb.GetLocalIPAddress()
                                              ,lblRequestNo.ToolTip.ToString()
                                    },
                                             new string[] { "type_Production_LabTesting_Request_Result" },
                                             new DataTable[] { dtTR }, "TableSave");

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you! Record Save Successfully", "");

                            foreach (GridViewRow row in GridView1.Rows)
                            {
                                TextBox txtvalue = (TextBox)row.FindControl("txtvalue");
                                DropDownList ddloption = (DropDownList)row.FindControl("ddloption");
                                ddloption.ClearSelection();
                                txtvalue.Text = "";
                                txtRemarks_R.Text = "";

                            }
                            FillGrid();
                            Timer1_Tick(sender, e);
                        }
                        else
                        {
                            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Record already Exist.", "");
                        }
                    }


                }
                else
                {
                    lblModalMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Data Can't Insert Without Any Testing Parameter.");
                    return;
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);

            }
        }

        catch (Exception ex)
        {
            lblModalMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
        }

    }

    protected void lblviewresult_Click(object sender, EventArgs e)
    {

        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
        Label lblRowNumber = (Label)gvtestingrequestdetail.Rows[selRowIndex].FindControl("lblRowNumber");
        Label lblTestRequest_No = (Label)gvtestingrequestdetail.Rows[selRowIndex].FindControl("lblTestRequest_No");
        Label lblTestRequestFor = (Label)gvtestingrequestdetail.Rows[selRowIndex].FindControl("lblTestRequestFor");
        Label lblSampleBatch_No = (Label)gvtestingrequestdetail.Rows[selRowIndex].FindControl("lblSampleBatch_No");
        Label lblSampleLot_No = (Label)gvtestingrequestdetail.Rows[selRowIndex].FindControl("lblSampleLot_No");

        Label2.Text = lblTestRequest_No.Text;
        Label2.ToolTip = lblRowNumber.ToolTip.ToString();
        Label3.Text = lblTestRequestFor.Text;
        Label4.Text = lblSampleBatch_No.Text;
        Label5.Text = lblSampleLot_No.Text;
        Label lblTestRequestType = (Label)gvtestingrequestdetail.Rows[selRowIndex].FindControl("lblTestRequestType");
        Label lblTestRequestFor_ID = (Label)gvtestingrequestdetail.Rows[selRowIndex].FindControl("lblTestRequestFor_ID");

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

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        try
        {

            ds = null;
            ds = objdb.ByProcedure("spProductionMilkContainerMaster"
            , new string[] { "flag", "Office_Id" }
            , new string[] { "10", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds != null)
            {
                lblNotificationCount_Top.Text = ds.Tables[0].Rows.Count.ToString();
                lblNotificationCount.Text = ds.Tables[0].Rows.Count.ToString();
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }
            else
            {
                Repeater1.DataSource = string.Empty;
                Repeater1.DataBind();
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
            GvExportDetail.Visible = true;
           
            string FileName = "MilkTestingRequestToQCFeedBack";
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            GvExportDetail.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
            GvExportDetail.Visible = false;
            // GridView3.Columns[9].Visible = true;
            //GridView3.Columns[10].Visible = true;
            //GridView3.Columns[11].Visible = true;
            //GridView3.Columns[12].Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    #endregion
    
    #region GridView Row Event
    protected void GvExportDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblRowNumber = (Label)e.Row.FindControl("lblRowNumber");
            Label lblTestRequestType = (Label)e.Row.FindControl("lblTestRequestType");
            Label lblTestRequestFor_ID = (Label)e.Row.FindControl("lblTestRequestFor_ID");
            GridView gvResultDetail = (GridView)e.Row.FindControl("gvResultDetail");
          
            DataSet ds1 = null;
            DataSet ds2 = null;
            if (lblTestRequestType.Text == "Milk" || lblTestRequestType.Text == "Product")
            {

                ds1 = objdb.ByProcedure("spProductionMilkContainerMaster"
                , new string[] { "flag", "Office_ID", "TestRequest_ID", "ProductID" }
                , new string[] { "8", ViewState["Office_ID"].ToString(), lblRowNumber.ToolTip.ToString(), lblTestRequestFor_ID.Text }, "dataset");

                if (ds1 != null)
                {
                    gvResultDetail.DataSource = ds1;
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


                ds1 = objdb.ByProcedure("spProductionMilkContainerMaster"
                , new string[] { "flag", "Office_ID", "TestRequest_ID", "ProductID" }
                , new string[] { "11", ViewState["Office_ID"].ToString(), lblRowNumber.ToolTip.ToString(), lblTestRequestFor_ID.Text }, "dataset");
                if (ds1 != null)
                {
                    gvResultDetail.DataSource = ds1;
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
    
    protected void gvResultDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
    #endregion
}