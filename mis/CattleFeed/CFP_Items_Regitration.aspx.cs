using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.Text;

public partial class mis_CattelFeed_CFP_Items_Regitration : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        Unit();
        Category();

        RegiseredCFP();
        Fillgrd();
        lblMsg.Text = string.Empty;
    }

    private void Unit()
    {
        try
        {
            ddlUnit.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("CFP_ItemtblSpUnit",
                                  new string[] { "flag" },
                                  new string[] { "0" }, "dataset");
            ddlUnit.DataSource = ds;
            ddlUnit.DataTextField = "UnitName";
            ddlUnit.DataValueField = "Unit_Id";
            ddlUnit.DataBind();
            ddlUnit.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        catch (Exception ex)
        {


        }
        finally { ds.Dispose(); }
    }
    private void Category()
    {
        try
        {
            ddlItemCategory.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_CFP_ItemCategoryList",
                                  new string[] { "flag" },
                                  new string[] { "0" }, "dataset");
            ddlItemCategory.DataSource = ds;
            ddlItemCategory.DataTextField = "CFP_ItemCatName";
            ddlItemCategory.DataValueField = "CFPItemCat_id";
            ddlItemCategory.DataBind();
            ddlItemCategory.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        catch (Exception ex)
        {


        }
        finally { ds.Dispose(); }

    }
    private void ItemType()
    {
        try
        {
            ddlGroup.Items.Clear();
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_CFP_ItemTypeList",
                                  new string[] { "flag", "CategoryID" },
                                  new string[] { "0", ddlItemCategory.SelectedValue }, "dataset");
            ddlGroup.DataSource = ds;
            ddlGroup.DataTextField = "ItemTypeName";
            ddlGroup.DataValueField = "ItemType_id";
            ddlGroup.DataBind();
            ddlGroup.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
        catch (Exception ex)
        {

        }
        finally { ds.Dispose(); }
    }
    private void RegiseredCFP()
    {
        try
        {
            chkoffice.DataSource = null;
            ds = new DataSet();
            ds = objdb.ByProcedure("SPCFPOfficeRegistrationUnderDSList",
                                  new string[] { "flag", "DSID" },
                                  new string[] { "0", objdb.Office_ID() }, "dataset");
            chkoffice.DataSource = ds;
            //chkAllProductionUnit.DataTextField = "CFPName";
            //chkAllProductionUnit.DataValueField = "CFPOfficeID";
            chkoffice.DataBind();
        }
        catch (Exception ex)
        {

        }
        finally { ds.Dispose(); }
    }
    private void RegiseredCFPforedit()
    {
        try
        {
            chkDS.ClearSelection();
            ds = new DataSet();
            ds = objdb.ByProcedure("SPCFPOfficeRegistrationUnderDSList",
                                  new string[] { "flag", "DSID" },
                                  new string[] { "0", objdb.Office_ID() }, "dataset");
            chkDS.DataSource = ds;
            chkDS.DataTextField = "CFPName";
            chkDS.DataValueField = "CFPOfficeID";
            chkDS.DataBind();
        }
        catch (Exception ex)
        {

        }
        finally { ds.Dispose(); }
    }
    private void CHECKimplementationdone()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_Item_Implementation_CFP_Count", new string[] { "flag", "ItemID" }, new string[] { "0", hdnvalue.Value }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                hdncount.Value = Convert.ToString(ds.Tables[0].Rows[0]["NoofCount"]);
            }
            GC.SuppressFinalize(objdb);
            //GC.SuppressFinalize(ds);
        }
        catch (Exception)
        {


        }
        finally { ds.Dispose(); }

    }
    private void Implementationdetail()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("SP_Item_Implementation_CFP_Count", new string[] { "flag", "ItemID" }, new string[] { "1", hdnvalue.Value }, "dataset");
            grdimplementation.DataSource = ds;
            grdimplementation.DataBind();
            GC.SuppressFinalize(objdb);
            // GC.SuppressFinalize(ds);
        }
        catch (Exception ex)
        {

            //throw;
        }
        finally { ds.Dispose(); }

    }
    private void fillCFPofficeforimplmentation()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("SPCFPOfficeRegistrationUnderDSList", new string[] { "flag" }, new string[] { "1" }, "dataset");
            chkDS.DataSource = ds;
            chkDS.DataTextField = "CFPName";
            chkDS.DataValueField = "CFPOfficeID";
            chkDS.DataBind();
            GC.SuppressFinalize(objdb);
            //  GC.SuppressFinalize(ds);
        }
        catch (Exception ex)
        {

        }
        finally { ds.Dispose(); }


    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ItemType();
    }
    private void InsertOrUpdateProduct()
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                ds = new DataSet();
                string flag = "0";
                if (Convert.ToInt32(hdnvalue.Value) > 0)
                {
                    flag = "1";
                }
                ds = objdb.ByProcedure("SP_CFPItems_Insert_Update_Delete",
                    new string[] { "flag", "ItemTypeid", "Unitid", "ItemName", "ItemNameHindi", "Itemspecification", "ItemCode", "insertedBy", "ItemID" },
                    new string[] { flag, ddlGroup.SelectedValue, ddlUnit.SelectedValue, txtItemName.Text.Trim(), txtItemNameHi.Text.Trim(), txtItemSpecification.Text.Trim(), txtItemCode.Text.Trim(), objdb.createdBy(), hdnvalue.Value }, "TableSave");
                if (ds.Tables[0].Rows[0]["ErrorMSG"].ToString() == "OK")
                {
                    Fillgrd();
                    string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    Clear();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error : Fail due to some technical problem.");
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally { ds.Dispose(); }
    }
    private void Clear()
    {
        txtItemCode.Text = string.Empty;
        ddlItemCategory.SelectedValue = "0";
        ItemType();
        ddlGroup.SelectedValue = "0";
        ddlUnit.SelectedValue = "0";
        ddlUnit.Enabled = true;
        txtItemName.Text = string.Empty;
        txtItemNameHi.Text = string.Empty;
        txtItemSpecification.Text = string.Empty;
        txtItemCode.Enabled = true;
        hdnvalue.Value = "0";
        btnSave.Text = "Save";

    }
    private void Fillgrd()
    {
        try
        {
            grdCatlist.DataSource = objdb.ByProcedure("SP_CFPItemsList",
                            new string[] { "flag" },
                            new string[] { "0" }, "dataset");
            grdCatlist.DataBind();
            grdCatlist.HeaderRow.TableSection = TableRowSection.TableHeader;
            grdCatlist.UseAccessibleHeader = true;
            GC.SuppressFinalize(objdb);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private bool validateCode()
    {
        bool result = false;
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("CheckItemCodeByCode",
                  new string[] { "flag", "ItemCode" },
                  new string[] { "0", txtItemCode.Text.Trim() }, "TableSave");
            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IsExistCode"]))
            { result = true; }

        }
        catch (Exception ex)
        {

        }
        finally { ds.Dispose(); }
        return result;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        if (Convert.ToInt32(hdnvalue.Value) == 0)
        {
            if (!validateCode())
            {
                InsertOrUpdateProduct();
            }
            else
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-info", "Sorry!", "Error : Item Code already exist.");
            }
        }
        else
            InsertOrUpdateProduct();
    }
    protected void grdCatlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hdnvalue.Value = Convert.ToString(e.CommandArgument);
        lblMsg.Text = string.Empty;
        switch (e.CommandName)
        {
            case "RecordUpdate":
                DataSet ds1 = new DataSet();
                ds1 = objdb.ByProcedure("SP_CFPItems_By_ItemID_List", new string[] { "flag", "ItemID" }, new string[] { "0", hdnvalue.Value }, "dataset");
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    txtItemCode.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Item_Code"]);
                    ddlItemCategory.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["ItemCat_id"]);
                    ItemType();
                    ddlGroup.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["ItemType_id"]);
                    Unit();
                    ddlUnit.SelectedValue = Convert.ToString(ds1.Tables[0].Rows[0]["Unit_id"]);
                    if (ddlUnit.SelectedValue == "0")
                        ddlUnit.Enabled = true;
                    else
                        ddlUnit.Enabled = false;
                    txtItemName.Text = Convert.ToString(ds1.Tables[0].Rows[0]["ItemName"]);
                    txtItemNameHi.Text = Convert.ToString(ds1.Tables[0].Rows[0]["ItemName_Hindi"]);
                    txtItemSpecification.Text = Convert.ToString(ds1.Tables[0].Rows[0]["Itemspecification"]);
                    btnSave.Text = "Edit";
                    txtItemCode.Enabled = false;
                }
                GC.SuppressFinalize(objdb);
                GC.SuppressFinalize(ds1);
                break;
            case "RecordDelete":
                ds = new DataSet();

                ds = objdb.ByProcedure("SP_CFPItems_Insert_Update_Delete",
  new string[] { "flag", "ItemTypeid", "Unitid", "ItemName", "ItemNameHindi", "Itemspecification", "ItemCode", "insertedBy", "ItemID" },
new string[] { "3", ddlGroup.SelectedValue, ddlUnit.SelectedValue, txtItemName.Text.Trim(), txtItemNameHi.Text.Trim(), txtItemSpecification.Text.Trim(), txtItemCode.Text.Trim(), objdb.createdBy(), hdnvalue.Value }, "TableSave");
                if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorMsg"]) == "OK")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-error", "Fail!", "Due to some technical issues. Operation couldn't completed.");
                }



                Fillgrd();
                GC.SuppressFinalize(ds);
                GC.SuppressFinalize(objdb);
                break;
            case "implement":
                RegiseredCFP();
                CHECKimplementationdone();
                if (Convert.ToInt32(hdncount.Value) > 0)
                {
                    btnimplement.Visible = false;
                    ImplementationEntry.Visible = false;
                    ImplementationDetail.Visible = true;
                    Implementationdetail();
                }
                else
                {
                    btnimplement.Visible = true;
                    ImplementationEntry.Visible = true;
                    ImplementationDetail.Visible = false;
                }


                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupImplement();", true);
                break;
            case "Editimplement":
                RegiseredCFPforedit();
                ImplementatedDSdetail();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "MyScript", "ShowPopupEditImplement();", true);
                break;
            default:
                break;
        }
    }
    private void ImplementatedDSdetail()
    {
        chkDS.ClearSelection();
        ds = new DataSet();
        ds = objdb.ByProcedure("SP_Item_Implementation_CFP_Count", new string[] { "flag", "ItemID" }, new string[] { "1", hdnvalue.Value }, "dataset");
        foreach (DataRow item in ds.Tables[0].Rows)
        {
            for (int i = 0; i < chkDS.Items.Count; i++)
            {
                if (Convert.ToInt16(chkDS.Items[i].Value) == Convert.ToInt16(item["DSID"])) { chkDS.Items[i].Selected = true; chkDS.Items[i].Enabled = false; break; }

            }
        }
        GC.SuppressFinalize(objdb);
        GC.SuppressFinalize(ds);

    }
    protected void btnimplement_Click(object sender, EventArgs e)
    {
        ds = new DataSet();
        ds = objdb.ByProcedure("sp_Item_Implementation_cfp_Insert", new string[] { "flag", "str" }, new string[] { "0", Convert.ToString(GetXML()) }, "dataset");
        if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "OK")
        {
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Success !", "Thank You! Items implemented Successfully Completed ");
            // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Thank You! Operation Successfully Completed');", true);

            btnimplement.Visible = false;
            Fillgrd();
        }

    }
    protected void btneditimplementation_Click(object sender, EventArgs e)
    {
        ds = new DataSet();
        ds = objdb.ByProcedure("sp_Item_Implementated_CFP_Update", new string[] { "flag", "str" }, new string[] { "0", Convert.ToString(GetEditXML()) }, "dataset");
        if (ds.Tables[0].Rows[0]["ErrorMsg"].ToString() == "OK")
        {
            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Success !", "Thank You! Research Details implemented Successfully Completed ");
            // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Thank You! Operation Successfully Completed');", true);

            btnimplement.Visible = false;
            Fillgrd();
        }

    }
    private string GetXML()
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

        foreach (GridViewRow li in chkoffice.Rows)
        {
            CheckBox chk = (CheckBox)li.FindControl("CheckBox1");
            HiddenField hdnoffice = (HiddenField)li.FindControl("hdnDS");
            if (chk.Checked)
            {
                xw.WriteStartElement("ROW");
                xw.WriteAttributeString("ItemID", hdnvalue.Value);
                xw.WriteAttributeString("CFPID", hdnoffice.Value);
                xw.WriteAttributeString("Office_ID", objdb.Office_ID());
                //selectedValues.Add(li.Value);
                xw.WriteEndElement();
            }
        }
        xw.WriteEndDocument();
        xw.Flush();
        xw.Close();
        return sb.ToString();
    }
    private string GetEditXML()
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

        bool foud = false;
        foreach (ListItem li in chkDS.Items)
        {
            if (li.Selected)
            {

                xw.WriteStartElement("ROW");
                xw.WriteAttributeString("ItemID", hdnvalue.Value);
                xw.WriteAttributeString("CFPID", li.Value);
                xw.WriteAttributeString("Office_ID", objdb.Office_ID());
                //selectedValues.Add(li.Value);
                xw.WriteEndElement();

            }

        }


        xw.WriteEndDocument();
        xw.Flush();
        xw.Close();
        return sb.ToString();
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void grdCatlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCatlist.PageIndex = e.NewPageIndex;
        Fillgrd();
    }
}