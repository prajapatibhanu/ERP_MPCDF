using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_dailyplan_DailyProduction : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
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
                FillShift();
                FillProductSection();
                txtOrderDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtOrderDate.Attributes.Add("ReadOnly", "ReadOnly");
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }
    protected void FillShift()
    {
        try
        {
            ddlShift.Items.Insert(0, new ListItem("Select", "0"));
            ds = objdb.ByProcedure("USP_Mst_ShiftMaster", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlShift.DataSource = ds;
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataBind();
                ddlShift.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
    protected void FillProductSection()
    {
        try
        {

            lblMsg.Text = "";
            ddlProductSection.Items.Clear();
            ds = objdb.ByProcedure("SpProductionProduct_DS_Mapping",
                 new string[] { "flag", "Office_ID" },
                 new string[] { "0", ddlDS.SelectedValue.ToString() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlProductSection.DataSource = ds.Tables[0];
                ddlProductSection.DataTextField = "ProductSection_Name";
                ddlProductSection.DataValueField = "ProductSection_ID";
                ddlProductSection.DataBind();

            }
            ddlProductSection.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        FillGrid();
    }

    protected void ddlDS_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillProductSection();
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlProductSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
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
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();

            ViewState["SelectedOffice"] = ddlDS.SelectedValue.ToString();
            ViewState["SelectedSection"] = ddlProductSection.SelectedValue.ToString();
            ViewState["TranDt"] = Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd");
            ViewState["Shift_ID"] = ddlShift.SelectedValue.ToString();
            //ViewState["BatchNo"] = txtBatchNo.Text.ToString();
            //ViewState["LotNo"] = txtLotNo.Text.ToString();
            //ViewState["Remarks"] = txtRemark.Text.ToString();

            ds = objdb.ByProcedure("spProductionDaily", new string[] { "flag", "SenderOffice_ID", "ReceiverSection_ID", "TranDt", "Shift_id" }, new string[] { "0", ddlDS.SelectedValue.ToString(), ddlProductSection.SelectedValue.ToString(), ViewState["TranDt"].ToString(), ViewState["Shift_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
            if (ds != null)
            {
                btnSave.Visible = true;
            }

            /********************************/

            lblSelectedDate.Text = Convert.ToDateTime(ViewState["TranDt"], cult).ToString("dd-MM-yyyy");
            GridView2.DataSource = new string[] { };
            GridView2.DataBind();

            ds = objdb.ByProcedure("spProductionDaily", new string[] { "flag", "SenderOffice_ID", "ReceiverSection_ID", "TranDt", "Shift_id" }, new string[] { "5", ddlDS.SelectedValue.ToString(), ddlProductSection.SelectedValue.ToString(),ViewState["TranDt"].ToString(), ViewState["Shift_ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                GridView2.DataSource = ds.Tables[0];
                GridView2.DataBind();
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
          
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                Label ItemName = (Label)row.FindControl("ItemName");
                string Item_id = ItemName.ToolTip.ToString();
                TextBox ProductionQuantiy = (TextBox)row.FindControl("txtProductionQuantiy");
                TextBox txtQcQauntity = (TextBox)row.FindControl("txtQcQauntity");
                TextBox txtQcSample = (TextBox)row.FindControl("txtQcSample");

                TextBox txtBatchNo = (TextBox)row.FindControl("txtBatchNo");
                TextBox txtLotNo = (TextBox)row.FindControl("txtLotNo");

                Label lblSingleFat = (Label)row.FindControl("lblSingleFat");
                Label lblSingleSNF = (Label)row.FindControl("lblSingleSNF");



                if (chkSelect.Checked == true && int.Parse(ProductionQuantiy.Text)>0)
                {
                    float TotalFat = float.Parse(lblSingleFat.Text) * float.Parse(ProductionQuantiy.Text);
                    float TotalSnf = float.Parse(lblSingleSNF.Text) * float.Parse(ProductionQuantiy.Text);

                    ds = objdb.ByProcedure("spProductionDaily",
                        new string[] { "flag", "SenderOffice_ID", "SenderSection_ID", "Item_id", "Inward", "SenderID", "TranDt", "BatchNo", "LotNo", "TotalFat", "TotalSnf", "Shift_ID", "ReceivingRemark" },
                        new string[] { "1", ViewState["SelectedOffice"].ToString(), ViewState["SelectedSection"].ToString(), Item_id, ProductionQuantiy.Text, ViewState["Emp_ID"].ToString(), ViewState["TranDt"].ToString(), txtBatchNo.Text, txtLotNo.Text, TotalFat.ToString(), TotalSnf.ToString(), ViewState["Shift_ID"].ToString(), "" }, "dataset");

                    if (int.Parse(txtQcQauntity.Text) > 0)
                    {
                        if (ds != null && ds.Tables[0].Rows.Count != 0)
                        {
                            string ItmStock_id = ds.Tables[0].Rows[0]["ItmStock_id"].ToString();
                            string LabId = "1";
                            string TestType = "QC";
                            float TotalFatQc = float.Parse(lblSingleFat.Text) * float.Parse(txtQcQauntity.Text);
                            float TotalSnfQc = float.Parse(lblSingleSNF.Text) * float.Parse(txtQcQauntity.Text);

                            objdb.ByProcedure("spProduction_LabTesting",
                            new string[] { "flag", "SampleBy_Office", "SampleBy_Section", "Item_id", "SampleQuantity", "SampleBy_ID", "SampleDate", "SampleBatch_No", "SampleLot_No", "TotalFat", "TotalSnf", "Shift_ID","Test_Type","Lab_ID","SampleName","ItmStock_id" },
                            new string[] { "0", ViewState["SelectedOffice"].ToString(), ViewState["SelectedSection"].ToString(), Item_id, txtQcQauntity.Text, ViewState["Emp_ID"].ToString(), ViewState["TranDt"].ToString(), txtBatchNo.Text, txtLotNo.Text, TotalFatQc.ToString(), TotalSnfQc.ToString(), ViewState["Shift_ID"].ToString(), TestType, LabId, txtQcSample.Text, ItmStock_id }, "dataset");
                        }
                    }

                }

            }

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string ItmStock_id = GridView2.DataKeys[e.RowIndex].Value.ToString();
            objdb.ByProcedure("spProductionDaily",
                   new string[] { "flag", "ItmStock_id" },
                   new string[] { "6", ItmStock_id }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }    
}