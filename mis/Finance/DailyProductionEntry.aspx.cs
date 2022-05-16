using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class mis_Finance_DailyProductionEntry : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {

                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Office_FinAddress"] = Session["Office_FinAddress"].ToString();


                    lblheadingFirst.Text = ViewState["Office_FinAddress"].ToString() + "<br/> Daily Production Entry";
                    //txtFromDate.Attributes.Add("readonly", "readonly");
                    txtTxnDate.Attributes.Add("readonly", "readonly");
                    txtProductionQuantity.Text = "0";
                    txtProductionCumulativeQuantity.Text = "0";
                    txtSaleQuantity.Text = "0";
                    txtSaleCumulativeQuantity.Text = "0";

                    FillVoucherDate();
                    FillDropdown();
                    FillGrid();

                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

   
    protected void FillDropdown()
    {
        try
        {
           

            ddlitems.DataSource = objdb.ByProcedure("Sp_tblSpItemStock",
                                       new string[] { "flag", "Office_Id", "ItemCat_id", "ItemType_id" },
                                       new string[] { "3", ViewState["Office_ID"].ToString(), "1", "102" }, "Dataset");
            ddlitems.DataTextField = "ItemName";
            ddlitems.DataValueField = "Item_id";
            ddlitems.DataBind();
            ddlitems.Items.Insert(0, new ListItem("Select", "0"));


            ds = objdb.ByProcedure("spFinDailyProduction",
                              new string[] { "flag" },
                              new string[] { "5" }, "dataset");         
            ddlUnit.DataSource = ds;
            ddlUnit.DataTextField = "UnitName";
            ddlUnit.DataValueField = "Unit_Id";
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, "Select");

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void FillVoucherDate()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                txtTxnDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
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
            lblheadingFirst.Visible = false;
            ds = objdb.ByProcedure("spFinDailyProduction",
                                new string[] { "flag", "TxnDate", "Office_ID"},
                                new string[] { "1", Convert.ToDateTime(txtTxnDate.Text, cult).ToString("yyyy/MM/dd"),ViewState["Office_ID"].ToString()}, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {

                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;


                lblheadingFirst.Text = ViewState["Office_FinAddress"].ToString() + "<br/> Daily Production Entry <br/> Date : " + txtTxnDate.Text;
                lblheadingFirst.Visible = true;

            }
            else
            {
                GridView1.DataSource = new string[] { };
                GridView1.DataBind();
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
            string msg = "";
            if (txtTxnDate.Text == "")
            {
                msg += "Enter Date. \\n";
            }

            if (ddlitems.SelectedIndex <= 0)
            {
                msg += "Select Item Name. \\n";
            }
           
            if (ddlUnit.SelectedIndex <= 0)
            {
                msg += "Select Unit. \\n";
            }
            if (txtProductionQuantity.Text =="")
            {
                msg += "Enter Production Quantity. \\n";
            }
            if (txtProductionCumulativeQuantity.Text == "")
            {
                msg += "Enter Production Cumulative Quantity. \\n";
            }
            if (txtSaleQuantity.Text == "")
            {
                msg += "Enter Sale Quantity. \\n";
            }
            if (txtSaleCumulativeQuantity.Text == "")
            {
                msg += "Enter Sale Cumulative Quantity. \\n";
            }
            ds = objdb.ByProcedure("spFinDailyProduction",
                                  new string[] { "flag", "ItemID" ,"TxnDate","Office_ID" },
                                  new string[] { "3",ddlitems.SelectedValue.ToString() , Convert.ToDateTime(txtTxnDate.Text, cult).ToString("yyyy/MM/dd"),ViewState["Office_ID"].ToString()}, "dataset");
               
            if(ds.Tables.Count > 0)
            {
                msg += ds.Tables[0].Rows[0]["Status"].ToString();
            }

            if (msg.Trim() == "")
            {

                objdb.ByProcedure("spFinDailyProduction",
                                new string[] { "flag", "ItemID", "ItemName", "UnitID", "UnitName", "TxnDate", "ProductionQuantity", "ProductionCumulativeQuantity", "SaleQuantity", "SaleCumulativeQuantity", "Office_ID", "UpdatedBy" },
                                new string[] { "0",ddlitems.SelectedValue.ToString() ,ddlitems.SelectedItem.ToString(),ddlUnit.SelectedValue.ToString(),ddlUnit.SelectedItem.ToString()
                                           , Convert.ToDateTime(txtTxnDate.Text, cult).ToString("yyyy/MM/dd"),txtProductionQuantity.Text , txtProductionCumulativeQuantity.Text , txtSaleQuantity.Text , txtSaleCumulativeQuantity.Text,ViewState["Office_ID"].ToString(),ViewState["Emp_ID"].ToString()}, "dataset");
                
                txtProductionQuantity.Text = "0";
                txtProductionCumulativeQuantity.Text = "0";
                txtSaleQuantity.Text = "0";
                txtSaleCumulativeQuantity.Text = "0";
                ddlUnit.ClearSelection();
                ddlitems.ClearSelection();
                FillGrid();

                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Data Successfully saved.");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtTxnDate_TextChanged(object sender, EventArgs e)
    {
        try
        {

            lblMsg.Text = "";
            FillGrid();


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string ID = GridView1.DataKeys[e.RowIndex].Value.ToString();

            objdb.ByProcedure("spFinDailyProduction",
                   new string[] { "flag", "ID", "UpdatedBy" },
                   new string[] { "4", ID, ViewState["Emp_ID"].ToString() }, "dataset");

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Successfully Deleted.");
            FillGrid();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}