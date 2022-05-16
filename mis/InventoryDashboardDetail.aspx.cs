using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class mis_InventoryDashboardDetail : System.Web.UI.Page
{
    DataSet ds, ds1, ds2;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    IFormatProvider culture = new CultureInfo("en-US", true);
    CommanddlFill objddl = new CommanddlFill();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try 
            {
                string PanelID = Request.QueryString["TableID"];
                if (PanelID.ToString() == "Rc7")
                {
                    Rc7.Visible = true;
                }
                else
                {
                    Rc7.Visible = false;
                }
                if (PanelID.ToString() == "Rc30")
                {
                    Rc30.Visible = true;
                }
                else
                {
                    Rc30.Visible = false;
                }
                if (PanelID.ToString() == "Item7")
                {
                    Item7.Visible = true;
                }
                else
                {
                    Item7.Visible = false;
                }
                if (PanelID.ToString() == "Item30")
                {
                    Item30.Visible = true;
                }
                else
                {
                    Item30.Visible = false;
                }
                if (PanelID.ToString() == "Item30")
                {
                    Item30.Visible = true;
                }
                else
                {
                    Item30.Visible = false;
                }
                if (PanelID.ToString() == "TodayMilk")
                {
                    TodayMilk.Visible = true;
                }
                else
                {
                    TodayMilk.Visible = false;
                }
                if (PanelID.ToString() == "Stock")
                {
                    Stock.Visible = true;
                }
                else
                {
                    Stock.Visible = false;
                }
                if (PanelID.ToString() == "Report")
                {
                    Report.Visible = true;
                }
                else
                {
                    Report.Visible = false;
                }
                if (PanelID.ToString() == "MilkProduction")
                {
                    MilkProduction.Visible = true;
                }
                else
                {
                    MilkProduction.Visible = false;
                }
                if (PanelID.ToString() == "LowStock")
                {
                    PanelLowStock.Visible = true;
                }
                else
                {
                    PanelLowStock.Visible = false;
                }

                
                
                
                

                
            }
            catch (Exception ex)
            {
               // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
    }
}