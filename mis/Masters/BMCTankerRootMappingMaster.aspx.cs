using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_Masters_BMCTankerRootMappingMaster : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    APIProcedure objdb1 = new APIProcedure();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            lblMsg.Text = "";

            if (!Page.IsPostBack)
            {
                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }

                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                GetAllBMCRootWise();
                FillOffice();
                FillBMCRoot();
                GetCCDetails();
                //FillBMC();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            objdb.redirectToHome();
        }

    }

    private void GetAllBMCRootWise()
    {

        try
        {
            if (ddlBMCTankerRootName.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
                          new string[] { "flag", "Office_Parant_ID" },
                          new string[] { "8", objdb.Office_ID() }, "dataset");

                if (ds.Tables[0].Rows.Count != 0)
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    
    
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }

    public void FillOffice()
    {

        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
                      new string[] { "flag", "OfficeType_ID" },
                      new string[] { "0", objdb1.OfficeType_ID() }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddldsname.DataTextField = "Office_Name";
                ddldsname.DataValueField = "Office_ID";
                ddldsname.DataSource = ds;
                ddldsname.DataBind();
                ddldsname.SelectedValue = objdb1.Office_ID();
                ddldsname.Enabled = false;
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
                      new string[] { "flag", "Office_ID", "OfficeType_ID" },
                      new string[] { "11",objdb.Office_ID(),objdb.OfficeType_ID() }, "dataset");

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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
                        ddlccbmcdetail.DataTextField = "Office_Name";
                        ddlccbmcdetail.DataValueField = "Office_ID";


                        ddlccbmcdetail.DataSource = ds;
                        ddlccbmcdetail.DataBind();

                        if (objdb.OfficeType_ID() == "2")
                        {
                            ddlccbmcdetail.Items.Insert(0, new ListItem("Select", "0"));
                        }
                        else
                        {
                            ddlccbmcdetail.SelectedValue = objdb.Office_ID();
                            //GetMCUDetails();
                            ddlccbmcdetail.Enabled = false;
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

    protected void ddlccbmcdetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           // GetMCUDetails();
		   FillBMC();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void ddlmcudetails_Init(object sender, EventArgs e)
    {
        try
        {
            ddlmcudetails.Items.Add(new ListItem("Select", "0"));
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

            string code = ddlMilkCollectionUnit.SelectedValue;
            ds = null;
            ds = objdb.ByProcedure("SpAdminOffice",
                                        new string[] { "flag", "OfficeType_ID", "Office_Parant_ID" },
                                        new string[] { "33", code, ddlccbmcdetail.SelectedValue }, "TableSave");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlmcudetails.DataTextField = "Office_Name";
                    ddlmcudetails.DataValueField = "Office_ID";
                    ddlmcudetails.DataSource = ds;
                    ddlmcudetails.DataBind();
                    ddlmcudetails.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally { if (ds != null) { ds.Dispose(); } }

    }
    //protected void GetMCUDetails()
    //{
    //    try
    //    {

    //        ds = null;

    //        ds = objdb.ByProcedure("USP_Trn_MilkInwardOutwardReferenceDetails_MCU",
    //                 new string[] { "flag", "AttachedToCC" },
    //                 new string[] { "303", ddlccbmcdetail.SelectedValue }, "dataset");

    //        if (ds != null)
    //        {
    //            if (ds.Tables.Count > 0)
    //            {
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    ddlmcudetails.DataTextField = "Office_Name";
    //                    ddlmcudetails.DataValueField = "Office_ID";
    //                    ddlmcudetails.DataSource = ds;
    //                    ddlmcudetails.DataBind();
    //                    ddlmcudetails.Items.Insert(0, new ListItem("Select", "0"));

    //                }
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}

    protected void btnAddcc_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        AddMCUDetails();
    }

    private void AddMCUDetails()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
                           new string[] { "flag", "Office_ID" },
                           new string[] { "7", ddlmcudetails.SelectedValue }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Office of \"" + ddlmcudetails.SelectedItem.Text + "\" already exist in Root Name " + ds.Tables[0].Rows[0]["BMCTankerRootName"].ToString());
                return;
            }

            else
            {

                int CompartmentType = 0;
                if(txtDistanceInKm.Text == "")
				{
					txtDistanceInKm.Text = "0";
				}
                if (Convert.ToString(ViewState["InsertRecord"]) == null || Convert.ToString(ViewState["InsertRecord"]) == "")
                {
                    DataTable dt = new DataTable();
                    DataRow dr;
                    dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                    dt.Columns.Add(new DataColumn("I_OfficeID", typeof(int)));
                    dt.Columns.Add(new DataColumn("Office_Name", typeof(string)));
                    dt.Columns.Add(new DataColumn("DistanceInKm", typeof(int)));
                    dt.Columns.Add(new DataColumn("ArrivalDateTime", typeof(string)));
                    dt.Columns.Add(new DataColumn("DispatchDateTime", typeof(string)));
                    dr = dt.NewRow();
                    dr[0] = 1;
                    dr[1] = ddlmcudetails.SelectedValue;
                    dr[2] = ddlmcudetails.SelectedItem.Text;
                    dr[3] = txtDistanceInKm.Text;
                    dr[4] = txtArrivalTime.Text;
                    dr[5] = txtDispatchTime.Text;
                    dt.Rows.Add(dr);
                    ViewState["InsertRecord"] = dt;
                    gv_CCDetails.DataSource = dt;
                    gv_CCDetails.DataBind();

                }
                else
                {
                    DataTable dt = new DataTable();
                    DataTable DT = new DataTable();
                    DataRow dr;
                    dt.Columns.Add(new DataColumn("S.No", typeof(int)));
                    dt.Columns.Add(new DataColumn("I_OfficeID", typeof(int)));
                    dt.Columns.Add(new DataColumn("Office_Name", typeof(string)));
                    dt.Columns.Add(new DataColumn("DistanceInKm", typeof(int)));
                    dt.Columns.Add(new DataColumn("ArrivalDateTime", typeof(string)));
                    dt.Columns.Add(new DataColumn("DispatchDateTime", typeof(string)));

                    DT = (DataTable)ViewState["InsertRecord"];

                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        if (ddlmcudetails.SelectedValue == DT.Rows[i]["I_OfficeID"].ToString())
                        {
                            CompartmentType = 1;
                        }
                    }

                    if (CompartmentType == 1)
                    {
                        lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Office of \"" + ddlmcudetails.SelectedItem.Text + "\" already exist.");
                    }
                    else
                    {
                        dr = dt.NewRow();
                        dr[0] = 1;
                        dr[1] = ddlmcudetails.SelectedValue;
                        dr[2] = ddlmcudetails.SelectedItem.Text;
                        dr[3] = txtDistanceInKm.Text;
                        dr[4] = txtArrivalTime.Text;
                        dr[5] = txtDispatchTime.Text;

                        dt.Rows.Add(dr);
                    }

                    foreach (DataRow tr in DT.Rows)
                    {
                        dt.Rows.Add(tr.ItemArray);
                    }
                    ViewState["InsertRecord"] = dt;
                    gv_CCDetails.DataSource = dt;
                    gv_CCDetails.DataBind();
                }

                ddlccbmcdetail.Enabled = false;
                ddlmcudetails.ClearSelection();
                txtDistanceInKm.Text = "";

            }

        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }

    }

    protected void lnkDeleteCC_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridViewRow row1 = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt3 = ViewState["InsertRecord"] as DataTable;
            dt3.Rows.Remove(dt3.Rows[row1.RowIndex]);
            ViewState["InsertRecord"] = dt3;

            gv_CCDetails.DataSource = dt3;
            gv_CCDetails.DataBind();

            ddlmcudetails.ClearSelection();
            txtDistanceInKm.Text = "";

            DataTable dtdeletecc = ViewState["InsertRecord"] as DataTable;

            if (dtdeletecc.Rows.Count == 0)
            {
                ddlccbmcdetail.Enabled = true;
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

    private DataTable GetCCGridvalue()
    {
        DataTable dtcc = new DataTable();
        DataRow drcc;

        dtcc.Columns.Add(new DataColumn("I_OfficeID", typeof(int)));
        dtcc.Columns.Add(new DataColumn("DistanceInKm", typeof(int)));
        dtcc.Columns.Add(new DataColumn("ArrivalDateTime", typeof(string)));
        dtcc.Columns.Add(new DataColumn("DispatchDateTime", typeof(string)));

        foreach (GridViewRow rowcc in gv_CCDetails.Rows)
        {

            Label lblI_OfficeID = (Label)rowcc.FindControl("lblI_OfficeID");
            Label lblDistanceInKm = (Label)rowcc.FindControl("lblDistanceInKm");
            Label lblArrivalDateTime = (Label)rowcc.FindControl("lblArrivalDateTime");
            Label lblDispatchDateTime = (Label)rowcc.FindControl("lblDispatchDateTime");


            drcc = dtcc.NewRow();
            drcc[0] = lblI_OfficeID.Text;
            drcc[1] = lblDistanceInKm.Text;
            drcc[2] = lblArrivalDateTime.Text;
            drcc[3] = lblDispatchDateTime.Text;
            dtcc.Rows.Add(drcc);
        }
        return dtcc;
    }

    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
		    if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
			{
				 //Milk Sample Details
            DataTable dtccF = new DataTable();

            dtccF = GetCCGridvalue();

            if (dtccF.Rows.Count > 0)
            {
                ds = null;

                ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
                                              new string[] { "Flag",
                                                 "BMCTankerRoot_Id"
                                                ,"Office_Parant_ID"
                                                ,"OfficeType_ID" 
                                                ,"IsActive"  
                                                ,"CreatedBy"
                                                ,"CreatedByIP"
                                                ,"AttachedToCC"
                                    },
                                              new string[] { "4" ,
                                                ddlBMCTankerRootName.SelectedValue,
                                                objdb.Office_ID(),
                                                objdb.OfficeType_ID(), 
                                                "1",   
                                                objdb.createdBy(),
                                                objdb.GetLocalIPAddress(),
                                                ddlccbmcdetail.SelectedValue
                                                 
                                    },
                                             new string[] { "type_Mst_BMCTankerRootMapping" },
                                             new DataTable[] { dtccF }, "TableSave");



                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                {
                    Session["IsSuccess"] = true;
                    Response.Redirect("BMCTankerRootMappingMaster.aspx", false);

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                    Session["IsSuccess"] = false;
                }

                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

            }

            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "First To Fill At Least One BMC Details");
				
            }
			 FillOffice();
            FillBMCRoot();
            GetCCDetails();
			}
           

           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }


    protected void ddlBMCTankerRootName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBMCTankerRootName.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
                          new string[] { "flag", "BMCTankerRoot_Id", "Office_Parant_ID" },
                          new string[] { "2", ddlBMCTankerRootName.SelectedValue, objdb.Office_ID() }, "dataset");

                if (ds.Tables[0].Rows.Count != 0)
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }

    protected void lblremovebmc_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lblremovebmc = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lblremovebmc");

            objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
                       new string[] { "flag", "BMCTankerRootMapping_Id", "Office_Parant_ID" },
                       new string[] { "3", lblremovebmc.CommandArgument, objdb.Office_ID() }, "dataset");

            ddlBMCTankerRootName_SelectedIndexChanged(sender, e);

            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank you!", "Record Successfully Deleted");

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }
    protected void ddlMilkCollectionUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillBMC();
    }
}