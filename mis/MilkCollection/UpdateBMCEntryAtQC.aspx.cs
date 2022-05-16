using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web;
public partial class mis_MilkCollection_UpdateBMCEntryAtQC : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    MilkCalculationClass Obj_MC = new MilkCalculationClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            

            if (!IsPostBack)
            {
                lblMsg.Text = "";

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                //txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //txtDate.Attributes.Add("readonly", "readonly");
                //txtDate_TextChanged(sender, e);
                FillReferenceNo();
                txtArrivalDate.Text = System.DateTime.Now.ToString();
                ddlOption.DataSource = objdb.ByProcedure("USP_Mst_MilkQualityList",
                      new string[] { "flag" },
                      new string[] { "1" }, "dataset");
                ddlOption.DataValueField = "V_MilkQualityList";
                ddlOption.DataTextField = "V_MilkQualityList";
                ddlOption.DataBind();
                ddlOption.Items.Insert(0, new ListItem("Select", "0"));
                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Updated Successfully');", true);
                    }
                }

            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        try
        {
            //if (Session["event_control"] != null)
            //{
            //    TextBox control = (TextBox)Session["event_control"];
            //    control.Focus();
            //}
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    private void FillGrid()
    {
        try
        {
            btnAdd.Visible = false;
            divdetail.Visible = false;
            gvBMCDetails.DataSource = string.Empty;
            gvBMCDetails.DataBind();
            gvTankerSealDetails.DataSource = string.Empty;
            gvTankerSealDetails.DataBind();
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New",
                                new string[] { "flag", "BI_MilkInOutRefID" },
                                new string[] { "7", ddlReferenceNo.SelectedValue, }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvBMCDetails.DataSource = ds.Tables[0];
                    gvBMCDetails.DataBind();

                    gvTankerSealDetails.DataSource = ds.Tables[1];
                    gvTankerSealDetails.DataBind();

                    if (ds.Tables[2].Rows[0]["TankerType"].ToString() == "S")
                    {
                        ddlCompartmentType.Items.Clear();
                        ddlCompartmentType.Items.Add(new ListItem("Single", "S"));
                        ddlCompartmentType.Enabled = false;
                        rfvddlCompartmentType.ValidationGroup = "a";
                        rfvddlOption.ValidationGroup = "a";
                        rfvddlCOB.ValidationGroup = "a";
                        rfvAcidity.ValidationGroup = "a";
                      

                    }
                    else
                    {
                        ddlCompartmentType.Items.Clear();
                        ddlCompartmentType.Items.Insert(0, new ListItem("Select", "0"));
                        ddlCompartmentType.Items.Add(new ListItem("Front", "F"));
                        ddlCompartmentType.Items.Add(new ListItem("Rear", "R"));
                        btnAdd.Visible = true;
                        ddlCompartmentType.Enabled = true;
                        rfvddlOption.ValidationGroup = "Add";
                        rfvddlCOB.ValidationGroup = "Add";
                        rfvAcidity.ValidationGroup = "Add";
                        rfvddlCompartmentType.ValidationGroup = "Add";
                    }
                    divdetail.Visible = true;

                    GetCalculation();
                }
                else
                {
                    gvBMCDetails.DataSource = string.Empty;
                    gvBMCDetails.DataBind();
                    gvTankerSealDetails.DataSource = string.Empty;
                    gvTankerSealDetails.DataBind();
                }

                //if(ds.Tables[1].Rows.Count > 0)
                //{

                //    txtAcidity.Text = ds.Tables[1].Rows[0]["Acidity"].ToString();
                //    ddlOption.ClearSelection();
                //    ddlOption.Items.FindByText(ds.Tables[1].Rows[0]["OralTest"].ToString()).Selected = true;
                //    ddlCOB.ClearSelection();
                //    ddlCOB.Items.FindByValue(ds.Tables[1].Rows[0]["COB"].ToString()).Selected = true;
                //    ddlUrea.ClearSelection();
                //    ddlUrea.Items.FindByValue(ds.Tables[1].Rows[0]["Urea"].ToString()).Selected = true;
                //    ddlNeutralizer.ClearSelection();
                //    ddlNeutralizer.Items.FindByValue(ds.Tables[1].Rows[0]["Neutralizer"].ToString()).Selected = true;
                //    ddlMaltodextrin.ClearSelection();
                //    ddlMaltodextrin.Items.FindByValue(ds.Tables[1].Rows[0]["Maltodextrin"].ToString()).Selected = true;
                //    ddlGlucose.ClearSelection();
                //    ddlGlucose.Items.FindByValue(ds.Tables[1].Rows[0]["Glucose"].ToString()).Selected = true;
                //    ddlSucrose.ClearSelection();
                //    ddlSucrose.Items.FindByValue(ds.Tables[1].Rows[0]["Sucrose"].ToString()).Selected = true;
                //    ddlSalt.ClearSelection();
                //    ddlSalt.Items.FindByValue(ds.Tables[1].Rows[0]["Salt"].ToString()).Selected = true;
                //    ddlStarch.ClearSelection();
                //    ddlStarch.Items.FindByValue(ds.Tables[1].Rows[0]["Starch"].ToString()).Selected = true;
                //    ddlDetergent.ClearSelection();
                //    ddlDetergent.Items.FindByValue(ds.Tables[1].Rows[0]["Detergent"].ToString()).Selected = true;
                //    ddlNitrateTest.ClearSelection();
                //    ddlNitrateTest.Items.FindByValue(ds.Tables[1].Rows[0]["NitrateTest"].ToString()).Selected = true;

                //}
            }
            else
            {
                gvBMCDetails.DataSource = string.Empty;
                gvBMCDetails.DataBind();
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5:" + ex.Message.ToString());
        }
    }
    protected void gvBMCDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Label lblOffice_Name_E = (Label)e.Row.FindControl("lblOffice_Name_E");
            Label lblType = (Label)e.Row.FindControl("lblType");
            Label lblV_Temp = (Label)e.Row.FindControl("lblV_Temp");
            TextBox txtV_Temp = (TextBox)e.Row.FindControl("txtV_Temp");
            Label lblD_MilkQuantity = (Label)e.Row.FindControl("lblD_MilkQuantity");
            TextBox txtD_MilkQuantity = (TextBox)e.Row.FindControl("txtD_MilkQuantity");
            Label lblFAT = (Label)e.Row.FindControl("lblFAT");
            TextBox txtFAT = (TextBox)e.Row.FindControl("txtFAT");
            Label lblCLR = (Label)e.Row.FindControl("lblCLR");
            TextBox txtCLR = (TextBox)e.Row.FindControl("txtCLR");
            Label lblSNF = (Label)e.Row.FindControl("lblSNF");
            TextBox txtSNF = (TextBox)e.Row.FindControl("txtSNF");
            Label lblKgFat = (Label)e.Row.FindControl("lblKgFat");
            TextBox txtKgFat = (TextBox)e.Row.FindControl("txtKgFat");
            Label lblKgSNF = (Label)e.Row.FindControl("lblKgSNF");
            TextBox txtKgSNF = (TextBox)e.Row.FindControl("txtKgSNF");



            if (lblType.Text == "Single" || lblType.Text == "Front" || lblType.Text == "Rear")
            {


                lblOffice_Name_E.Font.Bold = true;
                lblOffice_Name_E.Visible = true;

                lblV_Temp.Visible = false;
                txtV_Temp.Visible = true;
                lblD_MilkQuantity.Visible = false;
                txtD_MilkQuantity.Visible = true;
                lblFAT.Visible = false;
                txtFAT.Visible = true;
                lblCLR.Visible = false;
                txtCLR.Visible = true;
                lblSNF.Visible = false;
                txtSNF.Visible = true;
                lblKgFat.Visible = false;
                txtKgFat.Visible = true;
                lblKgSNF.Visible = false;
                txtKgSNF.Visible = true;


            }

            else
            {

                lblOffice_Name_E.Visible = true;

                lblOffice_Name_E.Font.Bold = true;
                lblV_Temp.Visible = true;
                txtV_Temp.Visible = false;
                lblD_MilkQuantity.Visible = true;
                txtD_MilkQuantity.Visible = false;
                lblFAT.Visible = true;
                txtFAT.Visible = false;
                lblCLR.Visible = true;
                txtCLR.Visible = false;
                lblSNF.Visible = true;
                txtSNF.Visible = false;
                lblKgFat.Visible = true;
                txtKgFat.Visible = false;
                lblKgSNF.Visible = true;
                txtKgSNF.Visible = false;


            }
            //If Salary is less than 10000 than set the row Background Color to Cyan  

        }
    }
    protected void txtD_MilkQuantity_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox txtD_MilkQuantity = (TextBox)row.FindControl("txtD_MilkQuantity");
        TextBox txtFAT = (TextBox)row.FindControl("txtFAT");
        TextBox txtCLR = (TextBox)row.FindControl("txtCLR");
        TextBox txtSNF = (TextBox)row.FindControl("txtSNF");
        TextBox txtKgFat = (TextBox)row.FindControl("txtKgFat");
        TextBox txtKgSNF = (TextBox)row.FindControl("txtKgSNF");
        txtSNF.Text = GetSNF(txtFAT.Text, txtCLR.Text).ToString();
        txtKgSNF.Text = GetSNF_InKG(txtFAT.Text, txtCLR.Text, txtD_MilkQuantity.Text).ToString();
        txtKgFat.Text = GetFAT_InKG(txtFAT.Text, txtD_MilkQuantity.Text).ToString();

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
        GetCalculation();
        SetFocus(txtFAT);
        txtFAT.Text = "";
    }
    protected void txtFAT_TextChanged(object sender, EventArgs e)
    {

        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox txtD_MilkQuantity = (TextBox)row.FindControl("txtD_MilkQuantity");
        TextBox txtFAT = (TextBox)row.FindControl("txtFAT");
        TextBox txtCLR = (TextBox)row.FindControl("txtCLR");
        TextBox txtSNF = (TextBox)row.FindControl("txtSNF");
        TextBox txtKgFat = (TextBox)row.FindControl("txtKgFat");
        TextBox txtKgSNF = (TextBox)row.FindControl("txtKgSNF");
        txtSNF.Text = GetSNF(txtFAT.Text, txtCLR.Text).ToString();
        txtKgSNF.Text = GetSNF_InKG(txtFAT.Text, txtCLR.Text, txtD_MilkQuantity.Text).ToString();
        txtKgFat.Text = GetFAT_InKG(txtFAT.Text, txtD_MilkQuantity.Text).ToString();

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
        GetCalculation();
        txtCLR.Focus();
        txtCLR.Text = "";


    }
    protected void txtCLR_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox txtD_MilkQuantity = (TextBox)row.FindControl("txtD_MilkQuantity");
        TextBox txtFAT = (TextBox)row.FindControl("txtFAT");
        TextBox txtCLR = (TextBox)row.FindControl("txtCLR");
        TextBox txtSNF = (TextBox)row.FindControl("txtSNF");
        TextBox txtKgFat = (TextBox)row.FindControl("txtKgFat");
        TextBox txtKgSNF = (TextBox)row.FindControl("txtKgSNF");
        txtSNF.Text = GetSNF(txtFAT.Text, txtCLR.Text).ToString();
        txtKgSNF.Text = GetSNF_InKG(txtFAT.Text, txtCLR.Text, txtD_MilkQuantity.Text).ToString();
        txtKgFat.Text = GetFAT_InKG(txtFAT.Text, txtD_MilkQuantity.Text).ToString();
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
        GetCalculation();
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
            snf = Obj_MC.GetSNFPer(fat, clr);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
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

            snf_Per = Obj_MC.GetSNFPer(fat, clr);

            SNF_InKG = Obj_MC.GetSNFInKg(MilkQty, snf_Per);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }

        return Math.Round(FAT_InKG, 3);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            decimal MilkQuantity = 0;
            DataTable dt = new DataTable();
            dt = GetSealGridvalue();

            DataTable dt1 = new DataTable();
            dt1 = GetQualityDetails();

           if(ddlCompartmentType.SelectedValue == "S")
           {

           }
           else
           {
               if (dt1.Rows.Count == 2)
               {
                   
               }
               else
               {
                   lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Thank You!", "Opps! :" + "Both Front and Rear Chamber Detail is required.");
                   return;
               }
           }
            if (dt.Rows.Count == 0)
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Thank You!", "Opps! :" + "Minimum 1 seal is required.");
            }
            else
            {
                if ((dt.Select("ChamberType = 'Chamber'").Length > 0 && dt.Select("ChamberType = 'ValveBox'").Length > 0))
                {
                    string Uploadedfilename = "";

                    if (FuSealVerificationfile.HasFile)
                    {
                        decimal size = ((decimal)FuSealVerificationfile.PostedFile.ContentLength / (decimal)100);
                        string Fileext = Path.GetExtension(FuSealVerificationfile.FileName).ToLower();
                        if (Fileext == ".png" || Fileext == ".jpg" || Fileext == ".jpeg" || Fileext == ".doc" || Fileext == ".docx" || Fileext == ".pdf" || Fileext == ".zip" || Fileext == ".rar")
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


                    ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New",
                                 new string[] { "flag" 
                                      ,"D_Date"    
                                      ,"BI_MilkInOutRefID"
                                      ,"I_OfficeID"
                                      ,"I_OfficeTypeID"                                    
                                      ,"SealVerificationDocument"
                                                                  
                                   },
                                new string[] {"8"
                                    ,txtArrivalDate.Text
                                    // ,Convert.ToDateTime(txtArrivalDate.Text,cult).ToString("yyyy/MM/dd hh:mm:ss")
                                     ,ddlReferenceNo.SelectedValue
                                     ,objdb.Office_ID()
                                     ,objdb.OfficeType_ID()                                    
                                     ,Uploadedfilename
                        
                        }, new string[] { "type_Trn_TankerSealDetails_MCU", "type_Trn_MilkInwardOutwardAtQC_MCU" },
                              new DataTable[] { dt, dt1 },

                                "TableSave");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                Session["IsSuccess"] = true;
                                Response.Redirect("UpdateBMCEntryAtQC.aspx", false);

                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                Session["IsSuccess"] = false;

                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1:" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                Session["IsSuccess"] = false;
                            }
                        }
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", "Minimum chamber seal required 1 and maximum 10, Valve seal required minimum 1 and maximum 2");
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
    }

    //private DataTable GetBMCDetails()
    //{
    //    decimal MilkQuantity = 0;
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add("BMCEntryAtQCChild_Id", typeof(int));
    //    dt.Columns.Add("Quantity", typeof(decimal));
    //    dt.Columns.Add("Temp", typeof(string));
    //    dt.Columns.Add("FAT", typeof(decimal));
    //    dt.Columns.Add("SNF", typeof(decimal));
    //    dt.Columns.Add("CLR", typeof(decimal));
    //    dt.Columns.Add("FatKg", typeof(decimal));
    //    dt.Columns.Add("SnfKg", typeof(decimal));

    //    foreach(GridViewRow Row in gvBMCDetails.Rows)
    //    {
    //        Label lblID = (Label)Row.FindControl("lblID");
    //        Label lblType = (Label)Row.FindControl("lblType");
    //        TextBox txtV_Temp = (TextBox)Row.FindControl("txtV_Temp");           
    //        TextBox txtD_MilkQuantity = (TextBox)Row.FindControl("txtD_MilkQuantity");          
    //        TextBox txtFAT = (TextBox)Row.FindControl("txtFAT");          
    //        TextBox txtCLR = (TextBox)Row.FindControl("txtCLR");           
    //        TextBox txtSNF = (TextBox)Row.FindControl("txtSNF");           
    //        TextBox txtKgFat = (TextBox)Row.FindControl("txtKgFat");           
    //        TextBox txtKgSNF = (TextBox)Row.FindControl("txtKgSNF");
    //        if (txtD_MilkQuantity.Text == "")
    //        {
    //            MilkQuantity = 0;
    //        }
    //        else
    //        {
    //            MilkQuantity = decimal.Parse(txtD_MilkQuantity.Text);
    //        }
    //        if (lblType.Text == "ChildEntry")
    //        {
    //            dt.Rows.Add(lblID.Text, MilkQuantity, txtV_Temp.Text, txtFAT.Text, txtSNF.Text, txtCLR.Text, txtKgFat.Text, txtKgSNF.Text);
    //        }



    //    }
    //    return dt;
    //}

    protected void GetCalculation()
    {
        decimal RcvdTotalQty = 0;
        //decimal RcvdTotalFat = 0;
        //decimal RcvdTotalSnf = 0;
        //decimal RcvdTotalClr = 0;
        decimal RcvdTotalFatKg = 0;
        decimal RcvdTotalSnfKg = 0;
        decimal ViaTankerTotalQty = 0;
        //decimal ViaTankerTotalFat = 0;
        //decimal ViaTankerTotalSnf = 0;
        //decimal ViaTankerTotalClr = 0;
        decimal ViaTankerTotalFatKg = 0;
        decimal ViaTankerTotalSnfKg = 0;

        foreach (GridViewRow gvrow in gvBMCDetails.Rows)
        {

            Label lblType = (Label)gvrow.FindControl("lblType");
            Label lblD_MilkQuantity = (Label)gvrow.FindControl("lblD_MilkQuantity");
            TextBox MilkQuantity = (TextBox)gvrow.FindControl("txtD_MilkQuantity");
            Label lblFAT = (Label)gvrow.FindControl("lblFAT");
            TextBox MilkFat = (TextBox)gvrow.FindControl("txtFAT");
            Label lblCLR = (Label)gvrow.FindControl("lblCLR");
            TextBox MilkClr = (TextBox)gvrow.FindControl("txtCLR");
            Label lblSNF = (Label)gvrow.FindControl("lblSNF");
            TextBox MilkSnf = (TextBox)gvrow.FindControl("txtSNF");
            Label lblKgFat = (Label)gvrow.FindControl("lblKgFat");
            TextBox MilkFatKg = (TextBox)gvrow.FindControl("txtKgFat");
            Label lblKgSNF = (Label)gvrow.FindControl("lblKgSNF");
            TextBox MilkSnfKg = (TextBox)gvrow.FindControl("txtKgSNF");
            if (lblType.Text == "Via Tanker")
            {
                lblD_MilkQuantity.Text = RcvdTotalQty.ToString();

                //if (RcvdTotalFat != 0)
                //{
                //    lblFAT.Text = Math.Round((RcvdTotalFat / RowcountFat), 0).ToString();
                //    RcvdTotalFat = decimal.Parse(lblFAT.Text);
                //}
                //if (RcvdTotalClr != 0)
                //{
                //    lblCLR.Text = Math.Round((RcvdTotalClr / RowcounClr), 0).ToString();
                //    RcvdTotalClr = decimal.Parse(lblCLR.Text);
                //}

                //lblSNF.Text = GetSNF(lblFAT.Text, lblCLR.Text).ToString();   
                //RcvdTotalSnf = decimal.Parse(lblSNF.Text);
                lblKgFat.Text = RcvdTotalFatKg.ToString();
                lblKgSNF.Text = RcvdTotalSnfKg.ToString();

            }
            else if (lblType.Text == "Single" || lblType.Text == "Front" || lblType.Text == "Rear")
            {
                if (MilkQuantity.Text != "")
                {
                    ViaTankerTotalQty += decimal.Parse(MilkQuantity.Text);
                }
                //if (MilkFat.Text != "")
                //{
                //    ViaTankerTotalFat += decimal.Parse(MilkFat.Text);
                //}
                //if (MilkClr.Text != "")
                //{
                //    ViaTankerTotalClr += decimal.Parse(MilkClr.Text);
                //}
                //if (MilkSnf.Text != "")
                //{
                //    ViaTankerTotalSnf += decimal.Parse(MilkSnf.Text);
                //}
                if (MilkFatKg.Text != "")
                {
                    ViaTankerTotalFatKg += decimal.Parse(MilkFatKg.Text);
                }
                if (MilkSnfKg.Text != "")
                {
                    ViaTankerTotalSnfKg += decimal.Parse(MilkSnfKg.Text);
                }



            }
            else if (lblType.Text == "Difference")
            {
                lblD_MilkQuantity.Text = (ViaTankerTotalQty - RcvdTotalQty).ToString();
                //lblFAT.Text = (ViaTankerTotalFat - RcvdTotalFat).ToString();
                //lblCLR.Text = (ViaTankerTotalClr - RcvdTotalClr).ToString();
                //lblSNF.Text = (ViaTankerTotalSnf - RcvdTotalSnf).ToString();
                lblKgFat.Text = (ViaTankerTotalFatKg - RcvdTotalFatKg).ToString();
                lblKgSNF.Text = (ViaTankerTotalSnfKg - RcvdTotalSnfKg).ToString();
            }
            else
            {


                if (MilkQuantity.Text != "")
                {
                    RcvdTotalQty += decimal.Parse(MilkQuantity.Text);
                }
                //if (MilkFat.Text != "")
                //{
                //    RowcountFat += 1;
                //    RcvdTotalFat += decimal.Parse(MilkFat.Text);

                //}
                //if (MilkClr.Text != "")
                //{
                //    RowcounClr += 1;                  
                //    RcvdTotalClr += decimal.Parse(MilkClr.Text);

                //}
                //if (MilkSnf.Text != "")
                //{
                //    //RcvdTotalSnf += decimal.Parse(MilkSnf.Text);

                //}
                if (MilkFatKg.Text != "")
                {
                    RcvdTotalFatKg += decimal.Parse(MilkFatKg.Text);
                }
                if (MilkSnfKg.Text != "")
                {
                    RcvdTotalSnfKg += decimal.Parse(MilkSnfKg.Text);
                }

            }
        }

    }
    protected void FillReferenceNo()
    {

        ds = null;
        ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New",
                     new string[] { "flag", "I_OfficeID" },
                     new string[] { "9", objdb.Office_ID() }, "dataset");

        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlReferenceNo.DataTextField = "C_ReferenceNo";
                    ddlReferenceNo.DataValueField = "BI_MilkInOutRefID";
                    ddlReferenceNo.DataSource = ds;
                    ddlReferenceNo.DataBind();
                    ddlReferenceNo.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlReferenceNo.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlReferenceNo.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        else
        {
            ddlReferenceNo.Items.Insert(0, new ListItem("Select", "0"));
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
        btnSave.Enabled = true;
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

            DataTable dtdeletecc = ViewState["InsertRecord1"] as DataTable;

            if (dtdeletecc.Rows.Count == 0)
            {
                btnSave.Enabled = false;
            }


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
    protected void ddlReferenceNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("UpdateBMCEntryAtQC.aspx");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        int ChamberType = 0;
        if (ViewState["InsertRecord"] == "" || ViewState["InsertRecord"] == null)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ChamberType", typeof(string));
            dt.Columns.Add("OralTest", typeof(string));
            dt.Columns.Add("COB", typeof(string));
            dt.Columns.Add("Acidity", typeof(string));
            dt.Columns.Add("Urea", typeof(string));
            dt.Columns.Add("Neutralizer", typeof(string));
            dt.Columns.Add("Maltodextrin", typeof(string));
            dt.Columns.Add("Glucose", typeof(string));
            dt.Columns.Add("Sucrose", typeof(string));
            dt.Columns.Add("Salt", typeof(string));
            dt.Columns.Add("Starch", typeof(string));
            dt.Columns.Add("Detergent", typeof(string));
            dt.Columns.Add("NitrateTest", typeof(string));
            dt.Rows.Add(ddlCompartmentType.SelectedValue,
                                  ddlOption.SelectedValue,
                                  ddlCOB.SelectedValue,
                                  txtAcidity.Text,
                                  ddlUrea.SelectedValue,
                                  ddlNeutralizer.SelectedValue,
                                  ddlMaltodextrin.SelectedValue,
                                  ddlGlucose.SelectedValue,
                                  ddlSucrose.SelectedValue,
                                  ddlSalt.SelectedValue,
                                  ddlStarch.SelectedValue,
                                  ddlDetergent.SelectedValue,
                                  ddlNitrateTest.SelectedValue);
            ViewState["InsertRecord"] = dt;
            gvDetails.DataSource = dt;
            gvDetails.DataBind();

        }
        else
        {

            DataTable dt = (DataTable)ViewState["InsertRecord"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (ddlCompartmentType.SelectedValue == dt.Rows[i]["ChamberType"].ToString())
                {
                    ChamberType = 1;
                }
            }
            if (ChamberType == 1)
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Record of \"" + ddlCompartmentType.Text + "\" already exist.");
                
            }
            else
            {
                dt.Rows.Add(ddlCompartmentType.SelectedValue,
                                 ddlOption.SelectedValue,
                                 ddlCOB.SelectedValue,
                                 txtAcidity.Text,
                                 ddlUrea.SelectedValue,
                                 ddlNeutralizer.SelectedValue,
                                 ddlMaltodextrin.SelectedValue,
                                 ddlGlucose.SelectedValue,
                                 ddlSucrose.SelectedValue,
                                 ddlSalt.SelectedValue,
                                 ddlStarch.SelectedValue,
                                 ddlDetergent.SelectedValue,
                                 ddlNitrateTest.SelectedValue);
                ViewState["InsertRecord"] = dt;
                gvDetails.DataSource = dt;
                gvDetails.DataBind();
            }
           


        }
        ddlChamberType.ClearSelection();
        ddlCOB.ClearSelection();
        txtAcidity.Text = "";


    }
    private DataTable GetQualityDetails()
        {

        DataTable dtQD = new DataTable();
        DataRow drQD;

        dtQD.Columns.Add("ChamberType", typeof(string));
        dtQD.Columns.Add("Quantity", typeof(string));
        dtQD.Columns.Add("Temp", typeof(string));
        dtQD.Columns.Add("FAT", typeof(string));
        dtQD.Columns.Add("SNF", typeof(string));
        dtQD.Columns.Add("CLR", typeof(string));
        dtQD.Columns.Add("FatKg", typeof(string));
        dtQD.Columns.Add("SnfKg", typeof(string));
        dtQD.Columns.Add("OralTest", typeof(string));
        dtQD.Columns.Add("COB", typeof(string));
        dtQD.Columns.Add("Acidity", typeof(string));
        dtQD.Columns.Add("Urea", typeof(string));
        dtQD.Columns.Add("Neutralizer", typeof(string));
        dtQD.Columns.Add("Maltodextrin", typeof(string));
        dtQD.Columns.Add("Glucose", typeof(string));
        dtQD.Columns.Add("Sucrose", typeof(string));
        dtQD.Columns.Add("Salt", typeof(string));
        dtQD.Columns.Add("Starch", typeof(string));
        dtQD.Columns.Add("Detergent", typeof(string));
        dtQD.Columns.Add("NitrateTest", typeof(string));
        foreach (GridViewRow Row in gvBMCDetails.Rows)
        {
            TextBox txtV_Temp = (TextBox)Row.FindControl("txtV_Temp");
            TextBox txtD_MilkQuantity = (TextBox)Row.FindControl("txtD_MilkQuantity");
            TextBox txtFAT = (TextBox)Row.FindControl("txtFAT");
            TextBox txtCLR = (TextBox)Row.FindControl("txtCLR");
            TextBox txtSNF = (TextBox)Row.FindControl("txtSNF");
            TextBox txtKgFat = (TextBox)Row.FindControl("txtKgFat");
            TextBox txtKgSNF = (TextBox)Row.FindControl("txtKgSNF");
            Label lblType = (Label)Row.FindControl("lblType");
            if (lblType.Text == "Single")
            {
                dtQD.Rows.Add(ddlCompartmentType.SelectedValue,
                                 txtD_MilkQuantity.Text,
                                 txtV_Temp.Text,
                                 txtFAT.Text,
                                 txtSNF.Text,
                                 txtCLR.Text,
                                 txtKgFat.Text,
                                 txtKgSNF.Text,
                                 ddlOption.SelectedValue,
                                 ddlCOB.SelectedValue,
                                 txtAcidity.Text,
                                 ddlUrea.SelectedValue,
                                 ddlNeutralizer.SelectedValue,
                                 ddlMaltodextrin.SelectedValue,
                                 ddlGlucose.SelectedValue,
                                 ddlSucrose.SelectedValue,
                                 ddlSalt.SelectedValue,
                                 ddlStarch.SelectedValue,
                                 ddlDetergent.SelectedValue,
                                 ddlNitrateTest.SelectedValue);
            }
            if (lblType.Text == "Front")
            {
                foreach (GridViewRow row1 in gvDetails.Rows)
                {
                    Label lblChamberType = (Label)row1.FindControl("lblChamberType");
                    Label lblOralTest = (Label)row1.FindControl("lblOralTest");
                    Label lblCOB = (Label)row1.FindControl("lblCOB");
                    Label lblAcidity = (Label)row1.FindControl("lblAcidity");
                    Label lblUrea = (Label)row1.FindControl("lblUrea");
                    Label lblNeutralizer = (Label)row1.FindControl("lblNeutralizer");
                    Label lblMaltodextrin = (Label)row1.FindControl("lblMaltodextrin");
                    Label lblGlucose = (Label)row1.FindControl("lblGlucose");
                    Label lblSucrose = (Label)row1.FindControl("lblSucrose");
                    Label lblSalt = (Label)row1.FindControl("lblSalt");
                    Label lblStarch = (Label)row1.FindControl("lblStarch");
                    Label lblNitrateTest = (Label)row1.FindControl("lblNitrateTest");
                    Label lblDetergent = (Label)row1.FindControl("lblDetergent");
                    if (lblChamberType.Text == "F")
                    {
                        dtQD.Rows.Add(lblChamberType.Text,
                               txtD_MilkQuantity.Text,
                               txtV_Temp.Text,
                               txtFAT.Text,
                               txtSNF.Text,
                               txtCLR.Text,
                               txtKgFat.Text,
                               txtKgSNF.Text,
                               lblOralTest.Text,
                               lblCOB.Text,
                               lblAcidity.Text,
                               lblUrea.Text,
                               lblNeutralizer.Text,
                               lblMaltodextrin.Text,
                               lblGlucose.Text,
                               lblSucrose.Text,
                               lblSalt.Text,
                               lblStarch.Text,
                               lblDetergent.Text,
                               lblNitrateTest.Text);
                    }
                }


            }
            if (lblType.Text == "Rear")
            {

                foreach (GridViewRow row1 in gvDetails.Rows)
                {
                    Label lblChamberType = (Label)row1.FindControl("lblChamberType");
                    Label lblOralTest = (Label)row1.FindControl("lblOralTest");
                    Label lblCOB = (Label)row1.FindControl("lblCOB");
                    Label lblAcidity = (Label)row1.FindControl("lblAcidity");
                    Label lblUrea = (Label)row1.FindControl("lblUrea");
                    Label lblNeutralizer = (Label)row1.FindControl("lblNeutralizer");
                    Label lblMaltodextrin = (Label)row1.FindControl("lblMaltodextrin");
                    Label lblGlucose = (Label)row1.FindControl("lblGlucose");
                    Label lblSucrose = (Label)row1.FindControl("lblSucrose");
                    Label lblSalt = (Label)row1.FindControl("lblSalt");
                    Label lblStarch = (Label)row1.FindControl("lblStarch");
                    Label lblNitrateTest = (Label)row1.FindControl("lblNitrateTest");
                    Label lblDetergent = (Label)row1.FindControl("lblDetergent");
                    if (lblChamberType.Text == "R")
                    {
                        dtQD.Rows.Add(lblChamberType.Text,
                               txtD_MilkQuantity.Text,
                               txtV_Temp.Text,
                               txtFAT.Text,
                               txtSNF.Text,
                               txtCLR.Text,
                               txtKgFat.Text,
                               txtKgSNF.Text,
                               lblOralTest.Text,
                               lblCOB.Text,
                               lblAcidity.Text,
                               lblUrea.Text,
                               lblNeutralizer.Text,
                               lblMaltodextrin.Text,
                               lblGlucose.Text,
                               lblSucrose.Text,
                               lblSalt.Text,
                               lblStarch.Text,
                               lblDetergent.Text,
                               lblNitrateTest.Text);
                    }
                }
            }
        }
        return dtQD;
    }
    protected void lnkQDDelete_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt2 = ViewState["InsertRecord"] as DataTable;
            dt2.Rows.Remove(dt2.Rows[row.RowIndex]);
            ViewState["InsertRecord"] = dt2;
            gvDetails.DataSource = dt2;
            gvDetails.DataBind();

            

            DataTable dtdeletecc = ViewState["InsertRecord"] as DataTable;

            


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
}