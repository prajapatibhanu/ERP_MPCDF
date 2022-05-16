using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web;

public partial class mis_MilkCollection_DeleteRMRDChallanEntry : System.Web.UI.Page
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
                txtDate.Text = Convert.ToDateTime(DateTime.Now, cult).ToString("dd/MM/yyyy");
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                GetCCDetails();
                //FillBMCRoot();

            }
        }
        else
        {
            objdb.redirectToHome();
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
                        ddlCC.DataTextField = "Office_Name";
                        ddlCC.DataValueField = "Office_ID";
                        ddlCC.DataSource = ds;
                        ddlCC.DataBind();
                        ddlCC.Items.Insert(0, new ListItem("Select", "0"));

                        //ddlFltrCC.DataTextField = "Office_Name";
                        //ddlFltrCC.DataValueField = "Office_ID";
                        //ddlFltrCC.DataSource = ds;
                        //ddlFltrCC.DataBind();
                        //ddlFltrCC.Items.Insert(0, new ListItem("Select", "0"));
                        //if (objdb.OfficeType_ID() == "4" || objdb.OfficeType_ID() == "5")
                        //{
                        //    ddlCC.SelectedValue = objdb.Office_ID();
                        //    ddlFltrCC.SelectedValue = objdb.Office_ID();
                        //}

                    }
                }
            }



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //public void FillBMCRoot()
    //{

    //    try
    //    {
    //        ds = null;
    //        ds = objdb.ByProcedure("USP_Mst_BMCTankerRootMapping",
    //                  new string[] { "flag", "Office_ID", "OfficeType_ID" },
    //                  new string[] { "11",objdb.Office_ID(),objdb.OfficeType_ID() }, "dataset");
    //        if (ds.Tables[0].Rows.Count != 0)
    //        {
    //            ddlBMCTankerRootName.DataTextField = "BMCTankerRootName";
    //            ddlBMCTankerRootName.DataValueField = "BMCTankerRoot_Id";
    //            ddlBMCTankerRootName.DataSource = ds;
    //            ddlBMCTankerRootName.DataBind();
    //            ddlBMCTankerRootName.Items.Insert(0, new ListItem("Select", "0"));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());
    //    }

    //}
    protected void FillSociety()
    {
        try
        {
            if (ddlCC.SelectedValue != "0")
            {
                ds = null;
                ds = objdb.ByProcedure("Usp_MilkCollectionRoutWiseAdditionsDeductionsEntry",
                         new string[] { "flag", "Office_ID", "Office_Parant_ID", "OfficeType_ID" },
                         new string[] { "18", ddlCC.SelectedValue.ToString(), objdb.Office_ID(), objdb.OfficeType_ID() }, "dataset");

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddlSociety.DataTextField = "Office_Name";
                        ddlSociety.DataValueField = "Office_ID";
                        ddlSociety.DataSource = ds.Tables[0];
                        ddlSociety.DataBind();
                        ddlSociety.Items.Insert(0, new ListItem("Select", "0"));


                    }
                    else
                    {
                        ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                    }
                }
                else
                {
                    ddlSociety.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                Response.Redirect("DeleteRMRDChallanEntry.aspx", false);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlBMCTankerRootName_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSociety();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void FillGrid()
    {
        try
        {
            btnDelete.Visible = false;

            string Date = Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd");

            ds = objdb.ByProcedure("Usp_DeleteChallanEntry", new string[] { "flag", "Date", "Office_ID" }, new string[] { "3", Date, ddlSociety.SelectedValue}, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnDelete.Visible = true;
                    gv_MilkCollectionChallanEntryDetails.DataSource = ds;
                    gv_MilkCollectionChallanEntryDetails.DataBind();
                   

                    

                }
                else
                {
                    btnDelete.Visible = false;
                    gv_MilkCollectionChallanEntryDetails.DataSource = string.Empty;
                    gv_MilkCollectionChallanEntryDetails.DataBind();
                }
            }
            else
            {
                btnDelete.Visible = false;
                gv_MilkCollectionChallanEntryDetails.DataSource = string.Empty;
                gv_MilkCollectionChallanEntryDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.StackTrace + " - " + ex.Message.ToString());

        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string MilkCollectionChallan_ID = "";
        foreach(GridViewRow row in gv_MilkCollectionChallanEntryDetails.Rows)
        {
            Label lblMilkCollectionChallan_ID = (Label)row.FindControl("lblMilkCollectionChallan_ID");
            MilkCollectionChallan_ID += lblMilkCollectionChallan_ID.Text + ",";
           
        }
        objdb.ByProcedure("Usp_DeleteChallanEntry", new string[] { "flag", "MilkCollectionChallan_ID_Mlt" }, new string[] { "4", MilkCollectionChallan_ID }, "dataset");
        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thankyou!", "Record Deleted Successfully");
        btnSearch_Click(sender, e);
    }
    protected void ddlCC_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSociety();
    }
}