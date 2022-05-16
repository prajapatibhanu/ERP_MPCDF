using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class mis_DemandSupply_CrateMgmtAtDistributorOrSuperStockist : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1,ds2 = new DataSet();
    int sum11 = 0;
    string success = "", error = "";
    int i = 0;
    int cellIndexbooth = 2;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {

            if (!Page.IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void GetShift()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Mst_ShiftMaster",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataSource = ds;
                ddlShift.DataBind();
                ddlShift.Items.Insert(0, new ListItem("Select", "0"));

            }
            else
            {
                ddlShift.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    protected void GetCategory()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlItemCategory.DataTextField = "ItemCatName";
                ddlItemCategory.DataValueField = "ItemCat_id";
                ddlItemCategory.DataSource = ds;
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
                ddlItemCategory.Items.Remove(ddlItemCategory.Items.FindByText("Product"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
    }
    private void GetCrateDetailsByDistributor()
    {
        try
        {
            DateTime sdate = DateTime.ParseExact(txtSupplyDate.Text, "dd/MM/yyyy", culture);
            string supplydate = sdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds1 = objdb.ByProcedure("USP_Trn_MilkCrateDetails",
                     new string[] { "flag","Delivery_Date","DelivaryShift_id", "ItemCat_id","Office_ID", "DistributorId" },
                       new string[] { "1",supplydate,ddlShift.SelectedValue,ddlItemCategory.SelectedValue, 
                                    objdb.Office_ID(),objdb.createdBy() }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                DataTable dt = new DataTable();
                dt = ds1.Tables[0];
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();
                GridView1.FooterRow.Cells[2].Text = "Total";
                GridView1.FooterRow.Cells[2].Font.Bold = true;

                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() != "MilkOrProductDemandId" && column.ToString() != "BName" && column.ToString() != "RouteId" && column.ToString() != "RetailerTypeID" && column.ToString() != "BoothId")
                    {

                        sum11 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));

                        GridView1.FooterRow.Cells[3].Text = sum11.ToString();
                        GridView1.FooterRow.Cells[3].Font.Bold = true;
                    }
                }
                pnlsubmit.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                ddlItemCategory.SelectedIndex = 0;
                pnlsubmit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
            ddlItemCategory.SelectedIndex = 0;
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }

    private void ReturnInserted()
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                DateTime delivearydat = DateTime.ParseExact(txtSupplyDate.Text, "dd/MM/yyyy", culture);
                string delidate = delivearydat.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                foreach (GridViewRow gr in GridView1.Rows)
                {
                    string milkorproductdemandid = GridView1.DataKeys[gr.RowIndex].Value.ToString();
                    CheckBox chkSelect = (CheckBox)gr.FindControl("chkSelect");
                    HiddenField hfTotalCrate = (HiddenField)gr.FindControl("hfTotalCrate");
                    TextBox txtReceivedDate = (TextBox)gr.FindControl("txtReceivedDate");
                    TextBox txttotalcratereceived = (TextBox)gr.FindControl("txttotalcratereceived");
                    TextBox txttotalcratebroken = (TextBox)gr.FindControl("txttotalcratebroken");
                    TextBox txttotalcratemissing = (TextBox)gr.FindControl("txttotalcratemissing");
                    TextBox txtremark = (TextBox)gr.FindControl("txtremark");
                    Label lblRetailerTypeID = (Label)gr.FindControl("lblRetailerTypeID");
                    Label lblRouteId = (Label)gr.FindControl("lblRouteId");
                    Label lblBoothId = (Label)gr.FindControl("lblBoothId");

                    if (chkSelect.Checked == true && hfTotalCrate.Value != "" && txttotalcratereceived.Text.Trim() != "" && txttotalcratebroken.Text.Trim() != "" && txttotalcratemissing.Text.Trim() != "" && txtReceivedDate.Text.Trim()!="")
                    {
                        string myStringfrodelmdat = txtSupplyDate.Text;
                        DateTime fdeldate = DateTime.ParseExact(myStringfrodelmdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        string myStringtorecdate = txtReceivedDate.Text;
                        DateTime trecdate = DateTime.ParseExact(myStringtorecdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        DateTime recdate = DateTime.ParseExact(txtReceivedDate.Text, "dd/MM/yyyy", culture);
                        string receiveddate = recdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        int hftc = Convert.ToInt32(hfTotalCrate.Value);
                        int sumofrbm = Convert.ToInt32(txttotalcratereceived.Text.Trim()) + Convert.ToInt32(txttotalcratebroken.Text.Trim()) + Convert.ToInt32(txttotalcratemissing.Text.Trim());
                        if (hftc == sumofrbm && fdeldate <= trecdate)
                        {
                            ds2 = objdb.ByProcedure("USP_Trn_MilkCrateMgmt",
                               new string[] { "flag", "MilkOrProductDemandId", "RetailerTypeID", "RouteId","BoothId"
                                           ,"Delivary_Date","DelivaryShift_id","ItemCat_id","ReceivedDate","Total_SuppliedCrate","Total_ReceivedCrate","Total_BrokenCrate"
                                            , "Total_MissingCrate", "Remark_ByDistOrSubDistOrInst","Crate_Status" , "Office_ID","PlatformType"
                                           , "CreatedBy", "CreatedByIP", },
                               new string[] { "1", milkorproductdemandid,lblRetailerTypeID.Text,lblRouteId.Text.Trim(),lblBoothId.Text
                                   ,delidate.ToString(),ddlShift.SelectedValue,ddlItemCategory.SelectedValue,receiveddate,
                                   hfTotalCrate.Value,txttotalcratereceived.Text.Trim(),txttotalcratebroken.Text.Trim(),txttotalcratemissing.Text.Trim()
                                   ,txtremark.Text.Trim(),"1",objdb.Office_ID(),"1"
                                   , objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "TableSave");
                            if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();

                                if (i == 0)
                                {
                                    i = 1;
                                }
                                else
                                {
                                    i = i + 1;
                                }
                            }
                            else
                            {
                                i = 0;
                                error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();

                            }
                        }
                        
                    }
                }
                if (i > 0)
                {
                    GetCrateDetailsByDistributor();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                }
            }
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 4 : " + ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    protected void ddlShift_Init(object sender, EventArgs e)
    {
        GetShift();
    }
    protected void ddlItemCategory_Init(object sender, EventArgs e)
    {
        GetCategory();
    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtSupplyDate.Text != "" && ddlShift.SelectedValue != "0" && ddlItemCategory.SelectedValue != "0")
        {
            pnlparlourdetails.Visible = true;
            GetCrateDetailsByDistributor();
        }
        else
        {
            pnlparlourdetails.Visible = false;
            pnlsubmit.Visible = false;
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            ReturnInserted();
        }
    }
}