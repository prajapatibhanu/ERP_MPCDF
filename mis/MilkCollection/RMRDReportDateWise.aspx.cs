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

public partial class mis_MilkCollection_RMRDReport : System.Web.UI.Page
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
                FillBMCRoot();
                             
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
                       new string[] { "flag", "Office_ID", "OfficeType_ID" },
                       new string[] { "11", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlBMCTankerRootName.DataTextField = "BMCTankerRootName";
                ddlBMCTankerRootName.DataValueField = "BMCTankerRoot_Id";
                ddlBMCTankerRootName.DataSource = ds;
                ddlBMCTankerRootName.DataBind();
                ddlBMCTankerRootName.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }

    }
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
			divprint.InnerHtml = "";
            string FromDate = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            string ToDate = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            ds = objdb.ByProcedure("Usp_MilkCollectionChallanEntry", new string[] { "flag", "FromDate", "ToDate", "Created_Office_ID", "BMCTankerRoot_Id" }, new string[] { "5", FromDate, ToDate, objdb.Office_ID(), ddlBMCTankerRootName.SelectedValue }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnprint.Visible = true;
                    btnExport.Visible = true;
                    string EntryDate = "";
                    string AttachedBMC_ID = "";
                    string Shift = "";
                    decimal TotalQty = 0;
                    decimal TotalFat = 0;
                    decimal TotalSnf = 0;
                    decimal TotalClr = 0;
                    decimal TotalKgFat = 0;
                    decimal TotalKgSnf = 0;
                    decimal TotalBufGoodQty = 0;
                    decimal TotalBufSourQty = 0;
                    decimal TotalBufCurdQty = 0;
                    decimal TotalBufGoodKgFat = 0;
                    decimal TotalBufSourKgFat = 0;
                    decimal TotalBufCurdKgFat = 0;
                    decimal TotalBufGoodKgSnf = 0;
                    decimal TotalBufSourKgSnf = 0;
                    decimal TotalBufCurdKgSnf = 0;
                    decimal TotalCowGoodQty = 0;
                    decimal TotalCowSourQty = 0;
                    decimal TotalCowCurdQty = 0;
                    decimal TotalCowGoodKgFat = 0;
                    decimal TotalCowSourKgFat = 0;
                    decimal TotalCowCurdKgFat = 0;
                    decimal TotalCowGoodKgSnf = 0;
                    decimal TotalCowSourKgSnf = 0;
                    decimal TotalCowCurdKgSnf = 0;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table border='1' class='table table-bordered'>");
                    int Count = ds.Tables[0].Rows.Count;
                    for (int i = 0; i < Count; i++)
			        {
                        if(i == 0)
                        {
                         sb.Append("<tr>");
                         //sb.Append("<td colspan='12' style='font-size:16px; text-align:center;'><b>" + Session["Office_Name"] + "(BULK MILK COOLER):" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "&nbsp;&nbsp;&nbsp;DATE:" + ds.Tables[0].Rows[i]["EntryDate"].ToString() + "&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[i]["Shift"].ToString() + "</b></td>");
                        sb.Append("<td colspan='12' style='font-size:16px; text-align:center;'><b>" + Session["Office_Name"] + "(BULK MILK COOLER):" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "&nbsp;&nbsp;&nbsp;DATE:" + ds.Tables[0].Rows[i]["EntryDate"].ToString() + "</b></td>");
                         sb.Append("</tr>");
                         sb.Append("<tr>");
                         sb.Append("<th style='width:80px;'>Society Code</th>");
                         sb.Append("<th>Name of Society</th>");
                         sb.Append("<th>Date</th>");
                         sb.Append("<th>Shift</th>");
                         sb.Append("<th>Buf/Cow</th>");
                         sb.Append("<th>Milk Category</th>");
                         sb.Append("<th>Quantity</th>");
                         sb.Append("<th>Fat</th>");
                         sb.Append("<th>LR</th>");
                         sb.Append("<th>Snf</th>");
                         sb.Append("<th>Kg Fat</th>");
                         sb.Append("<th>Kg Snf</th>");
                         sb.Append("</tr>");
                         sb.Append("<tr>");
                         sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["Office_Code"].ToString() + " </td>");
                         sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                         sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["EntryDate"].ToString() + "</td>");
                         sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["Shift"].ToString() + "</td>");
                         sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["MilkType"].ToString() + "</td>");
                         sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["MilkQuality"].ToString() + "</td>");
                         sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["MilkQuantity"].ToString() + "</td>");
                         sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["Fat"].ToString() + "</td>");
                         sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["Clr"].ToString() + "</td>");
                         sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["Snf"].ToString() + "</td>");
                         sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["FatInKg"].ToString() + "</td>");
                         sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["SnfInKg"].ToString() + "</td>");
                         if (ds.Tables[0].Rows[i]["MilkType"].ToString() == "Buf" && ds.Tables[0].Rows[i]["MilkQuality"].ToString() == "Good")
                         {
                             TotalBufGoodQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                             TotalBufGoodKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                             TotalBufGoodKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                         }
                         else if (ds.Tables[0].Rows[i]["MilkType"].ToString() == "Buf" && ds.Tables[0].Rows[i]["MilkQuality"].ToString() == "Sour")
                         {
                             TotalBufSourQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                             TotalBufSourKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                             TotalBufSourKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                         }
                         else if (ds.Tables[0].Rows[i]["MilkType"].ToString() == "Buf" && ds.Tables[0].Rows[i]["MilkQuality"].ToString() == "Curd")
                         {
                             TotalBufCurdQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                             TotalBufCurdKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                             TotalBufCurdKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                         }
                         else if (ds.Tables[0].Rows[i]["MilkType"].ToString() == "Cow" && ds.Tables[0].Rows[i]["MilkQuality"].ToString() == "Good")
                         {
                             TotalCowGoodQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                             TotalCowGoodKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                             TotalCowGoodKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                         }
                         else if (ds.Tables[0].Rows[i]["MilkType"].ToString() == "Cow" && ds.Tables[0].Rows[i]["MilkQuality"].ToString() == "Sour")
                         {
                             TotalCowSourQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                             TotalCowSourKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                             TotalCowSourKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                         }
                         else if (ds.Tables[0].Rows[i]["MilkType"].ToString() == "Cow" && ds.Tables[0].Rows[i]["MilkQuality"].ToString() == "Curd")
                         {
                             TotalCowCurdQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                             TotalCowCurdKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                             TotalCowCurdKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                         }
                         
                        
                         TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                         TotalFat += decimal.Parse(ds.Tables[0].Rows[i]["Fat"].ToString());
                         TotalClr += decimal.Parse(ds.Tables[0].Rows[i]["Clr"].ToString());
                         TotalSnf += decimal.Parse(ds.Tables[0].Rows[i]["Snf"].ToString());
                         TotalKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                         TotalKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                         
                         sb.Append("</tr>");
                        }
                        //else if (EntryDate == ds.Tables[0].Rows[i]["EntryDate"].ToString() && AttachedBMC_ID == ds.Tables[0].Rows[i]["AttachedBMC_ID"].ToString() && Shift == ds.Tables[0].Rows[i]["Shift"].ToString())
                        else if (EntryDate == ds.Tables[0].Rows[i]["EntryDate"].ToString() && AttachedBMC_ID == ds.Tables[0].Rows[i]["AttachedBMC_ID"].ToString() )
                        {
                            sb.Append("<tr>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["Office_Code"].ToString() + " </td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["EntryDate"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["Shift"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["MilkType"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["MilkQuality"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px'>" + ds.Tables[0].Rows[i]["MilkQuantity"].ToString() + "</td>");

                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["Fat"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["Clr"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["Snf"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["SnfInKg"].ToString() + "</td>");
                            sb.Append("</tr>");
                            TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                            //TotalFat += decimal.Parse(ds.Tables[0].Rows[i]["Fat"].ToString());
                            //TotalClr += decimal.Parse(ds.Tables[0].Rows[i]["Clr"].ToString());
                            //TotalSnf += decimal.Parse(ds.Tables[0].Rows[i]["Snf"].ToString());
                            TotalKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                            TotalKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                            if (ds.Tables[0].Rows[i]["MilkType"].ToString() == "Buf" && ds.Tables[0].Rows[i]["MilkQuality"].ToString() == "Good")
                            {
                                TotalBufGoodQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                                TotalBufGoodKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                                TotalBufGoodKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                            }
                            else if (ds.Tables[0].Rows[i]["MilkType"].ToString() == "Buf" && ds.Tables[0].Rows[i]["MilkQuality"].ToString() == "Sour")
                            {
                                TotalBufSourQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                                TotalBufSourKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                                TotalBufSourKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                            }
                            else if (ds.Tables[0].Rows[i]["MilkType"].ToString() == "Buf" && ds.Tables[0].Rows[i]["MilkQuality"].ToString() == "Curd")
                            {
                                TotalBufCurdQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                                TotalBufCurdKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                                TotalBufCurdKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                            }
                            else if (ds.Tables[0].Rows[i]["MilkType"].ToString() == "Cow" && ds.Tables[0].Rows[i]["MilkQuality"].ToString() == "Good")
                            {
                                TotalCowGoodQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                                TotalCowGoodKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                                TotalCowGoodKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                            }
                            else if (ds.Tables[0].Rows[i]["MilkType"].ToString() == "Cow" && ds.Tables[0].Rows[i]["MilkQuality"].ToString() == "Sour")
                            {
                                TotalCowSourQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                                TotalCowSourKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                                TotalCowSourKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                            }
                            else if (ds.Tables[0].Rows[i]["MilkType"].ToString() == "Cow" && ds.Tables[0].Rows[i]["MilkQuality"].ToString() == "Curd")
                            {
                                TotalCowCurdQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                                TotalCowCurdKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                                TotalCowCurdKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                            }
                         

                        }
                        else
                        {
                            
                          
                            TotalFat = (TotalKgFat / TotalQty) * 100;
                            TotalSnf = (TotalKgSnf / TotalQty) * 100;
                            TotalClr = Obj_MC.GetCLR_DCS(TotalFat, TotalSnf);
                            sb.Append("<tr>");
                            sb.Append("<td></td>");
                            sb.Append("<td><b>Total(Good+Sour+Curd)</b></td>");
                            sb.Append("<td></td>");
                            sb.Append("<td></td>");
                            sb.Append("<td></td>");
                            sb.Append("<td></td>");
                            sb.Append("<td><b>" + Math.Round(TotalQty, 3) + "</b></td>");
                            sb.Append("<td><b>" + Math.Round(TotalFat, 2) + "</b></td>");
                            sb.Append("<td><b>" + Math.Round(TotalClr, 2) + "</b></td>");
                            sb.Append("<td><b>" + Math.Round(TotalSnf, 2) + "</b></td>");
                            sb.Append("<td><b>" + Math.Round(TotalKgFat, 3) + "</b></td>");
                            sb.Append("<td><b>" + Math.Round(TotalKgSnf, 3) + "</b></td>");
                            sb.Append("</tr>");
                    if (TotalBufGoodQty != 0)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td></td>");
                        sb.Append("<td><b>Total(Buf(Good))</b></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td><b>" + Math.Round(TotalBufGoodQty, 3) + "</b></td>");
                        sb.Append("<td><b></b></td>");
                        sb.Append("<td><b></b></td>");
                        sb.Append("<td><b></b></td>");
                        sb.Append("<td><b>" + Math.Round(TotalBufGoodKgFat, 3) + "</b></td>");
                        sb.Append("<td><b>" + Math.Round(TotalBufGoodKgSnf, 3) + "</b></td>");
                        sb.Append("</tr>");
                    }
                    if (TotalBufSourQty != 0)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td></td>");
                        sb.Append("<td><b>Total(Buf(Sour))</b></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td><b>" + Math.Round(TotalBufSourQty, 3) + "</b></td>");
                        sb.Append("<td><b></b></td>");
                        sb.Append("<td><b></b></td>");
                        sb.Append("<td><b></b></td>");
                        sb.Append("<td><b>" + Math.Round(TotalBufSourKgFat, 3) + "</b></td>");
                        sb.Append("<td><b>" + Math.Round(TotalBufSourKgSnf, 3) + "</b></td>");
                        sb.Append("</tr>");
                    }
                    if (TotalBufCurdQty != 0)
                    {
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>Total(Buf(Curd))</b></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>" + Math.Round(TotalBufCurdQty, 3) + "</b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalBufCurdKgFat, 3) + "</b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalBufCurdKgSnf, 3) + "</b></td>");
                    sb.Append("</tr>");
                    }
                    if (TotalCowGoodQty != 0)
                    {
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>Total(Cow(Good))</b></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>" + Math.Round(TotalCowGoodQty, 3) + "</b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalCowGoodKgFat, 3) + "</b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalCowGoodKgSnf, 3) + "</b></td>");
                    sb.Append("</tr>");
                    }
                    if (TotalCowSourQty != 0)
                    {
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>Total(Cow(Sour))</b></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>" + Math.Round(TotalCowSourQty, 3) + "</b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalCowSourKgFat, 3) + "</b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalCowSourKgSnf, 3) + "</b></td>");
                    sb.Append("</tr>");
                    }
                    if (TotalCowCurdQty != 0)
                    {
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>Total(Cow(Curd))</b></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>" + Math.Round(TotalCowCurdQty, 3) + "</b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalCowCurdKgFat, 3) + "</b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalCowCurdKgSnf, 3) + "</b></td>");
                    sb.Append("</tr>");
                    }
                            TotalQty = 0;
                            TotalFat = 0;
                            TotalSnf = 0;
                            TotalClr = 0;
                            TotalKgFat = 0;
                            TotalKgSnf = 0;
                            TotalBufGoodQty = 0;
                            TotalBufSourQty = 0;
                            TotalBufCurdQty = 0;
                            TotalBufGoodKgFat = 0;
                            TotalBufSourKgFat = 0;
                            TotalBufCurdKgFat = 0;
                            TotalBufGoodKgSnf = 0;
                            TotalBufSourKgSnf = 0;
                            TotalBufCurdKgSnf = 0;
                            TotalCowGoodQty = 0;
                            TotalCowSourQty = 0;
                            TotalCowCurdQty = 0;
                            TotalCowGoodKgFat = 0;
                            TotalCowSourKgFat = 0;
                            TotalCowCurdKgFat = 0;
                            TotalCowGoodKgSnf = 0;
                            TotalCowSourKgSnf = 0;
                            TotalCowCurdKgSnf = 0;

                            sb.Append("</table>");
                            sb.Append("<div style='page-break-after: always;'></div>");
                            sb.Append("<table border='1' class='table table-bordered'>");
                            sb.Append("<tr>");
                            //sb.Append("<td colspan='12' style='font-size:16px; text-align:center;'><b>" + Session["Office_Name"] + "(BULK MILK COOLER):" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "&nbsp;&nbsp;&nbsp;DATE:" + ds.Tables[0].Rows[i]["EntryDate"].ToString() + "&nbsp;&nbsp;&nbsp;" + ds.Tables[0].Rows[i]["Shift"].ToString() + "</b></td>");
                            sb.Append("<td colspan='12' style='font-size:16px; text-align:center;'><b>" + Session["Office_Name"] + "(BULK MILK COOLER):" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "&nbsp;&nbsp;&nbsp;DATE:" + ds.Tables[0].Rows[i]["EntryDate"].ToString() +  "</b></td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<th style='width:80px;'>Society Code</th>");
                            sb.Append("<th>Name of Society</th>");
                            sb.Append("<th>Date</th>");
                            sb.Append("<th>Shift</th>");
                            sb.Append("<th>Buf/Cow</th>");
                             sb.Append("<th>Milk Category</th>");
                            sb.Append("<th>Quantity</th>");
                            sb.Append("<th>Fat</th>");
                            sb.Append("<th>LR</th>");
                            sb.Append("<th>Snf</th>");
                            sb.Append("<th>Kg Fat</th>");
                            sb.Append("<th>Kg Snf</th>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["Office_Code"].ToString() + " </td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["EntryDate"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["Shift"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["MilkType"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["MilkQuality"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["MilkQuantity"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["Fat"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["Clr"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["Snf"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["FatInKg"].ToString() + "</td>");
                            sb.Append("<td style='padding:20px;'>" + ds.Tables[0].Rows[i]["SnfInKg"].ToString() + "</td>");
                            sb.Append("</tr>");
                            TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                            //TotalFat += decimal.Parse(ds.Tables[0].Rows[i]["Fat"].ToString());
                            //TotalClr += decimal.Parse(ds.Tables[0].Rows[i]["Clr"].ToString());
                            //TotalSnf += decimal.Parse(ds.Tables[0].Rows[i]["Snf"].ToString());
                            TotalKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                            TotalKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                            if (ds.Tables[0].Rows[i]["MilkType"].ToString() == "Buf" && ds.Tables[0].Rows[i]["MilkQuality"].ToString() == "Good")
                            {
                                TotalBufGoodQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                                TotalBufGoodKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                                TotalBufGoodKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                            }
                            else if (ds.Tables[0].Rows[i]["MilkType"].ToString() == "Buf" && ds.Tables[0].Rows[i]["MilkQuality"].ToString() == "Sour")
                            {
                                TotalBufSourQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                                TotalBufSourKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                                TotalBufSourKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                            }
                            else if (ds.Tables[0].Rows[i]["MilkType"].ToString() == "Buf" && ds.Tables[0].Rows[i]["MilkQuality"].ToString() == "Curd")
                            {
                                TotalBufCurdQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                                TotalBufCurdKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                                TotalBufCurdKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                            }
                            else if (ds.Tables[0].Rows[i]["MilkType"].ToString() == "Cow" && ds.Tables[0].Rows[i]["MilkQuality"].ToString() == "Good")
                            {
                                TotalCowGoodQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                                TotalCowGoodKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                                TotalCowGoodKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                            }
                            else if (ds.Tables[0].Rows[i]["MilkType"].ToString() == "Cow" && ds.Tables[0].Rows[i]["MilkQuality"].ToString() == "Sour")
                            {
                                TotalCowSourQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                                TotalCowSourKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                                TotalCowSourKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                            }
                            else if (ds.Tables[0].Rows[i]["MilkType"].ToString() == "Cow" && ds.Tables[0].Rows[i]["MilkQuality"].ToString() == "Curd")
                            {
                                TotalCowCurdQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity"].ToString());
                                TotalCowCurdKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg"].ToString());
                                TotalCowCurdKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg"].ToString());
                            }
                        }
                        EntryDate = ds.Tables[0].Rows[i]["EntryDate"].ToString();
                        AttachedBMC_ID = ds.Tables[0].Rows[i]["AttachedBMC_ID"].ToString();
                        Shift = ds.Tables[0].Rows[i]["Shift"].ToString();
			            
			        }
                    TotalFat = (TotalKgFat / TotalQty) * 100;
                    TotalSnf = (TotalKgSnf / TotalQty) * 100;
                    TotalClr = Obj_MC.GetCLR_DCS(TotalFat, TotalSnf);
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>Total(Good+Sour+Curd)</b></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>" + Math.Round(TotalQty, 3) + "</b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalFat, 2) + "</b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalClr, 2) + "</b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalSnf, 2) + "</b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalKgFat, 3) + "</b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalKgSnf, 3) + "</b></td>");
                    sb.Append("</tr>");
                    if (TotalBufGoodQty != 0)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td></td>");
                        sb.Append("<td><b>Total(Buf(Good))</b></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td><b>" + Math.Round(TotalBufGoodQty, 3) + "</b></td>");
                        sb.Append("<td><b></b></td>");
                        sb.Append("<td><b></b></td>");
                        sb.Append("<td><b></b></td>");
                        sb.Append("<td><b>" + Math.Round(TotalBufGoodKgFat, 3) + "</b></td>");
                        sb.Append("<td><b>" + Math.Round(TotalBufGoodKgSnf, 3) + "</b></td>");
                        sb.Append("</tr>");
                    }
                    if (TotalBufSourQty != 0)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td></td>");
                        sb.Append("<td><b>Total(Buf(Sour))</b></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td></td>");
                        sb.Append("<td><b>" + Math.Round(TotalBufSourQty, 3) + "</b></td>");
                        sb.Append("<td><b></b></td>");
                        sb.Append("<td><b></b></td>");
                        sb.Append("<td><b></b></td>");
                        sb.Append("<td><b>" + Math.Round(TotalBufSourKgFat, 3) + "</b></td>");
                        sb.Append("<td><b>" + Math.Round(TotalBufSourKgSnf, 3) + "</b></td>");
                        sb.Append("</tr>");
                    }
                    if (TotalBufCurdQty != 0)
                    {
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>Total(Buf(Curd))</b></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>" + Math.Round(TotalBufCurdQty, 3) + "</b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalBufCurdKgFat, 3) + "</b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalBufCurdKgSnf, 3) + "</b></td>");
                    sb.Append("</tr>");
                    }
                    if (TotalCowGoodQty != 0)
                    {
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>Total(Cow(Good))</b></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>" + Math.Round(TotalCowGoodQty, 3) + "</b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalCowGoodKgFat, 3) + "</b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalCowGoodKgSnf, 3) + "</b></td>");
                    sb.Append("</tr>");
                    }
                    if (TotalCowSourQty != 0)
                    {
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>Total(Cow(Sour))</b></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>" + Math.Round(TotalCowSourQty, 3) + "</b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalCowSourKgFat, 3) + "</b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalCowSourKgSnf, 3) + "</b></td>");
                    sb.Append("</tr>");
                    }
                    if (TotalCowCurdQty != 0)
                    {
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>Total(Cow(Curd))</b></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>" + Math.Round(TotalCowCurdQty, 3) + "</b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b></b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalCowCurdKgFat, 3) + "</b></td>");
                    sb.Append("<td><b>" + Math.Round(TotalCowCurdKgSnf, 3) + "</b></td>");
                    sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    divRpt.InnerHtml = sb.ToString();
					divprint.InnerHtml = sb.ToString();

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