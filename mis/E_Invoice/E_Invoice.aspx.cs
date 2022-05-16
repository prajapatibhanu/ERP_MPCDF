using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class mis_E_Invoice_E_Invoice : System.Web.UI.Page
{
    DataSet ds;
    DataTable dt, dt2;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != "")
            {
                if (!IsPostBack)
                {
                    pnlNotM.Enabled = false;
                    pnlDispatch.Enabled = false;
                    pnlShipping.Enabled = false;
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["VoucherTx_ID"] = "";
                    txtDc_No.Attributes.Add("readonly", "readonly");
                    if (Request.QueryString["VoucherTx_No"] != "" && Request.QueryString["VoucherTx_ID"] != "")
                    {
                        ViewState["VoucherTx_No"] = objdb.Decrypt(Request.QueryString["VoucherTx_No"].ToString());
                        ViewState["VoucherTx_ID"] = objdb.Decrypt(Request.QueryString["VoucherTx_ID"].ToString());
                        FillVoucherDetails();
                    }
                    else
                    {
                        Response.Redirect("BRptB2B_Details.aspx");
                    }

                }

            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void FillVoucherDetails()
    {
        try
        {
            lblMsg.Text = "";

            ds = objdb.ByProcedure("SpFinE_Invoice", new string[] { "flag", "VoucherTx_No", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_No"].ToString(), ViewState["VoucherTx_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count != 0)
                {
                    txtS_Gstin.Text = ds.Tables[0].Rows[0]["Seller GSTIN"].ToString();
                    txtS_LglNm.Text = ds.Tables[0].Rows[0]["Legal Name"].ToString();
                    txtS_TrdNm.Text = ds.Tables[0].Rows[0]["Trad Name"].ToString();
                    txtS_Addr1.Text = ds.Tables[0].Rows[0]["Seller Address 1"].ToString();
                    txtS_Addr2.Text = ds.Tables[0].Rows[0]["Seller Address 2"].ToString();
                    txtS_Loc.Text = ds.Tables[0].Rows[0]["Seller Location"].ToString();
                    txtS_Pin.Text = ds.Tables[0].Rows[0]["Pin Code"].ToString();
                    // ddlS_Stcd.SelectedValue = ds.Tables[0].Rows[0]["S_Stcd"].ToString();
                    txtS_Ph.Text = ds.Tables[0].Rows[0]["Phone Number"].ToString();
                    txtS_Em.Text = ds.Tables[0].Rows[0]["Email ID"].ToString();
                }

                if (ds.Tables[1].Rows.Count != 0)
                {
                    txtDc_Typ.Text = ds.Tables[1].Rows[0]["Document Type"].ToString();
                    txtDc_No.Text = ds.Tables[1].Rows[0]["Document Number"].ToString();
                    txtDc_Dt.Text = ds.Tables[1].Rows[0]["Document Date"].ToString();

                }
                if (ds.Tables[2].Rows.Count != 0)
                {
                    txtB_Gstin.Text = ds.Tables[2].Rows[0]["Buyer GSTIN"].ToString();
                    txtB_LglNm.Text = ds.Tables[2].Rows[0]["Buyer Legal Name"].ToString();
                    txtB_TrdNm.Text = ds.Tables[2].Rows[0]["Buyer Trade Name"].ToString();
                    // txtB_Pos.Text = ds.Tables[2].Rows[0]["B_Pos"].ToString();
                    txtB_Addr1.Text = ds.Tables[2].Rows[0]["Buyer Addr1"].ToString();
                    txtB_Addr2.Text = ds.Tables[2].Rows[0]["Buyer Addr2"].ToString();
                    txtB_Loc.Text = ds.Tables[2].Rows[0]["Buyer Location"].ToString();
                    txtB_Pin.Text = ds.Tables[2].Rows[0]["Buyer Pin Code"].ToString();
                    //  txtB_Stcd.Text = ds.Tables[2].Rows[0]["B_Stcd"].ToString();
                    txtB_Ph.Text = ds.Tables[2].Rows[0]["Buyer Phone Number"].ToString();
                    txtB_Em.Text = ds.Tables[2].Rows[0]["Buyer Email Id"].ToString();
                }


                if (ds.Tables[3].Rows.Count != 0)
                {
                    ddlTr_RegRev.SelectedValue = ds.Tables[3].Rows[0]["Reverse Charge"].ToString();
                    ddlTr_IgstOnIntra.SelectedValue = ds.Tables[3].Rows[0]["Igst on Intra"].ToString();
                }

                if (ds.Tables[4].Rows.Count != 0)
                {
                    GridItemDetails.DataSource = ds.Tables[4];
                    GridItemDetails.DataBind();
                }
                if (ds.Tables[5].Rows.Count != 0)
                {
                    GridTotal.DataSource = ds.Tables[5];
                    GridTotal.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (txtVoucherNo.Text != "")
            {
                ds = objdb.ByProcedure("SpFinE_Invoice", new string[] { "flag", "VoucherTx_No" }, new string[] { "2", txtVoucherNo.Text }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        txtS_Gstin.Text = ds.Tables[0].Rows[0]["Seller GSTIN"].ToString();
                        txtS_LglNm.Text = ds.Tables[0].Rows[0]["Legal Name"].ToString();
                        txtS_TrdNm.Text = ds.Tables[0].Rows[0]["Trad Name"].ToString();
                        txtS_Addr1.Text = ds.Tables[0].Rows[0]["Seller Address 1"].ToString();
                        txtS_Addr2.Text = ds.Tables[0].Rows[0]["Seller Address 1"].ToString();
                        txtS_Loc.Text = ds.Tables[0].Rows[0]["Seller Location"].ToString();
                        txtS_Pin.Text = ds.Tables[0].Rows[0]["Pin Code"].ToString();
                        // ddlS_Stcd.SelectedValue = ds.Tables[0].Rows[0]["S_Stcd"].ToString();
                        txtS_Ph.Text = ds.Tables[0].Rows[0]["Phone Number"].ToString();
                        txtS_Em.Text = ds.Tables[0].Rows[0]["Email ID"].ToString();
                    }
                    if (ds.Tables[1].Rows.Count != 0)
                    {
                        ddlTr_RegRev.SelectedValue = ds.Tables[1].Rows[0]["Reverse Charge"].ToString();

                    }
                    if (ds.Tables[2].Rows.Count != 0)
                    {
                        txtDc_Typ.Text = ds.Tables[2].Rows[0]["Document Type"].ToString();
                        txtDc_No.Text = ds.Tables[2].Rows[0]["Document Number"].ToString();
                        txtDc_Dt.Text = ds.Tables[2].Rows[0]["Document Date"].ToString();

                    }
                    if (ds.Tables[3].Rows.Count != 0)
                    {
                        txtB_Gstin.Text = ds.Tables[3].Rows[0]["Buyer GSTIN"].ToString();
                        txtB_LglNm.Text = ds.Tables[3].Rows[0]["Buyer Legal Name"].ToString();
                        txtB_TrdNm.Text = ds.Tables[3].Rows[0]["Buyer Trade Name"].ToString();
                        // txtB_Pos.Text = ds.Tables[3].Rows[0]["B_Pos"].ToString();
                        txtB_Addr1.Text = ds.Tables[3].Rows[0]["Buyer Addr1"].ToString();
                        txtB_Addr2.Text = ds.Tables[3].Rows[0]["Buyer Addr2"].ToString();
                        txtB_Loc.Text = ds.Tables[3].Rows[0]["Buyer Location"].ToString();
                        txtB_Pin.Text = ds.Tables[3].Rows[0]["Buyer Pin Code"].ToString();
                        //  txtB_Stcd.Text = ds.Tables[3].Rows[0]["B_Stcd"].ToString();
                        txtB_Ph.Text = ds.Tables[3].Rows[0]["Buyer Phone Number"].ToString();
                        txtB_Em.Text = ds.Tables[3].Rows[0]["Buyer Email Id"].ToString();
                    }
                    if (ds.Tables[4].Rows.Count != 0)
                    {
                        GridItemDetails.DataSource = ds.Tables[4];
                        GridItemDetails.DataBind();
                    }
                    if (ds.Tables[5].Rows.Count != 0)
                    {
                        GridTotal.DataSource = ds.Tables[5];
                        GridTotal.DataBind();
                    }
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-info", "alert-info", "Alert!", "Enter Voucher No.");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSaveGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            // string VoucherTx_No = ViewState["VoucherTx_No"].ToString();
            string VoucherTx_No = txtDc_No.Text;
            string Version = txtVersion.Text;
            string Tr_TaxSch = "GST";
            string Tr_SupTyp = ddlTr_SupTyp.SelectedValue.ToString();
            string Tr_RegRev = ddlTr_RegRev.SelectedValue.ToString();

            string Tr_EcmGstin = txtTr_EcmGstin.Text;
            string Tr_IgstOnIntra = ddlTr_IgstOnIntra.SelectedValue.ToString();
            string Dc_Typ = txtDc_Typ.Text;
            string Dc_No = txtDc_No.Text;
            string Dc_Dt = "";
            if (txtDc_Dt.Text != "")
            {
                Dc_Dt = Convert.ToDateTime(txtDc_Dt.Text, cult).ToString("yyyy/MM/dd");
            }

            string S_Gstin = txtS_Gstin.Text;
            string S_LglNm = txtS_LglNm.Text;
            string S_TrdNm = txtS_TrdNm.Text;
            string S_Addr1 = txtS_Addr1.Text;
            string S_Addr2 = txtS_Addr2.Text;
            string S_Loc = txtS_Loc.Text;
            string S_Pin = txtS_Pin.Text;
            string S_Stcd = ddlS_Stcd.SelectedValue.ToString();
            string S_Ph = txtS_Ph.Text;
            string S_Em = txtS_Em.Text;

            string B_Gstin = txtB_Gstin.Text;
            string B_LglNm = txtB_LglNm.Text;
            string B_TrdNm = txtB_TrdNm.Text;
            string B_Pos = "23";
            string B_Addr1 = txtB_Addr1.Text;
            string B_Addr2 = txtB_Addr2.Text;
            string B_Loc = txtB_Loc.Text;
            string B_Pin = txtB_Pin.Text;
            string B_Stcd = ddlB_Stcd.SelectedValue.ToString();
            string B_Ph = txtB_Ph.Text;
            string B_Em = txtB_Em.Text;





            string Tot_AssVal = "";
            string Tot_CgstVal = "";
            string Tot_SgstVal = "";
            string Tot_IgstVal = "";
            string Tot_CesVal = "";
            string Tot_StCesVal = "";
            string Tot_Discount = "";
            string Tot_OthChrg = "";
            string Tot_RndOffAmt = "";
            string Tot_TotInvVal = "";
            string Tot_TotInvValFc = "";

            foreach (GridViewRow rows in GridTotal.Rows)
            {
                Label lblTot_AssVal = (Label)rows.FindControl("Tot_AssVal");
                Label lblTot_SgstVal = (Label)rows.FindControl("Tot_SgstVal");
                Label lblTot_CgstVal = (Label)rows.FindControl("Tot_CgstVal");
                Label lblTot_IgstVal = (Label)rows.FindControl("Tot_IgstVal");
                Label lblTot_CesVal = (Label)rows.FindControl("Tot_CesVal");
                Label lblTot_StCesVal = (Label)rows.FindControl("Tot_StCesVal");
                Label lblTot_Discount = (Label)rows.FindControl("Tot_Discount");
                Label lblTot_OthChrg = (Label)rows.FindControl("Tot_OthChrg");
                Label lblTot_RndOffAmt = (Label)rows.FindControl("Tot_RndOffAmt");
                Label lblTot_TotInvVal = (Label)rows.FindControl("Tot_TotInvVal");
                Label lblTot_TotInvValFc = (Label)rows.FindControl("Tot_TotInvValFc");

                Tot_AssVal = lblTot_AssVal.Text;
                Tot_CgstVal = lblTot_CgstVal.Text;
                Tot_SgstVal = lblTot_SgstVal.Text;
                Tot_IgstVal = lblTot_IgstVal.Text;
                Tot_CesVal = lblTot_CesVal.Text;
                Tot_StCesVal = lblTot_StCesVal.Text;
                Tot_Discount = lblTot_Discount.Text;
                Tot_OthChrg = lblTot_OthChrg.Text;
                Tot_RndOffAmt = lblTot_RndOffAmt.Text;
                Tot_TotInvVal = lblTot_TotInvVal.Text;
                Tot_TotInvValFc = lblTot_TotInvValFc.Text;
            }

            string Pay_Nm = txtPay_Nm.Text;
            string Pay_Accdet = txtPay_Accdet.Text;
            string Pay_Mode = txtPay_Mode.Text;
            string Pay_Fininsbr = txtPay_Fininsbr.Text;
            string Pay_Payterm = txtPay_Payterm.Text;
            string Pay_Payinstr = txtPay_Payinstr.Text;
            string Pay_Crtrn = txtPay_Crtrn.Text;
            string Pay_Dirdr = txtPay_Dirdr.Text;
            string Pay_Crday = txtPay_Crday.Text;
            string Pay_Paidamt = txtPay_Paidamt.Text;
            string Pay_Paymtdue = txtPay_Paymtdue.Text;
            string Ref_InvRm = txtRef_InvRm.Text;
            string Ref_InvStDt = "";
            if (txtRef_InvStDt.Text != "")
            {
                Ref_InvStDt = Convert.ToDateTime(txtRef_InvStDt.Text, cult).ToString("yyyy/MM/dd").ToString();
            }

            string Ref_InvEndDt = "";
            if (txtRef_InvEndDt.Text != "")
            {
                Ref_InvEndDt = Convert.ToDateTime(txtRef_InvEndDt.Text, cult).ToString("yyyy/MM/dd");
            }
            string Pdc_InvNo = txtPdc_InvNo.Text;
            string Pdc_InvDt = "";
            if (txtPdc_InvDt.Text != "")
            {
                Pdc_InvDt = Convert.ToDateTime(txtPdc_InvDt.Text, cult).ToString("yyyy/MM/dd");
            }
            string Pdc_OthRefNo = txtPdc_OthRefNo.Text;
            string Cont_RecAdvRefr = txtCont_RecAdvRefr.Text;
            string Cont_RecAdvDt = "";
            if (txtCont_RecAdvDt.Text != "")
            {
                Cont_RecAdvDt = Convert.ToDateTime(txtCont_RecAdvDt.Text, cult).ToString("yyyy/MM/dd");
            }
            string Cont_Tendrefr = txtCont_Tendrefr.Text;
            string Cont_Contrrefr = txtCont_Contrrefr.Text;
            string Cont_Extrefr = txtCont_Extrefr.Text;
            string Cont_Projrefr = txtCont_Projrefr.Text;
            string Cont_Porefr = txtCont_Porefr.Text;
            string Cont_PoRefDt = "";
            if (txtCont_PoRefDt.Text != "")
            {
                Cont_PoRefDt = Convert.ToDateTime(txtCont_PoRefDt.Text, cult).ToString("yyyy/MM/dd");
            }
            string Adc_Url = txtAdc_Url.Text;
            string Adc_Docs = txtAdc_Docs.Text;
            string Adc_Info = txtAdc_Info.Text;
            string Exp_ShipBNo = txtExp_ShipBNo.Text;
            string Exp_ShipBDt = "";
            if (txtExp_ShipBDt.Text != "")
            {
                Exp_ShipBDt = Convert.ToDateTime(txtExp_ShipBDt.Text, cult).ToString("yyyy/MM/dd");
            }
            string Exp_Port = txtExp_Port.Text;
            string Exp_RefClm = ddlExp_RefClm.SelectedValue.ToString();
            string Exp_ForCur = txtExp_ForCur.Text;
            string Exp_CntCode = txtExp_CntCode.Text;

            string IsDispatch = ddlIsDispatch.SelectedValue.ToString();
            string IsBillToShip = ddlIsBillToShip.SelectedValue.ToString();
            string IsBchDtls = ddlIsBchDtls.SelectedValue.ToString();
            string IsEwayBill = ddlIsEwayBill.SelectedValue.ToString();
            string IsOtherDtls = ddlIsOtherDtls.SelectedValue.ToString();

            string UpdatedBy = ViewState["Emp_ID"].ToString();

            string VoucherTx_ID = ViewState["VoucherTx_ID"].ToString();
            string Office_ID = ViewState["Office_ID"].ToString();

            ds = objdb.ByProcedure("SpFinE_Invoice"
                , new string[] { "flag", "Office_ID", "VoucherTx_ID", "VoucherTx_No", "Version", "Tr_TaxSch", "Tr_SupTyp", "Tr_RegRev", "Tr_EcmGstin", "Tr_IgstOnIntra", "Dc_Typ", "Dc_No", "Dc_Dt", "S_Gstin", "S_LglNm", "S_TrdNm", "S_Addr1", "S_Addr2", "S_Loc", "S_Pin", "S_Stcd", "S_Ph", "S_Em", "B_Gstin", "B_LglNm", "B_TrdNm", "B_Pos", "B_Addr1", "B_Addr2", "B_Loc", "B_Pin", "B_Stcd", "B_Ph", "B_Em", "Tot_AssVal", "Tot_CgstVal", "Tot_SgstVal", "Tot_IgstVal", "Tot_CesVal", "Tot_StCesVal", "Tot_Discount", "Tot_OthChrg", "Tot_RndOffAmt", "Tot_TotInvVal", "Tot_TotInvValFc", "Pay_Nm", "Pay_Accdet", "Pay_Mode", "Pay_Fininsbr", "Pay_Payterm", "Pay_Payinstr", "Pay_Crtrn", "Pay_Dirdr", "Pay_Crday", "Pay_Paidamt", "Pay_Paymtdue", "Ref_InvRm", "Ref_InvStDt", "Ref_InvEndDt", "Pdc_InvNo", "Pdc_InvDt", "Pdc_OthRefNo", "Cont_RecAdvRefr", "Cont_RecAdvDt", "Cont_Tendrefr", "Cont_Contrrefr", "Cont_Extrefr", "Cont_Projrefr", "Cont_Porefr", "Cont_PoRefDt", "Adc_Url", "Adc_Docs", "Adc_Info", "Exp_ShipBNo", "Exp_ShipBDt", "Exp_Port", "Exp_RefClm", "Exp_ForCur", "Exp_CntCode", "IsDispatch", "IsBillToShip", "IsBchDtls", "IsOtherDtls", "IsEwayBill", "UpdatedBy" }
                , new string[] { "0", Office_ID, VoucherTx_ID, VoucherTx_No, Version, Tr_TaxSch, Tr_SupTyp, Tr_RegRev, Tr_EcmGstin, Tr_IgstOnIntra, Dc_Typ, Dc_No, Dc_Dt, S_Gstin, S_LglNm, S_TrdNm, S_Addr1, S_Addr2, S_Loc, S_Pin, S_Stcd, S_Ph, S_Em, B_Gstin, B_LglNm, B_TrdNm, B_Pos, B_Addr1, B_Addr2, B_Loc, B_Pin, B_Stcd, B_Ph, B_Em, Tot_AssVal, Tot_CgstVal, Tot_SgstVal, Tot_IgstVal, Tot_CesVal, Tot_StCesVal, Tot_Discount, Tot_OthChrg, Tot_RndOffAmt, Tot_TotInvVal, Tot_TotInvValFc, Pay_Nm, Pay_Accdet, Pay_Mode, Pay_Fininsbr, Pay_Payterm, Pay_Payinstr, Pay_Crtrn, Pay_Dirdr, Pay_Crday, Pay_Paidamt, Pay_Paymtdue, Ref_InvRm, Ref_InvStDt, Ref_InvEndDt, Pdc_InvNo, Pdc_InvDt, Pdc_OthRefNo, Cont_RecAdvRefr, Cont_RecAdvDt, Cont_Tendrefr, Cont_Contrrefr, Cont_Extrefr, Cont_Projrefr, Cont_Porefr, Cont_PoRefDt, Adc_Url, Adc_Docs, Adc_Info, Exp_ShipBNo, Exp_ShipBDt, Exp_Port, Exp_RefClm, Exp_ForCur, Exp_CntCode, IsDispatch, IsBillToShip, IsBchDtls, IsOtherDtls, IsEwayBill, UpdatedBy }, "dataset");
            if (ds.Tables.Count > 0)
            {
                string E_ID = ds.Tables[0].Rows[0]["E_ID"].ToString();
                //string VoucherTx_ID = ds.Tables[0].Rows[0]["VoucherTx_ID"].ToString();
                //ViewState["VoucherTx_ID"] = VoucherTx_ID.ToString();
                /********Items Details***********/
                int Itm_SlNo = 0;
                foreach (GridViewRow rows in GridItemDetails.Rows)
                {
                    Itm_SlNo = Itm_SlNo + 1;
                    Label lblItemID = (Label)rows.FindControl("lblItemID");
                    Label Itm_PrdDesc = (Label)rows.FindControl("Itm_PrdDesc");
                    Label Itm_IsServc = (Label)rows.FindControl("Itm_IsServc");
                    Label Itm_Barcde = (Label)rows.FindControl("Itm_Barcde");
                    Label Itm_HsnCd = (Label)rows.FindControl("Itm_HsnCd");
                    Label Itm_Qty = (Label)rows.FindControl("Itm_Qty");
                    Label Itm_FreeQty = (Label)rows.FindControl("Itm_FreeQty");
                    Label Itm_Unit = (Label)rows.FindControl("Itm_Unit");
                    Label Itm_UnitPrice = (Label)rows.FindControl("Itm_UnitPrice");
                    Label Itm_TotAmt = (Label)rows.FindControl("Itm_TotAmt");
                    TextBox Itm_Discount = (TextBox)rows.FindControl("Itm_Discount");
                    Label Itm_PreTaxVal = (Label)rows.FindControl("Itm_PreTaxVal");
                    Label Itm_AssAmt = (Label)rows.FindControl("Itm_AssAmt");
                    Label Itm_GstRt = (Label)rows.FindControl("Itm_GstRt");
                    Label Itm_SgstAmt = (Label)rows.FindControl("Itm_SgstAmt");
                    Label Itm_CgstAmt = (Label)rows.FindControl("Itm_CgstAmt");
                    Label Itm_IgstAmt = (Label)rows.FindControl("Itm_IgstAmt");
                    Label Itm_CesRt = (Label)rows.FindControl("Itm_CesRt");
                    Label Itm_CesAmt = (Label)rows.FindControl("Itm_CesAmt");
                    Label Itm_CesNonAdvlAmt = (Label)rows.FindControl("Itm_CesNonAdvlAmt");
                    Label Itm_StateCesRt = (Label)rows.FindControl("Itm_StateCesRt");
                    Label Itm_StateCesAmt = (Label)rows.FindControl("Itm_StateCesAmt");
                    Label Itm_StateCesNonAdvlAmt = (Label)rows.FindControl("Itm_StateCesNonAdvlAmt");
                    Label Itm_OthChrg = (Label)rows.FindControl("Itm_OthChrg");
                    Label Itm_TotItemVal = (Label)rows.FindControl("Itm_TotItemVal");
                    Label Itm_OrdLineRef = (Label)rows.FindControl("Itm_OrdLineRef");
                    Label Itm_OrgCntry = (Label)rows.FindControl("Itm_OrgCntry");
                    Label Itm_PrdSlNo = (Label)rows.FindControl("Itm_PrdSlNo");
                    Label Bch_Nm = (Label)rows.FindControl("Bch_Nm");
                    Label Bch_Expdt = (Label)rows.FindControl("Bch_Expdt");
                    Label Bch_wrDt = (Label)rows.FindControl("Bch_wrDt");
                    Label Att_Nm = (Label)rows.FindControl("Att_Nm");
                    Label Att_Val = (Label)rows.FindControl("Att_Val");
                    // Item Detail
                    if (IsBchDtls == "Y")
                    {
                        IsBchDtls = "Y";
                    }
                    // Item Detail
                    if (IsOtherDtls == "Y")
                    {
                        IsOtherDtls = "Y";
                    }

                    objdb.ByProcedure("SpFinE_Invoice"
                               , new string[] { "flag", "VoucherTx_ID", "E_ID", "Itm_SlNo", "Itm_PrdDesc", "Itm_IsServc", "Itm_HsnCd", "Itm_Barcde", "Itm_Qty", "Itm_FreeQty", "Itm_Unit", "Itm_UnitPrice", "Itm_TotAmt", "Itm_Discount", "Itm_PreTaxVal", "Itm_AssAmt", "Itm_GstRt", "Itm_IgstAmt", "Itm_CgstAmt", "Itm_SgstAmt", "Itm_CesRt", "Itm_CesAmt", "Itm_CesNonAdvlAmt", "Itm_StateCesRt", "Itm_StateCesAmt", "Itm_StateCesNonAdvlAmt", "Itm_OthChrg", "Itm_TotItemVal", "Itm_OrdLineRef", "Itm_OrgCntry", "Itm_PrdSlNo", "Bch_Nm", "Bch_Expdt", "Bch_wrDt", "Att_Nm", "Att_Val" }
                               , new string[] { "5", VoucherTx_ID, E_ID, Itm_SlNo.ToString(), Itm_PrdDesc.Text, Itm_IsServc.Text, Itm_HsnCd.Text, Itm_Barcde.Text, Itm_Qty.Text, Itm_FreeQty.Text, Itm_Unit.Text, Itm_UnitPrice.Text, Itm_TotAmt.Text, Itm_Discount.Text, Itm_PreTaxVal.Text, Itm_AssAmt.Text, Itm_GstRt.Text, Itm_IgstAmt.Text, Itm_CgstAmt.Text, Itm_SgstAmt.Text, Itm_CesRt.Text, Itm_CesAmt.Text, Itm_CesNonAdvlAmt.Text, Itm_StateCesRt.Text, Itm_StateCesAmt.Text, Itm_StateCesNonAdvlAmt.Text, Itm_OthChrg.Text, Itm_TotItemVal.Text, Itm_OrdLineRef.Text, Itm_OrgCntry.Text, Itm_PrdSlNo.Text, Bch_Nm.Text, Bch_Expdt.Text, Bch_wrDt.Text, Att_Nm.Text, Att_Val.Text }, "dataset");
                }


                if (IsDispatch == "Y")
                {
                    IsDispatch = "Y";
                    string D_Nm = txtD_Nm.Text;
                    string D_Addr1 = txtD_Addr1.Text;
                    string D_Addr2 = txtD_Addr2.Text;
                    string D_Loc = txtD_Loc.Text;
                    string D_Pin = txtD_Pin.Text;
                    string D_Stcd = ddlD_Stcd.SelectedValue.ToString();
                    objdb.ByProcedure("SpFinE_Invoice"
                               , new string[] { "flag", "E_ID", "D_Nm", "D_Addr1", "D_Addr2", "D_Loc", "D_Pin", "D_Stcd" }
                               , new string[] { "3", E_ID, D_Nm, D_Addr1, D_Addr2, D_Loc, D_Pin, D_Stcd }, "dataset");
                }
                if (IsBillToShip == "Y")
                {
                    IsBillToShip = "Y";
                    string Sp_Gstin = txtSp_Gstin.Text;
                    string Sp_LglNm = txtSp_LglNm.Text;
                    string Sp_TrdNm = txtSp_TrdNm.Text;
                    string Sp_Addr1 = txtSp_Addr1.Text;
                    string Sp_Addr2 = txtSp_Addr2.Text;
                    string Sp_Loc = txtSp_Loc.Text;
                    string Sp_Pin = txtSp_Pin.Text;
                    string Sp_Stcd = ddl_Sp_Stcd.SelectedValue.ToString();
                    objdb.ByProcedure("SpFinE_Invoice"
                              , new string[] { "flag", "E_ID", "Sp_Gstin", "Sp_LglNm", "Sp_TrdNm", "Sp_Addr1", "Sp_Addr2", "Sp_Loc", "Sp_Pin", "Sp_Stcd" }
                              , new string[] { "4", E_ID, Sp_Gstin, Sp_LglNm, Sp_TrdNm, Sp_Addr1, Sp_Addr2, Sp_Loc, Sp_Pin, Sp_Stcd }, "dataset");
                }


                //if (IsEwayBill == "Y")
                //{
                //    IsEwayBill = "Y";
                //}

            }
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");


            /****************************/
            GenerateJson(ViewState["VoucherTx_ID"].ToString(), ViewState["VoucherTx_No"].ToString());
            /****************************/



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }


    public void GenerateJson(string VoucherTx_ID, string VoucherTx_No)
    {
        DataTable datatable13;
        ds = objdb.ByProcedure("SpFinE_Invoice",
             new string[] { "flag", "VoucherTx_ID" },
                     new string[] { "9", VoucherTx_ID.ToString() }, "dataset");

        /***********Invoice Version **************/
        string E_invoice_version = ds.Tables[0].Rows[0]["Version"].ToString();
        /*******END********/
        datatable13 = ds.Tables[13];
        /***********Transaction Detail **************/
        Dictionary<string, string> TranDtls = new Dictionary<string, string>();
        TranDtls.Add("TaxSch", ds.Tables[1].Rows[0]["TaxSch"].ToString());
        TranDtls.Add("SupTyp", ds.Tables[1].Rows[0]["SupTyp"].ToString());
        TranDtls.Add("IgstOnIntra", ds.Tables[1].Rows[0]["IgstOnIntra"].ToString());

        if ((ds.Tables[1].Rows[0]["RegRev"].ToString() == "N") || (ds.Tables[1].Rows[0]["RegRev"].ToString() == ""))
        {
            TranDtls.Add("RegRev", null);
        }
        else
        {
            TranDtls.Add("RegRev", ds.Tables[1].Rows[0]["RegRev"].ToString());
        }

        if (ds.Tables[1].Rows[0]["EcmGstin"].ToString() == "")
        {
            TranDtls.Add("EcmGstin", null);
        }
        else
        {
            TranDtls.Add("EcmGstin", ds.Tables[1].Rows[0]["EcmGstin"].ToString());
        }
        /*******END********/

        /***********Doc Detail **************/
        Dictionary<string, string> DocDtls = new Dictionary<string, string>();
        DocDtls.Add("Typ", ds.Tables[2].Rows[0]["Typ"].ToString());
        DocDtls.Add("No", ds.Tables[2].Rows[0]["No"].ToString());
        DocDtls.Add("Dt", ds.Tables[2].Rows[0]["Dt"].ToString());
        /*******END********/

        /************Item Details*******************/
        List<Dictionary<string, object>> ItemList = new List<Dictionary<string, object>>();
        Dictionary<string, object> row = null;

        List<Dictionary<string, object>> inner_rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> inner_row = new Dictionary<string, object>();
        inner_row.Add("Nm", null);
        inner_row.Add("Val", null);
        inner_rows.Add(inner_row);

        foreach (DataRow rs in datatable13.Rows)
        {
            //row = new Dictionary<string, object>();
            //foreach (DataColumn col in datatable13.Columns)
            //{
            //    row.Add(col.ColumnName, rs[col]);
            //}

            //ItemList.Add(row);
            row = new Dictionary<string, object>();
            row.Add("SlNo", rs["SlNo"].ToString());
            row.Add("PrdDesc", rs["PrdDesc"].ToString());
            row.Add("IsServc", rs["IsServc"].ToString());
            row.Add("HsnCd", rs["HsnCd"].ToString());
            //row.Add("Barcde", rs["Barcde"].ToString());
            row.Add("Barcde", null);

            // ValDtls.Add("AssVal", decimal.Parse((ds.Tables[7].Rows[0]["Tot_AssVal"].ToString() != "") ? ds.Tables[7].Rows[0]["Tot_AssVal"].ToString() : "0"));

            if (!(objdb.isNumber(rs["Qty"].ToString())))
            {
                row.Add("Qty", float.Parse((rs["Qty"].ToString() != "") ? rs["Qty"].ToString() : "0"));
            }
            else
            {
                row.Add("Qty", int.Parse((rs["Qty"].ToString() != "") ? rs["Qty"].ToString() : "0"));
            }
            //row.Add("Qty", rs["Qty"].ToString());
            row.Add("FreeQty", 0);
            //row.Add("FreeQty", rs["FreeQty"].ToString());

            if (rs["Unit"].ToString() == "Number")
            {
                row.Add("Unit", "NOS");
            }
            else
            {
                row.Add("Unit", rs["Unit"].ToString());
            }


            //row.Add("Unit", rs["Unit"].ToString());
            row.Add("UnitPrice", decimal.Parse((rs["UnitPrice"].ToString() != "") ? rs["UnitPrice"].ToString() : "0"));
            //row.Add("UnitPrice", rs["UnitPrice"].ToString());
            row.Add("TotAmt", decimal.Parse((rs["TotAmt"].ToString() != "") ? rs["TotAmt"].ToString() : "0"));
            //row.Add("TotAmt", rs["TotAmt"].ToString());
            row.Add("Discount", decimal.Parse((rs["Discount"].ToString() != "") ? rs["Discount"].ToString() : "0"));
            //row.Add("Discount", rs["Discount"].ToString());
            row.Add("PreTaxVal", decimal.Parse((rs["PreTaxVal"].ToString() != "") ? rs["PreTaxVal"].ToString() : "0"));
            //row.Add("PreTaxVal", rs["PreTaxVal"].ToString());
            row.Add("AssAmt", decimal.Parse((rs["AssAmt"].ToString() != "") ? rs["AssAmt"].ToString() : "0"));
            //row.Add("AssAmt", rs["AssAmt"].ToString());
            row.Add("GstRt", decimal.Parse((rs["GstRt"].ToString() != "") ? rs["GstRt"].ToString() : "0"));
            //row.Add("GstRt", rs["GstRt"].ToString());
            row.Add("IgstAmt", decimal.Parse((rs["IgstAmt"].ToString() != "") ? rs["IgstAmt"].ToString() : "0"));
            //row.Add("IgstAmt", rs["IgstAmt"].ToString());
            row.Add("CgstAmt", decimal.Parse((rs["CgstAmt"].ToString() != "") ? rs["CgstAmt"].ToString() : "0"));
            //row.Add("CgstAmt", rs["CgstAmt"].ToString());
            row.Add("SgstAmt", decimal.Parse((rs["SgstAmt"].ToString() != "") ? rs["SgstAmt"].ToString() : "0"));
            //row.Add("SgstAmt", rs["SgstAmt"].ToString());
            row.Add("CesRt", decimal.Parse((rs["CesRt"].ToString() != "") ? rs["CesRt"].ToString() : "0"));
            //row.Add("CesRt", rs["CesRt"].ToString());
            row.Add("CesAmt", decimal.Parse((rs["CesAmt"].ToString() != "") ? rs["CesAmt"].ToString() : "0"));
            //row.Add("CesAmt", rs["CesAmt"].ToString());
            row.Add("CesNonAdvlAmt", decimal.Parse((rs["CesNonAdvlAmt"].ToString() != "") ? rs["CesNonAdvlAmt"].ToString() : "0"));
            //row.Add("CesNonAdvlAmt", rs["CesNonAdvlAmt"].ToString());
            row.Add("StateCesRt", decimal.Parse((rs["StateCesRt"].ToString() != "") ? rs["StateCesRt"].ToString() : "0"));
            //row.Add("StateCesRt", rs["StateCesRt"].ToString());
            row.Add("StateCesAmt", decimal.Parse((rs["StateCesAmt"].ToString() != "") ? rs["StateCesAmt"].ToString() : "0"));
            //row.Add("StateCesAmt", rs["StateCesAmt"].ToString());
            row.Add("StateCesNonAdvlAmt", decimal.Parse((rs["StateCesNonAdvlAmt"].ToString() != "") ? rs["StateCesNonAdvlAmt"].ToString() : "0"));
            //row.Add("StateCesNonAdvlAmt", rs["StateCesNonAdvlAmt"].ToString());
            row.Add("OthChrg", decimal.Parse((rs["OthChrg"].ToString() != "") ? rs["OthChrg"].ToString() : "0"));
            //row.Add("OthChrg", rs["OthChrg"].ToString());
            row.Add("TotItemVal", decimal.Parse((rs["TotItemVal"].ToString() != "") ? rs["TotItemVal"].ToString() : "0"));
            //row.Add("TotItemVal", rs["TotItemVal"].ToString());

            row.Add("OrdLineRef", null);
            row.Add("OrgCntry", null);
            row.Add("PrdSlNo", null);
            row.Add("BchDtls", null);
            row.Add("AttribDtls", inner_rows);
            ItemList.Add(row);
        }
        /*******END********/


        /************Additional Details*******************/
        List<Dictionary<string, object>> AddlDocDtls = new List<Dictionary<string, object>>();
        Dictionary<string, object> AddlDocDtls_row = new Dictionary<string, object>();
        AddlDocDtls_row.Add("Url", null);
        AddlDocDtls_row.Add("Docs", null);
        AddlDocDtls_row.Add("Info", null);
        AddlDocDtls.Add(AddlDocDtls_row);
        /*******END********/


        /************Seller Detail*******************/
        Dictionary<string, object> SellerDtls = new Dictionary<string, object>();
        SellerDtls.Add("Gstin", ds.Tables[3].Rows[0]["S_Gstin"].ToString());

        if (ds.Tables[3].Rows[0]["S_LglNm"].ToString() == "")
        {
            SellerDtls.Add("LglNm", null);
        }
        else
        {
            SellerDtls.Add("LglNm", ds.Tables[3].Rows[0]["S_LglNm"].ToString());
        }

        //SellerDtls.Add("LglNm", ds.Tables[3].Rows[0]["S_LglNm"].ToString());
        if (ds.Tables[3].Rows[0]["S_TrdNm"].ToString() == "")
        {
            SellerDtls.Add("TrdNm", null);
        }
        else
        {
            SellerDtls.Add("TrdNm", ds.Tables[3].Rows[0]["S_TrdNm"].ToString());
        }

        //SellerDtls.Add("TrdNm", ds.Tables[3].Rows[0]["S_TrdNm"].ToString());
        if (ds.Tables[3].Rows[0]["S_Addr1"].ToString() == "")
        {
            SellerDtls.Add("Addr1", null);
        }
        else
        {
            SellerDtls.Add("Addr1", ds.Tables[3].Rows[0]["S_Addr1"].ToString());
        }
        //SellerDtls.Add("Addr1", ds.Tables[3].Rows[0]["S_Addr1"].ToString());
        if (ds.Tables[3].Rows[0]["S_Addr2"].ToString() == "")
        {
            SellerDtls.Add("Addr2", null);
        }
        else
        {
            SellerDtls.Add("Addr2", ds.Tables[3].Rows[0]["S_Addr2"].ToString());
        }
        //SellerDtls.Add("Addr2", ds.Tables[3].Rows[0]["S_Addr2"].ToString());
        if (ds.Tables[3].Rows[0]["S_Loc"].ToString() == "")
        {
            SellerDtls.Add("Loc", null);
        }
        else
        {
            SellerDtls.Add("Loc", ds.Tables[3].Rows[0]["S_Loc"].ToString());
        }
        //SellerDtls.Add("Loc", ds.Tables[3].Rows[0]["S_Loc"].ToString());
        if (ds.Tables[3].Rows[0]["S_Pin"].ToString() == "")
        {
            SellerDtls.Add("Pin", null);
        }
        else
        {
            SellerDtls.Add("Pin", int.Parse(ds.Tables[3].Rows[0]["S_Pin"].ToString()));
        }
        //SellerDtls.Add("Pin", ds.Tables[3].Rows[0]["S_Pin"].ToString());
        if (ds.Tables[3].Rows[0]["S_Stcd"].ToString() == "")
        {
            SellerDtls.Add("Stcd", null);
        }
        else
        {
            SellerDtls.Add("Stcd", ds.Tables[3].Rows[0]["S_Stcd"].ToString());
        }
        //SellerDtls.Add("Stcd", ds.Tables[3].Rows[0]["S_Stcd"].ToString());
        if (ds.Tables[3].Rows[0]["S_Ph"].ToString() == "")
        {
            SellerDtls.Add("Ph", null);
        }
        else
        {
            SellerDtls.Add("Ph", ds.Tables[3].Rows[0]["S_Ph"].ToString());
        }
        //SellerDtls.Add("Ph", ds.Tables[3].Rows[0]["S_Ph"].ToString());
        if (ds.Tables[3].Rows[0]["S_Em"].ToString() == "")
        {
            SellerDtls.Add("Em", null);
        }
        else
        {
            SellerDtls.Add("Em", ds.Tables[3].Rows[0]["S_Em"].ToString());
        }
        //SellerDtls.Add("Em", ds.Tables[3].Rows[0]["S_Em"].ToString());
        /*******END********/

        /************Buyer Detail*******************/
        Dictionary<string, object> BuyerDtls = new Dictionary<string, object>();
        BuyerDtls.Add("Gstin", ds.Tables[4].Rows[0]["B_Gstin"].ToString());

        if (ds.Tables[4].Rows[0]["B_LglNm"].ToString() == "")
        {
            BuyerDtls.Add("LglNm", null);
        }
        else
        {
            BuyerDtls.Add("LglNm", ds.Tables[4].Rows[0]["B_LglNm"].ToString());
        }

        //BuyerDtls.Add("LglNm", ds.Tables[4].Rows[0]["B_LglNm"].ToString());

        if (ds.Tables[4].Rows[0]["B_TrdNm"].ToString() == "")
        {
            BuyerDtls.Add("TrdNm", null);
        }
        else
        {
            BuyerDtls.Add("TrdNm", ds.Tables[4].Rows[0]["B_TrdNm"].ToString());
        }

        //BuyerDtls.Add("TrdNm", ds.Tables[4].Rows[0]["B_TrdNm"].ToString());

        if (ds.Tables[4].Rows[0]["B_Pos"].ToString() == "")
        {
            BuyerDtls.Add("Pos", null);
        }
        else
        {
            BuyerDtls.Add("Pos", ds.Tables[4].Rows[0]["B_Pos"].ToString());
        }

        //BuyerDtls.Add("Pos", ds.Tables[4].Rows[0]["B_Pos"].ToString());

        BuyerDtls.Add("Addr1", ds.Tables[4].Rows[0]["B_Addr1"].ToString());

        if (ds.Tables[4].Rows[0]["B_Addr2"].ToString() == "")
        {
            BuyerDtls.Add("Addr2", null);
        }
        else
        {
            BuyerDtls.Add("Addr2", ds.Tables[4].Rows[0]["B_Addr2"].ToString());
        }

        //BuyerDtls.Add("Addr2", ds.Tables[4].Rows[0]["B_Addr2"].ToString());
        if (ds.Tables[4].Rows[0]["B_Loc"].ToString() == "")
        {
            BuyerDtls.Add("Loc", null);
        }
        else
        {
            BuyerDtls.Add("Loc", ds.Tables[4].Rows[0]["B_Loc"].ToString());
        }

        //BuyerDtls.Add("Loc", ds.Tables[4].Rows[0]["B_Loc"].ToString());
        if (ds.Tables[4].Rows[0]["B_Pin"].ToString() == "")
        {
            BuyerDtls.Add("Pin", null);
        }
        else
        {
            BuyerDtls.Add("Pin", int.Parse(ds.Tables[4].Rows[0]["B_Pin"].ToString()));
        }
        //BuyerDtls.Add("Pin", int.Parse(ds.Tables[4].Rows[0]["B_Pin"].ToString()));
        if (ds.Tables[4].Rows[0]["B_Stcd"].ToString() == "")
        {
            BuyerDtls.Add("Stcd", null);
        }
        else
        {
            BuyerDtls.Add("Stcd", ds.Tables[4].Rows[0]["B_Stcd"].ToString());
        }
        //BuyerDtls.Add("Stcd", ds.Tables[4].Rows[0]["B_Stcd"].ToString());

        if (ds.Tables[4].Rows[0]["B_Ph"].ToString() == "")
        {
            BuyerDtls.Add("Ph", null);
        }
        else
        {
            BuyerDtls.Add("Ph", ds.Tables[4].Rows[0]["B_Ph"].ToString());
        }

        //BuyerDtls.Add("Ph", ds.Tables[4].Rows[0]["B_Ph"].ToString());

        if (ds.Tables[4].Rows[0]["B_Em"].ToString() == "")
        {
            BuyerDtls.Add("Em", null);
        }
        else
        {
            BuyerDtls.Add("Em", ds.Tables[4].Rows[0]["B_Em"].ToString());
        }
        //BuyerDtls.Add("Em", ds.Tables[4].Rows[0]["B_Em"].ToString());

        /*******END********/


        /************Dispatch Detail*******************/
        Dictionary<string, object> DispDtls = new Dictionary<string, object>();

        if (ds.Tables[5].Rows[0]["D_Nm"].ToString() == "")
        {
            DispDtls.Add("Nm", null);
        }
        else
        {
            DispDtls.Add("Nm", ds.Tables[5].Rows[0]["D_Nm"].ToString());
        }



        DispDtls.Add("Addr1", ds.Tables[5].Rows[0]["D_Addr1"].ToString());

        if (ds.Tables[5].Rows[0]["D_Addr2"].ToString() == "")
        {
            DispDtls.Add("Addr2", null);
        }
        else
        {
            DispDtls.Add("Addr2", ds.Tables[5].Rows[0]["D_Addr2"].ToString());
        }

        if (ds.Tables[5].Rows[0]["D_Loc"].ToString() == "")
        {
            DispDtls.Add("Loc", null);
        }
        else
        {
            DispDtls.Add("Loc", ds.Tables[5].Rows[0]["D_Loc"].ToString());
        }

        if (ds.Tables[5].Rows[0]["D_Pin"].ToString() == "")
        {
            DispDtls.Add("Pin", null);
        }
        else
        {
            DispDtls.Add("Pin", int.Parse(ds.Tables[5].Rows[0]["D_Pin"].ToString()));
        }

        if (ds.Tables[5].Rows[0]["D_Stcd"].ToString() == "")
        {
            DispDtls.Add("Stcd", null);
        }
        else
        {
            DispDtls.Add("Stcd", ds.Tables[5].Rows[0]["D_Stcd"].ToString());
        }

        /*******END********/


        /************Shipping Detail*******************/
        Dictionary<string, object> ShipDtls = new Dictionary<string, object>();

        ShipDtls.Add("Gstin", ds.Tables[6].Rows[0]["Sp_Gstin"].ToString());

        if (ds.Tables[6].Rows[0]["Sp_LglNm"].ToString() == "")
        {
            ShipDtls.Add("LglNm", null);
        }
        else
        {
            ShipDtls.Add("LglNm", ds.Tables[6].Rows[0]["Sp_LglNm"].ToString());
        }


        if (ds.Tables[6].Rows[0]["Sp_TrdNm"].ToString() == "")
        {
            ShipDtls.Add("TrdNm", null);
        }
        else
        {
            ShipDtls.Add("TrdNm", ds.Tables[6].Rows[0]["Sp_TrdNm"].ToString());
        }


        ShipDtls.Add("Addr1", ds.Tables[6].Rows[0]["Sp_Addr1"].ToString());

        if (ds.Tables[6].Rows[0]["Sp_Addr2"].ToString() == "")
        {
            ShipDtls.Add("Addr2", null);
        }
        else
        {
            ShipDtls.Add("Addr2", ds.Tables[6].Rows[0]["Sp_Addr2"].ToString());
        }


        if (ds.Tables[6].Rows[0]["Sp_Loc"].ToString() == "")
        {
            ShipDtls.Add("Loc", null);
        }
        else
        {
            ShipDtls.Add("Loc", ds.Tables[6].Rows[0]["Sp_Loc"].ToString());
        }

        if (ds.Tables[6].Rows[0]["Sp_Pin"].ToString() == "")
        {
            ShipDtls.Add("Pin", null);
        }
        else
        {
            ShipDtls.Add("Pin", int.Parse(ds.Tables[6].Rows[0]["Sp_Pin"].ToString()));
        }

        if (ds.Tables[6].Rows[0]["Sp_Stcd"].ToString() == "")
        {
            ShipDtls.Add("Stcd", null);
        }
        else
        {
            ShipDtls.Add("Stcd", ds.Tables[6].Rows[0]["Sp_Stcd"].ToString());
        }


        /*******END********/

        /************ValDtls Detail*******************/
        Dictionary<string, object> ValDtls = new Dictionary<string, object>();
        ValDtls.Add("AssVal", decimal.Parse((ds.Tables[7].Rows[0]["Tot_AssVal"].ToString() != "") ? ds.Tables[7].Rows[0]["Tot_AssVal"].ToString() : "0"));

        ValDtls.Add("IgstVal", decimal.Parse((ds.Tables[7].Rows[0]["Tot_IgstVal"].ToString() != "") ? ds.Tables[7].Rows[0]["Tot_IgstVal"].ToString() : "0"));
        //ValDtls.Add("IgstVal", ds.Tables[7].Rows[0]["Tot_IgstVal"].ToString());

        ValDtls.Add("CgstVal", decimal.Parse((ds.Tables[7].Rows[0]["Tot_CgstVal"].ToString() != "") ? ds.Tables[7].Rows[0]["Tot_CgstVal"].ToString() : "0"));
        //ValDtls.Add("CgstVal", ds.Tables[7].Rows[0]["Tot_CgstVal"].ToString());
        ValDtls.Add("SgstVal", decimal.Parse((ds.Tables[7].Rows[0]["Tot_SgstVal"].ToString() != "") ? ds.Tables[7].Rows[0]["Tot_SgstVal"].ToString() : "0"));
        //ValDtls.Add("SgstVal", ds.Tables[7].Rows[0]["Tot_SgstVal"].ToString());
        ValDtls.Add("CesVal", decimal.Parse((ds.Tables[7].Rows[0]["Tot_CesVal"].ToString() != "") ? ds.Tables[7].Rows[0]["Tot_CesVal"].ToString() : "0"));
        //ValDtls.Add("CesVal", ds.Tables[7].Rows[0]["Tot_CesVal"].ToString());
        ValDtls.Add("StCesVal", decimal.Parse((ds.Tables[7].Rows[0]["Tot_StCesVal"].ToString() != "") ? ds.Tables[7].Rows[0]["Tot_StCesVal"].ToString() : "0"));
        //ValDtls.Add("StCesVal", ds.Tables[7].Rows[0]["Tot_StCesVal"].ToString());
        ValDtls.Add("Discount", decimal.Parse((ds.Tables[7].Rows[0]["Tot_Discount"].ToString() != "") ? ds.Tables[7].Rows[0]["Tot_Discount"].ToString() : "0"));
        //ValDtls.Add("Discount", ds.Tables[7].Rows[0]["Tot_Discount"].ToString());
        ValDtls.Add("OthChrg", decimal.Parse((ds.Tables[7].Rows[0]["Tot_OthChrg"].ToString() != "") ? ds.Tables[7].Rows[0]["Tot_OthChrg"].ToString() : "0"));
        //ValDtls.Add("OthChrg", ds.Tables[7].Rows[0]["Tot_OthChrg"].ToString());

        ValDtls.Add("RndOffAmt", decimal.Parse((ds.Tables[7].Rows[0]["Tot_RndOffAmt"].ToString() != "") ? ds.Tables[7].Rows[0]["Tot_RndOffAmt"].ToString() : "0"));
        //ValDtls.Add("RndOffAmt", ds.Tables[7].Rows[0]["Tot_RndOffAmt"].ToString());

        ValDtls.Add("TotInvVal", decimal.Parse((ds.Tables[7].Rows[0]["Tot_TotInvVal"].ToString() != "") ? ds.Tables[7].Rows[0]["Tot_TotInvVal"].ToString() : "0"));
        //ValDtls.Add("TotInvVal", ds.Tables[7].Rows[0]["Tot_TotInvVal"].ToString());

        ValDtls.Add("TotInvValFc", decimal.Parse((ds.Tables[7].Rows[0]["Tot_TotInvValFc"].ToString() != "") ? ds.Tables[7].Rows[0]["Tot_TotInvValFc"].ToString() : "0"));
        //ValDtls.Add("TotInvValFc", ds.Tables[7].Rows[0]["Tot_TotInvValFc"].ToString());
        /*******END********/


        Dictionary<string, object> CompleteList = new Dictionary<string, object>();
        CompleteList.Add("Version", E_invoice_version);
        CompleteList.Add("TranDtls", TranDtls);
        CompleteList.Add("DocDtls", DocDtls);
        CompleteList.Add("SellerDtls", SellerDtls);
        CompleteList.Add("BuyerDtls", BuyerDtls);

        string IsDispatch = ddlIsDispatch.SelectedValue.ToString();
        string IsBillToShip = ddlIsBillToShip.SelectedValue.ToString();


        if (IsDispatch == "Y")
        {
            CompleteList.Add("DispDtls", DispDtls);
        }
        else
        {
            CompleteList.Add("DispDtls", null);
        }

        if (IsBillToShip == "Y")
        {
            CompleteList.Add("ShipDtls", ShipDtls);
        }
        else
        {
            CompleteList.Add("ShipDtls", null);
        }


        CompleteList.Add("ValDtls", ValDtls);
        CompleteList.Add("ExpDtls", null);
        CompleteList.Add("EwbDtls", null);
        CompleteList.Add("PayDtls", null);
        CompleteList.Add("RefDtls", null);
        CompleteList.Add("AddlDocDtls", AddlDocDtls);
        CompleteList.Add("ItemList", ItemList);

        List<object> FinalJson = new List<object>();
        FinalJson.Add(CompleteList);

        string json = JsonConvert.SerializeObject(FinalJson, Formatting.Indented);
        string txt = string.Empty;
        txt = json.ToString();

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment; filename=B2B_" + DateTime.Now.ToString("MM-dd-yyyy") + "_" + ViewState["VoucherTx_No"].ToString() + ".json");
        Response.Charset = "";
        Response.ContentType = "application/json";
        Response.Output.Write(txt);
        //  Response.Redirect(txt, false);
        Response.Flush();
        Response.End();
    }
    protected void ddlIsDispatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIsDispatch.SelectedValue.ToString() == "Y")
        {
            pnlDispatch.Enabled = true;
        }
        else
        {
            pnlDispatch.Enabled = false;
        }

    }
    protected void ddlIsBillToShip_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIsBillToShip.SelectedValue.ToString() == "Y")
        {
            pnlShipping.Enabled = true;
        }
        else
        {
            pnlShipping.Enabled = false;
        }
    }
}