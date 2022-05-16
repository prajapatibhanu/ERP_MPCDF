using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_DailyProductionReport : System.Web.UI.Page
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
                    //  FillGrid();

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
           
                //ds = objdb.ByProcedure("spFinDailyProduction",
                //                    new string[] { "flag", "ItemID", "FromDate", "ToDate", "Office_ID" },
                //                    new string[] { "2", ddlitems.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString() }, "dataset");

                ds = objdb.ByProcedure("spFinDailyProduction",
                                       new string[] { "flag", "ItemID", "Office_ID", "MonthNo", "Year" },
                                       new string[] { "7", ddlitems.SelectedValue.ToString(), ViewState["Office_ID"].ToString(), ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {

                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;

                    StringBuilder sb1 = new StringBuilder();
                    sb1.Append("<div id='piechart_3d' style='width: 360px; height: 200px;'></div>");
                    sb1.Append("<script type='text/javascript'>");
                    sb1.Append("google.charts.load('current', { packages: ['corechart'] });");
                    sb1.Append("google.charts.setOnLoadCallback(drawChart);");
                    sb1.Append("function drawChart() {");
                    sb1.Append("var data = google.visualization.arrayToDataTable([");
                    sb1.Append("  ['Type', 'Value'],");
                    sb1.Append("  ['Non Production', " + ds.Tables[1].Rows[0]["AvailableCapacity"].ToString() + "],");
                    sb1.Append("  ['Production Cumulative', " + ds.Tables[1].Rows[0]["ProductionCumulativeQuantity"].ToString() + "]");
                    sb1.Append("]);");
                    sb1.Append("var options = {");
                    sb1.Append("title: 'Production Cumulative Chart',");
                    sb1.Append("is3D: true,");
                    sb1.Append("};");
                    sb1.Append("var chart = new google.visualization.PieChart(document.getElementById('piechart_3d'));");
                    sb1.Append("chart.draw(data, options);");
                    sb1.Append("}");
                    sb1.Append("</script>");
                   
                    DivChart.InnerHtml = sb1.ToString();

                   // lblheadingFirst.Text = ViewState["Office_FinAddress"].ToString() + "<br/>Production Report <br/>  " + txtFromDate.Text + " To " + txtToDate.Text;
                    lblheadingFirst.Visible = true;
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
            
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            lblMsg.Text = "";
            string msg = "";
			DivChart.InnerHtml ="";
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