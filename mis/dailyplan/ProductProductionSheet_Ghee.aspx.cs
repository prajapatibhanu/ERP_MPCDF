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


public partial class mis_dailyplan_ProductProductionSheet_Ghee : System.Web.UI.Page
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
            ds = null;

            string OpeningTotal = "0";
            string From_Butter = "0";
            string Sour_Milk = "0";
            string Curdle_Milk = "0";
            string IssueTo_Store = "0";
            string IssueTo_Plant = "0";
            string PackingLosses = "0";
            string Closing = "0";

            int strcat_id = 0;

            if (ddlPSection.SelectedValue == "1")
            {
                strcat_id = 3;
            }
            if (ddlPSection.SelectedValue == "2")
            {
                strcat_id = 2;
            }

            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            
            ds = null;
            //ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut_ProductSheet",
            //      new string[] { "flag", "Office_ID", "Shift_Id", "ProductSection_ID", "Date", "ItemCat_id", "ItemType_id" },
            //      new string[] { "1", ddlDS.SelectedValue, ddlShift.SelectedValue, ddlPSection.SelectedValue, Fdate, strcat_id.ToString(), objdb.LooseGheeItemTypeId_ID() }, "dataset");
            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut_ProductSheet",
                 new string[] { "flag", "Office_ID", "Shift_Id", "ProductSection_ID", "Date", "ItemCat_id", "ItemType_id" },
                 new string[] { "1", ddlDS.SelectedValue, ddlShift.SelectedValue, ddlPSection.SelectedValue, Fdate, strcat_id.ToString(), "149"}, "dataset");


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                string lblItemType_id = ds.Tables[0].Rows[0]["ItemType_id"].ToString();
                string lbviewsheet = ds.Tables[0].Rows[0]["PPSM_Id"].ToString();

                DataSet dsVD_Child_Rpt = objdb.ByProcedure("SpProductionProduct_Milk_InOut_ProductSheet",
                new string[] { "flag", "Office_ID", "Date", "ProductSection_ID", "ItemType_id", "PPSM" },
                new string[] { "8", objdb.Office_ID(), Fdate, ddlPSection.SelectedValue, lblItemType_id, lbviewsheet }, "dataset");

                if (dsVD_Child_Rpt != null && dsVD_Child_Rpt.Tables[0].Rows.Count > 0)
                {

                    if (dsVD_Child_Rpt != null && dsVD_Child_Rpt.Tables[1].Rows.Count > 0)
                    {
                        OpeningTotal = dsVD_Child_Rpt.Tables[1].Rows[0]["Opening"].ToString();
                        From_Butter = dsVD_Child_Rpt.Tables[1].Rows[0]["From_Butter"].ToString();
                        Sour_Milk = dsVD_Child_Rpt.Tables[1].Rows[0]["Sour_Milk"].ToString();
                        Curdle_Milk = dsVD_Child_Rpt.Tables[1].Rows[0]["Curdle_Milk"].ToString();
                        IssueTo_Store = dsVD_Child_Rpt.Tables[1].Rows[0]["IssueTo_Store"].ToString();
                        IssueTo_Plant = dsVD_Child_Rpt.Tables[1].Rows[0]["IssueTo_Plant"].ToString();
                        PackingLosses = dsVD_Child_Rpt.Tables[1].Rows[0]["PackingLosses"].ToString();
                        Closing = dsVD_Child_Rpt.Tables[1].Rows[0]["Closing"].ToString();
                    }

                    string strTypeName = dsVD_Child_Rpt.Tables[0].Rows[0]["ItemTypeName"].ToString();
                    //lblTopTitle.Text = strTypeName;

                    int Count = dsVD_Child_Rpt.Tables[0].Rows.Count;
                    StringBuilder htmlStr = new StringBuilder();

                    htmlStr.Append("<div class='row'>");
                    htmlStr.Append("<div class='col-md-12' style='text-align:center'><h3 style='font-weight: 800; font-size: 20px;'>"+Session["Office_Name"].ToString()+"</h3><h5 style='font-weight: 500; font-size: 13px;'>DAIRY PLANT</h5><h5 style='font-weight: 800; font-size: 20px;'>DAILY GHEE ACCOUNT SHEET</h5></div>");
                    htmlStr.Append("<div class='col-md-12'></div>");

                    /********** Table Start******/
                    htmlStr.Append("<div class='row'>");
                    htmlStr.Append("<div class='col-md-12'>");
                    htmlStr.Append("<table class='datatable table table-striped table-bordered table-hover' style='width:100%;'>");
                    htmlStr.Append("<tr class='text-center'>");
                    htmlStr.Append("<th rowspan='2' style='width:18%'>PARTICULAR</th>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<th > " + dsVD_Child_Rpt.Tables[0].Rows[i]["VariantSize"].ToString() + "</th>");
                    }
                    htmlStr.Append("<th rowspan='2'>Total(Liter)</th>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr class='text-center'>");

                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<th>Qty</th>");
                    }
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td>B.F.</td>");

                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td> " + dsVD_Child_Rpt.Tables[0].Rows[i]["Opening"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>" + OpeningTotal + "</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td style='width:18%'>Manufactured From:.</td>");

                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td></td>");
                    }
                    htmlStr.Append("<td></td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");

                    htmlStr.Append("<td style='width:18%'>A) Butter</td>");

                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["From_Butter"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>" + From_Butter + "</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");

                    htmlStr.Append("<td style='width:18%'>B) Sour Milk</td>");

                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Sour_Milk"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>" + Sour_Milk + "</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");

                    htmlStr.Append("<td style='width:18%'>C) Curdle Milk</td>");

                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Curdle_Milk"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>" + Curdle_Milk + "</td>");
                    htmlStr.Append("</tr>");
                    //htmlStr.Append("<tr>");
            //htmlStr.Append("<td style='width:18%'>Received From .....</td>");
            //htmlStr.Append("<td colspan='8'>" + dsVD_Child_Rpt.Tables[0].Rows[0]["Received_From"].ToString() + "</td>");

            //htmlStr.Append("</tr>");
            htmlStr.Append("<tr>");

            htmlStr.Append("<td style='width:18%'>Batch No</td>");

            for (int i = 0; i < Count; i++)
            {
                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["BatchNo"].ToString() + "</td>");
            }
            htmlStr.Append("<td></td>");
            htmlStr.Append("</tr>");
                    htmlStr.Append("<tr style='background-color:#ff874c; border:1px solid #ff874c; color:#000; font-weight:700' >");

                    htmlStr.Append("<td style='width:18%'>TOTAL</th>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["InTotal"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td> " + (Convert.ToDecimal(OpeningTotal) + Convert.ToDecimal(From_Butter) + Convert.ToDecimal(Sour_Milk) + Convert.ToDecimal(Curdle_Milk)) + "</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td style='width:18%'>Issued To Store</td>");

                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["IssueTo_Store"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>" + IssueTo_Store + "</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td style='width:18%'>Issued To Plant</td>");

                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["IssueTo_Plant"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>" + IssueTo_Plant + "</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td style='width:18%'>Packing Losses</td>");

                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["PackingLosses"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>" + PackingLosses + "</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td style='width:18%'>C.B.</td>");

                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Closing"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>" + Closing + "</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr style='background-color:#ff874c; border:1px solid #ff874c; color:#000; font-weight:700' >");

                    htmlStr.Append("<td style='width:18%'>TOTAL</th>");
                    for (int i = 0; i < Count; i++)
                    {
                        htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["OutTotal"].ToString() + "</td>");
                    }
                    htmlStr.Append("<td>" + (Convert.ToDecimal(IssueTo_Plant) + Convert.ToDecimal(IssueTo_Store) + Convert.ToDecimal(Closing) + Convert.ToDecimal(PackingLosses)) + "</td>");
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
                    DivTable.InnerHtml = "";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

}