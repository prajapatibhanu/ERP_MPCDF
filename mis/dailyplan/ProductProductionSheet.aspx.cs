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
using System.Web.UI.HtmlControls;

public partial class mis_dailyplan_ProductProductionSheet : System.Web.UI.Page
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
                //txtDate.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        GetSectionDetail();
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
            ds = null;
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
            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut_ProductSheet",
                  new string[] { "flag", "Office_ID", "Shift_Id", "ProductSection_ID", "Date", "ItemCat_id" },
                  new string[] { "6", ddlDS.SelectedValue, ddlShift.SelectedValue, ddlPSection.SelectedValue, Fdate, strcat_id.ToString() }, "dataset");


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                divcont.Visible = true;
                gvmttos.DataSource = ds;
                gvmttos.DataBind();
				spnofcname.InnerHtml = Session["Office_Name"].ToString();

                foreach (GridViewRow row in gvmttos.Rows)
                {

                    Label lblItemType_id = (Label)row.FindControl("lblItemType_id");
                    Label lblPPSM_Id = (Label)row.FindControl("lblPPSM_Id");
                    HtmlGenericControl DivTable_Rpt = (HtmlGenericControl)row.FindControl("DivTable");

                    DataSet dsVD_Child_Rpt = objdb.ByProcedure("SpProductionProduct_Milk_InOut_ProductSheet",
                    new string[] { "flag", "Office_ID", "Date", "ProductSection_ID", "ItemType_id", "PPSM" },
                    new string[] { "5", objdb.Office_ID(), Fdate, ddlPSection.SelectedValue, lblItemType_id.Text, lblPPSM_Id.Text }, "dataset");

                    if (dsVD_Child_Rpt != null && dsVD_Child_Rpt.Tables[0].Rows.Count > 0)
                    {
                        string strTypeName = dsVD_Child_Rpt.Tables[0].Rows[0]["ItemTypeName"].ToString();
                        //lblTopTitle.Text = strTypeName;

                        int Count = dsVD_Child_Rpt.Tables[0].Rows.Count;
                        StringBuilder htmlStr = new StringBuilder();
                        htmlStr.Append("<table class='table table-bordered'>");
                        htmlStr.Append("<tr class=class='text-center'>");
                        htmlStr.Append("<th colspan='10' class='text-center' style='font-weight: 800; font-size: 18px;'>" + strTypeName + "</th>");
                        htmlStr.Append("</tr>");
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<th></th>");
                        for (int i = 0; i < Count; i++)
                        {
                            htmlStr.Append("<th>" + dsVD_Child_Rpt.Tables[0].Rows[i]["VariantSize"].ToString() + "</th>");
                        }

                        htmlStr.Append("<th></th>");
                        for (int i = 0; i < Count; i++)
                        {
                            htmlStr.Append("<th>" + dsVD_Child_Rpt.Tables[0].Rows[i]["VariantSize"].ToString() + "</th>");
                        }
                        htmlStr.Append("</tr>");
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<th style='width:12%;'>Balance-B/F</th>");
                        for (int i = 0; i < Count; i++)
                        {
                            htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["LastDayClosingBalance"].ToString() + "</td>");
                        }

                        htmlStr.Append("<th style='width:12%;'>Sale</th>");
                        for (int i = 0; i < Count; i++)
                        {
                            htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Sale"].ToString() + "</td>");
                        }

                        htmlStr.Append("</tr>");
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<th style='width:12%;'>Prepared</th>");
                        for (int i = 0; i < Count; i++)
                        {
                            htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Prepared"].ToString() + "</td>");
                        }

                        htmlStr.Append("<th style='width:12%;'>Testing</th>");
                        for (int i = 0; i < Count; i++)
                        {
                            htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Testing"].ToString() + "</td>");
                        }

                        htmlStr.Append("</tr>");
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<th style='width:12%;'>Return</th>");
                        for (int i = 0; i < Count; i++)
                        {
                            htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["ReturnQty"].ToString() + "</td>");
                        }
                        
                        //code chnages by mohini on 21sep2020
                        //htmlStr.Append("<th style='width:12%;'>Discarded</th>");
                        //for (int i = 0; i < Count; i++)
                        //{
                        //    htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Discarded"].ToString() + "</td>");
                        //}

                        //htmlStr.Append("</tr>");
                        //htmlStr.Append("<tr>");
                        //htmlStr.Append("<th style='width:12%;'></th>");
                        //for (int i = 0; i < Count; i++)
                        //{
                        //    htmlStr.Append("<td></td>");
                        //}

                        //htmlStr.Append("<th style='width:12%;'>CL.Closing</th>");
                        //for (int i = 0; i < Count; i++)
                        //{
                        //    htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Closing"].ToString() + "</td>");
                        //}
                        //code chnages by mohini on 21sep2020
                        if (lblItemType_id.Text == "20")
                        {
                            htmlStr.Append("<th style='width:12%;'>Issue For Kheer</th>");
                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["IssueForKheer"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");
                            htmlStr.Append("<tr>");
                            htmlStr.Append("<th style='width:12%;'></th>");
                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td></td>");
                            }
                            htmlStr.Append("<th style='width:12%;'>Discarded</th>");
                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Discarded"].ToString() + "</td>");
                            }
                            htmlStr.Append("</tr>");
                            htmlStr.Append("<tr>");
                            htmlStr.Append("<th style='width:12%;'></th>");
                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td></td>");
                            }
                            htmlStr.Append("<th style='width:12%;'>CL.Closing</th>");
                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Closing"].ToString() + "</td>");
                            }

                            htmlStr.Append("</tr>");
                        }
                        else
                        {
                            htmlStr.Append("<th style='width:12%;'>Discarded</th>");
                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Discarded"].ToString() + "</td>");
                            }

                            htmlStr.Append("</tr>");
                            htmlStr.Append("<tr>");
                            htmlStr.Append("<th style='width:12%;'></th>");
                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td></td>");
                            }

                            htmlStr.Append("<th style='width:12%;'>CL.Closing</th>");
                            for (int i = 0; i < Count; i++)
                            {
                                htmlStr.Append("<td>" + dsVD_Child_Rpt.Tables[0].Rows[i]["Closing"].ToString() + "</td>");
                            }

                        }
                        htmlStr.Append("</tr>");
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<th style='width:12%;'>Total</th>");
                        for (int i = 0; i < Count; i++)
                        {
                            htmlStr.Append("<td><b>" + dsVD_Child_Rpt.Tables[0].Rows[i]["InTotal"].ToString() + "</b></td>");
                        }

                        htmlStr.Append("<th style='width:12%;'>Total</th>");
                        for (int i = 0; i < Count; i++)
                        {
                            htmlStr.Append("<td><b>" + dsVD_Child_Rpt.Tables[0].Rows[i]["OutTotal"].ToString() + "</b></td>");
                        }

                        htmlStr.Append("</tr>");
                        htmlStr.Append("</table>");

                        DivTable_Rpt.InnerHtml = htmlStr.ToString();

                        //PS_P1.Attributes.Add(htmlStr.ToString(), htmlStr.ToString());

                    }
                }
            }
            else
            {
                divcont.Visible = false;
            }

        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

}