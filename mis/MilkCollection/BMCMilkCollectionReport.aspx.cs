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

public partial class mis_MilkCollection_BMCMilkCollectionReport : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    MilkCalculationClass Obj_MC = new MilkCalculationClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!IsPostBack)
            {
                txtFromDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtToDate.Attributes.Add("readonly", "readonly");
                //FillBMCRoot();

            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    //public void FillBMCRoot()
    //{

    //    try
    //    {
    //        ds = null;
    //        ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
    //                 new string[] { "flag", "Office_ID" },
    //                 new string[] { "1", objdb.Office_ID() }, "dataset");

    //        if (ds.Tables[0].Rows.Count != 0)
    //        {
    //            ddlBMCTankerRootName.DataTextField = "BMCTankerRootName";
    //            ddlBMCTankerRootName.DataValueField = "BMCTankerRoot_Id";
    //            ddlBMCTankerRootName.DataSource = ds;
    //            ddlBMCTankerRootName.DataBind();
    //            ddlBMCTankerRootName.Items.Insert(0, new ListItem("All", "0"));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
    //    }

    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblrptmsg.Text = "";
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    protected void FillGrid()
    {
        try
        {
            lblrptmsg.Text = "";
            lblMsg.Text = "";
            btnprint.Visible = false;
            btnExport.Visible = false;
            divRpt.InnerHtml = "";
            string FromDate = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            string ToDate = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntry", new string[] { "flag", "FromDate", "ToDate", "Created_Office_ID"}, new string[] { "8", FromDate, ToDate, objdb.Office_ID()}, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnprint.Visible = true;
                    btnExport.Visible = true;
                    string EntryDate = "";
                    string AttachedBMC_ID = "";
                    string C_ReferenceNo = "";
                    string Shift = "";
                    decimal TotalQty = 0;
                    decimal TotalFat = 0;
                    decimal TotalSnf = 0;
                    decimal TotalClr = 0;
                    decimal TotalKgFat = 0;
                    decimal TotalKgSnf = 0;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table border='1' class='table table-bordered'>");
                    int Count = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < Count; i++)
                    {
                        if (i == 0)
                        {
                            sb.Append("<tr>");
                           // sb.Append("<td colspan='9' style='font-size:16px; text-align:center;'><b>" + Session["Office_Name"] + "(BULK MILK COOLER):" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "&nbsp;&nbsp;&nbsp;DATE:" + ds.Tables[0].Rows[i]["EntryDate"].ToString() + "&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[i]["Shift"].ToString() + "</b></td>");
                            sb.Append("<td colspan='11' style='font-size:16px; text-align:center;'><b>" + Session["Office_Name"] + "&nbsp;&nbsp;&nbsp;DATE:" + ds.Tables[0].Rows[i]["D_Date"].ToString() + "</b></td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td colspan='11' style='font-size:16px; text-align:center;'><b>Reference No:" + ds.Tables[0].Rows[i]["C_ReferenceNo"].ToString() + "&nbsp;&nbsp;&nbsp;Tanker No:" + ds.Tables[0].Rows[i]["V_VehicleNo"].ToString() + "</b></td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                           
                            sb.Append("<th>Date</th>");
                            sb.Append("<th>Shift</th>");
                            sb.Append("<th style='width:80px;'>Society Code</th>");
                            sb.Append("<th>Name of Society</th>");
                            sb.Append("<th>Buf/Cow</th>");
                            sb.Append("<th>Quantity</th>");
                            sb.Append("<th>Fat</th>");
                            sb.Append("<th>LR</th>");
                            sb.Append("<th>Snf</th>");
                            sb.Append("<th>Kg Fat</th>");
                            sb.Append("<th>Kg Snf</th>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["EntryDate"].ToString() + " </td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Shift"].ToString() + " </td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Office_Code"].ToString() + " </td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["MilkType"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Fat"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Clr"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Snf"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["SnfInKg"].ToString() + "</td>");

                            TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                            TotalFat += decimal.Parse(ds.Tables[0].Rows[i]["Fat"].ToString());
                            TotalClr += decimal.Parse(ds.Tables[0].Rows[i]["Clr"].ToString());
                            TotalSnf += decimal.Parse(ds.Tables[0].Rows[i]["Snf"].ToString());
                            TotalKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                            TotalKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());

                            sb.Append("</tr>");
                        }
                        //else if (EntryDate == ds.Tables[0].Rows[i]["EntryDate"].ToString() && AttachedBMC_ID == ds.Tables[0].Rows[i]["AttachedBMC_ID"].ToString() && Shift == ds.Tables[0].Rows[i]["Shift"].ToString() && C_ReferenceNo == ds.Tables[0].Rows[i]["C_ReferenceNo"].ToString())
                        else if (C_ReferenceNo == ds.Tables[0].Rows[i]["C_ReferenceNo"].ToString())
                        {
                            sb.Append("<tr>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["EntryDate"].ToString() + " </td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Shift"].ToString() + " </td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Office_Code"].ToString() + " </td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["MilkType"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Fat"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Clr"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Snf"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["SnfInKg"].ToString() + "</td>");
                            sb.Append("</tr>");
                            TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                            
                            TotalKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                            TotalKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                        }
                        else
                        {
                            TotalFat = (TotalKgFat / TotalQty) * 100;
                            TotalSnf = (TotalKgSnf / TotalQty) * 100;
                            TotalClr = Obj_MC.GetCLR_DCS(TotalFat, TotalSnf);
                            sb.Append("<tr>");
                            sb.Append("<td></td>");
                            sb.Append("<td></td>");
                            sb.Append("<td></td>");
                            sb.Append("<td><b>SubTotal</b></td>");
                            sb.Append("<td></td>");
                            sb.Append("<td><b>" + Math.Round(TotalQty, 3) + "</b></td>");
                            sb.Append("<td><b>" + Math.Round(TotalFat, 2) + "</b></td>");
                            sb.Append("<td><b>" + Math.Round(TotalClr, 2) + "</b></td>");
                            sb.Append("<td><b>" + Math.Round(TotalSnf, 2) + "</b></td>");
                            sb.Append("<td><b>" + Math.Round(TotalKgFat, 3) + "</b></td>");
                            sb.Append("<td><b>" + Math.Round(TotalKgSnf, 3) + "</b></td>");
                            sb.Append("</tr>");
                            TotalQty = 0;
                            TotalFat = 0;
                            TotalSnf = 0;
                            TotalClr = 0;
                            TotalKgFat = 0;
                            TotalKgSnf = 0;
                            sb.Append("<tr>");
                            //sb.Append("<td colspan='9' style='font-size:16px; text-align:center;'><b>" + Session["Office_Name"] + "(BULK MILK COOLER):" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "&nbsp;&nbsp;&nbsp;DATE:" + ds.Tables[0].Rows[i]["EntryDate"].ToString() + "&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[i]["Shift"].ToString() + "</b></td>");
                            sb.Append("<td colspan='11' style='font-size:16px; text-align:center;'><b>" + Session["Office_Name"] + "&nbsp;&nbsp;&nbsp;DATE:" + ds.Tables[0].Rows[i]["D_Date"].ToString() + "</b></td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td colspan='11' style='font-size:16px; text-align:center;'><b>Reference No:" + ds.Tables[0].Rows[i]["C_ReferenceNo"].ToString() + "&nbsp;&nbsp;&nbsp;Tanker No:" + ds.Tables[0].Rows[i]["V_VehicleNo"].ToString() + "</b></td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                           
                            sb.Append("<th>Date</th>");
                            sb.Append("<th>Shift</th>");
                            sb.Append("<th style='width:80px;'>Society Code</th>");
                            sb.Append("<th>Name of Society</th>");
                            sb.Append("<th>Buf/Cow</th>");
                            sb.Append("<th>Quantity</th>");
                            sb.Append("<th>Fat</th>");
                            sb.Append("<th>LR</th>");
                            sb.Append("<th>Snf</th>");
                            sb.Append("<th>Kg Fat</th>");
                            sb.Append("<th>Kg Snf</th>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["EntryDate"].ToString() + " </td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Shift"].ToString() + " </td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Office_Code"].ToString() + " </td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["MilkType"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Fat"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Clr"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Snf"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["SnfInKg"].ToString() + "</td>");
                            sb.Append("</tr>");
                            TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                           
                            TotalKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                            TotalKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                        }
                        EntryDate = ds.Tables[0].Rows[i]["EntryDate"].ToString();
                        AttachedBMC_ID = ds.Tables[0].Rows[i]["AttachedBMC_ID"].ToString();
                        C_ReferenceNo = ds.Tables[0].Rows[i]["C_ReferenceNo"].ToString();
                        Shift = ds.Tables[0].Rows[i]["Shift"].ToString();

                    }
                    TotalFat = (TotalKgFat / TotalQty) * 100;
                    TotalSnf = (TotalKgSnf / TotalQty) * 100;
                    TotalClr = Obj_MC.GetCLR_DCS(TotalFat, TotalSnf);
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>SubTotal</b></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>" + Math.Round(TotalQty, 3) + "</b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalFat, 2) + "</b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalClr, 2) + "</b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalSnf, 2) + "</b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalKgFat, 3) + "</b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalKgSnf, 3) + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    divRpt.InnerHtml = sb.ToString();

                }
                else
                {
                    lblrptmsg.Text = "No Record Found";
                }
            }
            else
            {
                lblrptmsg.Text = "No Record Found";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "RouteWiseRMRDChallanEntryReport" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            divRpt.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());

            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }



}