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

public partial class mis_dailyplan_VitaminRequestToQC : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    string Fdate = "";
    MilkCalculationClass Obj_MC = new MilkCalculationClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            try
            {
                lblMsg.Text = "";

                if (!IsPostBack)
                {

                    if (Session["IsSuccess"] != null)
                    {
                        if ((Boolean)Session["IsSuccess"] == true)
                        {
                            Session["IsSuccess"] = false;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Insert Successfully !')", true);

                        }
                    }
                    divVariant.Visible = false;
                    FillVariant();
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                   
                    FillShift();
                    
                    FillOffice();
                    GetSectionView();
                  
                  
                    txtFilterDT_TextChanged(sender, e);

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

    protected void FillVariant()
    {
        try
        {
            lblMsg.Text = "";
            ddlVariant.DataSource = objdb.ByProcedure("Usp_Production_TankPosition",
                      new string[] { "flag" },
                      new string[] { "4" }, "dataset");
            ddlVariant.DataValueField = "ItemType_id";
            ddlVariant.DataTextField = "ItemTypeName";
            ddlVariant.DataBind();
            ddlVariant.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillShift()
    {
        try
        {
            lblMsg.Text = "";
            ds = null;
            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                 new string[] { "flag" },
                 new string[] { "2" }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlShift.DataSource = ds.Tables[0];
                ddlShift.DataTextField = "Name";
                ddlShift.DataValueField = "Shift_Id";
                ddlShift.DataBind();
                ddlShift.SelectedValue = ds.Tables[1].Rows[0]["Shift_Id"].ToString();
                txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                //Convert.ToDateTime(ds.Tables[1].Rows[0]["currentDate"].ToString(), cult).ToString("dd/MM/yyyy");
                txtDate.Enabled = false;
                ddlShift.Enabled = false;

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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


                ddlFilterOffice.DataSource = ds.Tables[0];
                ddlFilterOffice.DataTextField = "Office_Name";
                ddlFilterOffice.DataValueField = "Office_ID";
                ddlFilterOffice.DataBind();
                ddlFilterOffice.Items.Insert(0, new ListItem("Select", "0"));
                ddlFilterOffice.SelectedValue = ViewState["Office_ID"].ToString();
                ddlFilterOffice.Enabled = false;
                txtFilterDT.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

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

    private void GetSectionView()
    {

        try
        {

            ds = null;

            ds = objdb.ByProcedure("SpProductionProduct_Milk_InOut",
                  new string[] { "flag", "Office_ID" },
                  new string[] { "6", ddlDS.SelectedValue }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPSection.DataSource = ds.Tables[0];
                ddlPSection.DataTextField = "ProductSection_Name";
                ddlPSection.DataValueField = "ProductSection_ID";
                ddlPSection.DataBind();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));

            }
            else
            {
                ddlPSection.Items.Clear();
                ddlPSection.Items.Insert(0, new ListItem("Select", "0"));
                ddlPSection.DataBind();
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    #endregion

    #region Changed Event
    protected void ddlPSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
            divVariant.Visible = false;
            ddlTestRequestType.Items.Clear();
            ddlTestRequestType.Items.Insert(0, new ListItem("Select", "0"));
            ddlmilktestrequestfor.Items.Clear();
            ddlmilktestrequestfor.Items.Insert(0, new ListItem("Select", "0"));
           

            if (ddlPSection.SelectedValue == "1")
            {
                ddlTestRequestType.Items.Clear();
                ddlTestRequestType.Items.Insert(0, new ListItem("Select", "0"));
                ddlTestRequestType.Items.Insert(1, new ListItem("Silo", "Silo"));
                ddlTestRequestType.Items.Insert(2, new ListItem("PMT", "PMT"));
                ddlTestRequestType.Items.Insert(3, new ListItem("RMT", "RMT"));
                
                divVariant.Visible = true;
                //ddlTestRequestType.Items.Insert(4, new ListItem("Packaging[Milk]", "Milk"));
            }


            if (ddlPSection.SelectedValue == "2")
            {
                ddlTestRequestType.Items.Clear();
                ddlTestRequestType.Items.Insert(0, new ListItem("Select", "0"));
                ddlTestRequestType.Items.Insert(1, new ListItem("Packaging[Product]", "Product"));



            }
            if (ddlPSection.SelectedValue == "9")
            {
                ddlTestRequestType.Items.Clear();
                ddlTestRequestType.Items.Insert(0, new ListItem("Select", "0"));
                ddlTestRequestType.Items.Insert(1, new ListItem("Packaging[Milk]", "Milk"));
                
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlTestRequestType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlTestRequestType.SelectedValue == "0")
            {
                ddlmilktestrequestfor.Items.Clear();
                ddlmilktestrequestfor.Items.Insert(0, new ListItem("Select", "0"));
               
            }
            else
            {

                if (ddlTestRequestType.SelectedValue == "Milk" || ddlTestRequestType.SelectedValue == "Product")
                {
                    string ddtestid = "";

                    if (ddlTestRequestType.SelectedValue == "Milk")
                    {
                        ddtestid = "1";
                    }

                    if (ddlTestRequestType.SelectedValue == "Product")
                    {
                        ddtestid = "2";
                    }

                    ds = null;
                    ds = objdb.ByProcedure("spProductionMilkContainerMaster",
                          new string[] { "flag", "ProductSection_ID", "Office_Id" },
                          new string[] { "3", ddtestid, objdb.Office_ID() }, "dataset");

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlmilktestrequestfor.DataSource = ds.Tables[0];
                        ddlmilktestrequestfor.DataTextField = "ItemName";
                        ddlmilktestrequestfor.DataValueField = "Item_id";
                        ddlmilktestrequestfor.DataBind();
                        ddlmilktestrequestfor.Items.Insert(0, new ListItem("Select", "0"));

                    }
                    else
                    {
                        ddlmilktestrequestfor.Items.Clear();
                        ddlmilktestrequestfor.Items.Insert(0, new ListItem("Select", "0"));
                        ddlmilktestrequestfor.DataBind();
                    }
                }
                else
                {

                    ds = null;
                    ds = objdb.ByProcedure("spProductionMilkContainerMaster",
                           new string[] { "flag", "V_MCType", "Office_Id" },
                           new string[] { "2", ddlTestRequestType.SelectedItem.Text, objdb.Office_ID() }, "dataset");

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        ddlmilktestrequestfor.DataSource = ds.Tables[0];
                        ddlmilktestrequestfor.DataTextField = "V_MCName";
                        ddlmilktestrequestfor.DataValueField = "I_MCID";
                        ddlmilktestrequestfor.DataBind();
                        ddlmilktestrequestfor.Items.Insert(0, new ListItem("Select", "0"));

                    }
                    else
                    {
                        ddlmilktestrequestfor.Items.Clear();
                        ddlmilktestrequestfor.Items.Insert(0, new ListItem("Select", "0"));
                        ddlmilktestrequestfor.DataBind();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void txtFilterDT_TextChanged(object sender, EventArgs e)
    {
        try
        {
            btnExport.Visible = false;

            if (txtFilterDT.Text != "")
            {
                Fdate = Convert.ToDateTime(txtFilterDT.Text, cult).ToString("yyyy/MM/dd");
            }


            GridView1.DataSource = null;
            GridView1.DataBind();
            btnExport.Visible = false;
          
            ds = null;
            ds = objdb.ByProcedure("Usp_Production_VitaminRequest"
            , new string[] { "flag", "Office_Id", "VitaminRequest_DT" }
            , new string[] { "2", ViewState["Office_ID"].ToString(), Fdate }, "dataset");

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = ds;
                        GridView1.DataBind();
                       
                        btnExport.Visible = true;
                      
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
            else
            {
                GridView1.DataSource = string.Empty;
                GridView1.DataBind();
               
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    
    
    
    #endregion

    #region Button Click Event
   
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                if(btnSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("Usp_Production_VitaminRequest"
                                     , new string[] {"flag",
                                                     "Office_Id", 
                                                     "VitaminRequest_DT", 
                                                     "Shift_ID", 
                                                     "ProductSection_ID", 
                                                     "RequestType", 
                                                     "RequestFor", 
                                                     "RequestFor_ID", 
                                                     "ItemType_id", 
                                                     "MilkQuantity",                                                     
                                                     "Request_Status", 
                                                     "Request_Remark", 
                                                     "CreatedBy", 
                                                     "CreatedBy_IP"
                                                    }
                                    , new string[]  {"1" ,
                                                     objdb.Office_ID(),
                                                     Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd hh:mm tt"),
                                                     ddlShift.SelectedValue.ToString(),
                                                     ddlPSection.SelectedValue,
                                                     ddlTestRequestType.SelectedValue,
                                                     ddlmilktestrequestfor.SelectedItem.Text,
                                                     ddlmilktestrequestfor.SelectedValue,
                                                     ddlVariant.SelectedValue,
                                                     txtMilkQty.Text,
                                                     "Pending",
                                                     txtRemarks_R.Text,
                                                     objdb.createdBy(),
                                                     objdb.GetLocalIPAddress()
                                                    }
                                    , "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {

                                Session["IsSuccess"] = true;
                                Response.Redirect("VitaminRequestToQC.aspx", false);


                            }
                            else
                            {
                                string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                                if (error == "Already Exists.")
                                {
                                    Session["IsSuccess"] = false;
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Testing Request already Send.');", true);

                                }
                                else
                                {
                                    Session["IsSuccess"] = false;
                                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + error);
                                }
                            }
                        }
                    }
                }
                else if (btnSave.Text == "Update")
                {
                    ds = objdb.ByProcedure("Usp_Production_VitaminRequest"
                                     , new string[] {"flag",
                                                     "VitaminRequest_ID",          
                                                     "Shift_ID", 
                                                     "ProductSection_ID", 
                                                     "RequestType", 
                                                     "RequestFor", 
                                                     "RequestFor_ID", 
                                                     "ItemType_id", 
                                                     "MilkQuantity",                                                                                                        
                                                     "Request_Remark", 
                                                     "CreatedBy", 
                                                     "CreatedBy_IP"
                                                    }
                                    , new string[]  {"7" ,
                                                     ViewState["VitaminRequest_ID"].ToString(),
                                                     ddlShift.SelectedValue,
                                                     ddlPSection.SelectedValue,
                                                     ddlTestRequestType.SelectedValue,
                                                     ddlmilktestrequestfor.SelectedItem.Text,
                                                     ddlmilktestrequestfor.SelectedValue,
                                                     ddlVariant.SelectedValue,
                                                     txtMilkQty.Text,                                                    
                                                     txtRemarks_R.Text,
                                                     objdb.createdBy(),
                                                     objdb.GetLocalIPAddress()
                                                    }
                                    , "dataset");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {

                                Session["IsSuccess"] = true;
                                Response.Redirect("VitaminRequestToQC.aspx", false);


                            }
                            else
                            {
                                string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();

                                if (error == "Already Exists.")
                                {
                                    Session["IsSuccess"] = false;
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Testing Request already Send.');", true);

                                }
                                else
                                {
                                    Session["IsSuccess"] = false;
                                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + error);
                                }
                            }
                        }
                    }
                }
                
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {

            lblMsg.Text = "";
            string FileName = Session["Office_Name"].ToString() + "_" + "VitaminRequestDetails_";
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=" + FileName + DateTime.Now + ".xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            GridView1.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());

            Response.End();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
     
    #endregion



    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string VitaminRequest_ID = e.CommandArgument.ToString();
        ViewState["VitaminRequest_ID"] = VitaminRequest_ID.ToString();
        DataSet dsRecord = objdb.ByProcedure("Usp_Production_VitaminRequest", new string[] { "flag", "VitaminRequest_ID" }, new string[] { "6", VitaminRequest_ID }, "dataset");
        if(dsRecord != null && dsRecord.Tables.Count > 0)
        {
            if(dsRecord.Tables[0].Rows.Count > 0)
            {
                txtDate.Text = dsRecord.Tables[0].Rows[0]["VitaminRequest_DT"].ToString();

                ddlShift.ClearSelection();
                ddlShift.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Shift_ID"].ToString()).Selected = true;

                ddlPSection.ClearSelection();
                ddlPSection.Items.FindByValue(dsRecord.Tables[0].Rows[0]["ProductSection_ID"].ToString()).Selected = true;

                ddlPSection_SelectedIndexChanged(sender, e);
                ddlTestRequestType.ClearSelection();
                ddlTestRequestType.Items.FindByText(dsRecord.Tables[0].Rows[0]["RequestType"].ToString()).Selected = true;


                ddlTestRequestType_SelectedIndexChanged(sender, e);

                ddlmilktestrequestfor.ClearSelection();
                ddlmilktestrequestfor.Items.FindByValue(dsRecord.Tables[0].Rows[0]["RequestFor_ID"].ToString()).Selected = true;

                ddlVariant.ClearSelection();
                ddlVariant.Items.FindByValue(dsRecord.Tables[0].Rows[0]["ItemType_id"].ToString()).Selected = true;

                txtMilkQty.Text = dsRecord.Tables[0].Rows[0]["MilkQuantity"].ToString();
                txtRemarks_R.Text = dsRecord.Tables[0].Rows[0]["Request_Remark"].ToString();

                btnSave.Text = "Update";
            }
        }
    }
}