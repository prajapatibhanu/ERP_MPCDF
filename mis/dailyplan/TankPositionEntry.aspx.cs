using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Globalization;

public partial class mis_dailyplan_TankPositionEntry : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    MilkCalculationClass Obj_MC = new MilkCalculationClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null)
        {
            if (!IsPostBack)
            {
                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }
                lblMsg.Text = "";
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                txtEntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFilterDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFilterDate.Attributes.Add("readonly", "readonly");
                FillGrid();
                FillDetailGrid();
            }
        }
        else
        {
            Response.Redirect("~/mis/Login.aspx", false);
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
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
    #region User Defined Function
    protected void FillGrid()
    {
        try
        {
            lblMsg.Text = "";
            ds = objdb.ByProcedure("Usp_Production_TankPosition", new string[] { "flag", "Office_Id" }, new string[] { "2", objdb.Office_ID() }, "dataset");
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvTankPosition.DataSource = ds;
                        gvTankPosition.DataBind();

                    }
                    else
                    {
                        gvTankPosition.DataSource = string.Empty;
                        gvTankPosition.DataBind();
                    }
                }
                else
                {
                    gvTankPosition.DataSource = string.Empty;
                    gvTankPosition.DataBind();
                }
            }
            else
            {
                gvTankPosition.DataSource = string.Empty;
                gvTankPosition.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5:" + ex.Message.ToString());
        }
    }
    private DataTable GetTankPositionvalue()
    {

        DataTable dt = new DataTable();
        DataRow dr;


        dt.Columns.Add(new DataColumn("I_MCID", typeof(int)));
        dt.Columns.Add(new DataColumn("ItemType_Id", typeof(int)));
        dt.Columns.Add(new DataColumn("TankPosition", typeof(string)));
        dt.Columns.Add(new DataColumn("OT", typeof(string)));
        dt.Columns.Add(new DataColumn("TEMP", typeof(string)));
        dt.Columns.Add(new DataColumn("FAT", typeof(decimal)));
        dt.Columns.Add(new DataColumn("CLR", typeof(decimal)));
        dt.Columns.Add(new DataColumn("SNF", typeof(decimal)));
        dt.Columns.Add(new DataColumn("Acidity", typeof(string)));
        dt.Columns.Add(new DataColumn("COB", typeof(string)));
        dt.Columns.Add(new DataColumn("Remark", typeof(string)));

        foreach (GridViewRow rowMilkColl in gvTankPosition.Rows)
        {

            Label lblI_MCID = (Label)rowMilkColl.FindControl("lblI_MCID");
            DropDownList ddlOption = (DropDownList)rowMilkColl.FindControl("ddlOption");
            DropDownList ddlVariant = (DropDownList)rowMilkColl.FindControl("ddlVariant");
            DropDownList ddlTankPosition = (DropDownList)rowMilkColl.FindControl("ddlTankPosition");
            TextBox txtTEMP = (TextBox)rowMilkColl.FindControl("txtTEMP");
            TextBox txtFAT = (TextBox)rowMilkColl.FindControl("txtFAT");
            TextBox txtCLR = (TextBox)rowMilkColl.FindControl("txtCLR");
            TextBox txtSNF = (TextBox)rowMilkColl.FindControl("txtSNF");
            TextBox txtACIDITY = (TextBox)rowMilkColl.FindControl("txtACIDITY");
            DropDownList ddlCOB = (DropDownList)rowMilkColl.FindControl("ddlCOB");
            TextBox txtREMARK = (TextBox)rowMilkColl.FindControl("txtREMARK");
            CheckBox chkselect = (CheckBox)rowMilkColl.FindControl("chkselect");
            if (chkselect.Checked == true)
            {
                dr = dt.NewRow();
                
                dr[0] = lblI_MCID.Text;
                
                if (ddlVariant.SelectedValue.ToString() == "0")
                {
                    dr[1] = DBNull.Value;
                }
                else
                {
                    dr[1] = ddlVariant.SelectedValue;
                }
                if (ddlTankPosition.SelectedItem.Text == "Select")
                {
                    dr[2] = DBNull.Value;
                }
                else
                {
                    dr[2] = ddlTankPosition.SelectedItem.Text;
                }
                if (ddlOption.SelectedItem.Text == "Select")
                {
                    dr[3] = DBNull.Value;
                }
                else
                {
                    dr[3] = ddlOption.SelectedItem.Text;
                }
                if (txtTEMP.Text == "")
                {
                    dr[4] = DBNull.Value;
                }
                else
                {
                    dr[4] = txtTEMP.Text;
                }

                if (txtFAT.Text == "")
                {
                    dr[5] = DBNull.Value;
                }
                else
                {
                    dr[5] = txtFAT.Text;
                }
                if (txtCLR.Text == "")
                {
                    dr[6] = DBNull.Value;
                }
                else
                {
                    dr[6] = txtCLR.Text;
                }
                if (txtSNF.Text == "")
                {
                    dr[7] = DBNull.Value;
                }
                else
                {
                    dr[7] = txtSNF.Text;
                }
                if (txtACIDITY.Text == "")
                {
                    dr[8] = DBNull.Value;
                }
                else
                {
                    dr[8] = txtACIDITY.Text;
                }
                if (ddlCOB.SelectedItem.Text == "Select")
                {
                    dr[9] = DBNull.Value;
                }
                else
                {
                    dr[9] = ddlCOB.SelectedItem.Text;
                }
                if (txtREMARK.Text == "")
                {
                    dr[10] = DBNull.Value;
                }
                else
                {
                    dr[10] = txtREMARK.Text;
                }
               
                dt.Rows.Add(dr);
            }

        }
        return dt;

    }
    private decimal GetSNF(string FAT, string CLR)
    {
        decimal snf = 0, clr = 0, fat = 1;
        try
        {
            //Formula for Get SNF as given below:
            if (CLR != "")
            { clr = Convert.ToDecimal(CLR); }
            if (FAT != "")
            { fat = Convert.ToDecimal(FAT); }

            //SNF = (CLR / 4) + (FAT * 0.2) + 0.7 -- Formula Before
            //SNF = (CLR / 4) + (0.25 * FAT) + 0.44 -- Formula After Mail dt: 27 Jan 2020, 16:26
            //snf = ((clr / 4) + (fat * Convert.ToDecimal(0.25)) + Convert.ToDecimal(0.44));
            snf = Obj_MC.GetSNFPer_DCS(fat, clr);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
        }
        return Math.Round(snf, 2);
    }
    protected void FillDetailGrid()
    {
        try
        {
            lblMsg.Text = "";
            btnExcel.Visible = false;
            btnShowntoplant.Visible = false;
            ds = objdb.ByProcedure("Usp_Production_TankPosition", new string[] { "flag", "Office_ID", "EntryDate" }, new string[] { "3", objdb.Office_ID(), Convert.ToDateTime(txtFilterDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnExcel.Visible = true;
                    btnShowntoplant.Visible = true;
                    gvDetail.DataSource = ds;
                    gvDetail.DataBind();
                }
                else
                {
                    gvDetail.DataSource = string.Empty;
                    gvDetail.DataBind();
                }
            }
            else
            {
                gvDetail.DataSource = string.Empty;
                gvDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }

   
    #endregion

    #region Button Click Event
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Page.Validate("save");
            if (Page.IsValid)
            {
                
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    lblMsg.Text = "";
                    DataTable dt = new DataTable();
                    dt = GetTankPositionvalue();
                    if (dt.Rows.Count > 0)
                    {
                        ds = objdb.ByProcedure("Usp_Production_TankPosition", new string[]
                                         {"flag"
                                          ,"EntryDate"
                                          ,"EntryTIme"
                                          ,"Office_ID"
                                          ,"CreatedBy"
                                          ,"CreatedBy_IP"
                                         }
                                             , new string[]
                                         {"1"
                                          ,Convert.ToDateTime(txtEntryDate.Text,cult).ToString("yyyy/MM/dd")
                                          ,txtEntryTime.Text
                                          ,objdb.Office_ID()
                                          ,objdb.createdBy()
                                          ,objdb.GetLocalIPAddress()

                                         }
                                             , new string[] { 
                                              "type_Production_TankPosition"                                           
                                          },
                                          new DataTable[] {
                                              dt 
                                          },
                                          "TableSave");
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                Session["IsSuccess"] = true;
                                Response.Redirect("TankPositionEntry.aspx", false);
                                //string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", Success.ToString());
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                Session["IsSuccess"] = false;
                            }

                            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        }
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 5:" + ex.Message.ToString());
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            gvDetail.Columns[13].Visible = false;
            string FileName = "TankPositionReport";
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gvDetail.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();
            gvDetail.Columns[14].Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("TankPositionEntry.aspx", false);
    }
    #endregion
    #region Gridview Row Event
    protected void chkselect_CheckedChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        CheckBox chkbox = sender as CheckBox;
        GridViewRow currentRow = chkbox.NamingContainer as GridViewRow;
        RequiredFieldValidator rfvddlOption = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("rfvddlOption") as RequiredFieldValidator;

        RequiredFieldValidator rfvddlVariant = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("rfvddlVariant") as RequiredFieldValidator;
        RequiredFieldValidator rfvtxtTEMP = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("rfvtxtTEMP") as RequiredFieldValidator;
        RequiredFieldValidator rfvtxtFAT = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("rfvtxtFAT") as RequiredFieldValidator;
        RequiredFieldValidator rfvtxtCLR = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("rfvtxtCLR") as RequiredFieldValidator;
        RequiredFieldValidator rfvtxtSNF = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("rfvtxtSNF") as RequiredFieldValidator;
        RequiredFieldValidator rfvddlCOB = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("rfvddlCOB") as RequiredFieldValidator;
        RequiredFieldValidator rfvtxtACIDITY = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("rfvtxtACIDITY") as RequiredFieldValidator;
        RequiredFieldValidator rfvtxtRemark = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("rfvtxtRemark") as RequiredFieldValidator;
        RequiredFieldValidator rfvddlTankPosition = gvTankPosition.Rows[currentRow.RowIndex]
                                          .FindControl("rfvddlTankPosition") as RequiredFieldValidator;

        CheckBox chkselect = gvTankPosition.Rows[currentRow.RowIndex]
                                         .FindControl("chkselect") as CheckBox;
        DropDownList ddlOption = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("ddlOption") as DropDownList;

        DropDownList ddlVariant = gvTankPosition.Rows[currentRow.RowIndex]
                                          .FindControl("ddlVariant") as DropDownList;

        TextBox txtTEMP = gvTankPosition.Rows[currentRow.RowIndex]
                                          .FindControl("txtTEMP") as TextBox;
        TextBox txtFAT = gvTankPosition.Rows[currentRow.RowIndex]
                                          .FindControl("txtFAT") as TextBox;
        TextBox txtCLR = gvTankPosition.Rows[currentRow.RowIndex]
                                          .FindControl("txtCLR") as TextBox;

        TextBox txtSNF = gvTankPosition.Rows[currentRow.RowIndex]
                                          .FindControl("txtSNF") as TextBox;

        TextBox txtACIDITY = gvTankPosition.Rows[currentRow.RowIndex]
                                          .FindControl("txtACIDITY") as TextBox;

        TextBox txtRemark = gvTankPosition.Rows[currentRow.RowIndex]
                                          .FindControl("txtRemark") as TextBox;

        DropDownList ddlCOB = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("ddlCOB") as DropDownList;

        DropDownList ddlTankPosition = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("ddlTankPosition") as DropDownList;

        if (ddlTankPosition.SelectedValue == "Empty")
        {
            rfvddlOption.Enabled = false;
            rfvtxtTEMP.Enabled = false;
            rfvtxtFAT.Enabled = false;
            rfvtxtCLR.Enabled = false;
            rfvtxtSNF.Enabled = false;
            rfvddlCOB.Enabled = false;
            rfvtxtACIDITY.Enabled = false;
            rfvtxtRemark.Enabled = false;
            rfvddlVariant.Enabled = false;


            ddlOption.Enabled = false;
            txtTEMP.Enabled = false;
            txtFAT.Enabled = false;
            txtCLR.Enabled = false;
            txtSNF.Enabled = false;
            txtACIDITY.Enabled = false;
            txtRemark.Enabled = false;
            ddlCOB.Enabled = false;
            ddlVariant.Enabled = false;

        }
        else if (ddlTankPosition.SelectedValue == "Under Processing" || ddlTankPosition.SelectedValue == "Under Agitation")
        {

            ddlOption.Enabled = true;
            txtTEMP.Enabled = true;
            txtFAT.Enabled = true;
            txtCLR.Enabled = true;
            txtSNF.Enabled = true;
            txtACIDITY.Enabled = true;
            txtRemark.Enabled = true;
            ddlCOB.Enabled = true;
            ddlVariant.Enabled = true;

            if (chkselect.Checked)
            {
                rfvddlOption.Enabled = true;
                rfvtxtTEMP.Enabled = true;
                rfvtxtFAT.Enabled = false;
                rfvtxtCLR.Enabled = false;
                rfvtxtSNF.Enabled = false;
                rfvddlCOB.Enabled = true;
                rfvtxtACIDITY.Enabled = true;
                rfvtxtRemark.Enabled = false;
                rfvddlVariant.Enabled = false;
            }
            else
            {
                rfvddlOption.Enabled = false;
                rfvtxtTEMP.Enabled = false;
                rfvtxtFAT.Enabled = false;
                rfvtxtCLR.Enabled = false;
                rfvtxtSNF.Enabled = false;
                rfvddlCOB.Enabled = false;
                rfvtxtACIDITY.Enabled = false;
                rfvtxtRemark.Enabled = false;
                rfvddlVariant.Enabled = false;

            }
        }
        else if (ddlTankPosition.SelectedValue == "Under Filling Transfer")
        {

            ddlOption.Enabled = true;
            txtTEMP.Enabled = true;
            txtFAT.Enabled = true;
            txtCLR.Enabled = true;
            txtSNF.Enabled = true;
            txtACIDITY.Enabled = true;
            txtRemark.Enabled = true;
            ddlCOB.Enabled = true;
            ddlVariant.Enabled = true;
            if (chkselect.Checked)
            {
                rfvddlOption.Enabled = true;
                rfvtxtTEMP.Enabled = true;
                rfvtxtFAT.Enabled = false;
                rfvtxtCLR.Enabled = false;
                rfvtxtSNF.Enabled = false;
                rfvddlCOB.Enabled = true;
                rfvtxtACIDITY.Enabled = true;
                rfvtxtRemark.Enabled = false;
                rfvddlVariant.Enabled = true;
            }
            else
            {
                rfvddlOption.Enabled = false;
                rfvtxtTEMP.Enabled = false;
                rfvtxtFAT.Enabled = false;
                rfvtxtCLR.Enabled = false;
                rfvtxtSNF.Enabled = false;
                rfvddlCOB.Enabled = false;
                rfvtxtACIDITY.Enabled = false;
                rfvtxtRemark.Enabled = false;
                rfvddlVariant.Enabled = false;

            }
        }
        else if (ddlTankPosition.SelectedValue == "Ready")
        {
            ddlOption.Enabled = true;
            txtTEMP.Enabled = true;
            txtFAT.Enabled = true;
            txtCLR.Enabled = true;
            txtSNF.Enabled = true;
            txtACIDITY.Enabled = true;
            txtRemark.Enabled = true;
            ddlCOB.Enabled = true;
            ddlVariant.Enabled = true;
            if (chkselect.Checked)
            {
                rfvddlOption.Enabled = true;
                rfvtxtTEMP.Enabled = true;
                rfvtxtFAT.Enabled = true;
                rfvtxtCLR.Enabled = true;
                rfvtxtSNF.Enabled = true;
                rfvddlCOB.Enabled = true;
                rfvtxtACIDITY.Enabled = true;
                rfvtxtRemark.Enabled = true;
                rfvddlVariant.Enabled = true;
            }
            else
            {
                rfvddlOption.Enabled = false;
                rfvtxtTEMP.Enabled = false;
                rfvtxtFAT.Enabled = false;
                rfvtxtCLR.Enabled = false;
                rfvtxtSNF.Enabled = false;
                rfvddlCOB.Enabled = false;
                rfvtxtACIDITY.Enabled = false;
                rfvtxtRemark.Enabled = false;
                rfvddlVariant.Enabled = false;

            }

        }
        else
        {
            if (chkselect.Checked)
            {
                rfvddlOption.Enabled = true;
                rfvtxtTEMP.Enabled = true;
                rfvtxtFAT.Enabled = true;
                rfvtxtCLR.Enabled = true;
                rfvtxtSNF.Enabled = true;
                rfvddlCOB.Enabled = true;
                rfvtxtACIDITY.Enabled = true;
                rfvtxtRemark.Enabled = true;
                rfvddlVariant.Enabled = true;
                rfvddlTankPosition.Enabled = true;
            }
            else
            {
                rfvddlOption.Enabled = false;
                rfvtxtTEMP.Enabled = false;
                rfvtxtFAT.Enabled = false;
                rfvtxtCLR.Enabled = false;
                rfvtxtSNF.Enabled = false;
                rfvddlCOB.Enabled = false;
                rfvtxtACIDITY.Enabled = false;
                rfvtxtRemark.Enabled = false;
                rfvddlVariant.Enabled = false;
                rfvddlTankPosition.Enabled = false;
            }

            ddlOption.Enabled = true;
            txtTEMP.Enabled = true;
            txtFAT.Enabled = true;
            txtCLR.Enabled = true;
            txtSNF.Enabled = true;
            txtACIDITY.Enabled = true;
            txtRemark.Enabled = true;
            ddlCOB.Enabled = true;
            ddlVariant.Enabled = true;
        }
        SetFocus(ddlTankPosition);
    }

    //protected void txtFAT_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        TextBox txt = sender as TextBox;

    //        GridViewRow currentRow = txt.NamingContainer as GridViewRow;
    //        TextBox txtFAT = gvTankPosition.Rows[currentRow.RowIndex]
    //                                           .FindControl("txtFAT") as TextBox;
    //        TextBox txtCLR = gvTankPosition.Rows[currentRow.RowIndex]
    //                                          .FindControl("txtCLR") as TextBox;

    //        TextBox txtSNF = gvTankPosition.Rows[currentRow.RowIndex]
    //                                        .FindControl("txtSNF") as TextBox;
    //        txtSNF.Text = GetSNF(txtFAT.Text, txtCLR.Text).ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
    //    }

    //}
    //protected void txtCLR_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        TextBox txt = sender as TextBox;

    //        GridViewRow currentRow = txt.NamingContainer as GridViewRow;
    //        TextBox txtFAT = gvTankPosition.Rows[currentRow.RowIndex]
    //                                           .FindControl("txtFAT") as TextBox;
    //        TextBox txtCLR = gvTankPosition.Rows[currentRow.RowIndex]
    //                                          .FindControl("txtCLR") as TextBox;

    //        TextBox txtSNF = gvTankPosition.Rows[currentRow.RowIndex]
    //                                        .FindControl("txtSNF") as TextBox;
    //        txtSNF.Text = GetSNF(txtFAT.Text, txtCLR.Text).ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 10:" + ex.Message.ToString());
    //    }
    //}
    protected void txtFilterDate_TextChanged(object sender, EventArgs e)
    {
        FillDetailGrid();
    }
    protected void gvTankPosition_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DropDownList ddloption = e.Row.FindControl("ddloption") as DropDownList;
            ddloption.Items.Clear();
            Label lblV_MCType = e.Row.FindControl("lblV_MCType") as Label;
             Label lblI_MCID = e.Row.FindControl("lblI_MCID") as Label;
            DropDownList ddlVariant = e.Row.FindControl("ddlVariant") as DropDownList;
            ddlVariant.Items.Clear();
            ds = null;
            ddloption.DataSource = objdb.ByProcedure("USP_Mst_MilkQualityList",
                        new string[] { "flag" },
                        new string[] { "1" }, "dataset");
            ddloption.DataValueField = "V_MilkQualityList";
            ddloption.DataTextField = "V_MilkQualityList";
            ddloption.DataBind();
            ddloption.Items.Insert(0, new ListItem("Select", "0"));

            ds = null;
            if (lblV_MCType.Text == "Tank")
            {
                string ItemType_id = "0";
                if(lblI_MCID.Text == "17")
                {
                    ItemType_id = "130";
                }
                else if (lblI_MCID.Text == "18")
                {
                    ItemType_id = "28";
                }
                else if (lblI_MCID.Text == "19")
                {
                    ItemType_id = "109";
                }
                else if (lblI_MCID.Text == "20")
                {
                    //ItemType_id = "103";
                }
                ddlVariant.DataSource = objdb.ByProcedure("Usp_Production_TankPosition",
                     new string[] { "flag", "ItemType_id" },
                     new string[] { "5", ItemType_id }, "dataset");
                ddlVariant.DataValueField = "ItemType_id";
                ddlVariant.DataTextField = "ItemTypeName";
                ddlVariant.DataBind();
                ddlVariant.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlVariant.DataSource = objdb.ByProcedure("Usp_Production_TankPosition",
                      new string[] { "flag" },
                      new string[] { "4" }, "dataset");
                ddlVariant.DataValueField = "ItemType_id";
                ddlVariant.DataTextField = "ItemTypeName";
                ddlVariant.DataBind();
                ddlVariant.Items.Insert(0, new ListItem("Select", "0"));
            }
          

        }
    }
    protected void ddlTankPosition_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        DropDownList ddlTankPosition = sender as DropDownList;
        GridViewRow currentRow = ddlTankPosition.NamingContainer as GridViewRow;

        RequiredFieldValidator rfvddlVariant = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("rfvddlVariant") as RequiredFieldValidator;

        RequiredFieldValidator rfvddlOption = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("rfvddlOption") as RequiredFieldValidator;
        RequiredFieldValidator rfvtxtTEMP = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("rfvtxtTEMP") as RequiredFieldValidator;
        RequiredFieldValidator rfvtxtFAT = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("rfvtxtFAT") as RequiredFieldValidator;
        RequiredFieldValidator rfvtxtCLR = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("rfvtxtCLR") as RequiredFieldValidator;
        RequiredFieldValidator rfvtxtSNF = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("rfvtxtSNF") as RequiredFieldValidator;
        RequiredFieldValidator rfvddlCOB = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("rfvddlCOB") as RequiredFieldValidator;
        RequiredFieldValidator rfvtxtACIDITY = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("rfvtxtACIDITY") as RequiredFieldValidator;
        RequiredFieldValidator rfvtxtRemark = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("rfvtxtRemark") as RequiredFieldValidator;
        RequiredFieldValidator rfvddlTankPosition = gvTankPosition.Rows[currentRow.RowIndex]
                                          .FindControl("rfvddlTankPosition") as RequiredFieldValidator;

        CheckBox chkselect = gvTankPosition.Rows[currentRow.RowIndex]
                                          .FindControl("chkselect") as CheckBox;
        CheckBox chkselect1 = gvTankPosition.Rows[currentRow.RowIndex + 1]
                                         .FindControl("chkselect") as CheckBox;
        DropDownList ddlOption = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("ddlOption") as DropDownList;

        DropDownList ddlVariant = gvTankPosition.Rows[currentRow.RowIndex]
                                          .FindControl("ddlVariant") as DropDownList;

        TextBox txtTEMP = gvTankPosition.Rows[currentRow.RowIndex]
                                          .FindControl("txtTEMP") as TextBox;
        TextBox txtFAT = gvTankPosition.Rows[currentRow.RowIndex]
                                          .FindControl("txtFAT") as TextBox;
        TextBox txtCLR = gvTankPosition.Rows[currentRow.RowIndex]
                                          .FindControl("txtCLR") as TextBox;

        TextBox txtSNF = gvTankPosition.Rows[currentRow.RowIndex]
                                          .FindControl("txtSNF") as TextBox;

        TextBox txtACIDITY = gvTankPosition.Rows[currentRow.RowIndex]
                                          .FindControl("txtACIDITY") as TextBox;

        TextBox txtRemark = gvTankPosition.Rows[currentRow.RowIndex]
                                          .FindControl("txtRemark") as TextBox;

        DropDownList ddlCOB = gvTankPosition.Rows[currentRow.RowIndex]
                                           .FindControl("ddlCOB") as DropDownList;
        if (ddlTankPosition.SelectedValue == "Empty")
        {
            rfvddlOption.Enabled = false;
            rfvtxtTEMP.Enabled = false;
            rfvtxtFAT.Enabled = false;
            rfvtxtCLR.Enabled = false;
            rfvtxtSNF.Enabled = false;
            rfvddlCOB.Enabled = false;
            rfvtxtACIDITY.Enabled = false;
            rfvtxtRemark.Enabled = false;
            rfvddlVariant.Enabled = false;


            ddlOption.Enabled = false;
            txtTEMP.Enabled = false;
            
            txtFAT.Enabled = false;
            txtCLR.Enabled = false;
            txtSNF.Enabled = false;
            txtACIDITY.Enabled = false;
            txtRemark.Enabled = false;
            ddlCOB.Enabled = false;
            ddlVariant.Enabled = false;
            txtTEMP.Text = "";
            txtFAT.Text = "";
            txtSNF.Text = "";
            txtCLR.Text = "";
            txtACIDITY.Text = "";
            txtRemark.Text = "";
            ddlCOB.ClearSelection();
            ddlVariant.ClearSelection();
            SetFocus(chkselect1);

        }
        else if (ddlTankPosition.SelectedValue == "Under Processing" || ddlTankPosition.SelectedValue == "Under Agitation")
        {

            ddlOption.Enabled = true;
            txtTEMP.Enabled = true;
            txtFAT.Enabled = true;
            txtCLR.Enabled = true;
            txtSNF.Enabled = true;
            txtACIDITY.Enabled = true;
            txtRemark.Enabled = true;
            ddlCOB.Enabled = true;
            ddlVariant.Enabled = true;

            if (chkselect.Checked)
            {
                rfvddlOption.Enabled = true;
                rfvtxtTEMP.Enabled = true;
                rfvtxtFAT.Enabled = false;
                rfvtxtCLR.Enabled = false;
                rfvtxtSNF.Enabled = false;
                rfvddlCOB.Enabled = true;
                rfvtxtACIDITY.Enabled = true;
                rfvtxtRemark.Enabled = false;
                rfvddlVariant.Enabled = false;
            }
            else
            {
                rfvddlOption.Enabled = false;
                rfvtxtTEMP.Enabled = false;
                rfvtxtFAT.Enabled = false;
                rfvtxtCLR.Enabled = false;
                rfvtxtSNF.Enabled = false;
                rfvddlCOB.Enabled = false;
                rfvtxtACIDITY.Enabled = false;
                rfvtxtRemark.Enabled = false;
                rfvddlVariant.Enabled = false;

            }
            SetFocus(ddlVariant);
        }
        else if (ddlTankPosition.SelectedValue == "Under Filling Transfer")
        {

            ddlOption.Enabled = true;
            txtTEMP.Enabled = true;
            txtFAT.Enabled = true;
            txtCLR.Enabled = true;
            txtSNF.Enabled = true;
            txtACIDITY.Enabled = true;
            txtRemark.Enabled = true;
            ddlCOB.Enabled = true;
            ddlVariant.Enabled = true;
            if (chkselect.Checked)
            {
                rfvddlOption.Enabled = true;
                rfvtxtTEMP.Enabled = true;
                rfvtxtFAT.Enabled = false;
                rfvtxtCLR.Enabled = false;
                rfvtxtSNF.Enabled = false;
                rfvddlCOB.Enabled = true;
                rfvtxtACIDITY.Enabled = true;
                rfvtxtRemark.Enabled = false;
                rfvddlVariant.Enabled = true;
            }
            else
            {
                rfvddlOption.Enabled = false;
                rfvtxtTEMP.Enabled = false;
                rfvtxtFAT.Enabled = false;
                rfvtxtCLR.Enabled = false;
                rfvtxtSNF.Enabled = false;
                rfvddlCOB.Enabled = false;
                rfvtxtACIDITY.Enabled = false;
                rfvtxtRemark.Enabled = false;
                rfvddlVariant.Enabled = false;

            }
            SetFocus(ddlVariant);
        }
        else if (ddlTankPosition.SelectedValue == "Ready")
        {
            ddlOption.Enabled = true;
            txtTEMP.Enabled = true;
            txtFAT.Enabled = true;
            txtCLR.Enabled = true;
            txtSNF.Enabled = true;
            txtACIDITY.Enabled = true;
            txtRemark.Enabled = true;
            ddlCOB.Enabled = true;
            ddlVariant.Enabled = true;
            if (chkselect.Checked)
            {
                rfvddlOption.Enabled = true;
                rfvtxtTEMP.Enabled = true;
                rfvtxtFAT.Enabled = true;
                rfvtxtCLR.Enabled = true;
                rfvtxtSNF.Enabled = true;
                rfvddlCOB.Enabled = true;
                rfvtxtACIDITY.Enabled = true;
                rfvtxtRemark.Enabled = true;
                rfvddlVariant.Enabled = true;
            }
            else
            {
                rfvddlOption.Enabled = false;
                rfvtxtTEMP.Enabled = false;
                rfvtxtFAT.Enabled = false;
                rfvtxtCLR.Enabled = false;
                rfvtxtSNF.Enabled = false;
                rfvddlCOB.Enabled = false;
                rfvtxtACIDITY.Enabled = false;
                rfvtxtRemark.Enabled = false;
                rfvddlVariant.Enabled = false;

            }
            SetFocus(ddlVariant);
        }
        else
        {
            rfvddlOption.Enabled = false;
            rfvtxtTEMP.Enabled = false;
            rfvtxtFAT.Enabled = false;
            rfvtxtCLR.Enabled = false;
            rfvtxtSNF.Enabled = false;
            rfvddlCOB.Enabled = false;
            rfvtxtACIDITY.Enabled = false;
            rfvtxtRemark.Enabled = false;
            rfvddlVariant.Enabled = false;

            ddlOption.Enabled = true;
            txtTEMP.Enabled = true;
            txtFAT.Enabled = true;
            txtCLR.Enabled = true;
            txtSNF.Enabled = true;
            txtACIDITY.Enabled = true;
            txtRemark.Enabled = true;
            ddlCOB.Enabled = true;
            ddlVariant.Enabled = true;
            SetFocus(ddlVariant);
        }
    }
    #endregion


    protected void txtFAT_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        int index = row.RowIndex;
        TextBox txtFat = (TextBox)gvTankPosition.Rows[index].FindControl("txtFAT");
        TextBox txtCLR = (TextBox)gvTankPosition.Rows[index].FindControl("txtCLR");
        TextBox txtSNF = (TextBox)gvTankPosition.Rows[index].FindControl("txtSNF");
        DropDownList ddlVariant = (DropDownList)gvTankPosition.Rows[index].FindControl("ddlVariant");
        decimal Fat = 0;
        decimal CLR = 0;

        if (ddlVariant.SelectedIndex > 0 && txtFat.Text != "" && txtCLR.Text != "")
        {
            ViewState["ItemType_id"] = ddlVariant.SelectedValue;
            if (txtFat.Text == "")
            {
                Fat = 0;
            }
            else
            {
                Fat = decimal.Parse(txtFat.Text);
            }
            if (txtCLR.Text == "")
            {
                Fat = 0;
            }
            else
            {
                CLR = decimal.Parse(txtCLR.Text);
            }
            GetSNF(Fat, CLR);
            txtSNF.Text = ViewState["SNF"].ToString();
        }
        else
        {
            txtSNF.Text = "";
        }
        SetFocus(txtCLR);
    }
    protected void txtCLR_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
        int index = row.RowIndex;
        TextBox txtFat = (TextBox)gvTankPosition.Rows[index].FindControl("txtFAT");
        TextBox txtCLR = (TextBox)gvTankPosition.Rows[index].FindControl("txtCLR");
        TextBox txtSNF = (TextBox)gvTankPosition.Rows[index].FindControl("txtSNF");
        DropDownList ddlVariant = (DropDownList)gvTankPosition.Rows[index].FindControl("ddlVariant");
        DropDownList ddlCOB = (DropDownList)gvTankPosition.Rows[index].FindControl("ddlCOB");
        decimal Fat = 0;
        decimal CLR = 0;

        if (ddlVariant.SelectedIndex > 0 && txtFat.Text != "" && txtCLR.Text != "")
        {
            ViewState["ItemType_id"] = ddlVariant.SelectedValue;
            if (txtFat.Text == "")
            {
                Fat = 0;
            }
            else
            {
                Fat = decimal.Parse(txtFat.Text);
            }
            if (txtCLR.Text == "")
            {
                Fat = 0;
            }
            else
            {
                CLR = decimal.Parse(txtCLR.Text);
            }
            GetSNF(Fat, CLR);
            txtSNF.Text = ViewState["SNF"].ToString();
        }
        else
        {
            txtSNF.Text = "";
        }
        SetFocus(ddlCOB);
    }
    protected void GetSNF(decimal Fat, decimal CLR)
    {
        decimal SNF = 0;

        if (ViewState["ItemType_id"].ToString() == "58")
        {
            SNF = (CLR / 4) + (Fat * Convert.ToDecimal(.25)) + Convert.ToDecimal(.44);
        }
        else if (ViewState["ItemType_id"].ToString() == "59" || ViewState["ItemType_id"].ToString() == "60" || ViewState["ItemType_id"].ToString() == "61")
        {
            SNF = (CLR / 4) + (Fat * Convert.ToDecimal(.25)) + Convert.ToDecimal(.52);
        }
        else if (ViewState["ItemType_id"].ToString() == "62")
        {
            SNF = (CLR / 4) + (Fat * Convert.ToDecimal(.25)) + Convert.ToDecimal(.72);
        }
        else if (ViewState["ItemType_id"].ToString() == "87")
        {
            SNF = (CLR / 4) + (Fat * Convert.ToDecimal(.25)) + Convert.ToDecimal(.60);
        }
        else
        {
            SNF = Obj_MC.GetSNFPer(Fat, CLR);
        }
        ViewState["SNF"] = Math.Round(SNF, 2);
    }
    protected void ddlVariant_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
        int index = row.RowIndex;
        TextBox txtFat = (TextBox)gvTankPosition.Rows[index].FindControl("txtFAT");
        TextBox txtCLR = (TextBox)gvTankPosition.Rows[index].FindControl("txtCLR");
        TextBox txtSNF = (TextBox)gvTankPosition.Rows[index].FindControl("txtSNF");
        DropDownList ddlVariant = (DropDownList)gvTankPosition.Rows[index].FindControl("ddlVariant");
        DropDownList ddlOption = (DropDownList)gvTankPosition.Rows[index].FindControl("ddlOption");
        decimal Fat = 0;
        decimal CLR = 0;

        if (ddlVariant.SelectedIndex > 0 && txtFat.Text != "" && txtCLR.Text != "")
        {
            ViewState["ItemType_id"] = ddlVariant.SelectedValue;
            if (txtFat.Text == "")
            {
                Fat = 0;
            }
            else
            {
                Fat = decimal.Parse(txtFat.Text);
            }
            if (txtCLR.Text == "")
            {
                Fat = 0;
            }
            else
            {
                CLR = decimal.Parse(txtCLR.Text);
            }
            GetSNF(Fat, CLR);
            txtSNF.Text = ViewState["SNF"].ToString();
        }
        else
        {
            txtSNF.Text = "";
        }
        SetFocus(ddlOption);
    }
    protected void btnShowntoplant_Click(object sender, EventArgs e)
    {
        try
        {
            string Status = "0";
            string TankPositionID_Mlt = "";

            lblMsg.Text = "";
            gridlblmsg.Text = "";
           foreach (GridViewRow rows in gvDetail.Rows)
           {
               CheckBox chk = (CheckBox)rows.FindControl("rptchk2");
               Label lblRowNumber = (Label)rows.FindControl("lblRowNumber");
               if (chk.Checked == true)
               {
                   Status = "1";
                   TankPositionID_Mlt += lblRowNumber.ToolTip.ToString() + ",";
               }
           }
           if (Status == "1")
           {
               ds = objdb.ByProcedure("Usp_Production_TankPosition", new string[] { "flag", "TankPositionID_Mlt" }, new string[] { "6", TankPositionID_Mlt }, "dataset");
               gridlblmsg.Text = objdb.Alert("fa-success", "alert-success", "Thankyou!", "Now Record will be shown to Plant");
               txtFilterDate_TextChanged(sender, e);



           }
           else
           {
               gridlblmsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Please Select At Least One CheckBox Row");
           }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : ). " + ex.Message.ToString());
        }
    }
    protected void check_All_CheckedChanged(object sender, EventArgs e)
    {
        foreach(GridViewRow Rows in gvTankPosition.Rows)
        {
            RequiredFieldValidator rfvddlOption = gvTankPosition.Rows[Rows.RowIndex]
                                           .FindControl("rfvddlOption") as RequiredFieldValidator;

            RequiredFieldValidator rfvddlVariant = gvTankPosition.Rows[Rows.RowIndex]
                                           .FindControl("rfvddlVariant") as RequiredFieldValidator;
            RequiredFieldValidator rfvtxtTEMP = gvTankPosition.Rows[Rows.RowIndex]
                                           .FindControl("rfvtxtTEMP") as RequiredFieldValidator;
            RequiredFieldValidator rfvtxtFAT = gvTankPosition.Rows[Rows.RowIndex]
                                           .FindControl("rfvtxtFAT") as RequiredFieldValidator;
            RequiredFieldValidator rfvtxtCLR = gvTankPosition.Rows[Rows.RowIndex]
                                           .FindControl("rfvtxtCLR") as RequiredFieldValidator;
            RequiredFieldValidator rfvtxtSNF = gvTankPosition.Rows[Rows.RowIndex]
                                           .FindControl("rfvtxtSNF") as RequiredFieldValidator;
            RequiredFieldValidator rfvddlCOB = gvTankPosition.Rows[Rows.RowIndex]
                                           .FindControl("rfvddlCOB") as RequiredFieldValidator;
            RequiredFieldValidator rfvtxtACIDITY = gvTankPosition.Rows[Rows.RowIndex]
                                           .FindControl("rfvtxtACIDITY") as RequiredFieldValidator;
            RequiredFieldValidator rfvtxtRemark = gvTankPosition.Rows[Rows.RowIndex]
                                           .FindControl("rfvtxtRemark") as RequiredFieldValidator;
            RequiredFieldValidator rfvddlTankPosition = gvTankPosition.Rows[Rows.RowIndex]
                                          .FindControl("rfvddlTankPosition") as RequiredFieldValidator;

            CheckBox chkselect = gvTankPosition.Rows[Rows.RowIndex]
                                         .FindControl("chkselect") as CheckBox;
            DropDownList ddlOption = gvTankPosition.Rows[Rows.RowIndex]
                                           .FindControl("ddlOption") as DropDownList;

            DropDownList ddlVariant = gvTankPosition.Rows[Rows.RowIndex]
                                          .FindControl("ddlVariant") as DropDownList;

            TextBox txtTEMP = gvTankPosition.Rows[Rows.RowIndex]
                                          .FindControl("txtTEMP") as TextBox;
            TextBox txtFAT = gvTankPosition.Rows[Rows.RowIndex]
                                          .FindControl("txtFAT") as TextBox;
            TextBox txtCLR = gvTankPosition.Rows[Rows.RowIndex]
                                          .FindControl("txtCLR") as TextBox;

            TextBox txtSNF = gvTankPosition.Rows[Rows.RowIndex]
                                          .FindControl("txtSNF") as TextBox;

            TextBox txtACIDITY = gvTankPosition.Rows[Rows.RowIndex]
                                          .FindControl("txtACIDITY") as TextBox;

            TextBox txtRemark = gvTankPosition.Rows[Rows.RowIndex]
                                          .FindControl("txtRemark") as TextBox;

            DropDownList ddlCOB = gvTankPosition.Rows[Rows.RowIndex]
                                           .FindControl("ddlCOB") as DropDownList;

            DropDownList ddlTankPosition = gvTankPosition.Rows[Rows.RowIndex]
                                           .FindControl("ddlTankPosition") as DropDownList;

        if (ddlTankPosition.SelectedValue == "Empty")
        {
            rfvddlOption.Enabled = false;
            rfvtxtTEMP.Enabled = false;
            rfvtxtFAT.Enabled = false;
            rfvtxtCLR.Enabled = false;
            rfvtxtSNF.Enabled = false;
            rfvddlCOB.Enabled = false;
            rfvtxtACIDITY.Enabled = false;
            rfvtxtRemark.Enabled = false;
            rfvddlVariant.Enabled = false;


            ddlOption.Enabled = false;
            txtTEMP.Enabled = false;
            txtFAT.Enabled = false;
            txtCLR.Enabled = false;
            txtSNF.Enabled = false;
            txtACIDITY.Enabled = false;
            txtRemark.Enabled = false;
            ddlCOB.Enabled = false;
            ddlVariant.Enabled = false;

        }
        else if (ddlTankPosition.SelectedValue == "Under Processing" || ddlTankPosition.SelectedValue == "Under Agitation")
        {

            ddlOption.Enabled = true;
            txtTEMP.Enabled = true;
            txtFAT.Enabled = true;
            txtCLR.Enabled = true;
            txtSNF.Enabled = true;
            txtACIDITY.Enabled = true;
            txtRemark.Enabled = true;
            ddlCOB.Enabled = true;
            ddlVariant.Enabled = true;

            if (chkselect.Checked)
            {
                rfvddlOption.Enabled = true;
                rfvtxtTEMP.Enabled = true;
                rfvtxtFAT.Enabled = false;
                rfvtxtCLR.Enabled = false;
                rfvtxtSNF.Enabled = false;
                rfvddlCOB.Enabled = true;
                rfvtxtACIDITY.Enabled = true;
                rfvtxtRemark.Enabled = false;
                rfvddlVariant.Enabled = false;
            }
            else
            {
                rfvddlOption.Enabled = false;
                rfvtxtTEMP.Enabled = false;
                rfvtxtFAT.Enabled = false;
                rfvtxtCLR.Enabled = false;
                rfvtxtSNF.Enabled = false;
                rfvddlCOB.Enabled = false;
                rfvtxtACIDITY.Enabled = false;
                rfvtxtRemark.Enabled = false;
                rfvddlVariant.Enabled = false;

            }
        }
        else if (ddlTankPosition.SelectedValue == "Under Filling Transfer")
        {

            ddlOption.Enabled = true;
            txtTEMP.Enabled = true;
            txtFAT.Enabled = true;
            txtCLR.Enabled = true;
            txtSNF.Enabled = true;
            txtACIDITY.Enabled = true;
            txtRemark.Enabled = true;
            ddlCOB.Enabled = true;
            ddlVariant.Enabled = true;
            if (chkselect.Checked)
            {
                rfvddlOption.Enabled = true;
                rfvtxtTEMP.Enabled = true;
                rfvtxtFAT.Enabled = false;
                rfvtxtCLR.Enabled = false;
                rfvtxtSNF.Enabled = false;
                rfvddlCOB.Enabled = true;
                rfvtxtACIDITY.Enabled = true;
                rfvtxtRemark.Enabled = false;
                rfvddlVariant.Enabled = true;
            }
            else
            {
                rfvddlOption.Enabled = false;
                rfvtxtTEMP.Enabled = false;
                rfvtxtFAT.Enabled = false;
                rfvtxtCLR.Enabled = false;
                rfvtxtSNF.Enabled = false;
                rfvddlCOB.Enabled = false;
                rfvtxtACIDITY.Enabled = false;
                rfvtxtRemark.Enabled = false;
                rfvddlVariant.Enabled = false;

            }
        }
        else if (ddlTankPosition.SelectedValue == "Ready")
        {
            ddlOption.Enabled = true;
            txtTEMP.Enabled = true;
            txtFAT.Enabled = true;
            txtCLR.Enabled = true;
            txtSNF.Enabled = true;
            txtACIDITY.Enabled = true;
            txtRemark.Enabled = true;
            ddlCOB.Enabled = true;
            ddlVariant.Enabled = true;
            if (chkselect.Checked)
            {
                rfvddlOption.Enabled = true;
                rfvtxtTEMP.Enabled = true;
                rfvtxtFAT.Enabled = true;
                rfvtxtCLR.Enabled = true;
                rfvtxtSNF.Enabled = true;
                rfvddlCOB.Enabled = true;
                rfvtxtACIDITY.Enabled = true;
                rfvtxtRemark.Enabled = true;
                rfvddlVariant.Enabled = true;
            }
            else
            {
                rfvddlOption.Enabled = false;
                rfvtxtTEMP.Enabled = false;
                rfvtxtFAT.Enabled = false;
                rfvtxtCLR.Enabled = false;
                rfvtxtSNF.Enabled = false;
                rfvddlCOB.Enabled = false;
                rfvtxtACIDITY.Enabled = false;
                rfvtxtRemark.Enabled = false;
                rfvddlVariant.Enabled = false;

            }

        }
        else
        {
            if (chkselect.Checked)
            {
                rfvddlOption.Enabled = true;
                rfvtxtTEMP.Enabled = true;
                rfvtxtFAT.Enabled = true;
                rfvtxtCLR.Enabled = true;
                rfvtxtSNF.Enabled = true;
                rfvddlCOB.Enabled = true;
                rfvtxtACIDITY.Enabled = true;
                rfvtxtRemark.Enabled = true;
                rfvddlVariant.Enabled = true;
                rfvddlTankPosition.Enabled = true;
            }
            else
            {
                rfvddlOption.Enabled = false;
                rfvtxtTEMP.Enabled = false;
                rfvtxtFAT.Enabled = false;
                rfvtxtCLR.Enabled = false;
                rfvtxtSNF.Enabled = false;
                rfvddlCOB.Enabled = false;
                rfvtxtACIDITY.Enabled = false;
                rfvtxtRemark.Enabled = false;
                rfvddlVariant.Enabled = false;
                rfvddlTankPosition.Enabled = false;
            }

            ddlOption.Enabled = true;
            txtTEMP.Enabled = true;
            txtFAT.Enabled = true;
            txtCLR.Enabled = true;
            txtSNF.Enabled = true;
            txtACIDITY.Enabled = true;
            txtRemark.Enabled = true;
            ddlCOB.Enabled = true;
            ddlVariant.Enabled = true;
        }
        }
    }
}