using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

public partial class mis_MilkCollection_BMC_ReceiveTankerChallanQA : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure apiprocedure = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    MilkCalculationClass Obj_MC = new MilkCalculationClass();
     
    protected void Page_Load(object sender, EventArgs e)
    {
        if (apiprocedure.createdBy() != null)
        {
            if (!IsPostBack)
            {
                //Get Page Token
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                // SET Datetime
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtArrivalDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtfilterdate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");

                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }

                GetReferenceInfo();
                FillGrid();
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
    #region User Defined Function
    private void GetReferenceInfo()
    {

        DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                                  new string[] { "flag", "I_OfficeID", "V_EntryType" },
                                  new string[] { "8", apiprocedure.Office_ID(), "Out" }, "dataset");
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
    private void FillGrid()
    {
        try
        {
            string date = "";

            if (txtfilterdate.Text != "")
            {
                date = Convert.ToDateTime(txtfilterdate.Text, cult).ToString("yyyy/MM/dd");
            }

            //gvReceivedEntry.DataSource = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
            //                   new string[] { "flag", "I_OfficeID", "V_EntryType", "Filter_Date" },
            //                   new string[] { "14", apiprocedure.Office_ID(), "In", date }, "dataset");
            //gvReceivedEntry.DataBind();
            ds = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                               new string[] { "flag", "I_OfficeID", "V_EntryType", "Filter_Date" },
                               new string[] { "25", apiprocedure.Office_ID(), "In", date }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    gvReceivedEntry.DataSource = ds;
                    gvReceivedEntry.DataBind();
                }
                else
                {
                    gvReceivedEntry.DataSource = string.Empty;
                    gvReceivedEntry.DataBind();
                }
            }
            else
            {
                gvReceivedEntry.DataSource = string.Empty;
                gvReceivedEntry.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5:" + ex.Message.ToString());
        }
    }
    private void BindAdulterationTestGrid()
    {
        try
        {

            DataTable dtTL = new DataTable();
            DataRow drTL;

            DataSet DSAT = apiprocedure.ByProcedure("USP_Mst_AdulterationTestList",
                                   new string[] { "flag" },
                                   new string[] { "1" }, "dataset");

            if (DSAT.Tables[0].Rows.Count != 0)
            {
                dtTL.Columns.Add(new DataColumn("S.No", typeof(int)));
                //dtTL.Columns.Add(new DataColumn("V_SealLocation", typeof(string)));
                //dtTL.Columns.Add(new DataColumn("V_SealLocationAlias", typeof(string)));
                dtTL.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));

                //for (int i = 0; i < ddlCompartmentType.Items.Count; i++)
                //{
                for (int j = 0; j < DSAT.Tables[0].Rows.Count; j++)
                {
                    drTL = dtTL.NewRow();
                    drTL[0] = dtTL.Rows.Count + 1;
                    //drTL[1] = ddlCompartmentType.SelectedValue;
                    //drTL[2] = ddlCompartmentType.SelectedItem.Text.ToString();
                    drTL[1] = DSAT.Tables[0].Rows[j]["V_AdulteratioTName"].ToString();
                    dtTL.Rows.Add(drTL);
                }
                //}
            }

            gvmilkAdulterationtestdetail.DataSource = dtTL;
            gvmilkAdulterationtestdetail.DataBind();


        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
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
            // Label lblSealLocation = (Label)row.FindControl("lblSealLocation");
            Label lblAdulterationType = (Label)row.FindControl("lblAdulterationType");
            DropDownList ddlAdelterationTestValue = (DropDownList)row.FindControl("ddlAdelterationTestValue");

            //string SealLocation = "";
            //switch (lblSealLocation.Text)
            //{
            //    case "Single":
            //        SealLocation = "S";
            //        break;
            //    case "Rear":
            //        SealLocation = "R";
            //        break;
            //    case "Front":
            //        SealLocation = "F";
            //        break;
            //    default:
            //        break;
            //}

            dr = dt.NewRow();
            dr[0] = "N";
            dr[1] = lblAdulterationType.Text;
            dr[2] = ddlAdelterationTestValue.SelectedValue;
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private string GetShift()
    {

        try
        {
            DataSet dsct = apiprocedure.ByProcedure("USP_GetServerDatetime",
                             new string[] { "flag" },
                             new string[] { "1" }, "dataset");

            string currrentime = dsct.Tables[0].Rows[0]["currentTime"].ToString();

            string[] s = currrentime.Split(':');

            if ((Convert.ToInt32(s[0]) >= 0 && Convert.ToInt32(s[0]) <= 12 && ((Convert.ToInt32(s[0]) == 0 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 12 && Convert.ToInt32(s[1]) <= 59))))
            {
                return "Morning";
            }
            else
            {
                return "Evening";
            }

        }
        catch (Exception ex)
        {
            return "";
        }

    }
    protected void FillBMCQualityDetail()
    {
        try
        {
            lblMsg.Text = "";
            divaction.Visible = false;
            divadt.Visible = false;
            ds = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU", new string[] { "flag", "BI_MilkInOutRefID" }, new string[] { "35", ddlReferenceNo.SelectedValue }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    divaction.Visible = true;
                    divadt.Visible = true;
                    ViewState["TankerNo"] = ds.Tables[0].Rows[0]["V_VehicleNo"].ToString();
                    ViewState["BMCTankerRootName"] = ds.Tables[0].Rows[0]["BMCTankerRootName"].ToString();
                    ViewState["DT_TankerArrivalDate"] = ds.Tables[0].Rows[0]["DT_TankerArrivalDate"].ToString();
                    txtDriverName.Text = ds.Tables[0].Rows[0]["V_DriverName"].ToString();
                    txtDriverMobileNo.Text = ds.Tables[0].Rows[0]["V_DriverMobileNo"].ToString();
                    txtVehicleNo.Text = ds.Tables[0].Rows[0]["V_VehicleNo"].ToString();
                    // gvBMCDetails.DataSource = ds.Tables[1];
                    // gvBMCDetails.DataBind();
                    //StringBuilder sb = new StringBuilder();
                    //sb.Append("<table class='table table-bordered'>");
                    //sb.Append("<tr>");
                    //sb.Append("<th style='border-left:0px; border-right:0px;'>BMC</th>");
                    //sb.Append("<th>" + ds.Tables[0].Rows[0]["BMCTankerRootName"].ToString() + "</th>");
                    //sb.Append("<th colspan='2'>Tan No.&nbsp;&nbsp;" + ds.Tables[0].Rows[0]["V_VehicleNo"].ToString() + "</th>");
                    //sb.Append("<th colspan='5'>Date -" + ds.Tables[0].Rows[0]["DT_TankerArrivalDate"].ToString() + "</th>");
                    //sb.Append("</tr>");
                    //sb.Append("<tr>");
                    //sb.Append("<th style='width:10px;'>S.No</th>");
                    //sb.Append("<th style='width:10px;'>Name</th>");
                    //sb.Append("<th style='width:10px;'>Temp</th>");
                    //sb.Append("<th style='width:10px;'>Qty</th>");
                    //sb.Append("<th style='width:10px;'>Fat %</th>");
                    //sb.Append("<th style='width:10px;'>CLR</th>");
                    //sb.Append("<th style='width:10px;'>SNF %</th>");
                    //sb.Append("<th style='width:10px;'>Kg Fat</th>");
                    //sb.Append("<th style='width:10px;'>Kg SNF</th>");
                    //sb.Append("</tr>");
                    //int Count = ds.Tables[1].Rows.Count;
                    //for (int i = 0; i < Count; i++)
                    //{
                    //    sb.Append("<tr>");
                    //    sb.Append("<td style='width:10px;'>" + (i + 1).ToString() + "</td>");
                    //    sb.Append("<td style='width:10px;'>" + ds.Tables[1].Rows[i]["Office_Name_E"].ToString() + "</td>");
                    //    sb.Append("<td style='width:10px;'>" + ds.Tables[1].Rows[i]["V_Temp"].ToString() + "</td>");
                    //    sb.Append("<td style='width:10px;'>" + ds.Tables[1].Rows[i]["D_MilkQuantity"].ToString() + "</td>");
                    //    sb.Append("<td style='width:10px;'>" + ds.Tables[1].Rows[i]["FAT"].ToString() + "</td>");
                    //    sb.Append("<td style='width:10px;'>" + ds.Tables[1].Rows[i]["CLR"].ToString() + "</td>");
                    //    sb.Append("<td style='width:10px;'>" + ds.Tables[1].Rows[i]["SNF"].ToString() + "</td>");
                    //    sb.Append("<td style='width:10px;'>" + ds.Tables[1].Rows[i]["KgFat"].ToString() + "</td>");
                    //    sb.Append("<td style='width:10px;'>" + ds.Tables[1].Rows[i]["KgSNF"].ToString() + "</td>");
                    //    sb.Append("</tr>");
                    //}
                    //decimal TotalRcvdQty = 0;
                    //decimal TotalRcvdFatPer = 0;
                    //decimal TotalRcvdCLR = 0;
                    //decimal TotalRcvdSnfPer = 0;
                    //decimal TotalRcvdKgFat = 0;
                    //decimal TotalRcvdKgSNf = 0;
                    //TotalRcvdQty = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("D_MilkQuantity"));
                    //TotalRcvdFatPer = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("FAT"));
                    //TotalRcvdCLR = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("CLR"));
                    //TotalRcvdSnfPer = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SNF"));
                    //TotalRcvdKgFat = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    //TotalRcvdKgSNf = ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("KgSNF"));
                    //sb.Append("<tr>");
                    //sb.Append("<td></td>");
                    //sb.Append("<td><b>Received</b></td>");
                    //sb.Append("<td></td>");
                    //sb.Append("<td><b>" + TotalRcvdQty + "</b></td>");
                    //sb.Append("<td><b>" + TotalRcvdFatPer + "</b></td>");
                    //sb.Append("<td><b>" + TotalRcvdCLR + "</b></td>");
                    //sb.Append("<td><b>" + TotalRcvdSnfPer + "</b></td>");
                    //sb.Append("<td><b>" + TotalRcvdKgFat + "</b></td>");
                    //sb.Append("<td><b>" + TotalRcvdKgSNf + "</b></td>");

                    //sb.Append("</tr>");
                    //sb.Append("<tr>");
                    //sb.Append("<td></td>");
                    //sb.Append("<td><b>" + ds.Tables[0].Rows[0]["V_VehicleNo"].ToString() + "</b></td>");
                    //sb.Append("<td><input type='Text' class='form-control' /></td>");
                    //sb.Append("<td><input type='Text' class='form-control' /></td>");
                    //sb.Append("<td><input type='Text' class='form-control' /></td>");
                    //sb.Append("<td><input type='Text' class='form-control' /></td>");
                    //sb.Append("<td><input type='Text' class='form-control' /></td>");
                    //sb.Append("<td><input type='Text' class='form-control' /></td>");
                    //sb.Append("<td><input type='Text' class='form-control' /></td>");
                    //sb.Append("</tr>");
                    //sb.Append("<tr>");
                    //sb.Append("<td></td>");
                    //sb.Append("<td><b>Difference</b></td>");
                    //sb.Append("<td></td>");
                    //sb.Append("<td></td>");
                    //sb.Append("<td></td>");
                    //sb.Append("<td></td>");
                    //sb.Append("<td></td>");
                    //sb.Append("<td></td>");
                    //sb.Append("<td></td>");
                    //sb.Append("</tr>");
                    //sb.Append("</table>");
                    //bmcdetails.InnerHtml = sb.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }
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
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
        return Math.Round(snf, 3);
    }

    private decimal GetSNF_InKG(string FAT, string CLR, string Quantity)
    {
        decimal clr = 0, fat = 0, snf_Per = 0, MilkQty = 0, SNF_InKG = 0;

        try
        {
            if (Quantity == "") { MilkQty = 0; } else { MilkQty = Convert.ToDecimal(Quantity); }

            if (FAT == "") { fat = 0; } else { fat = Convert.ToDecimal(FAT); }

            if (CLR == "") { clr = 0; } else { clr = Convert.ToDecimal(CLR); }

            snf_Per = Obj_MC.GetSNFPer_DCS(fat, clr);

            SNF_InKG = Obj_MC.GetSNFInKg(MilkQty, snf_Per);
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }

        return Math.Round(SNF_InKG, 3);
    }

    private decimal GetFAT_InKG(string FAT, string Quantity)
    {
        decimal fat_Per = 0, MilkQty = 0, FAT_InKG = 0;

        try
        {
            if (Quantity == "") { MilkQty = 0; } else { MilkQty = Convert.ToDecimal(Quantity); }

            if (FAT == "") { fat_Per = 0; } else { fat_Per = Convert.ToDecimal(FAT); }

            FAT_InKG = Obj_MC.GetSNFInKg(MilkQty, fat_Per);
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }

        return Math.Round(FAT_InKG, 3);
    }
    #endregion

    #region Change Event
    protected void ddlReferenceNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            gvmilkAdulterationtestdetail.DataSource = string.Empty;
            gvmilkAdulterationtestdetail.DataBind();

            divadt.Visible = false;
            divaction.Visible = false;

            if (ddlReferenceNo.SelectedValue != "0")
            {
                FillBMCQualityDetail();
                BindAdulterationTestGrid();
                //DataSet ds1 = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                //               new string[] { "flag", "BI_MilkInOutRefID", "V_EntryType" },
                //               new string[] { "9", ddlReferenceNo.SelectedValue, "Out" }, "dataset");

                //if (ds1.Tables[0].Rows.Count != 0)
                //{

                //    ddlchallanno.DataSource = ds1;
                //    ddlchallanno.DataTextField = "Challanno";
                //    ddlchallanno.DataValueField = "I_EntryID";
                //    ddlchallanno.DataBind();
                //    ddlchallanno.Items.Insert(0, new ListItem("Select", "0"));


                //}
                //else
                //{
                //    ddlchallanno.DataSource = string.Empty;
                //    ddlchallanno.DataBind();
                //    ddlchallanno.Items.Insert(0, new ListItem("Select", "0"));
                //}
            }
            else
            {
                //ddlchallanno.DataSource = string.Empty;
                //ddlchallanno.DataBind();
                //ddlchallanno.Items.Insert(0, new ListItem("Select", "0"));

            }

            //ddlchallanno_SelectedIndexChanged(sender, e);

        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }





    protected void txtfilterdate_TextChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    #endregion
    
    #region Button Click Event
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {         
                lblMsg.Text = "";            
                
                //Check adulteration details filled all row

                DataTable dtAdultration = new DataTable();
                dtAdultration = GetAdulterationTestDetails();

                DateTime ADate = Convert.ToDateTime(string.Concat(Convert.ToDateTime(txtArrivalDate.Text, cult).ToString("yyyy/MM/dd"), " ", Convert.ToDateTime(txtArrivalTime.Text, cult).ToString("hh:mm:ss tt")), cult);


                if (dtAdultration.Rows.Count > 0)
                {
                    decimal MilkQty = 0;
                    string Temp = "";
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal Clr = 0;
                    decimal KgFat = 0;
                    decimal KgSnf = 0;
                    // foreach (GridViewRow gvrow in gvBMCDetails.Rows)
                    // {

                        // Label lblOffice_Name_E = (Label)gvrow.FindControl("lblOffice_Name_E");
                        // TextBox txtD_MilkQuantity = (TextBox)gvrow.FindControl("txtD_MilkQuantity");
                        // TextBox txtFAT = (TextBox)gvrow.FindControl("txtFAT");
                        // TextBox txtCLR = (TextBox)gvrow.FindControl("txtCLR");
                        // TextBox txtSNF = (TextBox)gvrow.FindControl("txtSNF");
                        // TextBox txtKgFat = (TextBox)gvrow.FindControl("txtKgFat");
                        // TextBox txtKgSNF = (TextBox)gvrow.FindControl("txtKgSNF");
                        // TextBox txtV_Temp = (TextBox)gvrow.FindControl("txtV_Temp");
                        // if (lblOffice_Name_E.Text == ViewState["TankerNo"].ToString())
                        // {
                            // MilkQty = decimal.Parse(txtD_MilkQuantity.Text);
                            // Fat = decimal.Parse(txtFAT.Text);
                            // Snf = decimal.Parse(txtSNF.Text);

                            // Clr = decimal.Parse(txtCLR.Text);
                            // KgFat = decimal.Parse(txtKgFat.Text);
                            // KgSnf = decimal.Parse(txtKgSNF.Text);
                            // Temp = txtV_Temp.Text;
                        // }
                        
                    // }
                    ds = null;
                    ds = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardDetails_MCU",
                            new string[] { "flag",  
                                                "V_ChallanNo",               
                                                "I_OfficeID",
                                                "I_OfficeTypeID",                                               
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
                                                "DT_ArrivalDateTime",
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
                                                "DT_Date"
                                                 },

                                                new string[] { "24",
                                                ddlReferenceNo.SelectedItem.Text,  
                                                apiprocedure.Office_ID(),
                                                apiprocedure.OfficeType_ID(),                                               
                                                txtVehicleNo.Text,
                                                txtDriverName.Text,
                                                txtDriverMobileNo.Text,
                                                "In",
                                                "",
                                                GetShift(),
                                                MilkQty.ToString(),
                                                Fat.ToString(),
                                                Clr.ToString(),
                                                Snf.ToString(),
                                                ADate.ToString(),
                                                apiprocedure.createdBy(),
                                                "",
                                                "Web",
                                                Temp,
                                                "0.00",
                                                "0.00",
                                                "0.00",
                                                MilkQty.ToString(),
                                                KgFat.ToString(),
                                                KgSnf.ToString(),
                                                ddlReferenceNo.SelectedValue,
                                                "",
                                                "0.00",
                                                Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                                                },
                                             new string[] { "type_Trn_tblAdulterationTest_MCU"},
                                             new DataTable[] { dtAdultration}, "TableSave");


                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        Session["IsSuccess"] = true;
                        Response.Redirect("BMC_ReceiveTankerChallanQA.aspx", false);

                    }
                    else
                    {
                        lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1:" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        Session["IsSuccess"] = false;
                    }

                }
                else
                {
                    lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Ooops!", "Warning : Required All Adulteration Test!");
                    Session["IsSuccess"] = false;
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 2:" + ex.Message.ToString());
            Session["IsSuccess"] = false;
        }
    }
    #endregion

    #region GridView Event
    // protected void gvBMCDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    // {
        // if (e.Row.RowType == DataControlRowType.DataRow)
        // {
            // Label lblRowNo = (Label)e.Row.FindControl("lblRowNo");
            // Label lblOffice_Name_E = (Label)e.Row.FindControl("lblOffice_Name_E");
            // Label lblV_Temp = (Label)e.Row.FindControl("lblV_Temp");
            // TextBox txtV_Temp = (TextBox)e.Row.FindControl("txtV_Temp");
            // Label lblD_MilkQuantity = (Label)e.Row.FindControl("lblD_MilkQuantity");
            // TextBox txtD_MilkQuantity = (TextBox)e.Row.FindControl("txtD_MilkQuantity");
            // Label lblFAT = (Label)e.Row.FindControl("lblFAT");
            // TextBox txtFAT = (TextBox)e.Row.FindControl("txtFAT");
            // Label lblCLR = (Label)e.Row.FindControl("lblCLR");
            // TextBox txtCLR = (TextBox)e.Row.FindControl("txtCLR");
            // Label lblSNF = (Label)e.Row.FindControl("lblSNF");
            // TextBox txtSNF = (TextBox)e.Row.FindControl("txtSNF");
            // Label lblKgFat = (Label)e.Row.FindControl("lblKgFat");
            // TextBox txtKgFat = (TextBox)e.Row.FindControl("txtKgFat");
            // Label lblKgSNF = (Label)e.Row.FindControl("lblKgSNF");
            // TextBox txtKgSNF = (TextBox)e.Row.FindControl("txtKgSNF");
            // if (lblOffice_Name_E.Text == ViewState["TankerNo"].ToString())
            // {
                // lblRowNo.Text = "";
                // lblOffice_Name_E.Font.Bold = true;
                // lblV_Temp.Visible = false;
                // txtV_Temp.Visible = true;
                // lblD_MilkQuantity.Visible = false;
                // txtD_MilkQuantity.Visible = true;
                // lblFAT.Visible = false;
                // txtFAT.Visible = true;
                // lblCLR.Visible = false;
                // txtCLR.Visible = true;
                // lblSNF.Visible = false;
                // txtSNF.Visible = true;
                // lblKgFat.Visible = false;
                // txtKgFat.Visible = true;
                // lblKgSNF.Visible = false;
                // txtKgSNF.Visible = true;

            // }
            // else if (lblOffice_Name_E.Text == "Difference" || lblOffice_Name_E.Text == "Received")
            // {
                // lblRowNo.Text = "";
                // lblOffice_Name_E.Font.Bold = true;
            // }
            // //If Salary is less than 10000 than set the row Background Color to Cyan  
            
        // }  
    // }
    // protected void gvBMCDetails_RowCreated(object sender, GridViewRowEventArgs e)
    // {
        // if (e.Row.RowType == DataControlRowType.Header)
        // {
            // GridView HeaderGrid = (GridView)sender;
            // GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            // TableCell HeaderCell = new TableCell();
            // HeaderCell.Text = "BMC " + ViewState["BMCTankerRootName"].ToString() + "";
            // HeaderCell.ColumnSpan = 3;
            // HeaderGridRow.Cells.Add(HeaderCell);

            // HeaderCell = new TableCell();
            // HeaderCell.Text = "Tan.No. " + ViewState["TankerNo"].ToString() + "";
            // HeaderCell.ColumnSpan = 3;
            // HeaderGridRow.Cells.Add(HeaderCell);


            // HeaderCell = new TableCell();
            // HeaderCell.Text = ViewState["DT_TankerArrivalDate"].ToString();
            // HeaderCell.ColumnSpan = 4;
            // HeaderGridRow.Cells.Add(HeaderCell);



            // gvBMCDetails.Controls[0].Controls.AddAt(0, HeaderGridRow);

        // }
    // }
    // protected void txtD_MilkQuantity_TextChanged(object sender, EventArgs e)
    // {
        // GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        // TextBox txtD_MilkQuantity = (TextBox)row.FindControl("txtD_MilkQuantity");
        // decimal RcvdTotalQty = 0;
        // if (txtD_MilkQuantity.Text != "")
        // {
            // decimal EnterQty = decimal.Parse(txtD_MilkQuantity.Text);
            // foreach (GridViewRow gvrow in gvBMCDetails.Rows)
            // {
                
                // Label lblOffice_Name_E = (Label)gvrow.FindControl("lblOffice_Name_E");
                // Label lblD_MilkQuantity = (Label)gvrow.FindControl("lblD_MilkQuantity");
                // if (lblOffice_Name_E.Text == "Received")
                // {
                    // RcvdTotalQty = decimal.Parse(lblD_MilkQuantity.Text);
                // }
                // if (lblOffice_Name_E.Text == "Difference")
                // {
                    // lblD_MilkQuantity.Text = (RcvdTotalQty - EnterQty).ToString();
                // }
            // }
        // }
        
    // }
    // protected void txtFAT_TextChanged(object sender, EventArgs e)
    // {

        // GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        // TextBox txtD_MilkQuantity = (TextBox)row.FindControl("txtD_MilkQuantity");
        // TextBox txtFAT = (TextBox)row.FindControl("txtFAT");
        // TextBox txtCLR = (TextBox)row.FindControl("txtCLR");
        // TextBox txtSNF = (TextBox)row.FindControl("txtSNF");
        // TextBox txtKgFat = (TextBox)row.FindControl("txtKgFat");
        // TextBox txtKgSNF = (TextBox)row.FindControl("txtKgSNF");
        // txtSNF.Text = GetSNF(txtFAT.Text, txtCLR.Text).ToString();
        // txtKgSNF.Text = GetSNF_InKG(txtFAT.Text, txtCLR.Text, txtD_MilkQuantity.Text).ToString();
        // txtKgFat.Text = GetFAT_InKG(txtFAT.Text, txtD_MilkQuantity.Text).ToString();
        
        // decimal Fat = 0;
        // decimal Clr = 0;
        // decimal Snf = 0;
        // decimal KgFat = 0;
        // decimal KgSnf = 0;
        // decimal RcvdFat = 0;
        // decimal RcvdClr = 0;
        // decimal RcvdSnf = 0;
        // decimal RcvdKgFat = 0;
        // decimal RcvdKgSnf = 0;
        // if (txtFAT.Text == "") { Fat = 0; } else { Fat = Convert.ToDecimal(txtFAT.Text); }
        // if (txtCLR.Text == "") { Clr = 0; } else { Clr = Convert.ToDecimal(txtCLR.Text); }
        // if (txtSNF.Text == "") { Snf = 0; } else { Snf = Convert.ToDecimal(txtSNF.Text); }
        // if (txtKgFat.Text == "") { KgFat = 0; } else { KgFat = Convert.ToDecimal(txtKgFat.Text); }
        // if (txtKgSNF.Text == "") { KgSnf = 0; } else { KgSnf = Convert.ToDecimal(txtKgSNF.Text); }
        // foreach (GridViewRow gvrow in gvBMCDetails.Rows)
        // {

            // Label lblOffice_Name_E = (Label)gvrow.FindControl("lblOffice_Name_E");
            // Label lblD_MilkQuantity = (Label)gvrow.FindControl("lblD_MilkQuantity");
            // Label lblFAT = (Label)gvrow.FindControl("lblFAT");
            // Label lblCLR = (Label)gvrow.FindControl("lblCLR");
            // Label lblSNF = (Label)gvrow.FindControl("lblSNF");
            // Label lblKgFat = (Label)gvrow.FindControl("lblKgFat");
            // Label lblKgSNF = (Label)gvrow.FindControl("lblKgSNF");
            // if (lblOffice_Name_E.Text == "Received")
            // {
                // RcvdFat = decimal.Parse(lblFAT.Text);
                // RcvdClr = decimal.Parse(lblCLR.Text);
                // RcvdFat = decimal.Parse(lblFAT.Text);
                // RcvdSnf = decimal.Parse(lblSNF.Text);
                // RcvdKgFat = decimal.Parse(lblKgFat.Text);
                // RcvdKgSnf = decimal.Parse(lblKgSNF.Text);
            // }
            // if (lblOffice_Name_E.Text == "Difference")
            // {
                // lblFAT.Text = (RcvdFat - Fat).ToString();
                // lblCLR.Text = (RcvdClr - Clr).ToString();
                // lblSNF.Text = (RcvdSnf - Snf).ToString();
                // lblKgFat.Text = (RcvdKgFat - KgFat).ToString();
                // lblKgSNF.Text = (RcvdKgSnf - KgSnf).ToString();
            // }
        // }

    // }
    // protected void txtCLR_TextChanged(object sender, EventArgs e)
    // {
        // GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        // TextBox txtD_MilkQuantity = (TextBox)row.FindControl("txtD_MilkQuantity");
        // TextBox txtFAT = (TextBox)row.FindControl("txtFAT");
        // TextBox txtCLR = (TextBox)row.FindControl("txtCLR");
        // TextBox txtSNF = (TextBox)row.FindControl("txtSNF");
        // TextBox txtKgFat = (TextBox)row.FindControl("txtKgFat");
        // TextBox txtKgSNF = (TextBox)row.FindControl("txtKgSNF");
        // txtSNF.Text = GetSNF(txtFAT.Text, txtCLR.Text).ToString();
        // txtKgSNF.Text = GetSNF_InKG(txtFAT.Text, txtCLR.Text,txtD_MilkQuantity.Text).ToString();
        // txtKgFat.Text = GetFAT_InKG(txtFAT.Text, txtD_MilkQuantity.Text).ToString();
        // decimal Fat = 0;
        // decimal Clr = 0;
        // decimal Snf = 0;
        // decimal KgFat = 0;
        // decimal KgSnf = 0;
        // decimal RcvdFat = 0;
        // decimal RcvdClr = 0;
        // decimal RcvdSnf = 0;
        // decimal RcvdKgFat = 0;
        // decimal RcvdKgSnf = 0;
        // if (txtFAT.Text == "") { Fat = 0; } else { Fat = Convert.ToDecimal(txtFAT.Text); }
        // if (txtCLR.Text == "") { Clr = 0; } else { Clr = Convert.ToDecimal(txtCLR.Text); }
        // if (txtSNF.Text == "") { Snf = 0; } else { Snf = Convert.ToDecimal(txtSNF.Text); }
        // if (txtKgFat.Text == "") { KgFat = 0; } else { KgFat = Convert.ToDecimal(txtKgFat.Text); }
        // if (txtKgSNF.Text == "") { KgSnf = 0; } else { KgSnf = Convert.ToDecimal(txtKgSNF.Text); }
        // foreach (GridViewRow gvrow in gvBMCDetails.Rows)
        // {

            // Label lblOffice_Name_E = (Label)gvrow.FindControl("lblOffice_Name_E");
            // Label lblD_MilkQuantity = (Label)gvrow.FindControl("lblD_MilkQuantity");
            // Label lblFAT = (Label)gvrow.FindControl("lblFAT");
            // Label lblCLR = (Label)gvrow.FindControl("lblCLR");
            // Label lblSNF = (Label)gvrow.FindControl("lblSNF");
            // Label lblKgFat = (Label)gvrow.FindControl("lblKgFat");
            // Label lblKgSNF = (Label)gvrow.FindControl("lblKgSNF");
            // if (lblOffice_Name_E.Text == "Received")
            // {
                // RcvdFat = decimal.Parse(lblFAT.Text);
                // RcvdClr = decimal.Parse(lblCLR.Text);
                // RcvdFat = decimal.Parse(lblFAT.Text);
                // RcvdSnf = decimal.Parse(lblSNF.Text);
                // RcvdKgFat = decimal.Parse(lblKgFat.Text);
                // RcvdKgSnf = decimal.Parse(lblKgSNF.Text);
            // }
            // if (lblOffice_Name_E.Text == "Difference")
            // {
                // lblFAT.Text = (RcvdFat - Fat).ToString();
                // lblCLR.Text = (RcvdClr - Clr).ToString();
                // lblSNF.Text = (RcvdSnf - Snf).ToString();
                // lblKgFat.Text = (RcvdKgFat - KgFat).ToString();
                // lblKgSNF.Text = (RcvdKgSnf - KgSnf).ToString();
            // }
        // }
    // }
    #endregion

}