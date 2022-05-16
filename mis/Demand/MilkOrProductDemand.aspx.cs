using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Demand_MilkOrProductDemand : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    IFormatProvider culture = new CultureInfo("en-US", true);
    decimal totalValue = 0, Totalqty = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                getGV_DemandDetails();
                ddlProductVariant.Enabled = false;
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                txtDate.Text = objdb.getdate();
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

    #region -- Init Event --

    protected void ddlshift_Init(object sender, EventArgs e)
    {
        GetShift();
    }
    protected void ddlDS_Init(object sender, EventArgs e)
    {
        GetDSOfficeDetails();
    }
    protected void ddlProductCategory_Init(object sender, EventArgs e)
    {
        GetProductCategory();
    }
    protected void ddlModeOfRequest_Init(object sender, EventArgs e)
    {
        GetModeOfRequest();
    }

    #endregion

    #region -- User Defined Function --

    private void GetModeOfRequest()
    {
        try
        {
            ddlModeOfRequest.DataSource = objddl.GetModeOfRequest().Tables[0];
            ddlModeOfRequest.DataTextField = "ModeName";
            ddlModeOfRequest.DataValueField = "id";
            ddlModeOfRequest.DataBind();
            ddlModeOfRequest.Items.Insert(0, new ListItem("Select", "0"));
            ddlModeOfRequest.Items.Remove(ddlModeOfRequest.Items.FindByValue(objddl.RequestModeWebId()));
            ddlModeOfRequest.Items.Remove(ddlModeOfRequest.Items.FindByValue(objddl.RequestModeMobileAppId()));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 01:" + ex.Message.ToString());
        }
    }
    private void getGV_DemandDetails()
    {
        try
        {
            if (ds != null) { ds.Clear(); }
            ds = objdb.ByProcedure("sp_tblPUDemand",
                        new string[] { "flag", "Office_ID", "CreatedBy", "OfficeType_ID" },
                        new string[] { "4", objdb.Office_ID(), objdb.createdBy(), objdb.OfficeType_ID() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_DemandDetails.DataSource = ds;
                gv_DemandDetails.DataBind();
            }
            else
            {
                ds.Clear();
                gv_DemandDetails.DataSource = ds;
                gv_DemandDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 03:" + ex.Message.ToString());
        }
    }
    private void GetShift()
    {
        try
        {
            ddlshift.DataSource = objddl.ShiftFill().Tables[0];
            ddlshift.DataTextField = "ShiftName";
            ddlshift.DataValueField = "PUShift_id";
            ddlshift.DataBind();
            ddlshift.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 01:" + ex.Message.ToString());
        }
    }
    protected void GetProductCategory()
    {
        try
        {
            ddlProductCategory.DataSource = objdb.ByProcedure("SptblPUProductMaster",
                   new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlProductCategory.DataTextField = "Category_Name";
            ddlProductCategory.DataValueField = "Cat_id";
            ddlProductCategory.DataBind();
            ddlProductCategory.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetProduct()
    {
        try
        {
            if (ddlProductCategory.SelectedValue != "0")
            {
                ddlProductVariant.DataSource = objdb.ByProcedure("SptblPUProductVariant",
                              new string[] { "flag", "Cat_id" },
                              new string[] { "7", ddlProductCategory.SelectedValue }, "dataset");
                ddlProductVariant.DataTextField = "VariantName";
                ddlProductVariant.DataValueField = "VariantId";
                ddlProductVariant.DataBind();
                ddlProductVariant.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error: 2 " + ex.Message.ToString());
        }
    }
    public void AddToGrid()
    {
        try
        {
            decimal Variant = 0;
            if (Convert.ToString(ViewState["InsertRecord"]) == null || Convert.ToString(ViewState["InsertRecord"]) == "")
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("Cat_id", typeof(int)));
                dt.Columns.Add(new DataColumn("CatName", typeof(string)));
                dt.Columns.Add(new DataColumn("VariantId", typeof(int)));
                dt.Columns.Add(new DataColumn("VariantName", typeof(string)));
                dt.Columns.Add(new DataColumn("NoofPacekts", typeof(decimal)));
                dt.Columns.Add(new DataColumn("MRPRate", typeof(decimal)));
                dt.Columns.Add(new DataColumn("TotalValue", typeof(decimal)));

                dr = dt.NewRow();
                dr[0] = 1;
                dr[1] = ddlProductCategory.SelectedValue;
                dr[2] = ddlProductCategory.SelectedItem.Text;
                dr[3] = ddlProductVariant.SelectedValue;
                dr[4] = ddlProductVariant.SelectedItem.Text;
                dr[5] = txtQuantity.Text;
                dr[6] = objddl.GetProductVariantPrice(ddlProductVariant.SelectedValue).Tables[0].Rows[0]["ProductRate"].ToString();
                dr[7] = Convert.ToDecimal(objddl.GetProductVariantPrice(ddlProductVariant.SelectedValue).Tables[0].Rows[0]["ProductRate"].ToString()) * Convert.ToDecimal(txtQuantity.Text);

                dt.Rows.Add(dr);
                ViewState["InsertRecord"] = dt;
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                DataTable dt = new DataTable();
                DataTable DT = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("Cat_id", typeof(int)));
                dt.Columns.Add(new DataColumn("CatName", typeof(string)));
                dt.Columns.Add(new DataColumn("VariantId", typeof(int)));
                dt.Columns.Add(new DataColumn("VariantName", typeof(string)));
                dt.Columns.Add(new DataColumn("NoofPacekts", typeof(decimal)));
                dt.Columns.Add(new DataColumn("MRPRate", typeof(decimal)));
                dt.Columns.Add(new DataColumn("TotalValue", typeof(decimal)));

                DT = (DataTable)ViewState["InsertRecord"];
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (ddlProductVariant.SelectedValue == DT.Rows[i]["VariantId"].ToString())
                    {
                        Variant = 1;
                    }
                }
                if (Variant == 1)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Product Variant of \"" + ddlProductVariant.SelectedItem.Text + "\" Already Exist.");
                }
                else
                {
                    dr = dt.NewRow();
                    dr[0] = 1;
                    dr[1] = ddlProductCategory.SelectedValue;
                    dr[2] = ddlProductCategory.SelectedItem.Text;
                    dr[3] = ddlProductVariant.SelectedValue;
                    dr[4] = ddlProductVariant.SelectedItem.Text;
                    dr[5] = txtQuantity.Text;
                    dr[6] = objddl.GetProductVariantPrice(ddlProductVariant.SelectedValue).Tables[0].Rows[0]["ProductRate"].ToString();
                    dr[7] = Convert.ToDecimal(objddl.GetProductVariantPrice(ddlProductVariant.SelectedValue).Tables[0].Rows[0]["ProductRate"].ToString()) * Convert.ToDecimal(txtQuantity.Text);
                    dt.Rows.Add(dr);
                }

                foreach (DataRow tr in DT.Rows)
                {
                    dt.Rows.Add(tr.ItemArray);
                }
                ViewState["InsertRecord"] = dt;
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

            //Clear Record
            ddlProductCategory.ClearSelection();
            ddlProductVariant.ClearSelection();
            txtQuantity.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }
    private void GetDSOfficeDetails()
    {
        try
        {
            ddlDS.DataSource = objdb.ByProcedure("SpAdminOffice",
                    new string[] { "flag" },
                    new string[] { "1" }, "dataset");
            ddlDS.DataTextField = "Office_Name";
            ddlDS.DataValueField = "Office_ID";
            ddlDS.DataBind();
            ddlDS.SelectedValue = objdb.Office_ID();
            ddlDS.Enabled = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 07:" + ex.Message.ToString());
        }
    }
    private DataTable getProductDetail()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new DataColumn("Cat_id", typeof(int)));
        dt.Columns.Add(new DataColumn("VariantId", typeof(int)));
        dt.Columns.Add(new DataColumn("MRPRate", typeof(decimal)));
        dt.Columns.Add(new DataColumn("NoofPacekts", typeof(int)));
        dt.Columns.Add(new DataColumn("TotalValue", typeof(decimal)));

        foreach (GridViewRow row in GridView1.Rows)
        {
            Label lblCat_id = (Label)row.FindControl("lblCat_id");
            Label lblVariantId = (Label)row.FindControl("lblVariantId");
            Label lblProductRate = (Label)row.FindControl("lblProductRate");
            Label lblQuantity = (Label)row.FindControl("lblQuantity");
            Label lblTotalAmount = (Label)row.FindControl("lblTotalAmount");

            dr = dt.NewRow();
            dr[0] = lblCat_id.Text;
            dr[1] = lblVariantId.Text;
            dr[2] = lblProductRate.Text;
            dr[3] = lblQuantity.Text;
            dr[4] = lblTotalAmount.Text;
            dt.Rows.Add(dr);
        }
        return dt;
    }
    private void ClearAll()
    {
        txtDate.Text = "";
        txtCode.Text = "";
        txtQuantity.Text = "";
        txtRemark.Text = "";
        lblRemark.Text = "";
        ddlModeOfRequest.ClearSelection();
        ddlshift.ClearSelection();
        ddlRouteNo.DataSource = string.Empty;
        ddlRouteNo.DataBind();
        ddlRouteNo.Items.Insert(0, new ListItem("-", "0"));
        ddlRouteNo.ClearSelection();
        ddlDistributorName.DataSource = string.Empty;
        ddlDistributorName.DataBind();
        ddlDistributorName.Items.Insert(0, new ListItem("-", "0"));
        ddlDistributorName.ClearSelection();
        ddlProductCategory.ClearSelection();
        ddlProductVariant.DataSource = string.Empty;
        ddlProductVariant.DataBind();
        ddlProductVariant.Items.Insert(0, new ListItem("Select", "0"));
        ddlProductVariant.ClearSelection();
        ddlProductVariant.Enabled = false;
        ViewState["InsertRecord"] = null;
        GridView1.DataSource = ViewState["InsertRecord"] as DataTable;
        GridView1.DataBind();
        GridView1.SelectedIndex = -1;
        gv_DemandDetails.SelectedIndex = -1;
        gv_popup_ProductDetails.SelectedIndex = -1;
    }

    #endregion

    #region -- Change Event --

    protected void txtCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtCode.Text.Trim() != "")
            {
                lblMsg.Text = "";
                ds = objdb.ByProcedure("sp_tblPUDemand",
                   new string[] { "flag", "UCode", "UserName", "Office_ID" },
                   new string[] { "2", txtCode.Text.Substring(0, 1), txtCode.Text.Trim(), objdb.Office_ID() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {

                    lblDistributor.Text = ds.Tables[0].Rows[0]["UserType"].ToString();
                    hfUserType_id.Value = ds.Tables[0].Rows[0]["UserTypeId"].ToString();
                    ddlDistributorName.DataSource = ds.Tables[0];
                    ddlDistributorName.DataTextField = "Name";
                    ddlDistributorName.DataValueField = "ID";
                    ddlDistributorName.DataBind();

                    ddlRouteNo.DataSource = ds.Tables[0];
                    ddlRouteNo.DataTextField = "RouteName";
                    ddlRouteNo.DataValueField = "RouteId";
                    ddlRouteNo.DataBind();
                }
                else
                {
                    ds.Clear();
                    ddlDistributorName.DataSource = ds.Tables[0];
                    ddlDistributorName.DataBind();
                    ddlDistributorName.Items.Insert(0, new ListItem("-", "0"));

                    ddlRouteNo.DataSource = ds.Tables[0];
                    ddlRouteNo.DataBind();
                    ddlRouteNo.Items.Insert(0, new ListItem("-", "0"));

                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", "Record not found for Code : <b>" + txtCode.Text + "</b>");
                    txtCode.Text = "";
                    lblDistributor.Text = "Distributor";
                }
            }
            else
            {
                lblMsg.Text = "";
                txtCode.Text = "";
                lblDistributor.Text = "Distributor";

                ddlDistributorName.DataSource = string.Empty;
                ddlDistributorName.DataBind();
                ddlDistributorName.Items.Insert(0, new ListItem("-", "0"));

                ddlRouteNo.DataSource = string.Empty;
                ddlRouteNo.DataBind();
                ddlRouteNo.Items.Insert(0, new ListItem("-", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 15:" + ex.Message.ToString());
        }
    }
    protected void ddlProductCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProductCategory.SelectedIndex != 0)
        {
            ddlProductVariant.Enabled = true;
            GetProduct();
        }
        else
        {
            ddlProductVariant.DataSource = string.Empty;
            ddlProductVariant.DataBind();
            ddlProductVariant.Items.Insert(0, new ListItem("Select", "0"));
            ddlProductVariant.Enabled = false;
        }
    }

    #endregion

    #region -- Button Event --

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        AddToGrid();
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt = ViewState["InsertRecord"] as DataTable;
            dt.Rows.Remove(dt.Rows[row.RowIndex]);
            ViewState["InsertRecord"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            //For clear record for add child record
            ddlProductCategory.ClearSelection();
            ddlProductVariant.ClearSelection();
            txtQuantity.Text = "";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        ClearAll();
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (btnSubmit.Text == "Save")
            {
                try
                {
                    if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                    {
                        DataTable dt = new DataTable();
                        dt = getProductDetail();

                        if (dt.Rows.Count > 0)
                        {
                            if (ds != null) { ds.Clear(); }

                            DateTime date3 = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", culture);
                            string dDate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

                            ds = objdb.ByProcedure("sp_tblPUDemand",
                                     new string[] { "flag", "Modeid", "UserTypeId", "UserId", "UserName", "DemandDate", "Shift_id", "OfficeType_ID", "Office_ID", "CreatedBy", "CreatedBy_IP", "Remark" },
                                     new string[] { "1", ddlModeOfRequest.SelectedValue, hfUserType_id.Value, ddlDistributorName.SelectedValue, txtCode.Text, dDate, ddlshift.SelectedValue, objdb.OfficeType_ID(), objdb.Office_ID(), objdb.createdBy(), objdb.GetLocalIPAddress() + " : " + objdb.GetMACAddress(), txtRemark.Text },
                                     new string[] { "type_tblPUDemandChild" },
                                  new DataTable[] { dt }, "TableSave");

                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                ClearAll();
                                getGV_DemandDetails();
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 15:" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            }
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Ooops!", "Warning : First to add atleast 1 Product detail!");
                        }
                        Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
                }
            }
            else if (btnSubmit.Text == "Update")
            {
                try
                {

                }
                catch (Exception ex)
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 14:" + ex.Message.ToString());
                }
            }
        }
    }

    #endregion

    #region -- GridView Events -- 

    protected void gv_DemandDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViewRow")
            {
                lblMsg.Text = "";
                if (ds != null) { ds.Clear(); }
                ds = objdb.ByProcedure("sp_tblPUDemand",
                         new string[] { "flag", "PUDemandId" },
                         new string[] { "5", e.CommandArgument.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (GridViewRow gvRow in gv_DemandDetails.Rows)
                    {
                        if (gv_DemandDetails.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            gv_DemandDetails.SelectedIndex = gvRow.DataItemIndex;
                            gv_DemandDetails.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }

                    lblUserType.Text = ds.Tables[0].Rows[0]["OfficeTypeName"].ToString();
                    //lblUserId.Text = ds.Tables[0].Rows[0]["DPersonName"].ToString();
                    lblRemark.Text = ds.Tables[0].Rows[0]["Remark"].ToString();
                    Control ctrl = e.CommandSource as Control;
                    if (ctrl != null)
                    {
                        GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                        Label lblDPersonName = (Label)row.FindControl("lblDPersonName");
                        lblUserId.Text = lblDPersonName.Text;
                    }

                    gv_popup_ProductDetails.DataSource = ds.Tables[0];
                    gv_popup_ProductDetails.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ViewDetailModal()", true);
                }
                else
                {
                    ds.Clear();
                    gv_popup_ProductDetails.DataSource = ds.Tables[0];
                    gv_popup_ProductDetails.DataBind();
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                }

            }
            else if (e.CommandName == "DeleteRow")
            {
                lblMsg.Text = "";
                if (ds != null) { ds.Clear(); }
                ds = objdb.ByProcedure("sp_tblPUDemand",
                        new string[] { "flag", "PUDemandId", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
                        new string[] { "6", e.CommandArgument.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Record Deleted" }, "dataset");
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks You!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    getGV_DemandDetails();
                    ClearAll();
                    btnSubmit.Text = "Save";
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    getGV_DemandDetails();
                    ClearAll();
                    btnSubmit.Text = "Save";
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 18:" + ex.Message.ToString());
        }
    }
    protected void gv_popup_ProductDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblQuantity = (Label)e.Row.FindControl("lblQuantity");
                Label lblTotalAmount = (Label)e.Row.FindControl("lblTotalAmount");

                Totalqty += Convert.ToDecimal(lblQuantity.Text);
                totalValue += Convert.ToDecimal(lblTotalAmount.Text);
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalQuantity = (Label)e.Row.FindControl("lblTotalQuantity");
                Label lblGrandTotalAmount = (Label)e.Row.FindControl("lblGrandTotalAmount");

                lblTotalQuantity.Text = Totalqty.ToString();
                lblGrandTotalAmount.Text = totalValue.ToString("0.00");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 17:" + ex.Message.ToString());
        }
    }

    #endregion
}