using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web;

public partial class mis_mcms_reports_DcsMilkCollectionRpt : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    IFormatProvider culture = new CultureInfo("en-US", true);

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
                FillSociety();
                txtFdt.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                

            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    protected void FillSociety()
    {
        try
        {

            ds = null;
            ds = objdb.ByProcedure("sp_Mst_DailyMilkCollection",
                              new string[] { "flag", "Office_ID" },
                              new string[] { "10", ViewState["Office_ID"].ToString() }, "dataset");

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSociety.DataTextField = "Office_Name";
                    ddlSociety.DataValueField = "Office_ID";
                    ddlSociety.DataSource = ds;
                    ddlSociety.DataBind();
                    ddlSociety.Enabled = false;

                }
                else
                {
                    ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {

                lblmsgshow.Visible = false;
                string Fdate = "";

                if (txtFdt.Text != "")
                {
                    Fdate = Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd");
                }


                ds = null;

                 ds = objdb.ByProcedure("Sp_Mst_SocietyWiseMilkProcess",
                           new string[] { "flag", "EntryDate", "OfficeId", "EntryShift","EntryMode" },
                           new string[] { "20", Fdate, ddlSociety.SelectedValue, ddlShift.SelectedValue,ddlEntryMode.SelectedValue }, "dataset");

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            lblSociety.Text = ds.Tables[0].Rows[0]["V_SocietyName"].ToString() + " / " + ds.Tables[0].Rows[0]["Office_Code"].ToString();
                            lbldateshift.Text = ds.Tables[0].Rows[0]["Dt_Date"].ToString() + " / " + ds.Tables[0].Rows[0]["V_Shift"].ToString();

                            if (ds.Tables[1].Rows.Count != 0)
                            {
                                if (ds.Tables[1].Rows.Count > 0)
                                {

                                    FS_DailyReport.Visible = true;
                                    gv_Milkcollectionrpt.DataSource = ds.Tables[1];
                                    gv_Milkcollectionrpt.DataBind();

                                    Label lblI_MilkSupplyQtyTotal = (gv_Milkcollectionrpt.FooterRow.FindControl("lblI_MilkSupplyQtyTotal") as Label);
                                    Label lblTotal_Fat = (gv_Milkcollectionrpt.FooterRow.FindControl("lblTotal_Fat") as Label);
                                    Label lblTotal_SNF = (gv_Milkcollectionrpt.FooterRow.FindControl("lblTotal_SNF") as Label);
                                    Label lblTotal_CLR = (gv_Milkcollectionrpt.FooterRow.FindControl("lblTotal_CLR") as Label);
                                    Label lblmsgRate_Per_Ltr = (gv_Milkcollectionrpt.FooterRow.FindControl("lblmsgRate_Per_Ltr") as Label);
                                    Label lblNetValue = (gv_Milkcollectionrpt.FooterRow.FindControl("lblNetValue") as Label);


                                    lblI_MilkSupplyQtyTotal.Text = ds.Tables[2].Rows[0]["I_MilkSupplyQty"].ToString();
                                    lblTotal_Fat.Text = ds.Tables[2].Rows[0]["Total_Fat"].ToString();
                                    lblTotal_SNF.Text = ds.Tables[2].Rows[0]["Total_SNF"].ToString();
                                    lblTotal_CLR.Text = ds.Tables[2].Rows[0]["Total_CLR"].ToString();
                                    lblmsgRate_Per_Ltr.Text = ds.Tables[2].Rows[0]["Rate_Per_Ltr"].ToString();
                                    lblNetValue.Text = ds.Tables[2].Rows[0]["NetValue"].ToString();

                                    if (ds.Tables[3].Rows.Count != 0)
                                    {
                                        if (ds.Tables[3].Rows.Count > 0)
                                        {

                                            gv_milktypewiserpt.DataSource = ds.Tables[3];
                                            gv_milktypewiserpt.DataBind();

                                            Label lblI_MilkSupplyQtyTotal_P = (gv_milktypewiserpt.FooterRow.FindControl("lblI_MilkSupplyQtyTotal_P") as Label);
                                            Label lbltotalV_MilkType_Count = (gv_milktypewiserpt.FooterRow.FindControl("lbltotalV_MilkType_Count") as Label);

                                            lblI_MilkSupplyQtyTotal_P.Text = ds.Tables[2].Rows[0]["I_MilkSupplyQty"].ToString();
                                            lbltotalV_MilkType_Count.Text = ds.Tables[1].Rows.Count.ToString();

                                        }
                                        else
                                        {
                                            gv_milktypewiserpt.DataSource = string.Empty;
                                            gv_milktypewiserpt.DataBind();
                                        }
                                    }
                                    else
                                    {
                                        gv_milktypewiserpt.DataSource = string.Empty;
                                        gv_milktypewiserpt.DataBind();
                                    }

                                    GGT();

                                }

                                else
                                {
                                    lblmsgshow.Visible = true;
                                    lblmsgshow.Text = "No Record Found";

                                    FS_DailyReport.Visible = false;
                                    gv_Milkcollectionrpt.DataSource = string.Empty;
                                    gv_Milkcollectionrpt.DataBind();
                                }
                            }

                            else
                            {
                                lblmsgshow.Visible = true;
                                lblmsgshow.Text = "No Record Found";

                                FS_DailyReport.Visible = false;
                                gv_Milkcollectionrpt.DataSource = string.Empty;
                                gv_Milkcollectionrpt.DataBind();
                            }

                        }
                        else
                        {
                            lblmsgshow.Visible = true;
                            lblmsgshow.Text = "No Record Found";

                            FS_DailyReport.Visible = false;
                            gv_Milkcollectionrpt.DataSource = string.Empty;
                            gv_Milkcollectionrpt.DataBind();
                        }
                    }

                    else
                    {
                        lblmsgshow.Visible = true;
                        lblmsgshow.Text = "No Record Found";

                        FS_DailyReport.Visible = false;
                        gv_Milkcollectionrpt.DataSource = string.Empty;
                        gv_Milkcollectionrpt.DataBind();
                    }


                }
                else
                {
                    lblmsgshow.Visible = true;
                    lblmsgshow.Text = "No Record Found";
                    lblSociety.Text = "";

                    FS_DailyReport.Visible = false;

                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    private void ExportGridToExcel()
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = ddlSociety.SelectedItem.Text + "_MilkCollection" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gv_Milkcollectionrpt.GridLines = GridLines.Both;
            gv_Milkcollectionrpt.HeaderStyle.Font.Bold = true;
            gv_Milkcollectionrpt.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void btnexporttoexcel_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }


    public void GGT()
    {
        try
        {

            lblMsg.Text = "";
            DateTime Fromdate = DateTime.ParseExact(txtFdt.Text, "dd/MM/yyyy", culture);
            string Dt_Date_ShiftE = Fromdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);


            DataSet ds1 = objdb.ByProcedure("Usp_RptGenerateGT", new string[] {"flag"
                ,"Office_Id"
                ,"Dt_Date_ShiftE"
                ,"EntryShift_E" }, new string[] {"1"
                    ,objdb.Office_ID()
                    ,Dt_Date_ShiftE 
                    ,ddlShift.SelectedValue}, "dataset");
            if (ds1 != null)
            {
                if (ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows.Count > 0 && ds1.Tables[1].Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<table class='table2' style='width:100%'>");
                        sb.Append("<tr>");
                        sb.Append("<th>Milk Purchase</th>");
                        sb.Append("<th>Buffalo</th>");
                        sb.Append("<th>Cow</th>");
                        sb.Append("<th>Total</th>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td>Milk Quantity(Litre)</td>");
                         
                        sb.Append("<td>" + ds1.Tables[1].Rows[0]["Total_sum_MilkIn_Ltr"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[1].Rows[1]["Total_sum_MilkIn_Ltr"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[0].Rows[0]["sum_MilkIn_Ltr"].ToString() + "</td>");
                        sb.Append("</tr>");


                        sb.Append("<tr>");
                        sb.Append("<td>FAT In Kg</td>");
                        sb.Append("<td>" + ds1.Tables[1].Rows[0]["KG_FAT"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[1].Rows[1]["KG_FAT"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[0].Rows[0]["Total_KG_FAT"].ToString() + "</td>");
                        sb.Append("</tr>");
                         
                        sb.Append("<tr>");
                        sb.Append("<td>SNF  In Kg</td>");
                        sb.Append("<td>" + ds1.Tables[1].Rows[0]["KG_SNF"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[1].Rows[1]["KG_SNF"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[0].Rows[0]["Total_KG_SNF"].ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                         
                        sb.Append("<tr>");
                        sb.Append("<td>CLR  In Kg</td>");
                        sb.Append("<td>" + ds1.Tables[1].Rows[0]["KG_CLR"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[1].Rows[1]["KG_CLR"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[0].Rows[0]["Total_KG_CLR"].ToString() + "</td>");
                        sb.Append("</tr>");
                          


                        sb.Append("<tr>");
                        sb.Append("<td>FAT(%)</td>"); 
                        sb.Append("<td>" + ds1.Tables[1].Rows[0]["Avg_FatPer"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[1].Rows[1]["Avg_FatPer"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[0].Rows[0]["Total_Avg_FatPer"].ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td>SNF(%)</td>");

                        sb.Append("<td>" + ds1.Tables[1].Rows[0]["Avg_SNFPer"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[1].Rows[1]["Avg_SNFPer"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[0].Rows[0]["Total_Avg_SNFPer"].ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td>CLR(%)</td>");

                        sb.Append("<td>" + ds1.Tables[1].Rows[0]["Avg_CLRPer"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[1].Rows[1]["Avg_CLRPer"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[0].Rows[0]["Total_Avg_CLRPer"].ToString() + "</td>");
                        sb.Append("</tr>");

                        sb.Append("<tr>");
                        sb.Append("<td>Amount</td>");

                        sb.Append("<td>" + ds1.Tables[1].Rows[0]["Amount"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[1].Rows[1]["Amount"].ToString() + "</td>");
                        sb.Append("<td>" + ds1.Tables[0].Rows[0]["Amount"].ToString() + "</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");

                        dvreport.InnerHtml = sb.ToString();
                    }
                    else
                    {
                        dvreport.InnerHtml = "";

                    }
                }
                else
                {
                    dvreport.InnerHtml = "";

                }
            }
            else
            {
                dvreport.InnerHtml = "";

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }


}