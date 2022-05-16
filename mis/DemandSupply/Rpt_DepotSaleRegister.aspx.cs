using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;
using System.IO;
using System.Data;

public partial class mis_DemandSupply_Rpt_DepotSaleRegister : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds3, ds5 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetOfficeDetails();
                GetLocation();
                GetDistributor();
                txtMonth.Attributes.Add("readonly", "true");
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
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
            ddlLocation.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    private void GetDistributor()
    {
        try
        {
            ddlDistributor.DataTextField = "DRName";
            ddlDistributor.DataValueField = "RouteId";
            ddlDistributor.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "AreaId", "ItemCat_id", "Office_ID" },
                   new string[] { "12", ddlLocation.SelectedValue, objdb.GetMilkCatId(), objdb.Office_ID() }, "dataset");
            ddlDistributor.DataBind();
            ddlDistributor.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0")
        {
            lblMsg.Text = string.Empty;
            GetDistributor();
        }
    }
    protected void GetOfficeDetails()
    {
        try
        {
            ds3 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply", new string[] { "flag", "Office_ID" }, new string[] { "9", objdb.Office_ID() }, "dataset");
            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {
                ViewState["Office_Name"] = ds3.Tables[0].Rows[0]["Office_Name"].ToString();
                ViewState["Office_ContactNo"] = ds3.Tables[0].Rows[0]["Office_ContactNo"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null)
            {
                ds3.Dispose();
            }
        }
    }
    protected void GenerateReport()
    {
        try
        {
            lblMsg.Text = "";
            btnPrint.Visible = false;
            btnExcel.Visible = false;
            string Office_ID = objdb.Office_ID();
            string catid = objdb.GetMilkCatId();
            div_page_content.InnerHtml = "";
            Print.InnerHtml = "";
            string fm = "01/" + txtMonth.Text;
            //  Code for current month date
            DateTime fmonth = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
            string fromnonth = fmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            DateTime addmonth = fmonth.AddMonths(1);
            DateTime minusday = addmonth.AddDays(-1);

            string tmnth = minusday.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tmonth = DateTime.ParseExact(tmnth, "dd/MM/yyyy", culture);
            string tomonth = tmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            string tm = tmonth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            string DistributorName = ddlDistributor.SelectedItem.Text;
            string[] DistName = DistributorName.Split('-');

            ds5 = objdb.ByProcedure("USP_Trn_DepotMilkSaleReport"
                , new string[] { "Flag", "FromDate", "ToDate", "Office_ID", "ItemCat_id", "RouteId" }
                , new string[] { "1", fromnonth, tomonth, objdb.Office_ID(), objdb.GetMilkCatId(), ddlDistributor.SelectedValue }
                , "dataset");
            if (ds5 != null && ds5.Tables.Count > 0)
            {
                if (ds5.Tables[0].Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();


                    sb.Append("<div class='table-responsive'>");
                    sb.Append("<table class='table'>");
                    int Count = ds5.Tables[0].Rows.Count;
                    int ColCount = ds5.Tables[0].Columns.Count;
                    int Count1 = ds5.Tables[1].Rows.Count;
                    int ColCount1 = ds5.Tables[1].Columns.Count;

                    int Count2 = ds5.Tables[2].Rows.Count;
                    int ColCount2 = ds5.Tables[2].Columns.Count;

                    int Count3 = ds5.Tables[3].Rows.Count;
                    int ColCount3 = ds5.Tables[3].Columns.Count;

                    int Count4 = ds5.Tables[4].Rows.Count;
                    int ColCount4 = ds5.Tables[4].Columns.Count;

                    int Count5 = ds5.Tables[5].Rows.Count;
                    int ColCount5 = ds5.Tables[5].Columns.Count;
                    int Count6 = ds5.Tables[6].Rows.Count;
                    int ColCount6 = ds5.Tables[6].Columns.Count;

                    sb.Append("<thead>");
                    sb.Append("<tr>");

                    sb.Append("<td colspan='" + (ColCount + ColCount1 + ColCount2 + ColCount3 + ColCount4 + 3) + "' style='padding: 2px 5px;text-align: center;font-size: 15px;'><b>" + ViewState["Office_Name"].ToString() + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");

                    sb.Append("<td colspan='" + (ColCount + ColCount1 + ColCount2 + ColCount3 + ColCount4 + 3) + "' style='padding: 2px 5px;text-align: center;font-size: 13px;'><b>DEPOT SALE REGISTER</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");

                    sb.Append("<td colspan='" + (ColCount + ColCount1 + ColCount2 + ColCount3 + ColCount4 + 3) + "' style='padding: 2px 5px;text-align: center;font-size: 12px;'><b>    " + DistName[0] + "-" + DistName[1] + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");

                    sb.Append("<td colspan='" + (ColCount + ColCount1 + ColCount2 + ColCount3 + ColCount4 + 3) + "' style='padding: 2px 5px;text-align: center;'>From  -" + fm + "  To - " + tm + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td  colspan='" + (ColCount - 3) + "'style='text-align: center;border:1px solid black; border-right:2px solid black;'><b>ISSUE</b>");
                    sb.Append("</td>");
                    sb.Append("<td colspan='" + (ColCount1 - 4) + "' style='text-align: center;border:1px solid black; border-left:2px solid black;'><b>RETURN</b>");
                    sb.Append("</td>");
                    sb.Append("<td colspan='" + (ColCount2 - 4) + "' style='text-align: center;border:1px solid black; border-left:2px solid black;'><b>ADVANCE</b>");
                    sb.Append("</td>");
                    sb.Append("<td colspan='" + (ColCount3 - 4) + "' style='text-align: center;border:1px solid black; border-left:2px solid black;'><b>CREDIT</b>");
                    sb.Append("</td>");
                    sb.Append("<td colspan='" + (ColCount4 - 4) + "' style='text-align: center;border:1px solid black; border-left:2px solid black;'><b>CASH</b>");
                    sb.Append("</td>");
                    sb.Append("<td rowspan='2' style='text-align: center;border:1px solid black; border-left:2px solid black !important; border-right:2px solid black;'><b>Total Value</b>");
                    sb.Append("</td>");
                    sb.Append("<td rowspan='2' style='text-align: center;border:1px solid black; border-left:2px solid black !important; border-right:2px solid black;'><b>TCSTAX AMT on Total Value</b>");
                    sb.Append("</td>");
                    sb.Append("<td rowspan='2' style='text-align: center;border:1px solid black; border-left:2px solid black !important; border-right:2px solid black;'><b>Amount Deposited</b>");
                    sb.Append("</td>");
                    sb.Append("<td rowspan='2' style='text-align: center;border:1px solid black; border-left:2px solid black !important; border-right:2px solid black;'><b>R.T.G.S./Ch.No.&Date</b>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black !important;'><b>Date<br>Shift</b></td>");
                    for (int j = 0; j < ColCount; j++)//FOR ISSUE
                    {
                        if (ds5.Tables[0].Columns[j].ToString() != "DDate" && ds5.Tables[0].Columns[j].ToString() != "Delivary_Date" && ds5.Tables[0].Columns[j].ToString() != "DelivaryShift_id" && ds5.Tables[0].Columns[j].ToString() != "ShiftName")
                        {
                            string ColName = ds5.Tables[0].Columns[j].ToString();
                            if (j == (ColCount - 3))
                            {
                                sb.Append("<td style='border-right:2px solid black; border-bottom:1px solid black;'><b>" + ColName + "</b></td>");
                            }
                            else
                            {
                                sb.Append("<td style='border:1px solid black !important; '><b>" + ColName + "</b></td>");
                            }


                        }

                    }
                    for (int j = 0; j < ColCount1; j++)//FOR RETURN
                    {

                        if (ds5.Tables[1].Columns[j].ToString() != "DDate" && ds5.Tables[1].Columns[j].ToString() != "Delivary_Date" && ds5.Tables[1].Columns[j].ToString() != "DelivaryShift_id" && ds5.Tables[1].Columns[j].ToString() != "ShiftName")
                        {
                            string ColName = ds5.Tables[1].Columns[j].ToString();
                            if (j == 0)
                            {
                                sb.Append("<td style='border-left:2px solid black;'><b>" + ColName + "</b></td>");
                            }
                            else if (j == (ColCount1 - 3))
                            {
                                sb.Append("<td style='border-right:2px solid black; border-bottom:1px solid black;'><b>" + ColName + "</b></td>");
                            }
                            else
                            {
                                sb.Append("<td style='border:1px solid black !important; '><b>" + ColName + "</b></td>");
                            }

                        }

                    }

                    for (int j = 0; j < ColCount2; j++)//FOR ADVANCE
                    {

                        if (ds5.Tables[2].Columns[j].ToString() != "DDate" && ds5.Tables[2].Columns[j].ToString() != "Delivary_Date" && ds5.Tables[2].Columns[j].ToString() != "DelivaryShift_id" && ds5.Tables[2].Columns[j].ToString() != "ShiftName")
                        {
                            string ColName = ds5.Tables[2].Columns[j].ToString();
                            if (j == 0)
                            {
                                sb.Append("<td style='border-left:2px solid black;'><b>" + ColName + "</b></td>");
                            }
                            else if (j == (ColCount2 - 3))
                            {
                                sb.Append("<td style='border-right:2px solid black; border-bottom:1px solid black;'><b>" + ColName + "</b></td>");
                            }
                            else
                            {
                                sb.Append("<td style='border:1px solid black !important; '><b>" + ColName + "</b></td>");
                            }

                        }

                    }
                    for (int j = 0; j < ColCount3; j++)//FOR CREDIT
                    {

                        if (ds5.Tables[3].Columns[j].ToString() != "DDate" && ds5.Tables[3].Columns[j].ToString() != "Delivary_Date" && ds5.Tables[3].Columns[j].ToString() != "DelivaryShift_id" && ds5.Tables[3].Columns[j].ToString() != "ShiftName")
                        {
                            string ColName = ds5.Tables[3].Columns[j].ToString();
                            if (j == 0)
                            {
                                sb.Append("<td style='border-left:2px solid black;'><b>" + ColName + "</b></td>");
                            }
                            else if (j == (ColCount3 - 3))
                            {
                                sb.Append("<td style='border-right:2px solid black; border-bottom:1px solid black;'><b>" + ColName + "</b></td>");
                            }
                            else
                            {
                                sb.Append("<td style='border:1px solid black !important; '><b>" + ColName + "</b></td>");
                            }

                        }

                    }
                    for (int j = 0; j < ColCount4; j++)//FOR CASH
                    {

                        if (ds5.Tables[4].Columns[j].ToString() != "DDate" && ds5.Tables[4].Columns[j].ToString() != "Delivary_Date" && ds5.Tables[4].Columns[j].ToString() != "DelivaryShift_id" && ds5.Tables[4].Columns[j].ToString() != "ShiftName")
                        {
                            string ColName = ds5.Tables[4].Columns[j].ToString();
                            if (j == 0)
                            {
                                sb.Append("<td style='border-left:2px solid black;'><b>" + ColName + "</b></td>");
                            }
                            else if (j == (ColCount4 - 2))
                            {
                                sb.Append("<td style='border-right:2px solid black; border-bottom:1px solid black;'><b>" + ColName + "</b></td>");
                            }
                            else
                            {
                                sb.Append("<td style='border:1px solid black !important; '><b>" + ColName + "</b></td>");
                            }

                        }

                    }

                    sb.Append("</tr>");
                    sb.Append("</thead>");

                    for (int i = 0; i < Count; i++)
                    {

                        sb.Append("<tr>");
                        if (i == 0)
                        {
                            sb.Append("<td style='border:1px solid black !important;'>" + ds5.Tables[0].Rows[i]["DDate"].ToString() + "/" + ds5.Tables[0].Rows[i]["ShiftName"].ToString() + "</td>");
                        }
                        else
                        {
                            sb.Append("<td style='border:1px solid black !important;'>" + ds5.Tables[0].Rows[i]["DDate"].ToString() + "/" + ds5.Tables[0].Rows[i]["ShiftName"].ToString() + "</td>");
                        }
                        for (int K = 0; K < ColCount; K++) // FOR ISSUE
                        {

                            if (ds5.Tables[0].Columns[K].ToString() != "DDate" && ds5.Tables[0].Columns[K].ToString() != "Delivary_Date" && ds5.Tables[0].Columns[K].ToString() != "ShiftName" && ds5.Tables[0].Columns[K].ToString() != "DelivaryShift_id")
                            {
                                string ColName = ds5.Tables[0].Columns[K].ToString();
                                if (K == 0)
                                {
                                    if (ds5.Tables[0].Rows[i][ColName].ToString() == "")
                                    {
                                        sb.Append("<td style='border:1px solid black !important;'>0</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style='border:1px solid black !important;'>" + ds5.Tables[0].Rows[i][ColName].ToString() + "</td>");
                                    }
                                }
                                else if (K == (ColCount - 3))
                                {
                                    if (ds5.Tables[0].Rows[i][ColName].ToString() == "")
                                    {
                                        sb.Append("<td style='border-right:2px solid black !important; border-bottom:1px solid black;'>0</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style='border-right:2px solid black !important; border-bottom:1px solid black;'>" + ds5.Tables[0].Rows[i][ColName].ToString() + "</td>");
                                    }
                                }
                                else
                                {
                                    if (ds5.Tables[0].Rows[i][ColName].ToString() == "")
                                    {
                                        sb.Append("<td style='border:1px solid black !important;'>0</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style='border:1px solid black !important;'>" + ds5.Tables[0].Rows[i][ColName].ToString() + "</td>");
                                    }

                                }

                            }

                        }
                        for (int K = 0; K < ColCount1; K++) // FOR RETURN
                        {
                            if (ds5.Tables[1].Columns[K].ToString() != "DDate" && ds5.Tables[1].Columns[K].ToString() != "Delivary_Date" && ds5.Tables[1].Columns[K].ToString() != "ShiftName" && ds5.Tables[1].Columns[K].ToString() != "DelivaryShift_id")
                            {
                                string ColName = ds5.Tables[1].Columns[K].ToString();
                                if (K == 0)
                                {
                                    if (ds5.Tables[1].Rows[i][ColName].ToString() == "")
                                    {
                                        sb.Append("<td style='border-left:2px solid black !important;'>0</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style='border-left:2px solid black !important;'>" + ds5.Tables[1].Rows[i][ColName].ToString() + "</td>");
                                    }

                                }
                                else if (K == (ColCount1 - 3))
                                {
                                    if (ds5.Tables[1].Rows[i][ColName].ToString() == "")
                                    {
                                        sb.Append("<td style='border-right:2px solid black !important; border-bottom:1px solid black;'>0</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style='border-right:2px solid black !important; border-bottom:1px solid black;'>" + ds5.Tables[1].Rows[i][ColName].ToString() + "</td>");
                                    }
                                }
                                else
                                {
                                    if (ds5.Tables[1].Rows[i][ColName].ToString() == "")
                                    {
                                        sb.Append("<td style='border:1px solid black !important;'>0</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style='border:1px solid black !important;'>" + ds5.Tables[1].Rows[i][ColName].ToString() + "</td>");
                                    }
                                }

                            }

                        }
                        for (int A = 0; A < ColCount2; A++) // FOR ADVANCE
                        {
                            if (ds5.Tables[2].Columns[A].ToString() != "DDate" && ds5.Tables[2].Columns[A].ToString() != "Delivary_Date" && ds5.Tables[2].Columns[A].ToString() != "ShiftName" && ds5.Tables[2].Columns[A].ToString() != "DelivaryShift_id")
                            {
                                string ColName = ds5.Tables[2].Columns[A].ToString();
                                if (A == 0)
                                {
                                    if (ds5.Tables[2].Rows[i][ColName].ToString() == "")
                                    {
                                        sb.Append("<td style='border-left:2px solid black !important;'>0</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style='border-left:2px solid black !important;'>" + ds5.Tables[2].Rows[i][ColName].ToString() + "</td>");
                                    }

                                }
                                else if (A == (ColCount2 - 3))
                                {
                                    if (ds5.Tables[2].Rows[i][ColName].ToString() == "")
                                    {
                                        sb.Append("<td style='border-right:2px solid black !important; border-bottom:1px solid black;'>0</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style='border-right:2px solid black !important; border-bottom:1px solid black;'>" + ds5.Tables[2].Rows[i][ColName].ToString() + "</td>");
                                    }
                                }
                                else
                                {
                                    if (ds5.Tables[2].Rows[i][ColName].ToString() == "")
                                    {
                                        sb.Append("<td style='border:1px solid black !important;'>0</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style='border:1px solid black !important;'>" + ds5.Tables[2].Rows[i][ColName].ToString() + "</td>");
                                    }
                                }

                            }

                        }
                        for (int C = 0; C < ColCount3; C++) // FOR CREDIT
                        {
                            if (ds5.Tables[3].Columns[C].ToString() != "DDate" && ds5.Tables[3].Columns[C].ToString() != "Delivary_Date" && ds5.Tables[3].Columns[C].ToString() != "ShiftName" && ds5.Tables[3].Columns[C].ToString() != "DelivaryShift_id")
                            {
                                string ColName = ds5.Tables[3].Columns[C].ToString();
                                if (C == 0)
                                {
                                    if (ds5.Tables[3].Rows[i][ColName].ToString() == "")
                                    {
                                        sb.Append("<td style='border-left:2px solid black !important;'>0</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style='border-left:2px solid black !important;'>" + ds5.Tables[C].Rows[i][ColName].ToString() + "</td>");
                                    }

                                }
                                else if (C == (ColCount3 - 3))
                                {
                                    if (ds5.Tables[3].Rows[i][ColName].ToString() == "")
                                    {
                                        sb.Append("<td style='border-right:2px solid black !important; border-bottom:1px solid black;'>0</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style='border-right:2px solid black !important; border-bottom:1px solid black;'>" + ds5.Tables[3].Rows[i][ColName].ToString() + "</td>");
                                    }
                                }
                                else
                                {
                                    if (ds5.Tables[3].Rows[i][ColName].ToString() == "")
                                    {
                                        sb.Append("<td style='border:1px solid black !important;'>0</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style='border:1px solid black !important;'>" + ds5.Tables[3].Rows[i][ColName].ToString() + "</td>");
                                    }
                                }

                            }

                        }

                        for (int D = 0; D < ColCount4; D++) // FOR CASH
                        {
                            if (ds5.Tables[4].Columns[D].ToString() != "DDate" && ds5.Tables[4].Columns[D].ToString() != "Delivary_Date" && ds5.Tables[4].Columns[D].ToString() != "ShiftName" && ds5.Tables[4].Columns[D].ToString() != "DelivaryShift_id")
                            {
                                string ColName = ds5.Tables[4].Columns[D].ToString();
                                if (D == 0)
                                {
                                    if (ds5.Tables[4].Rows[i][ColName].ToString() == "")
                                    {
                                        sb.Append("<td style='border-left:2px solid black !important;'>0</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style='border-left:2px solid black !important;'>" + ds5.Tables[4].Rows[i][ColName].ToString() + "</td>");
                                    }

                                }
                                else if (D == (ColCount4 - 2))
                                {
                                    if (ds5.Tables[4].Rows[i][ColName].ToString() == "")
                                    {
                                        sb.Append("<td style='border-right:2px solid black !important; border-bottom:1px solid black;'>0</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style='border-right:2px solid black !important; border-bottom:1px solid black;'>" + ds5.Tables[4].Rows[i][ColName].ToString() + "</td>");
                                    }
                                }
                                else
                                {
                                    if (ds5.Tables[4].Rows[i][ColName].ToString() == "")
                                    {
                                        sb.Append("<td style='border:1px solid black !important;'>0</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td style='border:1px solid black !important;'>" + ds5.Tables[4].Rows[i][ColName].ToString() + "</td>");
                                    }
                                }

                            }

                        }

                        //for Total Value
                        sb.Append("<td style='border-left:2px solid black !important; border-right:2px solid black; border-bottom:1px solid black; border-top:1px solid black;'>" + ds5.Tables[5].Rows[i]["TotalPaybleAmount"].ToString() + "</td>");
                        //for Total Value
                        sb.Append("<td style='border-left:2px solid black !important; border-right:2px solid black; border-bottom:1px solid black; border-top:1px solid black;'>" + ds5.Tables[8].Rows[i]["TCS_TaxAmt"].ToString() + "</td>");
                        //for Amount Deposited
                        sb.Append("<td style='border-left:2px solid black !important; border-right:2px solid black; border-bottom:1px solid black; border-top:1px solid black;'>" + ds5.Tables[6].Rows[i]["AmountDeposited"].ToString() + "</td>");
                        //for Payment No. and Date
                        sb.Append("<td style='border-left:2px solid black !important; border-right:2px solid black; border-bottom:1px solid black; border-top:1px solid black;'>" + (ds5.Tables[6].Rows[i]["PaymentNo"].ToString()==""? "" : ds5.Tables[6].Rows[i]["PaymentNo"].ToString() + " (" + ds5.Tables[6].Rows[i]["PaymentDate"].ToString() + ")") + "</td>");
                      

                    }
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black !important;'><b>Total</b></td>");
                    //Issue
                    DataTable dt = new DataTable();
                    dt = ds5.Tables[0];
                   
                    int sum11 = 0;
                    int dtIssueColcount = dt.Columns.Count;
                    int Issuecolcount  = 0; 
                    foreach (DataColumn column in dt.Columns)
                    {
                        ++ Issuecolcount;
                        if (column.ToString() != "DDate" && column.ToString() != "Delivary_Date" && column.ToString() != "ShiftName" && column.ToString() != "DelivaryShift_id")
                        {
                            if (Issuecolcount == (dtIssueColcount - 2))
                            {
                                sum11 = dt.AsEnumerable().Sum(r => r.Field<int?>("" + column + "") ?? 0);
                                sb.Append("<td style='border:1px solid black !important; border-right:2px solid black !important;'><b>" + sum11.ToString() + "</b></td>");
                            }
                            else
                            {
                                sum11 = dt.AsEnumerable().Sum(r => r.Field<int?>("" + column + "") ?? 0);
                                sb.Append("<td style='border:1px solid black !important;'><b>" + sum11.ToString() + "</b></td>");
                            }
                        

                        }
                        
                    }
                    //Return
                    DataTable dt1 = new DataTable();
                    dt1 = ds5.Tables[1];
                    int sum12 = 0;
                    int dt1ReturnColcount = dt1.Columns.Count;
                    int Returncolcount = 0; 
                    foreach (DataColumn column1 in dt1.Columns)
                    {
                        ++Returncolcount;
                        if (column1.ToString() != "DDate" && column1.ToString() != "Delivary_Date" && column1.ToString() != "ShiftName" && column1.ToString() != "DelivaryShift_id")
                        {
                            if (Returncolcount == (dt1ReturnColcount - 2))
                            {
                                sum12 = dt1.AsEnumerable().Sum(r => r.Field<int?>("" + column1 + "") ?? 0);
                                sb.Append("<td style='border:1px solid black;border-right:2px solid black !important;'><b>" + sum12.ToString() + "</b></td>");
                            }
                            else
                            {
                                sum12 = dt1.AsEnumerable().Sum(r => r.Field<int?>("" + column1 + "") ?? 0);
                                sb.Append("<td style='border:1px solid black; !important;'><b>" + sum12.ToString() + "</b></td>");
                            }

                        }
                       
                    }
                    //Advanced Card
                    DataTable dt2 = new DataTable();
                    dt2 = ds5.Tables[2];
                    int sum13 = 0;
                    int dt2ACColcount = dt2.Columns.Count;
                    int ACcolcount = 0; 
                    foreach (DataColumn column2 in dt2.Columns)
                    {
                        ++ACcolcount;
                        if (column2.ToString() != "DDate" && column2.ToString() != "Delivary_Date" && column2.ToString() != "ShiftName" && column2.ToString() != "DelivaryShift_id")
                        {
                            if (ACcolcount == (dt2ACColcount - 2))
                            {
                                sum13 = dt2.AsEnumerable().Sum(r1 => r1.Field<int?>("" + column2 + "") ?? 0);
                                sb.Append("<td style='border:1px solid black;border-right:2px solid black !important;'><b>" + sum13.ToString() + "</b></td>");
                            }
                            else
                            {
                                sum13 = dt2.AsEnumerable().Sum(r1 => r1.Field<int?>("" + column2 + "") ?? 0);
                                sb.Append("<td style='border:1px solid black; !important;'><b>" + sum13.ToString() + "</b></td>");
                            }
                            
                            

                        }
                    }
                    //credit(Institute Qty)
                    DataTable dt3 = new DataTable();
                    dt3 = ds5.Tables[3];
                    int sum14 = 0;
                    int dt3CreditColcount = dt3.Columns.Count;
                    int Creditcolcount = 0; 
                    foreach (DataColumn column3 in dt3.Columns)
                    {
                        ++Creditcolcount;
                        if (column3.ToString() != "DDate" && column3.ToString() != "Delivary_Date" && column3.ToString() != "ShiftName" && column3.ToString() != "DelivaryShift_id")
                        {
                            if (Creditcolcount == (dt3CreditColcount - 2))
                            {
                                sum14 = dt3.AsEnumerable().Sum(r1 => r1.Field<int?>("" + column3 + "") ?? 0);
                                sb.Append("<td style='border:1px solid black;border-right:2px solid black !important;'><b>" + sum14.ToString() + "</b></td>");
                            }
                            else
                            {
                                sum14 = dt3.AsEnumerable().Sum(r1 => r1.Field<int?>("" + column3 + "") ?? 0);
                                sb.Append("<td style='border:1px solid black; !important;'><b>" + sum14.ToString() + "</b></td>");
                            }
                           
                            

                        }
                    }
                    //Cash(Billing Qty)
                    DataTable dt4 = new DataTable();
                    dt4 = ds5.Tables[4];
                    int sum15 = 0;
                    decimal sum21 = 0;
                    int dt4CashColcount = dt4.Columns.Count;
                    int Cashcolcount = 0; 
                    foreach (DataColumn column4 in dt4.Columns)
                    {
                        ++Cashcolcount;
                        if (column4.ToString() != "DDate" && column4.ToString() != "Delivary_Date" && column4.ToString() != "ShiftName" && column4.ToString() != "DelivaryShift_id")
                        {
                            //if (Cashcolcount == (dt4CashColcount - 2))
                            //{
                            //    sum15 = dt4.AsEnumerable().Sum(r2 => r2.Field<int?>("" + column4 + "") ?? 0);
                            //    sb.Append("<td style='border:1px solid black;border-right:2px solid black !important;'><b>" + sum15.ToString() + "</b></td>");
                            //}
                            //else
                            if (Cashcolcount == (dt4CashColcount - 2))
                            {
                                sum21 = dt4.AsEnumerable().Sum(r2 => r2.Field<decimal?>("" + column4 + "") ?? 0);
                                sb.Append("<td style='border:1px solid black; !important;'><b>" + sum21.ToString() + "</b></td>");
                            }
                            else
                            {
                                sum15 = dt4.AsEnumerable().Sum(r2 => r2.Field<int?>("" + column4 + "") ?? 0);
                                sb.Append("<td style='border:1px solid black; !important;'><b>" + sum15.ToString() + "</b></td>");
                            }
                           
                           

                        }
                    }

                    //for Total VValue
                    decimal sum16 = ds5.Tables[5].AsEnumerable().Sum(r2 => r2.Field<decimal?>("TotalPaybleAmount") ?? 0);
                    sb.Append("<td style='border:1px solid black !important; border-left:2px solid black !important; border-right:2px solid black !important;'><b>" + sum16.ToString() + "</b></td>");

                    //for TCS TAX
                    decimal sum_TCS = ds5.Tables[8].AsEnumerable().Sum(r2 => r2.Field<decimal?>("TCS_TaxAmt") ?? 0);
                    sb.Append("<td style='border:1px solid black !important; border-left:2px solid black !important; border-right:2px solid black !important;'><b>" + sum_TCS.ToString() + "</b></td>");

                    //for Amount Deposited 
                    decimal sum17 = ds5.Tables[6].AsEnumerable().Sum(r2 => r2.Field<decimal?>("AmountDeposited") ?? 0);
                    sb.Append("<td style='border:1px solid black !important; border-left:2px solid black !important; border-right:2px solid black !important;'><b>" + sum17.ToString() + "</b></td>");

                    //for Payment No. and Date 
                    sb.Append("<td style='border:1px solid black !important; border-left:2px solid black !important; border-right:2px solid black !important;'></td>");

                    sb.Append("</tr>");
                    sb.Append("</table>");
                    decimal ob = 0;
                    ob = Convert.ToDecimal(ds5.Tables[7].Rows[0]["OpeningBalance"].ToString() == "" ? 0 : ds5.Tables[7].Rows[0]["OpeningBalance"]);
                    sb.Append("<table style='width:500px;' class='table'>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black !important;'><b>Opening Balance</b></td>");
                    sb.Append("<td style='border:1px solid black !important;'><b>Total Value</b></td>");
                    sb.Append("<td style='border:1px solid black !important;'><b>Total Amount Deposited</b></td>");
                    sb.Append("<td style='border:1px solid black !important;'><b>Closing Balance</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='border:1px solid black !important;'>" + ob.ToString("0.00") + "</td>");
                    sb.Append("<td style='border:1px solid black !important;'>" + sum16.ToString("0.00") + "</td>");
                    sb.Append("<td style='border:1px solid black !important;'>" + sum17.ToString("0.00") + "</td>");
                    sb.Append("<td style='border:1px solid black !important;'>" + ((ob + sum16) - sum17) + "</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");

                    ////////////////End Of Route Wise Print Code   ///////////////////////
                    btnPrint.Visible = true;
                    btnExcel.Visible = true;
                    div_page_content.InnerHtml = sb.ToString();
                    Print.InnerHtml = sb.ToString();
                    sum16 = 0;
                    sum17 = 0;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GenerateReport();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        btnPrint.Visible = false;
        btnExcel.Visible = false;
        div_page_content.InnerHtml = "";
        Print.InnerHtml = "";
        lblMsg.Text = string.Empty;
        txtMonth.Text = string.Empty;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "DepotMilkSaleRegister" + DateTime.Now + ".xls");
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