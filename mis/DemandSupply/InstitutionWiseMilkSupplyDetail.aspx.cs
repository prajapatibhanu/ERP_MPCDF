using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_DemandSupply_InstitutionWiseMilkSupplyDetail : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds5, ds6 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetLocation();
                GetCategory();
                GetOfficeDetails();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================

    protected void GetLocation()
    {
        try
        {
            ddlLocation.DataTextField = "AreaName";
            ddlLocation.DataValueField = "AreaId";
            ddlLocation.DataSource = objdb.ByProcedure("USP_Mst_Area",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void GenerateReport()
    {
        try
        {
            lblMsg.Text = "";
            btnPrint.Visible = false;
            btnExcel.Visible = false;
            div_page_content.InnerHtml = "";
            DateTime Fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            string FDate = Fromdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            DateTime Todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string TDate = Todate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string Institution = ddlInstitution.SelectedItem.Text;
            string[] InstName = Institution.Split('[');
            Institution = InstName[0];
            ds1 = objdb.ByProcedure("USP_InstitutionwiseMilkSupplyDetailRpt", 
                new string[] { "flag", "FromDate", "ToDate", "Office_ID", "ItemCat_id", "BoothId" },
                new string[] { "1", FDate,TDate,objdb.Office_ID(),ddlItemCategory.SelectedValue,ddlInstitution.SelectedValue }, "dataset");
            if(ds1 != null && ds1.Tables.Count > 0)
            {
                if(ds1.Tables[0].Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();

                    
                    //sb.Append("<table style='width:100%; height:100%'>");
                    //sb.Append("<thead>");
                    //sb.Append("<tr>");
                    //sb.Append("<td></td>");
                    //sb.Append("<td style='padding: 2px 5px;text-align: center;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                    //sb.Append("</tr>");
                    //sb.Append("<tr>");
                    //sb.Append("<td></td>");
                    //sb.Append("<td style='padding: 2px 5px;text-align: center;border-bottom:1px dashed black !important;'>MILK SUPPLY A/C    " + Institution + "</td>");
                    //sb.Append("</tr>");
                    
                    //sb.Append("<tr>");
                    // sb.Append("<td></td>");
                    //sb.Append("<td style='padding: 2px 5px;text-align: center;border-top:1px dashed black !important;'>From  -" + txtFromDate.Text + "  To - " + txtToDate.Text + "</td>");
                    //sb.Append("</tr>");
                    //sb.Append("</thead>");
                    //sb.Append("</table>");
                    
                    
                    sb.Append("<table class='table'>");
                    int Count = ds1.Tables[0].Rows.Count;
                    int ColCount = ds1.Tables[0].Columns.Count;
                    int Count1 = ds1.Tables[1].Rows.Count;
                    int ColCount1 = ds1.Tables[1].Columns.Count;
                    sb.Append("<thead>");
                    sb.Append("<tr>");
                    
                    sb.Append("<td colspan='" + (ColCount + ColCount1) + "' style='padding: 2px 5px;text-align: center;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    
                    sb.Append("<td colspan='" + (ColCount + ColCount1) + "' style='padding: 2px 5px;text-align: center;border-bottom:1px dashed black !important;'>MILK SUPPLY A/C    " + Institution + "</td>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                   
                    sb.Append("<td colspan='" + (ColCount + ColCount1) + "' style='padding: 2px 5px;text-align: center;border-top:1px dashed black !important;'>From  -" + txtFromDate.Text + "  To - " + txtToDate.Text + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td  colspan='" + (ColCount-2 )+ "' style='text-align: center;'>MILK SUPPLY");
                    sb.Append("</td>");
                    sb.Append("<td colspan='" + (ColCount1-3 ) + "' style='text-align: center;'>MILK RETURN");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border-bottom:1px dashed black !important;'>Date<br>Shift</td>");                   
                    for (int j = 0; j < ColCount; j++)
                    {

                        //if (ds1.Tables[0].Columns[j].ToString() != "S.No." && ds1.Tables[0].Columns[j].ToString() != "RouteId" && ds1.Tables[0].Columns[j].ToString() != "Route" && ds1.Tables[0].Columns[j].ToString() != "Total Supply" && ds1.Tables[0].Columns[j].ToString() != "Total Crate" && ds1.Tables[0].Columns[j].ToString() != "Total Supply in Litre")
                        if (ds1.Tables[0].Columns[j].ToString() != "Demand_Date" && ds1.Tables[0].Columns[j].ToString() != "ShiftName" && ds1.Tables[0].Columns[j].ToString() != "Shift_id")
                        {
                            string ColName = ds1.Tables[0].Columns[j].ToString();
                            if (j == (ColCount - 1))
                            {
                                sb.Append("<td style='border-bottom:1px dashed black !important; border-right:1px solid black !important;'>" + ColName + "</td>");
                            }
                            else
                            {
                                sb.Append("<td style='border-bottom:1px dashed black !important; '>" + ColName + "</td>");
                            }
                            

                        }

                    }
                    for (int j = 0; j < ColCount1; j++)
                    {

                        //if (ds1.Tables[0].Columns[j].ToString() != "S.No." && ds1.Tables[0].Columns[j].ToString() != "RouteId" && ds1.Tables[0].Columns[j].ToString() != "Route" && ds1.Tables[0].Columns[j].ToString() != "Total Supply" && ds1.Tables[0].Columns[j].ToString() != "Total Crate" && ds1.Tables[0].Columns[j].ToString() != "Total Supply in Litre")
                        if (ds1.Tables[1].Columns[j].ToString() != "Demand_Date" && ds1.Tables[1].Columns[j].ToString() != "ShiftName" && ds1.Tables[1].Columns[j].ToString() != "Shift_id")
                        {
                            string ColName = ds1.Tables[1].Columns[j].ToString();
                            sb.Append("<td style='border-bottom:1px dashed black !important;'>" + ColName + "</td>");

                        }

                    }
                   
                    sb.Append("</tr>");
                    sb.Append("</thead>");

                    for (int i = 0; i < Count; i++)
                    {

                        sb.Append("<tr>");
                        if (i == 0)
                        {
                            sb.Append("<td style='border-top:1px dashed black !important;'>" + ds1.Tables[0].Rows[i]["Demand_Date"].ToString() + "/" + ds1.Tables[0].Rows[i]["ShiftName"].ToString() + "</td>");
                        }
                        else
                        {
                            sb.Append("<td>" + ds1.Tables[0].Rows[i]["Demand_Date"].ToString() + "/" + ds1.Tables[0].Rows[i]["ShiftName"].ToString() + "</td>");
                        }
                        for (int K = 0; K < ColCount; K++)
                        {

                            //if (ds1.Tables[0].Columns[K].ToString() != "S.No." && ds1.Tables[0].Columns[K].ToString() != "RouteId" && ds1.Tables[0].Columns[K].ToString() != "Route" && ds1.Tables[0].Columns[K].ToString() != "Total Supply" && ds1.Tables[0].Columns[K].ToString() != "Total Crate" && ds1.Tables[0].Columns[K].ToString() != "Total Supply in Litre")
                            if (ds1.Tables[0].Columns[K].ToString() != "Demand_Date" && ds1.Tables[0].Columns[K].ToString() != "ShiftName" && ds1.Tables[0].Columns[K].ToString() != "Shift_id")
                            {
                                string ColName = ds1.Tables[0].Columns[K].ToString();
                                if(i == 0)
                                {
                                    if (ds1.Tables[0].Rows[i][ColName].ToString() == "")
                                    {
                                        sb.Append("<td style='border-top:1px dashed black !important;'>0</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style='border-top:1px dashed black !important;'>" + ds1.Tables[0].Rows[i][ColName].ToString() + "</td>");
                                    }
                                }
                                else
                                {  
                                    if(ds1.Tables[0].Rows[i][ColName].ToString()=="")
                                    {
                                        sb.Append("<td>0</td>");
                                    } 
                                    else
                                    {
                                        sb.Append("<td>" + ds1.Tables[0].Rows[i][ColName].ToString() + "</td>");
                                    }                                                                                                                                                                                                                                                                                                                                                                                                                                                      
                                    
                                }

                            }

                        }
                        for (int K = 0; K < ColCount1; K++)
                        {
                            //if (ds1.Tables[0].Columns[K].ToString() != "S.No." && ds1.Tables[0].Columns[K].ToString() != "RouteId" && ds1.Tables[0].Columns[K].ToString() != "Route" && ds1.Tables[0].Columns[K].ToString() != "Total Supply" && ds1.Tables[0].Columns[K].ToString() != "Total Crate" && ds1.Tables[0].Columns[K].ToString() != "Total Supply in Litre")
                            if (ds1.Tables[1].Columns[K].ToString() != "Demand_Date" && ds1.Tables[1].Columns[K].ToString() != "ShiftName" && ds1.Tables[1].Columns[K].ToString() != "Shift_id")
                            {
                                string ColName = ds1.Tables[1].Columns[K].ToString();
                                if (i == 0)
                                {
                                    if (ds1.Tables[1].Rows[i][ColName].ToString()=="")
                                    {
                                        sb.Append("<td style='border-top:1px dashed black !important;'>0</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style='border-top:1px dashed black !important; '>" + ds1.Tables[1].Rows[i][ColName].ToString() + "</td>");
                                    }
                                    
                                }
                                else
                                {
                                    if (ds1.Tables[1].Rows[i][ColName].ToString() == "")
                                    {
                                        sb.Append("<td>0</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td>" + ds1.Tables[1].Rows[i][ColName].ToString() + "</td>");
                                    }
                                }

                            }

                        }
                       
                        sb.Append("</tr>");

                    }
                   
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>Total</td>");
                    DataTable dt = new DataTable();
                    dt = ds1.Tables[0];
                    int sum11 = 0;
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() != "Demand_Date" && column.ToString() != "ShiftName" && column.ToString() != "Shift_id")
                        {

                            //sum11 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));
                            sum11 = dt.AsEnumerable().Sum(r => r.Field<int?>("" + column + "") ?? 0);
                            sb.Append("<td>" + sum11.ToString() + "</td>");
                            
                        }
                    }

                  
                   
                   
                   
                   
                   
                    DataTable dt1 = new DataTable();
                    dt1 = ds1.Tables[1];
                    int sum12 = 0;
                    foreach (DataColumn column1 in dt1.Columns)
                    {
                        if (column1.ToString() != "Demand_Date" && column1.ToString() != "ShiftName" && column1.ToString() != "Shift_id")
                        {

                            //sum12 = Convert.ToInt32(dt1.Compute("SUM([" + column + "])", string.Empty));
                            sum12 = dt1.AsEnumerable().Sum(r1 => r1.Field<int?>("" + column1 + "") ?? 0);
                            sb.Append("<td>" + sum12.ToString() + "</td>");

                        }
                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    
                   
                    ////////////////End Of Route Wise Print Code   ///////////////////////
                    btnPrint.Visible = true;
                    btnExcel.Visible = true;
                    div_page_content.InnerHtml = sb.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    private void GetRoute()
    {
        try
        {

            ddlRoute.Items.Clear();
            ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                     new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                     new string[] { "7", objdb.Office_ID(), ddlLocation.SelectedValue, ddlItemCategory.SelectedValue }, "dataset");
            ddlRoute.DataTextField = "RName";
            ddlRoute.DataValueField = "RouteId";
            ddlRoute.DataBind();
            ddlRoute.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 3: ", ex.Message.ToString());
        }
    }

    private void GetInstitution()
    {
        try
        {

            ddlInstitution.DataSource = objdb.ByProcedure("USP_Mst_BoothReg",
                 new string[] { "flag", "Office_ID", "RouteId" },
                 new string[] { "16", objdb.Office_ID(), ddlRoute.SelectedValue }, "dataset");
            ddlInstitution.DataTextField = "BName";
            ddlInstitution.DataValueField = "BoothId";
            ddlInstitution.DataBind();
            ddlInstitution.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 2:", ex.Message.ToString());
        }
    }

    protected void GetCategory()
    {
        try
        {
            ddlItemCategory.DataTextField = "ItemCatName";
            ddlItemCategory.DataValueField = "ItemCat_id";
            ddlItemCategory.DataSource = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlItemCategory.DataBind();
            ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void GetOfficeDetails()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                ViewState["Office_Name"] = ds1.Tables[0].Rows[0]["Office_Name"].ToString();
                ViewState["Office_ContactNo"] = ds1.Tables[0].Rows[0]["Office_ContactNo"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    #endregion========================================================
    #region=========== init or changed even===========================

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0")
        {
            lblMsg.Text = string.Empty;
            GetRoute();
        }
    }

    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        GetInstitution();
    }
    #endregion===========================
    #region=========== click event for grdiview row bound event===========================

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

            GenerateReport();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        ddlRoute.SelectedIndex = 0;
        ddlInstitution.SelectedIndex = 0;
        ddlItemCategory.SelectedIndex = 0;
       // pnlData.Visible = false;
        ddlLocation.SelectedIndex = 0;
        ddlRoute.Items.Clear();
        ddlRoute.Items.Insert(0, new ListItem("Select", "0"));

    }


    #endregion===========================
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename="  + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            div_page_content.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
}