using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class mis_CattleFeed_CFP_Product_RecipeGeneration : System.Web.UI.Page
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
    private DataTable getValues()
    {
        ds = new DataSet();
        ds = objapi.ByProcedure("SP_CFP_Item_Specifcation_By_Product_GenerateRecipe_List", new string[] { "flag", "ProductID", "CFPID", "OfficeID", "Quantity" }, new string[] { "0", ddlProd.SelectedValue, ddlProdUnit.SelectedValue, objapi.Office_ID(), txtQty.Text }, "dataset");
        return ds.Tables[0];
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string xml = GetXML();
        if (xml != string.Empty)
        {
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFP_Product_Recipe_Insert",
                                   new string[] { "flag", "CFPID", "ProductID", "ProductUnitID", "ProductQuantity", "OfficeID", "IPAddress", "InsertedBY", "str" },
                                   new string[] { "0", ddlProdUnit.SelectedValue, ddlProd.SelectedValue, "7", txtQty.Text, objapi.Office_ID(), Request.UserHostAddress, objapi.createdBy(), xml }, "Dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "OK")
                {
                    lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Success !", "Thank You! Research Detals Successfully Completed ");
                    //Clear();
                    fillgrd();
                    btnSubmit.Visible = false;
                    btnview.Enabled = false;
                }
            }
            else
            {
                lblMsg.Text = objapi.Alert("fa-ban", "alert-danger", "Failed ! Due to Some technical problem. Please try it again.", " ");

            }
        }
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
                HiddenField hdnItemRatioID = (HiddenField)row.FindControl("hdnProductItemSetRatioID");
                HiddenField hdnCFPProductSpecifcationID = (HiddenField)row.FindControl("hdnCFPProductSpecifcationID");
                HiddenField hdnItemID = (HiddenField)row.FindControl("hdnItemID");
                HiddenField hdnItemTypeID = (HiddenField)row.FindControl("hdnItemTypeID");
                HiddenField hdnItemCatID = (HiddenField)row.FindControl("hdnItemCatID");
                HiddenField hdnUnitid = (HiddenField)row.FindControl("hdnUnitid");
                Label lblitemquantity = (Label)row.FindControl("lblitemquantity");
                xw.WriteStartElement("ROW");
                xw.WriteAttributeString("ProductItemSetRatioID", hdnItemRatioID.Value);
                xw.WriteAttributeString("Item_Cat_ID", hdnItemCatID.Value);
                xw.WriteAttributeString("Item_Type_ID", hdnItemTypeID.Value);
                xw.WriteAttributeString("Item_ID", hdnItemID.Value);
                xw.WriteAttributeString("Item_Unit_ID", "7");
                xw.WriteAttributeString("ItemQuantity", lblitemquantity.Text);
                xw.WriteEndElement();
            }
            xw.WriteEndDocument();
            xw.Flush();
            xw.Close();
            result = sb.ToString();
        }
        return result;

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
        ddlProdUnit.SelectedValue = "0";
        fillProdunderCFP();
        ddlProd.SelectedValue = "0";
        txtQty.Text = "0";
        txtQty.Enabled = true;
        fillgrd();
        btnview.Enabled = true;
        lblMsg.Text = string.Empty;
    }
    protected void btnview_Click(object sender, EventArgs e)
    {
        txtQty.Enabled = false;
        fillgrd();
        btnview.Enabled = false;
        btnSubmit.Visible = true;
    }


    protected void ddlProdUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillProdunderCFP();
    }
}