using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Globalization;

public partial class mis_MilkCollection_BMCDCSMapping : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, ds2;
    IFormatProvider cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null && objdb.Department_ID() != null)
        {
            if (!IsPostBack)
            {
                //Get Page Token
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                FillBMC();
                FillDCS();
                GetSupplyUnit();
                FillGrid();
				 ddlMCU_SelectedIndexChanged(sender, e);
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
    private void AddDCSDetails()
    {
        try
        {

            int Status = 0;
            if (Convert.ToString(ViewState["InsertRecord"]) == null || Convert.ToString(ViewState["InsertRecord"]) == "")
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("DCSID", typeof(int)));
                dt.Columns.Add(new DataColumn("DCSName", typeof(string)));
                dt.Columns.Add(new DataColumn("DCSName_E", typeof(string)));
                dt.Columns.Add(new DataColumn("SocietyCode", typeof(string)));

                dr = dt.NewRow();
                dr[0] = 1;
                dr[1] = ddlDCS.SelectedValue;
                dr[2] = ddlDCS.SelectedItem.Text;
                dr[3] = txtDCSIn_E.Text;
                dr[4] = txtDCSSocietyCode.Text;

                dt.Rows.Add(dr);


                ViewState["InsertRecord"] = dt;

                gvDCSDetails.DataSource = dt;
                gvDCSDetails.DataBind();


            }
            else
            {
                DataTable dt = new DataTable();
                DataTable DT = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                dt.Columns.Add(new DataColumn("DCSID", typeof(int)));
                dt.Columns.Add(new DataColumn("DCSName", typeof(string)));
                dt.Columns.Add(new DataColumn("DCSName_E", typeof(string)));
                dt.Columns.Add(new DataColumn("SocietyCode", typeof(string)));
                DT = (DataTable)ViewState["InsertRecord"];


                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (ddlDCS.SelectedValue == DT.Rows[i]["DCSID"].ToString())
                    {
                        Status = 1;
                    }

                    if (txtDCSSocietyCode.Text == DT.Rows[i]["SocietyCode"].ToString())
                    {
                        Status = 2;
                    }
                }

                if (Status == 1)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + ddlDCS.SelectedItem.Text + "\" already exist.");
                }

                else if (Status == 2)
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Society Code. \"" + txtDCSSocietyCode.Text + "\" already exist.");
                }
                else
                {
                    dr = dt.NewRow();
                    dr[0] = 1;
                    dr[1] = ddlDCS.SelectedValue;
                    dr[2] = ddlDCS.SelectedItem.Text;
                    dr[3] = txtDCSIn_E.Text;
                    dr[4] = txtDCSSocietyCode.Text;

                    dt.Rows.Add(dr);
                }

                foreach (DataRow tr in DT.Rows)
                {
                    dt.Rows.Add(tr.ItemArray);
                }
                ViewState["InsertRecord"] = dt;
                gvDCSDetails.DataSource = dt;
                gvDCSDetails.DataBind();
            }

            //Clear Record

            ddlDCS.ClearSelection();
            txtDCSSocietyCode.Text = "";
            txtDCSIn_E.Text = "";

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    protected void ddlMilkSupply_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlMilkSupply.SelectedIndex > 0)
            {
                GetSupplyUnit();
            }
            else
            {
                ddlMilkSupply.ClearSelection();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillBMC()
    {
        try
        {

            ddLBMC.Items.Clear();
            string code = ddlMCU.SelectedValue;
            ds = null;
            ds = objdb.ByProcedure("SpAdminOffice",
                                        new string[] { "flag", "OfficeType_ID", "Office_Parant_ID" },
                                        new string[] { "33", code, objdb.Office_ID() }, "TableSave");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddLBMC.DataTextField = "Office_Name";
                    ddLBMC.DataValueField = "Office_ID";
                    ddLBMC.DataSource = ds;
                    ddLBMC.DataBind();
                    ddLBMC.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally { if (ds != null) { ds.Dispose(); } }

    }

    protected void FillDCS()
    {
        try
        {
            string code = "6";

            ds = null;
            ds = objdb.ByProcedure("SpAdminOffice",
                                       new string[] { "flag", "OfficeType_ID", "Office_Parant_ID" },
                                       new string[] { "33", code, objdb.Office_ID() }, "TableSave");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlDCS.DataTextField = "Office_Name";
                    ddlDCS.DataValueField = "Office_ID";
                    ddlDCS.DataSource = ds;
                    ddlDCS.DataBind();
                    ddlDCS.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally { if (ds != null) { ds.Dispose(); } }
    }
    protected void GetSupplyUnit()
    {
        try
        {
            ddlSupplyUnit.Items.Clear();
            string code = "";
            if (ddlMilkSupply.SelectedItem.Text.Trim() == "Select")
            {
                code = string.Empty;
            }
            else if (ddlMilkSupply.SelectedItem.Text.Trim() == "Plant")
            {
                code = "2";
            }
            else if (ddlMilkSupply.SelectedItem.Text.Trim() == "DCS")
            {
                code = "6";
            }
            else if (ddlMilkSupply.SelectedItem.Text.Trim() == "MDP")
            {
                code = "3";
            }
            else if (ddlMilkSupply.SelectedItem.Text.Trim() == "BMC")
            {
                code = "5";
            }
            else if (ddlMilkSupply.SelectedItem.Text.Trim() == "CC")
            {
                code = "4";
            }

            ds = null;
            ds = objdb.ByProcedure("SpAdminOffice",
                                       new string[] { "flag", "Office_ID","Office_Parant_ID" },
                                       new string[] { "17", code,objdb.Office_ID() }, "TableSave");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlSupplyUnit.DataTextField = "Office_Name";
                    ddlSupplyUnit.DataValueField = "Office_ID";
                    ddlSupplyUnit.DataSource = ds;
                    ddlSupplyUnit.DataBind();

                }
            }
            ddlSupplyUnit.Items.Insert(0, new ListItem("Select Supply Unit", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt2 = ViewState["InsertRecord"] as DataTable;
            dt2.Rows.Remove(dt2.Rows[row.RowIndex]);
            ViewState["InsertRecord"] = dt2;
            gvDCSDetails.DataSource = dt2;
            gvDCSDetails.DataBind();



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
       lblMsg.Text = "";
        //string Status = "0";
        AddDCSDetails();
        //ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "SocietyCode", "Office_ID" }, new string[] {"28",txtDCSSocietyCode.Text,ddlDCS.SelectedValue }, "dataset");
        //if(ds != null && ds.Tables[0].Rows.Count > 0)
        //{
        //    Status = ds.Tables[0].Rows[0]["Status"].ToString();
        //    if(Status.ToString() == "0")
        //    {
        //        
        //    }
        //    else
        //    {
        //        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Sorry!", "Society Code Already Exists");
        //    }
        //}
        
    }
    private DataTable GetDCSGridvalue()
    {

        DataTable dtDCS = new DataTable();
        DataRow drDCS;

        dtDCS.Columns.Add(new DataColumn("Office_ID", typeof(int)));
        dtDCS.Columns.Add(new DataColumn("Office_Name_E", typeof(string)));
        dtDCS.Columns.Add(new DataColumn("SocietyCode", typeof(string)));
        foreach (GridViewRow rowDCS in gvDCSDetails.Rows)
        {

            Label lblDCSID = (Label)rowDCS.FindControl("lblDCSID");
            Label lblDCSName_E = (Label)rowDCS.FindControl("lblDCSName_E");
            Label lblSocietyCode = (Label)rowDCS.FindControl("lblSocietyCode");

            drDCS = dtDCS.NewRow();

            drDCS[0] = lblDCSID.Text;
            drDCS[1] = lblDCSName_E.Text;
            drDCS[2] = lblSocietyCode.Text;

            dtDCS.Rows.Add(drDCS);
        }
        return dtDCS;

    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {


                DataTable dtDCS = new DataTable();
                dtDCS = GetDCSGridvalue();
                if (ddlCenter.SelectedValue == "1")
                {
                    if (dtDCS.Rows.Count > 0)
                    {
                        ds = objdb.ByProcedure("SpAdminOffice", new string[] 
                                          {"flag"
                                           ,"Office_ID"
                                           ,"MilkSupplyto"
                                           ,"supplyUnit"
                                           ,"SocietyCode"
                                           ,"Office_Name_E"
                                          },
                                           new string[]            
                                          {"24"
                                          ,ddLBMC.SelectedValue
                                          ,ddlMilkSupply.SelectedItem.Text
                                          ,ddlSupplyUnit.SelectedValue
                                          ,txtBMCSocietyCode.Text
                                          ,txtBMCIn_E.Text
                                          },
                                          "TableSave");
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                            int Count = dtDCS.Rows.Count;
                            for (int i = 0; i < Count; i++)
                            {
                                DataRow dr = dtDCS.Rows[i];
                                string Office_ID = dr["Office_ID"].ToString();
                                string SocietyCode = dr["SocietyCode"].ToString();
                                string Office_Name_E = dr["Office_Name_E"].ToString();
                                objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "Office_ID", "MilkSupplyto", "supplyUnit", "SocietyCode", "Office_Name_E" }, new string[] { "24", Office_ID, ddlMCU.SelectedItem.Text, ddLBMC.SelectedValue, SocietyCode, Office_Name_E }, "dataset");
                            }
                            string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou!", Success.ToString());
                            ClearText();
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                        {
                            string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Sorry!", Warning.ToString());
                        }
                        else
                        {
                            string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Success.ToString());
                        }

                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", "Please Map atleaset One DCS");
                    }
                }
                else
                {
                    ds = objdb.ByProcedure("SpAdminOffice", new string[] 
                                          {"flag"
                                           ,"Office_ID"
                                           ,"MilkSupplyto"
                                           ,"supplyUnit"
                                           ,"SocietyCode"
                                           ,"Office_Name_E"
                                          },
                                           new string[]            
                                          {"24"
                                          ,ddLBMC.SelectedValue
                                          ,ddlMilkSupply.SelectedItem.Text
                                          ,ddlSupplyUnit.SelectedValue
                                          ,txtBMCSocietyCode.Text
                                          ,txtBMCIn_E.Text
                                          },
                                          "TableSave");
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {

                        string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou!", Success.ToString());
                        ClearText();
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                    {
                        string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Sorry!", Warning.ToString());
                    }
                    else
                    {
                        string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Success.ToString());
                    }

                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearText()
    {
        ddLBMC.ClearSelection();
        ddlDCS.ClearSelection();
        ddlMilkSupply.ClearSelection();
        GetSupplyUnit();
        txtBMCIn_E.Text = "";
        ddlCenter.ClearSelection();
        txtBMCSocietyCode.Text = "";
        ViewState["InsertRecord"] = null;
        gvDCSDetails.DataSource = null;
        gvDCSDetails.DataBind();
        FillBMC();
        FillDCS();
    }
    protected void ddlDCS_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            //ddlMilkSupply.ClearSelection();
            txtDCSSocietyCode.Text = "";
            txtDCSIn_E.Text = "";
            if (ddlDCS.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "Office_ID" }, new string[] { "27", ddlDCS.SelectedValue }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    txtDCSSocietyCode.Text = ds.Tables[0].Rows[0]["SocietyCode"].ToString();
                    txtDCSIn_E.Text = ds.Tables[0].Rows[0]["Office_Name_E"].ToString();
                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddLBMC_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            txtDCSSocietyCode.Text = "";
            txtDCSIn_E.Text = "";
            if (ddLBMC.SelectedIndex > 0)
            {
                DataSet ds1 = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "Office_ID" }, new string[] { "27", ddLBMC.SelectedValue }, "dataset");
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    txtBMCSocietyCode.Text = ds1.Tables[0].Rows[0]["SocietyCode"].ToString();
                    txtBMCIn_E.Text = ds1.Tables[0].Rows[0]["Office_Name_E"].ToString();
                    if (ds1.Tables[0].Rows[0]["SocietyCode"].ToString() != "")
                    {
                        if (ds1.Tables[0].Rows[0]["MilkSupplyto"].ToString() != "")
                        {
                            ddlMilkSupply.ClearSelection();
                            ddlMilkSupply.Items.FindByText(ds1.Tables[0].Rows[0]["MilkSupplyto"].ToString()).Selected = true;
                        }

                        GetSupplyUnit();

                        if (ds1.Tables[0].Rows[0]["supplyUnit"].ToString() != "")
                        {
                            ddlSupplyUnit.ClearSelection();
                            ddlSupplyUnit.Items.FindByValue(ds1.Tables[0].Rows[0]["supplyUnit"].ToString()).Selected = true;
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
    protected void ddlCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlCenter.SelectedValue.ToString() == "1")
            {
                dcsPanel.Visible = true;
            }
            else
            {
                ddlDCS.ClearSelection();
                txtDCSSocietyCode.Text = "";
                txtDCSIn_E.Text = "";
                dcsPanel.Visible = false;
                ViewState["InsertRecord"] = null;
                gvDCSDetails.DataSource = string.Empty;
                gvDCSDetails.DataBind();
            }
        }     
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            btnExport.Visible = false;
            btnPrint.Visible = false;
            gvbmccodedetails.DataSource = string.Empty;
            gvbmccodedetails.DataBind();
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpAdminOffice", new string[] { "flag", "Office_Parant_ID", "OfficeType_ID" }, new string[] { "29", objdb.Office_ID(), "5" }, "dataset");
            if(ds != null && ds.Tables[0].Rows.Count > 0)
            {
                btnExport.Visible = true;
                btnPrint.Visible = true;
                gvbmccodedetails.DataSource = ds;
                gvbmccodedetails.DataBind();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    
    protected void gvbmccodedetails_DataBound(object sender, EventArgs e)
    {
        for (int i = gvbmccodedetails.Rows.Count - 2; i > 0; i--)
        {
            GridViewRow row = gvbmccodedetails.Rows[i];
            GridViewRow previousRow = gvbmccodedetails.Rows[i - 1];
            for (int j = 0; j < row.Cells.Count; j++)
            {
                if (row.Cells[j].Text == previousRow.Cells[j].Text)
                {
                    if (previousRow.Cells[j].RowSpan == 0)
                    {
                        if (row.Cells[j].RowSpan == 0)
                        {
                            previousRow.Cells[j].RowSpan += 2;
                        }
                        else
                        {
                            previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                        }
                        row.Cells[j].Visible = false;
                    }
                }
            }
        }
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
            string FileName = "RouteWiseBMC-DCSMapping";
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            gvbmccodedetails.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());
            Response.End();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
	protected void ddlMCU_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";

            FillBMC();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}