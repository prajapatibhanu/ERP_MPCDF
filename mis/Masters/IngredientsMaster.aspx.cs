using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;

public partial class mis_Masters_IngredientsMaster : System.Web.UI.Page
{

    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (objdb.createdBy() != null && objdb.Office_ID() != null)
        {
            if (!Page.IsPostBack)
            {
               GetIngredientsDetails();
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

     private void GetIngredientsDetails()
    {
        try
        {

            ds = objdb.ByProcedure("USP_Mst_Ingredients",
                    new string[] { "flag", },
                    new string[] { "1",  }, "dataset");
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }



     private void Clear()
     {
         txtIngredient.Text = string.Empty;
         
     }


    private void InsertorUpdateIngredients()
    {
        if (Page.IsValid)
        {
            try
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    if (btnSubmit.Text == "Save")
                    {
                        lblMsg.Text = "";
                        ds = objdb.ByProcedure("USP_Mst_Ingredients",
                            new string[] { "flag", "IngredientsName", "CreatedBy_IP" },
                            new string[] { "2",txtIngredient.Text.Trim(),objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()}, "TableSave");

                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
                        {

                            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            GetIngredientsDetails();
                           Clear();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
                        }
                        else
                        {
                            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                            if (error == "Retailer Code Already Exists")
                            {
                                lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", error);
                            }
                            else
                            {
                                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
                            }
                        }
                        ds.Clear();
                    }
                   
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
            }
            catch (Exception ex)
            {
                lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
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
                    Label lblRoute_Number = (Label)row.FindControl("lblRoute_Number");



                    txtIngredient.Text = lblIngredients_Name.Text;
                   
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
                }

            }
            //if (e.CommandName == "RecordDelete")
            //{
            //    if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
            //    {
            //        lblMsg.Text = string.Empty;
            //        ViewState["rowid"] = e.CommandArgument;
            //        ds = objdb.ByProcedure("USP_Mst_Route",
            //                    new string[] { "flag", "RouteId", "CreatedBy", "CreatedBy_IP", "PageName", "Remark" },
            //                    new string[] { "4", ViewState["rowid"].ToString(), objdb.createdBy()
            //                        , objdb.GetLocalIPAddress() + ":" + objdb.GetMACAddress()
            //                        , Path.GetFileName(Request.Url.AbsolutePath)
            //                        , "Route Master Details Deleted" }, "TableSave");

            //        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "Ok")
            //        {
            //            string success = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
            //            //GetRouteMasterDetails();
            //            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Success :" + success);
            //        }
            //        else
            //        {
            //            string error = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
            //            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", "Error :" + error);
            //        }
            //        ds.Clear();
            //        Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            //    }


            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
protected void btnSubmit_Click(object sender, EventArgs e)
{
    InsertorUpdateIngredients();
}
protected void btnClear_Click(object sender, EventArgs e)
{

    Clear();
    lblMsg.Text = "";
}
}