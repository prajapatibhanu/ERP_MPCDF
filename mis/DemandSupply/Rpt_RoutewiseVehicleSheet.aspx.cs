using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_DemandSupply_Rpt_RoutewiseVehicleSheet : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1,ds3 = new DataSet();
    double sum1, sum2 = 0;
    int sum11, sum22 = 0;
    int dsum11 = 0, dsum22 = 0, dsum33 = 0, dsum44 = 0;
    int csum11 = 0, csum22 = 0, csum33 = 0, csum44 = 0;
    int cellIndex = 2;
    int cellIndexbooth = 2,i_Qty=0,i_NaQty=0;
    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetLocation();
                GetVehicleNo();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================
    protected void GetShift()
    {
        try
        {
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataSource = objdb.ByProcedure("USP_Mst_ShiftMaster",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");
                ddlShift.DataBind();
                ddlShift.Items.Insert(0, new ListItem("Select", "0"));

           

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
    }
    protected void GetCategory()
    {
        try
        {
                ddlItemCategory.DataTextField = "ItemCatName";
                ddlItemCategory.DataValueField = "ItemCat_id";
                ddlItemCategory.DataSource = objdb.ByProcedure("USP_Trn_MilkOrProductDemand",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    private void GetVehicleNo()
    {
        try
        {
            ddlVehicleNo.DataTextField = "VehicleNo";
            ddlVehicleNo.DataValueField = "VehicleMilkOrProduct_ID";
            ddlVehicleNo.DataSource = objdb.ByProcedure("USP_Mst_VehicleMilkOrProduct",
                           new string[] { "flag", "Office_ID" },
                           new string[] { "5", objdb.Office_ID() }, "dataset");
            ddlVehicleNo.DataBind();
            ddlVehicleNo.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Transportation ", ex.Message.ToString());
        }
    }
    protected void GetLocation()
    {
        try
        {
            ddlLocation.DataTextField = "AreaName";
            ddlLocation.DataValueField = "AreaId";
            ddlLocation.DataSource = objdb.ByProcedure("USP_Mst_Area",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    private void GetRoute()
    {
        try
        {

            ddlRoute.Items.Clear();
            ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_Route",
                     new string[] { "flag", "Office_ID", "AreaId" },
                     new string[] { "6", objdb.Office_ID(), ddlLocation.SelectedValue }, "dataset");
            ddlRoute.DataTextField = "RName";
            ddlRoute.DataValueField = "RouteId";
            ddlRoute.DataBind();
            ddlRoute.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 3: ", ex.Message.ToString());
        }
    }
    private void GetSupplyRouteWise()
    {
        try
        {
            lblMsg.Text = "";
            DateTime date3 = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", culture);
            string deliverydate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductDemandForSupply",
                     new string[] { "flag", "Office_ID", "Shift_id", "ItemCat_id", "Delivery_Date", "AreaId", "RouteId", "VehicleMilkOrProduct_ID" },
                       new string[] { "16", objdb.Office_ID(), ddlShift.SelectedValue, ddlItemCategory.SelectedValue, deliverydate,ddlLocation.SelectedValue,ddlRoute.SelectedValue,ddlVehicleNo.SelectedValue }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0 && ds1.Tables[1].Rows.Count != 0)
            {

                lblDate.Text = txtDeliveryDate.Text;
                lblItemCategory.Text = ddlItemCategory.SelectedItem.Text;
                lblRouteName.Text = ddlRoute.SelectedItem.Text;
                lblShift.Text = ddlShift.SelectedItem.Text;
                lblVehicleNo.Text = ds1.Tables[1].Rows[0]["VehicleNo"].ToString();
                pnldata.Visible = true;
               


                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();

                DataTable dt = new DataTable();
                dt = ds1.Tables[0];

                if(ddlItemCategory.SelectedValue=="3")
                {
                    DataTable dtcrate = new DataTable();// create dt for Crate total
                    DataRow drcrate;

                    dtcrate.Columns.Add("ItemName", typeof(string));
                    dtcrate.Columns.Add("CrateQty", typeof(int));
                    dtcrate.Columns.Add("CratePacketQty", typeof(String));
                    drcrate = dtcrate.NewRow();

                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ToString() != "RouteId" && column.ToString() != "Route" && column.ToString() != "Total Supply")
                        {

                            sum11 = Convert.ToInt32(dt.Compute("SUM([" + column + "])", string.Empty));
                            //  below code for crate calculation

                            if (ddlItemCategory.SelectedValue == "3")
                            {

                                ds3 = objdb.ByProcedure("USP_Mst_SetItemCarriageMode",
                                        new string[] { "Flag", "ItemCat_id", "Itemname", "Office_ID", "EffectiveDate" },
                                        new string[] { "7", ddlItemCategory.SelectedValue, column.ToString(), objdb.Office_ID(), deliverydate }, "dataset"); if (ds3.Tables[0].Rows.Count > 0)
                                {
                                    i_Qty = Convert.ToInt32(ds3.Tables[0].Rows[0]["ItemQtyByCarriageMode"].ToString());
                                    i_NaQty = Convert.ToInt32(ds3.Tables[0].Rows[0]["NotIssueQty"].ToString());
                                }
                                else
                                {
                                    i_Qty = 0;
                                    i_NaQty = 0;
                                }
                                if (ds3 != null) { ds3.Dispose(); }
                                int Actualcrate = 0, remenderCrate = 0, FinalCrate = 0;
                                string Extrapacket = "0";
                                Actualcrate = sum11 / i_Qty;
                                remenderCrate = sum11 % i_Qty;

                                if (remenderCrate <= i_NaQty)
                                {
                                    FinalCrate = Actualcrate;
                                    Extrapacket = remenderCrate.ToString();

                                }
                                else
                                {
                                    FinalCrate = Actualcrate + 1;
                                    Extrapacket = "-" + (i_Qty - remenderCrate);
                                }
                                drcrate[0] = column.ToString();
                                drcrate[1] = FinalCrate;
                                drcrate[2] = Extrapacket;

                                dtcrate.Rows.Add(drcrate.ItemArray);
                            }
                            //  end code for crate calculation

                            if (ddlItemCategory.SelectedValue == "3")
                            {
                                //  sum and bind data in string  builder
                                int cratetotal = Convert.ToInt32(dtcrate.Compute("SUM([" + "CrateQty" + "])", string.Empty));
                                int Rowcount = dtcrate.Rows.Count;
                                StringBuilder sbtable = new StringBuilder();
                                sbtable.Append("<div style='margin-left:450px; margin-right:450px; margin-bottom:10px; text-align:center;'>CRATE SUMMARY</div>");
                                sbtable.Append("<table class='table table-striped table-bordered'>");
                                sbtable.Append("<tr>");
                                sbtable.Append("<td style='text-align:right;'>");
                                sbtable.Append("</td>");

                                for (int i = 0; i < Rowcount; i++)
                                {
                                    sbtable.Append("<td>" + dtcrate.Rows[i]["ItemName"].ToString() + "");



                                }
                                sbtable.Append("<td style='text-align:center;' colspan='" + (Rowcount + 1) + "'>TOTAL");
                                sbtable.Append("</td>");
                                sbtable.Append("</tr>");

                                sbtable.Append("<tr>");
                                sbtable.Append("<td style='text-align:right;'> CRATE DETAILS");
                                sbtable.Append("</td>");
                                for (int i = 0; i < Rowcount; i++)
                                {
                                    sbtable.Append("<td>" + dtcrate.Rows[i]["CrateQty"].ToString() + "");


                                }
                                sbtable.Append("<td style='text-align:center;' colspan='" + (Rowcount + 1) + "'>" + cratetotal.ToString());

                                sbtable.Append("</tr>");
                                sbtable.Append("<tr>");
                                sbtable.Append("<td style='text-align:right;'> EXTRA PKT(+/-)");
                                sbtable.Append("</td>");

                                for (int i = 0; i < Rowcount; i++)
                                {
                                    sbtable.Append("<td>" + dtcrate.Rows[i]["CratePacketQty"].ToString() + "");


                                }
                                sbtable.Append("<td style='text-align:center;' colspan='" + (Rowcount + 1) + "'>");
                                sbtable.Append("</tr>");
                                sbtable.Append("</table>");
                                divtable.InnerHtml = sbtable.ToString();
                                // ViewState["CrateDetails"] = sbtable.ToString();
                                if (dtcrate != null) { dtcrate.Dispose(); }
                            }
                        }


                    }
                }

               
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning! : ", "No Record Found.");
                pnldata.Visible = false;
            }
               
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error Routewise Approved Supply ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    #endregion========================================================
    #region=========== init or changed event row command ===========================

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //int maxrowcell1 = e.Row.Cells.Count - 1;  // previous default is : 2
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;

            //if (ddlItemCategory.SelectedValue != "3")
            //{
            //    e.Row.Cells[maxrowcell1].Visible = false;
            //    //  e.Row.Cells[maxrowcell1 + 1].Visible = false;
            //}
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //int maxheadercell1 = e.Row.Cells.Count - 1;  // previous default is : 2
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            //if (ddlItemCategory.SelectedValue != "3")
            //{
            //    e.Row.Cells[maxheadercell1].Visible = false;
            //    //  e.Row.Cells[maxheadercell1 + 1].Visible = false;
            //}
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
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0")
        {
            lblMsg.Text = string.Empty;
            GetRoute();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            GetSupplyRouteWise();
        }
    }
    #endregion=====================================================
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        txtDeliveryDate.Text = string.Empty;
        ddlShift.SelectedIndex = 0;
        ddlRoute.SelectedIndex = 0;
        ddlItemCategory.SelectedIndex = 0;
        ddlVehicleNo.SelectedIndex = 0;
        GridView1.DataSource = null;
        GridView1.DataBind();
        pnldata.Visible = false;
        divtable.InnerHtml = "";
    }
}