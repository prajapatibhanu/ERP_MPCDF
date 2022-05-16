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

public partial class mis_dailyplan_GheeSheetEntryNew : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
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
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        if (ddlPSection.SelectedValue != "0")
        {
            GetSectionDetail();
            btnGetTotal_Click(sender, e);
        }

    }
    protected void ddlPSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPSection.SelectedValue != "0")
        {

            GetSectionDetail();
            btnGetTotal_Click(sender, e);

        }

    }
    private void GetSectionDetail()
    {

        try
        {
            string Fdate = "";
            btnPopupSave_Product.Text = "Save";
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            // For Variant

            DataSet dsVD_Child = objdb.ByProcedure("Sp_Production_GheeIntermediateSheetChild",
            new string[] { "flag", "Office_ID", "Date", "ProductSection_ID", "ItemType_id" },
            new string[] { "4", objdb.Office_ID(), Fdate, ddlPSection.SelectedValue,objdb.LooseGheeItemTypeId_ID() }, "dataset");

            if (dsVD_Child != null && dsVD_Child.Tables[0].Rows.Count > 0)
            {
                if (dsVD_Child.Tables[0].Rows[0]["GheeSheet_ID"].ToString() == "0")
                {
                    btnPopupSave_Product.Text = "Save";
                   
                }
                else
                {
                    btnPopupSave_Product.Text = "Update";
                    ViewState["GheeSheet_ID"] = dsVD_Child.Tables[0].Rows[0]["GheeSheet_ID"].ToString();
                }
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

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);

        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    protected void GVVariantDetail_In_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell0 = new TableCell();
            HeaderCell0.Text = "";
            HeaderCell0.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell0);

            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Opening Balance";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.Style.Add("text-align", "center");
            HeaderGridRow.Cells.Add(HeaderCell);

            TableCell HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ghee Pack";
            HeaderCell1.ColumnSpan = 2;
            HeaderCell1.Style.Add("text-align", "center");
            HeaderGridRow.Cells.Add(HeaderCell1);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Return From FP";
            HeaderCell2.ColumnSpan = 2;
            HeaderCell2.Style.Add("text-align", "center");
            HeaderGridRow.Cells.Add(HeaderCell2);

            TableCell HeaderCell3 = new TableCell();
            HeaderCell3.Text = "Other";
            HeaderCell3.ColumnSpan = 2;
            HeaderCell3.Style.Add("text-align", "center");
            HeaderGridRow.Cells.Add(HeaderCell3);

            TableCell HeaderCell4 = new TableCell();
            HeaderCell4.Text = "Total";
            HeaderCell4.ColumnSpan = 2;
            HeaderCell4.Style.Add("text-align", "center");
            HeaderGridRow.Cells.Add(HeaderCell4);

            


            GVVariantDetail_In.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void GVVariantDetail_Out_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            

            TableCell HeaderCell0 = new TableCell();
            HeaderCell0.Text = "";
            HeaderCell0.ColumnSpan = 1;
           
            HeaderGridRow.Cells.Add(HeaderCell0);

            TableCell HeaderCell5 = new TableCell();
            HeaderCell5.Text = "";
            HeaderCell5.ColumnSpan = 1;

            HeaderGridRow.Cells.Add(HeaderCell5);
            
            TableCell HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Issue to FP";
            HeaderCell1.ColumnSpan = 2;
            HeaderCell1.Style.Add("text-align", "center");
            HeaderGridRow.Cells.Add(HeaderCell1);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Issue to Other";
            HeaderCell2.ColumnSpan = 2;
            HeaderCell2.Style.Add("text-align", "center");
            HeaderGridRow.Cells.Add(HeaderCell2);

            TableCell HeaderCell6 = new TableCell();
            HeaderCell6.Text = "Leakage Packing";
            HeaderCell6.ColumnSpan = 2;
            HeaderCell6.Style.Add("text-align", "center");
            HeaderGridRow.Cells.Add(HeaderCell6);

            TableCell HeaderCell3 = new TableCell();
            HeaderCell3.Text = "Closing Balance";
            HeaderCell3.ColumnSpan = 2;
            HeaderCell3.Style.Add("text-align", "center");
            HeaderGridRow.Cells.Add(HeaderCell3);

            TableCell HeaderCell4 = new TableCell();
            HeaderCell4.Text = "Total";
            HeaderCell4.ColumnSpan = 2;
            HeaderCell4.Style.Add("text-align", "center");
            HeaderGridRow.Cells.Add(HeaderCell4);


           


            GVVariantDetail_Out.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void btnGetTotal_Click(object sender, EventArgs e)
    {
        try
        {
			btnPopupSave_Product.Enabled = true;
            lblMsg.Text = "";
            decimal OBNo = 0;
            decimal OBQtyInKg = 0;
            decimal GheePackNo = 0;
            decimal GheePackQtyInKg = 0;
            decimal ReturnFromFPNo = 0;
            decimal ReturnFromFPQtyInKg = 0;
            decimal OtherNo = 0;
            decimal OtherQtyInKg = 0;
            decimal TotalNo = 0;
            decimal TotalQtyInKg = 0;
            
            foreach (GridViewRow row1 in GVVariantDetail_In.Rows)
            {
                TextBox txtOBNo = (TextBox)row1.FindControl("txtOBNo");
                TextBox txtOBQtyInKg = (TextBox)row1.FindControl("txtOBQtyInKg");
                TextBox txtGheePackNo = (TextBox)row1.FindControl("txtGheePackNo");
                TextBox txtGheePackQtyInKg = (TextBox)row1.FindControl("txtGheePackQtyInKg");
                TextBox txtReturnFromFPNo = (TextBox)row1.FindControl("txtReturnFromFPNo");
                Label lblGheeIssueForPacking = (Label)row1.FindControl("lblGheeIssueForPacking");
                TextBox txtReturnFromFPQtyInKg = (TextBox)row1.FindControl("txtReturnFromFPQtyInKg");
                TextBox txtOtherNo = (TextBox)row1.FindControl("txtOtherNo");
                TextBox txtOtherQtyInKg = (TextBox)row1.FindControl("txtOtherQtyInKg");
                TextBox txtTotalNo = (TextBox)row1.FindControl("txtTotalNo");
                TextBox txtTotalQtyInKg = (TextBox)row1.FindControl("txtTotalQtyInKg");
                if (txtOBNo.Text == "")
                {
                    txtOBNo.Text = "0";
                }
                if (txtOBQtyInKg.Text == "")
                {
                    txtOBQtyInKg.Text = "0";
                }
                if (txtGheePackNo.Text == "")
                {
                    txtGheePackNo.Text = "0";
                }
                if (txtGheePackQtyInKg.Text == "")
                {
                    txtGheePackQtyInKg.Text = "0";
                }
                if (txtReturnFromFPNo.Text == "")
                {
                    txtReturnFromFPNo.Text = "0";
                }

                if (txtReturnFromFPQtyInKg.Text == "")
                {
                    txtReturnFromFPQtyInKg.Text = "0";
                }

                if (txtOtherNo.Text == "")
                {
                    txtOtherNo.Text = "0";
                }
                if (txtOtherQtyInKg.Text == "")
                {
                    txtOtherQtyInKg.Text = "0";
                }
                if (txtGheePackQtyInKg.Text == "")
                {
                    txtGheePackQtyInKg.Text = "0";
                }
                OBNo += decimal.Parse(txtOBNo.Text);
                OBQtyInKg += decimal.Parse(txtOBQtyInKg.Text);
                GheePackNo += decimal.Parse(txtGheePackNo.Text);
                GheePackQtyInKg += decimal.Parse(txtGheePackQtyInKg.Text);
                ReturnFromFPNo += decimal.Parse(txtReturnFromFPNo.Text);
                ReturnFromFPQtyInKg += decimal.Parse(txtReturnFromFPQtyInKg.Text);
                OtherNo += decimal.Parse(txtOtherNo.Text); 
                OtherQtyInKg += decimal.Parse(txtOtherQtyInKg.Text);
                
                

                
                
                txtTotalNo.Text = (Convert.ToDecimal(txtOBNo.Text) + Convert.ToDecimal(txtGheePackNo.Text) + Convert.ToDecimal(txtReturnFromFPNo.Text) + Convert.ToDecimal(txtOtherNo.Text)).ToString();
                txtTotalQtyInKg.Text = (Convert.ToDecimal(txtOBQtyInKg.Text) + Convert.ToDecimal(txtGheePackQtyInKg.Text) + Convert.ToDecimal(txtReturnFromFPQtyInKg.Text) + Convert.ToDecimal(txtOtherQtyInKg.Text)).ToString();
                TotalNo += decimal.Parse(txtTotalNo.Text);
                TotalQtyInKg += decimal.Parse(txtTotalQtyInKg.Text);
                if (txtTotalNo.Text == "")
                {
                    txtTotalNo.Text = "0";
                }
                if (txtTotalQtyInKg.Text == " Quantity In Kg")
                {
                    txtTotalQtyInKg.Text = "0";
                }
                hfPackagingValue.Value = lblGheeIssueForPacking.Text.ToString();
            }
            
            GVVariantDetail_In.FooterRow.Cells[0].Text = "<b>Total : </b>";
            GVVariantDetail_In.FooterRow.Cells[1].Text = "<b>" + OBNo.ToString() + "</b>";
            GVVariantDetail_In.FooterRow.Cells[2].Text = "<b>" + OBQtyInKg.ToString() + "</b>";
            GVVariantDetail_In.FooterRow.Cells[3].Text = "<b>" + GheePackNo.ToString() + "</b>";
            GVVariantDetail_In.FooterRow.Cells[4].Text = "<b>" + GheePackQtyInKg.ToString() + "</b>";
            GVVariantDetail_In.FooterRow.Cells[5].Text = "<b>" + ReturnFromFPNo.ToString() + "</b>";

            GVVariantDetail_In.FooterRow.Cells[6].Text = "<b>" + ReturnFromFPQtyInKg.ToString() + "</b>";
            GVVariantDetail_In.FooterRow.Cells[7].Text = "<b>" + OtherNo.ToString() + "</b>";
            GVVariantDetail_In.FooterRow.Cells[8].Text = "<b>" + OtherQtyInKg.ToString() + "</b>";
            GVVariantDetail_In.FooterRow.Cells[9].Text = "<b>" + TotalNo.ToString() + "</b>";
            GVVariantDetail_In.FooterRow.Cells[10].Text = "<b>" + TotalQtyInKg.ToString() + "</b>";

            decimal IsuuetoFPNo = 0;
            decimal IsuuetoFPQtyInKg = 0;
            decimal IsuuetoOtherNo = 0;
            decimal IsuuetoOtherQtyInKg = 0;
            decimal LeakagePackingNo = 0;
            decimal LeakagePackingQtyInKg = 0;
            decimal CBNo = 0;
            decimal CBQtyInKg = 0;
            decimal OutFlowTotalNo = 0;
            decimal OutFlowTotalQtyInKg = 0;
           
            foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
            {

                TextBox txtIsuuetoFPNo = (TextBox)row1.FindControl("txtIsuuetoFPNo");
                TextBox txtIsuuetoFPQtyInKg = (TextBox)row1.FindControl("txtIsuuetoFPQtyInKg");
                TextBox txtIsuuetoOtherNo = (TextBox)row1.FindControl("txtIsuuetoOtherNo");
                TextBox txtIsuuetoOtherQtyInKg = (TextBox)row1.FindControl("txtIsuuetoOtherQtyInKg");
                
                TextBox txtLeakagePackingNo = (TextBox)row1.FindControl("txtLeakagePackingNo");
                TextBox txtLeakagePackingQtyInKg = (TextBox)row1.FindControl("txtLeakagePackingQtyInKg");
                TextBox txtCBNo = (TextBox)row1.FindControl("txtCBNo");
                TextBox txtCBQtyInKg = (TextBox)row1.FindControl("txtCBQtyInKg");
                TextBox txtOutFlowTotalNo = (TextBox)row1.FindControl("txtOutFlowTotalNo");
                TextBox txtOutFlowTotalQtyInKg = (TextBox)row1.FindControl("txtOutFlowTotalQtyInKg");
                Label lblGheeIssuetoFP = (Label)row1.FindControl("lblGheeIssuetoFP");

                if (txtIsuuetoFPNo.Text == "")
                {
                    txtIsuuetoFPNo.Text = "0";
                }
                if (txtIsuuetoOtherNo.Text == "")
                {
                    txtIsuuetoOtherNo.Text = "0";
                }
                if (txtIsuuetoFPQtyInKg.Text == "")
                {
                    txtIsuuetoFPQtyInKg.Text = "0";
                }

                if (txtIsuuetoOtherQtyInKg.Text == "")
                {
                    txtIsuuetoOtherQtyInKg.Text = "0";
                }
                if (txtLeakagePackingNo.Text == "")
                {
                    txtLeakagePackingNo.Text = "0";
                }

                if (txtLeakagePackingQtyInKg.Text == "")
                {
                    txtLeakagePackingQtyInKg.Text = "0";
                }
                if (txtCBNo.Text == "")
                {
                    txtCBNo.Text = "0";
                }

                if (txtCBQtyInKg.Text == "")
                {
                    txtCBQtyInKg.Text = "0";
                }
                if (lblGheeIssuetoFP.Text == "")
                {
                    lblGheeIssuetoFP.Text = "0";
                }
                IsuuetoFPNo += decimal.Parse(txtIsuuetoFPNo.Text);
                IsuuetoFPQtyInKg = decimal.Parse(lblGheeIssuetoFP.Text);
                IsuuetoOtherNo += decimal.Parse(txtIsuuetoOtherNo.Text);
                IsuuetoOtherQtyInKg += decimal.Parse(txtIsuuetoOtherQtyInKg.Text);
                LeakagePackingNo += decimal.Parse(txtLeakagePackingNo.Text);
                LeakagePackingQtyInKg += decimal.Parse(txtLeakagePackingQtyInKg.Text);
               
                CBNo += decimal.Parse(txtCBNo.Text);
                CBQtyInKg += decimal.Parse(txtCBQtyInKg.Text);




                txtOutFlowTotalNo.Text = (Convert.ToDecimal(txtIsuuetoFPNo.Text) + Convert.ToDecimal(txtIsuuetoOtherNo.Text) + Convert.ToDecimal(txtLeakagePackingNo.Text) + Convert.ToDecimal(txtCBNo.Text)).ToString();
                txtOutFlowTotalQtyInKg.Text = (Convert.ToDecimal(txtIsuuetoFPQtyInKg.Text) + Convert.ToDecimal(txtIsuuetoOtherQtyInKg.Text) + Convert.ToDecimal(txtLeakagePackingQtyInKg.Text) + Convert.ToDecimal(txtCBQtyInKg.Text)).ToString();
                OutFlowTotalNo += decimal.Parse(txtOutFlowTotalNo.Text);

                OutFlowTotalQtyInKg += decimal.Parse(txtOutFlowTotalQtyInKg.Text);
                if (txtOutFlowTotalNo.Text == "")
                {
                    txtOutFlowTotalNo.Text = "0";
                }
                
            }
            GVVariantDetail_Out.FooterRow.Cells[0].Text = "<b>Total : </b>";
            GVVariantDetail_Out.FooterRow.Cells[2].Text = "<b>" + IsuuetoFPNo.ToString() + "</b>";
            GVVariantDetail_Out.FooterRow.Cells[3].Text = "<b>" + IsuuetoFPQtyInKg.ToString() + "</b>";
            GVVariantDetail_Out.FooterRow.Cells[4].Text = "<b>" + IsuuetoOtherNo.ToString() + "</b>";
            GVVariantDetail_Out.FooterRow.Cells[5].Text = "<b>" + IsuuetoOtherQtyInKg.ToString() + "</b>";
            GVVariantDetail_Out.FooterRow.Cells[6].Text = "<b>" + LeakagePackingNo.ToString() + "</b>";
            GVVariantDetail_Out.FooterRow.Cells[7].Text = "<b>" + LeakagePackingQtyInKg.ToString() + "</b>";
            GVVariantDetail_Out.FooterRow.Cells[8].Text = "<b>" + CBNo.ToString() + "</b>";

            GVVariantDetail_Out.FooterRow.Cells[9].Text = "<b>" + CBQtyInKg.ToString() + "</b>";
            GVVariantDetail_Out.FooterRow.Cells[10].Text = "<b>" + OutFlowTotalNo.ToString() + "</b>";
            GVVariantDetail_Out.FooterRow.Cells[11].Text = "<b>" + OutFlowTotalQtyInKg.ToString() + "</b>";

            // if (TotalNo != 0 && OutFlowTotalNo != 0 && TotalQtyInKg != 0 && OutFlowTotalQtyInKg != 0)
            // {
               // if ((OutFlowTotalQtyInKg - TotalQtyInKg) <= 1)
                // {
                    // btnPopupSave_Product.Enabled = true;
                // }
                // else
                // {
                    // btnPopupSave_Product.Enabled = false;
                // }
            // }
            // else
            // {
                // btnPopupSave_Product.Enabled = false;
            // }
            //txtGainLossQtyInKg.Text = (TotalQtyOutFlow - TotalQtyInflow).ToString();
            //txtGainLossFatInKg.Text = (TotalFatOutFlow - TotalFatInflow).ToString();
            //txtGainLossSnfInKg.Text = (TotalSnfOutFlow - TotalSnfInflow).ToString();

            //decimal TotalGainLoss = Convert.ToDecimal(txtGainLossQtyInKg.Text) + Convert.ToDecimal(txtGainLossFatInKg.Text) + Convert.ToDecimal(txtGainLossSnfInKg.Text);
            //if (TotalGainLoss == 0)
            //{
            //    lblGainLoss.Text = "";
            //    btnPopupSave_Product.Enabled = true;
            //}
            //else
            //{
            //    btnPopupSave_Product.Enabled = false;
            //    lblGainLoss.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Gain/Loss should be 0");
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    private DataTable GetProductVarientInfo()
    {

        decimal OBNo = 0;
        decimal OBQtyInKg = 0;


        decimal GheePackNo = 0;
        decimal GheePackQtyInKg = 0;
        decimal ReturnFromFPNo = 0;
        decimal ReturnFromFPQtyInKg = 0;
        decimal OtherNo = 0;
        decimal OtherQtyInKg = 0;
        decimal TotalInFlowNo = 0;
        decimal TotalInFlowQtyInKg = 0;
        decimal IsuuetoFPNoofCases = 0;
        decimal IsuuetoFPNo = 0;
        decimal IsuuetoFPQtyInKg = 0;
        decimal IsuuetoOtherNo = 0;
        decimal IsuuetoOtherQtyInKg = 0;
        decimal LeakagePackingNo = 0;
        decimal LeakagePackingQtyInKg = 0;
        decimal ClosingBalanceNo = 0;
        decimal ClosingBalanceQtyInKg = 0;

        decimal TotalOutFlowNo = 0;
        decimal TotalOutFlowQtyInKg = 0;

        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("ItemCat_id", typeof(int)));
        dt.Columns.Add(new DataColumn("ItemType_id", typeof(int)));
        dt.Columns.Add(new DataColumn("Item_id", typeof(int)));
        dt.Columns.Add(new DataColumn("OBNo", typeof(decimal)));
        dt.Columns.Add(new DataColumn("OBQtyInKg", typeof(decimal)));

        dt.Columns.Add(new DataColumn("GheePackNo", typeof(decimal)));
        dt.Columns.Add(new DataColumn("GheePackQtyInKg", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ReturnFromFPNo", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ReturnFromFPQtyInKg", typeof(decimal)));
        dt.Columns.Add(new DataColumn("OtherNo", typeof(decimal)));
        dt.Columns.Add(new DataColumn("OtherQtyInKg", typeof(decimal)));
        dt.Columns.Add(new DataColumn("TotalInFlowNo", typeof(decimal)));
        dt.Columns.Add(new DataColumn("TotalInFlowQtyInKg", typeof(decimal)));
        dt.Columns.Add(new DataColumn("IsuuetoFPNoofCases", typeof(decimal)));
        dt.Columns.Add(new DataColumn("IsuuetoFPNo", typeof(decimal)));
        dt.Columns.Add(new DataColumn("IsuuetoFPQtyInKg", typeof(decimal)));
        dt.Columns.Add(new DataColumn("IsuuetoOtherNo", typeof(decimal)));
        dt.Columns.Add(new DataColumn("IsuuetoOtherQtyInKg", typeof(decimal)));
        dt.Columns.Add(new DataColumn("LeakagePackingNo", typeof(decimal)));
        dt.Columns.Add(new DataColumn("LeakagePackingQtyInKg", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ClosingBalanceNo", typeof(decimal)));
        dt.Columns.Add(new DataColumn("ClosingBalanceQtyInKg", typeof(decimal)));
        dt.Columns.Add(new DataColumn("TotalOutFlowNo", typeof(decimal)));
        dt.Columns.Add(new DataColumn("TotalOutFlowQtyInKg", typeof(decimal)));

        foreach (GridViewRow row in GVVariantDetail_In.Rows)
        {
            Label lblItem_id = (Label)row.FindControl("lblInFlowItem_id");

            TextBox txtOBNo = (TextBox)row.FindControl("txtOBNo");
            TextBox txtOBQtyInKg = (TextBox)row.FindControl("txtOBQtyInKg");
            TextBox txtGheePackNo = (TextBox)row.FindControl("txtGheePackNo");
            TextBox txtGheePackQtyInKg = (TextBox)row.FindControl("txtGheePackQtyInKg");
            TextBox txtReturnFromFPNo = (TextBox)row.FindControl("txtReturnFromFPNo");

            TextBox txtReturnFromFPQtyInKg = (TextBox)row.FindControl("txtReturnFromFPQtyInKg");
            TextBox txtOtherNo = (TextBox)row.FindControl("txtOtherNo");
            TextBox txtOtherQtyInKg = (TextBox)row.FindControl("txtOtherQtyInKg");
            TextBox txtTotalNo = (TextBox)row.FindControl("txtTotalNo");
            TextBox txtTotalQtyInKg = (TextBox)row.FindControl("txtTotalQtyInKg");

            if (txtOBNo.Text != "" && txtOBNo.Text != "0" && txtOBNo.Text != "0.00")
            {
                OBNo = Convert.ToDecimal(txtOBNo.Text);
            }
            else
            {
                OBNo = 0;
            }


            if (txtOBQtyInKg.Text != "" && txtOBQtyInKg.Text != "0" && txtOBQtyInKg.Text != "0.00")
            {
                OBQtyInKg = Convert.ToDecimal(txtOBQtyInKg.Text);
            }
            else
            {
                OBQtyInKg = 0;
            }

            if (txtGheePackNo.Text != "" && txtGheePackNo.Text != "0" && txtGheePackNo.Text != "0.00")
            {
                GheePackNo = Convert.ToDecimal(txtGheePackNo.Text);
            }
            else
            {
                GheePackNo = 0;
            }


            if (txtGheePackQtyInKg.Text != "" && txtGheePackQtyInKg.Text != "0" && txtGheePackQtyInKg.Text != "0.00")
            {
                GheePackQtyInKg = Convert.ToDecimal(txtGheePackQtyInKg.Text);
            }
            else
            {
                GheePackQtyInKg = 0;
            }
            if (txtReturnFromFPNo.Text != "" && txtReturnFromFPNo.Text != "0" && txtReturnFromFPNo.Text != "0.00")
            {
                ReturnFromFPNo = Convert.ToDecimal(txtReturnFromFPNo.Text);
            }
            else
            {
                ReturnFromFPNo = 0;
            }
            if (txtReturnFromFPQtyInKg.Text != "" && txtReturnFromFPQtyInKg.Text != "0" && txtReturnFromFPQtyInKg.Text != "0.00")
            {
                ReturnFromFPQtyInKg = Convert.ToDecimal(txtReturnFromFPQtyInKg.Text);
            }
            else
            {
                ReturnFromFPQtyInKg = 0;
            }
            if (txtOtherNo.Text != "" && txtOtherNo.Text != "0" && txtOtherNo.Text != "0.00")
            {
                OtherNo = Convert.ToDecimal(txtOtherNo.Text);
            }
            else
            {
                OtherNo = 0;
            }
            if (txtOtherQtyInKg.Text != "" && txtOtherQtyInKg.Text != "0" && txtOtherQtyInKg.Text != "0.00")
            {
                OtherQtyInKg = Convert.ToDecimal(txtOtherQtyInKg.Text);
            }
            else
            {
                OtherQtyInKg = 0;
            }
            if (txtTotalNo.Text != "" && txtTotalNo.Text != "0" && txtTotalNo.Text != "0.00")
            {
                TotalInFlowNo = Convert.ToDecimal(txtTotalNo.Text);
            }
            else
            {
                TotalInFlowNo = 0;
            }
            if (txtTotalQtyInKg.Text != "" && txtTotalQtyInKg.Text != "0" && txtTotalQtyInKg.Text != "0.00")
            {
                TotalInFlowQtyInKg = Convert.ToDecimal(txtTotalQtyInKg.Text);
            }
            else
            {
                TotalInFlowQtyInKg = 0;
            }
            

            dr = dt.NewRow();
            dr[0] = objdb.LooseGheeItemCategoryId_ID();
            dr[1] = objdb.LooseGheeItemTypeId_ID();
            dr[2] = lblItem_id.Text;
            dr[3] = OBNo;
            dr[4] = OBQtyInKg;
            dr[5] = GheePackNo;
            dr[6] = GheePackQtyInKg;
            dr[7] = ReturnFromFPNo;
            dr[8] = ReturnFromFPQtyInKg;
            dr[9] = OtherNo;
            dr[10] = OtherQtyInKg;
            dr[11] = TotalInFlowNo;
            dr[12] = TotalInFlowQtyInKg;
            dr[13] = IsuuetoFPNoofCases;
            dr[14] = IsuuetoFPNo;
            dr[15] = IsuuetoFPQtyInKg;
            dr[16] = IsuuetoOtherNo;
            dr[17] = IsuuetoOtherQtyInKg;
            dr[18] = LeakagePackingNo;
            dr[19] = LeakagePackingQtyInKg;
            dr[20] = ClosingBalanceNo;
            dr[21] = ClosingBalanceQtyInKg;
            dr[22] = TotalOutFlowNo;
            dr[23] = TotalOutFlowQtyInKg;
            dt.Rows.Add(dr);


        }

        foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
        {
            Label lblItem_id = (Label)row1.FindControl("lblOutFlowItem_id");
            TextBox txtIsuuetoFPNo = (TextBox)row1.FindControl("txtIsuuetoFPNo");
            TextBox txtIsuuetoFPNoofCases = (TextBox)row1.FindControl("txtIsuuetoFPNoofCases");
            TextBox txtIsuuetoFPQtyInKg = (TextBox)row1.FindControl("txtIsuuetoFPQtyInKg");
            TextBox txtIsuuetoOtherNo = (TextBox)row1.FindControl("txtIsuuetoOtherNo");
            TextBox txtIsuuetoOtherQtyInKg = (TextBox)row1.FindControl("txtIsuuetoOtherQtyInKg");
            TextBox txtLeakagePackingNo = (TextBox)row1.FindControl("txtLeakagePackingNo");
            TextBox txtLeakagePackingQtyInKg = (TextBox)row1.FindControl("txtLeakagePackingQtyInKg");
            TextBox txtCBNo = (TextBox)row1.FindControl("txtCBNo");
            TextBox txtCBQtyInKg = (TextBox)row1.FindControl("txtCBQtyInKg");
            TextBox txtOutFlowTotalNo = (TextBox)row1.FindControl("txtOutFlowTotalNo");
            TextBox txtOutFlowTotalQtyInKg = (TextBox)row1.FindControl("txtOutFlowTotalQtyInKg");


            if (txtIsuuetoFPNo.Text != "0" && txtIsuuetoFPNo.Text != "0.00")
            {
                IsuuetoFPNo = Convert.ToDecimal(txtIsuuetoFPNo.Text);
            }
            else
            {
                IsuuetoFPNo = 0;
            }

            if (txtIsuuetoFPQtyInKg.Text != "0" && txtIsuuetoFPQtyInKg.Text != "0.00")
            {
                IsuuetoFPQtyInKg = Convert.ToDecimal(txtIsuuetoFPQtyInKg.Text);
            }
            else
            {
                IsuuetoFPQtyInKg = 0;
            }
            if (txtIsuuetoOtherNo.Text != "0" && txtIsuuetoOtherNo.Text != "0.00")
            {
                IsuuetoOtherNo = Convert.ToDecimal(txtIsuuetoOtherNo.Text);
            }
            else
            {
                IsuuetoOtherNo = 0;
            }

            if (txtIsuuetoOtherQtyInKg.Text != "0" && txtIsuuetoOtherQtyInKg.Text != "0.00")
            {
                IsuuetoOtherQtyInKg = Convert.ToDecimal(txtIsuuetoOtherQtyInKg.Text);
            }
            else
            {
                IsuuetoOtherQtyInKg = 0;
            }
            if (txtLeakagePackingNo.Text != "0" && txtLeakagePackingNo.Text != "0.00")
            {
                LeakagePackingNo = Convert.ToDecimal(txtLeakagePackingNo.Text);
            }
            else
            {
                LeakagePackingNo = 0;
            }
            if (txtLeakagePackingQtyInKg.Text != "0" && txtLeakagePackingQtyInKg.Text != "0.00")
            {
                LeakagePackingQtyInKg = Convert.ToDecimal(txtLeakagePackingQtyInKg.Text);
            }
            else
            {
                LeakagePackingQtyInKg = 0;
            }
            if (txtCBNo.Text != "0" && txtCBNo.Text != "0.00")
            {
                ClosingBalanceNo = Convert.ToDecimal(txtCBNo.Text);
            }
            else
            {
                ClosingBalanceNo = 0;
            }
            if (txtCBQtyInKg.Text != "0" && txtCBQtyInKg.Text != "0.00")
            {
                ClosingBalanceQtyInKg = Convert.ToDecimal(txtCBQtyInKg.Text);
            }
            else
            {
                ClosingBalanceQtyInKg = 0;
            }
            if (txtOutFlowTotalNo.Text != "0" && txtOutFlowTotalNo.Text != "0.00")
            {
                TotalOutFlowNo = Convert.ToDecimal(txtOutFlowTotalNo.Text);
            }
            else
            {
                TotalOutFlowNo = 0;
            }
            if (txtOutFlowTotalQtyInKg.Text != "0" && txtOutFlowTotalQtyInKg.Text != "0.00")
            {
                TotalOutFlowQtyInKg = Convert.ToDecimal(txtOutFlowTotalQtyInKg.Text);
            }
            else
            {
                TotalOutFlowQtyInKg = 0;
            }
            

            Int32 IID = Convert.ToInt32(lblItem_id.Text);
            DataRow dr1 = dt.AsEnumerable().Where(r => ((Int32)r["Item_id"]).Equals(IID)).First();

            dr1["IsuuetoFPNoofCases"] = IsuuetoFPNoofCases;
            dr1["IsuuetoFPNo"] = IsuuetoFPNo;
            dr1["IsuuetoFPQtyInKg"] = IsuuetoFPQtyInKg;
            dr1["IsuuetoOtherNo"] = IsuuetoOtherNo;
            dr1["IsuuetoOtherQtyInKg"] = IsuuetoOtherQtyInKg;
            dr1["LeakagePackingNo"] = LeakagePackingNo;
            dr1["LeakagePackingQtyInKg"] = LeakagePackingQtyInKg;
            dr1["ClosingBalanceNo"] = ClosingBalanceNo;
            dr1["ClosingBalanceQtyInKg"] = ClosingBalanceQtyInKg;
            dr1["TotalOutFlowNo"] = TotalOutFlowNo;
            dr1["TotalOutFlowQtyInKg"] = TotalOutFlowQtyInKg;
           

        }
        return dt;
    }
    protected void btnPopupSave_Product_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                btnGetTotal_Click(sender, e);
                DataTable dtIDF = new DataTable();
                dtIDF = GetProductVarientInfo();

                if (dtIDF.Rows.Count > 0)
                {
                    if(btnPopupSave_Product.Text == "Save")
                    {
                        ds = null;
                        ds = objdb.ByProcedure("Sp_Production_GheeSheetNew",
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
                                                  new string[] { "1"
                                              ,objdb.Office_ID()
                                              , Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")
                                               ,ddlPSection.SelectedValue 
                                              ,"0"
                                              ,"0"
                                              , ViewState["Emp_ID"].ToString()
                                              ,objdb.GetLocalIPAddress()
                                              ,objdb.LooseGheeItemTypeId_ID()
                                              
                                    },
                                                 new string[] { "type_Production_GheeSheetNew" },
                                                 new DataTable[] { dtIDF }, "TableSave");


                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            GetSectionDetail();
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
                        ds = objdb.ByProcedure("Sp_Production_GheeSheetNew",
                                                  new string[] { "flag" 
                                                ,"GheeSheet_ID"
                                                
                                                
                                    },
                                                  new string[] { "4"
                                              ,ViewState["GheeSheet_ID"].ToString()
                                              
                                              
                                    },
                                                 new string[] { "type_Production_GheeSheetNew" },
                                                 new DataTable[] { dtIDF }, "TableSave");


                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            GetSectionDetail();
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

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());


            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);
        }
    }
}