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
public partial class mis_Demand_Rpt_SectionwiseMilkOrProductVariant_BDS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds2, ds3, ds4, ds5 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetCategory();
                GetOfficeDetails();
                GetDisOrSS();
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtFromDate.Text = Date;
                txtFromDate.Attributes.Add("readonly", "true");
                txtToDate.Text = Date;
                txtToDate.Attributes.Add("readonly", "true");
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    private void GetDisOrSS()
    {
        try
        {
            ddlDitributor.DataTextField = "DTName";
            ddlDitributor.DataValueField = "DistributorId";
            ddlDitributor.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "AreaId", "ItemCat_id", "Office_ID" },
                   new string[] { "11", "0", objdb.GetProductCatId(), objdb.Office_ID() }, "dataset");
            ddlDitributor.DataBind();
            ddlDitributor.Items.Insert(0, new ListItem("Select", "0"));


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
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
    protected void GetSection()
    {
        try
        {
            ddlSection.DataTextField = "SectionName";
            ddlSection.DataValueField = "MOrPSection_id";
            ddlSection.DataSource = objdb.ByProcedure("USP_Mst_MilkOrProductSectionName",
                 new string[] { "Flag", "Office_ID", "ItemCat_id" },
                   new string[] { "5", objdb.Office_ID(), ddlItemCategory.SelectedValue }, "dataset");
            ddlSection.DataBind();
            ddlSection.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
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
    private void GetItem()
    {
        try
        {

            ddlItemName.Items.Clear();
            ds3 = objdb.ByProcedure("USP_Mst_MilkOrProductItemSectionMapping",
                                new string[] { "Flag", "Office_ID", "MOrPSection_id" },
                                new string[] { "4", objdb.Office_ID(), ddlSection.SelectedValue }, "dataset");
            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {
                ddlItemName.DataSource = ds3.Tables[0];
                ddlItemName.DataTextField = "ItemName";
                ddlItemName.DataValueField = "Item_id";
                ddlItemName.DataBind();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }
    private void GetCompareDate()
    {
        try
        {
            string myStringfromdat = txtFromDate.Text; // From Database
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtToDate.Text; // From Database
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                lblMsg.Text = string.Empty;

                string itemid = "";
                int iddata = 0;
                foreach (ListItem item in ddlItemName.Items)
                {
                    if (item.Selected)
                    {
                        ++iddata;
                        if (iddata == 1)
                        {
                            itemid = item.Value;
                        }
                        else
                        {
                            itemid += "," + item.Value;
                        }


                    }
                }

                if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
                {
                    MilkSectionVariantwiseRpt(itemid);
                }
                else
                {
                    ProductSectionVariantwiseRpt(itemid);
                }

            }
            else
            {
                txtToDate.Text = string.Empty;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must be greater than or equal to the FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4: ", ex.Message.ToString());
        }
    }
    private void MilkSectionVariantwiseRpt(string itemid)
    {
        try
        {

            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds5 = objdb.ByProcedure("USP_Trn_MilkOrProductSectionwise_MIS_new",
                     new string[] { "Flag", "FromDate", "ToDate", "ItemCat_id", "MOrPSection_id", "Item_id", "Office_ID", "DistributorId" },
                       new string[] { "1", fromdat, todat, ddlItemCategory.SelectedValue, ddlSection.SelectedValue, itemid, objdb.Office_ID(), ddlDitributor.SelectedValue }, "dataset");

            if (ds5.Tables[0].Rows.Count != 0 && ds5 != null)
            {
                btnPrintRoutWise.Visible = true;
                btnExportAll.Visible = true;

                int Count = ds5.Tables[0].Rows.Count;
                int ColCount = ds5.Tables[0].Columns.Count;
                StringBuilder sb1 = new StringBuilder();

                //string s = date3.DayOfWeek.ToString();
                sb1.Append("<table style='width:100%; height:100%'>");
                sb1.Append("<tr>");
                sb1.Append("<td style='padding: 2px 5px;text-align:center' colspan='2'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='padding: 2px 5px;text-align: lefg;'><b>Section  :-" + ddlSection.SelectedItem.Text + "</b></td>");
                sb1.Append("<td style='padding: 2px 5px;text-align: right;'><b>Period  :-" + txtFromDate.Text + " To " + txtToDate.Text + "</b></td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                sb1.Append("<table class='table'>");
                sb1.Append("<thead>");
                sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>S.no</b></td>");
                sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Distributor Name</b></td>");
                //sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Date</b></td>");
                for (int j = 0; j < ColCount; j++)
                {

                    if (ds5.Tables[0].Columns[j].ToString() != "DistributorId" && ds5.Tables[0].Columns[j].ToString() != "DistName" && ds5.Tables[0].Columns[j].ToString() != "Delivary_Date")
                    {
                        string ColName = ds5.Tables[0].Columns[j].ToString();
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");

                    }

                }
                sb1.Append("</thead>");

                for (int i = 0; i < Count; i++)
                {

                    sb1.Append("<tr>");
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + (i + 1).ToString() + "</td>");

                    sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["DistName"] + "</td>");
                    //sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["Delivary_Date"] + "</td>");
                    for (int K = 0; K < ColCount; K++)
                    {
                        if (ds5.Tables[0].Columns[K].ToString() != "DistributorId" && ds5.Tables[0].Columns[K].ToString() != "DistName" && ds5.Tables[0].Columns[K].ToString() != "Delivary_Date")
                        {
                            string ColName = ds5.Tables[0].Columns[K].ToString();
                            sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + (ds5.Tables[0].Rows[i][ColName].ToString() == "" ? "0" : ds5.Tables[0].Rows[i][ColName].ToString()) + "</td>");

                        }

                    }
                    sb1.Append("</tr>");

                }
                sb1.Append("<tr>");
                sb1.Append("<td style=' border: 1px solid #000000 !important;text-align:center; 'colspan='2'><b>Total</b></td>");
                DataTable dt = new DataTable();
                dt = ds5.Tables[0];
                int sum11 = 0;
                decimal dsum11 = 0;
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() != "DistributorId" && column.ToString() != "DistName" && column.ToString() != "Delivary_Date")
                    {
                        if (column.ToString() == "In Litre" || column.ToString() == "Amount")
                        {
                            dsum11 = dt.AsEnumerable().Sum(r => r.Field<decimal?>("" + column + "") ?? 0);
                            sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + dsum11.ToString() + "</b></td>");
                        }
                        else
                        {
                            sum11 = dt.AsEnumerable().Sum(r => r.Field<int?>("" + column + "") ?? 0);
                            sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + sum11.ToString() + "</b></td>");
                        }


                    }
                }
                sb1.Append("</tr>");
                sb1.Append("</table>");
                div_page_content.InnerHtml = sb1.ToString();
                ViewState["SectionWiseMilkDetails"] = sb1.ToString();
                if (dt != null) { dt.Dispose(); }

            }
            else
            {
                btnPrintRoutWise.Visible = false;
                btnExportAll.Visible = false;
                ViewState["SectionWiseMilkDetails"] = "";
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found.");
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }
    }
    private void ProductSectionVariantwiseRpt(string itemid)
    {
        try
        {

            DateTime date3 = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", culture);
            DateTime date4 = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", culture);
            string fromdat = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            string todat = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            if(objdb.Office_ID()=="2")
            {
                ds5 = objdb.ByProcedure("USP_Trn_MilkOrProductSectionwise_MIS_new",
                    new string[] { "Flag", "FromDate", "ToDate", "ItemCat_id", "MOrPSection_id", "Item_id", "Office_ID", "DistributorId" },
                      new string[] { "3", fromdat, todat, ddlItemCategory.SelectedValue, ddlSection.SelectedValue, itemid, objdb.Office_ID(),ddlDitributor.SelectedValue
                       }, "dataset");
            }
            else
            { 
            ds5 = objdb.ByProcedure("USP_Trn_MilkOrProductSectionwise_MIS_new",
                     new string[] { "Flag", "FromDate", "ToDate", "ItemCat_id", "MOrPSection_id", "Item_id", "Office_ID", "DistributorId" },
                       new string[] { "2", fromdat, todat, ddlItemCategory.SelectedValue, ddlSection.SelectedValue, itemid, objdb.Office_ID(),ddlDitributor.SelectedValue
                       }, "dataset");
            }

            if (ds5.Tables[0].Rows.Count != 0 && ds5 != null)
            {
                btnPrintRoutWise.Visible = true;
                btnExportAll.Visible = true;

                int Count = ds5.Tables[0].Rows.Count;
                int ColCount = ds5.Tables[0].Columns.Count;
                StringBuilder sb1 = new StringBuilder();

                //string s = date3.DayOfWeek.ToString();
                sb1.Append("<table style='width:100%; height:100%'>");
                sb1.Append("<tr>");
                sb1.Append("<td style='padding: 2px 5px;text-align:center' colspan='2'><b>" + ViewState["Office_Name"].ToString() + "<b></td>");
                sb1.Append("</tr>");
                sb1.Append("<tr>");
                sb1.Append("<td style='padding: 2px 5px;text-align: lefg;'><b>Section  :-" + ddlSection.SelectedItem.Text + "</b></td>");
                sb1.Append("<td style='padding: 2px 5px;text-align: right;'><b>Period  :-" + txtFromDate.Text + " To " + txtToDate.Text + "</b></td>");
                sb1.Append("</tr>");
                sb1.Append("</table>");
                sb1.Append("<table class='table'>");
                sb1.Append("<thead>");
                sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>S.no</b></td>");
                sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Party Name</b></td>");
                sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Party Code</b></td>");
                //sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>DM No</b></td>");
                //sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>Date</b></td>");
                for (int j = 0; j < ColCount; j++)
                {

                    if (ds5.Tables[0].Columns[j].ToString() != "DistributorId" && ds5.Tables[0].Columns[j].ToString() != "DName" && ds5.Tables[0].Columns[j].ToString() != "DCode") //&& ds5.Tables[0].Columns[j].ToString() != "DMChallanNo" && ds5.Tables[0].Columns[j].ToString() != "DMDate"
                    {
                        string ColName = ds5.Tables[0].Columns[j].ToString();
                        sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + ColName + "</b></td>");

                    }

                }
                sb1.Append("</thead>");

                for (int i = 0; i < Count; i++)
                {

                    sb1.Append("<tr>");
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + (i + 1).ToString() + "</td>");

                    sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["DName"] + "</td>");
                    sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["DCode"] + "</td>");
                    //sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["DMChallanNo"] + "</td>");
                    //sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + ds5.Tables[0].Rows[i]["DMDate"] + "</td>");
                    for (int K = 0; K < ColCount; K++)
                    {
                        if (ds5.Tables[0].Columns[K].ToString() != "DistributorId" && ds5.Tables[0].Columns[K].ToString() != "DName" && ds5.Tables[0].Columns[K].ToString() != "DCode") //&& ds5.Tables[0].Columns[K].ToString() != "DMChallanNo" && ds5.Tables[0].Columns[K].ToString() != "DMDate"
                        {
                            string ColName = ds5.Tables[0].Columns[K].ToString();
                            sb1.Append("<td style=' border: 1px solid #000000 !important;'>" + (ds5.Tables[0].Rows[i][ColName].ToString() == "" ? "0" : ds5.Tables[0].Rows[i][ColName].ToString()) + "</td>");

                        }

                    }
                    sb1.Append("</tr>");

                }
                sb1.Append("<tr>");
                sb1.Append("<td style=' border: 1px solid #000000 !important;text-align:center; 'colspan='3'><b>Total</b></td>");
                DataTable dt = new DataTable();
                dt = ds5.Tables[0];
                int sum11 = 0;
                decimal dsum11 = 0;
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() != "DistributorId" && column.ToString() != "DName" && column.ToString() != "DCode") //&& column.ToString() != "DMChallanNo" && column.ToString() != "DMDate"
                    {
                        if (column.ToString() == "InKG" || column.ToString() == "Amount")
                        {
                            dsum11 = dt.AsEnumerable().Sum(r => r.Field<decimal?>("" + column + "") ?? 0);
                            sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + dsum11.ToString() + "</b></td>");
                        }
                        else
                        {
                            sum11 = dt.AsEnumerable().Sum(r => r.Field<int?>("" + column + "") ?? 0);
                            sb1.Append("<td style=' border: 1px solid #000000 !important;'><b>" + sum11.ToString() + "</b></td>");
                        }


                    }
                }
                sb1.Append("</tr>");
                sb1.Append("</table>");
                div_page_content.InnerHtml = sb1.ToString();
                ViewState["SectionWiseProdDetails"] = sb1.ToString();
                if (dt != null) { dt.Dispose(); }

            }
            else
            {
                btnPrintRoutWise.Visible = false;
                btnExportAll.Visible = false;
                ViewState["SectionWiseProdDetails"] = "";
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found.");
                div_page_content.InnerHtml = "";
                Print.InnerHtml = "";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
        finally
        {
            if (ds5 != null) { ds5.Dispose(); }
        }
    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlItemCategory.SelectedValue != "0")
        {
            GetSection();
            ddlItemName.Items.Clear();
        }

    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSection.SelectedValue != "0")
        {
            GetItem();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetCompareDate();
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        btnPrintRoutWise.Visible = false;
        btnExportAll.Visible = false;
        ViewState["SectionWiseMilkDetails"] = "";
        ViewState["SectionWiseProdDetails"] = "";
        div_page_content.InnerHtml = "";
        lblMsg.Text = string.Empty;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
    }
    protected void btnPrintRoutWise_Click(object sender, EventArgs e)
    {
        if (ddlItemCategory.SelectedValue == objdb.GetMilkCatId())
        {
            Print.InnerHtml = ViewState["SectionWiseMilkDetails"].ToString();
        }
        else
        {
            Print.InnerHtml = ViewState["SectionWiseProdDetails"].ToString();
        }

        ClientScriptManager CSM = Page.ClientScript;
        string strScript = "<script>";
        strScript += "window.print();";

        strScript += "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Startup", strScript, false);

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void btnExportAll_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + "Section-" + ddlSection.SelectedItem.Text + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

            Print.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error Export-All " + ex.Message.ToString());
        }
    }

}