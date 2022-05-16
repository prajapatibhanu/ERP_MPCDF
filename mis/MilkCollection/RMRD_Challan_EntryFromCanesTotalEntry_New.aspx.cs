using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;

public partial class mis_MilkCollection_RMRD_Challan_EntryFromCanesTotalEntry_New : System.Web.UI.Page
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
                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDate.Attributes.Add("readonly", "readonly");
                FillBMCRoot();

                txtfilterdate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                FillGrid();


            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region User Defined Function
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
        //if (Session["event_control"] != null)
        //{
        //    if (Session["event_control"] is TextBox)
        ////    {
        //        TextBox control = (TextBox)Session["event_control"];
        //        control.Focus();
        //    }
        //    else if (Session["event_control"] is DropDownList)
        //    {
        //        DropDownList control = (DropDownList)Session["event_control"];
        //        control.Focus();
        //    }
        //}
    }
    public void FillBMCRoot()
    {

        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
                      new string[] { "flag", "Office_ID" },
                      new string[] { "1", objdb.Office_ID() }, "dataset");

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

    private DataTable GetBufMilkDetails()
    {

        DataTable dt = new DataTable();
        DataRow dr;


        dt.Columns.Add(new DataColumn("Office_ID", typeof(string)));
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

        foreach (GridViewRow row in gvBMCMilkDetails.Rows)
        {
            CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
            Label lblOffice_ID = (Label)row.FindControl("lblOffice_ID");
            TextBox txtBufTEMP = (TextBox)row.FindControl("txtBufTEMP");
            DropDownList ddlBufMilkQuality = (DropDownList)row.FindControl("ddlBufMilkQuality");
            TextBox txtBuftotalCan = (TextBox)row.FindControl("txtBuftotalCan");
            TextBox txtBufGoodCan = (TextBox)row.FindControl("txtBufGoodCan");
            TextBox txtBufMilkQuantity = (TextBox)row.FindControl("txtBufMilkQuantity");
            TextBox txtBufFat = (TextBox)row.FindControl("txtBufFat");
            TextBox txtBufCLR = (TextBox)row.FindControl("txtBufCLR");
            TextBox txtBufSnf = (TextBox)row.FindControl("txtBufSnf");
            TextBox lblkgFat = (TextBox)row.FindControl("lblkgFat");
            TextBox lblkgSnf = (TextBox)row.FindControl("lblkgSnf");
            TextBox txtBufFatinkg = (TextBox)row.FindControl("txtBufFatinkg");
            TextBox txtBufSnfinkg = (TextBox)row.FindControl("txtBufSnfinkg");
            if (chkSelect.Checked == true)
            {
                if (decimal.Parse(txtBufMilkQuantity.Text) > 0)
                {
                    dr = dt.NewRow();
                    dr[0] = lblOffice_ID.Text;

                    dr[1] = "Buf";
                    dr[2] = txtBufTEMP.Text;
                    dr[3] = DBNull.Value;
                    dr[4] = txtBufMilkQuantity.Text;
                    dr[5] = ddlBufMilkQuality.SelectedValue;
                    dr[6] = txtBufFat.Text;
                    dr[7] = txtBufSnf.Text;
                    dr[8] = txtBufCLR.Text;
                    dr[9] = txtBufFatinkg.Text;
                    dr[10] = txtBufSnfinkg.Text;
                    dr[11] = txtBuftotalCan.Text;
                    dr[12] = txtBufGoodCan.Text;
                    dt.Rows.Add(dr);
                }
                
            }



        }

        return dt;
    }
    private DataTable GetCowMilkDetails()
    {

        DataTable dt = new DataTable();
        DataRow dr;


        dt.Columns.Add(new DataColumn("Office_ID", typeof(string)));
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

        foreach (GridViewRow row in gvBMCMilkDetails.Rows)
        {
            CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
            Label lblOffice_ID = (Label)row.FindControl("lblOffice_ID");
            TextBox txtCowTEMP = (TextBox)row.FindControl("txtCowTEMP");
            DropDownList ddlCowMilkQuality = (DropDownList)row.FindControl("ddlCowMilkQuality");
            TextBox txtCowtotalCan = (TextBox)row.FindControl("txtCowtotalCan");
            TextBox txtCowGoodCan = (TextBox)row.FindControl("txtCowGoodCan");
            TextBox txtCowMilkQuantity = (TextBox)row.FindControl("txtCowMilkQuantity");
            TextBox txtCowFat = (TextBox)row.FindControl("txtCowFat");
            TextBox txtCowCLR = (TextBox)row.FindControl("txtCowCLR");
            TextBox txtCowSnf = (TextBox)row.FindControl("txtCowSnf");
            TextBox lblkgFat = (TextBox)row.FindControl("lblkgFat");
            TextBox lblkgSnf = (TextBox)row.FindControl("lblkgSnf");
            TextBox txtCowFatinkg = (TextBox)row.FindControl("txtCowFatinkg");
            TextBox txtCowSnfinkg = (TextBox)row.FindControl("txtCowSnfinkg");
            if (chkSelect.Checked == true)
            {
                if (decimal.Parse(txtCowMilkQuantity.Text) > 0)
                {
                    dr = dt.NewRow();
                    dr[0] = lblOffice_ID.Text;

                    dr[1] = "Cow";
                    dr[2] = txtCowTEMP.Text;
                    dr[3] = DBNull.Value;
                    dr[4] = txtCowMilkQuantity.Text;
                    dr[5] = ddlCowMilkQuality.SelectedValue;
                    dr[6] = txtCowFat.Text;
                    dr[7] = txtCowSnf.Text;
                    dr[8] = txtCowCLR.Text;
                    dr[9] = txtCowFatinkg.Text;
                    dr[10] = txtCowSnfinkg.Text;
                    dr[11] = txtCowtotalCan.Text;
                    dr[12] = txtCowGoodCan.Text;
                    dt.Rows.Add(dr);
                }
                

            }
        }

        return dt;
    }
    protected void FillGrid()
    {
        try
        {

            ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntryViaCanes", new string[] { "flag", "EntryDate", "Created_Office_ID" }, new string[] { "2", Convert.ToDateTime(txtfilterdate.Text, cult).ToString("yyyy/MM/dd"), objdb.Office_ID() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
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
            gvBMCMilkDetails.DataSource = string.Empty;
            gvBMCMilkDetails.DataBind();
            lblMsg.Text = "";
            ds = null;
            if (ddlBMCTankerRootName.SelectedIndex > 0)
            {

                ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntryViaCanes",
                           new string[] { "flag", "BMCTankerRoot_Id", "Office_ID", "OfficeType_ID", "EntryDate", "Shift" },
                           new string[] { "5", ddlBMCTankerRootName.SelectedValue, objdb.Office_ID(), objdb.OfficeType_ID(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), ddlShift.SelectedValue }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    gvBMCMilkDetails.DataSource = ds;
                    gvBMCMilkDetails.DataBind();


                }
                else
                {
                    gvBMCMilkDetails.DataSource = string.Empty;
                    gvBMCMilkDetails.DataBind();
                }

            }
            else
            {
                gvBMCMilkDetails.DataSource = string.Empty;
                gvBMCMilkDetails.DataBind();

            }


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
    //protected void btnAddSocietyDetails_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        AddMilkDetails();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

    //    }

    //}
    #endregion
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            lblMsg.Text = "";
            Page.Validate("a");
            // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ValidatePage();", true);
            if (Page.IsValid)
            {
                DataTable dtBuf = new DataTable();
                DataTable dtCow = new DataTable();
                dtBuf = GetBufMilkDetails();
                dtCow = GetCowMilkDetails();
                string IsActive = "1";
                if (dtBuf.Rows.Count > 0 && dtBuf.Rows.Count > 0)
                {
                    ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntryViaCanes",
                      new string[] 
                     {"flag"
                      ,"EntryDate"
                      ,"BMCTankerRoot_Id"
                      ,"Shift"
                      ,"IsActive"
                      ,"CreatedBy"
                      ,"CreatedByIP"
                      ,"Created_Office_ID"
                     },
                      new string[] 
                     {"6"
                      ,Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd")
                      ,ddlBMCTankerRootName.SelectedValue 
                      ,ddlShift.SelectedItem.Text
                      ,IsActive                          
                      ,objdb.createdBy()
                      ,objdb.GetLocalIPAddress() 
                      ,objdb.Office_ID()
                     },
                     new string[] { 
                           "type_CowMilkCollectionChallanEntryViaCanes"    
                          ,"type_BufMilkCollectionChallanEntryViaCanes"             
                       },
                     new DataTable[] {
                           dtCow
                          ,dtBuf
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
                                gvBMCMilkDetails.DataSource = new string[] { };
                                gvBMCMilkDetails.DataBind();
                                ddlBMCTankerRootName.ClearSelection();
                                ////ddlBMCTankerRootName_SelectedIndexChanged(sender, e);
                                //ddlMilkCollectionUnit.ClearSelection();
                                //ddlMilkCollectionUnit_SelectedIndexChanged(sender, e);


                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                            {
                                Session["IsSuccess"] = true;
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString() + "");

                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                Session["IsSuccess"] = false;
                            }
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Please check at Least one row.');",true);
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    //protected void ddlMilkCollectionUnit_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    FillSociety();
    //}

    //protected void FillSociety()
    //{
    //    try
    //    {
    //        if (ddlMilkCollectionUnit.SelectedValue != "0")
    //        {
    //            ds = null;
    //            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
    //                              new string[] { "flag", "Office_ID", "OfficeType_ID" },
    //                              new string[] { "10", ViewState["Office_ID"].ToString(), ddlMilkCollectionUnit.SelectedValue }, "dataset");

    //            if (ds.Tables.Count > 0)
    //            {
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    ddlSociety.DataTextField = "Office_Name";
    //                    ddlSociety.DataValueField = "Office_ID";
    //                    ddlSociety.DataSource = ds.Tables[2];
    //                    ddlSociety.DataBind();
    //                    ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
    //                }
    //                else
    //                {
    //                    ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
    //                }
    //            }
    //            else
    //            {
    //                ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
    //            }
    //        }
    //        else
    //        {
    //            Response.Redirect("RMRD_Challan_EntryFromCanes.aspx", false);
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void txtfilterdate_TextChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    //protected void txtNetFat_TextChanged(object sender, EventArgs e)
    //{
    //    //txtnetsnf.Text = GetSNF().ToString();
    //    //txtfatinkg.Text = GetFAT_InKG().ToString();
    //    //txtsnfinkg.Text = GetSNF_InKG().ToString();

    //    //if (txtNetCLR.Text == "")
    //    //{
    //    //    txtNetCLR.Focus();
    //    //}

    //    //if (txtNetFat.Text == "")
    //    //{
    //    //    txtNetFat.Focus();
    //    //}

    //}

    //protected void txtNetCLR_TextChanged(object sender, EventArgs e)
    //{
    //    //txtnetsnf.Text = GetSNF().ToString();
    //    //txtfatinkg.Text = GetFAT_InKG().ToString();
    //    //txtsnfinkg.Text = GetSNF_InKG().ToString();

    //    //if (txtNetCLR.Text == "")
    //    //{
    //    //    txtNetCLR.Focus();
    //    //}

    //    //if (txtNetFat.Text == "")
    //    //{
    //    //    txtNetFat.Focus();
    //    //}

    //}
    private decimal GetSNF(string FAT, string CLR)
    {
        decimal snf = 0, clr = 0, fat = 1;
        try
        {
            //Formula for Get SNF as given below:
            if (CLR != "")
            { clr = Convert.ToDecimal(CLR); }
            if (FAT != "")
            { fat = Convert.ToDecimal(FAT); }

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


    //protected void txtMilkQuantity_TextChanged(object sender, EventArgs e)
    //{
    //    //txtnetsnf.Text = GetSNF().ToString();
    //    //txtfatinkg.Text = GetFAT_InKG().ToString();
    //    //txtsnfinkg.Text = GetSNF_InKG().ToString();

    //    //if (txtNetFat.Text == "")
    //    //{
    //    //    txtNetFat.Focus();
    //    //}
    //}
    protected void gvEntryList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string MilkCollectionViaCanesChallan_ID = e.CommandArgument.ToString();
            if (e.CommandName == "DeleteRecord")
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
    protected void gvBMCMilkDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 2;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "< ---- Buf ---- >";
            HeaderCell.ColumnSpan = 8;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "< ---- Cow ---- >";
            HeaderCell.ColumnSpan = 8;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvBMCMilkDetails.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void txtBufMilkQuantity_TextChanged(object sender, EventArgs e)
    {

        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox txtBufMilkQuantity = (TextBox)row.FindControl("txtBufMilkQuantity");
        TextBox txtBufFat = (TextBox)row.FindControl("txtBufFat");
        TextBox txtBufCLR = (TextBox)row.FindControl("txtBufCLR");
        TextBox txtBufSNF = (TextBox)row.FindControl("txtBufSNF");
        TextBox txtBufFatinkg = (TextBox)row.FindControl("txtBufFatinkg");
        TextBox txtBufSnfinkg = (TextBox)row.FindControl("txtBufSnfinkg");
        txtBufSNF.Text = GetSNF(txtBufFat.Text, txtBufCLR.Text).ToString();
        if (txtBufMilkQuantity.Text != "" && txtBufFat.Text != "")
        {
            txtBufFatinkg.Text = Obj_MC.GetSNFInKg(decimal.Parse(txtBufMilkQuantity.Text), decimal.Parse(txtBufFat.Text)).ToString();
        }
        else
        {
            txtBufFatinkg.Text = "";
        }
        if (txtBufMilkQuantity.Text != "" && txtBufSNF.Text != "")
        {
            txtBufSnfinkg.Text = Obj_MC.GetSNFInKg(decimal.Parse(txtBufMilkQuantity.Text), decimal.Parse(txtBufSNF.Text)).ToString();
        }
        else
        {
            txtBufSnfinkg.Text = "";
        }
        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
        RequiredFieldValidator rfvBufMilkQuality = (RequiredFieldValidator)row.FindControl("rfvBufMilkQuality");
        RequiredFieldValidator rfvtxtBufTEMP = (RequiredFieldValidator)row.FindControl("rfvtxtBufTEMP");
        RequiredFieldValidator rfvBufMilkQuantity = (RequiredFieldValidator)row.FindControl("rfvBufMilkQuantity");
        RequiredFieldValidator rfvBufFat = (RequiredFieldValidator)row.FindControl("rfvBufFat");
        RequiredFieldValidator rfvBufCLR = (RequiredFieldValidator)row.FindControl("rfvBufCLR");
        if (txtBufMilkQuantity.Text == "")
        {
            txtBufMilkQuantity.Text = "0";
        }

        if (chkSelect.Checked == true)
        {
            if (decimal.Parse(txtBufMilkQuantity.Text) > 0)
            {
                rfvBufMilkQuality.Enabled = true;
                rfvtxtBufTEMP.Enabled = true;
                rfvBufMilkQuantity.Enabled = true;
                rfvBufFat.Enabled = true;
                rfvBufCLR.Enabled = true;
            }
            else
            {
                rfvBufMilkQuality.Enabled = false;
                rfvtxtBufTEMP.Enabled = false;
                rfvBufMilkQuantity.Enabled = false;
                rfvBufFat.Enabled = false;
                rfvBufCLR.Enabled = false;
            }
           

        }
        else
        {
            rfvBufMilkQuality.Enabled = false;
            rfvtxtBufTEMP.Enabled = false;
            rfvBufMilkQuantity.Enabled = false;
            rfvBufFat.Enabled = false;
            rfvBufCLR.Enabled = false;
           

        }
        SetFocus(txtBufFat);
    }
    protected void txtBufFat_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox txtBufMilkQuantity = (TextBox)row.FindControl("txtBufMilkQuantity");
        TextBox txtBufFat = (TextBox)row.FindControl("txtBufFat");
        TextBox txtBufCLR = (TextBox)row.FindControl("txtBufCLR");
        TextBox txtBufSNF = (TextBox)row.FindControl("txtBufSNF");
        TextBox txtBufFatinkg = (TextBox)row.FindControl("txtBufFatinkg");
        TextBox txtBufSnfinkg = (TextBox)row.FindControl("txtBufSnfinkg");
        txtBufSNF.Text = GetSNF(txtBufFat.Text, txtBufCLR.Text).ToString();

        if (txtBufMilkQuantity.Text != "" && txtBufFat.Text != "")
        {
            txtBufFatinkg.Text = Obj_MC.GetSNFInKg(decimal.Parse(txtBufMilkQuantity.Text), decimal.Parse(txtBufFat.Text)).ToString();
        }
        else
        {
            txtBufFatinkg.Text = "";
        }
        if (txtBufMilkQuantity.Text != "" && txtBufSNF.Text != "")
        {
            txtBufSnfinkg.Text = Obj_MC.GetSNFInKg(decimal.Parse(txtBufMilkQuantity.Text), decimal.Parse(txtBufSNF.Text)).ToString();
        }
        else
        {
            txtBufSnfinkg.Text = "";
        }



        SetFocus(txtBufCLR);

    }
    protected void txtBufCLR_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox txtBufMilkQuantity = (TextBox)row.FindControl("txtBufMilkQuantity");
        TextBox txtBufFat = (TextBox)row.FindControl("txtBufFat");
        TextBox txtBufCLR = (TextBox)row.FindControl("txtBufCLR");
        TextBox txtBufSNF = (TextBox)row.FindControl("txtBufSNF");
        TextBox txtBuftotalCan = (TextBox)row.FindControl("txtBuftotalCan");
        TextBox txtBufFatinkg = (TextBox)row.FindControl("txtBufFatinkg");
        TextBox txtBufSnfinkg = (TextBox)row.FindControl("txtBufSnfinkg");
        txtBufSNF.Text = GetSNF(txtBufFat.Text, txtBufCLR.Text).ToString();
        if (txtBufMilkQuantity.Text != "" && txtBufFat.Text != "")
        {
            txtBufFatinkg.Text = Obj_MC.GetSNFInKg(decimal.Parse(txtBufMilkQuantity.Text), decimal.Parse(txtBufFat.Text)).ToString();
        }
        else
        {
            txtBufFatinkg.Text = "";
        }
        if (txtBufMilkQuantity.Text != "" && txtBufSNF.Text != "")
        {
            txtBufSnfinkg.Text = Obj_MC.GetSNFInKg(decimal.Parse(txtBufMilkQuantity.Text), decimal.Parse(txtBufSNF.Text)).ToString();
        }
        else
        {
            txtBufSnfinkg.Text = "";
        }


        SetFocus(txtBuftotalCan);
    }
    protected void txtCowMilkQuantity_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox txtCowMilkQuantity = (TextBox)row.FindControl("txtCowMilkQuantity");
        TextBox txtCowFat = (TextBox)row.FindControl("txtCowFat");
        TextBox txtCowCLR = (TextBox)row.FindControl("txtCowCLR");
        TextBox txtCowSNF = (TextBox)row.FindControl("txtCowSNF");
        TextBox txtCowFatinkg = (TextBox)row.FindControl("txtCowFatinkg");
        TextBox txtCowSnfinkg = (TextBox)row.FindControl("txtCowSnfinkg");
        txtCowSNF.Text = GetSNF(txtCowFat.Text, txtCowCLR.Text).ToString();

        if (txtCowMilkQuantity.Text != "" && txtCowFat.Text != "")
        {
            txtCowFatinkg.Text = Obj_MC.GetSNFInKg(decimal.Parse(txtCowMilkQuantity.Text), decimal.Parse(txtCowFat.Text)).ToString();
        }
        else
        {
            txtCowFatinkg.Text = "";
        }
        if (txtCowMilkQuantity.Text != "" && txtCowSNF.Text != "")
        {
            txtCowSnfinkg.Text = Obj_MC.GetSNFInKg(decimal.Parse(txtCowMilkQuantity.Text), decimal.Parse(txtCowSNF.Text)).ToString();
        }
        else
        {
            txtCowSnfinkg.Text = "";
        }

        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
        
        RequiredFieldValidator rfvCowMilkQuality = (RequiredFieldValidator)row.FindControl("rfvCowMilkQuality");
        RequiredFieldValidator rfvCowTEMP = (RequiredFieldValidator)row.FindControl("rfvCowTEMP");
        RequiredFieldValidator rfvCowMilkQuantity = (RequiredFieldValidator)row.FindControl("rfvCowMilkQuantity");
        RequiredFieldValidator rfvCowFat = (RequiredFieldValidator)row.FindControl("rfvCowFat");
        RequiredFieldValidator rfvCowCLR = (RequiredFieldValidator)row.FindControl("rfvCowCLR");
        if (txtCowMilkQuantity.Text == "")
        {
            txtCowMilkQuantity.Text = "0";
        }
        if (chkSelect.Checked == true)
        {
            
            if (decimal.Parse(txtCowMilkQuantity.Text) > 0)
            {
                rfvCowMilkQuality.Enabled = true;
                rfvCowTEMP.Enabled = true;
                rfvCowMilkQuantity.Enabled = true;
                rfvCowFat.Enabled = true;
                rfvCowCLR.Enabled = true;
            }
            else
            {
                rfvCowMilkQuality.Enabled = false;
                rfvCowTEMP.Enabled = false;
                rfvCowMilkQuantity.Enabled = false;
                rfvCowFat.Enabled = false;
                rfvCowCLR.Enabled = false;
            }

        }
        else
        {
           
            rfvCowMilkQuality.Enabled = false;
            rfvCowTEMP.Enabled = false;
            rfvCowMilkQuantity.Enabled = false;
            rfvCowFat.Enabled = false;
            rfvCowCLR.Enabled = false;

        }
        SetFocus(txtCowFat);
    }
    protected void txtCowFat_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox txtCowMilkQuantity = (TextBox)row.FindControl("txtCowMilkQuantity");
        TextBox txtCowFat = (TextBox)row.FindControl("txtCowFat");
        TextBox txtCowCLR = (TextBox)row.FindControl("txtCowCLR");
        TextBox txtCowSNF = (TextBox)row.FindControl("txtCowSNF");
        TextBox txtCowFatinkg = (TextBox)row.FindControl("txtCowFatinkg");
        TextBox txtCowSnfinkg = (TextBox)row.FindControl("txtCowSnfinkg");
        txtCowSNF.Text = GetSNF(txtCowFat.Text, txtCowCLR.Text).ToString();
        if (txtCowMilkQuantity.Text != "" && txtCowFat.Text != "")
        {
            txtCowFatinkg.Text = Obj_MC.GetSNFInKg(decimal.Parse(txtCowMilkQuantity.Text), decimal.Parse(txtCowFat.Text)).ToString();
        }
        else
        {
            txtCowFatinkg.Text = "";
        }
        if (txtCowMilkQuantity.Text != "" && txtCowSNF.Text != "")
        {
            txtCowSnfinkg.Text = Obj_MC.GetSNFInKg(decimal.Parse(txtCowMilkQuantity.Text), decimal.Parse(txtCowSNF.Text)).ToString();
        }
        else
        {
            txtCowSnfinkg.Text = "";
        }


        SetFocus(txtCowCLR);
    }
    protected void txtCowCLR_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox txtCowMilkQuantity = (TextBox)row.FindControl("txtCowMilkQuantity");
        TextBox txtCowFat = (TextBox)row.FindControl("txtCowFat");
        TextBox txtCowCLR = (TextBox)row.FindControl("txtCowCLR");
        TextBox txtCowSNF = (TextBox)row.FindControl("txtCowSNF");
        TextBox txtCowtotalCan = (TextBox)row.FindControl("txtCowtotalCan");
        TextBox txtCowFatinkg = (TextBox)row.FindControl("txtCowFatinkg");
        TextBox txtCowSnfinkg = (TextBox)row.FindControl("txtCowSnfinkg");
        txtCowSNF.Text = GetSNF(txtCowFat.Text, txtCowCLR.Text).ToString();
        if (txtCowMilkQuantity.Text != "" && txtCowFat.Text != "")
        {
            txtCowFatinkg.Text = Obj_MC.GetSNFInKg(decimal.Parse(txtCowMilkQuantity.Text), decimal.Parse(txtCowFat.Text)).ToString();
        }
        else
        {
            txtCowFatinkg.Text = "";
        }
        if (txtCowMilkQuantity.Text != "" && txtCowSNF.Text != "")
        {
            txtCowSnfinkg.Text = Obj_MC.GetSNFInKg(decimal.Parse(txtCowMilkQuantity.Text), decimal.Parse(txtCowSNF.Text)).ToString();
        }
        else
        {
            txtCowSnfinkg.Text = "";
        }


        SetFocus(txtCowtotalCan);
    }


    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {

        GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
        TextBox txtBufMilkQuantity = (TextBox)row.FindControl("txtBufMilkQuantity");
        TextBox txtCowMilkQuantity = (TextBox)row.FindControl("txtCowMilkQuantity");
        RequiredFieldValidator rfvBufMilkQuality = (RequiredFieldValidator)row.FindControl("rfvBufMilkQuality");
        RequiredFieldValidator rfvtxtBufTEMP = (RequiredFieldValidator)row.FindControl("rfvtxtBufTEMP");
        RequiredFieldValidator rfvBufMilkQuantity = (RequiredFieldValidator)row.FindControl("rfvBufMilkQuantity");
        RequiredFieldValidator rfvBufFat = (RequiredFieldValidator)row.FindControl("rfvBufFat");
        RequiredFieldValidator rfvBufCLR = (RequiredFieldValidator)row.FindControl("rfvBufCLR");
        RequiredFieldValidator rfvCowMilkQuality = (RequiredFieldValidator)row.FindControl("rfvCowMilkQuality");
        RequiredFieldValidator rfvCowTEMP = (RequiredFieldValidator)row.FindControl("rfvCowTEMP");
        RequiredFieldValidator rfvCowMilkQuantity = (RequiredFieldValidator)row.FindControl("rfvCowMilkQuantity");
        RequiredFieldValidator rfvCowFat = (RequiredFieldValidator)row.FindControl("rfvCowFat");
        RequiredFieldValidator rfvCowCLR = (RequiredFieldValidator)row.FindControl("rfvCowCLR");

        if(txtBufMilkQuantity.Text == "")
        {
            txtBufMilkQuantity.Text = "0";
        }
        if (txtCowMilkQuantity.Text == "")
        {
            txtCowMilkQuantity.Text = "0";
        }
        if (chkSelect.Checked == true)
        {
            if (decimal.Parse(txtBufMilkQuantity.Text) > 0)
            {
                rfvBufMilkQuality.Enabled = true;
                rfvtxtBufTEMP.Enabled = true;
                rfvBufMilkQuantity.Enabled = true;
                rfvBufFat.Enabled = true;
                rfvBufCLR.Enabled = true;
            }
            else
            {
                rfvBufMilkQuality.Enabled = false;
                rfvtxtBufTEMP.Enabled = false;
                rfvBufMilkQuantity.Enabled = false;
                rfvBufFat.Enabled = false;
                rfvBufCLR.Enabled = false;
            }
            if (decimal.Parse(txtCowMilkQuantity.Text) > 0)
            {
                rfvCowMilkQuality.Enabled = true;
                rfvCowTEMP.Enabled = true;
                rfvCowMilkQuantity.Enabled = true;
                rfvCowFat.Enabled = true;
                rfvCowCLR.Enabled = true;
            }
            else
            {
                rfvCowMilkQuality.Enabled = false;
                rfvCowTEMP.Enabled = false;
                rfvCowMilkQuantity.Enabled = false;
                rfvCowFat.Enabled = false;
                rfvCowCLR.Enabled = false;
            }
            
        }
        else
        {
            rfvBufMilkQuality.Enabled = false;
            rfvtxtBufTEMP.Enabled = false;
            rfvBufMilkQuantity.Enabled = false;
            rfvBufFat.Enabled = false;
            rfvBufCLR.Enabled = false;
            rfvCowMilkQuality.Enabled = false;
            rfvCowTEMP.Enabled = false;
            rfvCowMilkQuantity.Enabled = false;
            rfvCowFat.Enabled = false;
            rfvCowCLR.Enabled = false;
           
        }
        SetFocus(chkSelect);
    }
}
