using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class mis_mcms_reports_Mcms_Seal_Report_CC : System.Web.UI.Page
{
    APIProcedure apiprocedure = new APIProcedure();
    IFormatProvider cult = new CultureInfo("en-IN", true);
    DataSet ds;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.SessionID != null)
        {
            if (!IsPostBack)
            {

                txtCCWiseTankerSealReport.Text = DateTime.Now.ToString("dd/MM/yyyy");
                GetDS(sender, e);
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    protected void btnCCWiseTankerQCReport_Click(object sender, EventArgs e)
    {
        try
        {
            //Usp_ComparisionReports
            ds = apiprocedure.ByProcedure("Usp_ComparisionReports",
                                  new string[] { "flag", "OfficeID ", "DT_Date" },
                                  new string[] { "4", ddlCCName3.SelectedValue, Convert.ToDateTime(txtCCWiseTankerSealReport.Text, cult).ToString("yyyy/MM/dd") }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_CCWiseTankerSealReport.DataSource = ds;
                gv_CCWiseTankerSealReport.DataBind();
            }
            else
            {
                gv_CCWiseTankerSealReport.DataSource = null;
                gv_CCWiseTankerSealReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 6:" + ex.Message.ToString());
        }
    }

    public string GetChamberInfo(string ChallanNo)
    {
        try
        {

            DataSet dsct = apiprocedure.ByProcedure("Usp_ComparisionReports",
                                           new string[] { "flag", "V_ReferenceCode" },
                                           new string[] { "8", ChallanNo }, "dataset");
            if (dsct.Tables.Count != 0)
            {
                return dsct.Tables[0].Rows[0]["ChamberType"].ToString();
            }
            else
            {
                return "";
            }
        }
        catch (Exception)
        {

            return "";
        }

    }

    public string Getcc_Count(string ChallanNo)
    {
        try
        {

            DataSet dsccc = apiprocedure.ByProcedure("Usp_ComparisionReports",
                                           new string[] { "flag", "V_ReferenceCode" },
                                           new string[] { "8", ChallanNo }, "dataset");

            if (dsccc.Tables.Count != 0)
            {
                return dsccc.Tables[0].Rows[0]["CC_Count"].ToString();
            }
            else
            {
                return "";
            }
        }
        catch (Exception)
        {

            return "";
        }

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ShowTankerSealDetails")
            {
                //Usp_ComparisionReports
                ds = apiprocedure.ByProcedure("Usp_ComparisionReports",
                                      new string[] { "flag", "OfficeID", "DT_Date", "V_ReferenceCode" },
                                      new string[] { "5", ddlCCName3.SelectedValue, Convert.ToDateTime(txtCCWiseTankerSealReport.Text, cult).ToString("yyyy/MM/dd"), e.CommandArgument.ToString() }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    lblVehicleNo.Text = e.CommandArgument.ToString();

                    lblctype.Text = GetChamberInfo(e.CommandArgument.ToString());
                    lblcccount.Text = Getcc_Count(e.CommandArgument.ToString());

                    gvTankerSealDetailsForCC.DataSource = ds.Tables[0]; //For CC
                    gvTankerSealDetailsForCC.DataBind();

                    gvTankerSealDetailsForDS.DataSource = ds.Tables[1]; //For DS
                    gvTankerSealDetailsForDS.DataBind();

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowTankerSealDetails();", true);
                }
                else
                {
                    gvTankerSealDetailsForCC.DataSource = null;
                    gvTankerSealDetailsForCC.DataBind();

                    gvTankerSealDetailsForDS.DataSource = null;
                    gvTankerSealDetailsForDS.DataBind();

                    lblMsg.Text = apiprocedure.Alert("fa-info", "alert-info", "Info!", "No Data Found!");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 8:" + ex.Message.ToString());
        }
    }

    protected void ddlDSName3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDSName3.SelectedIndex != 0)
            {
                ddlCCName3.DataSource = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                              new string[] { "flag", "Office_Parant_ID" },
                              new string[] { "22", ddlDSName3.SelectedValue }, "dataset");
                ddlCCName3.DataTextField = "Office_Name";
                ddlCCName3.DataValueField = "Office_ID";
                ddlCCName3.DataBind();
                ddlCCName3.Items.Insert(0, new ListItem("Select", "0"));
                ddlCCName3.SelectedValue = apiprocedure.Office_ID();
                ddlCCName3.Enabled = false;

            }
            else
            {

                ddlCCName3.DataSource = string.Empty;
                ddlCCName3.DataBind();
                ddlCCName3.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 7:" + ex.Message.ToString());
        }
    }

    protected void GetDS(object sender, EventArgs e)
    {
        try
        {
            DataSet ds1 = apiprocedure.ByProcedure("SpAdminOffice",
                                  new string[] { "flag", "OfficeType_ID" },
                                  new string[] { "5", "2" }, "dataset");

            ddlDSName3.DataSource = ds1;
            ddlDSName3.DataTextField = "Office_Name";
            ddlDSName3.DataValueField = "Office_ID";
            ddlDSName3.DataBind();
            ddlDSName3.Items.Insert(0, new ListItem("Select", "0"));

            DataSet dspID = apiprocedure.ByProcedure("SpAdminOffice",
                                  new string[] { "flag", "Office_ID" },
                                  new string[] { "22", apiprocedure.Office_ID() }, "dataset");
           
            if (dspID.Tables.Count != 0)
            {
                ddlDSName3.SelectedValue = dspID.Tables[0].Rows[0]["Office_Parant_ID"].ToString(); 
                ddlDSName3.Enabled = false; 
                ddlDSName3_SelectedIndexChanged(sender, e);
                ddlCCName3.Enabled = false;

            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1:" + ex.Message.ToString());
        }
    }

}