using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class mis_CattelFeed_CFP_ItemCategory : System.Web.UI.Page
{
    APIProcedure objapi = new APIProcedure();
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        Fillgrd();
    }
    private void Fillgrd()
    {
        try
        {
            ds = new DataSet();
            ds = objapi.ByProcedure("SP_CFPCItemCategory_List", new string[] { "flag" }, new string[] { "0" }, "dataset");
            grdCatlist.DataSource = ds;
            grdCatlist.DataBind();
        }
        catch (Exception ex)
        {
            
            
        }
        finally
        {
            ds.Dispose();
        }
       
    }


    protected void grdCatlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Change":
                ds = new DataSet();
                ds = objapi.ByProcedure("SP_CFPCItemCategory_By_ItemCatID_List", new string[] { "flag", "ItemCatid" }, new string[] { "0", Convert.ToString(e.CommandArgument) }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdnvalue.Value = Convert.ToString(e.CommandArgument);
                    txtItem_Category.Text = ds.Tables[0].Rows[0]["ItemCatName"].ToString();
                    btnSave.Text = "Edit";
                }
                GC.SuppressFinalize(ds);
                GC.SuppressFinalize(objapi);
                break;
            case "inactive":
                ds = new DataSet();
                DataSet ds1 = new DataSet();
                ds1 = objapi.ByProcedure("CheckCFPItemCategoryByCatID",
          new string[] { "flag", "ItemCatID" },
          new string[] { "0", Convert.ToString(e.CommandArgument) }, "dataset");
                if (Convert.ToInt32(ds1.Tables[0].Rows[0]["NoofRecords"]) == 0)
                {
                    ds = objapi.ByProcedure("SP_CFPCItemCategory_Insert_Update_Delete",
       new string[] { "flag", "ItemCatName", "ItemCatID", "InsertedBy" },
       new string[] { "3", txtItem_Category.Text, Convert.ToString(e.CommandArgument), objapi.Office_ID() }, "dataset");
                    if (Convert.ToString(ds.Tables[0].Rows[0]["ErrorMsg"]) == "OK")
                    {
                        lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                    }
                    else
                    {
                        lblMsg.Text = objapi.Alert("fa-check", "alert-error", "Fail!", "Due to some technical issues. Operation couldn't completed.");
                    }
                }
                else
                {
                    lblMsg.Text = objapi.Alert("fa-check", "alert-error", "Fail!", "Category is used in Item Type. Operation couldn't completed.");
                }


                Fillgrd();
                GC.SuppressFinalize(ds);
                GC.SuppressFinalize(objapi);
                break;
            default:
                break;
        }
    }

    protected void grdCatlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        ds = new DataSet();
        string msg = "";
        if (txtItem_Category.Text == string.Empty)
        {
            msg += "Enter Item Group. \\n";
        }
        if (msg.Trim() == "")
        {
            string flagvalue = "0";
            if (Convert.ToInt32(hdnvalue.Value) > 0) { flagvalue = "1"; }
            ds = objapi.ByProcedure("SP_CFPCItemCategory_Insert_Update_Delete",
               new string[] { "flag", "ItemCatName", "ItemCatID", "InsertedBy" },
               new string[] { flagvalue, txtItem_Category.Text, hdnvalue.Value, objapi.Office_ID() }, "dataset");


            lblMsg.Text = objapi.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
            ClearText();
            Fillgrd();
        }
    }

    private void ClearText()
    {
        txtItem_Category.Text = string.Empty;
        lblMsg.Text = string.Empty;
        hdnvalue.Value = "0";
        btnSave.Text = "Save";
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        ClearText();
    }
}