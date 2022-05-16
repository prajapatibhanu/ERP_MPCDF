using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

public partial class mis_Admin_ItemMaster_Production : System.Web.UI.Page
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

                    FillOffice();
                    FillPackagingMode();
                    ViewState["ItemId"] = "0";
                    ddlItemCategory.Enabled = false;

                    ds = objdb.ByProcedure("SpItemCategory",
                                new string[] { "flag" },
                                new string[] { "1" }, "dataset");

                    ItemGroup.DataSource = ds;
                    ItemGroup.DataTextField = "ItemCatName";
                    ItemGroup.DataValueField = "ItemCat_id";
                    ItemGroup.DataBind();
                    ItemGroup.Items.Insert(0, new ListItem("Select", "0"));
                    ItemGroup.SelectedValue = "1";
                    //ItemGroup.Enabled = false;
                    ds1 = objdb.ByProcedure("SpUnit",
                                new string[] { "flag" },
                                new string[] { "7" }, "dataset");
                    FillItemCategory();
                    ddlUnit.DataSource = ds1;
                    ddlUnit.DataTextField = "UnitName";
                    ddlUnit.DataValueField = "Unit_Id";
                    ddlUnit.DataBind();
                    ddlUnit.Items.Insert(0, "Select");

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
                chkOffice.DataSource = ds.Tables[0];
                chkOffice.DataTextField = "Office_Name";
                chkOffice.DataValueField = "Office_ID";
                chkOffice.DataBind();

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
            FillGrid();
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
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }



    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        string msg = "";
    //        if (txtItemName.Text == "")
    //        {
    //            msg = "Enter Item Name";
    //        }
    //        if (ItemGroup.SelectedIndex <= 0)
    //        {
    //            msg += "Select Nature. \\n";
    //        }
    //        if (ddlItemCategory.SelectedIndex <= 0)
    //        {
    //            msg += "Select Item Group. \\n";
    //        }

    //        if (ddlUnit.SelectedIndex <= 0)
    //        {
    //            msg += "Select Unit. \\n";
    //        }

    //        if (msg.Trim() == "")
    //        {
    //            int Status = 0;

    //            txtItemName.Text = FirstLetterToUpper(txtItemName.Text);
    //            DataSet ds11 = objdb.ByProcedure("SpItemMaster",
    //                new string[] { "flag", "ItemName", "GroupId", "ItemId" },
    //                new string[] { "29", txtItemName.Text, ItemGroup.SelectedValue, ViewState["ItemId"].ToString() }, "dataset");
    //            if (ds11.Tables[0].Rows.Count > 0)
    //            {
    //                Status = Convert.ToInt32(ds11.Tables[0].Rows[0]["Status"].ToString());

    //            }
    //            if (btnSubmit.Text == "Submit" && ViewState["ItemId"].ToString() == "0" && Status == 0)
    //            {
    //                ds = objdb.ByProcedure("SpItemMaster",
    //                            new string[] { "flag", "GroupId", "CategoryId", "UnitId", "ItemName", "ItemName_Hindi", "ItemAliasCode", "CreatedBy", "ItemSpecification", "PackagingSize", "Packaging_Mode", "ItemOrderNo" },
    //                            new string[] { "26", ItemGroup.SelectedValue, ddlItemCategory.SelectedValue, ddlUnit.SelectedValue, txtItemName.Text, txtItemNameHindi.Text, txtitemaliscode.Text, ViewState["Emp_ID"].ToString(), txtItemSpecification.Text, txtPackgngSz.Text, ddlPackgngMode.SelectedValue.ToString(), txtItemOrderNo.Text }, "dataset");
    //                if (ds != null && ds.Tables[0].Rows.Count > 0)
    //                {
    //                    string ItemId = ds.Tables[0].Rows[0]["ItemId"].ToString();
    //                    if (ItemId != "")
    //                    {
    //                        foreach (ListItem item in chkOffice.Items)
    //                        {
    //                            if (item.Selected == true)
    //                            {
    //                                objdb.ByProcedure("SpItemMasterChild",
    //                                    new string[] { "flag", "Item_id", "Office_ID", "CreatedBy" },
    //                                    new string[] { "0", ItemId, item.Value, ViewState["Emp_ID"].ToString() }, "dataset");


    //                            }
    //                        }

    //                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
    //                        ClearData();
    //                        FillGrid();

    //                    }
    //                }
    //            }
    //            else if (btnSubmit.Text == "Update" && ViewState["ItemId"].ToString() != "0" && Status == 0)
    //            {

    //                objdb.ByProcedure("SpItemMaster",
    //                       new string[] { "flag", "ItemId", "GroupId", "CategoryId", "UnitId", "ItemName", "ItemName_Hindi", "ItemAliasCode", "CreatedBy", "ItemSpecification", "PackagingSize", "Packaging_Mode", "ItemOrderNo" },
    //                       new string[] { "27", ViewState["ItemId"].ToString(), ItemGroup.SelectedValue, ddlItemCategory.SelectedValue, ddlUnit.SelectedValue, txtItemName.Text, txtItemNameHindi.Text, txtitemaliscode.Text, ViewState["Emp_ID"].ToString(), txtItemSpecification.Text, txtPackgngSz.Text, ddlPackgngMode.SelectedValue.ToString(), txtItemOrderNo.Text }, "dataset");
    //                objdb.ByProcedure("SpItemMasterChild", new string[] { "flag", "Item_id" }, new string[] { "4", ViewState["ItemId"].ToString() }, "dataset");
    //                foreach (ListItem item in chkOffice.Items)
    //                {
    //                    if (item.Selected == true)
    //                    {

    //                        objdb.ByProcedure("SpItemMasterChild",
    //                            new string[] { "flag", "Item_id", "Office_ID", "CreatedBy" },
    //                            new string[] { "3", ViewState["ItemId"].ToString(), item.Value, ViewState["Emp_ID"].ToString() }, "dataset");

    //                    }
    //                }

    //                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
    //                ClearData();
    //                FillGrid();
    //                btnSubmit.Text = "Submit";
    //            }

    //            else
    //            {
    //                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Item Name is already exist.');", true);
    //                ClearData();

    //            }
    //        }
    //        else
    //        {
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
    //        }
    //    }

    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
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
            
            if (msg.Trim() == "")
            {
                int Status = 0;

                txtItemName.Text = FirstLetterToUpper(txtItemName.Text);

                if (btnSubmit.Text == "Submit")
                {
                    ds = objdb.ByProcedure("SpItemMaster",
                                new string[] { "flag", "GroupId", "CategoryId", "UnitId",  "ItemName", "ItemName_Hindi", "ItemAliasCode", "CreatedBy",  "ItemSpecification","PackagingSize", "Packaging_Mode", "ItemOrderNo", "Office_ID", "CreatedAt", "CreatedByIP" },
                                new string[] { "1", ItemGroup.SelectedValue, ddlItemCategory.SelectedValue, ddlUnit.SelectedValue, txtItemName.Text, txtItemNameHindi.Text, txtitemaliscode.Text, ViewState["Emp_ID"].ToString(),  txtItemSpecification.Text, txtPackgngSz.Text, ddlPackgngMode.SelectedValue.ToString(), txtItemOrderNo.Text, objdb.Office_ID(), objdb.Office_ID(), objdb.GetLocalIPAddress() }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "" + Success + "");


                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                            string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "oops!", "" + Warning + "");


                        }
                        else
                        {
                            string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "" + Danger + "");


                        }

                    }
                    FillGrid();
                    ClearData();
                }
                else if(btnSubmit.Text == "Update")
                {
                    ds = objdb.ByProcedure("SpItemMaster",
                              new string[] { "flag", "ItemId", "GroupId", "CategoryId", "UnitId", "ItemName", "ItemName_Hindi", "ItemAliasCode",  "ItemSpecification",  "PackagingSize", "Packaging_Mode", "ItemOrderNo", "Office_ID", "CreatedBy", "CreatedAt", "CreatedByIP" },
                              new string[] { "17", ViewState["ItemId"].ToString(), ItemGroup.SelectedValue, ddlItemCategory.SelectedValue, ddlUnit.SelectedValue, txtItemName.Text, txtItemNameHindi.Text, txtitemaliscode.Text,  txtItemSpecification.Text,  txtPackgngSz.Text, ddlPackgngMode.SelectedValue.ToString(), txtItemOrderNo.Text, objdb.Office_ID(), objdb.createdBy(), objdb.Office_ID(), objdb.GetLocalIPAddress() }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "" + Success + "");


                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                            string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "oops!", "" + Warning + "");


                        }
                        else
                        {
                            string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "" + Danger + "");


                        }

                    }
                    FillGrid();
                    ClearData();
                }

                else
                {
                   
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
        chkOffice.ClearSelection();
        chkDistrict.Checked = false;
        chkOffice.ClearSelection();
        btnSubmit.Text = "Submit";

    }
    private void FillGrid()
    {
        try
        {
            ds4 = objdb.ByProcedure("SpItemMaster",
                        new string[] { "flag", "GroupId", "CategoryId", "Office_ID" },
                        new string[] { "19", ItemGroup.SelectedValue.ToString(), ddlItemCategory.SelectedValue.ToString(), objdb.Office_ID() }, "dataset");
            if (ds4 != null && ds4.Tables[0].Rows.Count > 0)
            {
                GVItemDetail.DataSource = ds4.Tables[0];


                ds5 = ds4;
                //  ds5.Tables.Add(dt);
            }
            else
            {
                GVItemDetail.DataSource = new string[] { };

            }
            GVItemDetail.DataBind();
            GVItemDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVItemDetail.UseAccessibleHeader = true;
            foreach (GridViewRow rows in GVItemDetail.Rows)
            {
                Label lblID = (Label)rows.FindControl("lblID");
                LinkButton lnkdelete = (LinkButton)rows.FindControl("Delete");
                string id = lblID.Text;
                ds = objdb.ByProcedure("SpFinItemTx",
                             new string[] { "flag", "Item_id" },
                             new string[] { "2", id.ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Status"].ToString() == "true")
                    {
                        lnkdelete.Visible = false;
                    }
                    else
                    {
                        lnkdelete.Visible = true;
                    }
                }
            }
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
    protected void GVItemDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GVItemDetail.PageIndex = e.NewPageIndex;
            ds = objdb.ByProcedure("SpItemMaster",
                        new string[] { "flag" },
                        new string[] { "8" }, "dataset");
            if (ds.Tables[0].Rows.Count != 0)
            {
                GVItemDetail.DataSource = ds;
                GVItemDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GVItemDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //FillGrid();
            lblMsg.Text = "";
            chkDistrict.Checked = false;
            chkOffice.ClearSelection();
            ddlPackgngMode.ClearSelection();
            txtPackgngSz.Text = "";

            GVItemDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVItemDetail.UseAccessibleHeader = true;
            string ItemId = GVItemDetail.SelectedDataKey.Value.ToString();
            ViewState["ItemId"] = ItemId;
            ds = objdb.ByProcedure("SpItemMaster",
                new string[] { "flag", "ItemId" },
                new string[] { "16", ViewState["ItemId"].ToString() }, "dataset");
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

}