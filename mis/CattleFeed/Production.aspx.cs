using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class mis_CattleFeed_Production : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objapi = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        fillProdUnit();

    }
    private void fillProdUnit()
    {
        try
        {
            ddlcfp.Items.Clear();
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFPOffice_By_DS", new string[] { "flag", "DSID" }, new string[] { "0", objapi.Office_ID() }, "dataset");
            ddlcfp.DataSource = ds;
            ddlcfp.DataValueField = "CFPOfficeID";
            ddlcfp.DataTextField = "CFPName";
            ddlcfp.DataBind();
            ddlcfp.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    private void Fillgrd()
    {
        try
        {
            ds = new DataSet();
           ds =  objapi.ByProcedure("SP_CFPProduct_By_ItemTypeID_For_Production_List",
                            new string[] { "flag", "ItemTypeID", "CFPID", "TransactionDT" },
                            new string[] { "0" ,"4",ddlcfp.SelectedValue,txtTransactionDt.Text}, "dataset");
            gvOpeningStock.DataSource = ds;
            gvOpeningStock.DataBind();
            if (gvOpeningStock.Rows.Count > 0)
            {
               
                //Adds THEAD and TBODY to GridView.
                gvOpeningStock.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
           
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnsave.Visible = true;
                btnClear.Visible = true;
                btnGenerate.Enabled = false;
            }
            else
            {
                btnsave.Visible = false;
                btnClear.Visible = false;
                btnGenerate.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            GC.SuppressFinalize(objapi);
            btnGenerate.Enabled = true;
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
        btnGenerate.Enabled = true;
    }

    private void Clear()
    {
        txtTransactionDt.Text= string.Empty;
        ddlcfp.SelectedValue="0";
        Fillgrd();
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Fillgrd();
        
    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        ds = new DataSet();
        foreach (GridViewRow row in gvOpeningStock.Rows)
        {
            decimal dailyprod = 0, scrapprod = 0;

            TextBox batchno = (TextBox)row.FindControl("txtbatchno");
            TextBox txttotalpacket = (TextBox)row.FindControl("txttotalpacket");
            TextBox txtDailyProdCr = (TextBox)row.FindControl("txtDailyProdCr");
            TextBox txtScrapProd = (TextBox)row.FindControl("txtScrapProd");
            HiddenField hdnCFPProductSizeID = (HiddenField)row.FindControl("hdnCFPProductSizeID");
            HiddenField hdnpackingsize = (HiddenField)row.FindControl("hdnpackingsize");
            HiddenField hdnItem = (HiddenField)row.FindControl("hdnItem");
            HiddenField hdnCFP_ProductProductionid = (HiddenField)row.FindControl("hdnCFP_ProductProductionid");
            
            if (txttotalpacket.Text != "" && txtDailyProdCr.Text != "" && batchno.Text != "")
            {
                dailyprod = Convert.ToDecimal(txtDailyProdCr.Text.ToString());
                if (txtScrapProd.Text == "")
                {
                    txtScrapProd.Text = "0.000";
                }
                else
                {
                    scrapprod = Convert.ToDecimal(txtScrapProd.Text.ToString());
                }
                if (Convert.ToInt32(txttotalpacket.Text) > 0)
                {
                    ds = objapi.ByProcedure("SP_CFP_Product_Production_Insert",
                                  new string[] { "flag", "CFPID", "OfficeID", "InsertedID", "IPAddress", "TransactionDt", "ProductID", "CFPProductSizeID", "BatchNo", "ProductionQuantity", "ScrapProductionQuantity", "Remark", "TotalBagsCreated", "CFP_ProductProductionid" },
                                  new string[] { "0", ddlcfp.SelectedValue, objapi.Office_ID(), objapi.createdBy(), Request.UserHostAddress, txtTransactionDt.Text, hdnItem.Value, hdnCFPProductSizeID.Value, batchno.Text, txtDailyProdCr.Text, txtScrapProd.Text, "Daily Production", txttotalpacket.Text, hdnCFP_ProductProductionid.Value }, "dataset");
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "OK")
                        {

                            lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Success !", "Thank You! Operation has been Successfully Completed ");
                            
                        }
                        else
                        {
                            lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Fail !", " Operation has not been Successfully Completed ");
                            
                        }
                    }
                }

            }
        }
    }
    protected void txttotalpacket_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvOpeningStock.Rows)
        {
            double packets = 0, Weight = 0, MTWt = 0;
            TextBox batchno = (TextBox)row.FindControl("txtbatchno");
            TextBox totalpacket = (TextBox)row.FindControl("txttotalpacket");
            TextBox DailyProdCr = (TextBox)row.FindControl("txtDailyProdCr");
            TextBox txtScrapProd = (TextBox)row.FindControl("txtScrapProd");
            HiddenField hdnpackagingsize = (HiddenField)row.FindControl("hdnpackingsize");
            if (totalpacket.Text != "")
            {
                packets = Convert.ToDouble(totalpacket.Text.ToString());
                Weight = Convert.ToDouble(hdnpackagingsize.Value);
                MTWt = (packets * Weight) / 1000;
                DailyProdCr.Text = Math.Round(MTWt, 3).ToString();
                if (txtScrapProd.Text == "")
                {
                    txtScrapProd.Text = "0.000";
                }
                txtScrapProd.Enabled = true;
            }
            else
            {
                txtScrapProd.Text = "";
                DailyProdCr.Text = "";
                txtScrapProd.Enabled = false;
            }
        }
    }
}