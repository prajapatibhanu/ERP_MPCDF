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


public partial class mis_dailyplan_ProductButterSheetEntryNew : System.Web.UI.Page
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
                    txtDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                    ViewState["TotalQtyInflow"] = "";
                    ViewState["TotalQtyOutFlow"] = "";
                    ViewState["TotalFatInflow"] = "";
                    ViewState["TotalFatOutFlow"] = "";
                    ViewState["TotalSnfInflow"] = "";
                    ViewState["TotalSnfOutFlow"] = "";
                    GetSectionView(sender, e);
                    GetSectionDetail();
                    btnGetTotal_Click(sender, e);


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

    

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        if (ddlPSection.SelectedValue != "0")
        {
            GetSectionDetail();
            btnGetTotal_Click(sender, e);
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

    }

    private void GetSectionDetail()
    {

        try
        {
			 btnPopupSave_Product.Text = "Save";
            ViewState["ButterSheet_ID"] = "";
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            // For Variant

            DataSet dsVD_Child = objdb.ByProcedure("Sp_Production_ButterSheet",
            new string[] { "flag", "Office_ID", "Date", "ProductSection_ID", "ItemType_id" },
            new string[] { "1", objdb.Office_ID(), Fdate, ddlPSection.SelectedValue, objdb.LooseButterItemTypeId_ID() }, "dataset");

            if (dsVD_Child != null && dsVD_Child.Tables[0].Rows.Count > 0)
            {

                GVVariantDetail_In.DataSource = dsVD_Child;
                GVVariantDetail_In.DataBind();
                GVVariantDetail_Out.DataSource = dsVD_Child;
                GVVariantDetail_Out.DataBind();
                foreach (GridViewRow row in GVVariantDetail_In.Rows)
                {
                    Label lblButterSheet_ID = (Label)row.FindControl("lblButterSheet_ID");
                    if (lblButterSheet_ID.Text != "")
                    {
                        btnPopupSave_Product.Text = "Update";
                        ViewState["ButterSheet_ID"] = lblButterSheet_ID.Text;
                    }
                }
            }

            else
            {
                GVVariantDetail_In.DataSource = string.Empty;
                GVVariantDetail_In.DataBind();
                GVVariantDetail_Out.DataSource = string.Empty;
                GVVariantDetail_Out.DataBind();
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);

        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }

    protected void btnGetTotal_Click(object sender, EventArgs e)
    {
        try
        {
            decimal TotalQtyInflow = 0;
            decimal TotalQtyOutFlow = 0;
            decimal TotalFatInflow = 0;
            decimal TotalFatOutFlow = 0;
            decimal TotalSnfInflow = 0;
            decimal TotalSnfOutFlow = 0;
            
            
            foreach (GridViewRow row in GVVariantDetail_In.Rows)
            {
               
                Label lblOB_CC = (Label)row.FindControl("lblOB_CC");
                Label lblOB_Loose = (Label)row.FindControl("lblOB_Loose");
                Label lblItemName = (Label)row.FindControl("lblItemName");
                Label lblintotal = (Label)row.FindControl("lblintotal");
                Label lblOB_Cold_Room1 = (Label)row.FindControl("lblOB_Cold_Room1");
                Label lblOB_Cold_Room2 = (Label)row.FindControl("lblOB_Cold_Room2");
                TextBox txtWB_Mfg_Good = (TextBox)row.FindControl("txtWB_Mfg_Good");
                TextBox txtWB_Mfg_Sour = (TextBox)row.FindControl("txtWB_Mfg_Sour");
                TextBox txtRecieved_from_CC_1 = (TextBox)row.FindControl("txtRecieved_from_CC_1");
                TextBox txtRecieved_from_CC_2 = (TextBox)row.FindControl("txtRecieved_from_CC_2");
                TextBox txtRecieved_from_CC_3 = (TextBox)row.FindControl("txtRecieved_from_CC_3");
                TextBox txtRecieved_from_FP = (TextBox)row.FindControl("txtRecieved_from_FP");
                TextBox txtRecieved_from_others = (TextBox)row.FindControl("txtRecieved_from_others");


                if (lblOB_CC.Text == "")
                {
                    lblOB_CC.Text = "0";
                }
                if (lblOB_Loose.Text == "")
                {
                    lblOB_Loose.Text = "0";
                }
                if (lblOB_Cold_Room1.Text == "")
                {
                    lblOB_Cold_Room1.Text = "0";
                }
                if (lblOB_Cold_Room2.Text == "")
                {
                    lblOB_Cold_Room2.Text = "0";
                }
                if (txtWB_Mfg_Good.Text == "")
                {
                    txtWB_Mfg_Good.Text = "0";
                }
                if (txtWB_Mfg_Sour.Text == "")
                {
                    txtWB_Mfg_Sour.Text = "0";

                }
                if (txtRecieved_from_CC_1.Text == "")
                {
                    txtRecieved_from_CC_1.Text = "0";

                }
                if (txtRecieved_from_CC_2.Text == "")
                {
                    txtRecieved_from_CC_2.Text = "0";

                }
                if (txtRecieved_from_CC_3.Text == "")
                {
                    txtRecieved_from_CC_3.Text = "0";

                }
                if (txtRecieved_from_FP.Text == "")
                {
                    txtRecieved_from_FP.Text = "0";

                }
                if (txtRecieved_from_others.Text == "")
                {
                    txtRecieved_from_others.Text = "0";

                }

                lblintotal.Text = (Convert.ToDecimal(lblOB_CC.Text) + Convert.ToDecimal(lblOB_Loose.Text)
                               + Convert.ToDecimal(lblOB_Cold_Room1.Text) + Convert.ToDecimal(lblOB_Cold_Room2.Text)
                               + Convert.ToDecimal(txtWB_Mfg_Good.Text) + Convert.ToDecimal(txtWB_Mfg_Sour.Text) + Convert.ToDecimal(txtRecieved_from_CC_1.Text) + Convert.ToDecimal(txtRecieved_from_CC_2.Text) + Convert.ToDecimal(txtRecieved_from_CC_3.Text) + Convert.ToDecimal(txtRecieved_from_FP.Text) + Convert.ToDecimal(txtRecieved_from_others.Text)).ToString();
                if (lblintotal.Text == "")
                {
                    lblintotal.Text = "0";
                }
                if (lblItemName.Text == " Quantity In Kg")
                {
                    TotalQtyInflow = decimal.Parse(lblintotal.Text);
                    ViewState["TotalQtyInflow"] = TotalQtyInflow;
                }
                if (lblItemName.Text == " Fat In Kg")
                {
                    TotalFatInflow = decimal.Parse(lblintotal.Text);
                    ViewState["TotalFatInflow"] = TotalFatInflow;
                }
                if (lblItemName.Text == " Snf In Kg")
                {
                    TotalSnfInflow = decimal.Parse(lblintotal.Text);
                    ViewState["TotalSnfInflow"] = TotalSnfInflow;
                }

                if (lblintotal.Text != "")
                {
                    btnPopupSave_Product.Enabled = true;
                }
            }
            


            foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
            {
                Label Label2 = (Label)row1.FindControl("Label2");
                TextBox txtIssue_to_Processing = (TextBox)row1.FindControl("txtIssue_to_Processing");
                TextBox txtIssue_for_Ghee = (TextBox)row1.FindControl("txtIssue_for_Ghee");
                TextBox txtIssue_to_other = (TextBox)row1.FindControl("txtIssue_to_other");
                TextBox txtButter_Milk = (TextBox)row1.FindControl("txtButter_Milk");
                TextBox txtIssue_for_FP = (TextBox)row1.FindControl("txtIssue_for_FP");
                TextBox txtIssue_for_Sale = (TextBox)row1.FindControl("txtIssue_for_Sale");
                TextBox txtCB_CC = (TextBox)row1.FindControl("txtCB_CC");
                TextBox txtCB_Loose = (TextBox)row1.FindControl("txtCB_Loose");
                TextBox txtCB_Cold_Room1 = (TextBox)row1.FindControl("txtCB_Cold_Room1");
                TextBox txtCB_Cold_Room2 = (TextBox)row1.FindControl("txtCB_Cold_Room2");
                TextBox txtSample = (TextBox)row1.FindControl("txtSample");
                if (txtIssue_to_Processing.Text == "")
                {
                    txtIssue_to_Processing.Text = "0";
                }

                if (txtIssue_for_Ghee.Text == "")
                {
                    txtIssue_for_Ghee.Text = "0";
                }

                if (txtIssue_to_other.Text == "")
                {
                    txtIssue_to_other.Text = "0";
                }

                if (txtButter_Milk.Text == "")
                {
                    txtButter_Milk.Text = "0";
                }

                if (txtIssue_for_FP.Text == "")
                {
                    txtIssue_for_FP.Text = "0";
                }
                if (txtIssue_for_Sale.Text == "")
                {
                    txtIssue_for_Sale.Text = "0";
                }
                if (txtCB_CC.Text == "")
                {
                    txtCB_CC.Text = "0";
                }
                if (txtCB_Loose.Text == "")
                {
                    txtCB_Loose.Text = "0";
                }

                if (txtCB_Cold_Room1.Text == "")
                {
                    txtCB_Cold_Room1.Text = "0";
                }
                if (txtCB_Cold_Room2.Text == "")
                {
                    txtCB_Cold_Room2.Text = "0";
                }
                if (txtSample.Text == "")
                {
                    txtSample.Text = "0";
                }

                TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");


                lblouttotal.Text = (Convert.ToDecimal(txtIssue_to_Processing.Text) + Convert.ToDecimal(txtIssue_for_Ghee.Text)
                                   + Convert.ToDecimal(txtIssue_to_other.Text) + Convert.ToDecimal(txtButter_Milk.Text)
                                   + Convert.ToDecimal(txtIssue_for_FP.Text) + Convert.ToDecimal(txtIssue_for_Sale.Text) + Convert.ToDecimal(txtCB_CC.Text)
                                   + Convert.ToDecimal(txtCB_Loose.Text) + Convert.ToDecimal(txtCB_Cold_Room1.Text) + Convert.ToDecimal(txtCB_Cold_Room2.Text) + Convert.ToDecimal(txtSample.Text)).ToString();
               
                if (lblouttotal.Text == "")
                {
                    lblouttotal.Text = "0";
                }
                if (Label2.Text == " Quantity In Kg")
                {
                    TotalQtyOutFlow = decimal.Parse(lblouttotal.Text);
                    ViewState["TotalQtyOutFlow"] = TotalQtyOutFlow;
                }
                if (Label2.Text == " Fat In Kg")
                {
                    TotalFatOutFlow = decimal.Parse(lblouttotal.Text);
                    ViewState["TotalFatOutFlow"] = TotalFatOutFlow;
                }
                if (Label2.Text == " Snf In Kg")
                {
                    TotalSnfOutFlow = decimal.Parse(lblouttotal.Text);
                    ViewState["TotalSnfOutFlow"] = TotalSnfOutFlow;
                }
                
                if (lblouttotal.Text != "")
                {
                    btnPopupSave_Product.Enabled = true;

                }

                if (TotalQtyInflow == TotalQtyOutFlow && TotalFatInflow == TotalFatOutFlow && TotalSnfInflow == TotalSnfOutFlow)
                {
                    btnPopupSave_Product.Enabled = true;
                }
            }
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    private DataTable GetProductVarientInfo()
    {

        decimal OB_CC = 0;
        decimal OB_Loose = 0;
        decimal OB_Cold_Room1 = 0;
        decimal OB_Cold_Room2 = 0;
        decimal WB_Mfg_Good = 0;
        decimal WB_Mfg_Sour = 0;
        decimal Recieved_from_CC_1 = 0;
        decimal Recieved_from_CC_2 = 0;
        decimal Recieved_from_CC_3 = 0;
        decimal Recieved_from_FP = 0;
        decimal Recieved_from_others = 0;
        decimal Issue_to_Processing = 0;
        decimal Issue_for_Ghee = 0;

        decimal Butter_Milk = 0;
        decimal Issue_to_other = 0;
        decimal Issue_for_FP = 0;
        decimal Issue_for_Sale = 0;
        decimal CB_CC = 0;
        decimal CB_Loose = 0;
        decimal CB_Cold_Room1 = 0;
        decimal CB_Cold_Room2 = 0;
        decimal Sample = 0;
        decimal InTotal = 0;
        decimal OutTotal = 0;

        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("ItemCat_id", typeof(int)));
        dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
        dt.Columns.Add(new DataColumn("Item_id", typeof(int)));
        dt.Columns.Add(new DataColumn("OB_CC", typeof(decimal)));
        dt.Columns.Add(new DataColumn("OB_Loose", typeof(decimal)));
        dt.Columns.Add(new DataColumn("OB_Cold_Room1", typeof(decimal)));
        dt.Columns.Add(new DataColumn("OB_Cold_Room2", typeof(decimal)));
        dt.Columns.Add(new DataColumn("WB_Mfg_Good", typeof(decimal)));
        dt.Columns.Add(new DataColumn("WB_Mfg_Sour", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Recieved_from_CC_1", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Recieved_from_CC_2", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Recieved_from_CC_3", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Recieved_from_FP", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Recieved_from_others", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Issue_to_Processing", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Issue_for_Ghee", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Butter_Milk", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Issue_to_other", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Issue_for_FP", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Issue_for_Sale", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CB_CC", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CB_Loose", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CB_Cold_Room1", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CB_Cold_Room2", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Sample", typeof(decimal)));
      

        foreach (GridViewRow row in GVVariantDetail_In.Rows)
        {
            Label lblItem_id = (Label)row.FindControl("lblItem_id");

            Label lblOB_CC = (Label)row.FindControl("lblOB_CC");
            Label lblOB_Loose = (Label)row.FindControl("lblOB_Loose");
            Label lblOB_Cold_Room1 = (Label)row.FindControl("lblOB_Cold_Room1");
            Label lblOB_Cold_Room2 = (Label)row.FindControl("lblOB_Cold_Room2");
           // Label lblCreamrcvdforcookingbutter = (Label)row.FindControl("lblCreamrcvdforcookingbutter");
            TextBox txtWB_Mfg_Good = (TextBox)row.FindControl("txtWB_Mfg_Good");
            TextBox txtWB_Mfg_Sour = (TextBox)row.FindControl("txtWB_Mfg_Sour");
            TextBox txtRecieved_from_CC_1 = (TextBox)row.FindControl("txtRecieved_from_CC_1");
            TextBox txtRecieved_from_CC_2 = (TextBox)row.FindControl("txtRecieved_from_CC_2");
            TextBox txtRecieved_from_CC_3 = (TextBox)row.FindControl("txtRecieved_from_CC_3");
            TextBox txtRecieved_from_FP = (TextBox)row.FindControl("txtRecieved_from_FP");
            TextBox txtRecieved_from_others = (TextBox)row.FindControl("txtRecieved_from_others");



            if (lblOB_CC.Text != "" && lblOB_CC.Text != "0" && lblOB_CC.Text != "0.00")
            {
                OB_CC = Convert.ToDecimal(lblOB_CC.Text);
            }

            else
            {
                OB_CC = 0;
            }
            if (lblOB_Loose.Text != "" && lblOB_Loose.Text != "0" && lblOB_Loose.Text != "0.00")
            {
                OB_Loose = Convert.ToDecimal(lblOB_Loose.Text);
            }

            else
            {
                OB_Loose = 0;
            }
            if (lblOB_Cold_Room1.Text != "" && lblOB_Cold_Room1.Text != "0" && lblOB_Cold_Room1.Text != "0.00")
            {
                OB_Cold_Room1 = Convert.ToDecimal(lblOB_Cold_Room1.Text);
            }

            else
            {
                OB_Cold_Room1 = 0;
            }
            if (lblOB_Cold_Room2.Text != "" && lblOB_Cold_Room2.Text != "0" && lblOB_Cold_Room2.Text != "0.00")
            {
                OB_Cold_Room2 = Convert.ToDecimal(lblOB_Cold_Room2.Text);
            }

            else
            {
                OB_Cold_Room2 = 0;
            }
            if (txtWB_Mfg_Good.Text != "" && txtWB_Mfg_Good.Text != "0" && txtWB_Mfg_Good.Text != "0.00")
            {
                WB_Mfg_Good = Convert.ToDecimal(txtWB_Mfg_Good.Text);
            }

            else
            {
                WB_Mfg_Good = 0;
            }
            if (txtWB_Mfg_Sour.Text != "" && txtWB_Mfg_Sour.Text != "0" && txtWB_Mfg_Sour.Text != "0.00")
            {
                WB_Mfg_Sour = Convert.ToDecimal(txtWB_Mfg_Sour.Text);
            }

            else
            {
                WB_Mfg_Sour = 0;
            }
            if (txtRecieved_from_CC_1.Text != "" && txtRecieved_from_CC_1.Text != "0" && txtRecieved_from_CC_1.Text != "0.00")
            {
                Recieved_from_CC_1 = Convert.ToDecimal(txtRecieved_from_CC_1.Text);
            }

            else
            {
                Recieved_from_CC_1 = 0;
            }
            if (txtRecieved_from_CC_2.Text != "" && txtRecieved_from_CC_2.Text != "0" && txtRecieved_from_CC_2.Text != "0.00")
            {
                Recieved_from_CC_2 = Convert.ToDecimal(txtRecieved_from_CC_2.Text);
            }
            else
            {
                Recieved_from_CC_2 = 0;
            }
            if (txtRecieved_from_CC_3.Text != "" && txtRecieved_from_CC_3.Text != "0" && txtRecieved_from_CC_3.Text != "0.00")
            {
                Recieved_from_CC_3 = Convert.ToDecimal(txtRecieved_from_CC_3.Text);
            }
            else
            {
                Recieved_from_CC_3 = 0;
            }

            if (txtRecieved_from_FP.Text != "" && txtRecieved_from_FP.Text != "0" && txtRecieved_from_FP.Text != "0.00")
            {
                Recieved_from_FP = Convert.ToDecimal(txtRecieved_from_FP.Text);
            }
            else
            {
                Recieved_from_FP = 0;
            }
            if (txtRecieved_from_others.Text != "" && txtRecieved_from_others.Text != "0" && txtRecieved_from_others.Text != "0.00")
            {
                Recieved_from_others = Convert.ToDecimal(txtRecieved_from_others.Text);
            }
            else
            {
                Recieved_from_others = 0;
            }

            // InTotal = Convert.ToDecimal(lblCC_Opening.Text) + Convert.ToDecimal(lblLoose_Opening.Text) + Convert.ToDecimal(lblVE2_Opening.Text) + Convert.ToDecimal(lblVE1_Opening.Text);
            InTotal = Convert.ToDecimal(lblOB_CC.Text) + Convert.ToDecimal(lblOB_Loose.Text) + Convert.ToDecimal(lblOB_Cold_Room1.Text) + Convert.ToDecimal(lblOB_Cold_Room2.Text) + Convert.ToDecimal(txtWB_Mfg_Good.Text) + Convert.ToDecimal(txtWB_Mfg_Sour.Text) + Convert.ToDecimal(txtRecieved_from_CC_1.Text)
                      + Convert.ToDecimal(txtRecieved_from_CC_2.Text) + Convert.ToDecimal(txtRecieved_from_CC_3.Text) + Convert.ToDecimal(txtRecieved_from_FP.Text) + Convert.ToDecimal(txtRecieved_from_others.Text);
            dr = dt.NewRow();
            dr[0] = objdb.LooseButterItemCategoryId_ID();
            dr[1] = objdb.LooseButterItemTypeId_ID();
            dr[2] = lblItem_id.Text;
            dr[3] = OB_CC;
            dr[4] = OB_Loose;
            dr[5] = OB_Cold_Room1;
            dr[6] = OB_Cold_Room2;
            dr[7] = WB_Mfg_Good;
            dr[8] = WB_Mfg_Sour;
            dr[9] = Recieved_from_CC_1;
            dr[10] = Recieved_from_CC_2;
            dr[11] = Recieved_from_CC_3;
            dr[12] = Recieved_from_FP;
            dr[13] = Recieved_from_others;
            dr[14] = Issue_to_Processing;

            dr[15] = Issue_for_Ghee;
            dr[16] = Butter_Milk;
            dr[17] = Issue_to_other;
            dr[18] = Issue_for_FP;
            dr[19] = Issue_for_Sale;
            dr[20] = CB_CC;
            dr[21] = CB_Loose;
            dr[22] = CB_Cold_Room1;
            dr[23] = CB_Cold_Room2;
            dr[24] = Sample;
            
            dt.Rows.Add(dr);
        }

        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
        {
            Label lblItem_id = (Label)row1.FindControl("lblItem_id_Out");
            TextBox txtIssue_to_Processing = (TextBox)row1.FindControl("txtIssue_to_Processing");
            TextBox txtIssue_for_Ghee = (TextBox)row1.FindControl("txtIssue_for_Ghee");
            TextBox txtIssue_to_other = (TextBox)row1.FindControl("txtIssue_to_other");
            TextBox txtButter_Milk = (TextBox)row1.FindControl("txtButter_Milk");
            TextBox txtIssue_for_FP = (TextBox)row1.FindControl("txtIssue_for_FP");
            TextBox txtIssue_for_Sale = (TextBox)row1.FindControl("txtIssue_for_Sale");
            TextBox txtCB_CC = (TextBox)row1.FindControl("txtCB_CC");
            TextBox txtCB_Loose = (TextBox)row1.FindControl("txtCB_Loose");
            TextBox txtCB_Cold_Room1 = (TextBox)row1.FindControl("txtCB_Cold_Room1");
            TextBox txtCB_Cold_Room2 = (TextBox)row1.FindControl("txtCB_Cold_Room2");
            TextBox txtSample = (TextBox)row1.FindControl("txtSample");
            //TextBox txtLOOSE_Closing = (TextBox)row1.FindControl("txtLOOSE_Closing");
            //TextBox txtVE2_Closing = (TextBox)row1.FindControl("txtVE2_Closing");
            //TextBox txtVE1_Closing = (TextBox)row1.FindControl("txtVE1_Closing");

            if (txtIssue_to_Processing.Text != "" && txtIssue_to_Processing.Text != "0" && txtIssue_to_Processing.Text != "0.00")
            {
                Issue_to_Processing = Convert.ToDecimal(txtIssue_to_Processing.Text);
            }
            else
            {
                Issue_to_Processing = 0;
            }
            if (txtIssue_for_Ghee.Text != "" && txtIssue_for_Ghee.Text != "0" && txtIssue_for_Ghee.Text != "0.00")
            {
                Issue_for_Ghee = Convert.ToDecimal(txtIssue_for_Ghee.Text);
            }
            else
            {
                Issue_for_Ghee = 0;
            }
            if (txtButter_Milk.Text != "" && txtButter_Milk.Text != "0" && txtButter_Milk.Text != "0.00")
            {
                Butter_Milk = Convert.ToDecimal(txtButter_Milk.Text);
            }
            else
            {
                Butter_Milk = 0;
            }
            if (txtIssue_to_other.Text != "" && txtIssue_to_other.Text != "0" && txtIssue_to_other.Text != "0.00")
            {
                Issue_to_other = Convert.ToDecimal(txtIssue_to_other.Text);
            }
            else
            {
                Issue_to_other = 0;
            }

            if (txtIssue_for_FP.Text != "" && txtIssue_for_FP.Text != "0" && txtIssue_for_FP.Text != "0.00")
            {
                Issue_for_FP = Convert.ToDecimal(txtIssue_for_FP.Text);
            }
            else
            {
                Issue_for_FP = 0;
            }


            if (txtIssue_for_Sale.Text != "" && txtIssue_for_Sale.Text != "0" && txtIssue_for_Sale.Text != "0.00")
            {
                Issue_for_Sale = Convert.ToDecimal(txtIssue_for_Sale.Text);
            }
            else
            {
                Issue_for_Sale = 0;
            }
            if (txtCB_CC.Text != "" && txtCB_CC.Text != "0" && txtCB_CC.Text != "0.00")
            {
                CB_CC = Convert.ToDecimal(txtCB_CC.Text);
            }
            else
            {
                CB_CC = 0;
            }


            if (txtCB_Loose.Text != "" && txtCB_Loose.Text != "0" && txtCB_Loose.Text != "0.00")
            {
                CB_Loose = Convert.ToDecimal(txtCB_Loose.Text);
            }
            else
            {
                CB_Loose = 0;
            }

            if (txtCB_Cold_Room1.Text != "" && txtCB_Cold_Room1.Text != "0" && txtCB_Cold_Room1.Text != "0.00")
            {
                CB_Cold_Room1 = Convert.ToDecimal(txtCB_Cold_Room1.Text);
            }
            else
            {
                CB_Cold_Room1 = 0;
            }

            if (txtCB_Cold_Room2.Text != "" && txtCB_Cold_Room2.Text != "0" && txtCB_Cold_Room2.Text != "0.00")
            {
                CB_Cold_Room2 = Convert.ToDecimal(txtCB_Cold_Room2.Text);
            }
            else
            {
                CB_Cold_Room2 = 0;
            }
            if (txtSample.Text != "" && txtSample.Text != "0" && txtSample.Text != "0.00")
            {
                Sample = Convert.ToDecimal(txtSample.Text);
            }
            else
            {
                Sample = 0;
            }


            OutTotal = Issue_to_Processing + Issue_for_Ghee + Butter_Milk + Issue_to_other + Issue_for_FP + Issue_for_Sale
                + CB_CC + CB_Loose
                + CB_Cold_Room1 + CB_Cold_Room2 + Sample
               ;

            Int32 IID = Convert.ToInt32(lblItem_id.Text);
            DataRow dr1 = dt.AsEnumerable().Where(r => ((Int32)r["Item_id"]).Equals(IID)).First();

            dr1["Issue_to_Processing"] = Issue_to_Processing;
            dr1["Issue_for_Ghee"] = Issue_for_Ghee;
            dr1["Butter_Milk"] = Butter_Milk;
            dr1["Issue_to_other"] = Issue_to_other;
            dr1["Issue_for_FP"] = Issue_for_FP;
            dr1["Issue_for_Sale"] = Issue_for_Sale;
            dr1["CB_CC"] = CB_CC;
            dr1["CB_Loose"] = CB_Loose;
            dr1["CB_Cold_Room1"] = CB_Cold_Room1;
            dr1["CB_Cold_Room2"] = CB_Cold_Room2;
            dr1["Sample"] = Sample;
           


        }
        return dt;
    }

    protected void btnYesT_P_Click1(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                btnGetTotal_Click(sender, e);
                if (decimal.Parse(ViewState["TotalQtyInflow"].ToString()) == decimal.Parse(ViewState["TotalQtyOutFlow"].ToString()) && decimal.Parse(ViewState["TotalFatInflow"].ToString()) == decimal.Parse(ViewState["TotalFatOutFlow"].ToString()) && decimal.Parse(ViewState["TotalSnfInflow"].ToString()) == decimal.Parse(ViewState["TotalSnfOutFlow"].ToString()))
                {
                    DataTable dtIDF = new DataTable();
                    dtIDF = GetProductVarientInfo();

                    if (dtIDF.Rows.Count > 0)
                    {
                        if (btnPopupSave_Product.Text == "Save")
                        {
                            ds = null;
                            ds = objdb.ByProcedure("Sp_Production_ButterSheet",
                                                      new string[] { "flag" 
                                                ,"Office_ID"
                                                ,"Date" 
                                                ,"ProductSection_ID"
                                                ,"TotalIn"
                                                ,"TotalOut"
                                                ,"CreatedBy"
                                                ,"CreatedBy_IP"
                                                ,"ItemType_id"
                                    },
                                                      new string[] { "2"
                                              ,objdb.Office_ID()
                                              , Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                                               ,ddlPSection.SelectedValue 
                                              ,"0"
                                              ,"0"
                                              , ViewState["Emp_ID"].ToString()
                                              ,objdb.GetLocalIPAddress()
                                              ,objdb.LooseButterItemTypeId_ID()
                                    },
                                                     new string[] { "type_Production_ButterSheetNewChild" },
                                                     new DataTable[] { dtIDF }, "TableSave");


                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                GetSectionDetail();
                                btnGetTotal_Click(sender, e);
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);

                            }

                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Already Exists")
                            {
                                string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + success);

                            }

                            else
                            {
                                string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());

                            }
                        }
                        else if (btnPopupSave_Product.Text == "Update")
                        {
                            ds = null;
                            ds = objdb.ByProcedure("Sp_Production_ButterSheet",
                                                      new string[] { "flag",
                                                       "ButterSheet_ID"
                                                      ,"CreatedBy"
                                                      ,"CreatedBy_IP"
                                                      ,"ItemType_id"
                                    },
                                                      new string[] { "3"
                                              , ViewState["ButterSheet_ID"].ToString()                                           
                                              , ViewState["Emp_ID"].ToString()
                                              ,objdb.GetLocalIPAddress()
                                              ,objdb.LooseButterItemTypeId_ID()
                                    },
                                                     new string[] { "type_Production_ButterSheetNewChild" },
                                                     new DataTable[] { dtIDF }, "TableSave");


                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                GetSectionDetail();
                                btnGetTotal_Click(sender, e);
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);

                            }

                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Already Exists")
                            {
                                string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + success);

                            }

                            else
                            {
                                string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", error.ToString());

                            }
                        }

                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "variant Value Can't Empty");

                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Total Inflow and Total Out Flow should be equal");
                }


                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["TotalQtyInflow"] = "";
                ViewState["TotalQtyOutFlow"] = "";
                ViewState["TotalFatInflow"] = "";
                ViewState["TotalFatOutFlow"] = "";
                ViewState["TotalSnfInflow"] = "";
                ViewState["TotalSnfOutFlow"] = "";
				
				

            }

        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
        }

    }


}