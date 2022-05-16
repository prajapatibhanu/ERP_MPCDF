using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;

public partial class mis_MilkCollection_CCWiseEDReport : System.Web.UI.Page
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
                
                GetCCDetails();
               
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }
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
                            ddlccbmcdetail.Items.Insert(0, new ListItem("Select", "0"));
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
    protected void btnSearch_Click(object sender, EventArgs e)
        {
        try
        {
            lblMsg.Text = "";
            divReport.InnerHtml = "";
            divprint.InnerHtml = "";
            divshow.Visible = false;
            StringBuilder sb = new StringBuilder();
            ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports", new string[] { "flag", "FromDate", "ToDate", "Office_ID" }, new string[] { "22", Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd"), ddlccbmcdetail.SelectedValue }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    divshow.Visible = true;
                    int ColumnCount = ds.Tables[0].Columns.Count;
                    int RowCount = ds.Tables[0].Rows.Count;
                    sb.Append("<table class='table'>");
					sb.Append("<thead class='header'>");
                    sb.Append("<tr>");
                    sb.Append("<td style='text-align:right;' colspan='6'><b>" + Session["Office_Name"].ToString() + "</b></td>");
                    sb.Append("<td style='text-align:right;' colspan='2'><b>Period : " + txtFdt.Text + " to  " + txtTdt.Text + "</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='text-align:right;' colspan='6'><b>Earning Deduction Statement</b></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td style='text-align:left;' colspan='9'><b>Name of CC:-" + ddlccbmcdetail.SelectedItem.Text + "</b></td>");
                    sb.Append("</tr>");
                    int Count = ds.Tables[0].Rows.Count;
                    int j = 0;
                    int K = 0;
                    int E = 1;
                    int D = 0;
                    decimal TotalEarning = 0;
                    decimal TotalDeduction = 0;
                    string OfficeCode = "";
                    string HeadType = "";
                    for (int i = 0; i < Count; i++)
                    {
                        if(i == 0)
                        {
                            
                            sb.Append("<tr>");
                            sb.Append("<td ><b>" + (j + 1).ToString() + "</b></td>");

                            sb.Append("<td ><b>" + ds.Tables[0].Rows[i]["Office_Code"].ToString() + "</b></td>");
                            sb.Append("<td ><b>" + ds.Tables[0].Rows[i]["Office_Name_E"].ToString() + "</b></td>");

                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td></td>");
                            sb.Append("<td >E " + E.ToString() + "</td>");
                            sb.Append("<td >" + ds.Tables[0].Rows[i]["HeadName"].ToString() + ":  " + ds.Tables[0].Rows[i]["HeadAmount"].ToString() + "</td>");
                            K += 1;
                            OfficeCode = ds.Tables[0].Rows[i]["Office_Code"].ToString();
                            HeadType = ds.Tables[0].Rows[i]["HeadType"].ToString();
                            if (HeadType == "ADDITION")
                            {
                                if (ds.Tables[0].Rows[i]["HeadName"].ToString() == "Qty" || ds.Tables[0].Rows[i]["HeadName"].ToString() == "KgFat" || ds.Tables[0].Rows[i]["HeadName"].ToString() == "KgSnf")
                                {
                                    
                                }
                                else
                                {
                                    TotalEarning += decimal.Parse(ds.Tables[0].Rows[i]["HeadAmount"].ToString());
                                }
                            }
                            else
                            {
                                TotalDeduction += decimal.Parse(ds.Tables[0].Rows[i]["HeadAmount"].ToString());
                            }
                          
                        }
                        else if(OfficeCode == ds.Tables[0].Rows[i]["Office_Code"].ToString())
                        {
                            if(HeadType ==ds.Tables[0].Rows[i]["HeadType"].ToString())
                            {
                                if(K <=5)
                                {
                                    sb.Append("<td > " + ds.Tables[0].Rows[i]["HeadName"].ToString() + ":  " + ds.Tables[0].Rows[i]["HeadAmount"].ToString() + "</td>");
                                    
                                    K += 1;
                                    OfficeCode = ds.Tables[0].Rows[i]["Office_Code"].ToString();
                                    HeadType = ds.Tables[0].Rows[i]["HeadType"].ToString();
                                    if (ds.Tables[0].Rows[i]["HeadType"].ToString() == "ADDITION")
                                    {
                                        if (ds.Tables[0].Rows[i]["HeadName"].ToString() == "Qty" || ds.Tables[0].Rows[i]["HeadName"].ToString() == "KgFat" || ds.Tables[0].Rows[i]["HeadName"].ToString() == "KgSnf")
                                        {

                                        }
                                        else
                                        {
                                            TotalEarning += decimal.Parse(ds.Tables[0].Rows[i]["HeadAmount"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        TotalDeduction += decimal.Parse(ds.Tables[0].Rows[i]["HeadAmount"].ToString());
                                    }
                                }
                                else
                                {
                                   
                                    sb.Append("</tr>");
                                    
                                    K = 0;
                                    E += 1;
                                    sb.Append("<tr>");
                                    if (ds.Tables[0].Rows[i]["HeadType"].ToString() == "ADDITION")
                                    {
                                        sb.Append("<td></td>");
                                        sb.Append("<td >E " + E.ToString() + "</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td></td>");
                                        sb.Append("<td >D " + E.ToString() + "</td>");
                                    }
                                    
                                    sb.Append("<td >" + ds.Tables[0].Rows[i]["HeadName"].ToString() + ":  " + ds.Tables[0].Rows[i]["HeadAmount"].ToString() + "</td>");
                                    K += 1;
                                    OfficeCode = ds.Tables[0].Rows[i]["Office_Code"].ToString();
                                    HeadType = ds.Tables[0].Rows[i]["HeadType"].ToString();
                                    if (ds.Tables[0].Rows[i]["HeadType"].ToString() == "ADDITION")
                                    {
                                        if (ds.Tables[0].Rows[i]["HeadName"].ToString() == "Qty" || ds.Tables[0].Rows[i]["HeadName"].ToString() == "KgFat" || ds.Tables[0].Rows[i]["HeadName"].ToString() == "KgSnf")
                                        {

                                        }
                                        else
                                        {
                                            TotalEarning += decimal.Parse(ds.Tables[0].Rows[i]["HeadAmount"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        TotalDeduction += decimal.Parse(ds.Tables[0].Rows[i]["HeadAmount"].ToString());
                                    }
                                }

                            }
                            else
                            {
                                if (HeadType == "ADDITION")
                                {
                                    sb.Append("<td><b>Total Earning  </b>" + TotalEarning.ToString() + "</td>");
                                }
                                else
                                {
                                    sb.Append("<td><b>Total Deduction  </b>" + TotalDeduction.ToString() + "</td>");
                                }
                               
                                sb.Append("</tr>");
                                K = 0;
                                E = 1;
                                if(K== 0)
                                {
                                    sb.Append("<tr>");
                                    if (ds.Tables[0].Rows[i]["HeadType"].ToString() == "ADDITION")
                                    {
                                        sb.Append("<td></td>");
                                        sb.Append("<td >E " + E.ToString() + "</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td></td>");
                                        sb.Append("<td >D " + E.ToString() + "</td>");
                                    }
                                    sb.Append("<td>" + ds.Tables[0].Rows[i]["HeadName"].ToString() + ":  " + ds.Tables[0].Rows[i]["HeadAmount"].ToString() + "</td>");
                                    K += 1;
                                    OfficeCode = ds.Tables[0].Rows[i]["Office_Code"].ToString();
                                    HeadType = ds.Tables[0].Rows[i]["HeadType"].ToString();
                                    if (ds.Tables[0].Rows[i]["HeadType"].ToString() == "ADDITION")
                                    {
                                        if (ds.Tables[0].Rows[i]["HeadName"].ToString() == "Qty" || ds.Tables[0].Rows[i]["HeadName"].ToString() == "KgFat" || ds.Tables[0].Rows[i]["HeadName"].ToString() == "KgSnf")
                                        {

                                        }
                                        else
                                        {
                                            TotalEarning += decimal.Parse(ds.Tables[0].Rows[i]["HeadAmount"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        TotalDeduction += decimal.Parse(ds.Tables[0].Rows[i]["HeadAmount"].ToString());
                                    }
                                }
                                else if (K <= 5)
                                {
                                    sb.Append("<td>" + ds.Tables[0].Rows[i]["HeadName"].ToString() + ":  " + ds.Tables[0].Rows[i]["HeadAmount"].ToString() + "</td>");
                                    K += 1;
                                    OfficeCode = ds.Tables[0].Rows[i]["Office_Code"].ToString();
                                    HeadType = ds.Tables[0].Rows[i]["HeadType"].ToString();
                                }
                                else
                                {
                                    sb.Append("</tr>");
                                    K = 0;
                                    sb.Append("<tr>");
                                    sb.Append("<td></td>");
                                    sb.Append("<td>E " + D.ToString() + "</td>");
                                    sb.Append("<td>" + ds.Tables[0].Rows[i]["HeadName"].ToString() + ":  " + ds.Tables[0].Rows[i]["HeadAmount"].ToString() + "</td>");
                                    K += 1;
                                    OfficeCode = ds.Tables[0].Rows[i]["Office_Code"].ToString();
                                    HeadType = ds.Tables[0].Rows[i]["HeadType"].ToString();
                                    if (ds.Tables[0].Rows[i]["HeadType"].ToString() == "ADDITION")
                                    {
                                        if (ds.Tables[0].Rows[i]["HeadName"].ToString() == "Qty" || ds.Tables[0].Rows[i]["HeadName"].ToString() == "KgFat" || ds.Tables[0].Rows[i]["HeadName"].ToString() == "KgSnf")
                                        {

                                        }
                                        else
                                        {
                                            TotalEarning += decimal.Parse(ds.Tables[0].Rows[i]["HeadAmount"].ToString());
                                        }
                                    }
                                    else
                                    {
                                        TotalDeduction += decimal.Parse(ds.Tables[0].Rows[i]["HeadAmount"].ToString());
                                    }
                                }
                            }
                        }
                        else
                        {
                            j += 1;
                            K = 0;
                            E = 1;
                            if (HeadType == "ADDITION")
                            {
                                sb.Append("<td><b>Total Earning  </b>" + TotalEarning.ToString() + "</td>");
                            }
                            else
                            {
                                sb.Append("<td><b>Total Deduction  </b>" + TotalDeduction.ToString() + "</td>");
                            }
                            TotalEarning = 0;
                            TotalDeduction = 0;
                            sb.Append("<tr>");
                            sb.Append("<td ><b>" + (j + 1).ToString() + "</b></td>");

                            sb.Append("<td><b>" + ds.Tables[0].Rows[i]["Office_Code"].ToString() + "</b></td>");
                            sb.Append("<td><b>" + ds.Tables[0].Rows[i]["Office_Name_E"].ToString() + "</b></td>");

                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td></td>");
                            sb.Append("<td >E " + E.ToString() + "</td>");
                            sb.Append("<td >" + ds.Tables[0].Rows[i]["HeadName"].ToString() + ":  " + ds.Tables[0].Rows[i]["HeadAmount"].ToString() + "</td>");
                            K += 1;
                            OfficeCode = ds.Tables[0].Rows[i]["Office_Code"].ToString();
                            HeadType = ds.Tables[0].Rows[i]["HeadType"].ToString();
                            if (ds.Tables[0].Rows[i]["HeadType"].ToString() == "ADDITION")
                            {
                                if (ds.Tables[0].Rows[i]["HeadName"].ToString() == "Qty" || ds.Tables[0].Rows[i]["HeadName"].ToString() == "KgFat" || ds.Tables[0].Rows[i]["HeadName"].ToString() == "KgSnf")
                                {

                                }
                                else
                                {
                                    TotalEarning += decimal.Parse(ds.Tables[0].Rows[i]["HeadAmount"].ToString());
                                }
                            }
                            else
                            {
                                TotalDeduction += decimal.Parse(ds.Tables[0].Rows[i]["HeadAmount"].ToString());
                            }
                        }
                       
                        
                    }


                    if (HeadType == "ADDITION")
                    {
                        sb.Append("<td><b>Total Earning  </b>" + TotalEarning.ToString() + "</td>");
                    }
                    else
                    {
                        sb.Append("<td><b>Total Deduction  </b>" + TotalDeduction.ToString() + "</td>");
                    }
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='11'><b>GRAND TOTAL</b></td>");
                    sb.Append("</tr>");
                    int Count1 = ds.Tables[1].Rows.Count;

                    string HeadType1 = "";
                    int K1 = 0;
                    int E1 = 1;
                    TotalEarning = 0;
                    TotalDeduction = 0;
                    D = 0;
                    {
                        for (int l = 0; l < Count1; l++)
                        {
                            
                            if(l==0)
                            {
                                sb.Append("<tr>");
                                sb.Append("<td></td>");
                                sb.Append("<td >E " + E1.ToString() + "</td>");
                                sb.Append("<td >" + ds.Tables[1].Rows[l]["HeadName"].ToString() + ":  " + ds.Tables[1].Rows[l]["HeadAmount"].ToString() + "</td>");
                                
                                HeadType1 = ds.Tables[1].Rows[l]["HeadType"].ToString();
                                if (HeadType1 == "ADDITION")
                                {
                                    if (ds.Tables[1].Rows[l]["HeadName"].ToString() == "Qty" || ds.Tables[1].Rows[l]["HeadName"].ToString() == "KgFat" || ds.Tables[1].Rows[l]["HeadName"].ToString() == "KgSnf")
                                    {

                                    }
                                    else
                                    {
                                        TotalEarning += decimal.Parse(ds.Tables[1].Rows[l]["HeadAmount"].ToString());
                                    }
                                }
                                else
                                {
                                    TotalDeduction += decimal.Parse(ds.Tables[1].Rows[l]["HeadAmount"].ToString());
                                }
                            }
                            else
                            {
                                if (HeadType1 == ds.Tables[1].Rows[l]["HeadType"].ToString())
                                {
                                    if (K1 <= 5)
                                    {
                                        sb.Append("<td> " + ds.Tables[1].Rows[l]["HeadName"].ToString() + ":  " + ds.Tables[1].Rows[l]["HeadAmount"].ToString() + "</td>");

                                        K1 += 1;

                                        HeadType1 = ds.Tables[1].Rows[l]["HeadType"].ToString();
                                        if (ds.Tables[1].Rows[l]["HeadType"].ToString() == "ADDITION")
                                        {
                                            if (ds.Tables[1].Rows[l]["HeadName"].ToString() == "Qty" || ds.Tables[1].Rows[l]["HeadName"].ToString() == "KgFat" || ds.Tables[1].Rows[l]["HeadName"].ToString() == "KgSnf")
                                            {

                                            }
                                            else
                                            {
                                                TotalEarning += decimal.Parse(ds.Tables[1].Rows[l]["HeadAmount"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            TotalDeduction += decimal.Parse(ds.Tables[1].Rows[l]["HeadAmount"].ToString());
                                        }
                                    }
                                    else
                                    {

                                        sb.Append("</tr>");

                                        K1 = 0;
                                        E1 += 1;
                                        sb.Append("<tr>");
                                        if (ds.Tables[1].Rows[l]["HeadType"].ToString() == "ADDITION")
                                        {
                                            sb.Append("<td></td>");
                                            sb.Append("<td >E " + E1.ToString() + "</td>");
                                        }
                                        else
                                        {
                                            sb.Append("<td></td>");
                                            sb.Append("<td >D " + E1.ToString() + "</td>");
                                        }

                                        sb.Append("<td >" + ds.Tables[1].Rows[l]["HeadName"].ToString() + ":  " + ds.Tables[1].Rows[l]["HeadAmount"].ToString() + "</td>");
                                        K1 += 1;

                                        HeadType1 = ds.Tables[1].Rows[l]["HeadType"].ToString();
                                        if (ds.Tables[1].Rows[l]["HeadType"].ToString() == "ADDITION")
                                        {
                                            if (ds.Tables[1].Rows[l]["HeadName"].ToString() == "Qty" || ds.Tables[1].Rows[l]["HeadName"].ToString() == "KgFat" || ds.Tables[1].Rows[l]["HeadName"].ToString() == "KgSnf")
                                            {

                                            }
                                            else
                                            {
                                                TotalEarning += decimal.Parse(ds.Tables[1].Rows[l]["HeadAmount"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            TotalDeduction += decimal.Parse(ds.Tables[1].Rows[l]["HeadAmount"].ToString());
                                        }
                                    }

                                }
                                else
                                {
                                    if (HeadType1 == "ADDITION")
                                    {
                                        sb.Append("<td><b>Total Earning  </b>" + TotalEarning.ToString() + "</td>");
                                    }
                                    else
                                    {
                                        sb.Append("<td><b>Total Deduction  </b>" + TotalDeduction.ToString() + "</td>");
                                    }

                                    sb.Append("</tr>");
                                    K1 = 0;
                                    E1 = 1;
                                    if (K1 == 0)
                                    {
                                        sb.Append("<tr>");
                                        if (ds.Tables[1].Rows[l]["HeadType"].ToString() == "ADDITION")
                                        {
                                            sb.Append("<td></td>");
                                            sb.Append("<td >E " + E1.ToString() + "</td>");
                                        }
                                        else
                                        {
                                            sb.Append("<td></td>");
                                            sb.Append("<td >D " + E1.ToString() + "</td>");
                                        }
                                        sb.Append("<td>" + ds.Tables[1].Rows[l]["HeadName"].ToString() + ":  " + ds.Tables[1].Rows[l]["HeadAmount"].ToString() + "</td>");
                                        K1 += 1;

                                        HeadType1 = ds.Tables[1].Rows[l]["HeadType"].ToString();
                                        if (ds.Tables[1].Rows[l]["HeadType"].ToString() == "ADDITION")
                                        {
                                            if (ds.Tables[1].Rows[l]["HeadName"].ToString() == "Qty" || ds.Tables[1].Rows[l]["HeadName"].ToString() == "KgFat" || ds.Tables[1].Rows[l]["HeadName"].ToString() == "KgSnf")
                                            {

                                            }
                                            else
                                            {
                                                TotalEarning += decimal.Parse(ds.Tables[1].Rows[l]["HeadAmount"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            TotalDeduction += decimal.Parse(ds.Tables[1].Rows[l]["HeadAmount"].ToString());
                                        }
                                    }
                                    else if (K1 <= 5)
                                    {
                                        sb.Append("<td>" + ds.Tables[1].Rows[l]["HeadName"].ToString() + ":  " + ds.Tables[1].Rows[l]["HeadAmount"].ToString() + "</td>");
                                        K1 += 1;

                                        HeadType = ds.Tables[1].Rows[l]["HeadType"].ToString();
                                    }
                                    else
                                    {
                                        sb.Append("</tr>");
                                        K1 = 0;
                                        sb.Append("<tr>");
                                        sb.Append("<td></td>");
                                        sb.Append("<td>E " + E1.ToString() + "</td>");
                                        sb.Append("<td>" + ds.Tables[1].Rows[l]["HeadName"].ToString() + ":  " + ds.Tables[1].Rows[l]["HeadAmount"].ToString() + "</td>");
                                        K1 += 1;

                                        HeadType = ds.Tables[1].Rows[l]["HeadType"].ToString();
                                        if (ds.Tables[1].Rows[l]["HeadType"].ToString() == "ADDITION")
                                        {
                                            if (ds.Tables[1].Rows[l]["HeadName"].ToString() == "Qty" || ds.Tables[1].Rows[l]["HeadName"].ToString() == "KgFat" || ds.Tables[1].Rows[l]["HeadName"].ToString() == "KgSnf")
                                            {

                                            }
                                            else
                                            {
                                                TotalEarning += decimal.Parse(ds.Tables[1].Rows[l]["HeadAmount"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            TotalDeduction += decimal.Parse(ds.Tables[1].Rows[l]["HeadAmount"].ToString());
                                        }
                                    }
                                }
                            }
                            
                        }
                    }
                    if (HeadType == "ADDITION")
                    {
                        sb.Append("<td><b>Total Earning  </b>" + TotalEarning.ToString() + "</td>");
                    }
                    else
                    {
                        sb.Append("<td><b>Total Deduction  </b>" + TotalDeduction.ToString() + "</td>");
                    }

                    sb.Append("</tr>");
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
            Response.AddHeader("content-disposition", "attachment;filename=" + "CCWiseAdditionDeductionReport" + DateTime.Now + ".xls");
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