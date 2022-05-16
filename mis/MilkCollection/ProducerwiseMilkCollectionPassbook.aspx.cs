using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;

 
public partial class mis_MilkCollection_ProducerwiseMilkCollectionPassbook : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                //FillSociety();
                FillProducerName();

                txtFdt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                txtTdt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");

            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }



    protected void FillProducerName()
    {
        try
        {
            // lblMsg.Text = "";
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "Office_Id"},
                              new string[] { "3", objdb.Office_ID() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlFarmer.DataTextField = "ProducerName";
                    ddlFarmer.DataValueField = "ProducerId";
                    ddlFarmer.DataSource = ds;
                    ddlFarmer.DataBind();
                    ddlFarmer.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ddlFarmer.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlFarmer.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    //protected void FillSociety()
    //{
    //    try
    //    {
    //        // lblMsg.Text = "";
    //        ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
    //                          new string[] { "flag", "Office_ID" },
    //                          new string[] { "10", ViewState["Office_ID"].ToString() }, "dataset");

    //        if (ds.Tables.Count > 0)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                lblProducername.Text = "कैलाश त्यागी" + " / " + "P0001";
    //                lblOfficename.Text = "टिटोरा / DCS0001";
    //                lblbankInfo.Text = "SBIN000 / SBI / 000000000001";
    //                lblBillingPeriod.Text = "01/04/2020 To 10/04/2020";
    //            }
    //            else
    //            {
    //                //ddlFarmer.Items.Insert(0, new ListItem("Select", "0"));
    //            }
    //        }
    //        else
    //        {
    //            //  ddlFarmer.Items.Insert(0, new ListItem("Select", "0"));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    protected void btnYes_Click(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                FS_DailyReport.Visible = false;
                FS_DailyReport_Shift.Visible = false;
                string Fdate = "";
                string Tdate = "";

                if (txtFdt.Text != "")
                {
                    Fdate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
                }

                if (txtTdt.Text != "")
                {
                    Tdate = Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd");
                }

                ds = null;
                ds = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                            new string[] { "flag", "ProducerId", "FDT", "TDT", "OfficeId" },
                            new string[] { "8", ddlFarmer.SelectedValue, Fdate, Tdate, objdb.Office_ID() }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblProducername.Text = ds.Tables[0].Rows[0]["ProducerName"].ToString() + " / " + ds.Tables[0].Rows[0]["UserName"].ToString();
                            lblbankInfo.Text = ds.Tables[0].Rows[0]["BankBranch"].ToString() + " / " + ds.Tables[0].Rows[0]["IFSC"].ToString() + " / " + ds.Tables[0].Rows[0]["AccountNo"].ToString();
                            lblOfficename.Text = ds.Tables[0].Rows[0]["Office_Name"].ToString() + " / " + ds.Tables[0].Rows[0]["Office_Code"].ToString();
                            lblBillingPeriod.Text = ds.Tables[0].Rows[0]["FromDate"].ToString() + " To " + ds.Tables[0].Rows[0]["ToDate"].ToString();

                            if (ds.Tables[1].Rows.Count != 0)
                            {
                                if (ds.Tables[1].Rows.Count > 0)
                                {

                                    FS_DailyReport.Visible = true;
                                    gv_ProducerMilkDetail.DataSource = ds.Tables[1];
                                    gv_ProducerMilkDetail.DataBind();


                                    if (ds.Tables[2].Rows.Count != 0)
                                    {
                                        if (ds.Tables[2].Rows.Count > 0)
                                        {
                                            Label lblTotal_Fat = (gv_ProducerMilkDetail.FooterRow.FindControl("lblTotal_Fat") as Label);
                                            Label lblTotal_SNF = (gv_ProducerMilkDetail.FooterRow.FindControl("lblTotal_SNF") as Label);
                                            Label lblTotal_CLR = (gv_ProducerMilkDetail.FooterRow.FindControl("lblTotal_CLR") as Label);

                                            Label lblI_MilkSupplyQty = (gv_ProducerMilkDetail.FooterRow.FindControl("lblI_MilkSupplyQty") as Label);

                                            Label lblTotal_FAT_IN_KG = (gv_ProducerMilkDetail.FooterRow.FindControl("lblTotal_FAT_IN_KG") as Label);
                                            Label lblTotal_SNF_IN_KG = (gv_ProducerMilkDetail.FooterRow.FindControl("lblTotal_SNF_IN_KG") as Label);
                                            Label lblNetValue = (gv_ProducerMilkDetail.FooterRow.FindControl("lblNetValue") as Label);

                                            lblTotal_Fat.Text = ds.Tables[2].Rows[0]["Total_Fat"].ToString();
                                            lblTotal_SNF.Text = ds.Tables[2].Rows[0]["Total_SNF"].ToString();
                                            lblTotal_CLR.Text = ds.Tables[2].Rows[0]["Total_CLR"].ToString();
                                            lblI_MilkSupplyQty.Text = ds.Tables[2].Rows[0]["I_MilkSupplyQty"].ToString();
                                            lblTotal_FAT_IN_KG.Text = ds.Tables[2].Rows[0]["Total_FAT_IN_KG"].ToString();
                                            lblTotal_SNF_IN_KG.Text = ds.Tables[2].Rows[0]["Total_SNF_IN_KG"].ToString();
                                            lblNetValue.Text = ds.Tables[2].Rows[0]["NetValue"].ToString();

                                            lblMilkValue.Text = ds.Tables[2].Rows[0]["NetValue"].ToString();

                                            if (ds.Tables[5].Rows.Count != 0)
                                            {
                                                if (ds.Tables[5].Rows.Count > 0)
                                                {
                                                    lblProductSaleValue.Text = ds.Tables[5].Rows[0]["NetPurchaseAmount"].ToString();
                                                }
                                                else
                                                {
                                                    lblProductSaleValue.Text = "0.00";
                                                }

                                            }
                                            else
                                            {
                                                lblProductSaleValue.Text = "0.00";
                                            }
                                            if (ds.Tables[7].Rows.Count != 0)
                                            {
                                                if (ds.Tables[7].Rows.Count > 0)
                                                {
                                                    lblEarningValue.Text = (ds.Tables[7].AsEnumerable().Sum(row => row.Field<decimal>("NetAmount"))).ToString();
                                                }
                                                else
                                                {
                                                    lblEarningValue.Text = "0.00";
                                                }

                                            }
                                            else
                                            {
                                                lblEarningValue.Text = "0.00";
                                            }
                                            if (ds.Tables[8].Rows.Count != 0)
                                            {
                                                if (ds.Tables[8].Rows.Count > 0)
                                                {
                                                    lblAdjustAmount.Text = ds.Tables[8].Rows[0]["AdjustAmount"].ToString();
                                                }
                                                else
                                                {
                                                    lblAdjustAmount.Text = "0.00";
                                                }

                                            }
                                            else
                                            {
                                                lblAdjustAmount.Text = "0.00";
                                            }
                                            lblGrossEarning.Text = (Convert.ToDecimal(lblMilkValue.Text) + Convert.ToDecimal(lblEarningValue.Text) - Convert.ToDecimal(lblProductSaleValue.Text) - Convert.ToDecimal(lblAdjustAmount.Text)).ToString();
                                             

                                        }

                                        else
                                        {
                                            FS_DailyReport.Visible = false;
                                            gv_ProducerMilkDetail.DataSource = string.Empty;
                                            gv_ProducerMilkDetail.DataBind();
                                        }
                                    }

                                    else
                                    {
                                        FS_DailyReport.Visible = false;
                                        gv_ProducerMilkDetail.DataSource = string.Empty;
                                        gv_ProducerMilkDetail.DataBind();
                                    }



                                    // Table For 3 For Morning Shift

                                    if (ds.Tables[3].Rows.Count != 0)
                                    {
                                        if (ds.Tables[3].Rows.Count > 0)
                                        {

                                            FS_DailyReport_Shift.Visible = true;
                                            gv_ProducerMorningData.DataSource = ds.Tables[3];
                                            gv_ProducerMorningData.DataBind();

                                        }

                                        else
                                        {
                                            FS_DailyReport_Shift.Visible = false;
                                            gv_ProducerMorningData.DataSource = string.Empty;
                                            gv_ProducerMorningData.DataBind();
                                        }
                                    }

                                    else
                                    {
                                        //FS_DailyReport_Shift.Visible = false;
                                        gv_ProducerMorningData.DataSource = string.Empty;
                                        gv_ProducerMorningData.DataBind();
                                    }


                                    // Table For 4 For Evening Shift

                                    if (ds.Tables[4].Rows.Count != 0)
                                    {
                                        if (ds.Tables[4].Rows.Count > 0)
                                        {
                                            FS_DailyReport_Shift.Visible = true;
                                            gv_ProducerEveningData.DataSource = ds.Tables[4];
                                            gv_ProducerEveningData.DataBind();
                                        }
                                        else
                                        {
                                            FS_DailyReport_Shift.Visible = false;
                                            gv_ProducerEveningData.DataSource = string.Empty;
                                            gv_ProducerEveningData.DataBind();
                                        }
                                    }
                                    else
                                    {
                                       // FS_DailyReport_Shift.Visible = false;
                                        gv_ProducerEveningData.DataSource = string.Empty;
                                        gv_ProducerEveningData.DataBind();
                                    }


                                    // Table For 6 For Sale Detail

                                    if (ds.Tables[6].Rows.Count != 0)
                                    {
                                        if (ds.Tables[6].Rows.Count > 0)
                                        {
                                            FS_DailyReport_Shift.Visible = true;
                                            gvGetSaleDetails.DataSource = ds.Tables[6];
                                            gvGetSaleDetails.DataBind();
                                        }
                                        else
                                        {
                                            //FS_DailyReport_Shift.Visible = false;
                                            gvGetSaleDetails.DataSource = string.Empty;
                                            gvGetSaleDetails.DataBind();
                                        }
                                    }
                                    else
                                    {
                                       // FS_DailyReport_Shift.Visible = false;
                                        gvGetSaleDetails.DataSource = string.Empty;
                                        gvGetSaleDetails.DataBind();
                                    }

                                    // Table For 7 For Earning Detail

                                    if (ds.Tables[7].Rows.Count != 0)
                                    {
                                        if (ds.Tables[7].Rows.Count > 0)
                                        {
                                            FS_DailyReport_Shift.Visible = true;
                                            gv_EarningDetail.DataSource = ds.Tables[7];
                                            gv_EarningDetail.DataBind();
                                        }
                                        else
                                        {
                                            //FS_DailyReport_Shift.Visible = false;
                                            gv_EarningDetail.DataSource = string.Empty;
                                            gv_EarningDetail.DataBind();
                                        }
                                    }
                                    else
                                    {
                                        // FS_DailyReport_Shift.Visible = false;
                                        gv_EarningDetail.DataSource = string.Empty;
                                        gv_EarningDetail.DataBind();
                                    }

                                }
                                else
                                {
                                    FS_DailyReport.Visible = false;
                                    gv_ProducerMilkDetail.DataSource = string.Empty;
                                    gv_ProducerMilkDetail.DataBind();
                                }
                            }

                            else
                            {
                                FS_DailyReport.Visible = false;
                                gv_ProducerMilkDetail.DataSource = string.Empty;
                                gv_ProducerMilkDetail.DataBind();
                            }





                        }
                        else
                        {
                            lblProducername.Text = "";
                            lblbankInfo.Text = "";
                            lblOfficename.Text = "";
                            lblBillingPeriod.Text = "";
                            FS_DailyReport_Shift.Visible = false;
                            FS_DailyReport.Visible = false;
                        }
                    }
                    else
                    {
                        lblProducername.Text = "";
                        lblbankInfo.Text = "";
                        lblOfficename.Text = "";
                        lblBillingPeriod.Text = "";
                        FS_DailyReport_Shift.Visible = false;
                        FS_DailyReport.Visible = false;
                    }
                }
                else
                {
                    lblmsgshow.Visible = true;
                    lblmsgshow.Text = "No Record Found";
                     
                    lblProducername.Text = "";
                    lblbankInfo.Text = "";
                    lblOfficename.Text = "";
                    lblBillingPeriod.Text = "";
                    FS_DailyReport_Shift.Visible = false;
                    FS_DailyReport.Visible = false;
                }



            }

            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void gv_ProducerMorningData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int RowSpan = 3;
        for (int i = gv_ProducerMorningData.Rows.Count - 2; i >= 0; i--)
        {
            GridViewRow currRow = gv_ProducerMorningData.Rows[i];
            GridViewRow prevRow = gv_ProducerMorningData.Rows[i + 1];
            if (currRow.Cells[0].Text == prevRow.Cells[0].Text)
            {
                currRow.Cells[0].RowSpan = RowSpan;
                prevRow.Cells[0].Visible = false;
                RowSpan += 1;
            }
            else
            {
                RowSpan = 2;
            }
        }
    }

    protected void gv_ProducerEveningData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int RowSpan = 3;
        for (int i = gv_ProducerEveningData.Rows.Count - 2; i >= 0; i--)
        {
            GridViewRow currRow = gv_ProducerEveningData.Rows[i];
            GridViewRow prevRow = gv_ProducerEveningData.Rows[i + 1];
            if (currRow.Cells[0].Text == prevRow.Cells[0].Text)
            {
                currRow.Cells[0].RowSpan = RowSpan;
                prevRow.Cells[0].Visible = false;
                RowSpan += 1;
            }
            else
            {
                RowSpan = 2;
            }
        }
    }
}