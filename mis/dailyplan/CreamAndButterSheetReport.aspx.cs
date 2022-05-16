using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_dailyplan_CreamAndButterSheetReport : System.Web.UI.Page
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

                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillOffice();
                    GetSectionView(sender, e);
                    txtFDate.Text = Convert.ToDateTime(System.DateTime.Now, cult).ToString("dd/MM/yyyy");
                    txtTDate.Text = Convert.ToDateTime(System.DateTime.Now, cult).ToString("dd/MM/yyyy");

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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string Fdate = "";
            string Tdate = "";
            DivTable.InnerHtml = "";
            if (txtFDate.Text != "" && txtTDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtFDate.Text, cult).ToString("yyyy/MM/dd");
                Tdate = Convert.ToDateTime(txtTDate.Text, cult).ToString("yyyy/MM/dd");
            }

            ds = null;
            ds = objdb.ByProcedure("Sp_Production_ButterSheet",
                  new string[] { "flag", "Office_ID", "FDate","TDate", "TID_Cream", "TID_Butter" },
                  new string[] { "5", ddlDS.SelectedValue, Fdate,Tdate, objdb.LooseCreamItemTypeId_ID(), objdb.LooseButterItemTypeId_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    int CreamCount = Convert.ToInt32(ds.Tables[0].Rows[0]["Cream_Count"].ToString());
                    int Butter_Count = Convert.ToInt32(ds.Tables[1].Rows[0]["Butter_Count"].ToString());

                    if (CreamCount >= 1 && Butter_Count == 0)
                    {
                        GetCreamData();
                    }
                    if (CreamCount == 0 && Butter_Count >= 1)
                    {
                        GetButterData();
                    }
                    if (CreamCount >= 1 && Butter_Count >= 1)
                    {
                        GetCreamORButterData();
                    }
                    if (CreamCount == 0 && Butter_Count == 0)
                    {
                        //BlankCreamAndButterSheet();
                        DivTable.InnerHtml = "Cream & Butter Accounting Sheet Data Not Availble For This Date.";
                    }

                }
                else
                {
                    //BlankCreamAndButterSheet();
                    DivTable.InnerHtml = "Cream & Butter Accounting Sheet Data Not Availble For This Date.";
                }
            }
            else
            {
                //BlankCreamAndButterSheet();
                DivTable.InnerHtml = "Cream & Butter Accounting Sheet Data Not Availble For This Date.";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    public void GetButterData()
    {
        try
        {



            DataSet dsVD_Child_Rpt_Butter = objdb.ByProcedure("Sp_Production_ButterSheet",
                new string[] { "flag", "Office_ID", "FDate","TDate", "ProductSection_ID", "ItemType_id"},
                new string[] { "4", objdb.Office_ID(), Convert.ToDateTime(txtFDate.Text,cult).ToString("yyyy/MM/dd"),Convert.ToDateTime(txtFDate.Text,cult).ToString("yyyy/MM/dd"), ddlPSection.SelectedValue,objdb.LooseButterItemTypeId_ID() }, "dataset");

                if (dsVD_Child_Rpt_Butter != null && dsVD_Child_Rpt_Butter.Tables[0].Rows.Count > 0)
                {

                    int Count_Butter = dsVD_Child_Rpt_Butter.Tables[0].Rows.Count;

                    // int Count_Butter_Inner = dsVD_Child_Rpt_Butter.Tables[1].Rows.Count;

                    decimal QtyTotal = 0;
                    decimal FatTotal = 0;
                    decimal Snftotal = 0;

                    QtyTotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["OB_CC"].ToString())
                         + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["OB_Loose"].ToString())
                         + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["OB_Cold_Room1"].ToString())
                         + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["OB_Cold_Room2"].ToString())
                         + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["WB_Mfg_Good"].ToString())
                         + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["WB_Mfg_Sour"].ToString())
                         + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["Recieved_from_CC_1"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["Recieved_from_CC_2"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["Recieved_from_CC_3"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["Recieved_from_FP"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["Recieved_from_others"].ToString());

                    FatTotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["OB_CC"].ToString())
                     + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["OB_Loose"].ToString())
                     + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["OB_Cold_Room1"].ToString())
                     + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["OB_Cold_Room2"].ToString())
                     + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["WB_Mfg_Good"].ToString())
                     + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["WB_Mfg_Sour"].ToString())
                     + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["Recieved_from_CC_1"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["Recieved_from_CC_2"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["Recieved_from_CC_3"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["Recieved_from_FP"].ToString())
                       + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["Recieved_from_others"].ToString());

                    Snftotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["OB_CC"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["OB_Loose"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["OB_Cold_Room1"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["OB_Cold_Room2"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["WB_Mfg_Good"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["WB_Mfg_Sour"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["Recieved_from_CC_1"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["Recieved_from_CC_2"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["Recieved_from_CC_3"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["Recieved_from_FP"].ToString())
                       + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["Recieved_from_others"].ToString());


                    StringBuilder htmlStr = new StringBuilder();

                    htmlStr.Append("<div class='row'>");
                     htmlStr.Append("<div class='col-md-12' style='text-align:center'><h3 style='font-weight: 800; font-size: 20px;'>"+Session["Office_Name"]+"</h3><h5 style='font-weight: 500; font-size: 13px;'>DAIRY PLANT</h5><h5 style='font-weight: 800; font-size: 20px;'>CREAM AND BUTTER PRODUCT SHEET</h5></div>");
                    htmlStr.Append("<div class='col-md-12'></div>");

                    /********** Table Start******/
                    htmlStr.Append("<div class='row'>");
                    htmlStr.Append("<div class='col-md-12'>");
                    htmlStr.Append("<table class='datatable table table-striped table-bordered table-hover' style='width:100%;'>");
                    htmlStr.Append("<tr class='text-center'>");
                    htmlStr.Append("<th>CREAM ACCOUNT</th>");

                    htmlStr.Append("<th>Quantity Kgs.</th>");
                    htmlStr.Append("<th>Fat. Kgs.</th>");
                    htmlStr.Append("<th>SNF Kgs.</th>");

                    htmlStr.Append("<th>BUTTER</th>");
                    htmlStr.Append("<th>Quantity Kgs.</th>");
                    htmlStr.Append("<th>Fat. Kgs.</th>");
                    htmlStr.Append("<th>SNF Kgs.</th>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>OB</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>OB of CC (a)</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["OB_CC"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Good</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>OB of Loose</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["OB_Loose"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Sour</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>OB in Cold Room (1)</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["OB_Cold_Room1"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");


                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Cream received from Processing Section</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>OB in Cold Room (2)</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["OB_Cold_Room2"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Good</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>WB mfd. Good</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["WB_Mfg_Good"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Sour</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>WB mfd. Good</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["WB_Mfg_Sour"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");


                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Cream from Others Sources</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Received from CC 1 (b)</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Recieved_from_CC_1"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Good</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Received from CC 2 (c)</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Recieved_from_CC_2"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Sour</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Received from CC 3 (d)</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Recieved_from_CC_3"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Received from FP</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Recieved_from_FP"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Received from Others</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Recieved_from_others"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr style='background-color:#ff874c; border:1px solid #ff874c; color:#000; font-weight:700' >");

                    htmlStr.Append("<td style='text-align:right'>TOTAL</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>TOTAL</td>");
                    htmlStr.Append("<td>" + QtyTotal + "</td>");
                    htmlStr.Append("<td>" + FatTotal + "</td>");
                    htmlStr.Append("<td>" + Snftotal + "</td>");

                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr style='font-weight:700;text-align:center;' >");

                    htmlStr.Append("<td>DISPOSAL</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>DISPOSAL</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Issued For WB Good</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    
                    htmlStr.Append("<td>Issue to Processing</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Issue_to_Processing"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Issue for WB Sour</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    
                    htmlStr.Append("<td>Issue for Ghee </td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Issue_for_Ghee"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");

                    htmlStr.Append("<td>Issue to Others</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    
                    htmlStr.Append("<td>Butter Milk </td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Butter_Milk"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>CB Good</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    
                    htmlStr.Append("<td>Issue to Others</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Issue_to_other"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>CB Sour</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                   
                    htmlStr.Append("<td>Issue for FP</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Issue_for_FP"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");


                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Sample</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Issue for Sale</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Issue_for_Sale"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Sample</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Sample"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>CB of CC </td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CB_CC"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>CB Loose</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CB_Loose"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");


                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>CB Cold Room 1</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CB_Cold_Room1"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");


                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>CB Cold Room 2</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CB_Cold_Room2"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr style='background-color:#ff874c; border:1px solid #ff874c; color:#000; font-weight:700' >");

                    htmlStr.Append("<td style='text-align:right'>TOTAL</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td style='text-align:right'>TOTAL</td>");


                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + (
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Issue_to_Processing"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Issue_for_Ghee"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Butter_Milk"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Issue_to_other"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Issue_for_FP"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Issue_for_Sale"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CB_CC"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CB_Loose"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CB_Cold_Room1"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CB_Cold_Room2"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Sample"].ToString()))
                            + "</td>");
                    }


                    htmlStr.Append("</tr>");

                    htmlStr.Append("</table>");
                    htmlStr.Append("</div></div>");
                    htmlStr.Append("</div></div>");
                    htmlStr.Append("<div class='col-md-12 footerprint'><div class='col-md-4 col-sm-4 col-xs-4'><h5>D.G.M. Production</h5></div><div class='col-md-6  col-sm-6 col-xs-6 text-center'><h5>Manager(Product)</h5></div><div class='col-md-2  col-sm-2 col-xs-2'><h5>A.G.M. Production</h5></div></div>");
                    htmlStr.Append("</div>");
                    DivTable.InnerHtml = htmlStr.ToString();

                }
            

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    public void GetCreamData()
    {

        try
        {


            DataSet dsVD_Child_Rpt = objdb.ByProcedure("Sp_Production_CreamSheet",
                new string[] { "flag", "Office_ID", "FDate","TDate", "ProductSection_ID", "ItemType_id" },
                new string[] { "4", objdb.Office_ID(), Convert.ToDateTime(txtFDate.Text,cult).ToString("yyyy/MM/dd"),Convert.ToDateTime(txtTDate.Text,cult).ToString("yyyy/MM/dd"), ddlPSection.SelectedValue,objdb.LooseCreamItemTypeId_ID() }, "dataset");

                if (dsVD_Child_Rpt != null && dsVD_Child_Rpt.Tables[0].Rows.Count > 0)
                {

                    //string strTypeName = dsVD_Child_Rpt.Tables[0].Rows[0]["ItemTypeName"].ToString();
                    //lblTopTitle.Text = strTypeName;

                    int Count = dsVD_Child_Rpt.Tables[0].Rows.Count;

                    StringBuilder htmlStr = new StringBuilder();

                    htmlStr.Append("<div class='row'>");
                    htmlStr.Append("<div class='col-md-12' style='text-align:center'><h3 style='font-weight: 800; font-size: 20px;'>"+Session["Office_Name"]+"</h3><h5 style='font-weight: 500; font-size: 13px;'>DAIRY PLANT</h5><h5 style='font-weight: 800; font-size: 20px;'>CREAM AND BUTTER PRODUCT SHEET</h5></div>");
                    htmlStr.Append("<div class='col-md-12'></div>");

                    /********** Table Start******/
                    htmlStr.Append("<div class='row'>");
                    htmlStr.Append("<div class='col-md-12'>");
                    htmlStr.Append("<table class='datatable table table-striped table-bordered table-hover' style='width:100%;'>");
                    htmlStr.Append("<tr class='text-center'>");
                    htmlStr.Append("<th>CREAM ACCOUNT</th>");

                    htmlStr.Append("<th>Quantity Kgs.</th>");
                    htmlStr.Append("<th>Fat. Kgs.</th>");
                    htmlStr.Append("<th>SNF Kgs.</th>");


                    //htmlStr.Append("<th>Quantity Kgs.</th>");
                    //htmlStr.Append("<th>Fat. Kgs.</th>");
                    //htmlStr.Append("<th>SNF Kgs.</th>");
                    htmlStr.Append("<th>BUTTER</th>");
                    htmlStr.Append("<th>Quantity Kgs.</th>");
                    htmlStr.Append("<th>Fat. Kgs.</th>");
                    htmlStr.Append("<th>SNF Kgs.</th>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>OB</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>OB of CC (a)</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Good</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["OB_Good"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>OB of Loose</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Sour</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["OB_Sour"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>OB in Cold Room (1)</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");


                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Cream received from Processing Section</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>OB in Cold Room (2)</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Good</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["Cream_received_from_Processing_Section_Good"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>WB mfd. Good</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Sour</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["Cream_received_from_Processing_Section_Sour"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>WB mfd. Good</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");


                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Cream from Others Sources</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Received from CC 1 (b)</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Good</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["Cream_from_Others_Sources_Good"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>Received from CC 2 (c)</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Sour</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["Cream_from_Others_Sources_Sour"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>Received from CC 3 (d)</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");
      
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Received from FP</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Received from Others</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");


                    htmlStr.Append("<tr style='background-color:#ff874c; border:1px solid #ff874c; color:#000; font-weight:700' >");

                    htmlStr.Append("<td style='text-align:right'>TOTAL</td>");

                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + (Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["OB_Good"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["OB_Sour"].ToString())
                             + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["Cream_received_from_Processing_Section_Good"].ToString())
                              + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["Cream_received_from_Processing_Section_Sour"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["Cream_from_Others_Sources_Good"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["Cream_from_Others_Sources_Sour"].ToString()))
                            + "</td>");
                    }

                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");

                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr style='font-weight:700;text-align:center;' >");

                    htmlStr.Append("<td>DISPOSAL</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>DISPOSAL</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Issued For WB Good</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["Issue_for_WB_Good"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>Issue to Processing</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Issue for WB Sour</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["Issue_for_WB_Sour"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>Issue for Ghee </td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                  
                    htmlStr.Append("<td>Issue to Others</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["Issue_to_other"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>Butter Milk </td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");                   
                    htmlStr.Append("<td>CB Good</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CB_Good"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>Issue to Others</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");                   
                   htmlStr.Append("<td>CB Sour</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CB_Sour"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>Issue for FP</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    
                    
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Sample</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["Sample"].ToString() + "</td>");
                    }             
                    htmlStr.Append("<td>Issue for Sale</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Sample</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>CB of CC </td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>CB Loose</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");


                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>CB Cold Room 1</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");


                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>CB Cold Room 2</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");
                    
                    htmlStr.Append("<tr>");                  
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr style='background-color:#ff874c; border:1px solid #ff874c; color:#000; font-weight:700' >");

                    htmlStr.Append("<td style='text-align:right'>TOTAL</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + (Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["Issue_for_WB_Good"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["Issue_for_WB_Sour"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["Issue_to_other"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["CB_Good"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["CB_Sour"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["Sample"].ToString())
                            )
                            + "</td>");
                    }
                    htmlStr.Append("<td style='text-align:right'>TOTAL</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");

                    htmlStr.Append("</tr>");

                    htmlStr.Append("</table>");
                    htmlStr.Append("</div></div>");
                    htmlStr.Append("</div></div>");
                    htmlStr.Append("<div class='col-md-12 footerprint'><div class='col-md-4 col-sm-4 col-xs-4'><h5>D.G.M. Production</h5></div><div class='col-md-6  col-sm-6 col-xs-6 text-center'><h5>Manager(Product)</h5></div><div class='col-md-2  col-sm-2 col-xs-2'><h5>A.G.M. Production</h5></div></div>");
                    htmlStr.Append("</div>");
                    DivTable.InnerHtml = htmlStr.ToString();


                }
                else
                {
                    DivTable.InnerHtml = "No Record Found";
                }

            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    public void GetCreamORButterData()
    {

        try
        {



            DataSet dsVD_Child_Rpt = objdb.ByProcedure("Sp_Production_CreamSheet",
                new string[] { "flag", "Office_ID", "FDate","TDate", "ProductSection_ID", "ItemType_id"},
                new string[] { "4", objdb.Office_ID(), Convert.ToDateTime(txtFDate.Text,cult).ToString("yyyy/MM/dd"),Convert.ToDateTime(txtTDate.Text,cult).ToString("yyyy/MM/dd"), ddlPSection.SelectedValue,objdb.LooseCreamItemTypeId_ID()}, "dataset");

                if (dsVD_Child_Rpt != null && dsVD_Child_Rpt.Tables[0].Rows.Count > 0)
                {

                    //string strTypeName = dsVD_Child_Rpt.Tables[0].Rows[0]["ItemTypeName"].ToString();
                    //lblTopTitle.Text = strTypeName;



                    DataSet dsVD_Child_Rpt_Butter = objdb.ByProcedure("Sp_Production_ButterSheet",
                new string[] { "flag", "Office_ID", "FDate","TDate", "ProductSection_ID", "ItemType_id"},
                new string[] { "4", objdb.Office_ID(), Convert.ToDateTime(txtFDate.Text,cult).ToString("yyyy/MM/dd"),Convert.ToDateTime(txtTDate.Text,cult).ToString("yyyy/MM/dd"), ddlPSection.SelectedValue,objdb.LooseButterItemTypeId_ID()}, "dataset");

                        if (dsVD_Child_Rpt_Butter != null && dsVD_Child_Rpt_Butter.Tables[0].Rows.Count > 0)
                        {

                            int Count = dsVD_Child_Rpt.Tables[0].Rows.Count;

                            int Count_Butter = dsVD_Child_Rpt_Butter.Tables[0].Rows.Count;

                            // int Count_Butter_Inner = dsVD_Child_Rpt_Butter.Tables[1].Rows.Count;

                            decimal QtyTotal = 0;
                            decimal FatTotal = 0;
                            decimal Snftotal = 0;

                            QtyTotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["OB_CC"].ToString())
                         + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["OB_Loose"].ToString())
                         + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["OB_Cold_Room1"].ToString())
                         + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["OB_Cold_Room2"].ToString())
                         + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["WB_Mfg_Good"].ToString())
                         + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["WB_Mfg_Sour"].ToString())
                         + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["Recieved_from_CC_1"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["Recieved_from_CC_2"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["Recieved_from_CC_3"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["Recieved_from_FP"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["Recieved_from_others"].ToString());

                            FatTotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["OB_CC"].ToString())
                             + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["OB_Loose"].ToString())
                             + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["OB_Cold_Room1"].ToString())
                             + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["OB_Cold_Room2"].ToString())
                             + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["WB_Mfg_Good"].ToString())
                             + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["WB_Mfg_Sour"].ToString())
                             + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["Recieved_from_CC_1"].ToString())
                              + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["Recieved_from_CC_2"].ToString())
                              + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["Recieved_from_CC_3"].ToString())
                              + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["Recieved_from_FP"].ToString())
                               + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["Recieved_from_others"].ToString());

                            Snftotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["OB_CC"].ToString())
                                + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["OB_Loose"].ToString())
                                + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["OB_Cold_Room1"].ToString())
                                + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["OB_Cold_Room2"].ToString())
                                + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["WB_Mfg_Good"].ToString())
                                + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["WB_Mfg_Sour"].ToString())
                                + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["Recieved_from_CC_1"].ToString())
                              + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["Recieved_from_CC_2"].ToString())
                              + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["Recieved_from_CC_3"].ToString())
                              + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["Recieved_from_FP"].ToString())
                               + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["Recieved_from_others"].ToString());


                            StringBuilder htmlStr = new StringBuilder();

                            htmlStr.Append("<div class='row'>");
                            htmlStr.Append("<div class='col-md-12' style='text-align:center'><h3 style='font-weight: 800; font-size: 20px;'>" + Session["Office_Name"] + "</h3><h5 style='font-weight: 500; font-size: 13px;'>DAIRY PLANT</h5><h5 style='font-weight: 800; font-size: 20px;'>CREAM AND BUTTER PRODUCT SHEET</h5></div>");
                            htmlStr.Append("<div class='col-md-12'></div>");

                            /********** Table Start******/
                            htmlStr.Append("<div class='row'>");
                            htmlStr.Append("<div class='col-md-12'>");
                            htmlStr.Append("<table class='datatable table table-striped table-bordered table-hover' style='width:100%;'>");
                            htmlStr.Append("<tr class='text-center'>");
                            htmlStr.Append("<th>CREAM ACCOUNT</th>");

                            htmlStr.Append("<th>Quantity Kgs.</th>");
                            htmlStr.Append("<th>Fat. Kgs.</th>");
                            htmlStr.Append("<th>SNF Kgs.</th>");

                            htmlStr.Append("<th>BUTTER</th>");
                            htmlStr.Append("<th>Quantity Kgs.</th>");
                            htmlStr.Append("<th>Fat. Kgs.</th>");
                            htmlStr.Append("<th>SNF Kgs.</th>");
                            htmlStr.Append("</tr>");
                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>OB</td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td>OB of CC (a)</td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["OB_CC"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");

                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>Good</td>");
                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["OB_Good"].ToString() + "</td>");
                            }
                            htmlStr.Append("<td>OB of Loose</td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["OB_Loose"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");

                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>Sour</td>");
                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["OB_Sour"].ToString() + "</td>");
                            }
                            htmlStr.Append("<td>OB in Cold Room (1)</td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["OB_Cold_Room1"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");


                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>Cream received from Processing Section</td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td>OB in Cold Room (2)</td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["OB_Cold_Room2"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");

                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>Good</td>");
                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["Cream_received_from_Processing_Section_Good"].ToString() + "</td>");
                            }
                            htmlStr.Append("<td>WB mfd. Good</td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["WB_Mfg_Good"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");

                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>Sour</td>");
                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["Cream_received_from_Processing_Section_Sour"].ToString() + "</td>");
                            }
                            htmlStr.Append("<td>WB mfd. Good</td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["WB_Mfg_Sour"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");


                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>Cream from Others Sources</td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td>Received from CC 1 (b)</td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Recieved_from_CC_1"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");

                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>Good</td>");
                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["Cream_from_Others_Sources_Good"].ToString() + "</td>");
                            }
                            htmlStr.Append("<td>Received from CC 2 (c)</td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Recieved_from_CC_2"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");

                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>Sour</td>");
                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["Cream_from_Others_Sources_Sour"].ToString() + "</td>");
                            }
                            htmlStr.Append("<td>Received from CC 3 (d)</td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Recieved_from_CC_3"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");

                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td>Received from FP</td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Recieved_from_FP"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");

                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td>Received from Others</td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Recieved_from_others"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");

                            

                            htmlStr.Append("<tr style='background-color:#ff874c; border:1px solid #ff874c; color:#000; font-weight:700' >");

                            htmlStr.Append("<td style='text-align:right'>TOTAL</td>");

                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td > " + (Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["OB_Good"].ToString())
                                    + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["OB_Sour"].ToString())
                                     + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["Cream_received_from_Processing_Section_Good"].ToString())
                                      + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["Cream_received_from_Processing_Section_Sour"].ToString())
                                    + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["Cream_from_Others_Sources_Good"].ToString())
                                    + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["Cream_from_Others_Sources_Sour"].ToString()))
                                    + "</td>");
                            }

                            htmlStr.Append("<td style='text-align:right'>TOTAL</td>");
                            htmlStr.Append("<td>" + QtyTotal + "</td>");
                            htmlStr.Append("<td>" + FatTotal + "</td>");
                            htmlStr.Append("<td>" + Snftotal + "</td>");

                            htmlStr.Append("</tr>");
                            htmlStr.Append("<tr style='font-weight:700;text-align:center;' >");

                            htmlStr.Append("<td>DISPOSAL</td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td>DISPOSAL</td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("</tr>");
                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>Issued For WB Good</td>");
                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["Issue_for_WB_Good"].ToString() + "</td>");
                            }

                            htmlStr.Append("<td>Issue to Processing</td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Issue_to_Processing"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");

                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>Issue for WB Sour</td>");
                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["Issue_for_WB_Sour"].ToString() + "</td>");
                            }

                            htmlStr.Append("<td>Issue for Ghee </td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Issue_for_Ghee"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");

                            htmlStr.Append("<tr>");

                            htmlStr.Append("<td>Issue to Others</td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Issue_to_other"].ToString() + "</td>");
                            }

                            htmlStr.Append("<td>Butter Milk </td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Butter_Milk"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");

                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>CB Good</td>");
                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CB_Good"].ToString() + "</td>");
                            }

                            htmlStr.Append("<td>Issue to Others</td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Issue_to_other"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");

                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>CB Sour</td>");
                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CB_Sour"].ToString() + "</td>");
                            }

                            htmlStr.Append("<td>Issue for FP</td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Issue_for_FP"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");
                            htmlStr.Append("<tr>");


                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td>Sample</td>");
                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["Sample"].ToString() + "</td>");
                            }

                            htmlStr.Append("<td>Issue for Sale</td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Issue_for_Sale"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");
                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td>Sample</td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["Sample"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");
                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td>CB of CC </td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CB_CC"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");

                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td>CB Loose</td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CB_Loose"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");


                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td>CB Cold Room 1</td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CB_Cold_Room1"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");


                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td></td>");
                            htmlStr.Append("<td>CB Cold Room 2</td>");
                            for (int i = 0; i < Count_Butter; i++)
                            {
                                htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CB_Cold_Room2"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");

                            htmlStr.Append("<tr style='background-color:#ff874c; border:1px solid #ff874c; color:#000; font-weight:700' >");

                            htmlStr.Append("<td style='text-align:right'>TOTAL</td>");
                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td > " + (Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["Issue_for_WB_Good"].ToString())
                                    + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["Issue_for_WB_Sour"].ToString())
                                    + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["Issue_to_other"].ToString())
                                    + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["CB_Good"].ToString())
                                    + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["CB_Sour"].ToString())
                                    + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["Sample"].ToString())
                                    )
                                    + "</td>");
                            }
                            htmlStr.Append("<td style='text-align:right'>TOTAL</td>");

                            htmlStr.Append("<td>" + QtyTotal + "</td>");
                            htmlStr.Append("<td>" + FatTotal + "</td>");
                            htmlStr.Append("<td>" + Snftotal + "</td>");


                          

                            htmlStr.Append("</tr>");

                            htmlStr.Append("</table>");
                            htmlStr.Append("</div></div>");
                            htmlStr.Append("</div></div>");
                            htmlStr.Append("<div class='col-md-12 footerprint'><div class='col-md-4 col-sm-4 col-xs-4'><h5>D.G.M. Production</h5></div><div class='col-md-6  col-sm-6 col-xs-6 text-center'><h5>Manager(Product)</h5></div><div class='col-md-2  col-sm-2 col-xs-2'><h5>A.G.M. Production</h5></div></div>");
                            htmlStr.Append("</div>");
                            DivTable.InnerHtml = htmlStr.ToString();

                        }
                    

                }
                else
                {
                    DivTable.InnerHtml = "No Record Found";
                }

           

        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void BlankCreamAndButterSheet()
    {
        StringBuilder htmlStr = new StringBuilder();

        htmlStr.Append("<div class='row'>");
        htmlStr.Append("<div class='col-md-12' style='text-align:center'><h3 style='font-weight: 800; font-size: 20px;'>BHOPAL SAHAKARI DUGDH SANGH MARYADIT</h3><h5 style='font-weight: 500; font-size: 13px;'>BHOPAL DAIRY PLANT HABIBGANJ - BHOPAL</h5><h5 style='font-weight: 800; font-size: 20px;'>CREAM AND BUTTER PRODUCT SHEET</h5></div>");
        htmlStr.Append("<div class='col-md-12'></div>");

        /********** Table Start******/
        htmlStr.Append("<div class='row'>");
        htmlStr.Append("<div class='col-md-12'>");
        htmlStr.Append("<table class='datatable table table-striped table-bordered table-hover' style='width:100%;'>");
        htmlStr.Append("<tr class='text-center'>");
        htmlStr.Append("<th>CREAM ACCOUNT</th>");
        htmlStr.Append("<th>Quantity Kgs.</th>");
        htmlStr.Append("<th>Fat. Kgs.</th>");
        htmlStr.Append("<th>SNF Kgs.</th>");
        htmlStr.Append("<th>BUTTER</th>");
        htmlStr.Append("<th>Quantity Kgs.</th>");
        htmlStr.Append("<th>Fat. Kgs.</th>");
        htmlStr.Append("<th>SNF Kgs.</th>");
        htmlStr.Append("</tr>");
        htmlStr.Append("<tr>");
        htmlStr.Append("<td>OB</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>OB of CC (a)</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td>Good</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>OB of Loose</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td>Sour</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>OB in Cold Room (1)</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");


        htmlStr.Append("<tr>");
        htmlStr.Append("<td>Cream received from Processing Section</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>OB in Cold Room (2)</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td>Good</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>WB mfd. Good</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td>Sour</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>WB mfd. Good</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");


        htmlStr.Append("<tr>");
        htmlStr.Append("<td>Cream from Others Sources</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>Received from CC 1 (b)</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td>Good</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>Received from CC 2 (c)</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td>Sour</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>Received from CC 3 (d)</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>Received from FP</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>Received from Others</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");


        htmlStr.Append("<tr style='background-color:#ff874c; border:1px solid #ff874c; color:#000; font-weight:700' >");

        htmlStr.Append("<td style='text-align:right'>TOTAL</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");

        htmlStr.Append("</tr>");
        htmlStr.Append("<tr style='font-weight:700;text-align:center;' >");

        htmlStr.Append("<td>DISPOSAL</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>DISPOSAL</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");
        htmlStr.Append("<tr>");
        htmlStr.Append("<td>Issued For WB Good</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>Issue to Processing</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td>Issue for WB Sour</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>Issue for Ghee </td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");

        htmlStr.Append("<td>Issue to Others</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>Butter Milk </td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td>CB Good</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>Issue to Others</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td>CB Sour</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>Issue for FP</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");
        htmlStr.Append("<tr>");


        htmlStr.Append("<tr>");
        htmlStr.Append("<td>Sample</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>Issue for Sale</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>Sample</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>CB of CC </td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>CB Loose</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");


        htmlStr.Append("<tr>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>CB Cold Room 1</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");


        htmlStr.Append("<tr>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>CB Cold Room 2</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");
        htmlStr.Append("<tr style='background-color:#ff874c; border:1px solid #ff874c; color:#000; font-weight:700' >");

        htmlStr.Append("<td style='text-align:right'>TOTAL</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td style='text-align:right'>TOTAL</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");

        htmlStr.Append("</tr>");

        htmlStr.Append("</table>");
        htmlStr.Append("</div></div>");
        htmlStr.Append("</div></div>");
        htmlStr.Append("<div class='col-md-12 footerprint'><div class='col-md-4 col-sm-4 col-xs-4'><h5>D.G.M. Production</h5></div><div class='col-md-6  col-sm-6 col-xs-6 text-center'><h5>Manager(Product)</h5></div><div class='col-md-2  col-sm-2 col-xs-2'><h5>A.G.M. Production</h5></div></div>");
        htmlStr.Append("</div>");
        DivTable.InnerHtml = htmlStr.ToString();
    }
}