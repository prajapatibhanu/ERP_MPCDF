using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_MilkCollection_MilkInwardOutwardReferenceDetails : System.Web.UI.Page
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

            if (Convert.ToString(ViewState["InsertRecord1"]) == null || Convert.ToString(ViewState["InsertRecord1"]) == "")
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
                dt1.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
                dt1.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));
                dt1.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));
                dt1.Columns.Add(new DataColumn("V_SealColor", typeof(string)));

                DT1 = (DataTable)ViewState["InsertRecord1"];

                if (DT1.Rows.Count > 1)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Only 2 Seal Allow in One Tanker");
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
                ViewState["InsertRecord1"] = dt1;
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
            DataTable dt2 = ViewState["InsertRecord1"] as DataTable;
            dt2.Rows.Remove(dt2.Rows[row.RowIndex]);
            ViewState["InsertRecord1"] = dt2;
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
            lblMsg.Text = "";
            GridViewRow row1 = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt3 = ViewState["InsertRecord"] as DataTable;
            dt3.Rows.Remove(dt3.Rows[row1.RowIndex]);
            ViewState["InsertRecord"] = dt3;
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
                                                txtNV_DriverDrivingLicenceNo.Text
                                                 
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

}