using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class mis_dailyplan_Rpt_CyclewiseDetailsofIncomingOutgoingTankers : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    StringBuilder sb = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                if (!IsPostBack)
                {
                    //txtFromDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                    //txtToDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    btnPrint.Visible = false;
                    btnExcel.Visible = false;
                    FillOffice();
                    GetSectionView();
                   // FillCC();
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
    protected void FillOffice()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("USP_MilkProductionEntry_New",
                  new string[] { "flag" },
                  new string[] { "11" }, "dataset");

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
    private void GetSectionView()
    {

        try
        {

            ds = null;

            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                 new string[] { "flag", "Office_ID", "OfficeType_ID" },
                 new string[] { "21", ddlDS.SelectedValue, objdb.OfficeType_ID() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPSection.DataSource = ds.Tables[0];
                ddlPSection.DataTextField = "ProductSection_Name";
                ddlPSection.DataValueField = "ProductSection_ID";
                ddlPSection.DataBind();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.SelectedValue = "1";
                ddlPSection.Enabled = false;

            }
            else
            {
                ddlPSection.Items.Clear();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    //protected void FillCC()
    //{
    //    try
    //    {
    //        ds = objdb.ByProcedure("USP_MilkProductionEntry_New", new string[] { "flag", "Office_ID" },
    //                  new string[] { "3", objdb.Office_ID() }, "dataset");

    //        ddlCCName.DataSource = ds.Tables[0];
    //        ddlCCName.DataTextField = "Office_Name";
    //        ddlCCName.DataValueField = "Office_ID";
    //        ddlCCName.DataBind();
    //        ddlCCName.Items.Insert(0, new ListItem("All", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
			btnPrint.Visible = false;
            btnExcel.Visible = false;
            DivDetail.InnerHtml = "";
            string flag = "";
            if(ddlIncOut.SelectedValue == "Incoming")
            {
                flag = "26";
            }
            else
            {
                flag = "27";
            }
            string[] MonthyearPart = txtFdt.Text.Split('/');
            string Month = MonthyearPart[0];
            string Year = MonthyearPart[1];
            ds = objdb.ByProcedure("Usp_Production_Milk_InOutProcess_New",
                new string[] { "flag", "Office_ID", "ProductSection_ID", "month", "Year", },
                new string[] { flag, ddlDS.SelectedValue, ddlPSection.SelectedValue, Month, Year }, "dataset");
            if (ds != null && ds.Tables[3].Rows.Count > 0)
            {
                btnPrint.Visible = true;
                btnExcel.Visible = true;
                sb.Append("<table class='table' style='border:1px dashed black' border='1'>");
                sb.Append("<tr>");
                sb.Append("<td style='text-align:center; border-top:1px dashed black' colspan='13'>"+Session["Office_Name"]+"</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                if (ddlIncOut.SelectedValue == "Incoming")
                {
                    sb.Append("<td style='text-align:center' colspan='13'>CYCLE WISE DETAILS OF INCOMING MILK TANKERS.</td>");
                }
                else
                {
                    sb.Append("<td style='text-align:center' colspan='13'>CYCLE WISE DETAILS OF OUTGOING MILK TANKERS.</td>");
                }
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<th  rowspan='2'>CC Code & Name</th>");
                sb.Append("<th style='text-align:center' colspan='3'><---01-10/" + Month + "/" + Year + "---></th>");
                sb.Append("<th style='text-align:center' colspan='3'><---11-20/" + Month + "/" + Year + "---></th>");
                sb.Append("<th style='text-align:center' colspan='3'><---21-End/" + Month + "/" + Year + "---></th>");
                sb.Append("<th style='text-align:center' colspan='3'><----TOTAL----></th>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                //sb.Append("<th>S.No.</th>");
                sb.Append("<th>QTY(KG)</th>");
                sb.Append("<th>FAT(KG)</th>");
                sb.Append("<th>SNF(KG)</th>");
                sb.Append("<th>QTY(KG)</th>");
                sb.Append("<th>FAT(KG)</th>");
                sb.Append("<th>SNF(KG)</th>");
                sb.Append("<th>QTY(KG)</th>");
                sb.Append("<th>FAT(KG)</th>");
                sb.Append("<th>SNF(KG)</th>");
                sb.Append("<th>QTY(KG)</th>");
                sb.Append("<th>FAT(KG)</th>");
                sb.Append("<th>SNF(KG)</th>");
                sb.Append("</tr>");
                int Count = ds.Tables[3].Rows.Count;
                for (int i = 0; i < Count; i++)
                {
                    sb.Append("<tr>");
                    //sb.Append("<td>"+(i+1)+"</td>");
                    sb.Append("<td>" + ds.Tables[3].Rows[i]["Office_Name"].ToString() + "</th>");
                    int CC_Id = int.Parse(ds.Tables[3].Rows[i]["Office_Id"].ToString());
                    if(ds.Tables[0].Rows.Count > 0)
                    {
                         DataView dv = new DataView();
			
                        dv = ds.Tables[0].DefaultView;
                        dv.RowFilter = "Office_Id =" + CC_Id.ToString() + "";
                        DataTable dt = dv.ToTable();
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow rs in dt.Rows)
                            {
                                sb.Append("<td>" + rs["RQtyInKg"].ToString() + "</td>");
                                sb.Append("<td>" + rs["RKgFat"].ToString() + "</td>");
                                sb.Append("<td>" + rs["RKgSnf"].ToString() + "</td>");
                            }
                        }
                        else
                        {
                            sb.Append("<td>0.00</td>");
                            sb.Append("<td>0.00</td>");
                            sb.Append("<td>0.00</td>");
                        }


                    }
                    else
                    {
                        sb.Append("<td>0.00</td>");
                        sb.Append("<td>0.00</td>");
                        sb.Append("<td>0.00</td>");
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {

                        DataView dv = new DataView();

                        dv = ds.Tables[1].DefaultView;
                        dv.RowFilter = "Office_Id =" + CC_Id.ToString() + "";
                        DataTable dt = dv.ToTable();
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow rs in dt.Rows)
                            {
                                sb.Append("<td>" + rs["RQtyInKg"].ToString() + "</td>");
                                sb.Append("<td>" + rs["RKgFat"].ToString() + "</td>");
                                sb.Append("<td>" + rs["RKgSnf"].ToString() + "</td>");
                            }
                        }
                        else
                        {
                            sb.Append("<td>0.00</td>");
                            sb.Append("<td>0.00</td>");
                            sb.Append("<td>0.00</td>");
                        }


                    }
                    else
                    {
                        sb.Append("<td>0.00</td>");
                        sb.Append("<td>0.00</td>");
                        sb.Append("<td>0.00</td>");
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {

                        DataView dv = new DataView();

                        dv = ds.Tables[2].DefaultView;
                        dv.RowFilter = "Office_Id =" + CC_Id.ToString() + "";
                        DataTable dt = dv.ToTable();
                        if(dt.Rows.Count > 0)
                        {
                            foreach (DataRow rs in dt.Rows)
                            {
                                sb.Append("<td>" + rs["RQtyInKg"].ToString() + "</td>");
                                sb.Append("<td>" + rs["RKgFat"].ToString() + "</td>");
                                sb.Append("<td>" + rs["RKgSnf"].ToString() + "</td>");
                            }
                        }
                        else
                        {
                            sb.Append("<td>0.00</td>");
                            sb.Append("<td>0.00</td>");
                            sb.Append("<td>0.00</td>");
                        }
                        


                    }
                    else
                    {
                        sb.Append("<td>0.00</td>");
                        sb.Append("<td>0.00</td>");
                        sb.Append("<td>0.00</td>");
                    }

                    sb.Append("<td>" + ds.Tables[3].Rows[i]["RQtyInKg"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[3].Rows[i]["RKgFat"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[3].Rows[i]["RKgSnf"].ToString() + "</td>");
                    sb.Append("</tr>");
                   
                }
                sb.Append("<tr>");
                sb.Append("<td><b>G R A N D T O T A L</b></td>");
                sb.Append("<td>" + ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("RQtyInKg")).ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("RKgFat")).ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("RKgSnf")).ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("RQtyInKg")).ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("RKgFat")).ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[1].AsEnumerable().Sum(row => row.Field<decimal>("RKgSnf")).ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("RQtyInKg")).ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("RKgFat")).ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("RKgSnf")).ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("RQtyInKg")).ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("RKgFat")).ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("RKgSnf")).ToString() + "</td>");
                sb.Append("</tr>");
                
                sb.Append("</table>");
                DivDetail.InnerHtml = sb.ToString();
            }
			 else
            {
                sb.Append("<table>");
                sb.Append("<tr>");
                sb.Append("<td>No Record Found</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                DivDetail.InnerHtml = sb.ToString();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment; filename=CCWiseIncomingoutgoingtankerdetails" + (System.DateTime.Now.ToString("dd-MM-yyyy_HH_mm_ss_fff")) + ".xls");
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite =
        new HtmlTextWriter(stringWrite);
        DivDetail.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }
}