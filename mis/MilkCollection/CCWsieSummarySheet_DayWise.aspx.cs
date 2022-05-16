using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using System.Web.UI;
using System.Web;

public partial class mis_MilkCollection_CCWsieSummarySheet_DayWise : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
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
                GetCCDetails();

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
    protected void GetCCDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                     new string[] { "flag", "I_OfficeID", "I_OfficeTypeID" },
                     new string[] { "3", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlccbmcdetail.DataTextField = "Office_Name";
                        ddlccbmcdetail.DataValueField = "Office_ID";


                        ddlccbmcdetail.DataSource = ds;
                        ddlccbmcdetail.DataBind();

                        if (objdb.OfficeType_ID() == "2")
                        {
                            ddlccbmcdetail.Items.Insert(0, new ListItem("All", "0"));
                        }
                        else
                        {
                            ddlccbmcdetail.SelectedValue = objdb.Office_ID();
                            //GetMCUDetails();
                            ddlccbmcdetail.Enabled = false;
                        }


                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            dvReport.InnerHtml = "";
            divprint.InnerHtml = "";
            divshow.Visible = false;
            string[] MonthyearPart = txtFdt.Text.Split('/');
            lblMsg.Text = "";
            string Month = MonthyearPart[0];
            string Year = MonthyearPart[1];
            int BreakCount = 0;
            StringBuilder sb = new StringBuilder();
            ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports", new string[] { "flag", "FromDate", "ToDate", "Office_ID", "Office_Parant_ID","OfficeType_ID" }, new string[] { "11", Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd"), ddlccbmcdetail.SelectedValue,objdb.Office_ID(),objdb.OfficeType_ID() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                divshow.Visible = true;
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                string LastDate = "";
                string LastQuality = "";
                int DateChange = 0;
                
                sb.Append("<table class='table'>");
                sb.Append("<thead class='report-header'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='8'  style='text-align:left; font-size:16px;'><b>" + Session["Office_Name"].ToString() + "</b></td>");
                sb.Append("<td colspan='3' style='text-align:left; font-size:16px;'><b>Society : -" + ddlccbmcdetail.SelectedItem.Text + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='11' style='text-align:left; font-size:14px;'><b>FAT  & SNF Report Of Received Milk.</b></td>");
                decimal Tqty = 0;
                decimal TFatKg = 0;
                decimal TSnfKg = 0;
                decimal BQty = 0;
                decimal BKgFat = 0;
                decimal BKgSnf = 0;
                decimal CQty = 0;
                decimal CKgFat = 0;
                decimal CKgSnf = 0;
                decimal TQty = 0;
                decimal TKgFat = 0;
                decimal TKgSnf = 0;
                sb.Append("</tr>");
                sb.Append("</thead>");
                

                int RowCount = ds.Tables[0].Rows.Count;
                for (int i = 0; i < RowCount; i++)
                {

                    string Date = ds.Tables[0].Rows[i]["EntryDate"].ToString();

                    if (i == 0)
                    {
                        
                        Tqty = decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity_B"].ToString()) + decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity_C"].ToString());
                        TFatKg = decimal.Parse(ds.Tables[0].Rows[i]["FatInKg_B"].ToString()) + decimal.Parse(ds.Tables[0].Rows[i]["FatInKg_C"].ToString());
                        TSnfKg = decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg_B"].ToString()) + decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg_C"].ToString());
                        
                        sb.Append("<tr>");
                        sb.Append("<td colspan= '11' style='font-size:16px;'><b>Date : " + Date.ToString() + " </b></td>");
                        sb.Append("</tr>");
                        sb.Append("<tr >");
                        sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;' rowspan='2'>Quality</td>");
                        sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;' rowspan='2'>Shift</td>");
                        sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black text-align:center;' colspan='3'>< - - BUFF MILK - - ></td>");
                        sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black text-align:center;' colspan='3'>< - - COW MILK - - ></td>");
                        sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black text-align:center;' colspan='3'>< - - TOTAL MILK - - ></td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td>QTY</td>");
                        sb.Append("<td>KgFAT</td>");
                        sb.Append("<td>KgSNF</td>");
                        sb.Append("<td>QTY</td>");
                        sb.Append("<td>KgFAT</td>");
                        sb.Append("<td>KgSNF</td>");
                        sb.Append("<td>QTY</td>");
                        sb.Append("<td>KgFAT</td>");
                        sb.Append("<td>KgSNF</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["Quality"].ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["ShiftName"].ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["MilkQuantity_B"].ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["FatInKg_B"].ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["SnfInKg_B"].ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["MilkQuantity_C"].ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["FatInKg_C"].ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["SnfInKg_C"].ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + Tqty.ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + TFatKg.ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + TSnfKg.ToString() + "</td>");
                        BQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity_B"].ToString());
                        BKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg_B"].ToString());
                        BKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg_B"].ToString());
                        CQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity_C"].ToString());
                        CKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg_C"].ToString());
                        CKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg_C"].ToString());
                        TQty += Tqty;
                        TKgFat += TFatKg;
                        TKgSnf += TSnfKg;
                        sb.Append("</tr>");



                    }
                    else if (Date.ToString() == LastDate.ToString())
                    {
                        DateChange = 0;
                        Tqty = decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity_B"].ToString()) + decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity_C"].ToString());
                        TFatKg = decimal.Parse(ds.Tables[0].Rows[i]["FatInKg_B"].ToString()) + decimal.Parse(ds.Tables[0].Rows[i]["FatInKg_C"].ToString());
                        TSnfKg = decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg_B"].ToString()) + decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg_C"].ToString());

                        if (ds.Tables[0].Rows[i]["Quality"].ToString() == LastQuality.ToString())
                        {
                            sb.Append("<tr>");
                            sb.Append("<td></td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["ShiftName"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["MilkQuantity_B"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["FatInKg_B"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["SnfInKg_B"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["MilkQuantity_C"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["FatInKg_C"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["SnfInKg_C"].ToString() + "</td>");
                            sb.Append("<td>" + Tqty.ToString() + "</td>");
                            sb.Append("<td>" + TFatKg.ToString() + "</td>");
                            sb.Append("<td>" + TSnfKg.ToString() + "</td>");
                            sb.Append("</tr>");
                            BQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity_B"].ToString());
                            BKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg_B"].ToString());
                            BKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg_B"].ToString());
                            CQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity_C"].ToString());
                            CKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg_C"].ToString());
                            CKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg_C"].ToString());
                            TQty += Tqty;
                            TKgFat += TFatKg;
                            TKgSnf += TSnfKg;
                        }
                        else
                        {
                            if (DateChange == 0)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td></td>");
                                sb.Append("<td>T</td>");
                                sb.Append("<td>" + BQty.ToString() + "</td>");
                                sb.Append("<td>" + BKgFat.ToString() + "</td>");
                                sb.Append("<td>" + BKgSnf.ToString() + "</td>");
                                sb.Append("<td>" + CQty.ToString() + "</td>");
                                sb.Append("<td>" + CKgFat.ToString() + "</td>");
                                sb.Append("<td>" + CKgSnf.ToString() + "</td>");
                                sb.Append("<td>" + TQty.ToString() + "</td>");
                                sb.Append("<td>" + TKgFat.ToString() + "</td>");
                                sb.Append("<td>" + TKgSnf.ToString() + "</td>");
                                sb.Append("</tr>");
                                BQty = 0;
                                BKgFat = 0;
                                BKgSnf = 0;
                                CQty = 0;
                                CKgFat = 0;
                                CKgSnf = 0;
                                TQty = 0;
                                TKgFat = 0;
                                TKgSnf = 0;
                            }

                            sb.Append("<tr>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["Quality"].ToString() + "</td>");

                            sb.Append("<td>" + ds.Tables[0].Rows[i]["ShiftName"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["MilkQuantity_B"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["FatInKg_B"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["SnfInKg_B"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["MilkQuantity_C"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["FatInKg_C"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["SnfInKg_C"].ToString() + "</td>");
                            sb.Append("<td>" + Tqty.ToString() + "</td>");
                            sb.Append("<td>" + TFatKg.ToString() + "</td>");
                            sb.Append("<td>" + TSnfKg.ToString() + "</td>");
                            sb.Append("</tr>");
                            BQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity_B"].ToString());
                            BKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg_B"].ToString());
                            BKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg_B"].ToString());
                            CQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity_C"].ToString());
                            CKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg_C"].ToString());
                            CKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg_C"].ToString());
                            TQty += Tqty;
                            TKgFat += TFatKg;
                            TKgSnf += TSnfKg;
                        }


                    }
                    else
                    {
                        BreakCount = BreakCount + 1;
                        DateChange = 1;
                        
                        sb.Append("<tr>");
                        sb.Append("<td></td>");
                        sb.Append("<td>T</td>");
                        sb.Append("<td>" + BQty.ToString() + "</td>");
                        sb.Append("<td>" + BKgFat.ToString() + "</td>");
                        sb.Append("<td>" + BKgSnf.ToString() + "</td>");
                        sb.Append("<td>" + CQty.ToString() + "</td>");
                        sb.Append("<td>" + CKgFat.ToString() + "</td>");
                        sb.Append("<td>" + CKgSnf.ToString() + "</td>");
                        sb.Append("<td>" + TQty.ToString() + "</td>");
                        sb.Append("<td>" + TKgFat.ToString() + "</td>");
                        sb.Append("<td>" + TKgSnf.ToString() + "</td>");
                        sb.Append("</tr>");



                        BQty = 0;
                        BKgFat = 0;
                        BKgSnf = 0;
                        CQty = 0;
                        CKgFat = 0;
                        CKgSnf = 0;
                        TQty = 0;
                        TKgFat = 0;
                        TKgSnf = 0;
                        if (BreakCount % 2 == 0)
                        {
                            sb.Append("<tr class='page-break'></tr>");
                        }
                            sb.Append("<tr>");
                            sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black; font-size:16px;' colspan= '11'><b>Date : " + Date.ToString() + "</b></td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;' rowspan='2'>Quality</td>");
                            sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;' rowspan='2'>Shift</td>");
                            sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black text-align:center;' colspan='3'>< - - BUFF MILK - - ></td>");
                            sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black text-align:center;' colspan='3'>< - - COW MILK - - ></td>");
                            sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black text-align:center;' colspan='3'>< - - TOTAL MILK - - ></td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td>QTY</td>");
                            sb.Append("<td>KgFAT</td>");
                            sb.Append("<td>KgSNF</td>");
                            sb.Append("<td>QTY</td>");
                            sb.Append("<td>KgFAT</td>");
                            sb.Append("<td>KgSNF</td>");
                            sb.Append("<td>QTY</td>");
                            sb.Append("<td>KgFAT</td>");
                            sb.Append("<td>KgSNF</td>");
                            sb.Append("</tr>");
                       
                        Tqty = decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity_B"].ToString()) + decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity_C"].ToString());
                        TFatKg = decimal.Parse(ds.Tables[0].Rows[i]["FatInKg_B"].ToString()) + decimal.Parse(ds.Tables[0].Rows[i]["FatInKg_C"].ToString());
                        TSnfKg = decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg_B"].ToString()) + decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg_C"].ToString());
                       

                        if (ds.Tables[0].Rows[i]["Quality"].ToString() == LastQuality.ToString())
                        {
                            sb.Append("<tr>");
                            sb.Append("<td></td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["ShiftName"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["MilkQuantity_B"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["FatInKg_B"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["SnfInKg_B"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["MilkQuantity_C"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["FatInKg_C"].ToString() + "</td>");
                            sb.Append("<td>" + ds.Tables[0].Rows[i]["SnfInKg_C"].ToString() + "</td>");
                            sb.Append("<td>" + Tqty.ToString() + "</td>");
                            sb.Append("<td>" + TFatKg.ToString() + "</td>");
                            sb.Append("<td>" + TSnfKg.ToString() + "</td>");
                            sb.Append("</tr>");
                            BQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity_B"].ToString());
                            BKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg_B"].ToString());
                            BKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg_B"].ToString());
                            CQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity_C"].ToString());
                            CKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg_C"].ToString());
                            CKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg_C"].ToString());
                            TQty += Tqty;
                            TKgFat += TFatKg;
                            TKgSnf += TSnfKg;
                        }
                        else
                        {
                            if (DateChange == 0)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td></td>");
                                sb.Append("<td>T</td>");
                                sb.Append("<td>" + BQty.ToString() + "</td>");
                                sb.Append("<td>" + BKgFat.ToString() + "</td>");
                                sb.Append("<td>" + BKgSnf.ToString() + "</td>");
                                sb.Append("<td>" + CQty.ToString() + "</td>");
                                sb.Append("<td>" + CKgFat.ToString() + "</td>");
                                sb.Append("<td>" + CKgSnf.ToString() + "</td>");
                                sb.Append("<td>" + TQty.ToString() + "</td>");
                                sb.Append("<td>" + TKgFat.ToString() + "</td>");
                                sb.Append("<td>" + TKgSnf.ToString() + "</td>");
                                sb.Append("</tr>");
                                BQty = 0;
                                BKgFat = 0;
                                BKgSnf = 0;
                                CQty = 0;
                                CKgFat = 0;
                                CKgSnf = 0;
                                TQty = 0;
                                TKgFat = 0;
                                TKgSnf = 0;
                                sb.Append("<tr>");
                                sb.Append("<td>" + ds.Tables[0].Rows[i]["Quality"].ToString() + "</td>");

                                sb.Append("<td>" + ds.Tables[0].Rows[i]["ShiftName"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[0].Rows[i]["MilkQuantity_B"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[0].Rows[i]["FatInKg_B"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[0].Rows[i]["SnfInKg_B"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[0].Rows[i]["MilkQuantity_C"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[0].Rows[i]["FatInKg_C"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[0].Rows[i]["SnfInKg_C"].ToString() + "</td>");
                                sb.Append("<td>" + Tqty.ToString() + "</td>");
                                sb.Append("<td>" + TFatKg.ToString() + "</td>");
                                sb.Append("<td>" + TSnfKg.ToString() + "</td>");
                                sb.Append("</tr>");
                                BQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity_B"].ToString());
                                BKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg_B"].ToString());
                                BKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg_B"].ToString());
                                CQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity_C"].ToString());
                                CKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg_C"].ToString());
                                CKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg_C"].ToString());
                                TQty += Tqty;
                                TKgFat += TFatKg;
                                TKgSnf += TSnfKg;
                            }
                            else
                            {
                                sb.Append("<tr>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["Quality"].ToString() + "</td>");

                                sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["ShiftName"].ToString() + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["MilkQuantity_B"].ToString() + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["FatInKg_B"].ToString() + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["SnfInKg_B"].ToString() + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["MilkQuantity_C"].ToString() + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["FatInKg_C"].ToString() + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[0].Rows[i]["SnfInKg_C"].ToString() + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + Tqty.ToString() + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + TFatKg.ToString() + "</td>");
                                sb.Append("<td style='border-top:1px dashed black;'>" + TSnfKg.ToString() + "</td>");
                                sb.Append("</tr>");
                                BQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity_B"].ToString());
                                BKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg_B"].ToString());
                                BKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg_B"].ToString());
                                CQty += decimal.Parse(ds.Tables[0].Rows[i]["MilkQuantity_C"].ToString());
                                CKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatInKg_C"].ToString());
                                CKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfInKg_C"].ToString());
                                TQty += Tqty;
                                TKgFat += TFatKg;
                                TKgSnf += TSnfKg;
                            }

                        }
                    }



                    LastDate = ds.Tables[0].Rows[i]["EntryDate"].ToString();
                    LastQuality = ds.Tables[0].Rows[i]["Quality"].ToString();
                }

                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td>T</td>");
                sb.Append("<td>" + BQty.ToString() + "</td>");
                sb.Append("<td>" + BKgFat.ToString() + "</td>");
                sb.Append("<td>" + BKgSnf.ToString() + "</td>");
                sb.Append("<td>" + CQty.ToString() + "</td>");
                sb.Append("<td>" + CKgFat.ToString() + "</td>");
                sb.Append("<td>" + CKgSnf.ToString() + "</td>");
                sb.Append("<td>" + TQty.ToString() + "</td>");
                sb.Append("<td>" + TKgFat.ToString() + "</td>");
                sb.Append("<td>" + TKgSnf.ToString() + "</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='border-bottom:1px dashed black; font-size:16px;' colspan= '11'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='border-bottom:1px dashed black;  font-size:16px; border-top:1px dashed black;' colspan= '11'><b>Grand Total : = = = = ></b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='border-bottom:1px dashed black;'></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;' rowspan='2'>Quality</td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black;' rowspan='2'>Shift</td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black text-align:center;' colspan='3'>< - - BUFF MILK - - ></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black text-align:center;' colspan='3'>< - - COW MILK - - ></td>");
                sb.Append("<td style='border-top:1px dashed black; border-bottom:1px dashed black text-align:center;' colspan='3'>< - - TOTAL MILK - - ></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td>QTY</td>");
                sb.Append("<td>KgFAT</td>");
                sb.Append("<td>KgSNF</td>");
                sb.Append("<td>QTY</td>");
                sb.Append("<td>KgFAT</td>");
                sb.Append("<td>KgSNF</td>");
                sb.Append("<td>QTY</td>");
                sb.Append("<td>KgFAT</td>");
                sb.Append("<td>KgSNF</td>");
                sb.Append("</tr>");
                BQty = 0;
                BKgFat = 0;
                BKgSnf = 0;
                CQty = 0;
                CKgFat = 0;
                CKgSnf = 0;
                TQty = 0;
                TKgFat = 0;
                TKgSnf = 0;
                int dsCount = ds.Tables[1].Rows.Count;
                for (int i = 0; i < dsCount; i++)
                {
                    if(i == 0)
                    {
                        Tqty = decimal.Parse(ds.Tables[1].Rows[i]["MilkQuantity_B"].ToString()) + decimal.Parse(ds.Tables[1].Rows[i]["MilkQuantity_C"].ToString());
                        TFatKg = decimal.Parse(ds.Tables[1].Rows[i]["FatInKg_B"].ToString()) + decimal.Parse(ds.Tables[1].Rows[i]["FatInKg_C"].ToString());
                        TSnfKg = decimal.Parse(ds.Tables[1].Rows[i]["SnfInKg_B"].ToString()) + decimal.Parse(ds.Tables[1].Rows[i]["SnfInKg_C"].ToString());
                        sb.Append("<tr>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[1].Rows[i]["Quality"].ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[1].Rows[i]["ShiftName"].ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[1].Rows[i]["MilkQuantity_B"].ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[1].Rows[i]["FatInKg_B"].ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[1].Rows[i]["SnfInKg_B"].ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[1].Rows[i]["MilkQuantity_C"].ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[1].Rows[i]["FatInKg_C"].ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + ds.Tables[1].Rows[i]["SnfInKg_C"].ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + Tqty.ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + TFatKg.ToString() + "</td>");
                        sb.Append("<td style='border-top:1px dashed black;'>" + TSnfKg.ToString() + "</td>");
                        sb.Append("</tr>");
                        BQty += decimal.Parse(ds.Tables[1].Rows[i]["MilkQuantity_B"].ToString());
                        BKgFat += decimal.Parse(ds.Tables[1].Rows[i]["FatInKg_B"].ToString());
                        BKgSnf += decimal.Parse(ds.Tables[1].Rows[i]["SnfInKg_B"].ToString());
                        CQty += decimal.Parse(ds.Tables[1].Rows[i]["MilkQuantity_C"].ToString());
                        CKgFat += decimal.Parse(ds.Tables[1].Rows[i]["FatInKg_C"].ToString());
                        CKgSnf += decimal.Parse(ds.Tables[1].Rows[i]["SnfInKg_C"].ToString());
                        TQty += Tqty;
                        TKgFat += TFatKg;
                        TKgSnf += TSnfKg;
                    }
                    else if (ds.Tables[1].Rows[i]["Quality"].ToString() == LastQuality.ToString())
                    {
                        Tqty = decimal.Parse(ds.Tables[1].Rows[i]["MilkQuantity_B"].ToString()) + decimal.Parse(ds.Tables[1].Rows[i]["MilkQuantity_C"].ToString());
                        TFatKg = decimal.Parse(ds.Tables[1].Rows[i]["FatInKg_B"].ToString()) + decimal.Parse(ds.Tables[1].Rows[i]["FatInKg_C"].ToString());
                        TSnfKg = decimal.Parse(ds.Tables[1].Rows[i]["SnfInKg_B"].ToString()) + decimal.Parse(ds.Tables[1].Rows[i]["SnfInKg_C"].ToString());
                        sb.Append("<tr>");
                        sb.Append("<td></td>");
                        sb.Append("<td>" + ds.Tables[1].Rows[i]["ShiftName"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkQuantity_B"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[1].Rows[i]["FatInKg_B"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[1].Rows[i]["SnfInKg_B"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkQuantity_C"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[1].Rows[i]["FatInKg_C"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[1].Rows[i]["SnfInKg_C"].ToString() + "</td>");
                        sb.Append("<td>" + Tqty.ToString() + "</td>");
                        sb.Append("<td>" + TFatKg.ToString() + "</td>");
                        sb.Append("<td>" + TSnfKg.ToString() + "</td>");
                        sb.Append("</tr>");
                        BQty += decimal.Parse(ds.Tables[1].Rows[i]["MilkQuantity_B"].ToString());
                        BKgFat += decimal.Parse(ds.Tables[1].Rows[i]["FatInKg_B"].ToString());
                        BKgSnf += decimal.Parse(ds.Tables[1].Rows[i]["SnfInKg_B"].ToString());
                        CQty += decimal.Parse(ds.Tables[1].Rows[i]["MilkQuantity_C"].ToString());
                        CKgFat += decimal.Parse(ds.Tables[1].Rows[i]["FatInKg_C"].ToString());
                        CKgSnf += decimal.Parse(ds.Tables[1].Rows[i]["SnfInKg_C"].ToString());
                        TQty += Tqty;
                        TKgFat += TFatKg;
                        TKgSnf += TSnfKg;
                    }
                    else
                    {
                        sb.Append("<tr>");
                        sb.Append("<td></td>");
                        sb.Append("<td>T</td>");
                        sb.Append("<td>" + BQty.ToString() + "</td>");
                        sb.Append("<td>" + BKgFat.ToString() + "</td>");
                        sb.Append("<td>" + BKgSnf.ToString() + "</td>");
                        sb.Append("<td>" + CQty.ToString() + "</td>");
                        sb.Append("<td>" + CKgFat.ToString() + "</td>");
                        sb.Append("<td>" + CKgSnf.ToString() + "</td>");
                        sb.Append("<td>" + TQty.ToString() + "</td>");
                        sb.Append("<td>" + TKgFat.ToString() + "</td>");
                        sb.Append("<td>" + TKgSnf.ToString() + "</td>");
                        sb.Append("</tr>");
                        BQty = 0;
                        BKgFat = 0;
                        BKgSnf = 0;
                        CQty = 0;
                        CKgFat = 0;
                        CKgSnf = 0;
                        TQty = 0;
                        TKgFat = 0;
                        TKgSnf = 0;
                        Tqty = decimal.Parse(ds.Tables[1].Rows[i]["MilkQuantity_B"].ToString()) + decimal.Parse(ds.Tables[1].Rows[i]["MilkQuantity_C"].ToString());
                        TFatKg = decimal.Parse(ds.Tables[1].Rows[i]["FatInKg_B"].ToString()) + decimal.Parse(ds.Tables[1].Rows[i]["FatInKg_C"].ToString());
                        TSnfKg = decimal.Parse(ds.Tables[1].Rows[i]["SnfInKg_B"].ToString()) + decimal.Parse(ds.Tables[1].Rows[i]["SnfInKg_C"].ToString());
                        sb.Append("<tr>");
                        sb.Append("<td>" + ds.Tables[1].Rows[i]["Quality"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[1].Rows[i]["ShiftName"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkQuantity_B"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[1].Rows[i]["FatInKg_B"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[1].Rows[i]["SnfInKg_B"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[1].Rows[i]["MilkQuantity_C"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[1].Rows[i]["FatInKg_C"].ToString() + "</td>");
                        sb.Append("<td>" + ds.Tables[1].Rows[i]["SnfInKg_C"].ToString() + "</td>");
                        sb.Append("<td>" + Tqty.ToString() + "</td>");
                        sb.Append("<td>" + TFatKg.ToString() + "</td>");
                        sb.Append("<td>" + TSnfKg.ToString() + "</td>");
                        sb.Append("</tr>");
                        BQty += decimal.Parse(ds.Tables[1].Rows[i]["MilkQuantity_B"].ToString());
                        BKgFat += decimal.Parse(ds.Tables[1].Rows[i]["FatInKg_B"].ToString());
                        BKgSnf += decimal.Parse(ds.Tables[1].Rows[i]["SnfInKg_B"].ToString());
                        CQty += decimal.Parse(ds.Tables[1].Rows[i]["MilkQuantity_C"].ToString());
                        CKgFat += decimal.Parse(ds.Tables[1].Rows[i]["FatInKg_C"].ToString());
                        CKgSnf += decimal.Parse(ds.Tables[1].Rows[i]["SnfInKg_C"].ToString());
                        TQty += Tqty;
                        TKgFat += TFatKg;
                        TKgSnf += TSnfKg;
                    }
                    LastQuality = ds.Tables[1].Rows[i]["Quality"].ToString();
                }
                sb.Append("<tr>");
                sb.Append("<td style='border-bottom:1px dashed black;'></td>");
                sb.Append("<td style='border-bottom:1px dashed black;'>T</td>");
                sb.Append("<td style='border-bottom:1px dashed black;'>" + BQty.ToString() + "</td>");
                sb.Append("<td style='border-bottom:1px dashed black;'>" + BKgFat.ToString() + "</td>");
                sb.Append("<td style='border-bottom:1px dashed black;'>" + BKgSnf.ToString() + "</td>");
                sb.Append("<td style='border-bottom:1px dashed black;'>" + CQty.ToString() + "</td>");
                sb.Append("<td style='border-bottom:1px dashed black;'>" + CKgFat.ToString() + "</td>");
                sb.Append("<td style='border-bottom:1px dashed black;'>" + CKgSnf.ToString() + "</td>");
                sb.Append("<td style='border-bottom:1px dashed black;'>" + TQty.ToString() + "</td>");
                sb.Append("<td style='border-bottom:1px dashed black;'>" + TKgFat.ToString() + "</td>");
                sb.Append("<td style='border-bottom:1px dashed black;'>" + TKgSnf.ToString() + "</td>");
                sb.Append("</tr>");
            }

            sb.Append("</table>");
            dvReport.InnerHtml = sb.ToString();
            divprint.InnerHtml = sb.ToString();
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
            Response.AddHeader("content-disposition", "attachment;filename=" + "CCWiseSummarySheet(DayWise)" + DateTime.Now + ".xls");
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