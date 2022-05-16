using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;

public partial class mis_MilkCollection_RMRD_Challan_EntryFromCanes : System.Web.UI.Page
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
                if (Session["IsSuccess"] != null)
                {
                    if ((Boolean)Session["IsSuccess"] == true)
                    {
                        Session["IsSuccess"] = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Record Inserted Successfully');", true);
                    }
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDate.Attributes.Add("readonly", "readonly");
                FillBMCRoot();
                GetCCDetails();
                
                //ddlccbmcdetail_SelectedIndexChanged(sender, e);
				  if(objdb.OfficeType_ID() == "2")
                {
                   
                }
				else
                {
                    FillSociety();
                }
                txtfilterdate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                FillGrid();

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

    #region Init Event
    protected void rfvShift_Init(object sender, EventArgs e)
    {
        try
        {
            DataSet dsct = objdb.ByProcedure("USP_GetServerDatetime",
                             new string[] { "flag" },
                             new string[] { "1" }, "dataset");

            string currrentime = dsct.Tables[0].Rows[0]["currentTime"].ToString();

            string[] s = currrentime.Split(':');

            if ((Convert.ToInt32(s[0]) >= 0 && Convert.ToInt32(s[0]) <= 12 && ((Convert.ToInt32(s[0]) == 0 && Convert.ToInt32(s[1]) < 1) || (Convert.ToInt32(s[0]) < 12 && Convert.ToInt32(s[1]) <= 59))))
            {
                ddlShift.SelectedValue = "Morning";
            }
            else
            {
                ddlShift.SelectedValue = "Evening";
            }

            //txtDate.Attributes.Add("readonly", "readonly");
            //txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            //ddlShift.Enabled = false;


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlMilkQuality_Init(object sender, EventArgs e)
    {
        ddlMilkQuality.DataSource = objdb.ByProcedure("USP_Mst_MilkQualityList",
                                new string[] { "flag" },
                                new string[] { "1" }, "dataset");
        ddlMilkQuality.DataValueField = "V_MilkQualityList";
        ddlMilkQuality.DataTextField = "V_MilkQualityList";
        ddlMilkQuality.DataBind();
        ddlMilkQuality.Items.Insert(0, new ListItem("Select", "0"));
        ddlMilkQuality.SelectedValue = "Good";

    }
    #endregion

    #region User Defined Function
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
                ddlBMCTankerRootName.Items.Insert(0, new ListItem("select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }

    }
    private void AddSampleDetails()
    {
        try
        {
            //int CompartmentType = 0;

            if (Convert.ToString(ViewState["InsertRecord"]) == null || Convert.ToString(ViewState["InsertRecord"]) == "")
            {
                DataTable dt = new DataTable();
                DataRow dr;
                DataColumn RowNo = dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add(new DataColumn("MilkType", typeof(string)));
                dt.Columns.Add(new DataColumn("Quantity", typeof(decimal)));
                dt.Columns.Add(new DataColumn("Quality", typeof(string)));
                dt.Columns.Add(new DataColumn("TotalCan", typeof(string)));
                dt.Columns.Add(new DataColumn("GoodCan", typeof(string)));
                RowNo.AutoIncrement = true;
                RowNo.AutoIncrementSeed = 1;
                RowNo.AutoIncrementStep = 1;
                dr = dt.NewRow();

                dr[1] = ddlMilkType.SelectedItem.Text;
                dr[2] = txtMilkQuantity.Text;
                dr[3] = ddlMilkQuality.SelectedItem.Text; ;
                dr[4] = txtTotalCan.Text;

                dr[5] = txtGoodCan.Text;

                dt.Rows.Add(dr);
                ViewState["InsertRecord"] = dt;
                gv_SampleDetails.DataSource = dt;
                gv_SampleDetails.DataBind();


            }
            else
            {
                DataTable dt = new DataTable();
                DataTable DT = new DataTable();
                DataRow dr;
                DataColumn RowNo = dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add(new DataColumn("MilkType", typeof(string)));
                dt.Columns.Add(new DataColumn("Quantity", typeof(decimal)));
                dt.Columns.Add(new DataColumn("Quality", typeof(string)));
                dt.Columns.Add(new DataColumn("TotalCan", typeof(int)));
                dt.Columns.Add(new DataColumn("GoodCan", typeof(int)));
                RowNo.AutoIncrement = true;
                RowNo.AutoIncrementSeed = 1;
                RowNo.AutoIncrementStep = 1;
                dt = (DataTable)ViewState["InsertRecord"];

                dr = dt.NewRow();

                dr[1] = ddlMilkType.SelectedItem.Text;
                dr[2] = txtMilkQuantity.Text;
                dr[3] = ddlMilkQuality.SelectedItem.Text; ;
                dr[4] = txtTotalCan.Text;

                dr[5] = txtGoodCan.Text;
                dt.Rows.Add(dr);


                ViewState["InsertRecord"] = dt;
                gv_SampleDetails.DataSource = dt;
                gv_SampleDetails.DataBind();

            }

            txtMilkQuantity.Text = "";
            txtTotalCan.Text = "";
            txtGoodCan.Text = "";



        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }

    }
    private DataTable GetSampleDetails()
    {

        DataTable dt = new DataTable();
        DataRow dr;

        dt.Columns.Add(new DataColumn("MilkType", typeof(string)));

        dt.Columns.Add(new DataColumn("TotalCan", typeof(int)));
        dt.Columns.Add(new DataColumn("GoodCan", typeof(int)));
        dt.Columns.Add(new DataColumn("Quantity", typeof(decimal)));
        dt.Columns.Add(new DataColumn("V_MilkQuality", typeof(string)));

        foreach (GridViewRow row in gv_SampleDetails.Rows)
        {

            Label lblMilkType = (Label)row.FindControl("lblMilkType");
            Label lblQuantity = (Label)row.FindControl("lblQuantity");
            Label lblTotalCan = (Label)row.FindControl("lblTotalCan");
            Label lblGoodCan = (Label)row.FindControl("lblGoodCan");
            Label lblQuality = (Label)row.FindControl("lblQuality");
            dr = dt.NewRow();
            dr[0] = lblMilkType.Text;

            dr[1] = lblTotalCan.Text;
            if (lblGoodCan.Text == "")
            {
                dr[2] = "0";
            }
            else
            {
                dr[2] = lblGoodCan.Text;
            }
            dr[3] = lblQuantity.Text;
            dr[4] = lblQuality.Text;
            dt.Rows.Add(dr);


        }

        return dt;
    }
    protected void FillGrid()
    {
        try
        {
            gvEntryList.DataSource = string.Empty;
            gvEntryList.DataBind();
            ds = objdb.ByProcedure("Usp_Trn_MilkInwardReferenceDetails_RMRDViaCanes", new string[] { "flag", "D_Date", "I_OfficeID" }, new string[] { "2", Convert.ToDateTime(txtfilterdate.Text, cult).ToString("yyyy/MM/dd"), objdb.Office_ID() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gvEntryList.DataSource = ds;
                gvEntryList.DataBind();
                GetDatatableHeaderDesign();
                GetDatatableFooterDesign();
            }
            else
            {
                gvEntryList.DataSource = string.Empty;
                gvEntryList.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    protected void FillSociety()
    {
        try
        {
			ddlSociety.Items.Clear();
            if (ddlccbmcdetail.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("Usp_Trn_MilkInwardReferenceDetails_RMRDViaCanes",
                                  new string[] { "flag", "Supplyunitparant_ID" },
                                  new string[] { "16",ddlccbmcdetail.SelectedValue }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSociety.DataTextField = "Office_Name";
                        ddlSociety.DataValueField = "Office_ID";
                        ddlSociety.DataSource = ds.Tables[0];
                        ddlSociety.DataBind();
                        //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                    else
                    {
                        //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
                else
                {
                    //ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                Response.Redirect("RMRD_Challan_EntryFromCanes.aspx", false);
            }
			ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
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
                            
                            ddlccbmcdetail.Enabled = false;
                           // FillSociety();
                        }


                    }
                }
                ddlccbmcdetail.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion

    #region changed event
    protected void ddlBMCTankerRootName_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            //ddlBMC.Items.Clear();
            //lblMsg.Text = "";
            //ds = null;
            //if (ddlBMCTankerRootName.SelectedIndex > 0)
            //{

            //    ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
            //               new string[] { "flag", "BMCTankerRoot_Id", "Office_ID", "OfficeType_ID" },
            //               new string[] { "5", ddlBMCTankerRootName.SelectedValue, objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

            //    if (ds != null && ds.Tables[0].Rows.Count > 0)
            //    {
            //        ddlBMC.DataTextField = "Office_Name";
            //        ddlBMC.DataValueField = "I_OfficeID";
            //        ddlBMC.DataSource = ds;
            //        ddlBMC.DataBind();
            //        ddlBMC.Items.Insert(0, new ListItem("Select", "0"));

            //    }
            //    else
            //    {
            //        ddlBMC.Items.Insert(0, new ListItem("Select", "0"));
            //    }

            //}
            //else
            //{
            //    ddlBMC.Items.Insert(0, new ListItem("Select", "0"));

            //}


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
        }
    }

   

    protected void txtfilterdate_TextChanged(object sender, EventArgs e)
    {
        FillGrid();
    }
    #endregion

    #region Button Event
    protected void btnAddSocietyDetails_Click(object sender, EventArgs e)
    {
        try
        {
            AddSampleDetails();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }

    }
	
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                lblMsg.Text = "";
                DataTable dt2 = new DataTable();
                dt2 = GetSampleDetails();
                if (dt2.Rows.Count > 0)
                {
                    ds = objdb.ByProcedure("Usp_Trn_MilkInwardReferenceDetails_RMRDViaCanes",
                                            new string[] 
                            {"flag"
                             ,"BMCTankerRoot_Id"
                            ,"CC_Id"
                             ,"SocietyID"
                             ,"D_Date"
                             ,"Shift"
                             ,"V_EntryType"
                             ,"I_OfficeID"
                             ,"I_OfficeTypeID"
                             ,"I_CreatedBy"
                            },
                                            new string[] 
                            {"1"
                             ,ddlBMCTankerRootName.SelectedValue
                             ,ddlccbmcdetail.SelectedValue
                             ,ddlSociety.SelectedValue
                             ,Convert.ToDateTime(txtDate.Text,cult).ToString("yyyy/MM/dd")
                             , ddlShift.SelectedValue
                             ,"In"
                             , objdb.Office_ID()
                             , objdb.OfficeType_ID()
                             ,objdb.createdBy()
                            },
                                          new string[] { 
                                  "type_Trn_MilkSampleDetails_RMRDViaCanes"                                            
                              },
                                            new DataTable[] {
                                  dt2
                                
                              },
                                          "TableSave");
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {

                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Success!", "" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString() + "");
                                FillGrid();
								ViewState["InsertRecord"] = null;
                                gv_SampleDetails.DataSource = new string[] { };
                                gv_SampleDetails.DataBind();
								
                                //ddlBMCTankerRootName.ClearSelection();
                                //ddlBMCTankerRootName_SelectedIndexChanged(sender, e);
                                //ddlMilkCollectionUnit.ClearSelection();
                                //ddlMilkCollectionUnit_SelectedIndexChanged(sender, e);
								ddlSociety.ClearSelection();


                            }
                            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok Exists")
                            {
                                //Session["IsSuccess"] = true;
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString() + "");

                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                                //Session["IsSuccess"] = false;
                            }
                        }
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Oops!", "Please Enter Milk Collection Detail");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    #endregion


    private void GetDatatableFooterDesign()
    {
        try
        {
            if (gvEntryList.Rows.Count > 0)
            {
                gvEntryList.FooterRow.TableSection = TableRowSection.TableFooter;
                //gv_MilkCollectionChallanEntryDetails.foo = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (gvEntryList.Rows.Count > 0)
            {
                gvEntryList.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvEntryList.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    protected void gvEntryList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string BI_MilkInRMRDCanRefID = e.CommandArgument.ToString();
            if (e.CommandName == "DeleteRecord")
            {
                ds = objdb.ByProcedure("Usp_Trn_MilkInwardReferenceDetails_RMRDViaCanes", new string[] { "flag", "BI_MilkInRMRDCanRefID" }, new string[] { "12", BI_MilkInRMRDCanRefID }, "dataset");
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Success!", "" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString() + "");
                            FillGrid();
                        }

                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error -" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString());
                            //Session["IsSuccess"] = false;
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }

    }
    protected void ddlccbmcdetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSociety();
    }
}
