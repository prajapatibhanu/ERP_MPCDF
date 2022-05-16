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
using System.Text;
public partial class mis_MilkCollection_RptBMCEntryAtQC : System.Web.UI.Page
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
                txtDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");               
                txtDate.Attributes.Add("readonly", "readonly");
                txtDate_TextChanged(sender, e);
                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }

            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    //private void FillGrid()
    //{
    //    try
    //    {
    //        lblRptMsg.Text = "";
    //        btnPrint.Visible = false;
    //        StringBuilder sb = new StringBuilder();
    //        //gvBMCDetails.DataSource = string.Empty;
    //        //gvBMCDetails.DataBind();
    //        //ds = objdb.ByProcedure("Usp_Trn_MilkCollectionBMCEntryAtQC",
    //        //                    new string[] { "flag", "BMCTankerRoot_Id", "EntryDate"},
    //        //                    new string[] { "3", ddlBMCTankerRootName.SelectedValue,Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd") }, "dataset");
    //        ds = objdb.ByProcedure("Usp_Trn_MilkCollectionBMCEntryAtQC",
    //                            new string[] { "flag", "BI_MilkInOutRefID"},
    //                            new string[] { "11", ddlReferenceNo.SelectedValue }, "dataset");
    //        if(ds != null && ds.Tables.Count > 0)
    //        {
    //            if(ds.Tables[0].Rows.Count > 0)
    //            {
    //                btnPrint.Visible = true;
                    

    //                int RowNo = 1;
    //                string BMCEntryAtQC_Id = "";
    //                string ViaTankerNo = "";
    //                string Temp = "";
    //                string Quantity = "";
    //                string FAT = "";
    //                string SNF = "";
    //                string CLR = "";
    //                string FatKg = "";
    //                string SnfKg = "";
    //                decimal RcvdQty =   0;
    //                decimal RcvdFatKg = 0;
    //                decimal RcvdSnfKg = 0;
    //                int Count = ds.Tables[0].Rows.Count;
    //                for (int i = 0; i < Count; i++)
    //                {

    //                    if (i == 0)
    //                    {
    //                        sb.Append("<table class='table'>");
    //                        sb.Append("<tr>");
    //                        sb.Append("<td colspan='2' style='border-top:1px solid black; border-left:1px solid black; border-bottom:1px solid black;'><b>BMC " + ds.Tables[0].Rows[i]["BMCTankerRootName"].ToString() + "</b></td>");
    //                        sb.Append("<td  style='border-top:1px solid black; border-bottom:1px solid black;'><b>Tan. No. " + ds.Tables[0].Rows[i]["ViaTankerNo"].ToString() + "</b></td>");
    //                        sb.Append("<td colspan='3' style='border-top:1px solid black; border-bottom:1px solid black;'><b>Time - " + ds.Tables[0].Rows[i]["EntryTime"].ToString() + "</td>");
    //                        sb.Append("<td colspan='3' style='border-top:1px solid black; border-right:1px solid black; border-bottom:1px solid black;'><b>Date " + ds.Tables[0].Rows[i]["EntryDate"].ToString() + "</b></td>");
    //                        sb.Append("</tr>");
    //                        sb.Append("<tr>");
    //                        sb.Append("<th class='border' style='text-align:center'>S.N</th>");
    //                        sb.Append("<th class='border' style='text-align:center'>Name</th>");
    //                        sb.Append("<th class='border' style='text-align:center'>Temp</th>");
    //                        sb.Append("<th class='border' style='text-align:center'>Qty</th>");
    //                        sb.Append("<th class='border' style='text-align:center'>Fat %</th>");
    //                        sb.Append("<th class='border' style='text-align:center'>CLR</th>");
    //                        sb.Append("<th class='border' style='text-align:center'>SNF%</th>");
    //                        sb.Append("<th class='border' style='text-align:center'>Kg Fat</th>");
    //                        sb.Append("<th class='border' style='text-align:center'>Kg SNF</th>");
    //                        sb.Append("</tr>");
                            
    //                        sb.Append("<tr>");
    //                        sb.Append("<td class='border' style='text-align:center'><b>" + (RowNo).ToString() + "</b></td>");
    //                        sb.Append("<td class='border'>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
    //                        sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RTemp"].ToString() + "</td>");
    //                        sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RQuantity"].ToString() + "</td>");
    //                        sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RFAT"].ToString() + "</td>");
    //                        sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RSNF"].ToString() + "</td>");
    //                        sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RCLR"].ToString() + "</td>");
    //                        sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RFatKg"].ToString() + "</td>");
    //                        sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RSnfKg"].ToString() + "</td>");
    //                        sb.Append("</tr>");
    //                        BMCEntryAtQC_Id = ds.Tables[0].Rows[i]["BMCEntryAtQC_Id"].ToString();
    //                        ViaTankerNo = ds.Tables[0].Rows[i]["ViaTankerNo"].ToString();
    //                        Temp = ds.Tables[0].Rows[i]["Temp"].ToString();
    //                        Quantity = ds.Tables[0].Rows[i]["Quantity"].ToString();
    //                        FAT = ds.Tables[0].Rows[i]["FAT"].ToString();
    //                        SNF = ds.Tables[0].Rows[i]["SNF"].ToString();
    //                        CLR = ds.Tables[0].Rows[i]["CLR"].ToString();
    //                        FatKg = ds.Tables[0].Rows[i]["FatKg"].ToString();
    //                        SnfKg = ds.Tables[0].Rows[i]["SnfKg"].ToString();
    //                        RcvdQty +=   decimal.Parse(ds.Tables[0].Rows[i]["RQuantity"].ToString());
    //                        RcvdFatKg += decimal.Parse(ds.Tables[0].Rows[i]["RFatKg"].ToString());
    //                        RcvdSnfKg += decimal.Parse(ds.Tables[0].Rows[i]["RSnfKg"].ToString());
    //                        RowNo += 1;

    //                        if (i == (Count - 1))
    //                        {
    //                            sb.Append("<tr>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border'><b>D.C.S Received</b></td>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + RcvdQty + "</td>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + RcvdFatKg + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + RcvdSnfKg + "</td>");
    //                            sb.Append("</tr>");
    //                            sb.Append("<tr>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border'><b>" + ViaTankerNo + "</b></td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + Temp + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + Quantity + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + FAT + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + SNF + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + CLR + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + FatKg + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + SnfKg + "</td>");
    //                            sb.Append("</tr>");
    //                            sb.Append("<tr>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border'><b>Difference</td></b>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border' style='text-align:center'><b>" + (decimal.Parse(Quantity) - RcvdQty) + "</b></td>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border'style='text-align:center'><b>" + (decimal.Parse(FatKg) - RcvdFatKg) + "</b></td>");
    //                            sb.Append("<td class='border'style='text-align:center'><b>" + (decimal.Parse(SnfKg) - RcvdSnfKg) + "</b></td>");
    //                            sb.Append("</tr>");
    //                            sb.Append("</table>");
    //                        }
                            
                            
    //                    }
    //                    else
    //                    {
    //                        if (BMCEntryAtQC_Id == ds.Tables[0].Rows[i]["BMCEntryAtQC_Id"].ToString())
    //                        {
    //                            if (i == (Count - 1))
    //                            {
    //                                sb.Append("<tr>");
    //                                sb.Append("<td class='border' style='text-align:center; width:10px;'><b>" + (RowNo).ToString() + "</b></td>");
    //                                sb.Append("<td class='border'>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RTemp"].ToString() + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RQuantity"].ToString() + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RFAT"].ToString() + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RSNF"].ToString() + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RCLR"].ToString() + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RFatKg"].ToString() + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RSnfKg"].ToString() + "</td>");
    //                                sb.Append("</tr>");
    //                                BMCEntryAtQC_Id = ds.Tables[0].Rows[i]["BMCEntryAtQC_Id"].ToString();
    //                                ViaTankerNo = ds.Tables[0].Rows[i]["ViaTankerNo"].ToString();
    //                                Temp = ds.Tables[0].Rows[i]["Temp"].ToString();
    //                                Quantity = ds.Tables[0].Rows[i]["Quantity"].ToString();
    //                                FAT = ds.Tables[0].Rows[i]["FAT"].ToString();
    //                                SNF = ds.Tables[0].Rows[i]["SNF"].ToString();
    //                                CLR = ds.Tables[0].Rows[i]["CLR"].ToString();
    //                                FatKg = ds.Tables[0].Rows[i]["FatKg"].ToString();
    //                                SnfKg = ds.Tables[0].Rows[i]["SnfKg"].ToString();
    //                                RcvdQty += decimal.Parse(ds.Tables[0].Rows[i]["RQuantity"].ToString());
    //                                RcvdFatKg += decimal.Parse(ds.Tables[0].Rows[i]["RFatKg"].ToString());
    //                                RcvdSnfKg += decimal.Parse(ds.Tables[0].Rows[i]["RSnfKg"].ToString());
    //                                RowNo += 1;
    //                                sb.Append("<tr>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border'><b>D.C.S Received</b></td>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + RcvdQty + "</td>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + RcvdFatKg + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + RcvdSnfKg + "</td>");
    //                                sb.Append("</tr>");
    //                                sb.Append("<tr>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border'><b>" + ViaTankerNo + "</b></td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + Temp + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + Quantity + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + FAT + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + SNF + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + CLR + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + FatKg + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + SnfKg + "</td>");
    //                                sb.Append("</tr>");
    //                                sb.Append("<tr>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border'><b>Difference</b></td>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border' style='text-align:center'><b>" + (decimal.Parse(Quantity) - RcvdQty) + "</b></td>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border' style='text-align:center'><b>" + (decimal.Parse(FatKg) - RcvdFatKg) + "</b></td>");
    //                                sb.Append("<td class='border' style='text-align:center'><b>" + (decimal.Parse(SnfKg) - RcvdSnfKg) + "</b></td>");
    //                                sb.Append("</tr>");
    //                                sb.Append("</table>");
    //                            }
    //                            else
    //                            {
    //                                sb.Append("<tr>");
    //                                sb.Append("<td class='border' style='text-align:center; width:10px;'><b>" + (RowNo).ToString() + "</b></td>");
    //                                sb.Append("<td class='border'>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RTemp"].ToString() + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RQuantity"].ToString() + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RFAT"].ToString() + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RSNF"].ToString() + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RCLR"].ToString() + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RFatKg"].ToString() + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RSnfKg"].ToString() + "</td>");
    //                                sb.Append("</tr>");
    //                                BMCEntryAtQC_Id = ds.Tables[0].Rows[i]["BMCEntryAtQC_Id"].ToString();
    //                                ViaTankerNo = ds.Tables[0].Rows[i]["ViaTankerNo"].ToString();
    //                                Temp = ds.Tables[0].Rows[i]["Temp"].ToString();
    //                                Quantity = ds.Tables[0].Rows[i]["Quantity"].ToString();
    //                                FAT = ds.Tables[0].Rows[i]["FAT"].ToString();
    //                                SNF = ds.Tables[0].Rows[i]["SNF"].ToString();
    //                                CLR = ds.Tables[0].Rows[i]["CLR"].ToString();
    //                                FatKg = ds.Tables[0].Rows[i]["FatKg"].ToString();
    //                                SnfKg = ds.Tables[0].Rows[i]["SnfKg"].ToString();
    //                                RcvdQty += decimal.Parse(ds.Tables[0].Rows[i]["RQuantity"].ToString());
    //                                RcvdFatKg += decimal.Parse(ds.Tables[0].Rows[i]["RFatKg"].ToString());
    //                                RcvdSnfKg += decimal.Parse(ds.Tables[0].Rows[i]["RSnfKg"].ToString());
    //                                RowNo += 1;
    //                            }
                                
    //                        }
    //                        else
    //                        {
    //                            sb.Append("<tr>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border'><b>D.C.S Received</b></td>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + RcvdQty + "</td>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + RcvdFatKg + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + RcvdSnfKg + "</td>");
    //                            sb.Append("</tr>");
    //                            sb.Append("<tr>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border'><b>" + ViaTankerNo + "</b></td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + Temp + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + Quantity + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + FAT + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + SNF + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + CLR + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + FatKg + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + SnfKg + "</td>");
    //                            sb.Append("</tr>");
    //                            sb.Append("<tr>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border'><b>Difference</td></b>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border' style='text-align:center'><b>" + (decimal.Parse(Quantity) - RcvdQty) + "</b></td>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border'></td>");
    //                            sb.Append("<td class='border'style='text-align:center'><b>" + (decimal.Parse(FatKg) - RcvdFatKg) + "</b></td>");
    //                            sb.Append("<td class='border'style='text-align:center'><b>" + (decimal.Parse(SnfKg) - RcvdSnfKg) + "</b></td>");
    //                            sb.Append("</tr>");
    //                            sb.Append("</table>");
    //                            RowNo = 1;
    //                            RcvdQty = 0;
    //                            RcvdFatKg = 0;
    //                            RcvdSnfKg = 0;
    //                            sb.Append("<table class='table'>");
    //                            sb.Append("<tr>");
    //                            sb.Append("<td colspan='2' style='border-top:1px solid black; border-left:1px solid black; border-bottom:1px solid black;'><b>BMC " + ds.Tables[0].Rows[i]["BMCTankerRootName"].ToString() + "</b></td>");
    //                            sb.Append("<td style='border-top:1px solid black; border-bottom:1px solid black;'><b>Tan. No. " + ds.Tables[0].Rows[i]["ViaTankerNo"].ToString() + "</b></td>");
    //                            sb.Append("<td colspan='3' style='border-top:1px solid black; border-bottom:1px solid black;'><b>Time - " + ds.Tables[0].Rows[i]["EntryTime"].ToString() + "</td>");
    //                            sb.Append("<td colspan='3' style='border-top:1px solid black; border-right:1px solid black; border-bottom:1px solid black;'><b>Date " + ds.Tables[0].Rows[i]["EntryDate"].ToString() + "</b></td>");
    //                            sb.Append("</tr>");
    //                            sb.Append("<tr>");
    //                            sb.Append("<th class='border' style='text-align:center'>S.N</th>");
    //                            sb.Append("<th class='border' style='text-align:center'>Name</th>");
    //                            sb.Append("<th class='border' style='text-align:center'>Temp</th>");
    //                            sb.Append("<th class='border' style='text-align:center'>Qty</th>");
    //                            sb.Append("<th class='border' style='text-align:center'>Fat %</th>");
    //                            sb.Append("<th class='border' style='text-align:center'>CLR</th>");
    //                            sb.Append("<th class='border' style='text-align:center'>SNF%</th>");
    //                            sb.Append("<th class='border' style='text-align:center'>Kg Fat</th>");
    //                            sb.Append("<th class='border' style='text-align:center'>Kg SNF</th>");
    //                            sb.Append("</tr>");
                                
    //                            sb.Append("<tr>");
    //                            sb.Append("<td class='border' style='text-align:center; width:10px;'><b>" + (RowNo).ToString() + "</b></td>");
    //                            sb.Append("<td class='border'>" + ds.Tables[0].Rows[i]["Office_Name"].ToString() + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RTemp"].ToString() + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RQuantity"].ToString() + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RFAT"].ToString() + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RSNF"].ToString() + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RCLR"].ToString() + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RFatKg"].ToString() + "</td>");
    //                            sb.Append("<td class='border' style='text-align:center'>" + ds.Tables[0].Rows[i]["RSnfKg"].ToString() + "</td>");
    //                            sb.Append("</tr>");
    //                            BMCEntryAtQC_Id = ds.Tables[0].Rows[i]["BMCEntryAtQC_Id"].ToString();
    //                            ViaTankerNo = ds.Tables[0].Rows[i]["ViaTankerNo"].ToString();
    //                            Temp = ds.Tables[0].Rows[i]["Temp"].ToString();
    //                            Quantity = ds.Tables[0].Rows[i]["Quantity"].ToString();
    //                            FAT = ds.Tables[0].Rows[i]["FAT"].ToString();
    //                            SNF = ds.Tables[0].Rows[i]["SNF"].ToString();
    //                            CLR = ds.Tables[0].Rows[i]["CLR"].ToString();
    //                            FatKg = ds.Tables[0].Rows[i]["FatKg"].ToString();
    //                            SnfKg = ds.Tables[0].Rows[i]["SnfKg"].ToString();
    //                            RcvdQty += decimal.Parse(ds.Tables[0].Rows[i]["RQuantity"].ToString());
    //                            RcvdFatKg += decimal.Parse(ds.Tables[0].Rows[i]["RFatKg"].ToString());
    //                            RcvdSnfKg += decimal.Parse(ds.Tables[0].Rows[i]["RSnfKg"].ToString());
    //                            RowNo += 1;
    //                            if(i == (Count - 1))
    //                            {
    //                                sb.Append("<tr>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border'><b>D.C.S Received</b></td>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + RcvdQty + "</td>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + RcvdFatKg + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + RcvdSnfKg + "</td>");
    //                                sb.Append("</tr>");
    //                                sb.Append("<tr>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border'><b>" + ViaTankerNo + "</b></td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + Temp + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + Quantity + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + FAT + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + SNF + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + CLR + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + FatKg + "</td>");
    //                                sb.Append("<td class='border' style='text-align:center'>" + SnfKg + "</td>");
    //                                sb.Append("</tr>");
    //                                sb.Append("<tr>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border'><b>Difference</td></b>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border' style='text-align:center'><b>" + (decimal.Parse(Quantity) - RcvdQty) + "</b></td>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border'></td>");
    //                                sb.Append("<td class='border'style='text-align:center'><b>" + (decimal.Parse(FatKg) - RcvdFatKg) + "</b></td>");
    //                                sb.Append("<td class='border'style='text-align:center'><b>" + (decimal.Parse(SnfKg) - RcvdSnfKg) + "</b></td>");
    //                                sb.Append("</tr>");
    //                                sb.Append("</table>");
    //                            }


    //                        }
    //                    }

    //                }

                    
    //                //gvBMCDetails.DataSource = ds;
    //                //gvBMCDetails.DataBind();
                   

    //            }
    //            else
    //            {
    //                lblRptMsg.Text = "No Record Found";
    //                //gvBMCDetails.DataSource = string.Empty;
    //                //gvBMCDetails.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            lblRptMsg.Text = "No Record Found";
    //            //gvBMCDetails.DataSource = string.Empty;
    //            //gvBMCDetails.DataBind();
    //        }
    //        divReport.InnerHtml = sb.ToString();
    //        divPrintReport.InnerHtml = sb.ToString();

           
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5:" + ex.Message.ToString());
    //    }
    //}
    private void FillGrid()
    {
        try
        {
            lblRptMsg.Text = "";
            btnPrint.Visible = false;
            btnExport.Visible = false;
			StringBuilder sb = new StringBuilder();
            
           ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New",
                               new string[] { "flag", "BI_MilkInOutRefID", "I_OfficeID", "D_Date" },
                               new string[] { "13", ddlReferenceNo.SelectedValue, objdb.Office_ID(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
           if(ds != null && ds.Tables.Count > 0)
            {
               if(ds.Tables[0].Rows.Count > 0)
               {
                   btnPrint.Visible = true;
                   btnExport.Visible = true;
                   int count = ds.Tables[0].Rows.Count;
                   string BI_MilkInOutRefID = "";
                   string V_VehicleNo = "";
                   decimal TotalQty = 0;
                   decimal TKgFat = 0;
                   decimal TKgSnf = 0;
                   decimal RTotalQty = 0;
                   decimal RTKgFat = 0;
                   decimal RTKgSnf = 0;
                   int Count = 0;
                   for (int i = 0; i < count; i++)
                   {
                       
                       if(i == 0)
                       {
                           sb.Append("<table class='table table-bordered' border='1'>");
                           sb.Append("<tr>");
                           sb.Append("<td colspan='9' style='text-align:center;'><b>" + Session["Office_Name"].ToString() + "</b></td>");
                           sb.Append("</tr>");
                           sb.Append("<tr>");
                           sb.Append("<td colspan='9' style='text-align:center;'><b>BMC -  " + ds.Tables[0].Rows[i]["BMCTankerRootName"].ToString() + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Tan.No.&nbsp;&nbsp;" + ds.Tables[0].Rows[i]["V_VehicleNo"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;Time - " + ds.Tables[0].Rows[i]["Time"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;Date - " + ds.Tables[0].Rows[i]["Date"].ToString() + "</b></td>");
                           sb.Append("</tr>");
                           sb.Append("<tr>");
                           sb.Append("<th>S.No</td>");
                           sb.Append("<th style='text-align:center;'>Name</th>");
                           sb.Append("<th style='text-align:center;'>Temp</th>");
                           sb.Append("<th style='text-align:center;'>Qty</th>");
                           sb.Append("<th style='text-align:center;'>Fat %</th>");
                           sb.Append("<th style='text-align:center;'>CLR</th>");
                           sb.Append("<th style='text-align:center;'>SNF %</th>");
                           sb.Append("<th style='text-align:center;'>Kg Fat</th>");
                           sb.Append("<th style='text-align:center;'>Kg SNF</th>");
                           sb.Append("</tr>");
                           if (i == (count - 1))
                           {
                               sb.Append("<tr>");
                               sb.Append("<td style='text-align:center;'>" + (Count + 1).ToString() + "</td>");
                               sb.Append("<td style='text-align:left;'>" + ds.Tables[0].Rows[i]["Office_Name"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Temp"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Quantity"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FAT"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["CLR"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SNF"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FatKg"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SnfKg"] + "</td>");
                               sb.Append("</tr>");
                               BI_MilkInOutRefID = ds.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString();
                               V_VehicleNo = ds.Tables[0].Rows[i]["V_VehicleNo"].ToString();
                               TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString());
                               TKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatKg"].ToString());
                               TKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfKg"].ToString());
                               sb.Append("<tr>");
                               sb.Append("<td style='text-align:center;'></td>");
                               sb.Append("<td style='text-align:left;'><b>D.C.S Received</b></td>");
                               sb.Append("<td style='text-align:center;'></td>");
                               sb.Append("<td style='text-align:center;'>" + TotalQty + "</td>");
                               sb.Append("<td style='text-align:center;'></td>");
                               sb.Append("<td style='text-align:center;'></td>");
                               sb.Append("<td style='text-align:center;'></td>");
                               sb.Append("<td style='text-align:center;'>" + TKgFat.ToString() + "</td>");
                               sb.Append("<td style='text-align:center;'>" + TKgSnf.ToString() + "</td>");
                               sb.Append("</tr>");
                               DataSet dsRecord = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New", new string[] { "flag", "BI_MilkInOutRefID" }, new string[] { "14", BI_MilkInOutRefID }, "dataset");
                               if (dsRecord != null && dsRecord.Tables[0].Rows.Count > 0)
                               {
                                   int Count1 = dsRecord.Tables[0].Rows.Count;
                                   for (int j = 0; j < Count1; j++)
                                   {
                                       sb.Append("<tr>");
                                       sb.Append("<td style='text-align:center;'></td>");
                                       sb.Append("<td style='text-align:left;'><b>" + V_VehicleNo + " [ " + dsRecord.Tables[0].Rows[j]["ChamberType"] + " ]</b></td>");
                                       sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["Temp"] + "</td>");
                                       sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["Quantity"] + "</td>");
                                       sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["FAT"] + "</td>");
                                       sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["CLR"] + "</td>");
                                       sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["SNF"] + "</td>");
                                       sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["FatKg"].ToString() + "</td>");
                                       sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["SnfKg"].ToString() + "</td>");
                                       sb.Append("</tr>");
                                       RTotalQty += decimal.Parse(dsRecord.Tables[0].Rows[j]["Quantity"].ToString());
                                       RTKgFat += decimal.Parse(dsRecord.Tables[0].Rows[j]["FatKg"].ToString());
                                       RTKgSnf += decimal.Parse(dsRecord.Tables[0].Rows[j]["SnfKg"].ToString());
                                   }
                               }
                               sb.Append("<tr>");
                               sb.Append("<td style='text-align:center;'></td>");
                               sb.Append("<td style='text-align:left;'><b>Difference</b></td>");
                               sb.Append("<td style='text-align:center;'></td>");
                               sb.Append("<td style='text-align:center;'>" + (RTotalQty - TotalQty) + "</td>");
                               sb.Append("<td style='text-align:center;'></td>");
                               sb.Append("<td style='text-align:center;'></td>");
                               sb.Append("<td style='text-align:center;'></td>");
                               sb.Append("<td style='text-align:center;'>" + (RTKgFat - TKgFat) + "</td>");
                               sb.Append("<td style='text-align:center;'>" + (RTKgSnf - TKgSnf) + "</td>");
                               sb.Append("</tr>");
                               sb.Append("</table>");
                           }
                           else
                           {
                               sb.Append("<tr>");
                               sb.Append("<td style='text-align:center;'>" + (Count + 1).ToString() + "</td>");
                               sb.Append("<td style='text-align:left;'>" + ds.Tables[0].Rows[i]["Office_Name"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Temp"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Quantity"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FAT"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["CLR"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SNF"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FatKg"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SnfKg"] + "</td>");
                               sb.Append("</tr>");
                               BI_MilkInOutRefID = ds.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString();
                               V_VehicleNo = ds.Tables[0].Rows[i]["V_VehicleNo"].ToString();
                               TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString());
                               TKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatKg"].ToString());
                               TKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfKg"].ToString());
                               Count += 1;
                           }
                       }
                       else if(BI_MilkInOutRefID == ds.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString())
                       {
                           if (i == (count - 1))
                           {
                               sb.Append("<tr>");
                               sb.Append("<td style='text-align:center;'>" + (Count + 1).ToString() + "</td>");
                               sb.Append("<td style='text-align:left;'>" + ds.Tables[0].Rows[i]["Office_Name"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Temp"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Quantity"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FAT"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["CLR"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SNF"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FatKg"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SnfKg"] + "</td>");
                               sb.Append("</tr>");
                               BI_MilkInOutRefID = ds.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString();
                               V_VehicleNo = ds.Tables[0].Rows[i]["V_VehicleNo"].ToString();
                               TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString());
                               TKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatKg"].ToString());
                               TKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfKg"].ToString());
                               sb.Append("<tr>");
                               sb.Append("<td style='text-align:center;'></td>");
                               sb.Append("<td style='text-align:left;'><b>D.C.S Received</b></td>");
                               sb.Append("<td style='text-align:center;'></td>");
                               sb.Append("<td style='text-align:center;'>" + TotalQty + "</td>");
                               sb.Append("<td style='text-align:center;'></td>");
                               sb.Append("<td style='text-align:center;'></td>");
                               sb.Append("<td style='text-align:center;'></td>");
                               sb.Append("<td style='text-align:center;'>" + TKgFat.ToString() + "</td>");
                               sb.Append("<td style='text-align:center;'>" + TKgSnf.ToString() + "</td>");
                               sb.Append("</tr>");
                               DataSet dsRecord = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New", new string[] { "flag", "BI_MilkInOutRefID" }, new string[] { "14", BI_MilkInOutRefID }, "dataset");
                               if (dsRecord != null && dsRecord.Tables[0].Rows.Count > 0)
                               {
                                   int Count1 = dsRecord.Tables[0].Rows.Count;
                                   for (int j = 0; j < Count1; j++)
                                   {
                                       sb.Append("<tr>");
                                       sb.Append("<td style='text-align:center;'></td>");
                                       sb.Append("<td style='text-align:left;'><b>" + V_VehicleNo + " [ " + dsRecord.Tables[0].Rows[j]["ChamberType"] + " ]</b></td>");
                                       sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["Temp"] + "</td>");
                                       sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["Quantity"] + "</td>");
                                       sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["FAT"] + "</td>");
                                       sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["CLR"] + "</td>");
                                       sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["SNF"] + "</td>");
                                       sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["FatKg"].ToString() + "</td>");
                                       sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["SnfKg"].ToString() + "</td>");
                                       sb.Append("</tr>");
                                       RTotalQty += decimal.Parse(dsRecord.Tables[0].Rows[j]["Quantity"].ToString());
                                       RTKgFat += decimal.Parse(dsRecord.Tables[0].Rows[j]["FatKg"].ToString());
                                       RTKgSnf += decimal.Parse(dsRecord.Tables[0].Rows[j]["SnfKg"].ToString());
                                   }
                               }
                               sb.Append("<tr>");
                               sb.Append("<td style='text-align:center;'></td>");
                               sb.Append("<td style='text-align:left;'><b>Difference</b></td>");
                               sb.Append("<td style='text-align:center;'></td>");
                               sb.Append("<td style='text-align:center;'>" + (RTotalQty - TotalQty) + "</td>");
                               sb.Append("<td style='text-align:center;'></td>");
                               sb.Append("<td style='text-align:center;'></td>");
                               sb.Append("<td style='text-align:center;'></td>");
                               sb.Append("<td style='text-align:center;'>" + (RTKgFat - TKgFat) + "</td>");
                               sb.Append("<td style='text-align:center;'>" + (RTKgSnf - TKgSnf) + "</td>");
                               sb.Append("</tr>");
                               sb.Append("</table>");
                           }
                           else
                           {


                               sb.Append("<tr>");
                               sb.Append("<td style='text-align:center;'>" + (Count + 1).ToString() + "</td>");
                               sb.Append("<td style='text-align:left;'>" + ds.Tables[0].Rows[i]["Office_Name"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Temp"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Quantity"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FAT"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["CLR"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SNF"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FatKg"] + "</td>");
                               sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SnfKg"] + "</td>");
                               sb.Append("</tr>");
                               BI_MilkInOutRefID = ds.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString();
                               V_VehicleNo = ds.Tables[0].Rows[i]["V_VehicleNo"].ToString();
                               TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString());
                               TKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatKg"].ToString());
                               TKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfKg"].ToString());
                               Count += 1;
                           }
                       }
                       
                       else
                       {
                           sb.Append("<tr>");
                           sb.Append("<td style='text-align:center;'></td>");
                           sb.Append("<td style='text-align:left;'><b>D.C.S Received</b></td>");
                           sb.Append("<td style='text-align:center;'></td>");
                           sb.Append("<td style='text-align:center;'>" + TotalQty + "</td>");
                           sb.Append("<td style='text-align:center;'></td>");
                           sb.Append("<td style='text-align:center;'></td>");
                           sb.Append("<td style='text-align:center;'></td>");
                           sb.Append("<td style='text-align:center;'>" + TKgFat.ToString() + "</td>");
                           sb.Append("<td style='text-align:center;'>" + TKgSnf.ToString() + "</td>");
                           sb.Append("</tr>");
                           DataSet dsRecord = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New", new string[] { "flag", "BI_MilkInOutRefID" }, new string[] { "14", BI_MilkInOutRefID }, "dataset");
                           if (dsRecord != null && dsRecord.Tables[0].Rows.Count > 0)
                           {
                               int Count1 = dsRecord.Tables[0].Rows.Count;
                               for (int j = 0; j < Count1; j++)
                               {
                                   sb.Append("<tr>");
                                   sb.Append("<td style='text-align:center;'></td>");
                                   sb.Append("<td style='text-align:left;'><b>" + V_VehicleNo + " [ " + dsRecord.Tables[0].Rows[j]["ChamberType"] + " ]</b></td>");
                                   sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["Temp"] + "</td>");
                                   sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["Quantity"] + "</td>");
                                   sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["FAT"] + "</td>");
                                   sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["CLR"] + "</td>");
                                   sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["SNF"] + "</td>");
                                   sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["FatKg"].ToString() + "</td>");
                                   sb.Append("<td style='text-align:center;'>" + dsRecord.Tables[0].Rows[j]["SnfKg"].ToString() + "</td>");
                                   sb.Append("</tr>");
                                   RTotalQty += decimal.Parse(dsRecord.Tables[0].Rows[j]["Quantity"].ToString());
                                   RTKgFat += decimal.Parse(dsRecord.Tables[0].Rows[j]["FatKg"].ToString());
                                   RTKgSnf += decimal.Parse(dsRecord.Tables[0].Rows[j]["SnfKg"].ToString());
                               }
                           }
                           sb.Append("<tr>");
                           sb.Append("<td style='text-align:center;'></td>");
                           sb.Append("<td style='text-align:left;'><b>Difference</b></td>");
                           sb.Append("<td style='text-align:center;'></td>");
                           sb.Append("<td style='text-align:center;'>" + (RTotalQty - TotalQty) + "</td>");
                           sb.Append("<td style='text-align:center;'></td>");
                           sb.Append("<td style='text-align:center;'></td>");
                           sb.Append("<td style='text-align:center;'></td>");
                           sb.Append("<td style='text-align:center;'>" + (RTKgFat - TKgFat) + "</td>");
                           sb.Append("<td style='text-align:center;'>" + (RTKgSnf - TKgSnf) + "</td>");
                           sb.Append("</tr>");
                           Count = 0;
                           TotalQty = 0;
                           TKgFat = 0;
                           TKgSnf = 0;
						     RTotalQty = 0;
                           RTKgFat = 0;
                           RTKgSnf = 0;
                           sb.Append("</table>");
                           //sb.Append("<div style='page-break-before: always; '></div>");
                           sb.Append("<table class='table table-bordered' border='1'>");
                           sb.Append("<tr>");
                           sb.Append("<td colspan='9' style='text-align:center;'><b>" + Session["Office_Name"].ToString() + "</b></td>");
                           sb.Append("</tr>");
                           sb.Append("<tr>");
                           sb.Append("<td colspan='9' style='text-align:center;'><b>BMC -  " + ds.Tables[0].Rows[i]["BMCTankerRootName"].ToString() + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Tan.No.&nbsp;&nbsp;" + ds.Tables[0].Rows[i]["V_VehicleNo"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;Time - " + ds.Tables[0].Rows[i]["Time"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;Date - " + ds.Tables[0].Rows[i]["Date"].ToString() + "</b></td>");
                           sb.Append("</tr>");
                           sb.Append("<tr>");
                           sb.Append("<th>S.No</td>");
                           sb.Append("<th style='text-align:center;'>Name</th>");
                           sb.Append("<th style='text-align:center;'>Temp</th>");
                           sb.Append("<th style='text-align:center;'>Qty</th>");
                           sb.Append("<th style='text-align:center;'>Fat %</th>");
                           sb.Append("<th style='text-align:center;'>CLR</th>");
                           sb.Append("<th style='text-align:center;'>SNF %</th>");
                           sb.Append("<th style='text-align:center;'>Kg Fat</th>");
                           sb.Append("<th style='text-align:center;'>Kg SNF</th>");
                           sb.Append("</tr>");
                           sb.Append("<tr>");
                           sb.Append("<td style='text-align:center;'>" + (Count + 1).ToString() + "</td>");
                           sb.Append("<td style='text-align:left;'>" + ds.Tables[0].Rows[i]["Office_Name"] + "</td>");
                           sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Temp"] + "</td>");
                           sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["Quantity"] + "</td>");
                           sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FAT"] + "</td>");
                           sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["CLR"] + "</td>");
                           sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SNF"] + "</td>");
                           sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["FatKg"] + "</td>");
                           sb.Append("<td style='text-align:center;'>" + ds.Tables[0].Rows[i]["SnfKg"] + "</td>");
                           sb.Append("</tr>");
                           BI_MilkInOutRefID = ds.Tables[0].Rows[i]["BI_MilkInOutRefID"].ToString();
                           V_VehicleNo = ds.Tables[0].Rows[i]["V_VehicleNo"].ToString();
                           TotalQty += decimal.Parse(ds.Tables[0].Rows[i]["Quantity"].ToString());
                           TKgFat += decimal.Parse(ds.Tables[0].Rows[i]["FatKg"].ToString());
                           TKgSnf += decimal.Parse(ds.Tables[0].Rows[i]["SnfKg"].ToString());
                       }
                   }
                  
                   
               }
               divReport.InnerHtml = sb.ToString();
            //    {
            //        btnPrint.Visible = true;
                    
            //        gvBMCDetails.DataSource = ds;
            //        gvBMCDetails.DataBind();
                   

            //    }
            //    else
            //    {
                    
            //       gvBMCDetails.DataSource = string.Empty;
            //       gvBMCDetails.DataBind();
            //    }
            //}
            //else
            //{
                
            //     gvBMCDetails.DataSource = string.Empty;
            //     gvBMCDetails.DataBind();
            }
            

           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5:" + ex.Message.ToString());
        }
    }
    //protected void gvBMCDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {

    //        Label lblOffice_Name_E = (Label)e.Row.FindControl("lblOffice_Name_E");

    //        Label lblRowNo = (Label)e.Row.FindControl("lblRowNo");

    //        if (lblOffice_Name_E.Text == "Received" || lblOffice_Name_E.Text == "Difference" || lblOffice_Name_E.Text.Contains("via TankerNo"))
    //        {

    //            lblRowNo.Text = "";

    //        }
                      
    //    }  
    //}
    public override void VerifyRenderingInServerForm(Control control)
    {
        try
        {


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }



    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        ddlReferenceNo.Items.Clear();
        ds = null;
        ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New",
                     new string[] { "flag", "I_OfficeID", "D_Date"},
                     new string[] { "10", objdb.Office_ID(),Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd")}, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlReferenceNo.DataTextField = "C_ReferenceNo";
                        ddlReferenceNo.DataValueField = "BI_MilkInOutRefID";
                        ddlReferenceNo.DataSource = ds;
                        ddlReferenceNo.DataBind();
                        ddlReferenceNo.Items.Insert(0, new ListItem("All", "0"));
                    }
                    else
                    {
                        //ddlReferenceNo.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
                else
                {
                    //ddlReferenceNo.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                //ddlReferenceNo.Items.Insert(0, new ListItem("Select", "0"));
            }
        
    }

    //protected void gvBMCDetails_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        DataSet dsRecord = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU_New", new string[] { "flag", "BI_MilkInOutRefID" }, new string[] {"12",ddlReferenceNo.SelectedValue }, "dataset");
    //        if(dsRecord != null && dsRecord.Tables.Count > 0)
    //        {
    //            GridView HeaderGrid = (GridView)sender;
    //            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //            TableCell HeaderCell = new TableCell();
    //            HeaderCell.Text = Session["Office_Name"].ToString();
    //            HeaderCell.ColumnSpan = 9;
    //            HeaderCell.Style.Add("font", "bold");
    //            HeaderGridRow.Cells.Add(HeaderCell);
    //            gvBMCDetails.Controls[0].Controls.AddAt(0, HeaderGridRow);
    //            GridViewRow HeaderGridRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //            TableCell HeaderCell1 = new TableCell();
    //            HeaderCell1.Text = "BMC " + "Tan.No&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;: " + dsRecord.Tables[0].Rows[0]["V_VehicleNo"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Time -" + dsRecord.Tables[0].Rows[0]["Time"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Date -" + dsRecord.Tables[0].Rows[0]["Date"].ToString();
    //            HeaderCell1.ColumnSpan = 9;
    //            HeaderCell1.Style.Add("font", "bold");
    //            HeaderGridRow1.Cells.Add(HeaderCell1);

    //            gvBMCDetails.Controls[0].Controls.AddAt(1, HeaderGridRow1);
    //        }
            

    //    }
    //}
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "BMC/DCS Milk Collection QC Entry Report" + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            divReport.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());

            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}