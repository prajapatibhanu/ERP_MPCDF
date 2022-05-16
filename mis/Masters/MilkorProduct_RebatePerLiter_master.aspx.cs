using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using DocumentFormat.OpenXml.Wordprocessing;

public partial class mis_Masters_MilkorProduct_RebatePerLiter_master : System.Web.UI.Page
{
    APIProcedure objdb1 = new APIProcedure();
    //CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds1, ds2, ds3, ds4;
    static DataSet ds5;
    AbstApiDBApi objdb = new APIProcedure();
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


                   // FillPackagingMode();
                    ViewState["ItemId"] = "0";
                   // ddlItemCategory.Enabled = false;

                    ds = objdb.ByProcedure("Usp_MilkProductCreation",
                                new string[] { "flag" },
                                new string[] { "4" }, "dataset");
                    ddlItemCategory.DataTextField = "ItemCatName";
                    ddlItemCategory.DataValueField = "ItemCat_id";
                    ddlItemCategory.DataSource = ds.Tables[0];
                    ddlItemCategory.DataBind();
                    ddlItemCategory.SelectedValue = "3";
                    ddlItemCategory.Enabled = false;

                    GetLocation();
                   // FillOffice();
                   // FillGrid();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    getdata();


                    //corrent rebate
                     //ds = objdb.ByProcedure("Usp_Mst_MilkorProduct_special_Rebate",
                     //           new string[] { "flag", "Office_ID", "ItemCat_id", "AreaId" },
                     //           new string[] { "1", objdb1.Office_ID(), ddlItemCategory.SelectedValue, ddlLocation.SelectedValue }, "dataset");
                     //if (ds != null &&  ds.Tables.Count > 1 && ds.Tables[0].Rows.Count > 0)
                     //{
                     //    if (ds.Tables[1].Rows[0]["Msg"].ToString() == "Ok")
                     //    {
                     //        ddlLocation.SelectedValue = ds.Tables[0].Rows[0]["AreaId"].ToString();
                     //        ddlItemCategory.SelectedValue = ds.Tables[0].Rows[0]["ItemCat_id"].ToString();
                     //        txtEffectiveDate.Text = ds.Tables[0].Rows[0]["EffectiveDate"].ToString();
                     //        txtRebate_Amount.Text = ds.Tables[0].Rows[0]["RebatePerLiter_Amt"].ToString();
                     //    }
                     //}
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
    public void getdata()
    {
        try
        {

            ds = objdb.ByProcedure("Usp_Mst_MilkorProduct_special_Rebate",
                       new string[] { "flag", "Office_ID", "ItemCat_id", "AreaId" },
                       new string[] { "1", objdb1.Office_ID(), ddlItemCategory.SelectedValue, ddlLocation.SelectedValue }, "dataset");
            if (ds != null && ds.Tables.Count > 1 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[1].Rows[0]["Msg"].ToString() == "Ok")
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GVrebateDetail.DataSource = ds.Tables[0];
                        GVrebateDetail.DataBind();
                        GVrebateDetail.Visible = true;
                    }
                    else
                    {
                        GVrebateDetail.DataSource = null;
                        GVrebateDetail.DataBind();
                    }
                }
            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
            ddlLocation.SelectedValue = "2";
            
          // ddlLocation.Items.Insert(0, new ListItem("All", "0"));
            ddlLocation.Enabled = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
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
                //ddlDS.DataSource = ds.Tables[0];
                //ddlDS.DataTextField = "Office_Name";
                //ddlDS.DataValueField = "Office_ID";
                //ddlDS.DataBind();
                //ddlDS.Items.Insert(0, new ListItem("Select", "0"));
                //ddlDS.SelectedValue = ViewState["Office_ID"].ToString();
                //ddlDS.Enabled = false;

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
            if (txtRebate_Amount.Text == "")
            {
                msg = "Enter Rebate Amount. \\n";
            }
            if (txtEffectiveDate.Text =="")
            {
                msg += "Select Effective Date. \\n";
            }
            
            if (msg.Trim() == "")
            {

                if (btnSubmit.Text == "Submit")
                {
                    ds = objdb.ByProcedure("Usp_Mst_MilkorProduct_special_Rebate",
                                new string[] { "flag", "Office_ID", "ItemCat_id", "AreaId", "RebatePerLiter_Amt", "EffectiveDate", "IsActive", "CreatedBy", "CreatedByIP" },
                                new string[] { "0", objdb1.Office_ID(), ddlItemCategory.SelectedValue, ddlLocation.SelectedValue, txtRebate_Amount.Text, txtEffectiveDate.Text, "1", ViewState["Emp_ID"].ToString(),"" }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "" + Success + "");
                            ClearData();

                            getdata();
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                            string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "oops!", "" + Warning + "");
                            ClearData();
                            getdata();

                        }
                        else
                        {
                            string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "" + Danger + "");
                            ClearData();
                            getdata();

                        }

                    }
                }
                else if (btnSubmit.Text == "Update" &&  lblMilkorProduct_Rebate_Id.Text != " ")
                {

                    ds = objdb.ByProcedure("Usp_Mst_MilkorProduct_special_Rebate",
                               new string[] { "flag", "Office_ID", "ItemCat_id", "AreaId", "RebatePerLiter_Amt", "EffectiveDate", "IsActive", "CreatedBy", "CreatedByIP" },
                               new string[] { "3", objdb1.Office_ID(), ddlItemCategory.SelectedValue, ddlLocation.SelectedValue, txtRebate_Amount.Text, txtEffectiveDate.Text, "1", ViewState["Emp_ID"].ToString(), "" }, "dataset");

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "" + Success + "");
                        ClearData();
                        getdata();

                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Not Ok")
                    {
                        string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "oops!", "" + Warning + "");
                        ClearData();
                        getdata();

                    }
                    else
                    {
                        string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "" + Danger + "");
                        ClearData();
                        getdata();

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
        txtEffectiveDate.Text = "";
        txtRebate_Amount.Text = "";


    }
   

    

    public string FirstLetterToUpper(string str)
    {
        string txt = cult.TextInfo.ToTitleCase(str.ToLower());
        //  System.Web.Globalization.cult.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        return txt;
    }
    protected void FillItemCategory()
    {
        //lblMsg.Text = "";
        //try
        //{
        //    if (ItemGroup.SelectedIndex != 0)
        //    {
        //        ddlItemCategory.Enabled = true;
        //        ds2 = objdb.ByProcedure("SpItemType",
        //                            new string[] { "flag", "ItemCat_id" },
        //                            new string[] { "6", ItemGroup.SelectedValue }, "dataset");

        //        ddlItemCategory.DataSource = ds2;
        //        ddlItemCategory.DataTextField = "ItemTypeName";
        //        ddlItemCategory.DataValueField = "ItemType_id";
        //        ddlItemCategory.DataBind();
        //        ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
        //    }
        //    else
        //    {
        //        ddlItemCategory.Items.Clear();
        //        ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));
        //        ddlItemCategory.Enabled = false;

        //    }
        //}
        //catch (Exception ex)
        //{
        //    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        //}

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
        //lblMsg.Text = "";
        //GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
        //int index = row.RowIndex;
        //Label lblID = (Label)GVrebateDetail.Rows[index].FindControl("lblID");
        //CheckBox chk = (CheckBox)GVrebateDetail.Rows[index].FindControl("chkActive");
        //string Item_id = lblID.Text;
        //string ItemOffice_IsActive = "0";
        //if (chk.Checked == true)
        //{
        //    ItemOffice_IsActive = "1";
        //}
        //objdb.ByProcedure("Usp_MilkProductCreation", new string[] { "flag", "Item_id", "Office_ID", "ItemOffice_IsActive" }, new string[] { "5", Item_id, ViewState["Office_ID"].ToString(), ItemOffice_IsActive }, "dataset");
        //FillGrid();
    }
    
    protected void GVrebateDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            int MilkorProduct_Rebate_Id = Convert.ToInt32(e.CommandArgument.ToString());
            lblMilkorProduct_Rebate_Id.Text = e.CommandArgument.ToString();
            if (e.CommandName == "RecordUpdate")
            {
                
               ds = objdb.ByProcedure("Usp_Mst_MilkorProduct_special_Rebate",
                       new string[] { "flag", "Office_ID", "MilkorProduct_Rebate_Id" },
                       new string[] { "2", objdb1.Office_ID(), lblMilkorProduct_Rebate_Id.Text }, "dataset");

               DataTable ds1 = ds.Tables[1];
               if (ds.Tables.Count > 0)
               {
                   if (ds.Tables[0].Rows.Count > 0)


                       ddlLocation.SelectedValue = ds.Tables[0].Rows[0]["AreaId"].ToString();
                   ddlItemCategory.SelectedValue = ds.Tables[0].Rows[0]["ItemCat_id"].ToString();
                   txtEffectiveDate.Text = ds.Tables[0].Rows[0]["EffectiveDate"].ToString();
                   txtRebate_Amount.Text = ds.Tables[0].Rows[0]["RebatePerLiter_Amt"].ToString();




                  //ViewState["rowid"] = e.CommandArgument;

                   btnSubmit.Text = "Update";

               }

            }
            else if (e.CommandName == "View")
            {
                ////child table data
                //Control ctrl = e.CommandSource as Control;
                //if (ctrl != null)
                //{
                    lblMsg.Text = string.Empty;
                    lblModalMsg.Text = string.Empty;
                    ds = objdb.ByProcedure("Usp_Mst_MilkorProduct_special_Rebate",
                       new string[] { "flag", "Office_ID", "ItemCat_id", "AreaId", "MilkorProduct_Rebate_Id" },
                       new string[] { "4", objdb1.Office_ID(), ddlItemCategory.SelectedValue, ddlLocation.SelectedValue,lblMilkorProduct_Rebate_Id.Text }, "dataset");
                    if (ds != null && ds.Tables.Count> 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        DataTable ds1 = ds.Tables[1];
                        if (ds.Tables[1].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                GVrebateallDetail.DataSource = ds.Tables[0];
                                GVrebateallDetail.DataBind();
                                GVrebateallDetail.Visible = true;
                            }
                            else
                            {
                                GVrebateDetail.DataSource = null;
                                GVrebateDetail.DataBind();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "No Record Exists");
                            }
                       }
                    }
                    //GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;

                    //Label lblDemandDate = (Label)row.FindControl("lblDemandDate");
                    //Label lblBandOName = (Label)row.FindControl("lblBandOName");
                    //Label lblShiftName = (Label)row.FindControl("lblShiftName");
                    //Label lblItemCatid = (Label)row.FindControl("lblItemCatid");
                    //Label lblDemandStatus = (Label)row.FindControl("lblDemandStatus");
                    //ViewState["rowid"] = e.CommandArgument.ToString();
                    //ViewState["rowitemcatid"] = lblItemCatid.Text;
                    //GetItemDetailByDemandID();

                    //modalBoothName.InnerHtml = lblBandOName.Text;
                    //modaldate.InnerHtml = lblDemandDate.Text;
                    //modalshift.InnerHtml = lblShiftName.Text;
                    //modalorderStatus.InnerHtml = lblDemandStatus.Text;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    //GetDatatableHeaderDesign();

               // }
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}