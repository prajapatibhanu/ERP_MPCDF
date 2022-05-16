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


public partial class mis_dailyplan_ProductProductionEntry : System.Web.UI.Page
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
            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut_ProductSheet",
                  new string[] { "flag", "Office_ID", "Shift_Id", "ProductSection_ID", "Date", "ItemCat_id", "ItemType_id" },
                  new string[] { "1", ddlDS.SelectedValue, ddlShift.SelectedValue, ddlPSection.SelectedValue, Fdate, strcat_id.ToString(),"0" }, "dataset");


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
                if (lnkbtnVN.CommandArgument.ToString() == "20")
                {
                    GVVariantDetail_Out.Columns[3].Visible = true;
                }
                else
                {
                    GVVariantDetail_Out.Columns[3].Visible = false;
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

    private DataTable GetProductVarientInfo()
    {

        decimal Opening = 0;
        decimal Prepared = 0;
        decimal ReturnQty = 0;
        decimal Sale = 0;
        decimal Testing = 0;
        decimal Issueforkheer = 0;
        decimal Discarded = 0;
        decimal Closing = 0;
        decimal InTotal = 0;
        decimal OutTotal = 0;

        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("ItemCat_id", typeof(int)));
        dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
        dt.Columns.Add(new DataColumn("Item_id", typeof(int)));
        dt.Columns.Add(new DataColumn("Opening", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Prepared", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ReturnQty", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Sale", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Testing", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Issueforkheer", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Discarded", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Closing", typeof(decimal)));
        dt.Columns.Add(new DataColumn("InTotal", typeof(decimal)));
        dt.Columns.Add(new DataColumn("OutTotal", typeof(decimal)));

        foreach (GridViewRow row in GVVariantDetail_In.Rows)
        {
            Label lblItem_id = (Label)row.FindControl("lblItem_id");
            Label lblBalance_BF = (Label)row.FindControl("lblBalance_BF");
            TextBox txtPrepared = (TextBox)row.FindControl("txtPrepared");
            TextBox txtReturn = (TextBox)row.FindControl("txtReturn");
            
            if (lblBalance_BF.Text != "0" && lblBalance_BF.Text != "0.00")
            {
                Opening = Convert.ToDecimal(lblBalance_BF.Text);
            }
            else
            {
                Opening = 0;
            }

            if (txtPrepared.Text != "0" && txtPrepared.Text != "0.00")
            {
                Prepared = Convert.ToDecimal(txtPrepared.Text);
            }
            else
            {
                Prepared = 0;
            }

            if (txtReturn.Text != "0" && txtReturn.Text != "0.00")
            {
                ReturnQty = Convert.ToDecimal(txtReturn.Text);
            }
            else
            {
                ReturnQty = 0;
            }

            InTotal = Convert.ToDecimal(lblBalance_BF.Text) + Convert.ToDecimal(txtPrepared.Text) + Convert.ToDecimal(txtReturn.Text);

            dr = dt.NewRow();
            dr[0] = 2;
            dr[1] = ViewState["TypeId"].ToString();
            dr[2] = lblItem_id.Text;
            dr[3] = Opening;
            dr[4] = Prepared;
            dr[5] = ReturnQty;
            dr[6] = Sale;
            dr[7] = Testing;
            dr[8] = Issueforkheer;
            dr[9] = Discarded;
            dr[10] = Closing;
            dr[11] = InTotal;
            dr[12] = OutTotal;
            dt.Rows.Add(dr);

        }

        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
        {
            Label lblItem_id = (Label)row1.FindControl("lblItem_id");
            TextBox txtSale = (TextBox)row1.FindControl("txtSale");
            TextBox txtTesting = (TextBox)row1.FindControl("txtTesting");
            TextBox txtDiscarded = (TextBox)row1.FindControl("txtDiscarded");
            TextBox txtCLClosing = (TextBox)row1.FindControl("txtCLClosing");
            TextBox txtIssueforkheer = (TextBox)row1.FindControl("txtIssueforkheer");
            if(txtIssueforkheer.Text == "")
            {
                txtIssueforkheer.Text = "0";
            }
            if (ViewState["TypeId"].ToString() == "20")
            {
                if (txtIssueforkheer.Text != "0" && txtIssueforkheer.Text != "0.00")
                {
                    Issueforkheer = Convert.ToDecimal(txtIssueforkheer.Text);
                }
                else
                {
                    Issueforkheer = 0;
                }
            }

            if (txtSale.Text != "0" && txtSale.Text != "0.00")
            {
                Sale = Convert.ToDecimal(txtSale.Text);
            }
            else
            {
                Sale = 0;
            }

            if (txtTesting.Text != "0" && txtTesting.Text != "0.00")
            {
                Testing = Convert.ToDecimal(txtTesting.Text);
            }
            else
            {
                Testing = 0;
            }

            if (txtDiscarded.Text != "0" && txtDiscarded.Text != "0.00")
            {
                Discarded = Convert.ToDecimal(txtDiscarded.Text);
            }
            else
            {
                Discarded = 0;
            }

            if (txtCLClosing.Text != "0" && txtCLClosing.Text != "0.00")
            {
                Closing = Convert.ToDecimal(txtCLClosing.Text);
            }
            else
            {
                Closing = 0;
            }

            OutTotal = Convert.ToDecimal(txtSale.Text) + Convert.ToDecimal(txtTesting.Text) + Convert.ToDecimal(txtDiscarded.Text) + Convert.ToDecimal(txtCLClosing.Text) + Convert.ToDecimal(txtIssueforkheer.Text)  ;

            Int32 IID = Convert.ToInt32(lblItem_id.Text);
            DataRow dr1 = dt.AsEnumerable().Where(r => ((Int32)r["Item_id"]).Equals(IID)).First();
            dr1["Sale"] = Sale;
            dr1["Testing"] = Testing;
            dr1["Discarded"] = Discarded;
            dr1["Issueforkheer"] = Issueforkheer;
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
                                    },
                                              new string[] { "4"
                                              ,objdb.Office_ID()
                                              , Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                                               ,ddlPSection.SelectedValue 
                                              ,"0"
                                              ,"0"
                                              , ViewState["Emp_ID"].ToString()
                                              ,objdb.GetLocalIPAddress()
                                              ,ViewState["TypeId"].ToString()
                                    },
                                             new string[] { "type_Production_ProductSheetMasterChild" },
                                             new DataTable[] { dtIDF }, "TableSave");


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


    protected void btnGetTotal_Click(object sender, EventArgs e)
    {
        try
        {
            lblPopupMsg.Text = "";

            foreach (GridViewRow row in GVVariantDetail_In.Rows)
            {
                Label lblBalance_BF = (Label)row.FindControl("lblBalance_BF");
                TextBox txtPrepared = (TextBox)row.FindControl("txtPrepared");
                TextBox txtReturn = (TextBox)row.FindControl("txtReturn");

                if (lblBalance_BF.Text == "")
                {
                    lblBalance_BF.Text = "0";
                }
                if (txtPrepared.Text == "")
                {
                    txtPrepared.Text = "0";
                }
                if (txtReturn.Text == "")
                {
                    txtReturn.Text = "0";
                }
                TextBox lblIntotal = (TextBox)row.FindControl("lblIntotal");
                lblIntotal.Text = (Convert.ToDecimal(lblBalance_BF.Text) + Convert.ToDecimal(txtPrepared.Text) + Convert.ToDecimal(txtReturn.Text)).ToString();

                if (lblIntotal.Text != "")
                {
                    btnPopupSave_Product.Enabled = true;
                }

            }



            foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
            {
                TextBox txtSale = (TextBox)row1.FindControl("txtSale");
                TextBox txtTesting = (TextBox)row1.FindControl("txtTesting");
                TextBox txtDiscarded = (TextBox)row1.FindControl("txtDiscarded");
                TextBox txtIssueforkheer = (TextBox)row1.FindControl("txtIssueforkheer");
                TextBox txtCLClosing = (TextBox)row1.FindControl("txtCLClosing");

                if (txtSale.Text == "")
                {
                    txtSale.Text = "0";
                }

                if (txtTesting.Text == "")
                {
                    txtTesting.Text = "0";
                }

                if (txtDiscarded.Text == "")
                {
                    txtDiscarded.Text = "0";
                }
                if (txtIssueforkheer.Text == "")
                {
                    txtIssueforkheer.Text = "0";
                }
                if (txtCLClosing.Text == "")
                {
                    txtCLClosing.Text = "0";
                }

                TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");
                lblouttotal.Text = (Convert.ToDecimal(txtSale.Text) + Convert.ToDecimal(txtTesting.Text) + Convert.ToDecimal(txtDiscarded.Text) + Convert.ToDecimal(txtCLClosing.Text) + Convert.ToDecimal(txtIssueforkheer.Text)).ToString();
               
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

    protected void lbviewsheet_Click(object sender, EventArgs e)
    {


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
        new string[] { "5", objdb.Office_ID(), Fdate, ddlPSection.SelectedValue, lblItemType_id.Text, lbviewsheet.CommandArgument }, "dataset");

        if (dsVD_Child_Rpt != null && dsVD_Child_Rpt.Tables[0].Rows.Count > 0)
        {
            string strTypeName = dsVD_Child_Rpt.Tables[0].Rows[0]["ItemTypeName"].ToString();
            lblTopTitle.Text = strTypeName;

            int Count = dsVD_Child_Rpt.Tables[0].Rows.Count;
            StringBuilder htmlStr = new StringBuilder();
            htmlStr.Append("<table class='table table-bordered'>");
            htmlStr.Append("<tr class=class='text-center'>");
            htmlStr.Append("<th colspan='10'>" + strTypeName + "</th>");
            htmlStr.Append("</tr>");
            htmlStr.Append("<tr>");
            htmlStr.Append("<th></th>");
            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<th>" + dsVD_Child_Rpt.Tables[0].Rows[i]["VariantSize"].ToString() + "</th>");
            }

            htmlStr.Append("<th></th>");
            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<th>" + dsVD_Child_Rpt.Tables[0].Rows[i]["VariantSize"].ToString() + "</th>");
            }
            htmlStr.Append("</tr>");
            htmlStr.Append("<tr>");
            htmlStr.Append("<th style='width:12%;'>Balance-B/F</th>");
            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["LastDayClosingBalance"].ToString() + "</td>");
            }

            htmlStr.Append("<th style='width:12%;'>Sale</th>");
            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Sale"].ToString() + "</td>");
            }

            htmlStr.Append("</tr>");
            htmlStr.Append("<tr>");
            htmlStr.Append("<th style='width:12%;'>Prepared</th>");
            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Prepared"].ToString() + "</td>");
            }

            htmlStr.Append("<th style='width:12%;'>Testing</th>");
            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Testing"].ToString() + "</td>");
            }

            htmlStr.Append("</tr>");
            htmlStr.Append("<tr>");
            htmlStr.Append("<th style='width:12%;'>Return</th>");
            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["ReturnQty"].ToString() + "</td>");
            }
            if (lblItemType_id.Text == "20")
            {
                htmlStr.Append("<th style='width:12%;'>Issue For Kheer</th>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["IssueForKheer"].ToString() + "</td>");
                }
                htmlStr.Append("</tr>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<th style='width:12%;'></th>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td></td>");
                }
                htmlStr.Append("<th style='width:12%;'>Discarded</th>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Discarded"].ToString() + "</td>");
                }
                htmlStr.Append("</tr>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<th style='width:12%;'></th>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td></td>");
                }
                htmlStr.Append("<th style='width:12%;'>CL.Closing</th>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Closing"].ToString() + "</td>");
                }

                htmlStr.Append("</tr>");
            }
            else
            {
                htmlStr.Append("<th style='width:12%;'>Discarded</th>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Discarded"].ToString() + "</td>");
                }

                htmlStr.Append("</tr>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<th style='width:12%;'></th>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td></td>");
                }

                htmlStr.Append("<th style='width:12%;'>CL.Closing</th>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Closing"].ToString() + "</td>");
                }

            }
          
           htmlStr.Append("</tr>");
            htmlStr.Append("<tr>");
            htmlStr.Append("<th style='width:12%;'>Total</th>");
            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<td><b>" + dsVD_Child_Rpt.Tables[0].Rows[i]["InTotal"].ToString() + "</b></td>");
            }

            htmlStr.Append("<th style='width:12%;'>Total</th>");
            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<td><b>" + dsVD_Child_Rpt.Tables[0].Rows[i]["OutTotal"].ToString() + "</b></td>");
            }

            htmlStr.Append("</tr>");
            htmlStr.Append("</table>");
            DivTable.InnerHtml = htmlStr.ToString();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product_Rpt()", true);

        }

    }
     
}