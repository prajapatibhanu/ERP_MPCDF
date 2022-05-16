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

public partial class mis_dailyplan_MilkTestingRequestToQCHourlyTesting : System.Web.UI.Page
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

                    if (Session["IsSuccess"] != null)
                    {
                        if ((Boolean)Session["IsSuccess"] == true)
                        {
                            Session["IsSuccess"] = false;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Hourly Testing Details Successfully saved !')", true);

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
                    FillMachine();
                    // FillMachineHead();
                    MachineHeadDataTable();
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
    protected void MachineHeadDataTable()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add(new DataColumn("Machine_ID", typeof(int)));
        dt.Columns.Add(new DataColumn("MachineName", typeof(string)));
        dt.Columns.Add(new DataColumn("Head_ID", typeof(int)));
        dt.Columns.Add(new DataColumn("HeadName", typeof(string)));

        dt.Columns.Add(new DataColumn("Quantity", typeof(decimal)));


        ViewState["InsertRecord"] = dt;
        gvMachineHeadDetails.DataSource = dt;
        gvMachineHeadDetails.DataBind();
    }
    protected void FillMachine()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("Usp_Mst_Machine",
                                       new string[] { "flag", "Office_ID" },
                                       new string[] { "3", objdb.Office_ID() }, "dataset");

            ddlMachineList.DataSource = ds.Tables[0];
            ddlMachineList.DataTextField = "Machine_Name";
            ddlMachineList.DataValueField = "Machine_ID";
            ddlMachineList.DataBind();
            //ddlMachineListList.Items.Insert(0, new ListItem("Select", "0"));

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
            ddlUnit.Items.Insert(0, new ListItem("Select", "0"));

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

    private DataTable GetMachineHeadDetail()
    {

        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("Machine_ID", typeof(int)));
        dt.Columns.Add(new DataColumn("Head_ID", typeof(int)));
        dt.Columns.Add(new DataColumn("Quantity", typeof(decimal)));


        foreach (GridViewRow row in gvMachineHeadDetails.Rows)
        {

            Label lblMachine_ID = (Label)row.FindControl("lblMachine_ID");
            Label lblHead_ID = (Label)row.FindControl("lblHead_ID");
            TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");

            dr = dt.NewRow();

            dr[0] = lblMachine_ID.Text;
            dr[1] = lblHead_ID.Text;
            dr[2] = txtQuantity.Text;
            dt.Rows.Add(dr);


        }
        return dt;

    }

    private DataTable GetTestResult()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        decimal MinVal = 0;
        decimal MaxVal = 0;
        decimal FinalVal = 0;
        string TResult_P = "";



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

                    dr = dt.NewRow();
                    dr[0] = ddlmilktestrequestfor.SelectedItem.Text;
                    dr[1] = lblQCParameterName.Text;
                    dr[2] = TResult_P;
                    dr[3] = txtvalue.Text;
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

                    dr = dt.NewRow();
                    dr[0] = ddlmilktestrequestfor.SelectedItem.Text;
                    dr[1] = lblQCParameterName.Text;
                    dr[2] = TResult_P;
                    dr[3] = ddloption.SelectedValue;
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
    protected void ddlPSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            panelMachineHead.Visible = false;
            divVariant.Visible = false;
            ddlTestRequestType.Items.Clear();
            ddlTestRequestType.Items.Insert(0, new ListItem("Select", "0"));
            ddlmilktestrequestfor.Items.Clear();
            ddlmilktestrequestfor.Items.Insert(0, new ListItem("Select", "0"));
            GridView1.DataSource = string.Empty;
            GridView1.DataBind();

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
                ddlTestRequestType.Items.Insert(1, new ListItem("Packaging[Product]", "Product"));



            }
            if (ddlPSection.SelectedValue == "9")
            {
                ddlTestRequestType.Items.Clear();
                ddlTestRequestType.Items.Insert(0, new ListItem("Select", "0"));
                ddlTestRequestType.Items.Insert(1, new ListItem("Packaging[Milk]", "Milk"));
                panelMachineHead.Visible = true;
                //MachineHeadDataTable();
                //txtQuantity.Text = "";
                FillMachine();
                //FillMachineHead();
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

            if (ddlTestRequestType.SelectedValue == "0")
            {
                ddlmilktestrequestfor.Items.Clear();
                ddlmilktestrequestfor.Items.Insert(0, new ListItem("Select", "0"));
                GridView1.DataSource = string.Empty;
                GridView1.DataBind();
            }
            else
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
                           new string[] { "2", ddlTestRequestType.SelectedItem.Text, objdb.Office_ID() }, "dataset");

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
            //btnPrint.Visible = false;
            btnShowntoplant.Visible = false;
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtFilterDT.Text, cult).ToString("yyyy/MM/dd");
            }


            GridView3.DataSource = null;
            GridView3.DataBind();
            btnExport.Visible = false;
            //btnPrint.Visible = false;
            btnShowntoplant.Visible = false;
            ds = null;
            ds = objdb.ByProcedure("spProductionMilkContainerMaster"
            , new string[] { "flag", "Office_Id", "TestRequest_DT" }
            , new string[] { "19", ViewState["Office_ID"].ToString(), Fdate }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridView3.DataSource = ds;
                        GridView3.DataBind();
                        GridView5.DataSource = ds;
                        GridView5.DataBind();
                        btnExport.Visible = true;
                       // btnPrint.Visible = true;
                        btnShowntoplant.Visible = true;
                    }
                    else
                    {
                        GridView3.DataSource = string.Empty;
                        GridView3.DataBind();
                        GridView5.DataSource = string.Empty;
                        GridView5.DataBind();
                    }
                }
                else
                {
                    GridView3.DataSource = string.Empty;
                    GridView3.DataBind();
                    GridView5.DataSource = string.Empty;
                    GridView5.DataBind();
                }
            }
            else
            {
                GridView3.DataSource = string.Empty;
                GridView3.DataBind();
                GridView5.DataSource = string.Empty;
                GridView5.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlmilktestrequestfor_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (ddlmilktestrequestfor.SelectedValue != "0")
            {
                if (ddlPSection.SelectedValue == "1")
                {
                    if (ddlVariant.SelectedIndex > 0)
                    {
                        string ProductID = "";
                        if (ddlTestRequestType.SelectedItem.Text == "Silo")
                        {
                            ProductID = "-1";
                        }
                        if (ddlTestRequestType.SelectedItem.Text == "PMT")
                        {
                            ProductID = "-2";
                        }
                        if (ddlTestRequestType.SelectedItem.Text == "RMT")
                        {
                            ProductID = "-3";
                        }
                        if (ddlTestRequestType.SelectedItem.Text == "Tank")
                        {
                            ProductID = "-4";
                        }
                        ds = null;
                        ViewState["ItemType_id"] = ddlVariant.SelectedValue;
                        ds = objdb.ByProcedure("spProductionQCProductMapping"
                           , new string[] { "flag", "Office_ID", "ProductID" }
                           , new string[] { "12", ViewState["Office_ID"].ToString(), ProductID }, "dataset");
                        if (ds != null)
                        {


                            GridView1.DataSource = ds.Tables[0];
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
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "warning!", "Select Variant");
                    }
                }
                else
                {
                    ds = null;
                    ds = objdb.ByProcedure("spProductionQCProductMapping"
                       , new string[] { "flag", "Office_ID", "ProductID" }
                       , new string[] { "6", ViewState["Office_ID"].ToString(), ddlmilktestrequestfor.SelectedValue }, "dataset");
                    if (ds != null)
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            ViewState["ItemType_id"] = ds.Tables[1].Rows[0]["ItemType_id"].ToString();
                        }

                        GridView1.DataSource = ds.Tables[0];
                        GridView1.DataBind();
                    }
                    else
                    {
                        GridView1.DataSource = string.Empty;
                        GridView1.DataBind();
                    }
                }


            }
            else
            {
                GridView1.DataSource = string.Empty;
                GridView1.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void txtvalue_TextChanged(object sender, EventArgs e)
    {

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
    protected void ddlVariant_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlmilktestrequestfor_SelectedIndexChanged(sender, e);
    }
    #endregion

    #region GridView Row Event
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
    protected void gvMachineHeadDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //    try
        //    {
        //        lblMsg.Text = "";
        //        if (e.CommandName == "DeleteRow")
        //        {
        //            string Id = e.CommandArgument.ToString();
        //            DataTable dt = (DataTable)ViewState["InsertRecord"];
        //            int count = dt.Rows.Count;
        //            for (int i = 0; i < count; i++)
        //            {
        //                DataRow dr = dt.Rows[i];
        //                if (dr["Head_ID"].ToString() == Id.ToString())
        //                {
        //                    dr.Delete();
        //                }
        //            }
        //            dt.AcceptChanges();
        //            ViewState["InsertRecord"] = dt;
        //            gvMachineHeadDetails.DataSource = dt;
        //            gvMachineHeadDetails.DataBind();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        //    }
    }
    protected void GvResultDetail_RowDataBound(object sender, GridViewRowEventArgs e)
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
                lblValueRang.Text = "( " + lblMinValue.Text + "  -  " + lblMaxValue.Text + " )";
            }

            if (lblCalculationMethod.Text == "Adulteration Test" || lblCalculationMethod.Text == "Manufacturing Date" || lblCalculationMethod.Text == "OT")
            {
                lblValueRang.Text = lblOptionDetails.Text;
            }

        }
    }
    protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblRowNumber = (Label)e.Row.FindControl("lblRowNumber");
            Label lblTestRequestType = (Label)e.Row.FindControl("lblTestRequestType");
            Label lblTestRequestFor_ID = (Label)e.Row.FindControl("lblTestRequestFor_ID");
            GridView GvResultDetail = (GridView)e.Row.FindControl("GvResultDetail");
            GridView GvHeadDetail = (GridView)e.Row.FindControl("GvHeadDetail");
            DataSet ds1 = null;
            DataSet ds2 = null;
            if (lblTestRequestType.Text == "Milk" || lblTestRequestType.Text == "Product")
            {

                ds1 = objdb.ByProcedure("spProductionMilkContainerMaster"
                , new string[] { "flag", "Office_ID", "TestRequest_ID", "ProductID" }
                , new string[] { "24", ViewState["Office_ID"].ToString(), lblRowNumber.ToolTip.ToString(), lblTestRequestFor_ID.Text }, "dataset");

                if (ds1 != null)
                {
                    if (ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[1].Rows.Count > 0)
                        {
                            GvHeadDetail.DataSource = ds1.Tables[1];
                            GvHeadDetail.DataBind();
                        }
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            GvResultDetail.DataSource = ds1.Tables[0];
                            GvResultDetail.DataBind();
                        }

                    }



                }
                else
                {
                    GvResultDetail.DataSource = string.Empty;
                    GvResultDetail.DataBind();
                    GvHeadDetail.DataSource = string.Empty;
                    GvHeadDetail.DataBind();
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
                , new string[] { "25", ViewState["Office_ID"].ToString(), lblRowNumber.ToolTip.ToString(), lblTestRequestFor_ID.Text }, "dataset");
                if (ds1 != null)
                {
                    GvResultDetail.DataSource = ds1;
                    GvResultDetail.DataBind();

                }
                else
                {
                    GvResultDetail.DataSource = string.Empty;
                    GvResultDetail.DataBind();
                }

            }



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
            , new string[] { "20", ViewState["Office_ID"].ToString(), lblRowNumber.ToolTip.ToString(), lblTestRequestFor_ID.Text }, "dataset");

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
            , new string[] { "25", ViewState["Office_ID"].ToString(), lblRowNumber.ToolTip.ToString(), lblTestRequestFor_ID.Text }, "dataset");
            if (ds != null)
            {
                GridView2.DataSource = ds;
                GridView2.DataBind();
                txtRR.Text = ds.Tables[0].Rows[0]["TestRequest_Remark"].ToString();

            }
            else
            {
                GridView2.DataSource = string.Empty;
                GridView2.DataBind();
            }

        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ViewResultParameterModal()", true);

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
            lblMsg.Text = "";

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
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Field Result For " + lblQCParameterName.Text + " Can't Blank!");

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
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Field Result For " + lblQCParameterName.Text + " Can't Select!");

                        return;
                    }

                }


            }

            if (TResult_F == "Failed")
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", TResult_F, "");

            }
            else
            {
                if (TResult_P != "")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", TResult_P, "");

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Please Fill All Test Parameters Details!");

                }

            }


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

                decimal MinVal = 0;
                decimal MaxVal = 0;
                decimal FinalVal = 0;
                string TResult_P = "";
                string TResult_F = "";
                lblMsg.Text = "";
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
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Field Result For " + lblQCParameterName.Text + " Can't Blank!");

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
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Field Result For " + lblQCParameterName.Text + " Can't Select!");

                            return;
                        }

                    }
                }

                if (TResult_F == "Failed")
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", TResult_F, "");

                    Save_Result = TResult_F;
                }
                else
                {
                    if (TResult_P != "")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", TResult_P, "");

                        Save_Result = TResult_P;
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Please Fill All Test Parameters Details!");

                    }

                }


                //if (txtRemarks_R.Text == "")
                //{
                //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Please Enter Remarks");
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ParameterModal()", true);
                //    return;
                //}

                DataTable dtTR = new DataTable();
                dtTR = GetTestResult();

                DataTable dtMachineHeadDetail = new DataTable();
                dtMachineHeadDetail = GetMachineHeadDetail();
                // Insert Result


                if (dtTR.Rows.Count > 0)
                {
                    if (ddlPSection.SelectedValue == "9")
                    {
                        if (dtMachineHeadDetail.Rows.Count > 0)
                        {
                            ds = null;
                            ds = objdb.ByProcedure("spProductionMilkContainerMaster",
                                                      new string[] { "Flag" 
                                                ,"Office_Id"
                                                ,"TestRequest_Result_Status" 
                                                ,"TestRequest_Remark" 
                                                ,"CreatedBy" 
                                                ,"CreatedBy_IP"
                                                ,"Shift_ID"
                                                ,"ProductSection_ID"
                                                ,"TestRequestType"
                                                ,"TestRequestFor"
                                                ,"TestRequestFor_ID"                                                
                                                //,"SampleNameNo"
                                                //,"SampleQuantity"
                                                //,"Unit_Id"
                                                ,"SampleBatch_No"
                                                ,"SampleLot_No"
                                    },
                                                      new string[] { "18"
                                              ,objdb.Office_ID()
                                              , Save_Result
                                              , txtRemarks_R.Text
                                              , ViewState["Emp_ID"].ToString()
                                              ,objdb.GetLocalIPAddress()
                                              ,ddlShift.SelectedValue
                                              ,ddlPSection.SelectedValue
                                              ,ddlTestRequestType.SelectedValue
                                              ,ddlmilktestrequestfor.SelectedItem.Text
                                              ,ddlmilktestrequestfor.SelectedValue
                                              //,txtSampleName.Text
                                              //,txtSampleQuantity.Text
                                              //,ddlUnit.SelectedValue
                                              ,txtSampleBatch_No.Text
                                              ,txtSampleLot_No.Text

                                    },
                                                     new string[] { "type_Production_LabTesting_Request_Result_Hourly", "type_Production_LabTesting_Request_Hourly_MachineHead" },
                                                     new DataTable[] { dtTR, dtMachineHeadDetail }, "TableSave");

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
                                        panelMachineHead.Visible = false;

                                    }

                                    Session["IsSuccess"] = true;
                                    Response.Redirect("MilkTestingRequestToQCHourlyTesting.aspx", false);
                                }
                                else
                                {
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Record already Exist.", "");
                                }
                            }
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Data Can't Insert Without Any Machine Head Detail.");
                            return;
                        }
                    }
                    else
                    {


                        ds = null;
                        ds = objdb.ByProcedure("spProductionMilkContainerMaster",
                                                  new string[] { "Flag" 
                                                ,"Office_Id"
                                                ,"TestRequest_Result_Status" 
                                                ,"TestRequest_Remark" 
                                                ,"CreatedBy" 
                                                ,"CreatedBy_IP"
                                                ,"Shift_ID"
                                                ,"ProductSection_ID"
                                                ,"TestRequestType"
                                                ,"TestRequestFor"
                                                ,"TestRequestFor_ID"                                                
                                                //,"SampleNameNo"
                                                //,"SampleQuantity"
                                                //,"Unit_Id"
                                                ,"SampleBatch_No"
                                                ,"SampleLot_No"
                                    },
                                                  new string[] { "18"
                                              ,objdb.Office_ID()
                                              , Save_Result
                                              , txtRemarks_R.Text
                                              , ViewState["Emp_ID"].ToString()
                                              ,objdb.GetLocalIPAddress()
                                              ,ddlShift.SelectedValue
                                              ,ddlPSection.SelectedValue
                                              ,ddlTestRequestType.SelectedValue
                                              ,ddlmilktestrequestfor.SelectedItem.Text
                                              ,ddlmilktestrequestfor.SelectedValue
                                              //,txtSampleName.Text
                                              //,txtSampleQuantity.Text
                                              //,ddlUnit.SelectedValue
                                              ,txtSampleBatch_No.Text
                                              ,txtSampleLot_No.Text

                                    },
                                                 new string[] { "type_Production_LabTesting_Request_Result_Hourly", "type_Production_LabTesting_Request_Hourly_MachineHead" },
                                                 new DataTable[] { dtTR, dtMachineHeadDetail }, "TableSave");

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
                                    panelMachineHead.Visible = false;

                                }

                                Session["IsSuccess"] = true;
                                Response.Redirect("MilkTestingRequestToQCHourlyTesting.aspx", false);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Record already Exist.", "");
                            }
                        }
                    }

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Data Can't Insert Without Any Testing Parameter.");
                    return;
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());


            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
   
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView5.Visible = true;
            // GridView3.Columns[9].Visible = false;
            //  GridView3.Columns[10].Visible = false;
            //  GridView3.Columns[11].Visible = false;
            //  GridView3.Columns[12].Visible = false;
            //foreach (GridViewRow rows in GridView3.Rows)
            //{

            //    Label Label1 = (Label)rows.FindControl("Label1");
            //    Label Label7 = (Label)rows.FindControl("Label7");


            //    Label1.Visible = false;
            //    Label7.Visible = true;
            //}
            string FileName = "MilkTestingRequestToQCHourlyTesting";
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            GridView5.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
            GridView5.Visible = false;
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
    
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string MachineID = "";
            foreach (ListItem item in ddlMachineList.Items)
            {
                if (item.Selected)
                {
                    MachineID += item.Value + ",";
                }
            }
            ds = objdb.ByProcedure("Usp_Mst_Machine",
                                      new string[] { "flag", "Machine_ID_Mlt" },
                                      new string[] { "5", MachineID }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvMachineHeadDetails.DataSource = ds;
                        gvMachineHeadDetails.DataBind();
                    }
                    else
                    {
                        gvMachineHeadDetails.DataSource = string.Empty;
                        gvMachineHeadDetails.DataBind();
                    }
                }
                else
                {
                    gvMachineHeadDetails.DataSource = string.Empty;
                    gvMachineHeadDetails.DataBind();
                }
            }
            else
            {
                gvMachineHeadDetails.DataSource = string.Empty;
                gvMachineHeadDetails.DataBind();
            }
            //string Status = "0";
            //foreach(GridViewRow row in gvMachineHeadDetails.Rows)
            //{
            //    Label lblHead_ID = (Label)row.FindControl("lblHead_ID");
            //    if(lblHead_ID.Text == ddlMachineListHead.SelectedValue.ToString())
            //    {
            //        Status = "1";
            //    }
            //}
            //if(Status == "1")
            //{
            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Machine Head Already Exists !')", true);
            //}
            //else
            //{
            //    DataTable dt = (DataTable)ViewState["InsertRecord"];
            //    dt.Rows.Add(ddlMachineList.SelectedValue.ToString(), ddlMachineList.SelectedItem.Text, ddlMachineListHead.SelectedValue.ToString(), ddlMachineListHead.SelectedItem.Text, txtQuantity.Text);
            //    ViewState["InsertRecord"] = dt;
            //    gvMachineHeadDetails.DataSource = dt;
            //    gvMachineHeadDetails.DataBind();
            //    txtQuantity.Text = "";
            //    FillMachine();
            //    FillMachineHead();

            //}

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
       
    protected void lblHeadFilterReq_Click(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
        Label lblRowNumber = (Label)GridView3.Rows[selRowIndex].FindControl("lblRowNumber");
        Label lblTestRequest_No = (Label)GridView3.Rows[selRowIndex].FindControl("lblTestRequest_No");
        Label lblTestRequestFor = (Label)GridView3.Rows[selRowIndex].FindControl("lblTestRequestFor");
        Label lblSampleBatch_No = (Label)GridView3.Rows[selRowIndex].FindControl("lblSampleBatch_No");
        Label lblSampleLot_No = (Label)GridView3.Rows[selRowIndex].FindControl("lblSampleLot_No");

        Label8.Text = lblTestRequest_No.Text;
        Label8.ToolTip = lblRowNumber.ToolTip.ToString();
        Label9.Text = lblTestRequestFor.Text;
        Label10.Text = lblSampleBatch_No.Text;
        Label11.Text = lblSampleLot_No.Text;

        Label lblTestRequestFor_ID = (Label)GridView3.Rows[selRowIndex].FindControl("lblTestRequestFor_ID");


        ds = objdb.ByProcedure("spProductionMilkContainerMaster"
        , new string[] { "flag", "TestRequest_ID", }
        , new string[] { "21", lblRowNumber.ToolTip.ToString() }, "dataset");

        if (ds != null)
        {
            GridView4.DataSource = ds;
            GridView4.DataBind();

        }
        else
        {
            GridView4.DataSource = string.Empty;
            GridView4.DataBind();
        }


        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ViewHeadDetailModal()", true);
    }
    protected void btnShowntoplant_Click(object sender, EventArgs e)
    {
        try
        {
            string Status = "0";
            string TestRequest_ID_Mlt = "";

            lblMsg.Text = "";
            foreach (GridViewRow rows in GridView3.Rows)
            {
                CheckBox chk = (CheckBox)rows.FindControl("CheckBox1");
                Label lblRowNumber = (Label)rows.FindControl("lblRowNumber");
                if (chk.Checked == true)
                {
                    Status = "1";
                    TestRequest_ID_Mlt += lblRowNumber.ToolTip.ToString() + ",";
                }
            }
            if (Status == "1")
            {
                ds = objdb.ByProcedure("spProductionMilkContainerMaster", new string[] { "Flag", "TestRequest_ID_Mlt" }, new string[] { "22", TestRequest_ID_Mlt }, "dataset");
                lblMsg.Text = objdb.Alert("fa-success", "alert-success", "Thankyou!", "Now Record will be shown to Plant");
                txtFilterDT_TextChanged(sender, e);



            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Please Select At Least One CheckBox Row");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : ). " + ex.Message.ToString());
        }
    }
    #endregion

}