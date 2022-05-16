using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Globalization;

public partial class mis_Production_Rpt_RokadBahiReport : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                txtFromDate.Attributes.Add("readonly", "readonly");
                txtTodate.Attributes.Add("readonly", "readonly");
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void FillDetail()
    {
        lblMsg.Text = "";
        StringBuilder html = new StringBuilder();
        html.Append("<table class='table table-bordered'>");
        html.Append("<thead>");
        html.Append("<tr>");
        html.Append("<th colspan='4' style='text-align: center'>जमा विवरण</th>");
        html.Append("<th colspan='4' style='text-align: center'>नामे विवरण</th>");
        html.Append("</tr>");
        html.Append("<tr>");
        html.Append("<th style='text-align: center'>दिनांक</th>");
        html.Append("<th style='text-align: center'>खाता</th>");
        html.Append("<th style='text-align: center'>विवरण</th>");
        html.Append("<th style='text-align: center'>राशि</th>");
        html.Append("<th style='text-align: center'>दिनांक</th>");
        html.Append("<th style='text-align: center'>खाता</th>");
        html.Append("<th style='text-align: center'>विवरण</th>");
        html.Append("<th style='text-align: center'>राशि</th>");
        html.Append("</tr>");
        html.Append("</thead>");
        html.Append("<tbody>");
        DataSet dsCal = objdb.ByProcedure("USP_tblRokadBahiEntry", new string[] { "flag", "fromDate", "Todate" },
                       new string[] { "8", Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTodate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
        if (dsCal != null && dsCal.Tables[0].Rows.Count > 0)
        {
            for (int k = 0; k < dsCal.Tables[0].Rows.Count; k++)
            {
                ds = objdb.ByProcedure("USP_tblRokadBahiEntry", new string[] { "flag", "fromDate" },
                      new string[] { "4", dsCal.Tables[0].Rows[k]["dddd"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables.Count > 0)
                    {
                        int TCount = 0, LoopCount = 0;
                        int Count1 = ds.Tables[0].Rows.Count;
                        int Count2 = ds.Tables[1].Rows.Count;
                        if (Count1 >= Count2)
                        {
                            TCount = Count1;
                        }
                        else
                        {
                            TCount = Count2;
                        }
                        for (int i = 0; i < TCount; i++)
                        {
                            LoopCount = LoopCount + 1;
                            html.Append("<tr>");
                            if (LoopCount > ds.Tables[0].Rows.Count)
                            {
                                html.Append("<td></td>");
                                html.Append("<td></td>");
                                html.Append("<td></td>");
                                html.Append("<td></td>");
                            }
                            else
                            {
                                html.Append("<td>" + ds.Tables[0].Rows[i]["Date"].ToString() + "</td>");
                                html.Append("<td>" + ds.Tables[0].Rows[i]["AccountName"].ToString() + "</td>");
                                html.Append("<td>" + ds.Tables[0].Rows[i]["Description"].ToString() + "</td>");
                                html.Append("<td>" + ds.Tables[0].Rows[i]["Amount"].ToString() + "</td>");
                            }
                            if (LoopCount > ds.Tables[1].Rows.Count)
                            {
                                html.Append("<td></td>");
                                html.Append("<td></td>");
                                html.Append("<td></td>");
                                html.Append("<td></td>");

                            }
                            else
                            {
                                html.Append("<td>" + ds.Tables[1].Rows[i]["Date"].ToString() + "</td>");
                                html.Append("<td>" + ds.Tables[1].Rows[i]["AccountName"].ToString() + "</td>");
                                html.Append("<td>" + ds.Tables[1].Rows[i]["Description"].ToString() + "</td>");
                                html.Append("<td>" + ds.Tables[1].Rows[i]["Amount"].ToString() + "</td>");
                            }
                            html.Append("</tr>");
                        }
                    }
                    html.Append("<tr>");
                    html.Append("<td></td>");
                    html.Append("<td></td>");
                    html.Append("<th>कुल योग</th>");
                    html.Append("<th>" + ds.Tables[0].Rows[0]["KulYog"].ToString() + "</th>");
                    html.Append("<td></td>");
                    html.Append("<td></td>");
                    html.Append("<th>व्यय योग</th>");
                    html.Append("<th>" + ds.Tables[1].Rows[0]["VyayYog"].ToString() + "</th>");
                    html.Append("</tr>");
                    html.Append("<tr>");
                    html.Append("<td></td>");
                    html.Append("<td></td>");
                    html.Append("<td></td>");
                    html.Append("<td></td>");
                    html.Append("<td></td>");
                    html.Append("<td></td>");
                    html.Append("<th>कुल योग</th>");
                    html.Append("<th>" + ds.Tables[0].Rows[0]["KulYog"].ToString() + "</th>");
                    html.Append("</tr>");
                    html.Append("<tr>");
                    html.Append("<td></td>");
                    html.Append("<td></td>");
                    html.Append("<td></td>");
                    html.Append("<td></td>");
                    html.Append("<td></td>");
                    html.Append("<td></td>");
                    html.Append("<th>अंतिम रोकड़</th>");
                    html.Append("<th>" + ds.Tables[1].Rows[0]["AntimRokad"].ToString() + "</th>");
                    html.Append("</tr>");
                }
            }
        }
        html.Append("</tbody>");
        html.Append("</table>");
        DivReport.InnerHtml = html.ToString();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtFromDate.Text != "" && txtTodate.Text != "")
            {
                FillDetail();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}