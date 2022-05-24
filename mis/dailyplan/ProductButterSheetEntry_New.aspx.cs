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


public partial class mis_dailyplan_ProductButterSheetEntry : System.Web.UI.Page
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
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            // For Variant

            DataSet dsVD_Child = objdb.ByProcedure("Usp_ButterSheetEntry",
            new string[] { "flag", "Office_ID", "Date", "ProductSection_ID", "ItemType_id" },
            new string[] { "1", objdb.Office_ID(), Fdate, ddlPSection.SelectedValue, objdb.LooseButterItemTypeId_ID() }, "dataset");

            if (dsVD_Child != null && dsVD_Child.Tables[0].Rows.Count > 0)
            {
				if (dsVD_Child.Tables[0].Rows[0]["PPSM"].ToString() == "0")
                {
                    btnPopupSave_Product.Text = "Save";
                }
                else
                {
                    btnPopupSave_Product.Text = "Update";
                    ViewState["PPSM"] = dsVD_Child.Tables[0].Rows[0]["PPSM"].ToString();
                    txtReceived_From.Text = dsVD_Child.Tables[0].Rows[0]["Received_From"].ToString();
                }
                Gv_Opening.DataSource = dsVD_Child;
                Gv_Opening.DataBind();

                Gv_Cream_ButterReceived.DataSource = dsVD_Child;
                Gv_Cream_ButterReceived.DataBind();

                GV_ButterMfg.DataSource = dsVD_Child;
                GV_ButterMfg.DataBind();

                Gv_ButterIssueToFP.DataSource = dsVD_Child;
                Gv_ButterIssueToFP.DataBind();

                Gv_IssuetoOther.DataSource = dsVD_Child;
                Gv_IssuetoOther.DataBind();

                Gv_Closing.DataSource = dsVD_Child;
                Gv_Closing.DataBind();
            }

            else
            {

                Gv_Opening.DataSource = string.Empty;
                Gv_Opening.DataBind();

                Gv_Cream_ButterReceived.DataSource = string.Empty;
                Gv_Cream_ButterReceived.DataBind();

                GV_ButterMfg.DataSource = string.Empty;
                GV_ButterMfg.DataBind();

                Gv_ButterIssueToFP.DataSource = string.Empty;
                Gv_ButterIssueToFP.DataBind();

                Gv_IssuetoOther.DataSource = string.Empty;
                Gv_IssuetoOther.DataBind();

                Gv_Closing.DataSource = dsVD_Child;
                Gv_Closing.DataBind();
              
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);

        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }

    protected void GetCCDetails()
    {
        try
        {
            //ddlccdetails.Items.Clear();

            //if (ddlmfdfrom.SelectedValue == "1")
            //{
            //    ddlccdetails.Items.Insert(0, new ListItem(ddlDS.SelectedItem.Text, ddlDS.SelectedValue));

            //}
            //else if (ddlmfdfrom.SelectedValue == "2")
            //{
            //    ds = null;
            //    ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails",
            //             new string[] { "flag", "Office_Parant_ID" },
            //             new string[] { "3", objdb.Office_ID() }, "dataset");

            //    if (ds != null)
            //    {
            //        if (ds.Tables.Count > 0)
            //        {
            //            if (ds.Tables[0].Rows.Count > 0)
            //            {
            //                ddlccdetails.DataTextField = "Office_Name";
            //                ddlccdetails.DataValueField = "Office_ID";
            //                ddlccdetails.DataSource = ds;
            //                ddlccdetails.DataBind();
            //                ddlccdetails.Items.Insert(0, new ListItem("Select", "0"));

            //            }
            //        }
            //    }

            //}

            //else
            //{
            //    ddlccdetails.Items.Insert(0, new ListItem("Select", "0"));
            //}


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //protected void ddlmfdfrom_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetCCDetails();
    //}

    //protected void btnAddcc_Click(object sender, EventArgs e)
    //{
    //    lblMsg.Text = "";
    //    AddCCDetails();
    //}

    //private void AddCCDetails()
    //{
    //    try
    //    {

    //        int CompartmentType = 0;
    //        string mdf = "";

    //        if (Convert.ToString(ViewState["InsertRecord"]) == null || Convert.ToString(ViewState["InsertRecord"]) == "")
    //        {
    //            DataTable dt = new DataTable();
    //            DataRow dr;


    //            dt.Columns.Add(new DataColumn("MdfId", typeof(string)));
    //            dt.Columns.Add(new DataColumn("MdfOfficeId", typeof(string)));
    //            dt.Columns.Add(new DataColumn("MdftypeId", typeof(string)));
    //            dt.Columns.Add(new DataColumn("Name", typeof(string)));
    //            dt.Columns.Add(new DataColumn("QuantityInKg", typeof(decimal)));
    //            dt.Columns.Add(new DataColumn("FatInKg", typeof(decimal)));
    //            dt.Columns.Add(new DataColumn("SnfInKg", typeof(decimal)));

    //            if (ddlmfdfrom.SelectedValue == "1")
    //            {
    //                mdf = "White Butter Rcvd. " + ddlMilkType.SelectedItem.Text;

    //            }
    //            else if (ddlmfdfrom.SelectedValue == "2")
    //            {
    //                mdf = "White Butter Rcvd. -" + ddlMilkType.SelectedItem.Text + " (" + ddlccdetails.SelectedItem.Text + ")";

    //            }

    //            dr = dt.NewRow();
    //            dr[0] = ddlmfdfrom.SelectedValue;
    //            dr[1] = ddlccdetails.SelectedValue;
    //            dr[2] = ddlMilkType.SelectedValue;
    //            dr[3] = mdf;
    //            dr[4] = txtQuantityInKg.Text;
    //            dr[5] = txtFatInKg.Text;
    //            dr[6] = txtSnfInKg.Text;
    //            dt.Rows.Add(dr);
    //            ViewState["InsertRecord"] = dt;
    //            GVVariantDetail_CC.DataSource = dt;
    //            GVVariantDetail_CC.DataBind();

    //        }
    //        else
    //        {
    //            DataTable dt = new DataTable();
    //            DataTable DT = new DataTable();
    //            DataRow dr;
    //            dt.Columns.Add(new DataColumn("MdfId", typeof(string)));
    //            dt.Columns.Add(new DataColumn("MdfOfficeId", typeof(string)));
    //            dt.Columns.Add(new DataColumn("MdftypeId", typeof(string)));
    //            dt.Columns.Add(new DataColumn("Name", typeof(string)));
    //            dt.Columns.Add(new DataColumn("QuantityInKg", typeof(decimal)));
    //            dt.Columns.Add(new DataColumn("FatInKg", typeof(decimal)));
    //            dt.Columns.Add(new DataColumn("SnfInKg", typeof(decimal)));
    //            DT = (DataTable)ViewState["InsertRecord"];

    //            for (int i = 0; i < DT.Rows.Count; i++)
    //            {
    //                if (ddlmfdfrom.SelectedValue == DT.Rows[i]["MdfId"].ToString()
    //                    && ddlccdetails.SelectedValue == DT.Rows[i]["MdfOfficeId"].ToString()
    //                    && ddlMilkType.SelectedValue == DT.Rows[i]["MdftypeId"].ToString())
    //                {
    //                    CompartmentType = 1;
    //                }

    //            }
    //            if (CompartmentType == 1)
    //            {
    //                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Data already exist Mfd Type/ Office Name/ Milk Type.");
    //            }
    //            else
    //            {
    //                if (ddlmfdfrom.SelectedValue == "1")
    //                {
    //                    mdf = "White Butter Rcvd. " + ddlMilkType.SelectedItem.Text;

    //                }
    //                else if (ddlmfdfrom.SelectedValue == "2")
    //                {
    //                    mdf = "White Butter Rcvd. -" + ddlMilkType.SelectedItem.Text + " (" + ddlccdetails.SelectedItem.Text + ")";

    //                }

    //                dr = dt.NewRow();
    //                dr[0] = ddlmfdfrom.SelectedValue;
    //                dr[1] = ddlMilkType.SelectedValue;
    //                dr[2] = ddlMilkType.SelectedValue;
    //                dr[3] = mdf;
    //                dr[4] = txtQuantityInKg.Text;
    //                dr[5] = txtFatInKg.Text;
    //                dr[6] = txtSnfInKg.Text;
    //                dt.Rows.Add(dr);
    //            }

    //            foreach (DataRow tr in DT.Rows)
    //            {
    //                dt.Rows.Add(tr.ItemArray);
    //            }
    //            ViewState["InsertRecord"] = dt;
    //            GVVariantDetail_CC.DataSource = dt;
    //            GVVariantDetail_CC.DataBind();
    //        }

    //        //Clear Record

    //        ddlmfdfrom.ClearSelection();
    //        ddlccdetails.Items.Clear();
    //        ddlccdetails.Items.Insert(0, new ListItem("Select", "0"));
    //        ddlMilkType.ClearSelection();
    //        txtQuantityInKg.Text = "";
    //        txtFatInKg.Text = "";
    //        txtSnfInKg.Text = "";


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
    //    }
    //}

    //protected void lnkDeleteCC_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        GridViewRow row1 = (sender as LinkButton).NamingContainer as GridViewRow;
    //        DataTable dt3 = ViewState["InsertRecord"] as DataTable;
    //        dt3.Rows.Remove(dt3.Rows[row1.RowIndex]);
    //        ViewState["InsertRecord"] = dt3;
    //        GVVariantDetail_CC.DataSource = dt3;
    //        GVVariantDetail_CC.DataBind();
    //        ddlccdetails.ClearSelection();

    //        ddlmfdfrom.ClearSelection();
    //        ddlccdetails.Items.Clear();
    //        ddlccdetails.Items.Insert(0, new ListItem("Select", "0"));
    //        ddlMilkType.ClearSelection();
    //        txtQuantityInKg.Text = "";
    //        txtFatInKg.Text = "";
    //        txtSnfInKg.Text = "";

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
    //    }
    //}

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
            decimal TotalFAT = 0;
            decimal TotalSNF = 0;
            int i = 0;
            //foreach (GridViewRow row in GVVariantDetail_In.Rows)
            //{
            //    i = i + 1;
            //    Label lblCC_Opening = (Label)row.FindControl("lblCC_Opening");
            //    Label lblCreamrcvdforwhitebuttergood = (Label)row.FindControl("lblCreamrcvdforwhitebuttergood");
            //    Label lblCreamrcvdforwhitebuttersur = (Label)row.FindControl("lblCreamrcvdforwhitebuttersur");
            //    Label lblCreamrcvdfortablebutter = (Label)row.FindControl("lblCreamrcvdfortablebutter");
            //    Label lblCreamrcvdforcookingbutter = (Label)row.FindControl("lblCreamrcvdforcookingbutter");
            //    TextBox lblbutterrcvdfromcc = (TextBox)row.FindControl("lblbutterrcvdfromcc");
            //    TextBox lblbutterrcvdfromFinprod = (TextBox)row.FindControl("lblbutterrcvdfromFinprod");
            //    TextBox lblintotal = (TextBox)row.FindControl("lblintotal");
            //    Label lblItemName = (Label)row.FindControl("lblItemName");

            //    if (lblCC_Opening.Text == "")
            //    {
            //        lblCC_Opening.Text = "0";
            //    }
            //    if (lblCreamrcvdforwhitebuttergood.Text == "")
            //    {
            //        lblCreamrcvdforwhitebuttergood.Text = "0";
            //    }
            //    if (lblCreamrcvdforwhitebuttersur.Text == "")
            //    {
            //        lblCreamrcvdforwhitebuttersur.Text = "0";
            //    }
            //    if (lblCreamrcvdfortablebutter.Text == "")
            //    {
            //        lblCreamrcvdfortablebutter.Text = "0";
            //    }
            //    if (lblCreamrcvdforcookingbutter.Text == "")
            //    {
            //        lblCreamrcvdforcookingbutter.Text = "0";
            //    }
            //    if (lblbutterrcvdfromcc.Text == "")
            //    {
            //        lblbutterrcvdfromcc.Text = "0";

            //    }
            //    if (lblbutterrcvdfromFinprod.Text == "")
            //    {
            //        lblbutterrcvdfromFinprod.Text = "0";

            //    }
            //    if (i == 2)
            //    {
            //        TotalFAT = Convert.ToDecimal(lblbutterrcvdfromFinprod.Text);
            //    }
            //    if (i == 3)
            //    {
            //        TotalSNF = Convert.ToDecimal(lblbutterrcvdfromFinprod.Text);
            //    }
            //    lblintotal.Text = (Convert.ToDecimal(lblCC_Opening.Text) + Convert.ToDecimal(lblCreamrcvdforwhitebuttergood.Text)
            //                   + Convert.ToDecimal(lblCreamrcvdforwhitebuttersur.Text) + Convert.ToDecimal(lblCreamrcvdfortablebutter.Text)
            //                   + Convert.ToDecimal(lblCreamrcvdforcookingbutter.Text) + Convert.ToDecimal(lblbutterrcvdfromcc.Text) + Convert.ToDecimal(lblbutterrcvdfromFinprod.Text)).ToString();
            //    if (lblintotal.Text == "")
            //    {
            //        lblintotal.Text = "0";
            //    }
            //    if (lblItemName.Text == " Quantity In Kg")
            //    {

            //        TotalQtyInflow = decimal.Parse(lblintotal.Text);
            //    }
            //    if (lblItemName.Text == " Fat In Kg")
            //    {
            //        TotalFatInflow = decimal.Parse(lblintotal.Text);
            //    }
            //    if (lblItemName.Text == " Snf In Kg")
            //    {
            //        TotalSnfInflow = decimal.Parse(lblintotal.Text);
            //    }

            //    if (lblintotal.Text != "")
            //    {
            //        btnPopupSave_Product.Enabled = true;
            //    }
            //}
            //foreach (GridViewRow row in GVVariantDetail_CC.Rows)
            //{
            //    Label lblQuantityInKg = (Label)row.FindControl("lblQuantityInKg");
            //    Label lblFatInKg = (Label)row.FindControl("lblFatInKg");
            //    Label lblSnfInKg = (Label)row.FindControl("lblSnfInKg");

            //    TotalQtyInflow += decimal.Parse(lblQuantityInKg.Text);
            //    TotalFatInflow += decimal.Parse(lblFatInKg.Text);
            //    TotalSnfInflow += decimal.Parse(lblSnfInKg.Text);

            //}
            //if (GVVariantDetail_CC.Rows.Count > 0)
            //{
            //    GVVariantDetail_CC.FooterRow.Cells[0].Text = "<b>Total : </b>";
            //    GVVariantDetail_CC.FooterRow.Cells[1].Text = "<b>" + TotalQtyInflow.ToString() + "</b>";
            //    GVVariantDetail_CC.FooterRow.Cells[2].Text = "<b>" + TotalFatInflow.ToString() + "</b>";
            //    GVVariantDetail_CC.FooterRow.Cells[3].Text = "<b>" + TotalSnfInflow.ToString() + "</b>";
            //}


            //foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
            //{
            //    Label Label2 = (Label)row1.FindControl("Label2");
            //    TextBox txtIssueTo_Processing = (TextBox)row1.FindControl("txtIssueTo_Processing");
            //    TextBox txtwhitebuttermfg = (TextBox)row1.FindControl("txtwhitebuttermfg");
            //    TextBox txttablebuttermfg = (TextBox)row1.FindControl("txttablebuttermfg");
            //    TextBox txtcookingbuttermfg = (TextBox)row1.FindControl("txtcookingbuttermfg");
            //    TextBox txtIssueFor_Ghee = (TextBox)row1.FindControl("txtIssueFor_Ghee");
            //    TextBox txtButterIssueFor_other = (TextBox)row1.FindControl("txtButterIssueFor_other");
            //    TextBox txtIssueFor_Store = (TextBox)row1.FindControl("txtIssueFor_Store");
            //    TextBox txtIssueFor_SweetButterMilk = (TextBox)row1.FindControl("txtIssueFor_SweetButterMilk");
            //    TextBox txtCL_Closing = (TextBox)row1.FindControl("txtCL_Closing");
            //    //TextBox txtLOOSE_Closing = (TextBox)row1.FindControl("txtLOOSE_Closing");
            //    //TextBox txtVE2_Closing = (TextBox)row1.FindControl("txtVE2_Closing");
            //    //TextBox txtVE1_Closing = (TextBox)row1.FindControl("txtVE1_Closing");
            //    if (txtwhitebuttermfg.Text == "")
            //    {
            //        txtwhitebuttermfg.Text = "0";
            //    }

            //    if (txttablebuttermfg.Text == "")
            //    {
            //        txttablebuttermfg.Text = "0";
            //    }

            //    if (txtcookingbuttermfg.Text == "")
            //    {
            //        txtcookingbuttermfg.Text = "0";
            //    }

            //    if (txtIssueTo_Processing.Text == "")
            //    {
            //        txtIssueTo_Processing.Text = "0";
            //    }

            //    if (txtIssueFor_Ghee.Text == "")
            //    {
            //        txtIssueFor_Ghee.Text = "0";
            //    }
            //    if (txtButterIssueFor_other.Text == "")
            //    {
            //        txtButterIssueFor_other.Text = "0";
            //    }
            //    if (txtIssueFor_Store.Text == "")
            //    {
            //        txtIssueFor_Store.Text = "0";
            //    }
            //    if (txtIssueFor_SweetButterMilk.Text == "")
            //    {
            //        txtIssueFor_SweetButterMilk.Text = "0";
            //    }

            //    if (txtCL_Closing.Text == "")
            //    {
            //        txtCL_Closing.Text = "0";
            //    }

            //    //if (txtLOOSE_Closing.Text == "")
            //    //{
            //    //    txtLOOSE_Closing.Text = "0";
            //    //}

            //    //if (txtVE2_Closing.Text == "")
            //    //{
            //    //    txtVE2_Closing.Text = "0";
            //    //}
            //    //if (txtVE1_Closing.Text == "")
            //    //{
            //    //    txtVE1_Closing.Text = "0";
            //    //}

            //    TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");

            //    // lblouttotal.Text = (Convert.ToDecimal(txtIssueTo_Processing.Text) + Convert.ToDecimal(txtIssueFor_Ghee.Text) + Convert.ToDecimal(txtIssueFor_Store.Text) + Convert.ToDecimal(txtIssueFor_SweetButterMilk.Text) + Convert.ToDecimal(txtCL_Closing.Text)
            //    //  + Convert.ToDecimal(txtLOOSE_Closing.Text) + Convert.ToDecimal(txtVE2_Closing.Text) + Convert.ToDecimal(txtVE1_Closing.Text)).ToString();
            //    lblouttotal.Text = (Convert.ToDecimal(txtwhitebuttermfg.Text) + Convert.ToDecimal(txttablebuttermfg.Text)
            //                       + Convert.ToDecimal(txtcookingbuttermfg.Text) + Convert.ToDecimal(txtIssueTo_Processing.Text)
            //                       + Convert.ToDecimal(txtIssueFor_Ghee.Text) + Convert.ToDecimal(txtButterIssueFor_other.Text) + Convert.ToDecimal(txtIssueFor_Store.Text)
            //                       + Convert.ToDecimal(txtIssueFor_SweetButterMilk.Text) + Convert.ToDecimal(txtCL_Closing.Text)).ToString();
            //   // lblouttotal.Text = (Convert.ToDecimal(txtIssueFor_Ghee.Text) + Convert.ToDecimal(txtButterIssueFor_other.Text) + Convert.ToDecimal(txtIssueFor_Store.Text)
            //                      // + Convert.ToDecimal(txtIssueFor_SweetButterMilk.Text) + Convert.ToDecimal(txtCL_Closing.Text)).ToString();
            //    if (lblouttotal.Text == "")
            //    {
            //        lblouttotal.Text = "0";
            //    }
            //    if (Label2.Text == " Quantity In Kg")
            //    {

            //        TotalQtyOutFlow = decimal.Parse(lblouttotal.Text);
            //    }
            //    if (Label2.Text == " Fat In Kg")
            //    {
            //        TotalFatOutFlow = decimal.Parse(lblouttotal.Text);
            //    }
            //    if (Label2.Text == " Snf In Kg")
            //    {
            //        TotalSnfOutFlow = decimal.Parse(lblouttotal.Text);
            //    }

            //    if (lblouttotal.Text != "")
            //    {
            //        btnPopupSave_Product.Enabled = true;

            //    }

            //}
            //txtVariationQtyInKg.Text = (TotalQtyOutFlow - TotalQtyInflow).ToString();
            //txtVariationFatInKg.Text = (TotalFatOutFlow - TotalFatInflow).ToString();
            //txtVariationSnfInKg.Text = (TotalSnfOutFlow - TotalSnfInflow).ToString();

            ////FAT Per
            //if (Convert.ToDecimal(txtVariationFatInKg.Text) > 0)
            //{
            //    txtFATPer.Text = Convert.ToDecimal(((Convert.ToDecimal(txtVariationFatInKg.Text) / TotalFAT) * 100) / 100).ToString("0.00");
            //    spn.Visible = false;
            //}
            //else
            //{
            //    if (txtFATPer.Text == "")
            //    {
            //        txtFATPer.Text = "0";
            //        spn.Visible = true;
            //        spn.InnerText = "IF FAT in kg is in negative so FAT % won't be shown.";
            //    }
            //}

            //if (Convert.ToDecimal(txtFATPer.Text) == 0 || Convert.ToDecimal(txtFATPer.Text) <= Convert.ToDecimal("1.5"))
            //{
            //    lblVariation.Text = "";
            //    btnPopupSave_Product.Enabled = true;
            //}
            //else
            //{
            //    btnPopupSave_Product.Enabled = false;
            //    lblVariation.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "FAT % should be Less than or equal to 1.5.");
            //}
            ////SNF Per
            //if (Convert.ToDecimal(txtVariationSnfInKg.Text) > 0)
            //{
            //    txtSNFPer.Text = Convert.ToDecimal(((Convert.ToDecimal(txtVariationSnfInKg.Text) / TotalSNF) * 100) / 100).ToString("0.00");
            //    spn.Visible = false;
                
            //}
            //else
            //{
            //    if (txtSnfInKg.Text == "")
            //    {
            //        txtSnfInKg.Text = "0";
            //        spn.Visible = true;
            //        spn.InnerText = "IF SNF in Kg is in negative so SNF % won't be shown.";
            //    }
            //}
            //if (Convert.ToDecimal(txtFATPer.Text) < 0 && Convert.ToDecimal(txtSnfInKg.Text) < 0)
            //{
            //    spn.Visible = true;
            //    spn.InnerText = "IF FAT in kg and SNF in kg is in negative so FAT % and SNF % won't be shown.";
            //}

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    //protected void txtIssueTo_Processing_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
    //        {

    //            TextBox txtIssueTo_Processing = (TextBox)row1.FindControl("txtIssueTo_Processing");
    //            TextBox txtIssueFor_Ghee = (TextBox)row1.FindControl("txtIssueFor_Ghee");
    //            TextBox txtIssueFor_Store = (TextBox)row1.FindControl("txtIssueFor_Store");
    //            TextBox txtCL_Closing = (TextBox)row1.FindControl("txtCL_Closing");
    //            TextBox txtLOOSE_Closing = (TextBox)row1.FindControl("txtLOOSE_Closing");
    //            TextBox txtVE2_Closing = (TextBox)row1.FindControl("txtVE2_Closing");
    //            TextBox txtVE1_Closing = (TextBox)row1.FindControl("txtVE1_Closing");

    //            if (txtIssueTo_Processing.Text == "")
    //            {
    //                txtIssueTo_Processing.Text = "0";
    //            }

    //            if (txtIssueFor_Ghee.Text == "")
    //            {
    //                txtIssueFor_Ghee.Text = "0";
    //            }

    //            if (txtIssueFor_Store.Text == "")
    //            {
    //                txtIssueFor_Store.Text = "0";
    //            }

    //            if (txtCL_Closing.Text == "")
    //            {
    //                txtCL_Closing.Text = "0";
    //            }

    //            if (txtLOOSE_Closing.Text == "")
    //            {
    //                txtLOOSE_Closing.Text = "0";
    //            }

    //            if (txtVE2_Closing.Text == "")
    //            {
    //                txtVE2_Closing.Text = "0";
    //            }
    //            if (txtVE1_Closing.Text == "")
    //            {
    //                txtVE1_Closing.Text = "0";
    //            }

    //            TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");

    //            lblouttotal.Text = (Convert.ToDecimal(txtIssueTo_Processing.Text) + Convert.ToDecimal(txtIssueFor_Ghee.Text) + Convert.ToDecimal(txtIssueFor_Store.Text) + Convert.ToDecimal(txtCL_Closing.Text)
    //                + Convert.ToDecimal(txtLOOSE_Closing.Text) + Convert.ToDecimal(txtVE2_Closing.Text) + Convert.ToDecimal(txtVE1_Closing.Text)).ToString();

    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    //protected void txtIssueFor_Ghee_TextChanged(object sender, EventArgs e)
    //{
    //    txtIssueTo_Processing_TextChanged(sender, e);
    //}

    //protected void txtIssueFor_Store_TextChanged(object sender, EventArgs e)
    //{
    //    txtIssueTo_Processing_TextChanged(sender, e);
    //}

    //protected void txtCL_Closing_TextChanged(object sender, EventArgs e)
    //{
    //    txtIssueTo_Processing_TextChanged(sender, e);
    //}

    //protected void txtLOOSE_Closing_TextChanged(object sender, EventArgs e)
    //{
    //    txtIssueTo_Processing_TextChanged(sender, e);
    //}

    //protected void txtVE2_Closing_TextChanged(object sender, EventArgs e)
    //{
    //    txtIssueTo_Processing_TextChanged(sender, e);
    //}

    //protected void txtVE1_Closing_TextChanged(object sender, EventArgs e)
    //{
    //    txtIssueTo_Processing_TextChanged(sender, e);
    //}

    private DataTable GetProductVarientInfo()
    {

        decimal ButterCCOpening = 0;
        decimal ButterLooseOpening = 0;
        decimal ButterVE2Opening = 0;
        decimal ButterVE1Opening = 0;
        decimal CreamRcvdForWhiteButterGood = 0;
        decimal CreamRcvdForWhiteButterSour = 0;
        decimal CreamRcvdForTableButter = 0;
        decimal CreamRcvdForCookingButter = 0;
        decimal ButterRcvdFromCC = 0;
        decimal ButterRcvdFromFinishedProducts = 0;
        decimal WhiteButterMfg = 0;
        decimal TableButterMfg = 0;
        decimal CookingButterMfg = 0;

        decimal ButterIssueToProcessing = 0;
        decimal ButterIssueForGhee = 0;
        decimal ButterIssueForOther = 0;
        decimal ButterIssueForStore = 0;
        decimal ButterIssueForSweetButterMilk = 0;
        decimal ButterCCClosing = 0;
        decimal ButterLooseClosing = 0;
        decimal ButterVE2Closing = 0;
        decimal ButterVE1Closing = 0;
        decimal InTotal = 0;
        decimal OutTotal = 0;

        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("ItemCat_id", typeof(int)));
        dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
        dt.Columns.Add(new DataColumn("Item_id", typeof(int)));
        dt.Columns.Add(new DataColumn("ButterCCOpening", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ButterLooseOpening", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ButterVE2Opening", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ButterVE1Opening", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CreamRcvdForWhiteButterGood", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CreamRcvdForWhiteButterSour", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CreamRcvdForTableButter", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CreamRcvdForCookingButter", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ButterRcvdFromCC", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ButterRcvdFromFinishedProducts", typeof(decimal)));
        dt.Columns.Add(new DataColumn("WhiteButterMfg", typeof(decimal)));
        dt.Columns.Add(new DataColumn("TableButterMfg", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CookingButterMfg", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ButterIssueToProcessing", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ButterIssueForGhee", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ButterIssueForStore", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ButterIssueForSweetButterMilk", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ButterCCClosing", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ButterLooseClosing", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ButterVE2Closing", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ButterVE1Closing", typeof(decimal)));
        dt.Columns.Add(new DataColumn("InTotal", typeof(decimal)));
        dt.Columns.Add(new DataColumn("OutTotal", typeof(decimal)));

        //foreach (GridViewRow row in GVVariantDetail_In.Rows)
        //{
        //    Label lblItem_id = (Label)row.FindControl("lblItem_id");

        //    Label lblCC_Opening = (Label)row.FindControl("lblCC_Opening");
        //    Label lblCreamrcvdforwhitebuttergood = (Label)row.FindControl("lblCreamrcvdforwhitebuttergood");
        //    Label lblCreamrcvdforwhitebuttersur = (Label)row.FindControl("lblCreamrcvdforwhitebuttersur");
        //    Label lblCreamrcvdfortablebutter = (Label)row.FindControl("lblCreamrcvdfortablebutter");
        //    Label lblCreamrcvdforcookingbutter = (Label)row.FindControl("lblCreamrcvdforcookingbutter");
        //    TextBox lblbutterrcvdfromcc = (TextBox)row.FindControl("lblbutterrcvdfromcc");
        //    TextBox lblbutterrcvdfromFinprod = (TextBox)row.FindControl("lblbutterrcvdfromFinprod");
        //    //Label lblLoose_Opening = (Label)row.FindControl("lblLoose_Opening");
        //    //Label lblVE2_Opening = (Label)row.FindControl("lblVE2_Opening");
        //    //Label lblVE1_Opening = (Label)row.FindControl("lblVE1_Opening");


        //    if (lblCC_Opening.Text != "" && lblCC_Opening.Text != "0" && lblCC_Opening.Text != "0.00")
        //    {
        //        ButterCCOpening = Convert.ToDecimal(lblCC_Opening.Text);
        //    }

        //    else
        //    {
        //        ButterCCOpening = 0;
        //    }
        //    if (lblCreamrcvdforwhitebuttergood.Text != "" && lblCreamrcvdforwhitebuttergood.Text != "0" && lblCreamrcvdforwhitebuttergood.Text != "0.00")
        //    {
        //        CreamRcvdForWhiteButterGood = Convert.ToDecimal(lblCreamrcvdforwhitebuttergood.Text);
        //    }

        //    else
        //    {
        //        CreamRcvdForWhiteButterGood = 0;
        //    }
        //    if (lblCreamrcvdforwhitebuttersur.Text != "" && lblCreamrcvdforwhitebuttersur.Text != "0" && lblCreamrcvdforwhitebuttersur.Text != "0.00")
        //    {
        //        CreamRcvdForWhiteButterSour = Convert.ToDecimal(lblCreamrcvdforwhitebuttersur.Text);
        //    }

        //    else
        //    {
        //        CreamRcvdForWhiteButterSour = 0;
        //    }
        //    if (lblCreamrcvdfortablebutter.Text != "" && lblCreamrcvdfortablebutter.Text != "0" && lblCreamrcvdfortablebutter.Text != "0.00")
        //    {
        //        CreamRcvdForTableButter = Convert.ToDecimal(lblCreamrcvdfortablebutter.Text);
        //    }

        //    else
        //    {
        //        CreamRcvdForTableButter = 0;
        //    }
        //    if (lblCreamrcvdforcookingbutter.Text != "" && lblCreamrcvdforcookingbutter.Text != "0" && lblCreamrcvdforcookingbutter.Text != "0.00")
        //    {
        //        CreamRcvdForCookingButter = Convert.ToDecimal(lblCreamrcvdforcookingbutter.Text);
        //    }

        //    else
        //    {
        //        CreamRcvdForCookingButter = 0;
        //    }
        //    if (lblbutterrcvdfromcc.Text != "" && lblbutterrcvdfromcc.Text != "0" && lblbutterrcvdfromcc.Text != "0.00")
        //    {
        //        ButterRcvdFromCC = Convert.ToDecimal(lblbutterrcvdfromcc.Text);
        //    }

        //    else
        //    {
        //        ButterRcvdFromCC = 0;
        //    }
        //    if (lblbutterrcvdfromFinprod.Text != "" && lblbutterrcvdfromFinprod.Text != "0" && lblbutterrcvdfromFinprod.Text != "0.00")
        //    {
        //        ButterRcvdFromFinishedProducts = Convert.ToDecimal(lblbutterrcvdfromFinprod.Text);
        //    }

        //    else
        //    {
        //        ButterRcvdFromFinishedProducts = 0;
        //    }
        //    //if (lblLoose_Opening.Text != "" && lblLoose_Opening.Text != "0" && lblLoose_Opening.Text != "0.00")
        //    //{
        //    //    ButterLooseOpening = Convert.ToDecimal(lblLoose_Opening.Text);
        //    //}
        //    //else
        //    //{
        //    //    ButterLooseOpening = 0;
        //    //}
        //    //if (lblVE2_Opening.Text != "" && lblVE2_Opening.Text != "0" && lblVE2_Opening.Text != "0.00")
        //    //{
        //    //    ButterVE2Opening = Convert.ToDecimal(lblVE2_Opening.Text);
        //    //}
        //    //else
        //    //{
        //    //    ButterVE2Opening = 0;
        //    //}

        //    //if (lblVE1_Opening.Text != "" && lblVE1_Opening.Text != "0" && lblVE1_Opening.Text != "0.00")
        //    //{
        //    //    ButterVE1Opening = Convert.ToDecimal(lblVE1_Opening.Text);
        //    //}
        //    //else
        //    //{
        //    //    ButterVE1Opening = 0;
        //    //}

        //    // InTotal = Convert.ToDecimal(lblCC_Opening.Text) + Convert.ToDecimal(lblLoose_Opening.Text) + Convert.ToDecimal(lblVE2_Opening.Text) + Convert.ToDecimal(lblVE1_Opening.Text);
        //    InTotal = Convert.ToDecimal(lblCC_Opening.Text) + Convert.ToDecimal(lblCreamrcvdforwhitebuttergood.Text) + Convert.ToDecimal(lblCreamrcvdforwhitebuttersur.Text) + Convert.ToDecimal(lblCreamrcvdfortablebutter.Text) + Convert.ToDecimal(lblCreamrcvdforcookingbutter.Text) + Convert.ToDecimal(lblbutterrcvdfromcc.Text) + Convert.ToDecimal(lblbutterrcvdfromFinprod.Text);
        //    dr = dt.NewRow();
        //    dr[0] = objdb.LooseButterItemCategoryId_ID();
        //    dr[1] = objdb.LooseButterItemTypeId_ID();
        //    dr[2] = lblItem_id.Text;
        //    dr[3] = ButterCCOpening;
        //    dr[4] = ButterLooseOpening;
        //    dr[5] = ButterVE2Opening;
        //    dr[6] = ButterVE1Opening;
        //    dr[7] = CreamRcvdForWhiteButterGood;
        //    dr[8] = CreamRcvdForWhiteButterSour;
        //    dr[9] = CreamRcvdForTableButter;
        //    dr[10] = CreamRcvdForCookingButter;
        //    dr[11] = ButterRcvdFromCC;
        //    dr[12] = ButterRcvdFromFinishedProducts;
        //    dr[13] = WhiteButterMfg;
        //    dr[14] = TableButterMfg;
        //    dr[15] = CookingButterMfg;

        //    dr[16] = ButterIssueToProcessing;
        //    dr[17] = ButterIssueForGhee;
        //    dr[18] = ButterIssueForStore;
        //    dr[19] = ButterIssueForSweetButterMilk;
        //    dr[20] = ButterCCClosing;
        //    dr[21] = ButterLooseClosing;
        //    dr[22] = ButterVE2Closing;
        //    dr[23] = ButterVE1Closing;
        //    dr[24] = InTotal;
        //    dr[25] = OutTotal;
        //    dt.Rows.Add(dr);
        //}

        //foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
        //{
        //    Label lblItem_id = (Label)row1.FindControl("lblItem_id_Out");
        //    TextBox txtIssueTo_Processing = (TextBox)row1.FindControl("txtIssueTo_Processing");
        //    TextBox txtwhitebuttermfg = (TextBox)row1.FindControl("txtwhitebuttermfg");
        //    TextBox txttablebuttermfg = (TextBox)row1.FindControl("txttablebuttermfg");
        //    TextBox txtcookingbuttermfg = (TextBox)row1.FindControl("txtcookingbuttermfg");
        //    TextBox txtIssueFor_Ghee = (TextBox)row1.FindControl("txtIssueFor_Ghee");
        //    TextBox txtIssueFor_Store = (TextBox)row1.FindControl("txtIssueFor_Store");
        //    TextBox txtIssueFor_SweetButterMilk = (TextBox)row1.FindControl("txtIssueFor_SweetButterMilk");
        //    TextBox txtCL_Closing = (TextBox)row1.FindControl("txtCL_Closing");
        //    //TextBox txtLOOSE_Closing = (TextBox)row1.FindControl("txtLOOSE_Closing");
        //    //TextBox txtVE2_Closing = (TextBox)row1.FindControl("txtVE2_Closing");
        //    //TextBox txtVE1_Closing = (TextBox)row1.FindControl("txtVE1_Closing");

        //    if (txtwhitebuttermfg.Text != "" && txtwhitebuttermfg.Text != "0" && txtwhitebuttermfg.Text != "0.00")
        //    {
        //        WhiteButterMfg = Convert.ToDecimal(txtwhitebuttermfg.Text);
        //    }
        //    else
        //    {
        //        WhiteButterMfg = 0;
        //    }
        //    if (txttablebuttermfg.Text != "" && txttablebuttermfg.Text != "0" && txttablebuttermfg.Text != "0.00")
        //    {
        //        TableButterMfg = Convert.ToDecimal(txttablebuttermfg.Text);
        //    }
        //    else
        //    {
        //        TableButterMfg = 0;
        //    }
        //    if (txtcookingbuttermfg.Text != "" && txtcookingbuttermfg.Text != "0" && txtcookingbuttermfg.Text != "0.00")
        //    {
        //        CookingButterMfg = Convert.ToDecimal(txtcookingbuttermfg.Text);
        //    }
        //    else
        //    {
        //        CookingButterMfg = 0;
        //    }
        //    if (txtIssueTo_Processing.Text != "" && txtIssueTo_Processing.Text != "0" && txtIssueTo_Processing.Text != "0.00")
        //    {
        //        ButterIssueToProcessing = Convert.ToDecimal(txtIssueTo_Processing.Text);
        //    }
        //    else
        //    {
        //        ButterIssueToProcessing = 0;
        //    }

        //    if (txtIssueFor_Ghee.Text != "" && txtIssueFor_Ghee.Text != "0" && txtIssueFor_Ghee.Text != "0.00")
        //    {
        //        ButterIssueForGhee = Convert.ToDecimal(txtIssueFor_Ghee.Text);
        //    }
        //    else
        //    {
        //        ButterIssueForGhee = 0;
        //    }


        //    if (txtIssueFor_Store.Text != "" && txtIssueFor_Store.Text != "0" && txtIssueFor_Store.Text != "0.00")
        //    {
        //        ButterIssueForStore = Convert.ToDecimal(txtIssueFor_Store.Text);
        //    }
        //    else
        //    {
        //        ButterIssueForStore = 0;
        //    }
        //    if (txtIssueFor_SweetButterMilk.Text != "" && txtIssueFor_SweetButterMilk.Text != "0" && txtIssueFor_SweetButterMilk.Text != "0.00")
        //    {
        //        ButterIssueForSweetButterMilk = Convert.ToDecimal(txtIssueFor_SweetButterMilk.Text);
        //    }
        //    else
        //    {
        //        ButterIssueForSweetButterMilk = 0;
        //    }


        //    if (txtCL_Closing.Text != "" && txtCL_Closing.Text != "0" && txtCL_Closing.Text != "0.00")
        //    {
        //        ButterCCClosing = Convert.ToDecimal(txtCL_Closing.Text);
        //    }
        //    else
        //    {
        //        ButterCCClosing = 0;
        //    }


        //    //if (txtLOOSE_Closing.Text != "" && txtLOOSE_Closing.Text != "0" && txtLOOSE_Closing.Text != "0.00")
        //    //{
        //    //    ButterLooseClosing = Convert.ToDecimal(txtLOOSE_Closing.Text);
        //    //}
        //    //else
        //    //{
        //    //    ButterLooseClosing = 0;
        //    //}

        //    //if (txtVE1_Closing.Text != "" && txtVE1_Closing.Text != "0" && txtVE1_Closing.Text != "0.00")
        //    //{
        //    //    ButterVE1Closing = Convert.ToDecimal(txtVE1_Closing.Text);
        //    //}
        //    //else
        //    //{
        //    //    ButterVE1Closing = 0;
        //    //}

        //    //if (txtVE2_Closing.Text != "" && txtVE2_Closing.Text != "0" && txtVE2_Closing.Text != "0.00")
        //    //{
        //    //    ButterVE2Closing = Convert.ToDecimal(txtVE2_Closing.Text);
        //    //}
        //    //else
        //    //{
        //    //    ButterVE2Closing = 0;
        //    //}


        //    OutTotal = WhiteButterMfg + TableButterMfg + CookingButterMfg + ButterIssueToProcessing + ButterIssueForGhee + ButterIssueForSweetButterMilk
        //        + ButterIssueForStore + ButterCCClosing
        //        + ButterLooseClosing + ButterVE1Closing
        //        + ButterVE2Closing;

        //    Int32 IID = Convert.ToInt32(lblItem_id.Text);
        //    DataRow dr1 = dt.AsEnumerable().Where(r => ((Int32)r["Item_id"]).Equals(IID)).First();

        //    dr1["WhiteButterMfg"] = WhiteButterMfg;
        //    dr1["TableButterMfg"] = TableButterMfg;
        //    dr1["CookingButterMfg"] = CookingButterMfg;
        //    dr1["ButterIssueToProcessing"] = ButterIssueToProcessing;
        //    dr1["ButterIssueForGhee"] = ButterIssueForGhee;
        //    dr1["ButterIssueForStore"] = ButterIssueForStore;
        //    dr1["ButterIssueForSweetButterMilk"] = ButterIssueForSweetButterMilk;
        //    dr1["ButterCCClosing"] = ButterCCClosing;
        //    dr1["ButterLooseClosing"] = ButterLooseClosing;
        //    dr1["ButterVE2Closing"] = ButterVE2Closing;
        //    dr1["ButterVE1Closing"] = ButterVE1Closing;
        //    dr1["OutTotal"] = OutTotal;


        //}
        return dt;
    }

    private DataTable GetProductVarientInfo_Inner()
    {

        DataTable dt_CC = new DataTable();
        DataRow dr_CC;

        dt_CC.Columns.Add(new DataColumn("ItemCat_id", typeof(int)));
        dt_CC.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
        dt_CC.Columns.Add(new DataColumn("MfdFrom", typeof(string)));
        dt_CC.Columns.Add(new DataColumn("QuantityInKg", typeof(decimal)));
        dt_CC.Columns.Add(new DataColumn("FatInKg", typeof(decimal)));
        dt_CC.Columns.Add(new DataColumn("SnfInKg", typeof(decimal)));

        //foreach (GridViewRow row in GVVariantDetail_CC.Rows)
        //{
        //    Label lblName = (Label)row.FindControl("lblName");
        //    Label lblQuantityInKg = (Label)row.FindControl("lblQuantityInKg");
        //    Label lblFatInKg = (Label)row.FindControl("lblFatInKg");
        //    Label lblSnfInKg = (Label)row.FindControl("lblSnfInKg");

        //    dr_CC = dt_CC.NewRow();
        //    dr_CC[0] = objdb.LooseButterItemCategoryId_ID();
        //    dr_CC[1] = objdb.LooseButterItemTypeId_ID();
        //    dr_CC[2] = lblName.Text;
        //    dr_CC[3] = lblQuantityInKg.Text;
        //    dr_CC[4] = lblFatInKg.Text;
        //    dr_CC[5] = lblSnfInKg.Text;
        //    dt_CC.Rows.Add(dr_CC);
        //}


        return dt_CC;
    }

    protected void btnYesT_P_Click1(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                DataTable dtIDF = new DataTable();
                dtIDF = GetProductVarientInfo();

                DataTable dtIDFInner = new DataTable();
                dtIDFInner = GetProductVarientInfo_Inner();

                //if (Convert.ToDecimal(lblReceiptTotal.Text) == Convert.ToDecimal(lblIssuedtotal.Text))
                //{ 
                //}
                //else
                //{
                //    lblPopupMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Receipt Milk Qty And Issued Milk Qty Can't Equal");
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF()", true);
                //}

                if (dtIDF.Rows.Count > 0)
                {
                     if(btnPopupSave_Product.Text == "Save")
                    {
                        ds = null;
                        ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut_ProductSheet",
                                                  new string[] { "flag" 
                                                ,"Office_ID"
                                                ,"Date" 
                                                ,"ProductSection_ID"
                                                ,"TotalIn"
                                                ,"TotalOut"
                                                ,"CreatedBy"
                                                ,"CreatedBy_IP"
                                                ,"ItemType_id"
                                                ,"Received_From" 
                                    },
                                                  new string[] { "12"
                                              ,objdb.Office_ID()
                                              , Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                                               ,ddlPSection.SelectedValue 
                                              ,"0"
                                              ,"0"
                                              , ViewState["Emp_ID"].ToString()
                                              ,objdb.GetLocalIPAddress()
                                              ,objdb.LooseButterItemTypeId_ID()
                                              ,txtReceived_From.Text
                                    },
                                                 new string[] { "type_Production_ProductSheetMasterChild_Butter", "type_Production_ProductSheetMasterChild_ButterInner" },
                                                 new DataTable[] { dtIDF, dtIDFInner }, "TableSave");


                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            ddlPSection_SelectedIndexChanged(sender, e);
                            //GVVariantDetail_CC.DataSource = string.Empty;
                            //GVVariantDetail_CC.DataBind();
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
                    else if(btnPopupSave_Product.Text == "Update")
                    {
                        ds = null;
                        ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut_ProductSheet",
                                                  new string[] { "flag" 
                                                ,"PPSM"                                             
                                                ,"Received_From" 
                                    },
                                                  new string[] { "19"
                                              ,ViewState["PPSM"].ToString() 
                                              ,txtReceived_From.Text
                                    },
                                                 new string[] { "type_Production_ProductSheetMasterChild_Butter", "type_Production_ProductSheetMasterChild_ButterInner" },
                                                 new DataTable[] { dtIDF, dtIDFInner }, "TableSave");


                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            ddlPSection_SelectedIndexChanged(sender, e);
                            //GVVariantDetail_CC.DataSource = string.Empty;
                            //GVVariantDetail_CC.DataBind();
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
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Table Value Can't Empty.");
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }


        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
        }

    }


}