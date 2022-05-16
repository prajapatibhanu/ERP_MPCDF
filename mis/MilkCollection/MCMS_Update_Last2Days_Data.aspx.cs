using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_mcms_reports_MCMS_Update_Last2Days_Data : System.Web.UI.Page
{
    APIProcedure apiprocedure = new APIProcedure();
    IFormatProvider cult = new CultureInfo("en-IN", true);
    DataSet ds, ds2;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            
            ddlCollectionType_SelectedIndexChanged(sender, e);
			FillGrid();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    private void FillGrid()
    {
        try
        {

            gvReceivedEntry.DataSource = null;
            gvReceivedEntry.DataBind();

            string FromDate = "", ToDate = "";
            if (txtFromDate.Text != "")
            {
                FromDate = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            }
            if (txtToDate.Text != "")
            {
                ToDate = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            }
            ds = null;
            ds = apiprocedure.ByProcedure("Usp_UpdateMCMS_Detail",
                                new string[] { "flag", "Office_ID ", "V_EntryType", "FromDate", "ToDate" },
                                new string[] { "1", ddlDSName3.SelectedValue, ddlCollectionType.SelectedValue, FromDate, ToDate }, "dataset");

            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count > 0)
            {
                gvReceivedEntry.DataSource = ds;
                gvReceivedEntry.DataBind();
            }
            else
            {
                gvReceivedEntry.DataSource = string.Empty;
                gvReceivedEntry.DataBind();
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void ddlCollectionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDSName3.Items.Clear();

            if (ddlCollectionType.SelectedValue == "In")
            {
                ddlDSName3.DataSource = apiprocedure.ByProcedure("SpAdminOffice",
                                  new string[] { "flag", "OfficeType_ID" },
                                  new string[] { "5", "2" }, "dataset");
                ddlDSName3.DataTextField = "Office_Name";
                ddlDSName3.DataValueField = "Office_ID";
                ddlDSName3.DataBind();
                ddlDSName3.Items.Insert(0, new ListItem("All", "0"));
                ddlDSName3.SelectedValue = apiprocedure.Office_ID();
                ddlDSName3.Enabled = false;
            }
            else if (ddlCollectionType.SelectedValue == "Out")
            {
                ddlDSName3.DataSource = apiprocedure.ByProcedure("Usp_MilkInwardOutwardDetails",
                         new string[] { "flag", "Office_Parant_ID" },
                         new string[] { "22", apiprocedure.Office_ID() }, "dataset");
                ddlDSName3.DataTextField = "Office_Name";
                ddlDSName3.DataValueField = "Office_ID";
                ddlDSName3.DataBind();
                ddlDSName3.Items.Insert(0, new ListItem("All", "0"));
                ddlDSName3.Enabled = true;
            }
            else
            {
                ddlDSName3.DataSource = string.Empty;
                ddlDSName3.DataBind();
                ddlDSName3.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblmsgpopup.Text = "";
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lnkbtnVN = (LinkButton)gvReceivedEntry.Rows[selRowIndex].FindControl("LinkButton1");
            Label lblI_EntryID = (Label)gvReceivedEntry.Rows[selRowIndex].FindControl("lblI_EntryID");
            Label lblBI_MilkInOutRefID = (Label)gvReceivedEntry.Rows[selRowIndex].FindControl("lblBI_MilkInOutRefID");
            Label lblChallanNo = (Label)gvReceivedEntry.Rows[selRowIndex].FindControl("lblChallanNo");
            Label lblV_SealLocation = (Label)gvReceivedEntry.Rows[selRowIndex].FindControl("lblV_SealLocation");

            if (lblV_SealLocation.Text == "Single Chamber")
            {
                lblV_SealLocation.Text = "S";
            }
            if (lblV_SealLocation.Text == "Front Chamber")
            {
                lblV_SealLocation.Text = "F";

            }
            if (lblV_SealLocation.Text == "Rear Chamber")
            {
                lblV_SealLocation.Text = "R";
            }


            ViewState["BI_MilkInOutRefID"] = lblBI_MilkInOutRefID.Text;
            ds = null;
            ds = apiprocedure.ByProcedure("Usp_UpdateMCMS_Detail",
                            new string[] { "flag", "I_EntryID", "V_ReferenceCode", "V_SealLocation" },
                            new string[] { "2", lblI_EntryID.Text, lblChallanNo.Text, lblV_SealLocation.Text }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lblVehicleNo.Text = ds.Tables[0].Rows[0]["ChallanNo"].ToString();

                string V_SealLocation = ds.Tables[0].Rows[0]["V_SealLocation"].ToString();
                string ReferenceNo = ds.Tables[0].Rows[0]["ReferenceNo"].ToString();
                ViewState["I_QualityEntryID"] = ds.Tables[0].Rows[0]["I_QualityEntryID"].ToString();
                

                if (V_SealLocation == "S")
                {
                    ddlCompartmentType.Items.Clear();
                    ddlCompartmentType.Items.Add(new ListItem("Single", "S"));
                    ddlCompartmentType.Enabled = false;
                }
                if (V_SealLocation == "F")
                {
                    ddlCompartmentType.Items.Clear();
                    ddlCompartmentType.Items.Add(new ListItem("Front", "F"));
                    ddlCompartmentType.Enabled = false;

                }
                if (V_SealLocation == "R")
                {
                    ddlCompartmentType.Items.Clear();
                    ddlCompartmentType.Items.Add(new ListItem("Rear", "R"));
                    ddlCompartmentType.Enabled = false;
                }

                ddlMilkQuality.SelectedValue = ds.Tables[0].Rows[0]["V_MilkQuality"].ToString();
                ddlCOB.SelectedValue = ds.Tables[0].Rows[0]["V_COB"].ToString();
                if (ds.Tables[0].Rows[0]["V_Alcohol"].ToString().Contains("Negative"))
                {
                    ddlAlcohol.SelectedValue = "Negative";
                }
                else
                {
                    ddlAlcohol.SelectedValue = ds.Tables[0].Rows[0]["V_Alcohol"].ToString();
                }
            


                txtMilkQuantity.Text = ds.Tables[0].Rows[0]["I_MilkQuantity"].ToString();
                txtTemprature.Text = ds.Tables[0].Rows[0]["V_Temp"].ToString();
                txtAcidity.Text = ds.Tables[0].Rows[0]["V_Acidity"].ToString();
                txtFat.Text = ds.Tables[0].Rows[0]["D_FAT_F"].ToString();
                txtCLR.Text = ds.Tables[0].Rows[0]["D_CLR"].ToString();
                txtSNF.Text = ds.Tables[0].Rows[0]["D_SNF"].ToString();
                txtMBRT.Text = ds.Tables[0].Rows[0]["V_MBRT"].ToString();
                txtAlcoholperc.Text = ds.Tables[0].Rows[0]["V_Alcohol"].ToString();
            }

            else
            {

            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowQCDetails();", true);


        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlMilkQuality_Init(object sender, EventArgs e)
    {
        ddlMilkQuality.DataSource = apiprocedure.ByProcedure("USP_Mst_MilkQualityList",
                                new string[] { "flag" },
                                new string[] { "1" }, "dataset");
        ddlMilkQuality.DataValueField = "V_MilkQualityList";
        ddlMilkQuality.DataTextField = "V_MilkQualityList";
        ddlMilkQuality.DataBind();
        ddlMilkQuality.Items.Insert(0, new ListItem("Select", "0"));
    }


    protected void txtMilkQuantity_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            decimal TC = 0;
            if (txtMilkQuantity.Text != "")
            {

            }
            else
            {
                lblMsg.Text = "";
                SetFocus(txtMilkQuantity);
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowQCDetails();", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }

    protected void btnAddQualityDetails_Click(object sender, EventArgs e)
    {
        try
        {

            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                string alcoholePerS = "";

                if (ddlAlcohol.SelectedValue == "Negative")
                {
                    alcoholePerS = ddlAlcohol.SelectedValue + "(" + txtAlcoholperc.Text + "%)";
                }
                else if (ddlAlcohol.SelectedValue == "Positive")
                {
                    alcoholePerS = "Positive";
                }
                else
                {
                    alcoholePerS = "0";
                }

                ds = null;
                ds = apiprocedure.ByProcedure("Usp_UpdateMCMS_Detail",
                                            new string[] { "Flag"
                                                    ,"I_MilkQuantity"
                                                    ,"V_SealLocation"
                                                    ,"D_FAT"
                                                    ,"D_SNF"
                                                    ,"D_CLR"
                                                    ,"V_Temp"
                                                    ,"V_Acidity"
                                                    ,"V_COB"
                                                    ,"V_Alcohol"
                                                    ,"V_MBRT"
                                                    ,"V_MilkQuality"
                                                    ,"I_QualityEntryID" 
                                                    ,"I_CreatedByEmpID"
                                                    ,"BI_MilkInOutRefID"
                                        },
                                            new string[] { "3" ,
                                                    txtMilkQuantity.Text,
                                                    ddlCompartmentType.SelectedValue,
                                                    txtFat.Text,
                                                    txtSNF.Text,
                                                    txtCLR.Text,
                                                    txtTemprature.Text, 
                                                    txtAcidity.Text,
                                                    ddlCOB.SelectedValue,
                                                    alcoholePerS,
                                                    txtMBRT.Text == "" ? "0" : txtMBRT.Text,
                                                    ddlMilkQuality.SelectedValue,
                                                    ViewState["I_QualityEntryID"].ToString(),
                                                    apiprocedure.createdBy(),
                                                    ViewState["BI_MilkInOutRefID"].ToString()
                                        },
                                           new string[] { },
                                           new DataTable[] { }, "TableSave");


                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    FillGrid();
                    lblmsgpopup.Text = apiprocedure.Alert("fa-check", "alert-success", "Thank You!", "Record Updated Successfully");

                }
                else
                {
                    FillGrid();
                    lblmsgpopup.Text = apiprocedure.Alert("fa-check", "alert-warning", "warning!", "Some Thing Went Wrong!");

                }


                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowQCDetails();", true);
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowQCDetails();", true);
            lblmsgpopup.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 2:" + ex.Message.ToString());
            Session["IsSuccess"] = false;
        }
    }


    protected void txtCLR_TextChanged(object sender, EventArgs e)
    {
        txtSNF.Text = GetSNF().ToString();

        if (txtCLR.Text == "" || txtFat.Text == "")
        { txtSNF.Text = ""; }

        if (txtCLR.Text == "")
        {
            txtCLR.Focus();
        }

        if (txtFat.Text == "")
        {
            txtFat.Focus();
        }

        if (txtCLR.Text != "" && txtFat.Text != "")
        {
            txtMBRT.Focus();
        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowQCDetails();", true);
    }

    private decimal GetSNF()
    {
        decimal snf = 0, clr = 0, fat = 1;
        try
        {
            //Formula for Get SNF as given below:
            if (txtCLR.Text != "")
            { clr = Convert.ToDecimal(txtCLR.Text); }
            if (txtFat.Text != "")
            { fat = Convert.ToDecimal(txtFat.Text); }

            //SNF = (CLR / 4) + (FAT * 0.2) + 0.7 -- Formula Before
            //SNF = (CLR / 4) + (0.25 * FAT) + 0.44 -- Formula After Mail dt: 27 Jan 2020, 16:26
            snf = ((clr / 4) + (fat * Convert.ToDecimal(0.25)) + Convert.ToDecimal(0.44));
        }
        catch (Exception ex)
        {
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
        return Math.Round(snf, 2);

    }
     
    protected void ddlAlcohol_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtAlcoholperc.Text = "";

            if (ddlAlcohol.SelectedValue == "0")
            {
                DivAlcoholper.Visible = false;
            }

            if (ddlAlcohol.SelectedValue == "Positive")
            {
                DivAlcoholper.Visible = false;
            }

            if (ddlAlcohol.SelectedValue == "Negative")
            {
                DivAlcoholper.Visible = true;
            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ShowQCDetails();", true);
        }
        catch (Exception ex)
        {
            DivAlcoholper.Visible = false;
            lblMsg.Text = apiprocedure.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

}