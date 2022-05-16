using System;
using System.Data;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Collections.Generic;
using System.Web.UI;
using System.Web;
using System.Data.OleDb;

public partial class mis_MilkCollection_MilkCollectionAdditionDeductionEntry_New : System.Web.UI.Page
{
    DataSet ds;
    static DataSet ds5;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!IsPostBack)
            {
                txtDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDate.Attributes.Add("readonly", "readonly");
                txtFilterFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFilterFromDate.Attributes.Add("readonly", "readonly");
                txtFilterToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFilterToDate.Attributes.Add("readonly", "readonly");
                //txtBillingCycleFromDate.Text = Convert.ToDateTime(txtBillingCycleFromDate.Text, cult).ToString("dd/MM/yyyy");
                //txtBillingCycleToDate.Text = Convert.ToDateTime(txtBillingCycleToDate.Text, cult).ToString("dd/MM/yyyy");
                FillBMCRoot();
                ddlEntryType_SelectedIndexChanged(sender,e);

                GetCCDetails();
                SetInFlowInitialRow();
                if(objdb.Office_ID() == "2")
                {
                    ddlBillingCycle.SelectedValue = "5 days";
                }
                else
                {
                    ddlBillingCycle.SelectedValue = "10 days";
                }
                ddlfltEntryType_SelectedIndexChanged(sender, e);
                txtDate_TextChanged(sender, e);
                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }

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
    protected void FillSociety()
    {
        try
        {
			Session["ds1"] = "";
            ds = null;
            

            if (ddlEntryType.SelectedItem.Text == "Route Wise")
            {
                ds = objdb.ByProcedure("Usp_MilkCollectionRoutWiseAdditionsDeductionsEntry", new string[] { "flag", "BMCTankerRoot_Id", "Office_ID", "OfficeType_ID" }, new string[] { "9", ddlBMCTankerRootName.SelectedValue, objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
						Session["ds1"] = ds;
                        //ds6 = ds;

                    }
                    else
                    {
                        // ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
                else
                {
                    // ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            if (ddlEntryType.SelectedItem.Text == "Chilling Center Wise")
            {
                ds = objdb.ByProcedure("Usp_MilkCollectionRoutWiseAdditionsDeductionsEntry",
                         new string[] { "flag", "Office_ID", "Office_Parant_ID", "OfficeType_ID" },
                         new string[] { "8", ddlCC.SelectedValue.ToString(),objdb.Office_ID(),objdb.OfficeType_ID() }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
						Session["ds1"] = ds;
                        //ds6 = ds;

                    }
                    else
                    {
                        // ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
                else
                {
                    // ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    public void FillBMCRoot()
    {

        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
                     new string[] { "flag", "Office_ID" },
                     new string[] { "1", objdb.Office_ID() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlBMCTankerRootName.DataTextField = "BMCTankerRootName";
                ddlBMCTankerRootName.DataValueField = "BMCTankerRoot_Id";
                ddlBMCTankerRootName.DataSource = ds;
                ddlBMCTankerRootName.DataBind();
                ddlBMCTankerRootName.Items.Insert(0, new ListItem("Select", "0"));

                ddlFltrddlBMCTankerRootName.DataTextField = "BMCTankerRootName";
                ddlFltrddlBMCTankerRootName.DataValueField = "BMCTankerRoot_Id";
                ddlFltrddlBMCTankerRootName.DataSource = ds;
                ddlFltrddlBMCTankerRootName.DataBind();
                ddlFltrddlBMCTankerRootName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }

    }
    protected void GetCCDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
                   new string[] { "flag", "I_OfficeID", "I_OfficeTypeID" },
                   new string[] { "3", objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlCC.DataTextField = "Office_Name";
                        ddlCC.DataValueField = "Office_ID";
                        ddlCC.DataSource = ds;
                        ddlCC.DataBind();
                        ddlCC.Items.Insert(0, new ListItem("Select", "0"));

                        ddlFltrCC.DataTextField = "Office_Name";
                        ddlFltrCC.DataValueField = "Office_ID";
                        ddlFltrCC.DataSource = ds;
                        ddlFltrCC.DataBind();
                        ddlFltrCC.Items.Insert(0, new ListItem("Select", "0"));
						if (objdb.OfficeType_ID() == "4")
                        {
                            ddlCC.SelectedValue = objdb.Office_ID();
                            ddlFltrCC.SelectedValue = objdb.Office_ID();
                        }

                    }
                }
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlHeadType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            if (ddlHeadType.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("USP_Mst_ItemBillingHeadMaster",
               new string[] { "flag", "ItemBillingHead_Type", "Office_ID", "OfficeType_ID" },
               new string[] { "8", ddlHeadType.SelectedValue,objdb.Office_ID(),objdb.OfficeType_ID() }, "dataset");

                ddlHeaddetails.DataTextField = "ItemBillingHead_Name";
                ddlHeaddetails.DataValueField = "ItemBillingHead_ID";
                ddlHeaddetails.DataSource = ds;
                ddlHeaddetails.DataBind();
                ddlHeaddetails.Items.Insert(0, new ListItem("Select", "0"));
                SetFocus(ddlHeaddetails);

            }
            else
            {
                ddlHeadType.ClearSelection();
                ddlHeaddetails.Items.Clear();
                ddlHeaddetails.Items.Add(new ListItem("Select", "0"));
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    protected void ddlHeaddetails_Init(object sender, EventArgs e)
    {
        try
        {
            ddlHeaddetails.Items.Add(new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    //protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
    //    int Index = row.RowIndex;
    //    RequiredFieldValidator rfvHeadAmount = (RequiredFieldValidator)gvDetails.Rows[Index].FindControl("rfvHeadAmount");
    //    RequiredFieldValidator rfvQuantity = (RequiredFieldValidator)gvDetails.Rows[Index].FindControl("rfvQuantity");
    //    CheckBox chkSelect = (CheckBox)gvDetails.Rows[Index].FindControl("chkSelect");
    //    if(chkSelect.Checked)
    //    {
    //        rfvHeadAmount.Enabled = true;
    //        rfvQuantity.Enabled = true;
    //    }
    //    else
    //    {
    //        rfvHeadAmount.Enabled = false;
    //        rfvQuantity.Enabled = false;
    //    }
    //}

    private DataTable GetDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Office_ID", typeof(int));
        dt.Columns.Add("ItemBillingHead_ID", typeof(int));
        dt.Columns.Add("HeadAmount", typeof(decimal));
        dt.Columns.Add("Quantity", typeof(decimal));
        dt.Columns.Add("HeadRemark", typeof(string));
        foreach (GridViewRow row in gvDetails.Rows)
        {
            HiddenField hfOffice_ID = (HiddenField)row.FindControl("hfOffice_ID");
            TextBox txtSociety = (TextBox)row.FindControl("txtSociety");
            TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
            
            TextBox txtRemark = (TextBox)row.FindControl("txtRemark");


            if (txtSociety.Text != "" && txtAmount.Text != "" && txtAmount.Text != "0")
            {

                dt.Rows.Add(hfOffice_ID.Value, ddlHeaddetails.SelectedValue, txtAmount.Text, "0", txtRemark.Text);

            }
            else
            {

            }
        }

        return dt;
    }
    //protected void ddlFltHeadType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        lblRptMsg.Text = "";
    //        if (ddlFltHeadType.SelectedValue != "0")
    //        {
    //            ds = null;
    //            ds = objdb.ByProcedure("USP_Mst_ItemBillingHeadMaster",
    //           new string[] { "flag", "ItemBillingHead_Type" },
    //           new string[] { "8", ddlFltHeadType.SelectedValue }, "dataset");

    //            ddlFltHeaddetails.DataTextField = "ItemBillingHead_Name";
    //            ddlFltHeaddetails.DataValueField = "ItemBillingHead_ID";
    //            ddlFltHeaddetails.DataSource = ds;
    //            ddlFltHeaddetails.DataBind();
    //            ddlFltHeaddetails.Items.Insert(0, new ListItem("All", "0"));

    //        }
    //        else
    //        {
    //            ddlHeadType.ClearSelection();
    //            ddlHeaddetails.Items.Clear();
    //            //ddlHeaddetails.Items.Add(new ListItem("Select", "0"));
    //        }



    //    }
    //    catch (Exception ex)
    //    {
    //        lblRptMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

    //    }
    //}
    protected void btnFltSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string BMCTankerRoot_Id;
			btnExport.Visible = false;
            if (ddlfltEntryType.SelectedItem.Text == "Route Wise")
            {
                BMCTankerRoot_Id = ddlFltrddlBMCTankerRootName.SelectedValue;
            }
            else
            {
                BMCTankerRoot_Id = ddlFltrCC.SelectedValue;
            }
            ds = objdb.ByProcedure("Usp_MilkCollectionRoutWiseAdditionsDeductionsEntry", new string[] { "flag", "FromDate", "ToDate", "EntryType", "BMCTankerRoot_Id" }, new string[] { "11", Convert.ToDateTime(txtFilterFromDate.Text, cult).ToString("yyyy/MM/dd"),Convert.ToDateTime(txtFilterToDate.Text, cult).ToString("yyyy/MM/dd"), ddlfltEntryType.SelectedItem.Text, BMCTankerRoot_Id }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
					btnExport.Visible = true;
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                    decimal TotalAmount = 0;
                    TotalAmount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("HeadAmount"));

                    GridView1.FooterRow.Cells[4].Text = "<b>Total : </b>";
                    GridView1.FooterRow.Cells[5].Text = "<b>" + TotalAmount.ToString() + "</b>";
                   
                }
                else
                {
                    GridView1.DataSource = string.Empty;
                    GridView1.DataBind();
                }
            }
            else
            {
                GridView1.DataSource = string.Empty;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblRptMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            
            
            lblRptMsg.Text = "";
            lblMsg.Text = "";
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string AddtionsDeducEntry_ID = e.CommandArgument.ToString();
            LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
            LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
            Label lblHeadAmount = (Label)row.FindControl("lblHeadAmount");
            
            Label lblRemark = (Label)row.FindControl("lblRemark");
            TextBox txtHeadAmount = (TextBox)row.FindControl("txtHeadAmount");
            
            TextBox txtHeadRemark = (TextBox)row.FindControl("txtHeadRemark");

            if (e.CommandName == "EditRecord")
            {
                lnkUpdate.Visible = true;
                lnkEdit.Visible = false;
                txtHeadAmount.Visible = true;
                lblHeadAmount.Visible = false;
               
                
                txtHeadRemark.Visible = true;
                lblRemark.Visible = false;

            }
            if (e.CommandName == "UpdateRecord")
            {
                DataSet dsUpdate = objdb.ByProcedure("Usp_MilkCollectionRoutWiseAdditionsDeductionsEntry",
                                   new string[] 
                                   {"flag"
                                   ,"AddtionsDeducEntry_ID" 
                                   ,"HeadAmount"                                   
                                   ,"HeadRemark"
                                   ,"CreatedAt"
                                   ,"CreatedBy"
                                   ,"CreatedByIP"
                                   },
                                   new string[] 
                                   {"4"
                                  ,AddtionsDeducEntry_ID
                                  ,txtHeadAmount.Text                               
                                  ,txtHeadRemark.Text
                                  ,objdb.Office_ID()
                                  ,objdb.createdBy()
                                  ,objdb.GetLocalIPAddress()
                                   },
                                   "dataset");
                if (dsUpdate.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {

                    lblRptMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", dsUpdate.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    btnFltSearch_Click(sender, e);

                }
                else
                {
                    lblRptMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", dsUpdate.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    Session["IsSuccess"] = false;
                }


            }
            if (e.CommandName == "DeleteRecord")
            {
                objdb.ByProcedure("Usp_MilkCollectionRoutWiseAdditionsDeductionsEntry", new string[] { "flag", "AddtionsDeducEntry_ID" }, new string[] { "7", AddtionsDeducEntry_ID }, "dataset");
                lblRptMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Deleted Successfully");
                btnFltSearch_Click(sender, e);
            }
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            lblRptMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    //protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        TextBox txtHeadAmount = (TextBox)e.Row.FindControl("txtHeadAmount");

    //        if (txtHeadAmount.Text != "")
    //        {
    //            e.Row.Enabled = false;
    //        }
    //    }
    //}
    //protected void checkAll_CheckedChanged(object sender, EventArgs e)
    //{
    //   foreach(GridViewRow rows in gvDetails.Rows)
    //   {
    //       RequiredFieldValidator rfvHeadAmount = (RequiredFieldValidator)rows.FindControl("rfvHeadAmount");
    //       RequiredFieldValidator rfvQuantity = (RequiredFieldValidator)rows.FindControl("rfvQuantity");
    //       CheckBox chkSelect = (CheckBox)rows.FindControl("chkSelect");
    //       if (chkSelect.Checked)
    //       {
    //           rfvHeadAmount.Enabled = true;
    //           rfvQuantity.Enabled = true;
    //       }
    //       else
    //       {
    //           rfvHeadAmount.Enabled = false;
    //           rfvQuantity.Enabled = false;
    //       }
    //   }

    //}
    protected void ddlEntryType_SelectedIndexChanged(object sender, EventArgs e)
    {
        divEntry.Visible = false;
        colroot.Visible = false;
        colcc.Visible = false;
        if (ddlEntryType.SelectedIndex > 0)
        {
            if (ddlEntryType.SelectedItem.Text == "Route Wise")
            {
                colroot.Visible = true;
                colcc.Visible = false;
                ddlBMCTankerRootName.ClearSelection();
            }
            if (ddlEntryType.SelectedItem.Text == "Chilling Center Wise")
            {
                colcc.Visible = true;
                colroot.Visible = false;
                ddlCC.ClearSelection();
				if (objdb.OfficeType_ID() == "4")
                {
                    ddlCC.SelectedValue = objdb.Office_ID();
                    
                }
            }
        }
    }
    protected void ddlBMCTankerRootName_SelectedIndexChanged(object sender, EventArgs e)
    {
       

    }
    protected void ddlCC_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetValidate();
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<string> SearchSociety(string prefix)
    {
        List<string> society = new List<string>();
        try
        {
            DataView dv = new DataView();
			DataSet ds6 = (DataSet)HttpContext.Current.Session["ds1"];
            dv = ds6.Tables[0].DefaultView;
            dv.RowFilter = "Office_Name like '%" + prefix + "%'";
            DataTable dt = dv.ToTable();

            foreach (DataRow rs in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    society.Add(rs[col].ToString());
                }
            }

        }
        catch { }
        return society;
    }
    protected void txtSociety_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
           
            TextBox txtSociety = (TextBox)row.FindControl("txtSociety");
            TextBox txtSocietyName = (TextBox)row.FindControl("txtSocietyName");
            HiddenField hfOffice_ID = (HiddenField)row.FindControl("hfOffice_ID");
            TextBox txtAmount = (TextBox)row.FindControl("txtAmount");
            Label lblError = (Label)row.FindControl("lblError");
            RequiredFieldValidator rfvSociety = (RequiredFieldValidator)row.FindControl("rfvSociety");

            //RequiredFieldValidator rfvSocietyName = (RequiredFieldValidator)row.FindControl("rfvSocietyName");
          
            if (txtSociety.Text != "")
            {
				DataSet ds = (DataSet)Session["ds1"];
                DataTable dt = ds.Tables[0];
                int status = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    if (dr["Office_Code"].ToString() == txtSociety.Text.Trim())
                    {
                        txtSocietyName.Text = dr["Office_Name"].ToString();
                        hfOffice_ID.Value = dr["Office_ID"].ToString();
                        
                        lblError.Text = "";
                        SetFocus(txtAmount);
                        status = 1;
                        break;
                    }
                }
                

                
                if (status == 0)
                {
                    txtSocietyName.Text = "";
                    txtSociety.Text = "";
                    
                    SetFocus(txtSociety);
                    //rfvSocietyName.Enabled = true;
                    lblError.Text = " कृपया मान्य सोसायटी कोड दर्ज करें।";

                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('कृपया गाय या भैंस की मात्रा दर्ज करें')");
                }
                else
                {
                    
                    // DataTable dt1 = ds5.Tables[0];
                    // for (int i = 0; i < dt1.Rows.Count; i++)
                    // {
                        // DataRow dr = dt1.Rows[i];
                        // if (dr["Office_ID"].ToString() == hfOffice_ID.Value)
                        // {
                            // lblError.Text = "";
                            // SetFocus(txtAmount);
                            // status = 0;
                            // break;
                           
                        // }
                    // }
                    // if (status == 0)
                    // {
                        // txtSocietyName.Text = "";
                        // txtSociety.Text = "";

                        // SetFocus(txtSociety);
                        // //rfvSocietyName.Enabled = true;
                        // lblError.Text = "सोसायटी की जानकारी दर्ज़ की जा छुकी है।";

                        // //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('कृपया गाय या भैंस की मात्रा दर्ज करें')");
                    // }
                }
            }
            else
            {
                // lblError.Text = "";
                txtAmount.Text = "";
                txtSociety.Text = "";
                txtSocietyName.Text = "";
               
                SetFocus(txtSociety);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void SetInFlowInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;


        dt.Columns.Add(new DataColumn("Office_Name", typeof(string)));
        dt.Columns.Add(new DataColumn("SocietyName", typeof(string)));
        dt.Columns.Add(new DataColumn("Office_ID", typeof(string)));
        dt.Columns.Add(new DataColumn("Amount", typeof(string)));
        dt.Columns.Add(new DataColumn("Remark", typeof(string)));
       


        dr = dt.NewRow();

        dr["Office_Name"] = string.Empty;
        dr["SocietyName"] = string.Empty;
        dr["Office_ID"] = string.Empty;
        dr["Amount"] = string.Empty;
        dr["Remark"] = string.Empty;
      

        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;

        gvDetails.DataSource = dt;
        gvDetails.DataBind();
        foreach (GridViewRow rows in gvDetails.Rows)
        {
            TextBox Society = (TextBox)rows.FindControl("txtSociety");
            SetFocus(Society);
        }



    }
    private void AddInFlowNewRowToGrid()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values

                    TextBox Society = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("txtSociety");
                    TextBox SocietyName = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtSocietyName");
                    HiddenField Office_ID = (HiddenField)gvDetails.Rows[rowIndex].Cells[2].FindControl("hfOffice_ID");

                    TextBox txtAmount = (TextBox)gvDetails.Rows[rowIndex].Cells[4].FindControl("txtAmount");
                    TextBox txtRemark = (TextBox)gvDetails.Rows[rowIndex].Cells[4].FindControl("txtRemark");




                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["Office_Name"] = string.Empty;
                    drCurrentRow["SocietyName"] = string.Empty;
                    drCurrentRow["Office_ID"] = string.Empty;
                    drCurrentRow["Amount"] = string.Empty;
                    drCurrentRow["Remark"] = string.Empty;
                    


                    dtCurrentTable.Rows[i - 1]["Office_Name"] = Society.Text;
                    dtCurrentTable.Rows[i - 1]["SocietyName"] = SocietyName.Text;
                    dtCurrentTable.Rows[i - 1]["Office_ID"] = Office_ID.Value;
                    dtCurrentTable.Rows[i - 1]["Amount"] = txtAmount.Text;
                    dtCurrentTable.Rows[i - 1]["Remark"] = txtRemark.Text;
                  


                    rowIndex++;
                }

                if (drCurrentRow != null)
                {
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    gvDetails.DataSource = dtCurrentTable;
                    gvDetails.DataBind();
                }
            }

        }


        else
        {
            Response.Write("ViewState is null");
            ViewState["CurrentTable"] = null;
        }

        //Set Previous Data on Postbacks


        SetInFlowPreviousData();
    }

    private void SetInFlowPreviousData()
    {
        try
        {
            int rowIndex = 0;
            double Total = 0;

            if (ViewState["CurrentTable"] != null)
            {

                DataTable dt = (DataTable)ViewState["CurrentTable"];

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox Society = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("txtSociety");
                        TextBox SocietyName = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtSocietyName");
                        HiddenField Office_ID = (HiddenField)gvDetails.Rows[rowIndex].Cells[2].FindControl("hfOffice_ID");

                        TextBox txtAmount = (TextBox)gvDetails.Rows[rowIndex].Cells[4].FindControl("txtAmount");
                        TextBox txtRemark = (TextBox)gvDetails.Rows[rowIndex].Cells[4].FindControl("txtRemark");


                        //ddlCat.Text = dt.Rows[i]["Column1"].ToString();
                        Society.Text = dt.Rows[i]["Office_Name"].ToString();
                        SocietyName.Text = dt.Rows[i]["SocietyName"].ToString();
                        Office_ID.Value = dt.Rows[i]["Office_ID"].ToString();
                        txtAmount.Text = dt.Rows[i]["Amount"].ToString();
                        txtRemark.Text = dt.Rows[i]["Remark"].ToString();
                        

                        rowIndex++;
                        SetFocus(Society);
                        

                    }
                }





            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
   
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        Button lb = (Button)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        ViewState["InFlowrowID"] = rowID.ToString();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 1)
            {
                if (gvRow.RowIndex < dt.Rows.Count)
                {
                    //Remove the Selected Row data and reset row number  
                    dt.Rows.Remove(dt.Rows[rowID]);

                }
            }

            //Store the current data in ViewState for future reference  
            ViewState["CurrentTable"] = dt;

            //Re bind the GridView for the updated data  
            gvDetails.DataSource = dt;
            gvDetails.DataBind();
        }

        //Set Previous Data on Postbacks  
        SetInFlowPreviousOnRemove();
    }
    protected void SetInFlowPreviousOnRemove()
    {
        int rowIndex = 0;
        double Total = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox Society = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("txtSociety");
                    TextBox SocietyName = (TextBox)gvDetails.Rows[rowIndex].Cells[2].FindControl("txtSocietyName");
                    HiddenField Office_ID = (HiddenField)gvDetails.Rows[rowIndex].Cells[2].FindControl("hfOffice_ID");

                    TextBox txtAmount = (TextBox)gvDetails.Rows[rowIndex].Cells[4].FindControl("txtAmount");
                    TextBox txtRemark = (TextBox)gvDetails.Rows[rowIndex].Cells[4].FindControl("txtRemark");


                    //ddlCat.Text = dt.Rows[i]["Column1"].ToString();
                    Society.Text = dt.Rows[i]["Office_Name"].ToString();
                    SocietyName.Text = dt.Rows[i]["SocietyName"].ToString();
                    Office_ID.Value = dt.Rows[i]["Office_ID"].ToString();
                    txtAmount.Text = dt.Rows[i]["Amount"].ToString();
                    txtRemark.Text = dt.Rows[i]["Remark"].ToString();
                    rowIndex++;
                    

                }

            }

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {

        
        SetInFlowInitialRow();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    lblMsg.Text = "";
                    string IsActive = "1";
                    string BMCTankerRoot_Id;
                    if(ddlEntryType.SelectedItem.Text == "Route Wise")
                    {
                        BMCTankerRoot_Id = ddlBMCTankerRootName.SelectedValue;
                    }
                    else
                    {
                        BMCTankerRoot_Id = ddlCC.SelectedValue;
                    }
                    DataTable dt = new DataTable();
                    dt = GetDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        ds = objdb.ByProcedure("Usp_MilkCollectionRoutWiseAdditionsDeductionsEntry"
                                               , new string[]
                                       {"flag",
                                        "EntryDate",
                                        "EntryType",
                                        "BMCTankerRoot_Id",
                                        "Office_Parant_ID",
                                        "CreatedAt", 
                                        "CreatedBy", 
                                        "CreatedByIP",                                        
                                        "IsActive" ,
                                        "BillingPeriodFromDate",
                                        "BillingPeriodToDate",
										"OfficeType_ID"
                                       }
                                      , new string[]
                                       {"2",
                                        Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
                                        ddlEntryType.SelectedItem.Text,
                                        BMCTankerRoot_Id,
                                        objdb.Office_ID(),
                                        objdb.Office_ID(),
                                        objdb.createdBy(),
                                        objdb.GetLocalIPAddress(),
                                        IsActive,
                                        Convert.ToDateTime(txtBillingCycleFromDate.Text,cult).ToString("yyyy/MM/dd"),
                                        Convert.ToDateTime(txtBillingCycleToDate.Text,cult).ToString("yyyy/MM/dd"),
                                        objdb.OfficeType_ID()
                                       }
                                               , new string[]
                                       {
                                        "type_Trn_MilkCollectionRoutWiseAdditionsDeductionsEntry"
                                       }
                                               , new DataTable[]         
                                       {
                                        dt
                                       }
                                               , "TableSave");
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                {
                                    divEntry.Visible = false;
                                    Panelfltr.Enabled = true;
                                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                    //Session["IsSuccess"] = true;
                                    //Response.Redirect("MilkCollectionAdditionDeductionEntry_New.aspx", false);

                                }
                                else
                                {
                                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                    Session["IsSuccess"] = false;
                                }
                            }
                        }
                        Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Please Fill At Least one Record");
                    }
                }
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnAdd_Click1(object sender, EventArgs e)
    {
        divEntry.Visible = true;
        FillSociety();
        SetInFlowInitialRow();
        Panelfltr.Enabled = false;
       // GetDataforValidation();
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        
        ViewState["InFlowrowID"] = "";
        //BindGrid();
        AddInFlowNewRowToGrid();
    }
    protected void btnGetTotal_Click(object sender, EventArgs e)
    {
        decimal TotalAmount = 0;
        
        foreach (GridViewRow rows in gvDetails.Rows)
        {

            TextBox txtAmount = (TextBox)rows.FindControl("txtAmount");
           
            if (txtAmount.Text != "")
            {
                TotalAmount += decimal.Parse(txtAmount.Text);

            }
            

        }

        gvDetails.FooterRow.Cells[2].Text = "TOTAL";
        gvDetails.FooterRow.Cells[2].Font.Bold = true;
        gvDetails.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;

        gvDetails.FooterRow.Cells[3].Text = TotalAmount.ToString();
        gvDetails.FooterRow.Cells[3].Font.Bold = true;
        gvDetails.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Left;

       
        btnSave.Enabled = true;
    }
    protected void ddlfltEntryType_SelectedIndexChanged(object sender, EventArgs e)
    {

        Div1.Visible = false;
        Div2.Visible = false;
        if (ddlfltEntryType.SelectedIndex > 0)
        {
            if (ddlfltEntryType.SelectedItem.Text == "Route Wise")
            {
                Div1.Visible = true;
                Div2.Visible = false;
                ddlFltrddlBMCTankerRootName.ClearSelection();
            }
            if (ddlfltEntryType.SelectedItem.Text == "Chilling Center Wise")
            {
                Div2.Visible = true;
                Div1.Visible = false;
                ddlFltrCC.ClearSelection();
            }
        }
    }
    protected void GetDataforValidation()
    {
        lblMsg.Text = "";
        string BMCTankerRoot_Id;
        if (ddlEntryType.SelectedItem.Text == "Route Wise")
        {
            BMCTankerRoot_Id = ddlBMCTankerRootName.SelectedValue;
        }
        else
        {
            BMCTankerRoot_Id = ddlCC.SelectedValue;
        }
        ds = objdb.ByProcedure("Usp_MilkCollectionRoutWiseAdditionsDeductionsEntry", 
                               new string[] { "flag", "EntryType", "BMCTankerRoot_Id", "ItemBillingHead_ID", "BillingPeriodFromDate", "BillingPeriodToDate" },
                               new string[] { "12", ddlEntryType.SelectedItem.Text, BMCTankerRoot_Id, ddlHeaddetails.SelectedValue,txtBillingCycleFromDate.Text, txtBillingCycleToDate.Text}, "dataset");
        if(ds != null)
        {
            if(ds.Tables.Count > 0)
            {
                ds5 = ds;
            }
        }
    }


    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if(txtDate.Text != "")
            {
                string BillingCycle = ddlBillingCycle.SelectedItem.Text;
                string[] DatePart = txtDate.Text.Split('/');
                int Day = int.Parse(DatePart[0]);
                int Month = int.Parse(DatePart[1]);
                int Year = int.Parse(DatePart[2]);
                string SelectedFromDate="";
                string SelectedToDate="";
                if (BillingCycle == "5 days")
                {
                if (Day >= 1 && Day < 6) 
                {
                    SelectedFromDate = "01";
                    SelectedToDate = "05";
                }
                else if (Day > 5 && Day < 11) {
                    SelectedFromDate = "6";
                    SelectedToDate = "10";
                }
                else if (Day > 10 && Day < 16) 
                {
                    SelectedFromDate = "11";
                    SelectedToDate = "15";
                }
                else if (Day > 15 && Day < 21) 
                {
                    SelectedFromDate = "16";
                    SelectedToDate = "20";
                }
                else if (Day > 20 && Day < 26) 
                {
                    SelectedFromDate = "21";
                    SelectedToDate = "25";
                }
                else if (Day > 25 && Day <= 31)
                {
                    SelectedFromDate = "26";
                    if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12) 
                    {
                        SelectedToDate = "31";
                    }
                    else if (Month == 2) 
                    {
                        SelectedToDate = "28";
                    }
                    else 
                    {
                        SelectedToDate = "30";
                    }

                }
                SelectedFromDate = SelectedFromDate + "/" + Month + '/' + Year;
                SelectedToDate = SelectedToDate + "/" + Month + '/' + Year;

            }
            else 
           {
                if (Day >= 1 && Day < 11) 
                {
                    SelectedFromDate = "01";
                    SelectedToDate = "10";
                }
                else if (Day > 10 && Day < 21) 
                {
                    SelectedFromDate = "11";
                    SelectedToDate = "20";
                }
                else if (Day > 20 && Day <= 31) 
                {
                    SelectedFromDate = "21";
                    if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12) 
                    {
                        SelectedToDate = "31";
                    }
                    else if (Month == 2)
                    {
                        SelectedToDate = "28";
                    }
                    else {
                        SelectedToDate = "30";
                    }
                }

                SelectedFromDate = SelectedFromDate + "/" + Month + '/' + Year;
                SelectedToDate = SelectedToDate + "/" + Month + '/' + Year;
            }
                txtBillingCycleFromDate.Text = Convert.ToDateTime(SelectedFromDate, cult).ToString("dd/MM/yyyy");
                txtBillingCycleToDate.Text = Convert.ToDateTime(SelectedToDate, cult).ToString("dd/MM/yyyy");
                GetValidate();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void ddlBillingCycle_TextChanged(object sender, EventArgs e)
    {
        txtDate_TextChanged(sender, e);
    }
	protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            GenerateData();
            CreateDBFFile();
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private DataTable GenerateData()
    {
        DataTable dt = new DataTable();

        DataRow dr = null;

        dt.Columns.Add(new DataColumn("L_UN_CD", typeof(int)));

        dt.Columns.Add(new DataColumn("L_SOC_CD", typeof(int)));

        dt.Columns.Add(new DataColumn("L_LOAN_CD", typeof(int)));

        dt.Columns.Add(new DataColumn("L_AMOUNT", typeof(decimal)));

        dt.Columns.Add(new DataColumn("L_REMARK", typeof(string)));

		dt.Columns.Add(new DataColumn("L_PERIOD", typeof(string)));

        string BMCTankerRoot_Id;
        if (ddlfltEntryType.SelectedItem.Text == "Route Wise")
        {
            BMCTankerRoot_Id = ddlFltrddlBMCTankerRootName.SelectedValue;
        }
        else
        {
            BMCTankerRoot_Id = ddlFltrCC.SelectedValue;
        }
        ds = objdb.ByProcedure("Usp_MilkCollectionRoutWiseAdditionsDeductionsEntry", new string[] { "flag", "FromDate", "ToDate", "EntryType", "BMCTankerRoot_Id" }, new string[] { "19", Convert.ToDateTime(txtFilterFromDate.Text, cult).ToString("yyyy/MM/dd"),Convert.ToDateTime(txtFilterToDate.Text, cult).ToString("yyyy/MM/dd"), ddlfltEntryType.SelectedItem.Text, BMCTankerRoot_Id }, "dataset");
        if (ds != null && ds.Tables.Count > 0)
        {
            int Count = ds.Tables[0].Rows.Count;
            for (int i = 0; i < Count; i++)
            {
                dr = dt.NewRow();

                dr["L_UN_CD"] = ds.Tables[0].Rows[i]["L_UN_CD"].ToString();

                dr["L_SOC_CD"] = ds.Tables[0].Rows[i]["L_SOC_CD"].ToString();

                dr["L_LOAN_CD"] = ds.Tables[0].Rows[i]["L_LOAN_CD"].ToString();

                dr["L_AMOUNT"] = ds.Tables[0].Rows[i]["L_AMOUNT"].ToString();

                dr["L_REMARK"] = ds.Tables[0].Rows[i]["L_REMARK"].ToString();

               dr["L_PERIOD"] = ds.Tables[0].Rows[i]["L_PERIOD"].ToString();
			   
			   ViewState["BillingCycle"] = ds.Tables[0].Rows[i]["BillingCycle"].ToString();

                dt.Rows.Add(dr);
            }
        }



        return dt;
    }
    private void CreateDBFFile()
    {
        string filepath = null;

        filepath = Server.MapPath("~//Download//");

        string TableName = "T" + DateTime.Now.ToLongTimeString().Replace(":", "").Replace("AM", "").Replace("PM", "");

        using (dBaseConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; " + " Data Source=" + filepath + "; " + "Extended Properties=dBase IV"))
        {
            dBaseConnection.Open();

            OleDbCommand olecommand = dBaseConnection.CreateCommand();

            if ((System.IO.File.Exists(filepath + "" + TableName + ".dbf")))
            {
                System.IO.File.Delete(filepath + "" + TableName + ".dbf");
                olecommand.CommandText = "CREATE TABLE [" + TableName + "] ([L_UN_CD]  varchar(10), [L_SOC_CD]  varchar(10),  [L_LOAN_CD] varchar(10),[L_AMOUNT] decimal(18,2),[L_REMARK] varchar(100),[L_PERIOD] varchar(100))";
                olecommand.ExecuteNonQuery();
            }
            else
            {
                olecommand.CommandText = "CREATE TABLE [" + TableName + "] ([L_UN_CD]  varchar(10), [L_SOC_CD]  varchar(10),  [L_LOAN_CD] varchar(10),[L_AMOUNT] decimal(18,2),[L_REMARK] varchar(100),[L_PERIOD] varchar(100))";
                olecommand.ExecuteNonQuery();
            }

            OleDbDataAdapter oleadapter = new OleDbDataAdapter(olecommand);
            OleDbCommand oleinsertCommand = dBaseConnection.CreateCommand();

            foreach (DataRow dr in GenerateData().Rows)
            {
                int Column1 = int.Parse(dr["L_UN_CD"].ToString());
                int Column2 = int.Parse(dr["L_SOC_CD"].ToString());
                int Column3 = int.Parse(dr["L_LOAN_CD"].ToString());
                decimal Column4 = decimal.Parse(dr["L_AMOUNT"].ToString());
                string Column5 = dr["L_REMARK"].ToString();
                string Column6 = dr["L_PERIOD"].ToString();


                oleinsertCommand.CommandText = "INSERT INTO [" + TableName + "] ([L_UN_CD], [L_SOC_CD],[L_LOAN_CD],[L_AMOUNT],[L_REMARK],[L_PERIOD]) VALUES ('" + Column1.ToString() + "','" + Column2.ToString() + "','" + Column3.ToString() + "','" + Column4.ToString() + "','" + Column5  + "','" + Column6 + "')";

                oleinsertCommand.ExecuteNonQuery();
            }
        }
        String sDate = Convert.ToDateTime(txtFilterFromDate.Text, cult).ToString("MM/dd/yyyy HH:mm");
        DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));
        string dy = datevalue.Day.ToString();
        string mn = datevalue.Month.ToString();
		if (mn.Length > 1)
        {
           
        }
        else
        {
            mn = "0" + mn;
        }
        string yy = datevalue.ToString("yy");
		ViewState["BillingCycle"] = "0" + ViewState["BillingCycle"].ToString();
        FileStream sourceFile = new FileStream(filepath + "" + TableName + ".dbf", FileMode.Open);
        float FileSize = 0;
        FileSize = sourceFile.Length;
        byte[] getContent = new byte[Convert.ToInt32(Math.Truncate(FileSize))];
        sourceFile.Read(getContent, 0, Convert.ToInt32(sourceFile.Length));
        sourceFile.Close();
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Buffer = true;
        Response.ContentType = "application/dbf";
        Response.AddHeader("Content-Length", getContent.Length.ToString());
         Response.AddHeader("content-disposition", "attachment;filename=" + "AD" + ViewState["BillingCycle"] + mn + yy + ".dbf");
       // Response.AddHeader("content-disposition", "attachment;filename=" + "AD" + dy + mn + yy + ".dbf");
        Response.BinaryWrite(getContent);
        Response.Flush();
        System.IO.File.Delete(filepath + "" + TableName + ".dbf");
        Response.End();
    }
    protected void GetValidate()
    {
        try
        {
            btnAdd.Enabled = true;
            ds = objdb.ByProcedure("Usp_MilkCollectionRoutWiseAdditionsDeductionsEntry", 
                 new string[] { "flag", "BillingPeriodFromDate", "BillingPeriodToDate", "CC_Id" },
                 new string[] { "25", Convert.ToDateTime(txtBillingCycleFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtBillingCycleToDate.Text, cult).ToString("yyyy/MM/dd"),ddlCC.SelectedValue }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    if(ds.Tables[0].Rows[0]["Status"].ToString() == "0")
                    {
                        btnAdd.Enabled = true;
                    }
                    else
                    {
                        btnAdd.Enabled = false ;
                    }
                    
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    public System.Data.OleDb.OleDbConnection dBaseConnection { get; set; }
}