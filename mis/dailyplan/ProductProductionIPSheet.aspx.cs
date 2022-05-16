using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;
using System.Configuration;
using System.Web.Configuration;
using System.Text;


public partial class mis_dailyplan_ProductProductionIPSheet : System.Web.UI.Page
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
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillOffice();
                    FillShift();
                    GetSectionView(sender, e);

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
				
				ddlDSPurchasedfrom.DataSource = ds.Tables[0];
                ddlDSPurchasedfrom.DataTextField = "Office_Name";
                ddlDSPurchasedfrom.DataValueField = "Office_ID";
                ddlDSPurchasedfrom.DataBind();
                ddlDSPurchasedfrom.Items.Insert(0, new ListItem("Select", "0"));
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

    protected void FillShift()
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                 new string[] { "flag" },
                 new string[] { "2" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlShift.DataSource = ds.Tables[0];
                ddlShift.DataTextField = "Name";
                ddlShift.DataValueField = "Shift_Id";
                ddlShift.DataBind();
                ddlShift.SelectedValue = ds.Tables[1].Rows[0]["Shift_Id"].ToString();
                txtDate.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["currentDate"].ToString(), cult).ToString("dd/MM/yyyy");
                ddlShift.Enabled = false;
                //txtDate.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        if (ddlPSection.SelectedValue != "0")
        {
            GetSectionDetail();
        }
        else
        {
            gvmttos.DataSource = string.Empty;
            gvmttos.DataBind();
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

                ddlPSection_SelectedIndexChanged(sender, e);

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

    protected void ddlPSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPSection.SelectedValue != "0")
        {

            GetSectionDetail();

        }
        else
        {

            gvmttos.DataSource = string.Empty;
            gvmttos.DataBind();
        }
    }

    private void GetSectionDetail()
    {

        try
        {

            ds = null;

            int strcat_id = 0;

            if (ddlPSection.SelectedValue == "1")
            {
                strcat_id = 3;
            }
            if (ddlPSection.SelectedValue == "2")
            {
                strcat_id = 2;
            }

            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            ds = null;
            ds = objdb.ByProcedure("Usp_Production_ProductIPSheetMaster",
                  new string[] { "flag", "Office_ID", "Shift_Id", "ProductSection_ID", "Date", "ItemCat_id", "ItemType_id" },
                  new string[] { "1", ddlDS.SelectedValue, ddlShift.SelectedValue, ddlPSection.SelectedValue, Fdate, strcat_id.ToString(), "0" }, "dataset");
           


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvmttos.DataSource = ds;
                gvmttos.DataBind();

                //decimal PrvD_InPkt = 0;
                //decimal PrvD_InLtr = 0;
                //decimal CurD_InPkt = 0;
                //decimal CurD_InLtr = 0;

                //foreach (GridViewRow row in gvmttos.Rows)
                //{

                //    Label lblPrev_Demand_InPkt = (Label)row.FindControl("lblPrev_Demand_InPkt");
                //    Label Prev_Demand_InPkt_F = (gvmttos.FooterRow.FindControl("Prev_Demand_InPkt_F") as Label);

                //    if (lblPrev_Demand_InPkt.Text != "")
                //    {
                //        PrvD_InPkt += Convert.ToDecimal(lblPrev_Demand_InPkt.Text);
                //        Prev_Demand_InPkt_F.Text = PrvD_InPkt.ToString("0.00");
                //    }


                //    Label lblPrev_DemandInLtr = (Label)row.FindControl("lblPrev_DemandInLtr");
                //    Label lblPrev_DemandInLtr_F = (gvmttos.FooterRow.FindControl("lblPrev_DemandInLtr_F") as Label);


                //    if (lblPrev_DemandInLtr.Text != "")
                //    {
                //        PrvD_InLtr += Convert.ToDecimal(lblPrev_DemandInLtr.Text);
                //        lblPrev_DemandInLtr_F.Text = PrvD_InLtr.ToString("0.00");
                //    }

                //    Label lblCurrent_Demand_InPkt = (Label)row.FindControl("lblCurrent_Demand_InPkt");
                //    Label lblCurrent_Demand_InPkt_F = (gvmttos.FooterRow.FindControl("lblCurrent_Demand_InPkt_F") as Label);


                //    if (lblCurrent_Demand_InPkt.Text != "")
                //    {
                //        CurD_InPkt += Convert.ToDecimal(lblCurrent_Demand_InPkt.Text);
                //        lblCurrent_Demand_InPkt_F.Text = CurD_InPkt.ToString("0.00");
                //    }

                //    Label lblCurrent_Demand_InLtr = (Label)row.FindControl("lblCurrent_Demand_InLtr");
                //    Label lblCurrent_Demand_InLtr_F = (gvmttos.FooterRow.FindControl("lblCurrent_Demand_InLtr_F") as Label);


                //    if (lblCurrent_Demand_InLtr.Text != "")
                //    {
                //        CurD_InLtr += Convert.ToDecimal(lblCurrent_Demand_InLtr.Text);
                //        lblCurrent_Demand_InLtr_F.Text = CurD_InLtr.ToString("0.00");
                //    }

                //}


            }
            else
            {
                gvmttos.DataSource = string.Empty;
                gvmttos.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    protected void btnCrossButton_Click(object sender, EventArgs e)
    {
        ddlPSection_SelectedIndexChanged(sender, e);
    }

    protected void lnkbtnVN_Click(object sender, EventArgs e)
    {

        try
        {
            lblPopupMsg.Text = "";
			btnPopupSave_Product.Text = "Save";
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lnkbtnVN = (LinkButton)gvmttos.Rows[selRowIndex].FindControl("lnkbtnVN");
            Label lblFat = (Label)gvmttos.Rows[selRowIndex].FindControl("lblFat");
            Label lblSnf = (Label)gvmttos.Rows[selRowIndex].FindControl("lblSnf");
            Label lblWMFat = (Label)gvmttos.Rows[selRowIndex].FindControl("lblWMFat");
            Label lblWMSnf = (Label)gvmttos.Rows[selRowIndex].FindControl("lblWMSnf");
            Label lblItemRatioPer = (Label)gvmttos.Rows[selRowIndex].FindControl("lblItemRatioPer");
            Label lblCalculationMethod = (Label)gvmttos.Rows[selRowIndex].FindControl("lblCalculationMethod");
            ViewState["TypeId"] = lnkbtnVN.CommandArgument;
            ViewState["Fat"] = lblFat.Text;
            ViewState["Snf"] = lblSnf.Text;
            ViewState["WMFat"] = lblWMFat.Text;
            ViewState["WMSnf"] = lblWMSnf.Text;
            ViewState["ItemRatioPer"] = lblItemRatioPer.Text;
            ViewState["CalculationMethod"] = lblCalculationMethod.Text;
            lblProductName.Text = lnkbtnVN.Text;
			lbldate.Text = txtDate.Text; 
            lblsection.Text = ddlPSection.SelectedItem.Text;
			
			
            lblProductNameInner.Text = lnkbtnVN.Text;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);

            // For Variant

            DataSet dsVD_Child = objdb.ByProcedure("Usp_Production_ProductIPSheetMaster",
            new string[] { "flag", "Office_ID", "Date", "ProductSection_ID", "ItemType_id" },
            new string[] { "2", objdb.Office_ID(), Fdate, ddlPSection.SelectedValue, lnkbtnVN.CommandArgument }, "dataset");

            if (dsVD_Child != null && dsVD_Child.Tables[0].Rows.Count > 0)
            {
                GVVariantDetail_In.DataSource = dsVD_Child;
                GVVariantDetail_In.DataBind();
               
            }

            else
            {
                GVVariantDetail_In.DataSource = string.Empty;
                GVVariantDetail_In.DataBind();
              
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);

        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }

    private DataTable GetProductVarientInfo()
    {

        DataTable dt = new DataTable();
       
        dt.Columns.Add("Item_id", typeof(int));
        dt.Columns.Add("OpeningBalance", typeof(decimal));
        dt.Columns.Add("Return", typeof(decimal));
        dt.Columns.Add("Manufactured", typeof(decimal));
        dt.Columns.Add("Sample", typeof(decimal));
        dt.Columns.Add("Dispatch", typeof(decimal));
        dt.Columns.Add("Closing", typeof(decimal));
		dt.Columns.Add("PurchasedFrom", typeof(decimal));
        dt.Columns.Add("PurchasedFromDS_Id", typeof(decimal));
        foreach(GridViewRow rows in GVVariantDetail_In.Rows)
        {
            Label lblItem_id = (Label)rows.FindControl("lblItem_id");
            TextBox txtOpeningBalance = (TextBox)rows.FindControl("txtOpeningBalance");
            
            TextBox txtReturn = (TextBox)rows.FindControl("txtReturn");
            TextBox txtManufactured = (TextBox)rows.FindControl("txtManufactured");
			TextBox txtPurchasedFrom = (TextBox)rows.FindControl("txtPurchasedFrom");
            TextBox txtSample = (TextBox)rows.FindControl("txtSample");
            TextBox txtDispatch = (TextBox)rows.FindControl("txtDispatch");
            TextBox txtClosingBalance = (TextBox)rows.FindControl("txtClosingBalance");

            dt.Rows.Add(lblItem_id.Text, txtOpeningBalance.Text, txtReturn.Text, txtManufactured.Text, txtSample.Text, txtDispatch.Text, txtClosingBalance.Text,txtPurchasedFrom.Text,ddlDSPurchasedfrom.SelectedValue);
        }
        return dt;
    }
    private DataTable GetFirstStageManu()
    {

        DataTable dt = new DataTable();

        dt.Columns.Add("ItemType_id", typeof(int));
        dt.Columns.Add("Item_id", typeof(int));
        dt.Columns.Add("Qty", typeof(decimal));
        dt.Columns.Add("FatKg", typeof(decimal));
        dt.Columns.Add("SnfKg", typeof(decimal));
        decimal QtyKg = 0;
        decimal FatKg = 0;
        decimal SnfKg = 0;
        foreach (GridViewRow rows in GVVariantDetail_In.Rows)
        {

            Label lblItem_id = (Label)rows.FindControl("lblItem_id");
            Label UQCCode = (Label)rows.FindControl("lblUQCCode");
            Label PackagingSize = (Label)rows.FindControl("lblPackagingSize");
            
            TextBox txtManufactured = (TextBox)rows.FindControl("txtManufactured");
            if(UQCCode.Text == "GM")
            {
                QtyKg = (decimal.Parse(txtManufactured.Text) * decimal.Parse(PackagingSize.Text) / 1000);
            }
            else if (UQCCode.Text == "KG")
            {
                QtyKg = (decimal.Parse(txtManufactured.Text) * decimal.Parse(PackagingSize.Text));
            }
            else if(UQCCode.Text == "ML")
            {
                QtyKg = (decimal.Parse(txtManufactured.Text) * decimal.Parse(PackagingSize.Text) / 1000);
            }
            else if (UQCCode.Text == "LTR")
            {
                QtyKg = (decimal.Parse(txtManufactured.Text) * decimal.Parse(PackagingSize.Text));
            }
            if (ViewState["TypeId"].ToString() == "13" || ViewState["TypeId"].ToString() == "164")
            {
                DataSet dsChakka = objdb.ByProcedure("Usp_Production_ProductIPSheetMaster", new string[] { "flag", "ItemType_id", "Office_ID" }, new string[] { "7", "170", objdb.Office_ID() }, "dataset");
                if (dsChakka != null && dsChakka.Tables.Count > 0)
                {
                    if (dsChakka.Tables[0].Rows.Count > 0)
                    {
                        QtyKg = (QtyKg * 60) / 100;
                        FatKg = (QtyKg * decimal.Parse(dsChakka.Tables[0].Rows[0]["Fat"].ToString())) / 100;
                        SnfKg = (QtyKg * decimal.Parse(dsChakka.Tables[0].Rows[0]["Snf"].ToString())) / 100;
                        dt.Rows.Add("170",lblItem_id.Text, QtyKg, FatKg, SnfKg);
                        

                    }
                }
            }
            else
            {
                FatKg = (QtyKg * decimal.Parse(ViewState["Fat"].ToString())) / 100;
                SnfKg = (QtyKg * decimal.Parse(ViewState["Snf"].ToString())) / 100;
                dt.Rows.Add(ViewState["TypeId"].ToString(),lblItem_id.Text, QtyKg, FatKg, SnfKg);
            }
            
        }
        
        
        
        return dt;
    }
    private DataTable GetSecondStageManu()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("ItemType_id", typeof(int));
        dt.Columns.Add("Item_id", typeof(int));
        dt.Columns.Add("Qty", typeof(decimal));
        dt.Columns.Add("FatKg", typeof(decimal));
        dt.Columns.Add("SnfKg", typeof(decimal));
        if (ViewState["TypeId"].ToString() == "13" || ViewState["TypeId"].ToString() == "164")
        {
            decimal QtyKg = 0;
            decimal FatKg = 0;
            decimal SnfKg = 0;
            foreach (GridViewRow rows in GVVariantDetail_In.Rows)
            {

                Label lblItem_id = (Label)rows.FindControl("lblItem_id");
                Label UQCCode = (Label)rows.FindControl("lblUQCCode");
                Label PackagingSize = (Label)rows.FindControl("lblPackagingSize");

                TextBox txtManufactured = (TextBox)rows.FindControl("txtManufactured");
                if (UQCCode.Text == "GM")
                {
                    QtyKg = (decimal.Parse(txtManufactured.Text) * decimal.Parse(PackagingSize.Text) / 1000);
                }
                else if (UQCCode.Text == "KG")
                {
                    QtyKg = (decimal.Parse(txtManufactured.Text) * decimal.Parse(PackagingSize.Text));
                }
                else if (UQCCode.Text == "ML")
                {
                    QtyKg = (decimal.Parse(txtManufactured.Text) * decimal.Parse(PackagingSize.Text) / 1000);
                }
                else if (UQCCode.Text == "LTR")
                {
                    QtyKg = (decimal.Parse(txtManufactured.Text) * decimal.Parse(PackagingSize.Text));
                }
                FatKg = (QtyKg * decimal.Parse(ViewState["Fat"].ToString())) / 100;
                SnfKg = (QtyKg * decimal.Parse(ViewState["Snf"].ToString())) / 100;
                dt.Rows.Add(ViewState["TypeId"].ToString(), lblItem_id.Text, QtyKg, FatKg, SnfKg);

            }

           
        }
       
        
       

        return dt;
    }
    private DataTable GetMilkRequired()
    {

        DataTable dt = new DataTable();

        dt.Columns.Add("ItemType_id", typeof(int));
        dt.Columns.Add("Item_id", typeof(int));
        dt.Columns.Add("Qty", typeof(decimal));
        dt.Columns.Add("FatKg", typeof(decimal));
        dt.Columns.Add("SnfKg", typeof(decimal));
        decimal QtyKg = 0;
        decimal FatKg = 0;
        decimal SnfKg = 0;
        foreach (GridViewRow rows in GVVariantDetail_In.Rows)
        {
            Label lblItem_id = (Label)rows.FindControl("lblItem_id");
            Label UQCCode = (Label)rows.FindControl("lblUQCCode");
            Label PackagingSize = (Label)rows.FindControl("lblPackagingSize");

            TextBox txtManufactured = (TextBox)rows.FindControl("txtManufactured");
            if (UQCCode.Text == "GM")
            {
                QtyKg = (decimal.Parse(txtManufactured.Text) * decimal.Parse(PackagingSize.Text) / 1000);
            }
            else if (UQCCode.Text == "KG")
            {
                QtyKg = (decimal.Parse(txtManufactured.Text) * decimal.Parse(PackagingSize.Text));
            }
            else if (UQCCode.Text == "ML")
            {
                QtyKg = (decimal.Parse(txtManufactured.Text) * decimal.Parse(PackagingSize.Text) / 1000);
            }
            else if (UQCCode.Text == "LTR")
            {
                QtyKg = (decimal.Parse(txtManufactured.Text) * decimal.Parse(PackagingSize.Text));
            }
			 if (ViewState["TypeId"].ToString() == "13" || ViewState["TypeId"].ToString() == "164")
                {
                    QtyKg = (QtyKg * 60) / 100;
                }
            if (ViewState["CalculationMethod"].ToString() == "Percentage")
            {
                QtyKg = (QtyKg * decimal.Parse(ViewState["ItemRatioPer"].ToString())) / 100;
            }

            else
            {
                if (ViewState["ItemRatioPer"].ToString().Contains("/"))
                {
                    string[] arr = ViewState["ItemRatioPer"].ToString().Split('/');
                    {
                        QtyKg = (QtyKg * decimal.Parse(arr[0])) / decimal.Parse(arr[1]);
                    }
                }
                else
                {
                    QtyKg = (QtyKg * (decimal.Parse(ViewState["ItemRatioPer"].ToString())));
                }

            }

            FatKg = (QtyKg * decimal.Parse(ViewState["WMFat"].ToString())) / 100;
            SnfKg = (QtyKg * decimal.Parse(ViewState["WMSnf"].ToString())) / 100;
            dt.Rows.Add(ViewState["TypeId"].ToString(),lblItem_id.Text, QtyKg, FatKg, SnfKg);
        }
        
        return dt;
    }
	private DataTable GetPackingMaterialAccInfo()
    {

        DataTable dt = new DataTable();

        dt.Columns.Add("Item_id", typeof(int));
        dt.Columns.Add("OB_Cup_Cone_Duplex", typeof(int));
        dt.Columns.Add("OB_Lead", typeof(int));
        dt.Columns.Add("OB_Stick", typeof(int));
        dt.Columns.Add("OB_CBox", typeof(int));
        dt.Columns.Add("RFS_Cup_Cone_Duplex", typeof(int));
        dt.Columns.Add("RFS_Lead", typeof(int));
        dt.Columns.Add("RFS_Stick", typeof(int));
        dt.Columns.Add("RFS_CBox", typeof(int));
        dt.Columns.Add("UFP_Cup_Cone_Duplex", typeof(int));
        dt.Columns.Add("UFP_Lead", typeof(int));
        dt.Columns.Add("UFP_Stick", typeof(int));
        dt.Columns.Add("UFP_CBox", typeof(int));
        dt.Columns.Add("DM_Cup_Cone_Duplex", typeof(int));
        dt.Columns.Add("DM_Lead", typeof(int));
        dt.Columns.Add("DM_Stick", typeof(int));
        dt.Columns.Add("DM_CBox", typeof(int));
        dt.Columns.Add("Return_Cup_Cone_Duplex", typeof(int));
        dt.Columns.Add("Return_Lead", typeof(int));
        dt.Columns.Add("Return_Stick", typeof(int));
        dt.Columns.Add("Return_CBox", typeof(int));
        dt.Columns.Add("CB_Cup_Cone_Duplex", typeof(int));
        dt.Columns.Add("CB_Lead", typeof(int));
        dt.Columns.Add("CB_Stick", typeof(int));
        dt.Columns.Add("CB_CBox", typeof(int));
        foreach (GridViewRow rows in gvPackingMatAccount.Rows)
        {
            Label lblItem_id = (Label)rows.FindControl("lblItem_id");
            
            TextBox txtOB_Cup_Cone_Duplex = (TextBox)rows.FindControl("txtOB_Cup_Cone_Duplex");
            TextBox txtOB_Lead = (TextBox)rows.FindControl("txtOB_Lead");
            TextBox txtOB_Stick = (TextBox)rows.FindControl("txtOB_Stick");
            TextBox txtOB_CBox = (TextBox)rows.FindControl("txtOB_CBox");

            TextBox txtRFS_Cup_Cone_Duplex = (TextBox)rows.FindControl("txtRFS_Cup_Cone_Duplex");
            TextBox txtRFS_Lead = (TextBox)rows.FindControl("txtRFS_Lead");
            TextBox txtRFS_Stick = (TextBox)rows.FindControl("txtRFS_Stick");
            TextBox txtRFS_CBox = (TextBox)rows.FindControl("txtRFS_CBox");

            TextBox txtUFP_Cup_Cone_Duplex = (TextBox)rows.FindControl("txtUFP_Cup_Cone_Duplex");
            TextBox txtUFP_Lead = (TextBox)rows.FindControl("txtUFP_Lead");
            TextBox txtUFP_Stick = (TextBox)rows.FindControl("txtUFP_Stick");
            TextBox txtUFP_CBox = (TextBox)rows.FindControl("txtUFP_CBox");

            TextBox txtDM_Cup_Cone_Duplex = (TextBox)rows.FindControl("txtDM_Cup_Cone_Duplex");
            TextBox txtDM_Lead = (TextBox)rows.FindControl("txtDM_Lead");
            TextBox txtDM_Stick = (TextBox)rows.FindControl("txtDM_Stick");
            TextBox txtDM_CBox = (TextBox)rows.FindControl("txtDM_CBox");

            TextBox txtReturn_Cup_Cone_Duplex = (TextBox)rows.FindControl("txtReturn_Cup_Cone_Duplex");
            TextBox txtReturn_Lead = (TextBox)rows.FindControl("txtReturn_Lead");
            TextBox txtReturn_Stick = (TextBox)rows.FindControl("txtReturn_Stick");
            TextBox txtReturn_CBox = (TextBox)rows.FindControl("txtReturn_CBox");

            TextBox txtCB_Cup_Cone_Duplex = (TextBox)rows.FindControl("txtCB_Cup_Cone_Duplex");
            TextBox txtCB_Lead = (TextBox)rows.FindControl("txtCB_Lead");
            TextBox txtCB_Stick = (TextBox)rows.FindControl("txtCB_Stick");
            TextBox txtCB_CBox = (TextBox)rows.FindControl("txtCB_CBox");

            if (txtOB_Cup_Cone_Duplex.Text == "")
            {
                txtOB_Cup_Cone_Duplex.Text = "0";
            }
            if (txtOB_Lead.Text == "")
            {
                txtOB_Lead.Text = "0";
            }
            if (txtOB_Stick.Text == "")
            {
                txtOB_Stick.Text = "0";
            }
            if (txtOB_CBox.Text == "")
            {
                txtOB_CBox.Text = "0";
            }
            if (txtRFS_Cup_Cone_Duplex.Text == "")
            {
                txtRFS_Cup_Cone_Duplex.Text = "0";
            }
            if (txtRFS_Lead.Text == "")
            {
                txtRFS_Lead.Text = "0";
            }
            if (txtRFS_Stick.Text == "")
            {
                txtRFS_Stick.Text = "0";
            }
            if (txtRFS_CBox.Text == "")
            {
                txtRFS_CBox.Text = "0";
            }
            if (txtUFP_Cup_Cone_Duplex.Text == "")
            {
                txtUFP_Cup_Cone_Duplex.Text = "0";
            }
            if (txtUFP_Lead.Text == "")
            {
                txtUFP_Lead.Text = "0";
            }
            if (txtUFP_Stick.Text == "")
            {
                txtUFP_Stick.Text = "0";
            }
            if (txtUFP_CBox.Text == "")
            {
                txtUFP_CBox.Text = "0";
            }
            if (txtDM_Cup_Cone_Duplex.Text == "")
            {
                txtDM_Cup_Cone_Duplex.Text = "0";
            }
            if (txtDM_Lead.Text == "")
            {
                txtDM_Lead.Text = "0";
            }
            if (txtDM_Stick.Text == "")
            {
                txtDM_Stick.Text = "0";
            }
            if (txtDM_CBox.Text == "")
            {
                txtDM_CBox.Text = "0";
            }
            if (txtReturn_Cup_Cone_Duplex.Text == "")
            {
                txtReturn_Cup_Cone_Duplex.Text = "0";
            }
            if (txtReturn_Lead.Text == "")
            {
                txtReturn_Lead.Text = "0";
            }
            if (txtReturn_Stick.Text == "")
            {
                txtReturn_Stick.Text = "0";
            }
            if (txtReturn_CBox.Text == "")
            {
                txtReturn_CBox.Text = "0";
            }
            if (txtCB_Cup_Cone_Duplex.Text == "")
            {
                txtCB_Cup_Cone_Duplex.Text = "0";
            }
            if (txtCB_Lead.Text == "")
            {
                txtCB_Lead.Text = "0";
            }
            if (txtCB_Stick.Text == "")
            {
                txtCB_Stick.Text = "0";
            }
            if (txtCB_CBox.Text == "")
            {
                txtCB_CBox.Text = "0";
            }
            dt.Rows.Add(lblItem_id.Text, 
                        txtOB_Cup_Cone_Duplex.Text, 
                        txtOB_Lead.Text, 
                        txtOB_Stick.Text, 
                        txtOB_CBox.Text, 
                        txtRFS_Cup_Cone_Duplex.Text,
                        txtRFS_Lead.Text,
                        txtRFS_Stick.Text,
                        txtRFS_CBox.Text,
                        txtUFP_Cup_Cone_Duplex.Text,
                        txtUFP_Lead.Text,
                        txtUFP_Stick.Text,
                        txtUFP_CBox.Text,
                        txtDM_Cup_Cone_Duplex.Text,
                        txtDM_Lead.Text,
                        txtDM_Stick.Text,
                        txtDM_CBox.Text,
                        txtReturn_Cup_Cone_Duplex.Text,
                        txtReturn_Lead.Text,
                        txtReturn_Stick.Text,
                        txtReturn_CBox.Text,
                        txtCB_Cup_Cone_Duplex.Text,
                        txtCB_Lead.Text,
                        txtCB_Stick.Text,
                        txtCB_CBox.Text
                        );
        }
        return dt;
    }
    protected void btnYesT_P_Click(object sender, EventArgs e)
    {

        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                DataTable dtIDF = new DataTable();
                dtIDF = GetProductVarientInfo();

                DataTable dtFSM = new DataTable();
                dtFSM = GetFirstStageManu();


                DataTable dtSSM = new DataTable();
                dtSSM = GetSecondStageManu();

                DataTable dtMR = new DataTable();
                dtMR = GetMilkRequired();

               

                if (dtIDF.Rows.Count > 0)
                {
                    if(btnPopupSave_Product.Text == "Save")
                    {
                        ds = null;
                        ds = objdb.ByProcedure("Usp_Production_ProductIPSheetMaster",
                                                  new string[] { "flag" 
                                                ,"Office_ID"
                                                ,"Date" 
                                                ,"ProductSection_ID"
                                                ,"CreatedBy"
                                                ,"CreatedBy_IP"
                                                ,"ItemType_id" 
                                                ,"ItemCat_id"
                                    },
                                                  new string[] { "3"
                                              ,objdb.Office_ID()
                                              , Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                                               ,ddlPSection.SelectedValue                                              
                                              , ViewState["Emp_ID"].ToString()
                                              ,objdb.GetLocalIPAddress()
                                              ,ViewState["TypeId"].ToString()
                                              ,"2"
                                    },
                                                 new string[] { "type_Production_ProductIPSheetMasterChild", "type_Production_ProductIPFirstStageManu", "type_Production_ProductIPSecondStageManu", "type_Production_ProductIPMilkRequired" },
                                                 new DataTable[] { dtIDF, dtFSM,dtSSM, dtMR }, "TableSave");


                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblPopupMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
                            

                        }

                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Already Exists")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + success);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
                        }

                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
                        }
                    }
                    else if(btnPopupSave_Product.Text == "Update")
                    {
                        ds = null;
                        ds = objdb.ByProcedure("Usp_Production_ProductIPSheetMaster",
                                                  new string[] { "flag" 
                                                ,"PPIPSM"
                                                ,"CreatedBy"
                                                ,"CreatedBy_IP"
                                                ,"ItemType_id" 
                                                ,"ItemCat_id"

                                    },
                                                  new string[] { "6"
                                              ,ViewState["PPIPSM"].ToString()                                         
                                              , ViewState["Emp_ID"].ToString()
                                              ,objdb.GetLocalIPAddress()   
                                              ,ViewState["TypeId"].ToString()
                                              ,"2"
                                    },
                                                 new string[] { "type_Production_ProductIPSheetMasterChild", "type_Production_ProductIPFirstStageManu", "type_Production_ProductIPSecondStageManu", "type_Production_ProductIPMilkRequired" },
                                                 new DataTable[] { dtIDF, dtFSM,dtSSM, dtMR }, "TableSave");


                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblPopupMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);

                        }

                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Already Exists")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + success);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
                        }

                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
                        }
                    }

                }
                else
                {
                    lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "variant Value Can't Empty");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
                }


                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);

            }
        }

        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
        }


    }

    //protected void txtPrepared_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblPopupMsg.Text = "";

    //        foreach (GridViewRow row in GVVariantDetail_In.Rows)
    //        {
    //            Label lblBalance_BF = (Label)row.FindControl("lblBalance_BF");
    //            TextBox txtPrepared = (TextBox)row.FindControl("txtPrepared");
    //            TextBox txtReturn = (TextBox)row.FindControl("txtReturn");

    //            if (lblBalance_BF.Text == "")
    //            {
    //                lblBalance_BF.Text = "0";
    //            }
    //            if (txtPrepared.Text == "")
    //            {
    //                txtPrepared.Text = "0";
    //            }
    //            if (txtReturn.Text == "")
    //            {
    //                txtReturn.Text = "0";
    //            }
    //            TextBox lblIntotal = (TextBox)row.FindControl("lblIntotal");
    //            lblIntotal.Text = (Convert.ToDecimal(lblBalance_BF.Text) + Convert.ToDecimal(txtPrepared.Text) + Convert.ToDecimal(txtReturn.Text)).ToString();
    //            //txtReturn.Focus();

    //        }

    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
    //    }

    //}

    //protected void txtReturn_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblPopupMsg.Text = "";

    //        foreach (GridViewRow row in GVVariantDetail_In.Rows)
    //        {
    //            Label lblBalance_BF = (Label)row.FindControl("lblBalance_BF");
    //            TextBox txtPrepared = (TextBox)row.FindControl("txtPrepared");
    //            TextBox txtReturn = (TextBox)row.FindControl("txtReturn");

    //            if (lblBalance_BF.Text == "")
    //            {
    //                lblBalance_BF.Text = "0";
    //            }
    //            if (txtPrepared.Text == "")
    //            {
    //                txtPrepared.Text = "0";
    //            }
    //            if (txtReturn.Text == "")
    //            {
    //                txtReturn.Text = "0";
    //            }
    //            TextBox lblIntotal = (TextBox)row.FindControl("lblIntotal");
    //            lblIntotal.Text = (Convert.ToDecimal(lblBalance_BF.Text) + Convert.ToDecimal(txtPrepared.Text) + Convert.ToDecimal(txtReturn.Text)).ToString();
    //        }

    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
    //    }
    //}

    //protected void txtSale_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblPopupMsg.Text = "";

    //        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
    //        {
    //            TextBox txtSale = (TextBox)row1.FindControl("txtSale");
    //            TextBox txtTesting = (TextBox)row1.FindControl("txtTesting");
    //            TextBox txtDiscarded = (TextBox)row1.FindControl("txtDiscarded");
    //            TextBox txtCLClosing = (TextBox)row1.FindControl("txtCLClosing");

    //            if (txtSale.Text == "")
    //            {
    //                txtSale.Text = "0";
    //            }

    //            if (txtTesting.Text == "")
    //            {
    //                txtTesting.Text = "0";
    //            }

    //            if (txtDiscarded.Text == "")
    //            {
    //                txtDiscarded.Text = "0";
    //            }

    //            if (txtCLClosing.Text == "")
    //            {
    //                txtCLClosing.Text = "0";
    //            }

    //            TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");
    //            lblouttotal.Text = (Convert.ToDecimal(txtSale.Text) + Convert.ToDecimal(txtTesting.Text) + Convert.ToDecimal(txtDiscarded.Text) + Convert.ToDecimal(txtCLClosing.Text)).ToString();
    //            //txtTesting.Focus();

    //            if (lblouttotal.Text != "")
    //            {
    //                btnPopupSave_Product.Enabled = true;
    //            }

    //        }

    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
    //    }
    //}
    //protected void txtTesting_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblPopupMsg.Text = "";

    //        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
    //        {
    //            TextBox txtSale = (TextBox)row1.FindControl("txtSale");
    //            TextBox txtTesting = (TextBox)row1.FindControl("txtTesting");
    //            TextBox txtDiscarded = (TextBox)row1.FindControl("txtDiscarded");
    //            TextBox txtCLClosing = (TextBox)row1.FindControl("txtCLClosing");

    //            if (txtSale.Text == "")
    //            {
    //                txtSale.Text = "0";
    //            }

    //            if (txtTesting.Text == "")
    //            {
    //                txtTesting.Text = "0";
    //            }

    //            if (txtDiscarded.Text == "")
    //            {
    //                txtDiscarded.Text = "0";
    //            }

    //            if (txtCLClosing.Text == "")
    //            {
    //                txtCLClosing.Text = "0";
    //            }

    //            TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");
    //            lblouttotal.Text = (Convert.ToDecimal(txtSale.Text) + Convert.ToDecimal(txtTesting.Text) + Convert.ToDecimal(txtDiscarded.Text) + Convert.ToDecimal(txtCLClosing.Text)).ToString();
    //            // txtDiscarded.Focus();
    //            if (lblouttotal.Text != "")
    //            {
    //                btnPopupSave_Product.Enabled = true;
    //            }
    //        }

    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
    //    }
    //}
    //protected void txtDiscarded_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblPopupMsg.Text = "";

    //        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
    //        {
    //            TextBox txtSale = (TextBox)row1.FindControl("txtSale");
    //            TextBox txtTesting = (TextBox)row1.FindControl("txtTesting");
    //            TextBox txtDiscarded = (TextBox)row1.FindControl("txtDiscarded");
    //            TextBox txtCLClosing = (TextBox)row1.FindControl("txtCLClosing");

    //            if (txtSale.Text == "")
    //            {
    //                txtSale.Text = "0";
    //            }

    //            if (txtTesting.Text == "")
    //            {
    //                txtTesting.Text = "0";
    //            }

    //            if (txtDiscarded.Text == "")
    //            {
    //                txtDiscarded.Text = "0";
    //            }

    //            if (txtCLClosing.Text == "")
    //            {
    //                txtCLClosing.Text = "0";
    //            }

    //            TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");
    //            lblouttotal.Text = (Convert.ToDecimal(txtSale.Text) + Convert.ToDecimal(txtTesting.Text) + Convert.ToDecimal(txtDiscarded.Text) + Convert.ToDecimal(txtCLClosing.Text)).ToString();
    //            //txtCLClosing.Focus();
    //            if (lblouttotal.Text != "")
    //            {
    //                btnPopupSave_Product.Enabled = true;
    //            }
    //        }

    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
    //    }
    //}
    //protected void txtCLClosing_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblPopupMsg.Text = "";

    //        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
    //        {
    //            TextBox txtSale = (TextBox)row1.FindControl("txtSale");
    //            TextBox txtTesting = (TextBox)row1.FindControl("txtTesting");
    //            TextBox txtDiscarded = (TextBox)row1.FindControl("txtDiscarded");
    //            TextBox txtCLClosing = (TextBox)row1.FindControl("txtCLClosing");

    //            if (txtSale.Text == "")
    //            {
    //                txtSale.Text = "0";
    //            }

    //            if (txtTesting.Text == "")
    //            {
    //                txtTesting.Text = "0";
    //            }

    //            if (txtDiscarded.Text == "")
    //            {
    //                txtDiscarded.Text = "0";
    //            }

    //            if (txtCLClosing.Text == "")
    //            {
    //                txtCLClosing.Text = "0";
    //            }

    //            TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");
    //            lblouttotal.Text = (Convert.ToDecimal(txtSale.Text) + Convert.ToDecimal(txtTesting.Text) + Convert.ToDecimal(txtDiscarded.Text) + Convert.ToDecimal(txtCLClosing.Text)).ToString();
    //            if (lblouttotal.Text != "")
    //            {
    //                btnPopupSave_Product.Enabled = true;
    //            }
    //        }

    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
    //    }
    //}




    protected void lbviewsheet_Click(object sender, EventArgs e)
    {
        try
        {
            lblPopupMsg.Text = "";
			ddlDSPurchasedfrom.ClearSelection();
			btnPopupSave_Product.Text = "Save";
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lnkbtnVN = (LinkButton)gvmttos.Rows[selRowIndex].FindControl("lnkbtnVN");
            LinkButton lbviewsheet = (LinkButton)gvmttos.Rows[selRowIndex].FindControl("lbviewsheet");
            Label lblFat = (Label)gvmttos.Rows[selRowIndex].FindControl("lblFat");
            Label lblSnf = (Label)gvmttos.Rows[selRowIndex].FindControl("lblSnf");
            Label lblWMFat = (Label)gvmttos.Rows[selRowIndex].FindControl("lblWMFat");
            Label lblWMSnf = (Label)gvmttos.Rows[selRowIndex].FindControl("lblWMSnf");
            Label lblItemRatioPer = (Label)gvmttos.Rows[selRowIndex].FindControl("lblItemRatioPer");
             Label lblCalculationMethod = (Label)gvmttos.Rows[selRowIndex].FindControl("lblCalculationMethod");
            ViewState["TypeId"] = lnkbtnVN.CommandArgument;
            ViewState["Fat"] = lblFat.Text;
            ViewState["Snf"] = lblSnf.Text;
            ViewState["WMFat"] = lblWMFat.Text;
            ViewState["WMSnf"] = lblWMSnf.Text;
            ViewState["ItemRatioPer"] = lblItemRatioPer.Text;
            ViewState["CalculationMethod"] = lblCalculationMethod.Text;
            ViewState["PPIPSM"] = lbviewsheet.CommandArgument;
            lblProductName.Text = lnkbtnVN.Text;
            lbldate.Text = txtDate.Text;
            lblsection.Text = ddlPSection.SelectedItem.Text;

            
            lblProductNameInner.Text = lnkbtnVN.Text;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);

            // For Variant

            DataSet dsVD_Child = objdb.ByProcedure("Usp_Production_ProductIPSheetMaster",
            new string[] { "flag", "Office_ID", "Date", "ProductSection_ID", "ItemType_id", "PPIPSM" },
            new string[] { "5", objdb.Office_ID(), Fdate, ddlPSection.SelectedValue, lnkbtnVN.CommandArgument, lbviewsheet.CommandArgument }, "dataset");

            if (dsVD_Child != null && dsVD_Child.Tables[0].Rows.Count > 0)
            {
                GVVariantDetail_In.DataSource = dsVD_Child;
                GVVariantDetail_In.DataBind();
                if (ViewState["PPIPSM"].ToString() != "0")
                {
                    btnPopupSave_Product.Text = "Update";
					ddlDSPurchasedfrom.SelectedValue = dsVD_Child.Tables[0].Rows[0]["PurchasedFromDS_Id"].ToString();
                }
               

            }

            else
            {
                GVVariantDetail_In.DataSource = string.Empty;
                GVVariantDetail_In.DataBind();
				btnPopupSave_Product.Text = "Save";

            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);

        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
	protected void gvPackingMatAccount_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);


            TableCell HeaderCell0 = new TableCell();
            HeaderCell0.Text = "Opening Balance";
            HeaderCell0.ColumnSpan = 4;
            HeaderGridRow.Cells.Add(HeaderCell0);

            TableCell HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Received from Store";
            HeaderCell1.ColumnSpan = 4;
            HeaderGridRow.Cells.Add(HeaderCell1);
           

            
            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Used for Packing";
            HeaderCell2.ColumnSpan = 4;
            HeaderGridRow.Cells.Add(HeaderCell2);
            

            
            TableCell HeaderCell3 = new TableCell();
            HeaderCell3.Text = "Damage / Loss";
            HeaderCell3.ColumnSpan = 4;
            HeaderGridRow.Cells.Add(HeaderCell3);
            

            
            TableCell HeaderCell4 = new TableCell();
            HeaderCell4.Text = "Return";
            HeaderCell4.ColumnSpan = 4;
            HeaderGridRow.Cells.Add(HeaderCell4);
           
            
            TableCell HeaderCell5 = new TableCell();
            HeaderCell5.Text = "Closing Balance";
            HeaderCell5.ColumnSpan = 4;
            HeaderGridRow.Cells.Add(HeaderCell5);


            gvPackingMatAccount.Controls[0].Controls.AddAt(0, HeaderGridRow);

           

            

        }
    }
	protected void lbviewsheet1_Click(object sender, EventArgs e)
    {
        try
        {
            
            lblPopupMsg.Text = "";
            btnPackingMatAccount.Text = "Save";
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lnkbtnVN = (LinkButton)gvmttos.Rows[selRowIndex].FindControl("lnkbtnVN");
            LinkButton lbviewsheet = (LinkButton)gvmttos.Rows[selRowIndex].FindControl("lbviewsheet");
            LinkButton lbviewsheet1 = (LinkButton)gvmttos.Rows[selRowIndex].FindControl("lbviewsheet1");
            
            ViewState["TypeId"] = lnkbtnVN.CommandArgument;
           
            ViewState["PPIPSM"] = lbviewsheet.CommandArgument;
            ViewState["PPIPPackMat_ID"] = lbviewsheet1.CommandArgument;
            Label4.Text = lnkbtnVN.Text;
            Label7.Text = txtDate.Text;
            Label8.Text = ddlPSection.SelectedItem.Text;


            Label11.Text = lnkbtnVN.Text + "    (Packing Material Account (in No's))";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModel_PackingAccount()", true);

            // For Variant

            DataSet dsVD_Child = objdb.ByProcedure("Usp_Production_ProductIPSheetMaster",
            new string[] { "flag", "Office_ID", "Date", "ProductSection_ID", "ItemType_id", "PPIPSM" },
            new string[] { "11", objdb.Office_ID(), Fdate, ddlPSection.SelectedValue, lnkbtnVN.CommandArgument, lbviewsheet.CommandArgument }, "dataset");

            if (dsVD_Child != null && dsVD_Child.Tables[0].Rows.Count > 0)
            {
                gvPackingMatAccount.DataSource = dsVD_Child;
                gvPackingMatAccount.DataBind();
                btnPackingMatAccount.Text = "Save";

            }

            else
            {
                gvPackingMatAccount.DataSource = string.Empty;
                gvPackingMatAccount.DataBind();

            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModel_PackingAccount()", true);

        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
        
    }
    protected void btnPackingMatAccount_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                
                DataTable dtPackMatAcnt = new DataTable();
                dtPackMatAcnt = GetPackingMaterialAccInfo();



                if (dtPackMatAcnt.Rows.Count > 0)
                {
                    if (btnPackingMatAccount.Text == "Save")
                    {
                        ds = null;
                        ds = objdb.ByProcedure("Usp_Production_ProductIPSheetMaster",
                                                  new string[] { "flag" 
                                                ,"PPIPSM"
                                                ,"ItemCat_id" 
                                                ,"ItemType_id"
                                    },
                                                  new string[] { "12"
                                              ,ViewState["PPIPSM"].ToString() 
                                              ,ViewState["TypeId"].ToString()
                                              ,"2"
                                    },
                                                 new string[] { "type_Production_ProductIPPackingMaterialAccount" },
                                                 new DataTable[] { dtPackMatAcnt }, "TableSave");


                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblPopupMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModel_PackingAccount()", true);


                        }

                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Already Exists")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + success);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModel_PackingAccount()", true);
                        }

                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModel_PackingAccount()", true);
                        }
                    }
                    else if (btnPopupSave_Product.Text == "Update")
                    {
                        ds = null;
                        //ds = objdb.ByProcedure("btnPackingMatAccount",
                        //                          new string[] { "flag" 
                        //                        ,""
                        //                        ,"PPIPSM"                 
                        //                        ,"ItemType_id" 
                        //                        ,"ItemCat_id"

                        //            },
                        //                          new string[] { "6"
                        //                      ,ViewState["PPIPSM"].ToString()                                         
                        //                      , ViewState["Emp_ID"].ToString()
                        //                      ,objdb.GetLocalIPAddress()   
                        //                      ,ViewState["TypeId"].ToString()
                        //                      ,"2"
                        //            },
                        //                         new string[] { "type_Production_ProductIPPackingMaterialAccount" },
                        //                         new DataTable[] { dtMR }, "TableSave");


                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblPopupMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModel_PackingAccount()", true);

                        }

                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Already Exists")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + success);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModel_PackingAccount()", true);
                        }

                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModel_PackingAccount()", true);
                        }
                    }

                }
                else
                {
                    lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "variant Value Can't Empty");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModel_PackingAccount()", true);
                }


                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModel_PackingAccount()", true);

            }
        }

        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModel_PackingAccount()", true);
        }

    }
}