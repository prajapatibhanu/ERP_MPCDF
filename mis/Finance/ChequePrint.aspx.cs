﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using System.Drawing;
using System.IO;


public partial class mis_Finance_ChequePrint : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds;
    CultureInfo cult = new CultureInfo("gu-IN", true);
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //lblMsg.Text = "";
            if (Request.QueryString["ChequeTx_ID"] != null && Request.QueryString["Type"] != null)
            {

                if (!IsPostBack)
                {
                    if (Request.QueryString["ChequeTx_ID"] != null)
                    {
                        ViewState["ChequeTx_ID"] = objdb.Decrypt(Request.QueryString["ChequeTx_ID"].ToString());
                        ViewState["Type"] = objdb.Decrypt(Request.QueryString["Type"].ToString());
                        FillPrint();

                    }

                }
            }

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillPrint()
    {
        try
        {
            ds = objdb.ByProcedure("spFinBankChequePrint", new string[] { "flag", "ChequeTx_ID" }, new string[] { "2", ViewState["ChequeTx_ID"].ToString() }, "dataset");
            if (ds != null)
            {

                if (ds.Tables.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();

                    string AcPayee = ds.Tables[0].Rows[0]["AcPayee"].ToString();
                    string ChequeTx_DateF = ds.Tables[0].Rows[0]["ChequeTx_DateF"].ToString();
                    string FavouringName = ds.Tables[0].Rows[0]["FavouringName"].ToString();
                    string ChequeTx_Amount = ds.Tables[0].Rows[0]["ChequeTx_Amount"].ToString();
                    if (ViewState["Type"].ToString() == "PNB")
                    {
                        #region PNB
                        sb.Append(" <style>");
                        sb.Append(" body {  ");
                        sb.Append(" margin: 0mm 0mm 0mm 0mm;");
                        sb.Append(" height: 3.67in; ");
                        sb.Append(" width: 8.00in; ");
                        sb.Append(" } ");
                        sb.Append(" page { ");
                        sb.Append(" height: 3.67in; ");
                        sb.Append(" width:8.00in; ");
                        sb.Append(" } ");
                        sb.Append(" .divcss { ");
                        sb.Append(" position:fixed;");
                        sb.Append("  left:0; ");
                        sb.Append("  top:0;  ");
                        sb.Append("  height: 3.67in;  ");
                        sb.Append("  width: 8.00in;  ");
                        sb.Append("  } .amtcss { ");
                        sb.Append("  position: fixed; ");
                        sb.Append("  top: 1.4in; ");
                        sb.Append("  left: 6.2in; ");
                        sb.Append("  }");
                        sb.Append("  .partycss { ");
                        sb.Append("  position: fixed; ");
                        sb.Append("  top: 0.85in; ");
                        sb.Append("  left: 0.8in; ");
                        sb.Append("  font-size:13px; ");
                        sb.Append(" } ");
                        if (ds.Tables[0].Rows[0]["AcPayee"].ToString() == "Yes")
                        {
                            sb.Append(" .Linecss{ ");
                            sb.Append("  position: fixed; ");
                            sb.Append("  top: 0.3in; ");
                            sb.Append("  left: 0in; ");
                            sb.Append("  width:120px; ");
                            sb.Append("  height:3px; ");
                            sb.Append("  border-top:1px solid black; ");
                            sb.Append("  border-bottom:1px solid black; ");
                            sb.Append("  transform: rotate(150deg); ");
                            sb.Append("  } ");
                        }

                        sb.Append(" .Datecss{ ");
                        sb.Append("  position: fixed; ");
                        sb.Append("  top: 0.32in; ");
                        sb.Append("  left:  6.12in;  ");
                        sb.Append("  letter-spacing: 11px; ");
                        sb.Append(" } ");

                        sb.Append(" </style> ");
                        sb.Append("  <div class='divcss'>");
                        if (AcPayee == "Yes")
                        {
                            sb.Append("  <div class='Linecss'></div>");
                        }

                        sb.Append("<div class='Datecss'>" + ChequeTx_DateF + "</div>");
                        sb.Append(" <span style='padding-left: 6.19in; padding-top: 0.31in; padding-bottom: 0.33in; display: block;'>&nbsp;</span>");

                        //sb.Append("  <span style='padding-left: 6.19in; padding-top: 0.31in; padding-bottom: 0.33in; display: block;'>2&nbsp;&nbsp;&nbsp;9&nbsp;-&nbsp;0&nbsp;&nbsp;7&nbsp;-&nbsp;2&nbsp;&nbsp;&nbsp;0&nbsp;&nbsp;&nbsp;2&nbsp;&nbsp;&nbsp;2</span>");
                        sb.Append("  <span style='padding-left: 0.8in; padding-right: 2.8in; display: block; font-size: 13px; line-height: 2.5; '>&nbsp;</span>");
                        sb.Append("  <span class='partycss'>" + FavouringName + "</span>");
                        sb.Append("  <span style='padding-left: 0.5in; padding-right: 0.29in; display: block; font-size: 13px; line-height: 2;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + GenerateWordsinRs(ChequeTx_Amount) + " </span>");
                        sb.Append("  <span class='amtcss'>&nbsp;&nbsp;" + ChequeTx_Amount + "</span>");
                        sb.Append("  </div>");

                        divCheque.InnerHtml = sb.ToString();
                        // spnAmount.InnerHtml = GenerateWordsinRs(lblNetAmountdue.Text.ToString());
                        #endregion

                    }
                    else if (ViewState["Type"].ToString() == "SBI")
                    {
                        #region SBI
                        sb.Append(" <style>");
                        sb.Append(" body {  ");
                        sb.Append(" margin: 0mm 0mm 0mm 0mm;");
                        sb.Append(" height: 3.67in; ");
                        sb.Append(" width: 8.00in; ");
                        sb.Append(" } ");
                        sb.Append(" page { ");
                        sb.Append(" height: 3.67in; ");
                        sb.Append(" width:8.00in; ");
                        sb.Append(" } ");
                        sb.Append(" .divcss { ");
                        sb.Append(" position:fixed;");
                        sb.Append("  left:0; ");
                        sb.Append("  top:0;  ");
                        sb.Append("  height: 3.67in;  ");
                        sb.Append("  width: 8.00in;  ");
                        sb.Append("  } .amtcss { ");
                        sb.Append("  position: fixed; ");
                        sb.Append("  top: 1.45in; ");
                        //sb.Append("  left: 6.1in; ");
						sb.Append("  left: 6.2in; ");
                        sb.Append("  }");
                        sb.Append("  .partycss { ");
                        sb.Append("  position: fixed; ");
                        sb.Append("  top: 0.83in; ");
                        sb.Append("  left: 0.8in; ");
                        sb.Append("  font-size:13px; ");
                        sb.Append(" } ");
                        if (ds.Tables[0].Rows[0]["AcPayee"].ToString() == "Yes")
                        {
                            sb.Append(" .Linecss{ ");
                            sb.Append("  position: fixed; ");
                            sb.Append("  top: 0.3in; ");
                            sb.Append("  left: 0in; ");
                            sb.Append("  width:120px; ");
                            sb.Append("  height:3px; ");
                            sb.Append("  border-top:1px solid black; ");
                            sb.Append("  border-bottom:1px solid black; ");
                            sb.Append("  transform: rotate(150deg); ");
                            sb.Append("  } ");
                        }

                        sb.Append(" .Datecss{ ");
                        sb.Append("  position: fixed; ");
                        //sb.Append("  top: 0.33in; ");
                       // sb.Append("  left:  6.1in;  ");
						sb.Append("  top: 0.28in; ");
                        sb.Append("  left:  6.2in;  ");
                        sb.Append("  letter-spacing: 11px; ");
                        sb.Append(" } ");

                        sb.Append(" </style> ");
                        sb.Append("  <div class='divcss'>");
                        if (AcPayee == "Yes")
                        {
                            sb.Append("  <div class='Linecss'></div>");
                        }

                        sb.Append("<div class='Datecss'>" + ChequeTx_DateF + "</div>");
                        sb.Append(" <span style='padding-left: 6.19in; padding-top: 0.31in; padding-bottom: 0.22in; display: block;'>&nbsp;</span>");

                        //sb.Append("  <span style='padding-left: 6.19in; padding-top: 0.31in; padding-bottom: 0.33in; display: block;'>2&nbsp;&nbsp;&nbsp;9&nbsp;-&nbsp;0&nbsp;&nbsp;7&nbsp;-&nbsp;2&nbsp;&nbsp;&nbsp;0&nbsp;&nbsp;&nbsp;2&nbsp;&nbsp;&nbsp;2</span>");
                        sb.Append("  <span style='padding-left: 0.8in; padding-right: 2.8in; display: block; font-size: 13px; line-height: 2.5; '>&nbsp;</span>");
                        sb.Append("  <span class='partycss'>" + FavouringName + "</span>");
                        sb.Append("  <span style='padding-left: 0.5in; padding-right: 0.29in; display: block; font-size: 13px; line-height: 2;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + GenerateWordsinRs(ChequeTx_Amount) + " </span>");
                        sb.Append("  <span class='amtcss'>&nbsp;&nbsp;" + ChequeTx_Amount + "</span>");
                        sb.Append("  </div>");

                        divCheque.InnerHtml = sb.ToString();
                        // spnAmount.InnerHtml = GenerateWordsinRs(lblNetAmountdue.Text.ToString());

                        #endregion
                    }
                    else if (ViewState["Type"].ToString() == "AXIS")
                    {
                        #region AXIS
                        sb.Append(" <style>");
                        sb.Append(" body {  ");
                        sb.Append(" margin: 0mm 0mm 0mm 0mm;");
                        sb.Append(" height: 3.67in; ");
                        sb.Append(" width: 8.00in; ");
                        sb.Append(" } ");
                        sb.Append(" page { ");
                        sb.Append(" height: 3.67in; ");
                        sb.Append(" width:8.00in; ");
                        sb.Append(" } ");
                        sb.Append(" .divcss { ");
                        sb.Append(" position:fixed;");
                        sb.Append("  left:0; ");
                        sb.Append("  top:0;  ");
                        sb.Append("  height: 3.67in;  ");
                        sb.Append("  width: 8.00in;  ");
                        sb.Append("  } .amtcss { ");
                        sb.Append("  position: fixed; ");
                       // sb.Append("  top: 1.4in; ");
					    sb.Append("  top: 1.46in; ");
                        //sb.Append("  left: 6.18in; ");
						sb.Append("  left: 6.35in; ");
                        sb.Append("  }");
                        sb.Append("  .partycss { ");
                        sb.Append("  position: fixed; ");
                        sb.Append("  top: 0.85in; ");
                        sb.Append("  left: 0.9in; ");
                        sb.Append("  font-size:13px; ");
                        sb.Append(" } ");
                        if (ds.Tables[0].Rows[0]["AcPayee"].ToString() == "Yes")
                        {
                            sb.Append(" .Linecss{ ");
                            sb.Append("  position: fixed; ");
                            sb.Append("  top: 0.3in; ");
                            sb.Append("  left: 0in; ");
                            sb.Append("  width:120px; ");
                            sb.Append("  height:3px; ");
                            sb.Append("  border-top:1px solid black; ");
                            sb.Append("  border-bottom:1px solid black; ");
                            sb.Append("  transform: rotate(150deg); ");
                            sb.Append("  } ");
                        }

                        sb.Append(" .Datecss{ ");
                        sb.Append("  position: fixed; ");
						sb.Append("  top: 0.33in; ");
                       // sb.Append("  top: 0.30in; ");
                        //sb.Append("  left:  6.15in;  ");
                       // sb.Append("  letter-spacing: 11px; ");
						// sb.Append("  left:  6.19in;  ");
						 sb.Append("  left:  6.06in;  ");
                        sb.Append("  letter-spacing: 13px; ");
                        sb.Append(" } ");

                        sb.Append(" </style> ");
                        sb.Append("  <div class='divcss'>");
                        if (AcPayee == "Yes")
                        {
                            sb.Append("  <div class='Linecss'></div>");
                        }

                        sb.Append("<div class='Datecss'>" + ChequeTx_DateF + "</div>");
                        sb.Append(" <span style='padding-left: 6.19in; padding-top: 0.31in; padding-bottom: 0.33in; display: block;'>&nbsp;</span>");

                        //sb.Append("  <span style='padding-left: 6.19in; padding-top: 0.31in; padding-bottom: 0.33in; display: block;'>2&nbsp;&nbsp;&nbsp;9&nbsp;-&nbsp;0&nbsp;&nbsp;7&nbsp;-&nbsp;2&nbsp;&nbsp;&nbsp;0&nbsp;&nbsp;&nbsp;2&nbsp;&nbsp;&nbsp;2</span>");
                        sb.Append("  <span style='padding-left: 0.8in; padding-right: 2.8in; display: block; font-size: 13px; line-height: 2.5; '>&nbsp;</span>");
                        sb.Append("  <span class='partycss'>" + FavouringName + "</span>");
                        sb.Append("  <span style='padding-left: 0.5in; padding-right: 0.29in; display: block; font-size: 13px; line-height: 2;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + GenerateWordsinRs(ChequeTx_Amount) + " </span>");
                        sb.Append("  <span class='amtcss'>&nbsp;&nbsp;" + ChequeTx_Amount + "</span>");
                        sb.Append("  </div>");

                        divCheque.InnerHtml = sb.ToString();
                        // spnAmount.InnerHtml = GenerateWordsinRs(lblNetAmountdue.Text.ToString());
                        #endregion
                    }
                    else if (ViewState["Type"].ToString() == "BHOPAL")
                    {
                        #region BHOPAL
                        sb.Append(" <style>");
                        sb.Append(" body {  ");
                        sb.Append(" margin: 0mm 0mm 0mm 0mm;");
                        sb.Append(" height: 3.67in; ");
                        sb.Append(" width: 8.00in; ");
                        sb.Append(" } ");
                        sb.Append(" page { ");
                        sb.Append(" height: 3.67in; ");
                        sb.Append(" width:8.00in; ");
                        sb.Append(" } ");
                        sb.Append(" .divcss { ");
                        sb.Append(" position:fixed;");
                        sb.Append("  left:0; ");
                        sb.Append("  top:0;  ");
                        sb.Append("  height: 3.67in;  ");
                        sb.Append("  width: 8.00in;  ");
                        sb.Append("  } .amtcss { ");
                        sb.Append("  position: fixed; ");
                        sb.Append("  top: 1.4in; ");
                        sb.Append("  left: 6.10in; ");
                        sb.Append("  }");
                        sb.Append("  .partycss { ");
                        sb.Append("  position: fixed; ");
                        sb.Append("  top: 0.85in; ");
                        sb.Append("  left: 0.7in; ");
                        sb.Append("  font-size:13px; ");
                        sb.Append(" } ");
                        if (ds.Tables[0].Rows[0]["AcPayee"].ToString() == "Yes")
                        {
                            sb.Append(" .Linecss{ ");
                            sb.Append("  position: fixed; ");
                            sb.Append("  top: 0.3in; ");
                            sb.Append("  left: 0in; ");
                            sb.Append("  width:120px; ");
                            sb.Append("  height:3px; ");
                            sb.Append("  border-top:1px solid black; ");
                            sb.Append("  border-bottom:1px solid black; ");
                            sb.Append("  transform: rotate(150deg); ");
                            sb.Append("  } ");
                        }

                        sb.Append(" .Datecss{ ");
                        sb.Append("  position: fixed; ");
                        sb.Append("  top: 0.38in; ");
                        sb.Append("  left:  6.05in;  ");
                        sb.Append("  letter-spacing: 11px; ");
                        sb.Append(" } ");

                        sb.Append(" </style> ");
                        sb.Append("  <div class='divcss'>");
                        if (AcPayee == "Yes")
                        {
                            sb.Append("  <div class='Linecss'></div>");
                        }

                        sb.Append("<div class='Datecss'>" + ChequeTx_DateF + "</div>");
                        sb.Append(" <span style='padding-left: 6.19in; padding-top: 0.31in; padding-bottom: 0.33in; display: block;'>&nbsp;</span>");

                        //sb.Append("  <span style='padding-left: 6.19in; padding-top: 0.31in; padding-bottom: 0.33in; display: block;'>2&nbsp;&nbsp;&nbsp;9&nbsp;-&nbsp;0&nbsp;&nbsp;7&nbsp;-&nbsp;2&nbsp;&nbsp;&nbsp;0&nbsp;&nbsp;&nbsp;2&nbsp;&nbsp;&nbsp;2</span>");
                        sb.Append("  <span style='padding-left: 0.8in; padding-right: 2.8in; display: block; font-size: 13px; line-height: 2.4; '>&nbsp;</span>");
                        sb.Append("  <span class='partycss'>" + FavouringName + "</span>");
                        sb.Append("  <span style='padding-left: 0.5in; padding-right: 0.29in; display: block; font-size: 13px; line-height: 2;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + GenerateWordsinRs(ChequeTx_Amount) + " </span>");
                        sb.Append("  <span class='amtcss'>&nbsp;&nbsp;" + ChequeTx_Amount + "</span>");
                        sb.Append("  </div>");

                        divCheque.InnerHtml = sb.ToString();
                        // spnAmount.InnerHtml = GenerateWordsinRs(lblNetAmountdue.Text.ToString());
                        #endregion
                    }
                    else if (ViewState["Type"].ToString() == "HDFC")
                    {
                        #region HDFC
                        sb.Append(" <style>");
                        sb.Append(" body {  ");
                        sb.Append(" margin: 0mm 0mm 0mm 0mm;");
                        sb.Append(" height: 3.67in; ");
                        sb.Append(" width: 8.00in; ");
                        sb.Append(" } ");
                        sb.Append(" page { ");
                        sb.Append(" height: 3.67in; ");
                        sb.Append(" width:8.00in; ");
                        sb.Append(" } ");
                        sb.Append(" .divcss { ");
                        sb.Append(" position:fixed;");
                        sb.Append("  left:0; ");
                        sb.Append("  top:0;  ");
                        sb.Append("  height: 3.67in;  ");
                        sb.Append("  width: 8.00in;  ");
                        sb.Append("  } .amtcss { ");
                        sb.Append("  position: fixed; ");
                        sb.Append("  top: 1.4in; ");
                        sb.Append("  left: 6.2in; ");
                        sb.Append("  }");
                        sb.Append("  .partycss { ");
                        sb.Append("  position: fixed; ");
                        sb.Append("  top: 0.85in; ");
                        sb.Append("  left: 0.8in; ");
                        sb.Append("  font-size:13px; ");
                        sb.Append(" } ");
                        if (ds.Tables[0].Rows[0]["AcPayee"].ToString() == "Yes")
                        {
                            sb.Append(" .Linecss{ ");
                            sb.Append("  position: fixed; ");
                            sb.Append("  top: 0.3in; ");
                            sb.Append("  left: 0in; ");
                            sb.Append("  width:120px; ");
                            sb.Append("  height:3px; ");
                            sb.Append("  border-top:1px solid black; ");
                            sb.Append("  border-bottom:1px solid black; ");
                            sb.Append("  transform: rotate(150deg); ");
                            sb.Append("  } ");
                        }

                        sb.Append(" .Datecss{ ");
                        sb.Append("  position: fixed; ");
                        sb.Append("  top: 0.32in; ");
                        sb.Append("  left:  6.14in;  ");
                        sb.Append("  letter-spacing: 11px; ");
                        sb.Append(" } ");

                        sb.Append(" </style> ");
                        sb.Append("  <div class='divcss'>");
                        if (AcPayee == "Yes")
                        {
                            sb.Append("  <div class='Linecss'></div>");
                        }

                        sb.Append("<div class='Datecss'>" + ChequeTx_DateF + "</div>");
                        sb.Append(" <span style='padding-left: 6.19in; padding-top: 0.31in; padding-bottom: 0.33in; display: block;'>&nbsp;</span>");

                        //sb.Append("  <span style='padding-left: 6.19in; padding-top: 0.31in; padding-bottom: 0.33in; display: block;'>2&nbsp;&nbsp;&nbsp;9&nbsp;-&nbsp;0&nbsp;&nbsp;7&nbsp;-&nbsp;2&nbsp;&nbsp;&nbsp;0&nbsp;&nbsp;&nbsp;2&nbsp;&nbsp;&nbsp;2</span>");
                        sb.Append("  <span style='padding-left: 0.8in; padding-right: 2.8in; display: block; font-size: 13px; line-height: 2.3; '>&nbsp;</span>");
                        sb.Append("  <span class='partycss'>" + FavouringName + "</span>");
                        sb.Append("  <span style='padding-left: 0.5in; padding-right: 0.29in; display: block; font-size: 13px; line-height: 2;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + GenerateWordsinRs(ChequeTx_Amount) + " </span>");
                        sb.Append("  <span class='amtcss'>&nbsp;&nbsp;" + ChequeTx_Amount + "</span>");
                        sb.Append("  </div>");

                        divCheque.InnerHtml = sb.ToString();
                        // spnAmount.InnerHtml = GenerateWordsinRs(lblNetAmountdue.Text.ToString());
                        #endregion
                    }
                    else if (ViewState["Type"].ToString() == "KOTAK")
                    {
                        #region KOTAK
                        sb.Append(" <style>");
                        sb.Append(" body {  ");
                        sb.Append(" margin: 0mm 0mm 0mm 0mm;");
                        sb.Append(" height: 3.67in; ");
                        sb.Append(" width: 8.00in; ");
                        sb.Append(" } ");
                        sb.Append(" page { ");
                        sb.Append(" height: 3.67in; ");
                        sb.Append(" width:8.00in; ");
                        sb.Append(" } ");
                        sb.Append(" .divcss { ");
                        sb.Append(" position:fixed;");
                        sb.Append("  left:0; ");
                        sb.Append("  top:0;  ");
                        sb.Append("  height: 3.67in;  ");
                        sb.Append("  width: 8.00in;  ");
                        sb.Append("  } .amtcss { ");
                        sb.Append("  position: fixed; ");
                        sb.Append("  top: 1.4in; ");
                        sb.Append("  left: 6.1in; ");
                        sb.Append("  }");
                        sb.Append("  .partycss { ");
                        sb.Append("  position: fixed; ");
                        sb.Append("  top: 0.8in; ");
                        sb.Append("  left: 0.75in; ");
                        sb.Append("  font-size:13px; ");
                        sb.Append(" } ");
                        if (ds.Tables[0].Rows[0]["AcPayee"].ToString() == "Yes")
                        {
                            sb.Append(" .Linecss{ ");
                            sb.Append("  position: fixed; ");
                            sb.Append("  top: 0.3in; ");
                            sb.Append("  left: 0in; ");
                            sb.Append("  width:120px; ");
                            sb.Append("  height:3px; ");
                            sb.Append("  border-top:1px solid black; ");
                            sb.Append("  border-bottom:1px solid black; ");
                            sb.Append("  transform: rotate(150deg); ");
                            sb.Append("  } ");
                        }

                        sb.Append(" .Datecss{ ");
                        sb.Append("  position: fixed; ");
                        sb.Append("  top: 0.32in; ");
                        sb.Append("  left:  5.69in;  ");
                        sb.Append("  letter-spacing: 16px; ");
                        sb.Append(" } ");

                        sb.Append(" </style> ");
                        sb.Append("  <div class='divcss'>");
                        if (AcPayee == "Yes")
                        {
                            sb.Append("  <div class='Linecss'></div>");
                        }

                        sb.Append("<div class='Datecss'>" + ChequeTx_DateF + "</div>");
                        sb.Append(" <span style='padding-left: 6.19in; padding-top: 0.31in; padding-bottom: 0.33in; display: block;'>&nbsp;</span>");

                        //sb.Append("  <span style='padding-left: 6.19in; padding-top: 0.31in; padding-bottom: 0.33in; display: block;'>2&nbsp;&nbsp;&nbsp;9&nbsp;-&nbsp;0&nbsp;&nbsp;7&nbsp;-&nbsp;2&nbsp;&nbsp;&nbsp;0&nbsp;&nbsp;&nbsp;2&nbsp;&nbsp;&nbsp;2</span>");
                        sb.Append("  <span style='padding-left: 0.8in; padding-right: 2.8in; display: block; font-size: 13px; line-height: 2.2; '>&nbsp;</span>");
                        sb.Append("  <span class='partycss'>" + FavouringName + "</span>");
                        sb.Append("  <span style='padding-left: 0.4in; padding-right: 0.29in; display: block; font-size: 13px; line-height: 2;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + GenerateWordsinRs(ChequeTx_Amount) + " </span>");
                        sb.Append("  <span class='amtcss'>&nbsp;&nbsp;" + ChequeTx_Amount + "</span>");
                        sb.Append("  </div>");

                        divCheque.InnerHtml = sb.ToString();
                        // spnAmount.InnerHtml = GenerateWordsinRs(lblNetAmountdue.Text.ToString());

                        #endregion
                    }
                }
                


            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private string GenerateWordsinRs(string value)
    {
        decimal numberrs = Convert.ToDecimal(value);
        CultureInfo ci = new CultureInfo("en-IN");
        string aaa = String.Format("{0:#,##0.##}", numberrs);
        aaa = aaa + " " + ci.NumberFormat.CurrencySymbol.ToString();
        // label6.Text = aaa;


        string input = value;
        string a = "";
        string b = "";

        // take decimal part of input. convert it to word. add it at the end of method.
        string decimals = "";

        if (input.Contains("."))
        {
            decimals = input.Substring(input.IndexOf(".") + 1);
            // remove decimal part from input
            input = input.Remove(input.IndexOf("."));

        }
        string strWords = NUMBERTOWORDS.NumbersToWords(Convert.ToInt32(input));

        if (!value.Contains("."))
        {
            a = strWords + " Rupees Only";
        }
        else
        {
            a = strWords + " Rupees";
        }

        if (decimals.Length > 0)
        {
            // if there is any decimal part convert it to words and add it to strWords.
            string strwords2 = NUMBERTOWORDS.NumbersToWords(Convert.ToInt32(decimals));
            b = " And " + strwords2 + " Paisa Only ";
        }

        return a + b;
    }
}