using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Demand_CityWiseCrateEntry_GJ_DS : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds1, ds2, ds3 = new DataSet();
    int RCTotal = 0;

    IFormatProvider culture = new CultureInfo("en-US", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {

                string Date = DateTime.Now.ToString("dd/MM/yyyy");
                txtReturnDate.Text = Date;
                txtReturnDate.Attributes.Add("readonly", "readonly");
                txtSerchDate.Text = Date;
                txtSerchDate.Attributes.Add("readonly", "readonly");
                GetParty();
                GetPartySearch();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

            }
        }
        else
        {
            objdb.redirectToHome();
        }
    }
    #region=========== User Defined function======================
    private void ClearText()
    {

        txtCrateRemark.Text = string.Empty;
        txtReturnCrate.Text = string.Empty;
        txtChallano.Text = string.Empty;
        GridView1.SelectedIndex = -1;
        btnSubmit.Text = "Save";

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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error 1: " + ex.Message.ToString());
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    private void GetParty()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductCrateReturnMgmt",
                     new string[] { "Flag", "Office_ID", "AreaId" },
                       new string[] { "9", objdb.Office_ID(),"0"}, "dataset");
            ddlPartyName.Items.Clear();
            
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddlPartyName.DataTextField = "DRHName";
                ddlPartyName.DataValueField = "DRAId";
                ddlPartyName.DataSource = ds1.Tables[0];
                ddlPartyName.DataBind();
                ddlPartyName.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlPartyName.Items.Insert(0, new ListItem("No Record Found.", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 3 ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }

    private void GetPartySearch()
    {
        try
        {
            ds1 = objdb.ByProcedure("USP_Trn_MilkOrProductCrateReturnMgmt",
                new string[] { "Flag", "Office_ID", "AreaId" },
                  new string[] { "9", objdb.Office_ID(),"0" }, "dataset");
            
            ddlPartyNameSearch.Items.Clear();
            if (ds1.Tables[0].Rows.Count > 0)
            {
                ddlPartyNameSearch.DataTextField = "DRHName";
                ddlPartyNameSearch.DataValueField = "DRAId";
                ddlPartyNameSearch.DataSource = ds1.Tables[0];
                ddlPartyNameSearch.DataBind();
                ddlPartyNameSearch.Items.Insert(0, new ListItem("All", "0"));
            }
            else
            {
                ddlPartyName.Items.Insert(0, new ListItem("No Record Found.", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error party search ", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }

    private void FillGrid()
    {
        try
        {
            lblMsg.Text = string.Empty;
            string distid = "";
            if (ddlPartyNameSearch.SelectedValue == "0")
            {
                distid = "0";
            }
            else
            {
                string[] DRAId = ddlPartyNameSearch.SelectedValue.Split('-');
                distid = DRAId[0];
            }
            DateTime dateSearch = DateTime.ParseExact(txtSerchDate.Text, "dd/MM/yyyy", culture);
            string datSearch = dateSearch.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            ds2 = objdb.ByProcedure("USP_Trn_MilkOrProductCrateReturnMgmt",
                     new string[] { "Flag", "Office_ID", "ReturnDate", "DistributorId" },
                       new string[] { "10", objdb.Office_ID(), dateSearch.ToString(), distid.ToString() }, "dataset");
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds2.Tables[0];
                GridView1.DataBind();
                GetDatatableHeaderDesign();

            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1:", ex.Message.ToString());
        }
        finally
        {
            if (ds2 != null) { ds2.Dispose(); }
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblReturnCrate = (e.Row.FindControl("lblReturnCrate") as Label);
                RCTotal += Convert.ToInt32(lblReturnCrate.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblFAmount = (e.Row.FindControl("lblFAmount") as Label);

                lblFAmount.Text = RCTotal.ToString("0.00");

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! : Error 5 ", ex.Message.ToString());
        }
    }


    #endregion========================================================





    #region=========== click event for grdiview row command event===========================

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];

                    DateTime rdate = DateTime.ParseExact(txtReturnDate.Text, "dd/MM/yyyy", culture);
                    string rdat = rdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    string[] DRAId = ddlPartyName.SelectedValue.Split('-');


                    if (btnSubmit.Text == "Save")
                    {

                        ds3 = objdb.ByProcedure("USP_Trn_MilkOrProductCrateReturnMgmt"
                            , new string[] {"flag","ReturnDate", "SuperStockistId","DistributorId","RouteId" , "AreaId"
                                ,"ReturnCrate","SlipNo","CrateRemark" ,"Office_ID", "CreatedBy","CreatedByIP"}
                           , new string[] {"3" ,rdat,DBNull.Value.ToString(),DRAId[0],DRAId[1],DRAId[2]
                               ,txtReturnCrate.Text.Trim(),txtChallano.Text.Trim(),txtCrateRemark.Text.Trim(),objdb.Office_ID(),objdb.createdBy(),IPAddress
                              }, "dataset");
                        if (ds3 != null && ds3.Tables.Count > 0)
                        {
                            if (ds3.Tables[0].Rows.Count > 0)
                            {
                                if (ds3.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                {
                                    string Msg = ds3.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", Msg.ToString());
                                    ClearText();
                                }
                                else if (ds3.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                                {
                                    string Msg = ds3.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", Msg.ToString());
                                }
                                else
                                {
                                    string Msg = ds3.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Msg.ToString());
                                }
                            }
                        }
                    }
                    else if (btnSubmit.Text == "Update")
                    {

                        ds3 = objdb.ByProcedure("USP_Trn_MilkOrProductCrateReturnMgmt"
                            , new string[] {"flag","MilkandProductCrateMgmtId","ReturnDate", "SuperStockistId","DistributorId","RouteId" , "AreaId"
                                ,"ReturnCrate","SlipNo","CrateRemark" ,"Office_ID", "CreatedBy","CreatedByIP"}
                           , new string[] {"4" ,ViewState["rowid"].ToString(),rdat,DBNull.Value.ToString(),DRAId[0],DRAId[1],DRAId[2]
                               ,txtReturnCrate.Text.Trim(),txtChallano.Text.Trim(),txtCrateRemark.Text.Trim(),objdb.Office_ID(),objdb.createdBy(),IPAddress
                              }, "dataset");
                        if (ds3 != null && ds3.Tables.Count > 0)
                        {
                            if (ds3.Tables[0].Rows.Count > 0)
                            {
                                if (ds3.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                                {
                                    string Msg = ds3.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    FillGrid();
                                    ClearText();
                                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "ThankYou!", Msg.ToString());
                                   
                                    ddlPartyName.SelectedIndex = 0;
                                }
                                else if (ds3.Tables[0].Rows[0]["Msg"].ToString() == "Already")
                                {
                                    string Msg = ds3.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Sorry!", Msg.ToString());
                                }
                                else
                                {
                                    string Msg = ds3.Tables[0].Rows[0]["ErrorMsg"].ToString();
                                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Msg.ToString());
                                }
                            }
                        }
                    }

                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry! Error 1:", ex.Message.ToString());
        }
        finally
        {
            if (ds3 != null) { ds3.Dispose(); }
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = string.Empty;
            if (e.CommandName == "EditRecord")
            {
                Control ctrl = e.CommandSource as Control;
                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                    Label lblReturnDate = (Label)row.FindControl("lblReturnDate");
                    Label lblReturnCrate = (Label)row.FindControl("lblReturnCrate");
                    Label lblSlipNo = (Label)row.FindControl("lblSlipNo");
                    Label lblCrateRemark = (Label)row.FindControl("lblCrateRemark");
                    Label lblDRAId = (Label)row.FindControl("lblDRAId");

                    txtReturnDate.Text = lblReturnDate.Text;
                    txtReturnCrate.Text = lblReturnCrate.Text;
                    txtChallano.Text = lblSlipNo.Text;
                    txtCrateRemark.Text = lblCrateRemark.Text;
                    ddlPartyName.SelectedValue = lblDRAId.Text;

                    ViewState["rowid"] = e.CommandArgument;
                    btnSubmit.Text = "Update";

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
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        btnSubmit.Text = "Save";
        ddlPartyName.SelectedIndex = 0;
        ClearText();
        GetDatatableHeaderDesign();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            FillGrid();
        }
    }

    #endregion===========================
}