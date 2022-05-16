using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;

public partial class mis_CattleFeed_CFP_SetItemRatioForProducts : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objapi = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        fillProdUnit(); lblMsg.Text = string.Empty;
    }
    private void fillProdUnit()
    {
        try
        {
            ddlProdUnit.Items.Clear();
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFPOffice_By_DS", new string[] { "flag", "DSID" }, new string[] { "0", objapi.Office_ID() }, "dataset");
            ddlProdUnit.Items.Insert(0, new ListItem("-- Select --", "0"));
            ddlProdUnit.DataSource = ds;
            ddlProdUnit.DataValueField = "CFPOfficeID";
            ddlProdUnit.DataTextField = "CFPName";
            ddlProdUnit.DataBind();
            ddlProdUnit.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    private void fillProdunderCFP()
    {
        try
        {
            ddlProd.Items.Clear();
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFP_Items_Implementation_CFPofDSList", new string[] { "flag", "CFPID" }, new string[] { "0", ddlProdUnit.SelectedValue }, "dataset");
            ddlProd.DataSource = ds;
            ddlProd.DataValueField = "Itemid";
            ddlProd.DataTextField = "ItemName";
            ddlProd.DataBind();
            ddlProd.Items.Insert(0, new ListItem("-- Select --", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed !" + ex.ToString(), " ");
        }
        finally { ds.Dispose(); GC.SuppressFinalize(objapi); }
    }
    protected void ddlProdUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillProdunderCFP();
    }
    public static bool isFloatValue(string text)
    {
        Regex regex = new Regex(@"^\d*\.?\d?$");
        return regex.IsMatch(text);
    }
    protected void txtPercentage_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            decimal total = 0;
            lblMsg.Text = string.Empty;
            foreach (GridViewRow row in gvProductItems.Rows)
            {
                TextBox txtPercentage = (TextBox)row.FindControl("txtPercentage");
                HiddenField hdnvalue = (HiddenField)row.FindControl("hdnItemRatioID");
                if (hdnvalue.Value == "1")
                {
                    if (txtPercentage.Text == "")
                    {
                        txtPercentage.Text = "0";
                        txtPercentage.Focus();
                    }
                    if (txtPercentage.Text != "")
                    {
                        if (isFloatValue(txtPercentage.Text))
                        {
                            total += decimal.Parse(txtPercentage.Text);
                            if (total > 100)
                            {
                                lblMsg.Text = objapi.Alert("fa-warning", "alert-warning", "Warning!", "Please Check Entered Percentage. Can not greater than Or Less than 100%");
                                txtPercentage.Text = string.Empty;
                                total = 0;
                            }
                        }
                        else
                        {
                            txtPercentage.Text = "0";
                            txtPercentage.Focus();
                        }
                    }
                  
                }
            }

            Label lblFooterTotal = gvProductItems.FooterRow.FindControl("lblFooterTotal") as Label;
            if (total != 0)
            {
                lblFooterTotal.Text = total.ToString();
            }
            else
            {
                lblFooterTotal.Text = total.ToString();

            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Sorry!", "Error : " + ex.Message.ToString());
        }
    }

    private void fillgrd()
    {
        DataTable dt = new DataTable();
        dt = getValues();
        gvProductItems.DataSource = dt;
        gvProductItems.DataBind();
        if (dt.Rows.Count > 0)
        {
            pnlsave.Visible = true;
        }
        else
            pnlsave.Visible = false;

        GC.SuppressFinalize(dt);

    }
    private void fillSecgrd()
    {
        DataTable dt = new DataTable();
        dt = getSecValues();
        gvOpeningStock.DataSource = dt;
        gvOpeningStock.DataBind();


        GC.SuppressFinalize(dt);

    }
    private DataTable getValues()
    {
        ds = new DataSet();
        ds = objapi.ByProcedure("SP_CFP_Item_Specifcation_By_Product_SetRatio_List", new string[] { "flag", "ProductID", "CFPID" }, new string[] { "0", ddlProd.SelectedValue, ddlProdUnit.SelectedValue }, "dataset");
        return ds.Tables[0];
    }
    private DataTable getSecValues()
    {
        ds = new DataSet();
        ds = objapi.ByProcedure("SP_CFP_Item_Specifcation_By_Product_AlreadySetRatio_List", new string[] { "flag", "ProductID", "CFPID", "OfficeID" }, new string[] { "0", ddlProd.SelectedValue, ddlProdUnit.SelectedValue, objapi.Office_ID() }, "dataset");
        return ds.Tables[0];
    }
    private DataTable getTableValues()
    {
        DataTable dt = new DataTable();
        DataRow dr, dr1;
        dt.Columns.Add(new System.Data.DataColumn("ItemRatioID", typeof(Int32)));
        dt.Columns.Add(new System.Data.DataColumn("ItemName", typeof(string)));
        dt.Columns.Add(new System.Data.DataColumn("UQCCode", typeof(string)));
        dt.Columns.Add(new System.Data.DataColumn("Item_Ratio", typeof(string)));
        dr = dt.NewRow();
        dr[0] = 1;
        dr[1] = "DORB";
        dr[2] = "KG";
        dr[3] = "Percentage(%)";
        dt.Rows.Add(dr);
        dr1 = dt.NewRow();
        dr1[0] = 1;
        dr1[1] = "Rice Brand";
        dr1[2] = "KG";
        dr1[3] = "Percentage(%)";
        dt.Rows.Add(dr1);
        //foreach (GridViewRow row in gvProductItems.Rows)
        //{
        //    Label lblProdSpec_id = (Label)row.FindControl("lblProdSpec_id");
        //    Label lblItem_id = (Label)row.FindControl("lblItem_id");
        //    Label lblItemType_id = (Label)row.FindControl("lblItemType_id");
        //    Label lblItemCat_id = (Label)row.FindControl("lblItemCat_id");
        //    Label lblUnit_id = (Label)row.FindControl("lblUnit_id");
        //    Label lblItem_Ratio = (Label)row.FindControl("lblItem_Ratio");
        //    TextBox txtPercentage = (TextBox)row.FindControl("txtPercentage");
        //    dr = dt.NewRow();
        //    dr[0] = ddlProdUnit.SelectedValue;
        //    dr[1] = lblProdSpec_id.Text;
        //    dr[2] = ddlProdct.SelectedItem.Value;
        //    dr[3] = lblItem_id.Text;
        //    dr[4] = lblItemType_id.Text;
        //    dr[5] = lblItemCat_id.Text;
        //    dr[6] = lblItem_Ratio.Text;
        //    dr[7] = lblUnit_id.Text;
        //    dr[8] = txtPercentage.Text;

        //    dt.Rows.Add(dr);
        //}
        return dt;
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        fillgrd();
        lblproname.Text = ddlProd.SelectedItem.Text;
        fillSecgrd();
        filledgrd.Visible = true;
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        Clear();
        fillgrd();
        fillSecgrd();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        decimal total = 0;
        foreach (GridViewRow row in gvProductItems.Rows)
        {
            TextBox txtPercentage = (TextBox)row.FindControl("txtPercentage");
            HiddenField hdnvalue = (HiddenField)row.FindControl("hdnItemRatioID");
            //if (hdnvalue.Value == "1")
            //{                
                total += decimal.Parse(txtPercentage.Text);                
            //}
        }
        if (total == 100)
        {
            string xml = GetXML();
            if (xml != string.Empty)
            {
                ds = new DataSet();
                ds = objapi.ByProcedure("sp_CFP_Product_Item_Set_Ratio_Insert",
                                       new string[] { "flag", "CFPID", "ProductID", "str" },
                                       new string[] { "0", ddlProdUnit.SelectedValue, ddlProd.SelectedValue, xml }, "Dataset");
                if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "OK")
                {
                    lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Success !", "Thank You! Research Details Successfully Completed ");
                    //Clear();
                    fillgrd();
                    filledgrd.Visible = true;
                    fillSecgrd();
                }
                else
                {
                    lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed ! Due to Some technical problem. Please try it again.", " ");

                }
            }
            
        }
        else
        {
            lblMsg.Text = objapi.Alert("fa-warning", "alert-warning", "Warning!", "Please Check Entered Percentage. Can not greater than Or Less than 100%");
            total = 0;
        }

        
    }
    private void Clear()
    {
        ddlProdUnit.SelectedValue = "0";
        fillProdunderCFP();
        ddlProd.SelectedValue = "0";
        filledgrd.Visible = false;
    }
    private string GetXML()
    {
        string result = string.Empty;
        if (gvProductItems.Rows.Count > 0)
        {
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.Indent = true;
            xws.NewLineOnAttributes = true;
            xws.OmitXmlDeclaration = true;
            xws.CheckCharacters = true;
            xws.CloseOutput = false;
            xws.Encoding = Encoding.UTF8;
            StringBuilder sb = new StringBuilder();
            XmlWriter xw = XmlWriter.Create(sb, xws);
            xw.WriteStartDocument();
            xw.WriteStartElement("ROOT");

            foreach (GridViewRow row in gvProductItems.Rows)
            {
                HiddenField hdnItemRatioID = (HiddenField)row.FindControl("hdnItemRatioID");
                HiddenField hdnCFPProductSpecifcationID = (HiddenField)row.FindControl("hdnCFPProductSpecifcationID");
                HiddenField hdnItemID = (HiddenField)row.FindControl("hdnItemID");
                HiddenField hdnItemTypeID = (HiddenField)row.FindControl("hdnItemTypeID");
                HiddenField hdnItemCatID = (HiddenField)row.FindControl("hdnItemCatID");
                HiddenField hdnUnitid = (HiddenField)row.FindControl("hdnUnitid");
                TextBox txtPercentage = (TextBox)row.FindControl("txtPercentage");
                xw.WriteStartElement("ROW");
                xw.WriteAttributeString("Product_Specification_ID", hdnCFPProductSpecifcationID.Value);
                xw.WriteAttributeString("Item_Cat_ID", hdnItemCatID.Value);
                xw.WriteAttributeString("Item_Type_ID", hdnItemTypeID.Value);
                xw.WriteAttributeString("Item_ID", hdnItemID.Value);
                xw.WriteAttributeString("Item_Ratio_ID", hdnItemRatioID.Value);
                xw.WriteAttributeString("Unit_ID", hdnUnitid.Value);
                xw.WriteAttributeString("ProductPercentage", txtPercentage.Text);
                xw.WriteAttributeString("OfficeID", objapi.Office_ID());
                xw.WriteAttributeString("InsertedBy", objapi.createdBy());
                xw.WriteAttributeString("IPAddress", Request.UserHostAddress);
                xw.WriteEndElement();
            }
            xw.WriteEndDocument();
            xw.Flush();
            xw.Close();
            result = sb.ToString();
        }
        return result;

    }

}