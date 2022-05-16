using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

public partial class mis_dailyplan_MilkOpeningStockForProduction : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    string Fdate = "";
    string Tdate = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                lblMsg.Text = "";

                if (!IsPostBack)
                {
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    txtDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                    FillOffice();
                    FillShift();
                    btnSubmit_Click(sender, e);

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
            ds = objdb.ByProcedure("SpAdminOffice",
                 new string[] { "flag" },
                 new string[] { "12" }, "dataset");

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

    protected void FillShift()
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                 new string[] { "flag" },
                 new string[] { "2" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlShift.DataSource = ds.Tables[0];
                ddlShift.DataTextField = "Name";
                ddlShift.DataValueField = "Shift_Id";
                ddlShift.DataBind();
                ddlShift.Items.Insert(0, new ListItem("All", "0"));

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            lblMsg.Text = "";

            DataSet dsGV = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID", "Shift_Id", "FDate" },
                  new string[] { "1", objdb.Office_ID(), ddlShift.SelectedValue, Fdate }, "dataset");

            if (dsGV != null && dsGV.Tables[0].Rows.Count > 0)
            {
                gvReceivedMilkDetail.DataSource = dsGV.Tables[0];
                gvReceivedMilkDetail.DataBind();

            }
            else
            {
                gvReceivedMilkDetail.DataSource = string.Empty;
                gvReceivedMilkDetail.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        try
        {
            ds = null;
            if (txtDate.Text != "")
            {
                Fdate = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");
            }

            lblMsg.Text = "";

            DataSet dsGV1 = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID", "Shift_Id", "FDate" },
                  new string[] { "1", objdb.Office_ID(), ddlShift.SelectedValue, Fdate }, "dataset");

            if (dsGV1.Tables.Count > 0 && dsGV1.Tables[1].Rows.Count > 0)
            {
                GVCC.DataSource = dsGV1.Tables[1];
                GVCC.DataBind();

                decimal MilkQty_InKG = 0;
                decimal MilkFAT_InKG = 0;
                decimal MilkSNF_InKG = 0;

                foreach (GridViewRow row in GVCC.Rows)
                {
                    // Milk QTY  
                    Label txtgv_mqty = (Label)row.FindControl("lblCr");
                    if (txtgv_mqty.Text != "")
                    {
                        MilkQty_InKG += Convert.ToDecimal(txtgv_mqty.Text);
                        Label lblTMQty = (GVCC.FooterRow.FindControl("lblTMQty") as Label);
                        lblTMQty.Text = MilkQty_InKG.ToString("0.00");
                    }

                    // Milk FAT 
                    Label txtgvV_Fat = (Label)row.FindControl("lblFat");
                    if (txtgvV_Fat.Text != "")
                    {
                        MilkFAT_InKG += Convert.ToDecimal(txtgvV_Fat.Text);
                        Label lblTFQty = (GVCC.FooterRow.FindControl("lblTFQty") as Label);
                        lblTFQty.Text = MilkFAT_InKG.ToString("0.000");
                    }

                    // Milk SNF 
                    Label txtgvV_Snf = (Label)row.FindControl("lblSnf");
                    if (txtgvV_Snf.Text != "")
                    {
                        MilkSNF_InKG += Convert.ToDecimal(txtgvV_Snf.Text);
                        Label lblTSQty = (GVCC.FooterRow.FindControl("lblTSQty") as Label);
                        lblTSQty.Text = MilkSNF_InKG.ToString("0.000");
                    }

                }

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CCModelF()", true);
            }


        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    protected void btnCrossButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("MilkOpeningStockForProduction.aspx", false);
    }

}