using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;

public partial class mis_MilkCollection_CCWiseDCSSummary : System.Web.UI.Page
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
                            //ddlccbmcdetail.Items.Insert(0, new ListItem("Select", "0"));
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
            decimal KgFat = 0;
            decimal KgSnf = 0;
            decimal Quantity = 0;
            decimal TotalFatSnf = 0;
			decimal TotalGrossAmount = 0;
            decimal TotalCommission = 0;
            decimal TotalMilkValue = 0;
            int DSCount = 0;
            string CC_Id = "";
            divshow.Visible = false;
            StringBuilder sb = new StringBuilder();
            string CC_ID = "";

            int SerialNo = 0;
            int totalListItem = ddlccbmcdetail.Items.Count;
            foreach (ListItem item in ddlccbmcdetail.Items)
            {
                if (item.Selected)
                {
                    SerialNo++;
                    CC_ID = item.Value;
                    ds = objdb.ByProcedure("Usp_MilkCollectionBillingReports", new string[] { "flag", "FromDate", "ToDate", "CCID", }, new string[] { "36", Convert.ToDateTime(txtFdt.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTdt.Text, cult).ToString("yyyy/MM/dd"), CC_ID }, "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            divshow.Visible = true;
                            int ColumnCount = ds.Tables[0].Columns.Count;
                            int RowCount = ds.Tables[0].Rows.Count;
                            sb.Append("<table class='table' style='width:100%'>");
                            sb.Append("<thead class='header'>");
                            sb.Append("<tr>");
                            sb.Append("<td colspan='4'  style='text-align:left; font-size:14px;'><b>" + Session["Office_Name"].ToString() + "</b></td>");
                            sb.Append("<td colspan='5' style='text-align:left; font-size:14px;'><b>CC : -" + item.Text + "</b></td>");
                            sb.Append("</tr>");
                            sb.Append("<tr>");
                            sb.Append("<td colspan='4' style='text-align:left; font-size:14px;'><b>CC Wise DCS SUMMARY</b></td>");
                            sb.Append("<td colspan='5' style='text-align:left; font-size:14px;'><b>PERIOD : " + txtFdt.Text + " - " + txtTdt.Text + "</b></td>");
                            sb.Append("<tr>");
                            sb.Append("<th>S.No</th>");
                            sb.Append("<th>Soc.Code</th>");
                            sb.Append("<th>Soc.Name</th>");
                            sb.Append("<th>QTY</th>");
                            sb.Append("<th>KGFAT</th>");
                            sb.Append("<th>KGSNF</th>");
                            sb.Append("<th>KgFat + KgSnf</th>");
                            sb.Append("<th>Milk Value</th>");
                            sb.Append("<th>Commission</th>");
                            sb.Append("<th>Gross Amount</th>");
                            sb.Append("</tr>");
                            sb.Append("</tr>");

                            sb.Append("</thead>");

                            int Count = ds.Tables[0].Rows.Count;
                            for (int i = 0; i < Count; i++)
                            {

                               
                                decimal Totalkgfatsnf = decimal.Parse(ds.Tables[0].Rows[i]["KgFat"].ToString()) + decimal.Parse(ds.Tables[0].Rows[i]["KgSnf"].ToString());
                                sb.Append("<tr>");
                                sb.Append("<td>" + (i + 1).ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[0].Rows[i]["Office_Code"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[0].Rows[i]["Office_Name_E"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[0].Rows[i]["Qty"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[0].Rows[i]["KgFat"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[0].Rows[i]["KgSnf"].ToString() + "</td>");
                                sb.Append("<td>" + Totalkgfatsnf + "</td>");
                                sb.Append("<td>" + ds.Tables[0].Rows[i]["GrossAmount"].ToString() + "</td>");
                                sb.Append("<td>" + ds.Tables[0].Rows[i]["commission"].ToString() + "</td>");                             
                                sb.Append("<td>" + (decimal.Parse(ds.Tables[0].Rows[i]["commission"].ToString()) + decimal.Parse(ds.Tables[0].Rows[i]["GrossAmount"].ToString()))+ "</td>");
                                sb.Append("</tr>");


                              



                            }
                            Quantity = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("Qty"));
                            KgFat = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("KgFat"));
                            KgSnf = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("KgSnf"));
                            TotalMilkValue = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("GrossAmount"));
                            TotalCommission = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("commission"));
                            TotalGrossAmount = TotalMilkValue + TotalCommission;
                            TotalFatSnf = KgFat + KgSnf;

                            sb.Append("<tr>");
                            sb.Append("<td colspan='3'><b>Grand Total</b></td>");
                            sb.Append("<td><b>" + Quantity.ToString() + "</b></td>");
                            sb.Append("<td><b>" + KgFat.ToString() + "</b></td>");
                            sb.Append("<td><b>" + KgSnf.ToString() + "</b></td>");
                            sb.Append("<td><b>" + TotalFatSnf.ToString() + "</b></td>");
                            sb.Append("<td><b>" + TotalMilkValue.ToString() + "</b></td>");
                            sb.Append("<td><b>" + TotalCommission.ToString() + "</b></td>");
                            sb.Append("<td><b>" + TotalGrossAmount.ToString() + "</b></td>");
                            sb.Append("</table>");

                            divReport.InnerHtml = sb.ToString();
                            divprint.InnerHtml = sb.ToString();
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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "CCWiseDCSSummary" + DateTime.Now + ".xls");
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