using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_MilkCollection_ReceiveTankerAtSecurity : System.Web.UI.Page
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

                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }
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

    protected void ddlReferenceNo_Init(object sender, EventArgs e)
    {

        DataSet ds1 = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
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

    protected void ddlReferenceNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlReferenceNo.SelectedValue != "0")
            {

                DataSet ds1 = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                               new string[] { "flag", "BI_MilkInOutRefID", "V_EntryType" },
                               new string[] { "9", ddlReferenceNo.SelectedValue, "Out" }, "dataset");

                if (ds1.Tables[0].Rows.Count != 0)
                {

                    //ddlchallanno.DataSource = ds1;
                    //ddlchallanno.DataTextField = "Challanno";
                    //ddlchallanno.DataValueField = "I_EntryID";
                    //ddlchallanno.DataBind();
                    //ddlchallanno.Items.Insert(0, new ListItem("Select", "0"));

                    ddlTankerType.SelectedValue = ds1.Tables[0].Rows[0]["V_VehicleType"].ToString();
                    ddlTankerType_SelectedIndexChanged(sender, e);

                    ViewState["MilkQuality"] = ds1.Tables[1];

                    if (ds1.Tables[0].Rows.Count == 2) //In Case 2 Challan For Front and Rear Chamber Case
                    {
                        lblFirstChallan.Text = ds1.Tables[0].Rows[0]["Challanno"].ToString();
                        lblSecondChallan.Text = ds1.Tables[0].Rows[1]["Challanno"].ToString();

                        div_SealVerification_Single_Challan.Visible = true;
                        div_SealVerification_Dual_Challan.Visible = true;
                    }
                    else if (ds1.Tables[0].Rows.Count == 1) //In Case 1 Challan For Front/Rear/Single Chamber Case
                    {
                        div_SealVerification_Single_Challan.Visible = true;
                        div_SealVerification_Dual_Challan.Visible = false;
                        lblSecondChallan.Text = "";
                        lblFirstChallan.Text = ds1.Tables[0].Rows[0]["Challanno"].ToString();
                    }
                    else
                    {
                        lblSecondChallan.Text = "";
                        lblFirstChallan.Text = "";
                        div_SealVerification_Single_Challan.Visible = false;
                        div_SealVerification_Dual_Challan.Visible = false;
                    }
                }
                else
                {
                    //ddlchallanno.DataSource = string.Empty;
                    //ddlchallanno.DataBind();
                    //ddlchallanno.Items.Insert(0, new ListItem("Select", "0"));
                    ViewState["MilkQuality"] = null;

                    div_SealVerification_Single_Challan.Visible = false;
                    div_SealVerification_Dual_Challan.Visible = false;
                    lblSecondChallan.Text = "";
                    lblFirstChallan.Text = "";
                }
				 if(ds1.Tables[2].Rows.Count > 0)
                {
                    gvCCSealDetails.DataSource = ds1.Tables[2];
                    gvCCSealDetails.DataBind();
                    divccsealdetails.Visible = true;
                }
                else
                {
                    gvCCSealDetails.DataSource = string.Empty;
                    gvCCSealDetails.DataBind();
                    divccsealdetails.Visible = false;
                }
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

            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
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


    //protected void GetViewRefDetails()
    //{
    //    try
    //    {
    //        ds = null;
    //        ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
    //                 new string[] { "flag", "I_OfficeID", "BI_MilkInOutRefID" },
    //                 new string[] { "12", objdb.Office_ID(), ddlReferenceNo.SelectedValue }, "dataset");

    //        if (ds != null)
    //        {
    //            if (ds.Tables.Count > 0)
    //            {
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    gv_viewreferenceno.DataSource = ds;
    //                    gv_viewreferenceno.DataBind();

    //                }
    //            }
    //        }

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
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                lblMsg.Text = "";

                if (btnSubmit.Text == "Update")
                {
                    //Seal Details
                    int sealCount = 0, SecondsealCount = 0;

                    DataTable dt = new DataTable();
                    DataRow dr;

                    dt.Columns.Add(new DataColumn("EntryID", typeof(string)));
                    dt.Columns.Add(new DataColumn("SealNo", typeof(string)));
                    dt.Columns.Add(new DataColumn("SealColor", typeof(int)));
                    dt.Columns.Add(new DataColumn("SealLocation", typeof(string)));
                    dt.Columns.Add(new DataColumn("ReferenceCode", typeof(string)));
                    dt.Columns.Add(new DataColumn("SealRemark", typeof(string)));

                    if (ddlTankerType.SelectedValue == "D")
                    {
                        if (txtTotalSeals.Text != "")
                        {
                            sealCount = Convert.ToInt32(txtTotalSeals.Text);
                            sealCount += 2;
                        }

                        if (txtSecondTotalSeals.Text != "")
                        {
                            SecondsealCount = Convert.ToInt32(txtSecondTotalSeals.Text);
                        }
                    }
                    else if (ddlTankerType.SelectedValue == "S")
                    {
                        if (txtTotalSeals.Text != "")
                        {
                            sealCount = Convert.ToInt32(txtTotalSeals.Text);
                            sealCount += 2;
                        }
                    }

                    if (txtTotalSeals.Text != "")
                    {
                        for (int i = 0; i < sealCount; i++)
                        {
                            if (Request.Form["SealNo" + Convert.ToString(i + 1)] != "")
                            {
                                dr = dt.NewRow();
                                dr[0] = ((DataTable)ViewState["MilkQuality"]).Select("V_ReferenceCode='" + lblFirstChallan.Text + "'")[0]["I_EntryID"].ToString();
                                dr[1] = Request.Form["SealNo" + Convert.ToString(i + 1)];
                                dr[2] = Request.Form["SealColor" + Convert.ToString(i + 1)];
                                dr[3] = Request.Form["SealLocation" + Convert.ToString(i + 1)];
                                dr[4] = lblFirstChallan.Text;
                                dr[5] = Request.Form["SealRemark" + Convert.ToString(i + 1)];
                                dt.Rows.Add(dr);
                            }
                        }
                    }

                    if (txtSecondTotalSeals.Text != "")
                    {
                        for (int i = 0; i < SecondsealCount; i++)
                        {
                            if (Request.Form["SealNo_S" + Convert.ToString(i + 1)] != "")
                            {
                                dr = dt.NewRow();
                                dr[0] = ((DataTable)ViewState["MilkQuality"]).Select("V_ReferenceCode='" + lblSecondChallan.Text + "'")[0]["I_EntryID"].ToString();
                                dr[1] = Request.Form["SealNo_S" + Convert.ToString(i + 1)];
                                dr[2] = Request.Form["SealColor_S" + Convert.ToString(i + 1)];
                                dr[3] = Request.Form["SealLocation_S" + Convert.ToString(i + 1)];
                                dr[4] = lblSecondChallan.Text;
                                dr[5] = Request.Form["SealRemark_S" + Convert.ToString(i + 1)];
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                    //End of Seal Details

                    if (dt.Rows.Count == 0)
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Thank You!", "Opps! :" + "Minimum 1 seal is required.");
                    }
                    else
                    {
                        if ((dt.Select("SealLocation = 'F'").Length > 0 || dt.Select("SealLocation = 'R'").Length > 0) && dt.Select("SealLocation = 'VB'").Length > 0
                            ||
                            (dt.Select("SealLocation = 'S'").Length > 0 && dt.Select("SealLocation = 'VB'").Length > 0)
                            )
                        {
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

                            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                                     new string[] { "flag", "D_GrossWeight", "BI_MilkInOutRefID", "SealVerificationDocument", "WeightReceiptNo" },
                                     new string[] { "14", txtD_GrossWeight.Text, ddlReferenceNo.SelectedValue, Uploadedfilename, txtReceiptNo.Text },
                                     new string[] { "type_Trn_TankerSealDetails_at_Security" },
                                     new DataTable[] { dt }, "dataset");

                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                Session["IsSuccess"] = true;
                                Response.Redirect("ReceiveTankerAtSecurity.aspx", false);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error:" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", "Minimum chamber seal (Front / Rear) required 1 and maximum 10, Valve seal required minimum 1 and maximum 2");
                        }
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

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReceiveTankerAtSecurity.aspx");
    }


    protected void ddlchallanno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlReferenceNo.SelectedValue != "0")
            {

                DataSet ds1 = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
                               new string[] { "flag", "BI_MilkInOutRefID", "I_EntryID" },
                               new string[] { "10", ddlReferenceNo.SelectedValue, "" }, "dataset");

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    dv_TankerType.Visible = true;
                    btnAdd.Visible = true;
                    btnAddSecondSealDetails.Visible = true;
                    ddlTankerType.SelectedValue = ds1.Tables[0].Rows[0]["V_TankerType"].ToString();
                    ddlTankerType_SelectedIndexChanged(sender, e);

                    ViewState["MilkQuality"] = ds1.Tables[1];
                }
                else
                {
                    btnAdd.Visible = true;
                    btnAddSecondSealDetails.Visible = true;
                    dv_TankerType.Visible = true;
                    ddlTankerType.ClearSelection();

                    ViewState["MilkQuality"] = null;
                }
            }
            else
            {
                dv_TankerType.Visible = true;
                ddlTankerType.ClearSelection();

                ViewState["MilkQuality"] = null;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }

    protected void ddlTankerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        dvSealDetails.InnerHtml = string.Empty;
        rowSealDetails.Style.Add("display", "none");

        if (ddlTankerType.SelectedIndex != 0)
        {
            if (ddlTankerType.SelectedValue == "S")
            {
                Divsealvarfication.Visible = true;
                txtTotalSeals.Text = "1";
                txtSecondTotalSeals.Text = "";
                //Range validator setting
                rvTotalSeals.MinimumValue = "1";
                rv_SecondTotalSeals.MinimumValue = "1";
                btnAdd.Visible = true;
                btnAddSecondSealDetails.Visible = true;
                rvTotalSeals.ErrorMessage = "Minimum chamber seal required 1 and maximum 10 and valve seal required minimum 1 and maximum 2";
                rvTotalSeals.Text = "<i class='fa fa-exclamation-circle' title='Minimum chamber seal required 1 and maximum 10 and valve seal required minimum 1 and maximum 2!'></i>";
            }
            else if (ddlTankerType.SelectedValue == "D")
            {
                Divsealvarfication.Visible = true;
                txtTotalSeals.Text = "2";
                txtSecondTotalSeals.Text = "";
                //Range validator setting
                rvTotalSeals.MinimumValue = "1";
                rv_SecondTotalSeals.MinimumValue = "1";
                //hide Add generate seal entry control when Single Compartment tanker
                btnAdd.Visible = true;
                btnAddSecondSealDetails.Visible = true;
                rvTotalSeals.ErrorMessage = "Minimum chamber seal required 2 and maximum 10 and valve seal required minimum 1 and maximum 2";
                rvTotalSeals.Text = "<i class='fa fa-exclamation-circle' title='Minimum chamber seal required 2 and maximum 10 and valve seal required minimum 1 and maximum 2!'></i>";
                rv_SecondTotalSeals.ErrorMessage = "Minimum chamber seal required 2 and maximum 10";
                rv_SecondTotalSeals.Text = "<i class='fa fa-exclamation-circle' title='Minimum chamber seal required 2 and maximum 10!'></i>";
            }
        }
        else
        {
            txtTotalSeals.Text = "2";

            //Range validator setting
            rvTotalSeals.MinimumValue = "2";
            //hide Add generate seal entry control when selected index = 0
            btnAdd.Visible = false;
            btnAddSecondSealDetails.Visible = false;
            Divsealvarfication.Visible = false;

            lblMsg.Text = "";
            dvSealDetails.InnerHtml = string.Empty;
            rowSealDetails.Style.Add("display", "none");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (txtTotalSeals.Text != "")
        {
            StringBuilder html = new StringBuilder();

            int sealCount = Convert.ToInt32(txtTotalSeals.Text);
            int j = sealCount;

            fs_action.Visible = true;

            //Get Seal Color from Database Table
            DataSet ds1 = objdb.ByProcedure("USP_Mst_SealColor",
                            new string[] { "flag" },
                            new string[] { "1" }, "dataset");
            //End of Seal Color

            if (ddlTankerType.SelectedValue == "S")
            {
                for (int i = 0; i < sealCount; i++)
                {
                    html.Append("<tr><td><input type='text' class='form-control' id='txtSealNo" + Convert.ToString(i + 1) + "' name='SealNo" + Convert.ToString(i + 1) + "'");
                    html.Append(" Width='100%' placeholder='Seal No.' /></td><td><select class='form-control' id='ddlSealLocation" + Convert.ToString(i + 1) + "' name='SealLocation" + Convert.ToString(i + 1) + "'");
                    html.Append(" readonly='readonly'><option Value='S'>S</option></select>");
                    html.Append("</td><td>");
                    html.Append("<select class='form-control' id='ddlSealColor" + Convert.ToString(i + 1) + "' name='SealColor" + Convert.ToString(i + 1) + "'>");

                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int r = 0; r < ds1.Tables[0].Rows.Count; r++)
                        {
                            html.Append("<option Value='" + ds1.Tables[0].Rows[r]["I_SealColorID"].ToString() + "'>" + ds1.Tables[0].Rows[r]["V_SealColor"].ToString() + "</option>");
                        }
                    }
                    else
                    {
                        html.Append("<option Value='0'>Select</option>");
                    }

                    html.Append("</select></td><td>");
                    html.Append("<input type='text' class='form-control' id='txtSealRemark" + Convert.ToString(i + 1) + "' name='SealRemark" + Convert.ToString(i + 1) + "'");
                    html.Append(" Width='100%' placeholder='Seal Remark' /></td></tr>");
                }

                html.Append("<tr><th colspan='4' style='text-align:center;'>Valve Box</th></tr><tr><th>Seal No.</th><th>Seal&nbsp;Location</th><th>Seal Color</th><th>Seal Remark</th></tr>");

                for (int i = 0; i < 2; i++)
                {
                    html.Append("<tr><td><input type='text' class='form-control' id='txtSealNo" + Convert.ToString(j + 1) + "' name='SealNo" + Convert.ToString(j + 1) + "'");
                    html.Append(" Width='100%' placeholder='Seal No.' /></td><td><select class='form-control' id='ddlSealLocation" + Convert.ToString(j + 1) + "' name='SealLocation" + Convert.ToString(j + 1) + "'");
                    html.Append(" readonly='readonly'><option Value='VB'>VB</option></select>");
                    html.Append("</td><td>");
                    html.Append("<select class='form-control' id='ddlSealColor" + Convert.ToString(j + 1) + "' name='SealColor" + Convert.ToString(j + 1) + "'>");

                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int r = 0; r < ds1.Tables[0].Rows.Count; r++)
                        {
                            html.Append("<option Value='" + ds1.Tables[0].Rows[r]["I_SealColorID"].ToString() + "'>" + ds1.Tables[0].Rows[r]["V_SealColor"].ToString() + "</option>");
                        }
                    }
                    else
                    {
                        html.Append("<option Value='0'>Select</option>");
                    }

                    html.Append("</select></td><td>");
                    html.Append("<input type='text' class='form-control' id='txtSealRemark" + Convert.ToString(j + 1) + "' name='SealRemark" + Convert.ToString(j + 1) + "'");
                    html.Append(" Width='100%' placeholder='Seal Remark' /></td></tr>");
                    j++;
                }


                rowSealDetails.Style.Add("display", "block");
                btnSubmit.Visible = true;
            }
            else if (ddlTankerType.SelectedValue == "D")
            {
                for (int i = 0; i < sealCount; i++)
                {
                    html.Append("<tr><td><input type='text' class='form-control' id='txtSealNo" + Convert.ToString(i + 1) + "' name='SealNo" + Convert.ToString(i + 1) + "'");
                    html.Append(" Width='100%' placeholder='Seal No.' /></td><td><select class='form-control' id='ddlSealLocation" + Convert.ToString(i + 1) + "' name='SealLocation" + Convert.ToString(i + 1) + "' ");

                    //html.Append("<option Value='F'>F</option><option Value='R'>R</option></select></td>");

                    if (((DataTable)ViewState["MilkQuality"]).Select("V_SealLocation = 'F'").Length > 0 && ((DataTable)ViewState["MilkQuality"]).Select("V_SealLocation = 'R'").Length > 0)
                    {
                        html.Append(">");
                    }
                    else
                    {
                        html.Append("readonly='readonly'>");
                    }

                    if (((DataTable)ViewState["MilkQuality"]).Select("V_ReferenceCode = '" + lblFirstChallan.Text + "' and V_SealLocation = 'F'").Length > 0)
                    {
                        html.Append("<option Value='F'>F</option>");
                    }

                    if (((DataTable)ViewState["MilkQuality"]).Select("V_ReferenceCode = '" + lblFirstChallan.Text + "' and V_SealLocation = 'R'").Length > 0)
                    {
                        html.Append("<option Value='R'>R</option>");
                    }

                    html.Append("</select></td><td>");
                    html.Append("<select class='form-control' id='ddlSealColor" + Convert.ToString(i + 1) + "' name='SealColor" + Convert.ToString(i + 1) + "'>");

                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int r = 0; r < ds1.Tables[0].Rows.Count; r++)
                        {
                            html.Append("<option Value='" + ds1.Tables[0].Rows[r]["I_SealColorID"].ToString() + "'>" + ds1.Tables[0].Rows[r]["V_SealColor"].ToString() + "</option>");
                        }
                    }
                    else
                    {
                        html.Append("<option Value='0'>Select</option>");
                    }

                    html.Append("</select></td><td>");

                    html.Append("<input type='text' class='form-control' id='txtSealRemark" + Convert.ToString(i + 1) + "' name='SealRemark" + Convert.ToString(i + 1) + "'");
                    html.Append(" Width='100%' placeholder='Seal Remark' /></td></tr>");
                }

                html.Append("<tr><th colspan='4' style='text-align:center;'>Valve Box</th></tr><tr><th>Seal No.</th><th>Seal&nbsp;Location</th><th>Seal Color</th><th>Seal Remark</th></tr>");

                for (int i = 0; i < 2; i++)
                {
                    html.Append("<tr><td><input type='text' class='form-control' id='txtSealNo" + Convert.ToString(j + 1) + "' name='SealNo" + Convert.ToString(j + 1) + "'");
                    html.Append(" Width='100%' placeholder='Seal No.' /></td><td><select class='form-control' id='ddlSealLocation" + Convert.ToString(j + 1) + "' name='SealLocation" + Convert.ToString(j + 1) + "'");
                    html.Append(" readonly='readonly'><option Value='VB'>VB</option></select>");
                    html.Append("</td><td>");
                    html.Append("<select class='form-control' id='ddlSealColor" + Convert.ToString(j + 1) + "' name='SealColor" + Convert.ToString(j + 1) + "'>");

                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int r = 0; r < ds1.Tables[0].Rows.Count; r++)
                        {
                            html.Append("<option Value='" + ds1.Tables[0].Rows[r]["I_SealColorID"].ToString() + "'>" + ds1.Tables[0].Rows[r]["V_SealColor"].ToString() + "</option>");
                        }
                    }
                    else
                    {
                        html.Append("<option Value='0'>Select</option>");
                    }

                    html.Append("</select></td><td>");

                    html.Append("<input type='text' class='form-control' id='txtSealRemark" + Convert.ToString(j + 1) + "' name='SealRemark" + Convert.ToString(j + 1) + "'");
                    html.Append(" Width='100%' placeholder='Seal Remark' /></td></tr>");
                    j++;
                }

                rowSealDetails.Style.Add("display", "block");
                btnSubmit.Visible = true;
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Select Tanker Type first!");
                ddlTankerType.Focus();


            }

            dvSealDetails.InnerHtml = html.ToString();
        }
        else
        {
            fs_action.Visible = false;
            lblMsg.Text = objdb.Alert("fa-info", "alert-info", "Info!", "Enter No. Of Seal first!");
        }
    }
    protected void btnAddSecondSealDetails_Click(object sender, EventArgs e)
    {
        if (txtSecondTotalSeals.Text != "")
        {
            StringBuilder html = new StringBuilder();

            int sealCount = Convert.ToInt32(txtSecondTotalSeals.Text);
            int j = sealCount;

            fs_action.Visible = true;

            //Get Seal Color from Database Table
            DataSet ds1 = objdb.ByProcedure("USP_Mst_SealColor",
                            new string[] { "flag" },
                            new string[] { "1" }, "dataset");
            //End of Seal Color

            if (ddlTankerType.SelectedValue == "S")
            {
                for (int i = 0; i < sealCount; i++)
                {
                    html.Append("<tr><td><input type='text' class='form-control' id='txtSealNo_S" + Convert.ToString(i + 1) + "' name='SealNo_S" + Convert.ToString(i + 1) + "'");
                    html.Append(" Width='100%' placeholder='Seal No.' /></td><td><select class='form-control' id='ddlSealLocation_S" + Convert.ToString(i + 1) + "' name='SealLocation_S" + Convert.ToString(i + 1) + "'");
                    html.Append(" readonly='readonly'><option Value='S'>S</option></select>");
                    html.Append("</td><td>");
                    html.Append("<select class='form-control' id='ddlSealColor_S" + Convert.ToString(i + 1) + "' name='SealColor_S" + Convert.ToString(i + 1) + "'>");

                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int r = 0; r < ds1.Tables[0].Rows.Count; r++)
                        {
                            html.Append("<option Value='" + ds1.Tables[0].Rows[r]["I_SealColorID"].ToString() + "'>" + ds1.Tables[0].Rows[r]["V_SealColor"].ToString() + "</option>");
                        }
                    }
                    else
                    {
                        html.Append("<option Value='0'>Select</option>");
                    }

                    html.Append("</select></td><td>");
                    html.Append("<input type='text' class='form-control' id='txtSealRemark_S" + Convert.ToString(i + 1) + "' name='SealRemark_S" + Convert.ToString(i + 1) + "'");
                    html.Append(" Width='100%' placeholder='Seal Remark' /></td></tr>");
                }

                html.Append("<tr><th colspan='4' style='text-align:center;'>Valve Box</th></tr><tr><th>Seal No.</th><th>Seal&nbsp;Location</th><th>Seal Color</th><th>Seal Remark</th></tr>");

                for (int i = 0; i < 2; i++)
                {
                    html.Append("<tr><td><input type='text' class='form-control' id='txtSealNo_S" + Convert.ToString(j + 1) + "' name='SealNo_S" + Convert.ToString(j + 1) + "'");
                    html.Append(" Width='100%' placeholder='Seal No.' /></td><td><select class='form-control' id='ddlSealLocation_S" + Convert.ToString(j + 1) + "' name='SealLocation_S" + Convert.ToString(j + 1) + "'");
                    html.Append(" readonly='readonly'><option Value='VB'>VB</option></select>");

                    html.Append("</td><td>");
                    html.Append("<select class='form-control' id='ddlSealColor_S" + Convert.ToString(j + 1) + "' name='SealColor_S" + Convert.ToString(j + 1) + "'>");

                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int r = 0; r < ds1.Tables[0].Rows.Count; r++)
                        {
                            html.Append("<option Value='" + ds1.Tables[0].Rows[r]["I_SealColorID"].ToString() + "'>" + ds1.Tables[0].Rows[r]["V_SealColor"].ToString() + "</option>");
                        }
                    }
                    else
                    {
                        html.Append("<option Value='0'>Select</option>");
                    }

                    html.Append("</select></td><td>");

                    html.Append("<input type='text' class='form-control' id='txtSealRemark_S" + Convert.ToString(j + 1) + "' name='SealRemark_S" + Convert.ToString(j + 1) + "'");
                    html.Append(" Width='100%' placeholder='Seal Remark' /></td></tr>");
                    j++;
                }

                Div_SecondSealDetails.Style.Add("display", "block");
                btnSubmit.Visible = true;
            }
            else if (ddlTankerType.SelectedValue == "D")
            {
                for (int i = 0; i < sealCount; i++)
                {
                    html.Append("<tr><td><input type='text' class='form-control' id='txtSealNo_S" + Convert.ToString(i + 1) + "' name='SealNo_S" + Convert.ToString(i + 1) + "'");
                    html.Append(" Width='100%' placeholder='Seal No.' /></td><td><select class='form-control' id='ddlSealLocation_S" + Convert.ToString(i + 1) + "' name='SealLocation_S" + Convert.ToString(i + 1) + "' ");

                    //html.Append("<option Value='F'>F</option><option Value='R'>R</option></select></td>");

                    if (((DataTable)ViewState["MilkQuality"]).Select("V_SealLocation = 'F'").Length > 0 && ((DataTable)ViewState["MilkQuality"]).Select("V_SealLocation = 'R'").Length > 0)
                    {
                        html.Append(">");
                    }
                    else
                    {
                        html.Append("readonly='readonly'>");
                    }

                    if (((DataTable)ViewState["MilkQuality"]).Select("V_ReferenceCode = '" + lblSecondChallan.Text + "' and V_SealLocation = 'F'").Length > 0)
                    {
                        html.Append("<option Value='F'>F</option></select>");
                    }

                    if (((DataTable)ViewState["MilkQuality"]).Select("V_ReferenceCode = '" + lblSecondChallan.Text + "' and V_SealLocation = 'R'").Length > 0)
                    {
                        html.Append("<option Value='R'>R</option></select>");
                    }

                    html.Append("</td><td>");
                    html.Append("<select class='form-control' id='ddlSealColor_S" + Convert.ToString(i + 1) + "' name='SealColor_S" + Convert.ToString(i + 1) + "'>");

                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int r = 0; r < ds1.Tables[0].Rows.Count; r++)
                        {
                            html.Append("<option Value='" + ds1.Tables[0].Rows[r]["I_SealColorID"].ToString() + "'>" + ds1.Tables[0].Rows[r]["V_SealColor"].ToString() + "</option>");
                        }
                    }
                    else
                    {
                        html.Append("<option Value='0'>Select</option>");
                    }

                    html.Append("</select></td><td>");

                    html.Append("<input type='text' class='form-control' id='txtSealRemark_S" + Convert.ToString(i + 1) + "' name='SealRemark_S" + Convert.ToString(i + 1) + "'");
                    html.Append(" Width='100%' placeholder='Seal Remark' /></td></tr>");
                }

                //html.Append("<tr><th colspan='4' style='text-align:center;'>Valve Box</th></tr><tr><th>Seal No.</th><th>Seal&nbsp;Location</th><th>Seal Color</th><th>Seal Remark</th></tr>");

                //for (int i = 0; i < 2; i++)
                //{
                //    html.Append("<tr><td><input type='text' class='form-control' id='txtSealNo" + Convert.ToString(j + 1) + "' name='SealNo" + Convert.ToString(j + 1) + "'");
                //    html.Append(" Width='100%' placeholder='Seal No.' /></td><td><select class='form-control' id='ddlSealLocation" + Convert.ToString(j + 1) + "' name='SealLocation" + Convert.ToString(j + 1) + "'");
                //    html.Append(" readonly='readonly'><option Value='VB'>VB</option></select>");
                //html.Append("</td><td>");
                //html.Append("<select class='form-control' id='ddlSealColor_S" + Convert.ToString(j + 1) + "' name='SealColor_S" + Convert.ToString(j + 1) + "'>");

                //if (ds1.Tables[0].Rows.Count > 0)
                //{
                //    for (int r = 0; r < ds1.Tables[0].Rows.Count; r++)
                //    {
                //        html.Append("<option Value='" + ds1.Tables[0].Rows[r]["I_SealColorID"].ToString() + "'>" + ds1.Tables[0].Rows[r]["V_SealColor"].ToString() + "</option>");
                //    }
                //}
                //else
                //{
                //    html.Append("<option Value='0'>Select</option>");
                //}

                //html.Append("</select></td><td>");
                //    html.Append("<input type='text' class='form-control' id='txtSealRemark" + Convert.ToString(j + 1) + "' name='SealRemark" + Convert.ToString(j + 1) + "'");
                //    html.Append(" Width='100%' placeholder='Seal Remark' /></td></tr>");
                //    j++;
                //}

                Div_SecondSealDetails.Style.Add("display", "block");
                btnSubmit.Visible = true;
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Select Tanker Type first!");
                ddlTankerType.Focus();


            }

            dv_SecondSealDetails.InnerHtml = html.ToString();
        }
        else
        {
            fs_action.Visible = false;
            lblMsg.Text = objdb.Alert("fa-info", "alert-info", "Info!", "Enter No. Of Seal first!");
        }
    }
    protected void txtD_GrossWeight_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlReferenceNo.SelectedIndex != 0)
            {
                ds = null;
                ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
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
    
}