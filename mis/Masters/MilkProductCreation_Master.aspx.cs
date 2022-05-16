using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Web;
using System.Web.UI;

public partial class mis_Masters_MilkProductCreation_Master : System.Web.UI.Page
{
    DataSet ds, ds1, ds2, ds3, ds4;
    static DataSet ds5;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                lblMsg.Text = "";

                if (!IsPostBack)
                {
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();


                    FillPackagingMode();
                    ViewState["ItemId"] = "0";
                    ddlItemCategory.Enabled = false;

                    ds = objdb.ByProcedure("Usp_MilkProductCreation",
                                new string[] { "flag" },
                                new string[] { "4" }, "dataset");

                    ItemGroup.DataSource = ds;
                    ItemGroup.DataTextField = "ItemCatName";
                    ItemGroup.DataValueField = "ItemCat_id";
                    ItemGroup.DataBind();
                    ItemGroup.Items.Insert(0, new ListItem("Select", "0"));


                    gvfilterItemCategory.DataSource = ds;
                    gvfilterItemCategory.DataTextField = "ItemCatName";
                    gvfilterItemCategory.DataValueField = "ItemCat_id";
                    gvfilterItemCategory.DataBind();
                    gvfilterItemCategory.Items.Insert(0, new ListItem("All", "0"));
                    ds1 = objdb.ByProcedure("SpUnit",
                                new string[] { "flag" },
                                new string[] { "7" }, "dataset");
                    FillItemCategory();
                    ddlUnit.DataSource = ds1;
                    ddlUnit.DataTextField = "UnitName";
                    ddlUnit.DataValueField = "Unit_Id";
                    ddlUnit.DataBind();
                    ddlUnit.Items.Insert(0, "Select");
                    ds3 = objdb.ByProcedure("SpFinHSNMaster",
                               new string[] { "flag" },
                               new string[] { "2" }, "dataset");

                    ddlHsnCode.DataSource = ds3;
                    ddlHsnCode.DataTextField = "HSN_Code";
                    ddlHsnCode.DataValueField = "HSN_ID";
                    ddlHsnCode.DataBind();
                    ddlHsnCode.Items.Insert(0, "Select");
                    FillOffice();
                    FillGrid();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
                ddlDS.SelectedValue = ViewState["Office_ID"].ToString();
                ddlDS.Enabled = false;

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
    protected void FillPackagingMode()
    {
        try
        {

            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpItemMaster",
               new string[] { "flag" },
               new string[] { "25" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPackgngMode.DataSource = ds;
                ddlPackgngMode.DataTextField = "PackagingModeName";
                ddlPackgngMode.DataValueField = "PackagingModeId";
                ddlPackgngMode.DataBind();
                ddlPackgngMode.Items.Insert(0, "Select");

            }
            else
            {
                ddlPackgngMode.Items.Clear();
                ddlPackgngMode.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ItemGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillItemCategory();
           // FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           //, FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string msg = "";
            if (txtItemName.Text == "")
            {
                msg = "Enter Item Name";
            }
            if (ItemGroup.SelectedIndex <= 0)
            {
                msg += "Select Nature. \\n";
            }
            if (ddlItemCategory.SelectedIndex <= 0)
            {
                msg += "Select Item Group. \\n";
            }

            if (ddlUnit.SelectedIndex <= 0)
            {
                msg += "Select Unit. \\n";
            }
            if (txtItemOrderNo.Text == "")
            {
                msg += "Enter Item Order No. \\n";
            }
            string MarketingSection = "0";
            string PlantSection = "0";
            foreach (ListItem item in chkShownto.Items)
            {
                if(item.Value == "Marketing")
                {
                    if(item.Selected == true)
                    {
                        MarketingSection = "1";
                    }
                    
                }
                if (item.Value == "Plant")
                {
                    if (item.Selected == true)
                    {
                        PlantSection = "1";
                    }
                    
                }
            }
            if (msg.Trim() == "")
            {

                if (btnSubmit.Text == "Submit")
                {
                    ds = objdb.ByProcedure("Usp_MilkProductCreation",
                                new string[] { "flag", "ItemCat_id", "ItemType_id", "Unit_Id", "ItemName", "ItemName_Hindi", "ItemAliasCode", "CreatedBy", "CreatedAt", "CreatedByIP", "ItemSpecification", "PackagingSize", "Packaging_Mode", "ItemOrderNo", "HSNCode", "Office_ID", "MarketingSection", "PlantSection" },
                                new string[] { "1", ItemGroup.SelectedValue, ddlItemCategory.SelectedValue, ddlUnit.SelectedValue, txtItemName.Text, txtItemNameHindi.Text, txtitemaliscode.Text,objdb.createdBy(),objdb.Office_ID(),objdb.GetLocalIPAddress(), txtItemSpecification.Text, txtPackgngSz.Text, ddlPackgngMode.SelectedValue.ToString(), txtItemOrderNo.Text, ddlHsnCode.SelectedItem.Text, ViewState["Office_ID"].ToString(), MarketingSection, PlantSection }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "" + Success + "");
                            ClearData();
                            FillGrid();

                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                            string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "oops!", "" + Warning + "");
                            ClearData();
                            FillGrid();

                        }
                        else
                        {
                            string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "" + Danger + "");
                            ClearData();
                            FillGrid();

                        }

                    }
                }
                else if (btnSubmit.Text == "Update" && ViewState["ItemId"].ToString() != "0")
                {

                    ds = objdb.ByProcedure("Usp_MilkProductCreation",
                           new string[] { "flag", "Item_id", "ItemCat_id", "ItemType_id", "Unit_Id", "ItemName", "ItemName_Hindi", "ItemAliasCode", "ItemSpecification", "PackagingSize", "Packaging_Mode", "ItemOrderNo", "HSNCode", "Office_ID", "MarketingSection", "PlantSection", "CreatedBy", "CreatedAt", "CreatedByIP" },
                           new string[] { "3", ViewState["ItemId"].ToString(), ItemGroup.SelectedValue, ddlItemCategory.SelectedValue, ddlUnit.SelectedValue, txtItemName.Text, txtItemNameHindi.Text, txtitemaliscode.Text,  txtItemSpecification.Text, txtPackgngSz.Text, ddlPackgngMode.SelectedValue.ToString(), txtItemOrderNo.Text, ddlHsnCode.SelectedItem.Text, ViewState["Office_ID"].ToString(),MarketingSection,PlantSection, objdb.createdBy(),objdb.Office_ID(),objdb.GetLocalIPAddress() }, "dataset");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "" + Success + "");
                        ClearData();
                        FillGrid();

                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                    {
                        string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "oops!", "" + Warning + "");
                        ClearData();
                        FillGrid();

                    }
                    else
                    {
                        string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "" + Danger + "");
                        ClearData();
                        FillGrid();

                    }

                    btnSubmit.Text = "Submit";
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Item Name is already exist.');", true);
                    ClearData();

                }
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
    private void ClearData()
    {
        txtItemName.Text = "";
        txtItemNameHindi.Text = "";

        ddlItemCategory.ClearSelection();
        ddlUnit.ClearSelection();

        txtitemaliscode.Text = "";
        txtPackgngSz.Text = "";
        txtItemOrderNo.Text = "";
        ddlPackgngMode.ClearSelection();
        SetFocus(txtItemName);
        ddlHsnCode.ClearSelection();
        txtItemSpecification.Text = "";

    }
    private void FillGrid()
    {
        try
        {
			 btnExport.Visible = false;
            ds4 = objdb.ByProcedure("Usp_MilkProductCreation",
                        new string[] { "flag", "Office_ID", "ItemCat_id","ShownTo" },
                        new string[] { "2", ddlDS.SelectedValue,gvfilterItemCategory.SelectedValue,ddlShownto.SelectedValue.ToString() }, "dataset");
            if (ds4 != null && ds4.Tables[0].Rows.Count > 0)
            {
                GVItemDetail.DataSource = ds4.Tables[0];
                ds5 = ds4;
				 btnExport.Visible = true;
                //  ds5.Tables.Add(dt);
            }
            else
            {
                GVItemDetail.DataSource = new string[] { };

            }
            GVItemDetail.DataBind();
            //GVItemDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            //GVItemDetail.UseAccessibleHeader = true;

            //foreach (GridViewRow rows in GVItemDetail.Rows)
            //{
            //    Label lblID = (Label)rows.FindControl("lblID");
            //    LinkButton lnkdelete = (LinkButton)rows.FindControl("Delete");
            //    string id = lblID.Text;
            //    ds = objdb.ByProcedure("SpFinItemTx",
            //                 new string[] { "flag", "Item_id" },
            //                 new string[] { "2", id.ToString() }, "dataset");
            //    if (ds != null && ds.Tables[0].Rows.Count > 0)
            //    {
            //        if (ds.Tables[0].Rows[0]["Status"].ToString() == "true")
            //        {
            //            lnkdelete.Visible = false;
            //        }
            //        else
            //        {
            //            lnkdelete.Visible = false;
            //        }
            //    }
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1): " + ex.Message.ToString());
        }
    }
    protected void GVItemDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string id = GVItemDetail.DataKeys[e.RowIndex].Value.ToString();

            objdb.ByProcedure("SpItemMaster",
                              new string[] { "flag", "ItemId" },
                              new string[] { "2", id.ToString() }, "dataset");
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            FillGrid();



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1): " + ex.Message.ToString());
        }
    }
   
    protected void GVItemDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //FillGrid();
            lblMsg.Text = "";

            ddlPackgngMode.ClearSelection();
            txtPackgngSz.Text = "";

            GVItemDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVItemDetail.UseAccessibleHeader = true;
            string ItemId = GVItemDetail.SelectedDataKey.Value.ToString();
            ViewState["ItemId"] = ItemId;
            ds = objdb.ByProcedure("SpItemMaster",
                new string[] { "flag", "ItemId" },
                new string[] { "30", ViewState["ItemId"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                txtItemNameHindi.Text = ds.Tables[0].Rows[0]["ItemName_Hindi"].ToString();
                ItemGroup.ClearSelection();
                txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                ItemGroup.Items.FindByValue(ds.Tables[0].Rows[0]["ItemCat_id"].ToString()).Selected = true;
                FillItemCategory();
                ddlItemCategory.ClearSelection();
                ddlItemCategory.Items.FindByValue(ds.Tables[0].Rows[0]["ItemType_id"].ToString()).Selected = true;
                ddlUnit.ClearSelection();
                ddlUnit.Items.FindByValue(ds.Tables[0].Rows[0]["Unit_Id"].ToString()).Selected = true;
                txtItemOrderNo.Text = ds.Tables[0].Rows[0]["ItemOrderNo"].ToString();
                if (ds.Tables[0].Rows[0]["HSNCode"].ToString() != "")
                {
                    ddlHsnCode.ClearSelection();
                    ddlHsnCode.Items.FindByText(ds.Tables[0].Rows[0]["HSNCode"].ToString()).Selected = true;
                }
               
                if (ds.Tables[0].Rows[0]["ItemSpecification"].ToString() != "")
                {
                    txtItemSpecification.Text = ds.Tables[0].Rows[0]["ItemSpecification"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PackagingSize"].ToString() != "")
                {
                    txtPackgngSz.Text = ds.Tables[0].Rows[0]["PackagingSize"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Packaging_Mode"].ToString() != "")
                {
                    ddlPackgngMode.ClearSelection();
                    ddlPackgngMode.Items.FindByValue(ds.Tables[0].Rows[0]["Packaging_Mode"].ToString()).Selected = true;
                }
                foreach (ListItem item in chkShownto.Items)
                {
                    if (item.Value == "Marketing")
                    {
                        if (ds.Tables[0].Rows[0]["MarketingSection"].ToString() == "1")
                        {
                            item.Selected = true;
                        }
                    }
                    if (item.Value == "Plant")
                    {
                        if (ds.Tables[0].Rows[0]["PlantSection"].ToString() == "1")
                        {
                            item.Selected = true;
                        }
                    }
                }
                btnSubmit.Text = "Update";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    public string FirstLetterToUpper(string str)
    {
        string txt = cult.TextInfo.ToTitleCase(str.ToLower());
        //  System.Web.Globalization.cult.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        return txt;
    }
    protected void FillItemCategory()
    {
        lblMsg.Text = "";
        try
        {
            if (ItemGroup.SelectedIndex != 0)
            {
                ddlItemCategory.Enabled = true;
                ds2 = objdb.ByProcedure("SpItemType",
                                    new string[] { "flag", "ItemCat_id" },
                                    new string[] { "6", ItemGroup.SelectedValue }, "dataset");

                ddlItemCategory.DataSource = ds2;
                ddlItemCategory.DataTextField = "ItemTypeName";
                ddlItemCategory.DataValueField = "ItemType_id";
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlItemCategory.Items.Clear();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
                ddlItemCategory.Enabled = false;

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<string> SearchCustomers(string ItemName)
    {
        List<string> customers = new List<string>();
        try
        {
            DataView dv = new DataView();
            dv = ds5.Tables[1].DefaultView;
            dv.RowFilter = "ItemName like '%" + ItemName + "%'";
            DataTable dt = dv.ToTable();

            foreach (DataRow rs in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    customers.Add(rs[col].ToString());
                }
            }

        }
        catch { }
        return customers;
    }


    [WebMethod]

    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public string[] GetOfficedetail(string prefix, string selectedValue)
    public string[] GetOfficedetail(string prefix)
    {
        List<string> Officedetail = new List<string>();
        //using (SqlDataReader sdr = cmd.ExecuteReader())
        //{
        //    while (sdr.Read())
        //    {
        //        Officedetail.Add(string.Format("{0}-{1}", sdr["OfficerName"], sdr["ExecutiveID"]));
        //    }
        //}
        return Officedetail.ToArray();

    }

    protected void chkActive_CheckedChanged(object sender, EventArgs e)
    {
        // lblMsg.Text = "";
        // GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
        // int index = row.RowIndex;
        // Label lblID = (Label)GVItemDetail.Rows[index].FindControl("lblID");
        // CheckBox chk = (CheckBox)GVItemDetail.Rows[index].FindControl("chkActive");
        // string Item_id = lblID.Text;
        // string ItemOffice_IsActive = "0";
        // if (chk.Checked == true)
        // {
            // ItemOffice_IsActive = "1";
        // }
        //objdb.ByProcedure("Usp_MilkProductCreation", new string[] { "flag", "Item_id", "Office_ID", "ItemOffice_IsActive" }, new string[] { "5", Item_id, ViewState["Office_ID"].ToString(), ItemOffice_IsActive }, "dataset");
        FillGrid();
    }
    protected void gvfilterItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void ddlShownto_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
	 public override void VerifyRenderingInServerForm(Control control)
    {
        try
        {


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            
            GVItemDetail.Columns[11].Visible = false;
            string FileName = Session["Office_Name"] + "_" + "Milk/ProductDetail";
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            GVItemDetail.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            
            GVItemDetail.Columns[10].Visible = true;
            Response.End();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
}