using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class mis_Finance_PartyWiseItemRateMapping : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Emp_ID"] != null)
                {
                    
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["RowNo"] = "0";
                    FillPartyLedger();                   
                    FillItem();
                    FillGrid();
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                   
                                      
                }
                else
                {
                    Response.Redirect("~/mis/Login.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    //Fill Item DropDown
    protected void FillItem()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinVoucherTx", new string[] { "flag", "Office_ID" }, new string[] { "32", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlItem.Items.Clear();
                ddlItem.DataSource = ds.Tables[0];
                ddlItem.DataTextField = "AvailableStock1";
                ddlItem.DataValueField = "Item_id";
                ddlItem.DataBind();
                ddlItem.Items.Insert(0, new ListItem("Select", "0"));

            }
            else
            {
                ddlItem.Items.Clear();
                ddlItem.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //Fill PatryLedger DropDown
    protected void FillPartyLedger()
    {
        try
        {
            ds = objdb.ByProcedure("Usp_FinPartyWiseItemRateMaping",
                new string[] { "flag" },
                new string[] { "3"}, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlRetailerType.DataTextField = "RetailerTypeName";
                ddlRetailerType.DataValueField = "RetailerTypeID";
                ddlRetailerType.DataSource = ds.Tables[0];
                ddlRetailerType.DataBind();
                ddlRetailerType.Items.Insert(0, new ListItem("Select", "0"));


                ddlRetailerTypeflt.DataTextField = "RetailerTypeName";
                ddlRetailerTypeflt.DataValueField = "RetailerTypeID";
                ddlRetailerTypeflt.DataSource = ds.Tables[0];
                ddlRetailerTypeflt.DataBind();
                ddlRetailerTypeflt.Items.Insert(0, new ListItem("All", "0"));

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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string Status = "0";
            foreach(GridViewRow row in GridViewItem.Rows)
            {
                Label lblItemID = (Label)row.FindControl("lblItemID");  
                if(lblItemID.Text == ddlItem.SelectedValue)
                {
                    Status = "1";
                }
            }
            if (Status == "0")
            {
                AddItem("0");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Item already exists');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridViewItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string ID = GridViewItem.DataKeys[e.RowIndex].Value.ToString();
            AddItem(ID);

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void AddItem(string ID)
    {
        try
        {

            DataTable dt_GridViewItem = new DataTable();
            DataColumn RowNo = dt_GridViewItem.Columns.Add("ID", typeof(int));
            dt_GridViewItem.Columns.Add(new DataColumn("ItemID", typeof(string)));           
            dt_GridViewItem.Columns.Add(new DataColumn("Item", typeof(string)));          
            dt_GridViewItem.Columns.Add(new DataColumn("Rate", typeof(string)));
            
            RowNo.AutoIncrement = true;
            RowNo.AutoIncrementSeed = 1;
            RowNo.AutoIncrementStep = 1;
            int rowIndex = 0;
            int gridRows = GridViewItem.Rows.Count;
            for (rowIndex = 0; rowIndex < gridRows; rowIndex++)
            {
                Label lblItemRowNo = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemRowNo");
                Label lblItemID = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItemID");               
                Label lblItem = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblItem");                
                Label lblRate = (Label)GridViewItem.Rows[rowIndex].Cells[0].FindControl("lblRate");
               

                if (lblItemRowNo.Text != ID && ViewState["RowNo"].ToString() == "0")
                {
                    dt_GridViewItem.Rows.Add(lblItemRowNo.Text, lblItemID.Text,  lblItem.Text,lblRate.Text);

                }

                else if (ViewState["RowNo"].ToString() != "0")
                {
                   
                     dt_GridViewItem.Rows.Add(null, ddlItem.SelectedValue.ToString(), ddlItem.SelectedItem.Text, txtRate.Text);                    
                }

                // dt_GridViewItem.Rows.Add(rowIndex.ToString(), lblItem.Text, lblQuantity.Text, lblRate.Text, lblAmount.Text);

            }
            if (ID == "0" && ViewState["RowNo"].ToString() == "0")
            {

                dt_GridViewItem.Rows.Add(null, ddlItem.SelectedValue.ToString(), ddlItem.SelectedItem.Text, txtRate.Text); 
               
            }

           
            GridViewItem.DataSource = dt_GridViewItem;
            GridViewItem.DataBind();
            ViewState["RowNo"] = "0";
            ddlItem.ClearSelection();
            txtRate.Text = "";
            // GridViewLedger
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
            if (GridViewItem.Rows.Count > 0)
            {
                foreach (GridViewRow row in GridViewItem.Rows)
                {
                    Label lblItemID = (Label)row.FindControl("lblItemID");
                    Label lblRate = (Label)row.FindControl("lblRate");

                    ds = objdb.ByProcedure("Usp_FinPartyWiseItemRateMaping",
                                       new string[] { "flag", "EffectiveDate", "Office_ID", "RetailerType_ID", "Item_ID", "Rate", "IsActive", "CreatedBy", "CreatedByIP" },
                                       new string[] { "0", Convert.ToDateTime(txtEffectiveDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString(), ddlRetailerType.SelectedValue, lblItemID.Text, lblRate.Text, "1", objdb.createdBy(), objdb.GetLocalIPAddress() }, "dataset");

                }
                FillGrid();
                ddlRetailerType.ClearSelection();
                txtEffectiveDate.Text = "";
                GridViewItem.DataSource = string.Empty;
                GridViewItem.DataBind();


            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Select at least one item');", true);
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
            gvItemRateDetail.DataSource = string.Empty;
            gvItemRateDetail.DataBind();
            ds = objdb.ByProcedure("Usp_FinPartyWiseItemRateMaping", new string[] { "flag", "RetailerType_ID", "Office_ID" }, new string[] { "1", ddlRetailerTypeflt.SelectedValue, ViewState["Office_ID"].ToString() }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    gvItemRateDetail.DataSource = ds.Tables[0];
                    gvItemRateDetail.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlRetailerTypeflt_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
}