using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web;

public partial class mis_MilkCollection_BMC_ReceivedTankerOutBySecurity : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    CultureInfo cult = new CultureInfo("en-IN", true);
    decimal Totalmilkqty = 0;
     
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null)
        {
            txtArrivalDate.Text = System.DateTime.Now.ToString();

            if (!IsPostBack)
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

                BindddlReferenceNo();

                GetViewReceivedTankerDetails();

                //Get Page Token
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx", false);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    private void BindddlReferenceNo()
    {
        try
        {
            DataSet ds1 = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                              new string[] { "flag", "I_OfficeID", "V_EntryType" },
                              new string[] { "19", objdb.Office_ID(), "Out" }, "dataset");
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
            lblMsg.Text = "";

            if (ddlReferenceNo.SelectedValue != "0")
            {
                DataSet ds1 = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                               new string[] { "flag", "BI_MilkInOutRefID", "V_EntryType" },
                               new string[] { "20", ddlReferenceNo.SelectedValue, "Out" }, "dataset");

                if (ds1.Tables[0].Rows.Count != 0)
                {
                    fs_action.Visible = true;
                    btnSubmit.Visible = true;
                    txtD_GrossWeight.Text = ds1.Tables[0].Rows[0]["D_GrossWeight"].ToString();
                    txtD_GrossWeight.Enabled = false;
                    txtReceiptNo.Text = ds1.Tables[0].Rows[0]["WeightReceiptNo"].ToString();
                    ddlTankerType.SelectedValue = ds1.Tables[0].Rows[0]["V_VehicleType"].ToString();
                    if(ddlTankerType.SelectedValue == "S")
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("V_SealLocation", typeof(string));
                        dt.Columns.Add("SealLocationText", typeof(string));
                        dt.Columns.Add("I_MilkQuantity", typeof(string));

                        dt.Rows.Add("S", "Single Chamber", "0.00");
                        gvUpdateNetWeight.DataSource = dt;
                        gvUpdateNetWeight.DataBind();
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("V_SealLocation", typeof(string));
                        dt.Columns.Add("SealLocationText", typeof(string));
                        dt.Columns.Add("I_MilkQuantity", typeof(string));

                        dt.Rows.Add("F", "Front Chamber", "0.00");
                        dt.Rows.Add("R", "Rear Chamber", "0.00");
                        gvUpdateNetWeight.DataSource = dt;
                        gvUpdateNetWeight.DataBind();
                    }
                   // ViewChallanInfo();
                }
                else
                {
                    //gv_ViewChallanDetail.DataSource = string.Empty;
                    //gv_ViewChallanDetail.DataBind();
                    //div_SealVerification_Single_Challan.Visible = false;
                    fs_action.Visible = false;
                    btnSubmit.Visible = false;
                }
                 
            }
            else
            {
                //gv_ViewChallanDetail.DataSource = string.Empty;
                //gv_ViewChallanDetail.DataBind();
                //div_SealVerification_Single_Challan.Visible = false;
                fs_action.Visible = false;
                btnSubmit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }

    //protected void ViewChallanInfo()
    //{
    //    try
    //    {
    //        ds = null;
    //        ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
    //                 new string[] { "flag", "I_OfficeID", "BI_MilkInOutRefID" },
    //                 new string[] { "16", objdb.Office_ID(), ddlReferenceNo.SelectedValue }, "dataset");

    //        if (ds != null)
    //        {
    //            if (ds.Tables.Count > 0)
    //            {
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    div_SealVerification_Single_Challan.Visible = true;
    //                    gv_ViewChallanDetail.Visible = true;
    //                    gv_ViewChallanDetail.DataSource = ds;
    //                    gv_ViewChallanDetail.DataBind();

    //                }

    //                else
    //                {
    //                    gv_ViewChallanDetail.Visible = false;
    //                    gv_ViewChallanDetail.DataSource = string.Empty;
    //                    gv_ViewChallanDetail.DataBind();
    //                }

    //            }
    //            else
    //            {
    //                gv_ViewChallanDetail.Visible = false;
    //                gv_ViewChallanDetail.DataSource = string.Empty;
    //                gv_ViewChallanDetail.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            gv_ViewChallanDetail.Visible = false;
    //            gv_ViewChallanDetail.DataSource = string.Empty;
    //            gv_ViewChallanDetail.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
    //    }
    //}

    //protected void txtI_MilkQuantity_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";

    //        foreach (GridViewRow row in gv_ViewChallanDetail.Rows)
    //        {
    //            TextBox txtI_MilkQuantity = (TextBox)row.FindControl("txtI_MilkQuantity");

    //            if (txtI_MilkQuantity.Text != "")
    //            {
    //                Totalmilkqty += Convert.ToDecimal(txtI_MilkQuantity.Text);

    //                Label lblTotal_I_MilkQuantity = (gv_ViewChallanDetail.FooterRow.FindControl("lblTotal_I_MilkQuantity") as Label);

    //                lblTotal_I_MilkQuantity.Text = Totalmilkqty.ToString("0.00");

    //                if (Convert.ToDecimal(txtD_GrossWeight.Text) <= Totalmilkqty)
    //                {
    //                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Entered Net Weight : \"" + Totalmilkqty + "\" Should not be greater then Gross Weight  \"" + txtD_GrossWeight.Text + "\".");
    //                    return;
    //                }
    //            }
    //            else
    //            {
    //                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "InValid Milk Quantity (In KG)");
    //                return;
    //            }


    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 17:" + ex.Message.ToString());
    //    }
    //}

    //private DataTable GetMilkQualityDetails()
    //{
    //    DataTable dt = new DataTable();
    //    DataRow dr;
    //    dt.Columns.Add(new DataColumn("I_EntryID", typeof(string)));
    //    dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
    //    dt.Columns.Add(new DataColumn("I_MilkQuantity", typeof(decimal)));
    //    dt.Columns.Add(new DataColumn("I_QualityEntryID", typeof(string)));

    //    foreach (GridViewRow row in gv_ViewChallanDetail.Rows)
    //    {
    //        Label lblI_EntryID = (Label)row.FindControl("lblI_EntryID");
    //        Label lblV_SealLocation = (Label)row.FindControl("lblV_SealLocation");
    //        TextBox lblI_MilkQuantity = (TextBox)row.FindControl("txtI_MilkQuantity");
    //        Label lblI_QualityEntryID = (Label)row.FindControl("lblI_QualityEntryID");

    //        dr = dt.NewRow();
    //        dr[0] = lblI_EntryID.Text;
    //        dr[1] = lblV_SealLocation.Text;
    //        dr[2] = lblI_MilkQuantity.Text;
    //        dr[3] = lblI_QualityEntryID.Text;
    //        dt.Rows.Add(dr);
    //    }
    //    return dt;
    //}

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("BMC_ReceivedTankerOutBySecurity.aspx");
    }
     
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        GetViewReceivedTankerDetails();
    }


    public string BMC_NetWeight_Validation()
    {
        string RefNetWeight = "0";

        try
        {

            DataSet dsRefNetWeight = objdb.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                               new string[] { "flag", "BI_MilkInOutRefID", },
                               new string[] { "23", ddlReferenceNo.SelectedValue }, "dataset");

            if (dsRefNetWeight != null)
            {
                if (dsRefNetWeight.Tables.Count > 0)
                {
                    if (dsRefNetWeight.Tables[0].Rows.Count > 0)
                    {
                        RefNetWeight = dsRefNetWeight.Tables[0].Rows[0]["Status"].ToString();

                        return RefNetWeight;
                    }
                    else
                    {
                        return RefNetWeight;
                    }
                }
                else
                {
                    return RefNetWeight;
                }
            }
            else
            {
                return RefNetWeight;
            }

        }
        catch (Exception)
        {

            return RefNetWeight;
        }
    }
    public string BMC_QcEntry_Validation()
    {
        string QcEntry = "0";

        try
        {

            DataSet dsQcEntry = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                               new string[] { "flag", "BI_MilkInOutRefID", },
                               new string[] { "33", ddlReferenceNo.SelectedValue }, "dataset");

            if (dsQcEntry != null)
            {
                if (dsQcEntry.Tables.Count > 0)
                {
                    if (dsQcEntry.Tables[0].Rows.Count > 0)
                    {
                        QcEntry = dsQcEntry.Tables[0].Rows[0]["Status"].ToString();

                        return QcEntry;
                    }
                    else
                    {
                        return QcEntry;
                    }
                }
                else
                {
                    return QcEntry;
                }
            }
            else
            {
                return QcEntry;
            }

        }
        catch (Exception)
        {

            return QcEntry;
        }
    }
      
    protected void btnYes_Click(object sender, EventArgs e)
    {

        try
        {
            lblMsg.Text = "";

            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                if (ddlReferenceNo.SelectedIndex > 0)
                {

                    // Runtime Validation Net Weight Updated Or Not

                    string strtR = BMC_NetWeight_Validation();

                    if (strtR == "0")
                    {

                    }
                    else
                    {

                        lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Oops!", "Reference No. " + ddlReferenceNo.SelectedItem.Text + " Net Weight Already Updated!");
                        BindddlReferenceNo();
                        //gv_ViewChallanDetail.DataSource = null;
                        //gv_ViewChallanDetail.DataBind();
                        //div_SealVerification_Single_Challan.Visible = false;
                        GetViewReceivedTankerDetails();
                        return;
                    }
                    //string QCEntry = BMC_QcEntry_Validation();

                    //if (QCEntry == "0")
                    //{

                    //}
                    //else
                    //{

                    //    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Oops!", "QC Entry for. " + ddlReferenceNo.SelectedItem.Text + " not completed Yet!");
                    //    BindddlReferenceNo();
                    //    gv_ViewChallanDetail.DataSource = null;
                    //    gv_ViewChallanDetail.DataBind();
                    //    div_SealVerification_Single_Challan.Visible = false;
                    //    GetViewReceivedTankerDetails();
                    //    return;
                    //}
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Oops!", "Please select Reference Number.!");
                    return;
                }
                 



               foreach (GridViewRow row in gvUpdateNetWeight.Rows)
               {
                   TextBox lblI_MilkQuantity = (TextBox)row.FindControl("txtI_MilkQuantity");

                   if (lblI_MilkQuantity.Text == "" || lblI_MilkQuantity.Text == "0.00" || lblI_MilkQuantity.Text == "0" || lblI_MilkQuantity.Text == "00")
                   {
                       lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Entered Net Weight Can't Acceptable Blank , 0 , 0.00. Please Enter Valid Milk Quantity (In KG)");
                       return;
                   }

               }

                DataTable dt = new DataTable();
                dt = GetMilkQualityDetails();


                if (btnSubmit.Text == "Update")
                {

                       ds = null;
                        ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                             new string[] { "flag", "D_GrossWeight", "BI_MilkInOutRefID"},
                             new string[] { "16", txtD_GrossWeight.Text, ddlReferenceNo.SelectedValue},
                             new string[] { "type_Update_MilkQualityDetails_MCU" },
                             new DataTable[] { dt },
                             "dataset");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            Session["IsSuccess"] = true;
                            Response.Redirect("BMC_ReceivedTankerOutBySecurity.aspx", false);
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
     
    protected void GetViewReceivedTankerDetails()
    {
        try
        {
            ds = null;
            string date = "";
            btnExport.Visible = false;
            if (txtDate.Text != "")
            {
                date = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                     new string[] { "flag", "I_OfficeID", "D_Date" },
                     new string[] { "30", objdb.Office_ID(), date }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv_TodayReceivedTankerDetails.DataSource = ds;
                        gv_TodayReceivedTankerDetails.DataBind();
                        btnExport.Visible = true;

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


  
    protected void gv_TodayReceivedTankerDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (e.CommandName == "ViewMore")
            {
                StringBuilder sb = new StringBuilder();

                ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New",
                                    new string[] { "flag", "BI_MilkInOutRefID" },
                                    new string[] { "15", e.CommandArgument.ToString() }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        //btnPrint.Visible = true;

                        int count = ds.Tables[0].Rows.Count;
                        string BI_MilkInOutRefID = "";
                        string V_VehicleNo = "";
                        decimal TotalQty = 0;
                        decimal TKgFat = 0;
                        decimal TKgSnf = 0;
                        decimal RTotalQty = 0;
                        decimal RTKgFat = 0;
                        decimal RTKgSnf = 0;
                        int Count = 0;
                        for (int i = 0; i < count; i++)
                        {

                            if (i == 0)
                            {
                                sb.Append("<table class='table table-bordered'>");
                                sb.Append("<tr>");
                                sb.Append("<td colspan='9' style='text-align:center;'><b>" + Session["Office_Name"].ToString() + "</b></td>");
                                sb.Append("</tr>");
                                sb.Append("<tr>");
                                sb.Append("<td colspan='9' style='text-align:center;'><b>BMC -  " + ds.Tables[0].Rows[i]["BMCTankerRootName"].ToString() + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Tan.No.&nbsp;&nbsp;" + ds.Tables[0].Rows[i]["V_VehicleNo"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;Time - " + ds.Tables[0].Rows[i]["Time"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;Date - " + ds.Tables[0].Rows[i]["Date"].ToString() + "</b></td>");
                                sb.Append("</tr>");
                                sb.Append("<tr>");
                                sb.Append("<th>S.No</td>");
                                sb.Append("<th style='text-align:center;'>Name</th>");
                                sb.Append("<th style='text-align:center;'>Temp</th>");
                                sb.Append("<th style='text-align:center;'>Qty</th>");
                                sb.Append("<th style='text-align:center;'>Fat %</th>");
                                sb.Append("<th style='text-align:center;'>CLR</th>");
                                sb.Append("<th style='text-align:center;'>SNF %</th>");
                                sb.Append("<th style='text-align:center;'>Kg Fat</th>");
                                sb.Append("<th style='text-align:center;'>Kg SNF</th>");
                                sb.Append("</tr>");
                                if (i == (count - 1))
                                {
                                    //sb.Append("<tr>");
                                    //sb.Append("<td style='text-align:center;'>" + (Count + 1).ToString() + "</td>");
                                    //sb.Append("<td style='text-align:left;'>" + ds.Tables[0].Rows[i]["Office_Name"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Temp"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Quantity"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FAT"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["CLR"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SNF"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FatKg"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SnfKg"] + "</td>");
                                    //sb.Append("</tr>");
                                    BI_MilkInOutRefID = ds.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString();
                                    V_VehicleNo = ds.Tables[0].Rows[i]["V_VehicleNo"].ToString();
                                    TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString());
                                    TKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatKg"].ToString());
                                    TKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfKg"].ToString());
                                    sb.Append("<tr>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:left;'><b>D.C.S Received</b></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'>" + TotalQty + "</td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'>" + TKgFat.ToString() + "</td>");
                                    sb.Append("<td style='text-align:center;'>" + TKgSnf.ToString() + "</td>");
                                    sb.Append("</tr>");
                                    DataSet dsRecord = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New", new string[] { "flag", "BI_MilkInOutRefID" }, new string[] { "14", BI_MilkInOutRefID }, "dataset");
                                    if (dsRecord != null && dsRecord.Tables[0].Rows.Count > 0)
                                    {
                                        int Count1 = dsRecord.Tables[0].Rows.Count;
                                        for (int j = 0; j < Count1; j++)
                                        {
                                            sb.Append("<tr>");
                                            sb.Append("<td style='text-align:center;'></td>");
                                            sb.Append("<td style='text-align:left;'><b>" + V_VehicleNo +  " [ " + dsRecord.Tables[0].Rows[j]["ChamberType"] +" ]</b></td>");
                                            sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["Temp"] + "</td>");
                                            sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["Quantity"] + "</td>");
                                            sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["FAT"] + "</td>");
                                            sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["CLR"] + "</td>");
                                            sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["SNF"] + "</td>");
                                            sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["FatKg"].ToString() + "</td>");
                                            sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["SnfKg"].ToString() + "</td>");
                                            sb.Append("</tr>");
                                            RTotalQty += decimal.Parse(dsRecord.Tables[0].Rows[j]["Quantity"].ToString());
                                            RTKgFat += decimal.Parse(dsRecord.Tables[0].Rows[j]["FatKg"].ToString());
                                            RTKgSnf += decimal.Parse(dsRecord.Tables[0].Rows[j]["SnfKg"].ToString());
                                        }
                                       
                                    }
                                    sb.Append("<tr>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:left;'><b>Difference</b></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'>" + (RTotalQty - TotalQty) + "</td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'>" + (RTKgFat - TKgFat) + "</td>");
                                    sb.Append("<td style='text-align:center;'>" + (RTKgSnf - TKgSnf) + "</td>");
                                    sb.Append("</tr>");
                                    sb.Append("</table>");
                                }
                                else
                                {
                                    //sb.Append("<tr>");
                                    //sb.Append("<td style='text-align:center;'>" + (Count + 1).ToString() + "</td>");
                                    //sb.Append("<td style='text-align:left;'>" + ds.Tables[0].Rows[i]["Office_Name"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Temp"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Quantity"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FAT"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["CLR"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SNF"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FatKg"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SnfKg"] + "</td>");
                                    //sb.Append("</tr>");
                                    BI_MilkInOutRefID = ds.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString();
                                    V_VehicleNo = ds.Tables[0].Rows[i]["V_VehicleNo"].ToString();
                                    TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString());
                                    TKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatKg"].ToString());
                                    TKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfKg"].ToString());
                                    Count += 1;
                                }
                               
                            }
                            else if (BI_MilkInOutRefID == ds.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString())
                            {
                                if (i == (count - 1))
                                {
                                    //sb.Append("<tr>");
                                    //sb.Append("<td style='text-align:center;'>" + (Count + 1).ToString() + "</td>");
                                    //sb.Append("<td style='text-align:left;'>" + ds.Tables[0].Rows[i]["Office_Name"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Temp"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Quantity"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FAT"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["CLR"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SNF"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FatKg"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SnfKg"] + "</td>");
                                    //sb.Append("</tr>");
                                    BI_MilkInOutRefID = ds.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString();
                                    V_VehicleNo = ds.Tables[0].Rows[i]["V_VehicleNo"].ToString();
                                    TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString());
                                    TKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatKg"].ToString());
                                    TKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfKg"].ToString());
                                    sb.Append("<tr>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:left;'><b>D.C.S Received</b></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'>" + TotalQty + "</td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'>" + TKgFat.ToString() + "</td>");
                                    sb.Append("<td style='text-align:center;'>" + TKgSnf.ToString() + "</td>");
                                    sb.Append("</tr>");
                                    DataSet dsRecord = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New", new string[] { "flag", "BI_MilkInOutRefID" }, new string[] { "14", BI_MilkInOutRefID }, "dataset");
                                    if (dsRecord != null && dsRecord.Tables[0].Rows.Count > 0)
                                    {
                                        int Count1 = dsRecord.Tables[0].Rows.Count;
                                        for (int j = 0; j < Count1; j++)
                                        {
                                            sb.Append("<tr>");
                                            sb.Append("<td style='text-align:center;'></td>");
                                            sb.Append("<td style='text-align:left;'><b>" + V_VehicleNo + " [ " + dsRecord.Tables[0].Rows[j]["ChamberType"] + " ]</b></td>");
                                            sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["Temp"] + "</td>");
                                            sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["Quantity"] + "</td>");
                                            sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["FAT"] + "</td>");
                                            sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["CLR"] + "</td>");
                                            sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["SNF"] + "</td>");
                                            sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["FatKg"].ToString() + "</td>");
                                            sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["SnfKg"].ToString() + "</td>");
                                            sb.Append("</tr>");
                                            RTotalQty += decimal.Parse(dsRecord.Tables[0].Rows[j]["Quantity"].ToString());
                                            RTKgFat += decimal.Parse(dsRecord.Tables[0].Rows[j]["FatKg"].ToString());
                                            RTKgSnf += decimal.Parse(dsRecord.Tables[0].Rows[j]["SnfKg"].ToString());
                                        }
                                    }
                                    sb.Append("<tr>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:left;'><b>Difference</b></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'>" + (RTotalQty - TotalQty) + "</td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'>" + (RTKgFat - TKgFat) + "</td>");
                                    sb.Append("<td style='text-align:center;'>" + (RTKgSnf - TKgSnf) + "</td>");
                                    sb.Append("</tr>");
                                    sb.Append("</table>");
                                }
                                else
                                {


                                    //sb.Append("<tr>");
                                    //sb.Append("<td style='text-align:center;'>" + (Count + 1).ToString() + "</td>");
                                    //sb.Append("<td style='text-align:left;'>" + ds.Tables[0].Rows[i]["Office_Name"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Temp"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Quantity"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FAT"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["CLR"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SNF"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FatKg"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SnfKg"] + "</td>");
                                    //sb.Append("</tr>");
                                    BI_MilkInOutRefID = ds.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString();
                                    V_VehicleNo = ds.Tables[0].Rows[i]["V_VehicleNo"].ToString();
                                    TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString());
                                    TKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatKg"].ToString());
                                    TKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfKg"].ToString());
                                    Count += 1;
                                }
                            }

                            else
                            {
                                sb.Append("<tr>");
                                sb.Append("<td style='text-align:center;'></td>");
                                sb.Append("<td style='text-align:left;'><b>D.C.S Received</b></td>");
                                sb.Append("<td style='text-align:center;'></td>");
                                sb.Append("<td style='text-align:center;'>" + TotalQty + "</td>");
                                sb.Append("<td style='text-align:center;'></td>");
                                sb.Append("<td style='text-align:center;'></td>");
                                sb.Append("<td style='text-align:center;'></td>");
                                sb.Append("<td style='text-align:center;'>" + TKgFat.ToString() + "</td>");
                                sb.Append("<td style='text-align:center;'>" + TKgSnf.ToString() + "</td>");
                                sb.Append("</tr>");
                                DataSet dsRecord = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New", new string[] { "flag", "BI_MilkInOutRefID" }, new string[] { "14", BI_MilkInOutRefID }, "dataset");
                                if (dsRecord != null && dsRecord.Tables[0].Rows.Count > 0)
                                {
                                    int Count1 = dsRecord.Tables[0].Rows.Count;
                                    for (int j = 0; j < Count1; j++)
                                    {
                                        sb.Append("<tr>");
                                        sb.Append("<td style='text-align:center;'></td>");
                                        sb.Append("<td style='text-align:left;'><b>" + V_VehicleNo + " [ " + dsRecord.Tables[0].Rows[j]["ChamberType"] + " ]</b></td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["Temp"] + "</td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["Quantity"] + "</td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["FAT"] + "</td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["CLR"] + "</td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["SNF"] + "</td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["FatKg"].ToString() + "</td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["SnfKg"].ToString() + "</td>");
                                        sb.Append("</tr>");
                                        RTotalQty += decimal.Parse(dsRecord.Tables[0].Rows[j]["Quantity"].ToString());
                                        RTKgFat += decimal.Parse(dsRecord.Tables[0].Rows[j]["FatKg"].ToString());
                                        RTKgSnf += decimal.Parse(dsRecord.Tables[0].Rows[j]["SnfKg"].ToString());
                                    }
                                }
                                sb.Append("<tr>");
                                sb.Append("<td style='text-align:center;'></td>");
                                sb.Append("<td style='text-align:left;'><b>Difference</b></td>");
                                sb.Append("<td style='text-align:center;'></td>");
                                sb.Append("<td style='text-align:center;'>" + (RTotalQty - TotalQty) + "</td>");
                                sb.Append("<td style='text-align:center;'></td>");
                                sb.Append("<td style='text-align:center;'></td>");
                                sb.Append("<td style='text-align:center;'></td>");
                                sb.Append("<td style='text-align:center;'>" + (RTKgFat - TKgFat) + "</td>");
                                sb.Append("<td style='text-align:center;'>" + (RTKgSnf - TKgSnf) + "</td>");
                                sb.Append("</tr>");
                                Count = 0;
                                TotalQty = 0;
                                TKgFat = 0;
                                TKgSnf = 0;
                                sb.Append("</table>");
                                //sb.Append("<div style='page-break-before: always; '></div>");
                                sb.Append("<table class='table table-bordered'>");
                                sb.Append("<tr>");
                                sb.Append("<td colspan='9' style='text-align:center;'><b>" + Session["Office_Name"].ToString() + "</b></td>");
                                sb.Append("</tr>");
                                sb.Append("<tr>");
                                sb.Append("<td colspan='9' style='text-align:center;'><b>BMC -  " + ds.Tables[0].Rows[i]["BMCTankerRootName"].ToString() + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Tan.No.&nbsp;&nbsp;" + ds.Tables[0].Rows[i]["V_VehicleNo"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;Time - " + ds.Tables[0].Rows[i]["Time"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;Date - " + ds.Tables[0].Rows[i]["Date"].ToString() + "</b></td>");
                                sb.Append("</tr>");
                                sb.Append("<tr>");
                                sb.Append("<th>S.No</td>");
                                sb.Append("<th style='text-align:center;'>Name</th>");
                                sb.Append("<th style='text-align:center;'>Temp</th>");
                                sb.Append("<th style='text-align:center;'>Qty</th>");
                                sb.Append("<th style='text-align:center;'>Fat %</th>");
                                sb.Append("<th style='text-align:center;'>CLR</th>");
                                sb.Append("<th style='text-align:center;'>SNF %</th>");
                                sb.Append("<th style='text-align:center;'>Kg Fat</th>");
                                sb.Append("<th style='text-align:center;'>Kg SNF</th>");
                                sb.Append("</tr>");
                                //sb.Append("<tr>");
                                //sb.Append("<td style='text-align:center;'>" + (Count + 1).ToString() + "</td>");
                                //sb.Append("<td style='text-align:left;'>" + ds.Tables[0].Rows[i]["Office_Name"] + "</td>");
                                //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Temp"] + "</td>");
                                //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Quantity"] + "</td>");
                                //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FAT"] + "</td>");
                                //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["CLR"] + "</td>");
                                //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SNF"] + "</td>");
                                //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FatKg"] + "</td>");
                                //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SnfKg"] + "</td>");
                                //sb.Append("</tr>");
                                BI_MilkInOutRefID = ds.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString();
                                V_VehicleNo = ds.Tables[0].Rows[i]["V_VehicleNo"].ToString();
                                TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString());
                                TKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatKg"].ToString());
                                TKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfKg"].ToString());
                            }
                        }


                    }


                }
                divReport.InnerHtml = sb.ToString();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowModal();", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void txtI_MilkQuantity_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            foreach (GridViewRow row in gvUpdateNetWeight.Rows)
            {
                TextBox txtI_MilkQuantity = (TextBox)row.FindControl("txtI_MilkQuantity");

                if (txtI_MilkQuantity.Text != "")
                {
                    Totalmilkqty += Convert.ToDecimal(txtI_MilkQuantity.Text);

                    Label lblTotal_I_MilkQuantity = (gvUpdateNetWeight.FooterRow.FindControl("lblTotal_I_MilkQuantity") as Label);

                    lblTotal_I_MilkQuantity.Text = Totalmilkqty.ToString("0.00");

                    if (Convert.ToDecimal(txtD_GrossWeight.Text) <= Totalmilkqty)
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Entered Net Weight : \"" + Totalmilkqty + "\" Should not be greater then Gross Weight  \"" + txtD_GrossWeight.Text + "\".");
                        return;
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "InValid Milk Quantity (In KG)");
                    return;
                }


            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 17:" + ex.Message.ToString());
        }
    }
    private DataTable GetMilkQualityDetails()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("BI_MilkInOutRefID", typeof(string)));
        dt.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
        dt.Columns.Add(new DataColumn("I_MilkQuantity", typeof(decimal)));

        foreach (GridViewRow row in gvUpdateNetWeight.Rows)
        {
            
            Label lblV_SealLocation = (Label)row.FindControl("lblV_SealLocation");
            TextBox lblI_MilkQuantity = (TextBox)row.FindControl("txtI_MilkQuantity");

            dr = dt.NewRow();
            dr[0] = ddlReferenceNo.SelectedValue;
            dr[1] = lblV_SealLocation.Text;
            dr[2] = lblI_MilkQuantity.Text;
            dt.Rows.Add(dr);
        }
        return dt;
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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            gv_TodayReceivedTankerDetails.Columns[10].Visible = false;
            gv_TodayReceivedTankerDetails.Columns[9].Visible = true;
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "BMC/DCS Milk Collection QC Entry Report" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gv_TodayReceivedTankerDetails.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());

            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gv_TodayReceivedTankerDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                HtmlGenericControl div = e.Row.FindControl("divDetail") as HtmlGenericControl;
                LinkButton lnkViewMore = e.Row.FindControl("lnkViewMore") as LinkButton;
                StringBuilder sb = new StringBuilder();

                ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New",
                                    new string[] { "flag", "BI_MilkInOutRefID" },
                                    new string[] { "15", lnkViewMore.CommandArgument.ToString() }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        //btnPrint.Visible = true;

                        int count = ds.Tables[0].Rows.Count;
                        string BI_MilkInOutRefID = "";
                        string V_VehicleNo = "";
                        decimal TotalQty = 0;
                        decimal TKgFat = 0;
                        decimal TKgSnf = 0;
                        int Count = 0;
                        for (int i = 0; i < count; i++)
                        {

                            if (i == 0)
                            {
                                sb.Append("<table class='table table-bordered' border='1'>");
                                sb.Append("<tr>");
                                sb.Append("<td colspan='9' style='text-align:center;'><b>" + Session["Office_Name"].ToString() + "</b></td>");
                                sb.Append("</tr>");
                                sb.Append("<tr>");
                                sb.Append("<td colspan='9' style='text-align:center;'><b>BMC -  " + ds.Tables[0].Rows[i]["BMCTankerRootName"].ToString() + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Tan.No.&nbsp;&nbsp;" + ds.Tables[0].Rows[i]["V_VehicleNo"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;Time - " + ds.Tables[0].Rows[i]["Time"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;Date - " + ds.Tables[0].Rows[i]["Date"].ToString() + "</b></td>");
                                sb.Append("</tr>");
                                sb.Append("<tr>");
                                sb.Append("<th>S.No</td>");
                                sb.Append("<th style='text-align:center;'>Name</th>");
                                sb.Append("<th style='text-align:center;'>Temp</th>");
                                sb.Append("<th style='text-align:center;'>Qty</th>");
                                sb.Append("<th style='text-align:center;'>Fat %</th>");
                                sb.Append("<th style='text-align:center;'>CLR</th>");
                                sb.Append("<th style='text-align:center;'>SNF %</th>");
                                sb.Append("<th style='text-align:center;'>Kg Fat</th>");
                                sb.Append("<th style='text-align:center;'>Kg SNF</th>");
                                sb.Append("</tr>");
                                if (i == (count - 1))
                                {
                                    //sb.Append("<tr>");
                                    //sb.Append("<td style='text-align:center;'>" + (Count + 1).ToString() + "</td>");
                                    //sb.Append("<td style='text-align:left;'>" + ds.Tables[0].Rows[i]["Office_Name"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Temp"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Quantity"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FAT"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["CLR"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SNF"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FatKg"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SnfKg"] + "</td>");
                                    //sb.Append("</tr>");
                                    BI_MilkInOutRefID = ds.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString();
                                    V_VehicleNo = ds.Tables[0].Rows[i]["V_VehicleNo"].ToString();
                                    TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString());
                                    TKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatKg"].ToString());
                                    TKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfKg"].ToString());
                                    sb.Append("<tr>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:left;'><b>D.C.S Received</b></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'>" + TotalQty + "</td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'>" + TKgFat.ToString() + "</td>");
                                    sb.Append("<td style='text-align:center;'>" + TKgSnf.ToString() + "</td>");
                                    sb.Append("</tr>");
                                    DataSet dsRecord = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New", new string[] { "flag", "BI_MilkInOutRefID" }, new string[] { "14", BI_MilkInOutRefID }, "dataset");
                                    if (dsRecord != null && dsRecord.Tables[0].Rows.Count > 0)
                                    {
                                        sb.Append("<tr>");
                                        sb.Append("<td style='text-align:center;'></td>");
                                        sb.Append("<td style='text-align:left;'><b>" + V_VehicleNo + "</b></td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["Temp"] + "</td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["Quantity"] + "</td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["FAT"] + "</td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["CLR"] + "</td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["SNF"] + "</td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["FatKg"].ToString() + "</td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["SnfKg"].ToString() + "</td>");
                                        sb.Append("</tr>");
                                    }
                                    sb.Append("<tr>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:left;'><b>Difference</b></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'>" + (decimal.Parse(dsRecord.Tables[0].Rows[0]["Quantity"].ToString()) - TotalQty) + "</td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'>" + (decimal.Parse(dsRecord.Tables[0].Rows[0]["FatKg"].ToString()) - TKgFat) + "</td>");
                                    sb.Append("<td style='text-align:center;'>" + (decimal.Parse(dsRecord.Tables[0].Rows[0]["SnfKg"].ToString()) - TKgSnf) + "</td>");
                                    sb.Append("</tr>");
                                    sb.Append("</table>");
                                }
                                else
                                {
                                    //sb.Append("<tr>");
                                    //sb.Append("<td style='text-align:center;'>" + (Count + 1).ToString() + "</td>");
                                    //sb.Append("<td style='text-align:left;'>" + ds.Tables[0].Rows[i]["Office_Name"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Temp"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Quantity"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FAT"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["CLR"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SNF"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FatKg"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SnfKg"] + "</td>");
                                    //sb.Append("</tr>");
                                    BI_MilkInOutRefID = ds.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString();
                                    V_VehicleNo = ds.Tables[0].Rows[i]["V_VehicleNo"].ToString();
                                    TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString());
                                    TKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatKg"].ToString());
                                    TKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfKg"].ToString());
                                    Count += 1;
                                }

                            }
                            else if (BI_MilkInOutRefID == ds.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString())
                            {
                                if (i == (count - 1))
                                {
                                    //sb.Append("<tr>");
                                    //sb.Append("<td style='text-align:center;'>" + (Count + 1).ToString() + "</td>");
                                    //sb.Append("<td style='text-align:left;'>" + ds.Tables[0].Rows[i]["Office_Name"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Temp"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Quantity"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FAT"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["CLR"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SNF"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FatKg"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SnfKg"] + "</td>");
                                    //sb.Append("</tr>");
                                    BI_MilkInOutRefID = ds.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString();
                                    V_VehicleNo = ds.Tables[0].Rows[i]["V_VehicleNo"].ToString();
                                    TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString());
                                    TKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatKg"].ToString());
                                    TKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfKg"].ToString());
                                    sb.Append("<tr>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:left;'><b>D.C.S Received</b></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'>" + TotalQty + "</td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'>" + TKgFat.ToString() + "</td>");
                                    sb.Append("<td style='text-align:center;'>" + TKgSnf.ToString() + "</td>");
                                    sb.Append("</tr>");
                                    DataSet dsRecord = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New", new string[] { "flag", "BI_MilkInOutRefID" }, new string[] { "14", BI_MilkInOutRefID }, "dataset");
                                    if (dsRecord != null && dsRecord.Tables[0].Rows.Count > 0)
                                    {
                                        sb.Append("<tr>");
                                        sb.Append("<td style='text-align:center;'></td>");
                                        sb.Append("<td style='text-align:left;'><b>" + V_VehicleNo + "</b></td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["Temp"] + "</td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["Quantity"] + "</td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["FAT"] + "</td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["CLR"] + "</td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["SNF"] + "</td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["FatKg"].ToString() + "</td>");
                                        sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["SnfKg"].ToString() + "</td>");
                                        sb.Append("</tr>");
                                    }
                                    sb.Append("<tr>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:left;'><b>Difference</b></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'>" + (decimal.Parse(dsRecord.Tables[0].Rows[0]["Quantity"].ToString()) - TotalQty) + "</td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:center;'>" + (decimal.Parse(dsRecord.Tables[0].Rows[0]["FatKg"].ToString()) - TKgFat) + "</td>");
                                    sb.Append("<td style='text-align:center;'>" + (decimal.Parse(dsRecord.Tables[0].Rows[0]["SnfKg"].ToString()) - TKgSnf) + "</td>");
                                    sb.Append("</tr>");
                                    sb.Append("</table>");
                                }
                                else
                                {


                                    //sb.Append("<tr>");
                                    //sb.Append("<td style='text-align:center;'>" + (Count + 1).ToString() + "</td>");
                                    //sb.Append("<td style='text-align:left;'>" + ds.Tables[0].Rows[i]["Office_Name"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Temp"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Quantity"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FAT"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["CLR"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SNF"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FatKg"] + "</td>");
                                    //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SnfKg"] + "</td>");
                                    //sb.Append("</tr>");
                                    BI_MilkInOutRefID = ds.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString();
                                    V_VehicleNo = ds.Tables[0].Rows[i]["V_VehicleNo"].ToString();
                                    TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString());
                                    TKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatKg"].ToString());
                                    TKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfKg"].ToString());
                                    Count += 1;
                                }
                            }

                            else
                            {
                                sb.Append("<tr>");
                                sb.Append("<td style='text-align:center;'></td>");
                                sb.Append("<td style='text-align:left;'><b>D.C.S Received</b></td>");
                                sb.Append("<td style='text-align:center;'></td>");
                                sb.Append("<td style='text-align:center;'>" + TotalQty + "</td>");
                                sb.Append("<td style='text-align:center;'></td>");
                                sb.Append("<td style='text-align:center;'></td>");
                                sb.Append("<td style='text-align:center;'></td>");
                                sb.Append("<td style='text-align:center;'>" + TKgFat.ToString() + "</td>");
                                sb.Append("<td style='text-align:center;'>" + TKgSnf.ToString() + "</td>");
                                sb.Append("</tr>");
                                DataSet dsRecord = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New", new string[] { "flag", "BI_MilkInOutRefID" }, new string[] { "14", BI_MilkInOutRefID }, "dataset");
                                if (dsRecord != null && dsRecord.Tables[0].Rows.Count > 0)
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td style='text-align:center;'></td>");
                                    sb.Append("<td style='text-align:left;'><b>" + V_VehicleNo + "</b></td>");
                                    sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["Temp"] + "</td>");
                                    sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["Quantity"] + "</td>");
                                    sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["FAT"] + "</td>");
                                    sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["CLR"] + "</td>");
                                    sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["SNF"] + "</td>");
                                    sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["FatKg"].ToString() + "</td>");
                                    sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[0]["SnfKg"].ToString() + "</td>");
                                    sb.Append("</tr>");
                                }
                                sb.Append("<tr>");
                                sb.Append("<td style='text-align:center;'></td>");
                                sb.Append("<td style='text-align:left;'><b>Difference</b></td>");
                                sb.Append("<td style='text-align:center;'></td>");
                                sb.Append("<td style='text-align:center;'>" + (decimal.Parse(dsRecord.Tables[0].Rows[0]["Quantity"].ToString()) - TotalQty) + "</td>");
                                sb.Append("<td style='text-align:center;'></td>");
                                sb.Append("<td style='text-align:center;'></td>");
                                sb.Append("<td style='text-align:center;'></td>");
                                sb.Append("<td style='text-align:center;'>" + (decimal.Parse(dsRecord.Tables[0].Rows[0]["FatKg"].ToString()) - TKgFat) + "</td>");
                                sb.Append("<td style='text-align:center;'>" + (decimal.Parse(dsRecord.Tables[0].Rows[0]["SnfKg"].ToString()) - TKgSnf) + "</td>");
                                sb.Append("</tr>");
                                Count = 0;
                                TotalQty = 0;
                                TKgFat = 0;
                                TKgSnf = 0;
                                sb.Append("</table>");
                                //sb.Append("<div style='page-break-before: always; '></div>");
                                sb.Append("<table class='table table-bordered'>");
                                sb.Append("<tr>");
                                sb.Append("<td colspan='9' style='text-align:center;'><b>" + Session["Office_Name"].ToString() + "</b></td>");
                                sb.Append("</tr>");
                                sb.Append("<tr>");
                                sb.Append("<td colspan='9' style='text-align:center;'><b>BMC -  " + ds.Tables[0].Rows[i]["BMCTankerRootName"].ToString() + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Tan.No.&nbsp;&nbsp;" + ds.Tables[0].Rows[i]["V_VehicleNo"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;Time - " + ds.Tables[0].Rows[i]["Time"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;Date - " + ds.Tables[0].Rows[i]["Date"].ToString() + "</b></td>");
                                sb.Append("</tr>");
                                sb.Append("<tr>");
                                sb.Append("<th>S.No</td>");
                                sb.Append("<th style='text-align:center;'>Name</th>");
                                sb.Append("<th style='text-align:center;'>Temp</th>");
                                sb.Append("<th style='text-align:center;'>Qty</th>");
                                sb.Append("<th style='text-align:center;'>Fat %</th>");
                                sb.Append("<th style='text-align:center;'>CLR</th>");
                                sb.Append("<th style='text-align:center;'>SNF %</th>");
                                sb.Append("<th style='text-align:center;'>Kg Fat</th>");
                                sb.Append("<th style='text-align:center;'>Kg SNF</th>");
                                sb.Append("</tr>");
                                //sb.Append("<tr>");
                                //sb.Append("<td style='text-align:center;'>" + (Count + 1).ToString() + "</td>");
                                //sb.Append("<td style='text-align:left;'>" + ds.Tables[0].Rows[i]["Office_Name"] + "</td>");
                                //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Temp"] + "</td>");
                                //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Quantity"] + "</td>");
                                //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FAT"] + "</td>");
                                //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["CLR"] + "</td>");
                                //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SNF"] + "</td>");
                                //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FatKg"] + "</td>");
                                //sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SnfKg"] + "</td>");
                                //sb.Append("</tr>");
                                BI_MilkInOutRefID = ds.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString();
                                V_VehicleNo = ds.Tables[0].Rows[i]["V_VehicleNo"].ToString();
                                TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString());
                                TKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatKg"].ToString());
                                TKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfKg"].ToString());
                            }
                        }


                    }


                }
                div.InnerHtml = sb.ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
}