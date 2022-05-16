using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;

public partial class mis_MilkCollection_DSAdditionDeductionReport_BefInv : System.Web.UI.Page
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
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                GetDSDetails();
               
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void GetDSDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpAdminOffice",
                          new string[] { "flag", "OfficeType_ID", "Office_ID" },
                          new string[] { "41", objdb.OfficeType_ID(), objdb.Office_ID() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                if (objdb.OfficeType_ID() == "2")
                {
                    ddlOffice.DataSource = ds;
                    ddlOffice.DataTextField = "Office_Name";
                    ddlOffice.DataValueField = "Office_ID";
                    ddlOffice.DataBind();
                    ddlOffice.Items.Insert(0, new ListItem("Select", "0"));
                    ddlOffice.SelectedValue = ViewState["Office_ID"].ToString();
                }
                else
                {
                    ddlOffice.DataSource = ds;
                    ddlOffice.DataTextField = "Office_Name";
                    ddlOffice.DataValueField = "Office_ID";
                    ddlOffice.DataBind();

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
            divReport.InnerHtml = "";
            divprint.InnerHtml = "";
            divshow.Visible = false;
            StringBuilder sb = new StringBuilder();
            ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports", new string[] { "flag", "FromDate", "ToDate", "ItemBillingHead_Type", "GenerateFrom_Office_ID" }, new string[] { "40", Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd"), ddlHeadType.SelectedValue,objdb.Office_ID() }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    divshow.Visible = true;
                    int ColumnCount = ds.Tables[0].Columns.Count;
                    int RowCount = ds.Tables[0].Rows.Count;
                    sb.Append("<table class='table table-responsive' style='width:100%; overflow:scroll;'>");
					sb.Append("<thead class='header'>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4'  style='text-align:left; font-size:14px;'><b>" + Session["Office_Name"].ToString() + "</b></td>");

                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='4' style='text-align:left; font-size:14px;'><b>" + ddlHeadType.SelectedValue + " SUMMARY</b></td>");
                    sb.Append("<td colspan=" + (ColumnCount - 2) + " style='text-align:left; font-size:14px;'><b>PERIOD : " + txtFdt.Text + " - " + txtTdt.Text + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; border-left:1px dashed black; border-right:1px dashed black; font-size:13px;'><b>S.NO</b></td>");
                    sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; font-size:13px; border-left:1px dashed black; border-right:1px dashed black;'><b>CC NAME</b></td>");
                    for (int i = 0; i < ColumnCount; i++)
                    {
                        if (ds.Tables[0].Columns[i].ToString() != "Office_Name")
                       {

                        sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; font-size:15px; border-left:1px dashed black; border-right:1px dashed black;'><b>" + ds.Tables[0].Columns[i].ToString() + "</b></td>");
                    }
                        
                    }
                    if(ddlHeadType.SelectedValue == "ADDITION")
                    {
                        sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; font-size:15px; border-left:1px dashed black; border-right:1px dashed black;'><b>Total</b></td>");  
                    }
                    else
                    {
                        sb.Append("<td style='border-bottom:1px dashed black; border-top:1px dashed black; font-size:15px; border-left:1px dashed black; border-right:1px dashed black;'><b>Total</b></td>");  
                    }  
                    sb.Append("</tr>");
					sb.Append("</thead>");
                    decimal GrandTotal = 0;
                    for (int i = 0; i < RowCount; i++)
                    {

                        decimal Total = 0;
                        
                        if(i == 0)
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
                                        if (Columns == "Quantity" || Columns == "KGFAT" || Columns == "KGSNF" || Columns == "TotalDeduction" || Columns == "TotalEarning")
                                        {
                                            if (Columns == "TotalDeduction")
                                            {
                                                Total -= decimal.Parse(ds.Tables[0].Rows[i][Columns].ToString());
                                            }
                                        }
                                        else
                                        {
                                            Total += decimal.Parse(ds.Tables[0].Rows[i][Columns].ToString());
                                            
                                            
                                            
                                        }
                                       
                                        
                                        
                                    }
                                    

                                }
                            }
                            sb.Append("<td style='border-left:1px dashed black; border-top:1px dashed black;  border-right:1px dashed black;'>" + Total.ToString() + "</td>");
                             sb.Append("</tr>");
                             GrandTotal += Total;
                            
                        }
                        else
                        {
                            if(i == (RowCount - 1))
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
                                            //Total += decimal.Parse(ds.Tables[0].Rows[i][Columns].ToString());
                                        }


                                    }
                                }

                                sb.Append("<td style='border-left:1px dashed black; border-top:1px dashed black; border-bottom:1px dashed black; border-right:1px dashed black;'>" + GrandTotal.ToString() + "</td>");
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
                                            if (Columns == "Quantity" || Columns == "KGFAT" || Columns == "KGSNF" || Columns == "TotalDeduction" || Columns == "TotalEarning")
                                            {
                                                if (Columns == "TotalDeduction")
                                                {
                                                    Total -= decimal.Parse(ds.Tables[0].Rows[i][Columns].ToString());
                                                }
                                            }
                                            else
                                            {
                                                Total += decimal.Parse(ds.Tables[0].Rows[i][Columns].ToString());
                                            }
                                          
                                            
                                        }


                                    }
                                }
                                sb.Append("<td style='border-left:1px dashed black; border-right:1px dashed black;'>" + Total.ToString() + "</td>");
                                sb.Append("</tr>");
                                GrandTotal += Total;
                                
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
            Response.AddHeader("content-disposition", "attachment;filename=" + "UnionWiseAdditionDeductionReport" + DateTime.Now + ".xls");
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