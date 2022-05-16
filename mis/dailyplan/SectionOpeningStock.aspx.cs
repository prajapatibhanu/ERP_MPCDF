using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_dailyplan_SectionOpeningStock : System.Web.UI.Page
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
                FillProductSection();
                txtOrderDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
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
            ds = objdb.ByProcedure("spProductionItemStock", new string[] { "flag", "ReceiverOffice_ID", "ReceiverSection_ID" }, new string[] { "0", ddlDS.SelectedValue.ToString(), ddlProductSection.SelectedValue.ToString() }, "dataset");
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
            GridView2.DataSource = new string[] { };
            GridView2.DataBind();

            ds = objdb.ByProcedure("spProductionItemStock", new string[] { "flag", "ReceiverOffice_ID", "ReceiverSection_ID" }, new string[] { "5", ddlDS.SelectedValue.ToString(), ddlProductSection.SelectedValue.ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                GridView2.DataSource = ds.Tables[0];
                GridView2.DataBind();
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView2.UseAccessibleHeader = true;
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
            //ViewState["SelectedOffice"] = ddlProductSection.SelectedValue.ToString();
            //ViewState["SelectedSection"] = ddlProductSection.SelectedValue.ToString();
            ViewState["TranDt"] = Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd");
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                Label ItemName = (Label)row.FindControl("ItemName");
                string Item_id = ItemName.ToolTip.ToString();
                TextBox LastTarget = (TextBox)row.FindControl("txtTarget");
                TextBox BatchNo = (TextBox)row.FindControl("txtBatch");
                TextBox LotNo = (TextBox)row.FindControl("txtLot");
                Label lblSingleFat = (Label)row.FindControl("lblSingleFat");
                Label lblSingleSNF = (Label)row.FindControl("lblSingleSNF");
                Label lblItemType_id = (Label)row.FindControl("lblItemType_id");
                Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");
                
                

                if (chkSelect.Checked == true)
                {
                    float TotalFat = float.Parse(lblSingleFat.Text) * float.Parse(LastTarget.Text);
                    float TotalSnf = float.Parse(lblSingleSNF.Text) * float.Parse(LastTarget.Text);

                    objdb.ByProcedure("spProductionItemStock",
                        new string[] { "flag", "ReceiverOffice_ID", "ReceiverSection_ID", "Item_id", "Inward", "ReceiverID", "TranDt", "BatchNo", "LotNo", "TotalFat", "TotalSnf", "ItemCat_id", "ItemType_id" },
                        new string[] { "1", ViewState["SelectedOffice"].ToString(), ViewState["SelectedSection"].ToString(), Item_id, LastTarget.Text, ViewState["Emp_ID"].ToString(), ViewState["TranDt"].ToString(), BatchNo.Text, LotNo.Text, TotalFat.ToString(), TotalSnf.ToString(),lblItemCat_id.Text, lblItemType_id.Text }, "dataset");
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
            objdb.ByProcedure("spProductionItemStock",
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