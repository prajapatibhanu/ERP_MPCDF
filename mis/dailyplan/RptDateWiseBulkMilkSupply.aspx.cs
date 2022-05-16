using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_dailyplan_RptDateWiseBulkMilkSupply : System.Web.UI.Page
{
    DataSet ds, ds2;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillOffice();
                HideshowUnionOrThirdParty();
                txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtFromDate.Attributes.Add("ReadOnly", "ReadOnly");
                txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtToDate.Attributes.Add("ReadOnly", "ReadOnly");

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
            DataSet ds1 = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
                 new string[] { "flag", "Office_ID" },
                 new string[] { "1", objdb.Office_ID() }, "dataset");

            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                ddlUnion.DataSource = ds1.Tables[0];
                ddlUnion.DataTextField = "Office_Name";
                ddlUnion.DataValueField = "Office_ID";
                ddlUnion.DataBind();
                ddlUnion.Items.Insert(0, new ListItem("Select", "0"));

            }
            else
            {
                ddlUnion.Items.Clear();
                ddlUnion.Items.Insert(0, new ListItem("Select", "0"));
                ddlUnion.DataBind();
            }
            DataSet ds2 = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
                 new string[] { "flag", "Office_ID" },
                 new string[] { "2", objdb.Office_ID() }, "dataset");

            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                ddlThirdparty.DataSource = ds2.Tables[0];
                ddlThirdparty.DataTextField = "ThirdPartyUnion_Name";
                ddlThirdparty.DataValueField = "ThirdPartyUnion_Id";
                ddlThirdparty.DataBind();
                ddlThirdparty.Items.Insert(0, new ListItem("Select", "0"));



            }
            else
            {
                ddlThirdparty.Items.Clear();
                ddlThirdparty.Items.Insert(0, new ListItem("Select", "0"));
                ddlThirdparty.DataBind();
            }
            DataSet ds3 = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
                 new string[] { "flag" },
                 new string[] { "4" }, "dataset");

            if (ds3 != null && ds3.Tables[0].Rows.Count > 0)
            {
                ddlMDP.DataSource = ds3.Tables[0];
                ddlMDP.DataTextField = "Office_Name";
                ddlMDP.DataValueField = "Office_ID";
                ddlMDP.DataBind();
                ddlMDP.Items.Insert(0, new ListItem("Select", "0"));



            }
            else
            {
                ddlMDP.Items.Clear();
                ddlMDP.Items.Insert(0, new ListItem("Select", "0"));
                ddlMDP.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        lblMsg.Text = "";
        string msg = "";
       
        if (txtFromDate.Text == "")
        {
            msg += "Select From Datee. \\n";
        }
        if (txtToDate.Text == "")
        {
            msg += "Select To Date. \\n";
        }
       

        if (msg == "")
        {

            FillGrid();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
        }
    }

    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = new string[]{};
            GridView1.DataBind();
            string TransferToffice_ID = "";
            string MilkTransferTypeID = rbtnTransferType.SelectedValue;
            string TransferFromOffice_ID = ViewState["Office_ID"].ToString();
            if(MilkTransferTypeID == "1")
            {
                TransferToffice_ID = ddlUnion.SelectedValue;
            }
            else if (MilkTransferTypeID == "2")
            {
                TransferToffice_ID = ddlThirdparty.SelectedValue;
            }
            else
            {
                TransferToffice_ID = ddlMDP.SelectedValue;
            }


            ds = objdb.ByProcedure("SpBulkMilkSaleToUnionOrThirdParty",
                new string[] { "flag", "SaleFromOffice_Id ", "SaleToOffice_Id", "MilkTrasferType", "FromDate", "ToDate"},
                new string[] { "3", TransferFromOffice_ID, TransferToffice_ID,MilkTransferTypeID, Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd")}, "dataset");

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                }
                
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void HideshowUnionOrThirdParty()
    {
        try
        {
            ddlThirdparty.ClearSelection();
            ddlUnion.ClearSelection();
            ddlMDP.ClearSelection();
            lblMsg.Text = "";
            if (rbtnTransferType.SelectedIndex == 0)
            {
                union.Visible = true;
                thirdparty.Visible = false;
                MDP.Visible = false;

            }
            else if (rbtnTransferType.SelectedIndex == 1)
            {
                MDP.Visible = false;
                union.Visible = false;
                thirdparty.Visible = true;
            }
            else
            {
                MDP.Visible = true;
                union.Visible = false;
                thirdparty.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void rbtnsaleto_SelectedIndexChanged(object sender, EventArgs e)
    {
        HideshowUnionOrThirdParty();
    }

}