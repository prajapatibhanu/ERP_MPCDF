using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class mis_Mis_Reports_MDMisReport : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                txtToDate.Attributes.Add("readonly", "readonly");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                FillDetail();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void FillDetail()
    {
        try
        {
            lblMsg.Text = "";
            int RowNo = 1;
            ds = objdb.ByProcedure("USP_MIS_MDReport",
                new string[] { "flag", "ToDate" },
                  new string[] { "1", Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<table Class='table table-bordered table-hover'>");
                sb.Append("<thead class='report-header'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='9' style='text-align:center; font-size:18px;'><b>" + Session["Office_Name"].ToString() + "</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td colspan='9' style='text-align:center; font-size:18px;'><b>MD Report of : " + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd/MM/yyyy") + "</b></td>");
                sb.Append("</tr>");
                sb.Append("</thead>");
                sb.Append("<tr>");
                sb.Append("<th>S.No.</th>");
                sb.Append("<th>Particulars</th>");
                sb.Append("<th>BDS</th>");
                sb.Append("<th>IDS</th>");
                sb.Append("<th>UDS</th>");
                sb.Append("<th>GDS</th>");
                sb.Append("<th>JDS</th>");
                sb.Append("<th>BKDS</th>");
                sb.Append("<th>Total</th>");
                sb.Append("</tr>");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    sb.Append("<tr>");
                    sb.Append("<td><b>" + RowNo + "</b></td>");
                    sb.Append("<td>Reporting Date</td>");
                    sb.Append("<td>" + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd/MM/yyyy") + "</td>");
                    sb.Append("<td>" + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd/MM/yyyy") + "</td>");
                    sb.Append("<td>" + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd/MM/yyyy") + "</td>");
                    sb.Append("<td>" + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd/MM/yyyy") + "</td>");
                    sb.Append("<td>" + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd/MM/yyyy") + "</td>");
                    sb.Append("<td>" + Convert.ToDateTime(txtToDate.Text, cult).ToString("dd/MM/yyyy") + "</td>");
                    sb.Append("<td></td>");
                    sb.Append("</tr>");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        decimal Total = 0;
                        sb.Append("<tr>");
                        if (ds.Tables[0].Rows[i]["Header"].ToString() == "MainHeader")
                        {
                            RowNo = RowNo + 1;
                            sb.Append("<td><b>" + (RowNo).ToString() + "</b></td>");
                        }
                        else
                        {
                            sb.Append("<td></td>");
                        }
                        sb.Append("<td>" + ds.Tables[0].Rows[i]["ColumnName"].ToString() + "</td>");
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {
                            if (ds.Tables[0].Columns[j].ToString() != "ColumnName" && ds.Tables[0].Columns[j].ToString() != "Header")
                            {
                                string Columns = ds.Tables[0].Columns[j].ToString();
                                if (ds.Tables[0].Rows[i][Columns] == DBNull.Value || ds.Tables[0].Rows[i][Columns].ToString() == null)
                                {
                                    sb.Append("<td>0</td>");

                                    //sb.Append("<td>0</td>");
                                    //sb.Append("<td>0</td>");
                                    //sb.Append("<td>0</td>");
                                    //sb.Append("<td>0</td>");
                                    //sb.Append("<td>0</td>");
                                }
                                else
                                {
                                    sb.Append("<td>" + ds.Tables[0].Rows[i][Columns].ToString() + "</td>");
                                    Total += Convert.ToDecimal(ds.Tables[0].Rows[i][Columns].ToString());
                                    //sb.Append("<td>" + ds.Tables[0].Rows[i]["GDS"].ToString() + "</td>");
                                    //sb.Append("<td>" + ds.Tables[0].Rows[i]["IDS"].ToString() + "</td>");
                                    //sb.Append("<td>" + ds.Tables[0].Rows[i]["JDS"].ToString() + "</td>");
                                    //sb.Append("<td>" + ds.Tables[0].Rows[i]["UDS"].ToString() + "</td>");
                                    //sb.Append("<td>" + ds.Tables[0].Rows[i]["BKDS"].ToString() + "</td>");
                                }
                            }
                        }
                        sb.Append("<td><b>" + Total.ToString() + "</b></td>");
                        sb.Append("</tr>");
                    }
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    sb.Append("<tr>");
                    sb.Append("<td></td>");
                    sb.Append("<td>" + ds.Tables[1].Rows[0]["ColumnName"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[1].Rows[0]["BDS"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[1].Rows[0]["GDS"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[1].Rows[0]["IDS"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[1].Rows[0]["JDS"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[1].Rows[0]["UDS"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[1].Rows[0]["BKDS"].ToString() + "</td>");
                    sb.Append("<td></td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                DivDetail.InnerHtml = sb.ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            FillDetail();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! ", ex.Message.ToString());
        }
    }
}