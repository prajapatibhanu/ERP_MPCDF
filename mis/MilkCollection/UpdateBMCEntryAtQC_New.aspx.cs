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
public partial class mis_MilkCollection_UpdateBMCEntryAtQC_New : System.Web.UI.Page
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
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtDate.Attributes.Add("readonly", "readonly");
                btnUpdate.Visible = false;
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
    private void FillGrid()
    {
        try
        {
            gvBMCDetails.DataSource = string.Empty;
            gvBMCDetails.DataBind();
            ds = objdb.ByProcedure("USP_UpdateBMCEnrtyAtQcNew",
                                new string[] { "flag", "BI_MilkInOutRefID" },
                                new string[] { "3", ddlReferenceNo.SelectedValue, }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvBMCDetails.DataSource = ds.Tables[0];
                    gvBMCDetails.DataBind();
                    btnUpdate.Visible = true;
                    GetCalculation();
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
        //SetFocus(txtFAT);
        //txtFAT.Text = "";
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
        //txtCLR.Focus();
        //txtCLR.Text = "";


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
                lblKgFat.Text = RcvdTotalFatKg.ToString();
                lblKgSNF.Text = RcvdTotalSnfKg.ToString();

            }
            else if (lblType.Text == "Single" || lblType.Text == "Front" || lblType.Text == "Rear")
            {
                if (MilkQuantity.Text != "")
                {
                    ViaTankerTotalQty += decimal.Parse(MilkQuantity.Text);
                }
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
                lblKgFat.Text = (ViaTankerTotalFatKg - RcvdTotalFatKg).ToString();
                lblKgSNF.Text = (ViaTankerTotalSnfKg - RcvdTotalSnfKg).ToString();
            }
            else
            {
                if (MilkQuantity.Text != "")
                {
                    RcvdTotalQty += decimal.Parse(MilkQuantity.Text);
                }
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
    protected void FillReferenceNo()
    {
        ds = null;
        ddlReferenceNo.Items.Clear();
        ds = objdb.ByProcedure("USP_UpdateBMCEnrtyAtQcNew",
                     new string[] { "flag", "I_OfficeID", "D_Date" },
                     new string[] { "4", objdb.Office_ID(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
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
    protected void ddlReferenceNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        FillGrid();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("UpdateBMCEntryAtQC_New.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtUpdate = new DataTable();
            dtUpdate.Columns.Add("BI_MilkInOutRefID", typeof(string));
            dtUpdate.Columns.Add("Type", typeof(string));
            dtUpdate.Columns.Add("Temp", typeof(string));
            dtUpdate.Columns.Add("Qty", typeof(string));
            dtUpdate.Columns.Add("Fat_Per", typeof(string));
            dtUpdate.Columns.Add("Clr", typeof(string));
            dtUpdate.Columns.Add("SNF_Per", typeof(string));
            dtUpdate.Columns.Add("KG_Fat", typeof(string));
            dtUpdate.Columns.Add("KG_SNF", typeof(string));
            string Type = "";
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
                    Type = "S";
                    dtUpdate.Rows.Add(ddlReferenceNo.SelectedValue,
                                    Type,
                                    txtV_Temp.Text,
                                    txtD_MilkQuantity.Text,
                                    txtFAT.Text,
                                    txtCLR.Text,
                                    txtSNF.Text,
                                    txtKgFat.Text,
                                    txtKgSNF.Text);
                }
                if (lblType.Text == "Front")
                {
                    Type = "F";
                    dtUpdate.Rows.Add(ddlReferenceNo.SelectedValue,
                                    Type,
                                    txtV_Temp.Text,
                                    txtD_MilkQuantity.Text,
                                    txtFAT.Text,
                                    txtCLR.Text,
                                    txtSNF.Text,
                                    txtKgFat.Text,
                                    txtKgSNF.Text);
                }
                if (lblType.Text == "Rear")
                {
                    Type = "R";
                    dtUpdate.Rows.Add(ddlReferenceNo.SelectedValue,
                                    Type,
                                    txtV_Temp.Text,
                                    txtD_MilkQuantity.Text,
                                    txtFAT.Text,
                                    txtCLR.Text,
                                    txtSNF.Text,
                                    txtKgFat.Text,
                                    txtKgSNF.Text);
                }
            }
            ds = objdb.ByProcedure("USP_UpdateBMCEnrtyAtQcNew", new string[] { "flag", "BI_MilkInOutRefID" },
                                new string[] { "2", ddlReferenceNo.SelectedValue.ToString() }, new string[] { "type_UpdateBMCEnrtyAtQc" }, new DataTable[] { dtUpdate }, "TableSave");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    btnUpdate.Visible = false;
                    gvBMCDetails.DataSource = null;
                    gvBMCDetails.DataBind();
                    ddlReferenceNo.Items.Clear();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 11:" + ex.Message.ToString());
        }
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            gvBMCDetails.DataSource = null;
            gvBMCDetails.DataBind();
            FillReferenceNo();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }
}