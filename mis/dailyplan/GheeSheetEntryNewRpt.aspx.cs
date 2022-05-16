using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;
using System.Configuration;
using System.Web.Configuration;
using System.Text;

public partial class mis_dailyplan_GheeSheetEntryNewRpt : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                lblMsg.Text = "";

                if (!IsPostBack)
                {
                    if (Session["IsSuccess"] != null)
                    {
                        if ((Boolean)Session["IsSuccess"] == true)
                        {
                            Session["IsSuccess"] = false;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                        }
                    }

                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillOffice();
                    FillShift();
                    GetSectionView(sender, e);

                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }

        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "12" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDS.DataSource = ds.Tables[0];
                ddlDS.DataTextField = "Office_Name";
                ddlDS.DataValueField = "Office_ID";
                ddlDS.DataBind();
                ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                ddlDS.SelectedValue = ViewState["Office_ID"].ToString();
                ddlDS.Enabled = false;
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillShift()
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                 new string[] { "flag" },
                 new string[] { "2" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlShift.DataSource = ds.Tables[0];
                ddlShift.DataTextField = "Name";
                ddlShift.DataValueField = "Shift_Id";
                ddlShift.DataBind();
                ddlShift.SelectedValue = ds.Tables[1].Rows[0]["Shift_Id"].ToString();
                txtFDate.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["currentDate"].ToString(), cult).ToString("dd/MM/yyyy");
                txtTDate.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["currentDate"].ToString(), cult).ToString("dd/MM/yyyy");
                ddlShift.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetSectionView(object sender, EventArgs e)
    {

        try
        {

            ds = null;

            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID" },
                  new string[] { "6", ddlDS.SelectedValue }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPSection.DataSource = ds.Tables[0];
                ddlPSection.DataTextField = "ProductSection_Name";
                ddlPSection.DataValueField = "ProductSection_ID";
                ddlPSection.DataBind();
                ddlPSection.SelectedValue = "2";
                ddlPSection.Enabled = false;

                ddlPSection_SelectedIndexChanged(sender, e);

            }
            else
            {
                ddlPSection.Items.Clear();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.DataBind();
                ddlPSection.Enabled = false;
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        divprint.InnerHtml = "";
        if (ddlPSection.SelectedValue != "0")
        {
            GetSectionDetail();
        }

    }
    protected void ddlPSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPSection.SelectedValue != "0")
        {

            GetSectionDetail();

        }

    }
    private void GetSectionDetail()
    {

        try
        {
           
            string Fdate = "";
            string Tdate = "";
            btnExport.Visible = false;
            btnprint.Visible = false;
            divprint.InnerHtml = "";
            if (txtFDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtFDate.Text, cult).ToString("yyyy/MM/dd");
            }
            if (txtTDate.Text != "")
            {
                Tdate = Convert.ToDateTime(txtTDate.Text, cult).ToString("yyyy/MM/dd");
            }
            // For Variant

            DataSet dsVD_Child = objdb.ByProcedure("Sp_Production_GheeSheetNew",
            new string[] { "flag", "Office_ID", "FDate","TDate", "ProductSection_ID", "ItemType_id" },
            new string[] { "3", objdb.Office_ID(), Fdate, Tdate, ddlPSection.SelectedValue, objdb.LooseGheeItemTypeId_ID() }, "dataset");

            if (dsVD_Child != null && dsVD_Child.Tables.Count > 0)
            { 
                if(dsVD_Child.Tables[0].Rows.Count > 0)
                {                                               
                    btnExport.Visible = true;
                    btnprint.Visible = true;
                    StringBuilder sb = new StringBuilder();
                    int Count = dsVD_Child.Tables[0].Rows.Count;
                    sb.Append("<div class='row'>");
                    sb.Append("<div class='col-md-12' style='text-align:center'><h3 style='font-weight: 800; font-size: 20px;'>" + Session["Office_Name"] + "</h3><h5 style='font-weight: 500; font-size: 13px;'>DAIRY PLANT</h5><h5 style='font-weight: 800; font-size: 20px;'>GHEE PACKING ACCOUNT SHEET</h5></div>");
                    sb.Append("<div class='col-md-12' style='text-align:center'>Date:" + txtFDate.Text + " - " + txtTDate.Text + "</div>");
                    sb.Append("<div class='row'>");                    
                    sb.Append("<div class='col-md-12'>");                  
                    sb.Append("<table class='table table-bordered' border='1'>");                                        
                    sb.Append("<tr>");
                    sb.Append("<th colspan='12'>IN FLOW</th>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<th rowspan='2'></th>");                   
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<th colspan='2'>Opening Balance  </th>");
                    sb.Append("<th colspan='2'>Ghee Pack	</th>");
                    sb.Append("<th colspan='2'>Return From FP	</th>");
                    sb.Append("<th colspan='2'>Other</th>");
                    sb.Append("<th colspan='2'>Total</th>");
                    sb.Append("</tr>");
                    

                    sb.Append("<tr>");                    
                    sb.Append("<th>Packet Size</th>");
                    sb.Append("<th>No's</th>");
                    sb.Append("<th>Qty in Kg</th>");
                    sb.Append("<th>No's</th>");
                    sb.Append("<th>Qty in Kg</th>");
                    sb.Append("<th>No's</th>");
                    sb.Append("<th>Qty in Kg</th>");
                    sb.Append("<th>No's</th>");
                    sb.Append("<th>Qty in Kg</th>");
                    sb.Append("<th>No's</th>");
                    sb.Append("<th>Qty in Kg</th>");
                    sb.Append("</tr>");

                    decimal totalOBNo = 0, totalOBQtyInKg = 0, totalGheePackNo = 0, totalGheePackQtyInKg = 0, totalReturnFromFPNo = 0, totalReturnFromFPQtyInKg = 0,
                    totalOtherNo = 0, totalOtherQtyInKg = 0, totalTotalInFlowNo = 0, totalTotalInFlowQtyInKg = 0;

                    for (int i = 0; i < Count; i++)
                    {
                         sb.Append("<tr>");
                         sb.Append("<td>" + dsVD_Child.Tables[0].Rows[i]["ItemName"].ToString() + "</td>");
                         sb.Append("<td>" + dsVD_Child.Tables[0].Rows[i]["OBNo"].ToString() + "</td>");
                         sb.Append("<td>" + dsVD_Child.Tables[0].Rows[i]["OBQtyInKg"].ToString() + "</td>");
                         sb.Append("<td>" + dsVD_Child.Tables[0].Rows[i]["GheePackNo"].ToString() + "</td>");
                         sb.Append("<td>" + dsVD_Child.Tables[0].Rows[i]["GheePackQtyInKg"].ToString() + "</td>");
                         sb.Append("<td>" + dsVD_Child.Tables[0].Rows[i]["ReturnFromFPNo"].ToString() + "</td>");
                         sb.Append("<td>" + dsVD_Child.Tables[0].Rows[i]["ReturnFromFPQtyInKg"].ToString() + "</td>");
                         sb.Append("<td>" + dsVD_Child.Tables[0].Rows[i]["OtherNo"].ToString() + "</td>");
                         sb.Append("<td>" + dsVD_Child.Tables[0].Rows[i]["OtherQtyInKg"].ToString() + "</td>");

                         sb.Append("<td>" + (decimal.Parse(dsVD_Child.Tables[0].Rows[i]["OBNo"].ToString()) + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["GheePackNo"].ToString()) + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["ReturnFromFPNo"].ToString()) + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["OtherNo"].ToString())) + "</td>");
                         sb.Append("<td>" + (decimal.Parse(dsVD_Child.Tables[0].Rows[i]["OBQtyInKg"].ToString()) + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["GheePackQtyInKg"].ToString()) + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["ReturnFromFPQtyInKg"].ToString()) + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["OtherQtyInKg"].ToString())) + "</td>");
                         
                         sb.Append("</tr>");
                      
                    }

                    totalOBNo = dsVD_Child.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OBNo"));
                    totalOBQtyInKg = dsVD_Child.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OBQtyInKg"));
                    totalGheePackNo = dsVD_Child.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("GheePackNo"));
                    totalGheePackQtyInKg = dsVD_Child.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("GheePackQtyInKg"));
                    totalReturnFromFPNo = dsVD_Child.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ReturnFromFPNo"));
                    totalReturnFromFPQtyInKg = dsVD_Child.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ReturnFromFPQtyInKg"));
                    totalOtherNo = dsVD_Child.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OtherNo"));
                    totalOtherQtyInKg = dsVD_Child.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("OtherQtyInKg"));
                    totalTotalInFlowNo = totalOBNo + totalGheePackNo + totalReturnFromFPNo + totalOtherNo;
                    totalTotalInFlowQtyInKg = totalOBQtyInKg + totalGheePackQtyInKg + totalReturnFromFPQtyInKg + totalOtherQtyInKg;


                    sb.Append("<tr>");
                    sb.Append("<td><b>Total:</b></td>");
                    sb.Append("<td><b>" + totalOBNo + "</b></td>");
                    sb.Append("<td><b>" + totalOBQtyInKg + "</b></td>");
                    sb.Append("<td><b>" + totalGheePackNo + "</b></td>");
                    sb.Append("<td><b>" + totalGheePackQtyInKg + "</b></td>");
                    sb.Append("<td><b>" + totalReturnFromFPNo + "</b></td>");
                    sb.Append("<td><b>" + totalReturnFromFPQtyInKg + "</b></td>");
                    sb.Append("<td><b>" + totalOtherNo + "</b></td>");
                    sb.Append("<td><b>" + totalOtherQtyInKg + "</b></td>");
                    sb.Append("<td><b>" + totalTotalInFlowNo + "</b></td>");
                    sb.Append("<td><b>" + totalTotalInFlowQtyInKg + "</b></td>");
                    sb.Append("</tr>");

                    sb.Append("</table>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    divprint.InnerHtml = sb.ToString();
                    tblGheeIntermediate.InnerHtml = sb.ToString();


                    sb.Append("<div class='row'>");
                    sb.Append("<div class='row'>");
                    sb.Append("<div class='col-md-12'>");
                    sb.Append("<table class='table table-bordered' border='1'>");
                    sb.Append("<tr>");
                    //sb.Append("<td style='text-align:center'>"+Session["Office_Name"].ToString()+"</td>");
                    //sb.Append("<th style='text-align:center'>Ghee Sheet Entry Report</td>");
                    sb.Append("<th colspan='12'>OUT FLOW</th>");
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<th rowspan='2'></th>");
                    sb.Append("</tr>");
                    sb.Append("<th rowspan='2'>No of Cases</th>");
                    sb.Append("<th colspan='2'>Issue to FP</th>");
                    sb.Append("<th colspan='2'>Issue to Other</th>");
                    sb.Append("<th colspan='2'>Leakage Packing</th>");
                    sb.Append("<th colspan='2'>Closing Balance</th>");
                    sb.Append("<th colspan='2'>Total</th>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");



                    sb.Append("<th>Packet Size</th>");
                    sb.Append("<th>No's</th>");                   
                    sb.Append("<th>Qty in Kg</th>");
                    sb.Append("<th>No's</th>");
                    sb.Append("<th>Qty in Kg</th>");
                    sb.Append("<th>No's</th>");
                    sb.Append("<th>Qty in Kg</th>");
                    sb.Append("<th>No's</th>");
                    sb.Append("<th>Qty in Kg</th>");
                    sb.Append("<th>No's</th>");
                    sb.Append("<th>Qty in Kg</th>");
                    sb.Append("</tr>");

                    decimal totalIsuuetoFPNoofCases = 0, totalIsuuetoFPNo = 0, totalIsuuetoFPQtyInKg = 0, totalIsuuetoOtherNo = 0, totalIsuuetoOtherQtyInKg = 0, totalLeakagePackingNo = 0, totalLeakagePackingQtyInKg = 0, totalClosingBalanceNo = 0,
                        totalClosingBalanceQtyInKg = 0, totalTotalOutFlowNo = 0, totalTotalOutFlowQtyInKg = 0;
                    for (int i = 0; i < Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td>" + dsVD_Child.Tables[0].Rows[i]["ItemName"].ToString() + "</td>");
                        sb.Append("<td>" + dsVD_Child.Tables[0].Rows[i]["IsuuetoFPNoofCases"].ToString() + "</td>");
                        sb.Append("<td>" + dsVD_Child.Tables[0].Rows[i]["IsuuetoFPNo"].ToString() + "</td>");
                        sb.Append("<td>" + dsVD_Child.Tables[0].Rows[i]["IsuuetoFPQtyInKg"].ToString() + "</td>");
                        sb.Append("<td>" + dsVD_Child.Tables[0].Rows[i]["IsuuetoOtherNo"].ToString() + "</td>");
                        sb.Append("<td>" + dsVD_Child.Tables[0].Rows[i]["IsuuetoOtherQtyInKg"].ToString() + "</td>");
                        sb.Append("<td>" + dsVD_Child.Tables[0].Rows[i]["LeakagePackingNo"].ToString() + "</td>");
                        sb.Append("<td>" + dsVD_Child.Tables[0].Rows[i]["LeakagePackingQtyInKg"].ToString() + "</td>");
                        sb.Append("<td>" + dsVD_Child.Tables[0].Rows[i]["ClosingBalanceNo"].ToString() + "</td>");
                        sb.Append("<td>" + dsVD_Child.Tables[0].Rows[i]["ClosingBalanceQtyInKg"].ToString() + "</td>");
                        sb.Append("<td>" + (decimal.Parse(dsVD_Child.Tables[0].Rows[i]["IsuuetoFPNo"].ToString()) + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["IsuuetoOtherNo"].ToString()) + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["IsuuetoFPNo"].ToString()) + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["ClosingBalanceNo"].ToString())) + "</td>");
                        sb.Append("<td>" + (decimal.Parse(dsVD_Child.Tables[0].Rows[i]["IsuuetoFPQtyInKg"].ToString()) + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["IsuuetoOtherQtyInKg"].ToString()) + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["LeakagePackingQtyInKg"].ToString()) + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["ClosingBalanceQtyInKg"].ToString())) + "</td>");


                  //  //    sb.Append("<td>" + (decimal.Parse(dsVD_Child.Tables[0].Rows[i]["ItemName"].ToString()) + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["IsuuetoFPNoofCases"].ToString()) + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["IsuuetoFPNo"].ToString()) + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["IsuuetoFPQtyInKg"].ToString())
                  //  //        + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["IsuuetoOtherNo"].ToString()) + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["IsuuetoOtherQtyInKg"].ToString()) + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["ClosingBalanceNo"].ToString())
                  //  //        + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["ClosingBalanceQtyInKg"].ToString()) + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["TotalOutFlowNo"].ToString()) + decimal.Parse(dsVD_Child.Tables[0].Rows[i]["TotalOutFlowQtyInKg"].ToString())) + "</td>");

                      sb.Append("</tr>");
                  }
                    totalIsuuetoFPNoofCases = dsVD_Child.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("IsuuetoFPNoofCases"));
                    totalIsuuetoFPNo = dsVD_Child.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("IsuuetoFPNo"));
                    totalIsuuetoFPQtyInKg = dsVD_Child.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("IsuuetoFPQtyInKg"));
                    totalIsuuetoOtherNo = dsVD_Child.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("IsuuetoOtherNo"));
                    totalIsuuetoOtherQtyInKg = dsVD_Child.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("IsuuetoOtherQtyInKg"));
                    totalLeakagePackingNo = dsVD_Child.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("LeakagePackingNo"));
                    totalLeakagePackingQtyInKg = dsVD_Child.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("LeakagePackingQtyInKg"));
                    totalClosingBalanceNo = dsVD_Child.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ClosingBalanceNo"));
                    totalClosingBalanceQtyInKg = dsVD_Child.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("ClosingBalanceQtyInKg"));
                    totalTotalOutFlowNo = totalIsuuetoFPNo + totalIsuuetoOtherNo + totalLeakagePackingNo +  totalClosingBalanceNo;
                    totalTotalOutFlowQtyInKg = totalIsuuetoFPQtyInKg + totalIsuuetoOtherQtyInKg + totalLeakagePackingQtyInKg + totalClosingBalanceQtyInKg;


                    //totalTotalInFlowNo = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("totalTotalInFlowNo"));
                    //totalTotalInFlowQtyInKg = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("totalTotalInFlowQtyInKg"));


                    sb.Append("<tr>");
                    sb.Append("<td><b>Total:</td>");
                    sb.Append("<td><b>" + totalIsuuetoFPNoofCases + "<b></td>");
                    sb.Append("<td><b>" + totalIsuuetoFPNo + "<b></td>");
                    sb.Append("<td><b>" + totalIsuuetoFPQtyInKg + "<b></td>");
                    sb.Append("<td><b>" + totalIsuuetoOtherNo + "<b></td>");
                    sb.Append("<td><b>" + totalIsuuetoOtherQtyInKg + "<b></td>");
                    sb.Append("<td><b>" + totalLeakagePackingNo + "<b></td>");
                    sb.Append("<td><b>" + totalLeakagePackingQtyInKg + "<b></td>");
                    sb.Append("<td><b>" + totalClosingBalanceNo + "<b></td>");
                    sb.Append("<td><b>" + totalClosingBalanceQtyInKg + "<b></td>");
                    sb.Append("<td><b>" + totalTotalOutFlowNo + "<b></td>");
                    sb.Append("<td><b>" + totalTotalOutFlowQtyInKg + "<b></td>");

                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");

                    divprint.InnerHtml = sb.ToString();
                    tblGheeIntermediate.InnerHtml = sb.ToString();



                // GVVariantDetail_In.DataSource = dsVD_Child;
                // GVVariantDetail_In.DataBind();
                // GVVariantDetail_Out.DataSource = dsVD_Child;
                // GVVariantDetail_Out.DataBind();
                //gvinflow_print.DataSource = dsVD_Child;
                //gvinflow_print.DataBind();
                //gvoutflowprint.DataSource = dsVD_Child;
                //gvoutflowprint.DataBind();
                //GetTotal();
                }
                else
                {
                    //GVVariantDetail_In.DataSource = string.Empty;
                    //GVVariantDetail_In.DataBind();
                    //GVVariantDetail_Out.DataSource = string.Empty;
                    //GVVariantDetail_Out.DataBind();
                    //gvinflow_print.DataSource = string.Empty;
                    //gvinflow_print.DataBind();
                    //gvoutflowprint.DataSource = string.Empty;
                    //gvoutflowprint.DataBind();
                }
              
            }

            else
            {
                //gvinflow_print.DataSource = string.Empty;
                //gvinflow_print.DataBind();
                //gvoutflowprint.DataSource = string.Empty;
                //gvoutflowprint.DataBind();
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "VarientModelF_Product()", true);

        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    //protected void GVVariantDetail_In_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        GridView HeaderGrid = (GridView)sender;
    //        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //        TableCell HeaderCell0 = new TableCell();
    //        HeaderCell0.Text = "";
    //        HeaderCell0.ColumnSpan = 1;
    //        HeaderGridRow.Cells.Add(HeaderCell0);

    //        TableCell HeaderCell = new TableCell();
    //        HeaderCell.Text = "Opening Balance";
    //        HeaderCell.ColumnSpan = 2;
    //        HeaderCell.Style.Add("text-align", "center");
    //        HeaderGridRow.Cells.Add(HeaderCell);

    //        TableCell HeaderCell1 = new TableCell();
    //        HeaderCell1.Text = "Ghee Pack";
    //        HeaderCell1.ColumnSpan = 2;
    //        HeaderCell1.Style.Add("text-align", "center");
    //        HeaderGridRow.Cells.Add(HeaderCell1);

    //        TableCell HeaderCell2 = new TableCell();
    //        HeaderCell2.Text = "Return From FP";
    //        HeaderCell2.ColumnSpan = 2;
    //        HeaderCell2.Style.Add("text-align", "center");
    //        HeaderGridRow.Cells.Add(HeaderCell2);

    //        TableCell HeaderCell3 = new TableCell();
    //        HeaderCell3.Text = "Other";
    //        HeaderCell3.ColumnSpan = 2;
    //        HeaderCell3.Style.Add("text-align", "center");
    //        HeaderGridRow.Cells.Add(HeaderCell3);

    //        TableCell HeaderCell4 = new TableCell();
    //        HeaderCell4.Text = "Total";
    //        HeaderCell4.ColumnSpan = 2;
    //        HeaderCell4.Style.Add("text-align", "center");
    //        HeaderGridRow.Cells.Add(HeaderCell4);

            


    //        GVVariantDetail_In.Controls[0].Controls.AddAt(0, HeaderGridRow);

    //    }
    //}
    //protected void GVVariantDetail_Out_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        GridView HeaderGrid = (GridView)sender;
    //        GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            

    //        TableCell HeaderCell0 = new TableCell();
    //        HeaderCell0.Text = "";
    //        HeaderCell0.ColumnSpan = 1;
           
    //        HeaderGridRow.Cells.Add(HeaderCell0);

    //        TableCell HeaderCell5 = new TableCell();
    //        HeaderCell5.Text = "";
    //        HeaderCell5.ColumnSpan = 1;

    //        HeaderGridRow.Cells.Add(HeaderCell5);
            
    //        TableCell HeaderCell1 = new TableCell();
    //        HeaderCell1.Text = "Issue to FP";
    //        HeaderCell1.ColumnSpan = 2;
    //        HeaderCell1.Style.Add("text-align", "center");
    //        HeaderGridRow.Cells.Add(HeaderCell1);

    //        TableCell HeaderCell2 = new TableCell();
    //        HeaderCell2.Text = "Issue to Other";
    //        HeaderCell2.ColumnSpan = 2;
    //        HeaderCell2.Style.Add("text-align", "center");
    //        HeaderGridRow.Cells.Add(HeaderCell2);

    //        TableCell HeaderCell3 = new TableCell();
    //        HeaderCell3.Text = "Closing Balance";
    //        HeaderCell3.ColumnSpan = 2;
    //        HeaderCell3.Style.Add("text-align", "center");
    //        HeaderGridRow.Cells.Add(HeaderCell3);

    //        TableCell HeaderCell4 = new TableCell();
    //        HeaderCell4.Text = "Total";
    //        HeaderCell4.ColumnSpan = 2;
    //        HeaderCell4.Style.Add("text-align", "center");
    //        HeaderGridRow.Cells.Add(HeaderCell4);
           


    //        GVVariantDetail_Out.Controls[0].Controls.AddAt(0, HeaderGridRow);

    //    }
    //}
    protected void GetTotal()
    {
        //try
        //{
        //    lblMsg.Text = "";
        //    decimal OBNo = 0;
        //    decimal OBQtyInKg = 0;
        //    decimal GheePackNo = 0;
        //    decimal GheePackQtyInKg = 0;
        //    decimal ReturnFromFPNo = 0;
        //    decimal ReturnFromFPQtyInKg = 0;
        //    decimal OtherNo = 0;
        //    decimal OtherQtyInKg = 0;
        //    decimal TotalNo = 0;
        //    decimal TotalQtyInKg = 0;
            
        //    foreach (GridViewRow row1 in GVVariantDetail_In.Rows)
        //    {
        //        Label txtOBNo = (Label)row1.FindControl("txtOBNo");
        //        Label txtOBQtyInKg = (Label)row1.FindControl("txtOBQtyInKg");
        //        Label txtGheePackNo = (Label)row1.FindControl("txtGheePackNo");
        //        Label txtGheePackQtyInKg = (Label)row1.FindControl("txtGheePackQtyInKg");
        //        Label txtReturnFromFPNo = (Label)row1.FindControl("txtReturnFromFPNo");

        //        Label txtReturnFromFPQtyInKg = (Label)row1.FindControl("txtReturnFromFPQtyInKg");
        //        Label txtOtherNo = (Label)row1.FindControl("txtOtherNo");
        //        Label txtOtherQtyInKg = (Label)row1.FindControl("txtOtherQtyInKg");
        //        Label txtTotalNo = (Label)row1.FindControl("txtTotalNo");
        //        Label txtTotalQtyInKg = (Label)row1.FindControl("txtTotalQtyInKg");
        //        if (txtOBNo.Text == "")
        //        {
        //            txtOBNo.Text = "0";
        //        }
        //        if (txtOBQtyInKg.Text == "")
        //        {
        //            txtOBQtyInKg.Text = "0";
        //        }
        //        if (txtGheePackNo.Text == "")
        //        {
        //            txtGheePackNo.Text = "0";
        //        }
        //        if (txtGheePackQtyInKg.Text == "")
        //        {
        //            txtGheePackQtyInKg.Text = "0";
        //        }
        //        if (txtReturnFromFPNo.Text == "")
        //        {
        //            txtReturnFromFPNo.Text = "0";
        //        }

        //        if (txtReturnFromFPQtyInKg.Text == "")
        //        {
        //            txtReturnFromFPQtyInKg.Text = "0";
        //        }

        //        if (txtOtherNo.Text == "")
        //        {
        //            txtOtherNo.Text = "0";
        //        }
        //        if (txtOtherQtyInKg.Text == "")
        //        {
        //            txtOtherQtyInKg.Text = "0";
        //        }
        //        OBNo += decimal.Parse(txtOBNo.Text);
        //        OBQtyInKg += decimal.Parse(txtOBQtyInKg.Text);
        //        GheePackNo += decimal.Parse(txtGheePackNo.Text);
        //        GheePackQtyInKg += decimal.Parse(txtGheePackQtyInKg.Text);
        //        ReturnFromFPNo += decimal.Parse(txtReturnFromFPNo.Text);
        //        ReturnFromFPQtyInKg += decimal.Parse(txtReturnFromFPQtyInKg.Text);
        //        OtherNo += decimal.Parse(txtOtherNo.Text); 
        //        OtherQtyInKg += decimal.Parse(txtOtherQtyInKg.Text);
                

                
                
        //        txtTotalNo.Text = (Convert.ToDecimal(txtOBNo.Text) + Convert.ToDecimal(txtGheePackNo.Text) + Convert.ToDecimal(txtReturnFromFPNo.Text) + Convert.ToDecimal(txtOtherNo.Text)).ToString();
        //        txtTotalQtyInKg.Text = (Convert.ToDecimal(txtOBQtyInKg.Text) + Convert.ToDecimal(txtGheePackQtyInKg.Text) + Convert.ToDecimal(txtReturnFromFPQtyInKg.Text) + Convert.ToDecimal(txtOtherQtyInKg.Text) ).ToString();
        //        TotalNo += decimal.Parse(txtTotalNo.Text);
        //        TotalQtyInKg += decimal.Parse(txtTotalQtyInKg.Text);
        //        if (txtTotalNo.Text == "")
        //        {
        //            txtTotalNo.Text = "0";
        //        }
        //        if (txtTotalQtyInKg.Text == " Quantity In Kg")
        //        {
        //            txtTotalQtyInKg.Text = "0";
        //        }
               
        //    }
        //    GVVariantDetail_In.FooterRow.Cells[0].Text = "<b>Total : </b>";
        //    GVVariantDetail_In.FooterRow.Cells[1].Text = "<b>" + OBNo.ToString() + "</b>";
        //    GVVariantDetail_In.FooterRow.Cells[2].Text = "<b>" + OBQtyInKg.ToString() + "</b>";
        //    GVVariantDetail_In.FooterRow.Cells[3].Text = "<b>" + GheePackNo.ToString() + "</b>";
        //    GVVariantDetail_In.FooterRow.Cells[4].Text = "<b>" + GheePackQtyInKg.ToString() + "</b>";
        //    GVVariantDetail_In.FooterRow.Cells[5].Text = "<b>" + ReturnFromFPNo.ToString() + "</b>";

        //    GVVariantDetail_In.FooterRow.Cells[6].Text = "<b>" + ReturnFromFPQtyInKg.ToString() + "</b>";
        //    GVVariantDetail_In.FooterRow.Cells[7].Text = "<b>" + OtherNo.ToString() + "</b>";
        //    GVVariantDetail_In.FooterRow.Cells[8].Text = "<b>" + OtherQtyInKg.ToString() + "</b>";
        //    GVVariantDetail_In.FooterRow.Cells[9].Text = "<b>" + TotalNo.ToString() + "</b>";
        //    GVVariantDetail_In.FooterRow.Cells[10].Text = "<b>" + TotalQtyInKg.ToString() + "</b>";



           
        //    gvinflow_print.FooterRow.Cells[0].Text = "<b>Total : </b>";
        //    gvinflow_print.FooterRow.Cells[1].Text = "<b>" + OBNo.ToString() + "</b>";
        //    gvinflow_print.FooterRow.Cells[2].Text = "<b>" + OBQtyInKg.ToString() + "</b>";
        //    gvinflow_print.FooterRow.Cells[3].Text = "<b>" + GheePackNo.ToString() + "</b>";
        //    gvinflow_print.FooterRow.Cells[4].Text = "<b>" + GheePackQtyInKg.ToString() + "</b>";
        //    gvinflow_print.FooterRow.Cells[5].Text = "<b>" + ReturnFromFPNo.ToString() + "</b>";

        //    gvinflow_print.FooterRow.Cells[6].Text = "<b>" + ReturnFromFPQtyInKg.ToString() + "</b>";
        //    gvinflow_print.FooterRow.Cells[7].Text = "<b>" + OtherNo.ToString() + "</b>";
        //    gvinflow_print.FooterRow.Cells[8].Text = "<b>" + OtherQtyInKg.ToString() + "</b>";
        //    gvinflow_print.FooterRow.Cells[9].Text = "<b>" + TotalNo.ToString() + "</b>";
        //    gvinflow_print.FooterRow.Cells[10].Text = "<b>" + TotalQtyInKg.ToString() + "</b>";


        //    decimal IsuuetoFPNo = 0;
        //    decimal IsuuetoFPQtyInKg = 0;
        //    decimal IsuuetoOtherNo = 0;
        //    decimal IsuuetoOtherQtyInKg = 0;
        //    decimal CBNo = 0;
        //    decimal CBQtyInKg = 0;
        //    decimal OutFlowTotalNo = 0;
        //    decimal OutFlowTotalQtyInKg = 0;
           
        //    foreach (GridViewRow row1 in GVVariantDetail_Out.Rows)
        //    {

        //        Label txtIsuuetoFPNo = (Label)row1.FindControl("txtIsuuetoFPNo");
        //        Label txtIsuuetoFPQtyInKg = (Label)row1.FindControl("txtIsuuetoFPQtyInKg");
        //        Label txtIsuuetoOtherNo = (Label)row1.FindControl("txtIsuuetoOtherNo");
        //        Label txtIsuuetoOtherQtyInKg = (Label)row1.FindControl("txtIsuuetoOtherQtyInKg");
        //        Label txtCBNo = (Label)row1.FindControl("txtCBNo");
        //        Label txtCBQtyInKg = (Label)row1.FindControl("txtCBQtyInKg");
        //        Label txtOutFlowTotalNo = (Label)row1.FindControl("txtOutFlowTotalNo");
        //        Label txtOutFlowTotalQtyInKg = (Label)row1.FindControl("txtOutFlowTotalQtyInKg");

        //        if (txtIsuuetoFPNo.Text == "")
        //        {
        //            txtIsuuetoFPNo.Text = "0";
        //        }
        //        if (txtIsuuetoOtherNo.Text == "")
        //        {
        //            txtIsuuetoOtherNo.Text = "0";
        //        }
        //        if (txtIsuuetoFPQtyInKg.Text == "")
        //        {
        //            txtIsuuetoFPQtyInKg.Text = "0";
        //        }

        //        if (txtIsuuetoOtherQtyInKg.Text == "")
        //        {
        //            txtIsuuetoOtherQtyInKg.Text = "0";
        //        }

        //        if (txtCBNo.Text == "")
        //        {
        //            txtCBNo.Text = "0";
        //        }

        //        if (txtCBQtyInKg.Text == "")
        //        {
        //            txtCBQtyInKg.Text = "0";
        //        }
        //        IsuuetoFPNo += decimal.Parse(txtIsuuetoFPNo.Text);
        //        IsuuetoFPQtyInKg += decimal.Parse(txtIsuuetoFPQtyInKg.Text);
        //        IsuuetoOtherNo += decimal.Parse(txtIsuuetoOtherNo.Text);
        //        IsuuetoOtherQtyInKg += decimal.Parse(txtIsuuetoOtherQtyInKg.Text);
        //        CBNo += decimal.Parse(txtCBNo.Text);
        //        CBQtyInKg += decimal.Parse(txtCBQtyInKg.Text);
                
                


        //        txtOutFlowTotalNo.Text = (Convert.ToDecimal(txtIsuuetoFPNo.Text) + Convert.ToDecimal(txtIsuuetoOtherNo.Text) + Convert.ToDecimal(txtCBNo.Text)).ToString();
        //        txtOutFlowTotalQtyInKg.Text = (Convert.ToDecimal(txtIsuuetoFPQtyInKg.Text) + Convert.ToDecimal(txtIsuuetoOtherQtyInKg.Text) + Convert.ToDecimal(txtCBQtyInKg.Text)).ToString();
        //        OutFlowTotalNo += decimal.Parse(txtOutFlowTotalNo.Text);

        //        OutFlowTotalQtyInKg += decimal.Parse(txtOutFlowTotalQtyInKg.Text);
        //        if (txtOutFlowTotalNo.Text == "")
        //        {
        //            txtOutFlowTotalNo.Text = "0";
        //        }
                
        //    }
        //    GVVariantDetail_Out.FooterRow.Cells[0].Text = "<b>Total : </b>";
        //    GVVariantDetail_Out.FooterRow.Cells[2].Text = "<b>" + IsuuetoFPNo.ToString() + "</b>";
        //    GVVariantDetail_Out.FooterRow.Cells[3].Text = "<b>" + IsuuetoFPQtyInKg.ToString() + "</b>";
        //    GVVariantDetail_Out.FooterRow.Cells[4].Text = "<b>" + IsuuetoOtherNo.ToString() + "</b>";
        //    GVVariantDetail_Out.FooterRow.Cells[5].Text = "<b>" + IsuuetoOtherQtyInKg.ToString() + "</b>";
        //    GVVariantDetail_Out.FooterRow.Cells[6].Text = "<b>" + CBNo.ToString() + "</b>";

        //    GVVariantDetail_Out.FooterRow.Cells[7].Text = "<b>" + CBQtyInKg.ToString() + "</b>";
        //    GVVariantDetail_Out.FooterRow.Cells[8].Text = "<b>" + OutFlowTotalNo.ToString() + "</b>";
        //    GVVariantDetail_Out.FooterRow.Cells[9].Text = "<b>" + OutFlowTotalQtyInKg.ToString() + "</b>";



           
        //    gvoutflowprint.FooterRow.Cells[0].Text = "<b>Total : </b>";
        //    gvoutflowprint.FooterRow.Cells[2].Text = "<b>" + IsuuetoFPNo.ToString() + "</b>";
        //    gvoutflowprint.FooterRow.Cells[3].Text = "<b>" + IsuuetoFPQtyInKg.ToString() + "</b>";
        //    gvoutflowprint.FooterRow.Cells[4].Text = "<b>" + IsuuetoOtherNo.ToString() + "</b>";
        //    gvoutflowprint.FooterRow.Cells[5].Text = "<b>" + IsuuetoOtherQtyInKg.ToString() + "</b>";
        //    gvoutflowprint.FooterRow.Cells[6].Text = "<b>" + CBNo.ToString() + "</b>";

        //    gvoutflowprint.FooterRow.Cells[7].Text = "<b>" + CBQtyInKg.ToString() + "</b>";
        //    gvoutflowprint.FooterRow.Cells[8].Text = "<b>" + OutFlowTotalNo.ToString() + "</b>";
        //    gvoutflowprint.FooterRow.Cells[9].Text = "<b>" + OutFlowTotalQtyInKg.ToString() + "</b>";
           
            
        //}
        //catch (Exception ex)
        //{
        //    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        //}

    }

    protected void gvinflow_print_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell0 = new TableCell();
            HeaderCell0.Text = "InFlow";
            HeaderCell0.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell0);

            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Opening Balance";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.Style.Add("text-align", "center");
            HeaderGridRow.Cells.Add(HeaderCell);

            TableCell HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ghee Pack";
            HeaderCell1.ColumnSpan = 2;
            HeaderCell1.Style.Add("text-align", "center");
            HeaderGridRow.Cells.Add(HeaderCell1);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Return From FP";
            HeaderCell2.ColumnSpan = 2;
            HeaderCell2.Style.Add("text-align", "center");
            HeaderGridRow.Cells.Add(HeaderCell2);

            TableCell HeaderCell3 = new TableCell();
            HeaderCell3.Text = "Other";
            HeaderCell3.ColumnSpan = 2;
            HeaderCell3.Style.Add("text-align", "center");
            HeaderGridRow.Cells.Add(HeaderCell3);

            TableCell HeaderCell4 = new TableCell();
            HeaderCell4.Text = "Total";
            HeaderCell4.ColumnSpan = 2;
            HeaderCell4.Style.Add("text-align", "center");
            HeaderGridRow.Cells.Add(HeaderCell4);




            //gvinflow_print.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void gvoutflowprint_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);


            TableCell HeaderCell0 = new TableCell();
            HeaderCell0.Text = "OutFlow";
            HeaderCell0.ColumnSpan = 1;

            HeaderGridRow.Cells.Add(HeaderCell0);

            TableCell HeaderCell5 = new TableCell();
            HeaderCell5.Text = "";
            HeaderCell5.ColumnSpan = 1;

            HeaderGridRow.Cells.Add(HeaderCell5);

            TableCell HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Issue to FP";
            HeaderCell1.ColumnSpan = 2;
            HeaderCell1.Style.Add("text-align", "center");
            HeaderGridRow.Cells.Add(HeaderCell1);

            TableCell HeaderCell2 = new TableCell();
            HeaderCell2.Text = "Issue to Other";
            HeaderCell2.ColumnSpan = 2;
            HeaderCell2.Style.Add("text-align", "center");
            HeaderGridRow.Cells.Add(HeaderCell2);

            TableCell HeaderCell3 = new TableCell();
            HeaderCell3.Text = "Closing Balance";
            HeaderCell3.ColumnSpan = 2;
            HeaderCell3.Style.Add("text-align", "center");
            HeaderGridRow.Cells.Add(HeaderCell3);

            TableCell HeaderCell6 = new TableCell();
            HeaderCell6.Text = "Leakage Packing";
            HeaderCell6.ColumnSpan = 2;
            HeaderCell6.Style.Add("text-align", "center");
            HeaderGridRow.Cells.Add(HeaderCell6);

            TableCell HeaderCell4 = new TableCell();
            HeaderCell4.Text = "Total";
            HeaderCell4.ColumnSpan = 2;
            HeaderCell4.Style.Add("text-align", "center");
            HeaderGridRow.Cells.Add(HeaderCell4);



            //gvoutflowprint.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + "GheeSheetEntryNewRpt" + DateTime.Now + ".xls");
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
    public override void VerifyRenderingInServerForm(Control control)
    {

    

    }  
  
}