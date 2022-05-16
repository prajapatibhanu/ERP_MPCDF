using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;



public partial class mis_dailyplan_RptDailyDisposalSheet_New : System.Web.UI.Page
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
                    GetSectionView(sender, e);
                    FillShift();

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
            ds = objdb.ByProcedure("USP_MilkProductionEntry_New",
                 new string[] { "flag" },
                 new string[] { "11" }, "dataset");

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
                  new string[] { "flag", "Office_ID", "OfficeType_ID" },
                  new string[] { "21", ddlDS.SelectedValue, objdb.OfficeType_ID() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPSection.DataSource = ds.Tables[0];
                ddlPSection.DataTextField = "ProductSection_Name";
                ddlPSection.DataValueField = "ProductSection_ID";
                ddlPSection.DataBind();
                //ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.SelectedValue = "1";
                ddlPSection.Enabled = false;
                ddlPSection_SelectedIndexChanged(sender, e);

            }
            else
            {
                ddlPSection.Items.Clear();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.DataBind();
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
            ds = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                 new string[] { "flag", "Office_Id", "OfficeType_ID" },
                 new string[] { "3", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlShift.DataSource = ds.Tables[0];
                ddlShift.DataTextField = "Name";
                ddlShift.DataValueField = "Shift_Id";
                ddlShift.DataBind();
                ddlShift.SelectedValue = ds.Tables[0].Rows[0]["Shift_Id"].ToString();
                // txtDate.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["currentDate"].ToString(), cult).ToString("dd/MM/yyyy");
                //ddlShift.Enabled = false;
                //txtDate.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlPSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlPSection.SelectedValue != "0")
        //{
        //    divfinal.Visible = true;
        //    GetSectionDetail();
        //    ViewFinalDisposalSheet();
        //}
        //else
        //{
        //    gvDDSheet.DataSource = string.Empty;
        //    gvDDSheet.DataBind();
        //    divfinal.Visible = false;
        //}
    }

    private void ViewFinalDisposalSheet()
    {

        try
        {
            decimal InFlowTotalQtyInKg = 0;
            decimal InFlowTotalQtyInLtr = 0;
            decimal InFlowTotalFatInKg = 0;
            decimal InFlowTotalSnfInKg = 0;

            decimal OutFlowTotalQtyInKg = 0;
            decimal OutFlowTotalQtyInLtr = 0;
            decimal OutFlowTotalFatInKg = 0;
            decimal OutFlowTotalSnfInKg = 0;

            gvOpening.DataSource = string.Empty;
            gvOpening.DataBind();

            gvProcess.DataSource = string.Empty;
            gvProcess.DataBind();

            gvReturn.DataSource = string.Empty;
            gvReturn.DataBind();

            gvCCWiseProcurement.DataSource = string.Empty;
            gvCCWiseProcurement.DataBind();

            gvBMCDCSCollection.DataSource = string.Empty;
            gvBMCDCSCollection.DataBind();

            gvinflowother.DataSource = string.Empty;
            gvinflowother.DataBind();

            gvPaticulars.DataSource = string.Empty;
            gvPaticulars.DataBind();


            gvClosingBalances.DataSource = string.Empty;
            gvClosingBalances.DataBind();

            gvColdRoomBalances.DataSource = string.Empty;
            gvColdRoomBalances.DataBind();

            gvIssuetoMDPOrCC.DataSource = string.Empty;
            gvIssuetoMDPOrCC.DataBind();

            gvMilkToIP.DataSource = string.Empty;
            gvMilkToIP.DataBind();

            gvIsuuetoOther.DataSource = string.Empty;
            gvIsuuetoOther.DataBind();

            gvIssuetoCream.DataSource = string.Empty;
            gvIssuetoCream.DataBind();

            GVIssuetoPowderPlant.DataSource = string.Empty;
            GVIssuetoPowderPlant.DataBind();

            gvCanesCollection.DataSource = string.Empty;
            gvCanesCollection.DataBind();

            gvIssuetoCream.DataSource = string.Empty;
            gvIssuetoCream.DataBind();

            gvIssuetoIceCream.DataSource = string.Empty;
            gvIssuetoIceCream.DataBind();

            gvForPowderConversion.DataSource = string.Empty;
            gvForPowderConversion.DataBind();

            gvrcvdfrmothrUnion.DataSource = string.Empty;
            gvrcvdfrmothrUnion.DataBind();

            gvoutflowother.DataSource = string.Empty;
            gvoutflowother.DataBind();

            gvIsuuetoOther.DataSource = string.Empty;
            gvIsuuetoOther.DataBind();

            gvOther.DataSource = string.Empty;
            gvOther.DataBind();


            gvCCWiseGoatMilkProcurement.DataSource = string.Empty;
            gvCCWiseGoatMilkProcurement.DataBind();
            divfinal.Visible = false;
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }


            DataSet dsRecord = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                new string[] { "flag", "Office_ID", "Shift_Id", "Date", "ProductSection_ID" },
                new string[] { "2", objdb.Office_ID(), ddlShift.SelectedValue, Fdate, ddlPSection.SelectedValue }, "dataset");

            if (dsRecord != null && dsRecord.Tables.Count > 0)
            {
				spnOfficeName.InnerHtml = Session["Office_Name"].ToString();
                spndate.InnerHtml = txtDate.Text;
                spnshift.InnerHtml = ddlShift.SelectedItem.Text;
                divfinal.Visible = true;
                if (dsRecord.Tables[0].Rows.Count > 0)
                {
                    decimal OpeningFat = 0;
                    decimal OpeningSnf = 0;
                    decimal OpeningFatInKg = 0;
                    decimal OpeningSnfInKg = 0;
                    decimal OpeningQtyInKg = 0;
                    gvOpening.DataSource = dsRecord.Tables[0];
                    gvOpening.DataBind();
                    OpeningQtyInKg = dsRecord.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    OpeningFatInKg = dsRecord.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    OpeningSnfInKg = dsRecord.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));
                    OpeningFat = Math.Round(((OpeningFatInKg / OpeningQtyInKg) * 100), 2);
                    OpeningSnf = Math.Round(((OpeningSnfInKg / OpeningQtyInKg) * 100), 2);
                    InFlowTotalQtyInLtr += dsRecord.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("QtyInLtr"));
                    InFlowTotalQtyInKg += dsRecord.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    InFlowTotalFatInKg += dsRecord.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    InFlowTotalSnfInKg += dsRecord.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));
                    gvOpening.FooterRow.Cells[0].Text = "<b>Total : </b>";
                    gvOpening.FooterRow.Cells[1].Text = "<b>" + OpeningQtyInKg.ToString() + "</b>";
                    gvOpening.FooterRow.Cells[3].Text = "<b>" + OpeningFat.ToString() + "</b>";
                    gvOpening.FooterRow.Cells[4].Text = "<b>" + OpeningSnf.ToString() + "</b>";
                    gvOpening.FooterRow.Cells[5].Text = "<b>" + OpeningFatInKg.ToString() + "</b>";
                    gvOpening.FooterRow.Cells[6].Text = "<b>" + OpeningSnfInKg.ToString() + "</b>";
                    gvOpening.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                    gvOpening.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                    gvOpening.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    gvOpening.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                    gvOpening.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;

                }
                if (dsRecord.Tables[1].Rows.Count > 0)
                {
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal FatInKg = 0;
                    decimal SnfInKg = 0;
                    decimal QtyInKg = 0;
                    gvProcess.DataSource = dsRecord.Tables[1];
                    gvProcess.DataBind();

                    QtyInKg = dsRecord.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    FatInKg = dsRecord.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    SnfInKg = dsRecord.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));
                    Fat = Math.Round(((FatInKg / QtyInKg) * 100), 2);
                    Snf = Math.Round(((SnfInKg / QtyInKg) * 100), 2);
                    InFlowTotalQtyInLtr += dsRecord.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("QtyInLtr"));
                    InFlowTotalQtyInKg += dsRecord.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    InFlowTotalFatInKg += dsRecord.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    InFlowTotalSnfInKg += dsRecord.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));

                    gvProcess.FooterRow.Cells[2].Text = "<b>Total : </b>";
                    gvProcess.FooterRow.Cells[3].Text = "<b>" + QtyInKg.ToString() + "</b>";
                    gvProcess.FooterRow.Cells[4].Text = "<b>" + Fat.ToString() + "</b>";
                    gvProcess.FooterRow.Cells[5].Text = "<b>" + Snf.ToString() + "</b>";
                    gvProcess.FooterRow.Cells[6].Text = "<b>" + FatInKg.ToString() + "</b>";
                    gvProcess.FooterRow.Cells[7].Text = "<b>" + SnfInKg.ToString() + "</b>";
                    gvProcess.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                    gvProcess.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    gvProcess.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                    gvProcess.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                    gvProcess.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;


                }
                if (dsRecord.Tables[2].Rows.Count > 0)
                {
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal FatInKg = 0;
                    decimal SnfInKg = 0;
                    decimal QtyInKg = 0;
                    gvReturn.DataSource = dsRecord.Tables[2];
                    gvReturn.DataBind();
                    QtyInKg = dsRecord.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    FatInKg = dsRecord.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    SnfInKg = dsRecord.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));
                    Fat = Math.Round(((FatInKg / QtyInKg) * 100), 2);
                    Snf = Math.Round(((SnfInKg / QtyInKg) * 100), 2);

                    InFlowTotalQtyInLtr += dsRecord.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("PackedQtyInLtr"));
                    InFlowTotalQtyInKg += dsRecord.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("PackedQtyInKg"));
                    InFlowTotalFatInKg += dsRecord.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    InFlowTotalSnfInKg += dsRecord.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));

                    gvReturn.FooterRow.Cells[0].Text = "<b>Total : </b>";
                    gvReturn.FooterRow.Cells[1].Text = "<b>" + QtyInKg.ToString() + "</b>";
                    gvReturn.FooterRow.Cells[4].Text = "<b>" + Fat.ToString() + "</b>";
                    gvReturn.FooterRow.Cells[5].Text = "<b>" + Snf.ToString() + "</b>";
                    gvReturn.FooterRow.Cells[6].Text = "<b>" + FatInKg.ToString() + "</b>";
                    gvReturn.FooterRow.Cells[7].Text = "<b>" + SnfInKg.ToString() + "</b>";
                    gvReturn.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                    gvReturn.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    gvReturn.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                    gvReturn.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                    gvReturn.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;
                }
                if (dsRecord.Tables[3].Rows.Count > 0)
                {
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal FatInKg = 0;
                    decimal SnfInKg = 0;
                    decimal QtyInKg = 0;
                    gvCCWiseProcurement.DataSource = dsRecord.Tables[3];
                    gvCCWiseProcurement.DataBind();
                    QtyInKg = dsRecord.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    FatInKg = dsRecord.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    SnfInKg = dsRecord.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                    Fat = Math.Round(((FatInKg / QtyInKg) * 100), 2);
                    Snf = Math.Round(((SnfInKg / QtyInKg) * 100), 2);
                    gvCCWiseProcurement.FooterRow.Cells[3].Text = "<b>Total : </b>";
                    gvCCWiseProcurement.FooterRow.Cells[4].Text = "<b>" + QtyInKg.ToString() + "</b>";
                    gvCCWiseProcurement.FooterRow.Cells[6].Text = "<b>" + Fat.ToString() + "</b>";
                    gvCCWiseProcurement.FooterRow.Cells[7].Text = "<b>" + Snf.ToString() + "</b>";
                    gvCCWiseProcurement.FooterRow.Cells[8].Text = "<b>" + FatInKg.ToString() + "</b>";
                    gvCCWiseProcurement.FooterRow.Cells[9].Text = "<b>" + SnfInKg.ToString() + "</b>";
                    gvCCWiseProcurement.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    gvCCWiseProcurement.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                    gvCCWiseProcurement.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;
                    gvCCWiseProcurement.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Left;
                    gvCCWiseProcurement.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Left;

                    InFlowTotalQtyInLtr += dsRecord.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("QtyInLtr"));
                    InFlowTotalQtyInKg += dsRecord.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    InFlowTotalFatInKg += dsRecord.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    InFlowTotalSnfInKg += dsRecord.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));

                }
                if (dsRecord.Tables[4].Rows.Count > 0)
                {
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal FatInKg = 0;
                    decimal SnfInKg = 0;
                    decimal QtyInKg = 0;
                    gvrcvdfrmothrUnion.DataSource = dsRecord.Tables[4];
                    gvrcvdfrmothrUnion.DataBind();
                    QtyInKg = dsRecord.Tables[4].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    FatInKg = dsRecord.Tables[4].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    SnfInKg = dsRecord.Tables[4].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                    Fat = Math.Round(((FatInKg / QtyInKg) * 100), 2);
                    Snf = Math.Round(((SnfInKg / QtyInKg) * 100), 2);

                    InFlowTotalQtyInLtr +=dsRecord.Tables[4].AsEnumerable().Sum(row => row.Field<decimal>("QtyInLtr"));
                    InFlowTotalQtyInKg += dsRecord.Tables[4].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    InFlowTotalFatInKg += dsRecord.Tables[4].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    InFlowTotalSnfInKg += dsRecord.Tables[4].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));

                    gvrcvdfrmothrUnion.FooterRow.Cells[1].Text = "<b>Total : </b>";
                    gvrcvdfrmothrUnion.FooterRow.Cells[2].Text = "<b>" + QtyInKg.ToString() + "</b>";
                    gvrcvdfrmothrUnion.FooterRow.Cells[4].Text = "<b>" + Fat.ToString() + "</b>";
                    gvrcvdfrmothrUnion.FooterRow.Cells[5].Text = "<b>" + Snf.ToString() + "</b>";
                    gvrcvdfrmothrUnion.FooterRow.Cells[6].Text = "<b>" + FatInKg.ToString() + "</b>";
                    gvrcvdfrmothrUnion.FooterRow.Cells[7].Text = "<b>" + SnfInKg.ToString() + "</b>";
                    gvrcvdfrmothrUnion.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                    gvrcvdfrmothrUnion.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    gvrcvdfrmothrUnion.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                    gvrcvdfrmothrUnion.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                    gvrcvdfrmothrUnion.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;

                }
                if (dsRecord.Tables[5].Rows.Count > 0)
                {
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal FatInKg = 0;
                    decimal SnfInKg = 0;
                    decimal QtyInKg = 0;
                    gvForPowderConversion.DataSource = dsRecord.Tables[5];
                    gvForPowderConversion.DataBind();

                    QtyInKg = dsRecord.Tables[5].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    FatInKg = dsRecord.Tables[5].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    SnfInKg = dsRecord.Tables[5].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                    Fat = Math.Round(((FatInKg / QtyInKg) * 100), 2);
                    Snf = Math.Round(((SnfInKg / QtyInKg) * 100), 2);
                    InFlowTotalQtyInLtr +=dsRecord.Tables[5].AsEnumerable().Sum(row => row.Field<decimal>("QtyInLtr"));
                    InFlowTotalQtyInKg += dsRecord.Tables[5].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    InFlowTotalFatInKg += dsRecord.Tables[5].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    InFlowTotalSnfInKg += dsRecord.Tables[5].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));

                    gvForPowderConversion.FooterRow.Cells[1].Text = "<b>Total : </b>";
                    gvForPowderConversion.FooterRow.Cells[2].Text = "<b>" + QtyInKg.ToString() + "</b>";
                    gvForPowderConversion.FooterRow.Cells[4].Text = "<b>" + Fat.ToString() + "</b>";
                    gvForPowderConversion.FooterRow.Cells[5].Text = "<b>" + Snf.ToString() + "</b>";
                    gvForPowderConversion.FooterRow.Cells[6].Text = "<b>" + FatInKg.ToString() + "</b>";
                    gvForPowderConversion.FooterRow.Cells[7].Text = "<b>" + SnfInKg.ToString() + "</b>";
                    gvForPowderConversion.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                    gvForPowderConversion.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    gvForPowderConversion.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                    gvForPowderConversion.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                    gvForPowderConversion.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;

                }
                if (dsRecord.Tables[6].Rows.Count > 0)
                {
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal FatInKg = 0;
                    decimal SnfInKg = 0;
                    decimal QtyInKg = 0;
                    gvBMCDCSCollection.DataSource = dsRecord.Tables[6];
                    gvBMCDCSCollection.DataBind();
                    QtyInKg = dsRecord.Tables[6].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    FatInKg = dsRecord.Tables[6].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    SnfInKg = dsRecord.Tables[6].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                    Fat = Math.Round(((FatInKg / QtyInKg) * 100), 2);
                    Snf = Math.Round(((SnfInKg / QtyInKg) * 100), 2);
                    gvBMCDCSCollection.FooterRow.Cells[2].Text = "<b>Total : </b>";
                    gvBMCDCSCollection.FooterRow.Cells[3].Text = "<b>" + QtyInKg.ToString() + "</b>";
                    gvBMCDCSCollection.FooterRow.Cells[4].Text = "<b>" + Fat.ToString() + "</b>";
                    gvBMCDCSCollection.FooterRow.Cells[5].Text = "<b>" + Snf.ToString() + "</b>";
                    gvBMCDCSCollection.FooterRow.Cells[6].Text = "<b>" + FatInKg.ToString() + "</b>";
                    gvBMCDCSCollection.FooterRow.Cells[7].Text = "<b>" + SnfInKg.ToString() + "</b>";
                    gvBMCDCSCollection.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                    gvBMCDCSCollection.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    gvBMCDCSCollection.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                    gvBMCDCSCollection.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                    gvBMCDCSCollection.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;
                    InFlowTotalQtyInLtr +=dsRecord.Tables[6].AsEnumerable().Sum(row => row.Field<decimal>("QtyInLtr"));
                    InFlowTotalQtyInKg += dsRecord.Tables[6].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    InFlowTotalFatInKg += dsRecord.Tables[6].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    InFlowTotalSnfInKg += dsRecord.Tables[6].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));

                }
                if (dsRecord.Tables[7].Rows.Count > 0)
                {
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal FatInKg = 0;
                    decimal SnfInKg = 0;
                    decimal QtyInKg = 0;
                    gvinflowother.DataSource = dsRecord.Tables[7];
                    gvinflowother.DataBind();
                    QtyInKg = dsRecord.Tables[7].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    FatInKg = dsRecord.Tables[7].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    SnfInKg = dsRecord.Tables[7].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                    Fat = Math.Round(((FatInKg / QtyInKg) * 100), 2);
                    Snf = Math.Round(((SnfInKg / QtyInKg) * 100), 2);


                    InFlowTotalQtyInLtr +=dsRecord.Tables[7].AsEnumerable().Sum(row => row.Field<decimal>("QtyInLtr"));
                    InFlowTotalQtyInKg += dsRecord.Tables[7].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    InFlowTotalFatInKg += dsRecord.Tables[7].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    InFlowTotalSnfInKg += dsRecord.Tables[7].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));

                    gvinflowother.FooterRow.Cells[0].Text = "<b>Total : </b>";
                    gvinflowother.FooterRow.Cells[1].Text = "<b>" + QtyInKg.ToString() + "</b>";
                    gvinflowother.FooterRow.Cells[2].Text = "<b>" + Fat.ToString() + "</b>";
                    gvinflowother.FooterRow.Cells[3].Text = "<b>" + Snf.ToString() + "</b>";
                    gvinflowother.FooterRow.Cells[4].Text = "<b>" + FatInKg.ToString() + "</b>";
                    gvinflowother.FooterRow.Cells[5].Text = "<b>" + SnfInKg.ToString() + "</b>";
                    gvinflowother.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                    gvinflowother.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                    gvinflowother.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                    gvinflowother.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    gvinflowother.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;

                }
                if (dsRecord.Tables[18].Rows.Count > 0)
                {
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal FatInKg = 0;
                    decimal SnfInKg = 0;
                    decimal QtyInKg = 0;
                    gvCanesCollection.DataSource = dsRecord.Tables[18];
                    gvCanesCollection.DataBind();
                    QtyInKg = dsRecord.Tables[18].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    FatInKg = dsRecord.Tables[18].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    SnfInKg = dsRecord.Tables[18].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                    Fat = Math.Round(((FatInKg / QtyInKg) * 100), 2);
                    Snf = Math.Round(((SnfInKg / QtyInKg) * 100), 2);


                    InFlowTotalQtyInLtr += dsRecord.Tables[18].AsEnumerable().Sum(row => row.Field<decimal>("QtyInLtr"));
                    InFlowTotalQtyInKg += dsRecord.Tables[18].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    InFlowTotalFatInKg += dsRecord.Tables[18].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    InFlowTotalSnfInKg += dsRecord.Tables[18].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));

                    gvCanesCollection.FooterRow.Cells[1].Text = "<b>Total : </b>";
                    gvCanesCollection.FooterRow.Cells[2].Text = "<b>" + QtyInKg.ToString() + "</b>";
                    gvCanesCollection.FooterRow.Cells[3].Text = "<b>" + Fat.ToString() + "</b>";
                    gvCanesCollection.FooterRow.Cells[4].Text = "<b>" + Snf.ToString() + "</b>";
                    gvCanesCollection.FooterRow.Cells[5].Text = "<b>" + FatInKg.ToString() + "</b>";
                    gvCanesCollection.FooterRow.Cells[6].Text = "<b>" + SnfInKg.ToString() + "</b>";
                    gvCanesCollection.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                    gvCanesCollection.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                    gvCanesCollection.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    gvCanesCollection.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                    gvCanesCollection.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;

                }

                if (dsRecord.Tables[8].Rows.Count > 0)
                {
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal FatInKg = 0;
                    decimal SnfInKg = 0;
                    decimal QtyInKg = 0;
                    gvPaticulars.DataSource = dsRecord.Tables[8];
                    gvPaticulars.DataBind();
                    QtyInKg = dsRecord.Tables[8].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    FatInKg = dsRecord.Tables[8].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    SnfInKg = dsRecord.Tables[8].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));
                    Fat = Math.Round(((FatInKg / QtyInKg) * 100), 2);
                    Snf = Math.Round(((SnfInKg / QtyInKg) * 100), 2);


                    OutFlowTotalQtyInLtr +=dsRecord.Tables[8].AsEnumerable().Sum(row => row.Field<decimal>("QtyInLtr"));
                    OutFlowTotalQtyInKg += dsRecord.Tables[8].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    OutFlowTotalFatInKg += dsRecord.Tables[8].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    OutFlowTotalSnfInKg += dsRecord.Tables[8].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));

                    gvPaticulars.FooterRow.Cells[0].Text = "<b>Total : </b>";
                    gvPaticulars.FooterRow.Cells[1].Text = "<b>" + QtyInKg.ToString() + "</b>";
                    gvPaticulars.FooterRow.Cells[4].Text = "<b>" + Fat.ToString() + "</b>";
                    gvPaticulars.FooterRow.Cells[5].Text = "<b>" + Snf.ToString() + "</b>";
                    gvPaticulars.FooterRow.Cells[6].Text = "<b>" + FatInKg.ToString() + "</b>";
                    gvPaticulars.FooterRow.Cells[7].Text = "<b>" + SnfInKg.ToString() + "</b>";
                    gvPaticulars.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                    gvPaticulars.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    gvPaticulars.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                    gvPaticulars.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                    gvPaticulars.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;
                }
                if (dsRecord.Tables[9].Rows.Count > 0)
                {
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal FatInKg = 0;
                    decimal SnfInKg = 0;
                    decimal QtyInKg = 0;
                    gvIssuetoMDPOrCC.DataSource = dsRecord.Tables[9];
                    gvIssuetoMDPOrCC.DataBind();
                    QtyInKg = dsRecord.Tables[9].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    FatInKg = dsRecord.Tables[9].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    SnfInKg = dsRecord.Tables[9].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                    Fat = Math.Round(((FatInKg / QtyInKg) * 100), 2);
                    Snf = Math.Round(((SnfInKg / QtyInKg) * 100), 2);


                    OutFlowTotalQtyInLtr +=dsRecord.Tables[9].AsEnumerable().Sum(row => row.Field<decimal>("QtyInLtr"));
                    OutFlowTotalQtyInKg += dsRecord.Tables[9].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    OutFlowTotalFatInKg += dsRecord.Tables[9].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    OutFlowTotalSnfInKg += dsRecord.Tables[9].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));

                    gvIssuetoMDPOrCC.FooterRow.Cells[0].Text = "<b>Total : </b>";
                    gvIssuetoMDPOrCC.FooterRow.Cells[4].Text = "<b>" + QtyInKg.ToString() + "</b>";
                    gvIssuetoMDPOrCC.FooterRow.Cells[6].Text = "<b>" + Fat.ToString() + "</b>";
                    gvIssuetoMDPOrCC.FooterRow.Cells[7].Text = "<b>" + Snf.ToString() + "</b>";
                    gvIssuetoMDPOrCC.FooterRow.Cells[8].Text = "<b>" + FatInKg.ToString() + "</b>";
                    gvIssuetoMDPOrCC.FooterRow.Cells[9].Text = "<b>" + SnfInKg.ToString() + "</b>";
                    gvIssuetoMDPOrCC.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    gvIssuetoMDPOrCC.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                    gvIssuetoMDPOrCC.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;
                    gvIssuetoMDPOrCC.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Left;
                    gvIssuetoMDPOrCC.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Left;


                }
                if (dsRecord.Tables[10].Rows.Count > 0)
                {
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal FatInKg = 0;
                    decimal SnfInKg = 0;
                    decimal QtyInKg = 0;
                    gvMilkToIP.DataSource = dsRecord.Tables[10];
                    gvMilkToIP.DataBind();
                    QtyInKg = dsRecord.Tables[10].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    FatInKg = dsRecord.Tables[10].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    SnfInKg = dsRecord.Tables[10].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));
                    Fat = Math.Round(((FatInKg / QtyInKg) * 100), 2);
                    Snf = Math.Round(((SnfInKg / QtyInKg) * 100), 2);

                    OutFlowTotalQtyInLtr +=dsRecord.Tables[10].AsEnumerable().Sum(row => row.Field<decimal>("QtyInLtr"));
                    OutFlowTotalQtyInKg += dsRecord.Tables[10].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    OutFlowTotalFatInKg += dsRecord.Tables[10].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    OutFlowTotalSnfInKg += dsRecord.Tables[10].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));

                    gvMilkToIP.FooterRow.Cells[1].Text = "<b>Total : </b>";
                    gvMilkToIP.FooterRow.Cells[2].Text = "<b>" + QtyInKg.ToString() + "</b>";
                    gvMilkToIP.FooterRow.Cells[4].Text = "<b>" + Fat.ToString() + "</b>";
                    gvMilkToIP.FooterRow.Cells[5].Text = "<b>" + Snf.ToString() + "</b>";
                    gvMilkToIP.FooterRow.Cells[6].Text = "<b>" + FatInKg.ToString() + "</b>";
                    gvMilkToIP.FooterRow.Cells[7].Text = "<b>" + SnfInKg.ToString() + "</b>";
                    gvMilkToIP.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                    gvMilkToIP.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    gvMilkToIP.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                    gvMilkToIP.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                    gvMilkToIP.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;


                }
                if (dsRecord.Tables[11].Rows.Count > 0)
                {
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal FatInKg = 0;
                    decimal SnfInKg = 0;
                    decimal QtyInKg = 0;
                    gvIsuuetoOther.DataSource = dsRecord.Tables[11];
                    gvIsuuetoOther.DataBind();
                    QtyInKg = dsRecord.Tables[11].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    FatInKg = dsRecord.Tables[11].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    SnfInKg = dsRecord.Tables[11].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                    Fat = Math.Round(((FatInKg / QtyInKg) * 100), 2);
                    Snf = Math.Round(((SnfInKg / QtyInKg) * 100), 2);


                    OutFlowTotalQtyInLtr +=dsRecord.Tables[11].AsEnumerable().Sum(row => row.Field<decimal>("QtyInLtr"));
                    OutFlowTotalQtyInKg += dsRecord.Tables[11].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    OutFlowTotalFatInKg += dsRecord.Tables[11].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    OutFlowTotalSnfInKg += dsRecord.Tables[11].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));

                    gvIsuuetoOther.FooterRow.Cells[0].Text = "<b>Total : </b>";
                    gvIsuuetoOther.FooterRow.Cells[4].Text = "<b>" + QtyInKg.ToString() + "</b>";
                    gvIsuuetoOther.FooterRow.Cells[6].Text = "<b>" + Fat.ToString() + "</b>";
                    gvIsuuetoOther.FooterRow.Cells[7].Text = "<b>" + Snf.ToString() + "</b>";
                    gvIsuuetoOther.FooterRow.Cells[8].Text = "<b>" + FatInKg.ToString() + "</b>";
                    gvIsuuetoOther.FooterRow.Cells[9].Text = "<b>" + SnfInKg.ToString() + "</b>";
                    gvIsuuetoOther.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    gvIsuuetoOther.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                    gvIsuuetoOther.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;
                    gvIsuuetoOther.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Left;
                    gvIsuuetoOther.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Left;


                }
                if (dsRecord.Tables[12].Rows.Count > 0)
                {
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal FatInKg = 0;
                    decimal SnfInKg = 0;
                    decimal QtyInKg = 0;
                    gvoutflowother.DataSource = dsRecord.Tables[12];
                    gvoutflowother.DataBind();
                    QtyInKg = dsRecord.Tables[12].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    FatInKg = dsRecord.Tables[12].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    SnfInKg = dsRecord.Tables[12].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                    Fat = Math.Round(((FatInKg / QtyInKg) * 100), 2);
                    Snf = Math.Round(((SnfInKg / QtyInKg) * 100), 2);


                    OutFlowTotalQtyInLtr +=dsRecord.Tables[12].AsEnumerable().Sum(row => row.Field<decimal>("QtyInLtr"));
                    OutFlowTotalQtyInKg += dsRecord.Tables[12].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    OutFlowTotalFatInKg += dsRecord.Tables[12].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    OutFlowTotalSnfInKg += dsRecord.Tables[12].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));

                    gvoutflowother.FooterRow.Cells[0].Text = "<b>Total : </b>";
                    gvoutflowother.FooterRow.Cells[1].Text = "<b>" + QtyInKg.ToString() + "</b>";
                    gvoutflowother.FooterRow.Cells[3].Text = "<b>" + Fat.ToString() + "</b>";
                    gvoutflowother.FooterRow.Cells[4].Text = "<b>" + Snf.ToString() + "</b>";
                    gvoutflowother.FooterRow.Cells[5].Text = "<b>" + FatInKg.ToString() + "</b>";
                    gvoutflowother.FooterRow.Cells[6].Text = "<b>" + SnfInKg.ToString() + "</b>";
                    gvoutflowother.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                    gvoutflowother.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                    gvoutflowother.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    gvoutflowother.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                    gvoutflowother.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;

                }
                if (dsRecord.Tables[13].Rows.Count > 0)
                {
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal FatInKg = 0;
                    decimal SnfInKg = 0;
                    decimal QtyInKg = 0;
                    gvOther.DataSource = dsRecord.Tables[13];
                    gvOther.DataBind();
                    QtyInKg = dsRecord.Tables[13].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    FatInKg = dsRecord.Tables[13].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    SnfInKg = dsRecord.Tables[13].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                    Fat = Math.Round(((FatInKg / QtyInKg) * 100), 2);
                    Snf = Math.Round(((SnfInKg / QtyInKg) * 100), 2);


                    OutFlowTotalQtyInLtr +=dsRecord.Tables[13].AsEnumerable().Sum(row => row.Field<decimal>("QtyInLtr"));
                    OutFlowTotalQtyInKg += dsRecord.Tables[13].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    OutFlowTotalFatInKg += dsRecord.Tables[13].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    OutFlowTotalSnfInKg += dsRecord.Tables[13].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));

                    gvOther.FooterRow.Cells[0].Text = "<b>Total : </b>";
                    gvOther.FooterRow.Cells[1].Text = "<b>" + QtyInKg.ToString() + "</b>";
                    gvOther.FooterRow.Cells[3].Text = "<b>" + Fat.ToString() + "</b>";
                    gvOther.FooterRow.Cells[4].Text = "<b>" + Snf.ToString() + "</b>";
                    gvOther.FooterRow.Cells[5].Text = "<b>" + FatInKg.ToString() + "</b>";
                    gvOther.FooterRow.Cells[6].Text = "<b>" + SnfInKg.ToString() + "</b>";
                    gvOther.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                    gvOther.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                    gvOther.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    gvOther.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                    gvOther.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;


                }
                if (dsRecord.Tables[15].Rows.Count > 0)
                {
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal FatInKg = 0;
                    decimal SnfInKg = 0;
                    decimal QtyInKg = 0;
                    gvColdRoomBalances.DataSource = dsRecord.Tables[15];
                    gvColdRoomBalances.DataBind();
                    QtyInKg = dsRecord.Tables[15].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    FatInKg = dsRecord.Tables[15].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    SnfInKg = dsRecord.Tables[15].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));
                    Fat = Math.Round(((FatInKg / QtyInKg) * 100), 2);
                    Snf = Math.Round(((SnfInKg / QtyInKg) * 100), 2);

                    OutFlowTotalQtyInLtr +=dsRecord.Tables[15].AsEnumerable().Sum(row => row.Field<decimal>("QtyInLtr"));
                    OutFlowTotalQtyInKg += dsRecord.Tables[15].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    OutFlowTotalFatInKg += dsRecord.Tables[15].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    OutFlowTotalSnfInKg += dsRecord.Tables[15].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));

                    gvColdRoomBalances.FooterRow.Cells[2].Text = "<b>Total : </b>";
                    gvColdRoomBalances.FooterRow.Cells[3].Text = "<b>" + QtyInKg.ToString() + "</b>";
                    gvColdRoomBalances.FooterRow.Cells[4].Text = "<b>" + Fat.ToString() + "</b>";
                    gvColdRoomBalances.FooterRow.Cells[5].Text = "<b>" + Snf.ToString() + "</b>";
                    gvColdRoomBalances.FooterRow.Cells[6].Text = "<b>" + FatInKg.ToString() + "</b>";
                    gvColdRoomBalances.FooterRow.Cells[7].Text = "<b>" + SnfInKg.ToString() + "</b>";
                    gvColdRoomBalances.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                    gvColdRoomBalances.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    gvColdRoomBalances.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                    gvColdRoomBalances.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                    gvColdRoomBalances.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;


                }
                if (dsRecord.Tables[14].Rows.Count > 0)
                {
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal FatInKg = 0;
                    decimal SnfInKg = 0;
                    decimal QtyInKg = 0;
                    gvClosingBalances.DataSource = dsRecord.Tables[14];
                    gvClosingBalances.DataBind();
                    QtyInKg = dsRecord.Tables[14].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    FatInKg = dsRecord.Tables[14].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    SnfInKg = dsRecord.Tables[14].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));
                    Fat = Math.Round(((FatInKg / QtyInKg) * 100), 2);
                    Snf = Math.Round(((SnfInKg / QtyInKg) * 100), 2);


                    OutFlowTotalQtyInLtr +=dsRecord.Tables[14].AsEnumerable().Sum(row => row.Field<decimal>("QtyInLtr"));
                    OutFlowTotalQtyInKg += dsRecord.Tables[14].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    OutFlowTotalFatInKg += dsRecord.Tables[14].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    OutFlowTotalSnfInKg += dsRecord.Tables[14].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));

                    gvClosingBalances.FooterRow.Cells[0].Text = "<b>Total : </b>";
                    gvClosingBalances.FooterRow.Cells[1].Text = "<b>" + QtyInKg.ToString() + "</b>";
                    gvClosingBalances.FooterRow.Cells[3].Text = "<b>" + Fat.ToString() + "</b>";
                    gvClosingBalances.FooterRow.Cells[4].Text = "<b>" + Snf.ToString() + "</b>";
                    gvClosingBalances.FooterRow.Cells[5].Text = "<b>" + FatInKg.ToString() + "</b>";
                    gvClosingBalances.FooterRow.Cells[6].Text = "<b>" + SnfInKg.ToString() + "</b>";
                    gvClosingBalances.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                    gvClosingBalances.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                    gvClosingBalances.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    gvClosingBalances.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                    gvClosingBalances.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                }
                if (dsRecord.Tables[16].Rows.Count > 0)
                {
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal FatInKg = 0;
                    decimal SnfInKg = 0;
                    decimal QtyInKg = 0;
                    gvIssuetoCream.DataSource = dsRecord.Tables[16];
                    gvIssuetoCream.DataBind();
                    QtyInKg = dsRecord.Tables[16].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    FatInKg = dsRecord.Tables[16].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    SnfInKg = dsRecord.Tables[16].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));
                    Fat = Math.Round(((FatInKg / QtyInKg) * 100), 2);
                    Snf = Math.Round(((SnfInKg / QtyInKg) * 100), 2);


                    OutFlowTotalQtyInLtr +=dsRecord.Tables[16].AsEnumerable().Sum(row => row.Field<decimal>("QtyInLtr"));
                    OutFlowTotalQtyInKg += dsRecord.Tables[16].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    OutFlowTotalFatInKg += dsRecord.Tables[16].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    OutFlowTotalSnfInKg += dsRecord.Tables[16].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));

                    gvIssuetoCream.FooterRow.Cells[0].Text = "<b>Total : </b>";
                    gvIssuetoCream.FooterRow.Cells[1].Text = "<b>" + QtyInKg.ToString() + "</b>";
                    gvIssuetoCream.FooterRow.Cells[3].Text = "<b>" + Fat.ToString() + "</b>";
                    gvIssuetoCream.FooterRow.Cells[4].Text = "<b>" + Snf.ToString() + "</b>";
                    gvIssuetoCream.FooterRow.Cells[5].Text = "<b>" + FatInKg.ToString() + "</b>";
                    gvIssuetoCream.FooterRow.Cells[6].Text = "<b>" + SnfInKg.ToString() + "</b>";
                    gvIssuetoCream.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                    gvIssuetoCream.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                    gvIssuetoCream.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    gvIssuetoCream.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                    gvIssuetoCream.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                }
                if (dsRecord.Tables[17].Rows.Count > 0)
                {
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal FatInKg = 0;
                    decimal SnfInKg = 0;
                    decimal QtyInKg = 0;
                    GVIssuetoPowderPlant.DataSource = dsRecord.Tables[17];
                    GVIssuetoPowderPlant.DataBind();
                    QtyInKg = dsRecord.Tables[17].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    FatInKg = dsRecord.Tables[17].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    SnfInKg = dsRecord.Tables[17].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));
                    Fat = Math.Round(((FatInKg / QtyInKg) * 100), 2);
                    Snf = Math.Round(((SnfInKg / QtyInKg) * 100), 2);


                    OutFlowTotalQtyInLtr +=dsRecord.Tables[17].AsEnumerable().Sum(row => row.Field<decimal>("QtyInLtr"));
                    OutFlowTotalQtyInKg += dsRecord.Tables[17].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    OutFlowTotalFatInKg += dsRecord.Tables[17].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    OutFlowTotalSnfInKg += dsRecord.Tables[17].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));

                    GVIssuetoPowderPlant.FooterRow.Cells[1].Text = "<b>Total : </b>";
                    GVIssuetoPowderPlant.FooterRow.Cells[2].Text = "<b>" + QtyInKg.ToString() + "</b>";
                    GVIssuetoPowderPlant.FooterRow.Cells[4].Text = "<b>" + Fat.ToString() + "</b>";
                    GVIssuetoPowderPlant.FooterRow.Cells[5].Text = "<b>" + Snf.ToString() + "</b>";
                    GVIssuetoPowderPlant.FooterRow.Cells[6].Text = "<b>" + FatInKg.ToString() + "</b>";
                    GVIssuetoPowderPlant.FooterRow.Cells[7].Text = "<b>" + SnfInKg.ToString() + "</b>";
                    GVIssuetoPowderPlant.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                    GVIssuetoPowderPlant.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    GVIssuetoPowderPlant.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                    GVIssuetoPowderPlant.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                    GVIssuetoPowderPlant.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;
                }
                if (dsRecord.Tables[19].Rows.Count > 0)
                {
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal FatInKg = 0;
                    decimal SnfInKg = 0;
                    decimal QtyInKg = 0;
                    gvIssuetoIceCream.DataSource = dsRecord.Tables[19];
                    gvIssuetoIceCream.DataBind();
                    QtyInKg = dsRecord.Tables[19].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    FatInKg = dsRecord.Tables[19].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    SnfInKg = dsRecord.Tables[19].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));
                    Fat = Math.Round(((FatInKg / QtyInKg) * 100), 2);
                    Snf = Math.Round(((SnfInKg / QtyInKg) * 100), 2);


                    OutFlowTotalQtyInLtr +=dsRecord.Tables[19].AsEnumerable().Sum(row => row.Field<decimal>("QtyInLtr"));
                    OutFlowTotalQtyInKg += dsRecord.Tables[19].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    OutFlowTotalFatInKg += dsRecord.Tables[19].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                    OutFlowTotalSnfInKg += dsRecord.Tables[19].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));

                    gvIssuetoIceCream.FooterRow.Cells[0].Text = "<b>Total : </b>";
                    gvIssuetoIceCream.FooterRow.Cells[2].Text = "<b>" + QtyInKg.ToString() + "</b>";
                    gvIssuetoIceCream.FooterRow.Cells[4].Text = "<b>" + Fat.ToString() + "</b>";
                    gvIssuetoIceCream.FooterRow.Cells[5].Text = "<b>" + Snf.ToString() + "</b>";
                    gvIssuetoIceCream.FooterRow.Cells[6].Text = "<b>" + FatInKg.ToString() + "</b>";
                    gvIssuetoIceCream.FooterRow.Cells[7].Text = "<b>" + SnfInKg.ToString() + "</b>";
                    gvIssuetoIceCream.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                    gvIssuetoIceCream.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    gvIssuetoIceCream.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                    gvIssuetoIceCream.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                    gvIssuetoIceCream.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;
                }
                if (dsRecord.Tables[20].Rows.Count > 0)
                {
                    decimal Fat = 0;
                    decimal Snf = 0;
                    decimal FatInKg = 0;
                    decimal SnfInKg = 0;
                    decimal QtyInKg = 0;
                    gvCCWiseGoatMilkProcurement.DataSource = dsRecord.Tables[20];
                    gvCCWiseGoatMilkProcurement.DataBind();
                    QtyInKg = dsRecord.Tables[20].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    FatInKg = dsRecord.Tables[20].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    SnfInKg = dsRecord.Tables[20].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));
                    Fat = Math.Round(((FatInKg / QtyInKg) * 100), 2);
                    Snf = Math.Round(((SnfInKg / QtyInKg) * 100), 2);
                    gvCCWiseGoatMilkProcurement.FooterRow.Cells[3].Text = "<b>Total : </b>";
                    gvCCWiseGoatMilkProcurement.FooterRow.Cells[4].Text = "<b>" + QtyInKg.ToString() + "</b>";
                    gvCCWiseGoatMilkProcurement.FooterRow.Cells[6].Text = "<b>" + Fat.ToString() + "</b>";
                    gvCCWiseGoatMilkProcurement.FooterRow.Cells[7].Text = "<b>" + Snf.ToString() + "</b>";
                    gvCCWiseGoatMilkProcurement.FooterRow.Cells[8].Text = "<b>" + FatInKg.ToString() + "</b>";
                    gvCCWiseGoatMilkProcurement.FooterRow.Cells[9].Text = "<b>" + SnfInKg.ToString() + "</b>";
                    gvCCWiseGoatMilkProcurement.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                    gvCCWiseGoatMilkProcurement.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                    gvCCWiseGoatMilkProcurement.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Left;
                    gvCCWiseGoatMilkProcurement.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Left;
                    gvCCWiseGoatMilkProcurement.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Left;

                    InFlowTotalQtyInLtr += dsRecord.Tables[20].AsEnumerable().Sum(row => row.Field<decimal>("QtyInLtr"));
                    InFlowTotalQtyInKg += dsRecord.Tables[20].AsEnumerable().Sum(row => row.Field<decimal>("QtyInKg"));
                    InFlowTotalFatInKg += dsRecord.Tables[20].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    InFlowTotalSnfInKg += dsRecord.Tables[20].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));

                }
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
                DataSet dsRR_Child = objdb.ByProcedure("SpProductionProduct_Milk_InOut_RRSheetNew",
                new string[] { "flag", "Office_ID", "Date", "Shift_Id", "ProductSection_ID", "Ghee_RR", "Cream_RR", "Butter_RR", "SMP_RR" },
                new string[] { "1", objdb.Office_ID(), Fdate, ddlShift.SelectedValue, "2", objdb.LooseGheeItemTypeId_ID(), objdb.LooseCreamItemTypeId_ID(), objdb.LooseButterItemTypeId_ID(), objdb.LooseSMPItemTypeId_ID() }, "dataset");

                if (dsRR_Child != null && dsRR_Child.Tables[0].Rows.Count > 0)
                {
                    GVRRSheet.DataSource = dsRR_Child.Tables[1];
                    GVRRSheet.DataBind();



                }

                else
                {
                    GVRRSheet.DataSource = string.Empty;
                    GVRRSheet.DataBind();
                    
                }
                //txtInFlowTQtyInLtr.Text = InFlowTotalQtyInLtr.ToString();
                txtInFlowTQtyInKg.Text = InFlowTotalQtyInKg.ToString();
                txtInFlowTFatInKg.Text = InFlowTotalFatInKg.ToString();
                txtInFlowTSnfInKg.Text = InFlowTotalSnfInKg.ToString();

               // txtOutFlowTQtyInLtr.Text = OutFlowTotalQtyInLtr.ToString();
                txtOutFlowTQtyInKg.Text = OutFlowTotalQtyInKg.ToString();
                txtOutFlowTFatInKg.Text = OutFlowTotalFatInKg.ToString();
                txtOutFlowTSnfInKg.Text = OutFlowTotalSnfInKg.ToString();
				
				
				txtVariationTQtyInKg.Text = (decimal.Parse(txtOutFlowTQtyInKg.Text) - decimal.Parse(txtInFlowTQtyInKg.Text)).ToString();
				txtVariationTFatInKg.Text = (decimal.Parse(txtOutFlowTFatInKg.Text) - decimal.Parse(txtInFlowTFatInKg.Text)).ToString();
				txtVariationTSnfInKg.Text = (decimal.Parse(txtOutFlowTSnfInKg.Text) - decimal.Parse(txtInFlowTSnfInKg.Text)).ToString();
				// txtVariationTQtyInKg.Text = txtOutFlowTQtyInKg.Text - txtOutFlowTQtyInKg.Text;
				// txtVariationTFatInKg.Text = txtOutFlowTFatInKg.Text - txtInFlowTFatInKg.Text;
				// txtVariationTSnfInKg.Text = txtOutFlowTSnfInKg.Text - txtInFlowTSnfInKg.Text;
				
				
				
				

            }
            else
            {
                lblMsg.Text = "No Record Found";
                //lblReceiptqty.Text = "0";
            }



        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlPSection.SelectedValue != "0")
        {


            ViewFinalDisposalSheet();

        }
        else
        {
            divfinal.Visible = false;

        }
    }

    protected void gvOpening_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "OPENING BALANCE";
            HeaderCell.ColumnSpan = 7;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvOpening.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvProcess_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "PROCESS";
            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvProcess.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }

    }
    protected void gvReturn_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "RETURN";
            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvReturn.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvCCWiseProcurement_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "MDP/CC WISE RECEIPT";
            HeaderCell.ColumnSpan = 11;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvCCWiseProcurement.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvPaticulars_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "PARTICULARS(Sales Figures)";
            HeaderCell.ColumnSpan = 8;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvPaticulars.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvMilkToIP_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "MILK TO INDIGENOUS PRODUCT";
            HeaderCell.ColumnSpan = 8;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvMilkToIP.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvIssuetoMDPOrCC_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "ISSUE TO MDP/CC";
            HeaderCell.ColumnSpan = 10;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvIssuetoMDPOrCC.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvClosingBalances_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "CLOSING BALANCES";
            HeaderCell.ColumnSpan = 7;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvClosingBalances.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvColRoomBalances_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "COLD ROOM BALANCES";
            HeaderCell.ColumnSpan = 8;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvColdRoomBalances.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvIsuuetoOther_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "ISSUE TO OTHER PARTY";
            HeaderCell.ColumnSpan = 10;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvIsuuetoOther.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvBMCDCSCollection_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "BMC/DCS Collection";
            HeaderCell.ColumnSpan = 8;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvBMCDCSCollection.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvinflowother_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Others";
            HeaderCell.ColumnSpan = 7;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvinflowother.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvrcvdfrmothrUnion_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Received From Other Union";
            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvrcvdfrmothrUnion.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvForPowderConversion_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "For Powder Conversion";
            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvForPowderConversion.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvoutflowother_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Issue to Others";
            HeaderCell.ColumnSpan = 7;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvoutflowother.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvOther_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Others";
            HeaderCell.ColumnSpan = 7;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvOther.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void GVIssuetoPowderPlant_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "ISSUE TO POWDER PLANT";
            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);


            GVIssuetoPowderPlant.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvIssuetoCream_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "ISSUE TO CREAM";
            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvIssuetoCream.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvCanesCollection_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Cans Collection";
            HeaderCell.ColumnSpan = 8;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvCanesCollection.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvIssuetoIceCream_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "ISSUE TO ICE CREAM";
            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvIssuetoIceCream.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvCCWiseGoatMilkProcurement_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "CC WISE GOAT MILK RECEIPT";
            HeaderCell.ColumnSpan = 11;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvCCWiseGoatMilkProcurement.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
}