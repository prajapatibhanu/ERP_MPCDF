using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;
using System.Web.UI;

public partial class mis_MilkCollection_BMCMilkDispatch : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    MilkCalculationClass Obj_MC = new MilkCalculationClass();

    decimal grQtyTotal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!IsPostBack)
            {

                //if (Session["IsSuccess"] != null)
                //{
                //    if ((Boolean)Session["IsSuccess"] == true)
                //    {
                //        Session["IsSuccess"] = false;
                //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                //    }
                //}

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();

                DateTime currentDate = DateTime.Now;
                //currentDate = currentDate.AddDays();
                //txtDate.Text = currentDate.ToString("dd/MM/yyyy");
                //txtDate.Attributes.Add("readonly", "readonly");
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDispatchTime.Text = DateTime.Now.ToString("hh:mm tt");
                //txtDate.Enabled = false;
                FillSociety(sender, e);
                MilkDispatchDetail();
                txtDate_TextChanged(sender, e);

                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        lblMsg.Text = "";
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Milk Dispatch Process Successfully Completed');", true);
                    }
                }

            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        if (txtDate.Text != "")
        {

            ddlReferenceNo.Items.Clear();
            ddlReferenceNo.Items.Insert(0, new ListItem("Select", "0"));
            BindReferenceNo();
			MilkDispatchDetail();
            //ddlReferenceNo_SelectedIndexChanged(sender, e); 
        }
        else
        {
            ddlReferenceNo.Items.Clear();
            ddlReferenceNo.Items.Insert(0, new ListItem("Select", "0"));
        }

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    protected void ddlShift_Init(object sender, EventArgs e)
    {
        try
        {
            ddlShift.SelectedValue = "Morning";
            //DataSet dsct = objdb.ByProcedure("USP_GetServerDatetime",
            //                 new string[] { "flag" },
            //                 new string[] { "1" }, "dataset");

            //string currrentime = dsct.Tables[0].Rows[0]["currentTime"].ToString();

            //string[] s = currrentime.Split(':');

            //if ((Convert.ToInt32(s[0]) >= 0 && Convert.ToInt32(s[0]) <= 12 && ((Convert.ToInt32(s[0]) == 0 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 12 && Convert.ToInt32(s[1]) <= 59))))
            //{
            //    ddlShift.SelectedValue = "Morning";
            //}
            //else
            //{
            //    ddlShift.SelectedValue = "Evening";
            //}

            ddlShift.Enabled = false;


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void FillSociety(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "Office_ID" },
                              new string[] { "10", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count != 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtsocietyName.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                    txtBlock.Text = ds.Tables[0].Rows[0]["Block_Name"].ToString();
                    txtBlock.Enabled = false;

                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    ddlDCS.DataTextField = "Office_Name";
                    ddlDCS.DataValueField = "Office_ID";
                    ddlDCS.DataSource = ds.Tables[1];
                    ddlDCS.DataBind();

                }

            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    private void BindReferenceNo()
    {

        DataSet ds1 = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New",
                                   new string[] { "flag", "I_OfficeID" },
                                   new string[] { "4", objdb.Office_ID() }, "dataset");
        if (ds1.Tables[0].Rows.Count > 0)
        {
            //DataTable dt = new DataTable();
            //DataRow dr;
            //dt.Columns.Add(new DataColumn("C_ReferenceNo", typeof(string)));
            //dt.Columns.Add(new DataColumn("BI_MilkInOutRefID", typeof(string)));

            //for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            //{
            //    if (ds1.Tables[0].Rows[i]["I_EntryID"].ToString() == "" && ds1.Tables[0].Rows[i]["I_OfficeID"].ToString() == objdb.Office_ID())
            //    {
            //        dr = dt.NewRow();
            //        dr[0] = ds1.Tables[0].Rows[i]["C_ReferenceNo"].ToString();
            //        dr[1] = ds1.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString();
            //        dt.Rows.Add(dr);
            //    }
            //}

            ddlReferenceNo.DataSource = ds1;
            ddlReferenceNo.DataTextField = "C_ReferenceNo";
            ddlReferenceNo.DataValueField = "BI_MilkInOutRefID";
            ddlReferenceNo.DataBind();
            ddlReferenceNo.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {
            ddlReferenceNo.DataSource = string.Empty;
            ddlReferenceNo.DataBind();
            ddlReferenceNo.Items.Insert(0, new ListItem("Select", "0"));
        }


    }

    protected void ddlReferenceNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["InsertRecordMilkQuality"] = null;
            ViewState["InsertRecord1"] = null;
            gv_SealInfo.DataSource = string.Empty;
            gv_SealInfo.DataBind();
            btnSubmit.Enabled = false;
            divCollectionDetail.Visible = false;
            
            divmilkdispatch.Visible = false;
            divsealdetail.Visible = false;
            ddlMilkQuality.ClearSelection();
            txtV_DriverName.Text = "";
            txtV_DriverMobileNo.Text = "";
            txtV_VehicleNo.Text = "";
            txttemperature.Text = "";
            txtMilkQuantity.Text = "";
            lblD_VehicleCapacity.Text = "";
            txtNetFat.Text = "";
            txtNetCLR.Text = "";
            txtScaleReading.Text = "";
            txtSampalNo.Text = "";

            


            if (ddlReferenceNo.SelectedIndex != 0)
            {
                ds = null;
                ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New",
                                new string[] { "flag", "BI_MilkInOutRefID" },
                                new string[] { "5", ddlReferenceNo.SelectedValue }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtV_DriverName.Text = ds.Tables[0].Rows[0]["V_DriverName"].ToString();
                        txtV_DriverMobileNo.Text = ds.Tables[0].Rows[0]["V_DriverMobileNo"].ToString();
                        txtV_VehicleNo.Text = ds.Tables[0].Rows[0]["V_VehicleNo"].ToString();
                        txtV_TesterName.Text = ds.Tables[0].Rows[0]["V_TesterName"].ToString();
                        txtV_TesterMobileNo.Text = ds.Tables[0].Rows[0]["V_TesterMobileNo"].ToString();
                        ddlTankerType.ClearSelection();
                        ddlTankerType.Items.FindByText(ds.Tables[0].Rows[0]["TType"].ToString()).Selected=true;
                        //lblD_VehicleCapacity.Text = ds.Tables[1].Rows[0]["D_VehicleCapacity"].ToString();
                        ddlTankerType_SelectedIndexChanged(sender, e);
                        GetShiftwiseData(sender, e);

                        divCollectionDetail.Visible = true;
                        
                        divmilkdispatch.Visible = true;
                        divsealdetail.Visible = true;

                      //  ddlAdulterationTest_SelectedIndexChanged(sender, e);

                    }
                    else
                    {
                        //btnYes.Enabled = false;
                        divCollectionDetail.Visible = false;
                        
                        divmilkdispatch.Visible = false;
                        divsealdetail.Visible = false;
                        ddlMilkQuality.ClearSelection();
                        txtV_DriverName.Text = "";
                        txtV_DriverMobileNo.Text = "";
                        txtV_VehicleNo.Text = "";
                        txttemperature.Text = "";
                        txtMilkQuantity.Text = "";
                        lblD_VehicleCapacity.Text = "";
                        txtNetFat.Text = "";
                        txtNetCLR.Text = "";
                        txtScaleReading.Text = "";
                        txtSampalNo.Text = "";

                    }
                }
                else
                {
                    //btnYes.Enabled = false;
                    divCollectionDetail.Visible = false;
                    
                    divmilkdispatch.Visible = false;
                    divsealdetail.Visible = false;
                    ddlMilkQuality.ClearSelection();
                    txtV_DriverName.Text = "";
                    txtV_DriverMobileNo.Text = "";
                    txtV_VehicleNo.Text = "";
                    txttemperature.Text = "";
                    txtMilkQuantity.Text = "";
                    lblD_VehicleCapacity.Text = "";
                    txtNetFat.Text = "";
                    txtNetCLR.Text = "";
                    txtScaleReading.Text = "";
                    txtSampalNo.Text = "";

                }
            }
            else
            {
                //btnYes.Enabled = false;
                divCollectionDetail.Visible = false;
                
                divmilkdispatch.Visible = false;
                divsealdetail.Visible = false;
                ddlMilkQuality.ClearSelection();
                txtV_DriverName.Text = "";
                txtV_DriverMobileNo.Text = "";
                txtV_VehicleNo.Text = "";
                txttemperature.Text = "";
                txtMilkQuantity.Text = "";
                lblD_VehicleCapacity.Text = "";
                txtNetFat.Text = "";
                txtNetCLR.Text = "";
                txtScaleReading.Text = "";
                txtSampalNo.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }

    protected void ddlAdulterationTest_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdulterationTest.SelectedValue == "Yes")
            {
                milktestdetail.Visible = true;
                //BindAdulterationTestGrid();

            }
            else
            {
                milktestdetail.Visible = false;
                gvmilkAdulterationtestdetail.DataSource = string.Empty;
                gvmilkAdulterationtestdetail.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
        }
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
                                   new string[] { "1" }, "dataset");

            if (DSAT.Tables[0].Rows.Count != 0)
            {
                dtTL.Columns.Add(new DataColumn("S.No", typeof(int)));
                dtTL.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
                dtTL.Columns.Add(new DataColumn("V_SealLocationAlias", typeof(string)));
                dtTL.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));

                for (int i = 0; i < 1; i++)
                {
                    for (int j = 0; j < DSAT.Tables[0].Rows.Count; j++)
                    {
                        drTL = dtTL.NewRow();
                        drTL[0] = dtTL.Rows.Count + 1;
                        drTL[1] = ddlCompartmentType.SelectedValue;
                        drTL[2] = ddlCompartmentType.SelectedItem.Text;
                        drTL[3] = DSAT.Tables[0].Rows[j]["V_AdulteratioTName"].ToString();
                        dtTL.Rows.Add(drTL);
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

    private void GetShiftwiseData(object sender, EventArgs e)
    {
        try
        {

            DataSet dsct = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New",
                             new string[] { "flag", "OfficeId", "D_Date" },
                             new string[] { "6", objdb.Office_ID(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy-MM-dd")}, "dataset");

            if (dsct != null)
            {
                if (dsct.Tables.Count > 0)
                {
                    if (dsct.Tables[0].Rows.Count > 0 || dsct.Tables[1].Rows.Count > 0)
                    {
                        gv_ViewSelfMilkCollection.DataSource = dsct.Tables[0];
                        gv_ViewSelfMilkCollection.DataBind();


                        decimal MilkQty_InKG = 0;
                        foreach (GridViewRow row in gv_ViewSelfMilkCollection.Rows)
                        {
                            Label lblI_MilkSupplyQty = (Label)row.FindControl("lblI_MilkSupplyQty");

                            if (lblI_MilkSupplyQty.Text != "")
                            {
                                MilkQty_InKG += Convert.ToDecimal(lblI_MilkSupplyQty.Text);
                                Label lblI_MilkSupplyQtyTotal = (gv_ViewSelfMilkCollection.FooterRow.FindControl("lblI_MilkSupplyQtyTotal") as Label);
                                lblI_MilkSupplyQtyTotal.Text = MilkQty_InKG.ToString("0.0");
                            }

                        }

                        // Bind DCS Received Milk

                        gv_dcsmilkreceive.DataSource = dsct.Tables[1];
                        gv_dcsmilkreceive.DataBind();
                        decimal RecvMilkQty_InKG = 0;
                        foreach (GridViewRow row in gv_dcsmilkreceive.Rows)
                        {
                            Label lblI_MilkSupplyQty = (Label)row.FindControl("lblI_MilkSupplyQty");

                            if (lblI_MilkSupplyQty.Text != "")
                            {
                                RecvMilkQty_InKG += Convert.ToDecimal(lblI_MilkSupplyQty.Text);
                                Label lblI_MilkSupplyQtyTotal = (gv_dcsmilkreceive.FooterRow.FindControl("lblI_MilkSupplyQtyTotal") as Label);
                                lblI_MilkSupplyQtyTotal.Text = RecvMilkQty_InKG.ToString("0.0");
                            }

                        }
                        DataTable dt = dsct.Tables[0];
                        ViewState["dt"] = dt;


                        DataTable dt1 = dsct.Tables[1];
                        ViewState["dt1"] = dt1;
                       

                        

                        ddlMilkDispatchtype_SelectedIndexChanged(sender, e);
                        ddlMilkDispatchtype.Enabled = false;
                        ddlDCS.Enabled = false;

                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "At Present Milk Quantity Showing is 0 Value So Can't Dispatch Milk!");
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "At Present Milk Quantity Showing is 0 Value So Can't Dispatch Milk!");
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "At Present Milk Quantity Showing is 0 Value So Can't Dispatch Milk!");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    protected void gv_dcsmilkreceive_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
          //decimal tmpTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "MilkQuantity").ToString());
          //grQtyTotal += tmpTotal;

          //if (e.Row.RowIndex > 0)
          //{
          //    GridViewRow previousRow = gv_dcsmilkreceive.Rows[e.Row.RowIndex - 1];
          //    if (e.Row.Cells[0].Text == previousRow.Cells[0].Text)
          //    {
          //        if (previousRow.Cells[0].RowSpan == 0)
          //        {
          //            previousRow.Cells[0].RowSpan += 2;
          //            e.Row.Cells[0].Visible = false;
          //        }
          //    }
          //}
      }

      //if (e.Row.RowType == DataControlRowType.Footer)
      //{
      //    Label lblTotalqty = (Label)e.Row.FindControl("lblTotalqty");
      //    lblTotalqty.Text = grQtyTotal.ToString();
      //}

    }

    protected void gv_ViewSelfMilkCollection_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int RowSpan = 2;
        for (int i = gv_ViewSelfMilkCollection.Rows.Count - 2; i >= 0; i--)
        {
            GridViewRow currRow = gv_ViewSelfMilkCollection.Rows[i];
            GridViewRow prevRow = gv_ViewSelfMilkCollection.Rows[i + 1];
            if (currRow.Cells[0].Text == prevRow.Cells[0].Text)
            {
                currRow.Cells[0].RowSpan = RowSpan;
                prevRow.Cells[0].Visible = false;
                RowSpan += 1;
            }
            else
            {
                RowSpan = 2;
            }
        }
    }

    protected void ddlMilkDispatchtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (ddlMilkDispatchtype.SelectedValue == "Tanker")
            {
                divsealdetail.Visible = true;
            }
            else
            {
                divsealdetail.Visible = false;
            }

            if (txtMilkQuantity.Text != "")
            {

                if (Convert.ToDecimal(txtMilkQuantity.Text) <= 0)
                {
                    btnTankerSealDetails.Enabled = false;
                    btnTankerSealDetails.ToolTip = "At Present Milk Quantity Showing is Negative Value So Can't Dispatch!";
                    btnSubmit.Enabled = false;
                    btnSubmit.ToolTip = "At Present Milk Quantity Showing is Negative Value So Can't Dispatch!";
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "At Present Milk Quantity Showing is Negative Value So Can't Dispatch Milk!");
                }

                else
                {
                    lblMsg.Text = "";
                    btnTankerSealDetails.Enabled = true;
                }
            }
            else
            {
                lblMsg.Text = "";
                //lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "At Present Milk Quantity Showing is 0 Value So Can't Dispatch Milk!");

            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error-:" + ex.Message.ToString());
            Session["IsSuccess"] = false;
        }
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
            ddlSealColor.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {
            ddlSealColor.DataSource = string.Empty;
            ddlSealColor.DataBind();
            ddlSealColor.Items.Insert(0, new ListItem("Select", "0"));
        }
    }

    protected void btnTankerSealDetails_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        AddSealDetails();
        //btnSubmit.Enabled = true;
        MilkDispatchDetail();
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
                dt1.Columns.Add(new DataColumn("Sealtype", typeof(string)));
                dt1.Columns.Add(new DataColumn("ChamberType", typeof(string)));
                dt1.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));
                dt1.Columns.Add(new DataColumn("V_SealColor", typeof(string)));
                dt1.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
                dt1.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));

                dr1 = dt1.NewRow();
                dr1[0] = 1;
                dr1[1] = ddlSealtype.SelectedValue;
                dr1[2] = ddlChamberType.SelectedValue;
                dr1[3] = ddlSealColor.SelectedValue;
                dr1[4] = ddlSealColor.SelectedItem;
                dr1[5] = txtV_SealNo.Text;
                dr1[6] = txtV_SealRemark.Text;
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
                dt1.Columns.Add(new DataColumn("Sealtype", typeof(string)));
                dt1.Columns.Add(new DataColumn("ChamberType", typeof(string)));
                dt1.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));
                dt1.Columns.Add(new DataColumn("V_SealColor", typeof(string)));
                dt1.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
                dt1.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));

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
                    dr1[1] = ddlSealtype.SelectedValue;
                    dr1[2] = ddlChamberType.SelectedValue;
                    dr1[3] = ddlSealColor.SelectedValue;
                    dr1[4] = ddlSealColor.SelectedItem;
                    dr1[5] = txtV_SealNo.Text;
                    dr1[6] = txtV_SealRemark.Text;
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
            ddlSealtype.SelectedIndex = -1;
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

            ddlSealtype.SelectedIndex = -1;
            ddlChamberType.SelectedIndex = -1;
            ddlSealColor.SelectedIndex = -1;
            txtV_SealNo.Text = "";
            txtV_SealRemark.Text = "";

            //DataTable dtdeletecc = ViewState["InsertRecord1"] as DataTable;

            //if (dtdeletecc.Rows.Count == 0)
            //{
            //    btnSubmit.Enabled = false;
            //}

            MilkDispatchDetail();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    private DataTable GetSealGridvalue()
    {

        DataTable dtseal = new DataTable();
        DataRow drseal;

        dtseal.Columns.Add(new DataColumn("Sealtype", typeof(string)));
        dtseal.Columns.Add(new DataColumn("ChamberType", typeof(string)));
        dtseal.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));
        dtseal.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
        dtseal.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));

        foreach (GridViewRow rowseal in gv_SealInfo.Rows)
        {
            Label lblSealtype = (Label)rowseal.FindControl("lblSealtype");
            Label lblChamberType = (Label)rowseal.FindControl("lblChamberType");
            Label lblTI_SealColor = (Label)rowseal.FindControl("lblTI_SealColor");
            Label lblV_SealNo = (Label)rowseal.FindControl("lblV_SealNo");
            Label lblV_SealRemark = (Label)rowseal.FindControl("lblV_SealRemark");

            drseal = dtseal.NewRow();
            drseal[0] = lblSealtype.Text;
            drseal[1] = lblChamberType.Text;
            drseal[2] = lblTI_SealColor.Text;
            drseal[3] = lblV_SealNo.Text;
            drseal[4] = lblV_SealRemark.Text;

            dtseal.Rows.Add(drseal);
        }
        return dtseal;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("BMCMilkDispatch.aspx", false);
    }

    protected void MilkDispatchDetail()
    {
        try
        {
            // lblMsg.Text = ""; 
            //if (ddlShift.SelectedIndex > 0)
            //{ }
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                     new string[] { "flag", "FDate", "OfficeId" },
                     new string[] { "4", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy-MM-dd"), objdb.Office_ID() }, "dataset");


            if (ds.Tables.Count != 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnSubmit.Enabled = false;
                    btnSubmit.ToolTip = "Only 1 Time Dispatch Alllow In 1 Day or Shift";

                    div_milkdetails.Visible = true;
                    GrdDispatchDetails.DataSource = ds.Tables[0];
                    GrdDispatchDetails.DataBind();
                    

                    txtNetFat.Text = "";
                    txtnetsnf.Text = "";
                    txtNetCLR.Text = "";
                    txtMilkQuantity.Text = "";

                }
                else
                {
                    btnSubmit.Enabled = true;
                    div_milkdetails.Visible = false;

                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void txtMilkQuantity_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            //decimal TC = Convert.ToDecimal(lblD_VehicleCapacity.Text);
            decimal TC = 5000;
            if (txtMilkQuantity.Text != "")
            {
                if (Convert.ToDecimal(txtMilkQuantity.Text) > TC)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Entered Milk Quantity in [" + txtMilkQuantity.Text + " KG] will not accepted more than [" + TC + " KG] in Tanker");
                    txtMilkQuantity.Text = "";
                }
                else
                {
                    txttemperature.Focus();
                }
            }
            else
            {
                lblMsg.Text = "";
                SetFocus(txtMilkQuantity);
            }
            GetShiftwiseData(sender, e);
            GetEntry();
            SetFocus(txtNetFat);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
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
        return Math.Round(snf, 2);
    }

    protected void txtNetFat_TextChanged(object sender, EventArgs e)
    {
        txtnetsnf.Text = GetSNF().ToString();

        if (txtNetCLR.Text == "" || txtNetFat.Text == "")
        {
            txtnetsnf.Text = "";
        }

        if (txtNetCLR.Text == "")
        {
            txtNetCLR.Focus();
        }

        if (txtNetFat.Text == "")
        {
            txtNetFat.Focus();
        }
        GetShiftwiseData(sender, e);
        GetEntry();
        SetFocus(txtNetCLR);
    }

    protected void txtNetCLR_TextChanged(object sender, EventArgs e)
    {
        txtnetsnf.Text = GetSNF().ToString();

        if (txtNetCLR.Text == "" || txtNetFat.Text == "")
        {
            txtnetsnf.Text = "";
        }

        if (txtNetCLR.Text == "")
        {
            txtNetCLR.Focus();
        }

        if (txtNetFat.Text == "")
        {
            txtNetFat.Focus();
        }
        GetShiftwiseData(sender,e);
        GetEntry();
        SetFocus(txttemperature);
    }

    public string BMC_ReferenceRunTimeCancelStatus()
    {
        string ReferenceRunTimeCancelStatus = "0";

        try
        {
            DataSet dsReferenceRunTimeCancelStatus = objdb.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                               new string[] { "flag", "BI_MilkInOutRefID" },
                               new string[] { "18", ddlReferenceNo.SelectedValue }, "dataset");

            if (dsReferenceRunTimeCancelStatus != null)
            {
                if (dsReferenceRunTimeCancelStatus.Tables.Count > 0)
                {
                    if (dsReferenceRunTimeCancelStatus.Tables[0].Rows.Count > 0)
                    {
                        ReferenceRunTimeCancelStatus = dsReferenceRunTimeCancelStatus.Tables[0].Rows[0]["RefCancelStatus"].ToString();

                        return ReferenceRunTimeCancelStatus;
                    }
                    else
                    {
                        return ReferenceRunTimeCancelStatus;
                    }
                }
                else
                {
                    return ReferenceRunTimeCancelStatus;
                }
            }
            else
            {
                return ReferenceRunTimeCancelStatus;
            }

        }
        catch (Exception ex)
        {

            return ReferenceRunTimeCancelStatus;
        }


    }

    public string BMC_ChallanRunTimeValidation()
    {
        string ChallanRunTimeValidation = "0";

        try
        {
            DataSet dsChallanRunTimeValidation = objdb.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                               new string[] { "flag", "BI_MilkInOutRefID", "OfficeId" },
                               new string[] { "19", ddlReferenceNo.SelectedValue, objdb.Office_ID() }, "dataset");

            if (dsChallanRunTimeValidation != null)
            {
                if (dsChallanRunTimeValidation.Tables.Count > 0)
                {
                    if (dsChallanRunTimeValidation.Tables[0].Rows.Count > 0)
                    {
                        ChallanRunTimeValidation = dsChallanRunTimeValidation.Tables[0].Rows[0]["Status"].ToString();

                        return ChallanRunTimeValidation;
                    }
                    else
                    {
                        return ChallanRunTimeValidation;
                    }
                }
                else
                {
                    return ChallanRunTimeValidation;
                }
            }
            else
            {
                return ChallanRunTimeValidation;
            }

        }
        catch (Exception ex)
        {

            return ChallanRunTimeValidation;
        }


    }

    public string BMC_ChallanRunTime_GrossWeight_Validation()
    {
        string ReferenceGrossWeight = "0";

        try
        {

            DataSet dsReferenceGrossWeight = objdb.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                               new string[] { "flag", "BI_MilkInOutRefID" },
                               new string[] { "20", ddlReferenceNo.SelectedValue }, "dataset");

            if (dsReferenceGrossWeight != null)
            {
                if (dsReferenceGrossWeight.Tables.Count > 0)
                {
                    if (dsReferenceGrossWeight.Tables[0].Rows.Count > 0)
                    {
                        ReferenceGrossWeight = dsReferenceGrossWeight.Tables[0].Rows[0]["Status"].ToString();

                        return ReferenceGrossWeight;
                    }
                    else
                    {
                        return ReferenceGrossWeight;
                    }
                }
                else
                {
                    return ReferenceGrossWeight;
                }
            }
            else
            {
                return ReferenceGrossWeight;
            }

        }
        catch (Exception)
        {

            return ReferenceGrossWeight;
        }
    }

    private DataTable GetAdulterationTestDetails()
    {

        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
        dt.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));
        dt.Columns.Add(new DataColumn("V_AdulterationValue", typeof(string)));

        foreach (GridViewRow row in gvmilkAdulterationtestdetail.Rows)
        {
            Label lblSealLocation = (Label)row.FindControl("lblSealLocation");
            Label lblAdulterationType = (Label)row.FindControl("lblAdulterationType");
            DropDownList ddlAdelterationTestValue = (DropDownList)row.FindControl("ddlAdelterationTestValue");

            string SealLocation = "";
            switch (lblSealLocation.Text)
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

            dr = dt.NewRow();
            dr[0] = SealLocation;
            dr[1] = lblAdulterationType.Text;
            dr[2] = ddlAdelterationTestValue.SelectedValue;
            dt.Rows.Add(dr);
        }
        return dt;
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {

     try
     {
         if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
         {

             // Run Time Reference Cancel Validation

             if (ddlReferenceNo.SelectedIndex > 0)
             {
                 string ReferenceRunTimeCancelStatus = BMC_ReferenceRunTimeCancelStatus();

                 if (ReferenceRunTimeCancelStatus == "0")
                 {

                 }
                 else
                 {

                     lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Oops!", "Reference No - " + ddlReferenceNo.SelectedItem.Text + " Gatepass Already Canceled. Please Check Again and Submit Details!");
                     MilkDispatchDetail();
                     return;
                 }
             }
             else
             {
                 lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Oops!", "Please select Reference Number.!");
                 return;
             }

             // Run Time Challan Already Created

             if (ddlReferenceNo.SelectedIndex > 0)
             {
                 string ChallanRunTimeValidation = BMC_ChallanRunTimeValidation();

                 if (ChallanRunTimeValidation == "0")
                 {

                 }
                 else
                 {

                     lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Oops!", "Reference No - " + ddlReferenceNo.SelectedItem.Text + " Challan Number Already Generated!");
                     MilkDispatchDetail();
                     return;
                 }
             }
             else
             {
                 lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Oops!", "Please select Reference Number.!");
                 return;
             }

             // Check Gross Weight Enter Or Not

             if (ddlReferenceNo.SelectedIndex > 0)
             {
                 string strtR = BMC_ChallanRunTime_GrossWeight_Validation();

                 if (strtR == "0")
                 {

                 }
                 else
                 {

                     lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Oops!", "Reference No. " + ddlReferenceNo.SelectedItem.Text + " Gross Weight Already Updated, So You Can't Generate Challan!");
                     MilkDispatchDetail();
                     return;
                 }
             }
             else
             {
                 lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Oops!", "Please select Reference Number.!");
                 return;
             }


             lblMsg.Text = "";
             int I_TotalCans = 0;

             //if (txtSaleMilkQuantity.Text == "")
             //{
             //    txtSaleMilkQuantity.Text = "0.00";
             //}
             //if (txtMRPRate.Text == "")
             //{
             //    txtMRPRate.Text = "0.00";
             //}
             //if (txtNetAmount.Text == "")GetMilkQuality
             //{
             //    txtNetAmount.Text = "0.00";
             //}



             DataTable dtMquality = new DataTable();
             dtMquality = GetMilkQuality();
             //if (ViewState["InsertRecordMilkQuality"] != null)
             //{
             //    dtMquality = (DataTable)ViewState["InsertRecordMilkQuality"];
             //}
             //else
             //{
             //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Quality Data Can't Empty");
             //    return;
             //}

             DataTable dtsealF = new DataTable();
             dtsealF = GetSealGridvalue();

             if (ddlMilkDispatchtype.SelectedValue == "Tanker")
             {
                 if (dtsealF.Rows.Count > 0)
                 {

                 }
                 else
                 {
                     lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "In Case Of Tanker - First To Fill At Least One Tanker Valve Seal Details");
                     return;
                 }
             }

             // For Adultration Test

             DataTable dtAdultration = new DataTable();
             if (ddlAdulterationTest.SelectedValue == "Yes")
             {
                 dtAdultration = GetAdulterationTestDetails();

                 if (dtAdultration.Rows.Count > 0)
                 {

                 }
                 else
                 {
                     lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "In Adulteration Test Yes - The All Adulteration Field Is Mandatory.");
                     return;
                 }
             }
             else
             {
                 dtAdultration.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
                 dtAdultration.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));
                 dtAdultration.Columns.Add(new DataColumn("V_AdulterationValue", typeof(string)));
             }



            

             // Runtime Validation

             DataSet dsValidationRuntime = objdb.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                  new string[] { "flag", "DT_Date", "I_OfficeID", "BI_MilkInOutRefID" },
                  new string[] { "3", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy-MM-dd"), objdb.Office_ID(), ddlReferenceNo.SelectedValue }, "dataset");

             if (dsValidationRuntime.Tables[0].Rows.Count > 0)
             {
                 lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Only 1 time Dispatch Alow For Every Day.");
                 return;

             }
             else
             {
                 DateTime ADate = Convert.ToDateTime(string.Concat(Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), " ", Convert.ToDateTime(txtArrivalTime.Text, cult).ToString("hh:mm:ss tt")), cult);
                 DateTime DDate = Convert.ToDateTime(string.Concat(Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), " ", Convert.ToDateTime(txtDispatchTime.Text, cult).ToString("hh:mm:ss tt")), cult);

                 if (ADate < DDate)
                 {

                     ds = null;
                     ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                             new string[] { "flag", 
                                             "I_OfficeID",
                                             "I_OfficeTypeID",
                                             "AttachedToCC",
                                             "V_MilkDispatchType",
                                             "I_TotalCans",
                                             "V_VehicleNo",
                                             "V_DriverName",
                                             "V_DriverMobileNo",
                                             "V_EntryType",
                                             "D_MilkQuality",
                                             "V_Shift",
                                             "D_MilkQuantity",
                                             "FAT",
                                             "CLR",
                                             "SNF", 
                                             "I_CreatedByEmpID",
                                             "V_Remark",
                                             "V_EntryFrom",
                                             "V_Temp",
                                             "MilkSaleQty",
                                             "MilkSaleRatePerLtr",
                                             "MilkSaleAmount",
                                             "NetMilkQtyInKG",
                                             "NetFATInKG",
                                             "NetSNFInKG",
                                             "BI_MilkInOutRefID",
                                             "SampalNo",
                                             "ScaleReading",
                                             "DT_ArrivalDateTime",
                                             "DT_Date",
                                             "DT_DispatchDateTime",
                                             "AdulterationTestStatus"
                                             ,"CreatedByIP"
                                              },

                                             new string[] { "2",  
                                             objdb.Office_ID(),
                                             objdb.OfficeType_ID(), 
                                             ddlDCS.SelectedValue,
                                             ddlCompartmentType.SelectedValue,
                                             I_TotalCans.ToString(),
                                             txtV_VehicleNo.Text,
                                             txtV_DriverName.Text,
                                             txtV_DriverMobileNo.Text,
                                             "Out",
                                             ddlMilkQuality.SelectedItem.Text,
                                             ddlShift.SelectedValue,
                                             txtMilkQuantity.Text,
                                             txtNetFat.Text,
                                             txtNetCLR.Text,
                                             txtnetsnf.Text, 
                                             objdb.createdBy(),
                                             "",
                                             "Web",
                                             txttemperature.Text,
                                             "0.0",
                                             "0.0",
                                             "0.0",
                                             txtMilkQuantity.Text,
                                             "0.0",
                                             "0.0",
                                             ddlReferenceNo.SelectedValue,
                                             txtSampalNo.Text,
                                             txtScaleReading.Text,
                                             ADate.ToString("yyyy/MM/dd hh:mm:ss tt"),
                                             Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"),
                                             DDate.ToString("yyyy/MM/dd hh:mm:ss tt"),
                                             ddlAdulterationTest.SelectedItem.Text,
                                             objdb.GetLocalIPAddress()
                                             },
                                              new string[] { "type_Trn_TankerSealDetails_MCU", "type_MilkCollectionChallanEntry_New", "type_Trn_tblAdulterationTest_MCU" },
                                              new DataTable[] { dtsealF, dtMquality, dtAdultration}, "TableSave");

                     if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                     {
                         Session["IsSuccess"] = true;
                         Response.Redirect("BMCMilkDispatch.aspx", false);

                     }
                     else
                     {
                         lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                         Session["IsSuccess"] = false;
                     }

                 }
                 else
                 {
                     lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", "Warning : Tanker Arrival Date Time should be less then Dispatch date time!");
                     Session["IsSuccess"] = false;
                 }
             }

             Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
         }
     }

     catch (Exception ex)
     {
         lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

     }

    }

    protected void GetEntry()
     {
        try
        {
            
            lblMsg.Text = "";
            if (txtMilkQuantity.Text != "" && txtNetFat.Text != "" && txtNetCLR.Text != "" && ddlMilkQuality.SelectedValue != "0" && txttemperature.Text != "")
            {
                DataTable DT = new DataTable();
                DataRow dr;
                DT = (DataTable)ViewState["dt"];
                int Count = DT.Rows.Count;
                dr = DT.NewRow();
                
                dr[0] = Session["Office_ID"];
                dr[1] = Session["Office_Name"];
                
                dr[2] = ddlShift.SelectedValue;

                decimal SNF = GetGridSNF();
                decimal Tsnfinkg = GetGridSNF_InKG();
                decimal Tfatinkg = GetGridFAT_InKG();


                DataTable dt2 = new DataTable();
                dt2 = (DataTable)ViewState["dt1"];
                decimal Qty = 0;
                decimal Fat = 0;
                decimal Snf = 0;
                decimal Clr = 0;
                decimal kgFat = 0;
                decimal kgSnf = 0;
                foreach (DataRow tr in DT.Rows)
                {


                    Qty += decimal.Parse(tr["MilkQuantity"].ToString());
                    kgFat += decimal.Parse(tr["FatInKg"].ToString());
                    kgSnf += decimal.Parse(tr["SnfInKg"].ToString());
                    

                }
                foreach (DataRow tr in dt2.Rows)
                {


                    Qty += decimal.Parse(tr["MilkQuantity"].ToString());
                    kgFat += decimal.Parse(tr["FatInKg"].ToString());
                    kgSnf += decimal.Parse(tr["SnfInKg"].ToString());
                    //tr["kgFat"] = decimal.Parse(tr["kgFat"].ToString()) - fatinkg;
                    //tr["kgSnf"] = decimal.Parse(tr["kgSnf"].ToString()) - snfinkg;
                    ds = objdb.ByProcedure("MilkCalculation", new string[] { "flag", "MilkQtyInKg", "FATInPer" }, new string[] { "3", "591", txtNetFat.Text }, "dataset");





                }
               
                Qty = decimal.Parse(txtMilkQuantity.Text) - Qty;
                kgFat = Tfatinkg - kgFat;
                kgSnf = Tsnfinkg - kgSnf;
                 Fat = Math.Round((decimal.Parse(kgFat.ToString()) * 100 / (decimal.Parse(Qty.ToString()))), 1);

                Snf = Math.Round((decimal.Parse(kgSnf.ToString()) * 100) / (decimal.Parse(Qty.ToString())), 3);
                Clr = Math.Round(4 * (decimal.Parse(Snf.ToString()) - (Convert.ToDecimal(0.2) * decimal.Parse(Fat.ToString())) - Convert.ToDecimal(0.7)), 0);
                if (Fat >= 5.5M)
                {
                    dr[6] = "Buf";

                }
                else
                {
                    dr[6] = "Cow";
                }
                dr[3] = Qty;
                dr[4] = ddlMilkQuality.SelectedValue;
                dr[5] = txttemperature.Text ;
                dr[7] = Fat;
                dr[8] = Clr;
                dr[9] = Snf;
                dr[10] = kgFat;
                dr[11] = kgSnf;
                dr[12] = txtDate.Text;
                DT.Rows.Add(dr);
                
                gv_ViewSelfMilkCollection.DataSource = DT;
                gv_ViewSelfMilkCollection.DataBind();
                decimal MilkQty_InKG = 0;
                foreach (GridViewRow row in gv_ViewSelfMilkCollection.Rows)
                {
                    Label lblI_MilkSupplyQty = (Label)row.FindControl("lblI_MilkSupplyQty");

                    if (lblI_MilkSupplyQty.Text != "")
                    {
                        MilkQty_InKG += Convert.ToDecimal(lblI_MilkSupplyQty.Text);
                        Label lblI_MilkSupplyQtyTotal = (gv_ViewSelfMilkCollection.FooterRow.FindControl("lblI_MilkSupplyQtyTotal") as Label);
                        lblI_MilkSupplyQtyTotal.Text = MilkQty_InKG.ToString("0.0");
                    }

                }
            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }

    private decimal GetGridSNF()
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

    private decimal GetGridSNF_InKG()
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

    private decimal GetGridFAT_InKG()
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

    private DataTable GetMilkQuality()
    {

        DataTable dtMilkQuality = new DataTable();
        DataRow drMilkQuality;
        dtMilkQuality.Columns.Add(new DataColumn("EntryDate", typeof(string)));
        dtMilkQuality.Columns.Add(new DataColumn("Office_ID", typeof(int)));
        dtMilkQuality.Columns.Add(new DataColumn("AttachedBMC_ID", typeof(int)));
        dtMilkQuality.Columns.Add(new DataColumn("Shift", typeof(string)));
        dtMilkQuality.Columns.Add(new DataColumn("MilkType", typeof(string)));
        dtMilkQuality.Columns.Add(new DataColumn("Temp", typeof(string)));
        dtMilkQuality.Columns.Add(new DataColumn("MilkQuantity", typeof(decimal)));
        dtMilkQuality.Columns.Add(new DataColumn("MilkQuality", typeof(string)));
        dtMilkQuality.Columns.Add(new DataColumn("Fat", typeof(decimal)));
        dtMilkQuality.Columns.Add(new DataColumn("Snf", typeof(decimal)));
        dtMilkQuality.Columns.Add(new DataColumn("Clr", typeof(decimal)));
        dtMilkQuality.Columns.Add(new DataColumn("FatInKg", typeof(decimal)));
        dtMilkQuality.Columns.Add(new DataColumn("SnfInKg", typeof(decimal)));
        
        
   
        foreach (GridViewRow row in gv_ViewSelfMilkCollection.Rows)
        {
            Label lblOffice_Id = (Label)row.FindControl("lblOffice_Id");
            Label lblI_MilkSupplyQty = (Label)row.FindControl("lblI_MilkSupplyQty");
            Label lblMilkType = (Label)row.FindControl("lblMilkType");
            Label lblMilkQuality = (Label)row.FindControl("lblMilkQuality");
            Label lblTemp = (Label)row.FindControl("lblTemp");
            Label lblFatInKg = (Label)row.FindControl("lblFatInKg");
            Label lblSnfInKg = (Label)row.FindControl("lblSnfInKg");
            Label lblFAT_IN_Per = (Label)row.FindControl("lblFAT_IN_Per");
            Label lblCLR = (Label)row.FindControl("lblCLR");
            Label lblSNF_IN_Per = (Label)row.FindControl("lblSNF_IN_Per");
            Label lblV_Shift = (Label)row.FindControl("lblV_Shift");
            Label lblV_Date = (Label)row.FindControl("lblV_Date");

            drMilkQuality = dtMilkQuality.NewRow();
            drMilkQuality[0] = Convert.ToDateTime(lblV_Date.Text,cult).ToString("yyyy/MM/dd");
            drMilkQuality[1] = lblOffice_Id.Text;
            drMilkQuality[2] = objdb.Office_ID();
            drMilkQuality[3] = lblV_Shift.Text;
            drMilkQuality[4] = lblMilkType.Text;
            drMilkQuality[5] = lblTemp.Text;
            drMilkQuality[6] = lblI_MilkSupplyQty.Text;
            drMilkQuality[7] = lblMilkQuality.Text;        
            drMilkQuality[8] = lblFAT_IN_Per.Text;
            drMilkQuality[9] = lblSNF_IN_Per.Text;
            drMilkQuality[10] = lblCLR.Text;
            drMilkQuality[11] = lblFatInKg.Text;
            drMilkQuality[12] = lblSnfInKg.Text;
            
            dtMilkQuality.Rows.Add(drMilkQuality);
        }
        foreach (GridViewRow row1 in gv_dcsmilkreceive.Rows)
        {
            Label lblOffice_Id = (Label)row1.FindControl("lblOffice_Id");
            Label lblI_MilkSupplyQty = (Label)row1.FindControl("lblI_MilkSupplyQty");
            Label lblMilkType = (Label)row1.FindControl("lblMilkType");
            Label lblMilkQuality = (Label)row1.FindControl("lblMilkQuality");
            Label lblTemp = (Label)row1.FindControl("lblTemp");
            Label lblFatInKg = (Label)row1.FindControl("lblFatInKg");
            Label lblSnfInKg = (Label)row1.FindControl("lblSnfInKg");
            Label lblFAT_IN_Per = (Label)row1.FindControl("lblFAT_IN_Per");
            Label lblCLR = (Label)row1.FindControl("lblCLR");
            Label lblSNF_IN_Per = (Label)row1.FindControl("lblSNF_IN_Per");
            Label lblV_Shift = (Label)row1.FindControl("lblV_Shift");
            Label lblV_Date = (Label)row1.FindControl("lblV_Date");

            drMilkQuality = dtMilkQuality.NewRow();
            drMilkQuality[0] = Convert.ToDateTime(lblV_Date.Text, cult).ToString("yyyy/MM/dd");
            drMilkQuality[1] = lblOffice_Id.Text;
            drMilkQuality[2] = objdb.Office_ID();
            drMilkQuality[3] = lblV_Shift.Text;
            drMilkQuality[4] = lblMilkType.Text;
            drMilkQuality[5] = lblTemp.Text;
            drMilkQuality[6] = lblI_MilkSupplyQty.Text;
            drMilkQuality[7] = lblMilkQuality.Text;
            drMilkQuality[8] = lblFAT_IN_Per.Text;
            drMilkQuality[9] = lblSNF_IN_Per.Text;
            drMilkQuality[10] = lblCLR.Text;
            drMilkQuality[11] = lblFatInKg.Text;
            drMilkQuality[12] = lblSnfInKg.Text;

            dtMilkQuality.Rows.Add(drMilkQuality);
        }
        return dtMilkQuality;
    }
    protected void ddlMilkQuality_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetEntry();
        SetFocus(txtMilkQuantity);
    }
    protected void txttemperature_TextChanged(object sender, EventArgs e)
    {
        GetEntry();
        SetFocus(txtScaleReading);
    }
    protected void ddlTankerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        
       
        if (ddlTankerType.SelectedIndex != 0)
        {
            if (ddlTankerType.SelectedValue == "S")
            {
                

                ddlCompartmentType.Items.Clear();
                ddlCompartmentType.Items.Add(new ListItem("Single", "S"));
                ddlCompartmentType.Enabled = false;
                BindAdulterationTestGrid();

                

            }
            else if (ddlTankerType.SelectedValue == "D")
            {
                

                ddlCompartmentType.Items.Clear();
                ddlCompartmentType.Items.Insert(0, new ListItem("Select", "0"));
                ddlCompartmentType.Items.Add(new ListItem("Front", "F"));
                ddlCompartmentType.Items.Add(new ListItem("Rear", "R"));

                //DataSet ds2 = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                //                new string[] { "flag", "BI_MilkInOutRefID" },
                //                new string[] { "7", ddlReferenceNo.SelectedValue }, "dataset");

                //if (ds2.Tables.Count > 0)
                //{
                //    if (ds2.Tables[0].Rows.Count > 0)
                //    {
                //        if (ds2.Tables[0].Rows[0]["V_SealLocation"].ToString() == "F")
                //        {
                //            ddlCompartmentType.Items.Add(new ListItem("Rear", "R"));
                //        }
                //        else if (ds2.Tables[0].Rows[0]["V_SealLocation"].ToString() == "R")
                //        {
                //            ddlCompartmentType.Items.Add(new ListItem("Front", "F"));
                //        }
                //    }
                //    else
                //    {
                //        ddlCompartmentType.Items.Add(new ListItem("Front", "F"));
                //        ddlCompartmentType.Items.Add(new ListItem("Rear", "R"));
                //    }
                //}
                //else
                //{
                //    ddlCompartmentType.Items.Add(new ListItem("Front", "F"));
                //    ddlCompartmentType.Items.Add(new ListItem("Rear", "R"));
                //}

                ddlCompartmentType.Enabled = true;

               
            }
        }
        else
        {
           
        }
    }
    protected void ddlCompartmentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlCompartmentType.SelectedIndex > 0)
        {
            if (ddlAdulterationTest.SelectedValue == "Yes")
            {
                milktestdetail.Visible = true;
                BindAdulterationTestGrid();

            }
            else
            {
                milktestdetail.Visible = false;
                gvmilkAdulterationtestdetail.DataSource = string.Empty;
                gvmilkAdulterationtestdetail.DataBind();
            }
        }
        else
        {
            milktestdetail.Visible = false;
            gvmilkAdulterationtestdetail.DataSource = string.Empty;
            gvmilkAdulterationtestdetail.DataBind();
        }
    }
}