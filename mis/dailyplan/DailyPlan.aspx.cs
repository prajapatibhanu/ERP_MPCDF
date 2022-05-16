using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_dailyplan_DailyPlan : System.Web.UI.Page
{
    DataSet ds,ds2;
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
                FillDropdown();
                txtOrderDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtOrderDate.Attributes.Add("ReadOnly", "ReadOnly");
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx");
        }

    }

    protected void FillDropdown()
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        FillGrid();
    }

    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            GridView2.DataSource = new string[] { };
            GridView2.DataBind();

            ViewState["TranDate"]=Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd");
            ViewState["Shift_ID"]=ddlShift.SelectedValue.ToString();


            ds = objdb.ByProcedure("sp_ProductionTarget_Tx", new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "ProductionDate" }, new string[] { "0", ViewState["Office_ID"].ToString(), ddlShift.SelectedValue.ToString(), "2", Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd") }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                //btnSave.Visible = true;
            }
            if (ds != null && ds.Tables[1].Rows.Count != 0)
            {
                GridView2.DataSource = ds.Tables[1];
                GridView2.DataBind();
                //btnSave.Visible = true;
            }
            if (ds != null)
            {
                btnSave.Visible = true;
                headingmilk.Visible = true;
                headingproduct.Visible = true;
            }

            /********************************/
            GridView3.DataSource = new string[] { };
            GridView3.DataBind();
            GridView4.DataSource = new string[] { };
            GridView4.DataBind();
            ds = objdb.ByProcedure("sp_ProductionTarget_Tx", new string[] { "flag", "Office_ID", "Shift_id", "ProductionDate" }, new string[] { "2", ViewState["Office_ID"].ToString(), ddlShift.SelectedValue.ToString(), Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd") }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                GridView3.DataSource = ds.Tables[0];
                GridView3.DataBind();
            }
            if (ds != null && ds.Tables[1].Rows.Count != 0)
            {
                GridView4.DataSource = ds.Tables[1];
                GridView4.DataBind();
            }
            if (ds != null)
            {
                btnVerifyItem.Visible = true;
                headingmilk2.Visible = true;
                headingproduct2.Visible = true;
            }
            /********************************/


            
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
                Label ItemName = (Label)row.FindControl("ItemName");
                string Item_id = ItemName.ToolTip.ToString();
                TextBox LastTarget = (TextBox)row.FindControl("txtTarget");
                Label lblProductSection_ID = (Label)row.FindControl("lblProductSection_ID");
                if (int.Parse(LastTarget.Text) > 0)
                {
                    objdb.ByProcedure("sp_ProductionTarget_Tx",
                    new string[] { "flag", "Target_Date", "Target_Shift_ID", "Target_Product_ID", "Target_Set_Quantity", "Target_SetBy", "Target_Office_By", "Target_Section" },
                    new string[] { "1", Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd"), ddlShift.SelectedValue.ToString(), Item_id, LastTarget.Text, ViewState["Emp_ID"].ToString(), ViewState["Office_ID"].ToString(), lblProductSection_ID.Text }, "dataset");
                }
                


            }



            foreach (GridViewRow row2 in GridView2.Rows)
            {
                Label ItemName = (Label)row2.FindControl("ItemName");
                string Item_id = ItemName.ToolTip.ToString();
                TextBox LastTarget = (TextBox)row2.FindControl("txtPTarget");
                Label lblProductSection_ID = (Label)row2.FindControl("lblProductSection_ID");

                if (int.Parse(LastTarget.Text) > 0)
                {
                    objdb.ByProcedure("sp_ProductionTarget_Tx",
                        new string[] { "flag", "Target_Date", "Target_Shift_ID", "Target_Product_ID", "Target_Set_Quantity", "Target_SetBy", "Target_Office_By", "Target_Section" },
                        new string[] { "1", Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd"), ddlShift.SelectedValue.ToString(), Item_id, LastTarget.Text, ViewState["Emp_ID"].ToString(), ViewState["Office_ID"].ToString(), lblProductSection_ID.Text }, "dataset");
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
    protected void btn_VerifyItem(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            /********************************/
            
            ds = objdb.ByProcedure("sp_ProductionTarget_Tx", new string[] { "flag", "Office_ID", "Shift_id", "ProductionDate" }, new string[] { "5", ViewState["Office_ID"].ToString(), ddlShift.SelectedValue.ToString(), Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd") }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                //ViewState["TranDate"] = Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd");
                //ViewState["Shift_ID"] = ddlShift.SelectedValue.ToString();

                int Count = ds.Tables[0].Rows.Count;
                for (int i = 0; i < Count; i++)
                {

                    string Item_ID = ds.Tables[0].Rows[i]["Item_ID"].ToString();
                    //string ItemCat_id = ds.Tables[0].Rows[i]["ItemCat_id"].ToString();
                    string Target_Section = ds.Tables[0].Rows[i]["Target_Section"].ToString();
                    //string Item_Quantity_Unit = ds.Tables[0].Rows[i]["Item_Quantity_Unit"].ToString();
                    string Target_Office_By = ds.Tables[0].Rows[i]["Target_Office_By"].ToString();
                    string TotalItemRequired = ds.Tables[0].Rows[i]["TotalItemRequired"].ToString();
                    string ItemType_id = ds.Tables[0].Rows[i]["ItemType_id"].ToString();
                    string ItemCat_id = ds.Tables[0].Rows[i]["ItemCat_id"].ToString();

                    objdb.ByProcedure("spProductionItemStock", new string[] { "flag", "SenderOffice_ID", "Shift_id", "TranDt", "ReceiverSection_ID", "ReceiverOffice_ID", "Inward", "Item_id", "SenderID", "ItemCat_id", "ItemType_id" }, new string[] { "2", Target_Office_By.ToString(), ViewState["Shift_ID"].ToString(), ViewState["TranDate"].ToString(), Target_Section.ToString(), Target_Office_By.ToString(), TotalItemRequired.ToString(), Item_ID.ToString(), ViewState["Emp_ID"].ToString(), ItemCat_id, ItemType_id }, "dataset");                    

                }



            }
            /********************************/


            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridView5.DataSource = new string[] { };
        GridView5.DataBind();
        string single_item_id = GridView3.SelectedDataKey.Value.ToString();
        ds = objdb.ByProcedure("sp_ProductionTarget_Tx", new string[] { "flag", "Office_ID", "Shift_id", "ProductionDate", "item_id", "ItemCat_id" }, new string[] { "3", ViewState["Office_ID"].ToString(), ddlShift.SelectedValue.ToString(), Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd"), single_item_id.ToString(), "3" }, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count != 0)
        {
            lblItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
            GridView5.DataSource = ds.Tables[0];
            GridView5.DataBind();
        }


        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
    }

    protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView5.DataSource = new string[] { };
        GridView5.DataBind();
        string single_item_id = GridView4.SelectedDataKey.Value.ToString();
        ds = objdb.ByProcedure("sp_ProductionTarget_Tx", new string[] { "flag", "Office_ID", "Shift_id", "ProductionDate", "item_id", "ItemCat_id" }, new string[] { "3", ViewState["Office_ID"].ToString(), ddlShift.SelectedValue.ToString(), Convert.ToDateTime(txtOrderDate.Text, cult).ToString("yyyy-MM-dd"), single_item_id.ToString(), "2" }, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count != 0)
        {
            lblItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
            GridView5.DataSource = ds.Tables[0];
            GridView5.DataBind();
        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridView6.DataSource = new string[] { };
        GridView6.DataBind();
        string single_item_id = GridView1.SelectedDataKey.Value.ToString();
        ds = objdb.ByProcedure("sp_ProductionTarget_Tx", new string[] { "flag", "Office_ID", "Product_ID" }, new string[] { "4", ViewState["Office_ID"].ToString(), single_item_id.ToString() }, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count != 0)
        {
            lblProductName.Text = ds.Tables[0].Rows[0]["ProductName"].ToString();
            GridView6.DataSource = ds.Tables[0];
            GridView6.DataBind();
        }


        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalertproduct()", true);
    }

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridView6.DataSource = new string[] { };
        GridView6.DataBind();
        string single_item_id = GridView2.SelectedDataKey.Value.ToString();
        ds = objdb.ByProcedure("sp_ProductionTarget_Tx", new string[] { "flag", "Office_ID", "Product_ID" }, new string[] { "4", ViewState["Office_ID"].ToString(), single_item_id.ToString() }, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count != 0)
        {
            lblProductName.Text = ds.Tables[0].Rows[0]["ProductName"].ToString();
            GridView6.DataSource = ds.Tables[0];
            GridView6.DataBind();
        }


        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalertproduct()", true);
    }

    
}