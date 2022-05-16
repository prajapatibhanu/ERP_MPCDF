using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Drawing;

public partial class mis_Masters_SetProductRatio : System.Web.UI.Page
{

    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds = new DataSet();
    IFormatProvider culture = new CultureInfo("en-US", true);


    protected void Page_Load(object sender, EventArgs e)
    {


        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
                GetProdRatioDetails();
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
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



    protected void GetItem()
    {
        try
        {
          
                ddlProductName.DataTextField = "Abbreviation";
                ddlProductName.DataValueField = "ItemType_id";
                ddlProductName.DataSource = objdb.ByProcedure("USP_Mst_AdvanceCard",
                       new string[] { "flag", "ItemCat_id", "Office_ID" },
                       new string[] { "12", "2", objdb.Office_ID() }, "dataset");
                ddlProductName.DataBind();
                ddlProductName.Items.Insert(0, new ListItem("Select", "0"));
            

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
       
    }

    protected void GetItemFiltered()
    {
        try
        {

            ddlFilterProduct.DataTextField = "Abbreviation";
            ddlFilterProduct.DataValueField = "ItemType_id";
            ddlFilterProduct.DataSource = objdb.ByProcedure("USP_Mst_AdvanceCard",
                   new string[] { "flag", "ItemCat_id", "Office_ID" },
                   new string[] { "12", "2", objdb.Office_ID() }, "dataset");
            ddlFilterProduct.DataBind();
            ddlFilterProduct.Items.Insert(0, new ListItem("All", "0"));


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    } 
    protected void GetProdSpecificationDetails()
    {
        try
        {

            ds = objdb.ByProcedure("USP_Mst_ProductSpecification",
                    new string[] { "flag", "Prod_Id" },
                    new string[] { "5", ddlProductName.SelectedValue }, "dataset");
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }


    }

    protected void GetProdRatioDetails()
    {
        try
        {

            ds = objdb.ByProcedure("USP_Mst_SetProductRatio",
                    new string[] { "flag", "Prod_Id" },
                    new string[] { "1", ddlFilterProduct.SelectedValue }, "dataset");
            GridView2.DataSource = ds.Tables[0];
            GridView2.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }


    }

    private void InsertorUpdateIngredientsRatio()
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {

                    DataTable dt1 = new DataTable();
                    dt1 = getTableValues();

                    if (dt1.Rows.Count > 0)
                    {


                        ds = objdb.ByProcedure("USP_Mst_SetProductRatioSave",
                            new string[] { "Prod_id", "CreatedBy", "Office_ID", "CreatedBy_IP" },
                           new string[] { ddlProductName.SelectedValue, objdb.createdBy(), objdb.Office_ID(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "typeSetProductRatio", dt1, "TableSave");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {
                           
                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                           
                            pnlProduct.Visible = false;
                            GetProdRatioDetails();
                            ddlProductName.SelectedIndex = 0;

                            
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa fa-warning", "alert-warning", "Warning!",
                                     "Ratio Values are Empty");
                        

                    }

                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

            }

            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
            finally
            {
                if (ds != null) { ds.Dispose(); }
            }
        }
    }


    //private void InsertorUpdateIngredientsRatio()
    //{
    //    if (Page.IsValid)
    //    {
    //        try
    //        {
    //            if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
    //            {
    //                foreach (GridViewRow row in GridView1.Rows)
    //                {

    //                    Label lblProd_Id = (Label)row.FindControl("lblProd_Id");
    //                    Label lblIngredientsId = (Label)row.FindControl("lblIngredientsId");
    //                    Label lblUnit_ID = (Label)row.FindControl("lblUnit_ID");
    //                    TextBox gv_txtRatio = (TextBox)row.FindControl("gv_txtRatio");
    //                    if (btnSubmit.Text == "Save")
    //                    {
    //                        lblMsg.Text = "";

    //                        ds2 = objdb.ByProcedure("USP_Mst_SetProductRatio",
    //                    new string[] { "flag", "Office_ID", "Prod_Id", "IngredientsId","Unit_ID", "Ratio_Value", "CreatedBy", "CreatedBy_IP" },
    //                    new string[] { "2",  objdb.Office_ID(), lblProd_Id.Text,lblIngredientsId.Text,lblUnit_ID.Text,gv_txtRatio.Text,
    //                   objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress() }, "TableSave");

    //                        if (ds2.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
    //                        {
    //                            string success = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
    //                        }
    //                        else
    //                        {
    //                            string error = ds2.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //                            if (error == "Already Exist.")
    //                            {
    //                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Already exists!", error);
    //                            }
    //                            else
    //                            {
    //                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
    //                            }
    //                        }
    //                        ds2.Clear();

    //                    }

    //                    //if (btnSubmit.Text == "Update")
    //                    //{
    //                    //    lblMsg.Text = "";
    //                    //    ds = objdb.ByProcedure("USP_Mst_ProductSpecification",
    //                    //        new string[] { "flag","ProdSpec_id","IngredientsId","Prod_Id","Unit_ID"
    //                    //      ,"CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
    //                    //        new string[] { "3", ViewState["rowid"].ToString(),ddlNameofIngredients.SelectedValue,ddlProductName.SelectedValue
    //                    //       ,ddlItemRatio.SelectedValue
    //                    //       , objdb.createdBy(), objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress(),  Path.GetFileName(Request.Url.AbsolutePath), "Citizen registration record deleted"
    //                    //  }, "dataset");

    //                    //    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
    //                    //    {
    //                    //        string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //                    //        Clear();
    //                    //        GetProdSpecificationDetails();
    //                    //        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
    //                    //    }
    //                    //    else
    //                    //    {
    //                    //        string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
    //                    //        if (error == "Already Exists.")
    //                    //        {
    //                    //            lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error);
    //                    //        }
    //                    //        else
    //                    //        {
    //                    //            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
    //                    //        }
    //                    //    }
    //                    //    ds.Clear();
    //                    //}



    //                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //        }
    //        finally
    //        {
    //            if (ds != null) { ds.Dispose(); }
    //        }
    //    }
    //}



    private DataTable getTableValues()
    {
        DataTable dt = new DataTable();
        DataRow dr;
        dt.Columns.Add(new System.Data.DataColumn("IngredientsId", typeof(Int32)));
        dt.Columns.Add(new System.Data.DataColumn("Unit_ID", typeof(Int32)));
        dt.Columns.Add(new System.Data.DataColumn("Ratio_Value", typeof(string)));


        foreach (GridViewRow row in GridView1.Rows)
        {


            Label lblIngredientsId = (Label)row.FindControl("lblIngredientsId");
            Label lblUnit_ID = (Label)row.FindControl("lblUnit_ID");
            TextBox gv_txtRatio = (TextBox)row.FindControl("gv_txtRatio");
            if (gv_txtRatio.Text != "")
            {

            
            dr = dt.NewRow();

            dr[0] = lblIngredientsId.Text;
            dr[1] = lblUnit_ID.Text;
            dr[2] = gv_txtRatio.Text;

            dt.Rows.Add(dr);
            }
        }
        return dt;
    }
    protected void ddlProductName_Init(object sender, EventArgs e)
    {

        GetItem();
    }





    protected void ddlProductName_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlProduct.Visible = true;
        GetProdSpecificationDetails();
        lblMsg.Text = string.Empty;

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

      
        // lblMsg.Visible = false;
        InsertorUpdateIngredientsRatio();
      
            
    }


    protected void ddlFilterProduct_Init(object sender, EventArgs e)
    {
       GetItemFiltered();
    }
    protected void ddlFilterProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetProdRatioDetails();
        lblMsg.Text = string.Empty;

    }





    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;

       

    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblIngredientsName = e.Row.FindControl("lblIngredientsName") as Label;
                Label lblUnitName = e.Row.FindControl("lblUnitName") as Label;




            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error1 : " + ex.Message.ToString());
        }
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            lblMsg.Text = string.Empty;
            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;

                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;


                    Label lblIngredients_Name = (Label)row.FindControl("lblIngredients_Name");
                    Label lblProduct_Name = (Label)row.FindControl("lblProduct_Name");
                    Label lblUnitName = (Label)row.FindControl("lblUnitName");
                    Label lblProd_Id = (Label)row.FindControl("lblProd_Id");
                    Label lblIngredientsId = (Label)row.FindControl("lblIngredientsId");
                    Label lblUnit_ID = (Label)row.FindControl("lblUnit_ID");


                    ddlProductName.SelectedValue = lblProd_Id.Text;

                    ViewState["rowid"] = e.CommandArgument;

                    //btnSubmit.Text = "Update";

                    foreach (GridViewRow gvRow in GridView1.Rows)
                    {
                        if (GridView1.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            GridView1.SelectedIndex = gvRow.DataItemIndex;
                            GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                }




            }
        }
        catch
        {

        }
    }


    protected void GridVie2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            lblMsg.Text = string.Empty;
            if (e.CommandName == "RecordUpdate")
            {
                Control ctrl = e.CommandSource as Control;

                if (ctrl != null)
                {
                    GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;


                    Label lblIngredients_Name = (Label)row.FindControl("lblIngredients_Name");
                    Label lblProduct_Name = (Label)row.FindControl("lblProduct_Name");
                    Label lblUnitName = (Label)row.FindControl("lblUnitName");
                    Label lblProd_Id = (Label)row.FindControl("lblProd_Id");
                    Label lblIngredientsId = (Label)row.FindControl("lblIngredientsId");
                    Label lblUnit_ID = (Label)row.FindControl("lblUnit_ID");


                    ddlProductName.SelectedValue = lblProd_Id.Text;

                    ViewState["rowid"] = e.CommandArgument;

                    //btnSubmit.Text = "Update";

                    foreach (GridViewRow gvRow in GridView1.Rows)
                    {
                        if (GridView1.DataKeys[gvRow.DataItemIndex].Value.ToString() == e.CommandArgument.ToString())
                        {
                            GridView1.SelectedIndex = gvRow.DataItemIndex;
                            GridView1.SelectedRowStyle.BackColor = System.Drawing.Color.LightBlue;
                            break;
                        }
                    }
                }




            }
        }
        catch
        {

        }
    }

}