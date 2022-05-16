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


public partial class mis_dailyplan_GheeIntermediateSheetMonthlyRpt : System.Web.UI.Page
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
                txtFromDate.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["currentDate"].ToString(), cult).ToString("dd/MM/yyyy");
                txtToDate.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["currentDate"].ToString(), cult).ToString("dd/MM/yyyy");
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
            lblMsgRecord.Text = "";
            string Fdate = "";
            string Tdate = "";
            if (txtFromDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            }
            if (txtToDate.Text != "")
            {
                Tdate = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            }
            // For Variant
            txtGainLossQtyInKg.Text = "";
            txtGainLossFatInKg.Text = "";
            //txtGainLossSnfInKg.Text = "";
            //Label1.Text = "";
            Label3.Text = "";
            //Label4.Text = "";
            //Label5.Text = "";
            Label6.Text = "";
            //Label7.Text = "";
            panel1.Visible = false;
            DataSet dsVD_Child = objdb.ByProcedure("Sp_Production_GheeIntermediateSheetChild",
            new string[] { "flag", "Office_ID", "FromDate","ToDate", "ProductSection_ID", "ItemType_id" },
            new string[] { "6", objdb.Office_ID(), Fdate,Tdate, ddlPSection.SelectedValue, objdb.LooseGheeItemTypeId_ID() }, "dataset");

            if (dsVD_Child != null && dsVD_Child.Tables.Count > 0)
            {
                if(dsVD_Child.Tables[0].Rows.Count > 0)
                {
                    panel1.Visible = true;
					spn.InnerText = txtFromDate.Text + " - " + txtToDate.Text;
                    GVVariantDetail_In.DataSource = dsVD_Child;
                    GVVariantDetail_In.DataBind();
                    GVVariantDetail_Out.DataSource = dsVD_Child;
                    GVVariantDetail_Out.DataBind();
                    GetTotal();
                }
                else
                {
                    GVVariantDetail_In.DataSource = string.Empty;
                    GVVariantDetail_In.DataBind();
                    GVVariantDetail_Out.DataSource = string.Empty;
                    GVVariantDetail_Out.DataBind();
                    lblMsgRecord.Text = "No Record Found";

                }
            }

            else
            {
                GVVariantDetail_In.DataSource = string.Empty;
                GVVariantDetail_In.DataBind();
                GVVariantDetail_Out.DataSource = string.Empty;
                GVVariantDetail_Out.DataBind();
                lblMsgRecord.Text = "No Record Found";

            }

           

        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    protected void GetTotal()
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
            decimal ButterRcvdfromButterSecQty= 0;
            decimal ButterRcvdfromButterSecFat = 0;
            decimal ButterRcvdfromButterSecSnf = 0;
            decimal GheeMfgQty = 0;
            decimal GheeMfgFat = 0;
            decimal GheeMfgSnf = 0;
            foreach (GridViewRow row1 in GVVariantDetail_In.Rows)
            {
                Label lblPckt = (Label)row1.FindControl("lblPckt");
                Label lblLoose = (Label)row1.FindControl("lblLoose");
                Label lblButterRcvdfromButterSec = (Label)row1.FindControl("lblButterRcvdfromButterSec");
                Label lblSourMilkRcvd = (Label)row1.FindControl("lblSourMilkRcvd");
                Label lblCurdMilkRcvd = (Label)row1.FindControl("lblCurdMilkRcvd");
                Label lblCreamRcvd = (Label)row1.FindControl("lblCreamRcvd");
                Label lblGheeRetrunFromFP = (Label)row1.FindControl("lblGheeRetrunFromFP");
                Label lblItemName = (Label)row1.FindControl("lblItemName");
                if (lblPckt.Text == "")
                {
                    lblPckt.Text = "0";
                }
                if (lblLoose.Text == "")
                {
                    lblLoose.Text = "0";
                }
                if (lblButterRcvdfromButterSec.Text == "")
                {
                    
                    lblButterRcvdfromButterSec.Text = "0";
                }
                if (lblSourMilkRcvd.Text == "")
                {

                    lblSourMilkRcvd.Text = "0";
                }
                if (lblCurdMilkRcvd.Text == "")
                {

                    lblCurdMilkRcvd.Text = "0";
                }
                if (lblCreamRcvd.Text == "")
                {

                    lblCreamRcvd.Text = "0";
                }
                if (lblGheeRetrunFromFP.Text == "")
                {
                    lblGheeRetrunFromFP.Text = "0";
                }
                if (lblItemName.Text == " Quantity In Kg")
                {
                    ButterRcvdfromButterSecQty = decimal.Parse(lblButterRcvdfromButterSec.Text);
                }
                if (lblItemName.Text == " Fat In Kg")
                {
                    ButterRcvdfromButterSecFat = decimal.Parse(lblButterRcvdfromButterSec.Text);
                }
                if (lblItemName.Text == " Snf In Kg")
                {
                    ButterRcvdfromButterSecSnf = decimal.Parse(lblButterRcvdfromButterSec.Text);
                }
                
                

                Label lblIntotal = (Label)row1.FindControl("lblIntotal");

                lblIntotal.Text = (Convert.ToDecimal(lblPckt.Text) + Convert.ToDecimal(lblLoose.Text) + Convert.ToDecimal(lblButterRcvdfromButterSec.Text) + Convert.ToDecimal(lblSourMilkRcvd.Text)
                    + Convert.ToDecimal(lblCurdMilkRcvd.Text) + Convert.ToDecimal(lblCreamRcvd.Text) + Convert.ToDecimal(lblGheeRetrunFromFP.Text)).ToString();
                if (lblIntotal.Text == "")
                {
                    lblIntotal.Text = "0";
                }
                if (lblItemName.Text == " Quantity In Kg")
                {
                    TotalQtyInflow = decimal.Parse(lblIntotal.Text);
                }
                if (lblItemName.Text == " Fat In Kg")
                {
                    TotalFatInflow = decimal.Parse(lblIntotal.Text);
                }
                if (lblItemName.Text == " Snf In Kg")
                {
                    TotalSnfInflow = decimal.Parse(lblIntotal.Text);
                }
                
            }


            foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
            {
                Label Label2 = (Label)row1.FindControl("Label2");
                Label txtGheeMfg = (Label)row1.FindControl("txtGheeMfg");
                Label txtOutFlowOther = (Label)row1.FindControl("txtOutFlowOther");
                Label txtGheeIssuetoFP = (Label)row1.FindControl("txtGheeIssuetoFP");
                Label txtLooseClosingBalance = (Label)row1.FindControl("txtLooseClosingBalance");
                Label txtPacketClosingBalance = (Label)row1.FindControl("txtPacketClosingBalance");
                Label txtSample = (Label)row1.FindControl("txtSample");
                if (txtOutFlowOther.Text == "")
                {
                    txtOutFlowOther.Text = "0";
                }
                if (txtGheeIssuetoFP.Text == "")
                {
                    txtGheeIssuetoFP.Text = "0";
                }
                if (txtLooseClosingBalance.Text == "")
                {
                    txtLooseClosingBalance.Text = "0";
                }

                if (txtPacketClosingBalance.Text == "")
                {
                    txtPacketClosingBalance.Text = "0";
                }

                if (txtSample.Text == "")
                {
                    txtSample.Text = "0";
                }
                Label lblouttotal = (Label)row1.FindControl("lblouttotal");

                lblouttotal.Text = (Convert.ToDecimal(txtOutFlowOther.Text) + Convert.ToDecimal(txtGheeIssuetoFP.Text) + Convert.ToDecimal(txtLooseClosingBalance.Text) + Convert.ToDecimal(txtPacketClosingBalance.Text) + Convert.ToDecimal(txtSample.Text)).ToString();
                if (lblouttotal.Text == "")
                {
                    lblouttotal.Text = "0";
                }
                if (Label2.Text == " Quantity In Kg")
                {
                    TotalQtyOutFlow = decimal.Parse(lblouttotal.Text);
                    GheeMfgQty = decimal.Parse(txtGheeMfg.Text);
                }
                if (Label2.Text == " Fat In Kg")
                {
                    TotalFatOutFlow = decimal.Parse(lblouttotal.Text);
                    GheeMfgFat = decimal.Parse(txtGheeMfg.Text);
                }
                if (Label2.Text == " Snf In Kg")
                {
                    TotalSnfOutFlow = decimal.Parse(lblouttotal.Text);
                    GheeMfgSnf = decimal.Parse(txtGheeMfg.Text);
                }
               
            }
            
            txtGainLossQtyInKg.Text = (TotalQtyOutFlow - TotalQtyInflow).ToString();
            txtGainLossFatInKg.Text = (TotalFatOutFlow - TotalFatInflow).ToString();
           // txtGainLossSnfInKg.Text = (TotalSnfOutFlow - TotalSnfInflow).ToString();
            
            //Label1.Text = (Math.Round((decimal.Parse(txtGainLossQtyInKg.Text) / ButterRcvdfromButterSecQty) * 100,2)).ToString();
            Label3.Text = (Math.Round((decimal.Parse(txtGainLossFatInKg.Text) / ButterRcvdfromButterSecFat) * 100, 2)).ToString();
            //Label4.Text = (Math.Round((decimal.Parse(txtGainLossSnfInKg.Text) / ButterRcvdfromButterSecSnf) * 100, 2)).ToString();

            //Label5.Text = (Math.Round((GheeMfgQty / ButterRcvdfromButterSecQty) * 100, 2)).ToString();
            Label6.Text = (Math.Round((GheeMfgFat / ButterRcvdfromButterSecFat) * 100, 2)).ToString();
            //Label7.Text = (Math.Round((GheeMfgSnf / ButterRcvdfromButterSecSnf) * 100, 2)).ToString();
            //decimal TotalGainLoss = Convert.ToDecimal(txtGainLossQtyInKg.Text) + Convert.ToDecimal(txtGainLossFatInKg.Text) + Convert.ToDecimal(txtGainLossSnfInKg.Text);
            
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
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "In Flow";
            HeaderCell.ColumnSpan = 12;
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
            HeaderCell.ColumnSpan =17;
            HeaderGridRow.Cells.Add(HeaderCell);


            GVVariantDetail_Out.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlPSection.SelectedValue != "0")
        {
            GetSectionDetail();
           
        }

    }
}