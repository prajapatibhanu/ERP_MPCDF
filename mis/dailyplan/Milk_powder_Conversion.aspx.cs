using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

public partial class mis_dailyplan_Milk_powder_Conversion : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    string Fdate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                lblMsg.Text = "";

                if (!IsPostBack)
                {
                    txtDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                    txtDate.Attributes.Add("readonly", "readonly");
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    // txtCMFatPer.Attributes.Add("readonly", "readonly");
                    // txtCMSNFPer.Attributes.Add("readonly", "readonly");
                    // txtSMFatPer.Attributes.Add("readonly", "readonly");
                    // txtSMSNFPer.Attributes.Add("readonly", "readonly");
                    // txtWMRFatPer.Attributes.Add("readonly", "readonly");
                    // txtWMRSNFPer.Attributes.Add("readonly", "readonly");
                    // txtCSPFatPer.Attributes.Add("readonly", "readonly");
                    // txtCSPSNFPer.Attributes.Add("readonly", "readonly");
                    // txtSMPFatPer.Attributes.Add("readonly", "readonly");
                    // txtSMPSNFPer.Attributes.Add("readonly", "readonly");
                    // txtCBRFatPer.Attributes.Add("readonly", "readonly");
                    // txtCBRSNFPer.Attributes.Add("readonly", "readonly");
                    // txtOutCSPFatPer.Attributes.Add("readonly", "readonly");
                    // txtOutCSPSNFPer.Attributes.Add("readonly", "readonly");
                    txtTFatInKg.Attributes.Add("readonly", "readonly");
                    txtTSnfInKg.Attributes.Add("readonly", "readonly");
                    txtReFatInKg.Attributes.Add("readonly", "readonly");
                    txtReSNFInKg.Attributes.Add("readonly", "readonly");
                    txtNormFatInKg.Attributes.Add("readonly", "readonly");
                    txtNormSNFInKg.Attributes.Add("readonly", "readonly");
                    FillOffice();
                    GetSectionView(sender, e);
					GetData();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    #region User Defined Function

    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "12" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDS.DataSource = ds.Tables[0];
                ddlDS.DataTextField = "Office_Name";
                ddlDS.DataValueField = "Office_ID";
                ddlDS.DataBind();
                ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                ddlDS.SelectedValue = ViewState["Office_ID"].ToString();
                ddlDS.Enabled = false;
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetSectionView(object sender, EventArgs e)
    {
        try
        {
            ds = null;

            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID" },
                  new string[] { "6", ddlDS.SelectedValue }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPSection.DataSource = ds.Tables[0];
                ddlPSection.DataTextField = "ProductSection_Name";
                ddlPSection.DataValueField = "ProductSection_ID";
                ddlPSection.DataBind();
                ddlPSection.SelectedValue = "2";
                ddlPSection.Enabled = false;
            }
            else
            {
                ddlPSection.Items.Clear();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.DataBind();
                ddlPSection.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }
    #endregion
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int status = 0;
            //if ((Convert.ToDecimal(txtReFatInKg.Text) - Convert.ToDecimal(txtNormFatInKg.Text)) > Convert.ToDecimal("2"))
            //{
            //    status = 1;
            //}
            //else if ((Convert.ToDecimal(txtNormFatInKg.Text) - Convert.ToDecimal(txtReFatInKg.Text)) > Convert.ToDecimal("2"))
            //{
            //    status = 1;
            //}
            if (status == 0)
            {
                DataTable dtInFlow = new DataTable();
                DataTable dtOutFlow = new DataTable();
                DataTable dtVariation = new DataTable();

                dtInFlow.Columns.Add("ParticularName", typeof(string));
                dtInFlow.Columns.Add("Qty", typeof(string));
                dtInFlow.Columns.Add("FatPer", typeof(string));
                dtInFlow.Columns.Add("FatInKg", typeof(string));
                dtInFlow.Columns.Add("SNFPer", typeof(string));
                dtInFlow.Columns.Add("SNFInKg", typeof(string));


                dtOutFlow.Columns.Add("ParticularName", typeof(string));
                dtOutFlow.Columns.Add("Qty", typeof(string));
                dtOutFlow.Columns.Add("FatPer", typeof(string));
                dtOutFlow.Columns.Add("FatInKg", typeof(string));
                dtOutFlow.Columns.Add("SNFPer", typeof(string));
                dtOutFlow.Columns.Add("SNFInKg", typeof(string));

                dtVariation.Columns.Add("VariationName", typeof(string));
                dtVariation.Columns.Add("FatInKg", typeof(string));
                dtVariation.Columns.Add("SNFInKg", typeof(string));

                if (txtMOQtyInKg.Text == "")
                { txtMOQtyInKg.Text = "0"; }
                if (txtMOFatPer.Text == "")
                { txtMOFatPer.Text = "0"; }
                if (txtMOFatInKg.Text == "")
                { txtMOFatInKg.Text = "0"; }
                if (txtMOSNFPer.Text == "")
                { txtMOSNFPer.Text = "0"; }
                if (txtMOSNFInKg.Text == "")
                { txtMOSNFInKg.Text = "0"; }
                if (txtCMQtyInKg.Text == "")
                { txtCMQtyInKg.Text = "0"; }
                if (txtCMFatPer.Text == "")
                { txtCMFatPer.Text = "0"; }
                if (txtCMFatInKg.Text == "")
                { txtCMFatInKg.Text = "0"; }
                if (txtCMSNFPer.Text == "")
                { txtCMSNFPer.Text = "0"; }
                if (txtCMSNFInKg.Text == "")
                { txtCMSNFInKg.Text = "0"; }
                if (txtSMQtyInKg.Text == "")
                { txtSMQtyInKg.Text = "0"; }
                if (txtSMFatPer.Text == "")
                { txtSMFatPer.Text = "0"; }
                if (txtSMFatInKg.Text == "")
                { txtSMFatInKg.Text = "0"; }
                if (txtSMSNFPer.Text == "")
                { txtSMSNFPer.Text = "0"; }
                if (txtSMSNFInKg.Text == "")
                { txtSMSNFInKg.Text = "0"; }
                if (txtWMRQtyInKg.Text == "")
                { txtWMRQtyInKg.Text = "0"; }
                if (txtWMRFatPer.Text == "")
                { txtWMRFatPer.Text = "0"; }
                if (txtWMRFatInKg.Text == "")
                { txtWMRFatInKg.Text = "0"; }
                if (txtWMRSNFPer.Text == "")
                { txtWMRSNFPer.Text = "0"; }
                if (txtWMRSNFInKg.Text == "")
                { txtWMRSNFInKg.Text = "0"; }
                if (txtCSPQtyInKg.Text == "")
                { txtCSPQtyInKg.Text = "0"; }
                if (txtCSPFatPer.Text == "")
                { txtCSPFatPer.Text = "0"; }
                if (txtCSPFatInKg.Text == "")
                { txtCSPFatInKg.Text = "0"; }
                if (txtCSPSNFPer.Text == "")
                { txtCSPSNFPer.Text = "0"; }
                if (txtCSPSNFInKg.Text == "")
                { txtCSPSNFInKg.Text = "0"; }
                if (txtOtherQtyInKg.Text == "")
                { txtOtherQtyInKg.Text = "0"; }
                if (txtOtherFatPer.Text == "")
                { txtOtherFatPer.Text = "0"; }
                if (txtOtherFatInKg.Text == "")
                { txtOtherFatInKg.Text = "0"; }
                if (txtOtherSNFPer.Text == "")
                { txtOtherSNFPer.Text = "0"; }
                if (txtOtherSNFInKg.Text == "")
                { txtOtherSNFInKg.Text = "0"; }
                if (txtSMPQtyInKg.Text == "")
                { txtSMPQtyInKg.Text = "0"; }
                if (txtSMPFatPer.Text == "")
                { txtSMPFatPer.Text = "0"; }
                if (txtSMPFatInKg.Text == "")
                { txtSMPFatInKg.Text = "0"; }
                if (txtSMPSNFPer.Text == "")
                { txtSMPSNFPer.Text = "0"; }
                if (txtSMPSNFInKg.Text == "")
                { txtSMPSNFInKg.Text = "0"; }
                if (txtWMPQtyInKg.Text == "")
                { txtWMPQtyInKg.Text = "0"; }
                if (txtWMPFatPer.Text == "")
                { txtWMPFatPer.Text = "0"; }
                if (txtWMPFatInKg.Text == "")
                { txtWMPFatInKg.Text = "0"; }
                if (txtWMPSNFPer.Text == "")
                { txtWMPSNFPer.Text = "0"; }
                if (txtWMPSNFInKg.Text == "")
                { txtWMPSNFInKg.Text = "0"; }
                if (txtCMRQtyInKg.Text == "")
                { txtCMRQtyInKg.Text = "0"; }
                if (txtCMRFatPer.Text == "")
                { txtCMRFatPer.Text = "0"; }
                if (txtCMRFatInKg.Text == "")
                { txtCMRFatInKg.Text = "0"; }
                if (txtCMRSNFPer.Text == "")
                { txtCMRSNFPer.Text = "0"; }
                if (txtCMRSNFInKg.Text == "")
                { txtCMRSNFInKg.Text = "0"; }
                if (txtMRQtyInKg.Text == "")
                { txtMRQtyInKg.Text = "0"; }
                if (txtMRFatPer.Text == "")
                { txtMRFatPer.Text = "0"; }
                if (txtMRFatInKg.Text == "")
                { txtMRFatInKg.Text = "0"; }
                if (txtMRSNFPer.Text == "")
                { txtMRSNFPer.Text = "0"; }
                if (txtMRSNFInKg.Text == "")
                { txtMRSNFInKg.Text = "0"; }
                if (txtCBRQtyInKg.Text == "")
                { txtCBRQtyInKg.Text = "0"; }
                if (txtCBRFatPer.Text == "")
                { txtCBRFatPer.Text = "0"; }
                if (txtCBRFatInKg.Text == "")
                { txtCBRFatInKg.Text = "0"; }
                if (txtCBRSNFPer.Text == "")
                { txtCBRSNFPer.Text = "0"; }
                if (txtCBRSNFInKg.Text == "")
                { txtCBRSNFInKg.Text = "0"; }
                if (txtOutCSPQtyInKg.Text == "")
                { txtOutCSPQtyInKg.Text = "0"; }
                if (txtOutCSPFatPer.Text == "")
                { txtOutCSPFatPer.Text = "0"; }
                if (txtOutCSPFatInKg.Text == "")
                { txtOutCSPFatInKg.Text = "0"; }
                if (txtOutCSPSNFPer.Text == "")
                { txtOutCSPSNFPer.Text = "0"; }
                if (txtOutCSPSNFInKg.Text == "")
                { txtOutCSPSNFInKg.Text = "0"; }
                if (txtTFatInKg.Text == "")
                { txtTFatInKg.Text = "0"; }
                if (txtTSnfInKg.Text == "")
                { txtTSnfInKg.Text = "0"; }
                if (txtReFatInKg.Text == "")
                { txtReFatInKg.Text = "0"; }
                if (txtReSNFInKg.Text == "")
                { txtReSNFInKg.Text = "0"; }
                if (txtNormFatInKg.Text == "")
                { txtNormFatInKg.Text = "0"; }
                if (txtNormSNFInKg.Text == "")
                { txtNormSNFInKg.Text = "0"; }
                if (txtTotalQtyInKg.Text == "")
                { txtTotalQtyInKg.Text = "0"; }
                if (txtTotalFatInKg.Text == "")
                { txtTotalFatInKg.Text = "0"; }
                if (txtTotalSNFInKg.Text == "")
                { txtTotalSNFInKg.Text = "0"; }
                if (txtOutTotalQtyInKg.Text == "")
                { txtOutTotalQtyInKg.Text = "0"; }
                if (txtOutTotalFatInKg.Text == "")
                { txtOutTotalFatInKg.Text = "0"; }
                if (txtOutTotalSNFInKg.Text == "")
                { txtOutTotalSNFInKg.Text = "0"; }

                dtInFlow.Rows.Add("Milk Opening", txtMOQtyInKg.Text, txtMOFatPer.Text, txtMOFatInKg.Text, txtMOSNFPer.Text, txtMOSNFInKg.Text);
                dtInFlow.Rows.Add("Condense Milk", txtCMQtyInKg.Text, txtCMFatPer.Text, txtCMFatInKg.Text, txtCMSNFPer.Text, txtCMSNFInKg.Text);
                dtInFlow.Rows.Add("Skimmed Milk recived from Processing", txtSMQtyInKg.Text, txtSMFatPer.Text, txtSMFatInKg.Text, txtSMSNFPer.Text, txtSMSNFInKg.Text);
                dtInFlow.Rows.Add("Whole Milk recived from Processing", txtWMRQtyInKg.Text, txtWMRFatPer.Text, txtWMRFatInKg.Text, txtWMRSNFPer.Text, txtWMRSNFInKg.Text);
                dtInFlow.Rows.Add("CSP", txtCSPQtyInKg.Text, txtCSPFatPer.Text, txtCSPFatInKg.Text, txtCSPSNFPer.Text, txtCSPSNFInKg.Text);
                dtInFlow.Rows.Add("Others", txtOtherQtyInKg.Text, txtOtherFatPer.Text, txtOtherFatInKg.Text, txtOtherSNFPer.Text, txtOtherSNFInKg.Text);


                dtOutFlow.Rows.Add("SMP Mfg", txtSMPQtyInKg.Text, txtSMPFatPer.Text, txtSMPFatInKg.Text, txtSMPSNFPer.Text, txtSMPSNFInKg.Text);
                dtOutFlow.Rows.Add("WMP Mfg", txtWMPQtyInKg.Text, txtWMPFatPer.Text, txtWMPFatInKg.Text, txtWMPSNFPer.Text, txtWMPSNFInKg.Text);
                dtOutFlow.Rows.Add("Condense Milk return to processing", txtCMRQtyInKg.Text, txtCMRFatPer.Text, txtCMRFatInKg.Text, txtCMRSNFPer.Text, txtCMRSNFInKg.Text);
                dtOutFlow.Rows.Add("Milk return to processing", txtMRQtyInKg.Text, txtMRFatPer.Text, txtMRFatInKg.Text, txtMRSNFPer.Text, txtMRSNFInKg.Text);
                dtOutFlow.Rows.Add("Closing Balance ( Only Condense Milk)", txtCBRQtyInKg.Text, txtCBRFatPer.Text, txtCBRFatInKg.Text, txtCBRSNFPer.Text, txtCBRSNFInKg.Text);
                dtOutFlow.Rows.Add("CSP", txtOutCSPQtyInKg.Text, txtOutCSPFatPer.Text, txtOutCSPFatInKg.Text, txtOutCSPSNFPer.Text, txtOutCSPSNFInKg.Text);

                dtVariation.Rows.Add("Total", txtTFatInKg.Text, txtTSnfInKg.Text);
                dtVariation.Rows.Add("Recovery %", txtReFatInKg.Text, txtReSNFInKg.Text);
                dtVariation.Rows.Add("Norms (Fix content to show)", txtNormFatInKg.Text, txtNormSNFInKg.Text);

                ds = objdb.ByProcedure("USP_MilkPowderConversionEntry", new string[] { "flag", "EntryDate", "CreatedBy", "CreatedIP","Office_ID"  },
                    new string[] { "1", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Emp_ID"].ToString(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),objdb.Office_ID() }, "dataset");
                if (ds != null)
                {
                    string MilkPowderId = ds.Tables[0].Rows[0]["MilkPowderId"].ToString();
                    ds = objdb.ByProcedure("USP_MilkPowderConversionEntry", new string[] { "flag", "MilkPowderId" },
                   new string[] { "2", MilkPowderId },
                   new string[] { "type_MilkPowderConversionEntry_ChildInflow", "type_MilkPowderConversionEntry_ChildOutFlow", "type_MilkPowderConversionEntry_Variation" },
                   new DataTable[] { dtInFlow, dtOutFlow, dtVariation }, "TableSave");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            Clear();
                        }
                    }
                }
            }
            else if (status == 1)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Difference of Recovery % and Norm should not be greater than 2.');", true);
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void btnGetTotal_Click(object sender, EventArgs e)
    {

    }
    protected void Clear()
    {
        txtMOQtyInKg.Text = ""; txtMOFatPer.Text = ""; txtMOFatInKg.Text = ""; txtMOSNFPer.Text = ""; txtMOSNFInKg.Text = ""; txtCMQtyInKg.Text = ""; txtCMFatPer.Text = ""; txtCMFatInKg.Text = ""; txtCMSNFPer.Text = ""; txtCMSNFInKg.Text = "";
        txtSMQtyInKg.Text = ""; txtSMFatPer.Text = ""; txtSMFatInKg.Text = ""; txtSMSNFPer.Text = ""; txtSMSNFInKg.Text = ""; txtWMRQtyInKg.Text = ""; txtWMRFatPer.Text = ""; txtWMRFatInKg.Text = ""; txtWMRSNFPer.Text = ""; txtWMRSNFInKg.Text = "";
        txtCSPQtyInKg.Text = ""; txtCSPFatPer.Text = ""; txtCSPFatInKg.Text = ""; txtCSPSNFPer.Text = ""; txtCSPSNFInKg.Text = ""; txtOtherQtyInKg.Text = ""; txtOtherFatPer.Text = ""; txtOtherFatInKg.Text = ""; txtOtherSNFPer.Text = ""; txtOtherSNFInKg.Text = ""; txtSMPQtyInKg.Text = ""; txtSMPFatPer.Text = ""; txtSMPFatInKg.Text = ""; txtSMPSNFPer.Text = ""; txtSMPSNFInKg.Text = "";
        txtWMPQtyInKg.Text = ""; txtWMPFatPer.Text = ""; txtWMPFatInKg.Text = ""; txtWMPSNFPer.Text = ""; txtWMPSNFInKg.Text = "";
        txtCMRQtyInKg.Text = ""; txtCMRFatPer.Text = ""; txtCMRFatInKg.Text = ""; txtCMRSNFPer.Text = ""; txtCMRSNFInKg.Text = ""; txtMRQtyInKg.Text = ""; txtMRFatPer.Text = ""; txtMRFatInKg.Text = ""; txtMRSNFPer.Text = ""; txtMRSNFInKg.Text = ""; txtCBRQtyInKg.Text = ""; txtCBRFatPer.Text = ""; txtCBRFatInKg.Text = ""; txtCBRSNFPer.Text = ""; txtCBRSNFInKg.Text = ""; txtOutCSPQtyInKg.Text = ""; txtOutCSPFatPer.Text = ""; txtOutCSPFatInKg.Text = ""; txtOutCSPSNFPer.Text = ""; txtOutCSPSNFInKg.Text = ""; txtTFatInKg.Text = ""; txtTSnfInKg.Text = ""; txtReFatInKg.Text = ""; txtReSNFInKg.Text = ""; txtNormFatInKg.Text = ""; txtNormSNFInKg.Text = "";
        txtTotalQtyInKg.Text = ""; txtTotalFatInKg.Text = ""; txtTotalSNFInKg.Text = ""; txtOutTotalQtyInKg.Text = ""; txtOutTotalFatInKg.Text = ""; txtOutTotalSNFInKg.Text = "";
    }
	protected void GetData()
    
    {
        try
        {
			txtMOQtyInKg.Text = "";
            txtMOFatPer.Text =  "";
            txtMOFatInKg.Text = "";
            txtMOSNFPer.Text =  "";
            txtMOSNFInKg.Text = "";
            txtSMQtyInKg.Text = "";
			txtSMFatInKg.Text = "";
			txtSMSNFInKg.Text = "";
            txtWMRQtyInKg.Text = "";
            ds = objdb.ByProcedure("USP_MilkPowderConversionEntry", new string[] { "flag", "Date", "Office_ID" }, new string[] {"4",Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),objdb.Office_ID() }, "dataset");
            if(ds!= null && ds.Tables.Count > 0)
            {

                if(ds.Tables[0].Rows.Count > 0)
                {
                    int Count = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["Variant"].ToString() == "For Skimmed Milk")
                        {
                            txtSMQtyInKg.Text = ds.Tables[0].Rows[i]["QtyInKg"].ToString();
							txtSMFatInKg.Text = ds.Tables[0].Rows[i]["FatInKg"].ToString();
                            txtSMSNFInKg.Text = ds.Tables[0].Rows[i]["SNFInKg"].ToString();
                        }
                        else if (ds.Tables[0].Rows[i]["Variant"].ToString() == "For Whole Milk(HPWM)")
                        {
                            txtWMRQtyInKg.Text = ds.Tables[0].Rows[i]["QtyInKg"].ToString();
							txtWMRFatInKg.Text = ds.Tables[0].Rows[i]["FatInKg"].ToString();
                            txtWMRSNFInKg.Text = ds.Tables[0].Rows[i]["SNFInKg"].ToString();
                            txtWMRFatPer.Text = Math.Round(((decimal.Parse(txtWMRFatInKg.Text) * 100) / decimal.Parse(txtWMRQtyInKg.Text)),2).ToString();
                            txtWMRSNFPer.Text = Math.Round(((decimal.Parse(txtWMRSNFInKg.Text) * 100) / decimal.Parse(txtWMRQtyInKg.Text)),2).ToString();
                        }
                    }
                    
                }
				if(ds.Tables[1].Rows.Count > 0)
                {
                    txtCMQtyInKg.Text = ds.Tables[1].Rows[0]["Qty"].ToString();
                    txtCMFatPer.Text = ds.Tables[1].Rows[0]["FatPer"].ToString();
                    txtCMFatInKg.Text = ds.Tables[1].Rows[0]["FatInKg"].ToString();
                    txtCMSNFPer.Text = ds.Tables[1].Rows[0]["SNFPer"].ToString();
                    txtCMSNFInKg.Text = ds.Tables[1].Rows[0]["SNFInKg"].ToString();
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
	protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        GetData();
    }
}