using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Services;
using System.Globalization;

public partial class mis_dailyplan_GeneralHourlyTesting : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("en-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null)
        {
            if (!IsPostBack)
            {
                lblMsg.Text = "";
                //Get Page Token
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                txtEntryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtEntryDate.Attributes.Add("readonly", "readonly");
                txtFilterDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFilterDate.Attributes.Add("readonly", "readonly");
                FillGrid();
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
            btnExcel.Visible = false;
            btnShowntoplant.Visible = false;
            ds = objdb.ByProcedure("Usp_Production_GeneralHourlyTesting", new string[] { "flag", "Office_ID", "Date" }, new string[] { "1", objdb.Office_ID(), Convert.ToDateTime(txtFilterDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
            if(ds != null && ds.Tables.Count > 0)
            {
                if(ds.Tables[0].Rows.Count > 0)
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
    protected void ClearText()
    {
        txtTempAt.Text = "";
        txtCanWasher_Temp.Text = "";
        txtCIP_Caustic_Temp.Text = "";
        txtCIP_Acid_Temp.Text = "";
        txtCrateWasher1_Temp.Text = "";
        txtCrateWasher2_Temp.Text = "";
        txtCrateWasher3_Temp.Text = "";
        txtCrateWasher4_Temp.Text = "";
        txtStengthofdetAt.Text = "";
        txtCanWasher_Stengthofdet.Text = "";
        txtCIP_Caustic_Stengthofdet.Text = "";
        txtCIP_Acid_Stengthofdet.Text = "";
        txtCrateWasher1_Stengthofdet.Text = "";
        txtCrateWasher2_Stengthofdet.Text = "";
        txtCrateWasher3_Stengthofdet.Text = "";
        txtCrateWasher4_Stengthofdet.Text = "";
        txtHardnessofWaterAt.Text = "";
        txtTW_HardnessofWater.Text = "";
        txtSoftner1_HardnessofWater.Text = "";
        txtSoftner2_HardnessofWater.Text = "";
        txtTempofcoldstorageAt.Text = "";
        txtMCSR1_Tempofcoldstorage.Text = "";
        txtMCSR2_Tempofcoldstorage.Text = "";
        txtTempofProdcoldstorageAt.Text = "";
        txtProductCR1_Tempofcoldstorage.Text = "";
        txtProductCR2_Tempofcoldstorage.Text = "";
        txtProductCR3_Tempofcoldstorage.Text = "";
        txtTempofbufferdeepfreezerAt.Text = "";
        txtDeepFreezer1_Tempofbufferdeepfreezer.Text = "";
        txtDeepFreezer2_Tempofbufferdeepfreezer.Text = "";
        txtTempChilledWaterAt.Text = "";
        txtTank1_TempChilledWater.Text = "";
        txtTank2_TempChilledWater.Text = "";
        txtTank3_TempChilledWater.Text = "";
        txtTank4_TempChilledWater.Text = "";
    }
    #endregion

    #region Button Click Event
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                lblMsg.Text = "";
                string IsActive = "1";
                ds = objdb.ByProcedure("Usp_Production_GeneralHourlyTesting",
                                       new string[]
                                   {"flag" 
                                    ,"Date"
                                    ,"TempAt"
                                    ,"HotWater_Temp"
                                    ,"CanWasher_Temp"
                                    ,"CIP_Caustic_Temp"
                                    ,"CIP_Acid_Temp"
                                    ,"CrateWasher1_Temp"
                                    ,"CrateWasher2_Temp"
                                    ,"CrateWasher3_Temp"
                                    ,"CrateWasher4_Temp"
                                    ,"StengthofdetAt"
                                    ,"HotWater_Stengthofdet"
                                    ,"CanWasher_Stengthofdet"
                                    ,"CIP_Caustic_Stengthofdet"
                                    ,"CIP_Acid_Stengthofdet"
                                    ,"CrateWasher1_Stengthofdet"
                                    ,"CrateWasher2_Stengthofdet"
                                    ,"CrateWasher3_Stengthofdet"
                                    ,"CrateWasher4_Stengthofdet"
                                    ,"HardnessofWaterAt"
                                    ,"TW_HardnessofWater"
                                    ,"Softner1_HardnessofWater"
                                    ,"Softner2_HardnessofWater"
                                    ,"TempofcoldstorageAt"
                                    ,"MCSR1_Tempofcoldstorage"
                                    ,"MCSR2_Tempofcoldstorage"
                                    ,"TempofProdcoldstorageAt"
                                    ,"ProductCR1_Tempofcoldstorage"
                                    ,"ProductCR2_Tempofcoldstorage"	
                                    ,"ProductCR3_Tempofcoldstorage"
                                    ,"TempofbufferdeepfreezerAt"
                                    ,"DeepFreezer1_Tempofbufferdeepfreezer"
                                    ,"DeepFreezer2_Tempofbufferdeepfreezer"
                                    ,"TempChilledWaterAt"
                                    ,"Tank1_TempChilledWater"
                                    ,"Tank2_TempChilledWater"
                                    ,"Tank3_TempChilledWater"
                                    ,"Tank4_TempChilledWater"
                                    ,"Office_ID"
		                            ,"IsActive"
		                            ,"CreatedBy"
		                            ,"CreatedByIp"
                                   },
                                       new string[]
                                   {"0"   
                                    ,Convert.ToDateTime(txtEntryDate.Text,cult).ToString("yyyy/MM/dd")
                                    ,txtTempAt.Text
                                    ,txtHotWater_Temp.Text                                    
                                    ,txtCanWasher_Temp.Text
                                    ,txtCIP_Caustic_Temp.Text
                                    ,txtCIP_Acid_Temp.Text
                                    ,txtCrateWasher1_Temp.Text
                                    ,txtCrateWasher2_Temp.Text
                                    ,txtCrateWasher3_Temp.Text
                                    ,txtCrateWasher4_Temp.Text
                                    ,txtStengthofdetAt.Text
                                    ,txtHotWater_Stengthofdet.Text
                                    ,txtCanWasher_Stengthofdet.Text
                                    ,txtCIP_Caustic_Stengthofdet.Text
                                    ,txtCIP_Acid_Stengthofdet.Text
                                    ,txtCrateWasher1_Stengthofdet.Text
                                    ,txtCrateWasher2_Stengthofdet.Text
                                    ,txtCrateWasher3_Stengthofdet.Text
                                    ,txtCrateWasher4_Stengthofdet.Text
                                    ,txtHardnessofWaterAt.Text
                                    ,txtTW_HardnessofWater.Text
                                    ,txtSoftner1_HardnessofWater.Text
                                    ,txtSoftner2_HardnessofWater.Text
                                    ,txtTempofcoldstorageAt.Text
                                    ,txtMCSR1_Tempofcoldstorage.Text
                                    ,txtMCSR2_Tempofcoldstorage.Text
                                    ,txtTempofProdcoldstorageAt.Text
                                    ,txtProductCR1_Tempofcoldstorage.Text
                                    ,txtProductCR2_Tempofcoldstorage.Text	
                                    ,txtProductCR3_Tempofcoldstorage.Text
                                    ,txtTempofbufferdeepfreezerAt.Text
                                    ,txtDeepFreezer1_Tempofbufferdeepfreezer.Text
                                    ,txtDeepFreezer2_Tempofbufferdeepfreezer.Text
                                    ,txtTempChilledWaterAt.Text
                                    ,txtTank1_TempChilledWater.Text
                                    ,txtTank2_TempChilledWater.Text
                                    ,txtTank3_TempChilledWater.Text
                                    ,txtTank4_TempChilledWater.Text
                                    ,objdb.Office_ID()
                                    ,IsActive
                                    ,objdb.createdBy()
                                    ,objdb.GetLocalIPAddress()
                                   }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string Success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", Success.ToString());

                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                    {
                        string Warning = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", Warning.ToString());

                    }
                    else
                    {
                        string Danger = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Danger.ToString());
                    }
                    ClearText();
                    FillGrid();
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 12:" + ex.Message.ToString());
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            gvDetail.Columns[0].Visible = false;
            string FileName = "GeneralHourlyTestingReport";
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
            gvDetail.Columns[0].Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 10 ", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("GeneralHourlyTesting.aspx", false);
    }
    #endregion

    #region Gridview RowEvent
    protected void gvDetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 2;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "TEMPERATURE ( °C )";
            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "STRENGTH OF DETERGENT ( % )";
            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "HARDNESS OF WATER (PPM)";
            HeaderCell.ColumnSpan = 4;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "TEMP OF COLD STORAGE ( °C )";
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "TEMP OF PRODUCT COLD STORAGE ( °C )";
            HeaderCell.ColumnSpan = 4;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "TEMP OF BUFFER DEEP FREEZER ( °C )";
            HeaderCell.ColumnSpan = 3;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "TEMP CHILLED WATER AT ( °C )";
            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);

            gvDetail.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }
    }
    protected void txtFilterDate_TextChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    #endregion



    protected void btnShowntoplant_Click(object sender, EventArgs e)
    {
        try
        {
            string Status = "0";
            string GeneralHourlyTestingId_Mlt = "";

            lblMsg.Text = "";
            gridlblmsg.Text = "";
            foreach (GridViewRow rows in gvDetail.Rows)
            {
                CheckBox chk = (CheckBox)rows.FindControl("CheckBox1");
                Label lblRowNumber = (Label)rows.FindControl("lblRowNumber");
                if (chk.Checked == true)
                {
                    Status = "1";
                    GeneralHourlyTestingId_Mlt += lblRowNumber.ToolTip.ToString() + ",";
                }
            }
            if (Status == "1")
            {
                ds = objdb.ByProcedure("Usp_Production_GeneralHourlyTesting", new string[] { "flag", "GeneralHourlyTestingId_Mlt" }, new string[] { "2", GeneralHourlyTestingId_Mlt }, "dataset");
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
}