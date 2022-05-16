using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_MilkCollection_UpdateUnionWiseBillingHeadCode : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    APIProcedure objdb1 = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["ItemBillingHead_ID"] = "0";
                    FillGrid();
                    lblMsg.Text = "";
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlBillingHeadName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtBillingHeadNameAlias.Text = "";
            txtItemBillingHead_Code.Text = "";
            if (ddlBillingHeadName.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("USP_Mst_ItemBillingHeadMaster", new string[] { "flag", "ItemBillingHead_ID", "Office_ID" }, new string[] {"10",ddlBillingHeadName.SelectedValue,objdb1.Office_ID() }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txtBillingHeadNameAlias.Text = ds.Tables[0].Rows[0]["ItemBillingHeadAlias_Name"].ToString();
                        txtItemBillingHead_Code.Text = ds.Tables[0].Rows[0]["ItemBillingHead_Code"].ToString();
                        ViewState["ItemBillingHeadChild_ID"] = ds.Tables[0].Rows[0]["ItemBillingHeadChild_ID"].ToString();
                    }
                }
            }
            ddlBillingHeadName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
       
    }
    protected void ddlItemBillingHead_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtBillingHeadNameAlias.Text = "";
            txtItemBillingHead_Code.Text = "";
            ddlBillingHeadName.Items.Clear();
            if (ddlItemBillingHead_Type.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("USP_Mst_ItemBillingHeadMaster", new string[] { "flag", "ItemBillingHead_Type" }, new string[] {"9",ddlItemBillingHead_Type.SelectedValue }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlBillingHeadName.DataSource = ds;
                        ddlBillingHeadName.DataTextField = "ItemBillingHead_Name";
                        ddlBillingHeadName.DataValueField = "ItemBillingHead_ID";
                        ddlBillingHeadName.DataBind();
                    }
                }
            }
            ddlBillingHeadName.Items.Insert(0, new ListItem("Select", "0"));
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
            ds = objdb.ByProcedure("USP_Mst_ItemBillingHeadMaster", new string[] { "flag", "ItemBillingHeadChild_ID", "ItemBillingHead_Name", "ItemBillingHead_Code" }, new string[] { "12", ViewState["ItemBillingHeadChild_ID"].ToString(),txtBillingHeadNameAlias.Text,txtItemBillingHead_Code.Text }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    if(ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        lblMsg.Text = objdb1.Alert("fa-check", "alert-success", "ThankYou !", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                    else
                    {
                        lblMsg.Text = objdb1.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    }
                }
                ddlItemBillingHead_Type.ClearSelection();
                ddlItemBillingHead_Type_SelectedIndexChanged(sender, e);
                txtBillingHeadNameAlias.Text = "";
                txtItemBillingHead_Code.Text = "";
                ViewState["ItemBillingHeadChild_ID"] = "0";
                FillGrid();
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
            gvDetails.DataSource = string.Empty;
            gvDetails.DataBind();
            ds = objdb.ByProcedure("USP_Mst_ItemBillingHeadMaster", new string[] { "flag", "Office_ID" }, new string[] { "11", objdb1.Office_ID() }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDetails.DataSource = ds;
                    gvDetails.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}