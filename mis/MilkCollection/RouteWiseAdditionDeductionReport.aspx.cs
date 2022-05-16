using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;

public partial class mis_MilkCollection_RouteWiseAdditionDeductionReport : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!Page.IsPostBack)
            {
                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                FillBMCRoot();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    public void FillBMCRoot()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
                      new string[] { "flag", "Office_ID" },
                      new string[] { "1", objdb.Office_ID() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlBMCTankerRootName.DataTextField = "BMCTankerRootName";
                ddlBMCTankerRootName.DataValueField = "BMCTankerRoot_Id";
                ddlBMCTankerRootName.DataSource = ds;
                ddlBMCTankerRootName.DataBind();
                ddlBMCTankerRootName.Items.Insert(0, new ListItem("Select", "0"));
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
            divReport.InnerHtml = "";
            divprint.InnerHtml = "";
            divshow.Visible = false;
            StringBuilder sb = new StringBuilder();
            ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports", new string[] { "flag", "FromDate", "ToDate", "BMCTankerRoot_Id", "ItemBillingHead_Type" }, new string[] { "4", Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd"), ddlBMCTankerRootName.SelectedValue, ddlHeadType.SelectedValue }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    divshow.Visible = true;
                    int ColumnCount = ds.Tables[0].Columns.Count;
                    int RowCount = ds.Tables[0].Rows.Count;
                    sb.Append("<table class='table table-responsive'>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='2'  style='text-align:left; font-size:14px;'><b>" + Session["Office_Name"].ToString() + "</b></td>");
                    sb.Append("<td colspan=" + (ColumnCount - 2) + " style='text-align:left; font-size:14px;'><b>" + ddlBMCTankerRootName.SelectedItem.Text + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='2' style='text-align:left; font-size:14px;'><b>DCS WISE " + ddlHeadType.SelectedValue + " SUMMARY</b></td>");
                    sb.Append("<td colspan=" + (ColumnCount - 2) + " style='text-align:left; font-size:14px;'><b>PERIOD : " + txtFdt.Text + " - " + txtTdt.Text + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; border-left:1px dashed black; border-right:1px dashed black; font-size:13px;'><b>S.NO</b></td>");
                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; font-size:13px; border-left:1px dashed black; border-right:1px dashed black;'><b>DCS CODE & NAME</b></td>");
                    for (int i = 0; i < ColumnCount; i++)
                    {
                        if (ds.Tables[0].Columns[i].ToString() != "Office_Name")
                        {

                            sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; font-size:15px; border-left:1px dashed black; border-right:1px dashed black;'><b>" + ds.Tables[0].Columns[i].ToString() + "</b></td>");
                        }
                    }
                    sb.Append("</tr>");

                    for (int i = 0; i < RowCount; i++)
                    {
                        if (i == 0)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td style='border-left:1px dashed black; border-top:1px dashed black;  border-right:1px dashed black;'>" + (i + 1).ToString() + "</td>");
                            sb.Append("<td style='border-left:1px dashed black; border-top:1px dashed black;  border-right:1px dashed black;'>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                            for (int j = 0; j < ColumnCount; j++)
                            {
                                if (ds.Tables[0].Columns[j].ToString() != "Office_Name")
                                {
                                    string Columns = ds.Tables[0].Columns[j].ToString();
                                    if (ds.Tables[0].Rows[i][Columns] == DBNull.Value || ds.Tables[0].Rows[i][Columns].ToString() == null)
                                    {
                                        sb.Append("<td style='border-left:1px dashed black; border-top:1px dashed black;  border-right:1px dashed black;'>0.00</td>");
                                        // Total += 0.00;
                                    }
                                    else
                                    {
                                        sb.Append("<td style='border-left:1px dashed black; border-top:1px dashed black;  border-right:1px dashed black;'>" + ds.Tables[0].Rows[i][Columns].ToString() + "</td>");
                                        //Total += double.Parse(ds.Tables[0].Rows[i][Columns].ToString());
                                    }


                                }
                            }
                            sb.Append("</tr>");
                        }
                        else
                        {
                            if (i == (RowCount - 1))
                            {
                                sb.Append("<tr>");

                                sb.Append("<td colspan = '2' style='border-left:1px dashed black; border-top:1px dashed black; border-bottom:1px dashed black; border-right:1px dashed black; text-align:right'><b>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</b></td>");
                                for (int j = 0; j < ColumnCount; j++)
                                {
                                    if (ds.Tables[0].Columns[j].ToString() != "Office_Name")
                                    {
                                        string Columns = ds.Tables[0].Columns[j].ToString();
                                        if (ds.Tables[0].Rows[i][Columns] == DBNull.Value || ds.Tables[0].Rows[i][Columns].ToString() == null)
                                        {
                                            sb.Append("<td style='border-left:1px dashed black; border-top:1px dashed black; border-bottom:1px dashed black; border-right:1px dashed black;'><b>0.00</b></td>");
                                            // Total += 0.00;
                                        }
                                        else
                                        {
                                            sb.Append("<td style='border-left:1px dashed black; border-top:1px dashed black; border-bottom:1px dashed black; border-right:1px dashed black;'><b>" + ds.Tables[0].Rows[i][Columns].ToString() + "</b></td>");
                                            //Total += double.Parse(ds.Tables[0].Rows[i][Columns].ToString());
                                        }


                                    }
                                }
                                sb.Append("</tr>");
                            }
                            else
                            {
                                sb.Append("<tr>");
                                sb.Append("<td style='border-left:1px dashed black; border-right:1px dashed black;'>" + (i + 1).ToString() + "</td>");
                                sb.Append("<td style='border-left:1px dashed black;  border-right:1px dashed black;'>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                                for (int j = 0; j < ColumnCount; j++)
                                {
                                    if (ds.Tables[0].Columns[j].ToString() != "Office_Name")
                                    {
                                        string Columns = ds.Tables[0].Columns[j].ToString();
                                        if (ds.Tables[0].Rows[i][Columns] == DBNull.Value || ds.Tables[0].Rows[i][Columns].ToString() == null)
                                        {
                                            sb.Append("<td style='border-left:1px dashed black; border-right:1px dashed black;'>0.00</td>");
                                            // Total += 0.00;
                                        }
                                        else
                                        {
                                            sb.Append("<td style='border-left:1px dashed black; border-right:1px dashed black;'>" + ds.Tables[0].Rows[i][Columns].ToString() + "</td>");
                                            //Total += double.Parse(ds.Tables[0].Rows[i][Columns].ToString());
                                        }


                                    }
                                }
                                sb.Append("</tr>");
                            }
                        }
                    }

                    sb.Append("</table>");
                    divReport.InnerHtml = sb.ToString();
                    divprint.InnerHtml = sb.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "CCWiseAdditionDeductionReport" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            divprint.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());

            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}