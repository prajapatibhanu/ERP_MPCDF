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

public partial class mis_dailyplan_ProductIPSheetCommoditiesReport : System.Web.UI.Page
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
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillOffice();
                    txtFDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtTDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    GetSectionView(sender, e);
                    //txtDate_TextChanged(sender, e);
                    

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
    //protected void txtDate_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        StringBuilder sb = new StringBuilder();
    //        ds = objdb.ByProcedure("Usp_Production_ProductIPSheetMaster", new string[] { "flag", "Date", "Office_ID" }, new string[] { "8", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), objdb.Office_ID() }, "dataset");
    //        if (ds != null && ds.Tables.Count > 0)
    //        {
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                int ColumnCount = ds.Tables[0].Columns.Count;
    //                sb.Append("<table class='table table-bordered'>");
    //                sb.Append("<tr>");
    //                sb.Append("<th style='text-align:center;' colspan='" + (ColumnCount + 1) + "'>Comodities (in KG)</th>");
    //                sb.Append("</tr>");
    //                sb.Append("<tr>");
    //                sb.Append("<th style='text-align:center;'>Product Name</th>");
    //                for (int i = 0; i < ColumnCount; i++)
    //                {
    //                    if (ds.Tables[0].Columns[i].ToString() != "ItemTypeName")
    //                    {

    //                        sb.Append("<th style='text-align:center;'><b>" + ds.Tables[0].Columns[i].ToString() + "</th>");
    //                    }
    //                }
    //                    sb.Append("</tr>");
    //                    sb.Append("<tr>");



    //                    sb.Append("</tr>");
    //                    int Count = ds.Tables[0].Rows.Count;
    //                    for (int j = 0; j < Count; j++)
    //                    {
    //                        sb.Append("<tr>");
    //                        sb.Append("<td>" + ds.Tables[0].Rows[j]["ItemTypeName"].ToString() + "</td>");
    //                        for (int k = 0; k < ColumnCount; k++)
    //                        {
    //                            if (ds.Tables[0].Columns[k].ToString() != "ItemTypeName")
    //                            {
    //                                string Columns = ds.Tables[0].Columns[k].ToString();
    //                                sb.Append("<td><b>" + ds.Tables[0].Rows[j][Columns].ToString() + "</b></td>");
    //                            }

    //                        }


    //                        sb.Append("</tr>");
    //                    }

    //                    sb.Append("</table>");
    //                }
               

    //        }
    //        divReport.InnerHtml = sb.ToString();

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            StringBuilder sb = new StringBuilder();
            ds = objdb.ByProcedure("Usp_Production_ProductIPSheetMaster", new string[] { "flag", "FDate","TDate", "Office_ID" }, new string[] { "8", Convert.ToDateTime(txtFDate.Text, cult).ToString("yyyy/MM/dd"),Convert.ToDateTime(txtTDate.Text, cult).ToString("yyyy/MM/dd"), objdb.Office_ID() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int ColumnCount = ds.Tables[0].Columns.Count;
                    sb.Append("<div class='table-responsive'>");
                    sb.Append("<table class='table table-bordered'>");
                    sb.Append("<tr>");
                    sb.Append("<th style='text-align:center;' colspan='" + (ColumnCount + 1) + "'>Comodities (in KG)</th>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<th style='text-align:center;'>Product Name</th>");
                    for (int i = 0; i < ColumnCount; i++)
                    {
                        if (ds.Tables[0].Columns[i].ToString() != "ItemTypeName")
                        {

                            sb.Append("<th style='text-align:center;'><b>" + ds.Tables[0].Columns[i].ToString() + "</th>");
                        }
                    }
                        sb.Append("</tr>");
                        sb.Append("<tr>");



                        sb.Append("</tr>");
                        int Count = ds.Tables[0].Rows.Count;
                        for (int j = 0; j < Count; j++)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td>" + ds.Tables[0].Rows[j]["ItemTypeName"].ToString() + "</td>");
                            for (int k = 0; k < ColumnCount; k++)
                            {
                                if (ds.Tables[0].Columns[k].ToString() != "ItemTypeName")
                                {
                                    string Columns = ds.Tables[0].Columns[k].ToString();
                                    sb.Append("<td><b>" + ds.Tables[0].Rows[j][Columns].ToString() + "</b></td>");
                                }

                            }


                            sb.Append("</tr>");
                        }

                        sb.Append("</table>");
                        sb.Append("</div>");
                    }
               

            }
            divReport.InnerHtml = sb.ToString();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}