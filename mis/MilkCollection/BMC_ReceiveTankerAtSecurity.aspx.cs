using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_MilkCollection_BMC_ReceiveTankerAtSecurity : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    CultureInfo cult = new CultureInfo("en-IN", true);
     
    protected void Page_Load(object sender, EventArgs e)
    {

        if (objdb.createdBy() != null)
        {
            txtArrivalDate.Text = System.DateTime.Now.ToString();

            if (!IsPostBack)
            {
                txtDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                //Get Page Token
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }

                GetReferenceInfo();
                GetViewReceivedTankerDetails();

            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx", false);
        }
    }

    public void GetReferenceInfo()
    {
        try
        {
            DataSet ds1 = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                               new string[] { "flag", "I_OfficeID", "V_EntryType" },
                               new string[] { "13", objdb.Office_ID(), "Out" }, "dataset");
            if (ds1.Tables[0].Rows.Count != 0)
            {

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
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }

    }

    protected void ddlReferenceNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            ViewState["InsertRecord1"] = null;
            txtD_GrossWeight.Text = "";
            txtReceiptNo.Text = "";
            //divsealdetail.Visible = false;
            fs_action.Visible = false;

            if (ddlReferenceNo.SelectedValue != "0")
            {

                DataSet ds1 = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                               new string[] { "flag", "BI_MilkInOutRefID", "V_EntryType" },
                               new string[] { "9", ddlReferenceNo.SelectedValue, "Out" }, "dataset");

                if (ds1.Tables[0].Rows.Count != 0)
                {
                    fs_action.Visible = true;
                    //divsealdetail.Visible = true;
                    ddlMilkDispatchtype.SelectedValue = ds1.Tables[0].Rows[0]["V_VehicleType"].ToString();
                }
                else
                {
                    ViewState["InsertRecord1"] = null;
                    txtD_GrossWeight.Text = "";
                    txtReceiptNo.Text = "";
                   // divsealdetail.Visible = false;
                    fs_action.Visible = false;

                }

            }
            else
            {
                ViewState["InsertRecord1"] = null;
                txtD_GrossWeight.Text = "";
                txtReceiptNo.Text = "";
                //divsealdetail.Visible = false;
                fs_action.Visible = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        GetViewReceivedTankerDetails();
    }

    protected void txtD_GrossWeight_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlReferenceNo.SelectedIndex != 0)
            {
                ds = null;
                ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                                new string[] { "flag", "BI_MilkInOutRefID" },
                                new string[] { "22", ddlReferenceNo.SelectedValue }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        decimal StrTW = Convert.ToDecimal(ds.Tables[0].Rows[0]["MilkQuantity"].ToString());

                        if (Convert.ToDecimal(txtD_GrossWeight.Text) < StrTW)
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Entered Gross Weight [" + txtD_GrossWeight.Text + " KG] and will not accepted Gross weight less than : [" + StrTW + " KG]");
                            txtD_GrossWeight.Text = "";
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Something wents wrong!");
                    }
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Plase Select Reference No. First!");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
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

            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                     new string[] { "flag", "I_OfficeID", "D_Date" },
                     new string[] { "29", objdb.Office_ID(), date }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv_TodayReceivedTankerDetails.DataSource = ds;
                        gv_TodayReceivedTankerDetails.DataBind();

                    }
                    else
                    {
                        gv_TodayReceivedTankerDetails.DataSource = null;
                        gv_TodayReceivedTankerDetails.DataBind();
                        

                    }
                }
                else
                {
                    gv_TodayReceivedTankerDetails.DataSource = null;
                    gv_TodayReceivedTankerDetails.DataBind();
                   
                }
            }
            else
            {
                gv_TodayReceivedTankerDetails.DataSource = null;
                gv_TodayReceivedTankerDetails.DataBind();
                
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("BMC_ReceiveTankerAtSecurity.aspx");
    }

    //protected void ddlSealColor_Init(object sender, EventArgs e)
    //{
    //    DataSet ds1 = objdb.ByProcedure("USP_Mst_SealColor",
    //                          new string[] { "flag" },
    //                          new string[] { "1" }, "dataset");
    //    if (ds1.Tables[0].Rows.Count > 0)
    //    {
    //        ddlSealColor.DataSource = ds1;
    //        ddlSealColor.DataTextField = "V_SealColor";
    //        ddlSealColor.DataValueField = "I_SealColorID";
    //        ddlSealColor.DataBind();
    //        ddlSealColor.Items.Insert(0, new ListItem("Select", "0"));
    //    }
    //    else
    //    {
    //        ddlSealColor.DataSource = string.Empty;
    //        ddlSealColor.DataBind();
    //        ddlSealColor.Items.Insert(0, new ListItem("Select", "0"));
    //    }
    //}

    //protected void btnTankerSealDetails_Click(object sender, EventArgs e)
    //{
    //    lblMsg.Text = "";
    //    AddSealDetails();
    //    btnSubmit.Enabled = true;
    //}

    //private void AddSealDetails()
    //{
    //    try
    //    {
    //        int CompartmentType = 0;

    //        if (Convert.ToString(ViewState["InsertRecord1"]) == null || Convert.ToString(ViewState["InsertRecord1"]) == "")
    //        {
    //            DataTable dt1 = new DataTable();
    //            DataRow dr1;
    //            dt1.Columns.Add(new DataColumn("S.No", typeof(int)));
    //            dt1.Columns.Add(new DataColumn("Sealtype", typeof(string)));
    //            dt1.Columns.Add(new DataColumn("ChamberType", typeof(string)));
    //            dt1.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));
    //            dt1.Columns.Add(new DataColumn("V_SealColor", typeof(string)));
    //            dt1.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
    //            dt1.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));

    //            dr1 = dt1.NewRow();
    //            dr1[0] = 1;
    //            dr1[1] = ddlSealtype.SelectedValue;
    //            dr1[2] = ddlChamberType.SelectedValue;
    //            dr1[3] = ddlSealColor.SelectedValue;
    //            dr1[4] = ddlSealColor.SelectedItem;
    //            dr1[5] = txtV_SealNo.Text;
    //            dr1[6] = txtV_SealRemark.Text;
    //            dt1.Rows.Add(dr1);

    //            ViewState["InsertRecord1"] = dt1;
    //            gv_SealInfo.DataSource = dt1;
    //            gv_SealInfo.DataBind();
    //        }
    //        else
    //        {
    //            DataTable dt1 = new DataTable();
    //            DataTable DT1 = new DataTable();
    //            DataRow dr1;
    //            dt1.Columns.Add(new DataColumn("S.No", typeof(int)));
    //            dt1.Columns.Add(new DataColumn("Sealtype", typeof(string)));
    //            dt1.Columns.Add(new DataColumn("ChamberType", typeof(string)));
    //            dt1.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));
    //            dt1.Columns.Add(new DataColumn("V_SealColor", typeof(string)));
    //            dt1.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
    //            dt1.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));

    //            DT1 = (DataTable)ViewState["InsertRecord1"];

    //            if (DT1.Rows.Count > 1)
    //            {
    //                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Only 2 Seal Allow in One Tanker");
    //                return;
    //            }


    //            for (int i = 0; i < DT1.Rows.Count; i++)
    //            {
    //                if (txtV_SealNo.Text == DT1.Rows[i]["V_SealNo"].ToString())
    //                {
    //                    CompartmentType = 1;
    //                }
    //            }
    //            if (CompartmentType == 1)
    //            {
    //                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Seal of \"" + txtV_SealNo.Text + "\" already exist.");

    //            }
    //            else
    //            {
    //                dr1 = dt1.NewRow();
    //                dr1[0] = 1;
    //                dr1[1] = ddlSealtype.SelectedValue;
    //                dr1[2] = ddlChamberType.SelectedValue;
    //                dr1[3] = ddlSealColor.SelectedValue;
    //                dr1[4] = ddlSealColor.SelectedItem;
    //                dr1[5] = txtV_SealNo.Text;
    //                dr1[6] = txtV_SealRemark.Text;
    //                dt1.Rows.Add(dr1);
    //            }

    //            foreach (DataRow tr in DT1.Rows)
    //            {
    //                dt1.Rows.Add(tr.ItemArray);
    //            }
    //            ViewState["InsertRecord1"] = dt1;
    //            gv_SealInfo.DataSource = dt1;
    //            gv_SealInfo.DataBind();
    //        }


    //        //Clear Record
    //        ddlSealtype.SelectedIndex = -1;
    //        ddlChamberType.SelectedIndex = -1;
    //        ddlSealColor.SelectedIndex = -1;
    //        txtV_SealNo.Text = "";
    //        txtV_SealRemark.Text = "";

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
    //    }
    //}

    //protected void lnkDelete_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";

    //        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
    //        DataTable dt2 = ViewState["InsertRecord1"] as DataTable;
    //        dt2.Rows.Remove(dt2.Rows[row.RowIndex]);
    //        ViewState["InsertRecord1"] = dt2;
    //        gv_SealInfo.DataSource = dt2;
    //        gv_SealInfo.DataBind();

    //        ddlSealtype.SelectedIndex = -1;
    //        ddlChamberType.SelectedIndex = -1;
    //        ddlSealColor.SelectedIndex = -1;
    //        txtV_SealNo.Text = "";
    //        txtV_SealRemark.Text = "";

    //        DataTable dtdeletecc = ViewState["InsertRecord1"] as DataTable;

    //        if (dtdeletecc.Rows.Count == 0)
    //        {
    //            btnSubmit.Enabled = false;
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
    //    }
    //}

    //private DataTable GetSealGridvalue()
    //{

    //    DataTable dtseal = new DataTable();
    //    DataRow drseal;

    //    dtseal.Columns.Add(new DataColumn("Sealtype", typeof(string)));
    //    dtseal.Columns.Add(new DataColumn("ChamberType", typeof(string)));
    //    dtseal.Columns.Add(new DataColumn("TI_SealColor", typeof(int)));
    //    dtseal.Columns.Add(new DataColumn("V_SealNo", typeof(string)));
    //    dtseal.Columns.Add(new DataColumn("V_SealRemark", typeof(string)));

    //    foreach (GridViewRow rowseal in gv_SealInfo.Rows)
    //    {
    //        Label lblSealtype = (Label)rowseal.FindControl("lblSealtype");
    //        Label lblChamberType = (Label)rowseal.FindControl("lblChamberType");
    //        Label lblTI_SealColor = (Label)rowseal.FindControl("lblTI_SealColor");
    //        Label lblV_SealNo = (Label)rowseal.FindControl("lblV_SealNo");
    //        Label lblV_SealRemark = (Label)rowseal.FindControl("lblV_SealRemark");

    //        drseal = dtseal.NewRow();
    //        drseal[0] = lblSealtype.Text;
    //        drseal[1] = lblChamberType.Text;
    //        drseal[2] = lblTI_SealColor.Text;
    //        drseal[3] = lblV_SealNo.Text;
    //        drseal[4] = lblV_SealRemark.Text;

    //        dtseal.Rows.Add(drseal);
    //    }
    //    return dtseal;
    //}

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

    protected void btnYes_Click(object sender, EventArgs e)
    {

        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                lblMsg.Text = "";


                // Check Gross Weight Enter Or Not

                if (ddlReferenceNo.SelectedIndex > 0)
                {
                    string strtR = BMC_ChallanRunTime_GrossWeight_Validation();

                    if (strtR == "0")
                    {

                    }
                    else
                    {
                        GetReferenceInfo();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Oops!", "Reference No. " + ddlReferenceNo.SelectedItem.Text + " Gross Weight Already Updated!");
                        GetViewReceivedTankerDetails();
                        //gv_SealInfo.DataSource = null;
                        //gv_SealInfo.DataBind();
                        //divsealdetail.Visible = false;
                        return;
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Oops!", "Please select Reference Number.!");
                    return;
                }


                if (btnSubmit.Text == "Update")
                {


                    //DataTable dt = new DataTable();
                    //dt = GetSealGridvalue();

                    //if (ddlMilkDispatchtype.SelectedValue == "Single Chamber")
                    //{
                    //    if (dt.Rows.Count > 0)
                    //    {

                    //    }
                    //    else
                    //    {
                    //        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "In Case Of Tanker - First To Fill At Least One Tanker Valve Seal & One Chamber Seal Details");
                    //        return;
                    //    }
                    //}

                    //End of Seal Details

                    //if (dt.Rows.Count == 0)
                    //{
                    //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Thank You!", "Opps! :" + "Minimum 1 seal is required.");
                    //}
                    //else
                    //{
                    //    if ((dt.Select("ChamberType = 'Chamber'").Length > 0 && dt.Select("ChamberType = 'ValveBox'").Length > 0))
                    //    {
                    //        string Uploadedfilename = "";

                    //        if (FuSealVerificationfile.HasFile)
                    //        {
                    //            decimal size = ((decimal)FuSealVerificationfile.PostedFile.ContentLength / (decimal)100);
                    //            string Fileext = Path.GetExtension(FuSealVerificationfile.FileName).ToLower();
                    //            if (Fileext == ".png" || Fileext == ".jpg" || Fileext == ".jpeg" || Fileext == ".doc" || Fileext == ".docx" || Fileext == ".pdf" || Fileext == ".zip" || Fileext == ".rar")
                    //            {
                    //                if (size <= 5000)
                    //                {
                    //                    Uploadedfilename = string.Concat("~/mis/MilkCollection/SealVerificationdoc/", Guid.NewGuid(), FuSealVerificationfile.FileName);
                    //                    FuSealVerificationfile.SaveAs(Server.MapPath(Uploadedfilename));
                    //                }
                    //                else
                    //                {
                    //                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Thank You!", "Opps! :" + "FIle size should be less then 5 MP");
                    //                    return;
                    //                }
                    //            }
                    //            else
                    //            {
                    //                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Thank You!", "Opps! :" + "Allow File Formate only (.jpg,.jpeg,.doc,.docx,.pdf,.zip,.rar");
                    //                return;
                    //            }
                    //        }

                    //        ds = null;
                    //        ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                    //                 new string[] { "flag", "D_GrossWeight", "BI_MilkInOutRefID", "SealVerificationDocument", "WeightReceiptNo" },
                    //                 new string[] { "14", txtD_GrossWeight.Text, ddlReferenceNo.SelectedValue, Uploadedfilename, txtReceiptNo.Text },
                    //                 new string[] { "type_Trn_TankerSealDetails_MCU"},
                    //                 new DataTable[] { dt }, "dataset");

                    //        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    //        {
                    //            Session["IsSuccess"] = true;
                    //            Response.Redirect("BMC_ReceiveTankerAtSecurity.aspx", false);
                    //        }
                    //        else
                    //        {
                    //            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error:" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    //        }
                    //    }
                    //    else
                    //    {
                    //        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", "Minimum chamber seal required 1 and maximum 10, Valve seal required minimum 1 and maximum 2");
                    //    }
                    //}
                    ds = null;
                    ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                             new string[] { "flag", "D_GrossWeight", "BI_MilkInOutRefID",  "WeightReceiptNo" },
                             new string[] { "14", txtD_GrossWeight.Text, ddlReferenceNo.SelectedValue,txtReceiptNo.Text }
                            , "dataset");
                    
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        Session["IsSuccess"] = true;
                        Response.Redirect("BMC_ReceiveTankerAtSecurity.aspx", false);
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error:" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
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


}