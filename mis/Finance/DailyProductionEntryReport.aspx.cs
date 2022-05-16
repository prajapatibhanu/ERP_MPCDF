using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Finance_DailyProductionEntryReport : System.Web.UI.Page
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
                    //txtFromDate.Attributes.Add("readonly", "readonly");
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    txtToDate.Attributes.Add("readonly", "readonly");
                    FillVoucherDate();
                    FillDropdown();
                    lblheadingFirst.Visible = false;
                    lblheadingFirst.Text = ViewState["Office_FinAddress"].ToString() + "<br/> Production Report";
                  //  FillGrid();

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
            ddlitems.Items.Insert(0, new ListItem("All", "0"));

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
                txtFromDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
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
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {

                ds = objdb.ByProcedure("spFinDailyProduction",
                                    new string[] { "flag", "ItemID","FromDate", "ToDate", "Office_ID" },
                                    new string[] { "2", ddlitems.SelectedValue.ToString(), Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["Office_ID"].ToString() }, "dataset");
                if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {

                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;

                    lblheadingFirst.Text = ViewState["Office_FinAddress"].ToString() + "<br/>Production Report <br/>  " + txtFromDate.Text + " To " + txtToDate.Text;
                    lblheadingFirst.Visible = true;
                }
              
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            //if (ddlitems.SelectedIndex == 0)
            //{
            //    msg += "Select Item Name. \\n";
            //}
            if (txtFromDate.Text == "")
            {
                msg += "Enter From Date. \\n";
            }
            if (txtToDate.Text == "")
            {
                msg += "Enter To Date. \\n";
            }

            if (msg.Trim() == "")
            {


                FillGrid();

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
   
}