using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web;

public partial class mis_MilkCollection_MilkCollectionAdditionDeductionEntry : System.Web.UI.Page
{
    DataSet ds;
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
                txtFilterDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFilterDate.Attributes.Add("readonly", "readonly");
                FillBMCRoot();
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
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
           
            divEntry.Visible = false;
            ds = objdb.ByProcedure("Usp_MilkCollectionRoutWiseAdditionsDeductionsEntry", new string[] { "flag", "BMCTankerRoot_Id", "Office_ID", "OfficeType_ID", "ItemBillingHead_ID", "EntryDate" }, new string[] { "1", ddlBMCTankerRootName.SelectedValue, objdb.Office_ID(),objdb.OfficeType_ID(), ddlHeaddetails.SelectedValue, Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd")}, "dataset");
            if(ds !=  null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
                {
                    divEntry.Visible = true;
                    gvDetails.DataSource = ds;
                    gvDetails.DataBind();
                }
                else
                {
                    gvDetails.DataSource = string.Empty;
                    gvDetails.DataBind();
                }
            }
            else
            {
                gvDetails.DataSource = string.Empty;
                gvDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
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
               new string[] { "flag", "ItemBillingHead_Type" },
               new string[] { "8", ddlHeadType.SelectedValue }, "dataset");

                ddlHeaddetails.DataTextField = "ItemBillingHead_Name";
                ddlHeaddetails.DataValueField = "ItemBillingHead_ID";
                ddlHeaddetails.DataSource = ds;
                ddlHeaddetails.DataBind();
                ddlHeaddetails.Items.Insert(0, new ListItem("Select", "0"));

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
    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
        int Index = row.RowIndex;
        RequiredFieldValidator rfvHeadAmount = (RequiredFieldValidator)gvDetails.Rows[Index].FindControl("rfvHeadAmount");
        RequiredFieldValidator rfvQuantity = (RequiredFieldValidator)gvDetails.Rows[Index].FindControl("rfvQuantity");
        CheckBox chkSelect = (CheckBox)gvDetails.Rows[Index].FindControl("chkSelect");
        if(chkSelect.Checked)
        {
            rfvHeadAmount.Enabled = true;
            rfvQuantity.Enabled = true;
        }
        else
        {
            rfvHeadAmount.Enabled = false;
            rfvQuantity.Enabled = false;
        }
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
                                        "IsActive"                                                                          
                                       }
                                      , new string[]
                                       {"2",
                                        Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd"),
										"Route Wise",
                                        ddlBMCTankerRootName.SelectedValue,
                                        objdb.Office_ID(),
                                        objdb.Office_ID(),
                                        objdb.createdBy(),
                                        objdb.GetLocalIPAddress(),
                                        IsActive
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

                                    Session["IsSuccess"] = true;
                                    Response.Redirect("MilkCollectionAdditionDeductionEntry.aspx", false);

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
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Please Select At Least One CheckBox Row");
                    }
                }
            }
            

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    private DataTable GetDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Office_ID", typeof(int));
        dt.Columns.Add("ItemBillingHead_ID", typeof(int));
        dt.Columns.Add("HeadAmount", typeof(decimal));
        dt.Columns.Add("Quantity", typeof(decimal));
        dt.Columns.Add("HeadRemark", typeof(string));
        foreach(GridViewRow row in gvDetails.Rows)
        {
            Label lblItemOffice_ID = (Label)row.FindControl("lblItemOffice_ID");
            Label lblItemBillingHead_ID = (Label)row.FindControl("lblItemBillingHead_ID");
            TextBox txtHeadAmount = (TextBox)row.FindControl("txtHeadAmount");
            TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
            TextBox txtRemark = (TextBox)row.FindControl("txtRemark");
            CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
            
            if(chkSelect.Checked)
            {

                dt.Rows.Add(lblItemOffice_ID.Text, lblItemBillingHead_ID.Text, txtHeadAmount.Text, txtQuantity.Text, txtRemark.Text);

            }
            else
            {
                
            }
        }

        return dt;
    }
    protected void ddlFltHeadType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblRptMsg.Text = "";
            if (ddlFltHeadType.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("USP_Mst_ItemBillingHeadMaster",
               new string[] { "flag", "ItemBillingHead_Type" },
               new string[] { "8", ddlFltHeadType.SelectedValue }, "dataset");

                ddlFltHeaddetails.DataTextField = "ItemBillingHead_Name";
                ddlFltHeaddetails.DataValueField = "ItemBillingHead_ID";
                ddlFltHeaddetails.DataSource = ds;
                ddlFltHeaddetails.DataBind();
                ddlFltHeaddetails.Items.Insert(0, new ListItem("All", "0"));

            }
            else
            {
                ddlHeadType.ClearSelection();
                ddlHeaddetails.Items.Clear();
                //ddlHeaddetails.Items.Add(new ListItem("Select", "0"));
            }



        }
        catch (Exception ex)
        {
            lblRptMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnFltSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ds = objdb.ByProcedure("Usp_MilkCollectionRoutWiseAdditionsDeductionsEntry", new string[] { "flag", "EntryDate", "ItemBillingHead_ID", "CreatedAt","ItemBillingHead_Type" }, new string[] { "3", Convert.ToDateTime(txtFilterDate.Text, cult).ToString("yyyy/MM/dd"), ddlFltHeaddetails.SelectedValue, objdb.Office_ID(),ddlFltHeadType.SelectedValue }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
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
            Label lblQuantity = (Label)row.FindControl("lblQuantity");
            Label lblRemark = (Label)row.FindControl("lblRemark");
            TextBox txtHeadAmount = (TextBox)row.FindControl("txtHeadAmount");
            TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
            TextBox txtHeadRemark = (TextBox)row.FindControl("txtHeadRemark");

            if (e.CommandName == "EditRecord")
            {
                lnkUpdate.Visible = true;
                lnkEdit.Visible = false;
                txtHeadAmount.Visible = true;
                lblHeadAmount.Visible = false;
                txtQuantity.Visible = true;
                lblQuantity.Visible = false;
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
                                   ,"Quantity"
                                   ,"HeadRemark"
                                   ,"CreatedAt"
                                   ,"CreatedBy"
                                   ,"CreatedByIP"
                                   },
                                   new string[] 
                                   {"4"
                                  ,AddtionsDeducEntry_ID
                                  ,txtHeadAmount.Text
                                  ,txtQuantity.Text
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
			if(e.CommandName == "DeleteRecord")
            {
                objdb.ByProcedure("Usp_MilkCollectionRoutWiseAdditionsDeductionsEntry", new string[] { "flag", "AddtionsDeducEntry_ID" }, new string[] { "7", AddtionsDeducEntry_ID }, "dataset");
                lblRptMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Deleted Successfully");
                btnFltSearch_Click(sender, e);
            }
        }
        catch (Exception ex)
        {
            lblRptMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtHeadAmount = (TextBox)e.Row.FindControl("txtHeadAmount");

            if (txtHeadAmount.Text != "")
            {
                e.Row.Enabled = false;
            }
        }
    }
    protected void checkAll_CheckedChanged(object sender, EventArgs e)
    {
       foreach(GridViewRow rows in gvDetails.Rows)
       {
           RequiredFieldValidator rfvHeadAmount = (RequiredFieldValidator)rows.FindControl("rfvHeadAmount");
           RequiredFieldValidator rfvQuantity = (RequiredFieldValidator)rows.FindControl("rfvQuantity");
           CheckBox chkSelect = (CheckBox)rows.FindControl("chkSelect");
           if (chkSelect.Checked)
           {
               rfvHeadAmount.Enabled = true;
               rfvQuantity.Enabled = true;
           }
           else
           {
               rfvHeadAmount.Enabled = false;
               rfvQuantity.Enabled = false;
           }
       }
        
    }
}