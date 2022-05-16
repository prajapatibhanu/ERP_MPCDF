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

public partial class mis_MilkCollection_EditRMRDChallanEntry : System.Web.UI.Page
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

                FillBMCRoot();

            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    public void FillBMCRoot()
    {

        try
        {
            ds = null;
            ds = objdb.ByProcedure("Usp_DeleteChallanEntry",
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
    protected void FillSociety()
    {
        try
        {
            if (ddlBMCTankerRootName.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("Usp_DeleteChallanEntry",
                                  new string[] { "flag", "BMCTankerRoot_Id" },
                                  new string[] { "2", ddlBMCTankerRootName.SelectedValue }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSociety.DataTextField = "Office_Name";
                        ddlSociety.DataValueField = "Office_ID";
                        ddlSociety.DataSource = ds.Tables[0];
                        ddlSociety.DataBind();
                        ddlSociety.Items.Insert(0, new ListItem("Select", "0"));


                    }
                    else
                    {
                        ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
                else
                {
                    ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                Response.Redirect("DeleteRMRDChallanEntry.aspx", false);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlBMCTankerRootName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSociety();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void FillGrid()
    {
        try
        {
            btnEdit.Visible = false;

            string Date = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");

            ds = objdb.ByProcedure("Usp_DeleteChallanEntry", new string[] { "flag", "Date", "Office_ID" }, new string[] { "3", Date, ddlSociety.SelectedValue }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnEdit.Visible = true;
                    gv_MilkCollectionChallanEntryDetails.DataSource = ds;
                    gv_MilkCollectionChallanEntryDetails.DataBind();




                }
                else
                {
                    btnEdit.Visible = false;
                    gv_MilkCollectionChallanEntryDetails.DataSource = string.Empty;
                    gv_MilkCollectionChallanEntryDetails.DataBind();
                }
            }
            else
            {
                btnEdit.Visible = false;
                gv_MilkCollectionChallanEntryDetails.DataSource = string.Empty;
                gv_MilkCollectionChallanEntryDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }

    protected void gv_MilkCollectionChallanEntryDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblV_Shift = (Label)e.Row.FindControl("lblV_Shift");
            DropDownList ddlShift = (DropDownList)e.Row.FindControl("ddlShift");
            Label lblV_MilkType = (Label)e.Row.FindControl("lblV_MilkType");
            DropDownList ddlMilkType = (DropDownList)e.Row.FindControl("ddlMilkType");
            Label lblQuality = (Label)e.Row.FindControl("lblQuality");
            DropDownList ddlMilkQuality = (DropDownList)e.Row.FindControl("ddlMilkQuality");



            ddlShift.Items.FindByText(lblV_Shift.Text).Selected = true;
            ddlMilkType.Items.FindByText(lblV_MilkType.Text).Selected = true;
            ddlMilkQuality.Items.FindByText(lblQuality.Text).Selected = true;


        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataTable dtData = GetData();
            if(dtData.Rows.Count > 0)
            {
                ds = objdb.ByProcedure("Usp_DeleteChallanEntry", new string[] { "flag" }, new string[] { "5" }, new string[] { "type_EditMilkCollectionChallanEntry" }, new DataTable[] { dtData }, "dataset");
                if(ds != null && ds.Tables.Count > 0)
                {
                    if(ds.Tables[0].Rows.Count > 0)
                    {
                        if(ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-success", "alert-success", "TankYou!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                        }
                    }
                }
                FillGrid();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Please select at Least one Record for Update");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    protected DataTable GetData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("MilkCollectionChallan_ID", typeof(int));
        dt.Columns.Add("Shift", typeof(string));
        dt.Columns.Add("MilkType", typeof(string));  
        dt.Columns.Add("MilkQuantity", typeof(decimal));
        dt.Columns.Add("MilkQuality", typeof(string));
        dt.Columns.Add("Fat", typeof(decimal));
        dt.Columns.Add("Snf", typeof(decimal));
        dt.Columns.Add("Clr", typeof(decimal));
        dt.Columns.Add("FatInKg", typeof(decimal));
        dt.Columns.Add("SnfInKg", typeof(decimal));
        foreach (GridViewRow row in gv_MilkCollectionChallanEntryDetails.Rows)
            {
                DropDownList ddlShift = (DropDownList)row.FindControl("ddlShift");
                Label lblMilkCollectionChallan_ID = (Label)row.FindControl("lblMilkCollectionChallan_ID");
                DropDownList ddlMilkType = (DropDownList)row.FindControl("ddlMilkType");

                DropDownList ddlMilkQuality = (DropDownList)row.FindControl("ddlMilkQuality");
                TextBox txtI_MilkSupplyQty = (TextBox)row.FindControl("txtI_MilkSupplyQty");
                TextBox txtFat = (TextBox)row.FindControl("txtFat");
                TextBox txtCLR = (TextBox)row.FindControl("txtCLR");
                TextBox txtSNF = (TextBox)row.FindControl("txtSNF");
                TextBox txtFAT_IN_KG = (TextBox)row.FindControl("txtFAT_IN_KG");
                TextBox txtSNF_IN_KG = (TextBox)row.FindControl("txtSNF_IN_KG");
                CheckBox chkselect = (CheckBox)row.FindControl("chkselect");
                if(chkselect.Checked)
                {
                    dt.Rows.Add(lblMilkCollectionChallan_ID.Text, ddlShift.SelectedValue, ddlMilkType.SelectedValue, txtI_MilkSupplyQty.Text, ddlMilkQuality.SelectedValue, txtFat.Text, txtSNF.Text, txtCLR.Text, txtFAT_IN_KG.Text, txtSNF_IN_KG.Text);
                }
            }
        return dt;
    }
    protected void chkselect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow Rows in gv_MilkCollectionChallanEntryDetails.Rows)
            {
                DropDownList ddlShift = (DropDownList)Rows.FindControl("ddlShift");

                DropDownList ddlMilkType = (DropDownList)Rows.FindControl("ddlMilkType");

                DropDownList ddlMilkQuality = (DropDownList)Rows.FindControl("ddlMilkQuality");
                TextBox txtI_MilkSupplyQty = (TextBox)Rows.FindControl("txtI_MilkSupplyQty");
                TextBox txtFat = (TextBox)Rows.FindControl("txtFat");
                TextBox txtCLR = (TextBox)Rows.FindControl("txtCLR");
                TextBox txtSNF = (TextBox)Rows.FindControl("txtSNF");
                TextBox txtFAT_IN_KG = (TextBox)Rows.FindControl("txtFAT_IN_KG");
                TextBox txtSNF_IN_KG = (TextBox)Rows.FindControl("txtSNF_IN_KG");
                CheckBox chkselect = (CheckBox)Rows.FindControl("chkselect");
                if (chkselect.Checked)
                {
                    ddlShift.Enabled = true;
                    ddlMilkType.Enabled = true;
                    ddlMilkQuality.Enabled = true;
                    txtI_MilkSupplyQty.Enabled = true;
                    txtFat.Enabled = true;
                    txtCLR.Enabled = true;
                    ddlShift.Enabled = true;
                    txtFAT_IN_KG.Enabled = true;
                    txtSNF_IN_KG.Enabled = true;
                    txtSNF.Enabled = true;
                }
                else
                {
                    ddlShift.Enabled = false;
                    ddlMilkType.Enabled = false;
                    ddlMilkQuality.Enabled = false;
                    txtI_MilkSupplyQty.Enabled = false;
                    txtFat.Enabled = false;
                    txtCLR.Enabled = false;
                    ddlShift.Enabled = false;
                    txtFAT_IN_KG.Enabled = false;
                    txtSNF_IN_KG.Enabled = false;
                    txtSNF.Enabled = false;
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
    protected void txtI_MilkSupplyQty_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox txtI_MilkSupplyQty = (TextBox)row.FindControl("txtI_MilkSupplyQty");
        TextBox txtFAT = (TextBox)row.FindControl("txtFat");
        TextBox txtCLR = (TextBox)row.FindControl("txtCLR");
        TextBox txtSNF = (TextBox)row.FindControl("txtSNF");
        TextBox txtKgFat = (TextBox)row.FindControl("txtFAT_IN_KG");
        TextBox txtKgSNF = (TextBox)row.FindControl("txtSNF_IN_KG");
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

    }
    protected void txtFat_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox txtI_MilkSupplyQty = (TextBox)row.FindControl("txtI_MilkSupplyQty");
        TextBox txtFAT = (TextBox)row.FindControl("txtFat");
        TextBox txtCLR = (TextBox)row.FindControl("txtCLR");
        TextBox txtSNF = (TextBox)row.FindControl("txtSNF");
        TextBox txtKgFat = (TextBox)row.FindControl("txtFAT_IN_KG");
        TextBox txtKgSNF = (TextBox)row.FindControl("txtSNF_IN_KG");
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

    }
    protected void txtCLR_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox txtI_MilkSupplyQty = (TextBox)row.FindControl("txtI_MilkSupplyQty");
        TextBox txtFAT = (TextBox)row.FindControl("txtFat");
        TextBox txtCLR = (TextBox)row.FindControl("txtCLR");
        TextBox txtSNF = (TextBox)row.FindControl("txtSNF");
        TextBox txtKgFat = (TextBox)row.FindControl("txtFAT_IN_KG");
        TextBox txtKgSNF = (TextBox)row.FindControl("txtSNF_IN_KG");
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
    }
}