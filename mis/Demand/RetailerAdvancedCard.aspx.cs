using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Globalization;


public partial class mis_Demand_RetailerAdvancedCard : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3,ds4,ds5 = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);
    string currentdate = "", currrentime = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                GetLocation();
                GetSearchItemCategory();
                GetSearchLocation();
                SetEffectiveDate();
                //GetItemMappingDetails();

               
            }
            if (ddlShift.SelectedValue != "0" && ddlRoute.SelectedValue != "0" && ddlItemCategory.SelectedValue != "0" && ddlShift.SelectedValue != "0")
            {
                GetItemListByRetailer();
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

    private void GetDatatableHeaderDesign()
    {
        try
        {
            if (GridView2.Rows.Count > 0)
            {
                GridView2.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView2.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }

    #region==========Item Citizen Mapping code========================

    #region=======================user defined function========================    
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
            ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    private void GetRoute()
    {
        try
        {
            ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                 new string[] { "7", objdb.Office_ID(), ddlLocation.SelectedValue, ddlItemCategory.SelectedValue }, "dataset");
            ddlRoute.DataTextField = "RName";
            ddlRoute.DataValueField = "RouteId";
            ddlRoute.DataBind();
            ddlRoute.Items.Insert(0, new ListItem("Select", "0"));



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetSearchLocation()
    {
        try
        {
            ddlSearchLocation.DataTextField = "AreaName";
            ddlSearchLocation.DataValueField = "AreaId";
            ddlSearchLocation.DataSource = objdb.ByProcedure("USP_Mst_Area",
                 new string[] { "flag" },
                   new string[] { "1" }, "dataset");
            ddlSearchLocation.DataBind();
            ddlSearchLocation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 2 ", ex.Message.ToString());
        }
    }
    private void GetSearchRoute()
    {
        try
        {
            ddlSearchRoute.DataSource = objdb.ByProcedure("USP_Mst_DistributorRouteMapping",
                 new string[] { "flag", "Office_ID", "AreaId", "ItemCat_id" },
                 new string[] { "7", objdb.Office_ID(), ddlSearchLocation.SelectedValue, ddlSearchItemCategory.SelectedValue }, "dataset");
            ddlSearchRoute.DataTextField = "RName";
            ddlSearchRoute.DataValueField = "RouteId";
            ddlSearchRoute.DataBind();
            ddlSearchRoute.Items.Insert(0, new ListItem("All", "0"));



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GetSearchItemCategory()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Trn_RetailerAdvanceCard",
                  new string[] { "flag" },
                 new string[] { "1" }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                ddlSearchItemCategory.DataTextField = "ItemCatName";
                ddlSearchItemCategory.DataValueField = "ItemCat_id";
                ddlSearchItemCategory.DataSource = ds1.Tables[0];
                ddlSearchItemCategory.DataBind();
                ddlSearchItemCategory.Items.Insert(0, new ListItem("Select", "0"));
				ddlItemCategory.SelectedValue = "3";

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    //private void GetRoute()
    //{
    //    try
    //    {

    //        ddlRoute.Items.Clear();
    //        ddlRoute.DataSource = objdb.ByProcedure("USP_Mst_Route",
    //                 new string[] { "flag", "Office_ID", "AreaId" },
    //                 new string[] { "6", objdb.Office_ID(), ddlLocation.SelectedValue }, "dataset");
    //        ddlRoute.DataTextField = "RName";
    //        ddlRoute.DataValueField = "RouteId";
    //        ddlRoute.DataBind();
    //        ddlRoute.Items.Insert(0, new ListItem("Select", "0"));

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 3: ", ex.Message.ToString());
    //    }
    //}
    //private void GetRetailer()
    //{
    //    try
    //    {
           
    //        ds1 = objdb.ByProcedure("USP_Mst_BoothReg",
    //                 new string[] { "flag", "RouteId" },
    //                   new string[] { "12", ddlRoute.SelectedValue }, "dataset");

    //        if (ds1.Tables[0].Rows.Count != 0)
    //        {
    //            ddlRetailer.DataTextField = "BoothName";
    //            ddlRetailer.DataValueField = "BoothId";
    //            ddlRetailer.DataSource = ds1.Tables[0];
    //            ddlRetailer.DataBind();
    //            ddlRetailer.Items.Insert(0, new ListItem("Select", "0"));
    //        }
    //        else
    //        {
    //            ddlRetailer.Items.Insert(0, new ListItem("Select", "0"));
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds1 != null) { ds1.Dispose(); }
    //    }
    //}
    private void SetEffectiveDate()
    {
        try
        {

            ds3 = objdb.ByProcedure("USP_GetServerDatetime",
                    new string[] { "flag" },
                      new string[] { "1" }, "dataset");
            if (ds3.Tables[0].Rows.Count > 0)
            {

                currentdate = ds3.Tables[0].Rows[0]["currentDate"].ToString();
                DateTime cdate = DateTime.ParseExact(currentdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string nextmonthdate = cdate.AddMonths(1).ToString("dd/MM/yyyy");
                string[] cmd = currentdate.Split('/');
                string[] nmd = nextmonthdate.Split('/');
                string effectfivefdate = "16" + "/" + cmd[1] + "/" + cmd[2];
                string effectfivetdate = "15" + "/" + nmd[1] + "/" + nmd[2];
                txtEffectiveFromDate.Text = effectfivefdate.ToString();
                txtEffectiveToDate.Text = effectfivetdate.ToString();
                //txtEffectiveFromDate.Attributes.Add("disabled", "disabled");
                //txtEffectiveToDate.Attributes.Add("disabled", "disabled");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error Set Effective FromDate and ToDate: ", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }
    private void GetCompareDate()
    {
        try
        {
            string myStringfromdat = txtEffectiveFromDate.Text;
            DateTime fdate = DateTime.ParseExact(myStringfromdat, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string myStringtodate = txtEffectiveToDate.Text;
            DateTime tdate = DateTime.ParseExact(myStringtodate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (fdate <= tdate)
            {
                lblMsg.Text = string.Empty;
            }
            else
            {
                txtEffectiveToDate.Text = string.Empty;
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "ToDate must less than FromDate ");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error D: ", ex.Message.ToString());
        }
    }
    protected void GetItemCategory()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Trn_RetailerAdvanceCard",
                  new string[] { "flag" },
                 new string[] { "1" }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                ddlItemCategory.DataTextField = "ItemCatName";
                ddlItemCategory.DataValueField = "ItemCat_id";
                ddlItemCategory.DataSource = ds1.Tables[0];
                ddlItemCategory.DataBind();
                ddlItemCategory.Items.Insert(0, new ListItem("Select", "0"));

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void GetItemListByRetailer()
    {
        try
        {
            lblMsg.Text = string.Empty;
            ds1 = objdb.ByProcedure("USP_Trn_RetailerAdvanceCard",
                       new string[] { "flag", "ItemCat_id", "RouteId", "Office_ID", "AreaId"},
                       new string[] { "2", ddlItemCategory.SelectedValue,ddlRoute.SelectedValue, objdb.Office_ID(),ddlLocation.SelectedValue }, "dataset");
             if (ds1.Tables[0].Rows.Count != 0)
            {
            if (ViewState["DynamicGridBind"] == null)
            {
                foreach (DataColumn column in ds1.Tables[0].Columns)
                {
                    TemplateField tfield = new TemplateField();
                    tfield.HeaderText = column.ColumnName;
                    GridView1.Columns.Add(tfield);
                }
            }
			}
            
            if (ds1.Tables[0].Rows.Count != 0)
            {
                GridView1.Visible = true;

                ViewState["RetailerData"] = ds1.Tables[0];
                ViewState["DynamicGridBind"] = "1";
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();
            }
            else
            {
                GridView1.Visible = false;
                ViewState["RetailerData"] = "";
                //GridView1.DataSource = null;
                //GridView1.DataBind();
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Sorry!", "No Record Found");
                ddlRoute.SelectedIndex = -1;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void GetShift()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Mst_ShiftMaster",
                     new string[] { "flag" },
                       new string[] { "1" }, "dataset");

            if (ds1.Tables[0].Rows.Count != 0)
            {
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataValueField = "Shift_id";
                ddlShift.DataSource = ds1.Tables[0];
                ddlShift.DataBind();
              //  ddlShift.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 1 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    private void GetItemMappingDetails()
    {
        try
        {
            lblMsg.Text = "";
            string fm = "";
            if(objdb.Office_ID()=="4")
            {
                fm = "11/" + txtMonth.Text;
            }
            else
            {
                 fm= "16/" + txtMonth.Text;
            }

            DateTime fmonth = DateTime.ParseExact(fm, "dd/MM/yyyy", culture);
            string fromnonth = fmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            DateTime addmonth = fmonth.AddMonths(1);
            DateTime minusday = addmonth.AddDays(-1);

            string tmnth = minusday.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime tmonth = DateTime.ParseExact(tmnth, "dd/MM/yyyy", culture);
            string tomonth = tmonth.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            string tm = tmonth.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            ds4 = objdb.ByProcedure("USP_Trn_RetailerAdvanceCard",
                         new string[] { "flag", "FromDate", "ToDate", "ItemCat_id", "AreaId", "RouteId", "Office_ID" },
                        new string[] { "6", fromnonth, tomonth, ddlSearchItemCategory.SelectedValue, ddlSearchLocation.SelectedValue, ddlSearchRoute.SelectedValue, objdb.Office_ID() }, "dataset");
            if(ds4.Tables[0].Rows.Count>0)
            {
                GridView2.DataSource = ds4.Tables[0];
                GridView2.DataBind();
                GetDatatableHeaderDesign();
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
            }
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 13 ", ex.Message.ToString());
        }
        finally
        {
            if (ds4 != null) { ds4.Dispose(); }
        }
    }
    private void InsertRetailerAdvanceCard()
    {

        try
        {
            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            {
                DateTime date3 = DateTime.ParseExact(txtEffectiveFromDate.Text, "dd/MM/yyyy", culture);
                DateTime date4 = DateTime.ParseExact(txtEffectiveToDate.Text, "dd/MM/yyyy", culture);
				
				// string fdate = date3.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                // string tdate = date4.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                string IPAddress = Request.ServerVariables["REMOTE_ADDR"];

                DataTable dt1 = (DataTable)ViewState["RetailerData"];
                ds2 = objdb.ByProcedure("USP_GetServerDatetime",
                   new string[] { "flag" },
                     new string[] { "1" }, "dataset");
                if (dt1 != null)
                {             
			 if (ds2.Tables[0].Rows.Count > 0 && dt1.Rows.Count > 0)
                {

                    currentdate = ds2.Tables[0].Rows[0]["currentDate"].ToString();
                    string[] im = currentdate.Split('/');

                    DataTable dtStatus = new DataTable();
                    DataRow dr;

                    dtStatus.Columns.Add("AreaId", typeof(int));
                    dtStatus.Columns.Add("RouteId", typeof(int));
                    dtStatus.Columns.Add("BoothId", typeof(int));
                    dtStatus.Columns.Add("ItemCat_id", typeof(int));
                    dtStatus.Columns.Add("Item_id", typeof(int));
                    dtStatus.Columns.Add("Total_ItemQty", typeof(int));
                    dtStatus.Columns.Add("Shift_id", typeof(int));
                    dtStatus.Columns.Add("EffectiveFromDate", typeof(string));
                    dtStatus.Columns.Add("EffectiveToDate", typeof(string));
                    dtStatus.Columns.Add("Office_ID", typeof(int));

                    dr = dtStatus.NewRow();

                    for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                    {
                        GridViewRow row1 = GridView1.Rows[i];


                        Label lblBoothId = (Label)row1.FindControl("lblBoothId");
                        Label lblRetailerTypeID = (Label)row1.FindControl("lblRetailerTypeID");

                        int j = 0;
                        int k = 3;


                        if ((row1.FindControl("chkSelect0") as CheckBox).Checked)
                        {
                            foreach (DataColumn column in dt1.Columns)
                            {

                                if (j > 2)
                                {
                                    string columnname = column.ColumnName;
                                    k = k + 1;


                                    string txtboxval = (row1.FindControl("txtboxid" + k) as TextBox).Text;

                                    if (txtboxval != "" && !string.IsNullOrEmpty(txtboxval))
                                    {

                                        ds5 = objdb.ByProcedure("USP_Trn_RetailerAdvanceCard",
                                                new string[] { "flag", "BoothId", "ItemCat_id", "ItemName","Shift_id",
                                                    "EffectiveFromDate","EffectiveToDate", "Office_ID" },

                                                new string[] { "7",lblBoothId.Text, ddlItemCategory.SelectedValue,columnname
                                                    ,ddlShift.SelectedValue,date3.ToString("yyyy/MM/dd"),date4.ToString("yyyy/MM/dd")
                                                    ,objdb.Office_ID() }, "TableSave");
                                        if (ds5.Tables[0].Rows.Count > 0)
                                        {
                                            if (ds5.Tables[0].Rows[0]["Msg"].ToString() == "Ok" && ds5.Tables[0].Rows[0]["Isexist"].ToString() == "0" && ds5.Tables[0].Rows[0]["Itemid"].ToString()!="0")
                                            {
                                                if (txtboxval != "0" && !string.IsNullOrEmpty(txtboxval) && txtboxval != "" && !string.IsNullOrEmpty(objdb.Office_ID()))
                                                {
                                                    dr[0] = ddlLocation.SelectedValue;
                                                    dr[1] = ddlRoute.SelectedValue;
                                                    dr[2] = lblBoothId.Text;
                                                    dr[3] = ddlItemCategory.SelectedValue;
                                                    dr[4] = ds5.Tables[0].Rows[0]["Itemid"].ToString();
                                                    dr[5] = txtboxval;
                                                    dr[6] = ddlShift.SelectedValue;
                                                    dr[7] = date3.ToString("yyyy/MM/dd");
                                                    dr[8] = date4.ToString("yyyy/MM/dd");
                                                    dr[9] = objdb.Office_ID();

                                                    dtStatus.Rows.Add(dr.ItemArray);
                                                }

                                            }
                                        }
                                    }


                                }
                                j = j + 1;


                            }
                        }

                    }
                    //if (Convert.ToInt16(im[0]) >= 1 && Convert.ToInt16(im[0]) <= 15)
                    //{
                    if (btnMSubmit.Text == "Save")
                    {
                        lblMsg.Text = "";
                        if (dtStatus.Rows.Count > 0)
                        {
                            ds1 = objdb.ByProcedure("USP_Trn_RetailerAdvanceCard",
                                                            new string[] { "flag", "CreatedBy", "CreatedByIP", "PlatformType" },
                                                            new string[] { "3", objdb.createdBy(), IPAddress, "1" },
                                                            new string[] { "type_USP_Trn_RetailerAdvanceCard" },
                                                            new DataTable[] { dtStatus }, "TableSave");

                            if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                            {
                                string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                //GetItemMappingDetails();
                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                                GridView1.Visible = false;
                                ViewState["RetailerData"] = null;
                                ddlRoute.SelectedIndex = 0;
                                ddlShift.SelectedIndex = 0;
                            }
                            else
                            {
                                string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);

                            }
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Duplicate entry exists.");
                        }

                    }
                    //}
                    //else
                    //{
                    //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Item Mapping of Citizen allowed between 10th to 15th of Every Month");
                    //}
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Date not Set.Record Save after sometime.");
                }
				}
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " At least one retailer data should be availbalbe");
                }
                if (dt1 != null) { dt1.Dispose(); }
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Select Location");
            }
            Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
            if (ds5 != null) { ds5.Dispose(); }
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    //private void InsertRetailerAdvanceCard()
    //{

    //    try
    //    {
    //        if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
    //        {
    //            DateTime date3 = DateTime.ParseExact(txtEffectiveFromDate.Text, "dd/MM/yyyy", culture);
    //            DateTime date4 = DateTime.ParseExact(txtEffectiveToDate.Text, "dd/MM/yyyy", culture);
    //            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];

    //            ds2 = objdb.ByProcedure("USP_GetServerDatetime",
    //               new string[] { "flag" },
    //                 new string[] { "1" }, "dataset");
    //            if (ds2.Tables[0].Rows.Count > 0)
    //            {

    //                currentdate = ds2.Tables[0].Rows[0]["currentDate"].ToString();
    //                string[] im = currentdate.Split('/');

    //                DataTable dtStatus = new DataTable();
    //                DataRow dr;

    //                dtStatus.Columns.Add("AreaId", typeof(int));
    //                dtStatus.Columns.Add("RouteId", typeof(int));
    //                dtStatus.Columns.Add("BoothId", typeof(int));
    //                dtStatus.Columns.Add("ItemCat_id", typeof(int));
    //                dtStatus.Columns.Add("Item_id", typeof(int));
    //                dtStatus.Columns.Add("Total_ItemQty", typeof(int));
    //                dtStatus.Columns.Add("Shift_id", typeof(int));
    //                dtStatus.Columns.Add("EffectiveFromDate", typeof(string));
    //                dtStatus.Columns.Add("EffectiveToDate", typeof(string));
    //                dtStatus.Columns.Add("Office_ID", typeof(int));

    //                dr = dtStatus.NewRow();

    //                foreach (GridViewRow row in GridView1.Rows)
    //                {
    //                    Label lblItemName = (Label)row.FindControl("ItemName");
    //                    Label lblItemid = (Label)row.FindControl("lblItemid");
    //                    TextBox txtItemQty = (TextBox)row.FindControl("txtItemQty");

    //                    ds5 = objdb.ByProcedure("USP_Trn_RetailerAdvanceCard",
    //                            new string[] { "flag", "BoothId", "ItemCat_id", "Item_id","Shift_id",
    //                            "EffectiveFromDate","EffectiveToDate", "Office_ID" },

    //                            new string[] { "7","", ddlItemCategory.SelectedValue,lblItemid.Text
    //                            ,ddlShift.SelectedValue,date3.ToString("yyyy/MM/dd"),date4.ToString("yyyy/MM/dd")
    //                            ,objdb.Office_ID() }, "TableSave");

    //                    if(ds5.Tables[0].Rows.Count > 0)
    //                    {
    //                        if (ds5.Tables[0].Rows[0]["Msg"].ToString() == "Ok" && ds5.Tables[0].Rows[0]["Isexist"].ToString() == "0")
    //                        {
    //                            if (txtItemQty.Text != "0" && !string.IsNullOrEmpty(txtItemQty.Text) && txtItemQty.Text != "" && !string.IsNullOrEmpty(objdb.Office_ID()))
    //                            {
    //                                dr[0] = ddlLocation.SelectedValue;
    //                                dr[1] = ddlRoute.SelectedValue;
    //                                dr[2] = "";
    //                                dr[3] = ddlItemCategory.SelectedValue;
    //                                dr[4] = lblItemid.Text;
    //                                dr[5] = txtItemQty.Text;
    //                                dr[6] = ddlShift.SelectedValue;
    //                                dr[7] = date3.ToString("yyyy/MM/dd");
    //                                dr[8] = date4.ToString("yyyy/MM/dd");
    //                                dr[9] = objdb.Office_ID();

    //                                dtStatus.Rows.Add(dr.ItemArray);
    //                            }
                               
    //                        }
    //                    }
    //                }

    //                //if (Convert.ToInt16(im[0]) >= 1 && Convert.ToInt16(im[0]) <= 15)
    //                //{
    //                    if (btnMSubmit.Text == "Save")
    //                    {
    //                        lblMsg.Text = "";
    //                        if (dtStatus.Rows.Count>0)
    //                        {
    //                            ds1 = objdb.ByProcedure("USP_Trn_RetailerAdvanceCard",
    //                                                            new string[] { "flag","CreatedBy", "CreatedByIP","PlatformType" },
    //                                                            new string[] { "3", objdb.createdBy(),  IPAddress,"1" },
    //                                                            new string[] { "type_USP_Trn_RetailerAdvanceCard" },
    //                                                            new DataTable[] { dtStatus }, "TableSave");

    //                            if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
    //                            {
    //                                string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //                                GetItemMappingDetails();
    //                                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
    //                                GetDatatableHeaderDesign();
    //                            }
    //                            else
    //                            {
    //                                string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                                   
    //                            }
    //                        }
    //                        else
    //                        {
    //                            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Enter at least one item quantity or Duplicate entry exists.");
    //                            GetDatatableHeaderDesign();
    //                        }
                            
    //                    }
    //                //}
    //                //else
    //                //{
    //                //    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Item Mapping of Citizen allowed between 10th to 15th of Every Month");
    //                //}
    //            }
    //            else
    //            {
    //                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Date not Set.Record Save after sometime.");
    //            }
    //        }
    //        else
    //        {
    //            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Select Location");
    //        }
    //        Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        if (ds2 != null) { ds2.Dispose(); }
    //        if (ds5 != null) { ds5.Dispose(); }
    //        if (ds1 != null) { ds1.Dispose(); }
    //    }
    //}
    #endregion=============== end user defined function
    protected void ddlItemCategory_Init(object sender, EventArgs e)
    {
        GetItemCategory();
        
    }
    protected void ddlShift_Init(object sender, EventArgs e)
    {
        GetShift();
    }
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedValue != "0" && ddlItemCategory.SelectedValue!="0")
        {
            lblMsg.Text = string.Empty;
            GetRoute();
            GetDatatableHeaderDesign();
        }
    }
    //protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GetItem();
    //    GetDatatableHeaderDesign();
    //}
    protected void txtEffectiveFromDate_TextChanged(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        txtEffectiveToDate.Text = string.Empty;
        GetDatatableHeaderDesign();
    }
    protected void ddlRoute_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlShift.SelectedValue != "0" && ddlRoute.SelectedValue != "0" && ddlItemCategory.SelectedValue != "0" && ddlShift.SelectedValue != "0")
        {
            this.GetItemListByRetailer();
            GetDatatableHeaderDesign();
        }

    }
    protected void txtEffectiveToDate_TextChanged(object sender, EventArgs e)
    {
        if (txtEffectiveFromDate.Text != "" || txtEffectiveFromDate.Text != string.Empty)
        {
            GetCompareDate();
            GetDatatableHeaderDesign();
        }
        else
        {
            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", "Select From Date First");
        }
    }
    protected void btnMSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            InsertRetailerAdvanceCard();
        }
    }
    protected void btnMClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        ddlLocation.SelectedIndex = 0;
        ddlItemCategory.SelectedIndex = 0;
        ddlRoute.Items.Clear();
     //   ddlRetailer.Items.Clear();
        ddlRoute.Items.Insert(0, new ListItem("Select", "0"));
     //   ddlRetailer.Items.Insert(0, new ListItem("Select", "0"));
        ddlShift.SelectedIndex = 0;
        GridView1.Visible = false;
        ViewState["RetailerData"] = null;
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "RecordEdit")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    lblMsg.Text = string.Empty;
                    lblMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblItemQty = (Label)row.FindControl("lblTotalItemQty");
                    TextBox txtTotalItemQty = (TextBox)row.FindControl("txtTotalItemQty");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    //RequiredFieldValidator rfv10 = row.FindControl("riq") as RequiredFieldValidator;
                    //RegularExpressionValidator rev10 = row.FindControl("rfvtiq") as RegularExpressionValidator;

                    foreach (GridViewRow gvRow in GridView2.Rows)
                    {
                        Label HlblItemQty = (Label)gvRow.FindControl("lblTotalItemQty");
                        TextBox HtxtTotalItemQty = (TextBox)gvRow.FindControl("txtTotalItemQty");
                        LinkButton HlnkEdit = (LinkButton)gvRow.FindControl("lnkEdit");
                        LinkButton HlnkUpdate = (LinkButton)gvRow.FindControl("lnkUpdate");
                        LinkButton HlnkReset = (LinkButton)gvRow.FindControl("lnkReset");
                        //RequiredFieldValidator Hrfv10 = gvRow.FindControl("riq") as RequiredFieldValidator;
                        //RegularExpressionValidator Hrev10 = gvRow.FindControl("rfvtiq") as RegularExpressionValidator;
                        HlblItemQty.Visible = true;
                        HtxtTotalItemQty.Visible = false;
                        HlnkEdit.Visible = true;
                        HlnkUpdate.Visible = false;
                        HlnkReset.Visible = false;
                        //Hrfv10.Enabled = false;
                        //Hrev10.Enabled = false;

                    }

                    lblItemQty.Visible = false;
                    lnkEdit.Visible = false;

                    lnkUpdate.Visible = true;
                    lnkReset.Visible = true;
                    //rfv10.Enabled = true;
                    //rev10.Enabled = true;
                    txtTotalItemQty.Visible = true;
                    foreach (GridViewRow gvRow in GridView2.Rows)
                    {
                        if (GridView2.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            GridView2.SelectedIndex = gvRow.DataItemIndex;
                            GridView2.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                    GetDatatableHeaderDesign();
                }

            }
            if (e.CommandName == "RecordReset")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {

                    lblMsg.Text = string.Empty;
                    lblMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblItemQty = (Label)row.FindControl("lblTotalItemQty");
                    TextBox txtTotalItemQty = (TextBox)row.FindControl("txtTotalItemQty");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");
                    //RequiredFieldValidator rfv10 = row.FindControl("rfv10") as RequiredFieldValidator;
                    //RegularExpressionValidator rev10 = row.FindControl("rev10") as RegularExpressionValidator;

                    lblItemQty.Visible = true;

                    lnkEdit.Visible = true;


                    lnkUpdate.Visible = false;
                    lnkReset.Visible = false;
                    //rfv10.Enabled = false;
                    //rev10.Enabled = false;
                    txtTotalItemQty.Visible = false;

                    GridView2.SelectedIndex = -1;
                    GetDatatableHeaderDesign();

                }

            }
            if (e.CommandName == "RecordUpdate")
            {

                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myItemDetailsModal()", true);
                    lblMsg.Text = string.Empty;
                    lblMsg.Text = string.Empty;
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblItemQty = (Label)row.FindControl("lblTotalItemQty");
                    TextBox txtTotalItemQty = (TextBox)row.FindControl("txtTotalItemQty");
                    LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
                    LinkButton lnkUpdate = (LinkButton)row.FindControl("lnkUpdate");
                    LinkButton lnkReset = (LinkButton)row.FindControl("lnkReset");


                    if (txtTotalItemQty.Text=="")
                    {
                        txtTotalItemQty.Text = "0";
                    }

                    ds1 = objdb.ByProcedure("USP_Trn_RetailerAdvanceCard",
                                new string[] { "flag", "RetailerAdvanceCardId", "Total_ItemQty", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                                new string[] { "4", e.CommandArgument.ToString(), txtTotalItemQty.Text.Trim(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Item Qty Updated from Advanced Card Mapping " }, "TableSave");

                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        GetItemMappingDetails();
                        GridView2.SelectedIndex = -1;
                        GetDatatableHeaderDesign();
                    }
                    else
                    {
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

                }


            }
            if (e.CommandName == "MappingRecordDel")
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    lblMsg.Text = string.Empty;
                    ds1 = objdb.ByProcedure("USP_Trn_RetailerAdvanceCard",
                                new string[] { "flag", "RetailerAdvanceCardId", "CreatedBy", "CreatedByIP", "PageName", "Remark" },
                                new string[] { "5", e.CommandArgument.ToString(), objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(), Path.GetFileName(Request.Url.AbsolutePath), "Citizen Item Mapping registration record deleted" }, "TableSave");

                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                    {
                        lblMsg.Text = string.Empty;
                        string success = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        GetItemMappingDetails();
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                    }
                    else
                    {
                        string error = ds1.Tables[0].Rows[0]["ErrorMsg"].ToString();
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                    }
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }


            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }
    #endregion========================================================

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DataTable dt1 = (DataTable)ViewState["RetailerData"];
            for (int i = 0; i < dt1.Columns.Count + 1; i++)
            {
                if (i == 0)
                {


                    CheckBox chkSelect = new CheckBox();

                    chkSelect.ID = "chkSelect" + i;
                    e.Row.Cells[i].Controls.Add(chkSelect);
                }
                if (i > 3)
                {

                    string strcolval = dt1.Columns[i - 1].ColumnName;
                    TextBox txtboxid = new TextBox();
                    txtboxid.MaxLength = 5;
                    txtboxid.Width = 50;
                    txtboxid.ID = "txtboxid" + i;
                    txtboxid.Attributes.Add("autocomplete", "off");
                    txtboxid.Text = (e.Row.DataItem as DataRowView).Row[strcolval].ToString();
                    if (txtboxid.Text == "")
                    {
                        txtboxid.Text = "0";
                    }
                    txtboxid.Attributes.Add("onkeypress", "return validateNum(event);");
                    e.Row.Cells[i + 1].Controls.Add(txtboxid);
                }


            }

        }

        e.Row.Cells[2].Visible = false;
        e.Row.Cells[3].Visible = false;
        e.Row.Cells[4].Visible = false;
    }

    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            GetItemMappingDetails();
        }
    }
    protected void ddlSearchLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlSearchItemCategory.SelectedValue!="0" && ddlSearchLocation.SelectedValue!="0")
        {
            GetSearchRoute();
            GetDatatableHeaderDesign();
        }
    }
}