using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_MilkCollection_DirectProducerCollection : System.Web.UI.Page
{
    DataSet ds;
    decimal totalValue = 0, Totalmilkqty = 0;
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    IFormatProvider culture = new CultureInfo("en-US", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetOfficeTypeName();
                getGV_MilkCollection();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                txtDate.Text = objdb.getdate();
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

    #region User Defined Function

    private void GetShift()
    {
        try
        {
            ddlshift.DataSource = objddl.ShiftFill().Tables[0];
            ddlshift.DataTextField = "ShiftName";
            ddlshift.DataValueField = "PUShift_id";
            ddlshift.DataBind();
            ddlshift.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 01:" + ex.Message.ToString());
        }
    }
    private void getMemberCodeAndName()
    {
        try
        {
            ds = objdb.ByProcedure("SptblPUProducerReg",
                        new string[] { "flag", "Office_ID" },
                        new string[] { "1", objdb.Office_ID() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlMemberID.DataSource = ds.Tables[0];
                ddlMemberID.DataTextField = "MemberName";
                ddlMemberID.DataValueField = "ProducerId";
                ddlMemberID.DataBind();
                ddlMemberID.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlMemberID.DataSource = string.Empty;
                ddlMemberID.DataBind();
                ddlMemberID.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 02:" + ex.Message.ToString());
        }
    }
    private void getGV_MilkCollection()
    {
        try
        {
            if (ds != null) { ds.Clear(); }
            ds = objdb.ByProcedure("sp_tblPUDCSCollection",
                     new string[] { "flag", "Office_ID" },
                     new string[] { "2", objdb.Office_ID() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_MilkCollectionDetails.DataSource = ds;
                gv_MilkCollectionDetails.DataBind();
            }
            else
            {
                ds.Clear();
                gv_MilkCollectionDetails.DataSource = ds;
                gv_MilkCollectionDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 03:" + ex.Message.ToString());
        }
    }
    private int CheckShiftEntryDateWise()
    {
        int status = 0;
        try
        {
            if (ds != null) { ds.Clear(); }
            if (txtDate.Text != "" && ddlshift.SelectedIndex != 0)
            {
                DateTime CDate = Convert.ToDateTime(txtDate.Text, culture);
                ds = objdb.ByProcedure("sp_tblPUDCSCollection",
                            new string[] { "flag", "CollectionDate", "Shift_id", "ProducerId" },
                            new string[] { "8", CDate.ToString("yyyy/dd/MM"), ddlshift.SelectedValue, ddlMemberID.SelectedValue }, "dataset");
                if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() != "Ok")
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    ddlMemberID.ClearSelection();
                    status = 1;
                }
                else
                {
                    lblMsg.Text = "";
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", "First to Enter Date and Shift.");
                ddlMemberID.ClearSelection();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 30:" + ex.Message.ToString());
        }
        return status;
    }
    private void getMilkType()
    {
        try
        {
            ddlMilkType.DataSource = objddl.GetMilkType().Tables[0];
            ddlMilkType.DataTextField = "MilkTypeName";
            ddlMilkType.DataValueField = "MilkType_id";
            ddlMilkType.DataBind();
            ddlMilkType.Items.Insert(0, new ListItem("Select", "0"));
            ddlMilkType.Items.RemoveAt(3); //For Remove Mix
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 04:" + ex.Message.ToString());
        }
    }
    private void GetUnit()
    {
        try
        {
            ddlUnit.DataSource = objddl.UnitFill().Tables[0];
            ddlUnit.DataTextField = "UQCCode";
            ddlUnit.DataValueField = "Unit_id";
            ddlUnit.DataBind();

            for (int i = ddlUnit.Items.Count - 1; i >= 0; i--)
            {
                if (ddlUnit.Items[i].Value != objdb.GetLtrId().ToString())
                {
                    ddlUnit.Items.RemoveAt(i);
                }
            }
            ddlUnit.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 05:" + ex.Message.ToString());
        }
    }
    private void GetOfficeTypeName()
    {
        try
        {
            if (ds != null)
            {
                ds.Clear();
            }
            ds = objdb.ByProcedure("SpAdminOfficeType",
                    new string[] { "flag", "OfficeType_ID" },
                    new string[] { "1", objdb.UserTypeID() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblOfficeTypeName.Text = ds.Tables[0].Rows[0]["OfficeTypeName"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 06:" + ex.Message.ToString());
        }
    }
    private void GetDSOfficeDetails()
    {
        try
        {
            ddlDS.DataSource = objdb.ByProcedure("SpAdminOffice",
                    new string[] { "flag" },
                    new string[] { "1" }, "dataset");
            ddlDS.DataTextField = "Office_Name";
            ddlDS.DataValueField = "Office_ID";
            ddlDS.DataBind();
            //ddlDS.Items.Insert(0, new ListItem("Select", "0"));
            ddlDS.SelectedValue = objddl.GetOfficeParant_ID().Tables[0].Rows[0]["Office_Parant_ID"].ToString();
            ddlDS.Enabled = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 07:" + ex.Message.ToString());
        }
    }
    private void GetDCSOfficeDetails()
    {
        try
        {
            ddlDCS.DataSource = objdb.ByProcedure("SpAdminOffice",
                    new string[] { "flag" },
                    new string[] { "1" }, "dataset");
            ddlDCS.DataTextField = "Office_Name";
            ddlDCS.DataValueField = "Office_ID";
            ddlDCS.DataBind();
            //ddlDCS.Items.Insert(0, new ListItem("Select", "0"));
            ddlDCS.SelectedValue = objdb.Office_ID();
            ddlDCS.Enabled = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 08:" + ex.Message.ToString());
        }
    }
    private DataTable getMilkCollectionDetail()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("MilkType_id", typeof(string)));
        dt.Columns.Add(new DataColumn("Unit_id", typeof(string)));
        dt.Columns.Add(new DataColumn("MilkQty", typeof(string)));
        dt.Columns.Add(new DataColumn("FAT", typeof(decimal)));
        dt.Columns.Add(new DataColumn("SNF", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CLR", typeof(decimal)));
        dt.Columns.Add(new DataColumn("MilkCollectionRatePerKG", typeof(decimal)));
        dt.Columns.Add(new DataColumn("RatePerLtr", typeof(decimal)));
        dt.Columns.Add(new DataColumn("TotalValue", typeof(decimal)));

        foreach (GridViewRow row in gv_temp_CollectionDetail.Rows)
        {
            Label lblMilkType_id = (Label)row.FindControl("lblMilkType_id");
            Label lblUnit_id = (Label)row.FindControl("lblUnit_id");
            Label lblMilkQty = (Label)row.FindControl("lblMilkQty");
            Label lblFAT = (Label)row.FindControl("lblFAT");
            Label lblSNF = (Label)row.FindControl("lblSNF");
            Label lblCLR = (Label)row.FindControl("lblCLR");
            Label lblMilkCollectionRatePerKG = (Label)row.FindControl("lblMilkCollectionRatePerKG");
            Label lblRate = (Label)row.FindControl("lblRate");
            Label lblValue = (Label)row.FindControl("lblValue");

            dr = dt.NewRow();
            dr[0] = Convert.ToInt32(lblMilkType_id.Text);
            dr[1] = Convert.ToInt32(lblUnit_id.Text);
            dr[2] = Convert.ToDecimal(lblMilkQty.Text).ToString("0.00");
            dr[3] = Convert.ToDecimal(lblFAT.Text).ToString("0.00");
            dr[4] = Convert.ToDecimal(lblSNF.Text).ToString("0.00");
            dr[5] = Convert.ToDecimal(lblCLR.Text).ToString("0.00");
            dr[6] = Convert.ToDecimal(lblMilkCollectionRatePerKG.Text).ToString("0.00");
            dr[7] = Convert.ToDecimal(lblRate.Text).ToString("0.00");
            dr[8] = Convert.ToDecimal(lblValue.Text).ToString("0.00");
            dt.Rows.Add(dr);
        }
        return dt;
    }
    private decimal GetRate()
    {
        decimal rate = 0, fat = 1;
        try
        {
            //Formula for Get SNF and Rate/Ltr as given below:
            if (txtFat.Text != "")
            { fat = Convert.ToDecimal(txtFat.Text); }

            if (ddlMilkType.SelectedValue == "1") // For Cow
            {
                // Rate/Ltr = ((FAT + SNF) * RATE/KG) / 100 -- For Cow
                rate = ((fat + GetSNF()) * Convert.ToDecimal(objddl.GetMilkCollectionRatePerKG(ddlMilkType.SelectedValue, ddlDS.SelectedValue).Tables[0].Rows[0]["Rate"].ToString())) / 100;
            }
            else if (ddlMilkType.SelectedValue == "2") // For Buffelo
            {
                // Rate/Ltr = (FAT * RATE/KG) / 100 -- For Buffelo
                rate = (fat * Convert.ToDecimal(objddl.GetMilkCollectionRatePerKG(ddlMilkType.SelectedValue, ddlDS.SelectedValue).Tables[0].Rows[0]["Rate"].ToString())) / 100;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 09:" + ex.Message.ToString());
        }
        return rate;
    }
    private decimal GetSNF()
    {
        decimal snf = 0, clr = 0, fat = 1;
        try
        {
            //Formula for Get SNF as given below:
            if (txtCLRSNF.Text != "")
            { clr = Convert.ToDecimal(txtCLRSNF.Text); }
            if (txtFat.Text != "")
            { fat = Convert.ToDecimal(txtFat.Text); }

            //SNF = (CLR / 4) + (FAT * 0.2) + 0.7
            snf = ((clr / 4) + (fat * Convert.ToDecimal(0.2)) + Convert.ToDecimal(0.7));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
        return snf;
    }
    private void FillMainGV(string id)
    {
        try
        {
            if (ds != null) { ds.Clear(); }
            ds = objdb.ByProcedure("sp_tblPUDCSCollection",
                         new string[] { "flag", "DCSCollectionId", "Office_ID" },
                         new string[] { "3", id.ToString(), objdb.Office_ID() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_MC_Main.DataSource = ds.Tables[0];
                gv_MC_Main.DataBind();
            }
            else
            {
                ds.Clear();
                gv_MC_Main.DataSource = ds.Tables[0];
                gv_MC_Main.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 25:" + ex.Message.ToString());
        }
    }
    private decimal GetTotalValue()
    {
        decimal totalvalue = 0, qty = 1;
        try
        {
            //Formula for TotalValue as given below:
            //Total Value = (Rate/Ltr * MilkQty (In Ltr))
            if (txtMilkQty.Text != "")
            { qty = Convert.ToDecimal(txtMilkQty.Text); }
            totalvalue = (GetRate() * qty);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 11:" + ex.Message.ToString());
        }
        return totalvalue;
    }
    public void AddToGrid()
    {
        try
        {
            decimal milktype = 0;

            if (Convert.ToString(ViewState["InsertRecord"]) == null || Convert.ToString(ViewState["InsertRecord"]) == "")
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("MilkType_id", typeof(int)));
                dt.Columns.Add(new DataColumn("MilkTypeName", typeof(string)));
                dt.Columns.Add(new DataColumn("Unit_id", typeof(int)));
                dt.Columns.Add(new DataColumn("UnitName", typeof(string)));
                dt.Columns.Add(new DataColumn("MilkQty", typeof(decimal)));
                dt.Columns.Add(new DataColumn("FAT", typeof(decimal)));
                dt.Columns.Add(new DataColumn("SNF", typeof(decimal)));
                dt.Columns.Add(new DataColumn("CLR", typeof(decimal)));
                dt.Columns.Add(new DataColumn("MilkCollectionRatePerKG", typeof(decimal)));
                dt.Columns.Add(new DataColumn("RatePerLtr", typeof(decimal)));
                dt.Columns.Add(new DataColumn("TotalValue", typeof(decimal)));

                dr = dt.NewRow();
                dr[0] = 1;
                dr[1] = ddlMilkType.SelectedValue;
                dr[2] = ddlMilkType.SelectedItem.Text;
                dr[3] = ddlUnit.SelectedValue;
                dr[4] = ddlUnit.SelectedItem.Text;
                dr[5] = txtMilkQty.Text;
                dr[6] = txtFat.Text;
                dr[7] = GetSNF();
                dr[8] = txtCLRSNF.Text;
                dr[9] = objddl.GetMilkCollectionRatePerKG(ddlMilkType.SelectedValue, ddlDS.SelectedValue).Tables[0].Rows[0]["Rate"].ToString();
                dr[10] = GetRate().ToString();
                dr[11] = GetTotalValue().ToString();

                dt.Rows.Add(dr);
                ViewState["InsertRecord"] = dt;
                gv_temp_CollectionDetail.DataSource = dt;
                gv_temp_CollectionDetail.DataBind();
            }
            else
            {
                DataTable dt = new DataTable();
                DataTable DT = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("MilkType_id", typeof(int)));
                dt.Columns.Add(new DataColumn("MilkTypeName", typeof(string)));
                dt.Columns.Add(new DataColumn("Unit_id", typeof(int)));
                dt.Columns.Add(new DataColumn("UnitName", typeof(string)));
                dt.Columns.Add(new DataColumn("MilkQty", typeof(decimal)));
                dt.Columns.Add(new DataColumn("FAT", typeof(decimal)));
                dt.Columns.Add(new DataColumn("SNF", typeof(decimal)));
                dt.Columns.Add(new DataColumn("CLR", typeof(decimal)));
                dt.Columns.Add(new DataColumn("MilkCollectionRatePerKG", typeof(decimal)));
                dt.Columns.Add(new DataColumn("RatePerLtr", typeof(decimal)));
                dt.Columns.Add(new DataColumn("TotalValue", typeof(decimal)));

                DT = (DataTable)ViewState["InsertRecord"];
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (ddlMilkType.SelectedValue == DT.Rows[i]["MilkType_id"].ToString())
                    {
                        milktype = 1;
                    }
                }
                if (milktype == 1)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Milk Type of \"" + ddlMilkType.SelectedItem.Text + "\" Already Exist.");
                }
                else
                {
                    dr = dt.NewRow();
                    dr[0] = 1;
                    dr[1] = ddlMilkType.SelectedValue;
                    dr[2] = ddlMilkType.SelectedItem.Text;
                    dr[3] = ddlUnit.SelectedValue;
                    dr[4] = ddlUnit.SelectedItem.Text;
                    dr[5] = txtMilkQty.Text;
                    dr[6] = txtFat.Text;
                    dr[7] = GetSNF();
                    dr[8] = txtCLRSNF.Text;
                    dr[9] = objddl.GetMilkCollectionRatePerKG(ddlMilkType.SelectedValue, ddlDS.SelectedValue).Tables[0].Rows[0]["Rate"].ToString();
                    dr[10] = GetRate().ToString();
                    dr[11] = GetTotalValue().ToString();
                    dt.Rows.Add(dr);
                }

                foreach (DataRow tr in DT.Rows)
                {
                    dt.Rows.Add(tr.ItemArray);
                }
                ViewState["InsertRecord"] = dt;
                gv_temp_CollectionDetail.DataSource = dt;
                gv_temp_CollectionDetail.DataBind();
            }

            //Clear Record
            ddlMilkType.ClearSelection();
            ddlUnit.ClearSelection();
            txtMilkQty.Text = "";
            txtFat.Text = "";
            txtCLRSNF.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }
    private void ClearAll()
    {
        try
        {
            txtDate.Text = "";
            ddlshift.ClearSelection();
            ddlMemberID.ClearSelection();
            txtMemberName.Text = "";
            ddlMilkType.ClearSelection();
            txtFat.Text = "";
            txtCLRSNF.Text = "";
            ddlUnit.ClearSelection();
            txtMilkQty.Text = "";
            ViewState["InsertRecord"] = null;
            ViewState["UpdateId"] = null;
            btnSave.Text = "Save";
            gv_MilkCollectionDetails.SelectedIndex = -1;
            gv_temp_CollectionDetail.DataSource = string.Empty;
            gv_temp_CollectionDetail.DataBind();
            gv_MC_Main.DataSource = string.Empty;
            gv_MC_Main.DataBind();
            if (ds != null) { ds.Clear(); }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 13:" + ex.Message.ToString());
        }
    }

    #endregion

    #region Init Function

    protected void ddlDS_Init(object sender, EventArgs e)
    {
        GetDSOfficeDetails();
    }
    protected void ddlDCS_Init(object sender, EventArgs e)
    {
        GetDCSOfficeDetails();
    }
    protected void ddlshift_Init(object sender, EventArgs e)
    {
        GetShift();
    }
    protected void ddlMemberID_Init(object sender, EventArgs e)
    {
        getMemberCodeAndName();
    }
    protected void ddlMilkType_Init(object sender, EventArgs e)
    {
        getMilkType();
    }
    protected void ddlUnit_Init(object sender, EventArgs e)
    {
        GetUnit();
    }

    #endregion

    #region Button Event

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (ViewState["UpdateId"] == null)
        {
            AddToGrid();
        }
        else
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                if (ds != null) { ds.Clear(); }
                ds = objdb.ByProcedure("sp_tblPUDCSCollection",
                         new string[] { "flag", "DCSCollectionId", "MilkType_id", "Unit_id", "MilkQty", "FAT", "SNF", "CLR", "MilkCollectionRatePerKG", "RatePerLtr", "TotalValue", "Office_ID", "CreatedBy", "CreatedBy_IP" },
                         new string[] { "7", ViewState["UpdateId"].ToString(), ddlMilkType.SelectedValue, ddlUnit.SelectedValue, txtMilkQty.Text, txtFat.Text, GetSNF().ToString(), txtCLRSNF.Text, objddl.GetMilkCollectionRatePerKG(ddlMilkType.SelectedValue, ddlDS.SelectedValue).Tables[0].Rows[0]["Rate"].ToString(), GetRate().ToString(), GetTotalValue().ToString(), objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");

                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    FillMainGV(ViewState["UpdateId"].ToString());
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnSave.Text == "Save")
            {
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                DataTable dt = ViewState["InsertRecord"] as DataTable;
                dt.Rows.Remove(dt.Rows[row.RowIndex]);
                ViewState["InsertRecord"] = dt;
                gv_temp_CollectionDetail.DataSource = dt;
                gv_temp_CollectionDetail.DataBind();
            }

            //For clear record for add child record
            ddlMilkType.ClearSelection();
            ddlUnit.ClearSelection();
            txtMilkQty.Text = "";
            txtFat.Text = "";
            txtCLRSNF.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
        }
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    if (btnSave.Text == "Save")
                    {
                        DataTable dt = new DataTable();
                        dt = getMilkCollectionDetail();

                        if (dt.Rows.Count > 0)
                        {
                            if (ds != null) { ds.Clear(); }
                            ds = objdb.ByProcedure("sp_tblPUDCSCollection",
                                     new string[] { "flag", "ProducerId", "CollectionDate", "Shift_id", "OfficeType_ID", "Office_ID", "CreatedBy", "CreatedBy_IP" },
                                     new string[] { "1", ddlMemberID.SelectedValue, txtDate.Text, ddlshift.SelectedValue, objdb.UserTypeID(), objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() },
                                     "type_tblPUDCSCollectionChild", dt, "TableSave");

                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                ClearAll();
                                getGV_MilkCollection();
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 15:" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", "Warning : First to add atleast 1 Milk collection detail!");
                        }
                    }
                    else if (btnSave.Text == "Update")
                    {
                        lblMsg.Text = "";
                        if (ds != null) { ds.Clear(); }
                        ds = objdb.ByProcedure("sp_tblPUDCSCollection",
                                 new string[] { "flag", "DCSCollectionId", "ProducerId", "CollectionDate", "Shift_id", "OfficeType_ID", "Office_ID", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                                 new string[] { "5", ViewState["UpdateId"].ToString(), ddlMemberID.SelectedValue, txtDate.Text, ddlshift.SelectedValue, objdb.UserTypeID(), objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress(), "DirectProducerCollection.aspx", "Master Record Updated." }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            ClearAll();
                            getGV_MilkCollection();
                            pnl_mc_Main_gv.Visible = false;
                            Pnl_mc_Temp_gv.Visible = true;
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 15:" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            getGV_MilkCollection();
                        }
                    }
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 16:" + ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        ClearAll();
    }

    #endregion

    #region Gridview Events _RowCommand And _RowDataBound

    protected void gv_temp_CollectionDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblValue = (Label)e.Row.FindControl("lblValue");
                Label lblMilkQty = (Label)e.Row.FindControl("lblMilkQty");

                Totalmilkqty += Convert.ToDecimal(lblMilkQty.Text);
                totalValue += Convert.ToDecimal(lblValue.Text);
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalMilkQty = (Label)e.Row.FindControl("lblTotalMilkQty");
                Label lblTotalValue = (Label)e.Row.FindControl("lblTotalValue");

                lblTotalMilkQty.Text = Totalmilkqty.ToString("0.00");
                lblTotalValue.Text = totalValue.ToString("0.00");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 17:" + ex.Message.ToString());
        }
    }
    protected void gvPopup_ViewMilkCollectionDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblValue = (Label)e.Row.FindControl("lblValue");
                Label lblMilkQty = (Label)e.Row.FindControl("lblMilkQty");

                Totalmilkqty += Convert.ToDecimal(lblMilkQty.Text);
                totalValue += Convert.ToDecimal(lblValue.Text);
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalMilkQty = (Label)e.Row.FindControl("lblTotalMilkQty");
                Label lblTotalValue = (Label)e.Row.FindControl("lblTotalValue");

                lblTotalMilkQty.Text = Totalmilkqty.ToString("0.00");
                lblTotalValue.Text = totalValue.ToString("0.00");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 17:" + ex.Message.ToString());
        }
    }
    protected void gv_MC_Main_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "DeleteRow")
            {
                lblMsg.Text = "";
                if (ds != null) { ds.Clear(); }
                ds = objdb.ByProcedure("sp_tblPUDCSCollection",
                         new string[] { "flag", "DCSChild_Id", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                         new string[] { "6", e.CommandArgument.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress(), "DirectProducerCollection.aspx", "Record Deleted" }, "dataset");
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    FillMainGV(ViewState["UpdateId"].ToString());
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 18:" + ex.Message.ToString());
        }
    }
    protected void gv_MilkCollectionDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViewRow")
            {
                lblMsg.Text = "";
                if (ds != null) { ds.Clear(); }
                ds = objdb.ByProcedure("sp_tblPUDCSCollection",
                         new string[] { "flag", "DCSCollectionId", "Office_ID" },
                         new string[] { "3", e.CommandArgument.ToString(), objdb.Office_ID() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (GridViewRow gvRow in gv_MilkCollectionDetails.Rows)
                    {
                        if (gv_MilkCollectionDetails.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            gv_MilkCollectionDetails.SelectedIndex = gvRow.DataItemIndex;
                            gv_MilkCollectionDetails.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                    lblMemberId.Text = ds.Tables[0].Rows[0]["UserName"].ToString() + " [" + ds.Tables[0].Rows[0]["ProducerName"].ToString() + "]";
                    lblMemberId.Font.Bold = true;

                    gvPopup_ViewMilkCollectionDetails.DataSource = ds;
                    gvPopup_ViewMilkCollectionDetails.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ViewDetailModal()", true);
                }
                else
                {
                    ds.Clear();
                    gvPopup_ViewMilkCollectionDetails.DataSource = ds;
                    gvPopup_ViewMilkCollectionDetails.DataBind();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                }
            }
            else if (e.CommandName == "EditRow")
            {
                lblMsg.Text = "";
                if (ds != null) { ds.Clear(); }
                ds = objdb.ByProcedure("sp_tblPUDCSCollection",
                         new string[] { "flag", "DCSCollectionId", "Office_ID" },
                         new string[] { "3", e.CommandArgument.ToString(), objdb.Office_ID() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (GridViewRow gvRow in gv_MilkCollectionDetails.Rows)
                    {
                        if (gv_MilkCollectionDetails.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            gv_MilkCollectionDetails.SelectedIndex = gvRow.DataItemIndex;
                            gv_MilkCollectionDetails.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                    DateTime CDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["CollectionDate"].ToString(), culture);
                    txtDate.Text = CDate.ToString("dd/MM/yyyy");
                    ddlshift.SelectedValue = ds.Tables[0].Rows[0]["Shift_id"].ToString();
                    ddlMemberID.SelectedValue = ds.Tables[0].Rows[0]["ProducerId"].ToString();
                    txtMemberName.Text = ds.Tables[0].Rows[0]["ProducerMobile"].ToString();

                    ViewState["UpdateId"] = e.CommandArgument.ToString();
                    Pnl_mc_Temp_gv.Visible = false;
                    pnl_mc_Main_gv.Visible = true;

                    gv_MC_Main.DataSource = ds.Tables[0];
                    gv_MC_Main.DataBind();
                    btnSave.Text = "Update";
                }
                else
                {
                    ds.Clear();
                    gvPopup_ViewMilkCollectionDetails.DataSource = ds;
                    gvPopup_ViewMilkCollectionDetails.DataBind();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                }
            }
            else if (e.CommandName == "DeleteRow")
            {
                lblMsg.Text = "";
                if (ds != null) { ds.Clear(); }
                ds = objdb.ByProcedure("sp_tblPUDCSCollection",
                         new string[] { "flag", "DCSCollectionId", "Office_ID", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                         new string[] { "4", e.CommandArgument.ToString(), objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress(), "DirectProducerCollection.aspx", "Record Deleted" }, "dataset");
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    getGV_MilkCollection();
                    ClearAll();
                    btnSave.Text = "Save";
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    getGV_MilkCollection();
                    ClearAll();
                    btnSave.Text = "Save";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 18:" + ex.Message.ToString());
        }
    }

    #endregion

    #region ChangeEvent

    protected void ddlMemberID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (CheckShiftEntryDateWise() != 1)
            {
                ds = objdb.ByProcedure("SptblPUProducerReg",
                        new string[] { "flag", "ProducerId" },
                        new string[] { "8", ddlMemberID.SelectedValue }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtMemberName.Text = ds.Tables[0].Rows[0]["Mobile"].ToString();
                }
                else
                {
                    txtMemberName.Text = "";
                }
            }
            else
            {
                txtDate.Text = "";
                ddlshift.ClearSelection();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 19:" + ex.Message.ToString());
        }
    }

    #endregion

}