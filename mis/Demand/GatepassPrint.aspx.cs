using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;

public partial class mis_Demand_GatepassPrint : System.Web.UI.Page
{
     APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3, ds4, ds9 = new DataSet();

    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        String cid = Request.QueryString["cid"].ToString();
        String IC = Request.QueryString["IC"].ToString();
        lblIC.Text = Request.QueryString["IC"].ToString();
        lblOffice_ID.Text = Request.QueryString["Office_ID"].ToString();
        ViewState["MultiDemandId"] = Request.QueryString["MultiDemandId"].ToString();
        ViewState["PStatus"] = "";
        if (IC.ToString() == objdb.GetMilkCatId())
        {
            GetMilkChallanDetails(cid.ToString());

        }
        else
        {
            GetProductChallanDetails(cid.ToString());
        }
    }
   //public void getdata()
   //{
   //    if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
   //    {
   //        GetMilkChallanDetails();

   //    }
   //    else
   //    {
   //        GetProductChallanDetails();
   //    }
   //}
    private void GetMilkChallanDetails(string cid)
    {
        try
        {
            ds3 = objdb.ByProcedure("USP_Trn_VehicleDispDelivChallan",
                     new string[] { "Flag", "VehicleDispId", "Office_ID" },
                     new string[] { "8", cid, lblOffice_ID.Text }, "dataset");

            if (ds3.Tables[0].Rows.Count > 0)
            {
                int Count = ds3.Tables[0].Rows.Count;
                int count2 = ds3.Tables[1].Rows.Count;
                int rowcount = count2 + 1;
                StringBuilder sb = new StringBuilder();
                StringBuilder sbP1 = new StringBuilder();
                int TotalofTotalSupplyQty = 0;
                int TotalissueCrate = 0;
                decimal TotalofTotalSupplyQtyInLTR = 0;
                string OfficeName = ds3.Tables[0].Rows[0]["Office_Name"].ToString();
                string[] Dairyplant = OfficeName.Split(' ');

                DataTable tabledemand = new DataTable();
                if (ds3.Tables[1].Rows[0]["Priyojna_status"].ToString() != "1")
                {
                    sb.Append("<div class='invoice'>");
                    sb.Append("<table class='table1' style='width:100%; height:100%'>");
                    sb.Append("<tr>");
                    sb.Append("<td rowspan='3'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                    sb.Append("<td style='font-size:21px;'><b>" + ds3.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></td>");
                    sb.Append("<td style='width:70px;'></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td><b style='font-size:17px;'>" + Dairyplant[0] + " डेरी प्लांट</b></br><b style='font-size:15px;'>दूध एवं दुग्ध पदार्थ गेट पास</b></td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("<table class='table table1-bordered' style='width:100%;'>");
                    sb.Append("<tr>");

                    sb.Append("<th style='text-align:center'></th>");

                    sb.Append("<th style='text-align:center'>क्रैट</th>");
                    sb.Append("<th style='text-align:center'>केन/बॉक्स</th>");
                    sb.Append("<th style='text-align:center'>डी.एम.नं .</th>");
                    sb.Append("<th style='text-align:center' rowspan='" + rowcount + "'><b>गेट पास नं . :" + ds3.Tables[0].Rows[0]["VDChallanNo"].ToString() +
                            "</b></br>दिनांक&nbsp;&nbsp;:<span >" + ds3.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</br>समय&nbsp;&nbsp;:<span >" + ds3.Tables[0].Rows[0]["VehicleOut_Time"].ToString() + "</br>शिफ्ट&nbsp;&nbsp;:<span >" + ds3.Tables[0].Rows[0]["ShiftName"].ToString() + "</th>");
                    sb.Append("</tr>");


                    for (int i = 0; i < count2; i++)
                    {
                        sb.Append("<tr>");

                        if (objdb.Office_ID() == "6")
                        {
                            sb.Append("<td>" + ds3.Tables[1].Rows[i]["BCName"].ToString() + "</br>(" + ds3.Tables[1].Rows[i]["SSName"].ToString() + ")" + "</td>");
                        }
                        else
                        {
                            sb.Append("<td>" + ds3.Tables[1].Rows[i]["DName"].ToString() + "(" + ds3.Tables[1].Rows[i]["SSName"].ToString() + ")" + "</td>");
                        }

                        sb.Append("<td style='text-align:center'>" + ds3.Tables[1].Rows[i]["IssueCrate"].ToString() + (Convert.ToInt32(ds3.Tables[1].Rows[i]["ExtraPacket"]) >= 0 ? ("+" + ds3.Tables[1].Rows[i]["ExtraPacket"].ToString()) : ds3.Tables[1].Rows[i]["ExtraPacket"].ToString()) + "</td>");
                        sb.Append("<td style='text-align:center'>" + ds3.Tables[1].Rows[i]["Box"].ToString() + "</td>");
                        sb.Append("<td style='text-align:center'>" + ds3.Tables[1].Rows[i]["DMNo"].ToString() + "</td>");
                        sb.Append("</tr>");


                    }
                    sb.Append("<tr>");
                    sb.Append("<td  colspan='1'>वाहन क्रं :&nbsp;&nbsp;<span>" + ds3.Tables[0].Rows[0]["VehicleNo"].ToString() + "</span></td>");
                    sb.Append("<td  colspan='3'>वाहन चालक :&nbsp;&nbsp;<span>" + ds3.Tables[0].Rows[0]["Driver_name"].ToString() + "</span></td>");
                    sb.Append("<td  colspan='1'>मो. नं :&nbsp;&nbsp;<span>" + ds3.Tables[0].Rows[0]["Driver_Mobile_No"].ToString() + "</span></td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("<table  class='table table1-bordered' style='width:100%; margin-top:-20px'>");
                    sb.Append("<tr>");
                    sb.Append("<th style='text-align:center'>क्र.म.</th>");
                    sb.Append("<th style='text-align:center'>दूध का नाम</th>");
                    if (objdb.Office_ID() != "3")
                    {
                        sb.Append("<th style='text-align:center'>दूध की मात्रा</th>");
                    }
                    else
                    {
                        sb.Append("<th style='text-align:center'>पैकेट की संख्या</th>");
                    }
                    sb.Append("<th style='text-align:center'>क्रैट की संख्या</th>");
                    sb.Append("<th style='text-align:center'>अन्य</th>");
                    sb.Append("<th style='text-align:center'>केन/बॉक्स/जार</th>");
                    if (objdb.Office_ID() == "6")
                    {

                    }
                    else
                    {
                        sb.Append("<th style='text-align:center'>थप्पी</th>");
                    }
                    sb.Append("</tr>");
                    //int TotalofTotalSupplyQty = 0;
                    //int TotalissueCrate = 0;

                    for (int i = 0; i < Count; i++)
                    {
                        TotalofTotalSupplyQty += int.Parse(ds3.Tables[0].Rows[i]["TotalSupplyQty"].ToString());
                        TotalissueCrate += int.Parse(ds3.Tables[0].Rows[i]["IssueCrate"].ToString());
                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        sb.Append("<td>" + (ds3.Tables[0].Rows[i]["ItemName_Hindi"].ToString() == "" ? ds3.Tables[0].Rows[i]["ItemName"].ToString() : ds3.Tables[0].Rows[i]["ItemName_Hindi"].ToString()) + "</td>");
                        sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["TotalSupplyQty"].ToString() + "</td>");
                        sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["IssueCrate"].ToString() + "</td>");
                        sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["ExtraPacket"].ToString() + "</td>");
                        sb.Append("<td style='text-align:center'></td>");
                        if (objdb.Office_ID() == "6")
                        {

                        }
                        else
                        {
                            sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["CrateBunch"].ToString() + "+" + ds3.Tables[0].Rows[i]["ExtraCrate"].ToString() + "</td>");
                        }
                        sb.Append("</tr>");

                    }
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>टोटल</b></td>");
                    sb.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQty.ToString() + "</b></td>");
                    sb.Append("<td style='text-align:center'><b>" + (ds3.Tables[0].Rows[0]["MilkCurDMCrateIsueStatus"].ToString() == "1" ? TotalissueCrate.ToString() : "0") + "</b></td>");

                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    if (objdb.Office_ID() == "6")
                    {

                    }
                    else
                    {
                        sb.Append("<td></td>");
                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("<table class='table1' style='width:100%; height:100%'>");
                    sb.Append("<tr>");
                    sb.Append("<td <span style='text-align:left; padding-top:5px; font-weight:500;'><b>Remark :-</b>" + ds3.Tables[0].Rows[0]["Remark"].ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    
                    if (objdb.Office_ID() == "6")
                    {
                        sb.Append("<td <span style='text-align:left; padding-top:20px; font-weight:700;'>&nbsp;&nbsp;&nbsp;&nbsp;हस्ताक्षर  </br>&nbsp;&nbsp;(वाहन चालक)</span></td>");
                        sb.Append("<td <span style='text-align:center; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>विपणन सहायक</span></td>");
                    }
                    else
                    {
                        sb.Append("<td <span style='text-align:left; padding-top:20px; font-weight:700;'>हस्ताक्षर सामग्री प्राप्तकर्ता </br>&nbsp;&nbsp;(वाहन चालक)</span></td>");
                        sb.Append("<td <span style='text-align:center; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>वितरण सहायक</span></td>");
                    }
                    sb.Append("<td <span style='text-align:center; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>सुरक्षा गार्ड</span></td>");
                    if (objdb.Office_ID() == "6")
                    {
                        sb.Append("<td <span style='text-align:right; padding-top:20px; font-weight:700;'>हस्ताक्षर  उत्पादन शाखा</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; सामग्री प्रदायकर्ता</span></td>");
                        sb.Append("<td <span style='text-align:right; padding-top:20px; font-weight:700;'>हस्ताक्षर </br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;भंडार शाखा</span></td>");

                    }
                    else
                    {
                        sb.Append("<td <span style='text-align:right; padding-top:20px; font-weight:700;'>हस्ताक्षर सामग्री प्रदायकर्ता</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(उत्पादन /भंडार शाखा)&nbsp;&nbsp;</span></td>");
                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");


                    string source = ViewState["MultiDemandId"].ToString();
                    string finalvalue = source.Contains(',').ToString();

                    if (finalvalue == "True")
                    {


                        DataColumn colMPDemandId = tabledemand.Columns.Add("MilkOrProductDemandId", typeof(string));
                        string[] lines = source.Split(',');
                        foreach (var line in lines)
                        {
                            string[] split = line.Split(',');

                            DataRow row = tabledemand.NewRow();
                            row.SetField(colMPDemandId, int.Parse(split[0]));

                            tabledemand.Rows.Add(row);
                        }

                        ViewState["MergeData"] = sb.ToString();
                        // ViewState["MergeData"] = sbP1.ToString();

                        if (tabledemand.Rows.Count > 0)
                        {
                            int tmprow = tabledemand.Rows.Count;
                            for (int i = 0; i < tmprow; i++)
                            {
                                string dmid = tabledemand.Rows[i]["MilkOrProductDemandId"].ToString();
                                GetDemandWiseDMDetails(dmid);
                            }

                            Divview.InnerHtml = ViewState["MergeData"].ToString();

                        }
                        if (tabledemand != null)
                        {
                            tabledemand.Dispose();
                            ViewState["MultiDemandId"] = "";
                            ViewState["MergeData"] = "";
                            ViewState["PStatus"] = "";
                        }

                    }
                    else
                    {
                        Divview.InnerHtml = sb.ToString();
                        //Session["View"] = sb.ToString();
                        //Response.Redirect("GatepassPrint.aspx?SB=" + Session["View"].ToString());
                        // Print.InnerHtml = sbP1.ToString();
                    }

                }

                else if (ds3.Tables[1].Rows[0]["Priyojna_status"].ToString() == "1")
                {
                    string[] place = ds3.Tables[1].Rows[0]["SSName"].ToString().Split(' ');

                    //for priyojna adhikari
                    //    if (ViewState["PStatus"].ToString() != "")
                    //{
                    //    sbP1.Append("<p style='page-break-after: always'>");
                    //}
                    //else
                    //{
                    //    sbP1.Append("<p style='page-break-after: always'>");
                    //}
                    sbP1.Append("<div class='invoice'>");
                    sbP1.Append("<table class='table1' style='width:100%; height:100%'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td rowspan='3'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:50px;'/></td>");
                    sbP1.Append("<td style='font-size:21px;'><b>" + ds3.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></td>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");

                    sbP1.Append("<td style='font-size:18px;'><b>" + ds3.Tables[0].Rows[0]["Seller_Address"].ToString() + "</b></td>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");

                    sbP1.Append("<td style='font-size:18px;'><b>GSTIN :" + ds3.Tables[0].Rows[0]["Office_Gst"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;State Code : 0 - MP</b></td>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr></tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("<td style='font-size:18px;'><span style='font-weight:700;'><U>|| गेटपास - सह पावती ||</U></span></td>");
                    sbP1.Append("<td style='width:70px;'></td>");

                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");
                    sbP1.Append("<table  style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;'>प्रति,</span></td>");
                    sbP1.Append("<td style='font-size:17px;width:200px;'><span style='font-weight:700;'>दिनांक : " + ds3.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</span></td>");
                    //sbP1.Append("<td style='font-size:17px;'></span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   परियोजना अधिकारी ,CDPO </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   समन्वित  बाल विकास योजना  </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   महिला / बाल विकास विभाग  </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'><b>   सावेर  </b></span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   जिला इंदौर (म. प्र.) </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>   महोदय , </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;' colspan='2'><span style='font-weight:500;padding-left:60px'>समन्वित बाल विकास योजना के अंतर्गत निम्न विवरण अनुसार सामग्री प्रदाय की जा रही है - </span></td><br/>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>  1. वाहन क्रमांक :  " + ds3.Tables[0].Rows[0]["VehicleNo"].ToString() + "</span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>  2. वाहन चालक का नाम : </span></td>");
                    sbP1.Append("<td style='font-size:15px;width:200px;'><span style='font-weight:500;'>मोबाइल नं. : </span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>  3. प्रदायित सामग्री का विवरण  </span></td>");
                    sbP1.Append("<td style='font-size:15px;width:200px;'><span style='font-weight:500;'>Tally D.M. No. : 4703  </span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("</table>");


                    sbP1.Append("<table class='table table1-bordered' style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<th style='text-align:center'>क्र.म.</th>");

                    sbP1.Append("<th style='text-align:center'>सामग्री का विवरण</th>");
                    sbP1.Append("<th style='text-align:center'>पैक साईज</th>");

                    sbP1.Append("<th style='text-align:center'>प्रदाय नग</th>");
                    sbP1.Append("<th style='text-align:center'>मात्रा (" + ds3.Tables[0].Rows[0]["UnitName"].ToString() + ")</th>");
                    sbP1.Append("<th style='text-align:center'>प्रदाय स्थान का विवरण</th>");
                    sbP1.Append("<th style='text-align:center'>उत्पादन दिनांक</th>");
                    sbP1.Append("<th style='text-align:center'>समाप्ति  दिनांक</th>");
                    sbP1.Append("</tr>");


                    for (int i = 0; i < Count; i++)
                    {
                        TotalofTotalSupplyQty += int.Parse(ds3.Tables[0].Rows[i]["TotalSupplyQty"].ToString());
                        TotalofTotalSupplyQtyInLTR += decimal.Parse(ds3.Tables[0].Rows[i]["SupplyTotalQtyINLTR"].ToString());
                        //if (ds4.Tables[0].Rows[i]["CarriageModeID"].ToString() == "1")
                        //{
                        //    TotalissueCrate += int.Parse(ds4.Tables[0].Rows[i]["IssueCrate"].ToString());
                        //}
                        sbP1.Append("<tr>");
                        sbP1.Append("<td>" + (i + 1) + "</td>");
                        sbP1.Append("<td>" + (ds3.Tables[0].Rows[i]["ItemName_Hindi"].ToString() == "" ? ds3.Tables[0].Rows[i]["ItemName"].ToString() : ds3.Tables[0].Rows[i]["ItemName_Hindi"].ToString()) + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["PackagingSize"].ToString() + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["TotalSupplyQty"].ToString() + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["SupplyTotalQtyInLtr"].ToString() + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + place[2] + "</td>");
                        sbP1.Append("<td style='text-align:center'></td>");
                        sbP1.Append("<td style='text-align:center'></td>");
                        sbP1.Append("</tr>");

                    }
                    sbP1.Append("<tr>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td><b>योग </b></td>");
                    sbP1.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQty.ToString() + "</b></td>");
                    sbP1.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQtyInLTR.ToString() + "</b></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");

                    sbP1.Append("<table  style='width:100%; '>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span >कृपया उपरोक्त सामग्री प्राप्त कर पावती दिलाने का कष्ट करें |</span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");
                    //sbP1.Append("<td style='font-size:17px;'></span></td>");

                    sbP1.Append("<table class='table table1-bordered' style='width:100%;'>");
                    sbP1.Append("<tr>");


                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर वाहन चालक</td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर विवरण  सहायक </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर भंडार सहायक </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर नोडल अधिकृत  अधिकारी</td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");


                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'></td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'> </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'> </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");
                    //sbP1.Append("</br>");
                    sbP1.Append("<hr>");
                    //sbP1.Append("</br>");
                    sbP1.Append("<table  style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;text-align:center;'><span style='font-weight:700;'><U>|| पावती ||</U></span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;उपरोक्तानुसार वाहन क्रमांक <b>" + ds3.Tables[0].Rows[0]["VehicleNo"].ToString() + "</b> से आज दिनांक <b>" + ds3.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b> समय <b>" + ds3.Tables[0].Rows[0]["VehicleOut_Time"].ToString() + "</b>  बजे पर सही स्थिति में प्राप्त किया गया है</span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");

                    sbP1.Append("<table  class='table table1-bordered' style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='text-align:left;height:120px;font-size:17px;width:23%'>वाहन चालक का नाम वहस्ताक्षर दिनांक सहित </td>");
                    sbP1.Append("<td style='text-align:left;height:120px;font-size:17px;width:23%'> </td>");
                    sbP1.Append("<td   style='text-align:left;height:120px;font-size:17px;width:54%'>परियोजना अधिकारी ,CDPO<br/>समन्वित  बाल विकास योजना <br/>महिला / बाल विकास विभाग <br/>सावेर <br/>जिला इंदौर (म. प्र.)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style='font-size:13px;'>हस्ताक्षर सील व  दिनांक सहित </span></td>");

                    sbP1.Append("</tr>");

                    sbP1.Append("</table>");
                    sbP1.Append("</div>");


                   string source = ViewState["MultiDemandId"].ToString();
                string finalvalue = source.Contains(',').ToString();

                if (finalvalue == "True")
                {

                    
                    DataColumn colMPDemandId = tabledemand.Columns.Add("MilkOrProductDemandId", typeof(int));
                    string[] lines = source.Split(',');
                    foreach (var line in lines)
                    {
                        string[] split = line.Split(',');

                        DataRow row = tabledemand.NewRow();
                        row.SetField(colMPDemandId, int.Parse(split[0]));

                        tabledemand.Rows.Add(row);
                    }

                    ViewState["MergeData"] = sbP1.ToString();
                   // ViewState["MergeData"] = sbP1.ToString();

                    if (tabledemand.Rows.Count > 0)
                    {
                        int tmprow = tabledemand.Rows.Count;
                        for (int i = 0; i < tmprow; i++)
                        {
                            string dmid = tabledemand.Rows[i]["MilkOrProductDemandId"].ToString();
                            GetDemandWiseDMDetails(dmid);
                        }

                        Divview.InnerHtml = ViewState["MergeData"].ToString();

                    }
                    if (tabledemand != null)
                    {
                        tabledemand.Dispose();
                        ViewState["MultiDemandId"] = "";
                        ViewState["MergeData"] = "";
                        ViewState["PStatus"] = "";
                    }

                }
                else
                {
                    Divview.InnerHtml = sbP1.ToString();
                   // Print.InnerHtml = sbP1.ToString();
                }
                }



                //string source = ViewState["MultiDemandId"].ToString();
                //string finalvalue = source.Contains(',').ToString();

                //if (finalvalue == "True")
                //{

                //    DataTable tabledemand = new DataTable();
                //    DataColumn colMPDemandId = tabledemand.Columns.Add("MilkOrProductDemandId", typeof(int));
                //    string[] lines = source.Split(',');
                //    foreach (var line in lines)
                //    {
                //        string[] split = line.Split(',');

                //        DataRow row = tabledemand.NewRow();
                //        row.SetField(colMPDemandId, int.Parse(split[0]));

                //        tabledemand.Rows.Add(row);
                //    }

                //    ViewState["MergeData"] = sb.ToString();
                //   // ViewState["MergeData"] = sbP1.ToString();

                //    if (tabledemand.Rows.Count > 0)
                //    {
                //        int tmprow = tabledemand.Rows.Count;
                //        for (int i = 0; i < tmprow; i++)
                //        {
                //            string dmid = tabledemand.Rows[i]["MilkOrProductDemandId"].ToString();
                //            GetDemandWiseDMDetails(dmid);
                //        }

                //        Print.InnerHtml = ViewState["MergeData"].ToString();

                //    }
                //    if (tabledemand != null)
                //    {
                //        tabledemand.Dispose();
                //        ViewState["MultiDemandId"] = "";
                //        ViewState["MergeData"] = "";
                //        ViewState["PStatus"] = "";
                //    }

                //}
                //else
                //{
                //    Print.InnerHtml = sb.ToString();
                //   // Print.InnerHtml = sbP1.ToString();
                //}

                //ClientScriptManager CSM = Page.ClientScript;
                //string strScript = "<script>";
                //strScript += "window.print();";

                //strScript += "</script>";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 8 ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }

    }
    private void GetProductChallanDetails(string cid)
    {
        try
        {
            ds3 = objdb.ByProcedure("USP_Trn_ProductGatePass",
                     new string[] { "Flag", "ProductGatePassId", "Office_ID" },
                     new string[] { "1", cid, lblOffice_ID.Text }, "dataset");

            if (ds3.Tables[0].Rows.Count > 0)
            {
                int Count = ds3.Tables[0].Rows.Count;
                int count2 = ds3.Tables[1].Rows.Count;
                int rowcount = count2 + 1;
                StringBuilder sb = new StringBuilder();
                StringBuilder sbP1 = new StringBuilder();
                int TotalofTotalSupplyQty = 0;
                int TotalissueCrate = 0;
                decimal TotalofTotalSupplyQtyInLTR = 0;
                string OfficeName = ds3.Tables[0].Rows[0]["Office_Name"].ToString();
                string[] Dairyplant = OfficeName.Split(' ');

                DataTable tabledemand = new DataTable();
                if (ds3.Tables[1].Rows[0]["Priyojna_status"].ToString() != "1")
                {
                    sb.Append("<div class='invoice'>");
                    sb.Append("<table class='table1' style='width:100%; height:100%'>");
                    sb.Append("<tr>");
                    sb.Append("<td rowspan='3'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                    sb.Append("<td style='font-size:21px;'><b>" + ds3.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></td>");
                    sb.Append("<td></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td><b style='font-size:17px;'>" + Dairyplant[0] + " डेरी प्लांट</b></br><b style='font-size:15px;'>दूध एवं दुग्ध पदार्थ गेट पास</b></td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("<table class='table table1-bordered' style='width:100%;'>");
                    sb.Append("<tr>");

                    sb.Append("<th style='text-align:center'></th>");

                    sb.Append("<th style='text-align:center'>क्रैट</th>");
                    sb.Append("<th style='text-align:center'>केन/बॉक्स</th>");
                    sb.Append("<th style='text-align:center'>डी.एम.नं .</th>");
                    sb.Append("<th style='text-align:center' rowspan='" + rowcount + "'><b>गेट पास नं . :" + ds3.Tables[0].Rows[0]["GatePassNo"].ToString() +
                            "</b></br>दिनांक&nbsp;&nbsp;:<span >" + ds3.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</br>समय&nbsp;&nbsp;:<span >" + ds3.Tables[0].Rows[0]["VehicleOut_Time"].ToString() + "</br>शिफ्ट&nbsp;&nbsp;:<span >" + ds3.Tables[0].Rows[0]["ShiftName"].ToString() + "</th>");
                    sb.Append("</tr>");


                    for (int i = 0; i < count2; i++)
                    {
                        sb.Append("<tr>");

                        if (objdb.Office_ID() == "6")
                        {
                            sb.Append("<td>" + ds3.Tables[1].Rows[i]["BCName"].ToString() + "</br>(" + ds3.Tables[1].Rows[i]["SSName"].ToString() + ")" + "</td>");
                        }
                        else
                        {
                            sb.Append("<td>" + ds3.Tables[1].Rows[i]["DName"].ToString() + "(" + ds3.Tables[1].Rows[i]["SSName"].ToString() + ")" + "</td>");
                        }
                        sb.Append("<td style='text-align:center'>" + ds3.Tables[1].Rows[i]["IssueCrate"].ToString() + (Convert.ToInt32(ds3.Tables[1].Rows[i]["ExtraPacket"]) >= 0 ? ("+" + ds3.Tables[1].Rows[i]["ExtraPacket"].ToString()) : ds3.Tables[1].Rows[i]["ExtraPacket"].ToString()) + "</td>");
                        sb.Append("<td style='text-align:center'>" + ds3.Tables[1].Rows[i]["Cane_Jar_Box"].ToString() + "</td>");
                        sb.Append("<td style='text-align:center'>" + ds3.Tables[1].Rows[i]["DMNo"].ToString() + "</td>");
                        sb.Append("</tr>");

                    }
                    sb.Append("<tr>");
                    sb.Append("<td  colspan='1'>वाहन क्रं :&nbsp;&nbsp;<span>" + ds3.Tables[0].Rows[0]["VehicleNo"].ToString() + "</span></td>");
                    sb.Append("<td  colspan='3'>वाहन चालक :&nbsp;&nbsp;<span>" + ds3.Tables[0].Rows[0]["Driver_name"].ToString() + "</span></td>");
                    sb.Append("<td  colspan='1'>मो. नं :&nbsp;&nbsp;<span>" + ds3.Tables[0].Rows[0]["Driver_Mobile_No"].ToString() + "</span></td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("<table  class='table table1-bordered' style='width:100%; margin-top:-20px'>");
                    sb.Append("<tr>");
                    sb.Append("<th style='text-align:center'>क्र.म.</th>");
                    if (lblIC.Text == objdb.GetMilkCatId())
                    {
                        sb.Append("<th style='text-align:center'>दूध का नाम</th>");
                    }
                    else
                    {
                        sb.Append("<th style='text-align:center'>उत्पाद प्रकार</th>");
                    }
                    sb.Append("<th style='text-align:center'>मात्रा</th>");
                    sb.Append("<th style='text-align:center'>क्रैट की संख्या</th>");
                    sb.Append("<th style='text-align:center'>अन्य</th>");
                    sb.Append("<th style='text-align:center'>केन/बॉक्स/जार</th>");
                    if (objdb.Office_ID() == "6")
                    {

                    }
                    else
                    {
                        sb.Append("<th style='text-align:center;'>थप्पी</th>");
                    }

                    sb.Append("</tr>");
                    //int TotalofTotalSupplyQty = 0;
                    //int TotalissueCrate = 0;

                    for (int i = 0; i < Count; i++)
                    {
                        if (ds3.Tables[0].Rows[i]["CarriageModeID"].ToString() == "1")
                        {
                            TotalissueCrate += int.Parse(ds3.Tables[0].Rows[i]["IssueCrate"].ToString());
                        }
                        TotalofTotalSupplyQty += int.Parse(ds3.Tables[0].Rows[i]["TotalSupplyQty"].ToString());

                        sb.Append("<tr>");
                        sb.Append("<td>" + (i + 1) + "</td>");
                        sb.Append("<td>" + (ds3.Tables[0].Rows[i]["ItemName_Hindi"].ToString() == "" ? ds3.Tables[0].Rows[i]["ItemName"].ToString() : ds3.Tables[0].Rows[i]["ItemName_Hindi"].ToString()) + "</td>");
                        sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["TotalSupplyQty"].ToString() + "</td>");
                        sb.Append("<td style='text-align:center'>" + (ds3.Tables[0].Rows[i]["CarriageModeID"].ToString() == "1" ? ds3.Tables[0].Rows[i]["IssueCrate"].ToString() : "0") + "</td>");
                        sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["ExtraPacket"].ToString() + "</td>");
                        sb.Append("<td style='text-align:center'>" + (ds3.Tables[0].Rows[i]["CarriageModeID"].ToString() == "2" ? ds3.Tables[0].Rows[i]["Cane_Jar_Box"].ToString() : "0") + "</td>");
                        if (objdb.Office_ID() == "6")
                        {

                        }
                        else
                        {
                            sb.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["CrateBunch"].ToString() + "+" + ds3.Tables[0].Rows[i]["ExtraCrate"].ToString() + "</td>");
                        }
                        sb.Append("</tr>");

                    }
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td><b>टोटल</b></td>");
                    sb.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQty.ToString() + "</b></td>");
                    sb.Append("<td style='text-align:center'><b>" + (ds3.Tables[0].Rows[0]["MilkCurDMCrateIsueStatus"].ToString() == "1" ? TotalissueCrate.ToString() : "0") + "</b></td>");

                    sb.Append("<td></td>");
                    sb.Append("<td></td>");
                    if (objdb.Office_ID() == "6")
                    {

                    }
                    else
                    {
                        sb.Append("<td></td>");
                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("<table class='table1' style='width:100%; height:100%'>");
                    sb.Append("<tr>");
                    sb.Append("<td <span style='text-align:left; padding-top:5px; font-weight:500;'><b>Remark :-</b>" + ds3.Tables[0].Rows[0]["Remark"].ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    
                    if (objdb.Office_ID() == "6")
                    {
                        sb.Append("<td <span style='text-align:left; padding-top:20px; font-weight:700;'>&nbsp;&nbsp;&nbsp;&nbsp;हस्ताक्षर  </br>&nbsp;&nbsp;(वाहन चालक)</span></td>");
                        sb.Append("<td <span style='text-align:center; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>विपणन सहायक</span></td>");
                    }
                    else
                    {
                        sb.Append("<td <span style='text-align:left; padding-top:20px; font-weight:700;'>हस्ताक्षर सामग्री प्राप्तकर्ता </br>&nbsp;&nbsp;(वाहन चालक)</span></td>");
                        sb.Append("<td <span style='text-align:center; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>वितरण सहायक</span></td>");
                    }

                    sb.Append("<td <span style='text-align:center; padding-top:20px; font-weight:700;'>हस्ताक्षर</br>सुरक्षा गार्ड</span></td>");
                    if (objdb.Office_ID() == "6")
                    {
                        sb.Append("<td <span style='text-align:right; padding-top:20px; font-weight:700;'>हस्ताक्षर  उत्पादन शाखा</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; सामग्री प्रदायकर्ता</span></td>");
                        sb.Append("<td <span style='text-align:right; padding-top:20px; font-weight:700;'>हस्ताक्षर </br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;भंडार शाखा</span></td>");

                    }
                    else
                    {
                        sb.Append("<td <span style='text-align:right; padding-top:20px; font-weight:700;'>हस्ताक्षर सामग्री प्रदायकर्ता</br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(उत्पादन /भंडार शाखा)&nbsp;&nbsp;</span></td>");
                    }


                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");


                    string source = ViewState["MultiDemandId"].ToString();
                    string finalvalue = source.Contains(',').ToString();

                    if (finalvalue == "True")
                    {


                        DataColumn colMPDemandId = tabledemand.Columns.Add("MilkOrProductDemandId", typeof(int));
                        string[] lines = source.Split(',');
                        foreach (var line in lines)
                        {
                            string[] split = line.Split(',');

                            DataRow row = tabledemand.NewRow();
                            row.SetField(colMPDemandId, int.Parse(split[0]));

                            tabledemand.Rows.Add(row);
                        }

                        // ViewState["MergeData"] = sb.ToString();
                        ViewState["MergeData"] = sb.ToString();

                        if (tabledemand.Rows.Count > 0)
                        {
                            int tmprow = tabledemand.Rows.Count;
                            for (int i = 0; i < tmprow; i++)
                            {
                                string dmid = tabledemand.Rows[i]["MilkOrProductDemandId"].ToString();
                                GetDemandWiseDMDetails(dmid);
                            }

                            Divview1.InnerHtml = ViewState["MergeData"].ToString();

                        }
                        if (tabledemand != null)
                        {
                            tabledemand.Dispose();
                            ViewState["MultiDemandId"] = "";
                            ViewState["MergeData"] = "";
                            ViewState["PStatus"] = "";
                        }

                    }
                    else
                    {
                        Divview1.InnerHtml = sb.ToString();
                        // Divview1.InnerHtml = sb.ToString();
                    }
                }
                else if (ds3.Tables[1].Rows[0]["Priyojna_status"].ToString() == "1")
                {
                    string[] place = ds3.Tables[1].Rows[0]["SSName"].ToString().Split(' ');
                    //for priyojna adhikari
                    //    if (ViewState["PStatus"].ToString() != "")
                    //{
                    //    sbP1.Append("<p style='page-break-after: always'>");
                    //}
                    //else
                    //{
                    //    sbP1.Append("<p style='page-break-after: always'>");
                    //}
                    sbP1.Append("<div class='invoice'>");
                    sbP1.Append("<table class='table1' style='width:100%; height:100%'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td rowspan='3'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:50px;'/></td>");
                    sbP1.Append("<td style='font-size:21px;'><b>" + ds3.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></td>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");

                    sbP1.Append("<td style='font-size:18px;'><b>" + ds3.Tables[0].Rows[0]["Seller_Address"].ToString() + "</b></td>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");

                    sbP1.Append("<td style='font-size:18px;'><b>GSTIN :" + ds3.Tables[0].Rows[0]["Office_Gst"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;State Code : 0 - MP</b></td>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr></tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("<td style='font-size:18px;'><span style='font-weight:700;'><U>|| गेटपास - सह पावती ||</U></span></td>");
                    sbP1.Append("<td style='width:70px;'></td>");

                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");
                    sbP1.Append("<table  style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;'>प्रति,</span></td>");
                    sbP1.Append("<td style='font-size:17px;width:200px;'><span style='font-weight:700;'>दिनांक : " + ds3.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</span></td>");
                    //sbP1.Append("<td style='font-size:17px;'></span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   परियोजना अधिकारी ,CDPO </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   समन्वित  बाल विकास योजना  </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   महिला / बाल विकास विभाग  </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'><b>   सावेर  </b></span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   जिला इंदौर (म. प्र.) </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>   महोदय , </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;' colspan='2'><span style='font-weight:500;padding-left:60px'>समन्वित बाल विकास योजना के अंतर्गत निम्न विवरण अनुसार सामग्री प्रदाय की जा रही है - </span></td><br/>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>  1. वाहन क्रमांक :  " + ds3.Tables[0].Rows[0]["VehicleNo"].ToString() + "</span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>  2. वाहन चालक का नाम : </span></td>");
                    sbP1.Append("<td style='font-size:15px;width:200px;'><span style='font-weight:500;'>मोबाइल नं. : </span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>  3. प्रदायित सामग्री का विवरण  </span></td>");
                    sbP1.Append("<td style='font-size:15px;width:200px;'><span style='font-weight:500;'>Tally D.M. No. : 4703  </span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("</table>");



                    sbP1.Append("<table class='table table1-bordered' style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<th style='text-align:center'>क्र.म.</th>");

                    sbP1.Append("<th style='text-align:center'>सामग्री का विवरण</th>");
                    sbP1.Append("<th style='text-align:center'>पैक साईज</th>");

                    sbP1.Append("<th style='text-align:center'>प्रदाय नग</th>");
                    sbP1.Append("<th style='text-align:center'>मात्रा (" + ds3.Tables[0].Rows[0]["UnitName"].ToString() + ")</th>");
                    sbP1.Append("<th style='text-align:center'>प्रदाय स्थान का विवरण</th>");
                    sbP1.Append("<th style='text-align:center'>उत्पादन दिनांक</th>");
                    sbP1.Append("<th style='text-align:center'>समाप्ति  दिनांक</th>");
                    sbP1.Append("</tr>");


                    for (int i = 0; i < Count; i++)
                    {
                        TotalofTotalSupplyQty += int.Parse(ds3.Tables[0].Rows[i]["TotalSupplyQty"].ToString());
                        TotalofTotalSupplyQtyInLTR += decimal.Parse(ds3.Tables[0].Rows[i]["SupplyTotalQtyINLTR"].ToString());
                        //if (ds4.Tables[0].Rows[i]["CarriageModeID"].ToString() == "1")
                        //{
                        //    TotalissueCrate += int.Parse(ds4.Tables[0].Rows[i]["IssueCrate"].ToString());
                        //}
                        sbP1.Append("<tr>");
                        sbP1.Append("<td>" + (i + 1) + "</td>");
                        sbP1.Append("<td>" + (ds3.Tables[0].Rows[i]["ItemName_Hindi"].ToString() == "" ? ds3.Tables[0].Rows[i]["ItemName"].ToString() : ds3.Tables[0].Rows[i]["ItemName_Hindi"].ToString()) + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["PackagingSize"].ToString() + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["TotalSupplyQty"].ToString() + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["SupplyTotalQtyINLTR"].ToString() + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + place[2] + "</td>");
                        sbP1.Append("<td style='text-align:center'></td>");
                        sbP1.Append("<td style='text-align:center'></td>");
                        sbP1.Append("</tr>");

                    }
                    sbP1.Append("<tr>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td><b>योग </b></td>");
                    sbP1.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQty.ToString() + "</b></td>");
                    sbP1.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQtyInLTR.ToString() + "</b></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");

                    sbP1.Append("<table  style='width:100%; '>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span >कृपया उपरोक्त सामग्री प्राप्त कर पावती दिलाने का कष्ट करें |</span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");
                    //sbP1.Append("<td style='font-size:17px;'></span></td>");

                    sbP1.Append("<table class='table table1-bordered' style='width:100%;'>");
                    sbP1.Append("<tr>");


                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर वाहन चालक</td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर विवरण  सहायक </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर भंडार सहायक </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर नोडल अधिकृत  अधिकारी</td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");


                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'></td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'> </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'> </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");
                    //sbP1.Append("</br>");
                    sbP1.Append("<hr>");
                    //sbP1.Append("</br>");
                    sbP1.Append("<table  style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;text-align:center;'><span style='font-weight:700;'><U>|| पावती ||</U></span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;उपरोक्तानुसार वाहन क्रमांक <b>" + ds3.Tables[0].Rows[0]["VehicleNo"].ToString() + "</b> से आज दिनांक <b>" + ds3.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b> समय <b>" + ds3.Tables[0].Rows[0]["VehicleOut_Time"].ToString() + "</b>  बजे पर सही स्थिति में प्राप्त किया गया है</span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");

                    sbP1.Append("<table  class='table table1-bordered' style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='text-align:left;height:120px;font-size:17px;width:23%'>वाहन चालक का नाम वहस्ताक्षर दिनांक सहित </td>");
                    sbP1.Append("<td style='text-align:left;height:120px;font-size:17px;width:23%'> </td>");
                    sbP1.Append("<td   style='text-align:left;height:120px;font-size:17px;width:54%'>परियोजना अधिकारी ,CDPO<br/>समन्वित  बाल विकास योजना <br/>महिला / बाल विकास विभाग <br/>सावेर <br/>जिला इंदौर (म. प्र.)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style='font-size:13px;'>हस्ताक्षर सील व  दिनांक सहित </span></td>");

                    sbP1.Append("</tr>");

                    sbP1.Append("</table>");
                    sbP1.Append("</div>");

                    string source = ViewState["MultiDemandId"].ToString();
                    string finalvalue = source.Contains(',').ToString();

                    if (finalvalue == "True")
                    {

                       
                        DataColumn colMPDemandId = tabledemand.Columns.Add("MilkOrProductDemandId", typeof(int));
                        string[] lines = source.Split(',');
                        foreach (var line in lines)
                        {
                            string[] split = line.Split(',');

                            DataRow row = tabledemand.NewRow();
                            row.SetField(colMPDemandId, int.Parse(split[0]));

                            tabledemand.Rows.Add(row);
                        }

                        // ViewState["MergeData"] = sb.ToString();
                        ViewState["MergeData"] = sbP1.ToString();

                        if (tabledemand.Rows.Count > 0)
                        {
                            int tmprow = tabledemand.Rows.Count;
                            for (int i = 0; i < tmprow; i++)
                            {
                                string dmid = tabledemand.Rows[i]["MilkOrProductDemandId"].ToString();
                                GetDemandWiseDMDetails(dmid);
                            }

                            Divview1.InnerHtml = ViewState["MergeData"].ToString();

                        }
                        if (tabledemand != null)
                        {
                            tabledemand.Dispose();
                            ViewState["MultiDemandId"] = "";
                            ViewState["MergeData"] = "";
                            ViewState["PStatus"] = "";
                        }

                    }
                    else
                    {
                        Divview1.InnerHtml = sbP1.ToString();
                        // Divview1.InnerHtml = sb.ToString();
                    }
                }


                //string source = ViewState["MultiDemandId"].ToString();
                //string finalvalue = source.Contains(',').ToString();

                //if (finalvalue == "True")
                //{

                //    DataTable tabledemand = new DataTable();
                //    DataColumn colMPDemandId = tabledemand.Columns.Add("MilkOrProductDemandId", typeof(int));
                //    string[] lines = source.Split(',');
                //    foreach (var line in lines)
                //    {
                //        string[] split = line.Split(',');

                //        DataRow row = tabledemand.NewRow();
                //        row.SetField(colMPDemandId, int.Parse(split[0]));

                //        tabledemand.Rows.Add(row);
                //    }

                //  // ViewState["MergeData"] = sb.ToString();
                //    ViewState["MergeData"] = sbP1.ToString();

                //    if (tabledemand.Rows.Count > 0)
                //    {
                //        int tmprow = tabledemand.Rows.Count;
                //        for (int i = 0; i < tmprow; i++)
                //        {
                //            string dmid = tabledemand.Rows[i]["MilkOrProductDemandId"].ToString();
                //            GetDemandWiseDMDetails(dmid);
                //        }

                //        Divview1.InnerHtml = ViewState["MergeData"].ToString();

                //    }
                //    if (tabledemand != null)
                //    {
                //        tabledemand.Dispose();
                //        ViewState["MultiDemandId"] = "";
                //        ViewState["MergeData"] = "";
                //        ViewState["PStatus"] = "";
                //    }

                //}
                //else
                //{
                //    Divview1.InnerHtml = sbP1.ToString();
                //   // Divview1.InnerHtml = sb.ToString();
                //}


                //ClientScriptManager CSM = Page.ClientScript;
                //string strScript = "<script>";
                //strScript += "window.print();";

                //strScript += "</script>";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

            }
        }
        catch (Exception ex)
        {
           // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 8 ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }

    }
    private void GetDemandWiseDMDetails(string dmid)
    {
        try
        {

            ds4 = objdb.ByProcedure("USP_Trn_ProductGatePass",
                     new string[] { "Flag", "MilkOrProductDemandId", "Office_ID" },
                     new string[] { "4", dmid, lblOffice_ID.Text }, "dataset");

            if (ds4.Tables[0].Rows.Count > 0)
            {

                int Count = ds4.Tables[0].Rows.Count;
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sbP1 = new StringBuilder();
                int TotalofTotalSupplyQty = 0;
                int TotalissueCrate = 0;
                decimal TotalofTotalSupplyQtyInLTR = 0;
                string OfficeName = ds4.Tables[0].Rows[0]["Office_Name"].ToString();
                string[] Dairyplant = OfficeName.Split(' ');


                DataTable tabledemand = new DataTable();
                if (ds4.Tables[0].Rows[0]["Priyojna_status"].ToString() != "1")
                {
                    if (ViewState["PStatus"].ToString() != "")
                    {
                        sb1.Append("<p style='page-break-after: always'>");
                    }
                    else
                    {
                        sb1.Append("<p style='page-break-after: always'>");
                    }
                    sb1.Append("<div class='invoice'>");
                    sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                    sb1.Append("<tr>");
                    sb1.Append("<td rowspan='3'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:70px;'/></td>");
                    sb1.Append("<td style='font-size:21px;'><b>" + ds4.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></td>");
                    sb1.Append("<td></td>");
                    sb1.Append("</tr>");
                    sb1.Append("<tr>");
                    sb1.Append("<td style='font-size:17px;'><b>" + Dairyplant[0] + " डेरी प्लांट</b></td>");
                    sb1.Append("<td style='width:70px;'></td>");
                    sb1.Append("</tr>");
                    sb1.Append("<tr></tr>");
                    sb1.Append("<tr>");
                    sb1.Append("<td style='text-align:left'><span style='font-weight:700;'>" + "" + "</span></td>");
                    sb1.Append("<td style='font-size:17px;'><b>वाहन वितरण चालान</b></td>");
                    sb1.Append("<td style='width:70px;'></td>");
                    sb1.Append("</tr>");
                    sb1.Append("</table>");
                    sb1.Append("<table class='table table1-bordered' style='width:100%; '>");
                    sb1.Append("<tr>");
                    if (objdb.Office_ID() == "6")
                    {
                        sb1.Append("<td>" + (ds4.Tables[0].Rows[0]["BCName"].ToString() + "</br>(" + ds4.Tables[0].Rows[0]["SSName"].ToString() + ")") + "</td>");
                    }
                    else
                    {
                        sb1.Append("<td>" + (ds4.Tables[0].Rows[0]["DName"].ToString() == ds4.Tables[0].Rows[0]["SSName"].ToString() ? ds4.Tables[0].Rows[0]["DName"].ToString() : ds4.Tables[0].Rows[0]["DName"].ToString() + "(" + ds4.Tables[0].Rows[0]["SSName"].ToString() + ")") + "</td>");
                    }

                    sb1.Append("<td style='text-align:center' ><b>डी.एम.नं . :" + ds4.Tables[0].Rows[0]["OrderId"].ToString()
                        + "</b></br><b>गेट पास नं .&nbsp;&nbsp;:<span >" + (ds4.Tables[0].Rows[0]["ItemCat_id"].ToString() == objdb.GetMilkCatId() ? ds4.Tables[0].Rows[0]["VDChallanNo"].ToString() : ds4.Tables[0].Rows[0]["GatePassNo"].ToString())
                        + "</b></br>दिनांक&nbsp;&nbsp;:<span >" + ds4.Tables[0].Rows[0]["Delivary_Date"].ToString()
                            + "</br>समय&nbsp;&nbsp;:<span >" + ds4.Tables[0].Rows[0]["DemandTime"].ToString()
                            + "</br>रूट&nbsp;&nbsp;:<span >" + ds4.Tables[0].Rows[0]["RName"].ToString()
                            + "</br>शिफ्ट&nbsp;&nbsp;:<span >" + ds4.Tables[0].Rows[0]["ShiftName"].ToString() + "</td>");

                    sb1.Append("</tr>");
                    sb1.Append("<tr>");
                    sb1.Append("<td  colspan='2'>वाहन क्रं&nbsp;&nbsp;<span>" + ds4.Tables[0].Rows[0]["VehicleNo"].ToString() + "</span></td>");
                    sb1.Append("</tr>");
                    sb1.Append("</table>");

                    sb1.Append("<table class='table table1-bordered' style='width:100%; margin-top:-20px'>");
                    sb1.Append("<tr>");
                    sb1.Append("<th style='text-align:center'>क्र.म.</th>");
                    if (lblIC.Text == objdb.GetMilkCatId())
                    {
                        sb1.Append("<th style='text-align:center'>दूध का नाम</th>");
                    }
                    else
                    {
                        sb1.Append("<th style='text-align:center'>उत्पाद प्रकार</th>");
                    }

                    sb1.Append("<th style='text-align:center'>मात्रा</th>");
                    sb1.Append("<th style='text-align:center'>क्रैट की संख्या</th>");
                    sb1.Append("<th style='text-align:center'>अन्य</th>");
                    sb1.Append("<th style='text-align:center'>केन/बॉक्स/जार</th>");
                    sb1.Append("</tr>");
                    //int TotalofTotalSupplyQty = 0;
                    //int TotalissueCrate = 0;

                    for (int i = 0; i < Count; i++)
                    {
                        TotalofTotalSupplyQty += int.Parse(ds4.Tables[0].Rows[i]["SupplyTotalQty"].ToString());
                        if (ds4.Tables[0].Rows[i]["CarriageModeID"].ToString() == "1")
                        {
                            TotalissueCrate += int.Parse(ds4.Tables[0].Rows[i]["IssueCrate"].ToString());
                        }

                        sb1.Append("<tr>");
                        sb1.Append("<td>" + (i + 1) + "</td>");
                        sb1.Append("<td>" + (ds4.Tables[0].Rows[i]["ItemName_Hindi"].ToString() == "" ? ds4.Tables[0].Rows[i]["ItemName"].ToString() : ds4.Tables[0].Rows[i]["ItemName_Hindi"].ToString()) + "</td>");
                        sb1.Append("<td style='text-align:center'>" + ds4.Tables[0].Rows[i]["SupplyTotalQty"].ToString() + "</td>");
                        sb1.Append("<td style='text-align:center'>" + (ds4.Tables[0].Rows[i]["CarriageModeID"].ToString() == "1" ? ds4.Tables[0].Rows[i]["IssueCrate"].ToString() : "0") + "</td>");
                        sb1.Append("<td style='text-align:center'>" + ds4.Tables[0].Rows[i]["ExtraPacket"].ToString() + "</td>");
                        sb1.Append("<td style='text-align:center'>" + (ds4.Tables[0].Rows[i]["CarriageModeID"].ToString() == "2" ? ds4.Tables[0].Rows[i]["Box"].ToString() : "0") + "</td>");
                        sb1.Append("</tr>");

                    }
                    sb1.Append("<tr>");
                    sb1.Append("<td></td>");
                    sb1.Append("<td><b>टोटल</b></td>");
                    sb1.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQty.ToString() + "</b></td>");
                    sb1.Append("<td style='text-align:center'><b>" + TotalissueCrate.ToString() + "</b></td>");
                    sb1.Append("<td></td>");
                    sb1.Append("<td></td>");
                    sb1.Append("</tr>");
                    sb1.Append("</table>");
                    sb1.Append("<table class='table1' style='width:100%; height:100%'>");
                    sb1.Append("<tr>");
                    sb1.Append("<td colspan='3' <span style='text-align:right; padding-top:20px; font-weight:700;'>हस्ताक्षर विपणन प्रभारी</br></span></td>");
                    sb1.Append("</tr>");
                    sb1.Append("</table>");
                    sb1.Append("</div>");


                    ViewState["MergeData"] += sb1.ToString();
                }
                else if (ds4.Tables[0].Rows[0]["Priyojna_status"].ToString() == "1")
                {
                    string[] place = ds4.Tables[0].Rows[0]["SSName"].ToString().Split(' ');
                    //for priyojna adhikari
                    if (ViewState["PStatus"].ToString() != "")
                    {
                        sbP1.Append("<p style='page-break-after: always'>");
                    }
                    else
                    {
                        sbP1.Append("<p style='page-break-after: always'>");
                    }
                    sbP1.Append("<div class='invoice'>");
                    sbP1.Append("<table class='table1' style='width:100%; height:100%'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td rowspan='3'><img src='/mis/image/bdsnew_blacklogo.png' style='width:70px; height:50px;'/></td>");
                    sbP1.Append("<td style='font-size:21px;'><b>" + ds4.Tables[0].Rows[0]["Office_Name"].ToString() + "</b></td>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");

                    sbP1.Append("<td style='font-size:18px;'><b>" + ds4.Tables[0].Rows[0]["Seller_Address"].ToString() + "</b></td>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");

                    sbP1.Append("<td style='font-size:18px;'><b>GSTIN :" + ds4.Tables[0].Rows[0]["Office_Gst"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;State Code : 0 - MP</b></td>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr></tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='width:70px;'></td>");
                    sbP1.Append("<td style='font-size:18px;'><span style='font-weight:700;'><U>|| गेटपास - सह पावती ||</U></span></td>");
                    sbP1.Append("<td style='width:70px;'></td>");

                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");
                    sbP1.Append("<table  style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;'>प्रति,</span></td>");
                    sbP1.Append("<td style='font-size:17px;width:200px;'><span style='font-weight:700;'>दिनांक : " + ds4.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</span></td>");
                    //sbP1.Append("<td style='font-size:17px;'></span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   परियोजना अधिकारी ,CDPO </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   समन्वित  बाल विकास योजना  </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   महिला / बाल विकास विभाग  </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'><b>   सावेर  </b></span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span style='font-weight:700;padding-left:50px'>   जिला इंदौर (म. प्र.) </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>   महोदय , </span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;' colspan='2'><span style='font-weight:500;padding-left:60px'>समन्वित बाल विकास योजना के अंतर्गत निम्न विवरण अनुसार सामग्री प्रदाय की जा रही है - </span></td><br/>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>  1. वाहन क्रमांक :  " + ds4.Tables[0].Rows[1]["VehicleNo"].ToString() + "</span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>  2. वाहन चालक का नाम : </span></td>");
                    sbP1.Append("<td style='font-size:15px;width:200px;'><span style='font-weight:500;'>मोबाइल नं. : </span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span style='font-weight:500;'>  3. प्रदायित सामग्री का विवरण  </span></td>");
                    sbP1.Append("<td style='font-size:15px;width:200px;'><span style='font-weight:500;'>Tally D.M. No. : 4703  </span></td>");
                    sbP1.Append("</tr>");

                    sbP1.Append("</table>");


                    sbP1.Append("<table class='table table1-bordered' style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<th style='text-align:center'>क्र.म.</th>");

                    sbP1.Append("<th style='text-align:center'>सामग्री का विवरण</th>");
                    sbP1.Append("<th style='text-align:center'>पैक साईज</th>");

                    sbP1.Append("<th style='text-align:center'>प्रदाय नग</th>");
                    sbP1.Append("<th style='text-align:center'>मात्रा" + ds4.Tables[0].Rows[0]["UnitName"].ToString() + "</th>");
                    sbP1.Append("<th style='text-align:center'>प्रदाय स्थान का विवरण</th>");
                    sbP1.Append("<th style='text-align:center'>उत्पादन दिनांक</th>");
                    sbP1.Append("<th style='text-align:center'>समाप्ति  दिनांक</th>");
                    sbP1.Append("</tr>");


                    for (int i = 0; i < Count; i++)
                    {
                        TotalofTotalSupplyQty += int.Parse(ds4.Tables[0].Rows[i]["SupplyTotalQty"].ToString());
                        TotalofTotalSupplyQtyInLTR += decimal.Parse(ds4.Tables[0].Rows[i]["SupplyTotalQtyINLTR"].ToString());
                        //if (ds4.Tables[0].Rows[i]["CarriageModeID"].ToString() == "1")
                        //{
                        //    TotalissueCrate += int.Parse(ds4.Tables[0].Rows[i]["IssueCrate"].ToString());
                        //}
                        sbP1.Append("<tr>");
                        sbP1.Append("<td>" + (i + 1) + "</td>");
                        sbP1.Append("<td>" + (ds4.Tables[0].Rows[i]["ItemName_Hindi"].ToString() == "" ? ds4.Tables[0].Rows[i]["ItemName"].ToString() : ds4.Tables[0].Rows[i]["ItemName_Hindi"].ToString()) + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["PackagingSize"].ToString() + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["SupplyTotalQty"].ToString() + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + ds3.Tables[0].Rows[i]["SupplyTotalQtyINLTR"].ToString() + "</td>");
                        sbP1.Append("<td style='text-align:center'>" + place[2] + "</td>");
                        sbP1.Append("<td style='text-align:center'></td>");
                        sbP1.Append("<td style='text-align:center'></td>");
                        sbP1.Append("</tr>");

                    }
                    sbP1.Append("<tr>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td><b>योग </b></td>");
                    sbP1.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQty.ToString() + "</b></td>");
                    sbP1.Append("<td style='text-align:center'><b>" + TotalofTotalSupplyQtyInLTR.ToString() + "</b></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("<td></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");

                    sbP1.Append("<table  style='width:100%; '>");

                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:15px;'><span >कृपया उपरोक्त सामग्री प्राप्त कर पावती दिलाने का कष्ट करें |</span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");
                    //sbP1.Append("<td style='font-size:17px;'></span></td>");

                    sbP1.Append("<table class='table table1-bordered' style='width:100%;'>");
                    sbP1.Append("<tr>");


                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर वाहन चालक</td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर विवरण  सहायक </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर भंडार सहायक </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'>हस्ताक्षर नोडल अधिकृत  अधिकारी</td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");


                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'></td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'> </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'> </td>");
                    sbP1.Append("<td style='text-align:center;height:50px;font-size:17px;width:25%'></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");
                    //sbP1.Append("</br>");
                    sbP1.Append("<hr>");
                    //sbP1.Append("</br>");
                    sbP1.Append("<table  style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;text-align:center;'><span style='font-weight:700;'><U>|| पावती ||</U></span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='font-size:17px;'><span >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;उपरोक्तानुसार वाहन क्रमांक <b>" + ds4.Tables[0].Rows[1]["VehicleNo"].ToString() + "</b> से आज दिनांक <b>" + ds4.Tables[0].Rows[0]["Delivary_Date"].ToString() + "</b> समय <b>" + ds4.Tables[0].Rows[0]["VehicleOut_Time"].ToString() + "</b>  बजे पर सही स्थिति में प्राप्त किया गया है</span></td>");
                    sbP1.Append("</tr>");
                    sbP1.Append("</table>");

                    sbP1.Append("<table  class='table table1-bordered' style='width:100%;'>");
                    sbP1.Append("<tr>");
                    sbP1.Append("<td style='text-align:left;height:120px;font-size:17px;width:23%'>वाहन चालक का नाम वहस्ताक्षर दिनांक सहित </td>");
                    sbP1.Append("<td style='text-align:left;height:120px;font-size:17px;width:23%'> </td>");
                    sbP1.Append("<td   style='text-align:left;height:120px;font-size:17px;width:54%'>परियोजना अधिकारी ,CDPO<br/>समन्वित  बाल विकास योजना <br/>महिला / बाल विकास विभाग <br/>सावेर <br/>जिला इंदौर (म. प्र.)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style='font-size:13px;'>हस्ताक्षर सील व  दिनांक सहित </span></td>");

                    sbP1.Append("</tr>");

                    sbP1.Append("</table>");
                    sbP1.Append("</div>");

                    ViewState["MergeData"] += sbP1.ToString();
                }


                //ViewState["MergeData"] += sb1.ToString();
                // ViewState["MergeData"] += sbP1.ToString();
                ViewState["PStatus"] = "1";
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 8 ", ex.Message.ToString());
        }
        finally
        {
            if (ds4 != null) { ds4.Dispose(); }
        }

    }
}