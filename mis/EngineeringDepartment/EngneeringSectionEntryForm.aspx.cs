using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Globalization;

public partial class mis_EngneeringDepartment_EngneeringSectionEntryForm : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtEntryDate.Text = Date;
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                FillHead();
                ds = objdb.ByProcedure("USP_Mst_ENGSectionEntry",
                         new string[] { "flag", "Office_ID", "CreatedBy" },
                        new string[] { "1", objdb.Office_ID(), objdb.createdBy() }, "dataset");
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                    }
                }
                //FillSearchRouteHead();
                // FillSubHead();
                GetSectionEntryDetails();
                BindFY();
            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    private void BindFY()
    {
        ds = objdb.ByProcedure("Sp_FY", new string[] { "Flag" }, new string[] { "1" }, "dataset");

        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlYear.Items.Clear();
                    ddlYear.DataSource = ds.Tables[0];
                    ddlYear.DataTextField = "FY1";
                    ddlYear.DataValueField = "FY1";
                    ddlYear.DataBind();
                    ddlYear.Items.Insert(0, new ListItem("Select", "0"));


                }
            }
        }
        if (ds != null) { ds.Dispose(); }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    protected void FillHead()
    {
        try
        {
            ddlHeadName.DataTextField = "ENGHeadName";
            ddlHeadName.DataValueField = "ENGHeadId";
            ddlHeadName.DataSource = objdb.ByProcedure("USP_Mst_ENGHead", new string[] { "Flag", "Office_ID", "CreatedBy" },
                        new string[] { "5", objdb.Office_ID(), objdb.createdBy() }, "dataset");
            ddlHeadName.DataBind();
            ddlHeadName.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillSubHead()
    {
        try
        {
            ddlSubHeadName.DataSource = null;
            if (ddlMonth.SelectedIndex > 0)
            {
                if (ddlHeadName.SelectedIndex > 0)
                {
                    ddlSubHeadName.DataTextField = "ENGSubHeadName";
                    ddlSubHeadName.DataValueField = "ENGSubHeadId";
                    ddlSubHeadName.DataSource = objdb.ByProcedure("usp_Mst_ENGSectionEntry",
                        new string[] { "Flag", "Office_ID", "ENGHeadId", "EntryMonth", "CreatedBy","EntryYear" },
                                   new string[] { "8", objdb.Office_ID(), ddlHeadName.SelectedValue, ddlMonth.SelectedValue, objdb.createdBy(),ddlYear.SelectedValue }, "dataset");
                    ddlSubHeadName.DataBind();
                    ddlSubHeadName.Items.Insert(0, new ListItem("Select", "0"));
                }
                //else
                //{
                //    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "warning!", "Please Select Month");
                //}
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "warning!", "Please Select Month");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillSubHead(string ENGSubHeadId)
    {
        try
        {
            ddlSubHeadName.DataSource = null;
            if (ddlMonth.SelectedIndex > 0)
            {
                if (ddlHeadName.SelectedIndex > 0)
                {
                    ddlSubHeadName.DataTextField = "ENGSubHeadName";
                    ddlSubHeadName.DataValueField = "ENGSubHeadId";
                    ddlSubHeadName.DataSource = objdb.ByProcedure("usp_Mst_ENGSectionEntry",
                        new string[] { "Flag", "Office_ID", "ENGHeadId", "EntryMonth", "ENGSubHeadId", "CreatedBy" },
                                   new string[] { "9", objdb.Office_ID(), ddlHeadName.SelectedValue, ddlMonth.SelectedValue, ENGSubHeadId, objdb.createdBy() }, "dataset");
                    ddlSubHeadName.DataBind();
                    ddlSubHeadName.Items.Insert(0, new ListItem("Select", "0"));
                }
                //else
                //{
                //    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "warning!", "Please Select Month");
                //}
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "warning!", "Please Select Month");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblEntryDate = (Label)row.FindControl("lblEntryDate");
                    Label lblEntryMonth = (Label)row.FindControl("lblEntryMonth");
                    Label lblHeadID = (Label)row.FindControl("lblHeadID");
                    Label lblSubHeadID = (Label)row.FindControl("lblSubHeadID");
                    Label lblHeadName = (Label)row.FindControl("lblHeadName");
                    Label lblSubHeadName = (Label)row.FindControl("lblSubHeadName");
                    Label lblNumber = (Label)row.FindControl("lblNumber");
                    Label lblQTY = (Label)row.FindControl("lblQTY");
                    Label lblAmount = (Label)row.FindControl("lblAmount");
                    Label lblRemark = (Label)row.FindControl("lblRemark");
                    Label lblIsActive = (Label)row.FindControl("lblIsActive");
                    Label lblEntryYear = (Label)row.FindControl("lblEntryYear");




                    txtEntryDate.Text = lblEntryDate.Text;
                    ddlMonth.SelectedValue = lblEntryMonth.Text;
                    ddlMonth.SelectedItem.Text = lblEntryMonth.Text;
                    FillHead();
                    ddlHeadName.SelectedValue = lblHeadID.Text;
                    ddlHeadName.SelectedItem.Text = lblHeadName.Text;
                    FillSubHead(lblSubHeadID.Text);
                    ddlSubHeadName.SelectedValue = lblSubHeadID.Text;
                    ddlSubHeadName.SelectedItem.Text = lblSubHeadName.Text;
                    txtNumber.Text = lblNumber.Text;
                    txtQty.Text = lblQTY.Text;
                    txtAmount.Text = lblAmount.Text;
                    txtRemark.Text = lblRemark.Text;
                    ddlYear.SelectedValue = lblEntryYear.Text;
                    //lblIsActive.Text = lblIsActive.Text;


                    ViewState["rowid"] = e.CommandArgument;
                    btnSubmit.Text = "UPDATE";

                    foreach (GridViewRow gvRow in GridView1.Rows)
                    {
                        if (GridView1.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            GridView1.SelectedIndex = gvRow.DataItemIndex;
                            GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                    GetDatatableHeaderDesign();
                }

            }
            if (e.CommandName == "RecordDelete")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    Control ctrl = e.CommandSource as Control;
                    if (ctrl != null)
                    {
                        GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                        //Label lblHeadName = (Label)row.FindControl("lblHeadName");
                        LinkButton lnkDelete = (LinkButton)row.FindControl("lnkDelete");
                        lblMsg.Text = string.Empty;
                        ViewState["rowid"] = e.CommandArgument;
                        string Status = lnkDelete.Text;

                        ds = objdb.ByProcedure("usp_Mst_ENGSectionEntry",
                                    new string[] { "flag", "ENGSectionEntryId", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                                    new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy()
                                    , objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
                                    , Path.GetFileName(Request.Url.AbsolutePath)
                                    , "Entry Section Details Deactivate" }, "TableSave");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            GetSectionEntryDetails();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }


                        ds.Clear();
                        Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    }
                }


            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetSectionEntryDetails()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Mst_ENGSectionEntry",
                         new string[] { "flag", "Office_ID", "CreatedBy" },
                        new string[] { "1", objdb.Office_ID(), objdb.createdBy() }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GetDatatableHeaderDesign();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    // GetDatatableHeaderDesign();
                }
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                // GetDatatableHeaderDesign();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 4:", ex.Message.ToString());
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            InsertorUpdateSectionEntry();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private void InsertorUpdateSectionEntry()
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    if (btnSubmit.Text == "SUBMIT")
                    {
                        DateTime date1 = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", culture);
                        string EntryDate = date1.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        string Office_ID = objdb.Office_ID();
                        string createdBy = objdb.createdBy();
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("USP_Mst_ENGSectionEntry",
                            new string[] { "flag", "ENGHeadId", "ENGHeadName", "ENGSubHeadId", "ENGSubHeadName", "EntryDate", "EntryMonth", "EntryYear", "Number", "Qty", "Amount", "Remark", "Office_ID", "CreatedBy", "CreatedByIP" },
                            new string[] { "0", ddlHeadName.SelectedValue,ddlHeadName.SelectedItem.Text,ddlSubHeadName.SelectedValue,ddlSubHeadName.SelectedItem.Text,EntryDate,ddlMonth.SelectedValue,ddlYear.SelectedValue,txtNumber.Text,txtQty.Text,txtAmount.Text,txtRemark.Text, objdb.Office_ID(), objdb.createdBy(),
                                            IPAddress + ":" + objdb.GetMACAddress() }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            GetSectionEntryDetails();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already")
                            {
                                // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Engineering Head " + txtHeadName.Text + " " + error + " Exist.");
                                GetDatatableHeaderDesign();
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                        ds.Clear();
                    }
                    if (btnSubmit.Text == "UPDATE")
                    {
                        DateTime date1 = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", culture);
                        string EntryDate = date1.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("USP_Mst_ENGSectionEntry",
                            new string[] { "flag", "ENGSectionEntryId", "ENGHeadId", "ENGHeadName", "ENGSubHeadId", "ENGSubHeadName", "EntryDate", "EntryMonth", "EntryYear", "Number", "Qty", "Amount", "Remark", "Office_ID", "CreatedBy", "CreatedByIP" },
                            new string[] { "5", ViewState["rowid"].ToString(),ddlHeadName.SelectedValue,ddlHeadName.SelectedItem.Text,ddlSubHeadName.SelectedValue,ddlSubHeadName.SelectedItem.Text,EntryDate,ddlMonth.SelectedValue,ddlYear.SelectedValue,txtNumber.Text,txtQty.Text,txtAmount.Text,txtRemark.Text, objdb.Office_ID(), objdb.createdBy(),
                                            IPAddress + ":" + objdb.GetMACAddress() }, "dataset");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            Clear();
                            GetSectionEntryDetails();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Already")
                            {
                                // lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Engineering Head Name " + txtHeadName.Text + " " + error + " Exist.");
                                GetDatatableHeaderDesign();
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                        ds.Clear();
                    }

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Enter Head Name");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
        }
    }
    private void Clear()
    {
        try
        {
            txtEntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ddlHeadName.SelectedIndex = 0;

            ddlSubHeadName.SelectedIndex = 0;
            ddlMonth.SelectedIndex = 0;
            txtNumber.Text = "";
            txtQty.Text = "";
            txtRemark.Text = "";
            txtAmount.Text = "";


            btnSubmit.Text = "SUBMIT";
            GridView1.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlHeadName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            FillSubHead();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    public IFormatProvider culture { get; set; }
    protected void ddlMonth_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            FillSubHead();

            ds = objdb.ByProcedure("USP_Mst_ENGSectionEntry",
                         new string[] { "flag", "Office_ID", "CreatedBy", "EntryMonth" },
                        new string[] { "10", objdb.Office_ID(), objdb.createdBy(),ddlMonth.SelectedValue }, "dataset");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtEntryDate.Text = ds.Tables[0].Rows[0]["EntryDate"].ToString();
                    txtEntryDate.Enabled = false;

                }
                else
                {
                    string Date = DateTime.Now.ToString("dd/MM/yyyy");
                    txtEntryDate.Text = Date;
                    txtEntryDate.Enabled = true;
                }
            }
            else
            {
                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtEntryDate.Text = Date;
                txtEntryDate.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}