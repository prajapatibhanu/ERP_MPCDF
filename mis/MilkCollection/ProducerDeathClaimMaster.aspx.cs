using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Collections.Generic;
using System.Web.UI;
using System.Web;

public partial class mis_MilkCollection_ProducerDeathClaimMaster : System.Web.UI.Page
{
    DataSet ds;
    static DataSet ds5;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!IsPostBack)
            {
                txtDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDate.Attributes.Add("readonly", "readonly");

                if (objdb.Office_ID() == "2")
                {
                    ddlBillingCycle.SelectedValue = "5 days";
                }
                else
                {
                    ddlBillingCycle.SelectedValue = "10 days";
                }

                txtDate_TextChanged(sender, e);

                FillGrid();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];

    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (txtDate.Text != "")
            {
                string BillingCycle = ddlBillingCycle.SelectedItem.Text;
                string[] DatePart = txtDate.Text.Split('/');
                int Day = int.Parse(DatePart[0]);
                int Month = int.Parse(DatePart[1]);
                int Year = int.Parse(DatePart[2]);
                string SelectedFromDate = "";
                string SelectedToDate = "";
                if (BillingCycle == "5 days")
                {
                    if (Day >= 1 && Day < 6)
                    {
                        SelectedFromDate = "01";
                        SelectedToDate = "05";
                    }
                    else if (Day > 5 && Day < 11)
                    {
                        SelectedFromDate = "6";
                        SelectedToDate = "10";
                    }
                    else if (Day > 10 && Day < 16)
                    {
                        SelectedFromDate = "11";
                        SelectedToDate = "15";
                    }
                    else if (Day > 15 && Day < 21)
                    {
                        SelectedFromDate = "16";
                        SelectedToDate = "20";
                    }
                    else if (Day > 20 && Day < 26)
                    {
                        SelectedFromDate = "21";
                        SelectedToDate = "25";
                    }
                    else if (Day > 25 && Day <= 31)
                    {
                        SelectedFromDate = "26";
                        if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12)
                        {
                            SelectedToDate = "31";
                        }
                        else if (Month == 2)
                        {
                            SelectedToDate = "28";
                        }
                        else
                        {
                            SelectedToDate = "30";
                        }

                    }
                    SelectedFromDate = SelectedFromDate + "/" + Month + '/' + Year;
                    SelectedToDate = SelectedToDate + "/" + Month + '/' + Year;

                }
                else
                {
                    if (Day >= 1 && Day < 11)
                    {
                        SelectedFromDate = "01";
                        SelectedToDate = "10";
                    }
                    else if (Day > 10 && Day < 21)
                    {
                        SelectedFromDate = "11";
                        SelectedToDate = "20";
                    }
                    else if (Day > 20 && Day <= 31)
                    {
                        SelectedFromDate = "21";
                        if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12)
                        {
                            SelectedToDate = "31";
                        }
                        else if (Month == 2)
                        {
                            SelectedToDate = "28";
                        }
                        else
                        {
                            SelectedToDate = "30";
                        }
                    }

                    SelectedFromDate = SelectedFromDate + "/" + Month + '/' + Year;
                    SelectedToDate = SelectedToDate + "/" + Month + '/' + Year;
                }
                txtBillingCycleFromDate.Text = Convert.ToDateTime(SelectedFromDate, cult).ToString("dd/MM/yyyy");
                txtBillingCycleToDate.Text = Convert.ToDateTime(SelectedToDate, cult).ToString("dd/MM/yyyy");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void ddlBillingCycle_TextChanged(object sender, EventArgs e)
    {
        txtDate_TextChanged(sender, e);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    ds = objdb.ByProcedure("Usp_ProducerDeathClaim", 
                                            new string[] {"flag",
                                                          "Office_ID",
                                                          "EntryDate",
                                                          "BillingPeriodFromDate",
                                                          "BillingPeriodToDate",
                                                          "Amount",
                                                          "IsActive",
                                                          "CreatedBy",
                                                          "CreatedAt",
                                                          "CreatedByIp"
                                                          },
                                            new string[] {"1",
                                                          objdb.Office_ID(),
                                                          Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                                          Convert.ToDateTime(txtBillingCycleFromDate.Text,cult).ToString("yyyy/MM/dd"),
                                                          Convert.ToDateTime(txtBillingCycleToDate.Text,cult).ToString("yyyy/MM/dd"),
                                                          txtDeathClaimAmount.Text,
                                                          "1",
                                                          objdb.createdBy(),
                                                          objdb.Office_ID(),
                                                          objdb.GetLocalIPAddress()
                                                         }, "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                            {
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "oops!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                        }
                    }
                    FillGrid();
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
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
            GridView1.DataSource = string.Empty;
            GridView1.DataBind();
            ds = objdb.ByProcedure("Usp_ProducerDeathClaim", new string[] { "flag", "Office_ID" }, new string[] {"2",objdb.Office_ID() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
}