using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_MilkCollection_MilkInwardOutwardReferenceDetails_New : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    IFormatProvider cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            lblMsg.Text = "";

            if (!Page.IsPostBack)
            {
                txtDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");

                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }

                GetTankerDetail();
                GetCCDetails();
                GetViewRefDetails();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        GetViewRefDetails();
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    protected void GetTankerDetail()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("Usp_TankerDetail",
                     new string[] { "flag", "I_OfficeID", "MilkCollectionFrom", "I_OfficeTypeID" },
                     new string[] { "6", objdb.Office_ID(), "CC", objdb.OfficeType_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlTankerDetail.DataTextField = "V_VehicleNo";
                        ddlTankerDetail.DataValueField = "I_TankerID";
                        ddlTankerDetail.DataSource = ds;
                        ddlTankerDetail.DataBind();
                        ddlTankerDetail.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void ddlTankerDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["InsertRecord"] = null;
            ViewState["InsertRecord1"] = null;
            ViewState["InsertRecord2"] = null;

            gv_MilkQualityDetail.DataSource = string.Empty;
            gv_MilkQualityDetail.DataBind();



            gvmilkAdulterationtestdetail.DataSource = string.Empty;
            gvmilkAdulterationtestdetail.DataBind();
            milktestdetail.Visible = false;

            ds = null;
            ds = objdb.ByProcedure("Usp_TankerDetail",
                    new string[] { "flag", "I_TankerID", "I_OfficeID" },
                    new string[] { "2", ddlTankerDetail.SelectedValue, objdb.Office_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string TS = ds.Tables[0].Rows[0]["TS"].ToString();

                        if (TS == "OLD")
                        {
                            string D_GrossWeight = ds.Tables[0].Rows[0]["D_GrossWeight"].ToString();
                            string D_NetWeight = ds.Tables[0].Rows[0]["D_NetWeight"].ToString();

                            string RefCancelDT = ds.Tables[0].Rows[0]["RefCancelDT"].ToString();

                            if (RefCancelDT == "")
                            {
                                if (D_GrossWeight == "" || D_NetWeight == "")
                                {
                                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "This Tanker No.- " + ddlTankerDetail.SelectedItem.Text + " Already In Process.");
                                    ddlTankerDetail.Items.Clear();
                                    GetTankerDetail();
                                    txtV_VehicleType.Text = "";
                                    txtD_VehicleCapacity.Text = "";
                                    txtV_VenderName.Text = "";
                                    txtV_VendorContactNo.Text = "";
                                    txtTankerStatus.Text = "";
                                    Divaddtanker.Visible = false;
                                    return;
                                }
                            }

                        }

                        Divaddtanker.Visible = true;
                        string strvtype = ds.Tables[0].Rows[0]["V_VehicleType"].ToString();

                        if (strvtype == "S")
                        {
                            txtV_VehicleType.Text = "Single Compartment";
                        }
                        if (strvtype == "D")
                        {
                            txtV_VehicleType.Text = "Dual Compartment";
                        }


                        txtD_VehicleCapacity.Text = ds.Tables[0].Rows[0]["D_VehicleCapacity"].ToString();
                        txtV_VenderName.Text = ds.Tables[0].Rows[0]["V_VenderName"].ToString();
                        txtV_VendorContactNo.Text = ds.Tables[0].Rows[0]["V_VendorContactNo"].ToString();

                        string strTankerStatus = ds.Tables[0].Rows[0]["TankerStatus"].ToString();

                        if (strTankerStatus != "FALSE")
                        {
                            txtTankerStatus.Text = "Tanker Is Ready";
                        }
                        else
                        {
                            txtTankerStatus.Text = "Tanker Is Booked";
                        }

                    }
                    else
                    {
                        txtV_VehicleType.Text = "";
                        txtD_VehicleCapacity.Text = "";
                        txtV_VenderName.Text = "";
                        txtV_VendorContactNo.Text = "";
                        txtTankerStatus.Text = "";
                        Divaddtanker.Visible = false;

                    }
                }
            }
            if (ddlTankerDetail.SelectedIndex != 0)
            {
                if (txtV_VehicleType.Text == "Single Compartment")
                {


                    ddlCompartmentType.Items.Clear();
                    ddlCompartmentType.Items.Add(new ListItem("Single", "S"));
                    ddlCompartmentType.Enabled = false;

                    //Hide Milk Quality detail add multi time
                    dv_gvMilkQualityDeailsAddButton.Visible = true;
                    dv_gvMilkQualityDeails.Visible = true;

                    //BindAdulterationTestGrid();



                    //show Add generate seal entry control when Single Compartment tanker
                    //btnAdd.Visible = false;

                    //Disable required field validator in add quality details setction
                    rfvMilkQuality_S.Enabled = true;
                    rfvMilkQuantity_S.Enabled = true;
                    rfvTemprature_S.Enabled = true;
                    rfvAcidity_S.Enabled = true;
                    rfvCOB_S.Enabled = true;
                    rfvFat_S.Enabled = true;
                    rfvSNF_S.Enabled = true;
                    rfvCLR_S.Enabled = true;

                    //Disable regular expression validator in add quality details setction
                    revMilkQuantity_S.Enabled = true;
                    revTemprature_S.Enabled = true;
                    revAcidity_S.Enabled = true;
                    revFat_S.Enabled = true;
                    revSNF_S.Enabled = true;
                    revCLR_S.Enabled = true;
                    revMBRT_S.Enabled = true;

                    //Disable required field validator in add quality details setction
                    rfvMilkQuality.Enabled = false;
                    rfvMilkQuantity.Enabled = false;
                    rfvTemprature.Enabled = false;
                    rfvAcidity.Enabled = false;
                    rfvCOB.Enabled = false;
                    rfvFat.Enabled = false;
                    rfvSNF.Enabled = false;
                    rfvCLR.Enabled = false;

                    //Disable regular expression validator in add quality details setction
                    revMilkQuantity.Enabled = false;
                    revTemprature.Enabled = false;
                    revAcidity.Enabled = false;
                    revFat.Enabled = false;
                    revSNF.Enabled = false;
                    revCLR.Enabled = false;
                    revMBRT.Enabled = false;



                }
                else if (txtV_VehicleType.Text == "Dual Compartment")
                {


                    ddlCompartmentType.Items.Clear();
                    ddlCompartmentType.Items.Insert(0, new ListItem("Select", "0"));


                    ddlCompartmentType.Items.Add(new ListItem("Front", "F"));
                    ddlCompartmentType.Items.Add(new ListItem("Rear", "R"));


                    ddlCompartmentType.Enabled = true;

                    //Show Milk Quality detail add multi time
                    dv_gvMilkQualityDeailsAddButton.Visible = true;
                    dv_gvMilkQualityDeails.Visible = true;

                    //Adulteration section Visible=false when change Tanker type
                    //dv_CompartmentTypeforAdulteration.Visible = false;
                    //dv_AdulterationType.Visible = false;
                    //dv_AdulterationValue.Visible = false;
                    //dv_btnAdulterationTest.Visible = false;
                    ////dv_AdulterationHeaderSection.Visible = false;
                    //dv_AdulterationTestGridVIew.Visible = false;

                    //hide Add generate seal entry control when Single Compartment tanker
                    // btnAdd.Visible = false;

                    //Disable required field validator in add quality details setction
                    rfvMilkQuality_S.Enabled = false;
                    rfvMilkQuantity_S.Enabled = false;
                    rfvTemprature_S.Enabled = false;
                    rfvAcidity_S.Enabled = false;
                    rfvCOB_S.Enabled = false;
                    rfvFat_S.Enabled = false;
                    rfvSNF_S.Enabled = false;
                    rfvCLR_S.Enabled = false;

                    //Disable regular expression validator in add quality details setction
                    revMilkQuantity_S.Enabled = false;
                    revTemprature_S.Enabled = false;
                    revAcidity_S.Enabled = false;
                    revFat_S.Enabled = false;
                    revSNF_S.Enabled = false;
                    revCLR_S.Enabled = false;
                    revMBRT_S.Enabled = false;

                    //Disable required field validator in add quality details setction
                    rfvMilkQuality.Enabled = true;
                    rfvMilkQuantity.Enabled = true;
                    rfvTemprature.Enabled = true;
                    rfvAcidity.Enabled = true;
                    rfvCOB.Enabled = true;
                    rfvFat.Enabled = true;
                    rfvSNF.Enabled = true;
                    rfvCLR.Enabled = true;

                    //Disable regular expression validator in add quality details setction
                    revMilkQuantity.Enabled = true;
                    revTemprature.Enabled = true;
                    revAcidity.Enabled = true;
                    revFat.Enabled = true;
                    revSNF.Enabled = true;
                    revCLR.Enabled = true;
                    revMBRT.Enabled = true;

                    //Milk Sample Details

                }
            }
            else
            {


                ddlCompartmentType.Items.Clear();
                ddlCompartmentType.Items.Insert(0, new ListItem("Select", "0"));
                ddlCompartmentType.Enabled = true;

                //Hide Milk Quality detail add multi time
                dv_gvMilkQualityDeailsAddButton.Visible = false;
                dv_gvMilkQualityDeails.Visible = false;



                //hide Add generate seal entry control when selected index = 0
                // btnAdd.Visible = false;

                //Disable required field validator in add quality details setction
                rfvMilkQuality_S.Enabled = true;
                rfvMilkQuantity_S.Enabled = true;
                rfvTemprature_S.Enabled = true;
                rfvAcidity_S.Enabled = true;
                rfvCOB_S.Enabled = true;
                rfvFat_S.Enabled = true;
                rfvSNF_S.Enabled = true;
                rfvCLR_S.Enabled = true;

                //Disable regular expression validator in add quality details setction
                revMilkQuantity_S.Enabled = true;
                revTemprature_S.Enabled = true;
                revAcidity_S.Enabled = true;
                revFat_S.Enabled = true;
                revSNF_S.Enabled = true;
                revCLR_S.Enabled = true;
                revMBRT_S.Enabled = true;

                //Disable required field validator in add quality details setction
                rfvMilkQuality.Enabled = false;
                rfvMilkQuantity.Enabled = false;
                rfvTemprature.Enabled = false;
                rfvAcidity.Enabled = false;
                rfvCOB.Enabled = false;
                rfvFat.Enabled = false;
                rfvSNF.Enabled = false;
                rfvCLR.Enabled = false;

                //Disable regular expression validator in add quality details setction
                revMilkQuantity.Enabled = false;
                revTemprature.Enabled = false;
                revAcidity.Enabled = false;
                revFat.Enabled = false;
                revSNF.Enabled = false;
                revCLR.Enabled = false;
                revMBRT.Enabled = false;


            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void GetCCDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                     new string[] { "flag", "Office_Parant_ID" },
                     new string[] { "3", objdb.Office_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlccdetails.DataTextField = "Office_Name";
                        ddlccdetails.DataValueField = "Office_ID";
                        ddlccdetails.DataSource = ds;
                        ddlccdetails.DataBind();
                        ddlccdetails.Items.Insert(0, new ListItem("Select", "0"));

                    }
                }
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnAddcc_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        AddCCDetails();
    }

    private void AddCCDetails()
    {
        try
        {

            int CompartmentType = 0;

            if (Convert.ToString(ViewState["InsertRecord"]) == null || Convert.ToString(ViewState["InsertRecord"]) == "")
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("I_OfficeID", typeof(int)));
                dt.Columns.Add(new DataColumn("Office_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("TI_SequenceNo", typeof(int)));

                dr = dt.NewRow();
                dr[0] = 1;
                dr[1] = ddlccdetails.SelectedValue;
                dr[2] = ddlccdetails.SelectedItem.Text;
                dr[3] = txtTI_SequenceNo.Text;
                dt.Rows.Add(dr);

                ddlCC.DataTextField = "Office_Name";
                ddlCC.DataValueField = "I_OfficeID";
                ddlCC.DataSource = dt;
                ddlCC.DataBind();
                ddlCC.Items.Insert(0, new ListItem("Select", "0"));

                ViewState["InsertRecord"] = dt;
                gv_CCDetails.DataSource = dt;
                gv_CCDetails.DataBind();

            }
            else
            {
                DataTable dt = new DataTable();
                DataTable DT = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("I_OfficeID", typeof(int)));
                dt.Columns.Add(new DataColumn("Office_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("TI_SequenceNo", typeof(int)));
                DT = (DataTable)ViewState["InsertRecord"];

                if (txtV_VehicleType.Text == "Dual Compartment")
                {
                    if (DT.Rows.Count > 1)
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Only 2 CC Allow in Dual Compartment");
                        return;
                    }
                }
                if (txtV_VehicleType.Text == "Single Compartment")
                {
                    if (DT.Rows.Count > 0)
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Only 1 CC Allow in Single Compartment");
                        return;
                    }
                }


                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (ddlccdetails.SelectedValue == DT.Rows[i]["I_OfficeID"].ToString())
                    {
                        CompartmentType = 1;
                    }
                    if (txtTI_SequenceNo.Text == DT.Rows[i]["TI_SequenceNo"].ToString())
                    {
                        CompartmentType = 2;
                    }
                }

                if (CompartmentType == 1)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "CC of \"" + ddlccdetails.SelectedItem.Text + "\" already exist.");
                }
                else if (CompartmentType == 2)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Sequence no. \"" + txtTI_SequenceNo.Text + "\" already exist.");
                }
                else
                {
                    dr = dt.NewRow();
                    dr[0] = 1;
                    dr[1] = ddlccdetails.SelectedValue;
                    dr[2] = ddlccdetails.SelectedItem.Text;
                    dr[3] = txtTI_SequenceNo.Text;
                    dt.Rows.Add(dr);

                }

                foreach (DataRow tr in DT.Rows)
                {
                    dt.Rows.Add(tr.ItemArray);
                }
                ViewState["InsertRecord"] = dt;
                ddlCC.DataTextField = "Office_Name";
                ddlCC.DataValueField = "I_OfficeID";
                ddlCC.DataSource = ViewState["InsertRecord"];
                ddlCC.DataBind();
                ddlCC.Items.Insert(0, new ListItem("Select", "0"));

                gv_CCDetails.DataSource = dt;
                gv_CCDetails.DataBind();
            }

            //Clear Record

            ddlccdetails.ClearSelection();
            txtTI_SequenceNo.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void btnTankerValveSealDetails_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        AddSealDetails();
    }

    protected void ddlSealColor_Init(object sender, EventArgs e)
    {
        DataSet ds1 = objdb.ByProcedure("USP_Mst_SealColor",
                              new string[] { "flag" },
                              new string[] { "1" }, "dataset");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            ddlSealColor.DataSource = ds1;
            ddlSealColor.DataTextField = "V_SealColor";
            ddlSealColor.DataValueField = "I_SealColorID";
            ddlSealColor.DataBind();
            //ddlSealColor.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {
            ddlSealColor.DataSource = string.Empty;
            ddlSealColor.DataBind();
            ddlSealColor.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    private void AddSealDetails()
    {
        try
        {
            int CompartmentType = 0;

            if (Convert.ToString(ViewState["InsertRecord3"]) == null || Convert.ToString(ViewState["InsertRecord3"]) == "")
            {
                DataTable dt1 = new DataTable();
                DataRow dr1;
                dt1.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt1.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
                dt1.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));
                dt1.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));
                dt1.Columns.Add(new DataColumn("V_SealColor", typeof(string)));

                dr1 = dt1.NewRow();
                dr1[0] = 1;
                dr1[1] = txtV_SealNo.Text;
                dr1[2] = txtV_SealRemark.Text;
                dr1[3] = ddlSealColor.SelectedValue;
                dr1[4] = ddlSealColor.SelectedItem;
                dt1.Rows.Add(dr1);

                ViewState["InsertRecord3"] = dt1;
                gv_SealInfo.DataSource = dt1;
                gv_SealInfo.DataBind();
            }
            else
            {
                DataTable dt1 = new DataTable();
                DataTable DT1 = new DataTable();
                DataRow dr1;
                dt1.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt1.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
                dt1.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));
                dt1.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));
                dt1.Columns.Add(new DataColumn("V_SealColor", typeof(string)));

                DT1 = (DataTable)ViewState["InsertRecord3"];

                if (DT1.Rows.Count > 9)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Only 10 Seal Allow in One Tanker");
                    return;
                }


                for (int i = 0; i < DT1.Rows.Count; i++)
                {
                    if (txtV_SealNo.Text == DT1.Rows[i]["V_SealNo"].ToString())
                    {
                        CompartmentType = 1;
                    }
                }
                if (CompartmentType == 1)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Seal of \"" + txtV_SealNo.Text + "\" already exist.");

                }
                else
                {
                    dr1 = dt1.NewRow();
                    dr1[0] = 1;
                    dr1[1] = txtV_SealNo.Text;
                    dr1[2] = txtV_SealRemark.Text;
                    dr1[3] = ddlSealColor.SelectedValue;
                    dr1[4] = ddlSealColor.SelectedItem;
                    dt1.Rows.Add(dr1);
                }

                foreach (DataRow tr in DT1.Rows)
                {
                    dt1.Rows.Add(tr.ItemArray);
                }
                ViewState["InsertRecord3"] = dt1;
                gv_SealInfo.DataSource = dt1;
                gv_SealInfo.DataBind();
            }

            //Clear Record
            txtV_SealNo.Text = "";
            txtV_SealRemark.Text = "";
            ddlSealColor.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("MilkInwardOutwardReferenceDetails.aspx", false);
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt2 = ViewState["InsertRecord3"] as DataTable;
            dt2.Rows.Remove(dt2.Rows[row.RowIndex]);
            ViewState["InsertRecord3"] = dt2;
            gv_SealInfo.DataSource = dt2;
            gv_SealInfo.DataBind();

            txtV_SealNo.Text = "";
            txtV_SealRemark.Text = "";
            ddlSealColor.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void lnkDeleteCC_Click(object sender, EventArgs e)
    {
        try
        {
            string Office_ID = "";
            lblMsg.Text = "";
            GridViewRow row1 = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt3 = ViewState["InsertRecord"] as DataTable;
            


            Office_ID = dt3.Rows[row1.RowIndex]["I_OfficeID"].ToString();
            dt3.Rows.Remove(dt3.Rows[row1.RowIndex]);
            ViewState["InsertRecord"] = dt3;


            DataTable dt = ViewState["InsertRecord1"] as DataTable;
            DataView view = new DataView(dt);
            view.RowFilter = "I_OfficeID = " + Office_ID + "";

            foreach (DataRowView row in view)
            {
                row.Delete();
            }
            ViewState["InsertRecord1"] = dt;
            gv_MilkQualityDetail.DataSource = dt;
            gv_MilkQualityDetail.DataBind();

            
            BindAdulterationTestGrid();
            ddlCC.DataTextField = "Office_Name";
            ddlCC.DataValueField = "I_OfficeID";
            ddlCC.DataSource = dt3;
            ddlCC.DataBind();
            ddlCC.Items.Insert(0, new ListItem("Select", "0"));
            gv_CCDetails.DataSource = dt3;
            gv_CCDetails.DataBind();
            ddlccdetails.ClearSelection();
            txtTI_SequenceNo.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    private DataTable GetCCGridvalue()
    {
        DataTable dtcc = new DataTable();
        DataRow drcc;

        dtcc.Columns.Add(new DataColumn("TI_SequenceNo", typeof(string)));
        dtcc.Columns.Add(new DataColumn("I_OfficeID", typeof(string)));
        dtcc.Columns.Add(new DataColumn("B_SequenceStatus", typeof(string)));

        foreach (GridViewRow rowcc in gv_CCDetails.Rows)
        {
            Label lblTI_SequenceNo = (Label)rowcc.FindControl("lblTI_SequenceNo");
            Label lblI_OfficeID = (Label)rowcc.FindControl("lblI_OfficeID");

            drcc = dtcc.NewRow();
            drcc[0] = lblTI_SequenceNo.Text;
            drcc[1] = lblI_OfficeID.Text;
            ViewState["CCID"] = lblI_OfficeID.Text;
            drcc[2] = "1";
            dtcc.Rows.Add(drcc);
        }
        return dtcc;
    }

    private DataTable GetSealGridvalue()
    {

        DataTable dtseal = new DataTable();
        DataRow drseal;
        dtseal.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
        dtseal.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
        dtseal.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));
        dtseal.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));

        foreach (GridViewRow rowseal in gv_SealInfo.Rows)
        {
            Label lblV_SealNo = (Label)rowseal.FindControl("lblV_SealNo");
            Label lblV_SealRemark = (Label)rowseal.FindControl("lblV_SealRemark");
            Label lblTI_SealColor = (Label)rowseal.FindControl("lblTI_SealColor");

            drseal = dtseal.NewRow();

            drseal[0] = lblV_SealNo.Text;
            drseal[1] = "VB";
            drseal[2] = lblV_SealRemark.Text;
            drseal[3] = lblTI_SealColor.Text;
            dtseal.Rows.Add(drseal);
        }
        return dtseal;
    }

    private DataTable GetChallanDetails()
    {

        DataTable dtChallan = new DataTable();
        DataRow drChallan;
        dtChallan.Columns.Add(new DataColumn("I_OfficeID", typeof(int)));

        foreach (GridViewRow rowcc in gv_CCDetails.Rows)
        {
            Label lblI_OfficeID = (Label)rowcc.FindControl("lblI_OfficeID");

            drChallan = dtChallan.NewRow();

            drChallan[0] = lblI_OfficeID.Text;
            dtChallan.Rows.Add(drChallan);
        }
        return dtChallan;
    }

    private DataTable GetMilkQualityDetails()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("I_OfficeID", typeof(int)));
        dt.Columns.Add(new DataColumn("I_MilkQuantity", typeof(int)));
        dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
        dt.Columns.Add(new DataColumn("D_FAT", typeof(decimal)));
        dt.Columns.Add(new DataColumn("D_SNF", typeof(decimal)));
        dt.Columns.Add(new DataColumn("D_CLR", typeof(decimal)));
        dt.Columns.Add(new DataColumn("V_Temp", typeof(string)));
        dt.Columns.Add(new DataColumn("V_Acidity", typeof(string)));
        dt.Columns.Add(new DataColumn("V_COB", typeof(string)));
        dt.Columns.Add(new DataColumn("V_Alcohol", typeof(string)));
        dt.Columns.Add(new DataColumn("V_MBRT", typeof(string)));
        dt.Columns.Add(new DataColumn("V_MilkQuality", typeof(string)));

        foreach (GridViewRow row in gv_MilkQualityDetail.Rows)
        {
            Label lblI_OfficeID = (Label)row.FindControl("lblI_OfficeID");
            Label lblMilkQuantity = (Label)row.FindControl("lblMilkQuantity");
            Label lblSealLocation = (Label)row.FindControl("lblSealLocation");
            Label lblFAT = (Label)row.FindControl("lblFAT");
            Label lblSNF = (Label)row.FindControl("lblSNF");
            Label lblCLR = (Label)row.FindControl("lblCLR");
            Label lblTemp = (Label)row.FindControl("lblTemp");
            Label lblAcidity = (Label)row.FindControl("lblAcidity");
            Label lblCOB = (Label)row.FindControl("lblCOB");
            Label lblAlcohol = (Label)row.FindControl("lblAlcohol");
            Label lblMBRT = (Label)row.FindControl("lblMBRT");
            Label lblMilkQuality = (Label)row.FindControl("lblMilkQuality");

            dr = dt.NewRow();
            dr[0] = lblI_OfficeID.Text;
            dr[1] = lblMilkQuantity.Text;
            dr[2] = lblSealLocation.Text;
            dr[3] = lblFAT.Text;
            dr[4] = lblSNF.Text;
            dr[5] = lblCLR.Text;
            dr[6] = lblTemp.Text;
            dr[7] = lblAcidity.Text;
            dr[8] = lblCOB.Text;
            dr[9] = lblAlcohol.Text;
            dr[10]= lblMBRT.Text;
            dr[11] = lblMilkQuality.Text;
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private DataTable GetAdulterationDetails()
    {
        int I_OfficeID = 0;
        string V_SealLocationAlias = "";
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("I_OfficeID", typeof(int)));
        dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
        dt.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));
        dt.Columns.Add(new DataColumn("V_AdulterationValue", typeof(string)));

        foreach (GridViewRow rows in gvmilkAdulterationtestdetail.Rows)
        {
            Label lblATSealLocation = (Label)rows.FindControl("lblSealLocation");
            Label lblAdulterationType = (Label)rows.FindControl("lblAdulterationType");
            DropDownList ddlAdelterationTestValue = (DropDownList)rows.FindControl("ddlAdelterationTestValue");

            string SealLocation = "";
            switch (lblATSealLocation.Text)
            {
                case "Single":
                    SealLocation = "S";
                    break;
                case "Rear":
                    SealLocation = "R";
                    break;
                case "Front":
                    SealLocation = "F";
                    break;
                default:
                    break;
            }
            foreach (GridViewRow row in gv_MilkQualityDetail.Rows)
            {
                Label lblI_OfficeID = (Label)row.FindControl("lblI_OfficeID");
                Label lblV_SealLocationAlias = (Label)row.FindControl("V_SealLocationAlias");
                if (lblATSealLocation.Text == lblV_SealLocationAlias.Text)
                {
                    I_OfficeID = int.Parse(lblI_OfficeID.Text);
                   
                }
                
            }
            
                dr = dt.NewRow();
                dr[0] = I_OfficeID;
                dr[1] = SealLocation;
                dr[2] = lblAdulterationType.Text;
                dr[3] = ddlAdelterationTestValue.SelectedValue;
                dt.Rows.Add(dr);
            

        }

        return dt;
    }

    public string GetTankerCurrentStatus()
    {
        string strtankerlivestatus = "False";

        try
        {
            DataSet dscheckTanker = objdb.ByProcedure("Usp_TankerDetail",
                     new string[] { "flag", "I_TankerID" },
                     new string[] { "2", ddlTankerDetail.SelectedValue }, "dataset");

            if (dscheckTanker != null)
            {
                if (dscheckTanker.Tables.Count > 0)
                {
                    if (dscheckTanker.Tables[0].Rows.Count > 0)
                    {
                        strtankerlivestatus = dscheckTanker.Tables[0].Rows[0]["TankerStatus"].ToString();

                        return strtankerlivestatus;
                    }
                    else
                    {
                        return strtankerlivestatus;
                    }
                }
                else
                {
                    return strtankerlivestatus;
                }
            }
            else
            {
                return strtankerlivestatus;
            }

        }
        catch (Exception ex)
        {

            return strtankerlivestatus;
        }


    }

    public string GetTankerCurrentStatus1()
    {
        string strtankerlivestatus = "False";

        try
        {

            DataSet dscheckTanker1 = objdb.ByProcedure("Usp_TankerDetail",
                   new string[] { "flag", "I_TankerID", "I_OfficeID" },
                   new string[] { "2", ddlTankerDetail.SelectedValue, objdb.Office_ID() }, "dataset");

            if (dscheckTanker1 != null)
            {
                if (dscheckTanker1.Tables.Count > 0)
                {
                    if (dscheckTanker1.Tables[0].Rows.Count > 0)
                    {

                        string TS = dscheckTanker1.Tables[0].Rows[0]["TS"].ToString();

                        if (TS == "OLD")
                        {
                            string D_GrossWeight = dscheckTanker1.Tables[0].Rows[0]["D_GrossWeight"].ToString();
                            string D_NetWeight = dscheckTanker1.Tables[0].Rows[0]["D_NetWeight"].ToString();
                            string RefCancelDT = dscheckTanker1.Tables[0].Rows[0]["RefCancelDT"].ToString();

                            if (RefCancelDT == "")
                            {
                                if (D_GrossWeight == "" || D_NetWeight == "")
                                {
                                    return strtankerlivestatus;
                                }
                                else
                                {
                                    strtankerlivestatus = dscheckTanker1.Tables[0].Rows[0]["TankerStatus"].ToString();
                                    return strtankerlivestatus;
                                }

                            }
                            else
                            {
                                strtankerlivestatus = dscheckTanker1.Tables[0].Rows[0]["TankerStatus"].ToString();
                                return strtankerlivestatus;

                            }

                        }

                        else
                        {
                            strtankerlivestatus = dscheckTanker1.Tables[0].Rows[0]["TankerStatus"].ToString();
                            return strtankerlivestatus;

                        }

                    }
                    else
                    {
                        return strtankerlivestatus;
                    }
                }
                else
                {
                    return strtankerlivestatus;
                }
            }
            else
            {
                return strtankerlivestatus;
            }

        }
        catch (Exception)
        {

            return strtankerlivestatus;
        }

    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                lblMsg.Text = "";

                string strtS = GetTankerCurrentStatus1();

                if (strtS == "True")
                {

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Oops! " + "This Tanker Already In-Process / Something went wrong");
                    GetViewRefDetails();
                    return;
                }

                DataTable dtccF = new DataTable();
                dtccF = GetCCGridvalue();
                if (ddlMovemnetType.SelectedItem.Text == "Empty Tanker")
                {
                    if (dtccF.Rows.Count > 0)
                    {
                        DataTable dtsealF = new DataTable();
                        dtsealF = GetSealGridvalue();

                        //if (dtsealF.Rows.Count > 0)
                        //{

                        ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                                           new string[] { "Flag",
                                                 "I_TankerID"
				                                ,"I_OfficeID"
				                                ,"I_OfficeTypeID" 
				                                ,"V_EntryType" 
				                                ,"V_DriverName"
				                                ,"V_DriverMobileNo"
				                                ,"V_EntryFrom"
				                                ,"V_Latitude"
				                                ,"V_Longitude"  
				                                ,"I_CreatedBy"
				                                ,"V_IPAddress"
				                                ,"V_MacAddress"
                                                ,"NV_DriverDrivingLicenceNo"
                                                ,"MovementType"
                                    },
                                           new string[] { "2" ,
                                                ddlTankerDetail.SelectedValue,
                                                objdb.Office_ID(),
                                                objdb.OfficeType_ID(), 
                                                "Out",  
                                                txtV_DriverName.Text.TrimEnd().TrimStart(),
                                                txtV_DriverMobileNo.Text,
                                                "Web",
                                                "22.888",
                                                "22.888",
                                                objdb.createdBy(),
                                                objdb.GetLocalIPAddress(),
                                                objdb.GetMACAddress(),
                                                txtNV_DriverDrivingLicenceNo.Text,
                                                ddlMovemnetType.SelectedItem.Text
                                                 
                                    },
                                          new string[] { "type_Trn_MilkInwardOfficeSequenceDetails", 
                                        "type_Trn_TankerValveSealDetails"},
                                          new DataTable[] { dtccF, dtsealF }, "TableSave");



                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            Session["IsSuccess"] = true;
                            Response.Redirect("MilkInwardOutwardReferenceDetails.aspx", false);

                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            Session["IsSuccess"] = false;
                        }

                        Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

                        //}
                        //else
                        //{
                        //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "First To Fill At Least One Tanker Valve Seal Details");
                        //}
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "First To Fill At Least One Chilling Centre Details");
                    }
                }
                else
                {
                    if (dtccF.Rows.Count > 0)
                    {
                        DataTable dtsealF = new DataTable();
                        dtsealF = GetSealGridvalue();

                        DataTable dtChallanDetails = new DataTable();
                        dtChallanDetails = GetChallanDetails();

                        DataTable dtMQD = new DataTable();
                        dtMQD = GetMilkQualityDetails();

                        DataTable dtADT = new DataTable();
                        dtADT = GetAdulterationDetails();


                        int MQCount = dtMQD.Rows.Count;
                        int CCCount = dtccF.Rows.Count;
                        int SealCount = dtsealF.Rows.Count;
                        
                            if (MQCount >= CCCount)
                            {
                                if (dtsealF.Rows.Count > 0)
                                {


                                    string V_TankerType = "";
                                    if (txtV_VehicleType.Text == "Dual Compartment")
                                    {
                                        V_TankerType = "D";
                                    }
                                    else
                                    {
                                        V_TankerType = "S";
                                    }
                                    //if (dtsealF.Rows.Count > 0)
                                    //{
                                    ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                                                       new string[] { "Flag",
                                                 "I_TankerID"
				                                ,"I_OfficeID"
				                                ,"I_OfficeTypeID" 
				                                ,"V_EntryType" 
				                                ,"V_DriverName"
				                                ,"V_DriverMobileNo"
				                                ,"V_EntryFrom"
				                                ,"V_Latitude"
				                                ,"V_Longitude"  
				                                ,"I_CreatedBy"
				                                ,"V_IPAddress"
				                                ,"V_MacAddress"
                                                ,"NV_DriverDrivingLicenceNo"
                                                ,"MovementType"
                                                ,"V_TankerType"
                                                ,"V_VehicleNo"
                                    },
                                                       new string[] { "32" ,
                                                ddlTankerDetail.SelectedValue,
                                                objdb.Office_ID(),
                                                objdb.OfficeType_ID(), 
                                                "Out",  
                                                txtV_DriverName.Text.TrimEnd().TrimStart(),
                                                txtV_DriverMobileNo.Text,
                                                "Web",
                                                "22.888",
                                                "22.888",
                                                objdb.createdBy(),
                                                objdb.GetLocalIPAddress(),
                                                objdb.GetMACAddress(),
                                                txtNV_DriverDrivingLicenceNo.Text,
                                                ddlMovemnetType.SelectedItem.Text,
                                                V_TankerType,
                                                ddlTankerDetail.SelectedItem.Text
                                                 
                                    },
                                                      new string[] { "type_Trn_MilkInwardOfficeSequenceDetails", 
                                        "type_Trn_TankerValveSealDetails","type_Trn_MilkInwardOutwarDetails","type_Trn_MilkQualityDetailsFilledTanker","type_Trn_tblAdulterationTestFilledTanker"},
                                                      new DataTable[] { dtccF, dtsealF, dtChallanDetails, dtMQD, dtADT }, "TableSave");



                                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                    {
                                        Session["IsSuccess"] = true;
                                        Response.Redirect("MilkInwardOutwardReferenceDetails_New.aspx", false);

                                    }



                                    else
                                    {
                                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                        Session["IsSuccess"] = false;
                                    }

                                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

                                    //}
                                    //else
                                    //{
                                    //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "First To Fill At Least One Tanker Valve Seal Details");
                                    //}
                                }
                                else
                                {
                                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Please Fill Tanker Seal Details.");
                                    return;
                                }
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Please Fill Quality Details of Selected CC.");
                                return;
                            }
                        
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "First To Fill At Least One Chilling Centre Details");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void GetViewRefDetails()
    {
        try
        {
            ds = null;
            string date = "";

            if (txtDate.Text != "")
            {
                date = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                     new string[] { "flag", "I_OfficeID", "D_Date" },
                     new string[] { "28", objdb.Office_ID(), date }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv_viewreferenceno.DataSource = ds;
                        gv_viewreferenceno.DataBind();

                    }
                    else
                    {
                        gv_viewreferenceno.DataSource = null;
                        gv_viewreferenceno.DataBind();
                    }
                }
                else
                {
                    gv_viewreferenceno.DataSource = null;
                    gv_viewreferenceno.DataBind();
                }
            }
            else
            {
                gv_viewreferenceno.DataSource = null;
                gv_viewreferenceno.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void lblAddTanker_Click(object sender, EventArgs e)
    {
        GetData();
        Clear();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
    }

    protected void Clear()
    {
        ddlTankerDetailT.SelectedIndex = 0;
        txtD_VehicleCapacityT.Text = "";
        txtV_VenderNameT.Text = "";
        txtV_VehicleNoT.Text = "";
        txtV_VendorContactNoT.Text = "";
        chkIsActive.Checked = true;
        gv_TankerDetails.SelectedIndex = -1;
    }

    protected void GetData()
    {
        try
        {
            gv_TankerDetails.DataSource = objdb.ByProcedure("Usp_TankerDetail",
                            new string[] { "flag", "MilkCollectionFrom", "I_OfficeID" },
                            new string[] { "1", "CC", objdb.Office_ID() }, "dataset");
            gv_TankerDetails.DataBind();
        }
        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }
    }

    protected void gv_TankerDetails_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    Label lblTankerType_ID = (Label)row.FindControl("LblvendorType");
                    Label lblVehicle_No = (Label)row.FindControl("lblV_VehicleNo");
                    Label lblVehicle_Capacity = (Label)row.FindControl("lblV_VehicleCapacity");
                    Label lblVendor_Name = (Label)row.FindControl("lblV_Vendor");
                    Label lblVendor_ContactNo = (Label)row.FindControl("lblV_VendorContact");
                    Label lblIsActive = (Label)row.FindControl("lblTstatus");
                    Label lblMilkCollectionFrom = (Label)row.FindControl("lblMilkCollectionFrom");
                    if (lblIsActive.Text == "False")
                    {
                        chkIsActive.Checked = false;
                    }
                    else
                    {
                        chkIsActive.Checked = true;
                    }

                    ddlMilkCollectionFrom.SelectedValue = lblMilkCollectionFrom.Text;

                    ddlTankerDetailT.SelectedValue = lblTankerType_ID.Text;
                    txtD_VehicleCapacityT.Text = lblVehicle_Capacity.Text;

                    txtV_VehicleNoT.Text = lblVehicle_No.Text;
                    txtD_VehicleCapacityT.Text = lblVehicle_Capacity.Text;
                    txtV_VenderNameT.Text = lblVendor_Name.Text;
                    txtV_VendorContactNoT.Text = lblVendor_ContactNo.Text;


                    ViewState["rowid"] = e.CommandArgument;

                    btnSaveTankerDetails.Text = "Update";

                    foreach (GridViewRow gvRow in gv_TankerDetails.Rows)
                    {
                        if (gv_TankerDetails.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            gv_TankerDetails.SelectedIndex = gvRow.DataItemIndex;
                            gv_TankerDetails.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
            }

        }
        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }
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

                if (btnSaveTankerDetails.Text == "Submit")
                {

                    ds = objdb.ByProcedure("Usp_TankerDetail", new string[] { "flag", "V_VehicleType"
				                                ,"V_VehicleNo"
				                                ,"I_OfficeID" 
				                                ,"I_OfficeTypeID" 
				                                ,"D_VehicleCapacity"
				                                ,"V_VenderName"
				                                ,"V_VendorContactNo"
                                                ,"CreatedBy"
                                                ,"IsActive"
                                                ,"MilkCollectionFrom"
                },
                                                    new string[] { "3",  ddlTankerDetailT.SelectedValue,
                                                txtV_VehicleNoT.Text,
                                                objdb.Office_ID(),
                                                objdb.OfficeType_ID(), 
                                                txtD_VehicleCapacityT.Text,
                                                txtV_VenderNameT.Text,
                                                txtV_VendorContactNoT.Text,                                        
                                                objdb.createdBy(),isactive.ToString(),
                                                ddlMilkCollectionFrom.SelectedValue   }, "dataset");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {

                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                        lblPopupMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        Clear();
                        GetData();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    }
                    else
                    {

                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        if (error == "Already Exists.")
                        {
                            lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Vehicle No Already Exists");
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                        }
                        else
                        {
                            lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + error);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                        }
                    }
                }
                if (btnSaveTankerDetails.Text == "Update")
                {
                    ds = objdb.ByProcedure("Usp_TankerDetail", new string[] { "flag", "V_VehicleType"
				                                ,"V_VehicleNo"
				                                ,"I_OfficeID" 
				                                ,"I_OfficeTypeID" 
				                                ,"D_VehicleCapacity"
				                                ,"V_VenderName"
				                                ,"V_VendorContactNo"
                                                ,"CreatedBy"
                                                ,"IsActive"
                                                ,"I_TankerID"
                                                ,"MilkCollectionFrom"
                },
                                                   new string[] { "4",  ddlTankerDetailT.SelectedValue,
                                                txtV_VehicleNoT.Text,
                                                objdb.Office_ID(),
                                                objdb.OfficeType_ID(), 
                                                txtD_VehicleCapacityT.Text,
                                                txtV_VenderNameT.Text,
                                                txtV_VendorContactNoT.Text,                                        
                                                objdb.createdBy(),isactive.ToString(),ViewState["rowid"].ToString(),
                                                ddlMilkCollectionFrom.SelectedValue   }, "dataset");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {

                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                        lblPopupMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        Clear();
                        GetData();
                        btnSaveTankerDetails.Text = "Save";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    }
                    else
                    {
                        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        if (error == "Already Exists.")
                        {
                            lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Vehicle No Already Exists");
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                        }
                        else
                        {
                            lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + error);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                        }
                    }
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
            }
        }

        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("MilkInwardOutwardReferenceDetails.aspx", false);
    }

    protected void btnCrossButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("MilkInwardOutwardReferenceDetails.aspx", false);
    }


    protected void gv_TankerDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Finding label
                Label lblTankerStatus = (Label)e.Row.FindControl("lblTankerStatus");

                LinkButton lnkUpdate = (LinkButton)e.Row.FindControl("lnkUpdate");

                if (lblTankerStatus.Text == "False")
                {
                    lnkUpdate.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }

    }

    protected void ddlMovemnetType_SelectedIndexChanged(object sender, EventArgs e)
    {
        v_MilkQualityDetails.Visible = false;
        if (ddlMovemnetType.SelectedValue == "Filled Tanker")
        {
            v_MilkQualityDetails.Visible = true;

        }
    }

    protected void ddlMilkQuality_Init(object sender, EventArgs e)
    {
        ddlMilkQuality.DataSource = objdb.ByProcedure("USP_Mst_MilkQualityList",
                                new string[] { "flag" },
                                new string[] { "1" }, "dataset");
        ddlMilkQuality.DataValueField = "V_MilkQualityList";
        ddlMilkQuality.DataTextField = "V_MilkQualityList";
        ddlMilkQuality.DataBind();
        ddlMilkQuality.Items.Insert(0, new ListItem("Select", "0"));
    }
    protected void txtMilkQuantity_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            decimal TC = 0;
            if (txtMilkQuantity.Text != "")
            {
                if (ddlCompartmentType.SelectedIndex > 0 || ddlCompartmentType.SelectedValue == "S")
                {
                    if (ddlTankerDetail.SelectedValue == "D")
                    {
                        TC = Convert.ToDecimal(lblD_VehicleCapacity.Text) / 2;
                    }
                    if (ddlTankerDetail.SelectedValue == "S")
                    {
                        TC = Convert.ToDecimal(lblD_VehicleCapacity.Text);
                    }

                    if (Convert.ToDecimal(txtMilkQuantity.Text) > TC)
                    {
                        if (ddlTankerDetail.SelectedValue == "D")
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Entered Milk Quantity in " + ddlCompartmentType.SelectedItem.Text + " Chamber is [" + txtMilkQuantity.Text + " KG] and will not accepted more than [" + TC + " KG] in Tanker " + ddlCompartmentType.SelectedItem.Text + " Chamber.");
                            txtMilkQuantity.Text = "";
                        }
                        if (ddlTankerDetail.SelectedValue == "S")
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Entered Milk Quantity in Single Chamber is [" + txtMilkQuantity.Text + " KG] and will not accepted more than : [" + TC + " KG]");
                            txtMilkQuantity.Text = "";
                        }
                    }
                    else
                    {
                        txtTemprature.Focus();
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Please Select Chamber Type First.");
                    txtMilkQuantity.Text = "";
                }
            }
            else
            {
                lblMsg.Text = "";
                SetFocus(txtMilkQuantity);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }
    protected void txtCLR_TextChanged(object sender, EventArgs e)
    {
        txtSNF.Text = GetSNF().ToString();

        if (txtCLR.Text == "" || txtFat.Text == "")
        { txtSNF.Text = ""; }

        if (txtCLR.Text == "")
        {
            txtCLR.Focus();
        }

        if (txtFat.Text == "")
        {
            txtFat.Focus();
        }

        if (txtCLR.Text != "" && txtFat.Text != "")
        {
            txtMBRT.Focus();
        }
    }

    private decimal GetSNF()
    {
        decimal snf = 0, clr = 0, fat = 1;
        try
        {
            //Formula for Get SNF as given below:
            if (txtCLR.Text != "")
            { clr = Convert.ToDecimal(txtCLR.Text); }
            if (txtFat.Text != "")
            { fat = Convert.ToDecimal(txtFat.Text); }

            //SNF = (CLR / 4) + (FAT * 0.2) + 0.7 -- Formula Before
            //SNF = (CLR / 4) + (0.25 * FAT) + 0.44 -- Formula After Mail dt: 27 Jan 2020, 16:26
            snf = ((clr / 4) + (fat * Convert.ToDecimal(0.25)) + Convert.ToDecimal(0.44));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
        return Math.Round(snf, 2);
    }
    protected void ddlAlcohol_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtAlcoholperc.Text = "";

            if (ddlAlcohol.SelectedValue == "0")
            {
                DivAlcoholper.Visible = false;
            }

            if (ddlAlcohol.SelectedValue == "Positive")
            {
                DivAlcoholper.Visible = false;
            }

            if (ddlAlcohol.SelectedValue == "Negative")
            {
                DivAlcoholper.Visible = true;
            }
        }
        catch (Exception ex)
        {
            DivAlcoholper.Visible = false;
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }
    protected void btnAddQualityDetails_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        AddQualityDetails();
        ddlCompartmentType.Focus();
    }
    private void BindAdulterationTestGrid()
    {
        try
        {
           
            //Start Logic Here
            DataTable dtTL = new DataTable();
            DataRow drTL;

            DataSet DSAT = objdb.ByProcedure("USP_Mst_AdulterationTestList",
                                   new string[] { "flag" },
                                   new string[] { "3" }, "dataset");

            if (DSAT.Tables[0].Rows.Count != 0)
            {
                dtTL.Columns.Add(new DataColumn("S.No", typeof(int)));
                dtTL.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
                dtTL.Columns.Add(new DataColumn("V_SealLocationAlias", typeof(string)));
                dtTL.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));

                if (txtV_VehicleType.Text == "Dual Compartment")
                {
                    for (int i = 0; i < ((DataTable)ViewState["InsertRecord1"]).Rows.Count; i++)
                    {
                        for (int j = 0; j < DSAT.Tables[0].Rows.Count; j++)
                        {
                            drTL = dtTL.NewRow();
                            drTL[0] = dtTL.Rows.Count + 1;
                            drTL[1] = ((DataTable)ViewState["InsertRecord1"]).Rows[i]["V_SealLocation"].ToString();
                            drTL[2] = ((DataTable)ViewState["InsertRecord1"]).Rows[i]["V_SealLocationAlias"].ToString();
                            drTL[3] = DSAT.Tables[0].Rows[j]["V_AdulteratioTName"].ToString();
                            dtTL.Rows.Add(drTL);
                        }
                    }
                }
                else
                {
                    //for (int i = 0; i < ddlCompartmentType.Items.Count; i++)
                    //{
                    //    for (int j = 0; j < DSAT.Tables[0].Rows.Count; j++)
                    //    {
                    //        drTL = dtTL.NewRow();
                    //        drTL[0] = dtTL.Rows.Count + 1;
                    //        drTL[1] = ddlCompartmentType.SelectedValue;
                    //        drTL[2] = ddlCompartmentType.SelectedItem.Text.ToString();
                    //        drTL[3] = DSAT.Tables[0].Rows[j]["V_AdulteratioTName"].ToString();
                    //        dtTL.Rows.Add(drTL);
                    //    }
                    //}
                    for (int i = 0; i < ((DataTable)ViewState["InsertRecord1"]).Rows.Count; i++)
                    {
                        for (int j = 0; j < DSAT.Tables[0].Rows.Count; j++)
                        {
                            drTL = dtTL.NewRow();
                            drTL[0] = dtTL.Rows.Count + 1;
                            drTL[1] = ((DataTable)ViewState["InsertRecord1"]).Rows[i]["V_SealLocation"].ToString();
                            drTL[2] = ((DataTable)ViewState["InsertRecord1"]).Rows[i]["V_SealLocationAlias"].ToString();
                            drTL[3] = DSAT.Tables[0].Rows[j]["V_AdulteratioTName"].ToString();
                            dtTL.Rows.Add(drTL);
                        }
                    }
                }
            }

            gvmilkAdulterationtestdetail.DataSource = dtTL;
            gvmilkAdulterationtestdetail.DataBind();

            milktestdetail.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
        }
    }
    private void AddQualityDetails()
    {
        try
        {
            int CompartmentType = 0;

            string alcoholePer = "";

            if (ddlAlcohol.SelectedValue == "Negative")
            {
                alcoholePer = ddlAlcohol.SelectedValue + "(" + txtAlcoholperc.Text + "%)";
            }
            else if (ddlAlcohol.SelectedValue == "Positive")
            {
                alcoholePer = "Positive";
            }
            else
            {
                alcoholePer = "0";
            }

            if (Convert.ToString(ViewState["InsertRecord1"]) == null || Convert.ToString(ViewState["InsertRecord1"]) == "")
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("I_OfficeID", typeof(int)));
                dt.Columns.Add(new DataColumn("Office_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("I_MilkQuantity", typeof(int)));
                dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
                dt.Columns.Add(new DataColumn("V_SealLocationAlias", typeof(string)));
                dt.Columns.Add(new DataColumn("D_FAT", typeof(decimal)));
                dt.Columns.Add(new DataColumn("D_SNF", typeof(decimal)));
                dt.Columns.Add(new DataColumn("D_CLR", typeof(decimal)));
                dt.Columns.Add(new DataColumn("V_Temp", typeof(string)));
                dt.Columns.Add(new DataColumn("V_Acidity", typeof(string)));
                dt.Columns.Add(new DataColumn("V_COB", typeof(string)));
                dt.Columns.Add(new DataColumn("V_Alcohol", typeof(string)));
                dt.Columns.Add(new DataColumn("V_MBRT", typeof(string)));
                dt.Columns.Add(new DataColumn("V_MilkQuality", typeof(string)));

                dr = dt.NewRow();
                dr[0] = 1;
                dr[1] = ddlCC.SelectedValue;
                dr[2] = ddlCC.SelectedItem.Text;
                dr[3] = txtMilkQuantity.Text;
                dr[4] = ddlCompartmentType.SelectedValue;
                dr[5] = ddlCompartmentType.SelectedItem.Text;
                dr[6] = txtFat.Text;
                dr[7] = txtSNF.Text;
                dr[8] = txtCLR.Text;
                dr[9] = txtTemprature.Text;
                dr[10] = txtAcidity.Text;
                dr[11] = ddlCOB.SelectedValue;
                dr[12] = alcoholePer; //ddlAlcohol.SelectedValue == "" ? "0" : ddlAlcohol.SelectedValue;
                dr[13] = txtMBRT.Text == "" ? "0" : txtMBRT.Text;
                dr[14] = ddlMilkQuality.SelectedValue;

                dt.Rows.Add(dr);
                ViewState["InsertRecord1"] = dt;
                gv_MilkQualityDetail.DataSource = dt;
                gv_MilkQualityDetail.DataBind();

                //ddlCompartmentTypeforQuality.DataSource = dt;
                //ddlCompartmentTypeforQuality.DataTextField = "V_SealLocationAlias";
                //ddlCompartmentTypeforQuality.DataValueField = "V_SealLocation";
                //ddlCompartmentTypeforQuality.DataBind();
                //ddlCompartmentTypeforQuality.Items.Insert(0, new ListItem("Select", "0"));


                //Adulteration Test details
                //dv_CompartmentTypeforAdulteration.Visible = true;
                //dv_AdulterationType.Visible = true;
                //dv_AdulterationValue.Visible = true;
                //dv_btnAdulterationTest.Visible = true;
                //dv_AdulterationHeaderSection.Visible = true;
                //dv_AdulterationTestGridVIew.Visible = true;




            }
            else
            {
                DataTable dt = new DataTable();
                DataTable DT = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("I_OfficeID", typeof(int)));
                dt.Columns.Add(new DataColumn("Office_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("I_MilkQuantity", typeof(int)));
                dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
                dt.Columns.Add(new DataColumn("V_SealLocationAlias", typeof(string)));
                dt.Columns.Add(new DataColumn("D_FAT", typeof(decimal)));
                dt.Columns.Add(new DataColumn("D_SNF", typeof(decimal)));
                dt.Columns.Add(new DataColumn("D_CLR", typeof(decimal)));
                dt.Columns.Add(new DataColumn("V_Temp", typeof(string)));
                dt.Columns.Add(new DataColumn("V_Acidity", typeof(string)));
                dt.Columns.Add(new DataColumn("V_COB", typeof(string)));
                dt.Columns.Add(new DataColumn("V_Alcohol", typeof(string)));
                dt.Columns.Add(new DataColumn("V_MBRT", typeof(string)));
                dt.Columns.Add(new DataColumn("V_MilkQuality", typeof(string)));

                DT = (DataTable)ViewState["InsertRecord1"];
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (ddlCompartmentType.SelectedValue == DT.Rows[i]["V_SealLocation"].ToString())
                    {
                        CompartmentType = 1;
                    }
                }
                if (CompartmentType == 1)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Compartment of \"" + ddlCompartmentType.SelectedItem.Text + "\" already exist.");
                }
                else
                {
                    dr = dt.NewRow();
                    dr[0] = dt.Rows.Count + 1;
                    dr[1] = ddlCC.SelectedValue;
                    dr[2] = ddlCC.SelectedItem.Text;
                    dr[3] = txtMilkQuantity.Text;
                    dr[4] = ddlCompartmentType.SelectedValue;
                    dr[5] = ddlCompartmentType.SelectedItem.Text;
                    dr[6] = txtFat.Text;
                    dr[7] = txtSNF.Text;
                    dr[8] = txtCLR.Text;
                    dr[9] = txtTemprature.Text;
                    dr[10] = txtAcidity.Text;
                    dr[11] = ddlCOB.SelectedValue;
                    dr[12] = alcoholePer; //ddlAlcohol.SelectedValue == "" ? "0" : ddlAlcohol.SelectedValue;
                    dr[13] = txtMBRT.Text == "" ? "0" : txtMBRT.Text;
                    dr[14] = ddlMilkQuality.SelectedValue;
                    dt.Rows.Add(dr);
                }

                foreach (DataRow tr in DT.Rows)
                {
                    dt.Rows.Add(tr.ItemArray);
                }
                ViewState["InsertRecord1"] = dt;
                gv_MilkQualityDetail.DataSource = dt;
                gv_MilkQualityDetail.DataBind();

                //Adelteration Test Details
                //ddlCompartmentTypeforQuality.DataSource = dt;
                //ddlCompartmentTypeforQuality.DataTextField = "V_SealLocationAlias";
                //ddlCompartmentTypeforQuality.DataValueField = "V_SealLocation";
                //ddlCompartmentTypeforQuality.DataBind();
                //ddlCompartmentTypeforQuality.Items.Insert(0, new ListItem("Select", "0"));

                //dv_CompartmentTypeforAdulteration.Visible = true;
                ///dv_AdulterationType.Visible = true;
                //dv_AdulterationValue.Visible = true;
                //dv_btnAdulterationTest.Visible = true;
                //dv_AdulterationHeaderSection.Visible = true;
                //dv_AdulterationTestGridVIew.Visible = true;


            }

            BindAdulterationTestGrid();


            //Clear Record
            txtAlcoholperc.Text = "";
            ddlAlcohol.ClearSelection();
            DivAlcoholper.Visible = false;
            ddlCC.ClearSelection();
            txtMilkQuantity.Text = "";
            ddlCompartmentType.ClearSelection();
            ddlMilkQuality.ClearSelection();
            txtFat.Text = "";
            txtSNF.Text = "";
            txtCLR.Text = "";
            txtTemprature.Text = "";
            txtAcidity.Text = "";
            ddlCOB.ClearSelection();
            txtMBRT.Text = "";
            txtMilkQuantity.Text = "";


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }

    protected void lnkDeleteQD_Click(object sender, EventArgs e)
    {
        try
        {

            lblMsg.Text = "";
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                DataTable dt = ViewState["InsertRecord1"] as DataTable;





                dt.Rows.Remove(dt.Rows[row.RowIndex]);
                ViewState["InsertRecord1"] = dt;
                gv_MilkQualityDetail.DataSource = dt;
                gv_MilkQualityDetail.DataBind();





                gvmilkAdulterationtestdetail.DataSource = null;
                gvmilkAdulterationtestdetail.DataBind();

                if (((DataTable)ViewState["InsertRecord1"]).Select("V_SealLocation = 'F'").Length > 0 && ((DataTable)ViewState["InsertRecord1"]).Select("V_SealLocation = 'R'").Length > 0)
                {
                    btnAddQualityDetails.Enabled = false;
                }
                else
                {
                    btnAddQualityDetails.Enabled = true;
                }

                BindAdulterationTestGrid();

                //For clear record for add child record
                txtMilkQuantity.Text = "";
                ddlCompartmentType.ClearSelection();
                txtFat.Text = "";
                txtSNF.Text = "";
                txtCLR.Text = "";
                txtTemprature.Text = "";
                txtAcidity.Text = "";
                ddlCOB.ClearSelection();
                txtMBRT.Text = "";
                txtMilkQuantity.Text = "";
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ddlCC.ClearSelection();

                txtAlcoholperc.Text = "";
                ddlAlcohol.ClearSelection();
                DivAlcoholper.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
        }
    }
}
