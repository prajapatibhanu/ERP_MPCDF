using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;
using System.Web.UI;
public partial class mis_MilkCollection_ReceiveCanesAtQA : System.Web.UI.Page
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
               
                txtfilterdate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");

                //if (Session["IsSuccess"] != null)
                //{
                //    if ((Boolean)Session["IsSuccess"] == true)
                //    {
                //        Session["IsSuccess"] = false;
                //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                //    }
                //}

                
                FillGrid();
               GetCCDetails();
			   if(apiprocedure.OfficeType_ID() == "2")
                {
                   
                }
				else
                {
                    FillSociety();
                }
              //ddlccbmcdetail_SelectedIndexChanged(sender, e);
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

    private void GetReferenceInfo()
    {


        DataSet ds1 = apiprocedure.ByProcedure("Usp_Trn_MilkInwardReferenceDetails_RMRDViaCanes",
                                  new string[] { "flag", "I_OfficeID"},
                                  new string[] { "3", ddlUnitName.SelectedValue}, "dataset");
        if (ds1.Tables[0].Rows.Count != 0)
        {

            ddlReferenceNo.DataSource = ds1;
            ddlReferenceNo.DataTextField = "C_ReferenceNo";
            ddlReferenceNo.DataValueField = "BI_MilkInRMRDCanRefID";
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
            lblMsg.Text = "";
            gvmilkAdulterationtestdetail.DataSource = string.Empty;
            gvmilkAdulterationtestdetail.DataBind();
            
            //ddlMilkQuality.ClearSelection();
           // txttemperature.Text = "";
            txtNetFat.Text = "";
            txtNetCLR.Text = "";
            txtnetsnf.Text = "";
            
            divmqd.Visible = false;
            divadt.Visible = false;
            divaction.Visible = false;

            if (ddlReferenceNo.SelectedValue != "0")
            {
                DataSet ds1 = apiprocedure.ByProcedure("Usp_Trn_MilkInwardReferenceDetails_RMRDViaCanes",
                               new string[] { "flag", "BI_MilkInRMRDCanRefID"},
                               new string[] { "4", ddlReferenceNo.SelectedValue}, "dataset");

                if (ds1.Tables[0].Rows.Count != 0)
                {

                    ddlchallanno.DataSource = ds1;
                    ddlchallanno.DataTextField = "Challanno";
                    ddlchallanno.DataValueField = "I_SampleID";
                    ddlchallanno.DataBind();
                    ddlchallanno.Items.Insert(0, new ListItem("Select", "0"));


                }
                else
                {
                    ddlchallanno.DataSource = string.Empty;
                    ddlchallanno.DataBind();
                    ddlchallanno.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlchallanno.DataSource = string.Empty;
                ddlchallanno.DataBind();
                ddlchallanno.Items.Insert(0, new ListItem("Select", "0"));

            }

            //ddlchallanno_SelectedIndexChanged(sender, e);

        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }

    protected void ddlchallanno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            gvmilkAdulterationtestdetail.DataSource = string.Empty;
            gvmilkAdulterationtestdetail.DataBind();
 
            //ddlMilkQuality.ClearSelection();

           // txttemperature.Text = "";
            txtNetFat.Text = "";
            txtNetCLR.Text = "";
            txtnetsnf.Text = "";
            divmqd.Visible = false;
            divadt.Visible = false;
            divaction.Visible = false;
           

            if (ddlReferenceNo.SelectedValue != "0" && ddlchallanno.SelectedValue != "0")
            {


                DataSet ds1 = apiprocedure.ByProcedure("Usp_Trn_MilkInwardReferenceDetails_RMRDViaCanes",
                                                 new string[] { "flag", "BI_MilkInRMRDCanRefID", "I_SampleID" },
                                                 new string[] { "5", ddlReferenceNo.SelectedValue, ddlchallanno.SelectedValue }, "dataset");

                     if (ds1.Tables[0].Rows.Count != 0)
                     {
                         //ddlUnitName.Items.Add(new ListItem(ds1.Tables[0].Rows[0]["Office_Name"].ToString(), ds1.Tables[0].Rows[0]["I_OfficeID"].ToString()));
                         //ddlUnitName.SelectedValue = ds1.Tables[0].Rows[0]["I_OfficeID"].ToString();
                         //ddlUnitName.Enabled = false;

                         divmqd.Visible = true;
                         divadt.Visible = true;
                         divaction.Visible = true;


                         BindAdulterationTestGrid();
                         //ddlReferenceNo.Enabled = false;

                     }
                     else
                     {
                         ddlUnitName.ClearSelection();

                         divmqd.Visible = false;
                         divadt.Visible = false;
                         divaction.Visible = false;

                     }
              
            }
            else
            {
                ddlUnitName.ClearSelection();
               
                divmqd.Visible = false;
                divadt.Visible = false;
                divaction.Visible = false;
             
               // ddlReferenceNo.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            //btnAdd.Visible = false;
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
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
          //ds = apiprocedure.ByProcedure("Usp_Trn_MilkInwardReferenceDetails_RMRDViaCanes",
          //                   new string[] { "flag", "I_Office_ID", "EntryDate" },
          //                   new string[] { "7", apiprocedure.Office_ID(), date }, "dataset");
          ds = apiprocedure.ByProcedure("Usp_Trn_MilkInwardReferenceDetails_RMRDViaCanes",
                           new string[] { "flag", "CC_Id", "EntryDate" },
                           new string[] { "7", ddlCC_flt.SelectedValue, date }, "dataset");
         if(ds != null && ds.Tables.Count > 0)
         {
             if(ds.Tables[0].Rows.Count > 0)
             {
                 decimal TotalMilkQuantity = 0;

                 gvReceivedEntry.DataSource = ds;
                 gvReceivedEntry.DataBind();
                 TotalMilkQuantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("I_MilkQuantity"));
                
                 gvReceivedEntry.FooterRow.Cells[5].Text = "<b>Grand Total : </b>";
                 gvReceivedEntry.FooterRow.Cells[6].Text = "<b>" + TotalMilkQuantity.ToString() + "</b>";
                 gvReceivedEntry.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                
                 GetDatatableHeaderDesign();
                 GetDatatableFooterDesign();

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

    protected void FillSociety()
    {
        try
        {
            ddlUnitName.Items.Clear();
            if (ddlccbmcdetail.SelectedValue != "0")
            {
                ds = null;
                ds = apiprocedure.ByProcedure("Usp_Trn_MilkInwardReferenceDetails_RMRDViaCanes",
                                  new string[] { "flag", "Supplyunitparant_ID" },
                                  new string[] { "16", ddlccbmcdetail.SelectedValue }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlUnitName.DataTextField = "Office_Name";
                        ddlUnitName.DataValueField = "Office_ID";
                        ddlUnitName.DataSource = ds.Tables[0];
                        ddlUnitName.DataBind();
                        //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    else
                    {
                        //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
                else
                {
                    //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                Response.Redirect("RMRD_Challan_EntryFromCanes.aspx", false);
            }
            ddlUnitName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void GetCCDetails()
    {
        try
        {
            ds = null;
            ds = apiprocedure.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                     new string[] { "flag", "I_OfficeID", "I_OfficeTypeID" },
                     new string[] { "3", apiprocedure.Office_ID(), apiprocedure.OfficeType_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlccbmcdetail.DataTextField = "Office_Name";
                        ddlccbmcdetail.DataValueField = "Office_ID";


                        ddlccbmcdetail.DataSource = ds;
                        ddlccbmcdetail.DataBind();

                        ddlCC_flt.DataTextField = "Office_Name";
                        ddlCC_flt.DataValueField = "Office_ID";
                        ddlCC_flt.DataSource = ds;
                        ddlCC_flt.DataBind();

                        if (apiprocedure.OfficeType_ID() == "2")
                        {
                            ddlccbmcdetail.Items.Insert(0, new ListItem("Select", "0"));
                            ddlCC_flt.Items.Insert(0, new ListItem("Select", "0"));
                        }
                        else
                        {
                            ddlccbmcdetail.Items.Insert(0, new ListItem("Select", "0"));
                           ddlccbmcdetail.SelectedValue = apiprocedure.Office_ID();
                            //GetMCUDetails();
                            ddlccbmcdetail.Enabled = false;
                            ddlCC_flt.Items.Insert(0, new ListItem("Select", "0"));
                            ddlCC_flt.SelectedValue = apiprocedure.Office_ID();
                            //GetMCUDetails();
                            ddlCC_flt.Enabled = false;
                        }


                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
            
                dtTL.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));

                
                    for (int j = 0; j < DSAT.Tables[0].Rows.Count; j++)
                    {
                        drTL = dtTL.NewRow();
                        drTL[0] = dtTL.Rows.Count + 1;
                        
                        drTL[1] = DSAT.Tables[0].Rows[j]["V_AdulteratioTName"].ToString();
                        dtTL.Rows.Add(drTL);
                    }
               
            }

            gvmilkAdulterationtestdetail.DataSource = dtTL;
            gvmilkAdulterationtestdetail.DataBind();


        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
        }
    }

    //protected void ddlMilkQuality_Init(object sender, EventArgs e)
    //{
    //    ddlMilkQuality.DataSource = apiprocedure.ByProcedure("USP_Mst_MilkQualityList",
    //                            new string[] { "flag" },
    //                            new string[] { "1" }, "dataset");
    //    ddlMilkQuality.DataValueField = "V_MilkQualityList";
    //    ddlMilkQuality.DataTextField = "V_MilkQualityList";
    //    ddlMilkQuality.DataBind();
    //    ddlMilkQuality.Items.Insert(0, new ListItem("Select", "0"));
    //    ddlMilkQuality.SelectedValue = "Good";
		
    //}

    private DataTable GetAdulterationTestDetails()
    {

        DataTable dt = new DataTable();
        DataRow dr;        
        dt.Columns.Add(new DataColumn("V_AdulterationType", typeof(string)));
        dt.Columns.Add(new DataColumn("V_AdulterationValue", typeof(string)));

        foreach (GridViewRow row in gvmilkAdulterationtestdetail.Rows)
        {
           
            Label lblAdulterationType = (Label)row.FindControl("lblAdulterationType");
            DropDownList ddlAdelterationTestValue = (DropDownList)row.FindControl("ddlAdelterationTestValue");

            

            dr = dt.NewRow();
           
            dr[0] = lblAdulterationType.Text;
            dr[1] = ddlAdelterationTestValue.SelectedValue;
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
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
        return Math.Round(snf, 2);
    }
    private decimal GetGridSNF(string Fat,string Clr)
    {
        decimal snf = 0, clr = 0, fat = 1;
        try
        {
            //Formula for Get SNF as given below:
            if (Clr != "")
            { clr = Convert.ToDecimal(Clr); }
            if (Fat != "")
            { fat = Convert.ToDecimal(Fat); }

            //SNF = (CLR / 4) + (FAT * 0.2) + 0.7 -- Formula Before
            //SNF = (CLR / 4) + (0.25 * FAT) + 0.44 -- Formula After Mail dt: 27 Jan 2020, 16:26
            //snf = ((clr / 4) + (fat * Convert.ToDecimal(0.25)) + Convert.ToDecimal(0.44));
            snf = Obj_MC.GetSNFPer_DCS(fat, clr);
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
        return Math.Round(snf, 2);
    }
    protected void txtNetFat_TextChanged(object sender, EventArgs e)
    {
        txtnetsnf.Text = GetSNF().ToString();

        if (txtNetCLR.Text == "")
        {
            txtNetCLR.Focus();
        }

        if (txtNetFat.Text == "")
        {
            txtNetFat.Focus();
        }

    }

    protected void txtNetCLR_TextChanged(object sender, EventArgs e)
    {
        txtnetsnf.Text = GetSNF().ToString();

        if (txtNetCLR.Text == "")
        {
            txtNetCLR.Focus();
        }

        if (txtNetFat.Text == "")
        {
            txtNetFat.Focus();
        }

    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                 //Check adulteration details filled all row

                DataTable dtAdultration = new DataTable();
                dtAdultration = GetAdulterationTestDetails();

               
                if (dtAdultration.Rows.Count > 0)
                {

                    ds = null;
                    ds = apiprocedure.ByProcedure("Usp_Trn_MilkInwardReferenceDetails_RMRDViaCanes",
                            new string[] { "flag", 
                                                 "I_SampleID",
                                                 //"V_MilkQuality",
                                                 "D_FAT",
                                                 "D_SNF",
                                                 "D_CLR",
                                                 "V_Temp", 
                                                 "V_Acidity",
                                                 "I_Office_ID",
                                                 "EntryDate"
                                                 },

                                                new string[] { "6",
                                                ddlchallanno.SelectedValue,                                              
                                               // ddlMilkQuality.SelectedItem.Text,                                               
                                                txtNetFat.Text, 
                                                txtnetsnf.Text, 
                                                txtNetCLR.Text,
                                                txttemperature.Text, 
                                                txtACIDITY.Text,
                                                apiprocedure.Office_ID(),
                                                Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd")

                                                },
                                             new string[] { "type_Trn_tblAdulterationTest_RMRDViaCanes" },
                                             new DataTable[] { dtAdultration }, "TableSave");


                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        
                        lblMsg.Text = apiprocedure.Alert("fa-check", "alert-success", "Success!", "" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString() + "");
                        GetReferenceInfo();
                        ddlReferenceNo_SelectedIndexChanged(sender, e);
                        ddlUnitName.ClearSelection();
                        FillGrid();
                    }
                    else
                    {
                        lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1:" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        
                    }

                }
                else
                {
                    lblMsg.Text = apiprocedure.Alert("fa-warning", "alert-warning", "Ooops!", "Warning : Required All Adulteration Test!");
                    
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 2:" + ex.Message.ToString());
            
        }
    }
    private void GetDatatableFooterDesign()
    {
        try
        {
            if (gvReceivedEntry.Rows.Count > 0)
            {
                gvReceivedEntry.FooterRow.TableSection = TableRowSection.TableFooter;
                //gv_MilkCollectionChallanEntryDetails.foo = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (gvReceivedEntry.Rows.Count > 0)
            {
                gvReceivedEntry.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvReceivedEntry.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }



    protected void gvReceivedEntry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            Label lblV_MilkType = (Label)row.FindControl("lblV_MilkType");
            DropDownList gvddlV_MilkType = (DropDownList)row.FindControl("gvddlV_MilkType");

            Label lblV_MilkQuality = (Label)row.FindControl("lblV_MilkQuality");
            DropDownList gvddlV_MilkQuality = (DropDownList)row.FindControl("gvddlV_MilkQuality");
            Label lblI_MilkQuantity = (Label)row.FindControl("lblI_MilkQuantity");
            TextBox gvtxtI_MilkQuantity = (TextBox)row.FindControl("gvtxtI_MilkQuantity");
            Label lblD_FAT = (Label)row.FindControl("lblD_FAT");
            TextBox gvtxtD_FAT = (TextBox)row.FindControl("gvtxtD_FAT");
            Label lblD_CLR = (Label)row.FindControl("lblD_CLR");
            TextBox gvtxtD_CLR = (TextBox)row.FindControl("gvtxtD_CLR");
            Label lblD_SNF = (Label)row.FindControl("lblD_SNF");
            TextBox gvtxtD_SNF = (TextBox)row.FindControl("gvtxtD_SNF");
            Label lblV_Temp = (Label)row.FindControl("lblV_Temp");
            TextBox gvtxtV_Temp = (TextBox)row.FindControl("gvtxtV_Temp");
            Label lblV_Acidity = (Label)row.FindControl("lblV_Acidity");
            TextBox gvtxtV_Acidity = (TextBox)row.FindControl("gvtxtV_Acidity");

            LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
            LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
            string I_SampleID = e.CommandArgument.ToString();
            if (e.CommandName == "DeleteRecord")
            {
                ds = apiprocedure.ByProcedure("Usp_Trn_MilkInwardReferenceDetails_RMRDViaCanes", new string[] { "flag", "I_SampleID" }, new string[] { "11", I_SampleID }, "dataset");
                if(ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        
                        lblMsg.Text = apiprocedure.Alert("fa-check", "alert-success", "Success!", "" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString() + "");
                        GetReferenceInfo();
                        ddlReferenceNo_SelectedIndexChanged(sender, e);
                        ddlUnitName.ClearSelection();
                        FillGrid();
                    }
                    else
                    {
                        lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1:" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                       
                    }
                }
            }
            if (e.CommandName == "EditRecord")
            {
                lblI_MilkQuantity.Visible = false;
                lblD_FAT.Visible = false;
                lblD_CLR.Visible = false;
                lblD_SNF.Visible = false;
                lblV_Temp.Visible = false;
                lblV_Acidity.Visible = false;
                lnkEdit.Visible = false;
                lblV_MilkType.Visible = false;
                lblV_MilkQuality.Visible = false;

                gvtxtI_MilkQuantity.Visible = true;
                gvtxtD_FAT.Visible = true;
                gvtxtD_CLR.Visible = true;
                gvtxtD_SNF.Visible = true;
                gvtxtV_Temp.Visible = true;
                gvtxtV_Acidity.Visible = true;
                lnkUpdate.Visible = true;
                gvddlV_MilkType.Visible = true;
                gvddlV_MilkType.ClearSelection();
                gvddlV_MilkType.Items.FindByText(lblV_MilkType.Text).Selected = true;
                gvddlV_MilkQuality.Visible = true;
                gvddlV_MilkQuality.ClearSelection();
                gvddlV_MilkQuality.Items.FindByText(lblV_MilkQuality.Text).Selected = true;

            }
            if (e.CommandName == "UpdateRecord")
            {
                ds = apiprocedure.ByProcedure("Usp_Trn_MilkInwardReferenceDetails_RMRDViaCanes",
                                  new string[] { "flag", "I_SampleID", "I_MilkQuantity", "V_MilkType", "D_FAT", "D_SNF", "D_CLR", "V_Temp", "V_Acidity", "V_MilkQuality" },
                                  new string[] { "13",I_SampleID, gvtxtI_MilkQuantity.Text, gvddlV_MilkType.SelectedValue, gvtxtD_FAT.Text, gvtxtD_SNF.Text, gvtxtD_CLR.Text, gvtxtV_Temp.Text, gvtxtV_Acidity.Text,gvddlV_MilkQuality.SelectedValue }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {

                        lblMsg.Text = apiprocedure.Alert("fa-check", "alert-success", "Success!", "" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString() + "");
                        GetReferenceInfo();
                        ddlReferenceNo_SelectedIndexChanged(sender, e);
                        ddlUnitName.ClearSelection();
                        FillGrid();
                    }
                    else
                    {
                        lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1:" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());

                    }
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }

    protected void gvtxtD_FAT_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
       
        TextBox gvtxtD_CLR = (TextBox)row.FindControl("gvtxtD_CLR");
        TextBox gvtxtD_FAT = (TextBox)row.FindControl("gvtxtD_FAT");
        TextBox gvtxtD_SNF = (TextBox)row.FindControl("gvtxtD_SNF");
        gvtxtD_SNF.Text = GetGridSNF(gvtxtD_FAT.Text, gvtxtD_CLR.Text).ToString();
    }
    protected void gvtxtD_CLR_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);

        TextBox gvtxtD_CLR = (TextBox)row.FindControl("gvtxtD_CLR");
        TextBox gvtxtD_FAT = (TextBox)row.FindControl("gvtxtD_FAT");
        TextBox gvtxtD_SNF = (TextBox)row.FindControl("gvtxtD_SNF");
        gvtxtD_SNF.Text = GetGridSNF(gvtxtD_FAT.Text, gvtxtD_CLR.Text).ToString();
    }
    protected void ddlUnitName_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        GetReferenceInfo();
        ddlReferenceNo_SelectedIndexChanged(sender, e);
    }
    protected void ddlccbmcdetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSociety();
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
}