using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_DailyProductionGraphicalReport : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {

                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Office_FinAddress"] = Session["Office_FinAddress"].ToString();
                    
                  
                    FillDropdown();

                    string currentMonth = DateTime.Now.Month.ToString();
                    string currentYear = DateTime.Now.Year.ToString();
                    ddlMonth.SelectedValue = currentMonth;
                    ddlYear.SelectedValue = currentYear;
                    lblheadingFirst.Visible = false;
                    lblheadingFirst.Text = ViewState["Office_FinAddress"].ToString() + "<br/> Production Report";
                   
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDropdown()
    {
        try
        {


            ddlitems.DataSource = objdb.ByProcedure("Sp_tblSpItemStock",
                                       new string[] { "flag", "Office_Id", "ItemCat_id", "ItemType_id" },
                                       new string[] { "3", ViewState["Office_ID"].ToString(), "1", "102" }, "Dataset");
            ddlitems.DataTextField = "ItemName";
            ddlitems.DataValueField = "Item_id";
            ddlitems.DataBind();
            ddlitems.Items.Insert(0, new ListItem("Select", "0"));


            ds = objdb.ByProcedure("SpHrYear_Master", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = ds;
                ddlYear.DataTextField = "Year";
                ddlYear.DataValueField = "Year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
   
    protected void FillGrid()
    {
        try
        {
            lblheadingFirst.Visible = false;
            DivChart.InnerHtml = "";
            if (ddlYear.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0  && ddlitems.SelectedIndex > 0 )
            {

                ds = objdb.ByProcedure("spFinDailyProduction",
                                    new string[] { "flag", "ItemID", "Office_ID", "MonthNo", "Year" },
                                    new string[] { "6", ddlitems.SelectedValue.ToString(), ViewState["Office_ID"].ToString(),ddlMonth.SelectedValue.ToString(),ddlYear.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count != 0)
                {
                    lblheadingFirst.Text = ViewState["Office_FinAddress"].ToString();
                    StringBuilder sb1 = new StringBuilder();

                    /*********Google Chart********/
                    //sb1.Append("<div id='curve_chart' style='width: 1000px; height: 400px'></div>");
                    //sb1.Append("<script type='text/javascript'>");
                    //sb1.Append("google.charts.load('current', { 'packages': ['corechart'] });");
                    //sb1.Append("google.charts.setOnLoadCallback(drawChart);");
                    //sb1.Append("function drawChart() {");
                    //sb1.Append("var data = google.visualization.arrayToDataTable([");
                    //sb1.Append(" ['Days', 'Production Cumulative Quantity', 'Sale Cumulative Quantity'],");

                    ////sb1.Append(" ['2004', 1000, 400],");
                    ////sb1.Append(" ['2005', 1170, 460],");
                    ////sb1.Append(" ['2008', 660, 1120]");
                    //int rowcount = ds.Tables[0].Rows.Count;
                    //for (int i = 0; i < rowcount; i++)
                    //{
                    //    if (i > 0)
                    //        sb1.Append(",");

                    //    sb1.Append(" ['" + ds.Tables[0].Rows[i]["DateDay"].ToString() + "', " + ds.Tables[0].Rows[i]["ProductionCumulativeQuantity"].ToString() + ", " + ds.Tables[0].Rows[i]["SaleCumulativeQuantity"].ToString() + "]");
                    //}
                    //sb1.Append(" ]);");
                    //sb1.Append(" var options = {");
                    //// sb1.Append("  title: 'Company Performance',");
                    //sb1.Append("  curveType: 'function',");
                    //sb1.Append("  legend: { position: 'bottom' }");
                    //sb1.Append("  };");
                    //sb1.Append("  var chart = new google.visualization.LineChart(document.getElementById('curve_chart'));");
                    //sb1.Append("  chart.draw(data, options);");
                    //sb1.Append("   }");
                    //sb1.Append("   </script>");


                    /**********Other Chart*********/
//                    

                    sb1.Append(" <canvas id='mixed-chart' width='800' height='450'></canvas>");
                    sb1.Append(" <script>");
                    sb1.Append("new Chart(document.getElementById('mixed-chart'), {");
                    sb1.Append("type: 'bar',");
                    sb1.Append("data: {");
                    int rowcount = ds.Tables[0].Rows.Count;
                    string Days = "";
                    string ProductionCumulativeQuantity = "";
                    string SaleCumulativeQuantity = "";

                    for (int i = 0; i < rowcount; i++)
                    {
                        if (i > 0)
                        {
                            //sb1.Append(",");
                            Days += ", ";
                            ProductionCumulativeQuantity += ", ";
                            SaleCumulativeQuantity += ", ";
                        }
                        //else
                        //{
                        //    Days += "'0', ";
                        //    ProductionCumulativeQuantity += "0, ";
                        //    SaleCumulativeQuantity += "0, ";
                        //}
                        Days += "'" + ds.Tables[0].Rows[i]["DateDay"].ToString() + "'";
                        ProductionCumulativeQuantity += ds.Tables[0].Rows[i]["ProductionCumulativeQuantity"].ToString();
                        SaleCumulativeQuantity += ds.Tables[0].Rows[i]["SaleCumulativeQuantity"].ToString();

                      
                    }

                  //  sb1.Append("labels: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17'],");
                    sb1.Append("labels: [" + Days + "],");
                    sb1.Append("datasets: [{");
                    sb1.Append("label: 'Production Cumulative Quantity',");
                    sb1.Append("type: 'line',");
                    sb1.Append("borderColor: '#f707ce',");
                    //sb1.Append("data: [408, 547, 675, 734, 408, 547, 675, 734, 408, 547, 675, 734, 408, 547, 675, 734,0],");
                    sb1.Append("data: [" + ProductionCumulativeQuantity + "],");
                    sb1.Append("fill: false");
                    sb1.Append("}, {");
                    sb1.Append("label: 'Sale Cumulative Quantity',");
                    sb1.Append("type: 'line',");
                    sb1.Append("borderColor: '#f19708',");
                   // sb1.Append("data: [133, 221, 783, 2478, 133, 10, 783, 2478, 20, 40, 783, 2478, 133, 221, 783, 2478,5],");
                    sb1.Append("data: [" + SaleCumulativeQuantity + "],");
                    sb1.Append("fill: false");
                    sb1.Append("}");
                    sb1.Append("]");
                    sb1.Append("},");
                    sb1.Append("options: {");
                    sb1.Append("title: {");
                    sb1.Append("display: true,");
                  //  sb1.Append("text: 'Population growth (millions): Europe & Africa'");
                    sb1.Append("text: '" + ddlitems.SelectedItem.ToString() + " [ Production Chart " + ddlMonth.SelectedItem.ToString() + " " + ddlYear.SelectedItem.ToString() + " ] '");
                    sb1.Append("},");
                    sb1.Append("legend: { display: false }");
                    sb1.Append("}");
                    sb1.Append("});");
                    sb1.Append("</script>");
                    sb1.Append("<div class='col-md-2'></div><div class='col-md-8'><table> <tr><td><div style='height: 15px; width: 15px; background-color: #f707ce;'></div></td><td>&nbsp;Production Cumulative Quantity&nbsp;&nbsp;</td><td><div style='height: 15px; width: 15px; background-color: #f19708;'></div></td><td>&nbsp;Sale Cumulative Quantity</td> </tr></table></div> <div class='col-md-2'></div>");
                    DivChart.InnerHtml = sb1.ToString();
                    
                    lblheadingFirst.Visible = true;
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
            string msg = "";
            if (ddlYear.SelectedIndex == 0)
            {
                msg += "Select Year. \\n";
            }
            if (ddlMonth.SelectedIndex == 0)
            {
                msg += "Select Month. \\n";
            }
            if (ddlitems.SelectedIndex == 0)
            {
                msg += "Select Item Name. \\n";
            }

            if (msg.Trim() == "")
            {


                FillGrid();

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

}