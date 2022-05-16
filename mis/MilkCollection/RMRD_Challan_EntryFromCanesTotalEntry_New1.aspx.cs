using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Collections.Generic;
using System.Web.UI;
using System.Web;
using System.Data.OleDb;


public partial class mis_MilkCollection_RMRD_Challan_EntryFromCanesTotalEntry_New1 : System.Web.UI.Page
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

                txtfilterdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtfilterdate.Attributes.Add("readonly", "readonly");
               
                GetCCDetails();
                //FillSociety();
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

    protected void GetCCDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                   new string[] { "flag", "I_OfficeID", "I_OfficeTypeID" },
                   new string[] { "3", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlCC.DataTextField = "Office_Name";
                        ddlCC.DataValueField = "Office_ID";
                        ddlCC.DataSource = ds;
                        ddlCC.DataBind();
                        ddlCC.Items.Insert(0, new ListItem("Select", "0"));

                        ddlCCflt.DataTextField = "Office_Name";
                        ddlCCflt.DataValueField = "Office_ID";
                        ddlCCflt.DataSource = ds;
                        ddlCCflt.DataBind();
                        ddlCCflt.Items.Insert(0, new ListItem("Select", "0"));
						if(objdb.OfficeType_ID() == "4")
                        {
                            ddlCC.SelectedValue = objdb.Office_ID();
							ddlCCflt.SelectedValue = objdb.Office_ID();
                        }

                    }
                }
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillSociety()
    {
        try
        {
			Session["ds2"]= "";
            ds = null;

            //ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
            //           new string[] { "flag", "BMCTankerRoot_Id", "Office_ID", "OfficeType_ID" },
            //           new string[] { "21", ddlBMCTankerRootName.SelectedValue, objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                          new string[] { "flag", "Office_ID", "Office_Parant_ID", "OfficeType_ID" },
                          new string[] { "21", ddlCC.SelectedValue.ToString(),objdb.Office_ID(),objdb.OfficeType_ID()}, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Session["ds2"] = ds;
                    //ddlSociety.DataTextField = "Office_Name";
                    //ddlSociety.DataValueField = "Office_ID";
                    //ddlSociety.DataSource = ds.Tables[0];
                    //ddlSociety.DataBind();
                    //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    // ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                // ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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

            Label lblOffice_ID = (Label)row.FindControl("lblOffice_ID");
            HiddenField hf = (HiddenField)row.FindControl("hfOffice_ID");
            DropDownList ddlBufMilkQuality = (DropDownList)row.FindControl("ddlBufMilkQuality");
            TextBox txtBufMilkQuantity = (TextBox)row.FindControl("txtBufMilkQuantity");
            TextBox txtBufFat = (TextBox)row.FindControl("txtBufFat");
            TextBox txtBufCLR = (TextBox)row.FindControl("txtBufCLR");


            decimal Snf = 0, FatInKg = 0, SnfInKg = 0;
            Snf = GetSNF(txtBufFat.Text, txtBufCLR.Text);
            FatInKg = GetFAT_InKG(txtBufMilkQuantity.Text, txtBufFat.Text);
            SnfInKg = GetSNF_InKG(txtBufMilkQuantity.Text, txtBufFat.Text, txtBufCLR.Text);
            if (txtBufMilkQuantity.Text != "")
            {
                dr = dt.NewRow();
                dr[0] = hf.Value;

                dr[1] = "Buf";
                dr[2] = "4";
                dr[3] = DBNull.Value;
                dr[4] = txtBufMilkQuantity.Text;
                dr[5] = ddlBufMilkQuality.SelectedValue;
                dr[6] = txtBufFat.Text;
                dr[7] = Snf;
                dr[8] = txtBufCLR.Text;
                dr[9] = FatInKg;
                dr[10] = SnfInKg;
                dr[11] = DBNull.Value;
                dr[12] = DBNull.Value;
                dt.Rows.Add(dr);
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

            Label lblOffice_ID = (Label)row.FindControl("lblOffice_ID");

            DropDownList ddlCowMilkQuality = (DropDownList)row.FindControl("ddlCowMilkQuality");
            HiddenField hf = (HiddenField)row.FindControl("hfOffice_ID");
            TextBox txtCowMilkQuantity = (TextBox)row.FindControl("txtCowMilkQuantity");
            TextBox txtCowFat = (TextBox)row.FindControl("txtCowFat");
            TextBox txtCowCLR = (TextBox)row.FindControl("txtCowCLR");


            decimal Snf = 0, FatInKg = 0, SnfInKg = 0;
            Snf = GetSNF(txtCowFat.Text, txtCowCLR.Text);
            FatInKg = GetFAT_InKG(txtCowMilkQuantity.Text, txtCowFat.Text);
            SnfInKg = GetSNF_InKG(txtCowMilkQuantity.Text, txtCowFat.Text, txtCowCLR.Text);
            if (txtCowMilkQuantity.Text != "")
            {
                dr = dt.NewRow();
                dr[0] = hf.Value;

                dr[1] = "Cow";
                dr[2] = "4";
                dr[3] = DBNull.Value;
                dr[4] = txtCowMilkQuantity.Text;
                dr[5] = ddlCowMilkQuality.SelectedValue;
                dr[6] = txtCowFat.Text;
                dr[7] = Snf;
                dr[8] = txtCowCLR.Text;
                dr[9] = FatInKg;
                dr[10] = SnfInKg;
                dr[11] = DBNull.Value;
                dr[12] = DBNull.Value;
                dt.Rows.Add(dr);
            }
        }

        return dt;
    }
    protected void FillGrid()
    {


        try
        {
             gvEntryList.DataSource = string.Empty;
            gvEntryList.DataBind();
            gvDbf.DataSource = string.Empty;
            gvDbf.DataBind();
            btnExport.Visible = false;
            ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntryViaCanes", new string[] { "flag", "Created_Office_ID", "EntryDate", "CC_Id" }, new string[] { "7", objdb.Office_ID(), Convert.ToDateTime(txtfilterdate.Text, cult).ToString("yyyy/MM/dd"),ddlCCflt.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvEntryList.DataSource = ds.Tables[0];
                gvEntryList.DataBind();
                gvEntryList.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvEntryList.UseAccessibleHeader = true;
                decimal TotalMilkQty = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("MilkQuantity"));
                gvEntryList.FooterRow.Cells[7].Text = "<b>Total : </b>";
                gvEntryList.FooterRow.Cells[8].Text = "<b>" + TotalMilkQty.ToString() + "</b>";
				gvDbf.DataSource = ds.Tables[0];
                gvDbf.DataBind();
                
                btnExport.Visible = true;
                //GetDatatableHeaderDesign();
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

                ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntryViaCanes",
                  new string[] 
                     {"flag"
                      ,"EntryDate"
                      ,"CC_Id"
                      ,"Shift"
                      ,"IsActive"
                      ,"CreatedBy"
                      ,"CreatedByIP"
                      ,"Created_Office_ID"
					  ,"OfficeType_ID"
                     },
                  new string[] 
                     {"6"
                      ,Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd")
                     ,ddlCC.SelectedValue 
                      ,ddlShift.SelectedItem.Text
                      ,IsActive                          
                      ,objdb.createdBy()
                      ,objdb.GetLocalIPAddress() 
                      ,objdb.Office_ID()
					  ,objdb.OfficeType_ID()
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
                            divDetails.Visible = false;
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
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
        return Math.Round(snf, 3);
    }

    private decimal GetSNF_InKG(string MilkQuantity, string FAT, string CLR)
    {
        decimal clr = 0, fat = 0, snf_Per = 0, MilkQty = 0, SNF_InKG = 0;

        try
        {
            if (MilkQuantity == "") { MilkQty = 0; } else { MilkQty = Convert.ToDecimal(MilkQuantity); }

            if (FAT == "") { fat = 0; } else { fat = Convert.ToDecimal(FAT); }

            if (CLR == "") { clr = 0; } else { clr = Convert.ToDecimal(CLR); }

            snf_Per = Obj_MC.GetSNFPer_DCS(fat, clr);

            SNF_InKG = Obj_MC.GetSNFInKg(MilkQty, snf_Per);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }

        return Math.Round(SNF_InKG, 3);
    }

    private decimal GetFAT_InKG(string MilkQuantity, string FAT)
    {
        decimal fat_Per = 0, MilkQty = 0, FAT_InKG = 0;

        try
        {
            if (MilkQuantity == "") { MilkQty = 0; } else { MilkQty = Convert.ToDecimal(MilkQuantity); }

            if (FAT == "") { fat_Per = 0; } else { fat_Per = Convert.ToDecimal(FAT); }

            FAT_InKG = Obj_MC.GetSNFInKg(MilkQty, fat_Per);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }

        return Math.Round(FAT_InKG, 3);
    }


    protected void gvEntryList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string MilkCollectionViaCanesChallan_ID = e.CommandArgument.ToString();
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblMilkQuantity = (Label)row.FindControl("lblMilkQuantity");
            Label lblFat = (Label)row.FindControl("lblFat");
            Label lblSnf = (Label)row.FindControl("lblSnf");
            Label lblClr = (Label)row.FindControl("lblClr");
            Label lblFatInKg = (Label)row.FindControl("lblFatInKg");
            Label lblSnfInKg = (Label)row.FindControl("lblSnfInKg");
            TextBox txtMilkQuantity = (TextBox)row.FindControl("txtMilkQuantity");
            TextBox txtFat = (TextBox)row.FindControl("txtFat");
            TextBox txtClr = (TextBox)row.FindControl("txtClr");
            TextBox txtSnf = (TextBox)row.FindControl("txtSnf");
            TextBox txtFatInKg = (TextBox)row.FindControl("txtFatInKg");
            TextBox txtSnfInKg = (TextBox)row.FindControl("txtSnfInKg");
            Label lblV_MilkType = (Label)row.FindControl("lblMilkType");
            
            Label lblMilkQuality = (Label)row.FindControl("lblMilkQuality");
            DropDownList ddlMilkQuality = (DropDownList)row.FindControl("ddlMilkQuality");
            LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
            LinkButton lnkDelete = (LinkButton)row.FindControl("lnkDelete");
            LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
           if (e.CommandName == "DeleteRecord")
           {
               objdb.ByProcedure("Usp_MilkCollectionChallanEntryViaCanes", new string[] { "flag", "MilkCollectionViaCanesChallan_ID" }, new string[] { "3", MilkCollectionViaCanesChallan_ID }, "dataset");

               lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", "Record Deleted Successfully.");
               FillGrid();
           }
            if (e.CommandName == "EditRecord")
            {
                lblMilkQuantity.Visible = false;
                lblFat.Visible = false;
                lblSnf.Visible = false;
                lblClr.Visible = false;
                lblFatInKg.Visible = false;
                lblSnfInKg.Visible = false;
               
                
                ddlMilkQuality.Visible = true;
                lblMilkQuality.Visible = false;
                txtMilkQuantity.Visible = true;
                txtFat.Visible = true;
                txtClr.Visible = true;
                txtSnf.Visible = true;
                txtFatInKg.Visible = true;
                txtSnfInKg.Visible = true;
                lnkEdit.Visible = false;
                lnkDelete.Visible = false;
                lnkUpdate.Visible = true;
                //objdb.ByProcedure("Usp_MilkCollectionChallanEntryViaCanes", new string[] { "flag", "MilkCollectionViaCanesChallan_ID" }, new string[] { "3", MilkCollectionViaCanesChallan_ID }, "dataset");

                //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", "Record Deleted Successfully.");
                //FillGrid();
            }
            if (e.CommandName == "UpdateRecord")
            {
                objdb.ByProcedure("Usp_MilkCollectionChallanEntryViaCanes",
                                new string[] { "flag", "MilkCollectionViaCanesChallan_ID", "MilkQuality", "MilkQuantity", "Fat", "Snf", "Clr", "FatInKg", "SnfInKg" }, 
                                new string[] { "9", MilkCollectionViaCanesChallan_ID,ddlMilkQuality.SelectedValue, txtMilkQuantity.Text,txtFat.Text,txtSnf.Text,txtClr.Text,txtFatInKg.Text,txtSnfInKg.Text }, "dataset");
               
                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", "Record Updated Successfully.");
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
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "< ---- Buf ---- >";
            HeaderCell.ColumnSpan = 4;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "< ---- Cow ---- >";
            HeaderCell.ColumnSpan = 4;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);
            gvBMCMilkDetails.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }

    //protected void txtBufFat_TextChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
    //    TextBox txtBufMilkQuantity = (TextBox)row.FindControl("txtBufMilkQuantity");
    //    TextBox txtBufFat = (TextBox)row.FindControl("txtBufFat");
    //    TextBox txtBufCLR = (TextBox)row.FindControl("txtBufCLR");
    //    TextBox txtBufSNF = (TextBox)row.FindControl("txtBufSNF");
    //    TextBox txtBufFatinkg = (TextBox)row.FindControl("txtBufFatinkg");
    //    TextBox txtBufSnfinkg = (TextBox)row.FindControl("txtBufSnfinkg");
    //    txtBufSNF.Text = GetSNF(txtBufFat.Text, txtBufCLR.Text).ToString();


    //    SetFocus(txtBufCLR);

    //}
    //protected void txtBufCLR_TextChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
    //    TextBox txtBufMilkQuantity = (TextBox)row.FindControl("txtBufMilkQuantity");
    //    TextBox txtCowMilkQuantity = (TextBox)row.FindControl("txtCowMilkQuantity");
    //    TextBox txtBufFat = (TextBox)row.FindControl("txtBufFat");
    //    TextBox txtBufCLR = (TextBox)row.FindControl("txtBufCLR");
    //    TextBox txtBufSNF = (TextBox)row.FindControl("txtBufSNF");
    //    TextBox txtBuftotalCan = (TextBox)row.FindControl("txtBuftotalCan");
    //    TextBox txtBufFatinkg = (TextBox)row.FindControl("txtBufFatinkg");
    //    TextBox txtBufSnfinkg = (TextBox)row.FindControl("txtBufSnfinkg");
    //    txtBufSNF.Text = GetSNF(txtBufFat.Text, txtBufCLR.Text).ToString();



    //    SetFocus(txtCowMilkQuantity);
    //}

    //protected void txtCowFat_TextChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
    //    TextBox txtCowMilkQuantity = (TextBox)row.FindControl("txtCowMilkQuantity");
    //    TextBox txtCowFat = (TextBox)row.FindControl("txtCowFat");
    //    TextBox txtCowCLR = (TextBox)row.FindControl("txtCowCLR");
    //    TextBox txtCowSNF = (TextBox)row.FindControl("txtCowSNF");
    //    TextBox txtCowFatinkg = (TextBox)row.FindControl("txtCowFatinkg");
    //    TextBox txtCowSnfinkg = (TextBox)row.FindControl("txtCowSnfinkg");
    //    txtCowSNF.Text = GetSNF(txtCowFat.Text, txtCowCLR.Text).ToString();



    //    //SetFocus(txtCowCLR);
    //}
    //protected void txtCowCLR_TextChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
    //    TextBox txtCowMilkQuantity = (TextBox)row.FindControl("txtCowMilkQuantity");
    //    TextBox txtCowFat = (TextBox)row.FindControl("txtCowFat");
    //    TextBox txtCowCLR = (TextBox)row.FindControl("txtCowCLR");
    //    TextBox txtCowSNF = (TextBox)row.FindControl("txtCowSNF");
    //    TextBox txtCowtotalCan = (TextBox)row.FindControl("txtCowtotalCan");
    //    TextBox txtCowFatinkg = (TextBox)row.FindControl("txtCowFatinkg");
    //    TextBox txtCowSnfinkg = (TextBox)row.FindControl("txtCowSnfinkg");
    //    txtCowSNF.Text = GetSNF(txtCowFat.Text, txtCowCLR.Text).ToString();


    //    //SetFocus(txtCowtotalCan);
    //}



    protected void btnAdd_Click(object sender, EventArgs e)
    {
        divDetails.Visible = true;
        FillSociety();
        SetInFlowInitialRow();
    }
    protected void btnGetTotal_Click(object sender, EventArgs e)
    {
        decimal BufMilkQuantity = 0;
        decimal CowMilkQuantity = 0;
        decimal BufFat = 0;
        decimal CowFat = 0;
        decimal BufCLR = 0;
        decimal CowCLR = 0;
        decimal BufCount = 0;
        decimal CowCount = 0;
        decimal BufAvgFat = 0;
        decimal CowAvgFat = 0;
        foreach (GridViewRow rows in gvBMCMilkDetails.Rows)
        {

            TextBox txtBufMilkQuantity = (TextBox)rows.FindControl("txtBufMilkQuantity");
            TextBox txtCowMilkQuantity = (TextBox)rows.FindControl("txtCowMilkQuantity");
            TextBox txtBufFat = (TextBox)rows.FindControl("txtBufFat");
            TextBox txtBufCLR = (TextBox)rows.FindControl("txtBufCLR");
            TextBox txtCowFat = (TextBox)rows.FindControl("txtCowFat");
            TextBox txtCowCLR = (TextBox)rows.FindControl("txtCowCLR");
            if (txtBufMilkQuantity.Text != "")
            {
                BufMilkQuantity += decimal.Parse(txtBufMilkQuantity.Text);

            }
            if (txtCowMilkQuantity.Text != "")
            {
                CowMilkQuantity += decimal.Parse(txtCowMilkQuantity.Text);
            }
            if (txtBufFat.Text != "")
            {
                BufCount += 1;

                BufFat += decimal.Parse(txtBufFat.Text);
                BufAvgFat = (BufFat / BufCount);

            }
            if (txtCowFat.Text != "")
            {

                CowCount += 1;
                CowFat += decimal.Parse(txtCowFat.Text);
                CowAvgFat = (CowFat / CowCount);
            }
            if (txtBufCLR.Text != "")
            {


                BufCLR += decimal.Parse(txtBufCLR.Text);

            }
            if (txtCowCLR.Text != "")
            {

                CowCLR += decimal.Parse(txtCowCLR.Text);


            }

        }

        gvBMCMilkDetails.FooterRow.Cells[3].Text = "TOTAL";
        gvBMCMilkDetails.FooterRow.Cells[3].Font.Bold = true;
        gvBMCMilkDetails.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;

        gvBMCMilkDetails.FooterRow.Cells[4].Text = BufMilkQuantity.ToString();
        gvBMCMilkDetails.FooterRow.Cells[4].Font.Bold = true;
        gvBMCMilkDetails.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;

        gvBMCMilkDetails.FooterRow.Cells[5].Text = BufFat.ToString();
        gvBMCMilkDetails.FooterRow.Cells[5].Font.Bold = true;
        gvBMCMilkDetails.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;

        gvBMCMilkDetails.FooterRow.Cells[6].Text = BufCLR.ToString();
        gvBMCMilkDetails.FooterRow.Cells[6].Font.Bold = true;
        gvBMCMilkDetails.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;

        gvBMCMilkDetails.FooterRow.Cells[8].Text = CowMilkQuantity.ToString();
        gvBMCMilkDetails.FooterRow.Cells[8].Font.Bold = true;
        gvBMCMilkDetails.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Left;

        gvBMCMilkDetails.FooterRow.Cells[9].Text = CowFat.ToString();
        gvBMCMilkDetails.FooterRow.Cells[9].Font.Bold = true;
        gvBMCMilkDetails.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Left;

        gvBMCMilkDetails.FooterRow.Cells[10].Text = CowCLR.ToString();
        gvBMCMilkDetails.FooterRow.Cells[10].Font.Bold = true;
        gvBMCMilkDetails.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Left;

        btnSave.Enabled = true;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<string> SearchSociety(string prefix)
    {
        List<string> society = new List<string>();
        try
        {
            DataView dv = new DataView();
			DataSet ds6 = (DataSet)HttpContext.Current.Session["ds2"];
            dv = ds6.Tables[0].DefaultView;
            dv.RowFilter = "Office_Name like '%" + prefix + "%'";
            DataTable dt = dv.ToTable();

            foreach (DataRow rs in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    society.Add(rs[col].ToString());
                }
            }

        }
        catch { }
        return society;
    }
    private void SetInFlowInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;


        dt.Columns.Add(new DataColumn("Office_Name", typeof(string)));
        dt.Columns.Add(new DataColumn("SocietyName", typeof(string)));
        dt.Columns.Add(new DataColumn("Office_ID", typeof(string)));
        dt.Columns.Add(new DataColumn("QualityC", typeof(string)));
        dt.Columns.Add(new DataColumn("QuantityC", typeof(string)));
        dt.Columns.Add(new DataColumn("FATC", typeof(string)));
        dt.Columns.Add(new DataColumn("CLRC", typeof(string)));
        dt.Columns.Add(new DataColumn("QualityB", typeof(string)));
        dt.Columns.Add(new DataColumn("QuantityB", typeof(string)));
        dt.Columns.Add(new DataColumn("FATB", typeof(string)));
        dt.Columns.Add(new DataColumn("CLRB", typeof(string)));


        dr = dt.NewRow();

        dr["Office_Name"] = string.Empty;
        dr["SocietyName"] = string.Empty;
        dr["Office_ID"] = string.Empty;
        dr["QualityC"] = string.Empty;
        dr["QuantityC"] = string.Empty;
        dr["FATC"] = string.Empty;
        dr["CLRC"] = string.Empty;
        dr["QualityB"] = string.Empty;
        dr["QuantityB"] = string.Empty;
        dr["FATB"] = string.Empty;
        dr["CLRB"] = string.Empty;

        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;

        gvBMCMilkDetails.DataSource = dt;
        gvBMCMilkDetails.DataBind();
        foreach (GridViewRow rows in gvBMCMilkDetails.Rows)
        {
            TextBox Society = (TextBox)rows.FindControl("txtSociety");
            SetFocus(Society);
        }



    }
    private void AddInFlowNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values

                    TextBox Society = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[1].FindControl("txtSociety");
                    TextBox SocietyName = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[2].FindControl("txtSocietyName");
                    HiddenField Office_ID = (HiddenField)gvBMCMilkDetails.Rows[rowIndex].Cells[2].FindControl("hfOffice_ID");
                    DropDownList ddlQltyB = (DropDownList)gvBMCMilkDetails.Rows[rowIndex].Cells[3].FindControl("ddlBufMilkQuality");
                    TextBox QtyB = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[4].FindControl("txtBufMilkQuantity");
                    TextBox FATB = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[5].FindControl("txtBufFat");
                    TextBox CLRB = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[6].FindControl("txtBufCLR");
                    DropDownList ddlQltyC = (DropDownList)gvBMCMilkDetails.Rows[rowIndex].Cells[7].FindControl("ddlCowMilkQuality");
                    TextBox QtyC = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[8].FindControl("txtCowMilkQuantity");
                    TextBox FATC = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[9].FindControl("txtCowFat");
                    TextBox CLRC = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[10].FindControl("txtCowCLR");




                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["Office_Name"] = string.Empty;
                    drCurrentRow["SocietyName"] = string.Empty;
                    drCurrentRow["Office_ID"] = string.Empty;
                    drCurrentRow["QualityC"] = "Good";
                    drCurrentRow["QuantityC"] = string.Empty;
                    drCurrentRow["FATC"] = string.Empty;
                    drCurrentRow["CLRC"] = string.Empty;
                    drCurrentRow["QualityB"] = "Good";
                    drCurrentRow["QuantityB"] = string.Empty;
                    drCurrentRow["FATB"] = string.Empty;
                    drCurrentRow["CLRB"] = string.Empty;


                    dtCurrentTable.Rows[i - 1]["Office_Name"] = Society.Text;
                    dtCurrentTable.Rows[i - 1]["SocietyName"] = SocietyName.Text;
                    dtCurrentTable.Rows[i - 1]["Office_ID"] = Office_ID.Value;
                    dtCurrentTable.Rows[i - 1]["QualityC"] = ddlQltyC.SelectedItem.Text;
                    dtCurrentTable.Rows[i - 1]["QuantityC"] = QtyC.Text;
                    dtCurrentTable.Rows[i - 1]["FATC"] = FATC.Text;
                    dtCurrentTable.Rows[i - 1]["CLRC"] = CLRC.Text;
                    dtCurrentTable.Rows[i - 1]["QualityB"] = ddlQltyB.SelectedItem.Text;
                    dtCurrentTable.Rows[i - 1]["QuantityB"] = QtyB.Text;
                    dtCurrentTable.Rows[i - 1]["FATB"] = FATB.Text;
                    dtCurrentTable.Rows[i - 1]["CLRB"] = CLRB.Text;


                    rowIndex++;
                }

                if (drCurrentRow != null)
                {
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    gvBMCMilkDetails.DataSource = dtCurrentTable;
                    gvBMCMilkDetails.DataBind();
                }
            }

        }


        else
        {
            Response.Write("ViewState is null");
            ViewState["CurrentTable"] = null;
        }

        //Set Previous Data on Postbacks


        SetInFlowPreviousData();
    }

    private void SetInFlowPreviousData()
    {
        try
        {
            int rowIndex = 0;
            double Total = 0;

            if (ViewState["CurrentTable"] != null)
            {

                DataTable dt = (DataTable)ViewState["CurrentTable"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox Society = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[1].FindControl("txtSociety");
                        HiddenField Office_ID = (HiddenField)gvBMCMilkDetails.Rows[rowIndex].Cells[2].FindControl("hfOffice_ID");
                        TextBox SocietyName = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[2].FindControl("txtSocietyName");
                        DropDownList ddlQltyB = (DropDownList)gvBMCMilkDetails.Rows[rowIndex].Cells[3].FindControl("ddlBufMilkQuality");
                        TextBox QtyB = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[4].FindControl("txtBufMilkQuantity");
                        TextBox FATB = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[5].FindControl("txtBufFat");
                        TextBox CLRB = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[6].FindControl("txtBufCLR");
                        DropDownList ddlQltyC = (DropDownList)gvBMCMilkDetails.Rows[rowIndex].Cells[7].FindControl("ddlCowMilkQuality");
                        TextBox QtyC = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[8].FindControl("txtCowMilkQuantity");
                        TextBox FATC = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[9].FindControl("txtCowFat");
                        TextBox CLRC = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[10].FindControl("txtCowCLR");


                        //ddlCat.Text = dt.Rows[i]["Column1"].ToString();
                        Society.Text = dt.Rows[i]["Office_Name"].ToString();
                        SocietyName.Text = dt.Rows[i]["SocietyName"].ToString();
                        Office_ID.Value = dt.Rows[i]["Office_ID"].ToString();
                        ddlQltyB.SelectedItem.Text = dt.Rows[i]["QualityB"].ToString();
                        QtyB.Text = dt.Rows[i]["QuantityB"].ToString();
                        FATB.Text = dt.Rows[i]["FATB"].ToString();
                        CLRB.Text = dt.Rows[i]["CLRB"].ToString();
                        ddlQltyC.SelectedItem.Text = dt.Rows[i]["QualityC"].ToString();
                        QtyC.Text = dt.Rows[i]["QuantityC"].ToString();
                        FATC.Text = dt.Rows[i]["FATC"].ToString();
                        CLRC.Text = dt.Rows[i]["CLRC"].ToString();

                        rowIndex++;
                        SetFocus(Society);
                        if (Society.Text != "")
                        {
                            QtyB.Enabled = true;
                            QtyC.Enabled = true;
                        }
                        if (QtyB.Text != "" && decimal.Parse(QtyB.Text) != 0)
                        {
                            FATB.Enabled = true;
                            CLRB.Enabled = true;
                        }
                        if (QtyC.Text != "" && decimal.Parse(QtyC.Text) != 0)
                        {
                            FATC.Enabled = true;
                            CLRC.Enabled = true;
                        }

                    }
                }





            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    //protected void ButtonAdd_Click(object sender, EventArgs e)
    //{
    //    ViewState["InFlowrowID"] = "";
    //    Button btn = (Button)sender;

    //    //Get the row that contains this button
    //    GridViewRow row = (GridViewRow)btn.NamingContainer;
      
    //    TextBox txtBufMilkQuantity = (TextBox)row.FindControl("txtBufMilkQuantity");
    //    TextBox txtCowMilkQuantity = (TextBox)row.FindControl("txtCowMilkQuantity");
    //    if (txtBufMilkQuantity.Text == "" && txtCowMilkQuantity.Text == "")
    //    {
          
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('कृपया गाय या भैंस की मात्रा दर्ज करें');", true);
    //    }
    //    //BindGrid();
    //    AddInFlowNewRowToGrid();
    //}
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        Button lb = (Button)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        ViewState["InFlowrowID"] = rowID.ToString();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 1)
            {
                if (gvRow.RowIndex < dt.Rows.Count)
                {
                    //Remove the Selected Row data and reset row number  
                    dt.Rows.Remove(dt.Rows[rowID]);

                }
            }

            //Store the current data in ViewState for future reference  
            ViewState["CurrentTable"] = dt;

            //Re bind the GridView for the updated data  
            gvBMCMilkDetails.DataSource = dt;
            gvBMCMilkDetails.DataBind();
        }

        //Set Previous Data on Postbacks  
        SetInFlowPreviousOnRemove();
    }
    protected void SetInFlowPreviousOnRemove()
    {
        int rowIndex = 0;
        double Total = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox Society = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[1].FindControl("txtSociety");
                    HiddenField Office_ID = (HiddenField)gvBMCMilkDetails.Rows[rowIndex].Cells[2].FindControl("hfOffice_ID");
                    DropDownList ddlQltyB = (DropDownList)gvBMCMilkDetails.Rows[rowIndex].Cells[2].FindControl("ddlBufMilkQuality");
                    TextBox QtyB = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[3].FindControl("txtBufMilkQuantity");
                    TextBox FATB = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[4].FindControl("txtBufFat");
                    TextBox CLRB = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[5].FindControl("txtBufCLR");
                    DropDownList ddlQltyC = (DropDownList)gvBMCMilkDetails.Rows[rowIndex].Cells[6].FindControl("ddlCowMilkQuality");
                    TextBox QtyC = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[7].FindControl("txtCowMilkQuantity");
                    TextBox FATC = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[8].FindControl("txtCowFat");
                    TextBox CLRC = (TextBox)gvBMCMilkDetails.Rows[rowIndex].Cells[9].FindControl("txtCowCLR");


                    //ddlCat.Text = dt.Rows[i]["Column1"].ToString();
                    Society.Text = dt.Rows[i]["Office_Name"].ToString();
                    Office_ID.Value = dt.Rows[i]["Office_ID"].ToString();
                    ddlQltyB.SelectedItem.Text = dt.Rows[i]["QualityB"].ToString();
                    QtyB.Text = dt.Rows[i]["QuantityB"].ToString();
                    FATB.Text = dt.Rows[i]["FATB"].ToString();
                    CLRB.Text = dt.Rows[i]["CLRB"].ToString();
                    ddlQltyC.SelectedItem.Text = dt.Rows[i]["QualityC"].ToString();
                    QtyC.Text = dt.Rows[i]["QuantityC"].ToString();
                    FATC.Text = dt.Rows[i]["FATC"].ToString();
                    CLRC.Text = dt.Rows[i]["CLRC"].ToString();
                    rowIndex++;
                    if (Society.Text != "")
                    {
                        QtyB.Enabled = true;
                        QtyC.Enabled = true;
                    }
                    if (QtyB.Text != "" && decimal.Parse(QtyB.Text) != 0)
                    {
                        FATB.Enabled = true;
                        CLRB.Enabled = true;
                    }
                    if (QtyC.Text != "" && decimal.Parse(QtyC.Text) != 0)
                    {
                        FATC.Enabled = true;
                        CLRC.Enabled = true;
                    }

                }

            }

        }
    }
    protected void txtBufMilkQuantity_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox txtBufMilkQuantity = (TextBox)row.FindControl("txtBufMilkQuantity");
        Label lblBufMilkQuantity = (Label)row.FindControl("lblBufMilkQuantity");
        TextBox txtBufFat = (TextBox)row.FindControl("txtBufFat");
        TextBox txtBufCLR = (TextBox)row.FindControl("txtBufCLR");
        RequiredFieldValidator rfvBufFat = (RequiredFieldValidator)row.FindControl("rfvBufFat");
        RequiredFieldValidator rfvBufCLR = (RequiredFieldValidator)row.FindControl("rfvBufCLR");
        
        if (txtBufMilkQuantity.Text != "" && decimal.Parse(txtBufMilkQuantity.Text) > 0)
        {
            txtBufFat.Enabled = true;
            txtBufCLR.Enabled = true;
            rfvBufFat.Enabled = true;
            lblBufMilkQuantity.Visible = false;
            rfvBufCLR.Enabled = true;
            SetFocus(txtBufFat);
        }
        else
        {
            txtBufFat.Text = "";
            txtBufCLR.Text = "";
            //txtBufMilkQuantity.Text = "";
            txtBufFat.Enabled = false;
            txtBufCLR.Enabled = false;
            rfvBufFat.Enabled = false;
            rfvBufCLR.Enabled = false;
            lblBufMilkQuantity.Visible = true;
            SetFocus(txtBufMilkQuantity);
        }

    }
    protected void txtCowMilkQuantity_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        Label lblCowMilkQuantity = (Label)row.FindControl("lblCowMilkQuantity");
        TextBox txtCowMilkQuantity = (TextBox)row.FindControl("txtCowMilkQuantity");
        TextBox txtCowFat = (TextBox)row.FindControl("txtCowFat");
        TextBox txtCowCLR = (TextBox)row.FindControl("txtCowCLR");
        RequiredFieldValidator rfvCowFat = (RequiredFieldValidator)row.FindControl("rfvCowFat");
        RequiredFieldValidator rfvCowCLR = (RequiredFieldValidator)row.FindControl("rfvCowCLR");
        if (txtCowMilkQuantity.Text != "" && decimal.Parse(txtCowMilkQuantity.Text) > 0)
        {
            txtCowFat.Enabled = true;
            txtCowCLR.Enabled = true;
            rfvCowFat.Enabled = true;
            rfvCowCLR.Enabled = true;
            lblCowMilkQuantity.Visible = false;
            SetFocus(txtCowFat);
        }
        else
        {
            txtCowFat.Text = "";
            txtCowCLR.Text = "";
           // txtCowMilkQuantity.Text = "";
            txtCowFat.Enabled = false;
            txtCowCLR.Enabled = false;
            rfvCowFat.Enabled = false;
            rfvCowCLR.Enabled = false;
            SetFocus(txtCowMilkQuantity);
            lblCowMilkQuantity.Visible = true;
        }
    }
    protected void txtSociety_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
            TextBox txtBufMilkQuantity = (TextBox)row.FindControl("txtBufMilkQuantity");
            TextBox txtCowMilkQuantity = (TextBox)row.FindControl("txtCowMilkQuantity");
            TextBox txtCowFat = (TextBox)row.FindControl("txtCowFat");
            TextBox txtCowCLR = (TextBox)row.FindControl("txtCowCLR");
            TextBox txtSociety = (TextBox)row.FindControl("txtSociety");
            TextBox txtSocietyName = (TextBox)row.FindControl("txtSocietyName");
            HiddenField hfOffice_ID = (HiddenField)row.FindControl("hfOffice_ID");
            TextBox txtBufFat = (TextBox)row.FindControl("txtBufFat");
            TextBox txtBufCLR = (TextBox)row.FindControl("txtBufCLR");
           Label lblError = (Label)row.FindControl("lblError");
            RequiredFieldValidator rfvSociety = (RequiredFieldValidator)row.FindControl("rfvSociety");
            
            //RequiredFieldValidator rfvSocietyName = (RequiredFieldValidator)row.FindControl("rfvSocietyName");
            RequiredFieldValidator rfvBufFat = (RequiredFieldValidator)row.FindControl("rfvBufFat");
            RequiredFieldValidator rfvBufCLR = (RequiredFieldValidator)row.FindControl("rfvBufCLR");
            RequiredFieldValidator rfvCowFat = (RequiredFieldValidator)row.FindControl("rfvCowFat");
            RequiredFieldValidator rfvCowCLR = (RequiredFieldValidator)row.FindControl("rfvCowCLR");
            RequiredFieldValidator rfvBufMilkQuantity = (RequiredFieldValidator)row.FindControl("rfvBufMilkQuantity");
            RequiredFieldValidator rfvCowMilkQuantity = (RequiredFieldValidator)row.FindControl("rfvCowMilkQuantity");
            if (txtSociety.Text != "")
            {
                 DataSet ds = (DataSet)Session["ds2"];
                DataTable dt = ds.Tables[0];
                int status = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    if (dr["Office_Code"].ToString() == txtSociety.Text.Trim())
                    {
                        txtSocietyName.Text = dr["Office_Name"].ToString();
                        hfOffice_ID.Value = dr["Office_ID"].ToString();
                        txtBufMilkQuantity.Enabled = true;
                        txtCowMilkQuantity.Enabled = true;
                        //rfvSocietyName.Enabled = false;
                        lblError.Text = "";
                        SetFocus(txtBufMilkQuantity);
                        status = 1;
                        break;
                    }
                }
                if (status == 0)
                {
                    txtSocietyName.Text = "";
                    txtSociety.Text = "";
                    txtBufMilkQuantity.Enabled = false;
                    txtCowMilkQuantity.Enabled = false;
                    SetFocus(txtSociety);
                    //rfvSocietyName.Enabled = true;
                    lblError.Text = " कृपया मान्य सोसायटी कोड दर्ज करें।";
                   
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('कृपया गाय या भैंस की मात्रा दर्ज करें')");
                }
            }
            else
            {
               // lblError.Text = "";
                txtBufMilkQuantity.Text = "";
                txtSociety.Text = "";
                txtSocietyName.Text = "";
                txtBufMilkQuantity.Enabled = false;
                txtCowMilkQuantity.Enabled = false;
                txtCowFat.Enabled = false;
                txtCowCLR.Enabled = false;
                txtBufFat.Enabled = false;
                txtBufCLR.Enabled = false;
                txtBufMilkQuantity.Text = "";
                txtCowMilkQuantity.Text = "";
                txtCowFat.Text = "";
                txtCowCLR.Text = "";
                txtBufFat.Text = "";
                txtBufCLR.Text = "";
                rfvBufMilkQuantity.Enabled = false;
                rfvCowMilkQuantity.Enabled = false;
                rfvCowFat.Enabled = false;
                rfvBufFat.Enabled = false;
                rfvBufCLR.Enabled = false;
                rfvCowCLR.Enabled = false;
                SetFocus(txtSociety);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gvBMCMilkDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblErrMsg.Text = "";
        if (e.CommandName == "SaveRecord")
        {
            ViewState["InFlowrowID"] = "";
            
           
              
                AddInFlowNewRowToGrid();
            
            //BindGrid();
            
        }
       
    }
    
    protected void txtFat_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox txtI_MilkSupplyQty = (TextBox)row.FindControl("txtMilkQuantity");
        TextBox txtFAT = (TextBox)row.FindControl("txtFat");
        TextBox txtCLR = (TextBox)row.FindControl("txtClr");
        TextBox txtSNF = (TextBox)row.FindControl("txtSnf");
        TextBox txtKgFat = (TextBox)row.FindControl("txtFatInKg");
        TextBox txtKgSNF = (TextBox)row.FindControl("txtSnfInKg");
        txtSNF.Text = GetSNF(txtFAT.Text, txtCLR.Text).ToString();
        txtKgSNF.Text = GetSNF_InKG(txtFAT.Text, txtCLR.Text, txtI_MilkSupplyQty.Text).ToString();
        txtKgFat.Text = GetFAT_InKG(txtFAT.Text, txtI_MilkSupplyQty.Text).ToString();

        decimal Fat = 0;
        decimal Clr = 0;
        decimal Snf = 0;
        decimal KgFat = 0;
        decimal KgSnf = 0;

        if (txtFAT.Text == "") { Fat = 0; } else { Fat = Convert.ToDecimal(txtFAT.Text); }
        if (txtCLR.Text == "") { Clr = 0; } else { Clr = Convert.ToDecimal(txtCLR.Text); }
        if (txtSNF.Text == "") { Snf = 0; } else { Snf = Convert.ToDecimal(txtSNF.Text); }
        if (txtKgFat.Text == "") { KgFat = 0; } else { KgFat = Convert.ToDecimal(txtKgFat.Text); }
        if (txtKgSNF.Text == "") { KgSnf = 0; } else { KgSnf = Convert.ToDecimal(txtKgSNF.Text); }
        SetFocus(txtFAT);
    }
   
    protected void txtClr_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox txtI_MilkSupplyQty = (TextBox)row.FindControl("txtMilkQuantity");
        TextBox txtFAT = (TextBox)row.FindControl("txtFat");
        TextBox txtCLR = (TextBox)row.FindControl("txtClr");
        TextBox txtSNF = (TextBox)row.FindControl("txtSnf");
        TextBox txtKgFat = (TextBox)row.FindControl("txtFatInKg");
        TextBox txtKgSNF = (TextBox)row.FindControl("txtSnfInKg");
        txtSNF.Text = GetSNF(txtFAT.Text, txtCLR.Text).ToString();
        txtKgSNF.Text = GetSNF_InKG(txtFAT.Text, txtCLR.Text, txtI_MilkSupplyQty.Text).ToString();
        txtKgFat.Text = GetFAT_InKG(txtFAT.Text, txtI_MilkSupplyQty.Text).ToString();

        decimal Fat = 0;
        decimal Clr = 0;
        decimal Snf = 0;
        decimal KgFat = 0;
        decimal KgSnf = 0;

        if (txtFAT.Text == "") { Fat = 0; } else { Fat = Convert.ToDecimal(txtFAT.Text); }
        if (txtCLR.Text == "") { Clr = 0; } else { Clr = Convert.ToDecimal(txtCLR.Text); }
        if (txtSNF.Text == "") { Snf = 0; } else { Snf = Convert.ToDecimal(txtSNF.Text); }
        if (txtKgFat.Text == "") { KgFat = 0; } else { KgFat = Convert.ToDecimal(txtKgFat.Text); }
        if (txtKgSNF.Text == "") { KgSnf = 0; } else { KgSnf = Convert.ToDecimal(txtKgSNF.Text); }
        SetFocus(txtCLR);
    }
    protected void txtMilkQuantity_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox txtI_MilkSupplyQty = (TextBox)row.FindControl("txtMilkQuantity");
        TextBox txtFAT = (TextBox)row.FindControl("txtFat");
        TextBox txtCLR = (TextBox)row.FindControl("txtClr");
        TextBox txtSNF = (TextBox)row.FindControl("txtSnf");
        TextBox txtKgFat = (TextBox)row.FindControl("txtFatInKg");
        TextBox txtKgSNF = (TextBox)row.FindControl("txtSnfInKg");
        txtSNF.Text = GetSNF(txtFAT.Text, txtCLR.Text).ToString();
        txtKgSNF.Text = GetSNF_InKG(txtFAT.Text, txtCLR.Text, txtI_MilkSupplyQty.Text).ToString();
        txtKgFat.Text = GetFAT_InKG(txtFAT.Text, txtI_MilkSupplyQty.Text).ToString();

        decimal Fat = 0;
        decimal Clr = 0;
        decimal Snf = 0;
        decimal KgFat = 0;
        decimal KgSnf = 0;

        if (txtFAT.Text == "") { Fat = 0; } else { Fat = Convert.ToDecimal(txtFAT.Text); }
        if (txtCLR.Text == "") { Clr = 0; } else { Clr = Convert.ToDecimal(txtCLR.Text); }
        if (txtSNF.Text == "") { Snf = 0; } else { Snf = Convert.ToDecimal(txtSNF.Text); }
        if (txtKgFat.Text == "") { KgFat = 0; } else { KgFat = Convert.ToDecimal(txtKgFat.Text); }
        if (txtKgSNF.Text == "") { KgSnf = 0; } else { KgSnf = Convert.ToDecimal(txtKgSNF.Text); }
        SetFocus(txtFAT);
    }

    protected void gvEntryList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblV_MilkType = (Label)e.Row.FindControl("lblMilkType");
           
            Label lblMilkQuality = (Label)e.Row.FindControl("lblMilkQuality");
            DropDownList ddlMilkQuality = (DropDownList)e.Row.FindControl("ddlMilkQuality");
            RangeValidator rvfFat = (RangeValidator)e.Row.FindControl("rvfFat");


            if (lblV_MilkType.Text == "Buf")
            {
                rvfFat.MinimumValue = "5.6";
                rvfFat.MaximumValue = "12";
                rvfFat.ErrorMessage = "(F.A.T %) 5.6 से 12 के बीच होना चाहिए।";
                rvfFat.Text = "<i class='fa fa-exclamation-circle' title='(F.A.T %) 5.6 से 12 के बीच होना चाहिए।'></i>";
            }
            else
            {
                rvfFat.MinimumValue = "3.2";
                rvfFat.MaximumValue = "5.5";
                rvfFat.ErrorMessage = "(F.A.T %) 3.2 से 5.5 के बीच होना चाहिए।";
                rvfFat.Text = "<i class='fa fa-exclamation-circle' title='(F.A.T %) 3.2 से 5.5 के बीच होना चाहिए।'></i>";
            }
           
            ddlMilkQuality.Items.FindByText(lblMilkQuality.Text).Selected = true;


        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            FillGrid();
        }
        
    }
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (gvEntryList.Rows.Count > 0)
            {
                gvEntryList.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvEntryList.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
	public override void VerifyRenderingInServerForm(Control control)
    {
        
    }
	protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            GenerateData();
            CreateDBFFile();
            //lblMsg.Text = "";
            //gvDbf.Visible = true;
            //Response.Clear();
            //String sDate = Convert.ToDateTime(txtDate.Text, cult).ToString("MM/dd/yyyy HH:mm");
            //DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
            //String dy = datevalue.Day.ToString();
            //String mn = datevalue.Month.ToString();
            //String yy = datevalue.Year.ToString();
            //Response.AddHeader("content-disposition", "attachment;filename=" + "MB" + mn + dy + ".dbf");
            ////Response.AddHeader("content-disposition", "attachment;filename=" + "TruckSheet"  + ".dbf");
            //Response.Charset = "";
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "application/vnd.dbf";
            //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            //gvDbf.RenderControl(htmlWrite);

            //Response.Write(stringWrite.ToString());
            //gvDbf.Visible = false;
            //Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private DataTable GenerateData()
    {
        DataTable dt = new DataTable();

        DataRow dr = null;

        dt.Columns.Add(new DataColumn("T_UN_CD", typeof(string)));

        dt.Columns.Add(new DataColumn("T_SOC_CD", typeof(string)));

        dt.Columns.Add(new DataColumn("T_DATE", typeof(string)));

        dt.Columns.Add(new DataColumn("T_SHIFT", typeof(string)));

        dt.Columns.Add(new DataColumn("T_BFCW_IND", typeof(string)));

        dt.Columns.Add(new DataColumn("T_CATG", typeof(string)));

        dt.Columns.Add(new DataColumn("T_QTY", typeof(string)));

        dt.Columns.Add(new DataColumn("T_FAT", typeof(string)));

        dt.Columns.Add(new DataColumn("T_SNF", typeof(string)));

        dt.Columns.Add(new DataColumn("T_CLR", typeof(string)));

       ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntryViaCanes", new string[] { "flag", "Created_Office_ID", "EntryDate", "CC_Id" }, new string[] { "7", objdb.Office_ID(), Convert.ToDateTime(txtfilterdate.Text, cult).ToString("yyyy/MM/dd"),ddlCCflt.SelectedValue }, "dataset");
        if(ds != null && ds.Tables.Count > 0)
        {
            int Count = ds.Tables[0].Rows.Count;
            for (int i = 0; i < Count; i++)
			{
			    dr = dt.NewRow();

                dr["T_UN_CD"] = ds.Tables[0].Rows[i]["T_UN_CD"].ToString();

                dr["T_SOC_CD"] =  ds.Tables[0].Rows[i]["T_SOC_CD"].ToString();

                dr["T_DATE"] = ds.Tables[0].Rows[i]["D_Date"].ToString();

                dr["T_SHIFT"] = ds.Tables[0].Rows[i]["T_SHIFT"].ToString();

                dr["T_BFCW_IND"] = ds.Tables[0].Rows[i]["T_BFCW_IND"].ToString();

                dr["T_CATG"] = ds.Tables[0].Rows[i]["T_CATG"].ToString();

                dr["T_QTY"] = ds.Tables[0].Rows[i]["MilkQuantity"].ToString();

                dr["T_FAT"] = ds.Tables[0].Rows[i]["Fat"].ToString();

                dr["T_SNF"] = ds.Tables[0].Rows[i]["Snf"].ToString();

                dr["T_CLR"] = ds.Tables[0].Rows[i]["Clr"].ToString();

                dt.Rows.Add(dr);
			}
        }

        

        return dt;
    }
    private void CreateDBFFile()
        {
            string filepath = null;

            filepath = Server.MapPath("~//Download//");

            string TableName = "T" + DateTime.Now.ToLongTimeString().Replace(":", "").Replace("AM", "").Replace("PM", "");
           
            using(dBaseConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; " + " Data Source=" + filepath + "; " + "Extended Properties=dBase IV"))
            {
                dBaseConnection.Open();

                OleDbCommand olecommand = dBaseConnection.CreateCommand();

                if ((System.IO.File.Exists(filepath + "" + TableName + ".dbf")))
                {
                    System.IO.File.Delete(filepath + "" + TableName + ".dbf");
                    olecommand.CommandText = "CREATE TABLE [" + TableName + "] ([T_UN_CD]  varchar(10), [T_SOC_CD]  varchar(10),  [T_DATE] varchar(10),[T_SHIFT] varchar(10),[T_BFCW_IND] varchar(10),[T_CATG] varchar(10),[T_QTY] varchar(10),[T_FAT] varchar(10),[T_SNF] varchar(10),[T_CLR] varchar(10))";
                    olecommand.ExecuteNonQuery();
                }
                else
                {
                    olecommand.CommandText = "CREATE TABLE [" + TableName + "] ([T_UN_CD]  varchar(10), [T_SOC_CD]  varchar(10),  [T_DATE] varchar(10),[T_SHIFT] varchar(10),[T_BFCW_IND] varchar(10),[T_CATG] varchar(10),[T_QTY] varchar(10),[T_FAT] varchar(10),[T_SNF] varchar(10),[T_CLR] varchar(10))";
                    olecommand.ExecuteNonQuery();
                }

                OleDbDataAdapter oleadapter = new OleDbDataAdapter(olecommand);
                OleDbCommand oleinsertCommand = dBaseConnection.CreateCommand();

                foreach (DataRow dr in GenerateData().Rows)
                {
                    string Column1 = dr["T_UN_CD"].ToString();
                    string Column2 = dr["T_SOC_CD"].ToString();
                    string Column3 = dr["T_DATE"].ToString();
                    string Column4 = dr["T_SHIFT"].ToString();
                    string Column5 = dr["T_BFCW_IND"].ToString();
                    string Column6 = dr["T_CATG"].ToString();
                    string Column7 = dr["T_QTY"].ToString();
                    string Column8 = dr["T_FAT"].ToString();
                    string Column9 = dr["T_SNF"].ToString();
                    string Column10 = dr["T_CLR"].ToString();
                    

                    oleinsertCommand.CommandText = "INSERT INTO [" + TableName + "] ([T_UN_CD], [T_SOC_CD],[T_DATE],[T_SHIFT],[T_BFCW_IND], [T_CATG],[T_QTY],[T_FAT],[T_SNF],[T_CLR]) VALUES ('" + Column1 + "','" + Column2 + "','" + Column3 + "','" + Column4 + "','" + Column5 + "','" + Column6 + "','" + Column7 + "','" + Column8 + "','" + Column9 + "','" + Column10 + "')";

                    oleinsertCommand.ExecuteNonQuery();
                }
            }
            String sDate = Convert.ToDateTime(txtfilterdate.Text, cult).ToString("MM/dd/yyyy HH:mm");
            DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
           string  dy = datevalue.Day.ToString();
            if (dy.Length > 1)
            {

            }
            else
            {
                dy = "0" + dy;
            }
            //String mn = datevalue.Month.ToString();
            string mn = datevalue.ToString("MMMM");
            string yy = datevalue.Year.ToString();
            
            FileStream sourceFile = new FileStream(filepath + "" + TableName + ".dbf", FileMode.Open);
            float FileSize = 0;
            FileSize = sourceFile.Length;
            byte[] getContent = new byte[Convert.ToInt32(Math.Truncate(FileSize))];
            sourceFile.Read(getContent, 0, Convert.ToInt32(sourceFile.Length));
            sourceFile.Close();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.ContentType = "application/dbf";
            Response.AddHeader("Content-Length", getContent.Length.ToString());
            //Response.AddHeader("content-disposition", "attachment;filename=" + "MB"  + dy + mn + yy +  ".dbf");
            Response.AddHeader("content-disposition", "attachment;filename=" + "MB" + mn.Substring(0,3) + dy + yy + ".dbf");
            Response.BinaryWrite(getContent);
            Response.Flush();
            System.IO.File.Delete(filepath + "" + TableName + ".dbf");
            Response.End();
        }
    
    public  System.Data.OleDb.OleDbConnection dBaseConnection { get; set; }


}
