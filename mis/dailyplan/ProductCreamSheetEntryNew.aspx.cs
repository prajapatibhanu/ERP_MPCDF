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


public partial class mis_dailyplan_ProductCreamSheetEntryNew : System.Web.UI.Page
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
                    if (Session["IsSuccess"] != null)
                    {
                        if ((Boolean)Session["IsSuccess"] == true)
                        {
                            Session["IsSuccess"] = false;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                        }
                    }

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
			ViewState["CreamSheet_ID"] = "";
            btnPopupSave_Product.Text = "Save";

            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            // For Variant

            DataSet dsVD_Child = objdb.ByProcedure("Sp_Production_CreamSheet",
            new string[] { "flag", "Office_ID", "Date", "ProductSection_ID", "ItemType_id" },
            new string[] { "1", objdb.Office_ID(), Fdate, ddlPSection.SelectedValue, objdb.LooseCreamItemTypeId_ID() }, "dataset");

            if (dsVD_Child != null && dsVD_Child.Tables[0].Rows.Count > 0)
            {
                GVVariantDetail_In.DataSource = dsVD_Child;
                GVVariantDetail_In.DataBind();

                GVVariantDetail_Out.DataSource = dsVD_Child;
                GVVariantDetail_Out.DataBind();

                foreach (GridViewRow row in GVVariantDetail_In.Rows)
                {
                    Label lblCreamSheet_ID = (Label)row.FindControl("lblCreamSheet_ID");
                    if(lblCreamSheet_ID.Text != "")
                    {
                        btnPopupSave_Product.Text = "Update";
                        ViewState["CreamSheet_ID"] = lblCreamSheet_ID.Text;
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
            lblMsg.Text = "";
            decimal TotalQtyInflow = 0;
            decimal TotalQtyOutFlow = 0;
            decimal TotalFatInflow = 0;
            decimal TotalFatOutFlow = 0;
            decimal TotalSnfInflow = 0;
            decimal TotalSnfOutFlow = 0;
            btnPopupSave_Product.Enabled = false;
            foreach (GridViewRow row1 in GVVariantDetail_In.Rows)
            {
                Label lblItemName = (Label)row1.FindControl("lblItemName");
                Label OB_Good = (Label)row1.FindControl("lblOB_Good");
                Label OB_Sour = (Label)row1.FindControl("lblOB_Sour");
                Label ItemName = (Label)row1.FindControl("lblItemName");
                TextBox CreamrcvdfrmPSGood = (TextBox)row1.FindControl("txtCream_received_from_Processing_Section_Good");
                TextBox CreamrcvdfrmPSSour = (TextBox)row1.FindControl("txtCream_received_from_Processing_Section_Sour");
               
                TextBox CreamfromOthersSourcesGood = (TextBox)row1.FindControl("txtCream_from_Others_Sources_Good");
                TextBox CreamfromOthersSourcesSour = (TextBox)row1.FindControl("txtCream_from_Others_Sources_Sour");

                if (OB_Good.Text == "")
                {
                    OB_Good.Text = "0";
                }
                if (OB_Sour.Text == "")
                {
                    OB_Sour.Text = "0";
                }
                if (CreamrcvdfrmPSGood.Text == "")
                {
                    CreamrcvdfrmPSGood.Text = "0";
                }
                if (CreamrcvdfrmPSSour.Text == "")
                {
                    CreamrcvdfrmPSSour.Text = "0";
                }

                if (CreamfromOthersSourcesGood.Text == "")
                {
                    CreamfromOthersSourcesGood.Text = "0";
                }

                if (CreamfromOthersSourcesSour.Text == "")
                {
                    CreamfromOthersSourcesSour.Text = "0";
                }
              

                TextBox lblIntotal = (TextBox)row1.FindControl("lblIntotal");

                lblIntotal.Text = (Convert.ToDecimal(OB_Good.Text) + Convert.ToDecimal(OB_Sour.Text) + Convert.ToDecimal(CreamrcvdfrmPSGood.Text) + Convert.ToDecimal(CreamrcvdfrmPSSour.Text) + Convert.ToDecimal(CreamfromOthersSourcesGood.Text) + Convert.ToDecimal(CreamfromOthersSourcesSour.Text)).ToString();
                if (lblIntotal.Text == "")
                {
                    lblIntotal.Text = "0";
                }              
                //if (lblIntotal.Text != "")
                //{
                //    btnPopupSave_Product.Enabled = true;
                //}
                if (lblItemName.Text == " Quantity In Kg")
                {
                    TotalQtyInflow = decimal.Parse(lblIntotal.Text);
                    ViewState["TotalQtyInflow"] = TotalQtyInflow;
                }
                if (lblItemName.Text == " Fat In Kg")
                {
                    TotalFatInflow = decimal.Parse(lblIntotal.Text);
                    ViewState["TotalFatInflow"] = TotalFatInflow;
                }
                if (lblItemName.Text == " Snf In Kg")
                {
                    TotalSnfInflow = decimal.Parse(lblIntotal.Text);
                    ViewState["TotalSnfInflow"] = TotalSnfInflow;
                }
            }


            foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
            {
                Label Label2 = (Label)row1.FindControl("Label2");
                TextBox txtIssue_for_WB_Good = (TextBox)row1.FindControl("txtIssue_for_WB_Good");
                TextBox txtIssue_for_WB_Sour = (TextBox)row1.FindControl("txtIssue_for_WB_Sour");
                TextBox txtIssue_to_other = (TextBox)row1.FindControl("txtIssue_to_other");
                TextBox txtCB_Good = (TextBox)row1.FindControl("txtCB_Good");
                TextBox txtCB_Sour = (TextBox)row1.FindControl("txtCB_Sour");
                TextBox txtSample = (TextBox)row1.FindControl("txtSample");

                if (txtIssue_for_WB_Good.Text == "")
                {
                    txtIssue_for_WB_Good.Text = "0";
                }
                if (txtIssue_for_WB_Sour.Text == "")
                {
                    txtIssue_for_WB_Sour.Text = "0";
                }
                if (txtIssue_to_other.Text == "")
                {
                    txtIssue_to_other.Text = "0";
                }

                if (txtCB_Good.Text == "")
                {
                    txtCB_Good.Text = "0";
                }

                if (txtCB_Sour.Text == "")
                {
                    txtCB_Sour.Text = "0";
                }
                if (txtSample.Text == "")
                {
                    txtSample.Text = "0";
                }
                TextBox lblouttotal = (TextBox)row1.FindControl("lblouttotal");

                lblouttotal.Text = (Convert.ToDecimal(txtIssue_for_WB_Good.Text) + Convert.ToDecimal(txtIssue_for_WB_Sour.Text) + Convert.ToDecimal(txtIssue_to_other.Text) + Convert.ToDecimal(txtCB_Good.Text) + Convert.ToDecimal(txtCB_Sour.Text)
                    + Convert.ToDecimal(txtSample.Text)).ToString();
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
                
            }
           
                if (TotalQtyInflow == TotalQtyOutFlow && TotalFatInflow == TotalFatOutFlow && TotalSnfInflow == TotalSnfOutFlow)
                {
                    btnPopupSave_Product.Enabled = true;
                }
            
            
            
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    
    private DataTable GetProductVarientInfo()
    {

        decimal OB_Good = 0;
        decimal OB_Sour = 0;
        decimal Cream_received_from_Processing_Section_Good = 0;
        decimal Cream_received_from_Processing_Section_Sour = 0;
        decimal Cream_from_Others_Sources_Good = 0;
        decimal Cream_from_Others_Sources_Sour = 0;
        decimal Issue_for_WB_Good = 0;
        decimal Issue_for_WB_Sour = 0;
        decimal Issue_to_other = 0;
        decimal CB_Good = 0;
        decimal CB_Sour = 0;
        decimal Sample = 0;
        decimal InTotal = 0;
        decimal OutTotal = 0;

        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("ItemCat_id", typeof(int)));
        dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
        dt.Columns.Add(new DataColumn("Item_id", typeof(int)));
        dt.Columns.Add(new DataColumn("OB_Good", typeof(decimal)));
        dt.Columns.Add(new DataColumn("OB_Sour", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Cream_received_from_Processing_Section_Good", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Cream_received_from_Processing_Section_Sour", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Cream_from_Others_Sources_Good", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Cream_from_Others_Sources_Sour", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Issue_for_WB_Good", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Issue_for_WB_Sour", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Issue_to_other", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CB_Good", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CB_Sour", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Sample", typeof(decimal)));
        

        foreach (GridViewRow row in GVVariantDetail_In.Rows)
        {
            Label lblItem_id = (Label)row.FindControl("lblItem_id");

            Label lblOB_Good = (Label)row.FindControl("lblOB_Good");
            Label lblOB_Sour = (Label)row.FindControl("lblOB_Sour");

            TextBox CreamrcvdfrmPSGood = (TextBox)row.FindControl("txtCream_received_from_Processing_Section_Good");
            TextBox CreamrcvdfrmPSSour = (TextBox)row.FindControl("txtCream_received_from_Processing_Section_Sour");

            TextBox CreamfromOthersSourcesGood = (TextBox)row.FindControl("txtCream_from_Others_Sources_Good");
            TextBox CreamfromOthersSourcesSour = (TextBox)row.FindControl("txtCream_from_Others_Sources_Sour");

            if (lblOB_Good.Text != "" && lblOB_Good.Text != "0" && lblOB_Good.Text != "0.00")
            {
                OB_Good = Convert.ToDecimal(lblOB_Good.Text);
            }
            else
            {
                OB_Good = 0;
            }


            if (lblOB_Sour.Text != "" && lblOB_Sour.Text != "0" && lblOB_Sour.Text != "0.00")
            {
                OB_Sour = Convert.ToDecimal(lblOB_Sour.Text);
            }
            else
            {
                OB_Sour = 0;
            }

            if (CreamrcvdfrmPSGood.Text != "" && CreamrcvdfrmPSGood.Text != "0" && CreamrcvdfrmPSGood.Text != "0.00")
            {
                Cream_received_from_Processing_Section_Good = Convert.ToDecimal(CreamrcvdfrmPSGood.Text);
            }
            else
            {
                Cream_received_from_Processing_Section_Good = 0;
            }


            if (CreamrcvdfrmPSSour.Text != "" && CreamrcvdfrmPSSour.Text != "0" && CreamrcvdfrmPSSour.Text != "0.00")
            {
                Cream_received_from_Processing_Section_Sour = Convert.ToDecimal(CreamrcvdfrmPSSour.Text);
            }
            else
            {
                Cream_received_from_Processing_Section_Sour = 0;
            }
            if (CreamfromOthersSourcesGood.Text != "" && CreamfromOthersSourcesGood.Text != "0" && CreamfromOthersSourcesGood.Text != "0.00")
            {
                Cream_from_Others_Sources_Good = Convert.ToDecimal(CreamfromOthersSourcesGood.Text);
            }
            else
            {
                Cream_from_Others_Sources_Sour = 0;
            }
            if (CreamfromOthersSourcesSour.Text != "" && CreamfromOthersSourcesSour.Text != "0" && CreamfromOthersSourcesSour.Text != "0.00")
            {
                Cream_from_Others_Sources_Sour = Convert.ToDecimal(CreamfromOthersSourcesSour.Text);
            }
            else
            {
                Cream_from_Others_Sources_Good = 0;
            }
            InTotal = OB_Good + OB_Sour + Cream_received_from_Processing_Section_Good + Cream_received_from_Processing_Section_Sour + Cream_from_Others_Sources_Good + Cream_from_Others_Sources_Sour;

            dr = dt.NewRow();
            dr[0] = objdb.LooseCreamItemCategoryId_ID();
            dr[1] = objdb.LooseCreamItemTypeId_ID();
            dr[2] = lblItem_id.Text;
            dr[3] = OB_Good;
            dr[4] = OB_Sour;
            dr[5] = Cream_received_from_Processing_Section_Good;
            dr[6] = Cream_received_from_Processing_Section_Sour;
            dr[7] = Cream_from_Others_Sources_Good;
            dr[8] = Cream_from_Others_Sources_Sour;
            dr[9] = Issue_for_WB_Good;
            dr[10] = Issue_for_WB_Sour;
            dr[11] = Issue_to_other;
            dr[12] = CB_Good;
            dr[13] = CB_Sour;
            dr[14] = Sample;
            dt.Rows.Add(dr);


        }

        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
        {
            Label lblItem_id = (Label)row1.FindControl("lblItem_id_Out");
            TextBox txtIssue_for_WB_Good = (TextBox)row1.FindControl("txtIssue_for_WB_Good");
            TextBox txtIssue_for_WB_Sour = (TextBox)row1.FindControl("txtIssue_for_WB_Sour");
            TextBox txtIssue_to_other = (TextBox)row1.FindControl("txtIssue_to_other");
            TextBox txtCB_Good = (TextBox)row1.FindControl("txtCB_Good");
            TextBox txtCB_Sour = (TextBox)row1.FindControl("txtCB_Sour");
            TextBox txtSample = (TextBox)row1.FindControl("txtSample");

            if (txtIssue_for_WB_Good.Text != "0" && txtIssue_for_WB_Good.Text != "0.00")
            {
                Issue_for_WB_Good = Convert.ToDecimal(txtIssue_for_WB_Good.Text);
            }
            else
            {
                Issue_for_WB_Good = 0;
            }

            if (txtIssue_for_WB_Sour.Text != "0" && txtIssue_for_WB_Sour.Text != "0.00")
            {
                Issue_for_WB_Sour = Convert.ToDecimal(txtIssue_for_WB_Sour.Text);
            }
            else
            {
                Issue_for_WB_Sour = 0;
            }
            if (txtIssue_to_other.Text != "0" && txtIssue_to_other.Text != "0.00")
            {
                Issue_to_other = Convert.ToDecimal(txtIssue_to_other.Text);
            }
            else
            {
                Issue_to_other = 0;
            }

            if (txtCB_Good.Text != "0" && txtCB_Good.Text != "0.00")
            {
                CB_Good = Convert.ToDecimal(txtCB_Good.Text);
            }
            else
            {
                CB_Good = 0;
            }
            if (txtCB_Sour.Text != "0" && txtCB_Sour.Text != "0.00")
            {
                CB_Sour = Convert.ToDecimal(txtCB_Sour.Text);
            }
            else
            {
                CB_Sour = 0;
            }
            if (txtSample.Text != "0" && txtSample.Text != "0.00")
            {
                Sample = Convert.ToDecimal(txtSample.Text);
            }
            else
            {
                Sample = 0;
            }


            OutTotal = Issue_for_WB_Good + Issue_for_WB_Sour + Issue_to_other + CB_Good + CB_Sour + Sample;

            Int32 IID = Convert.ToInt32(lblItem_id.Text);
            DataRow dr1 = dt.AsEnumerable().Where(r => ((Int32)r["Item_id"]).Equals(IID)).First();

            //dr1["OB_Good"] = OB_Good;
            //dr1["OB_Sour"] = OB_Sour;
            //dr1["Cream_received_from_Processing_Section_Good"] = Cream_received_from_Processing_Section_Good;
            //dr1["Cream_received_from_Processing_Section_Sour"] = Cream_received_from_Processing_Section_Sour;
            //dr1["Cream_from_Others_Sources_Good"] = Cream_from_Others_Sources_Good;
            //dr1["Cream_from_Others_Sources_Sour"] = Cream_from_Others_Sources_Sour;
            dr1["Issue_for_WB_Good"] = Issue_for_WB_Good;
            dr1["Issue_for_WB_Sour"] = Issue_for_WB_Sour;
            dr1["Issue_to_other"] = Issue_to_other;
            dr1["CB_Good"] = CB_Good;
            dr1["CB_Sour"] = CB_Sour;
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
                            ds = objdb.ByProcedure("Sp_Production_CreamSheet",
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
                                              ,objdb.LooseCreamItemTypeId_ID()
                                    },
                                                     new string[] { "type_Production_CreamSheetNewChild" },
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
                            ds = objdb.ByProcedure("Sp_Production_CreamSheet",
                                                      new string[] { "flag",
                                                       "CreamSheet_ID"
                                                      ,"CreatedBy"
                                                      ,"CreatedBy_IP"
                                                      ,"ItemType_id"
                                    },
                                                      new string[] { "3"
                                              ,ViewState["CreamSheet_ID"].ToString()                                            
                                              , ViewState["Emp_ID"].ToString()
                                              ,objdb.GetLocalIPAddress()
                                              ,objdb.LooseCreamItemTypeId_ID()
                                    },
                                                     new string[] { "type_Production_CreamSheetNewChild" },
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
    protected void GVVariantDetail_In_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "In Flow";
            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);


            GVVariantDetail_In.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void GVVariantDetail_Out_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Out Flow";
            HeaderCell.ColumnSpan = 14;
            HeaderGridRow.Cells.Add(HeaderCell);


            GVVariantDetail_Out.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
}