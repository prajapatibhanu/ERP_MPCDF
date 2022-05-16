using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_dailyplan_RptRecipeMaster : System.Web.UI.Page
{
    DataSet ds, ds2;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillOffice();
                FillProductSection();
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
    protected void FillProductSection()
    {
        try
        {

            lblMsg.Text = "";
            ddlProductSection.Items.Clear();
            ds = objdb.ByProcedure("SpProductionProduct_DS_Mapping",
                 new string[] { "flag", "Office_ID" },
                 new string[] { "0", ddlDS.SelectedValue.ToString() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlProductSection.DataSource = ds.Tables[0];
                ddlProductSection.DataTextField = "ProductSection_Name";
                ddlProductSection.DataValueField = "ProductSection_ID";
                ddlProductSection.DataBind();

            }
            ddlProductSection.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        FillGrid();
    }

    protected void FillGrid()
    {
        try
        {

            /********************************/
            StringBuilder htmlStr = new StringBuilder();

            htmlStr.Append("<table  id='DetailGrid' class='datatable table table-bordered table-hover GridView2' style='font-family:verdana; font-size:12px; width:100%'>");
            htmlStr.Append("<thead>");
            htmlStr.Append("<tr>");
            htmlStr.Append("<th style='width:120px !important;'>SNo</th>");
            htmlStr.Append("<th style='width:120px !important;'>Product Varient</th>");
            htmlStr.Append("<th style='min-width:350px !important;'>Item/Ingredient</th>");
            htmlStr.Append("<th style='min-width:150px !important;'>Quantity</th>");
            htmlStr.Append("<th>Unit</th>");
            htmlStr.Append("</tr>");
            htmlStr.Append("</thead>");
            htmlStr.Append("<tbody>");

            ds = objdb.ByProcedure("SpProductionRecepie_Master",
                   new string[] { "flag", "Office_ID", "ProductSection_ID" },
                   new string[] { "9", ddlDS.SelectedValue.ToString(), ddlProductSection.SelectedValue.ToString() }, "dataset");

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                int Count = ds.Tables[0].Rows.Count;
                for (int i = 0; i < Count; i++)
                {

                    htmlStr.Append("<tr>");
                    

                    htmlStr.Append("<th>" + (i + 1) + "</th>");
                    htmlStr.Append("<th>" + ds.Tables[0].Rows[i]["ItemName"].ToString() + "</th>");
                    htmlStr.Append("<th> </th>");
                    htmlStr.Append("<th> </th>");
                    htmlStr.Append("<th> </th>");
                    htmlStr.Append("</tr>");


                    ds2 = objdb.ByProcedure("SpProductionRecepie_Master",
                                               new string[] { "flag","Office_ID","ProductSection_ID","Product_ID" },
                                               new string[] { "10", ddlDS.SelectedValue.ToString(), ddlProductSection.SelectedValue.ToString(), ds.Tables[0].Rows[i]["Product_ID"].ToString() }, "dataset");

                    if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                    {
                        
                        int Count2 = ds2.Tables[0].Rows.Count;
                        for (int p = 0; p < Count2; p++)
                        {

                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td> </th>");
                            htmlStr.Append("<td> </td>");
                            htmlStr.Append("<td>" + ds2.Tables[0].Rows[p]["ItemName"].ToString() + "</td>");
                            htmlStr.Append("<td>" + ds2.Tables[0].Rows[p]["Item_Quantity"].ToString() + "</td>");
                            htmlStr.Append("<td>" + ds2.Tables[0].Rows[p]["UnitName"].ToString() + "</td>");
                            htmlStr.Append("</tr>");

                        }
                    }
                }

            }
            else
            {

            }
            htmlStr.Append("</tbody>");
            htmlStr.Append("</table>");

            DivTable.InnerHtml = htmlStr.ToString();
            /********************************/

            lblSeletedInfo.Text = "<b>Dugdh Sangh :</b> " + ddlDS.SelectedItem.ToString() + " </p> <p><b>Product Section :</b> " + ddlProductSection.SelectedItem.ToString() + " </p>";

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

}