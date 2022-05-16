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
public partial class mis_MilkCollection_BMCEntryAtQC : System.Web.UI.Page
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
                txtDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");               
                txtDate.Attributes.Add("readonly", "readonly");               
                FillBMCRoot();
                divdetail.Visible = false;
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
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
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
            if (Session["event_control"] != null)
            {
                TextBox control = (TextBox)Session["event_control"];
                control.Focus();
            }
        }
        catch (Exception ex)
        {
        }
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    private void FillGrid()
    {
        try
        {
            btnSave.Visible = false;
            divdetail.Visible = false;
            gvBMCDetails.DataSource = string.Empty;
            gvBMCDetails.DataBind();
            ds = objdb.ByProcedure("Usp_Trn_MilkCollectionBMCEntryAtQC",
                                new string[] { "flag", "BMCTankerRoot_Id", "Office_ID", "OfficeType_ID" },
                                new string[] { "2", ddlBMCTankerRootName.SelectedValue,objdb.Office_ID(),objdb.OfficeType_ID() }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    gvBMCDetails.DataSource = ds;
                    gvBMCDetails.DataBind();
                    btnSave.Visible = true;
                    divdetail.Visible = true;

                }
                else
                {
                    gvBMCDetails.DataSource = string.Empty;
                    gvBMCDetails.DataBind();
                }
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
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
            DropDownList ddlTankerNo = (DropDownList)e.Row.FindControl("ddlTankerNo");
            Label lblOffice_Name_E = (Label)e.Row.FindControl("lblOffice_Name_E");
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


            RequiredFieldValidator rfvTankerNo = (RequiredFieldValidator)e.Row.FindControl("rfvTankerNo");
            RequiredFieldValidator rfvTemp = (RequiredFieldValidator)e.Row.FindControl("rfvTemp");
            RequiredFieldValidator rfvMilkQty = (RequiredFieldValidator)e.Row.FindControl("rfvMilkQty");
            RequiredFieldValidator rfvFAT = (RequiredFieldValidator)e.Row.FindControl("rfvFAT");
            RequiredFieldValidator rfvCLR = (RequiredFieldValidator)e.Row.FindControl("rfvCLR");
            if (lblOffice_Name_E.Text == "Received"  || lblOffice_Name_E.Text == "Difference")
            {
                chkSelect.Visible = false;
                lblOffice_Name_E.Visible = true;
                ddlTankerNo.Visible = false;
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
                rfvTemp.Enabled = false;
                rfvMilkQty.Enabled = false;
                rfvFAT.Enabled = false;
                rfvCLR.Enabled = false;
                rfvTankerNo.Enabled = false;


            }
            else if (lblOffice_Name_E.Text == "Via Tanker No")
            {
                DataSet dsTanker = objdb.ByProcedure("Usp_Trn_MilkCollectionBMCEntryAtQC", new string[] {"flag"}, new string[] {"8" }, "dataset");
                if(dsTanker != null && dsTanker.Tables.Count > 0)
                {
                    if(dsTanker.Tables[0].Rows.Count > 0)
                    {
                        ddlTankerNo.DataSource = dsTanker;
                        ddlTankerNo.DataTextField = "V_VehicleNo";
                        ddlTankerNo.DataValueField = "I_TankerID";
                        ddlTankerNo.DataBind();
                        ddlTankerNo.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    else
                    {
                        ddlTankerNo.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
                else
                {
                    ddlTankerNo.Items.Insert(0, new ListItem("Select", "0"));
                }
                chkSelect.Visible = false;
                lblOffice_Name_E.Visible = true;
                ddlTankerNo.Visible = true;
                lblOffice_Name_E.Font.Bold = true;
              
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
                rfvTemp.Enabled = true;
                //rfvMilkQty.Enabled = true;
                rfvFAT.Enabled = true;
                rfvCLR.Enabled = true;
                rfvTankerNo.Enabled = true;

            }
            else
            {
                chkSelect.Visible = true;
                lblOffice_Name_E.Font.Bold = true;
                lblOffice_Name_E.Visible = true;
                ddlTankerNo.Visible = false;
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
                rfvTemp.Enabled = false;
                rfvMilkQty.Enabled = false;
                rfvFAT.Enabled = false;
                rfvCLR.Enabled = false;
                rfvTankerNo.Enabled = false;

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
        Session["event_control"] = txtFAT;

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
        Session["event_control"] = txtCLR;
       

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
            snf = Obj_MC.GetSNFPer_DCS(fat, clr);

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

            snf_Per = Obj_MC.GetSNFPer_DCS(fat, clr);

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
            DataTable dt = new DataTable();
            dt = GetBMCDetails();
            decimal MilkQuantity = 0;
            if(dt.Rows.Count > 0)
            {
                foreach (GridViewRow Row in gvBMCDetails.Rows)
                {

                    Label lblOffice_Name_E = (Label)Row.FindControl("lblOffice_Name_E");
                    DropDownList ddlTankerNo = (DropDownList)Row.FindControl("ddlTankerNo");
                    TextBox txtV_Temp = (TextBox)Row.FindControl("txtV_Temp");
                    TextBox txtD_MilkQuantity = (TextBox)Row.FindControl("txtD_MilkQuantity");
                    TextBox txtFAT = (TextBox)Row.FindControl("txtFAT");
                    TextBox txtCLR = (TextBox)Row.FindControl("txtCLR");
                    TextBox txtSNF = (TextBox)Row.FindControl("txtSNF");
                    TextBox txtKgFat = (TextBox)Row.FindControl("txtKgFat");
                    TextBox txtKgSNF = (TextBox)Row.FindControl("txtKgSNF");
                    if (txtD_MilkQuantity.Text == "")
                    {
                        MilkQuantity = 0;
                    }
                    else
                    {
                        MilkQuantity = decimal.Parse(txtD_MilkQuantity.Text);
                    }
                    if (lblOffice_Name_E.Text == "Via Tanker No")
                    {
                        ds = objdb.ByProcedure("Usp_Trn_MilkCollectionBMCEntryAtQC",
                             new string[] { "flag" 
                                      ,"EntryDate"
                                      ,"EntryTime"
                                      ,"BMCTankerRoot_Id"
                                      ,"Office_ID"
                                      ,"OfficeType_ID"
                                      ,"I_TankerID"
                                      ,"Quantity"
                                      ,"Temp"
                                      ,"FAT"
                                      ,"SNF"
                                      ,"CLR"
                                      ,"FatKg"
                                      ,"SnfKg"
                                      ,"OralTest"
                                      ,"COB"
                                      ,"Acidity"
                                      ,"Urea"
                                      ,"Neutralizer"
                                      ,"Maltodextrin"   
                                      ,"Glucose"
                                      ,"Sucrose"
                                      ,"Salt"
                                      ,"Starch"
                                      ,"Detergent"
                                      ,"NitrateTest"
                                      ,"IsActive"
                                      ,"CreatedAt"
                                      ,"CreatedBy"
                                      ,"CreatedByIP"                                    
                                   },
                            new string[] {"1"
                                     ,Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd")
                                     ,Convert.ToDateTime(txtTime.Text, cult).ToString("hh:mm:ss tt")
                                     ,ddlBMCTankerRootName.SelectedValue
                                     ,objdb.Office_ID()
                                     ,objdb.OfficeType_ID()
                                     ,ddlTankerNo.SelectedValue
                                     ,MilkQuantity.ToString()
                                     ,txtV_Temp.Text                                   
                                     ,txtFAT.Text
                                     ,txtSNF.Text
                                     ,txtCLR.Text
                                     ,txtKgFat.Text
                                     ,txtKgSNF.Text
                                     ,ddlOption.SelectedValue.ToString()
                                     ,ddlCOB.SelectedValue.ToString()
                                     ,txtAcidity.Text
                                     ,ddlUrea.SelectedValue.ToString()
                                     ,ddlNeutralizer.SelectedValue.ToString()
                                     ,ddlMaltodextrin.SelectedValue.ToString()
                                     ,ddlGlucose.SelectedValue.ToString()
                                     ,ddlSucrose.SelectedValue.ToString()
                                     ,ddlSalt.SelectedValue.ToString()
                                     ,ddlStarch.SelectedValue.ToString()
                                     ,ddlDetergent.SelectedValue.ToString()
                                     ,ddlNitrateTest.SelectedValue.ToString()
                                     ,"1"
                                     ,objdb.Office_ID()
                                     ,objdb.createdBy()
                                     ,objdb.GetLocalIPAddress()
                        
                        },
                            new string[] { "type_Trn_MilkCollectionBMCEntryAtQCChild" },
                            new DataTable[] { dt },
                            "TableSave");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                {
                                    Session["IsSuccess"] = true;
                                    Response.Redirect("BMCEntryAtQC.aspx", false);

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
                }

            }
           
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
    }
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            CheckBox chkbox = sender as CheckBox;
            GridViewRow currentRow = chkbox.NamingContainer as GridViewRow;

            
            RequiredFieldValidator rfvTemp = gvBMCDetails.Rows[currentRow.RowIndex]
                                               .FindControl("rfvTemp") as RequiredFieldValidator;
            RequiredFieldValidator rfvMilkQty = gvBMCDetails.Rows[currentRow.RowIndex]
                                               .FindControl("rfvMilkQty") as RequiredFieldValidator;
            RequiredFieldValidator rfvFAT = gvBMCDetails.Rows[currentRow.RowIndex]
                                               .FindControl("rfvFAT") as RequiredFieldValidator;
            RequiredFieldValidator rfvCLR = gvBMCDetails.Rows[currentRow.RowIndex]
                                               .FindControl("rfvCLR") as RequiredFieldValidator;

            CheckBox chkselect = gvBMCDetails.Rows[currentRow.RowIndex]
                                         .FindControl("chkselect") as CheckBox;

            if(chkselect.Checked)
            {
                rfvTemp.Enabled = true;
                rfvMilkQty.Enabled = true;
                rfvFAT.Enabled = true;
                rfvCLR.Enabled = true;
            }
            else
            {
                rfvTemp.Enabled = false;
                rfvMilkQty.Enabled = false;
                rfvFAT.Enabled = false;
                rfvCLR.Enabled = false;
            }
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
    }
    private DataTable GetBMCDetails()
    {
        decimal MilkQuantity = 0;
        DataTable dt = new DataTable();
        dt.Columns.Add("Office_ID", typeof(string));
        dt.Columns.Add("Quantity", typeof(string));
        dt.Columns.Add("Temp", typeof(string));
        dt.Columns.Add("FAT", typeof(string));
        dt.Columns.Add("SNF", typeof(string));
        dt.Columns.Add("CLR", typeof(string));
        dt.Columns.Add("FatKg", typeof(string));
        dt.Columns.Add("SnfKg", typeof(string));

        foreach(GridViewRow Row in gvBMCDetails.Rows)
        {
            CheckBox chkSelect = (CheckBox)Row.FindControl("chkSelect");
            Label lblOffice_ID = (Label)Row.FindControl("lblOffice_ID");
            TextBox txtV_Temp = (TextBox)Row.FindControl("txtV_Temp");           
            TextBox txtD_MilkQuantity = (TextBox)Row.FindControl("txtD_MilkQuantity");          
            TextBox txtFAT = (TextBox)Row.FindControl("txtFAT");          
            TextBox txtCLR = (TextBox)Row.FindControl("txtCLR");           
            TextBox txtSNF = (TextBox)Row.FindControl("txtSNF");           
            TextBox txtKgFat = (TextBox)Row.FindControl("txtKgFat");           
            TextBox txtKgSNF = (TextBox)Row.FindControl("txtKgSNF");
            if (txtD_MilkQuantity.Text == "")
            {
                MilkQuantity = 0;
            }
            else
            {
                MilkQuantity = decimal.Parse(txtD_MilkQuantity.Text);
            }
            if(chkSelect.Checked)
            {
                dt.Rows.Add(lblOffice_ID.Text, MilkQuantity, txtV_Temp.Text, txtFAT.Text, txtSNF.Text, txtCLR.Text, txtKgFat.Text, txtKgSNF.Text);
            }
        }
        return dt;
    }

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
           
            Label lblOffice_Name_E = (Label)gvrow.FindControl("lblOffice_Name_E");
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
            if (lblOffice_Name_E.Text == "Received")
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
            else if (lblOffice_Name_E.Text == "Via Tanker No")
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
            else if (lblOffice_Name_E.Text == "Difference")
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
}