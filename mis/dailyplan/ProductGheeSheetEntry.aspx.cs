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

public partial class mis_dailyplan_ProductGheeSheetEntry : System.Web.UI.Page
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
            //ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut_ProductSheet",
            //      new string[] { "flag", "Office_ID", "Shift_Id", "ProductSection_ID", "Date", "ItemCat_id", "ItemType_id" },
            //      new string[] { "1", ddlDS.SelectedValue, ddlShift.SelectedValue, ddlPSection.SelectedValue, Fdate, strcat_id.ToString(), objdb.LooseGheeItemTypeId_ID() }, "dataset");
            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut_ProductSheet",
                  new string[] { "flag", "Office_ID", "Shift_Id", "ProductSection_ID", "Date", "ItemCat_id", "ItemType_id" },
                  new string[] { "1", ddlDS.SelectedValue, ddlShift.SelectedValue, ddlPSection.SelectedValue, Fdate, strcat_id.ToString(), "149" }, "dataset");


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvmttos.DataSource = ds;
                gvmttos.DataBind();

                decimal PrvD_InPkt = 0;
                decimal PrvD_InLtr = 0;
                decimal CurD_InPkt = 0;
                decimal CurD_InLtr = 0;

                foreach (GridViewRow row in gvmttos.Rows)
                {

                    Label lblPrev_Demand_InPkt = (Label)row.FindControl("lblPrev_Demand_InPkt");
                    Label Prev_Demand_InPkt_F = (gvmttos.FooterRow.FindControl("Prev_Demand_InPkt_F") as Label);

                    if (lblPrev_Demand_InPkt.Text != "")
                    {
                        PrvD_InPkt += Convert.ToDecimal(lblPrev_Demand_InPkt.Text);
                        Prev_Demand_InPkt_F.Text = PrvD_InPkt.ToString("0.00");
                    }


                    Label lblPrev_DemandInLtr = (Label)row.FindControl("lblPrev_DemandInLtr");
                    Label lblPrev_DemandInLtr_F = (gvmttos.FooterRow.FindControl("lblPrev_DemandInLtr_F") as Label);


                    if (lblPrev_DemandInLtr.Text != "")
                    {
                        PrvD_InLtr += Convert.ToDecimal(lblPrev_DemandInLtr.Text);
                        lblPrev_DemandInLtr_F.Text = PrvD_InLtr.ToString("0.00");
                    }

                    Label lblCurrent_Demand_InPkt = (Label)row.FindControl("lblCurrent_Demand_InPkt");
                    Label lblCurrent_Demand_InPkt_F = (gvmttos.FooterRow.FindControl("lblCurrent_Demand_InPkt_F") as Label);


                    if (lblCurrent_Demand_InPkt.Text != "")
                    {
                        CurD_InPkt += Convert.ToDecimal(lblCurrent_Demand_InPkt.Text);
                        lblCurrent_Demand_InPkt_F.Text = CurD_InPkt.ToString("0.00");
                    }

                    Label lblCurrent_Demand_InLtr = (Label)row.FindControl("lblCurrent_Demand_InLtr");
                    Label lblCurrent_Demand_InLtr_F = (gvmttos.FooterRow.FindControl("lblCurrent_Demand_InLtr_F") as Label);


                    if (lblCurrent_Demand_InLtr.Text != "")
                    {
                        CurD_InLtr += Convert.ToDecimal(lblCurrent_Demand_InLtr.Text);
                        lblCurrent_Demand_InLtr_F.Text = CurD_InLtr.ToString("0.00");
                    }

                }


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

            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lnkbtnVN = (LinkButton)gvmttos.Rows[selRowIndex].FindControl("lnkbtnVN");
            ViewState["TypeId"] = lnkbtnVN.CommandArgument;

            lblProductName.Text = lnkbtnVN.Text;
            lbldate.Text = txtDate.Text;
            lblsection.Text = ddlPSection.SelectedItem.Text;

            lblProductNameInner.Text = lnkbtnVN.Text;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);

            // For Variant

            DataSet dsVD_Child = objdb.ByProcedure("SpProductionProduct_Milk_InOut_ProductSheet",
            new string[] { "flag", "Office_ID", "Date", "ProductSection_ID", "ItemType_id" },
            new string[] { "3", objdb.Office_ID(), Fdate, ddlPSection.SelectedValue, lnkbtnVN.CommandArgument }, "dataset");

            if (dsVD_Child != null && dsVD_Child.Tables[0].Rows.Count > 0)
            {
                GVVariantDetail_In.DataSource = dsVD_Child;
                GVVariantDetail_In.DataBind();
                GVVariantDetail_Out.DataSource = dsVD_Child;
                GVVariantDetail_Out.DataBind();
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
            lblPopupMsg.Text = "";

            foreach (GridViewRow row1 in GVVariantDetail_In.Rows)
            {

                Label lblBalance_BF = (Label)row1.FindControl("lblBalance_BF");
                TextBox txtFrom_Butter = (TextBox)row1.FindControl("txtFrom_Butter");
                TextBox txtSour_Milk = (TextBox)row1.FindControl("txtSour_Milk");
                TextBox txtCurdle_Milk = (TextBox)row1.FindControl("txtCurdle_Milk");

                if (lblBalance_BF.Text == "")
                {
                    lblBalance_BF.Text = "0";
                }

                if (txtFrom_Butter.Text == "")
                {
                    txtFrom_Butter.Text = "0";
                }

                if (txtSour_Milk.Text == "")
                {
                    txtSour_Milk.Text = "0";
                }

                if (txtCurdle_Milk.Text == "")
                {
                    txtCurdle_Milk.Text = "0";
                }

                TextBox lblIntotal = (TextBox)row1.FindControl("lblIntotal");

                lblIntotal.Text = (Convert.ToDecimal(lblBalance_BF.Text) + Convert.ToDecimal(txtFrom_Butter.Text) + Convert.ToDecimal(txtSour_Milk.Text) + Convert.ToDecimal(txtCurdle_Milk.Text)).ToString();

                if (lblIntotal.Text != "")
                {
                    btnPopupSave_Product.Enabled = true;
                }
            }


            foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
            {

                TextBox txtIssueTo_Store = (TextBox)row1.FindControl("txtIssueTo_Store");
                TextBox txtIssueTo_Plant = (TextBox)row1.FindControl("txtIssueTo_Plant");
                TextBox txtPackingLosses = (TextBox)row1.FindControl("txtPackingLosses");
                TextBox txtCLClosing = (TextBox)row1.FindControl("txtCLClosing");

                if (txtIssueTo_Store.Text == "")
                {
                    txtIssueTo_Store.Text = "0";
                }

                if (txtIssueTo_Plant.Text == "")
                {
                    txtIssueTo_Plant.Text = "0";
                }

                if (txtPackingLosses.Text == "")
                {
                    txtPackingLosses.Text = "0";
                }

                if (txtCLClosing.Text == "")
                {
                    txtCLClosing.Text = "0";
                }

                TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");

                lblouttotal.Text = (Convert.ToDecimal(txtIssueTo_Store.Text) + Convert.ToDecimal(txtIssueTo_Plant.Text) + Convert.ToDecimal(txtPackingLosses.Text) + Convert.ToDecimal(txtCLClosing.Text)).ToString();

                if (lblouttotal.Text != "")
                {
                    btnPopupSave_Product.Enabled = true;
                }
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
        }
        catch (Exception ex)
        {
            lblPopupMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
        }
    
    }

    //protected void txtFrom_Butter_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblPopupMsg.Text = "";

    //        foreach (GridViewRow row1 in GVVariantDetail_In.Rows)
    //        {

    //            Label lblBalance_BF = (Label)row1.FindControl("lblBalance_BF");
    //            TextBox txtFrom_Butter = (TextBox)row1.FindControl("txtFrom_Butter");
    //            TextBox txtSour_Milk = (TextBox)row1.FindControl("txtSour_Milk");
    //            TextBox txtCurdle_Milk = (TextBox)row1.FindControl("txtCurdle_Milk");

    //            if (lblBalance_BF.Text == "")
    //            {
    //                lblBalance_BF.Text = "0";
    //            }

    //            if (txtFrom_Butter.Text == "")
    //            {
    //                txtFrom_Butter.Text = "0";
    //            }

    //            if (txtSour_Milk.Text == "")
    //            {
    //                txtSour_Milk.Text = "0";
    //            }

    //            if (txtCurdle_Milk.Text == "")
    //            {
    //                txtCurdle_Milk.Text = "0";
    //            }

    //            TextBox lblIntotal = (TextBox)row1.FindControl("lblIntotal");

    //            lblIntotal.Text = (Convert.ToDecimal(lblBalance_BF.Text) + Convert.ToDecimal(txtFrom_Butter.Text) + Convert.ToDecimal(txtSour_Milk.Text) + Convert.ToDecimal(txtCurdle_Milk.Text)).ToString();

    //            if (lblIntotal.Text != "")
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

    //protected void txtSour_Milk_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblPopupMsg.Text = "";

    //        foreach (GridViewRow row1 in GVVariantDetail_In.Rows)
    //        {

    //            Label lblBalance_BF = (Label)row1.FindControl("lblBalance_BF");
    //            TextBox txtFrom_Butter = (TextBox)row1.FindControl("txtFrom_Butter");
    //            TextBox txtSour_Milk = (TextBox)row1.FindControl("txtSour_Milk");
    //            TextBox txtCurdle_Milk = (TextBox)row1.FindControl("txtCurdle_Milk");

    //            if (lblBalance_BF.Text == "")
    //            {
    //                lblBalance_BF.Text = "0";
    //            }

    //            if (txtFrom_Butter.Text == "")
    //            {
    //                txtFrom_Butter.Text = "0";
    //            }

    //            if (txtSour_Milk.Text == "")
    //            {
    //                txtSour_Milk.Text = "0";
    //            }

    //            if (txtCurdle_Milk.Text == "")
    //            {
    //                txtCurdle_Milk.Text = "0";
    //            }

    //            TextBox lblIntotal = (TextBox)row1.FindControl("lblIntotal");

    //            lblIntotal.Text = (Convert.ToDecimal(lblBalance_BF.Text) + Convert.ToDecimal(txtFrom_Butter.Text) + Convert.ToDecimal(txtSour_Milk.Text) + Convert.ToDecimal(txtCurdle_Milk.Text)).ToString();

    //            if (lblIntotal.Text != "")
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

    //protected void txtCurdle_Milk_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblPopupMsg.Text = "";

    //        foreach (GridViewRow row1 in GVVariantDetail_In.Rows)
    //        {

    //            Label lblBalance_BF = (Label)row1.FindControl("lblBalance_BF");
    //            TextBox txtFrom_Butter = (TextBox)row1.FindControl("txtFrom_Butter");
    //            TextBox txtSour_Milk = (TextBox)row1.FindControl("txtSour_Milk");
    //            TextBox txtCurdle_Milk = (TextBox)row1.FindControl("txtCurdle_Milk");

    //            if (lblBalance_BF.Text == "")
    //            {
    //                lblBalance_BF.Text = "0";
    //            }

    //            if (txtFrom_Butter.Text == "")
    //            {
    //                txtFrom_Butter.Text = "0";
    //            }

    //            if (txtSour_Milk.Text == "")
    //            {
    //                txtSour_Milk.Text = "0";
    //            }

    //            if (txtCurdle_Milk.Text == "")
    //            {
    //                txtCurdle_Milk.Text = "0";
    //            }

    //            TextBox lblIntotal = (TextBox)row1.FindControl("lblIntotal");

    //            lblIntotal.Text = (Convert.ToDecimal(lblBalance_BF.Text) + Convert.ToDecimal(txtFrom_Butter.Text) + Convert.ToDecimal(txtSour_Milk.Text) + Convert.ToDecimal(txtCurdle_Milk.Text)).ToString();

    //            if (lblIntotal.Text != "")
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

    //protected void txtIssueTo_Store_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblPopupMsg.Text = "";

    //        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
    //        {

    //            TextBox txtIssueTo_Store = (TextBox)row1.FindControl("txtIssueTo_Store");
    //            TextBox txtIssueTo_Plant = (TextBox)row1.FindControl("txtIssueTo_Plant");
    //            TextBox txtPackingLosses = (TextBox)row1.FindControl("txtPackingLosses");
    //            TextBox txtCLClosing = (TextBox)row1.FindControl("txtCLClosing");

    //            if (txtIssueTo_Store.Text == "")
    //            {
    //                txtIssueTo_Store.Text = "0";
    //            }

    //            if (txtIssueTo_Plant.Text == "")
    //            {
    //                txtIssueTo_Plant.Text = "0";
    //            }

    //            if (txtPackingLosses.Text == "")
    //            {
    //                txtPackingLosses.Text = "0";
    //            }

    //            if (txtCLClosing.Text == "")
    //            {
    //                txtCLClosing.Text = "0";
    //            }

    //            TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");

    //            lblouttotal.Text = (Convert.ToDecimal(txtIssueTo_Store.Text) + Convert.ToDecimal(txtIssueTo_Plant.Text) + Convert.ToDecimal(txtPackingLosses.Text) + Convert.ToDecimal(txtCLClosing.Text)).ToString();

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

    //protected void txtIssueTo_Plant_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblPopupMsg.Text = "";

    //        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
    //        {

    //            TextBox txtIssueTo_Store = (TextBox)row1.FindControl("txtIssueTo_Store");
    //            TextBox txtIssueTo_Plant = (TextBox)row1.FindControl("txtIssueTo_Plant");
    //            TextBox txtPackingLosses = (TextBox)row1.FindControl("txtPackingLosses");
    //            TextBox txtCLClosing = (TextBox)row1.FindControl("txtCLClosing");

    //            if (txtIssueTo_Store.Text == "")
    //            {
    //                txtIssueTo_Store.Text = "0";
    //            }

    //            if (txtIssueTo_Plant.Text == "")
    //            {
    //                txtIssueTo_Plant.Text = "0";
    //            }

    //            if (txtPackingLosses.Text == "")
    //            {
    //                txtPackingLosses.Text = "0";
    //            }

    //            if (txtCLClosing.Text == "")
    //            {
    //                txtCLClosing.Text = "0";
    //            }

    //            TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");

    //            lblouttotal.Text = (Convert.ToDecimal(txtIssueTo_Store.Text) + Convert.ToDecimal(txtIssueTo_Plant.Text) + Convert.ToDecimal(txtPackingLosses.Text) + Convert.ToDecimal(txtCLClosing.Text)).ToString();

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

    //protected void txtPackingLosses_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblPopupMsg.Text = "";

    //        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
    //        {

    //            TextBox txtIssueTo_Store = (TextBox)row1.FindControl("txtIssueTo_Store");
    //            TextBox txtIssueTo_Plant = (TextBox)row1.FindControl("txtIssueTo_Plant");
    //            TextBox txtPackingLosses = (TextBox)row1.FindControl("txtPackingLosses");
    //            TextBox txtCLClosing = (TextBox)row1.FindControl("txtCLClosing");

    //            if (txtIssueTo_Store.Text == "")
    //            {
    //                txtIssueTo_Store.Text = "0";
    //            }

    //            if (txtIssueTo_Plant.Text == "")
    //            {
    //                txtIssueTo_Plant.Text = "0";
    //            }

    //            if (txtPackingLosses.Text == "")
    //            {
    //                txtPackingLosses.Text = "0";
    //            }

    //            if (txtCLClosing.Text == "")
    //            {
    //                txtCLClosing.Text = "0";
    //            }

    //            TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");

    //            lblouttotal.Text = (Convert.ToDecimal(txtIssueTo_Store.Text) + Convert.ToDecimal(txtIssueTo_Plant.Text) + Convert.ToDecimal(txtPackingLosses.Text) + Convert.ToDecimal(txtCLClosing.Text)).ToString();

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

    //            TextBox txtIssueTo_Store = (TextBox)row1.FindControl("txtIssueTo_Store");
    //            TextBox txtIssueTo_Plant = (TextBox)row1.FindControl("txtIssueTo_Plant");
    //            TextBox txtPackingLosses = (TextBox)row1.FindControl("txtPackingLosses");
    //            TextBox txtCLClosing = (TextBox)row1.FindControl("txtCLClosing");

    //            if (txtIssueTo_Store.Text == "")
    //            {
    //                txtIssueTo_Store.Text = "0";
    //            }

    //            if (txtIssueTo_Plant.Text == "")
    //            {
    //                txtIssueTo_Plant.Text = "0";
    //            }

    //            if (txtPackingLosses.Text == "")
    //            {
    //                txtPackingLosses.Text = "0";
    //            }

    //            if (txtCLClosing.Text == "")
    //            {
    //                txtCLClosing.Text = "0";
    //            }

    //            TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");

    //            lblouttotal.Text = (Convert.ToDecimal(txtIssueTo_Store.Text) + Convert.ToDecimal(txtIssueTo_Plant.Text) + Convert.ToDecimal(txtPackingLosses.Text) + Convert.ToDecimal(txtCLClosing.Text)).ToString();

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

    private DataTable GetProductVarientInfo()
    {

        decimal Opening = 0;
        decimal From_Butter = 0;
        decimal Sour_Milk = 0;
        decimal Curdle_Milk = 0;

        decimal IssueTo_Store = 0;
        decimal IssueTo_Plant = 0;
        decimal PackingLosses = 0;
        decimal Closing = 0;

        decimal InTotal = 0;
        decimal OutTotal = 0;

        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("ItemCat_id", typeof(int)));
        dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
        dt.Columns.Add(new DataColumn("Item_id", typeof(int)));
        dt.Columns.Add(new DataColumn("Opening", typeof(decimal)));
        dt.Columns.Add(new DataColumn("From_Butter", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Sour_Milk", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Curdle_Milk", typeof(decimal)));
        dt.Columns.Add(new DataColumn("BatchNo", typeof(string)));
        dt.Columns.Add(new DataColumn("IssueTo_Store", typeof(decimal)));
        dt.Columns.Add(new DataColumn("IssueTo_Plant", typeof(decimal)));
        dt.Columns.Add(new DataColumn("PackingLosses", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Closing", typeof(decimal)));

        dt.Columns.Add(new DataColumn("InTotal", typeof(decimal)));
        dt.Columns.Add(new DataColumn("OutTotal", typeof(decimal)));

        foreach (GridViewRow row in GVVariantDetail_In.Rows)
        {
            Label lblItem_id = (Label)row.FindControl("lblItem_id");

            Label lblBalance_BF = (Label)row.FindControl("lblBalance_BF");
            TextBox txtFrom_Butter = (TextBox)row.FindControl("txtFrom_Butter");
            TextBox txtSour_Milk = (TextBox)row.FindControl("txtSour_Milk");
            TextBox txtCurdle_Milk = (TextBox)row.FindControl("txtCurdle_Milk");
            TextBox txtBatchNo = (TextBox)row.FindControl("txtBatchNo");

            if (lblBalance_BF.Text != "" && lblBalance_BF.Text != "0" && lblBalance_BF.Text != "0.00")
            {
                Opening = Convert.ToDecimal(lblBalance_BF.Text);
            }

            else
            {
                Opening = 0;
            }


            if (txtFrom_Butter.Text != "" && txtFrom_Butter.Text != "0" && txtFrom_Butter.Text != "0.00")
            {
                From_Butter = Convert.ToDecimal(txtFrom_Butter.Text);
            }
            else
            {
                From_Butter = 0;
            }


            if (txtSour_Milk.Text != "" && txtSour_Milk.Text != "0" && txtSour_Milk.Text != "0.00")
            {
                Sour_Milk = Convert.ToDecimal(txtSour_Milk.Text);
            }
            else
            {
                Sour_Milk = 0;
            }



            if (txtCurdle_Milk.Text != "" && txtCurdle_Milk.Text != "0" && txtCurdle_Milk.Text != "0.00")
            {
                Curdle_Milk = Convert.ToDecimal(txtCurdle_Milk.Text);
            }
            else
            {
                Curdle_Milk = 0;
            }

            InTotal = Convert.ToDecimal(lblBalance_BF.Text) + Convert.ToDecimal(txtFrom_Butter.Text) + Convert.ToDecimal(txtSour_Milk.Text) + Convert.ToDecimal(txtCurdle_Milk.Text);

            dr = dt.NewRow();
            dr[0] = 2;
            dr[1] = ViewState["TypeId"].ToString();
            dr[2] = lblItem_id.Text;
            dr[3] = Opening;
            dr[4] = From_Butter;
            dr[5] = Sour_Milk;
            dr[6] = Curdle_Milk;
            dr[7] = txtBatchNo.Text;
            dr[8] = IssueTo_Store;
            dr[9] = IssueTo_Plant;
            dr[10] = PackingLosses;
            dr[11] = Closing;
            dr[12] = InTotal;
            dr[13] = OutTotal;
            dt.Rows.Add(dr);


        }

        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
        {
            Label lblItem_id = (Label)row1.FindControl("lblItem_id_Out");
            TextBox txtIssueTo_Store = (TextBox)row1.FindControl("txtIssueTo_Store");
            TextBox txtIssueTo_Plant = (TextBox)row1.FindControl("txtIssueTo_Plant");
            TextBox txtPackingLosses = (TextBox)row1.FindControl("txtPackingLosses");
            TextBox txtCLClosing = (TextBox)row1.FindControl("txtCLClosing");

            if (txtIssueTo_Store.Text != "0" && txtIssueTo_Store.Text != "0.00")
            {
                IssueTo_Store = Convert.ToDecimal(txtIssueTo_Store.Text);
            }
            else
            {
                IssueTo_Store = 0;
            }

            if (txtIssueTo_Plant.Text != "0" && txtIssueTo_Plant.Text != "0.00")
            {
                IssueTo_Plant = Convert.ToDecimal(txtIssueTo_Plant.Text);
            }
            else
            {
                IssueTo_Plant = 0;
            }


            if (txtPackingLosses.Text != "0" && txtPackingLosses.Text != "0.00")
            {
                PackingLosses = Convert.ToDecimal(txtPackingLosses.Text);
            }
            else
            {
                PackingLosses = 0;
            }


            if (txtCLClosing.Text != "0" && txtCLClosing.Text != "0.00")
            {
                Closing = Convert.ToDecimal(txtCLClosing.Text);
            }
            else
            {
                Closing = 0;
            }

            OutTotal = Convert.ToDecimal(txtIssueTo_Store.Text) + Convert.ToDecimal(txtIssueTo_Plant.Text) + Convert.ToDecimal(txtPackingLosses.Text) + Convert.ToDecimal(txtCLClosing.Text);

            Int32 IID = Convert.ToInt32(lblItem_id.Text);
            DataRow dr1 = dt.AsEnumerable().Where(r => ((Int32)r["Item_id"]).Equals(IID)).First();

            dr1["IssueTo_Store"] = IssueTo_Store;
            dr1["IssueTo_Plant"] = IssueTo_Plant;
            dr1["PackingLosses"] = PackingLosses;
            dr1["Closing"] = Closing;
            dr1["OutTotal"] = OutTotal;


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
                                              new string[] { "7"
                                              ,objdb.Office_ID()
                                              , Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                                               ,ddlPSection.SelectedValue 
                                              ,"0"
                                              ,"0"
                                              , ViewState["Emp_ID"].ToString()
                                              ,objdb.GetLocalIPAddress()
                                              ,ViewState["TypeId"].ToString()
                                              ,txtReceived_From.Text
                                    },
                                             new string[] { "type_Production_ProductSheetMasterChild_Ghee" },
                                             new DataTable[] { dtIDF }, "TableSave");


                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblPopupMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
                        txtReceived_From.Text = "";

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

    protected void lbviewsheet_Click(object sender, EventArgs e)
    {
        string VariantSize = "";
        decimal TotalLtr = 0;

        if (txtDate.Text != "")
        {
            Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
        }

        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
        LinkButton lbviewsheet = (LinkButton)gvmttos.Rows[selRowIndex].FindControl("lbviewsheet");
        Label lblItemType_id = (Label)gvmttos.Rows[selRowIndex].FindControl("lblItemType_id");

        // For Variant Rpt

        DataSet dsVD_Child_Rpt = objdb.ByProcedure("SpProductionProduct_Milk_InOut_ProductSheet",
        new string[] { "flag", "Office_ID", "Date", "ProductSection_ID", "ItemType_id", "PPSM" },
        new string[] { "8", objdb.Office_ID(), Fdate, ddlPSection.SelectedValue, lblItemType_id.Text, lbviewsheet.CommandArgument }, "dataset");

        string OpeningTotal = "0";
        string From_Butter = "0";
        string Sour_Milk = "0";
        string Curdle_Milk = "0";
        string IssueTo_Store = "0";
        string IssueTo_Plant = "0";
        string PackingLosses = "0";
        string Closing = "0";

        if (dsVD_Child_Rpt != null && dsVD_Child_Rpt.Tables[0].Rows.Count > 0)
        {

            if (dsVD_Child_Rpt != null && dsVD_Child_Rpt.Tables[1].Rows.Count > 0)
            {
                OpeningTotal = dsVD_Child_Rpt.Tables[1].Rows[0]["Opening"].ToString();
                From_Butter = dsVD_Child_Rpt.Tables[1].Rows[0]["From_Butter"].ToString();
                Sour_Milk = dsVD_Child_Rpt.Tables[1].Rows[0]["Sour_Milk"].ToString();
                Curdle_Milk = dsVD_Child_Rpt.Tables[1].Rows[0]["Curdle_Milk"].ToString();
                IssueTo_Store = dsVD_Child_Rpt.Tables[1].Rows[0]["IssueTo_Store"].ToString();
                IssueTo_Plant = dsVD_Child_Rpt.Tables[1].Rows[0]["IssueTo_Plant"].ToString();
                PackingLosses = dsVD_Child_Rpt.Tables[1].Rows[0]["PackingLosses"].ToString();
                Closing = dsVD_Child_Rpt.Tables[1].Rows[0]["Closing"].ToString();
            }

            string strTypeName = dsVD_Child_Rpt.Tables[0].Rows[0]["ItemTypeName"].ToString();
            lblTopTitle.Text = strTypeName;

            int Count = dsVD_Child_Rpt.Tables[0].Rows.Count;
            StringBuilder htmlStr = new StringBuilder();

            htmlStr.Append("<div class='row'>");
            htmlStr.Append("<div class='col-md-12' style='text-align:center'><h3 >"+Session["Office_Name"].ToString()+"</h3><h5>DAIRY PLANT</h5><h5>DAILY GHEE ACCOUNT SHEET</h5></div>");
            htmlStr.Append("<div class='col-md-12'></div>");

            /********** Table Start******/
            htmlStr.Append("<div class='row'>");
            htmlStr.Append("<div class='col-md-12'>");
            htmlStr.Append("<table class='datatable table table-striped table-bordered table-hover' style='width:100%'>");
            htmlStr.Append("<tr class='text-center'>");
            htmlStr.Append("<th rowspan='2' style='width:18%'>PARTICULAR</th>");
            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<th > " + dsVD_Child_Rpt.Tables[0].Rows[i]["VariantSize"].ToString() + "</th>");
            }
            htmlStr.Append("<th rowspan='2'>Total(Liter)</th>");
            htmlStr.Append("</tr>");
            htmlStr.Append("<tr class='text-center'>");

            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<th>Qty</th>");
            }
            htmlStr.Append("</tr>");
            htmlStr.Append("<tr>");
            htmlStr.Append("<td>B.F.</td>");

            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<td> " + dsVD_Child_Rpt.Tables[0].Rows[i]["Opening"].ToString() + "</td>");

            }
            htmlStr.Append("<td>" + OpeningTotal + "</td>");
            htmlStr.Append("</tr>");
            htmlStr.Append("<tr>");
            htmlStr.Append("<td style='width:18%'>Manufactured From:.</td>");

            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<td></td>");
            }
            htmlStr.Append("<td></td>");
            htmlStr.Append("</tr>");
            htmlStr.Append("<tr>");

            htmlStr.Append("<td style='width:18%'>A) Butter</td>");

            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["From_Butter"].ToString() + "</td>");

                VariantSize = dsVD_Child_Rpt.Tables[0].Rows[i]["VariantSize"].ToString();

            }
            htmlStr.Append("<td>" + From_Butter + "</td>");
            htmlStr.Append("</tr>");
            htmlStr.Append("<tr>");

            htmlStr.Append("<td style='width:18%'>B) Sour Milk</td>");

            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Sour_Milk"].ToString() + "</td>");

            }
            htmlStr.Append("<td>" + Sour_Milk + "</td>");
            htmlStr.Append("</tr>");
            htmlStr.Append("<tr>");

            htmlStr.Append("<td style='width:18%'>C) Curdle Milk</td>");

            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Curdle_Milk"].ToString() + "</td>");
            }
            htmlStr.Append("<td>" + Curdle_Milk + "</td>");
            htmlStr.Append("</tr>");
            //htmlStr.Append("<tr>");
            //htmlStr.Append("<td style='width:18%'>Received From .....</td>");
            //htmlStr.Append("<td colspan='8'>" + dsVD_Child_Rpt.Tables[0].Rows[0]["Received_From"].ToString() + "</td>");

            //htmlStr.Append("</tr>");
            htmlStr.Append("<tr>");

            htmlStr.Append("<td style='width:18%'>Batch No</td>");

            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["BatchNo"].ToString() + "</td>");
            }
            htmlStr.Append("<td></td>");
            htmlStr.Append("</tr>");
            htmlStr.Append("<tr style='background-color:#ff874c; border:1px solid #ff874c; color:#000; font-weight:700' >");

            htmlStr.Append("<td style='width:18%'>TOTAL</th>");
            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["InTotal"].ToString() + "</td>");
            }
            htmlStr.Append("<td> " + (Convert.ToDecimal(OpeningTotal) + Convert.ToDecimal(From_Butter) + Convert.ToDecimal(Sour_Milk) + Convert.ToDecimal(Curdle_Milk)) + "</td>");
            htmlStr.Append("</tr>");
            htmlStr.Append("<tr>");
            htmlStr.Append("<td style='width:18%'>Issued To Store</td>");

            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["IssueTo_Store"].ToString() + "</td>");
            }
            htmlStr.Append("<td>" + IssueTo_Store + "</td>");
            htmlStr.Append("</tr>");
            htmlStr.Append("<tr>");
            htmlStr.Append("<td style='width:18%'>Issued To Plant</td>");

            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["IssueTo_Plant"].ToString() + "</td>");
            }
            htmlStr.Append("<td>" + IssueTo_Plant + "</td>");
            htmlStr.Append("</tr>");
            htmlStr.Append("<tr>");
            htmlStr.Append("<td style='width:18%'>Packing Losses</td>");

            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["PackingLosses"].ToString() + "</td>");
            }
            htmlStr.Append("<td>" + PackingLosses + "</td>");
            htmlStr.Append("</tr>");
            htmlStr.Append("<tr>");
            htmlStr.Append("<td style='width:18%'>C.B.</td>");

            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Closing"].ToString() + "</td>");
            }
            htmlStr.Append("<td>" + Closing + "</td>");
            htmlStr.Append("</tr>");
            htmlStr.Append("<tr style='background-color:#ff874c; border:1px solid #ff874c; color:#000; font-weight:700' >");

            htmlStr.Append("<td style='width:18%'>TOTAL</th>");
            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["OutTotal"].ToString() + "</td>");
            }
            htmlStr.Append("<td>" + (Convert.ToDecimal(IssueTo_Plant) + Convert.ToDecimal(IssueTo_Store) + Convert.ToDecimal(Closing) + Convert.ToDecimal(PackingLosses)) + "</td>");
            htmlStr.Append("</tr>");
            htmlStr.Append("</table>");
            htmlStr.Append("</div></div>");
            htmlStr.Append("</div></div>");
            htmlStr.Append("<div class='col-md-12 footerprint'><div class='col-md-4 col-sm-4 col-xs-4'><h5>D.G.M. Production</h5></div><div class='col-md-6  col-sm-6 col-xs-6 text-center'><h5>Manager(Product)</h5></div><div class='col-md-2  col-sm-2 col-xs-2'><h5>A.G.M. Production</h5></div></div>");
            htmlStr.Append("</div>");
            DivTable.InnerHtml = htmlStr.ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product_Rpt()", true);

        }

    }


}