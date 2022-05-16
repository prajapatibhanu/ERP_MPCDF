using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;

public partial class mis_MilkCollection_RMRD_Challan_EntryFromCanesTotalEntry : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    MilkCalculationClass Obj_MC = new MilkCalculationClass();
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
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDate.Attributes.Add("readonly", "readonly");
                FillBMCRoot();
                
                txtfilterdate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                FillGrid();
                SetFocus(ddlMilkCollectionUnit);
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region User Defined Function
    public void FillBMCRoot()
    {

        try
        {
            ds = null;
             ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
                      new string[] { "flag", "Office_ID", "OfficeType_ID" },
                      new string[] { "11",objdb.Office_ID(),objdb.OfficeType_ID() }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlBMCTankerRootName.DataTextField = "BMCTankerRootName";
                ddlBMCTankerRootName.DataValueField = "BMCTankerRoot_Id";
                ddlBMCTankerRootName.DataSource = ds;
                ddlBMCTankerRootName.DataBind();
                ddlBMCTankerRootName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }

    }
    private void AddMilkDetails()
    {
        try
        {
            //int CompartmentType = 0;

            if (Convert.ToString(ViewState["InsertRecord"]) == null || Convert.ToString(ViewState["InsertRecord"]) == "")
            {
                DataTable dt = new DataTable();
                DataRow dr;
                DataColumn RowNo = dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add(new DataColumn("MilkType", typeof(string)));
                dt.Columns.Add(new DataColumn("Quantity", typeof(decimal)));
                dt.Columns.Add(new DataColumn("Fat", typeof(decimal)));
                dt.Columns.Add(new DataColumn("LR", typeof(decimal)));
                dt.Columns.Add(new DataColumn("Snf", typeof(decimal)));
                dt.Columns.Add(new DataColumn("kgFat", typeof(decimal)));
                dt.Columns.Add(new DataColumn("kgSnf", typeof(decimal)));
                dt.Columns.Add(new DataColumn("Shift", typeof(string)));
                dt.Columns.Add(new DataColumn("Quality", typeof(string)));
                dt.Columns.Add(new DataColumn("Temp", typeof(string)));
                dt.Columns.Add(new DataColumn("TotalCan", typeof(string)));
                dt.Columns.Add(new DataColumn("GoodCan", typeof(string)));
                RowNo.AutoIncrement = true;
                RowNo.AutoIncrementSeed = 1;
                RowNo.AutoIncrementStep = 1;
                dr = dt.NewRow();

                dr[1] = ddlMilkType.SelectedItem.Text;
                dr[2] = txtMilkQuantity.Text;
                dr[3] = txtNetFat.Text;
                dr[4] = txtNetCLR.Text;
                dr[5] = txtnetsnf.Text;
                dr[6] = txtfatinkg.Text;
                dr[7] = txtsnfinkg.Text;
                dr[8] = ddlShift.SelectedItem.Text;
                dr[9] = ddlMilkQuality.SelectedItem.Text;
                dr[10] = txtTEMP.Text;
                dr[11] = txtTotalCan.Text;
                dr[12] = txtGoodCan.Text;

                dt.Rows.Add(dr);
                ViewState["InsertRecord"] = dt;
                gv_MilkDetails.DataSource = dt;
                gv_MilkDetails.DataBind();


            }
            else
            {
                DataTable dt = new DataTable();
                DataTable DT = new DataTable();
                DataRow dr;
                DataColumn RowNo = dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add(new DataColumn("MilkType", typeof(string)));
                dt.Columns.Add(new DataColumn("Quantity", typeof(decimal)));
                dt.Columns.Add(new DataColumn("Fat", typeof(decimal)));
                dt.Columns.Add(new DataColumn("LR", typeof(decimal)));
                dt.Columns.Add(new DataColumn("Snf", typeof(decimal)));
                dt.Columns.Add(new DataColumn("kgFat", typeof(decimal)));
                dt.Columns.Add(new DataColumn("kgSnf", typeof(decimal)));
                dt.Columns.Add(new DataColumn("Shift", typeof(string)));
                dt.Columns.Add(new DataColumn("Quality", typeof(string)));
                dt.Columns.Add(new DataColumn("Temp", typeof(string)));
                dt.Columns.Add(new DataColumn("TotalCan", typeof(string)));
                dt.Columns.Add(new DataColumn("GoodCan", typeof(string)));
                RowNo.AutoIncrement = true;
                RowNo.AutoIncrementSeed = 1;
                RowNo.AutoIncrementStep = 1;
                dt = (DataTable)ViewState["InsertRecord"];

                dr = dt.NewRow();

                dr[1] = ddlMilkType.SelectedItem.Text;
                dr[2] = txtMilkQuantity.Text;
                dr[3] = txtNetFat.Text;
                dr[4] = txtNetCLR.Text;
                dr[5] = txtnetsnf.Text;
                dr[6] = txtfatinkg.Text;
                dr[7] = txtsnfinkg.Text;
                dr[8] = ddlShift.SelectedItem.Text;
                dr[9] = ddlMilkQuality.SelectedItem.Text;
                dr[10] = txtTEMP.Text;
                dr[11] = txtTotalCan.Text;
                dr[12] = txtGoodCan.Text;
                dt.Rows.Add(dr);


                ViewState["InsertRecord"] = dt;
                gv_MilkDetails.DataSource = dt;
                gv_MilkDetails.DataBind();

            }

            txtMilkQuantity.Text = "";
            txtNetFat.Text = "";
            txtNetCLR.Text = "";
            txtnetsnf.Text = "";
            txtfatinkg.Text = "";
            txtsnfinkg.Text = "";
            ddlShift.ClearSelection();
            ddlMilkQuality.ClearSelection();
            txtTEMP.Text = "";
            txtTotalCan.Text = "";
            txtGoodCan.Text = "";



        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }

    }
    private DataTable GetMilkDetails()
    {

        DataTable dt = new DataTable();
        DataRow dr;


        dt.Columns.Add(new DataColumn("Shift", typeof(string)));
        dt.Columns.Add(new DataColumn("MilkType", typeof(string)));
        dt.Columns.Add(new DataColumn("Temp", typeof(string)));
        dt.Columns.Add(new DataColumn("Acidity", typeof(string)));
        dt.Columns.Add(new DataColumn("Quantity", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Quality", typeof(string)));
        dt.Columns.Add(new DataColumn("Fat", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Snf", typeof(decimal)));
        dt.Columns.Add(new DataColumn("LR", typeof(decimal)));
        dt.Columns.Add(new DataColumn("kgFat", typeof(decimal)));
        dt.Columns.Add(new DataColumn("kgSnf", typeof(decimal)));
        dt.Columns.Add(new DataColumn("TotalCan", typeof(string)));
        dt.Columns.Add(new DataColumn("GoodCan", typeof(string)));

        foreach (GridViewRow row in gv_MilkDetails.Rows)
        {
            Label lblMilkType = (Label)row.FindControl("lblMilkType");
            Label lblShift = (Label)row.FindControl("lblShift");
            Label lblTemp = (Label)row.FindControl("lblTemp");
            Label lblQuantity = (Label)row.FindControl("lblQuantity");
            Label lblTotalCan = (Label)row.FindControl("lblTotalCan");
            Label lblGoodCan = (Label)row.FindControl("lblGoodCan");
            Label lblQuality = (Label)row.FindControl("lblQuality");
            Label lblFat = (Label)row.FindControl("lblFat");
            Label lblSnf = (Label)row.FindControl("lblSnf");
            Label lblClr = (Label)row.FindControl("lblClr");
            Label lblkgFat = (Label)row.FindControl("lblkgFat");
            Label lblkgSnf = (Label)row.FindControl("lblkgSnf");

            dr = dt.NewRow();
            dr[0] = lblShift.Text;

            dr[1] = lblMilkType.Text;
            dr[2] = lblTemp.Text;
            dr[3] = DBNull.Value;
            dr[4] = lblQuantity.Text;
            dr[5] = lblQuality.Text;
            dr[6] = lblFat.Text;
            dr[7] = lblSnf.Text;
            dr[8] = lblClr.Text;
            dr[9] = lblkgFat.Text;
            dr[10]= lblkgSnf.Text;
            dr[11] = lblTotalCan.Text;
            dr[12] = lblGoodCan.Text;
            dt.Rows.Add(dr);


        }

        return dt;
    }
    protected void FillGrid()
    {
        try
        {

            ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntryViaCanes", new string[] { "flag", "EntryDate", "Created_Office_ID" }, new string[] { "2", Convert.ToDateTime(txtfilterdate.Text, cult).ToString("yyyy/MM/dd"), objdb.Office_ID() }, "dataset");
            if(ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvEntryList.DataSource = ds;
                gvEntryList.DataBind();
            }
            else
            {
                gvEntryList.DataSource = string.Empty;
                gvEntryList.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
     }
    #endregion
    #region changed event
    protected void ddlBMCTankerRootName_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            //ddlBMC.Items.Clear();
            //lblMsg.Text = "";
            //ds = null;
            //if (ddlBMCTankerRootName.SelectedIndex > 0)
            //{

            //    ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
            //               new string[] { "flag", "BMCTankerRoot_Id", "Office_ID", "OfficeType_ID" },
            //               new string[] { "5", ddlBMCTankerRootName.SelectedValue, objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            //    if (ds != null && ds.Tables[0].Rows.Count > 0)
            //    {
            //        ddlBMC.DataTextField = "Office_Name";
            //        ddlBMC.DataValueField = "I_OfficeID";
            //        ddlBMC.DataSource = ds;
            //        ddlBMC.DataBind();
            //        ddlBMC.Items.Insert(0, new ListItem("Select", "0"));

            //    }
            //    else
            //    {
            //        ddlBMC.Items.Insert(0, new ListItem("Select", "0"));
            //    }

            //}
            //else
            //{
            //    ddlBMC.Items.Insert(0, new ListItem("Select", "0"));

            //}


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void rfvShift_Init(object sender, EventArgs e)
    {
        try
        {
            DataSet dsct = objdb.ByProcedure("USP_GetServerDatetime",
                             new string[] { "flag" },
                             new string[] { "1" }, "dataset");

            string currrentime = dsct.Tables[0].Rows[0]["currentTime"].ToString();

            string[] s = currrentime.Split(':');

            if ((Convert.ToInt32(s[0]) >= 0 && Convert.ToInt32(s[0]) <= 12 && ((Convert.ToInt32(s[0]) == 0 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 12 && Convert.ToInt32(s[1]) <= 59))))
            {
                ddlShift.SelectedValue = "Morning";
            }
            else
            {
                ddlShift.SelectedValue = "Evening";
            }

            //txtDate.Attributes.Add("readonly", "readonly");
            //txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            //ddlShift.Enabled = false;


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Button Event
    protected void btnAddSocietyDetails_Click(object sender, EventArgs e)
    {
        try
        {
            AddMilkDetails();
            SetFocus(ddlShift);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }

    }
    #endregion
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataTable dt2 = new DataTable();
            dt2 = GetMilkDetails();
            string IsActive = "1";
            if (dt2.Rows.Count > 0)
            {
                ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntryViaCanes",
                                        new string[] 
                            {"flag"
                             ,"EntryDate"
                             ,"BMCTankerRoot_Id"
                             ,"Office_ID"
                             ,"IsActive"
                             ,"CreatedBy"
                             ,"CreatedByIP"
                             ,"Created_Office_ID"
                            },
                                        new string[] 
                            {"1"
                             ,Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd")
                             ,ddlBMCTankerRootName.SelectedValue
                             ,ddlSociety.SelectedValue  
                             ,IsActive                          
                             ,objdb.createdBy()
                             ,objdb.GetLocalIPAddress() 
                             ,objdb.Office_ID()
                            },
                                      new string[] { 
                                  "type_MilkCollectionChallanEntryViaCanes"                                            
                              },
                                        new DataTable[] {
                                  dt2
                                
                              },
                                      "TableSave");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                           
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Success!", "" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString() + "");
                            FillGrid();
                            gv_MilkDetails.DataSource = new string[] { };
                            gv_MilkDetails.DataBind();
                            ddlBMCTankerRootName.ClearSelection();
                            //ddlBMCTankerRootName_SelectedIndexChanged(sender, e);
                            ddlMilkCollectionUnit.ClearSelection();
                            ddlMilkCollectionUnit_SelectedIndexChanged(sender, e);


                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                           
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString() + "");

                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            
                        }
                    }
                }
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    protected void ddlMilkCollectionUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSociety();
        SetFocus(ddlSociety);
    }

    protected void FillSociety()
    {
        try
        {
            if (ddlMilkCollectionUnit.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                                  new string[] { "flag", "Office_ID", "OfficeType_ID" },
                                  new string[] { "10", ViewState["Office_ID"].ToString(), ddlMilkCollectionUnit.SelectedValue }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSociety.DataTextField = "Office_Name";
                        ddlSociety.DataValueField = "Office_ID";
                        ddlSociety.DataSource = ds.Tables[2];
                        ddlSociety.DataBind();
                        ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    else
                    {
                        ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
                else
                {
                    ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                Response.Redirect("RMRD_Challan_EntryFromCanesTotalEntry.aspx", false);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtfilterdate_TextChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void txtNetFat_TextChanged(object sender, EventArgs e)
    {
        txtnetsnf.Text = GetSNF().ToString();
        txtfatinkg.Text = GetFAT_InKG().ToString();
        txtsnfinkg.Text = GetSNF_InKG().ToString();

        if (txtNetCLR.Text == "")
        {
            txtNetCLR.Focus();
        }

        if (txtNetFat.Text == "")
        {
            txtNetFat.Focus();
        }
        if (Convert.ToDecimal(txtNetFat.Text) > Convert.ToDecimal(5.5))
        {
            ddlMilkType.ClearSelection();
            ddlMilkType.SelectedValue = "1";
        }
        else
        {
            ddlMilkType.ClearSelection();
            ddlMilkType.SelectedValue = "2";
        }
    }

    protected void txtNetCLR_TextChanged(object sender, EventArgs e)
    {
        txtnetsnf.Text = GetSNF().ToString();
        txtfatinkg.Text = GetFAT_InKG().ToString();
        txtsnfinkg.Text = GetSNF_InKG().ToString();

        if (txtNetCLR.Text == "")
        {
            txtNetCLR.Focus();
        }

        if (txtNetFat.Text == "")
        {
            txtNetFat.Focus();
        }
        SetFocus(txtTotalCan);
    }
    private decimal GetSNF()
    {
        decimal snf = 0, clr = 0, fat = 1;
        try
        {
            //Formula for Get SNF as given below:
            if (txtNetCLR.Text != "")
            { clr = Convert.ToDecimal(txtNetCLR.Text); }
            if (txtNetFat.Text != "")
            { fat = Convert.ToDecimal(txtNetFat.Text); }

            //SNF = (CLR / 4) + (FAT * 0.2) + 0.7 -- Formula Before
            //SNF = (CLR / 4) + (0.25 * FAT) + 0.44 -- Formula After Mail dt: 27 Jan 2020, 16:26
            //snf = ((clr / 4) + (fat * Convert.ToDecimal(0.25)) + Convert.ToDecimal(0.44));
            snf = Obj_MC.GetSNFPer_DCS(fat, clr);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
        return Math.Round(snf, 3);
    }

    private decimal GetSNF_InKG()
    {
        decimal clr = 0, fat = 0, snf_Per = 0, MilkQty = 0, SNF_InKG = 0;

        try
        {
            if (txtMilkQuantity.Text == "") { MilkQty = 0; } else { MilkQty = Convert.ToDecimal(txtMilkQuantity.Text); }

            if (txtNetFat.Text == "") { fat = 0; } else { fat = Convert.ToDecimal(txtNetFat.Text); }

            if (txtNetCLR.Text == "") { clr = 0; } else { clr = Convert.ToDecimal(txtNetCLR.Text); }

            snf_Per = Obj_MC.GetSNFPer_DCS(fat, clr);

            SNF_InKG = Obj_MC.GetSNFInKg(MilkQty, snf_Per);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }

        return Math.Round(SNF_InKG, 3);
    }

    private decimal GetFAT_InKG()
    {
        decimal fat_Per = 0, MilkQty = 0, FAT_InKG = 0;

        try
        {
            if (txtMilkQuantity.Text == "") { MilkQty = 0; } else { MilkQty = Convert.ToDecimal(txtMilkQuantity.Text); }

            if (txtNetFat.Text == "") { fat_Per = 0; } else { fat_Per = Convert.ToDecimal(txtNetFat.Text); }

            FAT_InKG = Obj_MC.GetSNFInKg(MilkQty, fat_Per);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }

        return Math.Round(FAT_InKG, 3);
    }
    protected void txtMilkQuantity_TextChanged(object sender, EventArgs e)
    {
        txtnetsnf.Text = GetSNF().ToString();
        txtfatinkg.Text = GetFAT_InKG().ToString();
        txtsnfinkg.Text = GetSNF_InKG().ToString();

        if (txtNetFat.Text == "")
        {
            txtNetFat.Focus();
        }
    }
    protected void gvEntryList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string MilkCollectionViaCanesChallan_ID = e.CommandArgument.ToString();
            if(e.CommandName == "DeleteRecord")
            {
                objdb.ByProcedure("Usp_MilkCollectionChallanEntryViaCanes", new string[] { "flag", "MilkCollectionViaCanesChallan_ID" }, new string[] { "3", MilkCollectionViaCanesChallan_ID }, "dataset");

                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", "Record Deleted Successfully.");
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlSociety_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlBMCTankerRootName.ClearSelection();
            ddlBMCTankerRootName.Enabled = false;
            if (ddlSociety.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntry", new string[] { "flag", "Office_ID" }, new string[] { "2", ddlSociety.SelectedValue }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlBMCTankerRootName.SelectedValue = ds.Tables[0].Rows[0]["BMCTankerRoot_Id"].ToString();

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
        SetFocus(ddlShift);
    }
}
