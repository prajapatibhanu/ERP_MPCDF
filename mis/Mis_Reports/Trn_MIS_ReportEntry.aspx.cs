using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_Mis_Reports_Trn_MIS_ReportEntry : System.Web.UI.Page
{
    DataSet ds1 = new DataSet();
    APIProcedure objdb = new APIProcedure();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!Page.IsPostBack)
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                MISFrom.Visible = false;
                txtEntryDate.Text = Date;
                txtEntryDate.Attributes.Add("readonly", "readonly");
                txtPaymentUpto.Attributes.Add("readonly", "readonly");
                txtEffectiveDate.Attributes.Add("readonly", "readonly");
                FillOffice();
            }
            //txtEntryDate_TextChanged(sender, e);
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds1 = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "12" }, "dataset");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds1.Tables[0];
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
                //ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
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
    private void GetOfficeDetails()
    {
        try
        {
            lblMsg.Text = "";
            DateTime entrydate = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", culture);
            DateTime previousdate = entrydate.AddDays(-1);
            string prevdate = previousdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            gvMilkSaleRate.DataSource = null;
            gvMilkSaleRate.DataBind();

            gvOwnProcurement.DataSource = null;
            gvOwnProcurement.DataBind();

            gvOtherUnionProcurement.DataSource = null;
            gvOtherUnionProcurement.DataBind();

            gvConversion.DataSource = null;
            gvConversion.DataBind();

            gvCFPAccouting.DataSource = null;
            gvCFPAccouting.DataBind();

            gvComoditiesStockPosition.DataSource = null;
            gvComoditiesStockPosition.DataBind();

            gvIPProduct.DataSource = null;
            gvIPProduct.DataBind();

            ds1.Clear();
            ds1 = objdb.ByProcedure("USP_MIS_Trn_MilkPurchaseRate",
                 new string[] { "Flag", "EntryDate", "Office_ID" },
                 new string[] { "2", entrydate.ToString(), ddlOffice.SelectedValue.ToString() }, "dataset");

            if (ds1 != null && ds1.Tables.Count > 0)
            {
                //MILK PURCHASE RATE                
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    txtBuff_KgFAT.Text = "";
                    txtCow_KgTS.Text = "";
                    txtComm_KgFAT.Text = "";
                    txtBuff_KgFAT.Text = ds1.Tables[0].Rows[0]["Buff_FAT_InKG"].ToString();
                    txtCow_KgTS.Text = ds1.Tables[0].Rows[0]["Cow_TS_InKG"].ToString();
                    txtComm_KgFAT.Text = ds1.Tables[0].Rows[0]["Comm_FAT_InKG"].ToString();
                    if (ds1.Tables[0].Rows[0]["RateEffectiveDate"].ToString() != null && ds1.Tables[0].Rows[0]["RateEffectiveDate"].ToString() != "")
                    {
                        txtEffectiveDate.Text = ds1.Tables[0].Rows[0]["RateEffectiveDate"].ToString();
                    }
                    if (ds1.Tables[0].Rows[0]["PaymentUpto"].ToString() != null && ds1.Tables[0].Rows[0]["PaymentUpto"].ToString() != "")
                    {
                        txtPaymentUpto.Text = ds1.Tables[0].Rows[0]["PaymentUpto"].ToString();
                    }
                    if (ds1.Tables[0].Rows[0]["DuesAmount"].ToString() != null && ds1.Tables[0].Rows[0]["DuesAmount"].ToString() != "")
                    {
                        txtDuesAmount.Text = ds1.Tables[0].Rows[0]["DuesAmount"].ToString();
                    }
                }
                else
                {
                    //DataTable dt_MP = (DataTable)ViewState["dt_MPRate"];
                    txtBuff_KgFAT.Text = "";
                    txtCow_KgTS.Text = "";
                    txtComm_KgFAT.Text = "";
                    txtEffectiveDate.Text = "";
                    txtPaymentUpto.Text = "";
                    txtDuesAmount.Text = "";
                    //txtBuff_KgFAT.Text = dt_MP.Rows[0]["Buff_FAT_InKG"].ToString();
                    //txtCow_KgTS.Text = dt_MP.Rows[0]["Cow_TS_InKG"].ToString();
                    //txtComm_KgFAT.Text = dt_MP.Rows[0]["Comm_FAT_InKG"].ToString();
                    //if (dt_MP.Rows[0]["RateEffectiveDate"].ToString() != null && dt_MP.Rows[0]["RateEffectiveDate"].ToString() != "")
                    //{
                    //    txtEffectiveDate.Text = dt_MP.Rows[0]["RateEffectiveDate"].ToString();
                    //}
                    //if (dt_MP.Rows[0]["PaymentUpto"].ToString() != null && dt_MP.Rows[0]["PaymentUpto"].ToString() != "")
                    //{
                    //    txtPaymentUpto.Text = dt_MP.Rows[0]["PaymentUpto"].ToString();
                    //}
                    //if (dt_MP.Rows[0]["DuesAmount"].ToString() != null && dt_MP.Rows[0]["DuesAmount"].ToString() != "")
                    //{
                    //    txtDuesAmount.Text = dt_MP.Rows[0]["DuesAmount"].ToString();
                    //}
                    //dt_MP.Dispose();
                }

                //MILK RATE
                if (ds1.Tables[1].Rows.Count > 0)
                {
                    gvMilkSaleRate.DataSource = ds1.Tables[1];     // MILK SALE RATE         
                    gvMilkSaleRate.DataBind();
                }
                else
                {
                    DataTable dt1 = (DataTable)ViewState["gvMilkSaleRate"];
                    gvMilkSaleRate.DataSource = dt1;     // MILK SALE RATE         
                    gvMilkSaleRate.DataBind();
                    dt1.Dispose();
                }


                //OWN PROCUREMENT
                if (ds1.Tables[2].Rows.Count > 0)
                {
                    gvOwnProcurement.DataSource = ds1.Tables[2];  // cc milk procurement office wise
                    gvOwnProcurement.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FetchData();", true);
                }
                else
                {
                    DataTable dt2 = (DataTable)ViewState["gvOwnProcurement"];
                    gvOwnProcurement.DataSource = dt2;  // cc milk procurement office wise
                    gvOwnProcurement.DataBind();
                    dt2.Dispose();
                }

                //OTHER UNION / PARTY PROCUREMENT
                if (ds1.Tables[3].Rows.Count > 0)
                {
                    gvOtherUnionProcurement.DataSource = ds1.Tables[3];  // other union milk procurement
                    gvOtherUnionProcurement.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FetchData1();", true);
                }
                else
                {
                    DataTable dt3 = (DataTable)ViewState["gvOtherUnionProcurement"];
                    gvOtherUnionProcurement.DataSource = dt3;  // other union milk procurement
                    gvOtherUnionProcurement.DataBind();
                    dt3.Dispose();
                }

                //CONVERSION
                if (ds1.Tables[4].Rows.Count > 0)
                {
                    gvConversion.DataSource = ds1.Tables[4];  // CONVERSION
                    gvConversion.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FetchData2();", true);
                }
                else
                {
                    DataTable dt4 = (DataTable)ViewState["gvConversion"];
                    gvConversion.DataSource = dt4;  // CONVERSION
                    gvConversion.DataBind();
                    dt4.Dispose();
                }


                //CFP ACCOUNTING
                if (ds1.Tables[5].Rows.Count > 0)
                {
                    gvCFPAccouting.DataSource = ds1.Tables[5];  // CONVERSION
                    gvCFPAccouting.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FetchData3();", true);
                }
                else
                {
                    DataTable dt5 = (DataTable)ViewState["gvCFPAccouting"];
                    gvCFPAccouting.DataSource = dt5;  // CONVERSION
                    gvCFPAccouting.DataBind();
                    dt5.Dispose();
                }

                //GHEE ACCOUNT
                if (ds1.Tables[6].Rows.Count > 0)
                {
                    txtGheePkt.Text = ds1.Tables[6].Rows[0]["StockPosition"].ToString();
                    txtGheeLosse.Text = ds1.Tables[6].Rows[1]["StockPosition"].ToString();
                }
                else
                {
                    txtGheePkt.Text = "";
                    txtGheeLosse.Text = "";
                }

                //COMODITIES CONSUMPTION
                if (ds1.Tables[7].Rows.Count > 0)
                {
                    txtSMP_QTY1.Text = ds1.Tables[7].Rows[0]["SMP_QTY"].ToString();
                    txtWB_QTY1.Text = ds1.Tables[7].Rows[0]["WB_QTY"].ToString();
                    txtSMP_QTY2.Text = ds1.Tables[7].Rows[1]["SMP_QTY"].ToString();
                    txtWB_QTY2.Text = ds1.Tables[7].Rows[1]["WB_QTY"].ToString();
                }
                else
                {
                    txtSMP_QTY1.Text = "";
                    txtWB_QTY1.Text = "";
                    txtSMP_QTY2.Text = "";
                    txtWB_QTY2.Text = "";
                }

                //COMODITIES STOCK POSITION
                if (ds1.Tables[8].Rows.Count > 0)
                {
                    gvComoditiesStockPosition.DataSource = ds1.Tables[8];  // COMODITIES STOCK POSITION
                    gvComoditiesStockPosition.DataBind();
                }
                else
                {
                    DataTable dt8 = (DataTable)ViewState["gvComoditiesStockPosition"];
                    gvComoditiesStockPosition.DataSource = dt8;  // COMODITIES STOCK POSITION
                    gvComoditiesStockPosition.DataBind();
                    dt8.Dispose();
                }


                //MILK CONSUMED IN INDIGNEOS PRODUCT MAKING (IN Kgs)
                if (ds1.Tables[9].Rows.Count > 0)
                {
                    txtMilkConsumedIndigneos.Text = ds1.Tables[9].Rows[0]["Quanity_InKG"].ToString();
                }
                else
                {
                    txtMilkConsumedIndigneos.Text = "";
                }


                //IP Product
                if (ds1.Tables[10].Rows.Count > 0)
                {
                    gvIPProduct.DataSource = ds1.Tables[10];
                    gvIPProduct.DataBind();
                }
                else
                {
                    DataTable dt10 = (DataTable)ViewState["gvIPProduct"];
                    gvIPProduct.DataSource = dt10;
                    gvIPProduct.DataBind();
                    if (dt10 != null)
                    {
                        dt10.Dispose();
                    }
                }
                //Town wise milk sale
                if (ds1.Tables[11].Rows.Count > 0)
                {
                    ViewState["Temp_TownwiseMilkSale"] = ds1.Tables[11];
                    if (ViewState["DynamicGridBind"] == null)
                    {
                        foreach (DataColumn column in ds1.Tables[11].Columns)
                        {
                            TemplateField tfield = new TemplateField();
                            tfield.HeaderText = column.ColumnName;
                            gvTownwiseMilkSale.Columns.Add(tfield);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); GC.SuppressFinalize(objdb); }
        }
    }
    private void GetTownwiseMilkSale()
    {

        DataTable dt1 = (DataTable)ViewState["Temp_TownwiseMilkSale"];
        if (dt1.Rows.Count > 0)
        {
            ViewState["TownwiseMilkSale"] = dt1;
            ViewState["DynamicGridBind"] = "1";
            gvTownwiseMilkSale.DataSource = dt1;
            gvTownwiseMilkSale.DataBind();
            DataTable dt7 = new DataTable();
            dt7 = dt1;
            ViewState["gvTownwiseMilkSale"] = dt7;
            dt7.Dispose();
        }
    }
    protected void lnkMilkPurchaseRate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                DateTime entrydate = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", culture);
                string EDate = entrydate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                DateTime paymentupto = DateTime.ParseExact(txtPaymentUpto.Text, "dd/MM/yyyy", culture);
                string paydate = paymentupto.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                DateTime RateEffDate = DateTime.ParseExact(txtEffectiveDate.Text, "dd/MM/yyyy", culture);
                string EffDate = RateEffDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                ds1 = objdb.ByProcedure("USP_MIS_Trn_MilkPurchaseRate",
                     new string[] { "Flag", "EntryDate", "Buff_FAT_InKG", "Cow_TS_InKG"
                     , "Comm_FAT_InKG","PaymentUpto","RateEffectiveDate","DuesAmount", "Office_ID","CreatedBy","CreatedByIP" },
                     new string[] { "1", entrydate.ToString(),txtBuff_KgFAT.Text.Trim(),txtCow_KgTS.Text.Trim()
                     ,txtComm_KgFAT.Text.Trim(),paydate.ToString(),EffDate.ToString(), txtDuesAmount.Text.Trim(),
                     ddlOffice.SelectedValue.ToString(), objdb.createdBy(), IPAddress }, "dataset");

                if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMilkPurchaseRate.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    txtBuff_KgFAT.Text = string.Empty;
                    txtComm_KgFAT.Text = string.Empty;
                    txtCow_KgTS.Text = string.Empty;
                    txtDuesAmount.Text = string.Empty;
                    txtPaymentUpto.Text = string.Empty;
                    txtEffectiveDate.Text = string.Empty;
                }
                else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "NotOk")
                {
                    string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMilkPurchaseRate.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    txtBuff_KgFAT.Text = string.Empty;
                    txtComm_KgFAT.Text = string.Empty;
                    txtCow_KgTS.Text = string.Empty;
                    txtDuesAmount.Text = string.Empty;
                    txtPaymentUpto.Text = string.Empty;
                    txtEffectiveDate.Text = string.Empty;
                }
                else
                {
                    string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMilkPurchaseRate.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  Milk Purchase Rate:" + error);
                }
                GetDateWiseData();
            }
            catch (Exception ex)
            {
                lblMilkPurchaseRate.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error Milk Purchase Rate", ex.Message.ToString());
            }
            finally
            {
                if (ds1 != null) { ds1.Dispose(); GC.SuppressFinalize(objdb); }
            }
        }
    }
    protected void btnSaveForOwnProcurement_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvOwnProcurement.Rows.Count > 0)
            {
                int qty = 0;
                DataTable dt = new DataTable();
                DataRow dr;

                dt.Columns.Add(new DataColumn("CCId_ID", typeof(int)));
                dt.Columns.Add(new DataColumn("Qty", typeof(decimal)));
                dt.Columns.Add(new DataColumn("FAT_Per", typeof(decimal)));
                dt.Columns.Add(new DataColumn("SNF_Per", typeof(decimal)));
                dr = dt.NewRow();
                foreach (GridViewRow row in gvOwnProcurement.Rows)
                {
                    TextBox txtQty = (TextBox)row.FindControl("txtQty");
                    TextBox txtFAT = (TextBox)row.FindControl("txtFAT");
                    TextBox txtSNF = (TextBox)row.FindControl("txtSNF");
                    Label lblOffice_ID = (Label)row.FindControl("lblOffice_ID");
                    if (txtQty.Text != "")
                    {
                        qty++;
                        dr[0] = lblOffice_ID.Text;
                        dr[1] = txtQty.Text;
                        dr[2] = txtFAT.Text;
                        dr[3] = txtSNF.Text;

                        dt.Rows.Add(dr.ItemArray);
                    }

                }
                DateTime entrydate = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", culture);
                string EDate = entrydate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                ds1 = objdb.ByProcedure("USP_MIS_Trn_MilkOwnProcurement",
                     new string[] { "Flag", "EntryDate", "Office_ID", "CreatedBy", "CreatedByIP" },
                     new string[] { "1", entrydate.ToString(), ddlOffice.SelectedValue.ToString(), objdb.createdBy(), IPAddress },
                new string[] { "type_MIS_Trn_MilkOwnProcurement" },
                       new DataTable[] { dt }, "dataset");

                if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    DataTable dt1 = (DataTable)ViewState["gvOwnProcurement"];
                    gvOwnProcurement.DataSource = dt1;
                    gvOwnProcurement.DataBind();
                    dt1.Dispose();
                    lblOwnProcurement.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);


                }
                else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "NotOk")
                {
                    string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblOwnProcurement.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                }
                else
                {
                    string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblOwnProcurement.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  Milk Purchase Rate:" + error);
                }
                GetDateWiseData();
            }
        }
        catch (Exception ex)
        {
            lblOwnProcurement.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error Own Procurement", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); GC.SuppressFinalize(objdb); }
        }


    }
    protected void btnOtherUnionProcureMent_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvOtherUnionProcurement.Rows.Count > 0)
            {
                int qty = 0;
                DataTable dt = new DataTable();
                DataRow dr;

                dt.Columns.Add(new DataColumn("ThirdPartyUnion_Id", typeof(int)));
                dt.Columns.Add(new DataColumn("Qty", typeof(decimal)));
                dt.Columns.Add(new DataColumn("FAT_Per", typeof(decimal)));
                dt.Columns.Add(new DataColumn("SNF_Per", typeof(decimal)));
                dr = dt.NewRow();
                foreach (GridViewRow row in gvOtherUnionProcurement.Rows)
                {


                    GridViewRow selectedrow = row;
                    TextBox txtQty = (TextBox)selectedrow.FindControl("txtQty");
                    TextBox txtFAT = (TextBox)selectedrow.FindControl("txtFAT");
                    TextBox txtSNF = (TextBox)selectedrow.FindControl("txtSNF");
                    Label lblThirdPartyUnion_Id = (Label)row.FindControl("lblThirdPartyUnion_Id");
                    if (txtQty.Text != "")
                    {
                        qty++;
                        dr[0] = lblThirdPartyUnion_Id.Text;
                        dr[1] = txtQty.Text;
                        dr[2] = txtFAT.Text;
                        dr[3] = txtSNF.Text;

                        dt.Rows.Add(dr.ItemArray);
                    }

                }

                if (qty > 0)
                {
                    DateTime entrydate = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", culture);
                    string EDate = entrydate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    ds1 = objdb.ByProcedure("USP_MIS_Trn_MilkOtherProcurement",
                         new string[] { "Flag", "EntryDate", "Office_ID", "CreatedBy", "CreatedByIP" },
                         new string[] { "1", entrydate.ToString(), ddlOffice.SelectedValue.ToString(), objdb.createdBy(), IPAddress },
                    new string[] { "type_MIS_Trn_MilkOtherProcurement" },
                           new DataTable[] { dt }, "dataset");

                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        DataTable dt1 = (DataTable)ViewState["gvOtherUnionProcurement"];
                        gvOtherUnionProcurement.DataSource = dt1;
                        gvOtherUnionProcurement.DataBind();
                        dt1.Dispose();
                        lblOtherUnionPartyProcurement.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "NotOk")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblOtherUnionPartyProcurement.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblOtherUnionPartyProcurement.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  Milk Purchase Rate:" + error);
                    }
                    GetDateWiseData();
                }
                else
                {
                    lblOtherUnionPartyProcurement.Text = objdb.Alert("fa-warning", "alert-warning", "Warninng !", "Select atleast one Other Procurement Entry");
                }
            }
        }
        catch (Exception ex)
        {
            lblOtherUnionPartyProcurement.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error Other Procurement", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); GC.SuppressFinalize(objdb); }
        }
    }
    protected void btnConverstion_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvConversion.Rows.Count > 0)
            {
                int qty = 0;
                DataTable dt = new DataTable();
                DataRow dr;

                dt.Columns.Add(new DataColumn("ThirdPartyUnion_Id", typeof(int)));
                dt.Columns.Add(new DataColumn("Qty", typeof(decimal)));
                dt.Columns.Add(new DataColumn("SMP_QTY", typeof(decimal)));
                dt.Columns.Add(new DataColumn("WB_QTY", typeof(decimal)));
                dr = dt.NewRow();
                foreach (GridViewRow row in gvConversion.Rows)
                {


                    GridViewRow selectedrow = row;
                    TextBox txtQty = (TextBox)selectedrow.FindControl("txtQty");
                    TextBox txtSMPQTY = (TextBox)selectedrow.FindControl("txtSMPQTY");
                    TextBox txtWBQTY = (TextBox)selectedrow.FindControl("txtWBQTY");
                    Label lblThirdPartyUnion_Id = (Label)row.FindControl("lblThirdPartyUnion_Id");
                    if (txtQty.Text != "")
                    {
                        qty++;
                        dr[0] = lblThirdPartyUnion_Id.Text;
                        dr[1] = txtQty.Text;
                        dr[2] = txtSMPQTY.Text;
                        dr[3] = txtWBQTY.Text;

                        dt.Rows.Add(dr.ItemArray);
                    }
                }
                if (qty > 0)
                {
                    DateTime entrydate = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", culture);
                    string EDate = entrydate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    ds1 = objdb.ByProcedure("USP_MIS_Trn_Conversion",
                         new string[] { "Flag", "EntryDate", "Office_ID", "CreatedBy", "CreatedByIP" },
                         new string[] { "1", entrydate.ToString(), ddlOffice.SelectedValue.ToString(), objdb.createdBy(), IPAddress },
                    new string[] { "type_MIS_Trn_Conversion" },
                           new DataTable[] { dt }, "dataset");

                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        DataTable dt1 = (DataTable)ViewState["gvConversion"];
                        gvConversion.DataSource = dt1;
                        gvConversion.DataBind();
                        dt1.Dispose();
                        lblConversion.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);


                    }
                    else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "NotOk")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblConversion.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblConversion.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  Milk Purchase Rate:" + error);
                    }
                    GetDateWiseData();
                }
                else
                {
                    lblConversion.Text = objdb.Alert("fa-warning", "alert-warning", "Warninng !", "Select atleast one Converstion Entry");
                }
            }
        }
        catch (Exception ex)
        {
            lblConversion.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error Converstion", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); GC.SuppressFinalize(objdb); }
        }
    }
    protected void btnCFPAccounting_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvCFPAccouting.Rows.Count > 0)
            {
                int prod = 0;
                DataTable dt = new DataTable();
                DataRow dr;

                dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dt.Columns.Add(new DataColumn("Production", typeof(decimal)));
                dt.Columns.Add(new DataColumn("DCS_Sale", typeof(decimal)));
                dt.Columns.Add(new DataColumn("Other_Sale", typeof(decimal)));
                dr = dt.NewRow();
                foreach (GridViewRow row in gvCFPAccouting.Rows)
                {


                    GridViewRow selectedrow = row;
                    TextBox txtProduction = (TextBox)selectedrow.FindControl("txtProduction");
                    TextBox txtDCS_Sale = (TextBox)selectedrow.FindControl("txtDCS_Sale");
                    TextBox txtOther_Sale = (TextBox)selectedrow.FindControl("txtOther_Sale");
                    Label lblItemName = (Label)row.FindControl("lblItemName");
                    if (txtProduction.Text != "")
                    {
                        prod++;
                        dr[0] = lblItemName.Text;
                        dr[1] = txtProduction.Text;
                        dr[2] = txtDCS_Sale.Text;
                        dr[3] = txtOther_Sale.Text;

                        dt.Rows.Add(dr.ItemArray);
                    }

                }

                if (prod > 0)
                {
                    DateTime entrydate = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", culture);
                    string EDate = entrydate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    ds1 = objdb.ByProcedure("USP_MIS_Trn_CFPAccounting",
                         new string[] { "Flag", "EntryDate", "Office_ID", "CreatedBy", "CreatedByIP" },
                         new string[] { "1", entrydate.ToString(), ddlOffice.SelectedValue.ToString(), objdb.createdBy(), IPAddress },
                    new string[] { "type_MIS_Trn_CFPAccounting" },
                           new DataTable[] { dt }, "dataset");

                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        DataTable dt1 = (DataTable)ViewState["gvCFPAccouting"];
                        gvCFPAccouting.DataSource = dt1;
                        gvCFPAccouting.DataBind();
                        dt1.Dispose();
                        lblCFPAccounting.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);


                    }
                    else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "NotOk")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblCFPAccounting.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblCFPAccounting.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  Milk Purchase Rate:" + error);
                    }
                }
                else
                {
                    lblCFPAccounting.Text = objdb.Alert("fa-warning", "alert-warning", "Warninng !", "Select atleast one CFP Account Entry");
                }
                GetDateWiseData();
            }
        }
        catch (Exception ex)
        {
            lblCFPAccounting.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error CFP Account", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); GC.SuppressFinalize(objdb); }
        }
    }
    protected void btnGheeAccount_Click(object sender, EventArgs e)
    {
        try
        {

            int prod = 0;
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add(new DataColumn("Particular", typeof(string)));
            dt.Columns.Add(new DataColumn("StockPosition", typeof(decimal)));
            dr = dt.NewRow();

            if (txtGheePkt.Text != "")
            {
                prod++;
                dr[0] = "Ghee Pkt";
                dr[1] = txtGheePkt.Text;


                dt.Rows.Add(dr.ItemArray);
            }
            if (txtGheeLosse.Text != "")
            {
                prod++;
                dr[0] = "Ghee Loose";
                dr[1] = txtGheeLosse.Text;


                dt.Rows.Add(dr.ItemArray);
            }

            if (prod > 0)
            {
                DateTime entrydate = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", culture);
                string EDate = entrydate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                ds1 = objdb.ByProcedure("USP_MIS_Trn_GheeAccount",
                     new string[] { "Flag", "EntryDate", "Office_ID", "CreatedBy", "CreatedByIP" },
                     new string[] { "1", entrydate.ToString(), ddlOffice.SelectedValue.ToString(), objdb.createdBy(), IPAddress },
                new string[] { "type_MIS_Trn_GheeAccount" },
                       new DataTable[] { dt }, "dataset");

                if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    txtGheeLosse.Text = string.Empty;
                    txtGheePkt.Text = string.Empty;
                    dt.Dispose();
                    lblGheeAccount.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                }
                else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "NotOk")
                {
                    string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblGheeAccount.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                }
                else
                {

                    txtGheePkt.Focus();
                    string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblGheeAccount.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  Milk Purchase Rate:" + error);
                }
                GetDateWiseData();
            }
            else
            {
                lblGheeAccount.Text = objdb.Alert("fa-warning", "alert-warning", "Warninng !", "Select atleast one Ghee Account Entry");
            }
        }
        catch (Exception ex)
        {
            lblGheeAccount.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error Ghee Account Entry", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); GC.SuppressFinalize(objdb); }
        }
    }
    protected void btnCommoditiesConsumption_Click(object sender, EventArgs e)
    {
        try
        {

            int prod = 0;
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add(new DataColumn("Particular", typeof(string)));
            dt.Columns.Add(new DataColumn("SMP_QTY", typeof(decimal)));
            dt.Columns.Add(new DataColumn("WB_QTY", typeof(decimal)));
            dr = dt.NewRow();

            if (txtSMP_QTY1.Text != "" || txtSMP_QTY2.Text != "")
            {
                prod++;
                if (txtSMP_QTY1.Text == "")
                {
                    txtSMP_QTY1.Text = "0";
                }
                if (txtWB_QTY1.Text == "")
                {
                    txtWB_QTY1.Text = "0";
                }
                dr[0] = "For Milk";
                dr[1] = txtSMP_QTY1.Text;
                dr[2] = txtWB_QTY1.Text;


                dt.Rows.Add(dr.ItemArray);
            }
            if (txtSMP_QTY2.Text != "" || txtWB_QTY2.Text != "")
            {
                if (txtSMP_QTY2.Text == "")
                {
                    txtSMP_QTY2.Text = "0";
                }
                if (txtWB_QTY2.Text == "")
                {
                    txtWB_QTY2.Text = "0";
                }
                prod++;
                dr[0] = "For Other";
                dr[1] = txtSMP_QTY2.Text;
                dr[2] = txtWB_QTY2.Text;


                dt.Rows.Add(dr.ItemArray);
            }

            if (prod > 0)
            {
                DateTime entrydate = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", culture);
                string EDate = entrydate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                ds1 = objdb.ByProcedure("USP_MIS_Trn_CommoditiesConsumption",
                     new string[] { "Flag", "EntryDate", "Office_ID", "CreatedBy", "CreatedByIP" },
                     new string[] { "1", entrydate.ToString(), ddlOffice.SelectedValue.ToString(), objdb.createdBy(), IPAddress },
                new string[] { "type_MIS_Trn_CommoditiesConsumption" },
                       new DataTable[] { dt }, "dataset");

                if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    txtSMP_QTY1.Text = "";
                    txtSMP_QTY2.Text = "";
                    txtWB_QTY1.Text = "";
                    txtWB_QTY2.Text = "";
                    dt.Dispose();
                    lblComoditiesConsumption.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                }
                else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "NotOk")
                {
                    string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblComoditiesConsumption.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                }
                else
                {

                    txtGheePkt.Focus();
                    string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblComoditiesConsumption.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  COMODITIES CONSUMPTION:" + error);
                }
                GetDateWiseData();
            }
            else
            {
                lblComoditiesConsumption.Text = objdb.Alert("fa-warning", "alert-warning", "Warninng !", "Select atleast one COMODITIES CONSUMPTION");
            }
        }
        catch (Exception ex)
        {
            lblComoditiesConsumption.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error CFP Account", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); GC.SuppressFinalize(objdb); }
        }
    }
    protected void btnComoditiesStockPosition_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvComoditiesStockPosition.Rows.Count > 0)
            {
                int qty = 0;
                DataTable dt = new DataTable();
                DataRow dr;

                dt.Columns.Add(new DataColumn("ThirdPartyUnion_Id", typeof(int)));
                dt.Columns.Add(new DataColumn("SMP_QTY", typeof(decimal)));
                dt.Columns.Add(new DataColumn("WB_QTY", typeof(decimal)));
                dt.Columns.Add(new DataColumn("WMP_QTY", typeof(decimal)));
                dr = dt.NewRow();
                foreach (GridViewRow row in gvComoditiesStockPosition.Rows)
                {


                    GridViewRow selectedrow = row;
                    TextBox txtSMPQTY = (TextBox)selectedrow.FindControl("txtSMPQTY");
                    TextBox txtWBQTY = (TextBox)selectedrow.FindControl("txtWBQTY");
                    TextBox txtWMPQTY = (TextBox)selectedrow.FindControl("txtWMPQTY");
                    Label lblThirdPartyUnion_Id = (Label)row.FindControl("lblThirdPartyUnion_Id");
                    if (txtSMPQTY.Text != "")
                    {
                        qty++;
                        dr[0] = lblThirdPartyUnion_Id.Text;
                        dr[1] = txtSMPQTY.Text;
                        dr[2] = txtWBQTY.Text;
                        dr[3] = txtWMPQTY.Text;

                        dt.Rows.Add(dr.ItemArray);
                    }

                }

                if (qty > 0)
                {
                    DateTime entrydate = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", culture);
                    string EDate = entrydate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    ds1 = objdb.ByProcedure("USP_MIS_Trn_CommoditiesStockPosition",
                         new string[] { "Flag", "EntryDate", "Office_ID", "CreatedBy", "CreatedByIP" },
                         new string[] { "1", entrydate.ToString(), ddlOffice.SelectedValue.ToString(), objdb.createdBy(), IPAddress },
                    new string[] { "type_MIS_Trn_CommoditiesStockPosition" },
                           new DataTable[] { dt }, "dataset");

                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        DataTable dt1 = (DataTable)ViewState["gvComoditiesStockPosition"];
                        gvComoditiesStockPosition.DataSource = dt1;
                        gvComoditiesStockPosition.DataBind();
                        dt1.Dispose();
                        lblComoditiesstockposition.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);


                    }
                    else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "NotOk")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblComoditiesstockposition.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblComoditiesstockposition.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  COMODITIES STOCK POSITION:" + error);
                    }
                }
                else
                {
                    lblComoditiesstockposition.Text = objdb.Alert("fa-warning", "alert-warning", "Warninng !", "Select atleast one COMODITIES STOCK POSITION Entry");
                }
                GetDateWiseData();
            }
        }
        catch (Exception ex)
        {
            lblComoditiesstockposition.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error COMODITIES STOCK POSITION", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); GC.SuppressFinalize(objdb); }
        }
    }
    protected void btnMilkSaleRate_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvMilkSaleRate.Rows.Count > 0)
            {
                int qty = 0;
                DataTable dt = new DataTable();
                DataRow dr;

                dt.Columns.Add(new DataColumn("ThirdPartyUnion_Id", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemRate", typeof(decimal)));

                dr = dt.NewRow();
                foreach (GridViewRow row in gvMilkSaleRate.Rows)
                {


                    GridViewRow selectedrow = row;
                    TextBox txtItemRate = (TextBox)selectedrow.FindControl("txtItemRate");
                    Label lblItem_id = (Label)row.FindControl("lblItem_id");
                    if (txtItemRate.Text != "")
                    {
                        qty++;
                        dr[0] = lblItem_id.Text;
                        dr[1] = txtItemRate.Text;

                        dt.Rows.Add(dr.ItemArray);
                    }

                }

                if (qty > 0)
                {
                    DateTime entrydate = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", culture);
                    string EDate = entrydate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    ds1 = objdb.ByProcedure("USP_MIS_Trn_MilkSaleRate",
                         new string[] { "Flag", "EntryDate", "Office_ID", "CreatedBy", "CreatedByIP" },
                         new string[] { "1", entrydate.ToString(), ddlOffice.SelectedValue.ToString(), objdb.createdBy(), IPAddress },
                    new string[] { "type_MIS_Trn_MilkSaleRate" },
                           new DataTable[] { dt }, "dataset");

                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        DataTable dt1 = (DataTable)ViewState["gvMilkSaleRate"];
                        gvMilkSaleRate.DataSource = dt1;
                        gvMilkSaleRate.DataBind();
                        dt1.Dispose();
                        lblMilkSaleRate.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "NotOk")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMilkSaleRate.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMilkSaleRate.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  Milk Sale Rate :" + error);
                    }
                    GetDateWiseData();
                }
                else
                {
                    lblMilkSaleRate.Text = objdb.Alert("fa-warning", "alert-warning", "Warninng !", "Select atleast one Milk Sale Rate ");
                }
            }
        }
        catch (Exception ex)
        {
            lblMilkSaleRate.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error Milk Sale Rate ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); GC.SuppressFinalize(objdb); }
        }
    }
    protected void btnIPProductSale_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvIPProduct.Rows.Count > 0)
            {
                int qty = 0;
                DataTable dt = new DataTable();
                DataRow dr;

                dt.Columns.Add(new DataColumn("ProductId", typeof(int)));
                dt.Columns.Add(new DataColumn("ProductSale", typeof(decimal)));

                dr = dt.NewRow();
                foreach (GridViewRow row in gvIPProduct.Rows)
                {


                    GridViewRow selectedrow = row;
                    TextBox txtProductSaleInKG = (TextBox)selectedrow.FindControl("txtProductSaleInKG");
                    Label lblProductId = (Label)row.FindControl("lblProductId");
                    if (txtProductSaleInKG.Text != "")
                    {
                        qty++;
                        dr[0] = lblProductId.Text;
                        dr[1] = txtProductSaleInKG.Text;

                        dt.Rows.Add(dr.ItemArray);
                    }

                }

                if (qty > 0)
                {
                    DateTime entrydate = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", culture);
                    string EDate = entrydate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    ds1 = objdb.ByProcedure("USP_MIS_Trn_IPProduct",
                         new string[] { "Flag", "EntryDate", "Office_ID", "CreatedBy", "CreatedByIP" },
                         new string[] { "1", entrydate.ToString(), ddlOffice.SelectedValue.ToString(), objdb.createdBy(), IPAddress },
                    new string[] { "type_MIS_Trn_IPProduct" },
                           new DataTable[] { dt }, "dataset");

                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        DataTable dt1 = (DataTable)ViewState["gvIPProduct"];
                        gvIPProduct.DataSource = dt1;
                        gvIPProduct.DataBind();
                        dt1.Dispose();
                        lblIPProductSale.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "NotOk")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblIPProductSale.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblIPProductSale.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error IP Product Sale :" + error);
                    }
                    GetDateWiseData();
                }
                else
                {
                    lblIPProductSale.Text = objdb.Alert("fa-warning", "alert-warning", "Warninng !", "Select atleast one IP Product Sale ");
                }
            }
        }
        catch (Exception ex)
        {
            lblIPProductSale.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error IP Product Sale ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); GC.SuppressFinalize(objdb); }
        }
    }
    protected void btnMilkConsumed_Click(object sender, EventArgs e)
    {
        try
        {

            int prod = 0;
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add(new DataColumn("Particular", typeof(string)));
            dt.Columns.Add(new DataColumn("Quanity_InKG", typeof(decimal)));
            dr = dt.NewRow();

            if (txtMilkConsumedIndigneos.Text != "")
            {
                prod++;
                dr[0] = "Milk";
                dr[1] = txtMilkConsumedIndigneos.Text;


                dt.Rows.Add(dr.ItemArray);
            }

            if (prod > 0)
            {
                DateTime entrydate = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", culture);
                string EDate = entrydate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                ds1 = objdb.ByProcedure("USP_MIS_Trn_MilkConsumedInIdigneosProductMaking",
                     new string[] { "Flag", "EntryDate", "Office_ID", "CreatedBy", "CreatedByIP" },
                     new string[] { "1", entrydate.ToString(), objdb.Office_ID(), objdb.createdBy(), IPAddress },
                new string[] { "type_MIS_Trn_MilkConsumedInIdigneosProductMaking" },
                       new DataTable[] { dt }, "dataset");

                if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    txtMilkConsumedIndigneos.Text = string.Empty;
                    dt.Dispose();
                    lblMilkConsumed.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);

                }
                else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "NotOk")
                {
                    string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMilkConsumed.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                }
                else
                {

                    txtMilkConsumedIndigneos.Focus();
                    string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    lblMilkConsumed.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error  MILK CONSUMED IN INDIGNEOS PRODUCT MAKING:" + error);
                }
            }
            else
            {
                lblMilkConsumed.Text = objdb.Alert("fa-warning", "alert-warning", "Warninng !", "Select at least one MILK CONSUMED IN INDIGNEOS PRODUCT MAKING");
            }
        }
        catch (Exception ex)
        {
            lblMilkConsumed.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error ILK CONSUMED IN INDIGNEOS PRODUCT MAKING", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); GC.SuppressFinalize(objdb); }
        }
    }
    protected void btnTownWiseMilkSale_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt1 = (DataTable)ViewState["TownwiseMilkSale"];
            DataTable dtitemname = (DataTable)ViewState["gvMilkSaleRate"];
            if (gvTownwiseMilkSale.Rows.Count > 0)
            {
                int qty = 0;
                DataTable dtFinalItem = new DataTable();
                DataRow dr1;

                dtFinalItem.Columns.Add(new DataColumn("RouteHeadId", typeof(int)));
                dtFinalItem.Columns.Add(new DataColumn("Item_id", typeof(int)));
                dtFinalItem.Columns.Add(new DataColumn("ProductSale", typeof(decimal)));

                dr1 = dtFinalItem.NewRow();
                for (int i = 0; i < gvTownwiseMilkSale.Rows.Count; i++)
                {
                    GridViewRow row1 = gvTownwiseMilkSale.Rows[i];

                    Label lblRouteHeadId = (Label)row1.FindControl("lblRouteHeadId");

                    int j = 0;
                    int k = 1;

                    foreach (DataColumn column in dt1.Columns)
                    {


                        if (j > 1)
                        {
                            string columnname = column.ColumnName;


                            Int64 item_id = (from DataRow dr in dtitemname.Rows
                                             where (string)dr["ItemName"] == columnname
                                             select (Int64)dr["Item_id"]).FirstOrDefault();

                            k = k + 1;

                            string txtboxval = (row1.FindControl("txtboxid" + k) as TextBox).Text;

                            if (txtboxval != "" && !string.IsNullOrEmpty(txtboxval))
                            {
                                dr1[0] = lblRouteHeadId.Text;
                                dr1[1] = item_id;
                                dr1[2] = txtboxval;
                                //  columnname txtboxval
                                dtFinalItem.Rows.Add(dr1.ItemArray);
                                qty = qty + 1;
                            }


                        }
                        j = j + 1;


                    }


                }

                if (qty > 0 && dtFinalItem.Rows.Count > 0)
                {
                    DateTime entrydate = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", culture);
                    string EDate = entrydate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    ds1 = objdb.ByProcedure("USP_MIS_Trn_TownwiseMilkSale",
                         new string[] { "Flag", "EntryDate", "Office_ID", "CreatedBy", "CreatedByIP" },
                         new string[] { "1", entrydate.ToString(), ddlOffice.SelectedValue.ToString(), objdb.createdBy(), IPAddress },
                    new string[] { "type_MIS_Trn_TownwiseMilkSale" },
                           new DataTable[] { dtFinalItem }, "dataset");

                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        DataTable dt2 = (DataTable)ViewState["gvTownwiseMilkSale"];
                        gvTownwiseMilkSale.DataSource = dt2;
                        gvTownwiseMilkSale.DataBind();
                        dt1.Dispose();
                        lblTownWiseMilkSale.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "NotOk")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblTownWiseMilkSale.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblTownWiseMilkSale.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error IP Product Sale :" + error);
                    }
                }
                else
                {
                    lblTownWiseMilkSale.Text = objdb.Alert("fa-warning", "alert-warning", "Warninng !", "Select atleast one IP Product Sale ");
                }
            }
        }
        catch (Exception ex)
        {
            lblTownWiseMilkSale.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error Townwise Milk Sale ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); GC.SuppressFinalize(objdb); }
        }
    }
    protected void gvTownwiseMilkSale_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DataTable dt1 = (DataTable)ViewState["TownwiseMilkSale"];
            for (int i = 0; i < dt1.Columns.Count; i++)
            {

                if (i > 1)
                {

                    string strcolval = dt1.Columns[i].ColumnName;
                    TextBox txtboxid = new TextBox();
                    txtboxid.MaxLength = 10;
                    txtboxid.Width = 90;
                    txtboxid.ID = "txtboxid" + i;
                    txtboxid.Text = (e.Row.DataItem as DataRowView).Row[strcolval].ToString();
                    if (txtboxid.Text == "")
                    {
                        txtboxid.Text = "0";
                    }
                    txtboxid.Attributes.Add("onkeypress", "return validateNum(event);");
                    txtboxid.Attributes.Add("runat", "server");
                    e.Row.Cells[i + 1].Controls.Add(txtboxid);
                }


            }

        }

        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
    }
    protected void GetDateWiseData()
    {
        try
        {
            DateTime entrydate = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", culture);

            gvMilkSaleRate.DataSource = null;
            gvMilkSaleRate.DataBind();

            gvOwnProcurement.DataSource = null;
            gvOwnProcurement.DataBind();

            gvOtherUnionProcurement.DataSource = null;
            gvOtherUnionProcurement.DataBind();

            gvConversion.DataSource = null;
            gvConversion.DataBind();

            gvCFPAccouting.DataSource = null;
            gvCFPAccouting.DataBind();

            gvComoditiesStockPosition.DataSource = null;
            gvComoditiesStockPosition.DataBind();

            gvIPProduct.DataSource = null;
            gvIPProduct.DataBind();

            ds1.Clear();
            ds1 = objdb.ByProcedure("USP_MIS_Trn_MilkPurchaseRate",
                 new string[] { "Flag", "EntryDate", "Office_ID" },
                 new string[] { "2", entrydate.ToString(), ddlOffice.SelectedValue.ToString() }, "dataset");

            if (ds1 != null && ds1.Tables.Count > 0)
            {
                //MILK PURCHASE RATE                
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    txtBuff_KgFAT.Text = "";
                    txtCow_KgTS.Text = "";
                    txtComm_KgFAT.Text = "";
                    txtBuff_KgFAT.Text = ds1.Tables[0].Rows[0]["Buff_FAT_InKG"].ToString();
                    txtCow_KgTS.Text = ds1.Tables[0].Rows[0]["Cow_TS_InKG"].ToString();
                    txtComm_KgFAT.Text = ds1.Tables[0].Rows[0]["Comm_FAT_InKG"].ToString();
                    if (ds1.Tables[0].Rows[0]["RateEffectiveDate"].ToString() != null && ds1.Tables[0].Rows[0]["RateEffectiveDate"].ToString() != "")
                    {
                        txtEffectiveDate.Text = ds1.Tables[0].Rows[0]["RateEffectiveDate"].ToString();
                    }
                    if (ds1.Tables[0].Rows[0]["PaymentUpto"].ToString() != null && ds1.Tables[0].Rows[0]["PaymentUpto"].ToString() != "")
                    {
                        txtPaymentUpto.Text = ds1.Tables[0].Rows[0]["PaymentUpto"].ToString();
                    }
                    if (ds1.Tables[0].Rows[0]["DuesAmount"].ToString() != null && ds1.Tables[0].Rows[0]["DuesAmount"].ToString() != "")
                    {
                        txtDuesAmount.Text = ds1.Tables[0].Rows[0]["DuesAmount"].ToString();
                    }
                }
                else
                {
                    DataTable dt_MP = (DataTable)ViewState["dt_MPRate"];
                    txtBuff_KgFAT.Text = "";
                    txtCow_KgTS.Text = "";
                    txtComm_KgFAT.Text = "";
                    txtBuff_KgFAT.Text = dt_MP.Rows[0]["Buff_FAT_InKG"].ToString();
                    txtCow_KgTS.Text = dt_MP.Rows[0]["Cow_TS_InKG"].ToString();
                    txtComm_KgFAT.Text = dt_MP.Rows[0]["Comm_FAT_InKG"].ToString();
                    if (dt_MP.Rows[0]["RateEffectiveDate"].ToString() != null && dt_MP.Rows[0]["RateEffectiveDate"].ToString() != "")
                    {
                        txtEffectiveDate.Text = dt_MP.Rows[0]["RateEffectiveDate"].ToString();
                    }
                    if (dt_MP.Rows[0]["PaymentUpto"].ToString() != null && dt_MP.Rows[0]["PaymentUpto"].ToString() != "")
                    {
                        txtPaymentUpto.Text = dt_MP.Rows[0]["PaymentUpto"].ToString();
                    }
                    if (dt_MP.Rows[0]["DuesAmount"].ToString() != null && dt_MP.Rows[0]["DuesAmount"].ToString() != "")
                    {
                        txtDuesAmount.Text = dt_MP.Rows[0]["DuesAmount"].ToString();
                    }
                    dt_MP.Dispose();
                }

                //MILK RATE
                if (ds1.Tables[1].Rows.Count > 0)
                {
                    gvMilkSaleRate.DataSource = ds1.Tables[1];     // MILK SALE RATE         
                    gvMilkSaleRate.DataBind();
                }
                else
                {
                    DataTable dt1 = (DataTable)ViewState["gvMilkSaleRate"];
                    gvMilkSaleRate.DataSource = dt1;     // MILK SALE RATE         
                    gvMilkSaleRate.DataBind();
                    dt1.Dispose();
                }


                //OWN PROCUREMENT
                if (ds1.Tables[2].Rows.Count > 0)
                {
                    gvOwnProcurement.DataSource = ds1.Tables[2];  // cc milk procurement office wise
                    gvOwnProcurement.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FetchData();", true);
                }
                else
                {
                    DataTable dt2 = (DataTable)ViewState["gvOwnProcurement"];
                    gvOwnProcurement.DataSource = dt2;  // cc milk procurement office wise
                    gvOwnProcurement.DataBind();
                    dt2.Dispose();
                }

                //OTHER UNION / PARTY PROCUREMENT
                if (ds1.Tables[3].Rows.Count > 0)
                {
                    gvOtherUnionProcurement.DataSource = ds1.Tables[3];  // other union milk procurement
                    gvOtherUnionProcurement.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FetchData1();", true);
                }
                else
                {
                    DataTable dt3 = (DataTable)ViewState["gvOtherUnionProcurement"];
                    gvOtherUnionProcurement.DataSource = dt3;  // other union milk procurement
                    gvOtherUnionProcurement.DataBind();
                    dt3.Dispose();
                }

                //CONVERSION
                if (ds1.Tables[4].Rows.Count > 0)
                {
                    gvConversion.DataSource = ds1.Tables[4];  // CONVERSION
                    gvConversion.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FetchData2();", true);
                }
                else
                {
                    DataTable dt4 = (DataTable)ViewState["gvConversion"];
                    gvConversion.DataSource = dt4;  // CONVERSION
                    gvConversion.DataBind();
                    dt4.Dispose();
                }


                //CFP ACCOUNTING
                if (ds1.Tables[5].Rows.Count > 0)
                {
                    gvCFPAccouting.DataSource = ds1.Tables[5];  // CONVERSION
                    gvCFPAccouting.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "FetchData3();", true);
                }
                else
                {
                    DataTable dt5 = (DataTable)ViewState["gvCFPAccouting"];
                    gvCFPAccouting.DataSource = dt5;  // CONVERSION
                    gvCFPAccouting.DataBind();
                    dt5.Dispose();
                }

                //GHEE ACCOUNT
                if (ds1.Tables[6].Rows.Count > 0)
                {
                    txtGheePkt.Text = ds1.Tables[6].Rows[0]["StockPosition"].ToString();
                    txtGheeLosse.Text = ds1.Tables[6].Rows[1]["StockPosition"].ToString();
                }
                else
                {
                    txtGheePkt.Text = "0";
                    txtGheeLosse.Text = "0";
                }

                //COMODITIES CONSUMPTION
                if (ds1.Tables[7].Rows.Count > 0)
                {
                    txtSMP_QTY1.Text = ds1.Tables[7].Rows[0]["SMP_QTY"].ToString();
                    txtWB_QTY1.Text = ds1.Tables[7].Rows[0]["WB_QTY"].ToString();
                    txtSMP_QTY2.Text = ds1.Tables[7].Rows[1]["SMP_QTY"].ToString();
                    txtWB_QTY2.Text = ds1.Tables[7].Rows[1]["WB_QTY"].ToString();
                }
                else
                {
                    txtSMP_QTY1.Text = "";
                    txtWB_QTY1.Text = "";
                    txtSMP_QTY2.Text = "";
                    txtWB_QTY2.Text = "";
                }

                //COMODITIES STOCK POSITION
                if (ds1.Tables[8].Rows.Count > 0)
                {
                    gvComoditiesStockPosition.DataSource = ds1.Tables[8];  // COMODITIES STOCK POSITION
                    gvComoditiesStockPosition.DataBind();
                }
                else
                {
                    DataTable dt8 = (DataTable)ViewState["gvComoditiesStockPosition"];
                    gvComoditiesStockPosition.DataSource = dt8;  // COMODITIES STOCK POSITION
                    gvComoditiesStockPosition.DataBind();
                    dt8.Dispose();
                }


                //MILK CONSUMED IN INDIGNEOS PRODUCT MAKING (IN Kgs)
                if (ds1.Tables[9].Rows.Count > 0)
                {
                    txtMilkConsumedIndigneos.Text = ds1.Tables[9].Rows[0]["Quanity_InKG"].ToString();
                }
                else
                {
                    txtMilkConsumedIndigneos.Text = "";
                }


                //IP Product
                if (ds1.Tables[10].Rows.Count > 0)
                {
                    gvIPProduct.DataSource = ds1.Tables[10];
                    gvIPProduct.DataBind();
                }
                else
                {
                    DataTable dt10 = (DataTable)ViewState["gvIPProduct"];
                    gvIPProduct.DataSource = dt10;
                    gvIPProduct.DataBind();
                    dt10.Dispose();
                }
                //Town wise milk sale
                if (ds1.Tables[11].Rows.Count > 0)
                {
                    ViewState["Temp_TownwiseMilkSale"] = ds1.Tables[11];
                    if (ViewState["DynamicGridBind"] != null)
                    {
                        foreach (DataColumn column in ds1.Tables[11].Columns)
                        {
                            TemplateField tfield = new TemplateField();
                            tfield.HeaderText = column.ColumnName;
                            gvTownwiseMilkSale.Columns.Add(tfield);
                        }
                    }
                }
                GetTownwiseMilkSale();

            }
        }
        catch (Exception ex)
        {
            lblTownWiseMilkSale.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error Townwise Milk Sale Date Changed ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); GC.SuppressFinalize(objdb); }
        }
    }
    protected void txtEntryDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GetDateWiseData();
        }
        catch (Exception ex)
        {
            lblTownWiseMilkSale.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error Townwise Milk Sale Date Changed ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); GC.SuppressFinalize(objdb); }
        }
    }
    protected void gvMilkSaleRate_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "MILK RATE";
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvMilkSaleRate.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvOwnProcurement_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "OWN PROCUREMENT (IN KG)";
            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvOwnProcurement.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvOtherUnionProcurement_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "OTHER UNION / PARTY PROCUREMENT (IN KG)";
            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);


            gvOtherUnionProcurement.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvConversion_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "CONVERSION";
            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);
            gvConversion.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvCFPAccouting_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "CFP ACCOUNTING";
            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);
            gvCFPAccouting.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvComoditiesStockPosition_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "COMODITIES STOCK POSITION";
            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);
            gvComoditiesStockPosition.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvIPProduct_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "IP Product";
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow.Cells.Add(HeaderCell);
            gvIPProduct.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvTownwiseMilkSale_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Town wise Milk Sale";
            HeaderCell.ColumnSpan = gvTownwiseMilkSale.Columns.Count;
            HeaderGridRow.Cells.Add(HeaderCell);
            gvTownwiseMilkSale.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            try
            {
                ViewState["Temp_TownwiseMilkSale"] = "";
                if (ddlOffice.SelectedIndex > 0)
                {
                    MISFrom.Visible = true;
                    GetOfficeDetails();
                    if (ViewState["Temp_TownwiseMilkSale"] != null && ViewState["Temp_TownwiseMilkSale"] != "")
                    {
                        GetTownwiseMilkSale();
                    }
                }
                else
                {
                    MISFrom.Visible = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        catch (Exception ex)
        {
            lblTownWiseMilkSale.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error Townwise Milk Sale Date Changed ", ex.Message.ToString());
        }
    }
}