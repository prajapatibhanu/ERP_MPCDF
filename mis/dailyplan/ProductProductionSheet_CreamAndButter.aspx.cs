using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_dailyplan_ProductProductionSheet_CreamAndButter : System.Web.UI.Page
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

                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillOffice();
                    GetSectionView(sender, e);
                    txtDate.Text = Convert.ToDateTime(System.DateTime.Now, cult).ToString("dd/MM/yyyy");

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
            btnExport.Visible = false;
            btnprint.Visible = false;
            
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            ds = null;
            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut_ProductSheet",
                  new string[] { "flag", "Office_ID", "Date", "TID_Cream", "TID_Butter" },
                  new string[] { "14", ddlDS.SelectedValue, Fdate, objdb.LooseCreamItemTypeId_ID(), objdb.LooseButterItemTypeId_ID() }, "dataset");

            if (ds != null)
            {
                btnExport.Visible = true;
                btnprint.Visible = true;
                if (ds.Tables.Count > 0)
                {
                    int CreamCount = Convert.ToInt32(ds.Tables[0].Rows[0]["Cream_Count"].ToString());
                    int Butter_Count = Convert.ToInt32(ds.Tables[1].Rows[0]["Butter_Count"].ToString());

                    if (CreamCount == 1 && Butter_Count == 0)
                    {
                        GetCreamData();
                    }
                    if (CreamCount == 0 && Butter_Count == 1)
                    {
                        GetButterData();
                    }
                    if (CreamCount == 1 && Butter_Count == 1)
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



            DataSet dsVD_Child_Rpt_Butter = objdb.ByProcedure("SpProductionProduct_Milk_InOut_ProductSheet",
            new string[] { "flag", "Office_ID", "FDate", "TDate", "ProductSection_ID", "ItemType_id" },
            new string[] { "17", objdb.Office_ID(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), ddlPSection.SelectedValue, objdb.LooseButterItemTypeId_ID() }, "dataset");

            if (dsVD_Child_Rpt_Butter != null && dsVD_Child_Rpt_Butter.Tables[0].Rows.Count > 0)
            {

                int Count_Butter = dsVD_Child_Rpt_Butter.Tables[0].Rows.Count;

                int Count_Butter_Inner = dsVD_Child_Rpt_Butter.Tables[1].Rows.Count;

                decimal QtyTotal = 0;
                decimal FatTotal = 0;
                decimal Snftotal = 0;
                decimal QtyInFlowTotal = 0;
                decimal FatInFlowTotal = 0;
                decimal SnfInFlowtotal = 0;
                decimal QtyOutFlowTotal = 0;
                decimal FatOutFlowTotal = 0;
                decimal SnfOutFlowtotal = 0;

                QtyTotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterCCOpening"].ToString())
                     + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["CreamRcvdForWhiteButterGood"].ToString())
                     + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["CreamRcvdForWhiteButterSour"].ToString())
                     + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["CreamRcvdForCookingButter"].ToString())
                     + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["CreamRcvdForTableButter"].ToString())
                     + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterRcvdFromCC"].ToString())
                     + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterRcvdFromFinishedProducts"].ToString())
                       + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterLooseOpening"].ToString())
                       + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterVE2Opening"].ToString())
                       + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterVE1Opening"].ToString());

                FatTotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterCCOpening"].ToString())
                 + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["CreamRcvdForWhiteButterGood"].ToString())
                 + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["CreamRcvdForWhiteButterSour"].ToString())
                 + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["CreamRcvdForCookingButter"].ToString())
                 + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["CreamRcvdForTableButter"].ToString())
                 + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterRcvdFromCC"].ToString())
                 + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterRcvdFromFinishedProducts"].ToString())
                  + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterLooseOpening"].ToString())
                  + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterVE2Opening"].ToString())
                  + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterVE1Opening"].ToString());

                Snftotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterCCOpening"].ToString())
                    + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["CreamRcvdForWhiteButterGood"].ToString())
                    + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["CreamRcvdForWhiteButterSour"].ToString())
                    + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["CreamRcvdForCookingButter"].ToString())
                    + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["CreamRcvdForTableButter"].ToString())
                    + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterRcvdFromCC"].ToString())
                    + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterRcvdFromFinishedProducts"].ToString())
                  + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterLooseOpening"].ToString())
                  + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterVE2Opening"].ToString())
                  + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterVE1Opening"].ToString());


                StringBuilder htmlStr = new StringBuilder();

                htmlStr.Append("<div class='row'>");
                htmlStr.Append("<div class='col-md-12' style='text-align:center'><h3 style='font-weight: 800; font-size: 20px;'>BHOPAL SAHAKARI DUGDH SANGH MARYADIT</h3><h5 style='font-weight: 500; font-size: 13px;'>BHOPAL DAIRY PLANT HABIBGANJ - BHOPAL</h5><h5 style='font-weight: 800; font-size: 20px;'>CREAM AND BUTTER PRODUCT SHEET</h5></div>");
                htmlStr.Append("<div class='col-md-12' style='text-align:center'>Date:" + txtDate.Text  + "</div>");

                /********** Table Start******/

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

                htmlStr.Append("<tr>");
                htmlStr.Append("<td>B.F.(Good)</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");

                htmlStr.Append("<td>1. (a) C.C.</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterCCOpening"].ToString() + "</td>");

                }
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td>B.F.(Sour)</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td>(b) Loose</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterLooseOpening"].ToString() + "</td>");
                }
                htmlStr.Append("</tr>");



                htmlStr.Append("<tr>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td>(c)-VE 2</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterVE2Opening"].ToString() + "</td>");
                }
                htmlStr.Append("</tr>");


                htmlStr.Append("<tr>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td>(d)-VE 1</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterVE1Opening"].ToString() + "</td>");
                }
                htmlStr.Append("</tr>");


                htmlStr.Append("<tr>");
                htmlStr.Append("<td>Cream Obtained From</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Cream Received For White Butter Good</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CreamRcvdForWhiteButterGood"].ToString() + "</td>");
                }
                //if (dsVD_Child_Rpt_Butter.Tables[1].Rows.Count > 0)
                //{
                //    int rowcount = dsVD_Child_Rpt_Butter.Tables[1].Rows.Count;

                //    QtyTotal += dsVD_Child_Rpt_Butter.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("QuantityInKg"));
                //    FatTotal += dsVD_Child_Rpt_Butter.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                //    Snftotal += dsVD_Child_Rpt_Butter.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));


                //    if (rowcount >= 1)
                //    {
                //        //htmlStr.Append("<td>2. " + dsVD_Child_Rpt_Butter.Tables[1].Rows[0]["MfdFrom"].ToString() + "</td>");
                //        //htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[0]["QuantityInKg"].ToString() + "</td>");
                //        //htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[0]["FatInKg"].ToString() + "</td>");
                //        //htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[0]["SnfInKg"].ToString() + "</td>");

                //    }
                //    else
                //    {
                //        //htmlStr.Append("<td>2. White Butter Mfd. Goods</td>");
                //        //htmlStr.Append("<td></td>");
                //        //htmlStr.Append("<td></td>");
                //        //htmlStr.Append("<td></td>");

                //    }

                //}
                //else
                //{
                //    htmlStr.Append("<td>2. White Butter Mfd. Goods</td>");
                //    htmlStr.Append("<td></td>");
                //    htmlStr.Append("<td></td>");
                //    htmlStr.Append("<td></td>");

                //}



                htmlStr.Append("</tr>");


                htmlStr.Append("<tr>");
                htmlStr.Append("<td>(a) Good Milk</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Cream Received For White Butter Sour</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CreamRcvdForWhiteButterSour"].ToString() + "</td>");
                }
                //if (dsVD_Child_Rpt_Butter.Tables[1].Rows.Count > 0)
                //{
                //    int rowcount = dsVD_Child_Rpt_Butter.Tables[1].Rows.Count;

                //    if (rowcount >= 2)
                //    {
                //        htmlStr.Append("<td>3. " + dsVD_Child_Rpt_Butter.Tables[1].Rows[1]["MfdFrom"].ToString() + "</td>");
                //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[1]["QuantityInKg"].ToString() + "</td>");
                //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[1]["FatInKg"].ToString() + "</td>");
                //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[1]["SnfInKg"].ToString() + "</td>");
                //    }
                //    else
                //    {
                //        htmlStr.Append("<td>3. White Butter Mfd. Sour</td>");
                //        htmlStr.Append("<td></td>");
                //        htmlStr.Append("<td></td>");
                //        htmlStr.Append("<td></td>");

                //    }

                //}
                //else
                //{
                //    htmlStr.Append("<td>3. White Butter Mfd. Sour</td>");
                //    htmlStr.Append("<td></td>");
                //    htmlStr.Append("<td></td>");
                //    htmlStr.Append("<td></td>");

                //}

                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td>(b) Sour Milk</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Cream Received For Cooking Butter</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CreamRcvdForCookingButter"].ToString() + "</td>");
                }
                //if (dsVD_Child_Rpt_Butter.Tables[1].Rows.Count > 0)
                //{
                //    int rowcount = dsVD_Child_Rpt_Butter.Tables[1].Rows.Count;

                //    if (rowcount >= 3)
                //    {
                //        htmlStr.Append("<td>4. " + dsVD_Child_Rpt_Butter.Tables[1].Rows[2]["MfdFrom"].ToString() + "</td>");
                //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[2]["QuantityInKg"].ToString() + "</td>");
                //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[2]["FatInKg"].ToString() + "</td>");
                //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[2]["SnfInKg"].ToString() + "</td>");
                //    }
                //    else
                //    {
                //        htmlStr.Append("<td>4. ..........</td>");
                //        htmlStr.Append("<td></td>");
                //        htmlStr.Append("<td></td>");
                //        htmlStr.Append("<td></td>");

                //    }

                //}
                //else
                //{
                //    htmlStr.Append("<td>4. White Butter Mfd. Sour</td>");
                //    htmlStr.Append("<td></td>");
                //    htmlStr.Append("<td></td>");
                //    htmlStr.Append("<td></td>");

                //}

                htmlStr.Append("<tr>");
                htmlStr.Append("<td>Cream Received From processing Section</td>");

                htmlStr.Append("<td></td>");//htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");//htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");//htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Cream Received For Table Butter</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CreamRcvdForTableButter"].ToString() + "</td>");
                }
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td></td>");

                htmlStr.Append("<td></td>");//htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");//htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");//htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Butter Received From CC</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterRcvdFromCC"].ToString() + "</td>");
                }
                htmlStr.Append("</tr>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<td></td>");

                htmlStr.Append("<td></td>");//htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");//htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");//htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Butter Received From Finished Products</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterRcvdFromFinishedProducts"].ToString() + "</td>");
                }
                htmlStr.Append("</tr>");

                //if (Count_Butter_Inner > 3)
                //{
                //    for (int i = 3; i < (Count_Butter_Inner); i++)
                //    {
                //        htmlStr.Append("<tr>");
                //        htmlStr.Append("<td></td>");
                //        htmlStr.Append("<td></td>");
                //        htmlStr.Append("<td></td>");
                //        htmlStr.Append("<td></td>");

                //        htmlStr.Append("<td>" + (i + 2) + ". " + dsVD_Child_Rpt_Butter.Tables[1].Rows[i]["MfdFrom"].ToString() + "</td>");
                //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[i]["QuantityInKg"].ToString() + "</td>");
                //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[i]["FatInKg"].ToString() + "</td>");
                //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[i]["SnfInKg"].ToString() + "</td>");
                //        htmlStr.Append("</tr>");
                //    }
                //}

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
                htmlStr.Append("<td>Issued For White Butter Goods</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td>White Butter Mfg</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["WhiteButterMfg"].ToString() + "</td>");
                }
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td>Issued For White Butter Sour</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Table Butter Mfg</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["TableButterMfg"].ToString() + "</td>");
                }
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td>Issue For Table Butter</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Cooking Butter Mfg</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CookingButterMfg"].ToString() + "</td>");
                }
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td>Issue For Cooking Butter</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Isued To Processing</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterIssueToProcessing"].ToString() + "</td>");
                }
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td>Return to Processing Section</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Issued For Ghee</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterIssueForGhee"].ToString() + "</td>");
                }
                htmlStr.Append("</tr>");
                htmlStr.Append("</tr>");


                htmlStr.Append("<tr>");
                htmlStr.Append("<td>White Butter Sour Milk</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Issue For Store</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterIssueForStore"].ToString() + "</td>");
                }
                htmlStr.Append("</tr>");
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td>White Butter Curdle Milk</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Issue For Butter Milk</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterIssueForSweetButterMilk"].ToString() + "</td>");
                }
                htmlStr.Append("</tr>");
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td>Closing Balance</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Closing Balance</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterCCClosing"].ToString() + "</td>");
                }
                htmlStr.Append("</tr>");


                htmlStr.Append("<tr>");
                htmlStr.Append("<td>Good</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td style='text-align:right'>LOOSE</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterLooseClosing"].ToString() + "</td>");
                }
                htmlStr.Append("</tr>");


                htmlStr.Append("<tr>");
                htmlStr.Append("<td>Sour</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td style='text-align:right'>- VE 2</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterVE2Closing"].ToString() + "</td>");
                }
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td style='text-align:right'>- VE 1</td>");
                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterVE1Closing"].ToString() + "</td>");
                }
                htmlStr.Append("</tr>");

                //htmlStr.Append("<tr>");
                //htmlStr.Append("<td style='text-align:right'>Sour</td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("</tr>");
                htmlStr.Append("<tr style='background-color:#ff874c; border:1px solid #ff874c; color:#000; font-weight:700' >");

                htmlStr.Append("<td style='text-align:right'>TOTAL</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td style='text-align:right'>TOTAL</td>");


                for (int i = 0; i < Count_Butter; i++)
                {
                    htmlStr.Append("<td > " + (Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterIssueToProcessing"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["WhiteButterMfg"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CookingButterMfg"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["TableButterMfg"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterIssueForGhee"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterIssueForStore"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterIssueForSweetButterMilk"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterCCClosing"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterLooseClosing"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterVE2Closing"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterVE1Closing"].ToString()))
                        + "</td>");
                }


                htmlStr.Append("</tr>");
                QtyInFlowTotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterCCOpening"].ToString())
                    + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["CreamRcvdForWhiteButterGood"].ToString())
                    + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["CreamRcvdForWhiteButterSour"].ToString())
                    + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["CreamRcvdForCookingButter"].ToString())
                    + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["CreamRcvdForTableButter"].ToString())
                    + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterRcvdFromCC"].ToString())
                    + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterRcvdFromFinishedProducts"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterLooseOpening"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterVE2Opening"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterVE1Opening"].ToString());

                FatInFlowTotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterCCOpening"].ToString())
                 + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["CreamRcvdForWhiteButterGood"].ToString())
                 + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["CreamRcvdForWhiteButterSour"].ToString())
                 + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["CreamRcvdForCookingButter"].ToString())
                 + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["CreamRcvdForTableButter"].ToString())
                 + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterRcvdFromCC"].ToString())
                 + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterRcvdFromFinishedProducts"].ToString())
                  + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterLooseOpening"].ToString())
                  + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterVE2Opening"].ToString())
                  + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterVE1Opening"].ToString());

                SnfInFlowtotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterCCOpening"].ToString())
                    + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["CreamRcvdForWhiteButterGood"].ToString())
                    + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["CreamRcvdForWhiteButterSour"].ToString())
                    + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["CreamRcvdForCookingButter"].ToString())
                    + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["CreamRcvdForTableButter"].ToString())
                    + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterRcvdFromCC"].ToString())
                    + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterRcvdFromFinishedProducts"].ToString())
                  + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterLooseOpening"].ToString())
                  + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterVE2Opening"].ToString())
                  + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterVE1Opening"].ToString());


                QtyOutFlowTotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterIssueToProcessing"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["WhiteButterMfg"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["CookingButterMfg"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["TableButterMfg"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterIssueForGhee"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterIssueForStore"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterIssueForSweetButterMilk"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterCCClosing"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterLooseClosing"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterVE2Closing"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterVE1Closing"].ToString());

                FatOutFlowTotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterIssueToProcessing"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["WhiteButterMfg"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["CookingButterMfg"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["TableButterMfg"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterIssueForGhee"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterIssueForStore"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterIssueForSweetButterMilk"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterCCClosing"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterLooseClosing"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterVE2Closing"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterVE1Closing"].ToString());

                SnfOutFlowtotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterIssueToProcessing"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["WhiteButterMfg"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["CookingButterMfg"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["TableButterMfg"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterIssueForGhee"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterIssueForStore"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterIssueForSweetButterMilk"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterCCClosing"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterLooseClosing"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterVE2Closing"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterVE1Closing"].ToString());
                htmlStr.Append("<tr>");
                htmlStr.Append("<td style='text-align:right'></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td style='text-align:right'><b>Variation</b></td>");
                htmlStr.Append("<td>" + (QtyOutFlowTotal - QtyInFlowTotal).ToString() + "</td>");
                htmlStr.Append("<td>" + (FatOutFlowTotal - FatInFlowTotal).ToString() + "</td>");
                htmlStr.Append("<td>" + (SnfOutFlowtotal - SnfInFlowtotal).ToString() + "</td>");
                htmlStr.Append("</tr>");

                htmlStr.Append("</tr>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<td style='text-align:right'></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td style='text-align:right'><b>Losses(in %)</b></td>");
                htmlStr.Append("<td>" + Math.Round(((QtyOutFlowTotal - QtyInFlowTotal) / QtyInFlowTotal) * 100, 2).ToString() + "</td>");
                htmlStr.Append("<td>" + Math.Round(((FatOutFlowTotal - FatInFlowTotal) / FatInFlowTotal) * 100, 2).ToString() + "</td>");
                htmlStr.Append("<td>" + Math.Round(((SnfOutFlowtotal - SnfInFlowtotal) / SnfInFlowtotal) * 100, 2).ToString() + "</td>");
                htmlStr.Append("</tr>");


                htmlStr.Append("</table>");
                htmlStr.Append("<div class='col-md-12 footerprint'><div class='col-md-4 col-sm-4 col-xs-4'><h5>D.G.M. Production</h5></div><div class='col-md-6  col-sm-6 col-xs-6 text-center'><h5>Manager(Product)</h5></div><div class='col-md-2  col-sm-2 col-xs-2'><h5>A.G.M. Production</h5></div></div>");
                htmlStr.Append("</div></div>");
                htmlStr.Append("</div></div>");
                htmlStr.Append("</div>");
                htmlStr.Append("</div>");
                DivTable.InnerHtml = htmlStr.ToString();
                //divprint.InnerHtml = "";
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


            DataSet dsVD_Child_Rpt = objdb.ByProcedure("SpProductionProduct_Milk_InOut_ProductSheet",
            new string[] { "flag", "Office_ID", "FDate", "TDate", "ProductSection_ID", "ItemType_id" },
            new string[] { "16", objdb.Office_ID(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), ddlPSection.SelectedValue, objdb.LooseCreamItemTypeId_ID() }, "dataset");

            if (dsVD_Child_Rpt != null && dsVD_Child_Rpt.Tables[0].Rows.Count > 0)
            {

                //string strTypeName = dsVD_Child_Rpt.Tables[0].Rows[0]["ItemTypeName"].ToString();
                //lblTopTitle.Text = strTypeName;

                int Count = dsVD_Child_Rpt.Tables[0].Rows.Count;

                StringBuilder htmlStr = new StringBuilder();

                htmlStr.Append("<div class='row'>");
                htmlStr.Append("<div class='col-md-12' style='text-align:center'><h3 style='font-weight: 800; font-size: 20px;'>" + Session["Office_Name"] + "</h3><h5 style='font-weight: 500; font-size: 13px;'>DAIRY PLANT</h5><h5 style='font-weight: 800; font-size: 20px;'>CREAM AND BUTTER PRODUCT SHEET</h5></div>");
                htmlStr.Append("<div class='col-md-12' style='text-align:center'>Date:" + txtDate.Text + "</div>");

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

                htmlStr.Append("<tr>");
                htmlStr.Append("<td>CST 1</td>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["OBCST1"].ToString() + "</td>");
                }

                htmlStr.Append("<td>1. (a) C.C.</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td>CST 2</td>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["OBCST2"].ToString() + "</td>");
                }

                htmlStr.Append("<td>(b) Loose</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");



                htmlStr.Append("<tr>");
                htmlStr.Append("<td>CST 3</td>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["OBCST3"].ToString() + "</td>");
                }
                htmlStr.Append("<td>(c)-VE 2</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");


                htmlStr.Append("<tr>");
                htmlStr.Append("<td>CST 4</td>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["OBCST4"].ToString() + "</td>");
                }
                htmlStr.Append("<td>(d)-VE 1</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");


                htmlStr.Append("<tr>");
                htmlStr.Append("<td>Cream Obtained From</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Cream Received For White Butter Good</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                //htmlStr.Append("<td>2. White Butter Mfd. Goods</td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");


                htmlStr.Append("<tr>");
                htmlStr.Append("<td>(a) Good Milk</td>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CreamObtFrom_GoodMilk"].ToString() + "</td>");
                }
                htmlStr.Append("<td>Cream Received For White Butter Sour</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                //htmlStr.Append("<td>3. White Butter Mfd. Sour</td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td>(b) Sour Milk</td>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CreamObtFrom_SourMilk"].ToString() + "</td>");
                }
                htmlStr.Append("<td>Cream Received For Table Butter</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                //htmlStr.Append("<td>4......................</td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td>Cream Received From processing Section</td>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CreamReceivedfromProcessSection"].ToString() + "</td>");
                }
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Cream Received For Cooking Butter</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                //htmlStr.Append("<td>5......................</td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Butter Received From CC</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                //htmlStr.Append("<td>6......................</td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Butter Received From Finished Products</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                //htmlStr.Append("<td>6......................</td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");


                htmlStr.Append("<tr style='background-color:#ff874c; border:1px solid #ff874c; color:#000; font-weight:700' >");

                htmlStr.Append("<td style='text-align:right'>TOTAL</td>");

                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td > " + (Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["OBCST1"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["OBCST2"].ToString())
                         + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["OBCST3"].ToString())
                          + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["OBCST4"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["CreamObtFrom_GoodMilk"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["CreamObtFrom_SourMilk"].ToString())
                        + +Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["CreamReceivedfromProcessSection"].ToString()))
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
                htmlStr.Append("<td>Issued For White Butter Goods</td>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_WhiteButterGood"].ToString() + "</td>");
                }
                htmlStr.Append("<td>1. White Butter Mfg</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td>Issued For White Butter Sour</td>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_WhiteButterSour"].ToString() + "</td>");
                }
                htmlStr.Append("<td>2. Table Butter Mfg</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Issue For Table Butter</td>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_TableButter"].ToString() + "</td>");
                }
                htmlStr.Append("<td>3. Cooking Butter Mfg</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");
                htmlStr.Append("<tr>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Issue For Cooking Butter</td>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_CookingButter"].ToString() + "</td>");
                }
                htmlStr.Append("<td>4. Isued To Processing</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");
                htmlStr.Append("<tr>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Return to Processing Section</td>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["ReturntoProcessingSection"].ToString() + "</td>");
                }
                htmlStr.Append("<td>5.Issued For Ghee</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");
                htmlStr.Append("<tr>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Issue For Sale</td>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_Sale"].ToString() + "</td>");
                }
                htmlStr.Append("<td>6.Issue For Store</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");
                htmlStr.Append("</tr>");
                htmlStr.Append("<tr>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                htmlStr.Append("<td>Issue For Others</td>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_Others"].ToString() + "</td>");
                }
                htmlStr.Append("<td>7.Issue For Butter Milk</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<td>4. Closing Balance</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                //for (int i = 0; i < Count; i++)
                //{
                //    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["ClosingGood"].ToString() + "</td>");
                //}
                htmlStr.Append("<td>4. Closing Balance</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");


                htmlStr.Append("<tr>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                //htmlStr.Append("<td></td>");
                htmlStr.Append("<td>CST1</td>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CBCST1"].ToString() + "</td>");
                }
                htmlStr.Append("<td style='text-align:right'>LOOSE</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");


                htmlStr.Append("<tr>");
                htmlStr.Append("<td>CST2</td>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CBCST2"].ToString() + "</td>");
                }
                //htmlStr.Append("<td style='text-align:right'>- CST 1 2</td>");
                //for (int i = 0; i < Count; i++)
                //{
                //    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CST1"].ToString() + "</td>");
                //}
                htmlStr.Append("<td style='text-align:right'>- VE 2</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td>CST3</td>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CBCST3"].ToString() + "</td>");
                }
                //htmlStr.Append("<td style='text-align:right'>- CST 2</td>");
                //for (int i = 0; i < Count; i++)
                //{
                //    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CST2"].ToString() + "</td>");
                //}
                htmlStr.Append("<td style='text-align:right'>- VE 1</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td>CST4</td>");
                for (int i = 0; i < Count; i++)
                {
                    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CBCST4"].ToString() + "</td>");
                }
                //htmlStr.Append("<td style='text-align:right'>- CST 2</td>");
                //for (int i = 0; i < Count; i++)
                //{
                //    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CST2"].ToString() + "</td>");
                //}
                htmlStr.Append("<td style='text-align:right'></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");
                htmlStr.Append("<tr>");
                //htmlStr.Append("<td style='text-align:right'>Sour</td>");
                //for (int i = 0; i < Count; i++)
                //{
                //    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["ClosingSour"].ToString() + "</td>");
                //}
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
                    htmlStr.Append("<td > " + (Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_WhiteButterGood"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_WhiteButterSour"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_TableButter"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_CookingButter"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["ReturntoProcessingSection"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["CBCST4"].ToString())
                         + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_Sale"].ToString())
                                + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_Others"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["CBCST1"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["CBCST2"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["CBCST3"].ToString())
                        )
                        + "</td>");
                }
                htmlStr.Append("<td style='text-align:right'>TOTAL</td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("<td></td>");

                htmlStr.Append("</tr>");

                htmlStr.Append("</table>");
                //htmlStr.Append("</div></div>");
                //htmlStr.Append("</div></div>");
                htmlStr.Append("<div class='col-md-12 footerprint'><div class='col-md-4 col-sm-4 col-xs-4'><h5>D.G.M. Production</h5></div><div class='col-md-6  col-sm-6 col-xs-6 text-center'><h5>Manager(Product)</h5></div><div class='col-md-2  col-sm-2 col-xs-2'><h5>A.G.M. Production</h5></div></div>");
                htmlStr.Append("</div>");
                htmlStr.Append("</div>");
                htmlStr.Append("</div>");
                DivTable.InnerHtml = htmlStr.ToString();
                // divprint.InnerHtml = htmlStr.ToString();

            }
            else
            {
                DivTable.InnerHtml = "";
                //divprint.InnerHtml = "";
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



            DataSet dsVD_Child_Rpt = objdb.ByProcedure("SpProductionProduct_Milk_InOut_ProductSheet",
            new string[] { "flag", "Office_ID", "FDate", "TDate", "ProductSection_ID", "ItemType_id" },
            new string[] { "16", objdb.Office_ID(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), ddlPSection.SelectedValue, objdb.LooseCreamItemTypeId_ID() }, "dataset");

            if (dsVD_Child_Rpt != null && dsVD_Child_Rpt.Tables[0].Rows.Count > 0)
            {

                //string strTypeName = dsVD_Child_Rpt.Tables[0].Rows[0]["ItemTypeName"].ToString();
                //lblTopTitle.Text = strTypeName;



                DataSet dsVD_Child_Rpt_Butter = objdb.ByProcedure("SpProductionProduct_Milk_InOut_ProductSheet",
        new string[] { "flag", "Office_ID", "FDate", "TDate", "ProductSection_ID", "ItemType_id" },
        new string[] { "17", objdb.Office_ID(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), ddlPSection.SelectedValue, objdb.LooseButterItemTypeId_ID() }, "dataset");

                if (dsVD_Child_Rpt_Butter != null && dsVD_Child_Rpt_Butter.Tables[0].Rows.Count > 0)
                {

                    int Count = dsVD_Child_Rpt.Tables[0].Rows.Count;

                    int Count_Butter = dsVD_Child_Rpt_Butter.Tables[0].Rows.Count;

                    // int Count_Butter_Inner = dsVD_Child_Rpt_Butter.Tables[1].Rows.Count;

                    decimal QtyTotal = 0;
                    decimal FatTotal = 0;
                    decimal Snftotal = 0;
                    decimal QtyInFlowTotal = 0;
                    decimal FatInFlowTotal = 0;
                    decimal SnfInFlowtotal = 0;
                    decimal QtyOutFlowTotal = 0;
                    decimal FatOutFlowTotal = 0;
                    decimal SnfOutFlowtotal = 0;
                    QtyTotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterCCOpening"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["CreamRcvdForWhiteButterGood"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["CreamRcvdForWhiteButterSour"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["CreamRcvdForCookingButter"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["CreamRcvdForTableButter"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterRcvdFromCC"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterRcvdFromFinishedProducts"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterLooseOpening"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterVE2Opening"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterVE1Opening"].ToString());

                    FatTotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterCCOpening"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["CreamRcvdForWhiteButterGood"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["CreamRcvdForWhiteButterSour"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["CreamRcvdForCookingButter"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["CreamRcvdForTableButter"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterRcvdFromCC"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterRcvdFromFinishedProducts"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterLooseOpening"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterVE2Opening"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterVE1Opening"].ToString());

                    Snftotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterCCOpening"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["CreamRcvdForWhiteButterGood"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["CreamRcvdForWhiteButterSour"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["CreamRcvdForCookingButter"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["CreamRcvdForTableButter"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterRcvdFromCC"].ToString())
                           + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterRcvdFromFinishedProducts"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterLooseOpening"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterVE2Opening"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterVE1Opening"].ToString());


                    StringBuilder htmlStr = new StringBuilder();

                    htmlStr.Append("<div class='row'>");
                    htmlStr.Append("<div class='col-md-12' style='text-align:center'><h3 style='font-weight: 800; font-size: 20px;'>" + Session["Office_Name"] + "</h3><h5 style='font-weight: 500; font-size: 13px;'>DAIRY PLANT</h5><h5 style='font-weight: 800; font-size: 20px;'>CREAM AND BUTTER PRODUCT SHEET</h5></div>");
                    htmlStr.Append("<div class='col-md-12' style='text-align:center'>Date:" + txtDate.Text + "</div>");

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

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>CST 1</td>");

                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["OBCST1"].ToString() + "</td>");
                    }

                    htmlStr.Append("<td>1. (a) C.C.</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterCCOpening"].ToString() + "</td>");

                    }
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>CST 2</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["OBCST2"].ToString() + "</td>");
                    }

                    htmlStr.Append("<td>(b) Loose</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterLooseOpening"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");



                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>CST 3</td>");

                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["OBCST3"].ToString() + "</td>");
                    }

                    htmlStr.Append("<td>(c)-VE 2</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterVE2Opening"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");


                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>CST 4</td>");

                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["OBCST4"].ToString() + "</td>");
                    }

                    htmlStr.Append("<td>(d)-VE 1</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterVE1Opening"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");


                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Cream Obtained From</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Cream Received For White Butter Good</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CreamRcvdForWhiteButterGood"].ToString() + "</td>");
                    }
                    //if (dsVD_Child_Rpt_Butter.Tables[1].Rows.Count > 0)
                    //{
                    //    int rowcount = dsVD_Child_Rpt_Butter.Tables[1].Rows.Count;

                    //    QtyTotal += dsVD_Child_Rpt_Butter.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("QuantityInKg"));
                    //    FatTotal += dsVD_Child_Rpt_Butter.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("FatInKg"));
                    //    Snftotal += dsVD_Child_Rpt_Butter.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("SnfInKg"));


                    //    if (rowcount >= 1)
                    //    {
                    //        htmlStr.Append("<td>2. " + dsVD_Child_Rpt_Butter.Tables[1].Rows[0]["MfdFrom"].ToString() + "</td>");
                    //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[0]["QuantityInKg"].ToString() + "</td>");
                    //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[0]["FatInKg"].ToString() + "</td>");
                    //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[0]["SnfInKg"].ToString() + "</td>");
                    //    }
                    //    else
                    //    {
                    //        htmlStr.Append("<td>2. White Butter Mfd. Goods</td>");
                    //        htmlStr.Append("<td></td>");
                    //        htmlStr.Append("<td></td>");
                    //        htmlStr.Append("<td></td>");

                    //    }

                    //}
                    //else
                    //{
                    //    htmlStr.Append("<td>2. White Butter Mfd. Goods</td>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td></td>");

                    //}



                    htmlStr.Append("</tr>");


                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>(a) Good Milk</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CreamObtFrom_GoodMilk"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>Cream Received For White Butter Sour</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CreamRcvdForWhiteButterSour"].ToString() + "</td>");
                    }
                    //if (dsVD_Child_Rpt_Butter.Tables[1].Rows.Count > 0)
                    //{
                    //    int rowcount = dsVD_Child_Rpt_Butter.Tables[1].Rows.Count;

                    //    if (rowcount >= 2)
                    //    {
                    //        htmlStr.Append("<td>3. " + dsVD_Child_Rpt_Butter.Tables[1].Rows[1]["MfdFrom"].ToString() + "</td>");
                    //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[1]["QuantityInKg"].ToString() + "</td>");
                    //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[1]["FatInKg"].ToString() + "</td>");
                    //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[1]["SnfInKg"].ToString() + "</td>");
                    //    }
                    //    else
                    //    {
                    //        htmlStr.Append("<td>3. White Butter Mfd. Sour</td>");
                    //        htmlStr.Append("<td></td>");
                    //        htmlStr.Append("<td></td>");
                    //        htmlStr.Append("<td></td>");

                    //    }

                    //}
                    //else
                    //{
                    //    htmlStr.Append("<td>3. White Butter Mfd. Sour</td>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td></td>");

                    //}

                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>(b) Sour Milk</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CreamObtFrom_SourMilk"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>Cream Received For Table Butter</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CreamRcvdForTableButter"].ToString() + "</td>");
                    }
                    //if (dsVD_Child_Rpt_Butter.Tables[1].Rows.Count > 0)
                    //{
                    //    int rowcount = dsVD_Child_Rpt_Butter.Tables[1].Rows.Count;

                    //    if (rowcount >= 3)
                    //    {
                    //        htmlStr.Append("<td>4. " + dsVD_Child_Rpt_Butter.Tables[1].Rows[2]["MfdFrom"].ToString() + "</td>");
                    //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[2]["QuantityInKg"].ToString() + "</td>");
                    //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[2]["FatInKg"].ToString() + "</td>");
                    //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[2]["SnfInKg"].ToString() + "</td>");
                    //    }
                    //    else
                    //    {
                    //        htmlStr.Append("<td>4. ..........</td>");
                    //        htmlStr.Append("<td></td>");
                    //        htmlStr.Append("<td></td>");
                    //        htmlStr.Append("<td></td>");

                    //    }

                    //}
                    //else
                    //{
                    //    htmlStr.Append("<td>4. White Butter Mfd. Sour</td>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td></td>");

                    //}



                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Cream Received From processing Section</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CreamReceivedfromProcessSection"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>Cream Received For Cooking Butter</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CreamRcvdForCookingButter"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");

                    //if (Count_Butter_Inner > 3)
                    //{
                    //    for (int i = 3; i < (Count_Butter_Inner); i++)
                    //    {
                    //        htmlStr.Append("<tr>");
                    //        htmlStr.Append("<td></td>");
                    //        htmlStr.Append("<td></td>");
                    //        htmlStr.Append("<td></td>");
                    //        htmlStr.Append("<td></td>");

                    //        htmlStr.Append("<td>" + (i + 2) + ". " + dsVD_Child_Rpt_Butter.Tables[1].Rows[i]["MfdFrom"].ToString() + "</td>");
                    //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[i]["QuantityInKg"].ToString() + "</td>");
                    //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[i]["FatInKg"].ToString() + "</td>");
                    //        htmlStr.Append("<td> " + dsVD_Child_Rpt_Butter.Tables[1].Rows[i]["SnfInKg"].ToString() + "</td>");
                    //        htmlStr.Append("</tr>");
                    //    }
                    //}
                    //else
                    //{
                    //    htmlStr.Append("<tr>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td></td>");

                    //    htmlStr.Append("<td>5......................</td>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("</tr>");

                    //    htmlStr.Append("<tr>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td>6......................</td>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("<td></td>");
                    //    htmlStr.Append("</tr>");

                    //}
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Butter Received From CC</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterRcvdFromCC"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Butter Received From Finished Products</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterRcvdFromFinishedProducts"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");



                    htmlStr.Append("<tr style='background-color:#ff874c; border:1px solid #ff874c; color:#000; font-weight:700' >");

                    htmlStr.Append("<td style='text-align:right'>TOTAL</td>");

                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + (Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["OBCST3"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["OBCST4"].ToString())
                             + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["OBCST1"].ToString())
                              + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["OBCST2"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["CreamObtFrom_GoodMilk"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["CreamObtFrom_SourMilk"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["CreamReceivedfromProcessSection"].ToString()))
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
                    htmlStr.Append("<td>Issued For White Butter Goods</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_WhiteButterGood"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>White Butter Mfg</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["WhiteButterMfg"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Issued For White Butter Sour</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_WhiteButterSour"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>Table Butter Mfg</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["TableButterMfg"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Issued For Table Butter</td>");

                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_TableButter"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>Cooking Butter Mfg</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CookingButterMfg"].ToString() + "</td>");
                    }

                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Issued For Cooking Butter</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_CookingButter"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>Isued To Processing</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterIssueToProcessing"].ToString() + "</td>");
                    }

                    htmlStr.Append("</tr>");


                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Return to Processing Section</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["ReturntoProcessingSection"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>Issued For Ghee</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterIssueForGhee"].ToString() + "</td>");
                    }

                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Issue For Sale,</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_Sale"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>Issue For Store</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterIssueForStore"].ToString() + "</td>");
                    }

                    htmlStr.Append("</tr>");


                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Issue For Others</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_Others"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>Issue For Butter Milk</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterIssueForSweetButterMilk"].ToString() + "</td>");
                    }

                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>Closing Balance</td>");
                    //for (int i = 0; i < Count; i++)
                    //{
                    //    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["ClosingGood"].ToString() + "</td>");
                    //}
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td>Closing Balance</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterCCClosing"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");


                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>CST1</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CBCST1"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td style='text-align:right'>LOOSE</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterLooseClosing"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");


                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>CST2</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CBCST2"].ToString() + "</td>");
                    }
                    //htmlStr.Append("<td style='text-align:right'>- CST 1 2</td>");
                    //for (int i = 0; i < Count; i++)
                    //{
                    //    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CST1"].ToString() + "</td>");
                    //}
                    htmlStr.Append("<td style='text-align:right'>- VE 2</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterVE2Closing"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>CST3</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CBCST3"].ToString() + "</td>");
                    }
                    //htmlStr.Append("<td style='text-align:right'>- CST 2</td>");
                    //for (int i = 0; i < Count; i++)
                    //{
                    //    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CST2"].ToString() + "</td>");
                    //}
                    htmlStr.Append("<td style='text-align:right'>- VE 1</td>");
                    for (int i = 0; i < Count_Butter; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterVE1Closing"].ToString() + "</td>");
                    }
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>CST4</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CBCST4"].ToString() + "</td>");
                    }
                    //htmlStr.Append("<td style='text-align:right'>- CST 2</td>");
                    //for (int i = 0; i < Count; i++)
                    //{
                    //    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["CST2"].ToString() + "</td>");
                    //}
                    htmlStr.Append("<td style='text-align:right'>- VE 1</td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");

                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    //htmlStr.Append("<td style='text-align:right'>Sour</td>");
                    //for (int i = 0; i < Count; i++)
                    //{
                    //    htmlStr.Append("<td > " + dsVD_Child_Rpt.Tables[0].Rows[i]["ClosingSour"].ToString() + "</td>");
                    //}
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr style='background-color:#ff874c; border:1px solid #ff874c; color:#000; font-weight:700' >");

                    htmlStr.Append("<td style='text-align:right'>TOTAL</td>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + (Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_WhiteButterGood"].ToString())

                            + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_WhiteButterSour"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_TableButter"].ToString())
                             + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_CookingButter"].ToString())
                              + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["ReturntoProcessingSection"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["CBCST3"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_Sale"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["IssueFor_Others"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["CBCST1"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["CBCST2"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt.Tables[0].Rows[i]["CBCST4"].ToString()))
                            + "</td>");
                    }
                    htmlStr.Append("<td style='text-align:right'>TOTAL</td>");

                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td > " + (Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterIssueToProcessing"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["WhiteButterMfg"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["CookingButterMfg"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["TableButterMfg"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterIssueForGhee"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterIssueForStore"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterIssueForSweetButterMilk"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterCCClosing"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterLooseClosing"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterVE2Closing"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[i]["ButterVE1Closing"].ToString()))
                            + "</td>");
                    }


                    //htmlStr.Append("<td></td>");
                    //htmlStr.Append("<td></td>");
                    //htmlStr.Append("<td></td>");

                    htmlStr.Append("</tr>");
                    QtyInFlowTotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterCCOpening"].ToString())
                 + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["CreamRcvdForWhiteButterGood"].ToString())
                 + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["CreamRcvdForWhiteButterSour"].ToString())
                 + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["CreamRcvdForCookingButter"].ToString())
                 + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["CreamRcvdForTableButter"].ToString())
                 + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterRcvdFromCC"].ToString())
                 + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterRcvdFromFinishedProducts"].ToString())
                   + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterLooseOpening"].ToString())
                   + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterVE2Opening"].ToString())
                   + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterVE1Opening"].ToString());

                    FatInFlowTotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterCCOpening"].ToString())
                     + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["CreamRcvdForWhiteButterGood"].ToString())
                     + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["CreamRcvdForWhiteButterSour"].ToString())
                     + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["CreamRcvdForCookingButter"].ToString())
                     + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["CreamRcvdForTableButter"].ToString())
                     + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterRcvdFromCC"].ToString())
                     + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterRcvdFromFinishedProducts"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterLooseOpening"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterVE2Opening"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterVE1Opening"].ToString());

                    SnfInFlowtotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterCCOpening"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["CreamRcvdForWhiteButterGood"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["CreamRcvdForWhiteButterSour"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["CreamRcvdForCookingButter"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["CreamRcvdForTableButter"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterRcvdFromCC"].ToString())
                        + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterRcvdFromFinishedProducts"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterLooseOpening"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterVE2Opening"].ToString())
                      + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterVE1Opening"].ToString());


                    QtyOutFlowTotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterIssueToProcessing"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["WhiteButterMfg"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["CookingButterMfg"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["TableButterMfg"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterIssueForGhee"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterIssueForStore"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterIssueForSweetButterMilk"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterCCClosing"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterLooseClosing"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterVE2Closing"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[0]["ButterVE1Closing"].ToString());

                    FatOutFlowTotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterIssueToProcessing"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["WhiteButterMfg"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["CookingButterMfg"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["TableButterMfg"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterIssueForGhee"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterIssueForStore"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterIssueForSweetButterMilk"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterCCClosing"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterLooseClosing"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterVE2Closing"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[1]["ButterVE1Closing"].ToString());

                    SnfOutFlowtotal += Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterIssueToProcessing"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["WhiteButterMfg"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["CookingButterMfg"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["TableButterMfg"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterIssueForGhee"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterIssueForStore"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterIssueForSweetButterMilk"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterCCClosing"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterLooseClosing"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterVE2Closing"].ToString())
                            + Convert.ToDecimal(dsVD_Child_Rpt_Butter.Tables[0].Rows[2]["ButterVE1Closing"].ToString());
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td style='text-align:right'></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td style='text-align:right'><b>Variation</b></td>");
                    htmlStr.Append("<td>" + (QtyOutFlowTotal - QtyInFlowTotal).ToString() + "</td>");
                    htmlStr.Append("<td>" + (FatOutFlowTotal - FatInFlowTotal).ToString() + "</td>");
                    htmlStr.Append("<td>" + (SnfOutFlowtotal - SnfInFlowtotal).ToString() + "</td>");
                    htmlStr.Append("</tr>");

                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td style='text-align:right'></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("<td style='text-align:right'><b>Losses(in %)</b></td>");
                    htmlStr.Append("<td>" + Math.Round(((QtyOutFlowTotal - QtyInFlowTotal) / QtyInFlowTotal) * 100, 2).ToString() + "</td>");
                    htmlStr.Append("<td>" + Math.Round(((FatOutFlowTotal - FatInFlowTotal) / FatInFlowTotal) * 100, 2).ToString() + "</td>");
                    htmlStr.Append("<td>" + Math.Round(((SnfOutFlowtotal - SnfInFlowtotal) / SnfInFlowtotal) * 100, 2).ToString() + "</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("</table>");

                    htmlStr.Append("</div></div>");
                    htmlStr.Append("</div></div>");
                    htmlStr.Append("<div class='col-md-12 footerprint'><div class='col-md-4 col-sm-4 col-xs-4'><h5>D.G.M. Production</h5></div><div class='col-md-6  col-sm-6 col-xs-6 text-center'><h5>Manager(Product)</h5></div><div class='col-md-2  col-sm-2 col-xs-2'><h5>A.G.M. Production</h5></div></div>");
                    htmlStr.Append("</div>");
                    DivTable.InnerHtml = htmlStr.ToString();
                    //divprint.InnerHtml = htmlStr.ToString();
                }


            }
            else
            {

                DivTable.InnerHtml = "";
                //  divprint.InnerHtml = "";
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

        htmlStr.Append("<tr>");
        htmlStr.Append("<td>CST 1</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>1. (a) C.C.</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td>CST2</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>(b) Loose</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");



        htmlStr.Append("<tr>");
        htmlStr.Append("<td>CST 3</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>(c)-VE 2</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");


        htmlStr.Append("<tr>");
        htmlStr.Append("<td>CST4</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>(d)-VE 1</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");


        htmlStr.Append("<tr>");
        htmlStr.Append("<td>Cream Obtained From</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>2. White Butter Mfd. Goods</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");


        htmlStr.Append("<tr>");
        htmlStr.Append("<td>(a) Good Milk</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>3. White Butter Mfd. Sour</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td>(b) Sour Milk</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>4......................</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>5......................</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>6......................</td>");
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
        htmlStr.Append("<td>Issued For White Butter Goods</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>1. Isued To Processing</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td>Issued For White Butter Sour</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>2. Issued For Ghee</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");



        htmlStr.Append("<tr>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>3. Issue For Store</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");


        htmlStr.Append("<tr>");
        htmlStr.Append("<td>4. Closing Balance</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td>4. Closing Balance</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");


        htmlStr.Append("<tr>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td style='text-align:right'>LOOSE</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");


        htmlStr.Append("<tr>");
        htmlStr.Append("<td style='text-align:right'>- CST 1 2</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td style='text-align:right'>- VE 2</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td style='text-align:right'>- CST 2</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td style='text-align:right'>- VE 1</td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("<td></td>");
        htmlStr.Append("</tr>");

        htmlStr.Append("<tr>");
        htmlStr.Append("<td style='text-align:right'>Sour</td>");
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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "Cream&ButterAccountingSheet" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            DivTable.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());

            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}