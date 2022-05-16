using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_DCSInformationSystem_Monthly_Mini_Dairy_Plant_Report_Feeded_by_Manager_MDP : System.Web.UI.Page
{
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                getyear();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    public void getyear()
    {
        try
        {

            DataSet dsyear = new DataSet();
            dsyear = objdb.ByProcedure("Get_year_and_month"
                   , new string[] { "flag" }
                   , new string[] { "0" }, "dataset");
            if (dsyear != null)
            {
                if (dsyear.Tables.Count > 0)
                {
                    if (dsyear.Tables[0].Rows.Count > 0)
                    {
                        ddlyear.Items.Clear();
                        ddlyear.DataSource = dsyear.Tables[0];
                        ddlyear.DataTextField = "Year_sel";
                        ddlyear.DataValueField = "Year_sel";
                        ddlyear.DataBind();
                        //CODE CHANGES STARTED BY AJAY ON 13-JUN-2019
                        //ddlDOA.Items.Insert(0, "--Select DOA--");
                        //  ddlyear.Items.Insert(0, DateTime.Now.Year.ToString());
                        ddlyear.Enabled = true;
                    }

                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Found");
                        lblMsg.Focus();
                        //panelasset.Visible = false;

                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void DDlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}