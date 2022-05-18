using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mis_Mis_Reports_MPRU_MonthlyEntryFrom : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFOGrandTotal.Attributes.Add("readonly", "readonly");
            txtPPMGrandTotal.Attributes.Add("readonly", "readonly");
            txtdcsclosedtemp.Attributes.Add("readonly", "readonly");
            // =========== FO ==================
            txtmembershiptotal.Attributes.Add("readonly", "readonly");
            txttotal.Attributes.Add("readonly", "readonly");
            txttotalfemale.Attributes.Add("readonly", "readonly");
            txttoalDues.Attributes.Add("readonly", "readonly");
            txtTotalFun.Attributes.Add("readonly", "readonly");
            txtFunTotal.Attributes.Add("readonly", "readonly");
            txtfemaletotal.Attributes.Add("readonly", "readonly");
            txttotalPourers.Attributes.Add("readonly", "readonly");
            txtAIcentertotal.Attributes.Add("readonly", "readonly");
            txtAItotalCow.Attributes.Add("readonly", "readonly");
            txtAItotalBuff.Attributes.Add("readonly", "readonly");
            txtAiperformedtotal.Attributes.Add("readonly", "readonly");
            txtcattleinductotal.Attributes.Add("readonly", "readonly");
            txttotalPAC.Attributes.Add("readonly", "readonly");
            txttotalcostAiactivites.Attributes.Add("readonly", "readonly");
            txtFPCtotalcost.Attributes.Add("readonly", "readonly");
            txtAHCtotalCost.Attributes.Add("readonly", "readonly");
            txtTotalCost.Attributes.Add("readonly", "readonly");
            txttotalcostOTI.Attributes.Add("readonly", "readonly");
            txtFOGrandTotal.Attributes.Add("readonly", "readonly");

            //============MPS ==================
            txttotalMilkProc.Attributes.Add("readonly", "readonly");
            txttotalmilksale.Attributes.Add("readonly", "readonly");
            txtTotalsmgsale_SMG.Attributes.Add("readonly", "readonly");
            txtTotalNMGsale_NMG.Attributes.Add("readonly", "readonly");
            txttotalBulkSale_OSALE.Attributes.Add("readonly", "readonly");
            //====== R ==========================
            txtTOTAL_forMilk.Attributes.Add("readonly", "readonly");
            txtTOTAL_forproduct.Attributes.Add("readonly", "readonly");
            txtRecombination.Attributes.Add("readonly", "readonly");
            //==== PCC ====================
            txtTotal_PC.Attributes.Add("readonly", "readonly");
            txtTotal_CC.Attributes.Add("readonly", "readonly");
            //============Marketing============
            txtTotal_MarkCostLM.Attributes.Add("readonly", "readonly");
            txtTotal_SMGNMG.Attributes.Add("readonly", "readonly");
            txtTotal_INDG.Attributes.Add("readonly", "readonly");
            txtTotalMKTG.Attributes.Add("readonly", "readonly");
            //====== Admnistrator ============================
            txtTotal_AD.Attributes.Add("readonly", "readonly");
            txtTotal_LI.Attributes.Add("readonly", "readonly");
            txtTotal_Provi.Attributes.Add("readonly", "readonly");
            txtFPTotal.Attributes.Add("readonly", "readonly");
            //=========== CAPACITY UTILISATION=================
            txtthroughpuINLTS_PC.Attributes.Add("readonly", "readonly");
            txtthroughputPERDAY_PC.Attributes.Add("readonly", "readonly");
            txtcapacityutilisationINKGS_PC.Attributes.Add("readonly", "readonly");
            txtcapacityuti_CC.Attributes.Add("readonly", "readonly");
            txtcapacityuti_CC.Attributes.Add("readonly", "readonly");
            txtcapacityuti_BMC.Attributes.Add("readonly", "readonly");
            //==============Receipts==================
            txtTotalSaleTR.Attributes.Add("readonly", "readonly");
            txtNetRecieptsTR.Attributes.Add("readonly", "readonly");
            txtopeningStocksTR.Attributes.Add("readonly", "readonly");
            //======= MATERIAL BALANCING =========
            txtfatinkgs_MG.Attributes.Add("readonly", "readonly");
            txtsngfinkgs_MG.Attributes.Add("readonly", "readonly");
            txtfatpercentage_MG.Attributes.Add("readonly", "readonly");
            txtsnfpercentage_MG.Attributes.Add("readonly", "readonly");
            txtdcsmilktotal_MP.Attributes.Add("readonly", "readonly");
            txtfatpercent_MP.Attributes.Add("readonly", "readonly");
            txtsnfpercent_MP.Attributes.Add("readonly", "readonly");
            //===============================RAW MATERIAL COST=================
            txtTotalRMC.Attributes.Add("readonly", "readonly");
            txtTotalPT.Attributes.Add("readonly", "readonly");
            FillYear();
            openingForm();
        }


    }

    protected void FillYear()
    {
        try
        {
            for (int i = 2019; i <= DateTime.Now.Year; i++)
            {
                ddlYear.Items.Add(i.ToString());
                ddlYear.DataValueField = "2019";
                ddlYear.DataTextField = "2019";
                ddlYear.DataBind();
            }
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }
    #region openingForm
    protected void openingForm()
    {
        FO.Visible = false;
        MOP.Visible = false;
        PMandSALE.Visible = false;
        recombination.Visible = false;
        PPMaking.Visible = false;
        PackagingAndCC.Visible = false;
        Marketing.Visible = false;
        Administration.Visible = false;
        Reciepts.Visible = false;
        CapUtilisation.Visible = false;
        materialbalancing.Visible = false;
        RawMaterialCost.Visible = false;


        if (ddlmonth.SelectedValue != "0" && ddlYear.SelectedValue != "0" && ddlform.SelectedValue != "0")
        {

            //ds = objdb.ByProcedure("USP_GetServerDatetime",
            //       new string[] { "flag" },
            //         new string[] { "1" }, "dataset");
            //if (ds.Tables[0].Rows.Count > 0)
            //{

            //    string myStringfromdat = "01/" + ddlmonth.SelectedValue + "/" + ddlYear.SelectedValue;
            //    string myStringcurrentdate = ds.Tables[0].Rows[0]["currentDate"].ToString();
            //    DateTime Prevcurrentdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    DateTime date = DateTime.ParseExact(myStringfromdat, "01/" + ddlmonth.SelectedValue + "/" + ddlYear.SelectedValue, CultureInfo.CurrentCulture);
            //    DateTime d1 = date.AddMonths(-1);

            //    if (Prevcurrentdate.Month == d1.Month || Prevcurrentdate.Month == date.Month)
            //    {
            //        btnAdministration.Visible = false;
            //    }
            //    else
            //    {
            //        btnAdministration.Visible = true;
            //    }
            //}
            if (ddlform.SelectedValue == "1")
            { //raghav
                #region FO.Visible
                try
                {
                    clearfo();
                    ds.Clear();
                    FO.Visible = true;
                    if (ddlmonth.SelectedValue != "0" && ddlYear.SelectedValue != "0")
                    {
                        ds = objdb.ByProcedure("SP_MonthlyCostingFarmerOrgnization",
                           new string[] { "Flag", "Office_ID", "EntryYear", "EntryMonth" },
                           new string[] { "2", objdb.Office_ID(), ddlYear.SelectedValue, ddlmonth.SelectedValue }, "dataset");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            { //tblFO_AHCCost
                                txtAHCsalaryandwages.Text = ds.Tables[0].Rows[0]["AHCSalaryAndWages"].ToString();
                                txtAHCotherdirectcost.Text = ds.Tables[0].Rows[0]["AHCOtherDirectCost"].ToString();
                                txtAHCtotalCost.Text = ds.Tables[0].Rows[0]["AHCTotalCost"].ToString();
                                txtFPCsalryandwages.Text = ds.Tables[0].Rows[0]["FodderSalaryAndWages"].ToString();
                                txtFPCotherdirectcost.Text = ds.Tables[0].Rows[0]["FodderOtherDirectCost"].ToString();
                                txtFPCtotalcost.Text = ds.Tables[0].Rows[0]["FodderTotalCost"].ToString();
                            }
                            if (ds != null && ds.Tables[1].Rows.Count > 0)
                            { //tblFO_AIActivities
                                txtAIcentersingle.Text = ds.Tables[1].Rows[0]["NoOfAICenterSingle"].ToString();
                                txtAIcentercluster.Text = ds.Tables[1].Rows[0]["NoOfAICenterCluster"].ToString();
                                txtAIcentertotal.Text = ds.Tables[1].Rows[0]["NoOfAICenterTotal"].ToString();
                                txtAIperformSinglecow.Text = ds.Tables[1].Rows[0]["NoOfAIPerformedSingleCow"].ToString();
                                txtAIperformBuff1.Text = ds.Tables[1].Rows[0]["NoOfAIPerformedSingleBuff"].ToString();
                                txtAIperformclustercow.Text = ds.Tables[1].Rows[0]["NoOfAIPerformedClusterCow"].ToString();
                                txtAIPerformBuff2.Text = ds.Tables[1].Rows[0]["NoOfAIPerformedClusterBuff"].ToString();
                                txtAItotalCow.Text = ds.Tables[1].Rows[0]["NoOfAIPerformedTotalCow"].ToString();
                                txtAItotalBuff.Text = ds.Tables[1].Rows[0]["NoOfAIPerformedTotalBuff"].ToString();
                                txtAiperformedtotal.Text = ds.Tables[1].Rows[0]["TotalAIPerformedNo"].ToString();
                            }
                            if (ds != null && ds.Tables[2].Rows.Count > 0)
                            { //tblFO_CalvesReportedBorn.
                                txtcalvborntotalcow.Text = ds.Tables[2].Rows[0]["TotalCow"].ToString();
                                txtcalvbornbuff.Text = ds.Tables[2].Rows[0]["Buff"].ToString();
                                txtanimalhusfirstAid.Text = ds.Tables[2].Rows[0]["FirstAidCases"].ToString();
                                txtaniamlhusAHWcase.Text = ds.Tables[2].Rows[0]["AHW_Cases"].ToString();
                                txtcattlefiedsold.Text = ds.Tables[2].Rows[0]["CattleFieldSoldBy"].ToString();
                                txtdcsselingbcf.Text = ds.Tables[2].Rows[0]["NoOfDCSSellingDCSBCF"].ToString();
                                txtmmsalebydcs.Text = ds.Tables[2].Rows[0]["MMSaleByDCSToProdusers"].ToString();
                                txtcattinducproject.Text = ds.Tables[2].Rows[0]["CattleInductionProject"].ToString();
                                txtcattinducselffinance.Text = ds.Tables[2].Rows[0]["CattleInductionSelfFinance"].ToString();
                                txtcattleinductotal.Text = ds.Tables[2].Rows[0]["CattleInductionTotal"].ToString();
                            }
                            if (ds != null && ds.Tables[3].Rows.Count > 0)
                            {//tblFO_FarmerOrganizationAndMemberShip
                                txtNOofroutes.Text = ds.Tables[3].Rows[0]["NoOfFunctionalRoutes"].ToString();
                                txtdcsorgnised.Text = ds.Tables[3].Rows[0]["DCSOrganised"].ToString();
                                txtdcsfunctional.Text = ds.Tables[3].Rows[0]["DCSFunctional"].ToString();
                                txtdcsclosedtemp.Text = ds.Tables[3].Rows[0]["DCSClosedTemp"].ToString();
                                txtnewdcsorganisedmonth.Text = ds.Tables[3].Rows[0]["NewDCSOrganisedDuringTheMonth"].ToString();
                                txtnewdcsregisteredmonth.Text = ds.Tables[3].Rows[0]["NewDCSRegisteredDuringTheMonth"].ToString();
                                txtdcsrevivedmonth.Text = ds.Tables[3].Rows[0]["NoOfDCSRevivedDuringTheMonth"].ToString();
                                txtcolesdmonth.Text = ds.Tables[3].Rows[0]["NoOfDCSClosedDuringTheMonth"].ToString();
                            }
                            if (ds != null && ds.Tables[4].Rows.Count > 0)
                            {//tblFO_FemaleMembershipOrganisedDCS
                                txtfemalGeneral.Text = ds.Tables[4].Rows[0]["General"].ToString();
                                txtScheCastfemale.Text = ds.Tables[4].Rows[0]["ScheduleCaste"].ToString();
                                txtschedtribfemale.Text = ds.Tables[4].Rows[0]["ScheduleTribe"].ToString();
                                txtotherbackwordfemale.Text = ds.Tables[4].Rows[0]["OtherBackword"].ToString();
                                txttotalfemale.Text = ds.Tables[4].Rows[0]["GSSO_Total"].ToString();
                                txtoldDues.Text = ds.Tables[4].Rows[0]["OLD"].ToString();
                                txtcurrentDues.Text = ds.Tables[4].Rows[0]["Dues_Current"].ToString();
                                txttoalDues.Text = ds.Tables[4].Rows[0]["Total"].ToString();
                            }
                            if (ds != null && ds.Tables[5].Rows.Count > 0)
                            {//tblFO_ProcurementActivitiesCost
                                txtsalarywagesPAC.Text = ds.Tables[5].Rows[0]["SalaryAndWagesPAC"].ToString();
                                txtTaandtransportPAC.Text = ds.Tables[5].Rows[0]["TaAndTransportation"].ToString();
                                txtTaandtransportPAC.Text = ds.Tables[5].Rows[0]["TaAndTransportation"].ToString();
                                txtcontractlabourPAC.Text = ds.Tables[5].Rows[0]["ContractLaboure"].ToString();
                                txtotherexpansesPAC.Text = ds.Tables[5].Rows[0]["OtherExpanses"].ToString();
                                txttotalPAC.Text = ds.Tables[5].Rows[0]["Total"].ToString();
                                txtsalaryandwagesAiActivites.Text = ds.Tables[5].Rows[0]["SalaryAndWages"].ToString();
                                txttransportAiActivites.Text = ds.Tables[5].Rows[0]["Transportation"].ToString();
                                txtLn2ConsumedAiAcitivites.Text = ds.Tables[5].Rows[0]["CostOfLN2Consumed"].ToString();
                                txtLn2transportAiactivites.Text = ds.Tables[5].Rows[0]["CostOfLN2Transportation"].ToString();
                                txtsemenandstrawesAiactivites.Text = ds.Tables[5].Rows[0]["CostOfSemenAndStraws"].ToString();
                                txtotherdirectcostAiactivites.Text = ds.Tables[5].Rows[0]["OtherDirectCost"].ToString();
                                txtlessincomeAiactivites.Text = ds.Tables[5].Rows[0]["LessIncome"].ToString();
                                txttotalcostAiactivites.Text = ds.Tables[5].Rows[0]["TotalCost"].ToString();
                            }
                            if (ds != null && ds.Tables[6].Rows.Count > 0)
                            {//tblFO_TotalMembershipFunctionalDCS
                                txtgenralFun.Text = ds.Tables[6].Rows[0]["General"].ToString();
                                txtschedulcasteFun.Text = ds.Tables[6].Rows[0]["Schedule_Caste"].ToString();
                                txtscheduletribeFun.Text = ds.Tables[6].Rows[0]["Scheduled_Tribe"].ToString();
                                txtbackwordFun.Text = ds.Tables[6].Rows[0]["OtherBackwordClasses"].ToString();
                                txtTotalFun.Text = ds.Tables[6].Rows[0]["GSSO_Total"].ToString();
                                txtlandlesslaboueFun.Text = ds.Tables[6].Rows[0]["LandlessLaboures"].ToString();
                                txtmarinalfarmerFun.Text = ds.Tables[6].Rows[0]["MargionalFarmers"].ToString();
                                txtsmallfarmerFun.Text = ds.Tables[6].Rows[0]["SamllFarmers"].ToString();
                                txtlargefarmerFun.Text = ds.Tables[6].Rows[0]["LargeFarmers"].ToString();
                                txtOthersFun.Text = ds.Tables[6].Rows[0]["Others"].ToString();
                                txtFunTotal.Text = ds.Tables[6].Rows[0]["Total"].ToString();
                            }
                            if (ds != null && ds.Tables[7].Rows.Count > 0)
                            {//tblFO_FemaleMembershipFunctionalDCS
                                txtfemalegeneral.Text = ds.Tables[7].Rows[0]["General"].ToString();
                                txtfemaleschedulcaste.Text = ds.Tables[7].Rows[0]["ScheduleCaste"].ToString();
                                txtfemaletribe.Text = ds.Tables[7].Rows[0]["ScheduleTribe"].ToString();
                                txtfemalebackword.Text = ds.Tables[7].Rows[0]["OtherBackwordClass"].ToString();
                                txtfemaletotal.Text = ds.Tables[7].Rows[0]["Total"].ToString();
                                txtmembers.Text = ds.Tables[7].Rows[0]["MilkPourerMembers"].ToString();
                                txtnonmerbers.Text = ds.Tables[7].Rows[0]["MilkPourerNonMembers"].ToString();
                                txttotalPourers.Text = ds.Tables[7].Rows[0]["MilkPourerTotal"].ToString();
                            }
                            if (ds != null && ds.Tables[8].Rows.Count > 0)
                            {//tblFO_TotalMembershipOrganisedDCS
                                txtGeneral.Text = ds.Tables[8].Rows[0]["General"].ToString();
                                txtSceduledcaste.Text = ds.Tables[8].Rows[0]["Schedule_Caste"].ToString();
                                txtscheduletribe.Text = ds.Tables[8].Rows[0]["Scheduled_Tribe"].ToString();
                                txtbackworsclasses.Text = ds.Tables[8].Rows[0]["OtherBackwordClasses"].ToString();
                                txtmembershiptotal.Text = ds.Tables[8].Rows[0]["GSSO_Total"].ToString();
                                txtlandlesslabour.Text = ds.Tables[8].Rows[0]["LandlessLaboures"].ToString();
                                txtmarginalfarmer.Text = ds.Tables[8].Rows[0]["MargionalFarmers"].ToString();
                                txtsmallfarmer.Text = ds.Tables[8].Rows[0]["SamllFarmers"].ToString();
                                txtlargefarmer.Text = ds.Tables[8].Rows[0]["LargeFarmers"].ToString();
                                txtothers.Text = ds.Tables[8].Rows[0]["Others"].ToString();
                                txttotal.Text = ds.Tables[8].Rows[0]["LMSLO_Total"].ToString();
                            }
                            if (ds != null && ds.Tables[9].Rows.Count > 0)
                            {//tblFO_TrainingAndExtensionCost

                                txtTEcostsalaryandwages.Text = ds.Tables[9].Rows[0]["TEC_SalaryAndWages"].ToString();
                                txtTEcostotherdirectcost.Text = ds.Tables[9].Rows[0]["TEC_OtherDirectCost"].ToString();
                                txtTEcostlessincome.Text = ds.Tables[9].Rows[0]["TEC_LessIncome"].ToString();
                                txtTotalCost.Text = ds.Tables[9].Rows[0]["OI_TotalCost"].ToString();
                                txtsalaryandwagesOTI.Text = ds.Tables[9].Rows[0]["OI_SalaryAndWages"].ToString();
                                txtotherincmecostOTI.Text = ds.Tables[9].Rows[0]["OI_OtherDirectCost"].ToString();
                                txttotalcostOTI.Text = ds.Tables[9].Rows[0]["OIC_TotalCost"].ToString();
                                txtFOGrandTotal.Text = ds.Tables[9].Rows[0]["GrandtoatalPAC"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    ds.Clear();
                }
                #endregion
            }
            if (ddlform.SelectedValue == "2")
            {
                #region MOP.Visible
                MOP.Visible = true;
                clrTxt_MPS();
                ds.Clear();
                try
                {
                    if (ddlmonth.SelectedValue != "0" && ddlYear.SelectedValue != "0")
                    {
                        ds = objdb.ByProcedure("SP_MonthlyCosting_MilkProcurementSale",
                            new string[] { "Flag", "Office_ID", "Entry_Year", "Entry_Month" },
                            new string[] { "2", objdb.Office_ID(), ddlYear.SelectedValue, ddlmonth.SelectedValue }, "dataset");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                //GMPMS
                                txtMILKproKGPD_KG_Monthly.Text = ds.Tables[0].Rows[0]["MILKproKGPD_KG_Monthly"].ToString();
                                txtLocalMILKLPD_Ltr_Monthly.Text = ds.Tables[0].Rows[0]["LocalMILKLPD_Ltr_Monthly"].ToString();
                                txtMILKproKGPD_Monthly.Text = ds.Tables[0].Rows[0]["MP_InTheMonth"].ToString();
                                txtMILKprocKGPD_Cummulative.Text = ds.Tables[0].Rows[0]["MP_TillMonth"].ToString();
                                txtLocalMILKLPD_Monthly.Text = ds.Tables[0].Rows[0]["LMS_InTheMonth"].ToString();
                                txtLocalmilkLPD_Cummulative.Text = ds.Tables[0].Rows[0]["LMS_TillMonth"].ToString();
                            }
                            if (ds != null && ds.Tables[1].Rows.Count > 0)
                            {
                                //LMS
                                txtwholemilk.Text = ds.Tables[1].Rows[0]["WholeMilk"].ToString();
                                txtfullcreammilk.Text = ds.Tables[1].Rows[0]["FullCreamMilk"].ToString();
                                txtstdmilk.Text = ds.Tables[1].Rows[0]["STDMilk"].ToString();
                                txttonedmilk.Text = ds.Tables[1].Rows[0]["TonedMilk"].ToString();
                                txtdtmilk.Text = ds.Tables[1].Rows[0]["DTMilk"].ToString();
                                txtskimmilk.Text = ds.Tables[1].Rows[0]["SkimMilk"].ToString();
                                txtrawchilldmilk.Text = ds.Tables[1].Rows[0]["RawChilldMilk"].ToString();
                                txtchaispecimilk.Text = ds.Tables[1].Rows[0]["ChaiSpecialMilk"].ToString();
                                txtcowmilk.Text = ds.Tables[1].Rows[0]["CowMilk"].ToString();
                                txtsanchilitemilk.Text = ds.Tables[1].Rows[0]["SanchiLiteMilk"].ToString();
                                txtchahamilk.Text = ds.Tables[1].Rows[0]["ChahaMilk"].ToString();
                                txttotalmilksale.Text = ds.Tables[1].Rows[0]["TotalMilkSale"].ToString();

                            }
                            if (ds != null && ds.Tables[2].Rows.Count > 0)
                            {
                                // MP 
                                txtDCSmilkRMRD.Text = ds.Tables[2].Rows[0]["DCSMilkRMRD"].ToString();
                                txtDCSmilkCCS.Text = ds.Tables[2].Rows[0]["DCSMilkCCs"].ToString();
                                txtSMGMILK.Text = ds.Tables[2].Rows[0]["SMGMilk"].ToString();
                                txtNMGmilk.Text = ds.Tables[2].Rows[0]["NMGMilk"].ToString();
                                txtOTHER.Text = ds.Tables[2].Rows[0]["Other"].ToString();
                                txttotalMilkProc.Text = ds.Tables[2].Rows[0]["TotalMilkProc"].ToString();
                            }
                            if (ds != null && ds.Tables[3].Rows.Count > 0)
                            {
                                //MPKGPD
                                txtMILKPROC_monthly.Text = ds.Tables[3].Rows[0]["MP_InTheMonth"].ToString();
                                txtMILKPROC_Cummulat.Text = ds.Tables[3].Rows[0]["MP_TillMonth"].ToString();
                                txtLocalMILK_MOnthly.Text = ds.Tables[3].Rows[0]["LMS_InTheMonth"].ToString();
                                txtLocalMilk_Cummulat.Text = ds.Tables[3].Rows[0]["LMS_TillMonth"].ToString();
                                txtTotalMilkSale_Monthly.Text = ds.Tables[3].Rows[0]["TMS_InTheMonth"].ToString();
                                txtTotalMilkSale_Cummulat.Text = ds.Tables[3].Rows[0]["TMS_TillMonth"].ToString();
                                txtSMGmilk_Monthly.Text = ds.Tables[3].Rows[0]["SMS_InTheMonth"].ToString();
                                txtSMGmilk_Cummulat.Text = ds.Tables[3].Rows[0]["SMS_TillMonth"].ToString();
                                txtNMGOTH_MOnthly.Text = ds.Tables[3].Rows[0]["NOS_InTheMonth"].ToString();
                                txtNMGOTH_Cummulat.Text = ds.Tables[3].Rows[0]["NOS_TillMonth"].ToString();
                            }
                            if (ds != null && ds.Tables[4].Rows.Count > 0)
                            {
                                //SMS
                                txtwholemilk_SMG.Text = ds.Tables[4].Rows[0]["SMS_WholeMilk"].ToString();
                                txtskimmilk_SMG.Text = ds.Tables[4].Rows[0]["SMS_SkimMilk"].ToString();
                                txtOther_SMG.Text = ds.Tables[4].Rows[0]["SMS_Other"].ToString();
                                txtTotalsmgsale_SMG.Text = ds.Tables[4].Rows[0]["SMS_TotalSMGSale"].ToString();
                                txtwholemilk_NMG.Text = ds.Tables[4].Rows[0]["NMS_WholeMilk"].ToString();
                                txtskimmilk_NMG.Text = ds.Tables[4].Rows[0]["NMS_SkimMilk"].ToString();
                                txtOther_NMG.Text = ds.Tables[4].Rows[0]["NMS_Other"].ToString();
                                txtTotalNMGsale_NMG.Text = ds.Tables[4].Rows[0]["NMS_TotalNMGSale"].ToString();
                                txtwholmilkinLit_OSALE.Text = ds.Tables[4].Rows[0]["OS_WholeMilkInLit"].ToString();
                                txtskimmilkinLit_OSALE.Text = ds.Tables[4].Rows[0]["OS_SkimMilkInLit"].ToString();
                                txtOther_OSALE.Text = ds.Tables[4].Rows[0]["OS_Other"].ToString();
                                txttotalBulkSale_OSALE.Text = ds.Tables[4].Rows[0]["OS_TotalBULKSale"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
                }
                finally
                {
                    ds.Clear();
                }
                #endregion
            }
            if (ddlform.SelectedValue == "3")
            {
                #region PMandSALE.Visible
                PMandSALE.Visible = true;
                clrtxtPMandSale();
                ds.Clear();
                try
                {
                    if (ddlmonth.SelectedValue != "0" && ddlYear.SelectedValue != "0")
                    {
                        ds = objdb.ByProcedure("SP_MonthlyCosting_ProductsManufacturingSale",
                                new string[] { "Flag", "Office_ID", "Entry_Year", "Entry_Month" },
                                new string[] { "2", objdb.Office_ID(), ddlYear.SelectedValue, ddlmonth.SelectedValue }, "dataset");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                // Ghee
                                txtGhee.Text = ds.Tables[0].Rows[0]["PM_Ghee"].ToString();
                                txtSkimmilkpowder.Text = ds.Tables[0].Rows[0]["PM_SkimMilkPowder"].ToString();
                                txttablebutter.Text = ds.Tables[0].Rows[0]["PM_TableButter"].ToString();
                                txtwhitebutter.Text = ds.Tables[0].Rows[0]["PM_WhiteButter"].ToString();
                                txtWMP.Text = ds.Tables[0].Rows[0]["PM_WMP"].ToString();
                                txtpaneer.Text = ds.Tables[0].Rows[0]["Paneer"].ToString();
                                txtSanchi_GHEESale.Text = ds.Tables[0].Rows[0]["GS_Sanchi"].ToString();
                                txtSneha_GHEESale.Text = ds.Tables[0].Rows[0]["GS_Sneha"].ToString();
                                txtOther_GHEESale.Text = ds.Tables[0].Rows[0]["GS_Other"].ToString();
                                txtTotal_GHEESale.Text = ds.Tables[0].Rows[0]["GS_TotalSale"].ToString();
                                txtskimmilk_Prodctsale.Text = ds.Tables[0].Rows[0]["PS_SkimMilkPowder"].ToString();
                                txttablebutter_Prodctsale.Text = ds.Tables[0].Rows[0]["PS_TableButter"].ToString();
                                txtwhitebutter_Prodctsale.Text = ds.Tables[0].Rows[0]["PS_WhiteButter"].ToString();
                                txtshrikhand_Prodctsale.Text = ds.Tables[0].Rows[0]["PS_ShriKhand"].ToString();
                            }
                            if (ds != null && ds.Tables[1].Rows.Count > 0)
                            {
                                // Mava
                                txtmawa_PMAS.Text = ds.Tables[1].Rows[0]["Mawa"].ToString();
                                txtdrycasein_PMAS.Text = ds.Tables[1].Rows[0]["DryCasein"].ToString();
                                txtcookingbutter_PMAS.Text = ds.Tables[1].Rows[0]["CookingButter"].ToString();
                                txtgulabjamun_PMAS.Text = ds.Tables[1].Rows[0]["GulabJamun"].ToString();
                                txtrasgulla_PMAS.Text = ds.Tables[1].Rows[0]["RashGulla"].ToString();
                                txtmawagulabjanum_PMAS.Text = ds.Tables[1].Rows[0]["MawaGulabJamun"].ToString();
                                txtmilkcake_PMAS.Text = ds.Tables[1].Rows[0]["MilkCakeSanchi"].ToString();
                                txtThandai_PMAS.Text = ds.Tables[1].Rows[0]["Thandai"].ToString();
                                txtMDm_PMAS.Text = ds.Tables[1].Rows[0]["MdmSweetenSmp"].ToString();
                                txtlightlassi_PMAS.Text = ds.Tables[1].Rows[0]["LightLassi"].ToString();
                                txtpudinaraita_PMAS.Text = ds.Tables[1].Rows[0]["PudinaRaita"].ToString();
                                txtWMP_PMAS.Text = ds.Tables[1].Rows[0]["WMP"].ToString();
                                txtpannerachar_PMAS.Text = ds.Tables[1].Rows[0]["PaneerAchar"].ToString();
                            }
                            if (ds != null && ds.Tables[2].Rows.Count > 0)
                            {
                                // Paneer
                                txtPAneer_PMAS.Text = ds.Tables[2].Rows[0]["paneer"].ToString();
                                txtflavmilk_PMAS.Text = ds.Tables[2].Rows[0]["FlavMilkInLit"].ToString();
                                txtBtrmilk_PMAS.Text = ds.Tables[2].Rows[0]["BtrMilkInLit"].ToString();
                                txtSweetcurd_PMAS.Text = ds.Tables[2].Rows[0]["SweetCurd"].ToString();
                                txtpeda_PMAS.Text = ds.Tables[2].Rows[0]["Peda"].ToString();
                                txtplaincurd_PMAS.Text = ds.Tables[2].Rows[0]["PlainCurd"].ToString();
                                txtorangsip_PMAS.Text = ds.Tables[2].Rows[0]["OrangeSip"].ToString();
                                txtprobioticcurd_PMAS.Text = ds.Tables[2].Rows[0]["ProBioticCurd"].ToString();
                                txtwholemilk_PMAS.Text = ds.Tables[2].Rows[0]["WholeMilk"].ToString();
                                txtChenarabdi_PMAS.Text = ds.Tables[2].Rows[0]["ChenanRabdi"].ToString();
                                txtpresscurd_PMAS.Text = ds.Tables[2].Rows[0]["PressCurd"].ToString();
                                txtcream_PMAS.Text = ds.Tables[2].Rows[0]["Cream"].ToString();
                                txtlassi_PMAS.Text = ds.Tables[2].Rows[0]["Lassi"].ToString();
                                txtamarkhand_PMAS.Text = ds.Tables[2].Rows[0]["Amrakhand"].ToString();
                                txtsmp_PMAS.Text = ds.Tables[2].Rows[0]["SMPCSP"].ToString();
                            }
                            if (ds != null && ds.Tables[3].Rows.Count > 0)
                            {
                                // SLM
                                txtsanchilitemilk_PMAS.Text = ds.Tables[3].Rows[0]["SanchiLiteMilk"].ToString();
                                txtnariyalbarfi_PMAS.Text = ds.Tables[3].Rows[0]["NariyalBarfi"].ToString();
                                txtgulabjamunMix_PMAS.Text = ds.Tables[3].Rows[0]["GulabJamunMix"].ToString();
                                txtcoffemix_PMAS.Text = ds.Tables[3].Rows[0]["CoffeeMix"].ToString();
                                txtcookingbutter.Text = ds.Tables[3].Rows[0]["CookingButter"].ToString();
                                txtlowfatpanner_PMAS.Text = ds.Tables[3].Rows[0]["LowFatPaneer"].ToString();
                                txtwheydrink_PMAS.Text = ds.Tables[3].Rows[0]["WheyDrink"].ToString();
                                txtsanchitea_PMAS.Text = ds.Tables[3].Rows[0]["SanchiTeaMix"].ToString();
                                txtpedaprasadi_PMAS.Text = ds.Tables[3].Rows[0]["PedaPrasadi"].ToString();
                                txticecream_PMAS.Text = ds.Tables[3].Rows[0]["IceCream"].ToString();
                                txtgoldenmilk_PMAS.Text = ds.Tables[3].Rows[0]["GoldenMilk"].ToString();
                                txtsugarfreepeda_PMAS.Text = ds.Tables[3].Rows[0]["SugarFreePeda"].ToString();
                                txthealthvita_PMAS.Text = ds.Tables[3].Rows[0]["HealthVitaPlus"].ToString();

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
                }
                finally
                {
                    ds.Clear();
                }
                #endregion
            }
            if (ddlform.SelectedValue == "4")
            {
                #region recombination.Visible
                recombination.Visible = true;
                clrTxt_R();
                ds.Clear();
                try
                {
                    if (ddlmonth.SelectedValue != "0" && ddlYear.SelectedValue != "0")
                    {
                        //R
                        ds = objdb.ByProcedure("SP_MonthlyCosting_Recombination",
                                new string[] { "Flag", "Office_ID", "Entry_Year", "Entry_Month" },
                                new string[] { "2", objdb.Office_ID(), ddlYear.SelectedValue, ddlmonth.SelectedValue }, "dataset");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                txtSMP_forMilk.Text = ds.Tables[0].Rows[0]["FM_Smp"].ToString();
                                txtWB_forMilk.Text = ds.Tables[0].Rows[0]["FM_Wb"].ToString();
                                txtGHEE_forMilk.Text = ds.Tables[0].Rows[0]["FM_Ghee"].ToString();
                                txtWMP_forMilk.Text = ds.Tables[0].Rows[0]["FM_Wmp"].ToString();
                                txtPANEER_forMilk.Text = ds.Tables[0].Rows[0]["FM_Paneer"].ToString();
                                txtTOTAL_forMilk.Text = ds.Tables[0].Rows[0]["FM_Total"].ToString();
                                txtSMP_forproduct.Text = ds.Tables[0].Rows[0]["FP_Smp"].ToString();
                                txtWB_forproduct.Text = ds.Tables[0].Rows[0]["FP_Wb"].ToString();
                                txtGHEE_forproduct.Text = ds.Tables[0].Rows[0]["FP_Ghee"].ToString();
                                txtWMP_forproduct.Text = ds.Tables[0].Rows[0]["FP_Wmp"].ToString();
                                txtPANEER_forproduct.Text = ds.Tables[0].Rows[0]["FP_Paneer"].ToString();
                                txtTOTAL_forproduct.Text = ds.Tables[0].Rows[0]["FP_Total"].ToString();
                                txtformilk_Commused.Text = ds.Tables[0].Rows[0]["ForMilkRs"].ToString();
                                txtforproduct_Commused.Text = ds.Tables[0].Rows[0]["ForProductRs"].ToString();
                                txtRecombination.Text = ds.Tables[0].Rows[0]["TotalRecombinationReconstitution"].ToString();
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
                }
                finally
                {
                    ds.Clear();
                }
                #endregion
            }

            if (ddlform.SelectedValue == "5")
            {
                #region PPMaking.Visible
                try
                {
                    ds.Clear();
                    clearppm();
                    PPMaking.Visible = true;
                    if (ddlmonth.SelectedValue != "0" && ddlYear.SelectedValue != "0")
                    {
                        ds = objdb.ByProcedure("SP_MonthlyCosting_PROCESSING_PRODUCTS_MAKING",
                            new string[] { "flag", "Office_ID", "Entry_Year", "Entry_Month" },
                            new string[] { "1", objdb.Office_ID(), ddlYear.SelectedValue, ddlmonth.SelectedValue }, "dataset");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                txtsalaryallow_CA.Text = ds.Tables[0].Rows[0]["SalaryAndAllow"].ToString();
                                txtContlaboures_CA.Text = ds.Tables[0].Rows[0]["ContLaboures"].ToString();
                                txtConsumblecommon_CA.Text = ds.Tables[0].Rows[0]["ConsumableCommon"].ToString();
                                txtConsumbleDirect_CA.Text = ds.Tables[0].Rows[0]["ConsumableDirect"].ToString();
                                txtchemicaldetergent_CA.Text = ds.Tables[0].Rows[0]["ChemicalAndDetergent"].ToString();
                                txtElectricity_CA.Text = ds.Tables[0].Rows[0]["Electricity"].ToString();
                                txtWater_CA.Text = ds.Tables[0].Rows[0]["Water"].ToString();
                                txtFurnanceoil_CA.Text = ds.Tables[0].Rows[0]["FurnanceOilGas"].ToString();
                                txtRepairMaint_CA.Text = ds.Tables[0].Rows[0]["RepairAndMaintenance"].ToString();
                                txtOtherExps_CA.Text = ds.Tables[0].Rows[0]["OtherExps"].ToString();
                                txtTOTal_CA.Text = ds.Tables[0].Rows[0]["Total"].ToString();
                                txtMilkProssCA.Text = ds.Tables[0].Rows[0]["MilkProcessingCostRS"].ToString();
                                txtMilkPrepackCA.Text = ds.Tables[0].Rows[0]["MilkPrepackCostRS"].ToString();
                                txtPPMGrandTotal.Text = ds.Tables[0].Rows[0]["Grandtotal"].ToString();
                            }
                            if (ds != null && ds.Tables[1].Rows.Count > 0)
                            {
                                txtsalaryandallow_FPCD.Text = ds.Tables[1].Rows[0]["SalaryAndAllow"].ToString();
                                txtContlaboures_FPCD.Text = ds.Tables[1].Rows[0]["ContLaboures"].ToString();
                                txtCosumbleCommon_FPCD.Text = ds.Tables[1].Rows[0]["ConsumableCommon"].ToString();
                                txtConsumbledirect_FPCD.Text = ds.Tables[1].Rows[0]["ConsumableDirect"].ToString();
                                txtchemicaldetergent_FPCD.Text = ds.Tables[1].Rows[0]["ChemicalAndDetergent"].ToString();
                                txtElectricity_FPCD.Text = ds.Tables[1].Rows[0]["Electricity"].ToString();
                                txtWater_FPCD.Text = ds.Tables[1].Rows[0]["Water"].ToString();
                                txtfurnanceoil_FPCD.Text = ds.Tables[1].Rows[0]["FurnanceOilGas"].ToString();
                                txtrepairmaintance_FPCD.Text = ds.Tables[1].Rows[0]["RepairAndMaintenance"].ToString();
                                txtOtherExps_FPCD.Text = ds.Tables[1].Rows[0]["OtherExps"].ToString();
                                txtTotal_FPCD.Text = ds.Tables[1].Rows[0]["Total"].ToString();
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    ds.Clear();
                }
                #endregion

            }
            if (ddlform.SelectedValue == "6")
            {
                #region PackagingAndCC.Visible
                PackagingAndCC.Visible = true;
                clrTxtPCC();
                ds.Clear();
                try
                {
                    if (ddlmonth.SelectedValue != "0" && ddlYear.SelectedValue != "0")
                    {
                        //R
                        ds = objdb.ByProcedure("SP_MonthlyCosting_PackagingAndCC",
                                new string[] { "Flag", "Office_ID", "Entry_Year", "Entry_Month" },
                                new string[] { "2", objdb.Office_ID(), ddlYear.SelectedValue, ddlmonth.SelectedValue }, "dataset");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                txtmilCC_PC.Text = ds.Tables[0].Rows[0]["PC_MilkAtCC"].ToString();
                                txtMilkDairy_PC.Text = ds.Tables[0].Rows[0]["PC_MilkAtDairy"].ToString();
                                txtmainproduct_PC.Text = ds.Tables[0].Rows[0]["PC_MainProducts"].ToString();
                                txtINDGproduct_PC.Text = ds.Tables[0].Rows[0]["PC_INDG_Products"].ToString();
                                txtTotal_PC.Text = ds.Tables[0].Rows[0]["PC_Total"].ToString();
                                txtsalaryallow_CC.Text = ds.Tables[0].Rows[0]["CC_SalaryAndAllow"].ToString();
                                txtContlaboure_CC.Text = ds.Tables[0].Rows[0]["CC_ContLaboures"].ToString();
                                txtElectricity_CC.Text = ds.Tables[0].Rows[0]["CC_Electricity"].ToString();
                                txtCoalDiesel_CC.Text = ds.Tables[0].Rows[0]["CC_CoalFoAndDiesel"].ToString();
                                txtConsumble_CC.Text = ds.Tables[0].Rows[0]["CC_Cosumable"].ToString();
                                txtChemandDeter_CC.Text = ds.Tables[0].Rows[0]["CC_ChemistryAndDetergent"].ToString();
                                txtRepairMAint_CC.Text = ds.Tables[0].Rows[0]["CC_RepairAndMaint"].ToString();
                                txtBMC_CC.Text = ds.Tables[0].Rows[0]["CC_BMC"].ToString();
                                txtSecurity_CC.Text = ds.Tables[0].Rows[0]["CC_Security"].ToString();
                                txtOtherExps_CC.Text = ds.Tables[0].Rows[0]["CC_OtherExps"].ToString();
                                txtTotal_CC.Text = ds.Tables[0].Rows[0]["CC_Total"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
                }
                finally
                {
                    ds.Clear();
                }
                #endregion
            }
            if (ddlform.SelectedValue == "7")
            { //raghav 04-05-22
                #region Marketing.Visible
                try
                {
                    Marketing.Visible = true;
                    ds.Clear();
                    MarkClear();
                    if (ddlmonth.SelectedValue != "0" && ddlYear.SelectedValue != "0")
                    {
                        ds = objdb.ByProcedure("SP_MonthlyCostingFINANCIAL_PERFORMANCE",
                            new string[] { "flag", "Office_ID", "Entry_Year", "Entry_Month" },
                            new string[] { "1", objdb.Office_ID(), ddlYear.SelectedValue, ddlmonth.SelectedValue }, "dataset");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                txtsalaryAllow_MarkCostLM.Text = ds.Tables[0].Rows[0]["SalaryAndAllow"].ToString();
                                txttranportation_MarkCostLM.Text = ds.Tables[0].Rows[0]["Transportation"].ToString();
                                txtsalesprom_MarkCostLM.Text = ds.Tables[0].Rows[0]["SalesPromotion"].ToString();
                                txtservicecharg_MarkCostLM.Text = ds.Tables[0].Rows[0]["MPCDFServiceCharge"].ToString();
                                txtAdvertise_MarkCostLM.Text = ds.Tables[0].Rows[0]["AdvertismentAndOhter"].ToString();
                                txtAdvanceCard_MarkCostLM.Text = ds.Tables[0].Rows[0]["AdvanceCard"].ToString();
                                txtContractlabour_MarkCostLM.Text = ds.Tables[0].Rows[0]["ContractLabour"].ToString();
                                txtOthers_MarkCostLM.Text = ds.Tables[0].Rows[0]["Others"].ToString();
                                txtTotal_MarkCostLM.Text = ds.Tables[0].Rows[0]["Total"].ToString();
                            }
                            if (ds != null && ds.Tables[1].Rows.Count > 0)
                            {
                                txtsalryAllow_SMGNMG.Text = ds.Tables[1].Rows[0]["SMG_Milk_SalaryAndAllow"].ToString();
                                txtTransport_SMGNMG.Text = ds.Tables[1].Rows[0]["SMG_Milk_Transportation"].ToString();
                                txtServicCharg_SMGNMG.Text = ds.Tables[1].Rows[0]["SMG_Milk_ServiceCharge"].ToString();
                                txtOthers_SMGNMG.Text = ds.Tables[1].Rows[0]["SMG_Milk_Ohters"].ToString();
                                txtTotal_SMGNMG.Text = ds.Tables[1].Rows[0]["SMG_Milk_Total"].ToString();
                                txtsalryAndAllow_INDG.Text = ds.Tables[1].Rows[0]["MAIN_Prod_SalaryAndAllow"].ToString();
                                txtTransport_INDG.Text = ds.Tables[1].Rows[0]["MAIN_Prod_Transportation"].ToString();
                                txtServicCharg_INDG.Text = ds.Tables[1].Rows[0]["MAIN_Prod_ServiceCharge"].ToString();
                                txtOthersTax_INDG.Text = ds.Tables[1].Rows[0]["MAIN_Prod_OhterTax"].ToString();
                                txtTotal_INDG.Text = ds.Tables[1].Rows[0]["MAIN_Prod_Total"].ToString();
                                txtTotalMKTG.Text = ds.Tables[1].Rows[0]["Total_MKTG_Cost"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
                }
                finally
                {
                    ds.Clear();
                }
                #endregion
            }
            if (ddlform.SelectedValue == "8")
            {
                #region Administration.Visible
                Administration.Visible = true;
                clrTxtADM();
                ds.Clear();
                try
                {
                    if (ddlmonth.SelectedValue != "0" && ddlYear.SelectedValue != "0")
                    {
                        //R
                        ds = objdb.ByProcedure("SP_MonthlyCosting_Administration",
                                new string[] { "Flag", "Office_ID", "Entry_Year", "Entry_Month" },
                                new string[] { "2", objdb.Office_ID(), ddlYear.SelectedValue, ddlmonth.SelectedValue }, "dataset");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                txtsalaryAndAllow_AD.Text = ds.Tables[0].Rows[0]["SalaryAndAllow"].ToString();
                                txtMedicalTA_AD.Text = ds.Tables[0].Rows[0]["Medical_TA"].ToString();
                                txtConveyence_AD.Text = ds.Tables[0].Rows[0]["Conveyance"].ToString();
                                txtSecurity_AD.Text = ds.Tables[0].Rows[0]["ADM_Security"].ToString();
                                txtSupervisionVehic_AD.Text = ds.Tables[0].Rows[0]["SupervisionVehicles"].ToString();
                                txtContractLabour_AD.Text = ds.Tables[0].Rows[0]["ContractLabour"].ToString();
                                txtInsuranceOTH_AD.Text = ds.Tables[0].Rows[0]["InsuranceAndOTH_Taxes"].ToString();
                                txtLegalAuditFee_AD.Text = ds.Tables[0].Rows[0]["LegalAuditFees"].ToString();
                                txtStationary_AD.Text = ds.Tables[0].Rows[0]["Stationery"].ToString();
                                txtOther_AD.Text = ds.Tables[0].Rows[0]["Others"].ToString();
                                txtTotal_AD.Text = ds.Tables[0].Rows[0]["Total"].ToString();
                            }
                            if (ds != null && ds.Tables[1].Rows.Count > 0)
                            {
                                txtBonus_Provi.Text = ds.Tables[1].Rows[0]["Bonus"].ToString();
                                txtAuditFees_Provi.Text = ds.Tables[1].Rows[0]["AuditFees"].ToString();
                                txtGroupGratutiy_Provi.Text = ds.Tables[1].Rows[0]["GroupGratuity"].ToString();
                                txtLiveires_Provi.Text = ds.Tables[1].Rows[0]["Liveries"].ToString();
                                txtLeavesalary_Provi.Text = ds.Tables[1].Rows[0]["LeaveSalaryOfRetiredEmployees"].ToString();
                                txtotherExps_Provi.Text = ds.Tables[1].Rows[0]["OtherExps"].ToString();
                                txtTotal_Provi.Text = ds.Tables[1].Rows[0]["Total"].ToString();
                                txtNDDB_LI.Text = ds.Tables[1].Rows[0]["LI_NDDB"].ToString();
                                txtBANKLoan_LI.Text = ds.Tables[1].Rows[0]["LI_BankLoan"].ToString();
                                txtTotal_LI.Text = ds.Tables[1].Rows[0]["LI_Total"].ToString();
                                txtDepreciation.Text = ds.Tables[1].Rows[0]["depreciation"].ToString();
                            }
                            if (ds != null && ds.Tables[2].Rows.Count > 0)
                            {
                                txtProductMaking.Text = ds.Tables[2].Rows[0]["ProductMaking"].ToString();
                                txtConversionCharge.Text = ds.Tables[2].Rows[0]["ConversionCharge"].ToString();
                                txtFPOther.Text = ds.Tables[2].Rows[0]["Other"].ToString();
                                txtFPTotal.Text = ds.Tables[2].Rows[0]["Total"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
                }
                finally
                {
                    ds.Clear();
                }
                #endregion
            }
            if (ddlform.SelectedValue == "9")
            {
                #region Reciepts.Visible
                try
                {
                    ds.Clear();
                    FPClear();
                    Reciepts.Visible = true;
                    if (ddlmonth.SelectedValue != "0" && ddlYear.SelectedValue != "0")
                    {
                        ds = objdb.ByProcedure("SP_MonthlyCosting_FINANCIAL_PERFORMANCE_rs",
                            new string[] { "flag", "Office_ID", "Entry_Year", "Entry_Month" },
                            new string[] { "1", objdb.Office_ID(), ddlYear.SelectedValue, ddlmonth.SelectedValue }, "dataset");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                txtmilksaleTR.Text = ds.Tables[0].Rows[0]["MilkSale"].ToString();
                                txtProductsTR.Text = ds.Tables[0].Rows[0]["Products"].ToString();
                                txtTotalSaleTR.Text = ds.Tables[0].Rows[0]["TotalSale"].ToString();
                                txtCommoditiesTR.Text = ds.Tables[0].Rows[0]["TotalCommoditiesUsed"].ToString();
                                txtcommditiesPurchTR.Text = ds.Tables[0].Rows[0]["CommoditiesPurchReturns"].ToString();

                                txtClosingStocksTR.Text = ds.Tables[0].Rows[0]["ClosingStocks"].ToString();
                                txtOtherIncomeTR.Text = ds.Tables[0].Rows[0]["OtherIncome"].ToString();
                                txtNetRecieptsTR.Text = ds.Tables[0].Rows[0]["TotalNetReceipts"].ToString();
                                txtbeforIDATR.Text = ds.Tables[0].Rows[0]["SD_BeforeIdaOff"].ToString();
                                txtbeforDEFERDTR.Text = ds.Tables[0].Rows[0]["SD_BeforeDeffered"].ToString();
                                txtbeforDEPRECIATIONTR.Text = ds.Tables[0].Rows[0]["SD_BeforeDepreciation"].ToString();
                                txtNETINCLUDEPTR.Text = ds.Tables[0].Rows[0]["SD_NetIncluDep"].ToString();
                            }
                            if (ds != null && ds.Tables[2].Rows.Count > 0)
                            {
                                txtopeningStocksTR.Text = ds.Tables[2].Rows[0]["lstopen"].ToString();
                            }
                            if (ds != null && ds.Tables[1].Rows.Count > 0)
                            {
                                txttotalvarriCostTVC.Text = ds.Tables[1].Rows[0]["TotalVarriCost"].ToString();
                                txtsalaryWages_TFCOSTTVC.Text = ds.Tables[1].Rows[0]["TFC_SalaryAndWadges"].ToString();
                                txtOthers_TFCOSTTVC.Text = ds.Tables[1].Rows[0]["TFC_Other"].ToString();
                                txttotalfixCostTVC.Text = ds.Tables[1].Rows[0]["TotalFixedCost"].ToString();
                                txtToCostTVC.Text = ds.Tables[1].Rows[0]["TotalCost"].ToString();
                                txtTFcostEXCLINTTTVC.Text = ds.Tables[1].Rows[0]["TotalFixedCostExcludingINTT"].ToString();
                                txtToSaleTVC.Text = ds.Tables[1].Rows[0]["TotalSale"].ToString();
                                txtIDAOpertingProfTVC.Text = ds.Tables[1].Rows[0]["BeforeIDAOfOperatingProfit"].ToString();
                                txtNEtIncluIDTTVC.Text = ds.Tables[1].Rows[0]["NetIncudingIDT"].ToString();
                                txtToSaleWithCFFTVC.Text = ds.Tables[1].Rows[0]["TotalSaleTurnOverWithCFF"].ToString();
                                txtOPLOssProfitCFFTVC.Text = ds.Tables[1].Rows[0]["TotalOperatingLossProfitWithCFF"].ToString();
                                txtNETprofitLossTVC.Text = ds.Tables[1].Rows[0]["NetProfitLossWithCFF"].ToString();
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
                }
                finally
                {
                    ds.Clear();
                }
                #endregion
            }
            if (ddlform.SelectedValue == "10")
            {
                #region CapUtilisation.Visible
                CapUtilisation.Visible = true;
                clrTxtCU();
                ds.Clear();
                try
                {
                    if (ddlmonth.SelectedValue != "0" && ddlYear.SelectedValue != "0")
                    {
                        //R
                        ds = objdb.ByProcedure("SP_MonthlyCosting_CapacityUtilisation",
                                new string[] { "Flag", "Office_ID", "Entry_Year", "Entry_Month" },
                                new string[] { "2", objdb.Office_ID(), ddlYear.SelectedValue, ddlmonth.SelectedValue }, "dataset");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                txtthruoputwithoutWC_PC.Text = ds.Tables[0].Rows[0]["ThroughPutInKGWithoutWC"].ToString();
                                txtthroughpuINKGS_PC.Text = ds.Tables[0].Rows[0]["ThroughPutInKGs"].ToString();
                                txtthroughpuINLTS_PC.Text = ds.Tables[0].Rows[0]["ThroughPutInLTs"].ToString();
                                txtthroughputPERDAY_PC.Text = ds.Tables[0].Rows[0]["ThroughPutPerDay"].ToString();
                                txtcapacityutilisationINKGS_PC.Text = ds.Tables[0].Rows[0]["CapacityUtilisationIn_PerAGE"].ToString();
                                txtAllCCsthruoput_CC.Text = ds.Tables[0].Rows[0]["CC_ALLCCsThroughPut"].ToString();
                                txtcapacityuti_CC.Text = ds.Tables[0].Rows[0]["CC_CapacityUtilisationInPerAge"].ToString();
                                txtbcfsale_BMC.Text = ds.Tables[0].Rows[0]["BCFSaleCFF"].ToString();
                                txtbcfProdCFF_BMC.Text = ds.Tables[0].Rows[0]["BCFPRODCFF"].ToString();
                                txtcapacityuti_BMC.Text = ds.Tables[0].Rows[0]["BCF_CapacityUtilisationInPerAge"].ToString();
                                txtSMPProd_SMP.Text = ds.Tables[0].Rows[0]["SMPProdNInMTs"].ToString();
                                txtcapacityuti_SMP.Text = ds.Tables[0].Rows[0]["SMP_CapacityUtilisationInPerAge"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
                }
                finally
                {
                    ds.Clear();
                }
                #endregion
            }
            if (ddlform.SelectedValue == "11")
            {
                #region materialbalancing.Visible
                materialbalancing.Visible = true;
                clrTxtMB();
                ds.Clear();
                try
                {
                    if (ddlmonth.SelectedValue != "0" && ddlYear.SelectedValue != "0")
                    {
                        //R
                        ds = objdb.ByProcedure("SP_MonthlyCosting_MaterialBalancing",
                                new string[] { "Flag", "Office_ID", "Entry_Year", "Entry_Month" },
                                new string[] { "2", objdb.Office_ID(), ddlYear.SelectedValue, ddlmonth.SelectedValue }, "dataset");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                txtdcsmilkcow_MP.Text = ds.Tables[0].Rows[0]["MP_DCSmilkCow"].ToString();
                                txtdcsmilkbuff_MP.Text = ds.Tables[0].Rows[0]["MP_DCSmilkBuff"].ToString();
                                txtdcsmilktotal_MP.Text = ds.Tables[0].Rows[0]["MP_DCSmilkTotal"].ToString();
                                txtfat_MP.Text = ds.Tables[0].Rows[0]["MP_FAT"].ToString();
                                txtsnf_MP.Text = ds.Tables[0].Rows[0]["MP_SNF"].ToString();
                                txtfatpercent_MP.Text = ds.Tables[0].Rows[0]["MP_FATper"].ToString();
                                txtsnfpercent_MP.Text = ds.Tables[0].Rows[0]["MP_SNFper"].ToString();
                                txtquantityinkgs_TI.Text = ds.Tables[0].Rows[0]["MP_TI_QntInKG"].ToString();
                                txtfatinkgs_TI.Text = ds.Tables[0].Rows[0]["MP_TI_FATkg"].ToString();
                                txtsnfinkgs_TI.Text = ds.Tables[0].Rows[0]["MP_TI_SNFkg"].ToString();
                                txtvalueinrs_TI.Text = ds.Tables[0].Rows[0]["MP_TI_ValueinRS"].ToString();

                                txtquantityinkgs_TO.Text = ds.Tables[0].Rows[0]["TO_QntinKG"].ToString();
                                txtfatinkgs_TO.Text = ds.Tables[0].Rows[0]["TO_FatinKG"].ToString();
                                txtsnfinKgs_TO.Text = ds.Tables[0].Rows[0]["TO_SNFinKG"].ToString();
                                txtValueinrs_To.Text = ds.Tables[0].Rows[0]["TO_ValueinRS"].ToString();
                                txtfatinkgs_MG.Text = ds.Tables[0].Rows[0]["TO_MG_FatinKG"].ToString();
                                txtsngfinkgs_MG.Text = ds.Tables[0].Rows[0]["TO_MG_SNFinKG"].ToString();
                                txtfatpercentage_MG.Text = ds.Tables[0].Rows[0]["TO_MG_FATinPR"].ToString();
                                txtsnfpercentage_MG.Text = ds.Tables[0].Rows[0]["TO_MG_SNFinPR"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
                }
                finally
                {
                    ds.Clear();
                }
                #endregion
            }
            if (ddlform.SelectedValue == "12")
            {
                RawMaterialCost.Visible = true;
                Rawcostclr();
                ds.Clear();
                try
                {
                    if (ddlmonth.SelectedValue != "0" && ddlYear.SelectedValue != "0")
                    {
                        //R
                        ds = objdb.ByProcedure("SP_MonthlyCoasting_RawMaterialCost",
                                new string[] { "flag", "Office_ID", "Entry_Year", "Entry_Month" },
                                new string[] { "1", objdb.Office_ID(), ddlYear.SelectedValue, ddlmonth.SelectedValue }, "dataset");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                txtDCSMilkRMC.Text = ds.Tables[0].Rows[0]["DCSmilkRMC"].ToString();
                                txtSMGMilkRMC.Text = ds.Tables[0].Rows[0]["SMGMilkRMC"].ToString();
                                txtNMGMilkRMC.Text = ds.Tables[0].Rows[0]["NMGMilkRMC"].ToString();
                                txtOtherMilkRMC.Text = ds.Tables[0].Rows[0]["OtherMilkRMC"].ToString();
                                txtCOMMUsedRMC.Text = ds.Tables[0].Rows[0]["COMMUsedRMC"].ToString();
                                txtTotalRMC.Text = ds.Tables[0].Rows[0]["TotalRMC"].ToString();
                                txtDCSdairyCCimc.Text = ds.Tables[0].Rows[0]["DCSdairyCCimc"].ToString();
                                txtccIMCtoDAIRYpt.Text = ds.Tables[0].Rows[0]["ccIMCtoDAIRYpt"].ToString();
                                txtSMGMILKPT.Text = ds.Tables[0].Rows[0]["SMGMILKPT"].ToString();
                                txtNMGMILKpt.Text = ds.Tables[0].Rows[0]["NMGMILKpt"].ToString();
                                txtTotalPT.Text = ds.Tables[0].Rows[0]["TotalPT"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
                }
                finally
                {
                    ds.Clear();
                }
            }
        }
    }
    #endregion
    #region FO
    protected void clearfo()
    {
        txtAHCsalaryandwages.Text = ""; txtAHCotherdirectcost.Text = ""; txtAHCtotalCost.Text = ""; txtFPCsalryandwages.Text = ""; txtFPCotherdirectcost.Text = ""; txtFPCtotalcost.Text = "";
        txtAIcentersingle.Text = ""; txtAIcentercluster.Text = ""; txtAIcentertotal.Text = ""; txtAIperformSinglecow.Text = ""; txtAIperformBuff1.Text = ""; txtAIperformclustercow.Text = ""; txtAIPerformBuff2.Text = ""; txtAItotalCow.Text = ""; txtAItotalBuff.Text = ""; txtAiperformedtotal.Text = "";
        txtcalvborntotalcow.Text = ""; txtcalvbornbuff.Text = ""; txtanimalhusfirstAid.Text = ""; txtaniamlhusAHWcase.Text = ""; txtcattlefiedsold.Text = ""; txtdcsselingbcf.Text = ""; txtmmsalebydcs.Text = ""; txtcattinducproject.Text = ""; txtcattinducselffinance.Text = ""; txtcattleinductotal.Text = "";
        txtNOofroutes.Text = ""; txtdcsorgnised.Text = ""; txtdcsfunctional.Text = ""; txtdcsclosedtemp.Text = ""; txtnewdcsorganisedmonth.Text = ""; txtnewdcsregisteredmonth.Text = ""; txtdcsrevivedmonth.Text = ""; txtcolesdmonth.Text = "";
        txtfemalGeneral.Text = ""; txtScheCastfemale.Text = ""; txtschedtribfemale.Text = ""; txtotherbackwordfemale.Text = ""; txttotalfemale.Text = ""; txtoldDues.Text = ""; txtcurrentDues.Text = ""; txttoalDues.Text = "";
        txtTaandtransportPAC.Text = ""; txtcontractlabourPAC.Text = ""; txtotherexpansesPAC.Text = ""; txttotalPAC.Text = ""; txtsalaryandwagesAiActivites.Text = ""; txttransportAiActivites.Text = ""; txtLn2ConsumedAiAcitivites.Text = ""; txtLn2transportAiactivites.Text = ""; txtsemenandstrawesAiactivites.Text = ""; txtotherdirectcostAiactivites.Text = ""; txtlessincomeAiactivites.Text = ""; txttotalcostAiactivites.Text = "";
        txtgenralFun.Text = ""; txtschedulcasteFun.Text = ""; txtscheduletribeFun.Text = ""; txtbackwordFun.Text = ""; txtTotalFun.Text = ""; txtlandlesslaboueFun.Text = ""; txtmarinalfarmerFun.Text = ""; txtsmallfarmerFun.Text = ""; txtlargefarmerFun.Text = ""; txtOthersFun.Text = ""; txtFunTotal.Text = "";
        txtfemalegeneral.Text = ""; txtfemaleschedulcaste.Text = ""; txtfemaletribe.Text = ""; txtfemalebackword.Text = ""; txtfemaletotal.Text = ""; txtmembers.Text = ""; txtnonmerbers.Text = ""; txttotalPourers.Text = "";
        txtGeneral.Text = ""; txtSceduledcaste.Text = ""; txtscheduletribe.Text = ""; txtbackworsclasses.Text = ""; txtmembershiptotal.Text = ""; txtlandlesslabour.Text = ""; txtmarginalfarmer.Text = ""; txtsmallfarmer.Text = ""; txtlargefarmer.Text = ""; txtothers.Text = ""; txttotal.Text = "";
        txtTEcostsalaryandwages.Text = ""; txtTEcostotherdirectcost.Text = ""; txtTEcostlessincome.Text = ""; txtTotalCost.Text = ""; txtsalaryandwagesOTI.Text = ""; txtotherincmecostOTI.Text = ""; txttotalcostOTI.Text = "";
        txtsalarywagesPAC.Text = ""; txtFOGrandTotal.Text = "";
    }
    protected void btnFO_Click(object sender, EventArgs e)
    { //raghav
        try
        {
            string msg = "";
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year. \\n";
            }
            if (ddlmonth.SelectedIndex == 0)
            {
                msg += "Select Month. \\n";
            }
            if (ddlform.SelectedIndex == 0)
            {
                msg += "Select Year. \\n";
            }
            //1
            if (txtAHCsalaryandwages.Text == "") { txtAHCsalaryandwages.Text = "0"; }
            if (txtAHCotherdirectcost.Text == "") { txtAHCotherdirectcost.Text = "0"; }
            if (txtAHCtotalCost.Text == "") { txtAHCtotalCost.Text = "0"; }
            if (txtFPCsalryandwages.Text == "") { txtFPCsalryandwages.Text = "0"; }
            if (txtFPCotherdirectcost.Text == "") { txtFPCotherdirectcost.Text = "0"; }
            if (txtFPCtotalcost.Text == "") { txtFPCtotalcost.Text = "0"; }
            if (txtAIcentersingle.Text == "") { txtAIcentersingle.Text = "0"; }
            if (txtAIcentercluster.Text == "") { txtAIcentercluster.Text = "0"; }
            if (txtAIcentertotal.Text == "") { txtAIcentertotal.Text = "0"; }
            if (txtAIperformSinglecow.Text == "") { txtAIperformSinglecow.Text = "0"; }
            if (txtAIperformBuff1.Text == "") { txtAIperformBuff1.Text = "0"; }
            if (txtAIperformclustercow.Text == "") { txtAIperformclustercow.Text = "0"; }
            if (txtAIPerformBuff2.Text == "") { txtAIPerformBuff2.Text = "0"; }
            if (txtAItotalCow.Text == "") { txtAItotalCow.Text = "0"; }
            if (txtAItotalBuff.Text == "") { txtAItotalBuff.Text = "0"; }
            if (txtAiperformedtotal.Text == "") { txtAiperformedtotal.Text = "0"; }
            if (txtcalvborntotalcow.Text == "") { txtcalvborntotalcow.Text = "0"; }
            if (txtcalvbornbuff.Text == "") { txtcalvbornbuff.Text = "0"; }
            if (txtanimalhusfirstAid.Text == "") { txtanimalhusfirstAid.Text = "0"; }
            if (txtaniamlhusAHWcase.Text == "") { txtaniamlhusAHWcase.Text = "0"; }
            if (txtcattlefiedsold.Text == "") { txtcattlefiedsold.Text = "0"; }
            if (txtdcsselingbcf.Text == "") { txtdcsselingbcf.Text = "0"; }
            if (txtmmsalebydcs.Text == "") { txtmmsalebydcs.Text = "0"; }
            if (txtcattinducproject.Text == "") { txtcattinducproject.Text = "0"; }
            if (txtcattinducselffinance.Text == "") { txtcattinducselffinance.Text = "0"; }
            if (txtcattleinductotal.Text == "") { txtcattleinductotal.Text = "0"; }
            if (txtNOofroutes.Text == "") { txtNOofroutes.Text = "0"; }
            if (txtdcsorgnised.Text == "") { txtdcsorgnised.Text = "0"; }
            if (txtdcsfunctional.Text == "") { txtdcsfunctional.Text = "0"; }
            if (txtdcsclosedtemp.Text == "") { txtdcsclosedtemp.Text = "0"; }
            if (txtnewdcsorganisedmonth.Text == "") { txtnewdcsorganisedmonth.Text = "0"; }
            if (txtnewdcsregisteredmonth.Text == "") { txtnewdcsregisteredmonth.Text = "0"; }
            if (txtdcsrevivedmonth.Text == "") { txtdcsrevivedmonth.Text = "0"; }
            if (txtcolesdmonth.Text == "") { txtcolesdmonth.Text = "0"; }
            if (txtfemalGeneral.Text == "") { txtfemalGeneral.Text = "0"; }
            if (txtScheCastfemale.Text == "") { txtScheCastfemale.Text = "0"; }
            if (txtschedtribfemale.Text == "") { txtschedtribfemale.Text = "0"; }
            if (txtotherbackwordfemale.Text == "") { txtotherbackwordfemale.Text = "0"; }
            if (txttotalfemale.Text == "") { txttotalfemale.Text = "0"; }
            if (txtoldDues.Text == "") { txtoldDues.Text = "0"; }
            if (txtcurrentDues.Text == "") { txtcurrentDues.Text = "0"; }
            if (txttoalDues.Text == "") { txttoalDues.Text = "0"; }
            if (txtsalarywagesPAC.Text == "") { txtsalarywagesPAC.Text = "0"; }
            if (txtTaandtransportPAC.Text == "") { txtTaandtransportPAC.Text = "0"; }
            if (txtcontractlabourPAC.Text == "") { txtcontractlabourPAC.Text = "0"; }
            if (txtotherexpansesPAC.Text == "") { txtotherexpansesPAC.Text = "0"; }
            if (txttotalPAC.Text == "") { txttotalPAC.Text = "0"; }
            if (txtsalaryandwagesAiActivites.Text == "") { txtsalaryandwagesAiActivites.Text = "0"; }
            if (txttransportAiActivites.Text == "") { txttransportAiActivites.Text = "0"; }
            if (txtLn2ConsumedAiAcitivites.Text == "") { txtLn2ConsumedAiAcitivites.Text = "0"; }
            if (txtLn2transportAiactivites.Text == "") { txtLn2transportAiactivites.Text = "0"; }
            if (txtsemenandstrawesAiactivites.Text == "") { txtsemenandstrawesAiactivites.Text = "0"; }
            if (txtotherdirectcostAiactivites.Text == "") { txtotherdirectcostAiactivites.Text = "0"; }
            if (txtlessincomeAiactivites.Text == "") { txtlessincomeAiactivites.Text = "0"; }
            if (txttotalcostAiactivites.Text == "") { txttotalcostAiactivites.Text = "0"; }
            if (txtgenralFun.Text == "") { txtgenralFun.Text = "0"; }
            if (txtschedulcasteFun.Text == "") { txtschedulcasteFun.Text = "0"; }
            if (txtscheduletribeFun.Text == "") { txtscheduletribeFun.Text = "0"; }
            if (txtbackwordFun.Text == "") { txtbackwordFun.Text = "0"; }
            if (txtTotalFun.Text == "") { txtTotalFun.Text = "0"; }
            if (txtlandlesslaboueFun.Text == "") { txtlandlesslaboueFun.Text = "0"; }
            if (txtmarinalfarmerFun.Text == "") { txtmarinalfarmerFun.Text = "0"; }
            if (txtsmallfarmerFun.Text == "") { txtsmallfarmerFun.Text = "0"; }
            if (txtlargefarmerFun.Text == "") { txtlargefarmerFun.Text = "0"; }
            if (txtOthersFun.Text == "") { txtOthersFun.Text = "0"; }
            if (txtFunTotal.Text == "") { txtFunTotal.Text = "0"; }
            if (txtfemalegeneral.Text == "") { txtfemalegeneral.Text = "0"; }
            if (txtfemaleschedulcaste.Text == "") { txtfemaleschedulcaste.Text = "0"; }
            if (txtfemaletribe.Text == "") { txtfemaletribe.Text = "0"; }
            if (txtfemalebackword.Text == "") { txtfemalebackword.Text = "0"; }
            if (txtfemaletotal.Text == "") { txtfemaletotal.Text = "0"; }
            if (txtmembers.Text == "") { txtmembers.Text = "0"; }
            if (txtnonmerbers.Text == "") { txtnonmerbers.Text = "0"; }
            if (txttotalPourers.Text == "") { txttotalPourers.Text = "0"; }
            if (txtGeneral.Text == "") { txtGeneral.Text = "0"; }
            if (txtSceduledcaste.Text == "") { txtSceduledcaste.Text = "0"; }
            if (txtscheduletribe.Text == "") { txtscheduletribe.Text = "0"; }
            if (txtbackworsclasses.Text == "") { txtbackworsclasses.Text = "0"; }
            if (txtmembershiptotal.Text == "") { txtmembershiptotal.Text = "0"; }
            if (txtlandlesslabour.Text == "") { txtlandlesslabour.Text = "0"; }
            if (txtmarginalfarmer.Text == "") { txtmarginalfarmer.Text = "0"; }
            if (txtsmallfarmer.Text == "") { txtsmallfarmer.Text = "0"; }
            if (txtlargefarmer.Text == "") { txtlargefarmer.Text = "0"; }
            if (txtothers.Text == "") { txtothers.Text = "0"; }
            if (txttotal.Text == "") { txttotal.Text = "0"; }
            if (txtTEcostsalaryandwages.Text == "") { txtTEcostsalaryandwages.Text = "0"; }
            if (txtTEcostotherdirectcost.Text == "") { txtTEcostotherdirectcost.Text = "0"; }
            if (txtTEcostlessincome.Text == "") { txtTEcostlessincome.Text = "0"; }
            if (txtTotalCost.Text == "") { txtTotalCost.Text = "0"; }
            if (txtsalaryandwagesOTI.Text == "") { txtsalaryandwagesOTI.Text = "0"; }
            if (txtotherincmecostOTI.Text == "") { txtotherincmecostOTI.Text = "0"; }
            if (txttotalcostOTI.Text == "") { txttotalcostOTI.Text = "0"; }
            if (txtFOGrandTotal.Text == "") { txtFOGrandTotal.Text = "0"; }

            if (msg == "")
            {
                ds = objdb.ByProcedure("SP_MonthlyCostingFarmerOrgnization", new string[] { "flag", "EntryYear", "EntryMonth", "AHCSalaryAndWages", "AHCOtherDirectCost", "AHCTotalCost", "FodderSalaryAndWages", "FodderOtherDirectCost", "FodderTotalCost", 
                    "NoOfAICenterSingle", "NoOfAICenterCluster", "NoOfAICenterTotal", "NoOfAIPerformedSingleCow", "NoOfAIPerformedSingleBuff", "NoOfAIPerformedClusterCow", "NoOfAIPerformedClusterBuff", "NoOfAIPerformedTotalCow", "NoOfAIPerformedTotalBuff", "TotalAIPerformedNo",
                    "TotalCow", "Buff", "FirstAidCases", "AHW_Cases", "CattleFieldSoldBy", "NoOfDCSSellingDCSBCF", "MMSaleByDCSToProdusers", "CattleInductionProject", "CattleInductionSelfFinance", "CattleInductionTotal",
                     "NoOfFunctionalRoutes", "DCSOrganised", "DCSFunctional", "DCSClosedTemp", "NewDCSOrganisedDuringTheMonth", "NewDCSRegisteredDuringTheMonth", "NoOfDCSRevivedDuringTheMonth", "NoOfDCSClosedDuringTheMonth",
                     "General_FMO", "ScheduleCaste_FMO", "ScheduleTribe_FMO", "OtherBackword", "GSSO_Total", "OLD", "Dues_Current", "Total_FMO",
                     "SalaryAndWagesPAC","TaAndTransportation","ContractLaboure","OtherExpanses","PACTotal","SalaryAndWages","Transportation","CostOfLN2Consumed","CostOfLN2Transportation","CostOfSemenAndStraws","OtherDirectCost","LessIncome","TotalCost",
                      "General_TMFD", "Schedule_Caste", "Scheduled_Tribe", "OtherBackwordClasses", "GSSO_Total_TMFD", "LandlessLaboures", "MargionalFarmers", "SamllFarmers", "LargeFarmers", "Others", "Total_FMF",
                       "General", "ScheduleCaste", "ScheduleTribe", "OtherBackwordClass", "Total", "MilkPourerMembers", "MilkPourerNonMembers", "MilkPourerTotal",
                        "General_TMO", "Schedule_Caste_TMO", "Scheduled_Tribe_TMO", "OtherBackwordClasses_TMO", "GSSO_Total_TMO", "LandlessLaboures_TMO", "MargionalFarmers_TMO", "SamllFarmers_TMO", "LargeFarmers_TMO", "Others_TMO", "LMSLO_Total",
                        "TEC_SalaryAndWages", "TEC_OtherDirectCost", "TEC_LessIncome", "OI_TotalCost", "OI_SalaryAndWages", "OI_OtherDirectCost", "OIC_TotalCost","GrandtoatalPAC",
                        "Office_ID", "CreatedBy", "CreatedIP" },
                    new string[] { "1", ddlYear.SelectedValue, ddlmonth.SelectedValue, txtAHCsalaryandwages.Text, txtAHCotherdirectcost.Text, txtAHCtotalCost.Text, txtFPCsalryandwages.Text, txtFPCotherdirectcost.Text, txtFPCtotalcost.Text,
                    txtAIcentersingle.Text,txtAIcentercluster.Text,txtAIcentertotal.Text,txtAIperformSinglecow.Text,txtAIperformBuff1.Text,txtAIperformclustercow.Text,txtAIPerformBuff2.Text,txtAItotalCow.Text,txtAItotalBuff.Text,txtAiperformedtotal.Text,
                    txtcalvborntotalcow.Text,txtcalvbornbuff.Text,txtanimalhusfirstAid.Text,txtaniamlhusAHWcase.Text,txtcattlefiedsold.Text,txtdcsselingbcf.Text,txtmmsalebydcs.Text,txtcattinducproject.Text,txtcattinducselffinance.Text,txtcattleinductotal.Text,
                    txtNOofroutes.Text,txtdcsorgnised.Text,txtdcsfunctional.Text,txtdcsclosedtemp.Text,txtnewdcsorganisedmonth.Text,txtnewdcsregisteredmonth.Text,txtdcsrevivedmonth.Text,txtcolesdmonth.Text,
                    txtfemalGeneral.Text,txtScheCastfemale.Text,txtschedtribfemale.Text,txtotherbackwordfemale.Text,txttotalfemale.Text,txtoldDues.Text,txtcurrentDues.Text,txttoalDues.Text,
                    txtsalarywagesPAC.Text,txtTaandtransportPAC.Text,txtcontractlabourPAC.Text,txtotherexpansesPAC.Text,txttotalPAC.Text,txtsalaryandwagesAiActivites.Text,txttransportAiActivites.Text,txtLn2ConsumedAiAcitivites.Text,txtLn2transportAiactivites.Text,txtsemenandstrawesAiactivites.Text,txtotherdirectcostAiactivites.Text,txtlessincomeAiactivites.Text,txttotalcostAiactivites.Text,
                    txtgenralFun.Text,txtschedulcasteFun.Text,txtscheduletribeFun.Text,txtbackwordFun.Text,txtTotalFun.Text,txtlandlesslaboueFun.Text,txtmarinalfarmerFun.Text,txtsmallfarmerFun.Text,txtlargefarmerFun.Text,txtOthersFun.Text,txtFunTotal.Text,
                    txtfemalegeneral.Text,txtfemaleschedulcaste.Text,txtfemaletribe.Text,txtfemalebackword.Text,txtfemaletotal.Text,txtmembers.Text,txtnonmerbers.Text,txttotalPourers.Text,
                    txtGeneral.Text,txtSceduledcaste.Text,txtscheduletribe.Text,txtbackworsclasses.Text,txtmembershiptotal.Text,txtlandlesslabour.Text,txtmarginalfarmer.Text,txtsmallfarmer.Text,txtlargefarmer.Text,txtothers.Text,txttotal.Text,
                    txtTEcostsalaryandwages.Text,txtTEcostotherdirectcost.Text,txtTEcostlessincome.Text,txtTotalCost.Text,txtsalaryandwagesOTI.Text,txtotherincmecostOTI.Text,txttotalcostOTI.Text,txtFOGrandTotal.Text,
                    objdb.Office_ID(),objdb.createdBy(),objdb.GetLocalIPAddress()}, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());

                    }
                }
                openingForm();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }
    #endregion
    #region MOP
    protected void btnMOP_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            // GMPMS
            if (txtMILKproKGPD_KG_Monthly.Text == "") { txtMILKproKGPD_KG_Monthly.Text = "0"; }
            if (txtLocalMILKLPD_Ltr_Monthly.Text == "") { txtLocalMILKLPD_Ltr_Monthly.Text = "0"; }
            if (txtMILKproKGPD_Monthly.Text == "") { txtMILKproKGPD_Monthly.Text = "0"; }
            if (txtMILKprocKGPD_Cummulative.Text == "") { txtMILKprocKGPD_Cummulative.Text = "0"; }
            if (txtLocalMILKLPD_Monthly.Text == "") { txtLocalMILKLPD_Monthly.Text = "0"; }
            if (txtLocalmilkLPD_Cummulative.Text == "") { txtLocalmilkLPD_Cummulative.Text = "0"; }
            //LMS
            if (txtwholemilk.Text == "") { txtwholemilk.Text = "0"; }
            if (txtfullcreammilk.Text == "") { txtfullcreammilk.Text = "0"; }
            if (txtstdmilk.Text == "") { txtstdmilk.Text = "0"; }
            if (txttonedmilk.Text == "") { txttonedmilk.Text = "0"; }
            if (txtdtmilk.Text == "") { txtdtmilk.Text = "0"; }
            if (txtskimmilk.Text == "") { txtskimmilk.Text = "0"; }
            if (txtrawchilldmilk.Text == "") { txtrawchilldmilk.Text = "0"; }
            if (txtchaispecimilk.Text == "") { txtchaispecimilk.Text = "0"; }
            if (txtcowmilk.Text == "") { txtcowmilk.Text = "0"; }
            if (txtsanchilitemilk.Text == "") { txtsanchilitemilk.Text = "0"; }
            if (txtchahamilk.Text == "") { txtchahamilk.Text = "0"; }
            if (txttotalmilksale.Text == "") { txttotalmilksale.Text = "0"; }
            // MP 
            if (txtDCSmilkRMRD.Text == "") { txtDCSmilkRMRD.Text = "0"; }
            if (txtDCSmilkCCS.Text == "") { txtDCSmilkCCS.Text = "0"; }
            if (txtSMGMILK.Text == "") { txtSMGMILK.Text = "0"; }
            if (txtNMGmilk.Text == "") { txtNMGmilk.Text = "0"; }
            if (txtOTHER.Text == "") { txtOTHER.Text = "0"; }
            if (txttotalMilkProc.Text == "") { txttotalMilkProc.Text = "0"; }
            //MPKGPD
            if (txtMILKPROC_monthly.Text == "") { txtMILKPROC_monthly.Text = "0"; }
            if (txtMILKPROC_Cummulat.Text == "") { txtMILKPROC_Cummulat.Text = "0"; }
            if (txtLocalMILK_MOnthly.Text == "") { txtLocalMILK_MOnthly.Text = "0"; }
            if (txtLocalMilk_Cummulat.Text == "") { txtLocalMilk_Cummulat.Text = "0"; }
            if (txtTotalMilkSale_Monthly.Text == "") { txtTotalMilkSale_Monthly.Text = "0"; }
            if (txtTotalMilkSale_Cummulat.Text == "") { txtTotalMilkSale_Cummulat.Text = "0"; }
            if (txtSMGmilk_Monthly.Text == "") { txtSMGmilk_Monthly.Text = "0"; }
            if (txtSMGmilk_Cummulat.Text == "") { txtSMGmilk_Cummulat.Text = "0"; }
            if (txtNMGOTH_MOnthly.Text == "") { txtNMGOTH_MOnthly.Text = "0"; }
            if (txtNMGOTH_Cummulat.Text == "") { txtNMGOTH_Cummulat.Text = "0"; }
            //SMS
            if (txtwholemilk_SMG.Text == "") { txtwholemilk_SMG.Text = "0"; }
            if (txtskimmilk_SMG.Text == "") { txtskimmilk_SMG.Text = "0"; }
            if (txtOther_SMG.Text == "") { txtOther_SMG.Text = "0"; }
            if (txtTotalsmgsale_SMG.Text == "") { txtTotalsmgsale_SMG.Text = "0"; }
            if (txtwholemilk_NMG.Text == "") { txtwholemilk_NMG.Text = "0"; }
            if (txtskimmilk_NMG.Text == "") { txtskimmilk_NMG.Text = "0"; }
            if (txtOther_NMG.Text == "") { txtOther_NMG.Text = "0"; }
            if (txtTotalNMGsale_NMG.Text == "") { txtTotalNMGsale_NMG.Text = "0"; }
            if (txtwholmilkinLit_OSALE.Text == "") { txtwholmilkinLit_OSALE.Text = "0"; }
            if (txtskimmilkinLit_OSALE.Text == "") { txtskimmilkinLit_OSALE.Text = "0"; }
            if (txtOther_OSALE.Text == "") { txtOther_OSALE.Text = "0"; }
            if (txttotalBulkSale_OSALE.Text == "") { txttotalBulkSale_OSALE.Text = "0"; }

            string msg = "";
            if (ddlYear.SelectedValue == "0")
            {
                msg += "Select Year. \\n";
            }
            if (ddlmonth.SelectedValue == "0")
            {
                msg += "Select Month. \\n";
            }
            if (ddlform.SelectedValue == "0")
            {
                msg += "Select Year. \\n";
            }
            if (msg == "")
            {

                if (ddlform.SelectedValue == "2")
                {
                    ds = objdb.ByProcedure("SP_MonthlyCosting_MilkProcurementSale",
                        new string[] { "Flag", "MP_InTheMonth_GMPMS", "MP_TillMonth_GMPMS", "LMS_InTheMonth_GMPMS", "LMS_TillMonth_GMPMS","MILKproKGPD_KG_Monthly","LocalMILKLPD_Ltr_Monthly",
                                "WholeMilk_LMS","FullCreamMilk_LMS","STDMilk_LMS","TonedMilk_LMS","DTMilk_LMS","SkimMilk_LMS", "RawChilldMilk_LMS","ChaiSpecialMilk_LMS", "CowMilk_LMS", "SanchiLiteMilk_LMS","ChahaMilk_LMS", "TotalMilkSale_LMS",
                                    "DCSMilkRMRD_MP","DCSMilkCCs_MP", "SMGMilk_MP","NMGMilk_MP","Other_MP","TotalMilkProc_MP", 
                                    "MP_InTheMonth_MPKGPD","MP_TillMonth_MPKGPD","LMS_InTheMonth_MPKGPD","LMS_TillMonth_MPKGPD", "TMS_InTheMonth_MPKGPD","TMS_TillMonth_MPKGPD","SMS_InTheMonth_MPKGPD","SMS_TillMonth_MPKGPD", "NOS_InTheMonth_MPKGPD","NOS_TillMonth_MPKGPD",
                                    "SMS_WholeMilk_SMS","SMS_SkimMilk_SMS","SMS_Other_SMS","SMS_TotalSMGSale_SMS", "NMS_WholeMilk_SMS", "NMS_SkimMilk_SMS","NMS_Other_SMS", "NMS_TotalNMGSale_SMS","OS_WholeMilkInLit_SMS","OS_SkimMilkInLit_SMS", "OS_Other_SMS","OS_TotalBULKSale_SMS", 
                                    "CreatedBy", "CreatedIP","Office_ID","Entry_Month","Entry_Year"}
                    , new string[] { "1", txtMILKproKGPD_Monthly.Text, txtMILKprocKGPD_Cummulative.Text, txtLocalMILKLPD_Monthly.Text, txtLocalmilkLPD_Cummulative.Text,txtMILKproKGPD_KG_Monthly.Text,txtLocalMILKLPD_Ltr_Monthly.Text,
                               txtwholemilk.Text,txtfullcreammilk.Text,txtstdmilk.Text,txttonedmilk.Text,txtdtmilk.Text,txtskimmilk.Text,txtrawchilldmilk.Text,txtchaispecimilk.Text,txtcowmilk.Text,txtsanchilitemilk.Text,txtchahamilk.Text,txttotalmilksale.Text,
                                txtDCSmilkRMRD.Text,txtDCSmilkCCS.Text,txtSMGMILK.Text,txtNMGmilk.Text,txtOTHER.Text,txttotalMilkProc.Text,
                                txtMILKPROC_monthly.Text,txtMILKPROC_Cummulat.Text,txtLocalMILK_MOnthly.Text,txtLocalMilk_Cummulat.Text,txtTotalMilkSale_Monthly.Text,txtTotalMilkSale_Cummulat.Text,txtSMGmilk_Monthly.Text,txtSMGmilk_Cummulat.Text,txtNMGOTH_MOnthly.Text,txtNMGOTH_Cummulat.Text,
                                txtwholemilk_SMG.Text, txtskimmilk_SMG.Text,txtOther_SMG.Text,txtTotalsmgsale_SMG.Text,txtwholemilk_NMG.Text,txtskimmilk_NMG.Text,txtOther_NMG.Text,txtTotalNMGsale_NMG.Text,txtwholmilkinLit_OSALE.Text,txtskimmilkinLit_OSALE.Text,txtOther_OSALE.Text,txttotalBulkSale_OSALE.Text,
                                objdb.createdBy(),objdb.GetLocalIPAddress(),objdb.Office_ID(),ddlmonth.SelectedValue,ddlYear.SelectedValue}, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());

                        }
                    }
                    openingForm();
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }
    protected void clrTxt_MPS()
    {
        //GMPMS
        txtMILKproKGPD_KG_Monthly.Text = "";
        txtLocalMILKLPD_Ltr_Monthly.Text = "";
        txtMILKproKGPD_Monthly.Text = "";
        txtMILKprocKGPD_Cummulative.Text = "";
        txtLocalMILKLPD_Monthly.Text = "";
        txtLocalmilkLPD_Cummulative.Text = "";

        //LMS
        txtwholemilk.Text = "";
        txtfullcreammilk.Text = "";
        txtstdmilk.Text = "";
        txttonedmilk.Text = "";
        txtdtmilk.Text = "";
        txtskimmilk.Text = "";
        txtrawchilldmilk.Text = "";
        txtchaispecimilk.Text = "";
        txtcowmilk.Text = "";
        txtsanchilitemilk.Text = "";
        txtchahamilk.Text = "";
        txttotalmilksale.Text = "";

        // MP 
        txtDCSmilkRMRD.Text = "";
        txtDCSmilkCCS.Text = "";
        txtSMGMILK.Text = "";
        txtNMGmilk.Text = "";
        txtOTHER.Text = "";
        txttotalMilkProc.Text = "";

        //MPKGPD
        txtMILKPROC_monthly.Text = "";
        txtMILKPROC_Cummulat.Text = "";
        txtLocalMILK_MOnthly.Text = "";
        txtLocalMilk_Cummulat.Text = "";
        txtTotalMilkSale_Monthly.Text = "";
        txtTotalMilkSale_Cummulat.Text = "";
        txtSMGmilk_Monthly.Text = "";
        txtSMGmilk_Cummulat.Text = "";
        txtNMGOTH_MOnthly.Text = "";
        txtNMGOTH_Cummulat.Text = "";

        //SMS
        txtwholemilk_SMG.Text = "";
        txtskimmilk_SMG.Text = "";
        txtOther_SMG.Text = "";
        txtTotalsmgsale_SMG.Text = "";
        txtwholemilk_NMG.Text = "";
        txtskimmilk_NMG.Text = "";
        txtOther_NMG.Text = "";
        txtTotalNMGsale_NMG.Text = "";
        txtwholmilkinLit_OSALE.Text = "";
        txtskimmilkinLit_OSALE.Text = "";
        txtOther_OSALE.Text = "";
        txttotalBulkSale_OSALE.Text = "";

    }
    #endregion
    #region PMandSale
    protected void btnPMandSale_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            // Ghee
            if (txtGhee.Text == "") { txtGhee.Text = "0"; }
            if (txtSkimmilkpowder.Text == "") { txtSkimmilkpowder.Text = "0"; }
            if (txttablebutter.Text == "") { txttablebutter.Text = "0"; }
            if (txtwhitebutter.Text == "") { txtwhitebutter.Text = "0"; }
            if (txtWMP.Text == "") { txtWMP.Text = "0"; }
            if (txtpaneer.Text == "") { txtpaneer.Text = "0"; }
            if (txtSanchi_GHEESale.Text == "") { txtSanchi_GHEESale.Text = "0"; }
            if (txtSneha_GHEESale.Text == "") { txtSneha_GHEESale.Text = "0"; }
            if (txtOther_GHEESale.Text == "") { txtOther_GHEESale.Text = "0"; }
            if (txtTotal_GHEESale.Text == "") { txtTotal_GHEESale.Text = "0"; }
            if (txtskimmilk_Prodctsale.Text == "") { txtskimmilk_Prodctsale.Text = "0"; }
            if (txttablebutter_Prodctsale.Text == "") { txttablebutter_Prodctsale.Text = "0"; }
            if (txtwhitebutter_Prodctsale.Text == "") { txtwhitebutter_Prodctsale.Text = "0"; }
            if (txtshrikhand_Prodctsale.Text == "") { txtshrikhand_Prodctsale.Text = "0"; }

            // Mava
            if (txtmawa_PMAS.Text == "") { txtmawa_PMAS.Text = "0"; }
            if (txtdrycasein_PMAS.Text == "") { txtdrycasein_PMAS.Text = "0"; }
            if (txtcookingbutter_PMAS.Text == "") { txtcookingbutter_PMAS.Text = "0"; }
            if (txtgulabjamun_PMAS.Text == "") { txtgulabjamun_PMAS.Text = "0"; }
            if (txtrasgulla_PMAS.Text == "") { txtrasgulla_PMAS.Text = "0"; }
            if (txtmawagulabjanum_PMAS.Text == "") { txtmawagulabjanum_PMAS.Text = "0"; }
            if (txtmilkcake_PMAS.Text == "") { txtmilkcake_PMAS.Text = "0"; }
            if (txtThandai_PMAS.Text == "") { txtThandai_PMAS.Text = "0"; }
            if (txtMDm_PMAS.Text == "") { txtMDm_PMAS.Text = "0"; }
            if (txtlightlassi_PMAS.Text == "") { txtlightlassi_PMAS.Text = "0"; }
            if (txtpudinaraita_PMAS.Text == "") { txtpudinaraita_PMAS.Text = "0"; }
            if (txtWMP_PMAS.Text == "") { txtWMP_PMAS.Text = "0"; }
            if (txtpannerachar_PMAS.Text == "") { txtpannerachar_PMAS.Text = "0"; }

            // Paneer
            if (txtPAneer_PMAS.Text == "") { txtPAneer_PMAS.Text = "0"; }
            if (txtflavmilk_PMAS.Text == "") { txtflavmilk_PMAS.Text = "0"; }
            if (txtBtrmilk_PMAS.Text == "") { txtBtrmilk_PMAS.Text = "0"; }
            if (txtSweetcurd_PMAS.Text == "") { txtSweetcurd_PMAS.Text = "0"; }
            if (txtpeda_PMAS.Text == "") { txtpeda_PMAS.Text = "0"; }
            if (txtplaincurd_PMAS.Text == "") { txtplaincurd_PMAS.Text = "0"; }
            if (txtorangsip_PMAS.Text == "") { txtorangsip_PMAS.Text = "0"; }
            if (txtprobioticcurd_PMAS.Text == "") { txtprobioticcurd_PMAS.Text = "0"; }
            if (txtwholemilk_PMAS.Text == "") { txtwholemilk_PMAS.Text = "0"; }
            if (txtChenarabdi_PMAS.Text == "") { txtChenarabdi_PMAS.Text = "0"; }
            if (txtpresscurd_PMAS.Text == "") { txtpresscurd_PMAS.Text = "0"; }
            if (txtcream_PMAS.Text == "") { txtcream_PMAS.Text = "0"; }
            if (txtlassi_PMAS.Text == "") { txtlassi_PMAS.Text = "0"; }
            if (txtamarkhand_PMAS.Text == "") { txtamarkhand_PMAS.Text = "0"; }
            if (txtsmp_PMAS.Text == "") { txtsmp_PMAS.Text = "0"; }
            // SLM
            if (txtsanchilitemilk_PMAS.Text == "") { txtsanchilitemilk_PMAS.Text = "0"; }
            if (txtnariyalbarfi_PMAS.Text == "") { txtnariyalbarfi_PMAS.Text = "0"; }
            if (txtgulabjamunMix_PMAS.Text == "") { txtgulabjamunMix_PMAS.Text = "0"; }
            if (txtcoffemix_PMAS.Text == "") { txtcoffemix_PMAS.Text = "0"; }
            if (txtcookingbutter.Text == "") { txtcookingbutter.Text = "0"; }
            if (txtlowfatpanner_PMAS.Text == "") { txtlowfatpanner_PMAS.Text = "0"; }
            if (txtwheydrink_PMAS.Text == "") { txtwheydrink_PMAS.Text = "0"; }
            if (txtsanchitea_PMAS.Text == "") { txtsanchitea_PMAS.Text = "0"; }
            if (txtpedaprasadi_PMAS.Text == "") { txtpedaprasadi_PMAS.Text = "0"; }
            if (txticecream_PMAS.Text == "") { txticecream_PMAS.Text = "0"; }
            if (txtgoldenmilk_PMAS.Text == "") { txtgoldenmilk_PMAS.Text = "0"; }
            if (txtsugarfreepeda_PMAS.Text == "") { txtsugarfreepeda_PMAS.Text = "0"; }
            if (txthealthvita_PMAS.Text == "") { txthealthvita_PMAS.Text = "0"; }
            string msg = "";
            if (ddlYear.SelectedValue == "0")
            {
                msg += "Select Year. \\n";
            }
            if (ddlmonth.SelectedValue == "0")
            {
                msg += "Select Month. \\n";
            }
            if (ddlform.SelectedValue == "0")
            {
                msg += "Select Year. \\n";
            }
            if (msg == "")
            {
                if (ddlform.SelectedValue == "3")
                {
                    ds = objdb.ByProcedure("SP_MonthlyCosting_ProductsManufacturingSale",
                        new string[] {"Flag","PM_Ghee_Ghee","PM_SkimMilkPowder_Ghee","PM_TableButter_Ghee","PM_WhiteButter_Ghee","PM_WMP_Ghee","Paneer_Ghee","GS_Sanchi_Ghee","GS_Sneha_Ghee","GS_Other_Ghee","GS_TotalSale_Ghee","PS_SkimMilkPowder_Ghee","PS_TableButter_Ghee","PS_WhiteButter_Ghee","PS_ShriKhand_Ghee",
                                        "Mawa_Mava","DryCasein_Mava","CookingButter_Mava","GulabJamun_Mava","RashGulla_Mava","MawaGulabJamun_Mava","MilkCakeSanchi_Mava","Thandai_Mava","MdmSweetenSmp_Mava","LightLassi_Mava","PudinaRaita_Mava","WMP_Mava","PaneerAchar_Mava",
                                        "paneer_Paneer","FlavMilkInLit_Paneer","BtrMilkInLit_Paneer","SweetCurd_Paneer","Peda_Paneer","PlainCurd_Paneer","OrangeSip_Paneer","ProBioticCurd_Paneer","WholeMilk_Paneer","ChenanRabdi_Paneer","PressCurd_Paneer","Cream_Paneer","Lassi_Paneer","Amrakhand_Paneer","SMPCSP_Paneer",
                                        "SanchiLiteMilk_SLM","NariyalBarfi_SLM","GulabJamunMix_SLM","CoffeeMix_SLM","CookingButter_SLM","LowFatPaneer_SLM","WheyDrink_SLM","SanchiTeaMix_SLM","PedaPrasadi_SLM","IceCream_SLM","GoldenMilk_SLM","SugarFreePeda_SLM","HealthVitaPlus_SLM",
                                        "CreatedBy","CreatedIP","Office_ID","Entry_Month","Entry_Year"},
                        new string[] {"1", txtGhee.Text, txtSkimmilkpowder.Text, txttablebutter.Text, txtwhitebutter.Text, txtWMP.Text, txtpaneer.Text, txtSanchi_GHEESale.Text, txtSneha_GHEESale.Text, txtOther_GHEESale.Text, txtTotal_GHEESale.Text, txtskimmilk_Prodctsale.Text, txttablebutter_Prodctsale.Text, txtwhitebutter_Prodctsale.Text, txtshrikhand_Prodctsale .Text,
                                    txtmawa_PMAS.Text, txtdrycasein_PMAS.Text, txtcookingbutter_PMAS.Text, txtgulabjamun_PMAS.Text, txtrasgulla_PMAS.Text, txtmawagulabjanum_PMAS.Text, txtmilkcake_PMAS.Text, txtThandai_PMAS.Text, txtMDm_PMAS.Text, txtlightlassi_PMAS.Text, txtpudinaraita_PMAS.Text, txtWMP_PMAS.Text, txtpannerachar_PMAS.Text.Trim(),
                                    txtPAneer_PMAS.Text,txtflavmilk_PMAS.Text,txtBtrmilk_PMAS.Text,txtSweetcurd_PMAS.Text,txtpeda_PMAS.Text,txtplaincurd_PMAS.Text,txtorangsip_PMAS.Text,txtprobioticcurd_PMAS.Text,txtwholemilk_PMAS.Text,txtChenarabdi_PMAS.Text,txtpresscurd_PMAS.Text,txtcream_PMAS.Text,txtlassi_PMAS.Text,txtamarkhand_PMAS.Text,txtsmp_PMAS.Text,
                                    txtsanchilitemilk_PMAS.Text,txtnariyalbarfi_PMAS.Text,txtgulabjamunMix_PMAS.Text,txtcoffemix_PMAS.Text,txtcookingbutter.Text,txtlowfatpanner_PMAS.Text,txtwheydrink_PMAS.Text, txtsanchitea_PMAS.Text,txtpedaprasadi_PMAS.Text,txticecream_PMAS.Text,txtgoldenmilk_PMAS.Text,txtsugarfreepeda_PMAS.Text,txthealthvita_PMAS.Text,
                                     objdb.createdBy(),objdb.GetLocalIPAddress(),objdb.Office_ID(),ddlmonth.SelectedValue,ddlYear.SelectedValue}, "dataset");

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());

                        }
                    }
                    openingForm();
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }
    protected void clrtxtPMandSale()
    {
        // Ghee
        txtGhee.Text = "";
        txtSkimmilkpowder.Text = "";
        txttablebutter.Text = "";
        txtwhitebutter.Text = "";
        txtWMP.Text = "";
        txtpaneer.Text = "";
        txtSanchi_GHEESale.Text = "";
        txtSneha_GHEESale.Text = "";
        txtOther_GHEESale.Text = "";
        txtTotal_GHEESale.Text = "";
        txtskimmilk_Prodctsale.Text = "";
        txttablebutter_Prodctsale.Text = "";
        txtwhitebutter_Prodctsale.Text = "";
        txtshrikhand_Prodctsale.Text = "";

        // Mava
        txtmawa_PMAS.Text = "";
        txtdrycasein_PMAS.Text = "";
        txtcookingbutter_PMAS.Text = "";
        txtgulabjamun_PMAS.Text = "";
        txtrasgulla_PMAS.Text = "";
        txtmawagulabjanum_PMAS.Text = "";
        txtmilkcake_PMAS.Text = "";
        txtThandai_PMAS.Text = "";
        txtMDm_PMAS.Text = "";
        txtlightlassi_PMAS.Text = "";
        txtpudinaraita_PMAS.Text = "";
        txtWMP_PMAS.Text = "";
        txtpannerachar_PMAS.Text = "";

        // Paneer
        txtPAneer_PMAS.Text = "";
        txtflavmilk_PMAS.Text = "";
        txtBtrmilk_PMAS.Text = "";
        txtSweetcurd_PMAS.Text = "";
        txtpeda_PMAS.Text = "";
        txtplaincurd_PMAS.Text = "";
        txtorangsip_PMAS.Text = "";
        txtprobioticcurd_PMAS.Text = "";
        txtwholemilk_PMAS.Text = "";
        txtChenarabdi_PMAS.Text = "";
        txtpresscurd_PMAS.Text = "";
        txtcream_PMAS.Text = "";
        txtlassi_PMAS.Text = "";
        txtamarkhand_PMAS.Text = "";
        txtsmp_PMAS.Text = "";
        // SLM
        txtsanchilitemilk_PMAS.Text = "";
        txtnariyalbarfi_PMAS.Text = "";
        txtgulabjamunMix_PMAS.Text = "";
        txtcoffemix_PMAS.Text = "";
        txtcookingbutter.Text = "";
        txtlowfatpanner_PMAS.Text = "";
        txtwheydrink_PMAS.Text = "";
        txtsanchitea_PMAS.Text = "";
        txtpedaprasadi_PMAS.Text = "";
        txticecream_PMAS.Text = "";
        txtgoldenmilk_PMAS.Text = "";
        txtsugarfreepeda_PMAS.Text = "";
        txthealthvita_PMAS.Text = "";

    }
    #endregion
    #region recombination
    protected void btnrecombination_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (txtSMP_forMilk.Text == "") { txtSMP_forMilk.Text = "0"; }
            if (txtWB_forMilk.Text == "") { txtWB_forMilk.Text = "0"; }
            if (txtGHEE_forMilk.Text == "") { txtGHEE_forMilk.Text = "0"; }
            if (txtWMP_forMilk.Text == "") { txtWMP_forMilk.Text = "0"; }
            if (txtPANEER_forMilk.Text == "") { txtPANEER_forMilk.Text = "0"; }
            if (txtTOTAL_forMilk.Text == "") { txtTOTAL_forMilk.Text = "0"; }
            if (txtSMP_forproduct.Text == "") { txtSMP_forproduct.Text = "0"; }
            if (txtWB_forproduct.Text == "") { txtWB_forproduct.Text = "0"; }
            if (txtGHEE_forproduct.Text == "") { txtGHEE_forproduct.Text = "0"; }
            if (txtWMP_forproduct.Text == "") { txtWMP_forproduct.Text = "0"; }
            if (txtPANEER_forproduct.Text == "") { txtPANEER_forproduct.Text = "0"; }
            if (txtTOTAL_forproduct.Text == "") { txtTOTAL_forproduct.Text = "0"; }
            if (txtformilk_Commused.Text == "") { txtformilk_Commused.Text = "0"; }
            if (txtforproduct_Commused.Text == "") { txtforproduct_Commused.Text = "0"; }
            if (txtRecombination.Text == "") { txtRecombination.Text = "0"; }
            string msg = "";
            if (ddlYear.SelectedValue == "0")
            {
                msg += "Select Year. \\n";
            }
            if (ddlmonth.SelectedValue == "0")
            {
                msg += "Select Month. \\n";
            }
            if (ddlform.SelectedValue == "0")
            {
                msg += "Select Year. \\n";
            }
            if (msg == "")
            {
                if (ddlform.SelectedValue == "4")
                {
                    ds = objdb.ByProcedure("SP_MonthlyCosting_Recombination",
                        new string[] { "Flag", "FM_Smp_R", "FM_Wb_R", "FM_Ghee_R", "FM_Wmp_R", "FM_Paneer_R", "FM_Total_R", "FP_Smp_R", "FP_Wb_R", "FP_Ghee_R", "FP_Wmp_R", "FP_Paneer_R", "FP_Total_R", "ForMilkRs_R", "ForProductRs_R", "TotalRecombinationReconstitution_R", "CreatedBy", "CreatedIP", "Office_ID", "Entry_Month", "Entry_Year" },
                        new string[] { "1", txtSMP_forMilk.Text, txtWB_forMilk.Text, txtGHEE_forMilk.Text, txtWMP_forMilk.Text, txtPANEER_forMilk.Text, txtTOTAL_forMilk.Text, txtSMP_forproduct.Text, txtWB_forproduct.Text, txtGHEE_forproduct.Text, txtWMP_forproduct.Text, txtPANEER_forproduct.Text, txtTOTAL_forproduct.Text, txtformilk_Commused.Text, txtforproduct_Commused.Text, txtRecombination.Text, objdb.createdBy(), objdb.GetLocalIPAddress(), objdb.Office_ID(), ddlmonth.SelectedValue, ddlYear.SelectedValue }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());

                        }
                    }
                    openingForm();
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }
    protected void clrTxt_R()
    {
        txtSMP_forMilk.Text = "";
        txtWB_forMilk.Text = "";
        txtGHEE_forMilk.Text = "";
        txtWMP_forMilk.Text = "";
        txtPANEER_forMilk.Text = "";
        txtTOTAL_forMilk.Text = "";
        txtSMP_forproduct.Text = "";
        txtWB_forproduct.Text = "";
        txtGHEE_forproduct.Text = "";
        txtWMP_forproduct.Text = "";
        txtPANEER_forproduct.Text = "";
        txtTOTAL_forproduct.Text = "";
        txtformilk_Commused.Text = "";
        txtforproduct_Commused.Text = "";
        txtRecombination.Text = "";
    }
    #endregion
    #region PPMaking
    protected void btnPPMaking_Click1(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year. \\n";
            }
            if (ddlmonth.SelectedIndex == 0)
            {
                msg += "Select Month. \\n";
            }
            if (ddlform.SelectedIndex == 0)
            {
                msg += "Select Year. \\n";
            }

            if (txtsalaryallow_CA.Text == "") { txtsalaryallow_CA.Text = "0"; }
            if (txtContlaboures_CA.Text == "") { txtContlaboures_CA.Text = "0"; }
            if (txtConsumblecommon_CA.Text == "") { txtConsumblecommon_CA.Text = "0"; }
            if (txtConsumbleDirect_CA.Text == "") { txtConsumbleDirect_CA.Text = "0"; }
            if (txtchemicaldetergent_CA.Text == "") { txtchemicaldetergent_CA.Text = "0"; }
            if (txtPPMGrandTotal.Text == "") { txtPPMGrandTotal.Text = "0"; }
            if (txtElectricity_CA.Text == "") { txtElectricity_CA.Text = "0"; }
            if (txtWater_CA.Text == "") { txtWater_CA.Text = "0"; }
            if (txtFurnanceoil_CA.Text == "") { txtFurnanceoil_CA.Text = "0"; }
            if (txtRepairMaint_CA.Text == "") { txtRepairMaint_CA.Text = "0"; }
            if (txtOtherExps_CA.Text == "") { txtOtherExps_CA.Text = "0"; }
            if (txtTOTal_CA.Text == "") { txtTOTal_CA.Text = "0"; }
            if (txtMilkProssCA.Text == "") { txtMilkProssCA.Text = "0"; }
            if (txtMilkPrepackCA.Text == "") { txtMilkPrepackCA.Text = "0"; }
            if (txtsalaryandallow_FPCD.Text == "") { txtsalaryandallow_FPCD.Text = "0"; }
            if (txtContlaboures_FPCD.Text == "") { txtContlaboures_FPCD.Text = "0"; }
            if (txtCosumbleCommon_FPCD.Text == "") { txtCosumbleCommon_FPCD.Text = "0"; }
            if (txtConsumbledirect_FPCD.Text == "") { txtConsumbledirect_FPCD.Text = "0"; }
            if (txtchemicaldetergent_FPCD.Text == "") { txtchemicaldetergent_FPCD.Text = "0"; }
            if (txtElectricity_FPCD.Text == "") { txtElectricity_FPCD.Text = "0"; }
            if (txtWater_FPCD.Text == "") { txtWater_FPCD.Text = "0"; }
            if (txtfurnanceoil_FPCD.Text == "") { txtfurnanceoil_FPCD.Text = "0"; }
            if (txtrepairmaintance_FPCD.Text == "") { txtrepairmaintance_FPCD.Text = "0"; }
            if (txtOtherExps_FPCD.Text == "") { txtOtherExps_FPCD.Text = "0"; }
            if (txtTotal_FPCD.Text == "") { txtTotal_FPCD.Text = "0"; }

            if (msg == "")
            {
                ds = objdb.ByProcedure("SP_MonthlyCosting_PROCESSING_PRODUCTS_MAKING",
                                       new string[] { "flag", "Office_ID", "Entry_Year", "Entry_Month" ,"SalaryAndAllowCA","ContLabouresCA","ConsumableCommonCA","ConsumableDirectCA","ChemicalAndDetergentCA","ElectricityCA","WaterCA","FurnanceOilGasCA","RepairAndMaintenanceCA","OtherExpsCA","TotalCA","MilkProcessingCostRS","MilkPrepackCostRS","Grandtotal",
                                                       "SalaryAndAllowCD","ContLabouresCD","ConsumableCommonCD","ConsumableDirectCD","ChemicalAndDetergentCD","ElectricityCD","WaterCD","FurnanceOilGasCD","RepairAndMaintenanceCD","OtherExpsCD","TotalCD",
                                                       "CreatedBy","CreatedIP"},
                                       new string[] { "0", objdb.Office_ID(), ddlYear.SelectedValue, ddlmonth.SelectedValue, txtsalaryallow_CA.Text, txtContlaboures_CA.Text, txtConsumblecommon_CA.Text, txtConsumbleDirect_CA.Text, txtchemicaldetergent_CA.Text, txtElectricity_CA.Text, txtWater_CA.Text, txtFurnanceoil_CA.Text, txtRepairMaint_CA.Text, txtOtherExps_CA.Text, txtTOTal_CA.Text, txtMilkProssCA.Text, txtMilkPrepackCA.Text,txtPPMGrandTotal.Text,
                                       txtsalaryandallow_FPCD.Text,txtContlaboures_FPCD.Text,txtCosumbleCommon_FPCD.Text,txtConsumbledirect_FPCD.Text,txtchemicaldetergent_FPCD.Text,txtElectricity_FPCD.Text,txtWater_FPCD.Text,txtfurnanceoil_FPCD.Text,txtrepairmaintance_FPCD.Text,txtOtherExps_FPCD.Text,txtTotal_FPCD.Text,
                                       objdb.createdBy(),objdb.GetLocalIPAddress()}, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());

                    }
                }
                openingForm();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }
    protected void clearppm()
    {
        txtsalaryallow_CA.Text = "";
        txtContlaboures_CA.Text = "";
        txtsalaryAndAllow_AD.Text = "";
        txtContractLabour_AD.Text = "";
        txtConsumblecommon_CA.Text = "";
        txtConsumbleDirect_CA.Text = "";
        txtchemicaldetergent_CA.Text = "";
        txtElectricity_CA.Text = "";
        txtWater_CA.Text = "";
        txtFurnanceoil_CA.Text = "";
        txtRepairMaint_CA.Text = "";
        txtOtherExps_CA.Text = "";
        txtTOTal_CA.Text = "";
        txtMilkProssCA.Text = "";
        txtMilkPrepackCA.Text = "";
        txtsalaryandallow_FPCD.Text = "";
        txtContlaboures_FPCD.Text = "";
        txtCosumbleCommon_FPCD.Text = "";
        txtConsumbledirect_FPCD.Text = "";
        txtchemicaldetergent_FPCD.Text = "";
        txtElectricity_FPCD.Text = "";
        txtWater_FPCD.Text = "";
        txtfurnanceoil_FPCD.Text = "";
        txtrepairmaintance_FPCD.Text = "";
        txtOtherExps_FPCD.Text = "";
        txtTotal_FPCD.Text = "";
        txtPPMGrandTotal.Text = "";
    }
    #endregion
    #region PackagingAndCC
    protected void btnPackagingAndCC_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (txtmilCC_PC.Text == "") { txtmilCC_PC.Text = "0"; }
            if (txtMilkDairy_PC.Text == "") { txtMilkDairy_PC.Text = "0"; }
            if (txtmainproduct_PC.Text == "") { txtmainproduct_PC.Text = "0"; }
            if (txtINDGproduct_PC.Text == "") { txtINDGproduct_PC.Text = "0"; }
            if (txtTotal_PC.Text == "") { txtTotal_PC.Text = "0"; }
            if (txtsalaryallow_CC.Text == "") { txtsalaryallow_CC.Text = "0"; }
            if (txtContlaboure_CC.Text == "") { txtContlaboure_CC.Text = "0"; }
            if (txtElectricity_CC.Text == "") { txtElectricity_CC.Text = "0"; }
            if (txtCoalDiesel_CC.Text == "") { txtCoalDiesel_CC.Text = "0"; }
            if (txtConsumble_CC.Text == "") { txtConsumble_CC.Text = "0"; }
            if (txtChemandDeter_CC.Text == "") { txtChemandDeter_CC.Text = "0"; }
            if (txtRepairMAint_CC.Text == "") { txtRepairMAint_CC.Text = "0"; }
            if (txtBMC_CC.Text == "") { txtBMC_CC.Text = "0"; }
            if (txtSecurity_CC.Text == "") { txtSecurity_CC.Text = "0"; }
            if (txtOtherExps_CC.Text == "") { txtOtherExps_CC.Text = "0"; }
            if (txtTotal_CC.Text == "") { txtTotal_CC.Text = "0"; }
            string msg = "";
            if (ddlYear.SelectedValue == "0")
            {
                msg += "Select Year. \\n";
            }
            if (ddlmonth.SelectedValue == "0")
            {
                msg += "Select Month. \\n";
            }
            if (ddlform.SelectedValue == "0")
            {
                msg += "Select Year. \\n";
            }
            if (msg == "")
            {
                if (ddlform.SelectedValue == "6")
                {
                    ds = objdb.ByProcedure("SP_MonthlyCosting_PackagingAndCC",
                        new string[] { "Flag", "PC_MilkAtCC_PCC", "PC_MilkAtDairy_PCC", "PC_MainProducts_PCC", "PC_INDG_Products_PCC", "PC_Total_PCC", "CC_SalaryAndAllow_PCC", "CC_ContLaboures_PCC", "CC_Electricity_PCC", "CC_CoalFoAndDiesel_PCC", "CC_Cosumable_PCC", "CC_ChemistryAndDetergent_PCC", "CC_RepairAndMaint_PCC", "CC_BMC_PCC", "CC_Security_PCC", "CC_OtherExps_PCC", "CC_Total_PCC", "CreatedBy", "CreatedIP", "Office_ID", "Entry_Month", "Entry_Year" },
                        new string[] { "1", txtmilCC_PC.Text, txtMilkDairy_PC.Text, txtmainproduct_PC.Text, txtINDGproduct_PC.Text, txtTotal_PC.Text, txtsalaryallow_CC.Text, txtContlaboure_CC.Text, txtElectricity_CC.Text, txtCoalDiesel_CC.Text, txtConsumble_CC.Text, txtChemandDeter_CC.Text, txtRepairMAint_CC.Text, txtBMC_CC.Text, txtSecurity_CC.Text, txtOtherExps_CC.Text, txtTotal_CC.Text, objdb.createdBy(), objdb.GetLocalIPAddress(), objdb.Office_ID(), ddlmonth.SelectedValue, ddlYear.SelectedValue }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());

                        }
                    }
                    openingForm();
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }
    protected void clrTxtPCC()
    {
        txtmilCC_PC.Text = "";
        txtMilkDairy_PC.Text = "";
        txtmainproduct_PC.Text = "";
        txtINDGproduct_PC.Text = "";
        txtTotal_PC.Text = "";
        txtsalaryallow_CC.Text = "";
        txtContlaboure_CC.Text = "";
        txtElectricity_CC.Text = "";
        txtCoalDiesel_CC.Text = "";
        txtConsumble_CC.Text = "";
        txtChemandDeter_CC.Text = "";
        txtRepairMAint_CC.Text = "";
        txtBMC_CC.Text = "";
        txtSecurity_CC.Text = "";
        txtOtherExps_CC.Text = "";
        txtTotal_CC.Text = "";
    }
    #endregion
    #region Marketing
    protected void btnMarketing_Click(object sender, EventArgs e)
    {
        try
        {

            lblMsg.Text = "";
            string msg = "";
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year. \\n";
            }
            if (ddlmonth.SelectedIndex == 0)
            {
                msg += "Select Month. \\n";
            }
            if (ddlform.SelectedIndex == 0)
            {
                msg += "Select Year. \\n";
            }
            //1
            if (txtsalaryAllow_MarkCostLM.Text == "") { txtsalaryAllow_MarkCostLM.Text = "0"; }
            if (txttranportation_MarkCostLM.Text == "") { txttranportation_MarkCostLM.Text = "0"; }
            if (txtsalesprom_MarkCostLM.Text == "") { txtsalesprom_MarkCostLM.Text = "0"; }
            if (txtservicecharg_MarkCostLM.Text == "") { txtservicecharg_MarkCostLM.Text = "0"; }
            if (txtAdvertise_MarkCostLM.Text == "") { txtAdvertise_MarkCostLM.Text = "0"; }
            if (txtAdvanceCard_MarkCostLM.Text == "") { txtAdvanceCard_MarkCostLM.Text = "0"; }
            if (txtContractlabour_MarkCostLM.Text == "") { txtContractlabour_MarkCostLM.Text = "0"; }
            if (txtOthers_MarkCostLM.Text == "") { txtOthers_MarkCostLM.Text = "0"; }
            if (txtTotal_MarkCostLM.Text == "") { txtTotal_MarkCostLM.Text = "0"; }
            //2
            if (txtsalryAllow_SMGNMG.Text == "") { txtsalryAllow_SMGNMG.Text = "0"; }
            if (txtTransport_SMGNMG.Text == "") { txtTransport_SMGNMG.Text = "0"; }
            if (txtServicCharg_SMGNMG.Text == "") { txtServicCharg_SMGNMG.Text = "0"; }
            if (txtOthers_SMGNMG.Text == "") { txtOthers_SMGNMG.Text = "0"; }
            if (txtTotal_SMGNMG.Text == "") { txtTotal_SMGNMG.Text = "0"; }
            if (txtsalryAndAllow_INDG.Text == "") { txtsalryAndAllow_INDG.Text = "0"; }
            if (txtTransport_INDG.Text == "") { txtTransport_INDG.Text = "0"; }
            if (txtServicCharg_INDG.Text == "") { txtServicCharg_INDG.Text = "0"; }
            if (txtOthersTax_INDG.Text == "") { txtOthersTax_INDG.Text = "0"; }
            if (txtTotal_INDG.Text == "") { txtTotal_INDG.Text = "0"; }
            if (txtTotalMKTG.Text == "") { txtTotalMKTG.Text = "0"; }
            if (msg == "")
            {
                ds = objdb.ByProcedure("SP_MonthlyCostingFINANCIAL_PERFORMANCE",
                           new string[] { "flag", "Office_ID", "Entry_Year", "Entry_Month", "SalaryAndAllow", "Transportation", "SalesPromotion", "MPCDFServiceCharge", "AdvertismentAndOhter", "AdvanceCard", "ContractLabour", "Others", "Total",
                           "SMG_Milk_SalaryAndAllow", "SMG_Milk_Transportation", "SMG_Milk_ServiceCharge", "SMG_Milk_Ohters", "SMG_Milk_Total", "MAIN_Prod_SalaryAndAllow", "MAIN_Prod_Transportation", "MAIN_Prod_ServiceCharge", "MAIN_Prod_OhterTax", "MAIN_Prod_Total", "Total_MKTG_Cost",
                           "CreatedBy", "CreatedIP"},
                           new string[] { "0", objdb.Office_ID(), ddlYear.SelectedValue, ddlmonth.SelectedValue, txtsalaryAllow_MarkCostLM.Text, txttranportation_MarkCostLM.Text, txtsalesprom_MarkCostLM.Text, txtservicecharg_MarkCostLM.Text, txtAdvertise_MarkCostLM.Text, txtAdvanceCard_MarkCostLM.Text, txtContractlabour_MarkCostLM.Text, txtOthers_MarkCostLM.Text, txtTotal_MarkCostLM.Text,
                           txtsalryAllow_SMGNMG.Text,txtTransport_SMGNMG.Text,txtServicCharg_SMGNMG.Text,txtOthers_SMGNMG.Text,txtTotal_SMGNMG.Text,txtsalryAndAllow_INDG.Text,txtTransport_INDG.Text,txtServicCharg_INDG.Text,txtOthersTax_INDG.Text,txtTotal_INDG.Text,txtTotalMKTG.Text,
                           objdb.createdBy(),objdb.GetLocalIPAddress()}, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());

                    }
                }
                openingForm();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }
    protected void MarkClear()
    {
        //1
        txtsalaryAllow_MarkCostLM.Text = "";
        txttranportation_MarkCostLM.Text = "";
        txtsalesprom_MarkCostLM.Text = "";
        txtservicecharg_MarkCostLM.Text = "";
        txtAdvertise_MarkCostLM.Text = "";
        txtAdvanceCard_MarkCostLM.Text = "";
        txtContractlabour_MarkCostLM.Text = "";
        txtOthers_MarkCostLM.Text = "";
        txtTotal_MarkCostLM.Text = "";
        //2
        txtsalryAllow_SMGNMG.Text = "";
        txtTransport_SMGNMG.Text = "";
        txtServicCharg_SMGNMG.Text = "";
        txtOthers_SMGNMG.Text = "";
        txtTotal_SMGNMG.Text = "";
        txtsalryAndAllow_INDG.Text = "";
        txtTransport_INDG.Text = "";
        txtServicCharg_INDG.Text = "";
        txtOthersTax_INDG.Text = "";
        txtTotal_INDG.Text = "";
        txtTotalMKTG.Text = "";

    }
    #endregion
    #region RowMaterialCost
    protected void btnroematerial_click(object sender, EventArgs e)
    {
        try
        {

            lblMsg.Text = "";
            string msg = "";
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year. \\n";
            }
            if (ddlmonth.SelectedIndex == 0)
            {
                msg += "Select Month. \\n";
            }
            if (ddlform.SelectedIndex == 0)
            {
                msg += "Select Year. \\n";
            }
            if (txtDCSMilkRMC.Text == "" || txtDCSMilkRMC.Text == ".") { txtDCSMilkRMC.Text = "0"; }
            if (txtSMGMilkRMC.Text == "") { txtSMGMilkRMC.Text = "0"; }
            if (txtNMGMilkRMC.Text == "") { txtNMGMilkRMC.Text = "0"; }
            if (txtOtherMilkRMC.Text == "") { txtOtherMilkRMC.Text = "0"; }
            if (txtCOMMUsedRMC.Text == "") { txtCOMMUsedRMC.Text = "0"; }
            if (txtTotalRMC.Text == "") { txtTotalRMC.Text = "0"; }
            if (txtDCSdairyCCimc.Text == "") { txtDCSdairyCCimc.Text = "0"; }
            if (txtccIMCtoDAIRYpt.Text == "") { txtccIMCtoDAIRYpt.Text = "0"; }
            if (txtSMGMILKPT.Text == "") { txtSMGMILKPT.Text = "0"; }
            if (txtNMGMILKpt.Text == "") { txtNMGMILKpt.Text = "0"; }
            if (txtTotalPT.Text == "") { txtTotalPT.Text = "0"; }
            if (msg == "")
            {
                ds = objdb.ByProcedure("SP_MonthlyCoasting_RawMaterialCost",
                           new string[] { "flag", "Office_ID", "Entry_Year", "Entry_Month","DCSmilkRMC","SMGMilkRMC","NMGMilkRMC","OtherMilkRMC","COMMUsedRMC","TotalRMC","DCSdairyCCimc","ccIMCtoDAIRYpt","SMGMILKPT","NMGMILKpt","TotalPT",
                           "CreatedBy", "CreatedIP"},
                           new string[] { "0", objdb.Office_ID(), ddlYear.SelectedValue, ddlmonth.SelectedValue,txtDCSMilkRMC.Text,txtSMGMilkRMC.Text,txtNMGMilkRMC.Text,txtOtherMilkRMC.Text,txtCOMMUsedRMC.Text,txtTotalRMC.Text,txtDCSdairyCCimc.Text,txtccIMCtoDAIRYpt.Text,txtSMGMILKPT.Text,txtNMGMILKpt.Text,txtTotalPT.Text,
                           objdb.createdBy(),objdb.GetLocalIPAddress()}, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());

                    }
                }
                openingForm();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }
    protected void Rawcostclr()
    {
        txtDCSMilkRMC.Text = "";
        txtSMGMilkRMC.Text = "";
        txtNMGMilkRMC.Text = "";
        txtOtherMilkRMC.Text = "";
        txtCOMMUsedRMC.Text = "";
        txtTotalRMC.Text = "";
        txtDCSdairyCCimc.Text = "";
        txtccIMCtoDAIRYpt.Text = "";
        txtSMGMILKPT.Text = "";
        txtNMGMILKpt.Text = "";
        txtTotalPT.Text = "";
    }
    #endregion
    #region Administration
    protected void btnAdministration_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            //ADM
            if (txtsalaryAndAllow_AD.Text == "") { txtsalaryAndAllow_AD.Text = "0"; }
            if (txtMedicalTA_AD.Text == "") { txtMedicalTA_AD.Text = "0"; }
            if (txtConveyence_AD.Text == "") { txtConveyence_AD.Text = "0"; }
            if (txtSecurity_AD.Text == "") { txtSecurity_AD.Text = "0"; }
            if (txtSupervisionVehic_AD.Text == "") { txtSupervisionVehic_AD.Text = "0"; }
            if (txtContractLabour_AD.Text == "") { txtContractLabour_AD.Text = "0"; }
            if (txtInsuranceOTH_AD.Text == "") { txtInsuranceOTH_AD.Text = "0"; }
            if (txtLegalAuditFee_AD.Text == "") { txtLegalAuditFee_AD.Text = "0"; }
            if (txtStationary_AD.Text == "") { txtStationary_AD.Text = "0"; }
            if (txtOther_AD.Text == "") { txtOther_AD.Text = "0"; }
            if (txtTotal_AD.Text == "") { txtTotal_AD.Text = "0"; }
            //PRO
            if (txtBonus_Provi.Text == "") { txtBonus_Provi.Text = "0"; }
            if (txtAuditFees_Provi.Text == "") { txtAuditFees_Provi.Text = "0"; }
            if (txtGroupGratutiy_Provi.Text == "") { txtGroupGratutiy_Provi.Text = "0"; }
            if (txtLiveires_Provi.Text == "") { txtLiveires_Provi.Text = "0"; }
            if (txtLeavesalary_Provi.Text == "") { txtLeavesalary_Provi.Text = "0"; }
            if (txtotherExps_Provi.Text == "") { txtotherExps_Provi.Text = "0"; }
            if (txtTotal_Provi.Text == "") { txtTotal_Provi.Text = "0"; }
            if (txtNDDB_LI.Text == "") { txtNDDB_LI.Text = "0"; }
            if (txtBANKLoan_LI.Text == "") { txtBANKLoan_LI.Text = "0"; }
            if (txtTotal_LI.Text == "") { txtTotal_LI.Text = "0"; }
            if (txtDepreciation.Text == "") { txtDepreciation.Text = "0"; }
            //PMC
            if (txtProductMaking.Text == "") { txtProductMaking.Text = "0"; }
            if (txtConversionCharge.Text == "") { txtConversionCharge.Text = "0"; }
            if (txtFPOther.Text == "") { txtFPOther.Text = "0"; }
            if (txtFPTotal.Text == "") { txtFPTotal.Text = "0"; }
            string msg = "";
            if (ddlYear.SelectedValue == "0")
            {
                msg += "Select Year. \\n";
            }
            if (ddlmonth.SelectedValue == "0")
            {
                msg += "Select Month. \\n";
            }
            if (ddlform.SelectedValue == "0")
            {
                msg += "Select Year. \\n";
            }
            if (msg == "")
            {
                if (ddlform.SelectedValue == "8")
                {
                    ds = objdb.ByProcedure("SP_MonthlyCosting_Administration",
                        new string[] { "Flag", "SalaryAndAllow_ADM","Medical_TA_ADM","Conveyance_ADM","ADM_Security_ADM","SupervisionVehicles_ADM","ContractLabour_ADM","InsuranceAndOTH_Taxes_ADM","LegalAuditFees_ADM","Stationery_ADM","Others_ADM","Total_ADM",
                                        "Bonus_PRO","AuditFees_PRO","GroupGratuity_PRO","Liveries_PRO","LeaveSalaryOfRetiredEmployees_PRO","OtherExps_PRO","Total_PRO","LI_NDDB_PRO","LI_BankLoan_PRO","LI_Total_PRO","depreciation_PRO",
                                        "ProductMaking_PMC","ConversionCharge_PMC","Other_PMC","Total_PMC",
                                         "CreatedBy", "CreatedIP", "Office_ID", "Entry_Month", "Entry_Year"},
                        new string[] { "1", txtsalaryAndAllow_AD.Text,txtMedicalTA_AD.Text,txtConveyence_AD.Text,txtSecurity_AD.Text,txtSupervisionVehic_AD.Text,txtContractLabour_AD.Text,txtInsuranceOTH_AD.Text,txtLegalAuditFee_AD.Text,txtStationary_AD.Text,txtOther_AD.Text,txtTotal_AD.Text,
                                        txtBonus_Provi.Text,txtAuditFees_Provi.Text,txtGroupGratutiy_Provi.Text,txtLiveires_Provi.Text,txtLeavesalary_Provi.Text,txtotherExps_Provi.Text,txtTotal_Provi.Text,txtNDDB_LI.Text,txtBANKLoan_LI.Text,txtTotal_LI.Text,txtDepreciation.Text,
                                        txtProductMaking.Text,txtConversionCharge.Text,txtFPOther.Text,txtFPTotal.Text,
                                         objdb.createdBy(), objdb.GetLocalIPAddress(), objdb.Office_ID(), ddlmonth.SelectedValue, ddlYear.SelectedValue}, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());

                        }
                    }
                    openingForm();
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }
    protected void clrTxtADM()
    {
        //ADM
        txtsalaryAndAllow_AD.Text = "";
        txtMedicalTA_AD.Text = "";
        txtConveyence_AD.Text = "";
        txtSecurity_AD.Text = "";
        txtSupervisionVehic_AD.Text = "";
        txtContractLabour_AD.Text = "";
        txtInsuranceOTH_AD.Text = "";
        txtLegalAuditFee_AD.Text = "";
        txtStationary_AD.Text = "";
        txtOther_AD.Text = "";
        txtTotal_AD.Text = "";
        //PRO
        txtBonus_Provi.Text = "";
        txtAuditFees_Provi.Text = "";
        txtGroupGratutiy_Provi.Text = "";
        txtLiveires_Provi.Text = "";
        txtLeavesalary_Provi.Text = "";
        txtotherExps_Provi.Text = "";
        txtTotal_Provi.Text = "";
        txtNDDB_LI.Text = "";
        txtBANKLoan_LI.Text = "";
        txtTotal_LI.Text = "";
        txtDepreciation.Text = "";
        //PMC
        txtProductMaking.Text = "";
        txtConversionCharge.Text = "";
        txtFPOther.Text = "";
        txtFPTotal.Text = "";
    }
    #endregion
    #region Receipt
    protected void btnReceipt_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year. \\n";
            }
            if (ddlmonth.SelectedIndex == 0)
            {
                msg += "Select Month. \\n";
            }
            if (ddlform.SelectedIndex == 0)
            {
                msg += "Select Year. \\n";
            }
            if (txtmilksaleTR.Text == "") { txtmilksaleTR.Text = "0"; }
            if (txtProductsTR.Text == "") { txtProductsTR.Text = "0"; }
            if (txtTotalSaleTR.Text == "") { txtTotalSaleTR.Text = "0"; }
            if (txtCommoditiesTR.Text == "") { txtCommoditiesTR.Text = "0"; }
            if (txtcommditiesPurchTR.Text == "") { txtcommditiesPurchTR.Text = "0"; }
            if (txtopeningStocksTR.Text == "") { txtopeningStocksTR.Text = "0"; }
            if (txtClosingStocksTR.Text == "") { txtClosingStocksTR.Text = "0"; }
            if (txtOtherIncomeTR.Text == "") { txtOtherIncomeTR.Text = "0"; }
            if (txtNetRecieptsTR.Text == "") { txtNetRecieptsTR.Text = "0"; }
            if (txtbeforIDATR.Text == "") { txtbeforIDATR.Text = "0"; }
            if (txtbeforDEFERDTR.Text == "") { txtbeforDEFERDTR.Text = "0"; }
            if (txtbeforDEPRECIATIONTR.Text == "") { txtbeforDEPRECIATIONTR.Text = "0"; }
            if (txtNETINCLUDEPTR.Text == "") { txtNETINCLUDEPTR.Text = "0"; }
            if (txttotalvarriCostTVC.Text == "") { txttotalvarriCostTVC.Text = "0"; }
            if (txtsalaryWages_TFCOSTTVC.Text == "") { txtsalaryWages_TFCOSTTVC.Text = "0"; }
            if (txtOthers_TFCOSTTVC.Text == "") { txtOthers_TFCOSTTVC.Text = "0"; }
            if (txttotalfixCostTVC.Text == "") { txttotalfixCostTVC.Text = "0"; }
            if (txtToCostTVC.Text == "") { txtToCostTVC.Text = "0"; }
            if (txtTFcostEXCLINTTTVC.Text == "") { txtTFcostEXCLINTTTVC.Text = "0"; }
            if (txtToSaleTVC.Text == "") { txtToSaleTVC.Text = "0"; }
            if (txtIDAOpertingProfTVC.Text == "") { txtIDAOpertingProfTVC.Text = "0"; }
            if (txtNEtIncluIDTTVC.Text == "") { txtNEtIncluIDTTVC.Text = "0"; }
            if (txtToSaleWithCFFTVC.Text == "") { txtToSaleWithCFFTVC.Text = "0"; }
            if (txtOPLOssProfitCFFTVC.Text == "") { txtOPLOssProfitCFFTVC.Text = "0"; }
            if (txtNETprofitLossTVC.Text == "") { txtNETprofitLossTVC.Text = "0"; }
            if (msg == "")
            {
                ds = objdb.ByProcedure("SP_MonthlyCosting_FINANCIAL_PERFORMANCE_rs",
                            new string[] { "flag", "Office_ID", "Entry_Year", "Entry_Month","TR_MilkSale","TR_Products","TR_TotalSale","TR_TotalCommoditiesUsed","TR_CommoditiesPurchReturns","TR_OpeningStocks","TR_ClosingStocks","TR_OtherIncome","TR_TotalNetReceipts","TR_SD_BeforeIdaOff","TR_SD_BeforeDeffered","TR_SD_BeforeDepreciation","TR_SD_NetIncluDep",
                                            "TotalVarriCost","TFC_SalaryAndWadges ","TFC_Other ","TFC_TotalFixedCost ","TFC_TotalCost ","TFC_TotalFixedCostExcludingINTT ","TFC_TotalSale ","TFC_BeforeIDAOfOperatingProfit ","TFC_NetIncudingIDT ","TFC_TotalSaleTurnOverWithCFF ","TFC_TotalOperatingLossProfitWithCFF ","TFC_NetProfitLossWithCFF",
                                            "CreatedBy","CreatedIP", },
                            new string[] { "0", objdb.Office_ID(), ddlYear.SelectedValue, ddlmonth.SelectedValue, txtmilksaleTR.Text, txtProductsTR.Text, txtTotalSaleTR.Text, txtCommoditiesTR.Text, txtcommditiesPurchTR.Text, txtopeningStocksTR.Text, txtClosingStocksTR.Text, txtOtherIncomeTR.Text, txtNetRecieptsTR.Text, txtbeforIDATR.Text, txtbeforDEFERDTR.Text, txtbeforDEPRECIATIONTR.Text, txtNETINCLUDEPTR.Text,
                                            txttotalvarriCostTVC.Text,txtsalaryWages_TFCOSTTVC.Text,txtOthers_TFCOSTTVC.Text,txttotalfixCostTVC.Text,txtToCostTVC.Text,txtTFcostEXCLINTTTVC.Text,txtToSaleTVC.Text,txtIDAOpertingProfTVC.Text,txtNEtIncluIDTTVC.Text,txtToSaleWithCFFTVC.Text,txtOPLOssProfitCFFTVC.Text,txtNETprofitLossTVC.Text,
                                            objdb.createdBy(),objdb.GetLocalIPAddress()}, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());

                    }
                }
                openingForm();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", msg);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }
    protected void FPClear()
    {
        txttotalvarriCostTVC.Text = "";
        txtsalaryWages_TFCOSTTVC.Text = "";
        txtOthers_TFCOSTTVC.Text = "";
        txttotalfixCostTVC.Text = "";
        txtToCostTVC.Text = "";
        txtTFcostEXCLINTTTVC.Text = "";
        txtToSaleTVC.Text = "";
        txtIDAOpertingProfTVC.Text = "";
        txtNEtIncluIDTTVC.Text = "";
        txtToSaleWithCFFTVC.Text = "";
        txtOPLOssProfitCFFTVC.Text = "";
        txtNETprofitLossTVC.Text = "";
        txtmilksaleTR.Text = "";
        txtProductsTR.Text = "";
        txtTotalSaleTR.Text = "";
        txtCommoditiesTR.Text = "";
        txtcommditiesPurchTR.Text = "";
        txtopeningStocksTR.Text = "";
        txtClosingStocksTR.Text = "";
        txtOtherIncomeTR.Text = "";
        txtNetRecieptsTR.Text = "";
        txtbeforIDATR.Text = "";
        txtbeforDEFERDTR.Text = "";
        txtbeforDEPRECIATIONTR.Text = "";
        txtNETINCLUDEPTR.Text = "";
    }
    #endregion
    #region CapUtilisation
    protected void btnCapUtilisation_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (txtthruoputwithoutWC_PC.Text == "") { txtthruoputwithoutWC_PC.Text = "0"; }
            if (txtthroughpuINKGS_PC.Text == "") { txtthroughpuINKGS_PC.Text = "0"; }
            if (txtthroughpuINLTS_PC.Text == "") { txtthroughpuINLTS_PC.Text = "0"; }
            if (txtthroughputPERDAY_PC.Text == "") { txtthroughputPERDAY_PC.Text = "0"; }
            if (txtcapacityutilisationINKGS_PC.Text == "") { txtcapacityutilisationINKGS_PC.Text = "0"; }
            if (txtAllCCsthruoput_CC.Text == "") { txtAllCCsthruoput_CC.Text = "0"; }
            if (txtcapacityuti_CC.Text == "") { txtcapacityuti_CC.Text = "0"; }
            if (txtbcfsale_BMC.Text == "") { txtbcfsale_BMC.Text = "0"; }
            if (txtbcfProdCFF_BMC.Text == "") { txtbcfProdCFF_BMC.Text = "0"; }
            if (txtcapacityuti_BMC.Text == "") { txtcapacityuti_BMC.Text = "0"; }
            if (txtSMPProd_SMP.Text == "") { txtSMPProd_SMP.Text = "0"; }
            if (txtcapacityuti_SMP.Text == "") { txtcapacityuti_SMP.Text = "0"; }
            string msg = "";
            if (ddlYear.SelectedValue == "0")
            {
                msg += "Select Year. \\n";
            }
            if (ddlmonth.SelectedValue == "0")
            {
                msg += "Select Month. \\n";
            }
            if (ddlform.SelectedValue == "0")
            {
                msg += "Select Year. \\n";
            }
            if (msg == "")
            {
                if (ddlform.SelectedValue == "10")
                {
                    ds = objdb.ByProcedure("SP_MonthlyCosting_CapacityUtilisation",
                        new string[] { "Flag","ThroughPutInKGWithoutWC_CU","ThroughPutInKGs_CU","ThroughPutInLTs_CU","ThroughPutPerDay_CU","CapacityUtilisationIn_PerAGE_CU","CC_ALLCCsThroughPut_CU","CC_CapacityUtilisationInPerAge_CU","BCFSaleCFF_CU","BCFPRODCFF_CU","BCF_CapacityUtilisationInPerAge_CU","SMPProdNInMTs_CU","SMP_CapacityUtilisationInPerAge_CU",
                                         "CreatedBy", "CreatedIP", "Office_ID", "Entry_Month", "Entry_Year"},
                        new string[] { "1", txtthruoputwithoutWC_PC.Text,txtthroughpuINKGS_PC.Text,txtthroughpuINLTS_PC.Text,txtthroughputPERDAY_PC.Text,txtcapacityutilisationINKGS_PC.Text,txtAllCCsthruoput_CC.Text,txtcapacityuti_CC.Text,txtbcfsale_BMC.Text,txtbcfProdCFF_BMC.Text,txtcapacityuti_BMC.Text,txtSMPProd_SMP.Text,txtcapacityuti_SMP.Text,
                                         objdb.createdBy(), objdb.GetLocalIPAddress(), objdb.Office_ID(), ddlmonth.SelectedValue, ddlYear.SelectedValue}, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());

                        }
                    }
                    openingForm();
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", msg);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }
    protected void clrTxtCU()
    {
        txtthruoputwithoutWC_PC.Text = "";
        txtthroughpuINKGS_PC.Text = "";
        txtthroughpuINLTS_PC.Text = "";
        txtthroughputPERDAY_PC.Text = "";
        txtcapacityutilisationINKGS_PC.Text = "";
        txtAllCCsthruoput_CC.Text = "";
        txtcapacityuti_CC.Text = "";
        txtbcfsale_BMC.Text = "";
        txtbcfProdCFF_BMC.Text = "";
        txtcapacityuti_BMC.Text = "";
        txtSMPProd_SMP.Text = "";
        txtcapacityuti_SMP.Text = "";
    }
    #endregion
    #region materialbalancing
    protected void btnmaterialbalancing_Click(object sender, EventArgs e)
    {
        try
        {
            // MPTI
            if (txtdcsmilkcow_MP.Text == "") { txtdcsmilkcow_MP.Text = "0"; }
            if (txtdcsmilkbuff_MP.Text == "") { txtdcsmilkbuff_MP.Text = "0"; }
            if (txtdcsmilktotal_MP.Text == "") { txtdcsmilktotal_MP.Text = "0"; }
            if (txtfat_MP.Text == "") { txtfat_MP.Text = "0"; }
            if (txtsnf_MP.Text == "") { txtsnf_MP.Text = "0"; }
            if (txtfatpercent_MP.Text == "") { txtfatpercent_MP.Text = "0"; }
            if (txtsnfpercent_MP.Text == "") { txtsnfpercent_MP.Text = "0"; }
            if (txtquantityinkgs_TI.Text == "") { txtquantityinkgs_TI.Text = "0"; }
            if (txtfatinkgs_TI.Text == "") { txtfatinkgs_TI.Text = "0"; }
            if (txtsnfinkgs_TI.Text == "") { txtsnfinkgs_TI.Text = "0"; }
            if (txtvalueinrs_TI.Text == "") { txtvalueinrs_TI.Text = "0"; }
            // TOMG
            if (txtquantityinkgs_TO.Text == "") { txtquantityinkgs_TO.Text = "0"; }
            if (txtfatinkgs_TO.Text == "") { txtfatinkgs_TO.Text = "0"; }
            if (txtsnfinKgs_TO.Text == "") { txtsnfinKgs_TO.Text = "0"; }
            if (txtValueinrs_To.Text == "") { txtValueinrs_To.Text = "0"; }
            if (txtfatinkgs_MG.Text == "") { txtfatinkgs_MG.Text = "0"; }
            if (txtsngfinkgs_MG.Text == "") { txtsngfinkgs_MG.Text = "0"; }
            if (txtfatpercentage_MG.Text == "") { txtfatpercentage_MG.Text = "0"; }
            if (txtsnfpercentage_MG.Text == "") { txtsnfpercentage_MG.Text = "0"; }
            string msg = "";
            if (ddlYear.SelectedValue == "0")
            {
                msg += "Select Year. \\n";
            }
            if (ddlmonth.SelectedValue == "0")
            {
                msg += "Select Month. \\n";
            }
            if (ddlform.SelectedValue == "0")
            {
                msg += "Select Year. \\n";
            }
            if (msg == "")
            {
                if (ddlform.SelectedValue == "11")
                {
                    ds = objdb.ByProcedure("SP_MonthlyCosting_MaterialBalancing",
                        new string[] { "Flag","MP_DCSmilkCow_MPTI","MP_DCSmilkBuff_MPTI","MP_DCSmilkTotal_MPTI","MP_FAT_MPTI","MP_SNF_MPTI","MP_FATper_MPTI", "MP_SNFper_MPTI","MP_TI_QntInKG_MPTI", "MP_TI_FATkg_MPTI","MP_TI_SNFkg_MPTI", "MP_TI_ValueinRS_MPTI",
                                      "TO_QntinKG_TOMG","TO_FatinKG_TOMG","TO_SNFinKG_TOMG", "TO_ValueinRS_TOMG", "TO_MG_FatinKG_TOMG", "TO_MG_SNFinKG_TOMG", "TO_MG_FATinPR_TOMG", "TO_MG_SNFinPR_TOMG",  
                                       "CreatedBy", "CreatedIP", "Office_ID", "Entry_Month", "Entry_Year"},
                        new string[] { "1",txtdcsmilkcow_MP.Text,txtdcsmilkbuff_MP.Text,txtdcsmilktotal_MP.Text,txtfat_MP.Text,txtsnf_MP.Text,txtfatpercent_MP.Text,txtsnfpercent_MP.Text,txtquantityinkgs_TI.Text,txtfatinkgs_TI.Text,txtsnfinkgs_TI.Text,txtvalueinrs_TI.Text,
                                        txtquantityinkgs_TO.Text,txtfatinkgs_TO.Text,txtsnfinKgs_TO.Text,txtValueinrs_To.Text,txtfatinkgs_MG.Text,txtsngfinkgs_MG.Text,txtfatpercentage_MG.Text,txtsnfpercentage_MG.Text,
                                         objdb.createdBy(), objdb.GetLocalIPAddress(), objdb.Office_ID(), ddlmonth.SelectedValue, ddlYear.SelectedValue}, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-exclamation-triangle", "alert-warning", "Warning! ", ds.Tables[0].Rows[0]["Errormsg"].ToString());

                        }
                    }
                    openingForm();
                }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", msg);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }
    protected void clrTxtMB()
    {
        txtdcsmilkcow_MP.Text = "";
        txtdcsmilkbuff_MP.Text = "";
        txtdcsmilktotal_MP.Text = "";
        txtfat_MP.Text = "";
        txtsnf_MP.Text = "";
        txtfatpercent_MP.Text = "";
        txtsnfpercent_MP.Text = "";
        txtquantityinkgs_TI.Text = "";
        txtfatinkgs_TI.Text = "";
        txtsnfinkgs_TI.Text = "";
        txtvalueinrs_TI.Text = "";

        txtquantityinkgs_TO.Text = "";
        txtfatinkgs_TO.Text = "";
        txtsnfinKgs_TO.Text = "";
        txtValueinrs_To.Text = "";
        txtfatinkgs_MG.Text = "";
        txtsngfinkgs_MG.Text = "";
        txtfatpercentage_MG.Text = "";
        txtsnfpercentage_MG.Text = "";
    }
    #endregion
    protected void ddlform_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        if (Convert.ToInt16(ddlmonth.SelectedValue) == DateTime.Now.Month || Convert.ToInt16(ddlmonth.SelectedValue) == (DateTime.Now.Month) - 1 && Convert.ToInt16(ddlYear.SelectedValue) == DateTime.Now.Year)
        {

            btnAdministration.Visible = true;
            btnCapUtilisation.Visible = true;
            btnFO.Visible = true;
            btnMarketing.Visible = true;
            btnmaterialbalancing.Visible = true;
            btnMOP.Visible = true;
            btnPackagingAndCC.Visible = true;
            btnPMandSale.Visible = true;
            btnPPMaking.Visible = true;
            btnReceipt.Visible = true;
            btnrecombination.Visible = true;
            btnroematerial.Visible = true;

        }
            //if (Convert.ToInt16(ddlmonth.SelectedValue) == DateTime.Now.Month || Convert.ToInt16(ddlmonth.SelectedValue) == (DateTime.Now.Month) - 1)
        //{
        //    if (Convert.ToInt16(ddlYear.SelectedValue) == DateTime.Now.Year)
        //    {
        //        btnAdministration.Visible = true;
        //        btnCapUtilisation.Visible = true;
        //        btnFO.Visible = true;
        //        btnMarketing.Visible = true;
        //        btnmaterialbalancing.Visible = true;
        //        btnMOP.Visible = true;
        //        btnPackagingAndCC.Visible = true;
        //        btnPMandSale.Visible = true;
        //        btnPPMaking.Visible = true;
        //        btnReceipt.Visible = true;
        //        btnrecombination.Visible = true;
        //        btnroematerial.Visible = true;
        //    }
        //}


        else
        {
            btnAdministration.Visible = false;
            btnCapUtilisation.Visible = false;
            btnFO.Visible = false;
            btnMarketing.Visible = false;
            btnmaterialbalancing.Visible = false;
            btnMOP.Visible = false;
            btnPackagingAndCC.Visible = false;
            btnPMandSale.Visible = false;
            btnPPMaking.Visible = false;
            btnReceipt.Visible = false;
            btnrecombination.Visible = false;
            btnroematerial.Visible = false;
        }
        openingForm();
    }
}