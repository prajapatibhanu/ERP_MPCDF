using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;
using System.IO;
using System.Web.Script.Services;
using System.Web.Services;

public partial class mis_RMRD_ReceiveTankerAtSecurity_RMRD : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    static DataSet ds5;
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
                 //GetOfficeType();
                FillBMCRoot();
                GetViewReceivedTankerDetails();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                GetDriverName();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    
    #region=======================user defined function========================
    protected void GetTankerDetail()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("Usp_TankerDetail",
                     new string[] { "flag", "I_OfficeID", "MilkCollectionFrom", "I_OfficeTypeID" },
                     new string[] { "11", objdb.Office_ID(), "BMC", objdb.OfficeType_ID() }, "dataset");

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
    //private void AddMilkCollDetails()
    //{
    //    try
    //    {

    //        int CompartmentType = 0;

    //        if (Convert.ToString(ViewState["InsertRecord"]) == null || Convert.ToString(ViewState["InsertRecord"]) == "")
    //        {
    //            DataTable dt = new DataTable();
    //            DataRow dr;
    //            dt.Columns.Add(new DataColumn("S.No", typeof(int)));
    //            dt.Columns.Add(new DataColumn("I_OfficeID", typeof(int)));
    //            dt.Columns.Add(new DataColumn("Office_Name", typeof(string)));
    //            dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
    //           // dt.Columns.Add(new DataColumn("V_SampleNo", typeof(string)));
    //            dt.Columns.Add(new DataColumn("V_SampleRemark", typeof(string)));

    //            dr = dt.NewRow();
    //            dr[0] = 1;
    //            dr[1] = ddlOfficeName.SelectedValue;
    //            dr[2] = ddlOfficeName.SelectedItem.Text;
    //            dr[3] = ddlChamberOfSampleNo.SelectedItem.Text;
    //            //dr[4] = txtSampleNo.Text;
    //            dr[4] = txtSampleRemark.Text;
    //            dt.Rows.Add(dr);

    //            ViewState["InsertRecord"] = dt;
    //            gv_MilkCollDetails.DataSource = dt;
    //            gv_MilkCollDetails.DataBind();

    //        }
    //        else
    //        {
    //            DataTable dt = new DataTable();
    //            DataTable DT = new DataTable();
    //            DataRow dr;
    //            dt.Columns.Add(new DataColumn("S.No", typeof(int)));
    //            dt.Columns.Add(new DataColumn("I_OfficeID", typeof(int)));
    //            dt.Columns.Add(new DataColumn("Office_Name", typeof(string)));
    //            dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
    //            //dt.Columns.Add(new DataColumn("V_SampleNo", typeof(string)));
    //            dt.Columns.Add(new DataColumn("V_SampleRemark", typeof(string)));
    //            DT = (DataTable)ViewState["InsertRecord"];


    //            for (int i = 0; i < DT.Rows.Count; i++)
    //            {
    //                if (ddlOfficeName.SelectedValue == DT.Rows[i]["I_OfficeID"].ToString())
    //                {
    //                    CompartmentType = 1;
    //                }

    //                //if (txtSampleNo.Text == DT.Rows[i]["V_SampleNo"].ToString())
    //                //{
    //                //    CompartmentType = 2;
    //                //}
    //            }

    //            if (CompartmentType == 1)
    //            {
    //                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Milk Collection from  \"" + ddlOfficeName.SelectedItem.Text + "\" already exist.");
    //            }

    //            //else if (CompartmentType == 2)
    //            //{
    //            //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Sample no. \"" + txtSampleNo.Text + "\" already exist.");
    //            //}
    //            else
    //            {
    //                dr = dt.NewRow();
    //                dr[0] = 1;
    //                dr[1] = ddlOfficeName.SelectedValue;
    //                dr[2] = ddlOfficeName.SelectedItem.Text;

    //                dr[3] = ddlChamberOfSampleNo.SelectedItem.Text;
    //                //dr[4] = txtSampleNo.Text;
    //                dr[4] = txtSampleRemark.Text;
    //                dt.Rows.Add(dr);
    //            }

    //            foreach (DataRow tr in DT.Rows)
    //            {
    //                dt.Rows.Add(tr.ItemArray);
    //            }
    //            ViewState["InsertRecord"] = dt;
    //            gv_MilkCollDetails.DataSource = dt;
    //            gv_MilkCollDetails.DataBind();
    //        }

    //        //Clear Record

    //        ddlOfficeName.ClearSelection();
    //        //txtSampleNo.Text = "";
    //        txtSampleRemark.Text = "";

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
    //    }
    //}
    //private void GetOfficeType()
    //{
    //    try
    //    {
    //        ddlOfficeType.DataSource = objddl.OfficeTypeFill();
    //        ddlOfficeType.DataTextField = "OfficeType_Title";
    //        ddlOfficeType.DataValueField = "OfficeType_ID";
    //        ddlOfficeType.DataBind();

    //        if (objdb.OfficeType_ID() == objdb.GetHOType_Id().ToString())
    //        {
    //            for (int i = ddlOfficeType.Items.Count - 1; i >= 0; i--)
    //            {
    //                if (ddlOfficeType.Items[i].Value != objdb.GetDSType_Id().ToString())// for show only dugdh sangh
    //                {
    //                    ddlOfficeType.Items.RemoveAt(i);
    //                }
    //            }
    //        }
    //        else if (objdb.OfficeType_ID() == objdb.GetDSType_Id().ToString())
    //        {
    //            for (int i = ddlOfficeType.Items.Count - 1; i >= 0; i--)
    //            {
    //                if (ddlOfficeType.Items[i].Value == objdb.GetHOType_Id().ToString() || ddlOfficeType.Items[i].Value == objdb.GetDSType_Id().ToString())// for show only dugdh sangh
    //                {
    //                    ddlOfficeType.Items.RemoveAt(i);
    //                }
    //            }
    //        }
    //        ddlOfficeType.Items.Insert(0, new ListItem("Select", "0"));


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
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
                            new string[] { "1", "BMC", objdb.Office_ID() }, "dataset");
            gv_TankerDetails.DataBind();
        }
        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }
    }
    protected void ddlSealColor_Init(object sender, EventArgs e)
    {
        try
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
                ddlSealColor.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlSealColor.DataSource = string.Empty;
                ddlSealColor.DataBind();
                ddlSealColor.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    private DataTable GetMilkCollGridvalue()
    {

        DataTable dtMilkColl = new DataTable();
        DataRow drMilkColl;


        dtMilkColl.Columns.Add(new DataColumn("I_OfficeID", typeof(int)));      
        dtMilkColl.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
        dtMilkColl.Columns.Add(new DataColumn("V_SampleRemark", typeof(string)));
        foreach (GridViewRow rowMilkColl in gv_BMCDetails.Rows)
        {

            Label lblI_OfficeID = (Label)rowMilkColl.FindControl("lblI_OfficeID");
            DropDownList lblSLocation = (DropDownList)rowMilkColl.FindControl("ddlLocation");
           // Label lblSampleNo = (Label)rowMilkColl.FindControl("lblSampleNo");
            TextBox lblSampleRemark = (TextBox)rowMilkColl.FindControl("txtRemark");

            drMilkColl = dtMilkColl.NewRow();

            drMilkColl[0] = lblI_OfficeID.Text;
           // drMilkColl[1] = lblSampleNo.Text;
            drMilkColl[1] = lblSLocation.SelectedItem.Text;
            drMilkColl[2] = lblSampleRemark.Text;
            dtMilkColl.Rows.Add(drMilkColl);
        }
        return dtMilkColl;

    }
    public string RMRDSeal_Validation()
    {

        string Status = "0";
        DataTable dt1 = new DataTable();
        DataTable DT1 = new DataTable();
        DataRow dr1;
        dt1.Columns.Add(new DataColumn("S.No", typeof(int)));
        dt1.Columns.Add(new DataColumn("ChamberType", typeof(string)));
        dt1.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));
        dt1.Columns.Add(new DataColumn("V_SealColor", typeof(string)));
        dt1.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
        dt1.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));

        DT1 = (DataTable)ViewState["InsertRecord1"];


        int VBCount = 0;
        int SCount = 0;
        int DCount = 0;

        for (int i = 0; i < DT1.Rows.Count; i++)
        {

            if (DT1.Rows[i]["ChamberType"].ToString() == "Single")
            {
                SCount += 1;
            }

            if (DT1.Rows[i]["ChamberType"].ToString() == "Front")
            {
                DCount += 1;
            }

            if (DT1.Rows[i]["ChamberType"].ToString() == "Rear")
            {
                DCount += 1;
            }

            if (DT1.Rows[i]["ChamberType"].ToString() == "ValveBox")
            {
                VBCount += 1;
            }



        }
        if (txtV_VehicleType.Text == "Single Compartment")
        {
            if (SCount == 0)
            {
                Status = "1";
            }
            if (VBCount < 1)
            {
                Status = "2";
            }
            if (SCount == 0 && VBCount < 2)
            {
                Status = "3";
            }

        }
        else
        {
            if (DCount < 2)
            {
                Status = "4";
            }
            if (VBCount < 1)
            {
                Status = "5";
            }
            if (DCount < 1 && VBCount < 2)
            {
                Status = "6";
            }
        }

        return Status;

    }
    private void AddSealDetails()
    {
        try
        {
            int CompartmentType = 0;

            if (Convert.ToString(ViewState["InsertRecord1"]) == null || Convert.ToString(ViewState["InsertRecord1"]) == "")
            {
                DataTable dt1 = new DataTable();
                DataRow dr1;
                dt1.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt1.Columns.Add(new DataColumn("ChamberType", typeof(string)));
                dt1.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));
                dt1.Columns.Add(new DataColumn("V_SealColor", typeof(string)));
                dt1.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
                dt1.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));

                dr1 = dt1.NewRow();
                dr1[0] = 1;
                dr1[1] = ddlChamberType.SelectedValue;
                dr1[2] = ddlSealColor.SelectedValue;
                dr1[3] = ddlSealColor.SelectedItem;
                dr1[4] = txtV_SealNo.Text;
                dr1[5] = txtV_SealRemark.Text;
                dt1.Rows.Add(dr1);

                ViewState["InsertRecord1"] = dt1;
                gv_SealInfo.DataSource = dt1;
                gv_SealInfo.DataBind();
            }
            else
            {
                DataTable dt1 = new DataTable();
                DataTable DT1 = new DataTable();
                DataRow dr1;
                dt1.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt1.Columns.Add(new DataColumn("ChamberType", typeof(string)));
                dt1.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));
                dt1.Columns.Add(new DataColumn("V_SealColor", typeof(string)));
                dt1.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
                dt1.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));

                DT1 = (DataTable)ViewState["InsertRecord1"];

                //if (DT1.Rows.Count > 1)
                //{
                //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Only 2 Seal Allow in One Tanker");
                //    return;
                //}

                int VBCount = 0;
                int SCount = 0;
                int DCount = 0;

                for (int i = 0; i < DT1.Rows.Count; i++)
                {
                    if (ddlChamberType.SelectedValue == "Single")
                    {
                        if (DT1.Rows[i]["ChamberType"].ToString() == "Single")
                        {
                            SCount += 1;
                        }
                    }
                    if (ddlChamberType.SelectedValue == "Front")
                    {
                        if (DT1.Rows[i]["ChamberType"].ToString() == "Front")
                        {
                            DCount += 1;
                        }
                    }
                    if (ddlChamberType.SelectedValue == "Rear")
                    {
                        if (DT1.Rows[i]["ChamberType"].ToString() == "Rear")
                        {
                            DCount += 1;
                        }
                    }
                    if (ddlChamberType.SelectedValue == "ValveBox")
                    {
                        if (DT1.Rows[i]["ChamberType"].ToString() == "ValveBox")
                        {
                            VBCount += 1;
                        }
                    }

                    if (txtV_SealNo.Text == DT1.Rows[i]["V_SealNo"].ToString())
                    {
                        CompartmentType = 1;
                    }

                }
                if (SCount == 10)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Maximum 10 Seal required for chamber.");
                    return;
                }
                if (DCount == 10)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Maximum 10 Seal required for chamber.");
                    return;
                }
                if (VBCount == 2)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Maximum 2 Seal required for Valve Box.");
                    return;
                }
                if (CompartmentType == 1)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Seal of \"" + txtV_SealNo.Text + "\" already exist.");
                    return;
                }
                else
                {
                    dr1 = dt1.NewRow();
                    dr1[0] = 1;
                    dr1[1] = ddlChamberType.SelectedValue;
                    dr1[2] = ddlSealColor.SelectedValue;
                    dr1[3] = ddlSealColor.SelectedItem;
                    dr1[4] = txtV_SealNo.Text;
                    dr1[5] = txtV_SealRemark.Text;
                    dt1.Rows.Add(dr1);
                }

                foreach (DataRow tr in DT1.Rows)
                {
                    dt1.Rows.Add(tr.ItemArray);
                }
                ViewState["InsertRecord1"] = dt1;
                gv_SealInfo.DataSource = dt1;
                gv_SealInfo.DataBind();
            }


            //Clear Record

            ddlChamberType.SelectedIndex = -1;
            ddlSealColor.SelectedIndex = -1;
            txtV_SealNo.Text = "";
            txtV_SealRemark.Text = "";

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    private DataTable GetSealGridvalue()
    {

        DataTable dtsealF = new DataTable();
        DataRow drseal;

        dtsealF.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
        dtsealF.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
        dtsealF.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));
        dtsealF.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));
        foreach (GridViewRow rowseal in gv_SealInfo.Rows)
        {

            Label lblChamberType = (Label)rowseal.FindControl("lblChamberType");
            Label lblTI_SealColor = (Label)rowseal.FindControl("lblTI_SealColor");
            Label lblV_SealNo = (Label)rowseal.FindControl("lblV_SealNo");
            Label lblV_SealRemark = (Label)rowseal.FindControl("lblV_SealRemark");

            drseal = dtsealF.NewRow();
            drseal[0] = lblV_SealNo.Text;
            drseal[1] = lblChamberType.Text;
            drseal[2] = lblV_SealRemark.Text;
            drseal[3] = lblTI_SealColor.Text;



            dtsealF.Rows.Add(drseal);
        }
        return dtsealF;

    }
    protected void GetViewReceivedTankerDetails()
    {
        try
        {
            ds = null;
            string date = "";

            if (txtDate.Text != "")
            {
                date = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_RMRD",
                     new string[] { "flag", "I_OfficeID", "D_Date" },
                     new string[] { "10", objdb.Office_ID(), date }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv_TodayReceivedTankerDetailsRMRD.DataSource = ds;
                        gv_TodayReceivedTankerDetailsRMRD.DataBind();

                    }
                    else
                    {
                        gv_TodayReceivedTankerDetailsRMRD.DataSource = null;
                        gv_TodayReceivedTankerDetailsRMRD.DataBind();
                    }
                }
                else
                {
                    gv_TodayReceivedTankerDetailsRMRD.DataSource = null;
                    gv_TodayReceivedTankerDetailsRMRD.DataBind();
                }
            }
            else
            {
                gv_TodayReceivedTankerDetailsRMRD.DataSource = null;
                gv_TodayReceivedTankerDetailsRMRD.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void GetDriverName()
    {
        try
        {
            lblMsg.Text = "";
            DataSet ds1 = objdb.ByProcedure("Usp_Mst_Driver", new string[] { "flag", "Office_ID" }, new string[] { "4", objdb.Office_ID() }, "dataset");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                ds5 = ds1;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }

    }
    public void FillBMCRoot()
    {

        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
                      new string[] { "flag", "OfficeType_ID", "Office_ID" },
                      new string[] { "6", objdb.OfficeType_ID(), objdb.Office_ID() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlBMCTankerRootName.DataTextField = "BMCTankerRootName";
                ddlBMCTankerRootName.DataValueField = "BMCTankerRoot_Id";
                ddlBMCTankerRootName.DataSource = ds;
                ddlBMCTankerRootName.DataBind();
                //ddlBMCTankerRootName.Items.Insert(0, "Select");
                ddlBMCTankerRootName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }

    }
    #endregion

    #region=============== changed event for controls =================
    protected void ddlTankerDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
            ddlBMCTankerRootName.ClearSelection();
            gv_BMCDetails.DataSource = string.Empty;
            gv_BMCDetails.DataBind();
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
                            ViewState["InsertRecord1"] = "";
                            divsealdetail.Visible = true;

                            ddlChamberType.Items.Clear();
                            ddlChamberType.Items.Insert(0, new ListItem("Select", "0"));
                            ddlChamberType.Items.Insert(1, new ListItem("Single", "Single"));
                            ddlChamberType.Items.Insert(2, new ListItem("ValveBox", "ValveBox"));
                            //ddlChamberOfSampleNo.Items.Clear();
                            //ddlChamberOfSampleNo.Items.Insert(0, new ListItem("S", "0"));


                        }
                        if (strvtype == "D")
                        {
                            txtV_VehicleType.Text = "Dual Compartment";
                            ViewState["InsertRecord1"] = "";
                            divsealdetail.Visible = true;
                            ddlChamberType.Items.Clear();
                            ddlChamberType.Items.Insert(0, new ListItem("Select", "0"));
                            ddlChamberType.Items.Insert(1, new ListItem("Front", "Front"));
                            ddlChamberType.Items.Insert(2, new ListItem("Rear", "Rear"));
                            ddlChamberType.Items.Insert(3, new ListItem("ValveBox", "ValveBox"));
                            //ddlChamberOfSampleNo.Items.Clear();
                            //ddlChamberOfSampleNo.Items.Insert(0, new ListItem("F", "0"));
                            //ddlChamberOfSampleNo.Items.Insert(1, new ListItem("R", "1"));

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

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void txtD_GrossWeight_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlTankerDetail.SelectedIndex != 0)
            {

                //if (Convert.ToDecimal(txtD_GrossWeight.Text) < Convert.ToDecimal(txtD_VehicleCapacity.Text))
                //{
                //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Entered Gross Weight should be less than Tanker Capacity]");
                //    txtD_GrossWeight.Text = "";
                //}
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Plase Select Tanker No. First!");
                txtD_GrossWeight.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
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
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        GetViewReceivedTankerDetails();
    }
    protected void ddlBMCTankerRootName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            if (ddlBMCTankerRootName.SelectedIndex > 0)
            {

                ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
                           new string[] { "flag", "BMCTankerRoot_Id", "Office_ID", "OfficeType_ID" },
                           new string[] { "5", ddlBMCTankerRootName.SelectedValue, objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    gv_BMCDetails.DataSource = ds;
                    gv_BMCDetails.DataBind();
                    foreach (GridViewRow rows in gv_BMCDetails.Rows)
                    {
                        string strvtype = txtV_VehicleType.Text;
                        DropDownList Location = (DropDownList)rows.FindControl("ddlLocation");
                        if (strvtype == "Single Compartment")
                        {
                            Location.Items.Clear();
                            Location.Items.Insert(0, new ListItem("S", "0"));
                        }
                        if (strvtype == "Dual Compartment")
                        {

                            Location.Items.Clear();
                            Location.Items.Insert(0, new ListItem("F", "0"));
                            Location.Items.Insert(1, new ListItem("R", "1"));

                        }

                    }

                }
                else
                {
                    gv_BMCDetails.DataSource = null;
                    gv_BMCDetails.DataBind();

                }

            }
            else
            {
                gv_BMCDetails.DataSource = null;
                gv_BMCDetails.DataBind();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    //protected void ddlOfficeType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ddlOfficeName.Items.Clear();
    //        ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_RMRD", new string[] { "flag", "OfficeType_ID", "Office_Parant_ID" }, new string[] { "1", ddlOfficeType.SelectedValue, objdb.Office_ID() }, "dataset");
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlOfficeName.DataSource = ds;
    //            ddlOfficeName.DataTextField = "Office_Name";
    //            ddlOfficeName.DataValueField = "Office_ID";
    //            ddlOfficeName.DataBind();

    //        }
    //        ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
    //    }
    //}
    #endregion


    #region=============== button click event =================
    protected void lblAddTanker_Click(object sender, EventArgs e)
    {
        GetData();
        Clear();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
    }
    //protected void BtnAddSample_Click(object sender, EventArgs e)
    //{
    //    lblMsg.Text = "";
    //    ddlChamberOfSampleNo.Focus();
    //}
    //protected void btnAddMilkColl_Click(object sender, EventArgs e)
    //{
    //    lblMsg.Text = "";
    //    AddMilkCollDetails();
    //}
    //protected void lnkDeleteMilkColl_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        GridViewRow row1 = (sender as LinkButton).NamingContainer as GridViewRow;
    //        DataTable dt3 = ViewState["InsertRecord"] as DataTable;
    //        dt3.Rows.Remove(dt3.Rows[row1.RowIndex]);
    //        ViewState["InsertRecord"] = dt3;
    //        gv_MilkCollDetails.DataSource = dt3;
    //        gv_MilkCollDetails.DataBind();
    //        ddlOfficeName.ClearSelection();

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
    //    }
    //}
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {

            lblMsg.Text = "";
            string Uploadedfilename = "";

            if (FuSealVerificationfile.HasFile)
            {
                decimal size = ((decimal)FuSealVerificationfile.PostedFile.ContentLength / (decimal)100);
                string Fileext = Path.GetExtension(FuSealVerificationfile.FileName).ToLower();
                if (Fileext == ".jpg" || Fileext == ".jpeg" || Fileext == ".doc" || Fileext == ".docx" || Fileext == ".pdf" || Fileext == ".zip" || Fileext == ".rar")
                {
                    if (size <= 5000)
                    {
                        Uploadedfilename = string.Concat("~/mis/MilkCollection/SealVerificationdoc/", Guid.NewGuid(), FuSealVerificationfile.FileName);
                        FuSealVerificationfile.SaveAs(Server.MapPath(Uploadedfilename));
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Thank You!", "Opps! :" + "FIle size should be less then 5 MP");
                        return;
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Thank You!", "Opps! :" + "Allow File Formate only (.jpg,.jpeg,.doc,.docx,.pdf,.zip,.rar");
                    return;
                }
            }


            DataTable dtMilkColl = new DataTable();
            dtMilkColl = GetMilkCollGridvalue();

            if (dtMilkColl.Rows.Count > 0)
            {
                DataTable dtsealF = new DataTable();
                dtsealF = GetSealGridvalue();

                if (dtsealF != null && dtsealF.Rows.Count > 0)
                {
                    string Shift = "";
                    DataSet dsct = objdb.ByProcedure("USP_GetServerDatetime",
                             new string[] { "flag" },
                             new string[] { "1" }, "dataset");

                    string currrentime = dsct.Tables[0].Rows[0]["currentTime"].ToString();

                    string[] s = currrentime.Split(':');

                    if ((Convert.ToInt32(s[0]) >= 0 && Convert.ToInt32(s[0]) <= 12 && ((Convert.ToInt32(s[0]) == 0 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 12 && Convert.ToInt32(s[1]) <= 59))))
                    {
                        Shift = "Morning";
                    }
                    else
                    {
                        Shift = "Evening";
                    }
                    ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_RMRD",
                                       new string[] { "flag"
                                                          ,"I_TankerID"
                                                          ,"I_OfficeID"
                                                          ,"I_OfficeTypeID"
                                                          ,"V_EntryType"
                                                          ,"V_DriverName"
                                                          ,"V_DriverMobileNo"
                                                          ,"V_EntryFrom"
                                                          ,"V_Latitude"
                                                          ,"V_Longitude"
                                                          ,"D_GrossWeight"                                                        
                                                          ,"I_CreatedBy"
                                                          ,"V_IPAddress"
                                                          ,"V_MacAddress"
                                                          ,"SealVerificationDocument"
                                                          ,"NV_DriverDrivingLicenceNo"
                                                          ,"WeightReceiptNo"
                                                          ,"Shift"

                                    },
                                       new string[] { "2" ,
                                                ddlTankerDetail.SelectedValue,
                                                objdb.Office_ID(),
                                                objdb.OfficeType_ID(), 
                                                "In",  
                                                txtV_DriverName.Text.TrimEnd().TrimStart(),
                                                txtV_DriverMobileNo.Text,
                                                "Web",
                                                "22.888",
                                                "22.888",
                                                txtD_GrossWeight.Text,
                                                objdb.createdBy(),
                                                objdb.GetLocalIPAddress(),
                                                objdb.GetMACAddress(),
                                                Uploadedfilename,
                                                txtNV_DriverDrivingLicenceNo.Text,                                           
                                                txtReceiptNo.Text
                                                ,Shift
                                                 
                                    },
                                      new string[] { 
                                              "type_Trn_MilkCollectionDetails_RMRD", 
                                              "type_Trn_TankerValveSealDetails_RMRD"
                                          },
                                      new DataTable[] {
                                              dtMilkColl,
                                              dtsealF 
                                          },
                                      "TableSave");


                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        Session["IsSuccess"] = true;
                        Response.Redirect("ReceiveTankerAtSecurity_RMRD.aspx", false);

                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        Session["IsSuccess"] = false;
                    }

                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    string strtR = RMRDSeal_Validation();
                    //if(strtR == "0")
                    //{

                    //}
                    //else
                    //{
                    //    if(strtR == "1")
                    //    {
                    //        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "At Least One Tanker Seal Details required for Single Compartment");
                    //    }
                    //    else if (strtR == "2")
                    //    {
                    //        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "At Least One Tanker ValveBox Details required for Single Compartment");
                    //    }
                    //    else if (strtR == "3")
                    //    {
                    //        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "At Least One Tanker Seal  and One Valve Box Details required for Single Compartment");
                    //    }
                    //    else if (strtR == "4")
                    //    {
                    //        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "At Least Two Tanker Seal Details required for Dual Compartment");
                    //    }
                    //    else if (strtR == "5")
                    //    {
                    //        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "At Least One Tanker ValveBox Details required for Dual Compartment");
                    //    }
                    //    else if (strtR == "6")
                    //    {
                    //        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "At Least Two Tanker Seal  and One Valve Box  Details required for Dual Compartment");
                    //    }
                    //}

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "First To Fill Tanker Seal Details");
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "First To Fill At Least One Milk Collection Centre Details");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void btnYesT_Click(object sender, EventArgs e)
    {
        try
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

        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
        }
    }
    protected void btnTankerSealDetails_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        AddSealDetails();
        //btnSubmit.Enabled = true;

    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt2 = ViewState["InsertRecord1"] as DataTable;
            dt2.Rows.Remove(dt2.Rows[row.RowIndex]);
            ViewState["InsertRecord1"] = dt2;
            gv_SealInfo.DataSource = dt2;
            gv_SealInfo.DataBind();


            ddlChamberType.SelectedIndex = -1;
            ddlSealColor.SelectedIndex = -1;
            txtV_SealNo.Text = "";
            txtV_SealRemark.Text = "";

            //DataTable dtdeletecc = ViewState["InsertRecord1"] as DataTable;

            //if (dtdeletecc.Rows.Count == 0)
            //{
            //    btnSubmit.Enabled = false;
            //}

            // MilkDispatchDetail();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    #endregion

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<string> SearchDrivers(string Driver_Name)
    {
        List<string> Drivers = new List<string>();
        try
        {
            DataView dv = new DataView();
            dv = ds5.Tables[0].DefaultView;
            dv.RowFilter = "Driver_Name like '%" + Driver_Name + "%'";
            DataTable dt = dv.ToTable();

            foreach (DataRow rs in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    Drivers.Add(rs[col].ToString());
                }
            }

        }
        catch { }
        return Drivers;
    }
  
    [WebMethod]
    public static Array FillDriverDetail(string DriverName)
    {
        string [] Value = new string[2];
        APIProcedure objdb = new APIProcedure();
        StringBuilder html = new StringBuilder();
        DataSet ds = objdb.ByProcedure("Usp_Mst_Driver", new string[] { "flag", "Driver_Name", "Office_ID" }, new string[] { "5", DriverName, objdb.Office_ID() }, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
           Value[0] = ds.Tables[0].Rows[0]["Driver_MobileNo"].ToString();
           Value[1] = ds.Tables[0].Rows[0]["Driver_LicenceNo"].ToString();
        }
        return Value;
    }

    
}
